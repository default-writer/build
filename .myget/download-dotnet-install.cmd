@echo off
@if defined _echo echo on

:install_dotnet_cli
setlocal
  set "DOTNET_MULTILEVEL_LOOKUP=0"

  set /p DotNet_Version=<"%~dp0..\.config\DotNetCLIVersion.txt"
  if not defined DotNet_Version (
    call :print_error_message Unknown DotNet CLI Version.
    exit /b 1
  )

  set /p NuGet_Version=<"%~dp0..\.config\NuGetCLIVersion.txt"
  if not defined NuGet_Version (
    call :print_error_message Unknown NuGet CLI Version.
    exit /b 1
  )

  set DotNet_Installer_Url=https://raw.githubusercontent.com/dotnet/cli/release/2.0.0/scripts/obtain/dotnet-install.ps1

  echo Downloading dotnet installer script dotnet-install.ps1
  powershell -NoProfile -ExecutionPolicy unrestricted -Command "Invoke-WebRequest -Uri '%DotNet_Installer_Url%' -OutFile '%~dp0dotnet-install.ps1'"

endlocal& (
  exit /b 0
)

:print_error_message
  echo/
  echo/  [ERROR] %*
  echo/
  exit /b %errorlevel%

:exit