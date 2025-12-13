# Скрипт для создания GitHub Project Board
# Требуется: GitHub Personal Access Token с правами repo

param(
    [Parameter(Mandatory=$true)]
    [string]$GitHubToken,
    
    [Parameter(Mandatory=$false)]
    [string]$Owner = "vasiliy2097projects",
    
    [Parameter(Mandatory=$false)]
    [string]$Repo = "NeuroAdminApp",
    
    [Parameter(Mandatory=$false)]
    [string]$ProjectName = "NeuroAdmin Development"
)

$headers = @{
    "Authorization" = "Bearer $GitHubToken"
    "Accept" = "application/vnd.github+json"
    "X-GitHub-Api-Version" = "2022-11-28"
}

Write-Host "Создание GitHub Project Board..." -ForegroundColor Green

# Шаг 1: Создать проект
$createProjectBody = @{
    name = $ProjectName
    body = "Project board для управления разработкой NeuroAdmin"
} | ConvertTo-Json

try {
    $projectResponse = Invoke-RestMethod -Uri "https://api.github.com/repos/$Owner/$Repo/projects" `
        -Method Post `
        -Headers $headers `
        -Body $createProjectBody `
        -ContentType "application/json"
    
    $projectId = $projectResponse.id
    $projectNumber = $projectResponse.number
    Write-Host "✓ Проект создан: ID=$projectId, Number=$projectNumber" -ForegroundColor Green
} catch {
    Write-Host "✗ Ошибка при создании проекта: $_" -ForegroundColor Red
    exit 1
}

# Шаг 2: Создать колонки
$columns = @(
    @{ name = "📋 Backlog"; position = 0 },
    @{ name = "🎨 Design"; position = 1 },
    @{ name = "🔨 In Progress"; position = 2 },
    @{ name = "👀 Review"; position = 3 },
    @{ name = "✅ Done"; position = 4 }
)

$columnIds = @{}

foreach ($column in $columns) {
    try {
        $columnBody = @{
            name = $column.name
        } | ConvertTo-Json
        
        $columnResponse = Invoke-RestMethod -Uri "https://api.github.com/projects/$projectId/columns" `
            -Method Post `
            -Headers $headers `
            -Body $columnBody `
            -ContentType "application/json"
        
        $columnIds[$column.name] = $columnResponse.id
        Write-Host "✓ Колонка создана: $($column.name)" -ForegroundColor Green
    } catch {
        Write-Host "✗ Ошибка при создании колонки $($column.name): $_" -ForegroundColor Red
    }
}

# Шаг 3: Получить все issues
try {
    $issues = Invoke-RestMethod -Uri "https://api.github.com/repos/$Owner/$Repo/issues?state=open&per_page=100" `
        -Method Get `
        -Headers $headers
    
    Write-Host "✓ Найдено issues: $($issues.Count)" -ForegroundColor Green
} catch {
    Write-Host "✗ Ошибка при получении issues: $_" -ForegroundColor Red
    exit 1
}

# Шаг 4: Распределить issues по колонкам
# Release issues и Design issues → Backlog
# Остальные issues → Backlog

$backlogColumnId = $columnIds["📋 Backlog"]
$designColumnId = $columnIds["🎨 Design"]

foreach ($issue in $issues) {
    $issueNumber = $issue.number
    $issueTitle = $issue.title
    $issueLabels = $issue.labels | ForEach-Object { $_.name }
    
    # Определить колонку на основе labels
    $targetColumnId = $backlogColumnId
    
    if ($issueLabels -contains "design") {
        $targetColumnId = $designColumnId
    }
    
    # Добавить issue в колонку
    try {
        $cardBody = @{
            content_id = $issue.id
            content_type = "Issue"
        } | ConvertTo-Json
        
        Invoke-RestMethod -Uri "https://api.github.com/projects/columns/$targetColumnId/cards" `
            -Method Post `
            -Headers $headers `
            -Body $cardBody `
            -ContentType "application/json" | Out-Null
        
        Write-Host "✓ Issue #$issueNumber добавлен в колонку" -ForegroundColor Cyan
    } catch {
        Write-Host "✗ Ошибка при добавлении issue #$issueNumber : $_" -ForegroundColor Yellow
    }
}

Write-Host "`n✓ GitHub Project Board успешно создан!" -ForegroundColor Green
Write-Host "Ссылка: https://github.com/$Owner/$Repo/projects/$projectNumber" -ForegroundColor Cyan

