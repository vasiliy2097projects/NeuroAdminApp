# Промпт для Figma AI - NeuroAdmin Design System

## Инструкция

Скопируйте весь текст ниже и вставьте в Figma AI для создания дизайн-системы.

---

```
Создай полную дизайн-систему для NeuroAdmin - AI-powered SMM платформы. Следуй Material Design принципам.

## СТРУКТУРА ФАЙЛА

Создай следующую структуру страниц:
1. 🎨 Design Tokens
2. 🧩 Components
3. 📱 Pages
4. 📚 Documentation

## 1. DESIGN TOKENS

### Цвета (Color Styles)

Создай следующие цветовые стили:

**Primary (Синий):**
- Primary/50: #E3F2FD
- Primary/100: #BBDEFB
- Primary/200: #90CAF9
- Primary/300: #64B5F6
- Primary/400: #42A5F5
- Primary/500: #2196F3 (основной)
- Primary/600: #1E88E5
- Primary/700: #1976D2
- Primary/800: #1565C0
- Primary/900: #0D47A1

**Secondary (Фиолетовый):**
- Secondary/50: #F3E5F5
- Secondary/100: #E1BEE7
- Secondary/200: #CE93D8
- Secondary/300: #BA68C8
- Secondary/400: #AB47BC
- Secondary/500: #9C27B0 (основной)
- Secondary/600: #8E24AA
- Secondary/700: #7B1FA2
- Secondary/800: #6A1B9A
- Secondary/900: #4A148C

**Status Colors:**
- Success/Light: #81C784
- Success/Main: #4CAF50
- Success/Dark: #388E3C
- Warning/Light: #FFB74D
- Warning/Main: #FF9800
- Warning/Dark: #F57C00
- Error/Light: #E57373
- Error/Main: #F44336
- Error/Dark: #D32F2F
- Info/Light: #64B5F6
- Info/Main: #2196F3
- Info/Dark: #1976D2

**Grays:**
- Gray/50: #FAFAFA
- Gray/100: #F5F5F5
- Gray/200: #EEEEEE
- Gray/300: #E0E0E0
- Gray/400: #BDBDBD
- Gray/500: #9E9E9E
- Gray/600: #757575
- Gray/700: #616161
- Gray/800: #424242
- Gray/900: #212121

**Semantic Colors:**
- Background/Default: #FFFFFF
- Background/Paper: #FAFAFA
- Background/Elevation: #F5F5F5
- Text/Primary: #212121
- Text/Secondary: #757575
- Text/Disabled: #BDBDBD
- Text/Hint: #9E9E9E

### Типографика (Text Styles)

Создай следующие текстовые стили:

**Headings:**
- Typography/H1/Bold: 40px (2.5rem), Weight 700, Line Height 1.2, Letter Spacing -0.01562em
- Typography/H2/Bold: 32px (2rem), Weight 700, Line Height 1.3, Letter Spacing -0.00833em
- Typography/H3/Medium: 28px (1.75rem), Weight 500, Line Height 1.4, Letter Spacing 0em
- Typography/H4/Medium: 24px (1.5rem), Weight 500, Line Height 1.4, Letter Spacing 0.00735em
- Typography/H5/Medium: 20px (1.25rem), Weight 500, Line Height 1.5, Letter Spacing 0em
- Typography/H6/Medium: 18px (1.125rem), Weight 500, Line Height 1.6, Letter Spacing 0.0075em

**Body:**
- Typography/Body1/Regular: 16px (1rem), Weight 400, Line Height 1.5, Letter Spacing 0.00938em
- Typography/Body2/Regular: 14px (0.875rem), Weight 400, Line Height 1.43, Letter Spacing 0.01071em

**Special:**
- Typography/Button/Medium: 14px (0.875rem), Weight 500, Line Height 1.75, Letter Spacing 0.02857em, Uppercase
- Typography/Caption/Regular: 12px (0.75rem), Weight 400, Line Height 1.66, Letter Spacing 0.03333em
- Typography/Overline/Regular: 12px (0.75rem), Weight 400, Line Height 2.66, Letter Spacing 0.08333em, Uppercase

**Font Family:** 'Roboto', 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif

### Effects (Shadows)

Создай следующие эффекты теней:

- Elevation/0: none
- Elevation/1: 0px 2px 1px -1px rgba(0,0,0,0.2), 0px 1px 1px 0px rgba(0,0,0,0.14), 0px 1px 3px 0px rgba(0,0,0,0.12)
- Elevation/2: 0px 3px 1px -2px rgba(0,0,0,0.2), 0px 2px 2px 0px rgba(0,0,0,0.14), 0px 1px 5px 0px rgba(0,0,0,0.12)
- Elevation/4: 0px 2px 4px -1px rgba(0,0,0,0.2), 0px 4px 5px 0px rgba(0,0,0,0.14), 0px 1px 10px 0px rgba(0,0,0,0.12)
- Elevation/8: 0px 5px 5px -3px rgba(0,0,0,0.2), 0px 8px 10px 1px rgba(0,0,0,0.14), 0px 3px 14px 2px rgba(0,0,0,0.12)
- Elevation/12: 0px 7px 8px -4px rgba(0,0,0,0.2), 0px 12px 17px 2px rgba(0,0,0,0.14), 0px 5px 22px 4px rgba(0,0,0,0.12)
- Elevation/16: 0px 8px 10px -5px rgba(0,0,0,0.2), 0px 16px 24px 2px rgba(0,0,0,0.14), 0px 6px 30px 5px rgba(0,0,0,0.12)
- Elevation/24: 0px 11px 15px -7px rgba(0,0,0,0.2), 0px 24px 38px 3px rgba(0,0,0,0.14), 0px 9px 46px 8px rgba(0,0,0,0.12)

### Spacing Scale

Создай визуальную шкалу отступов:
- 0px, 8px, 16px, 24px, 32px, 40px, 48px, 64px, 80px, 96px

### Border Radius

Создай примеры радиусов:
- Small: 4px
- Medium: 8px
- Large: 12px
- XLarge: 16px
- Round: 50%

## 2. КОМПОНЕНТЫ

Создай все компоненты с Auto Layout и Variants. Используй созданные Color Styles и Text Styles.

### Button Component

Создай компонент Button с вариантами:
- **Property: Type** → Primary, Secondary, Outlined, Text
- **Property: Size** → Small (32px), Medium (40px), Large (48px)
- **Property: State** → Default, Hover, Active, Disabled, Focus

**Спецификации:**
- Primary: Background Primary/500, Text White, Border None, Border Radius 4px, Shadow Elevation/2 (hover: Elevation/4)
- Secondary: Background Secondary/500, Text White, аналогично Primary
- Outlined: Background Transparent, Text Primary/500, Border 1px Primary/500
- Text: Background Transparent, Text Primary/500, Border None
- Disabled: Opacity 0.38
- Focus: Outline 2px solid Primary/500, offset 2px
- Padding: Small (8px 16px), Medium (10px 20px), Large (12px 24px)
- Font: Typography/Button/Medium

### Input Component

Создай компонент Input с вариантами:
- **Property: Type** → Text, Password, Email, Number, Textarea
- **Property: Size** → Small (32px), Medium (40px), Large (48px)
- **Property: State** → Default, Focus, Error, Disabled
- **Property: Has Label** → True, False
- **Property: Has Helper** → True, False
- **Property: Has Icon** → None, Start, End

**Спецификации:**
- Background: White
- Border: 1px Gray/300 (Default), Primary/500 (Focus), Error/500 (Error)
- Border Radius: 4px
- Padding: 12px 16px
- Font: Typography/Body1/Regular
- Label: Typography/Body2/Regular, Color Gray/700, Margin Bottom 4px
- Helper Text: Typography/Caption/Regular, Color Gray/600 (Default), Error/500 (Error)
- Placeholder: Color Gray/500

### Card Component

Создай компонент Card с вариантами:
- **Property: Variant** → Default, Elevated, Outlined
- **Property: Has Header** → True, False
- **Property: Has Actions** → True, False

**Спецификации:**
- Background: White
- Border: None (Default, Elevated), 1px Gray/200 (Outlined)
- Border Radius: 8px
- Shadow: Elevation/1 (Default), Elevation/2 (Elevated)
- Padding: 24px
- Header: Padding 16px 24px, Border Bottom 1px Gray/200, Title Typography/H5/Medium
- Actions: Padding 8px 16px, Border Top 1px Gray/200, Flex End, Gap 8px

### Table Component

Создай компонент Table со структурой:
- Table Container: Background White, Border 1px Gray/200, Border Radius 8px
- Table Header: Background Gray/50, Padding 16px, Typography/Body2/Medium, Uppercase, Color Gray/700, Border Bottom 2px Gray/200
- Table Row: Border Bottom 1px Gray/200, Padding 16px, Min Height 52px
- Table Row Hover: Background Gray/50
- Table Cell: Padding 16px, Typography/Body2/Regular
- Table Footer: Background Gray/50, Border Top 1px Gray/200, Padding 16px

### Modal/Dialog Component

Создай компонент Modal:
- Overlay: Background rgba(0,0,0,0.5), Full Screen, Backdrop Blur 4px
- Container: Background White, Border Radius 12px, Shadow Elevation/24, Max Width 560px, Center Position
- Header: Padding 24px 24px 16px 24px, Border Bottom 1px Gray/200, Title Typography/H4/Medium, Close Button Top Right
- Content: Padding 24px, Max Height calc(90vh - 200px), Overflow Auto
- Actions: Padding 16px 24px, Border Top 1px Gray/200, Flex End, Gap 8px

### Badge/Status Component

Создай компонент Badge с вариантами:
- **Property: Variant** → Success, Warning, Error, Info, Default
- **Property: Type** → Status, Numeric, Chip

**Спецификации:**
- Status Badge: Padding 4px 12px, Border Radius 12px, Typography/Caption/Medium, Uppercase
  - Success: Background Success/100, Color Success/700
  - Warning: Background Warning/100, Color Warning/700
  - Error: Background Error/100, Color Error/700
  - Info: Background Info/100, Color Info/700
  - Default: Background Gray/100, Color Gray/700
- Numeric Badge: Background Error/500, Color White, Border Radius 50%, Min Width 20px, Height 20px, Center
- Chip: Padding 6px 12px, Border Radius 16px, Background Gray/100, Color Gray/700

### Progress Bar Component

Создай компонент Progress:
- **Property: Type** → Linear, Circular
- **Property: Variant** → Determinate, Indeterminate
- **Property: Color** → Primary, Success, Warning, Error

**Спецификации:**
- Linear: Height 4px, Background Gray/200, Border Radius 2px, Progress Fill Primary/500
- Circular: Size 40px, Color Primary/500, Stroke Width 4px

### Loading Spinner Component

Создай компонент Loading:
- **Property: Type** → Circular, Skeleton
- **Property: Size** → Small (24px), Medium (40px), Large (60px)

**Спецификации:**
- Circular: Size 40px, Color Primary/500, Stroke Width 4px, Animation Rotate
- Skeleton: Background Gray/200, Border Radius 4px, Min Height 20px, Animation Pulse

### Alert/Notification Component

Создай компонент Alert:
- **Property: Variant** → Success, Warning, Error, Info
- **Property: Type** → Alert, Snackbar

**Спецификации:**
- Alert: Padding 16px, Border Radius 4px, Border Left 4px, Display Flex, Gap 12px
  - Success: Background Success/50, Border Success/500
  - Warning: Background Warning/50, Border Warning/500
  - Error: Background Error/50, Border Error/500
  - Info: Background Info/50, Border Info/500
- Icon: 24px
- Close Button: Top Right, 24px

### Select/Dropdown Component

Создай компонент Select:
- Input: Аналогично Input компоненту, End Icon arrow_drop_down
- Menu: Background White, Border 1px Gray/200, Border Radius 4px, Shadow Elevation/8, Min Width 200px
- Menu Item: Padding 12px 16px, Typography/Body2/Regular, Min Height 48px, Hover Background Gray/50

### Checkbox Component

Создай компонент Checkbox:
- **Property: State** → Unchecked, Checked, Indeterminate, Disabled
- **Property: Size** → Small (20px), Medium (24px)

**Спецификации:**
- Size: 20px
- Border: 2px Gray/400 (Unchecked), Primary/500 (Checked)
- Background: White (Unchecked), Primary/500 (Checked)
- Border Radius: 2px
- Label: Typography/Body2/Regular, Margin Left 8px

### Radio Component

Создай компонент Radio:
- **Property: State** → Unselected, Selected, Disabled
- **Property: Size** → Small (20px), Medium (24px)

**Спецификации:**
- Size: 20px
- Border: 2px Gray/400 (Unselected), Primary/500 (Selected)
- Border Radius: 50%
- Inner Circle: 8px, Primary/500 (Selected)
- Label: Typography/Body2/Regular, Margin Left 8px

### Tabs Component

Создай компонент Tabs:
- **Property: Variant** → Standard, Scrollable
- **Property: Orientation** → Horizontal, Vertical

**Спецификации:**
- Container: Border Bottom 1px Gray/200, Display Flex
- Tab: Padding 12px 24px, Typography/Body2/Medium, Color Gray/600, Min Height 48px, Border Bottom 2px transparent
- Tab Active: Color Primary/500, Border Bottom Primary/500
- Tab Hover: Background Gray/50
- Tab Panel: Padding 24px

## 3. LAYOUT КОМПОНЕНТЫ

### Header Component

Создай компонент Header:
- Height: 64px
- Background: White
- Border Bottom: 1px Gray/200
- Padding: 0 24px
- Display: Flex, Space Between, Center
- Position: Sticky, Top 0
- Shadow: Elevation/1
- Logo: Height 40px, Margin Right 24px
- Navigation: Flex, Gap 8px
- User Menu: Flex, Gap 16px, Avatar 40px

### Sidebar Component

Создай компонент Sidebar:
- **Property: State** → Expanded (256px), Collapsed (64px)
- Width: 256px (Expanded), 64px (Collapsed)
- Background: White
- Border Right: 1px Gray/200
- Height: 100vh
- Position: Fixed, Left 0, Top 64px

**Menu Item:**
- Padding: 12px 24px (Expanded), 12px (Collapsed)
- Display: Flex, Center, Gap 16px
- Typography: Body2/Medium, Color Gray/700
- Min Height: 48px
- Active: Background Primary/50, Color Primary/700, Border Left 3px Primary/500
- Hover: Background Gray/50
- Icon: 24px, Color Gray/600
- Label: Hidden when Collapsed

### Footer Component

Создай компонент Footer:
- Background: Gray/50
- Border Top: 1px Gray/200
- Padding: 24px
- Display: Flex, Space Between, Center
- Max Width: 1280px, Margin 0 auto
- Copyright: Typography/Body2/Regular, Color Gray/600
- Links: Flex, Gap 24px, Typography/Body2/Regular, Color Gray/600

### Container Component

Создай компонент Container:
- Max Width: 1280px (lg), 960px (md), 600px (sm)
- Width: 100%
- Margin: 0 auto
- Padding: 24px (desktop), 16px (mobile)

### Grid System

Создай визуальную демонстрацию Grid:
- 12 колонок
- Gutter: 16px
- Responsive breakpoints: xs, sm, md, lg, xl

## 4. ПРИМЕРЫ СТРАНИЦ

Создай примеры страниц используя созданные компоненты:

### Login Page
- Центрированная форма
- Card с формой входа
- Input для Email и Password
- Button Primary "Войти"
- Ссылки "Забыли пароль?" и "Регистрация"

### Dashboard Page
- Header
- Sidebar (Expanded)
- Main Content Area с Grid
- Статистические Card компоненты (4 штуки в ряд)
- Таблица с данными

## 5. ДОКУМЕНТАЦИЯ

Создай страницу Documentation с:
- Обзором дизайн-системы
- Описанием использования каждого компонента
- Примерами кода (опционально)
- Guidelines по использованию

## ТРЕБОВАНИЯ

1. Используй Auto Layout для всех компонентов
2. Создай Variants для всех вариантов компонентов
3. Применяй созданные Color Styles и Text Styles
4. Используй Constraints для адаптивности
5. Добавь Description к каждому компоненту
6. Организуй компоненты в логические группы
7. Используй правильные naming conventions (Component/Property/Value)
8. Убедись, что все компоненты доступны (WCAG 2.1 AA)
9. Добавь hover и focus states где необходимо
10. Создай примеры использования компонентов

Начни с Design Tokens, затем создай компоненты, и наконец примеры страниц.
```

---

## Как использовать

1. Откройте Figma
2. Откройте ваш файл (или создайте новый)
3. Найдите Figma AI (обычно в правой панели или через меню)
4. Скопируйте весь текст из блока выше (начиная с "Создай полную дизайн-систему...")
5. Вставьте в Figma AI
6. Дождитесь создания дизайн-системы

## Дополнительные инструкции

Если Figma AI не может создать все сразу, разбейте промпт на части:

1. Сначала создай Design Tokens (цвета, типографика, эффекты)
2. Затем создай базовые компоненты (Button, Input, Card)
3. Потом создай остальные компоненты
4. Наконец создай Layout компоненты и примеры страниц

## Примечания

- Figma AI может создавать компоненты постепенно
- Проверяйте созданные компоненты и корректируйте при необходимости
- Используйте созданные Design Tokens для всех компонентов
- Не забывайте про Auto Layout и Variants

---

**Удачи с созданием дизайн-системы! 🎨**
