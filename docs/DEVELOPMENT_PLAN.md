# План разработки NeuroAdmin

## Обзор проекта

NeuroAdmin - AI-powered SMM платформа для автоматизации управления социальными медиа. Текущая задача: модуль очистки аудитории от ботов для VK и Одноклассников.

**Архитектура:** Микросервисы с MVC паттерном

**Коммуникация:** Гибридная (HTTP REST для синхронных операций, RabbitMQ для асинхронных)

**Технологический стек:**

- Backend: C# (.NET 8), ASP.NET Core MVC, Dapper, PostgreSQL, NLog
- Frontend: React 18, TypeScript 5, Vite, Redux Toolkit, Material-UI
- Infrastructure: PostgreSQL, Redis, RabbitMQ, Docker

---

## Структура микросервисов

```
NeuroAdminApp/
├── services/
│   ├── api-gateway/              # API Gateway (BFF)
│   ├── auth-service/             # Сервис аутентификации
│   ├── bot-detection-service/    # Сервис очистки от ботов
│   ├── vk-service/               # Интеграция с VK API
│   └── ok-service/               # Интеграция с OK API
├── frontend/                     # React + TypeScript
└── docker-compose.yml
```

---

## Область 1: API Gateway Service

### 1.1 Инициализация проекта

- Создать ASP.NET Core Web API проект `services/api-gateway`
- Настроить MVC
- Установить NuGet пакеты:
  - `Microsoft.AspNetCore.Authentication.JwtBearer`
  - `Refit` (для вызовов других сервисов)
  - `Polly` (resilience patterns)
  - `Polly.Extensions.Http`
  - `NLog.Web.AspNetCore`

### 1.2 Структура MVC

```
api-gateway/
├── Controllers/
│   ├── AuthController.cs         # Прокси к auth-service
│   ├── BotDetectionController.cs # Прокси к bot-detection-service
│   └── SocialAccountsController.cs # Прокси к vk/ok services
├── Services/
│   ├── IAuthServiceClient.cs     # Refit интерфейс
│   ├── IBotDetectionServiceClient.cs
│   ├── IVkServiceClient.cs
│   └── IOkServiceClient.cs
├── Middleware/
│   ├── JwtValidationMiddleware.cs # Проверка JWT токенов
│   └── ErrorHandlingMiddleware.cs
├── Configuration/
│   └── ServiceUrls.cs            # URLs других сервисов
└── Program.cs
```

### 1.3 Конфигурация

- `appsettings.json` с URLs всех сервисов:
  - AuthServiceUrl
  - BotDetectionServiceUrl
  - VkServiceUrl
  - OkServiceUrl
- Настроить CORS для фронтенда
- Настроить JWT middleware для валидации токенов (без генерации)
- Настроить NLog
- Настроить HttpClient с Polly для retry и circuit breaker

### 1.4 Реализация

- Создать Refit интерфейсы для каждого сервиса
- Реализовать проксирование запросов в Controllers
- Настроить обработку ошибок и retry логику
- Настроить передачу JWT токенов между сервисами

---

## Область 2: Auth Service

### 2.1 Инициализация проекта

- Создать ASP.NET Core Web API проект `services/auth-service`
- Настроить MVC
- Установить пакеты:
  - `Npgsql`, `Dapper`, `Dapper.Contrib`
  - `BCrypt.Net-Next`
  - `Microsoft.AspNetCore.Authentication.JwtBearer`
  - `System.IdentityModel.Tokens.Jwt`
  - `FluentValidation.AspNetCore`
  - `NLog.Web.AspNetCore`
  - `FluentMigrator.Runner.Postgres`
  - `MailKit` (для email)

### 2.2 Структура MVC

```
auth-service/
├── Controllers/
│   ├── AuthController.cs
│   └── AccountController.cs
├── Models/
│   ├── Requests/
│   │   ├── RegisterRequest.cs
│   │   ├── LoginRequest.cs
│   │   ├── RefreshTokenRequest.cs
│   │   └── PasswordResetRequest.cs
│   └── Responses/
│       ├── AuthResponse.cs
│       └── UserResponse.cs
├── Services/
│   ├── AuthService.cs
│   ├── TokenService.cs
│   ├── PasswordService.cs
│   └── EmailService.cs
├── Repositories/
│   ├── UserRepository.cs
│   └── RefreshTokenRepository.cs
├── Middleware/
│   └── ErrorHandlingMiddleware.cs
├── Migrations/
│   └── 001_InitialSchema.cs
└── Program.cs
```

