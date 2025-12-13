# NeuroAdmin Project Board

Этот документ описывает структуру GitHub Project для управления задачами NeuroAdmin.

## Структура Project Board

### Колонки (Columns)

1. **📋 Backlog** - Задачи в очереди
2. **🎨 Design** - Дизайн задачи
3. **🔨 In Progress** - Задачи в разработке
4. **👀 Review** - Задачи на проверке
5. **✅ Done** - Завершенные задачи

### Фильтры (Views)

#### По релизам:
- Release 1.0: Базовая инфраструктура
- Release 2.0: Интеграции
- Release 3.0: Основной функционал
- Release 4.0: Инфраструктура

#### По типам:
- Backend
- Frontend
- Design
- Infrastructure
- Testing

#### По приоритету:
- High Priority
- Medium Priority
- Low Priority

---

## Группировка задач

### Release 1.0: Базовая инфраструктура

**Backend:**
- #5 - API Gateway Service
- #6 - Auth Service

**Frontend:**
- #7 - Frontend Setup & Infrastructure
- #8 - Frontend Authentication

**Design:**
- #17 - Дизайн-система и UI компоненты
- #18 - Дизайн страниц аутентификации
- #19 - Дизайн Dashboard

### Release 2.0: Интеграции

**Backend:**
- #9 - VK Service
- #10 - OK Service

**Design:**
- #21 - Дизайн интеграции с социальными сетями

### Release 3.0: Основной функционал

**Backend:**
- #11 - Bot Detection Service (синхронные)
- #12 - Bot Detection Service (асинхронные)

**Frontend:**
- #13 - Frontend Bot Detection Module

**Design:**
- #20 - Дизайн модуля очистки от ботов

### Release 4.0: Инфраструктура

**Infrastructure:**
- #14 - Docker Compose
- #15 - Health Checks & Monitoring

**Quality:**
- #16 - Testing & Documentation

**Design:**
- #22 - Адаптивный дизайн
- #23 - Темная тема

---

## Workflow

### Процесс работы с задачами

1. **Создание задачи** → Backlog
2. **Назначение исполнителя** → Design / In Progress
3. **Разработка** → In Progress
4. **Code Review** → Review
5. **Завершение** → Done

### Правила перемещения

- Задачи перемещаются слева направо
- Нельзя пропускать этапы
- Review обязателен для всех задач
- Design задачи могут идти параллельно с разработкой

---

## Метрики

### Velocity Tracking
- Отслеживание количества завершенных задач в неделю
- Оценка времени на каждую задачу
- Анализ блокеров

### Burndown Chart
- Отслеживание прогресса по релизам
- Прогнозирование дат завершения

---

## Зависимости

### Критические зависимости

```
Дизайн-система (#17)
    ↓
Дизайн страниц (#18)
    ↓
Frontend Authentication (#8)

API Gateway (#5) → Auth Service (#6)
    ↓
Frontend Setup (#7)

VK Service (#9) ─┐
OK Service (#10) ─┼→ Bot Detection (#11, #12)
                  └→ Frontend Bot Detection (#13)
```

---

## Примечания

- Все задачи должны иметь четкое описание
- Каждая задача должна быть связана с релизом
- Design задачи должны быть завершены до начала разработки соответствующих компонентов
- Backend и Frontend могут разрабатываться параллельно после завершения дизайна

---

**Для создания Project Board в GitHub:**
1. Перейти в репозиторий
2. Открыть вкладку "Projects"
3. Создать новый проект
4. Настроить колонки согласно структуре выше
5. Добавить issues в соответствующие колонки

