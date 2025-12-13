# Требования к микросервисам NeuroAdmin

## Обзор

Этот документ содержит четкие требования к созданию и реализации микросервисов в проекте NeuroAdmin на основе всей существующей документации.

**Версия:** 1.0  
**Дата создания:** 13 декабря 2025

---

## 📋 Общие требования к архитектуре

### Принципы

1. **Микросервисная архитектура** - каждый сервис независим и выполняет одну бизнес-функцию
2. **MVC паттерн** - все API сервисы используют ASP.NET Core MVC
3. **Гибридная коммуникация:**
   - HTTP REST для синхронных операций (получение статуса, результатов, управление)
   - RabbitMQ для асинхронных операций (обработка задач, фоновые процессы)
4. **API Gateway (BFF)** - единая точка входа для фронтенда, проксирование запросов к сервисам

### Технологический стек

**Backend:**

- .NET 9.0 (TargetFramework)
- ASP.NET Core MVC
- PostgreSQL 15+ (база данных)
- Dapper (микро-ORM)
- NLog (логирование)
- Refit (декларативные HTTP клиенты)
- Polly (resilience patterns: retry, circuit breaker)
- RabbitMQ 3.12 (очереди сообщений)
- Redis 7 (кэширование)

**Frontend:**

- React 18
- TypeScript 5
- Vite (сборщик)
- Redux Toolkit (управление состоянием)
- Material-UI (UI компоненты)
- React Hook Form + Zod (формы и валидация)
- Axios (HTTP клиент)

---

## 🏗️ Структурные требования

### Обязательная структура сервиса

```
service-name/
├── service-name.csproj           # Проект файл
├── Program.cs                    # Точка входа
├── appsettings.json              # Конфигурация
├── appsettings.Development.json  # Development конфигурация
├── nlog.config                   # NLog конфигурация (если используется)
├── Properties/
│   └── launchSettings.json       # Настройки запуска
├── Controllers/                  # API контроллеры
│   └── [ControllerName]Controller.cs
├── Services/                     # Бизнес-логика
│   ├── I[ServiceName]Service.cs # Интерфейс
│   └── [ServiceName]Service.cs  # Реализация
├── Configuration/                # Классы конфигурации
│   └── [ConfigClassName].cs
├── Middleware/                   # Middleware
│   └── ErrorHandlingMiddleware.cs
├── Logging/                      # LoggerMessage делегаты
│   └── LoggerDefinitions.cs
├── Models/                       # Модели данных (DTOs, Entities)
│   ├── Requests/
│   └── Responses/
├── Repositories/                 # Репозитории (если используется БД)
│   └── [RepositoryName].cs
├── Migrations/                   # Миграции БД (если используется)
│   └── [MigrationName].cs
└── Workers/                      # Background services (если используется RabbitMQ)
    └── [WorkerName].cs
```

### Обязательная структура тестового проекта

```
service-name-tests/
├── service-name.Tests.csproj
├── Controllers/
│   └── [ControllerName]Tests.cs
├── Services/
│   └── [ServiceName]Tests.cs
└── README.md
```

---

## 📝 Требования к коду

### Конвенции именования

#### Namespace (RootNamespace в .csproj)

- **Формат:** PascalCase БЕЗ подчеркиваний
- **Примеры:** `AuthService`, `BotDetectionService`, `TaskManagementService`
- **НЕ использовать:** `auth_service`, `auth-service`, `Auth_Service`

#### Имена папок

- **Формат:** kebab-case
- **Примеры:** `api-gateway`, `auth-service`, `bot-detection-service`

#### Классы и интерфейсы

- **Классы:** PascalCase (`AuthController`, `UserService`)
- **Интерфейсы:** `I` + PascalCase (`IAuthService`, `IUserRepository`)
- **Приватные поля:** `_` + camelCase (`_authService`, `_logger`)
- **Public static readonly (LoggerMessage):** PascalCase БЕЗ `_` (`LogRegisterError`)
- **Методы:** PascalCase (`GetUserAsync`, `CreateTaskAsync`)

### Обязательные настройки .csproj

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <RootNamespace>ServiceName</RootNamespace>
    <!-- Остальные настройки из Directory.Build.props -->
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="9.0.3" />
    <PackageReference Include="NLog.Web.AspNetCore" Version="6.1.0" />
    <!-- Дополнительные пакеты по необходимости -->
  </ItemGroup>
