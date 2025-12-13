# NeuroAdmin Design System - Style Guide

## Обзор

Этот документ описывает дизайн-систему для NeuroAdmin - AI-powered SMM платформы. Дизайн-система основана на Material Design принципах и адаптирована для административной панели.

**Версия:** 1.0  
**Дата создания:** 13 декабря 2025  
**Основа:** Material-UI (MUI) v5

---

## 🎨 Цветовая палитра

### Основные цвета

#### Primary (Основной)

- **Primary 50:** `#E3F2FD` - Светлый фон
- **Primary 100:** `#BBDEFB` - Очень светлый акцент
- **Primary 200:** `#90CAF9` - Светлый акцент
- **Primary 300:** `#64B5F6` - Средний акцент
- **Primary 400:** `#42A5F5` - Акцент
- **Primary 500:** `#2196F3` - **Основной цвет (Primary)**
- **Primary 600:** `#1E88E5` - Темный акцент
- **Primary 700:** `#1976D2` - Очень темный акцент
- **Primary 800:** `#1565C0` - Темный
- **Primary 900:** `#0D47A1` - Очень темный

#### Secondary (Вторичный)

- **Secondary 50:** `#F3E5F5` - Светлый фон
- **Secondary 100:** `#E1BEE7` - Очень светлый акцент
- **Secondary 200:** `#CE93D8` - Светлый акцент
- **Secondary 300:** `#BA68C8` - Средний акцент
- **Secondary 400:** `#AB47BC` - Акцент
- **Secondary 500:** `#9C27B0` - **Вторичный цвет (Secondary)**
- **Secondary 600:** `#8E24AA` - Темный акцент
- **Secondary 700:** `#7B1FA2` - Очень темный акцент
- **Secondary 800:** `#6A1B9A` - Темный
- **Secondary 900:** `#4A148C` - Очень темный

### Цвета статусов

#### Success (Успех)

- **Success Light:** `#81C784`
- **Success Main:** `#4CAF50` - **Основной цвет успеха**
- **Success Dark:** `#388E3C`
- **Success Contrast:** `#FFFFFF`

#### Warning (Предупреждение)

- **Warning Light:** `#FFB74D`
- **Warning Main:** `#FF9800` - **Основной цвет предупреждения**
- **Warning Dark:** `#F57C00`
- **Warning Contrast:** `#000000`

#### Error (Ошибка)

- **Error Light:** `#E57373`
- **Error Main:** `#F44336` - **Основной цвет ошибки**
- **Error Dark:** `#D32F2F`
- **Error Contrast:** `#FFFFFF`

#### Info (Информация)

- **Info Light:** `#64B5F6`
- **Info Main:** `#2196F3` - **Основной цвет информации**
- **Info Dark:** `#1976D2`
- **Info Contrast:** `#FFFFFF`

### Нейтральные цвета

#### Grays (Серые)

- **Gray 50:** `#FAFAFA` - Очень светлый фон
- **Gray 100:** `#F5F5F5` - Светлый фон
- **Gray 200:** `#EEEEEE` - Светлая граница
- **Gray 300:** `#E0E0E0` - Граница
- **Gray 400:** `#BDBDBD` - Неактивный текст
- **Gray 500:** `#9E9E9E` - Вторичный текст
- **Gray 600:** `#757575` - Вторичный текст (темный)
- **Gray 700:** `#616161` - Основной текст (светлый)
- **Gray 800:** `#424242` - Основной текст
- **Gray 900:** `#212121` - Основной текст (темный)

#### Background (Фон)

- **Background Default:** `#FFFFFF` - Белый фон
- **Background Paper:** `#FAFAFA` - Фон для карточек
- **Background Elevation:** `#F5F5F5` - Фон для поднятых элементов

#### Text (Текст)

- **Text Primary:** `#212121` - Основной текст
- **Text Secondary:** `#757575` - Вторичный текст
- **Text Disabled:** `#BDBDBD` - Неактивный текст
- **Text Hint:** `#9E9E9E` - Подсказки

