# API Gateway Tests

Unit тесты для API Gateway Service.

## Запуск тестов

```bash
cd services/api-gateway-tests
dotnet test
```

## Покрытие тестами

### ✅ Middleware Tests

- `ErrorHandlingMiddlewareTests`
  - ✅ Передача запроса без ошибок
  - ✅ Обработка общих исключений (500)
  - ✅ Обработка HttpRequestException (502 Bad Gateway)

### ✅ Controller Tests

- `AuthControllerTests`
  - ✅ Успешная регистрация (200)
  - ✅ Обработка ошибок регистрации (400)
  - ✅ Обработка исключений при логине (500)
  - ✅ Передача токена в LogoutAsync

### ✅ Configuration Tests

- `ServiceUrlsTests`
  - ✅ Значения по умолчанию
  - ✅ Установка значений

## Результаты

**Всего тестов:** 9  
**Пройдено:** 9 ✅  
**Провалено:** 0  
**Покрытие:** Базовое покрытие middleware, controllers и configuration

## Технологии

- **xUnit** - тестовый фреймворк
- **Moq** - мокирование зависимостей
- **FluentAssertions** - читаемые assertions
- **Microsoft.AspNetCore.Mvc.Testing** - тестирование ASP.NET Core