### 2.3 Database Schema

- Миграция для `Users`:
  - Id (Guid, PK), Email (unique), PasswordHash, FirstName, LastName
  - Role (int), Status (int), EmailVerified (bool)
  - CreatedAt, UpdatedAt, LastLoginAt
- Миграция для `RefreshTokens`:
  - Id, UserId (FK), Token (unique), ExpiresAt, CreatedAt, RevokedAt
- Миграция для `EmailVerificationTokens`:
  - Id, UserId (FK), Token (unique), ExpiresAt, UsedAt
- Миграция для `PasswordResetTokens`:
  - Id, UserId (FK), Token (unique), ExpiresAt, UsedAt

### 2.4 Реализация

- `AuthController`:
  - `POST /api/auth/register` - регистрация
  - `POST /api/auth/login` - вход
  - `POST /api/auth/refresh` - обновление токена
  - `POST /api/auth/logout` - выход
  - `POST /api/auth/verify-email` - верификация email
  - `POST /api/auth/forgot-password` - запрос сброса пароля
  - `POST /api/auth/reset-password` - сброс пароля
- `AccountController`:
  - `GET /api/account/me` - текущий пользователь
  - `PUT /api/account/profile` - обновление профиля
  - `PUT /api/account/change-password` - смена пароля
- `AuthService` - бизнес-логика аутентификации
- `UserRepository` - доступ к данным через Dapper
- `TokenService` - генерация и валидация JWT
- `PasswordService` - хеширование паролей (BCrypt)
- Настроить JWT генерацию с настройками из appsettings.json

---

## Область 3: Bot Detection Service

### 3.1 Инициализация проекта

- Создать ASP.NET Core Web API проект `services/bot-detection-service`
- Настроить MVC
- Установить пакеты:
  - `Npgsql`, `Dapper`
  - `Refit` (для вызовов vk-service и ok-service)
  - `Polly`, `Polly.Extensions.Http`
  - `StackExchange.Redis` (кэширование)
  - `RabbitMQ.Client` (асинхронная обработка)
  - `NLog.Web.AspNetCore`
  - `FluentMigrator.Runner.Postgres`

### 3.2 Структура MVC

```
bot-detection-service/
├── Controllers/
│   └── BotDetectionController.cs
├── Models/
│   ├── Requests/
│   │   └── StartAnalysisRequest.cs
│   └── Responses/
│       ├── AnalysisResponse.cs
│       ├── AnalysisListResponse.cs
│       └── BotResultResponse.cs
├── Services/
│   ├── BotDetectionService.cs
│   ├── BotAnalysisStrategy.cs
│   ├── ProfileAnalysisStrategy.cs
│   ├── ActivityAnalysisStrategy.cs
│   ├── BehaviorAnalysisStrategy.cs
│   └── SocialNetworkClient.cs (абстракция для vk/ok)
├── Repositories/
│   ├── BotAnalysisRepository.cs
│   └── AnalysisResultRepository.cs
├── Workers/ (Background services)
│   └── BotAnalysisWorker.cs (обработка через RabbitMQ)
├── Messaging/
│   ├── IMessagePublisher.cs
│   ├── IMessageConsumer.cs
│   ├── RabbitMqPublisher.cs
│   └── RabbitMqConsumer.cs
├── Migrations/
│   └── 001_InitialSchema.cs
└── Program.cs
```

### 3.3 Database Schema

- Миграция для `BotAnalyses`:
  - Id (Guid, PK), UserId (Guid), SocialAccountId (Guid)
  - Status (int: Pending, InProgress, Completed, Failed)
  - StartedAt, CompletedAt, CreatedAt
- Миграция для `AnalysisResults`:
  - Id (Guid, PK), BotAnalysisId (FK), UserId (string из соцсети)
  - BotScore (decimal), IsBot (bool), Confidence (decimal)
  - AnalysisDetails (JSONB), CreatedAt

### 3.4 Реализация - Синхронные операции (HTTP REST)