</Project>
```

**КРИТИЧНО:**

- RootNamespace должен быть PascalCase
- Используется `Microsoft.NET.Sdk.Web` для API сервисов
- Все общие настройки берутся из `Directory.Build.props`

### Обязательные настройки Program.cs

1. Configure NLog
2. Add services (Controllers, OpenAPI)
3. Configure ServiceUrls (если нужно)
4. Configure CORS (если нужно)
5. Configure JWT (если нужно)
6. Configure Polly/Refit клиенты (если нужно)
7. Register application services
8. Configure middleware pipeline (ErrorHandlingMiddleware должен быть первым)

### Требования к контроллерам

- Использовать file-scoped namespaces
- Всегда использовать dependency injection
- Всегда логировать ошибки через `LoggerDefinitions` из папки `Logging/`
- Использовать async/await для всех операций
- Возвращать `ObjectResult` из методов `HandleRefitResponse` (для производительности)
- Добавлять XML комментарии для публичных методов

### Требования к логированию

- **ВСЕГДА** использовать централизованные LoggerMessage делегаты из `Logging/LoggerDefinitions.cs`
- **НЕ создавать** новые делегаты в контроллерах/сервисах - добавлять их в LoggerDefinitions.cs
- Использовать правильные диапазоны EventId:
  - 1000-1999: Auth Service
  - 2000-2999: Bot Detection Service
  - 3000-3999: Social Accounts
  - 4000-4999: Task Management Service
  - 5000-5999: VK Service
  - 6000-6999: OK Service
  - 9000-9999: Common/Middleware

Формат делегата:

```csharp
public static readonly Action<ILogger, Exception> LogOperationError =
    LoggerMessage.Define(
        LogLevel.Error,
        new EventId(XXXX, "OperationError"),
        "Error occurred during operation");
```

### Требования к обработке ошибок

- Всегда использовать ErrorHandlingMiddleware (если есть) или обрабатывать ошибки в контроллерах
- Использовать try-catch блоки для всех операций
- Логировать все ошибки через LoggerDefinitions
- Возвращать понятные сообщения об ошибках пользователю
- Использовать правильные HTTP статус коды

---

## 🔌 Требования к интеграции

### Использование Refit для вызовов других сервисов

1. Создать Refit интерфейс в папке `Services/`
2. Имя: `I[ServiceName]Client`
3. Использовать атрибуты Refit: `[Get]`, `[Post]`, `[Header]`, `[Body]`
4. Зарегистрировать в Program.cs с Polly политиками (retry + circuit breaker)

Пример:

```csharp
public interface IAnotherServiceClient
{
    [Get("/api/resource/{id}")]
    Task<IApiResponse<object>> GetResourceAsync(int id, [Header("Authorization")] string? authorization);
}
```

### Регистрация в Program.cs

```csharp
var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

