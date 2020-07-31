@echo off
@if defined _echo echo on

:main
setlocal enabledelayedexpansion
  set errorlevel=

  set BuildConfiguration=Release
  set BuildSolution=Github.Build.sln

  set /p BuildVersion=<"%~dp0.config\BuildVersion.txt"

  set BuildSpec=
  set BuildSpec=Build.DependencyInjection.%BuildVersion%.nupkg

  if "%BuildSpec%"=="" endlocal& exit /b %errorlevel%

  set OutputDirectory=%~dp0nuget

  REM Step 1: Authenticate (if this is the first time)
REM   dotnet nuget add source https://nuget.pkg.github.com/hack2root/index.json -n github -u hack2root -p GH_TOKEN

  call :dotnet_build

  REM Step 2: Pack
  dotnet pack --configuration %BuildConfiguration% %BuildSolution%

  REM Step 3: Publish
  dotnet nuget push "Build/bin/Release/%BuildSpec%" --source "github"

endlocal& exit /b %errorlevel%

:dotnet_build
    call :remove_directory bin                                                                                              || exit /b 1
    call :remove_directory obj                                                                                              || exit /b 1
    echo/
    echo/ ========== NuGet ==========
    echo/   Building %cd%
    echo/ ========== NuGet ==========
    for %%v in (net451 net452 net46 net461 net462 net47 net471 net472 net48 netstandard2.0 netcoreapp2.1 netcoreapp3.1) do (
        dotnet.exe build --verbosity normal --no-dependencies -c %BuildConfiguration% --framework "%%v" %BuildSolution%
    )
    exit /b 0

REM @echo off
REM @if defined _echo echo on

REM :main
REM setlocal enabledelayedexpansion
REM   set errorlevel=

REM   set BuildConfiguration=Release

REM   set /p BuildVersion=<"%~dp0.config\BuildVersion.txt"

REM   set BuildSpec=
REM   set BuildSpec=%1

REM   if "%BuildSpec%"=="" endlocal& exit /b %errorlevel%

REM   set OutputDirectory=%~dp0nuget
REM   call :remove_directory "%OutputDirectory%" || exit /b 1

REM   rem Don't fall back to machine-installed versions of dotnet, only use repo-local version
REM   set DOTNET_MULTILEVEL_LOOKUP=0

REM   call "%~dp0NuGet-dotnet-install.cmd" || exit /b 1

REM   set GitHeadSha=
REM   if defined LV_GIT_HEAD_SHA (
REM     set "GitHeadSha=%LV_GIT_HEAD_SHA%"
REM   )

REM   set procedures=
REM   set procedures=%procedures% build
REM   set procedures=%procedures% build_test
REM   rem set procedures=%procedures% build_test_coverage
REM   set procedures=%procedures% build_nuget

REM   for %%p in (%procedures%) do (
REM     call :%%p || (
REM       call :print_error_message Failed to run %%p
REM       exit /b 1
REM     )
REM   )
REM endlocal& exit /b %errorlevel%

REM :build
REM setlocal
REM   cd /d %~dp0Build.Abstractions
REM   echo/
REM   echo/ ========== NuGet ==========
REM   echo/   Building %cd%
REM   echo/ ========== NuGet ==========
REM   call :dotnet_pack
REM   exit /b %errorlevel%

REM :build_test
REM setlocal
REM   cd /d %~dp0Build.Tests
REM   call :remove_directory bin                                                                                              || exit /b 1
REM   call :remove_directory obj                                                                                              || exit /b 1
REM   echo/
REM   echo/ ========== NuGet ==========
REM   echo/   Restoring %cd%
REM   echo/ ========== NuGet ==========
REM   dotnet.exe restore --no-cache --packages "%~dp0packages\.packages"                                                   || exit /b 1
REM   echo/
REM   echo/ ========== NuGet ==========
REM   echo/   Building %cd%
REM   echo/ ========== NuGet ==========
REM   dotnet.exe build --verbosity normal -c %BuildConfiguration% > build.log                                                 || exit /b 1
REM   echo/
REM   echo/ ========== NuGet ==========
REM   echo/   Testing %cd%
REM   echo/ ========== NuGet ==========
REM   dotnet.exe test --no-build -c %BuildConfiguration%                                                                      || exit /b 1
REM   exit /b %errorlevel%

