# Шаблон создания микросервисов в NeuroAdmin

Этот документ описывает единый формат и стандарты для создания новых микросервисов в проекте NeuroAdmin.

---

## 📋 Общая структура сервиса

Каждый микросервис должен следовать следующей структуре:

```
service-name/
├── service-name.csproj           # Проект файл
├── Program.cs                    # Точка входа
├── appsettings.json              # Конфигурация
├── appsettings.Development.json  # Development конфигурация
├── nlog.config                   # NLog конфигурация (если используется)
├── Properties/
│   └── launchSettings.json       # Настройки запуска
├── Controllers/                  # API контроллеры (если API сервис)
│   └── [ControllerName]Controller.cs
├── Services/                     # Бизнес-логика сервиса
│   └── I[ServiceName]Service.cs
├── Configuration/                # Классы конфигурации
│   └── [ConfigClassName].cs
├── Middleware/                   # Middleware (если используется)
│   └── [MiddlewareName]Middleware.cs
├── Logging/                      # LoggerMessage делегаты (если нужно)
│   └── LoggerDefinitions.cs
└── Models/                       # Модели данных (если нужно)
    └── [ModelName].cs
```

---

## 📝 Шаблон .csproj файла

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <!-- Directory.Build.props применяется автоматически -->

  <PropertyGroup>
    <!-- RootNamespace должен быть PascalCase: ServiceName вместо service-name -->
    <RootNamespace>ServiceName</RootNamespace>
  </PropertyGroup>

  <ItemGroup>
    <!-- Стандартные пакеты -->
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="9.0.3" />
    <PackageReference Include="NLog.Web.AspNetCore" Version="6.1.0" />

    <!-- Дополнительные пакеты (если нужны) -->
    <PackageReference Include="Refit" Version="9.0.2" />
    <PackageReference Include="Refit.HttpClientFactory" Version="9.0.2" />
    <PackageReference Include="Microsoft.Extensions.Http.Polly" Version="10.0.1" />
    <PackageReference Include="Polly" Version="8.6.5" />
    <PackageReference Include="Polly.Extensions.Http" Version="3.0.0" />
  </ItemGroup>

</Project>
```

**Важно:**

- Используется `Microsoft.NET.Sdk.Web` для API сервисов
- Все общие настройки (TargetFramework, Nullable, и т.д.) берутся из `Directory.Build.props`
- `RootNamespace` должен быть PascalCase

---

## 📝 Шаблон Program.cs

```csharp
using System.Text;
using ServiceName.Configuration;
using ServiceName.Middleware;
using ServiceName.Services;
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

// Configure ServiceUrls (если сервис вызывает другие сервисы)
var serviceUrls = builder.Configuration.GetSection("ServiceUrls").Get<ServiceUrls>() ?? new ServiceUrls();
builder.Services.Configure<ServiceUrls>(builder.Configuration.GetSection("ServiceUrls"));

// Configure CORS (если нужно)
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
if (allowedOrigins.Length > 0)
{
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
}

// Configure JWT Authentication (если сервис требует аутентификации)
var jwtSettings = builder.Configuration.GetSection("Jwt");
var secretKey = builder.Configuration["JWT_SECRET_KEY"]
    ?? Environment.GetEnvironmentVariable("JWT_SECRET_KEY")
    ?? jwtSettings["SecretKey"]
    ?? throw new InvalidOperationException("JWT SecretKey is not configured.");

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

// Configure Polly policies for resilience (если используются HTTP клиенты)
var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

// Configure Refit clients with Polly (если используются)
builder.Services.AddRefitClient<ISomeServiceClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(serviceUrls.SomeServiceUrl))
    .AddPolicyHandler(retryPolicy)
    .AddPolicyHandler(HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

// Register application services
builder.Services.AddScoped<ISomeService, SomeService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Add error handling middleware early in pipeline
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
```

---

## 📝 Шаблон контроллера

```csharp
using ServiceName.Services;
using Microsoft.AspNetCore.Mvc;

namespace ServiceName.Controllers;

/// <summary>
/// Контроллер для [описание функциональности]
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize] // Если требуется аутентификация
public class SomeController : ControllerBase
{
    private readonly ISomeService _someService;
    private readonly ILogger<SomeController> _logger;