- `BotDetectionController`:
  - `POST /api/bot-detection/analyze` - запуск анализа (публикует в RabbitMQ)
  - `GET /api/bot-detection/analyses` - список анализов пользователя
  - `GET /api/bot-detection/analyses/{id}` - детали анализа
  - `GET /api/bot-detection/analyses/{id}/results` - результаты анализа
  - `DELETE /api/bot-detection/analyses/{id}` - отмена/удаление анализа
- `BotDetectionService`:
  - `CreateAnalysisAsync()` - создание записи анализа
  - `GetAnalysisAsync()` - получение анализа
  - `GetAnalysisResultsAsync()` - получение результатов
  - `UpdateAnalysisStatusAsync()` - обновление статуса

### 3.5 Реализация - Асинхронные операции (RabbitMQ)

- `BotAnalysisWorker` (Background Service):
  - Читает сообщения из очереди `bot-analysis-requests`
  - Обновляет статус на "InProgress"
  - Вызывает VK/OK Service через HTTP для получения данных
  - Применяет стратегии определения ботов
  - Сохраняет результаты в БД
  - Обновляет статус на "Completed" или "Failed"
- `MessagePublisher` - публикация сообщений в RabbitMQ
- `MessageConsumer` - чтение сообщений из RabbitMQ
- Настроить очереди и exchanges в RabbitMQ

### 3.6 Стратегии определения ботов

- `ProfileAnalysisStrategy`:
  - Проверка наличия фото профиля
  - Анализ имени (стандартные имена)
  - Проверка описания профиля
- `ActivityAnalysisStrategy`:
  - Количество постов
  - Частота активности
  - Взаимодействия (лайки, комментарии)
- `BehaviorAnalysisStrategy`:
  - Паттерны подписок
  - Соотношение подписчиков/подписок
  - Подозрительные паттерны активности
- `CompositeStrategy` - комбинирует все стратегии и рассчитывает итоговый BotScore

### 3.7 Интеграция с VK/OK Services

- Создать HTTP клиенты (Refit) для вызовов vk-service и ok-service
- Методы:
  - `GetUserProfileAsync()` - получение профиля
  - `GetFollowersAsync()` - получение подписчиков
  - `GetFriendsAsync()` - получение друзей
- Обработка rate limits через Polly
- Кэширование в Redis для часто запрашиваемых данных

---

## Область 4: VK Service

### 4.1 Инициализация проекта

- Создать ASP.NET Core Web API проект `services/vk-service`
- Настроить MVC
- Установить пакеты:
  - `Refit` (для VK API)
  - `Polly`, `Polly.Extensions.Http`
  - `StackExchange.Redis` (кэширование)
  - `NLog.Web.AspNetCore`

### 4.2 Структура MVC

```
vk-service/
├── Controllers/
│   └── VkController.cs
├── Models/
│   ├── VkUserProfile.cs
│   ├── VkFollower.cs
│   ├── VkFriend.cs
│   └── VkApiResponse.cs
├── Services/
│   ├── VkApiService.cs
│   └── VkApiClient.cs (Refit интерфейс)
├── Configuration/
│   └── VkApiSettings.cs
└── Program.cs
```

### 4.3 Реализация

- `VkController`:
  - `GET /api/vk/users/{userId}/profile` - профиль пользователя
  - `GET /api/vk/users/{userId}/followers` - подписчики
  - `GET /api/vk/users/{userId}/friends` - друзья
  - `GET /api/vk/users/{userId}/subscribers` - подписки
- `VkApiService` - бизнес-логика работы с VK API
- `VkApiClient` - Refit интерфейс для вызовов VK API
- Обработка rate limits через Polly (retry, circuit breaker)
- Кэширование ответов в Redis (TTL настраиваемый)
- Обработка ошибок VK API

---

## Область 5: OK Service

### 5.1 Инициализация проекта

- Создать ASP.NET Core Web API проект `services/ok-service`
- Настроить MVC
- Установить пакеты (аналогично VK Service)

### 5.2 Структура MVC

```
ok-service/
├── Controllers/
│   └── OkController.cs
├── Models/
│   ├── OkUserProfile.cs
│   ├── OkFollower.cs
│   └── OkApiResponse.cs
├── Services/
│   ├── OkApiService.cs
│   └── OkApiClient.cs (Refit интерфейс)
├── Configuration/
│   └── OkApiSettings.cs
└── Program.cs
```

### 5.3 Реализация

