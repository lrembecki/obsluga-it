param(
    [Parameter(Mandatory=$true)]
    [string]$Option,

    [Parameter(Mandatory=$false)]
    [string]$Name,

    [Parameter(Mandatory=$false)]
    [string]$ProjectPath
)

function print {
    param(
        [Parameter(Mandatory=$true)]
        [string]$Name,
        [Parameter(Mandatory=$true)]
        [string]$Message
    )

    Write-Host "[$Name] $Message"
}

function formatName {
    param(
        [Parameter(Mandatory=$true)]
        [string]$Name
    )
    return $Name.Substring(0,1).ToUpper() + $Name.Substring(1)
}

function replaceContentInFile {
    param(
        [Parameter(Mandatory=$true)]
        [string]$FilePath,

        [Parameter(Mandatory=$true)]
        [string]$FindContent,

        [Parameter(Mandatory=$true)]
        [string]$ReplaceContent,

        [Parameter(Mandatory=$false)]
        [boolean]$KeepFindContent = $false
    )

    if (-not (Test-Path $FilePath)) {
        print -Name "replaceContentInFile" -Message "File not found: $FilePath"
        return
    }

    $content = Get-Content -Path $FilePath -Raw

    $replace = "$ReplaceContent";

    if ($KeepFindContent -eq $true) {
        $replace = "$ReplaceContent`r`n$FindContent"
    }
    print -Name "replaceContentInFile" -Message $ReplaceContent

    if ($content -notmatch [regex]::Escape($FindContent)) {
        print -Name "replaceContentInFile" -Message "Search text not found: $FindContent"
    } elseif ($content -match [regex]::Escape($FindContent)) {
        print -Name "replaceContentInFile" -Message "Implementation already exists: $ReplaceContent"
    } else {
        $content = $content -replace [regex]::Escape($FindContent), $replace
        Set-Content -Path $FilePath -Value $content -Encoding UTF8
        print -Name "replaceContentInFile" -Message "Replaced content in file: $FilePath"
    }
}

function includeMicrosoftAspNetCoreAppFrameworkReference {
    param(
        [Parameter(Mandatory=$true)]
        [string]$FilePath
    )

    if (-not (Test-Path $FilePath)) {
        Write-Host "[includeMicrosoftAspNetCoreAppFrameworkReference] File not found: $FilePath"
        return
    }

    $xmlDoc = New-Object System.Xml.XmlDocument
    $xmlDoc.PreserveWhitespace = $true

    try {
        $xmlDoc.Load($FilePath)
    } catch {
        Write-Host "[includeMicrosoftAspNetCoreAppFrameworkReference] Failed to load XML: $FilePath"
        return
    }

    $projectNode = $xmlDoc.SelectSingleNode("/Project")
    if (-not $projectNode) {
        Write-Host "[includeMicrosoftAspNetCoreAppFrameworkReference] <Project> root not found."
        return
    }

    # Utwórz osobny ItemGroup dla czytelności
    $itemGroup = $xmlDoc.CreateElement("ItemGroup")
    $frNode = $xmlDoc.CreateElement("FrameworkReference")
    $attr = $xmlDoc.CreateAttribute("Include")
    $attr.Value = "Microsoft.AspNetCore.App"
    $frNode.Attributes.Append($attr) | Out-Null
    $itemGroup.AppendChild($frNode) | Out-Null
    $projectNode.AppendChild($itemGroup) | Out-Null

    Write-Host $xmlDoc;

    try {
        $xmlDoc.Save($FilePath)
        Write-Host "[includeMicrosoftAspNetCoreAppFrameworkReference] Added FrameworkReference to $FilePath"
    } catch {
        Write-Host "[includeMicrosoftAspNetCoreAppFrameworkReference] Failed to save $FilePath"
    }
}

