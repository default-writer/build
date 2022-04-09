@echo off
@if defined _echo echo on

:main
setlocal enabledelayedexpansion
  set errorlevel=

  set BuildConfiguration=Release

  set BuildSolution=.github/Build.Abstractions.sln

  set LV_GIT_HEAD_SHA=
  for /f %%c in ('git rev-parse HEAD') do set "LV_GIT_HEAD_SHA=%%c"

  set GitHeadSha=
  if defined LV_GIT_HEAD_SHA (
    set "GitHeadSha=%LV_GIT_HEAD_SHA%"
  )

  set /p BuildVersion=<"%~dp0.config\BuildVersion.txt"

  echo/ ==================
  echo/  %LV_GIT_HEAD_SHA%
  echo/ ==================

  echo/ ==================
  echo/  Building %BuildVersion% %BuildConfiguration% version of NuGet packages.
  echo/ ==================

  set BuildSpec=
  set BuildSpec=Build.DependencyInjection.Abstractions.%BuildVersion%.nupkg

  if "%BuildSpec%"=="" endlocal& exit /b %errorlevel%

  set OutputDirectory=%~dp0nuget

  REM Step 1: Authenticate (if this is the first time)
REM   dotnet nuget add source https://nuget.pkg.github.com/funcelot/index.json -n github -u funcelot -p GH_TOKEN

  call :dotnet_build

  REM Step 2: Pack
  dotnet pack --configuration %BuildConfiguration% %BuildSolution%

  REM Step 3: Publish
  dotnet nuget push "Build.Abstractions/bin/Release/%BuildSpec%" --source "github"

endlocal& exit /b %errorlevel%

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

:dotnet_build
  call :remove_directory bin                                                                                              || exit /b 1
  call :remove_directory obj                                                                                              || exit /b 1
  echo/
  echo/ ========== NuGet ==========
  echo/  Building %cd%
  echo/ ========== NuGet ==========
  echo/ > build.log
  echo/
  echo/ ========== NuGet ==========
  for %%v in (net35 net46 net461 net462 net47 net471 net472 net48 netstandard2.0 netcoreapp3.1 net6.0) do (
    echo/  Building %%v
    dotnet.exe build --verbosity normal --no-dependencies -c %BuildConfiguration% --framework "%%v" %BuildSolution% >> build.log
  )
  echo/ ========== NuGet ==========
  exit /b 0

:exit