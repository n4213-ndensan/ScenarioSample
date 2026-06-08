param(
    [string]$Configuration = "Release",
    [string]$Version = "1.0.0"
)

$SolutionRoot = $PSScriptRoot
$OutputDirectory = Join-Path -Path $SolutionRoot -ChildPath "LocalNuGet"
$PackingProject  = Join-Path -Path $SolutionRoot -ChildPath "UIComponents/UIComponents.csproj"

dotnet pack $PackingProject -c $Configuration -o $OutputDirectory --version $Version