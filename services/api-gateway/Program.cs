using System.Text;
using ApiGateway.Configuration;
using ApiGateway.Middleware;
using ApiGateway.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using Polly;
using Polly.Extensions.Http;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Configure NLog
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Information);
builder.Host.UseNLog();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Configure ServiceUrls
var serviceUrls = builder.Configuration.GetSection("ServiceUrls").Get<ServiceUrls>() ?? new ServiceUrls();
builder.Services.Configure<ServiceUrls>(builder.Configuration.GetSection("ServiceUrls"));

// Configure CORS
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
// Secret key should come from environment variables or user secrets, not from config file
var secretKey = builder.Configuration["JWT_SECRET_KEY"]
    ?? Environment.GetEnvironmentVariable("JWT_SECRET_KEY")
    ?? jwtSettings["SecretKey"]
    ?? throw new InvalidOperationException("JWT SecretKey is not configured. Set JWT_SECRET_KEY environment variable or use User Secrets.");
var issuer = jwtSettings["Issuer"] ?? throw new InvalidOperationException("JWT Issuer is not configured");
var audience = jwtSettings["Audience"] ?? throw new InvalidOperationException("JWT Audience is not configured");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = jwtSettings.GetValue<bool>("ValidateIssuer"),
            ValidateAudience = jwtSettings.GetValue<bool>("ValidateAudience"),
            ValidateLifetime = jwtSettings.GetValue<bool>("ValidateLifetime"),
            ValidateIssuerSigningKey = jwtSettings.GetValue<bool>("ValidateIssuerSigningKey"),
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

builder.Services.AddAuthorization();

// Configure Polly policies for resilience
// Retry policy can be shared (stateless)
var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

// Each service needs its own circuit breaker (stateful) to isolate failures
var authCircuitBreaker = HttpPolicyExtensions
    .HandleTransientHttpError()
    .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

var botDetectionCircuitBreaker = HttpPolicyExtensions
    .HandleTransientHttpError()
    .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

var vkCircuitBreaker = HttpPolicyExtensions
    .HandleTransientHttpError()
    .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

var okCircuitBreaker = HttpPolicyExtensions
    .HandleTransientHttpError()
    .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

// Configure Refit clients with Polly
builder.Services.AddRefitClient<IAuthServiceClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(serviceUrls.AuthServiceUrl))
    .AddPolicyHandler(retryPolicy)
    .AddPolicyHandler(authCircuitBreaker);

builder.Services.AddRefitClient<IBotDetectionServiceClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(serviceUrls.BotDetectionServiceUrl))
    .AddPolicyHandler(retryPolicy)
    .AddPolicyHandler(botDetectionCircuitBreaker);

builder.Services.AddRefitClient<IVkServiceClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(serviceUrls.VkServiceUrl))
    .AddPolicyHandler(retryPolicy)
    .AddPolicyHandler(vkCircuitBreaker);

builder.Services.AddRefitClient<IOkServiceClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(serviceUrls.OkServiceUrl))
    .AddPolicyHandler(retryPolicy)
    .AddPolicyHandler(okCircuitBreaker);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Add error handling middleware early in pipeline to catch all exceptions
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
