# NeuroAdmin Design System - Руководство по использованию

## Обзор

Этот документ содержит инструкции по использованию дизайн-системы NeuroAdmin и созданию Figma файла.

**Версия:** 1.0  
**Дата создания:** 13 декабря 2025

---

## 📐 Структура Figma файла

### Рекомендуемая структура

```
NeuroAdmin Design System
├── 🎨 Design Tokens
│   ├── Colors
│   │   ├── Primary
│   │   ├── Secondary
│   │   ├── Status (Success, Warning, Error, Info)
│   │   ├── Grays
│   │   └── Background & Text
│   ├── Typography
│   │   ├── Font Families
│   │   ├── Headings (H1-H6)
│   │   ├── Body Text
│   │   └── Special (Button, Caption, Overline)
│   ├── Spacing
│   │   └── Scale (0-12)
│   ├── Border Radius
│   │   └── Scale (None, Small, Medium, Large, Round)
│   ├── Shadows
│   │   └── Elevation (0-24)
│   └── Icons
│       └── Material Icons Library
│
├── 🧩 Components
│   ├── Buttons
│   │   ├── Primary
│   │   ├── Secondary
│   │   ├── Outlined
│   │   └── Text
│   ├── Forms
│   │   ├── Input
│   │   ├── Select
│   │   ├── Checkbox
│   │   └── Radio
│   ├── Data Display
│   │   ├── Card
│   │   ├── Table
│   │   ├── Badge
│   │   └── Progress
│   ├── Feedback
│   │   ├── Alert
│   │   ├── Snackbar
│   │   ├── Modal
│   │   └── Loading
│   ├── Navigation
│   │   ├── Tabs
│   │   ├── Menu
│   │   └── Breadcrumbs
│   └── Layout
│       ├── Header
│       ├── Sidebar
│       ├── Footer
│       └── Container
│
├── 📱 Pages
│   ├── Authentication
│   │   ├── Login
│   │   ├── Register
│   │   └── Forgot Password
│   ├── Dashboard
│   │   └── Main Dashboard
│   ├── Bot Detection
│   │   ├── Analysis List
│   │   ├── Analysis Details
│   │   └── Results
│   └── Settings
│       └── Account Settings
│
└── 📚 Documentation
    ├── Style Guide
    ├── Component Library
    └── Usage Examples
```

---

## 🎨 Создание Design Tokens в Figma

### 1. Colors (Цвета)

#### Создание Color Styles

1. **Primary Colors:**

   - Создайте прямоугольники для каждого оттенка Primary (50-900)
   - Выберите цвет → Right Click → "Add as Style"
   - Название: `Primary/50`, `Primary/100`, ..., `Primary/900`
   - Основной цвет: `Primary/500` → `Primary`

2. **Secondary Colors:**

   - Аналогично Primary
   - Название: `Secondary/50`, ..., `Secondary/900`

3. **Status Colors:**

   - Success: `Status/Success/Light`, `Status/Success/Main`, `Status/Success/Dark`
   - Warning: `Status/Warning/Light`, `Status/Warning/Main`, `Status/Warning/Dark`
   - Error: `Status/Error/Light`, `Status/Error/Main`, `Status/Error/Dark`
   - Info: `Status/Info/Light`, `Status/Info/Main`, `Status/Info/Dark`

4. **Grays:**

   - Название: `Gray/50`, `Gray/100`, ..., `Gray/900`

5. **Semantic Colors:**
   - `Background/Default` - White
   - `Background/Paper` - Gray 50
   - `Text/Primary` - Gray 900
   - `Text/Secondary` - Gray 600
   - `Text/Disabled` - Gray 400

### 2. Typography (Типографика)

#### Создание Text Styles

1. **Headings:**

   - Создайте текстовые блоки для каждого уровня
   - Настройте Font Size, Weight, Line Height, Letter Spacing
   - Add as Style:
     - `Typography/H1/Bold`
     - `Typography/H2/Bold`
     - `Typography/H3/Medium`
     - `Typography/H4/Medium`
     - `Typography/H5/Medium`
     - `Typography/H6/Medium`

2. **Body Text:**

   - `Typography/Body1/Regular`
   - `Typography/Body2/Regular`

3. **Special:**
   - `Typography/Button/Medium`
   - `Typography/Caption/Regular`
   - `Typography/Overline/Regular`

