# NeuroAdmin Design System - Компоненты

## Обзор

Этот документ содержит детальные спецификации для всех UI компонентов дизайн-системы NeuroAdmin.

**Версия:** 1.0  
**Дата создания:** 13 декабря 2025

---

## 📦 Базовые компоненты

### 1. Button (Кнопка)

#### Варианты

- **Primary** - Основная кнопка (CTA)
- **Secondary** - Вторичная кнопка
- **Outlined** - Кнопка с обводкой
- **Text** - Текстовая кнопка

#### Размеры

- **Small:** Высота `32px`, Padding `8px 16px`, Font Size `0.8125rem`
- **Medium:** Высота `40px`, Padding `10px 20px`, Font Size `0.875rem` (по умолчанию)
- **Large:** Высота `48px`, Padding `12px 24px`, Font Size `0.9375rem`

#### Спецификации

**Primary Button:**

```
Background: Primary 500 (#2196F3)
Text Color: White (#FFFFFF)
Border: None
Border Radius: 4px
Shadow: Elevation 2 (hover)
Font Weight: 500
Text Transform: uppercase
Letter Spacing: 0.02857em

States:
- Hover: Background Primary 600 (#1E88E5), Shadow Elevation 4
- Active: Background Primary 700 (#1976D2)
- Disabled: Background Gray 300, Text Gray 400, Opacity 0.38
- Focus: Outline 2px solid Primary 500, Outline Offset 2px
```

**Secondary Button:**

```
Background: Secondary 500 (#9C27B0)
Text Color: White (#FFFFFF)
Border: None
Border Radius: 4px
Shadow: Elevation 2 (hover)
Font Weight: 500
Text Transform: uppercase
Letter Spacing: 0.02857em

States: (аналогично Primary, но с Secondary цветами)
```

**Outlined Button:**

```
Background: Transparent
Text Color: Primary 500 (#2196F3)
Border: 1px solid Primary 500
Border Radius: 4px
Shadow: None
Font Weight: 500
Text Transform: uppercase

States:
- Hover: Background Primary 50, Border Primary 600
- Active: Background Primary 100
- Disabled: Border Gray 300, Text Gray 400
```

**Text Button:**

```
Background: Transparent
Text Color: Primary 500 (#2196F3)
Border: None
Border Radius: 4px
Shadow: None
Font Weight: 500
Text Transform: uppercase

States:
- Hover: Background Primary 50
- Active: Background Primary 100
- Disabled: Text Gray 400
```

#### Иконки в кнопках

- **Start Icon:** Отступ справа `8px`
- **End Icon:** Отступ слева `8px`
- **Icon Only:** Квадратная кнопка, размер иконки `24px`

#### Примеры использования

- Primary: "Сохранить", "Отправить", "Начать анализ"
- Secondary: "Отмена", "Назад"
- Outlined: "Экспорт", "Фильтр"
- Text: "Отменить", "Удалить"

---

### 2. Input (Поле ввода)

#### Типы

- **Text** - Текстовое поле
- **Password** - Пароль (с иконкой показа/скрытия)
- **Email** - Email адрес
- **Number** - Числовое поле
- **Textarea** - Многострочный текст

#### Размеры

- **Small:** Высота `32px`, Font Size `0.875rem`
- **Medium:** Высота `40px`, Font Size `1rem` (по умолчанию)
- **Large:** Высота `48px`, Font Size `1.125rem`

#### Спецификации

**Default Input:**

```
Background: White (#FFFFFF)
Border: 1px solid Gray 300 (#E0E0E0)
Border Radius: 4px
Padding: 12px 16px
Font Size: 1rem
Line Height: 1.5
Text Color: Gray 900 (#212121)
Placeholder Color: Gray 500 (#9E9E9E)

States:
- Hover: Border Gray 400 (#BDBDBD)
- Focus: Border Primary 500 (#2196F3), Outline 2px solid Primary 500 (offset 2px)
- Error: Border Error 500 (#F44336), Text Error 500
- Disabled: Background Gray 100, Border Gray 300, Text Gray 400, Opacity 0.38
```

**Input с Label:**

```
Label:
- Position: Above input
- Font Size: 0.875rem
- Font Weight: 500
- Color: Gray 700 (#616161)
- Margin Bottom: 4px

Required Indicator:
- Asterisk (*) после label
- Color: Error 500 (#F44336)
```

