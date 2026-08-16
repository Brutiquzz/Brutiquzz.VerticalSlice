[CmdletBinding()]
param()

$ErrorActionPreference = "Stop"

function Invoke-DotNet {
	param(
		[Parameter(Mandatory)]
		[string[]]$Arguments
	)

	& dotnet @Arguments
	if ($LASTEXITCODE -ne 0) {
		throw "dotnet $($Arguments -join ' ') failed with exit code $LASTEXITCODE."
	}
}

$templateSource = Join-Path $PSScriptRoot "Brutiquzz.VerticalSlice"
$tempRoot = Join-Path ([System.IO.Path]::GetTempPath()) "Brutiquzz.VerticalSlice.TemplateVerification-$([Guid]::NewGuid())"
$customHive = Join-Path $tempRoot ".template-hive"
$outputDirectory = Join-Path $tempRoot "GeneratedApi"
$projectName = "Example.Catalog.Api"

try {
	New-Item -ItemType Directory -Path $tempRoot | Out-Null

	Invoke-DotNet @("new", "install", $templateSource, "--debug:custom-hive", $customHive)
	Invoke-DotNet @(
		"new",
		"brutiquzz-verticalslice-api",
		"--name",
		$projectName,
		"--output",
		$outputDirectory,
		"--debug:custom-hive",
		$customHive
	)

	$projectFile = Join-Path $outputDirectory "$projectName.csproj"
	if (-not (Test-Path $projectFile -PathType Leaf)) {
		throw "The generated project file '$projectFile' was not found."
	}

	$sampleFeature = Join-Path $outputDirectory "Features\Product\GetProductInformation.cs"
	if (-not (Test-Path $sampleFeature -PathType Leaf)) {
		throw "The generated project does not contain the Product sample feature."
	}

	$unexpectedFiles = Get-ChildItem $outputDirectory -Recurse -File | Where-Object {
		$_.Name -like "*DataAccess*" -or $_.Extension -eq ".sln"
	}
	if ($unexpectedFiles) {
		throw "The generated API unexpectedly contains solution or DataAccess files: $($unexpectedFiles.FullName -join ', ')"
	}

	$textFiles = Get-ChildItem $outputDirectory -Recurse -File | Where-Object {
		$_.Extension -in ".cs", ".csproj", ".json", ".http"
	}
	$unreplacedNames = $textFiles | Select-String -SimpleMatch "Brutiquzz.VerticalSlice"
	if ($unreplacedNames) {
		throw "The template did not replace all source project names: $($unreplacedNames.Path -join ', ')"
	}

	Invoke-DotNet @("restore", $projectFile)
	Invoke-DotNet @("build", $projectFile, "--no-restore")

	Write-Host "Template verification succeeded for '$projectName'."
}
finally {
	if (Test-Path $tempRoot) {
		Remove-Item $tempRoot -Recurse -Force
	}
}
