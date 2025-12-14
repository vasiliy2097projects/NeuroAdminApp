# Auth Service

Сервис аутентификации для платформы NeuroAdmin.

## Описание

Сервис предоставляет функционал регистрации, входа, управления токенами и профилем пользователя.

## Функциональность

- Регистрация пользователей
- Вход в систему
- Обновление JWT токенов (refresh token)
- Выход из системы
- Верификация email
- Восстановление пароля
- Управление профилем пользователя
- Смена пароля

## API Endpoints

### Аутентификация

- `POST /api/auth/register` - Регистрация нового пользователя
- `POST /api/auth/login` - Вход в систему
- `POST /api/auth/refresh` - Обновление токена
- `POST /api/auth/logout` - Выход из системы
- `POST /api/auth/verify-email` - Верификация email
- `POST /api/auth/forgot-password` - Запрос сброса пароля
- `POST /api/auth/reset-password` - Сброс пароля

### Управление аккаунтом

- `GET /api/account/me` - Получить информацию о текущем пользователе (требует авторизации)
- `PUT /api/account/profile` - Обновить профиль (требует авторизации)
- `PUT /api/account/change-password` - Сменить пароль (требует авторизации)

## Конфигурация

### appsettings.json

```json
{
  "Jwt": {
    "Issuer": "http://localhost:5001",
    "Audience": "http://localhost:5220",
    "SecretKey": "${JWT_SECRET_KEY}",
    "ExpirationMinutes": 60
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=auth_service_db;Username=postgres;Password=postgres"
  }
}
```

### Переменные окружения

- `JWT_SECRET_KEY` - Секретный ключ для подписи JWT токенов (обязательно)

## Запуск

```bash
cd services/auth-service
dotnet restore
dotnet run
```

Сервис будет доступен по адресу: `http://localhost:5001`

## Миграции БД

Миграции выполняются автоматически при запуске приложения через FluentMigrator.

Таблицы:

- `users` - Пользователи
- `refresh_tokens` - Refresh токены
- `email_verification_tokens` - Токены верификации email
- `password_reset_tokens` - Токены сброса пароля

## Технологии

- .NET 9.0
- ASP.NET Core MVC
- PostgreSQL
- Dapper
- BCrypt.Net-Next
- JWT Bearer Authentication
- FluentMigrator
- NLog
- FluentValidation

## Структура проекта

```
auth-service/
├── Controllers/          # API контроллеры
├── Services/            # Бизнес-логика
├── Repositories/        # Работа с БД
├── Models/              # Модели данных
│   ├── Requests/        # Модели запросов
│   ├── Responses/       # Модели ответов
│   └── Entities/        # Сущности БД
├── Migrations/          # Миграции БД
├── Middleware/          # Middleware
└── Logging/             # Логирование
```
