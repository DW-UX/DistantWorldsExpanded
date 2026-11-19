# Re-runs the UI scale rewrite from pristine sources.
#   -Apply   write the changes (otherwise dry run)
#   -Reset   first restore every tracked source file with `git checkout` (discards hand edits!)
param([switch]$Apply, [switch]$Reset)

$ErrorActionPreference = 'Stop'
$repo = Split-Path $PSScriptRoot -Parent
Set-Location $repo
[Environment]::CurrentDirectory = $repo

if ($Reset) {
    git checkout -- DistantWorldsExpanded
}

dotnet build tools\UiScaleRewriter -c Release -v q -nologo -clp:ErrorsOnly
if ($LASTEXITCODE -ne 0) { throw 'tool build failed' }

New-Item -ItemType Directory -Force tools\dump | Out-Null
dotnet build DistantWorldsExpanded.sln -c Release -v q -nologo -clp:ErrorsOnly `
    "-p:CustomAfterMicrosoftCommonTargets=$repo\tools\DumpCompile.targets" "-p:DumpDir=$repo\tools\dump\"
if ($LASTEXITCODE -ne 0) { throw 'game build failed' }

$toolArgs = @("$repo\tools\dump\DistantWorldsExpanded", '--report', "$repo\tools\report.txt")
if ($Apply) { $toolArgs += '--apply' }
dotnet tools\UiScaleRewriter\bin\Release\net10.0\UiScaleRewriter.dll @toolArgs
