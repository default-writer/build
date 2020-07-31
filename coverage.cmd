@echo off
setlocal
  
  set /p DotNet_Version=<"%~dp0.config\DotNetCLIVersion.txt"
  set DotNet_Path=%~dp0packages\dotnet\%DotNet_Version%
  set NuGet_Path=%~dp0packages\nuget
  set Tools_Path=%~dp0packages\tools

  set "PATH=%DotNet_Path%;%NuGet_Path%;%Tools_Path%;%PATH%"
  powershell -file "./coverage.ps1"

endlocal

set /a count = 0
for /f "tokens=1* delims=, " %%l in ('tasklist') do (
  if "%%l" == "dotnet.exe" (
    set /a count += 1
  )
)
if %count% neq 0 (
  call taskkill /IM dotnet.exe /F > nul
)

powershell -command "rm -recurse -force .sonarqube"