builder.Services.AddRefitClient<IAnotherServiceClient>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(serviceUrls.AnotherServiceUrl))
    .AddPolicyHandler(retryPolicy)
    .AddPolicyHandler(HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
```

---

## 🗄️ Требования к базе данных

### Миграции

- Использовать FluentMigrator для миграций
- Все миграции в папке `Migrations/`
- Именование: `[Number]_[Description].cs`
- Каждая миграция должна быть обратимой (Up/Down)

### Работа с БД

- Использовать Dapper для работы с БД
- Все SQL запросы должны быть параметризованы (защита от SQL injection)
- Использовать транзакции для операций, изменяющих несколько таблиц
- Логировать все операции с БД

---

## 📡 Требования к RabbitMQ

### Конфигурация

- Настроить exchanges и queues в Program.cs
- Использовать topic или direct exchanges
- Настроить retry механизм для failed messages
- Настроить dead letter queue для обработки ошибок

### Background Workers

- Все background workers должны наследоваться от `BackgroundService`
- Обрабатывать ошибки и логировать их
- Использовать cancellation tokens для graceful shutdown
- Сохранять checkpoint'ы для длительных операций

---

## 🔐 Требования к безопасности

### JWT Authentication

- Все защищенные endpoints должны использовать `[Authorize]` атрибут
- JWT токены должны передаваться в заголовке `Authorization: Bearer {token}`
- Валидация токенов должна быть настроена в Program.cs
- Использовать refresh tokens для обновления access tokens

### CORS

- Настроить CORS для фронтенда
- Разрешенные origins должны быть в appsettings.json
- Использовать `AllowCredentials` для работы с cookies

### Валидация данных

- Все входные данные должны быть валидированы
- Использовать FluentValidation или Data Annotations
- Возвращать понятные сообщения об ошибках валидации

---

## 📊 Требования к конфигурации

### appsettings.json

Должен включать:

- Logging секцию
- AllowedHosts
- ServiceUrls (если сервис вызывает другие)
- Jwt (если требуется аутентификация)
- Cors (если нужно)
- ConnectionStrings (если используется БД)
- RabbitMQ connection (если используется)
- Redis connection (если используется)

### launchSettings.json

- http профиль: `http://localhost:5XXX`
- https профиль: `https://localhost:7XXX;http://localhost:5XXX`

### Порты для сервисов

- API Gateway: 5220 (http), 7191 (https)
- Auth Service: 5001
- Bot Detection Service: 5002
- VK Service: 5003
- OK Service: 5004
- Task Management Service: 5005
- Новые сервисы: начиная с 5006

---

## 🧪 Требования к тестированию

### Тестовый проект

- Имя: `service-name-tests`
- Проект файл: `service-name.Tests.csproj`
- RootNamespace: `ServiceName.Tests`
- Использовать xUnit, Moq, FluentAssertions

### Покрытие тестами

- Unit тесты для всех сервисов
- Integration тесты для контроллеров
- Тесты для критических бизнес-логик
- Минимальное покрытие: 60%

---

## 📚 Требования к документации

### Обязательная документация

1. **README.md** в корне сервиса:

   - Описание сервиса
   - API endpoints
   - Инструкции по запуску
   - Конфигурация

2. **XML комментарии** для всех публичных классов и методов

3. **Swagger/OpenAPI** документация (автоматически генерируется)

---

## 🔄 Требования к обновлению зависимостей

После создания нового сервиса:

1. Добавить URL в `api-gateway/Configuration/ServiceUrls.cs`
2. Обновить `api-gateway/appsettings.json`
3. Добавить Refit клиент в `api-gateway/Program.cs` (если вызывается из gateway)
4. Добавить проект в `NeuroAdminApp.sln`

---

## ✅ Чеклист создания нового сервиса

- [ ] Создана структура папок согласно шаблону
- [ ] Создан .csproj файл с правильным RootNamespace (PascalCase)
- [ ] Создан Program.cs с необходимой конфигурацией
- [ ] Создан appsettings.json и appsettings.Development.json
- [ ] Создан Properties/launchSettings.json с уникальным портом
- [ ] Созданы контроллеры (если API сервис)
- [ ] Созданы сервисы с интерфейсами
- [ ] Создан LoggerDefinitions.cs в папке Logging
- [ ] Создан ErrorHandlingMiddleware (если нужно)
- [ ] Создан проект тестов с правильной структурой
- [ ] Добавлен в Solution файл
- [ ] Обновлен ServiceUrls в api-gateway (если сервис вызывается из gateway)
- [ ] Обновлен appsettings.json в api-gateway (если нужно)
- [ ] Создан README.md с документацией
- [ ] Добавлены XML комментарии для публичных методов
- [ ] Настроена интеграция с другими сервисами (если нужно)
- [ ] Настроена работа с БД (если нужно)
- [ ] Настроена работа с RabbitMQ (если нужно)
- [ ] Настроена работа с Redis (если нужно)

---

## 🎯 Функциональные требования по типам сервисов

### API Gateway Service

**Обязательные функции:**

- Проксирование всех запросов к соответствующим сервисам
- JWT валидация для защищенных endpoints
- Обработка ошибок и retry логика
- CORS настройка
- Логирование всех запросов

**Endpoints:**

- `/api/auth/*` → Auth Service
- `/api/bot-detection/*` → Bot Detection Service
- `/api/vk/*` → VK Service
- `/api/ok/*` → OK Service

### Auth Service

**Обязательные функции:**

- Регистрация пользователей
- Вход в систему
- Генерация и валидация JWT токенов
- Обновление токенов (refresh token)
- Восстановление пароля
- Управление профилем пользователя

**Endpoints:**

- `POST /api/auth/register`
- `POST /api/auth/login`
- `POST /api/auth/refresh`
- `POST /api/auth/logout`
- `GET /api/account/me`
- `PUT /api/account/profile`

### Bot Detection Service

**Обязательные функции:**

- Создание задачи очистки от ботов
- Получение статуса задачи
- Получение результатов анализа
- Асинхронная обработка через RabbitMQ
- Стратегии определения ботов

**Endpoints:**

- `POST /api/bot-detection/analyze`
- `GET /api/bot-detection/analyses`
- `GET /api/bot-detection/analyses/{id}`
- `GET /api/bot-detection/analyses/{id}/results`
- `DELETE /api/bot-detection/analyses/{id}`

### VK Service / OK Service

**Обязательные функции:**

- Получение профиля пользователя
- Получение подписчиков
- Получение друзей
- OAuth авторизация
- Управление токенами доступа
- Rate limiting
- Кэширование в Redis

**Endpoints:**

- `GET /api/vk/users/{userId}/profile`
- `GET /api/vk/users/{userId}/followers`
- `GET /api/vk/users/{userId}/friends`

---

## 🚨 Критические правила

1. **ВСЕГДА** используй PascalCase для namespace (RootNamespace)
2. **НИКОГДА** не используй подчеркивания в namespace
3. **ВСЕГДА** используй LoggerDefinitions для логирования ошибок
4. **ВСЕГДА** обрабатывай исключения в контроллерах
5. **ВСЕГДА** используй async/await для асинхронных операций
6. **ВСЕГДА** добавляй XML комментарии для публичных классов/методов
7. **ВСЕГДА** следуй структуре папок из шаблона
8. **ВСЕГДА** создавай тестовый проект параллельно с основным сервисом
9. **ВСЕГДА** используй ErrorHandlingMiddleware для обработки ошибок
10. **ВСЕГДА** валидируй входные данные

---

## 📖 Связанные документы

- [Шаблон создания сервисов](SERVICE_TEMPLATE.md) - детальный шаблон
- [Правила создания сервисов](.cursor/rules/service_creation_rules.mdc) - краткие правила
- [Юзеркейсы](USE_CASES.md) - функциональные требования
- [План разработки](DEVELOPMENT_PLAN.md) - технический план
- [Roadmap](ROADMAP.md) - дорожная карта проекта
- [Дизайн-система](DESIGN_SYSTEM_USAGE.md) - требования к UI

---

**Последнее обновление:** 13 декабря 2025