    public SomeController(ISomeService someService, ILogger<SomeController> logger)
    {
        _someService = someService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var result = await _someService.GetAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            LoggerDefinitions.LogSomeError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SomeRequest request)
    {
        try
        {
            var result = await _someService.CreateAsync(request);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            LoggerDefinitions.LogSomeError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }
}
```

**Важно:**

- Используется file-scoped namespace
- Всегда используется dependency injection
- Всегда логируются ошибки через LoggerDefinitions
- Используется async/await для всех операций

---

## 📝 Шаблон appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ServiceUrls": {
    "SomeServiceUrl": "http://localhost:500X"
  },
  "Jwt": {
    "Issuer": "http://localhost:5001",
    "Audience": "http://localhost:5000",
    "SecretKey": "${SECRET_KEY}",
    "ValidateIssuer": true,
    "ValidateAudience": true,
    "ValidateLifetime": true,
    "ValidateIssuerSigningKey": true
  },
  "Cors": {
    "AllowedOrigins": ["http://localhost:5173", "http://localhost:3000"]
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=servicename_db;Username=postgres;Password=postgres"
  }
}
```

---

## 📝 Шаблон Properties/launchSettings.json

```json
{
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "profiles": {
    "http": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "http://localhost:500X",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "https": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "https://localhost:7XXX;http://localhost:500X",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

**Порты для сервисов:**

- API Gateway: 5220 (http), 7191 (https)
- Auth Service: 5001
- Bot Detection Service: 5002
- VK Service: 5003
- OK Service: 5004
- Task Management Service: 5005
- Новые сервисы: начиная с 5006

---

## 📝 Конвенции именования

### Namespace

- **Формат:** `ServiceName` (PascalCase, без подчеркиваний)
- **Пример:** `AuthService`, `BotDetectionService`, `TaskManagementService`
- **Не используйте:** `auth_service`, `auth-service`, `Auth_Service`

### Классы

- **Формат:** PascalCase
- **Пример:** `AuthController`, `UserService`, `DatabaseContext`

### Интерфейсы

- **Формат:** `I` + PascalCase
- **Пример:** `IAuthService`, `IUserRepository`, `IDatabaseContext`

### Приватные поля

- **Формат:** `_` + camelCase
- **Пример:** `_authService`, `_logger`, `_configuration`

### Public статические readonly поля (LoggerMessage делегаты)

- **Формат:** PascalCase (без префикса `_`)
- **Пример:** `LogRegisterError`, `LogLoginError`
- **Расположение:** В `Logging/LoggerDefinitions.cs`

### Методы

- **Формат:** PascalCase
- **Пример:** `GetUserAsync`, `CreateTaskAsync`, `ProcessRequest`

### Константы

- **Формат:** PascalCase
- **Пример:** `MaxRetryCount`, `DefaultTimeout`

---

## 📝 Структура папок и их назначение

### Controllers/

- ASP.NET Core контроллеры для API endpoints
- Каждый контроллер соответствует ресурсу или группе ресурсов
- Пример: `AuthController`, `TasksController`, `UsersController`

### Services/

- Бизнес-логика сервиса
- Интерфейсы и их реализации
- Пример: `IAuthService.cs`, `AuthService.cs`

### Configuration/

- Классы конфигурации из appsettings.json
- Пример: `ServiceUrls.cs`, `DatabaseOptions.cs`

### Middleware/

- ASP.NET Core middleware
- Пример: `ErrorHandlingMiddleware.cs`, `RequestLoggingMiddleware.cs`

### Logging/

- Централизованные LoggerMessage делегаты
- Файл: `LoggerDefinitions.cs`
- Используется для всех логов в сервисе

### Models/

- Модели данных (DTOs, Entities)
- Пример: `UserDto.cs`, `TaskEntity.cs`, `CreateTaskRequest.cs`

### Data/ (если используется БД)

- Контексты баз данных
- Репозитории
- Миграции

---

## 📝 Логирование

### Использование LoggerMessage делегатов

Всегда используйте централизованные LoggerMessage делегаты из `Logging/LoggerDefinitions.cs`:

```csharp
// В LoggerDefinitions.cs
public static readonly Action<ILogger, Exception> LogSomeError =
    LoggerMessage.Define(
        LogLevel.Error,
        new EventId(1001, "SomeError"),
        "Error occurred while processing some operation");

// В контроллере/сервисе
try
{
    // код
}
catch (Exception ex)
{
    LoggerDefinitions.LogSomeError(_logger, ex);
    // обработка ошибки
}
```

**EventId диапазоны:**

- 1000-1999: Auth Service
- 2000-2999: Bot Detection Service
- 3000-3999: Social Accounts (VK/OK)
- 4000-4999: Task Management Service
- 5000-5999: VK Service
- 6000-6999: OK Service
- 9000-9999: Common/Middleware

---

## 📝 Обработка ошибок

Всегда используйте ErrorHandlingMiddleware (если есть) или обрабатывайте ошибки в контроллерах:

```csharp
try
{
    // операция
}
catch (HttpRequestException ex)
{
    // Ошибка при вызове другого сервиса
    LoggerDefinitions.LogServiceCallError(_logger, ex);
    return StatusCode(502, new { error = "Service unavailable" });
}
catch (Exception ex)
{
    LoggerDefinitions.LogUnexpectedError(_logger, ex);
    return StatusCode(500, new { error = "Internal server error" });
}
```

---

## 📝 Тестирование

Для каждого сервиса создается проект тестов:

```
service-name-tests/
├── service-name.Tests.csproj
├── Controllers/
│   └── [ControllerName]Tests.cs
├── Services/
│   └── [ServiceName]Tests.cs
└── README.md
```

**Шаблон .csproj для тестов:**

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <RootNamespace>ServiceName.Tests</RootNamespace>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.12.0" />
    <PackageReference Include="xunit" Version="2.9.2" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.8.2" />
    <PackageReference Include="coverlet.collector" Version="6.0.2" />
    <PackageReference Include="Moq" Version="4.20.72" />
    <PackageReference Include="FluentAssertions" Version="8.8.0" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="9.0.0" />
  </ItemGroup>

  <ItemGroup>
    <Using Include="Xunit" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\service-name\service-name.csproj" />
  </ItemGroup>

</Project>
```

---

## 📝 Интеграция с другими сервисами

### Использование Refit для вызовов других сервисов

```csharp
using Refit;

namespace ServiceName.Services;

/// <summary>
/// Refit интерфейс для вызовов [Service Name]
/// </summary>
public interface IAnotherServiceClient
{
    [Get("/api/resource/{id}")]
    Task<IApiResponse<object>> GetResourceAsync(int id, [Header("Authorization")] string? authorization);

    [Post("/api/resource")]
    Task<IApiResponse<object>> CreateResourceAsync([Body] object request, [Header("Authorization")] string? authorization);
}
```

### Регистрация в Program.cs

```csharp
builder.Services.AddRefitClient<IAnotherServiceClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(serviceUrls.AnotherServiceUrl))
    .AddPolicyHandler(retryPolicy)
    .AddPolicyHandler(HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
```

---

## 📝 Добавление нового сервиса в Solution

После создания сервиса, добавьте его в `NeuroAdminApp.sln`:

```xml
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "service-name", "services\service-name\service-name.csproj", "{GUID}"
EndProject
```

И добавьте тестовый проект:

```xml
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "service-name.Tests", "services\service-name-tests\service-name.Tests.csproj", "{GUID}"
EndProject
```

---

## ✅ Чеклист создания нового сервиса

- [ ] Создана структура папок согласно шаблону
- [ ] Создан .csproj файл с правильным RootNamespace (PascalCase)
- [ ] Создан Program.cs с необходимой конфигурацией
- [ ] Создан appsettings.json и appsettings.Development.json
- [ ] Создан Properties/launchSettings.json с уникальным портом
- [ ] Созданы контроллеры (если API сервис)
- [ ] Созданы сервисы с интерфейсами
- [ ] Создан LoggerDefinitions.cs в папке Logging (если нужен)
- [ ] Создан проект тестов с правильной структурой
- [ ] Добавлен в Solution файл
- [ ] Обновлен ServiceUrls в api-gateway (если сервис вызывается из gateway)
- [ ] Обновлен appsettings.json в api-gateway (если нужно)
- [ ] Обновлена документация

---

## 📚 Дополнительные ресурсы

- [.editorconfig](/.editorconfig) - Правила форматирования кода
- [Directory.Build.props](/Directory.Build.props) - Общие настройки проектов
- [USE_CASES.md](/docs/USE_CASES.md) - Юзеркейсы системы
- [ROADMAP.md](/ROADMAP.md) - Дорожная карта проекта

---

**Последнее обновление:** 13 декабря 2025