**Input с Helper Text:**

```
Helper Text:
- Position: Below input
- Font Size: 0.75rem
- Color: Gray 600 (#757575)
- Margin Top: 4px

Error Helper Text:
- Color: Error 500 (#F44336)
```

**Input с Icon:**

```
Start Icon:
- Position: Left inside input
- Padding Left: 16px
- Padding Right: 8px
- Color: Gray 500 (#9E9E9E)
- Size: 24px

End Icon (Password):
- Position: Right inside input
- Padding Right: 16px
- Padding Left: 8px
- Color: Gray 500
- Size: 24px
- Cursor: pointer
```

#### Валидация

- **Success State:** Border Success 500 (#4CAF50), End Icon check_circle (green)
- **Error State:** Border Error 500, End Icon error (red), Helper Text красный

---

### 3. Card (Карточка)

#### Варианты

- **Default** - Стандартная карточка
- **Elevated** - С тенью
- **Outlined** - С обводкой

#### Спецификации

**Default Card:**

```
Background: White (#FFFFFF)
Border: None
Border Radius: 8px
Shadow: Elevation 1
Padding: 24px
Min Height: Auto

States:
- Hover: Shadow Elevation 2 (для интерактивных карточек)
```

**Card Header:**

```
Padding: 16px 24px
Border Bottom: 1px solid Gray 200 (#EEEEEE)
Margin: -24px -24px 24px -24px

Title:
- Font Size: 1.25rem (H5)
- Font Weight: 500
- Color: Gray 900 (#212121)

Subtitle:
- Font Size: 0.875rem
- Color: Gray 600 (#757575)
- Margin Top: 4px
```

**Card Content:**

```
Padding: 24px (default)
```

**Card Actions:**

```
Padding: 8px 16px
Border Top: 1px solid Gray 200
Margin: 24px -24px -24px -24px
Display: Flex
Justify Content: flex-end
Gap: 8px
```

#### Примеры использования

- Статистические карточки на Dashboard
- Карточки анализов
- Карточки аккаунтов
- Информационные блоки

---

### 4. Table (Таблица)

#### Структура

- **Header** - Заголовок таблицы
- **Body** - Тело таблицы
- **Footer** - Подвал (пагинация, итоги)
- **Row** - Строка
- **Cell** - Ячейка

#### Спецификации

**Table Container:**

```
Background: White (#FFFFFF)
Border: 1px solid Gray 200 (#EEEEEE)
Border Radius: 8px
Overflow: Hidden
```

**Table Header:**

```
Background: Gray 50 (#FAFAFA)
Border Bottom: 2px solid Gray 200
Padding: 16px
Font Weight: 500
Font Size: 0.875rem
Text Transform: uppercase
Letter Spacing: 0.02857em
Color: Gray 700 (#616161)
```

**Table Row:**

```
Border Bottom: 1px solid Gray 200
Padding: 16px
Min Height: 52px

States:
- Hover: Background Gray 50 (#FAFAFA)
- Selected: Background Primary 50 (#E3F2FD)
```

**Table Cell:**

```
Padding: 16px
Font Size: 0.875rem
Color: Gray 900 (#212121)
Vertical Align: middle
```

**Table Footer:**

```
Background: Gray 50
Border Top: 1px solid Gray 200
Padding: 16px
```

#### Функциональность

- **Sorting:** Иконки стрелок в header, активная сортировка - Primary цвет
- **Selection:** Checkbox в первой колонке
- **Actions:** Кнопки действий в последней колонке
- **Pagination:** В footer таблицы

#### Responsive

- На мобильных: Таблица превращается в карточки
- Каждая строка = одна карточка
- Вертикальное расположение данных

---

### 5. Modal/Dialog (Модальное окно)

#### Типы

- **Dialog** - Диалоговое окно
- **Alert Dialog** - Предупреждение
- **Confirmation Dialog** - Подтверждение
- **Form Dialog** - Форма в модальном окне

#### Спецификации

**Modal Overlay:**

```
Background: rgba(0, 0, 0, 0.5)
Position: Fixed
Full Screen
Z-Index: 1300
Backdrop Filter: blur(4px)
```

**Modal Container:**

```
Background: White (#FFFFFF)
Border Radius: 12px
Shadow: Elevation 24
Max Width: 560px (Dialog)
Max Width: 900px (Form Dialog)
Width: 90% (mobile)
Max Height: 90vh
Overflow: Auto
Position: Center (vertical & horizontal)
```

**Modal Header:**

```
Padding: 24px 24px 16px 24px
Border Bottom: 1px solid Gray 200

Title:
- Font Size: 1.5rem (H4)
- Font Weight: 500
- Color: Gray 900

Close Button:
- Position: Top right
- Icon: close
- Size: 24px
- Color: Gray 500
- Padding: 8px
```

**Modal Content:**

```
Padding: 24px
Max Height: calc(90vh - 200px)
Overflow: Auto
```

**Modal Actions:**

```
Padding: 16px 24px
Border Top: 1px solid Gray 200
Display: Flex
Justify Content: flex-end
Gap: 8px
```

#### Примеры использования

- Подтверждение удаления
- Формы создания/редактирования
- Детальная информация
- Предупреждения

---

### 6. Badge/Status (Бейдж/Статус)

#### Варианты

- **Badge** - Числовой бейдж
- **Status** - Статусный бейдж
- **Chip** - Чип (тег)

#### Спецификации

**Status Badge:**

```
Display: Inline-flex
Padding: 4px 12px
Border Radius: 12px
Font Size: 0.75rem
Font Weight: 500
Text Transform: uppercase
Letter Spacing: 0.5px

Variants:
- Success: Background Success 100, Color Success 700
- Warning: Background Warning 100, Color Warning 700
- Error: Background Error 100, Color Error 700
- Info: Background Info 100, Color Info 700
- Default: Background Gray 100, Color Gray 700
```

**Numeric Badge:**

```
Position: Absolute (top-right)
Background: Error 500 (#F44336)
Color: White
Border Radius: 50%
Min Width: 20px
Height: 20px
Font Size: 0.75rem
Font Weight: 500
Padding: 0 6px
Display: Flex
Align Items: center
Justify Content: center
```

**Chip:**

```
Display: Inline-flex
Padding: 6px 12px
Border Radius: 16px
Font Size: 0.875rem
Background: Gray 100 (#F5F5F5)
Color: Gray 700 (#616161)
Border: None

With Delete Icon:
- Padding Right: 4px
- Icon Size: 18px
- Icon Color: Gray 500
```

#### Примеры использования

- Статусы анализов (Pending, InProgress, Completed, Failed)
- Количество уведомлений
- Теги и категории
- Статусы подключения аккаунтов

---

### 7. Progress Bar (Прогресс-бар)

#### Типы

- **Linear** - Линейный
- **Circular** - Круговой
- **Determinate** - С определенным значением
- **Indeterminate** - Неопределенный (загрузка)

#### Спецификации

**Linear Progress:**

```
Height: 4px (default), 8px (thick)
Background: Gray 200 (#EEEEEE)
Border Radius: 2px
Overflow: Hidden

Progress Fill:
- Background: Primary 500 (#2196F3)
- Height: 100%
- Transition: width 0.3s ease

Indeterminate:
- Animated gradient bar
- Animation: 1.5s linear infinite
```

**Circular Progress:**

```
Size: 40px (default), 60px (large)
Color: Primary 500
Stroke Width: 4px
Animation: Rotate 1.4s linear infinite
```

#### Варианты цветов

- **Primary:** Primary 500
- **Success:** Success 500
- **Warning:** Warning 500
- **Error:** Error 500

#### Примеры использования

- Прогресс анализа ботов
- Загрузка данных
- Загрузка файлов
- Индикатор выполнения задачи

---

### 8. Loading Spinner (Спиннер загрузки)

#### Типы

- **Circular** - Круговой
- **Linear** - Линейный
- **Skeleton** - Скелетон (placeholder)

#### Спецификации

**Circular Spinner:**

```
Size: 40px (default), 24px (small), 60px (large)
Color: Primary 500
Stroke Width: 4px
Animation: Rotate 1.4s linear infinite
```

**Skeleton:**

```
Background: Gray 200 (#EEEEEE)
Border Radius: 4px
Animation: Pulse 1.5s ease-in-out infinite
Min Height: 20px

Variants:
- Text: Height 16px, Width 100%
- Avatar: Border Radius 50%, Size 40px
- Button: Height 40px, Width 120px
- Card: Height 200px, Width 100%
```

#### Примеры использования

- Загрузка страницы
- Загрузка данных в таблице
- Загрузка карточек
- Placeholder контента

---

### 9. Alert/Notification (Уведомление)

#### Типы

- **Alert** - Статическое уведомление
- **Snackbar** - Всплывающее уведомление
- **Toast** - Временное уведомление

#### Спецификации

**Alert:**

```
Padding: 16px
Border Radius: 4px
Display: Flex
Align Items: flex-start
Gap: 12px
Border Left: 4px solid

Variants:
- Success: Background Success 50, Border Success 500, Icon Success 500
- Warning: Background Warning 50, Border Warning 500, Icon Warning 500
- Error: Background Error 50, Border Error 500, Icon Error 500
- Info: Background Info 50, Border Info 500, Icon Info 500

Icon: 24px
Close Button: Top right, 24px icon
```

**Snackbar:**

```
Position: Fixed
Bottom: 24px (default), Top: 24px (top variant)
Left: 50%
Transform: translateX(-50%)
Background: Gray 800 (#424242)
Color: White
Padding: 14px 16px
Border Radius: 4px
Shadow: Elevation 6
Min Width: 344px
Max Width: 568px
Z-Index: 1400

Action Button:
- Color: Primary 400
- Margin Left: 24px
- Text Transform: none
```

#### Примеры использования

- Успешное сохранение
- Ошибка валидации
- Предупреждение
- Информационное сообщение

---

### 10. Dropdown/Select (Выпадающий список)

#### Спецификации

**Select Input:**

```
Same as Input component
End Icon: arrow_drop_down (24px)
Cursor: pointer
```

**Dropdown Menu:**

```
Background: White
Border: 1px solid Gray 200
Border Radius: 4px
Shadow: Elevation 8
Min Width: 200px
Max Height: 300px
Overflow: Auto
Z-Index: 1300
```

**Menu Item:**

```
Padding: 12px 16px
Font Size: 0.875rem
Color: Gray 900
Cursor: pointer
Min Height: 48px
Display: Flex
Align Items: center

States:
- Hover: Background Gray 50
- Selected: Background Primary 50, Color Primary 700
- Disabled: Color Gray 400, Opacity 0.38
```

**Divider:**

```
Height: 1px
Background: Gray 200
Margin: 8px 0
```

---

### 11. Checkbox (Чекбокс)

#### Спецификации

**Checkbox:**

```
Size: 20px (default), 24px (large)
Border: 2px solid Gray 400
Border Radius: 2px
Background: White
Color: Primary 500 (when checked)

States:
- Unchecked: Border Gray 400
- Checked: Background Primary 500, Border Primary 500, Icon White
- Indeterminate: Background Primary 500, Border Primary 500, Minus icon
- Disabled: Opacity 0.38
- Focus: Outline 2px solid Primary 500, Offset 2px
```

**Checkbox Label:**

```
Font Size: 0.875rem
Color: Gray 900
Margin Left: 8px
Cursor: pointer
```

---

### 12. Radio (Радиокнопка)

#### Спецификации

**Radio:**

```
Size: 20px (default), 24px (large)
Border: 2px solid Gray 400
Border Radius: 50%
Background: White
Color: Primary 500 (when selected)

States:
- Unselected: Border Gray 400, No fill
- Selected: Border Primary 500, Inner circle Primary 500 (8px)
- Disabled: Opacity 0.38
- Focus: Outline 2px solid Primary 500, Offset 2px
```

**Radio Group:**

```
Display: Flex
Flex Direction: column
Gap: 12px
```

---

### 13. Tabs (Вкладки)

#### Спецификации

**Tabs Container:**

```
Border Bottom: 1px solid Gray 200
Display: Flex
Gap: 0
```

**Tab:**

```
Padding: 12px 24px
Font Size: 0.875rem
Font Weight: 500
Color: Gray 600 (#757575)
Text Transform: none
Min Height: 48px
Border Bottom: 2px solid transparent
Cursor: pointer

States:
- Active: Color Primary 500, Border Bottom Primary 500
- Hover: Background Gray 50
- Disabled: Color Gray 400, Opacity 0.38, Cursor: not-allowed
```

**Tab Panel:**

```
Padding: 24px
```

#### Варианты

- **Standard** - Горизонтальные вкладки
- **Scrollable** - С прокруткой (много вкладок)
- **Vertical** - Вертикальные вкладки

---

## 🏗️ Layout компоненты

### 14. Header (Шапка)

#### Спецификации

**Header Container:**

```
Height: 64px
Background: White (#FFFFFF)
Border Bottom: 1px solid Gray 200 (#EEEEEE)
Padding: 0 24px
Display: Flex
Align Items: center
Justify Content: space-between
Position: Sticky
Top: 0
Z-Index: 1100
Shadow: Elevation 1
```

**Logo:**

```
Height: 40px
Margin Right: 24px
```

**Navigation:**

```
Display: Flex
Gap: 8px
Align Items: center
```

**User Menu:**

```
Display: Flex
Align Items: center
Gap: 16px
```

**Notifications Icon:**

```
Size: 24px
Color: Gray 600
Position: Relative
Badge: Top right (red dot)
```

**Avatar:**

```
Size: 40px
Border Radius: 50%
Border: 2px solid Gray 200
```

---

### 15. Sidebar (Боковая панель)

#### Спецификации

**Sidebar Container:**

```
Width: 256px (expanded), 64px (collapsed)
Background: White (#FFFFFF)
Border Right: 1px solid Gray 200
Height: 100vh
Position: Fixed
Left: 0
Top: 64px
Z-Index: 1000
Transition: width 0.3s ease
Overflow: Hidden
```

**Sidebar Menu:**

```
Padding: 16px 0
```

**Menu Item:**

```
Padding: 12px 24px
Display: Flex
Align Items: center
Gap: 16px
Font Size: 0.875rem
Color: Gray 700
Cursor: pointer
Min Height: 48px

States:
- Active: Background Primary 50, Color Primary 700, Border Left 3px solid Primary 500
- Hover: Background Gray 50
- Collapsed: Justify Content: center, Padding: 12px
```

**Menu Icon:**

```
Size: 24px
Color: Gray 600
```

**Menu Label:**

```
Font Weight: 500
Collapsed: Hidden
```

**Divider:**

```
Margin: 8px 0
Height: 1px
Background: Gray 200
```

---

### 16. Footer (Подвал)

#### Спецификации

**Footer Container:**

```
Background: Gray 50 (#FAFAFA)
Border Top: 1px solid Gray 200
Padding: 24px
Margin Top: Auto
```

**Footer Content:**

```
Display: Flex
Justify Content: space-between
Align Items: center
Max Width: 1280px
Margin: 0 auto
```

**Copyright:**

```
Font Size: 0.875rem
Color: Gray 600
```

**Links:**

```
Display: Flex
Gap: 24px
Font Size: 0.875rem
Color: Gray 600
```

---

### 17. Container/Wrapper (Контейнер)

#### Спецификации

**Container:**

```
Max Width: 1280px (lg), 960px (md), 600px (sm)
Width: 100%
Margin: 0 auto
Padding: 24px

Responsive:
- xs: Padding 16px
- sm: Padding 20px
- md+: Padding 24px
```

**Wrapper:**

```
Padding: 24px
Background: Gray 50 (optional)
Min Height: calc(100vh - 64px)
```

---

### 18. Grid система

#### Спецификации

**Grid Container:**

```
Display: Grid
Grid Template Columns: repeat(12, 1fr)
Gap: 16px
```

**Grid Item:**

```
Grid Column: span X (1-12)
```

**Responsive:**

```
xs: span 12 (full width)
sm: span 6 (half width)
md: span 4 (third width)
lg: span 3 (quarter width)
```

---

## 📱 Примеры использования компонентов

### Dashboard Layout

```
Header (64px)
├── Sidebar (256px) ── Main Content Area
│   ├── Navigation Menu
│   └── User Section
└── Footer (auto)
```

### Card Grid

```
Container
└── Grid (12 columns)
    ├── Card (span 3) - Statistic
    ├── Card (span 3) - Statistic
    ├── Card (span 3) - Statistic
    └── Card (span 3) - Statistic
```

### Form Layout

```
Card
├── Card Header (Title)
├── Card Content
│   ├── Input (with Label)
│   ├── Input (with Label)
│   └── Select (with Label)
└── Card Actions
    ├── Button (Text) - Cancel
    └── Button (Primary) - Submit
```

---

**Последнее обновление:** 13 декабря 2025