function addAspNetCoreAppFrameworkReference {
    param(
        [Parameter(Mandatory=$true)]
        [string]$ProjectPath
    )

    Write-Host "Adding Microsoft.AspNetCore.App FrameworkReference to $ProjectPath"
    [xml]$xmlDoc = Get-Content -Path $ProjectPath

    $projectNode = $xmlDoc.SelectSingleNode("/Project");

    $frameworkReferenceNode = $xmlDoc.CreateElement("FrameworkReference");
    $frameworkReferenceNode.SetAttribute("Include", "Microsoft.AspNetCore.App");

    $itemGroupNode = $xmlDoc.CreateElement("ItemGroup");
    $itemGroupNode.AppendChild($frameworkReferenceNode) | Out-Null

    $projectNode.AppendChild($itemGroupNode) | Out-Null

    $xmlDoc.Save($ProjectPath);
    Write-Host "Added Microsoft.AspNetCore.App FrameworkReference to $ProjectPath"
}

function addInternalsVisibleTo {
    param(
        [Parameter(Mandatory=$true)]
        [string]$ProjectPath,
        
        [Parameter(Mandatory=$true)]
        [string]$FriendAssemblyName
    )

    Write-Host "Adding InternalsVisibleTo attribute to $ProjectPath for $FriendAssemblyName"
    [xml]$xmlDoc = Get-Content -Path $ProjectPath

    $projectNode = $xmlDoc.SelectSingleNode("/Project");

    $itemGroupNode = $xmlDoc.CreateElement("ItemGroup");
    $attributeNode = $xmlDoc.CreateElement("AssemblyAttribute");
    $attributeNode.SetAttribute("Include", "System.Runtime.CompilerServices.InternalsVisibleToAttribute");

    $argumentNode = $xmlDoc.CreateElement("_Parameter1");
    $argumentNode.InnerText = $FriendAssemblyName;

    $attributeNode.AppendChild($argumentNode) | Out-Null
    $itemGroupNode.AppendChild($attributeNode) | Out-Null

    $projectNode.AppendChild($itemGroupNode) | Out-Null

    $xmlDoc.Save($ProjectPath);
    Write-Host "Added InternalsVisibleTo attribute to $ProjectPath for $FriendAssemblyName"
}

function generateInfrastructure {
    param(
        [Parameter(Mandatory=$true)]
        [string]$Name
    )

    Set-Location src
    Set-Location $Name

    # infrastructure
    Write-Host "Generating infrastructure library: lrembecki.infrastructure.$Name"
    dotnet new classlib -n lrembecki.infrastructure.$Name
    Set-Location lrembecki.infrastructure.$Name
        dotnet add reference ..\..\shared\lrembecki.infrastructure.shared\lrembecki.infrastructure.shared.csproj
        dotnet add reference ..\lrembecki.core.$Name\lrembecki.core.$Name.csproj

        # template bootstrap
        $formatedName = $Name.Substring(0,1).ToUpper() + $Name.Substring(1)
        $formatedEndpoint = $Name.ToLower()
        $bootstrapFilePath = "Bootstrap.cs"
        $bootstrapContent = @"
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class Bootstrap$formatedName
{
    public static IServiceCollection Add$formatedName(this IServiceCollection services)
    {
        return services;
    }

    public static WebApplication Map$formatedName(this WebApplication app)
    {
        var group = app.MapGroup("/api/$formatedEndpoint")
            .WithTags("$formatedName")
            .RequireAuthorization("InternalJwtPolicy");

        return app;
    }

    public static ModelBuilder ApplyConfigurationFrom$formatedName(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Bootstrap$formatedName).Assembly);
        return modelBuilder;
    }
}
"@

        Set-Content -Path $bootstrapFilePath -Value $bootstrapContent -Encoding UTF8
        Delete-Item "Class1.cs"
    Set-Location .. # $Name directory
    Set-Location .. # src directory
    Set-Location lrembecki.infrastructure
        dotnet add reference ..\$Name\lrembecki.infrastructure.$Name
    Set-Location .. # src directory
    Set-Location lrembecki.host
        dotnet add reference ..\$Name\lrembecki.infrastructure.$Name
    Set-Location .. # src directory
    Set-Location .. # backend directory

    addInternalsVisibleTo -ProjectPath "src/$Name/lrembecki.core.$Name/lrembecki.core.$Name.csproj" -FriendAssemblyName "lrembecki.infrastructure.$Name"
    
    $formatedEndpoint = formatName -Name $Name

    $serviceRegistrationPath = "src/lrembecki.infrastructure/ServiceRegistration.cs"
    $serviceMarker = "// Add service registrations from other modules here"
    $serviceLine = "builder.Services.Add$formatedEndpoint();"
    replaceContentInFile -FilePath $serviceRegistrationPath -FindContent $serviceMarker -ReplaceContent $serviceLine -KeepFindContent $true

    $programCsPath = "src/lrembecki.host/Program.cs"
    $endpointMarker = "// Add endpoint mappings from other modules here"
    $endpointLine = "app.Map$formatedEndpoint();"
    replaceContentInFile -FilePath $programCsPath -FindContent $endpointMarker -ReplaceContent $endpointLine -KeepFindContent $true

    $dbContextFilePath = "src/lrembecki.infrastructure/LrembeckiDbContext.cs"
    $dbContextmarker = "// Add other module configurations here"
    $dbContextLine = "modelBuilder.ApplyConfigurationFrom$formatedEndpoint();"
    replaceContentInFile -FilePath $dbContextFilePath -FindContent $dbContextmarker -ReplaceContent $dbContextLine -KeepFindContent $true

    dotnet sln add src\$Name\lrembecki.infrastructure.$Name
}