REM :build_nuget
REM setlocal
REM   cd /d %~dp0
REM   for %%f in (%BuildSpec%) do (
REM     nuget pack .nuget\%%f -Properties Configuration=Release;BuildVersion=%BuildVersion%;GitHeadSha=%GitHeadSha% -OutputDirectory "%OutputDirectory%"
REM   )
REM   if "%NUGET_ACCESSTOKEN%" == "" (
REM     echo/ 
REM     echo/ ========== NuGet ==========
REM     echo/ Missing NuGet access token environment variable API key
REM     echo/ ========== NuGet ==========
REM   )
REM   if not "%NUGET_ACCESSTOKEN%" == "" (
REM     del /f /s /q %OutputDirectory%\*.symbols.nupkg
REM     for /f "tokens=* usebackq" %%f in (`dir /B %OutputDirectory%\*.nupkg`) do (
REM       echo/ 
REM       echo/ ========== NuGet ==========
REM       echo/ Uploading NuGet package %OutputDirectory%\%%f
REM       echo/ ========== NuGet ==========
REM       dotnet nuget push %OutputDirectory%\%%f -k %NUGET_ACCESSTOKEN% -s https://api.nuget.org/v3/index.json                                   
REM     )
REM   )
REM   call :remove_directory %~dp0packages\.packages
REM   exit /b 0

REM :dotnet_build
REM   call :remove_directory bin                                                                                              || exit /b 1
REM   call :remove_directory obj                                                                                              || exit /b 1
REM   echo/
REM   echo/ ========== NuGet ==========
REM   echo/   Building %cd%
REM   echo/ ========== NuGet ==========
REM   echo/ > build.log
REM   for %%v in (net451 net452 net46 net461 net462 net47 net471 net472 net48 netstandard2.0 netcoreapp2.1 netcoreapp3.1) do (
REM     dotnet.exe build --verbosity normal --no-dependencies -c %BuildConfiguration% --framework "%%v" >> build.log                             
REM   )
REM   exit /b 0

REM :dotnet_pack
REM setlocal
REM   echo/
REM   echo/ ========== NuGet ==========
REM   echo/   Restoring %cd%
REM   echo/ ========== NuGet ==========
REM   dotnet.exe restore --no-cache --packages "%~dp0packages\.packages"                                                   || exit /b 1
REM   call :dotnet_build                                                                                                      || exit /b 1

REM   dotnet.exe publish -c %BuildConfiguration%                                                                              || exit /b 1
   
REM   echo/
REM   echo/ ========== NuGet ==========
REM   echo/   Packing %cd%
REM   echo/ ========== NuGet ==========
REM   set MsBuildArgs=
REM   set "MsBuildArgs=%MsBuildArgs% --no-build"
REM   set "MsBuildArgs=%MsBuildArgs% -c %BuildConfiguration%"
REM   set "MsBuildArgs=%MsBuildArgs% --output "%OutputDirectory%""
REM   set "MsBuildArgs=%MsBuildArgs% --include-symbols --include-source"
REM   if defined LV_GIT_HEAD_SHA (
REM     set "MsBuildArgs=%MsBuildArgs% /p:GitHeadSha=%LV_GIT_HEAD_SHA%"
REM   )
REM   dotnet.exe pack %MsBuildArgs% || exit /b 1

REM   exit /b %errorlevel%

REM :build_test_coverage
REM setlocal
REM   cd /d %~dp0
REM   dotnet.exe restore --no-cache --packages "%~dp0packages\.packages"                                                   || exit /b 1
REM   call coverage                                                                                                           || exit /b 1
REM   exit /b %errorlevel%

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