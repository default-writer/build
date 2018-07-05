@echo off
setlocal
  
  set /p DotNet_Version=<"%~dp0.config\DotNetCLIVersion.txt"
  set DotNet_Path=%~dp0packages\dotnet\%DotNet_Version%
  set NuGet_Path=%~dp0packages\nuget
  set Tools_Path=%~dp0packages\tools

  set "PATH=%DotNet_Path%;%NuGet_Path%;%Tools_Path%;%PATH%"
  powershell -file "./coverage.ps1"

endlocal

pskill dotnet.exe
powershell -command "rm -recurse -force .sonarqube"