- `OkController` - endpoints для OK API (аналогично VK)
- `OkApiService` - бизнес-логика работы с OK API
- `OkApiClient` - Refit интерфейс для вызовов OK API
- Rate limiting и кэширование (аналогично VK Service)

---

## Область 6: Frontend Setup & Infrastructure

### 6.1 Инициализация проекта

- Создать React проект с Vite в `frontend/`
- Настроить TypeScript конфигурацию
- Настроить структуру папок:
  ```
  frontend/src/
  ├── pages/
  ├── components/
  ├── services/
  ├── store/
  ├── hooks/
  └── shared/
  ```

- Установить зависимости:
  - React 18, React Router v6
  - Redux Toolkit, React-Redux
  - Axios
  - React Hook Form, Zod
  - Material-UI (@mui/material)
  - date-fns

### 6.2 Конфигурация

- Создать `.env` файлы:
  - `.env.development` - для разработки
  - `.env.production` - для production
- Настроить API client (Axios) с base URL API Gateway
- Настроить interceptors:
  - Добавление JWT токена в заголовки
  - Обработка 401 (автоматический logout)
  - Обработка ошибок
- Настроить автоматическое обновление токенов

### 6.3 State Management

- Настроить Redux store
- Создать `authSlice` для аутентификации
- Создать `botDetectionSlice` для модуля очистки от ботов
- Настроить persist middleware (опционально)

### 6.4 Shared Components

- Layout компоненты:
  - `Header` с навигацией и меню пользователя
  - `Sidebar` для навигации
  - `Footer`
- UI компоненты:
  - `Button`, `Input`, `Modal`, `Table`, `Card`
  - `LoadingSpinner`, `ErrorMessage`
- Создать `ProtectedRoute` HOC для защищенных маршрутов

---

## Область 7: Frontend Authentication

### 7.1 Auth Pages

- Создать `LoginPage.tsx` с формой входа
- Создать `RegisterPage.tsx` с формой регистрации
- Создать `ForgotPasswordPage.tsx`
- Создать `ResetPasswordPage.tsx`

### 7.2 Auth Components

- Создать `LoginForm.tsx` компонент
- Создать `RegisterForm.tsx` компонент
- Создать `ForgotPasswordForm.tsx`
- Создать `ResetPasswordForm.tsx`
- Добавить валидацию форм через Zod

### 7.3 Auth Services & Hooks

- Создать `authApi.ts` с API методами (вызовы к API Gateway)
- Создать `useAuth` hook для работы с аутентификацией
- Создать `useLogin` hook
- Создать `useRegister` hook
- Настроить сохранение токенов (localStorage или httpOnly cookies)
- Реализовать автоматический logout при истечении токена

### 7.4 Account Management

- Создать `AccountPage.tsx` для управления профилем
- Создать `ProfileForm.tsx` для редактирования профиля
- Создать `ChangePasswordForm.tsx`
- Создать `UserMenu.tsx` компонент в Header

---

## Область 8: Frontend Bot Detection Module

### 8.1 Bot Detection Pages

- Создать `BotDetectionPage.tsx` - главная страница модуля
- Создать `AnalysisDetailsPage.tsx` - детали анализа
- Создать `AnalysisResultsPage.tsx` - результаты анализа

### 8.2 Bot Detection Components

- Создать `AnalysisCard.tsx` - карточка анализа
- Создать `AnalysisList.tsx` - список анализов
- Создать `AnalysisProgress.tsx` - прогресс анализа
- Создать `BotList.tsx` - список найденных ботов
- Создать `StartAnalysisForm.tsx` - форма запуска анализа
- Создать `AnalysisFilters.tsx` - фильтры для результатов
- Создать `BotDetailsModal.tsx` - модальное окно с деталями бота

### 8.3 Bot Detection Services & Hooks

- Создать `botDetectionApi.ts` с API методами (вызовы к API Gateway)
- Создать `useBotDetection` hook
- Создать `useAnalysis` hook для работы с конкретным анализом
- Реализовать polling для обновления статуса анализа (каждые 5-10 секунд)
- Настроить автоматическое обновление при изменении статуса

### 8.4 Social Accounts Integration UI

- Создать компонент для подключения VK аккаунта
- Создать компонент для подключения OK аккаунта
- Создать список подключенных аккаунтов
- Создать форму выбора аккаунта для анализа