### 3. Effects (Тени)

#### Создание Effect Styles

1. Создайте прямоугольники с разными уровнями теней
2. Примените shadow согласно спецификации
3. Add as Style:
   - `Elevation/0` (none)
   - `Elevation/1`
   - `Elevation/2`
   - `Elevation/4`
   - `Elevation/8`
   - `Elevation/12`
   - `Elevation/16`
   - `Elevation/24`

---

## 🧩 Создание компонентов в Figma

### Best Practices

1. **Используйте Auto Layout:**

   - Все компоненты должны использовать Auto Layout
   - Это упростит адаптацию и изменение размеров

2. **Создавайте Variants:**

   - Группируйте похожие варианты компонентов
   - Например: Button с вариантами Primary, Secondary, Outlined, Text
   - И размерами: Small, Medium, Large

3. **Используйте Constraints:**

   - Настройте constraints для адаптивности
   - Left & Right для горизонтального растягивания
   - Top & Bottom для вертикального

4. **Компонентная структура:**
   ```
   Component Name
   ├── States (Default, Hover, Active, Disabled, Focus)
   ├── Variants (Primary, Secondary, etc.)
   └── Sizes (Small, Medium, Large)
   ```

### Пример: Button Component

1. **Создайте базовую структуру:**

   ```
   Button
   ├── Auto Layout (Horizontal)
   │   ├── Icon (Optional, Start)
   │   ├── Text
   │   └── Icon (Optional, End)
   ```

2. **Настройте Auto Layout:**

   - Padding: 10px 20px (Medium)
   - Gap: 8px
   - Alignment: Center
   - Fill: Primary 500

3. **Создайте Variants:**

   - Property: `Type` → Primary, Secondary, Outlined, Text
   - Property: `Size` → Small, Medium, Large
   - Property: `State` → Default, Hover, Active, Disabled

4. **Примените Styles:**
   - Fill: Color Style (Primary/500)
   - Text: Typography/Button/Medium
   - Effect: Elevation/2 (для Primary, Secondary)

### Пример: Input Component

1. **Структура:**

   ```
   Input
   ├── Auto Layout (Vertical)
   │   ├── Label (Optional)
   │   ├── Input Field
   │   │   ├── Auto Layout (Horizontal)
   │   │   │   ├── Icon (Optional, Start)
   │   │   │   ├── Text (Placeholder)
   │   │   │   └── Icon (Optional, End)
   │   └── Helper Text (Optional)
   ```

2. **Input Field:**

   - Border: 1px solid Gray 300
   - Border Radius: 4px
   - Padding: 12px 16px
   - Fill: White

3. **Variants:**
   - Property: `State` → Default, Focus, Error, Disabled
   - Property: `Size` → Small, Medium, Large
   - Property: `Has Label` → True, False
   - Property: `Has Helper` → True, False

---

## 📱 Создание страниц

### Layout Structure

1. **Создайте Frame:**

   - Размер: 1920x1080 (Desktop) или используйте Device Frame
   - Название: `Page Name - Desktop`

2. **Добавьте Layout компоненты:**

   ```
   Page Frame
   ├── Header (Fixed, Top)
   ├── Auto Layout (Horizontal)
   │   ├── Sidebar (Fixed, Left)
   │   └── Main Content (Flexible)
   └── Footer (Fixed, Bottom)
   ```

3. **Используйте Grid:**
   - Включите Layout Grid в Frame
   - Columns: 12
   - Gutter: 16px
   - Margin: 24px

### Responsive Design

1. **Создайте разные Frames:**

   - `Page Name - Desktop` (1920px)
   - `Page Name - Tablet` (768px)
   - `Page Name - Mobile` (375px)

2. **Используйте Constraints:**
   - Настройте constraints для адаптации компонентов
   - Sidebar: Collapse на мобильных
   - Grid: 12 columns → 1 column на мобильных

---

## 🎯 Компонентная библиотека

### Организация компонентов

1. **Создайте Component Set:**

   - Группируйте связанные компоненты
   - Например: "Buttons", "Forms", "Cards"

2. **Используйте иконки:**

   - Установите Material Icons plugin
   - Или импортируйте иконки из библиотеки
   - Создайте Icon Components

3. **Документация компонентов:**
   - Добавьте Description к каждому компоненту
   - Укажите использование и примеры
   - Добавьте Property descriptions