function generateFeature {
    param(
        [Parameter(Mandatory=$true)]
        [string]$Name
    )
    
    Set-Location src
    
        Write-Host "Generating feature: $Name"
        New-Item -ItemType Directory -Path $Name
        Set-Location $Name

            # core

             # core
            Write-Host "Generating core library: lrembecki.core.$Name"
            dotnet new classlib -n lrembecki.core.$Name
            Set-Location lrembecki.core.$Name
                dotnet add reference ..\..\lrembecki.core\lrembecki.core.csproj
            Set-Location .. # $Name directory
            
            # presentation
            Write-Host "Generating presentation library: lrembecki.presentation.$Name"
            dotnet new classlib -n lrembecki.presentation.$Name
            Set-Location lrembecki.presentation.$Name

                Write-Host "Adding Microsoft.AspNetCore.App FrameworkReference to lrembecki.presentation.$Name.csproj"

                dotnet add reference ..\lrembecki.core.$Name\lrembecki.core.$Name.csproj
                dotnet add package Microsoft.Extensions.DependencyInjection
            Set-Location .. # $Name directory

            # unit tests
            Write-Host "Generating unit tests library: lrembecki.unit-tests.$Name"
            dotnet new xunit -n lrembecki.unit-tests.$Name
            Set-Location lrembecki.unit-tests.$Name
                dotnet add reference ..\lrembecki.presentation.$Name\lrembecki.presentation.$Name.csproj
            Set-Location .. # $Name directory

        Set-Location .. # src directory
    Set-Location .. # backend directory

    

            

    addAspNetCoreAppFrameworkReference -ProjectPath "src/$Name/lrembecki.presentation.$Name/lrembecki.presentation.$Name.csproj"

    # Solution
    dotnet sln add src\$Name\lrembecki.core.$Name
    dotnet sln add src\$Name\lrembecki.presentation.$Name
    dotnet sln add src\$Name\lrembecki.unit-tests.$Name

    Set-Location src
        # infrastructure
        Set-Location lrembecki.infrastructure
            dotnet add reference ..\$Name\lrembecki.core.$Name
        Set-Location .. # src directory
        # host
        Set-Location lrembecki.host
            dotnet add reference ..\$Name\lrembecki.presentation.$Name
        Set-Location .. # src directory
    Set-Location .. # backend directory
}

switch ($Option) {
    "feature" {
        generateFeature -Name $Name
    }
    "link-to-host" {
        linkInfrastructureToHost -Name $Name
    }
    "register-service" {
        registerServicesToInfrastructure -Name $Name
    }
    "add-infrastructure" {
        generateInfrastructure -Name $Name
    }
    "add-reference" {
        addAspNetCoreAppFrameworkReference -ProjectPath $ProjectPath
    }
    default {
        Write-Host "Unknown option: $Option"
        exit 1
    }
}
