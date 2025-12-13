# Скрипты для автоматизации

## create-github-project.ps1

Скрипт для автоматического создания GitHub Project Board с колонками и добавлением всех issues.

### Требования

1. GitHub Personal Access Token с правами:
   - `repo` (полный доступ к репозиторию)
   - `write:org` (если репозиторий в организации)

2. PowerShell 5.1+ или PowerShell Core

### Как получить Personal Access Token

1. Перейти в GitHub → Settings → Developer settings → Personal access tokens → Tokens (classic)
2. Нажать "Generate new token (classic)"
3. Выбрать права:
   - `repo` (Full control of private repositories)
   - `write:org` (если нужно)
4. Скопировать токен (он показывается только один раз!)

### Использование

```powershell
# Базовое использование
.\scripts\create-github-project.ps1 -GitHubToken "your_token_here"

# С параметрами
.\scripts\create-github-project.ps1 `
    -GitHubToken "your_token_here" `
    -Owner "vasiliy2097projects" `
    -Repo "NeuroAdminApp" `
    -ProjectName "NeuroAdmin Development"
```

### Что делает скрипт

1. Создает новый GitHub Project
2. Создает 5 колонок:
   - 📋 Backlog
   - 🎨 Design
   - 🔨 In Progress
   - 👀 Review
   - ✅ Done
3. Получает все открытые issues из репозитория
4. Распределяет issues по колонкам:
   - Issues с label "design" → колонка "🎨 Design"
   - Остальные issues → колонка "📋 Backlog"

### Безопасность

⚠️ **Важно:** Не коммитьте токен в репозиторий!

Используйте переменные окружения:
```powershell
$env:GITHUB_TOKEN = "your_token_here"
.\scripts\create-github-project.ps1 -GitHubToken $env:GITHUB_TOKEN
```

Или используйте секреты в CI/CD.

### Альтернативный способ (вручную)

Если скрипт не работает, можно создать проект вручную:

1. Перейти в репозиторий: https://github.com/vasiliy2097projects/NeuroAdminApp
2. Открыть вкладку "Projects"
3. Нажать "New project"
4. Выбрать "Board" (классический вид)
5. Создать колонки:
   - 📋 Backlog
   - 🎨 Design
   - 🔨 In Progress
   - 👀 Review
   - ✅ Done
6. Добавить issues вручную, перетаскивая их в соответствующие колонки

