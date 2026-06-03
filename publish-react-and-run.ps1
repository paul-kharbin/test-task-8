$ErrorActionPreference = "Stop"

$front = "$PSScriptRoot\TestTask.Web.Front"
$web = "$PSScriptRoot\TestTask.Web"
$app = "$web\wwwroot"

Push-Location $front
npm install
npm run build
Pop-Location
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

New-Item -ItemType Directory -Force $app | Out-Null
Get-ChildItem $app -Force | Remove-Item -Recurse -Force
Copy-Item "$front\build\client\*" $app -Recurse -Force

dotnet run --project "$web\TestTask.Web.csproj"
