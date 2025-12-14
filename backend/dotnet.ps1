param(
    [Parameter(Mandatory=$true, Position=0)]
    [string]$Option,

    [Parameter(Mandatory=$false, Position=1)]
    [string]$Name,

    [Parameter(ValueFromRemainingArguments=$true)]
    [string[]]$RemainingArgs
)

# Define the project paths
$projectPath = "src/lrembecki.infrastructure/lrembecki.infrastructure.csproj"
$startupProjectPath = "src/lrembecki.host/lrembecki.host.csproj"
$solutionPath = "src"

function restorePackages {
    Write-Host "[restorePackages] Restoring NuGet packages for entire solution"
    
    # Restore the startup project which will restore all dependencies
    dotnet restore $startupProjectPath
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Error: Failed to restore NuGet packages"
        exit 1
    }
    
    Write-Host "[restorePackages] Restore completed successfully"
}

function addMigration {
    param(
        [Parameter(Mandatory=$true)]
        [string]$Name
    )

    if (-not $Name) {
        Write-Host "[addMigration] Name not provided"
        return
    }

    # Add timestamp suffix to migration name
    $timestamp = Get-Date -Format "yyyyMMddHHmm"
    $migrationName = "$Name-$timestamp"

    Write-Host "[addMigration] Adding migration: $migrationName"
    Write-Host "[addMigration] Project: $projectPath"
    Write-Host "[addMigration] Startup Project: $startupProjectPath"
    
    dotnet ef migrations add $migrationName -p $projectPath -s $startupProjectPath
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Error: Failed to add migration"
        exit 1
    }
}

function updateDatabase {
    param(
        [Parameter(Mandatory=$false)]
        [string]$Name
    )
    if ($Name) {
        Write-Host "[updateDatabase] Updating database to migration: $Name"
        dotnet ef database update $Name -p $projectPath -s $startupProjectPath
    } else {
        Write-Host "[updateDatabase] Updating database to latest migration"
        dotnet ef database update -p $projectPath -s $startupProjectPath
    }
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Error: Failed to update database"
        exit 1
    }
}

function removeMigration {
    Write-Host "[removeMigration] Removing last migration"
    Write-Host "[removeMigration] Project: $projectPath"
    Write-Host "[removeMigration] Startup Project: $startupProjectPath"
    
    dotnet ef migrations remove -p $projectPath -s $startupProjectPath
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Error: Failed to remove migration"
        exit 1
    }
}

function listMigrations {
    Write-Host "[listMigrations] Listing all migrations"
    
    dotnet ef migrations list -p $projectPath -s $startupProjectPath
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Error: Failed to list migrations"
        exit 1
    }
}

# Capture the migration name from positional argument
if (-not $Name -and $RemainingArgs.Count -gt 0) {
    $Name = $RemainingArgs[0]
}

switch ($Option) {
    "add-migration" {
        if (-not $Name) {
            Write-Host "Error: Migration name is required for add-migration"
            exit 1
        }
        restorePackages
        addMigration -Name $Name
    }
    "update-database" {
        restorePackages
        updateDatabase -Name $Name
    }
    "remove-migration" {
        restorePackages
        removeMigration
    }
    "list-migrations" {
        restorePackages
        listMigrations
    }
    "restore" {
        restorePackages
    }
    default {
        Write-Host "Unknown option: $Option"
        Write-Host ""
        Write-Host "Available options:"
        Write-Host "  add-migration <name>       - Add a new migration"
        Write-Host "  update-database [name]     - Update database to latest or specific migration"
        Write-Host "  remove-migration           - Remove the last migration"
        Write-Host "  list-migrations            - List all migrations"
        Write-Host "  restore                    - Restore NuGet packages"
        exit 1
    }
}