---

## Область 9: Infrastructure & DevOps

### 9.1 Docker Configuration

- Создать `Dockerfile` для каждого сервиса:
  - `services/api-gateway/Dockerfile`
  - `services/auth-service/Dockerfile`
  - `services/bot-detection-service/Dockerfile`
  - `services/vk-service/Dockerfile`
  - `services/ok-service/Dockerfile`
- Создать `Dockerfile` для frontend
- Создать `docker-compose.yml` в корне проекта:
  - PostgreSQL service
  - Redis service
  - RabbitMQ service (с management UI)
  - api-gateway service
  - auth-service
  - bot-detection-service
  - vk-service
  - ok-service
  - frontend (опционально, для production)
- Настроить volumes для данных
- Настроить networks для связи между сервисами
- Настроить environment variables для каждого сервиса

### 9.2 Environment Configuration

- Создать `.env.example` для каждого сервиса
- Документировать необходимые переменные окружения:
  - Database connection strings
  - JWT settings
  - Service URLs
  - VK/OK API credentials
  - RabbitMQ connection
  - Redis connection
- Настроить разные конфигурации для dev/staging/production

### 9.3 Health Checks & Monitoring

- Настроить health checks endpoints в каждом сервисе:
  - Database health check (PostgreSQL)
  - Redis health check (для сервисов с кэшем)
  - RabbitMQ health check (для bot-detection-service)
- Настроить Swagger/OpenAPI документацию для каждого сервиса
- Настроить логирование запросов через middleware
- Настроить NLog для каждого сервиса

### 9.4 RabbitMQ Configuration

- Настроить exchanges:
  - `bot-detection-exchange` (topic или direct)
- Настроить queues:
  - `bot-analysis-requests` - запросы на анализ
- Настроить bindings между exchanges и queues
- Настроить retry механизм для failed messages
- Настроить dead letter queue для обработки ошибок

---

## Область 10: Testing & Documentation

### 10.1 Backend Testing

- Настроить xUnit для unit тестов каждого сервиса
- Создать тесты для `AuthService`
- Создать тесты для `BotDetectionService`
- Создать тесты для `VkService` и `OkService`
- Настроить Testcontainers для интеграционных тестов с PostgreSQL и RabbitMQ

### 10.2 Frontend Testing

- Настроить Vitest или Jest
- Создать тесты для критических компонентов
- Создать тесты для auth hooks
- Настроить тестирование форм

### 10.3 Documentation

- Обновить `README.md` с актуальной информацией:
  - Описание проекта
  - Архитектура микросервисов
  - Схема коммуникации
  - Инструкции по запуску
- Создать документацию по API (Swagger для каждого сервиса)
- Создать документацию по настройке окружения
- Создать документацию по архитектуре проекта

---

## Приоритеты реализации

### Этап 1: Базовая инфраструктура

1. API Gateway (базовая структура и проксирование)
2. Auth Service (регистрация, вход, JWT)
3. Frontend Setup & Infrastructure
4. Frontend Authentication (базовая)

### Этап 2: Интеграции

5. VK Service
6. OK Service

### Этап 3: Основной функционал

7. Bot Detection Service (синхронные операции)
8. Bot Detection Service (асинхронные операции через RabbitMQ)
9. Frontend Bot Detection Module

### Этап 4: Инфраструктура и полировка

10. Docker Compose
11. Health Checks & Monitoring
12. Testing & Documentation

---

## Схема коммуникации между сервисами

### Синхронные операции (HTTP REST)

```
Frontend → API Gateway → Auth Service
                    → Bot Detection Service → VK Service
                                          → OK Service
```

### Асинхронные операции (RabbitMQ)

```
Frontend → API Gateway → Bot Detection Service
                              ↓ (публикует сообщение)
                         RabbitMQ Queue
                              ↓ (читает worker)
                    Bot Detection Worker
                              ↓ (HTTP запросы)
                         VK/OK Services
```

---

## Зависимости между областями

```
API Gateway → Auth Service (для валидации токенов)
          → Bot Detection Service → VK Service (HTTP)
                                → OK Service (HTTP)
                                → RabbitMQ (MQ)

Frontend → API Gateway (все запросы)

Infrastructure (PostgreSQL, Redis, RabbitMQ) поддерживает все сервисы
```