### Использование цветов

```css
/* Primary используется для: */
- Основные кнопки (CTA)
- Активные ссылки
- Важные элементы интерфейса
- Иконки действий

/* Secondary используется для: */
- Вторичные кнопки
- Акценты
- Дополнительные элементы

/* Status цвета используются для: */
- Success: Успешные операции, подтверждения
- Warning: Предупреждения, важные уведомления
- Error: Ошибки, критические сообщения
- Info: Информационные сообщения
```

---

## 📝 Типографика

### Шрифты

#### Основной шрифт

- **Font Family:** `'Roboto', 'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif`
- **Font Weight:** 300, 400, 500, 700

#### Моноширинный шрифт (для кода)

- **Font Family:** `'Roboto Mono', 'Courier New', monospace`

### Размеры шрифтов

#### Заголовки

- **H1:**

  - Font Size: `2.5rem` (40px)
  - Font Weight: `700`
  - Line Height: `1.2`
  - Letter Spacing: `-0.01562em`
  - Использование: Главные заголовки страниц

- **H2:**

  - Font Size: `2rem` (32px)
  - Font Weight: `700`
  - Line Height: `1.3`
  - Letter Spacing: `-0.00833em`
  - Использование: Заголовки разделов

- **H3:**

  - Font Size: `1.75rem` (28px)
  - Font Weight: `500`
  - Line Height: `1.4`
  - Letter Spacing: `0em`
  - Использование: Подзаголовки

- **H4:**

  - Font Size: `1.5rem` (24px)
  - Font Weight: `500`
  - Line Height: `1.4`
  - Letter Spacing: `0.00735em`
  - Использование: Заголовки карточек

- **H5:**

  - Font Size: `1.25rem` (20px)
  - Font Weight: `500`
  - Line Height: `1.5`
  - Letter Spacing: `0em`
  - Использование: Малые заголовки

- **H6:**
  - Font Size: `1.125rem` (18px)
  - Font Weight: `500`
  - Line Height: `1.6`
  - Letter Spacing: `0.0075em`
  - Использование: Заголовки форм

#### Основной текст

- **Body 1:**

  - Font Size: `1rem` (16px)
  - Font Weight: `400`
  - Line Height: `1.5`
  - Letter Spacing: `0.00938em`
  - Использование: Основной текст

- **Body 2:**
  - Font Size: `0.875rem` (14px)
  - Font Weight: `400`
  - Line Height: `1.43`
  - Letter Spacing: `0.01071em`
  - Использование: Вторичный текст

#### Специальные размеры

- **Button:**

  - Font Size: `0.875rem` (14px)
  - Font Weight: `500`
  - Line Height: `1.75`
  - Letter Spacing: `0.02857em`
  - Text Transform: `uppercase`

- **Caption:**

  - Font Size: `0.75rem` (12px)
  - Font Weight: `400`
  - Line Height: `1.66`
  - Letter Spacing: `0.03333em`
  - Использование: Подписи, метки

- **Overline:**
  - Font Size: `0.75rem` (12px)
  - Font Weight: `400`
  - Line Height: `2.66`
  - Letter Spacing: `0.08333em`
  - Text Transform: `uppercase`
  - Использование: Метки категорий

---

## 📏 Система отступов (Spacing)

### Базовый unit

- **Base Unit:** `8px`

### Стандартные отступы

- **0:** `0px`
- **1:** `8px` (0.5rem)
- **2:** `16px` (1rem)
- **3:** `24px` (1.5rem)
- **4:** `32px` (2rem)
- **5:** `40px` (2.5rem)
- **6:** `48px` (3rem)
- **8:** `64px` (4rem)
- **10:** `80px` (5rem)
- **12:** `96px` (6rem)

### Использование

