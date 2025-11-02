param(
    [Parameter(Mandatory=$true)]
    [string]$Option,

    [Parameter(Mandatory=$false)]
    [string]$Name,

    [Parameter(Mandatory=$false)]
    [string]$ProjectPath
)

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
    "add-reference" {
        addAspNetCoreAppFrameworkReference -ProjectPath $ProjectPath
    }
    default {
        Write-Host "Unknown option: $Option"
        exit 1
    }
}
