param(
    [Parameter(Mandatory=$true)]
    [string]$Option,

    [Parameter(Mandatory=$false)]
    [string]$Name
)

function generateFeature {
    param(
        [Parameter(Mandatory=$true)]
        [string]$Name
    )
    
    Set-Location src
    New-Item -ItemType Directory -Path $Name
    Set-Location $Name

    # core
    dotnet new classlib -n lrembecki.core.$Name
    Set-Location lrembecki.core.$Name
    dotnet add reference ..\..\lrembecki.core\lrembecki.core.csproj
    Set-Location ..
    
    # presentation
    # dotnet new classlib -n lrembecki.presentation.$Name
    # Set-Location lrembecki.presentation.$Name
    # dotnet add reference ..\lrembecki.core.$Name\lrembecki.core.$Name.csproj
    # dotnet add reference Microsoft.AspNetCore.App --framework
    # dotnet add package Microsoft.Extensions.DependencyInjection
    # Set-Location ..

    # unit tests
    # dotnet new xunit -n lrembecki.unit-tests.$Name
    # Set-Location lrembecki.unit-tests.$Name
    # dotnet add reference ..\..\lrembecki.presentation.$Name\lrembecki.presentation.$Name.csproj
    # Set-Location ..

    # Set-Location ..\..

    # infrastructure
    # Set-Location lrembecki.infrastructure
    # dotnet add reference ..\account\lrembecki.core.$Name\lrembecki.core.$Name.csproj
    # Set-Location ..

    # host
    # Set-Location lrembecki.host
    # dotnet add reference ..\account\lrembecki.presentation.$Name\lrembecki.presentation.$Name.csproj
    # Set-Location ..

    # Solution
    # dotnet sln add src\$Name\lrembecki.core.$Name
    # dotnet sln add src\$Name\lrembecki.presentation.$Name
    # dotnet sln add src\$Name\lrembecki.unit-tests.$Name
}

switch ($Option) {
    "feature" {
        generateFeature -Name $Name
    }
    default {
        Write-Host "Unknown option: $Option"
        exit 1
    }
}