```css
/* Padding внутри компонентов */
- Small: 8px (1 unit)
- Medium: 16px (2 units)
- Large: 24px (3 units)
- XLarge: 32px (4 units)

/* Margin между элементами */
- Small: 8px (1 unit)
- Medium: 16px (2 units)
- Large: 24px (3 units)
- XLarge: 32px (4 units)

/* Gap в Grid/Flex */
- Small: 8px (1 unit)
- Medium: 16px (2 units)
- Large: 24px (3 units)
```

---

## 🔲 Радиусы скругления (Border Radius)

- **None:** `0px` - Прямые углы
- **Small:** `4px` - Маленькое скругление (кнопки, инпуты)
- **Medium:** `8px` - Среднее скругление (карточки)
- **Large:** `12px` - Большое скругление (модальные окна)
- **XLarge:** `16px` - Очень большое скругление
- **Round:** `50%` - Круглые элементы (аватары, иконки)

### Использование

- **Buttons:** `4px` (Small)
- **Inputs:** `4px` (Small)
- **Cards:** `8px` (Medium)
- **Modals:** `12px` (Large)
- **Avatars:** `50%` (Round)
- **Badges:** `12px` (Large)

---

## 🌑 Тени (Shadows)

### Elevation Levels

- **Elevation 0 (None):**

  ```
  box-shadow: none;
  ```

  Использование: Плоские элементы

- **Elevation 1:**

  ```
  box-shadow: 0px 2px 1px -1px rgba(0,0,0,0.2),
              0px 1px 1px 0px rgba(0,0,0,0.14),
              0px 1px 3px 0px rgba(0,0,0,0.12);
  ```

  Использование: Карточки, кнопки при hover

- **Elevation 2:**

  ```
  box-shadow: 0px 3px 1px -2px rgba(0,0,0,0.2),
              0px 2px 2px 0px rgba(0,0,0,0.14),
              0px 1px 5px 0px rgba(0,0,0,0.12);
  ```

  Использование: Карточки при hover

- **Elevation 4:**

  ```
  box-shadow: 0px 2px 4px -1px rgba(0,0,0,0.2),
              0px 4px 5px 0px rgba(0,0,0,0.14),
              0px 1px 10px 0px rgba(0,0,0,0.12);
  ```

  Использование: Поднятые карточки

- **Elevation 8:**

  ```
  box-shadow: 0px 5px 5px -3px rgba(0,0,0,0.2),
              0px 8px 10px 1px rgba(0,0,0,0.14),
              0px 3px 14px 2px rgba(0,0,0,0.12);
  ```

  Использование: Модальные окна, dropdowns

- **Elevation 12:**

  ```
  box-shadow: 0px 7px 8px -4px rgba(0,0,0,0.2),
              0px 12px 17px 2px rgba(0,0,0,0.14),
              0px 5px 22px 4px rgba(0,0,0,0.12);
  ```

  Использование: Высокие модальные окна

- **Elevation 16:**

  ```
  box-shadow: 0px 8px 10px -5px rgba(0,0,0,0.2),
              0px 16px 24px 2px rgba(0,0,0,0.14),
              0px 6px 30px 5px rgba(0,0,0,0.12);
  ```

  Использование: Выпадающие меню

- **Elevation 24:**
  ```
  box-shadow: 0px 11px 15px -7px rgba(0,0,0,0.2),
              0px 24px 38px 3px rgba(0,0,0,0.14),
              0px 9px 46px 8px rgba(0,0,0,0.12);
  ```
  Использование: Высокие модальные окна, tooltips

---

## 🎯 Иконки

### Иконографика

- **Библиотека:** Material Icons (Google Material Design Icons)
- **Размеры:** 16px, 20px, 24px, 32px, 40px, 48px
- **Стиль:** Outlined (по умолчанию), Filled, Rounded

### Основные размеры

- **Small:** `16px` - Маленькие иконки (в кнопках, списках)
- **Medium:** `24px` - Стандартный размер
- **Large:** `32px` - Большие иконки (в карточках)
- **XLarge:** `48px` - Очень большие иконки (пустые состояния)

