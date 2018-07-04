@echo off
@if defined _echo echo on

:main
setlocal enabledelayedexpansion
  set errorlevel=

  set /p BuildVersion=<"%~dp0BuildVersion.txt"

  set BuildConfiguration=%~1
  if "%BuildConfiguration%"=="" set BuildConfiguration=Release

  set OutputDirectory=%~dp0MyGetLocalPackages
  call :remove_directory "%OutputDirectory%" || exit /b 1

  rem Don't fall back to machine-installed versions of dotnet, only use repo-local version
  set DOTNET_MULTILEVEL_LOOKUP=0

  call "%~dp0.\MyGet-dotnet-install.cmd" || exit /b 1

  echo DotNet CLI
  where.exe /R %~dp0 /F dotnet.exe

  echo NuGet CLI 
  where.exe /R %~dp0 /F nuget.exe

  set procedures=
  set procedures=%procedures% build
  set procedures=%procedures% build_test
  set procedures=%procedures% build_myget

  for %%p in (%procedures%) do (
    call :%%p || (
      call :print_error_message Failed to run %%p
      exit /b 1
    )
  )
endlocal& exit /b %errorlevel%

:build
setlocal
  cd /d %~dp0\Build
  call :dotnet_pack
  exit /b %errorlevel%

:build_test
setlocal
  cd /d %~dp0\Build.Tests
  dotnet.exe restore --no-cache --packages "%~dp0packages"                                                                || exit /b 1
  echo/
  echo/  ==========
  echo/   Testing %cd%
  echo/  ==========
  call :remove_directory bin                                                                                              || exit /b 1
  call :remove_directory obj                                                                                              || exit /b 1
  dotnet.exe restore --no-cache --packages "%~dp0packages"                                                                || exit /b 1
  dotnet.exe build -c %BuildConfiguration%                                                                                || exit /b 1
  dotnet.exe test --no-build -c %BuildConfiguration%                                                                      || exit /b 1
  exit /b %errorlevel%

:build_myget
setlocal
  cd /d %~dp0
  if "%MYGET_ACCESSTOKEN%" == "" (
    call :print_error_message Missing MyGet access token environment variable API key
    exit /b 1
  )
  for /f "tokens=* usebackq" %%f in (`dir /B MyGet\*.nuspec`) do (
    nuget pack MyGet\%%f -Properties Configuration=Release;BuildVersion=%BuildVersion% -OutputDirectory "%OutputDirectory%"
  )
  for /f "tokens=* usebackq" %%f in (`dir /B %OutputDirectory%\*.nupkg`) do (
    nuget push %OutputDirectory%\%%f %MYGET_ACCESSTOKEN% -Source https://www.myget.org/F/build-core/api/v2/package
  )
  call :remove_directory "%OutputDirectory%" || exit /b 1
  exit /b 0

:dotnet_build
  echo/
  echo/  ==========
  echo/   Building %cd%
  echo/  ==========
  call :remove_directory bin                                                                                              || exit /b 1
  call :remove_directory obj                                                                                              || exit /b 1

  for %%v in (net45 net451 net452 net46 net461 net462 net47 net471 netstandard2.0 netcoreapp2.1) do (
    dotnet.exe build --no-dependencies -c %BuildConfiguration% --framework "%%v" || exit /b 1
  )
  exit /b 0

:dotnet_pack
setlocal
  dotnet.exe restore --no-cache --packages "%~dp0packages"                                                                || exit /b 1
  call :dotnet_build                                                                                                      || exit /b 1
  dotnet.exe publish -c %BuildConfiguration%                                                                              || exit /b 1
  exit /b 0

:print_error_message
  echo/
  echo/  [ERROR] %*
  echo/
  exit /b %errorlevel%

:remove_directory
  if "%~1" == "" (
    call :print_error_message Directory name was not specified.
    exit /b 1
  )
  if exist "%~1" rmdir /s /q "%~1"
  if exist "%~1" (
    call :print_error_message Failed to remove directory "%~1".
    exit /b 1
  )
  exit /b 0
:exit