---

## 📋 Чеклист создания Figma файла

### Design Tokens

- [ ] Все цвета созданы как Styles
- [ ] Все текстовые стили созданы
- [ ] Все тени созданы как Effects
- [ ] Spacing scale документирован
- [ ] Border radius значения определены

### Компоненты

- [ ] Button (все варианты и состояния)
- [ ] Input (все типы и состояния)
- [ ] Card (все варианты)
- [ ] Table (header, body, row, cell)
- [ ] Modal/Dialog
- [ ] Badge/Status
- [ ] Progress Bar
- [ ] Loading Spinner
- [ ] Alert/Notification
- [ ] Dropdown/Select
- [ ] Checkbox
- [ ] Radio
- [ ] Tabs

### Layout компоненты

- [ ] Header
- [ ] Sidebar (expanded & collapsed)
- [ ] Footer
- [ ] Container
- [ ] Grid система

### Страницы

- [ ] Login Page
- [ ] Register Page
- [ ] Dashboard
- [ ] Analysis List
- [ ] Analysis Details
- [ ] Results Page

### Документация

- [ ] Style Guide страница
- [ ] Component Library страница
- [ ] Usage Examples
- [ ] Spacing Guide
- [ ] Color Usage Guide

---

## 🔗 Интеграция с Material-UI

### Mapping Figma → MUI

1. **Colors:**

   - Figma Color Styles → MUI Theme Colors
   - Используйте те же названия для консистентности

2. **Typography:**

   - Figma Text Styles → MUI Typography Variants
   - Создайте кастомную тему MUI на основе Figma стилей

3. **Components:**
   - Figma Components → MUI Components
   - Используйте MUI компоненты как основу
   - Кастомизируйте согласно Figma спецификациям

### Пример MUI Theme

```typescript
import { createTheme } from "@mui/material/styles";

const theme = createTheme({
  palette: {
    primary: {
      main: "#2196F3", // Primary 500
      light: "#64B5F6", // Primary 300
      dark: "#1976D2", // Primary 700
    },
    secondary: {
      main: "#9C27B0", // Secondary 500
      light: "#BA68C8", // Secondary 300
      dark: "#7B1FA2", // Secondary 700
    },
    success: {
      main: "#4CAF50",
    },
    warning: {
      main: "#FF9800",
    },
    error: {
      main: "#F44336",
    },
  },
  typography: {
    fontFamily: "'Roboto', 'Inter', sans-serif",
    h1: {
      fontSize: "2.5rem",
      fontWeight: 700,
      lineHeight: 1.2,
    },
    // ... остальные стили
  },
  spacing: 8, // Base unit
  shape: {
    borderRadius: 4, // Small
  },
});
```

---

## 📖 Примеры использования

### Пример 1: Создание кнопки

1. Откройте компонент Button
2. Выберите вариант: Primary, Medium, Default
3. Скопируйте компонент на страницу
4. Измените текст на нужный
5. При необходимости добавьте иконку

### Пример 2: Создание формы

1. Создайте Card компонент
2. Добавьте Card Header с заголовком
3. В Card Content добавьте:
   - Input с Label
   - Input с Label
   - Select с Label
4. В Card Actions добавьте кнопки

### Пример 3: Создание таблицы

1. Создайте Table компонент
2. Добавьте Table Header с колонками
3. Добавьте Table Rows с данными
4. Добавьте Table Footer с пагинацией

---

## 🚀 Следующие шаги

1. **Создайте Figma файл:**

   - Используйте структуру выше
   - Начните с Design Tokens
   - Затем создайте компоненты
   - Наконец, создайте страницы

2. **Поделитесь файлом:**

   - Сделайте файл доступным для команды
   - Настройте права доступа
   - Добавьте комментарии и документацию

3. **Интегрируйте с разработкой:**
   - Экспортируйте спецификации
   - Создайте MUI тему на основе дизайн-системы
   - Используйте Figma для reference во время разработки

---

## 📝 Примечания

- Все компоненты должны быть доступными (WCAG 2.1)
- Используйте консистентные spacing и sizing
- Документируйте все неочевидные решения
- Регулярно обновляйте дизайн-систему
- Собирайте feedback от команды

---

**Последнее обновление:** 13 декабря 2025