### Цвета иконок

- **Default:** `rgba(0, 0, 0, 0.54)` - Стандартный цвет
- **Primary:** `#2196F3` - Основной цвет
- **Secondary:** `#9C27B0` - Вторичный цвет
- **Disabled:** `rgba(0, 0, 0, 0.26)` - Неактивные
- **Error:** `#F44336` - Ошибки
- **Success:** `#4CAF50` - Успех

### Популярные иконки для NeuroAdmin

- **Navigation:** menu, home, dashboard, settings
- **Actions:** add, edit, delete, save, cancel, search, filter
- **Status:** check_circle, error, warning, info
- **Social:** vk, ok (кастомные)
- **Data:** table_chart, bar_chart, pie_chart, analytics

---

## 📐 Grid система

### Breakpoints

- **xs:** `0px` - Мобильные устройства
- **sm:** `600px` - Планшеты (портретная ориентация)
- **md:** `960px` - Планшеты (альбомная ориентация)
- **lg:** `1280px` - Десктоп
- **xl:** `1920px` - Большие экраны

### Grid Columns

- **Количество колонок:** 12
- **Gutter (отступ между колонками):** 16px (2 units)

### Container Widths

- **xs:** Full width
- **sm:** 600px
- **md:** 960px
- **lg:** 1280px
- **xl:** 1920px

---

## 🎨 Состояния компонентов

### Interactive States

- **Default:** Стандартное состояние
- **Hover:** При наведении курсора
- **Focus:** При фокусе (клавиатура)
- **Active:** При нажатии
- **Disabled:** Неактивное состояние
- **Loading:** Состояние загрузки

### Visual Feedback

- **Opacity для disabled:** `0.38`
- **Opacity для hover overlay:** `0.04`
- **Opacity для focus overlay:** `0.12`
- **Opacity для active overlay:** `0.16`

---

## ♿ Accessibility (WCAG 2.1)

### Контрастность

- **Текст на фоне:** Минимум 4.5:1 (AA), рекомендуется 7:1 (AAA)
- **Large текст (18px+):** Минимум 3:1 (AA)
- **Интерактивные элементы:** Минимум 3:1 (AA)

### Focus Indicators

- **Outline:** 2px solid primary color
- **Outline Offset:** 2px
- **Border Radius:** Соответствует элементу

### Keyboard Navigation

- **Tab Order:** Логический порядок
- **Skip Links:** Для пропуска навигации
- **ARIA Labels:** Для всех интерактивных элементов

---

## 📱 Адаптивность

### Mobile First Approach

- Дизайн начинается с мобильной версии
- Постепенное улучшение для больших экранов

### Touch Targets

- **Минимальный размер:** 44x44px (iOS), 48x48px (Material)
- **Отступы между элементами:** Минимум 8px

---

## 🎭 Анимации

### Transitions

- **Duration:**
  - Short: `150ms`
  - Standard: `250ms`
  - Long: `300ms`
- **Easing:**
  - Standard: `cubic-bezier(0.4, 0.0, 0.2, 1)`
  - Deceleration: `cubic-bezier(0.0, 0.0, 0.2, 1)`
  - Acceleration: `cubic-bezier(0.4, 0.0, 1, 1)`
  - Sharp: `cubic-bezier(0.4, 0.0, 0.6, 1)`

### Common Animations

- **Fade In/Out:** `opacity` + `transform`
- **Slide:** `transform: translateX/Y`
- **Scale:** `transform: scale`
- **Rotate:** `transform: rotate`

---

## 📚 Дополнительные ресурсы

### Инструменты

- **Figma:** Основной инструмент дизайна
- **Material Design Guidelines:** Референс
- **Material-UI Documentation:** Компоненты

### Полезные ссылки

- [Material Design](https://material.io/design)
- [Material-UI](https://mui.com/)
- [WCAG Guidelines](https://www.w3.org/WAI/WCAG21/quickref/)

---

**Последнее обновление:** 13 декабря 2025
