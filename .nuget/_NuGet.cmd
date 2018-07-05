@echo off
@if defined _echo echo on

:main
setlocal enabledelayedexpansion
  set errorlevel=

  set BuildConfiguration=%~1
  if "%BuildConfiguration%"=="" set BuildConfiguration=Release

  set BuildVersion=%~2
  if "%BuildVersion%"=="" set /p BuildVersion=<"%~dp0..\.config\BuildVersion.txt"

  set OutputDirectory=%~dp0nuget
  call :remove_directory "%OutputDirectory%" || exit /b 1

  rem Don't fall back to machine-installed versions of dotnet, only use repo-local version
  set DOTNET_MULTILEVEL_LOOKUP=0

  call "%~dp0NuGet-dotnet-install.cmd" || exit /b 1

  set GitHeadSha=
  if defined LV_GIT_HEAD_SHA (
    set "GitHeadSha=%LV_GIT_HEAD_SHA%"
  )

  set procedures=
  set procedures=%procedures% build
  set procedures=%procedures% build_test
  set procedures=%procedures% build_test_coverage
  set procedures=%procedures% build_nuget

  for %%p in (%procedures%) do (
    call :%%p || (
      call :print_error_message Failed to run %%p
      exit /b 1
    )
  )
endlocal& exit /b %errorlevel%

:build
setlocal
  cd /d %~dp0..\Build
  echo/
  echo/ ========== NuGet ==========
  echo/   Building %cd%
  echo/ ========== NuGet ==========
  call :dotnet_pack
  exit /b %errorlevel%

:build_test
setlocal
  cd /d %~dp0..\Build.Tests
  call :remove_directory bin                                                                                              || exit /b 1
  call :remove_directory obj                                                                                              || exit /b 1
  echo/
  echo/ ========== NuGet ==========
  echo/   Restoring %cd%
  echo/ ========== NuGet ==========
  dotnet.exe restore --no-cache --packages "%~dp0..\packages\.packages"                                                   || exit /b 1
  echo/
  echo/ ========== NuGet ==========
  echo/   Building %cd%
  echo/ ========== NuGet ==========
  dotnet.exe build --verbosity normal -c %BuildConfiguration% > build.log                                                 || exit /b 1
  echo/
  echo/ ========== NuGet ==========
  echo/   Testing %cd%
  echo/ ========== NuGet ==========
  dotnet.exe test --no-build -c %BuildConfiguration%                                                                      || exit /b 1
  exit /b %errorlevel%

:build_nuget
setlocal
  cd /d %~dp0..\
  for /f "tokens=* usebackq" %%f in (`dir /B .nuget\*.nuspec`) do (
    nuget pack .nuget\%%f -Properties Configuration=Release;BuildVersion=%BuildVersion%;GitHeadSha=%GitHeadSha% -OutputDirectory "%OutputDirectory%"
  )
  if "%NUGET_ACCESSTOKEN%" == "" (
    echo/ 
    echo/ ========== NuGet ==========
    echo/ Missing NuGet access token environment variable API key
    echo/ ========== NuGet ==========
  )
  if not "%NUGET_ACCESSTOKEN%" == "" (
    for /f "tokens=* usebackq" %%f in (`dir /B %OutputDirectory%\*.nupkg`) do (
      echo/ 
      echo/ ========== NuGet ==========
     echo/ Uploading NuGet package %OutputDirectory%\%%f
      echo/ ========== NuGet ==========
      dotnet nuget push %OutputDirectory%\%%f -k %NUGET_ACCESSTOKEN% -s https://api.nuget.org/v3/index.json                                   
    )
  )
  call :remove_directory %~dp0..\packages\.packages
  exit /b 0

:dotnet_build
  call :remove_directory bin                                                                                              || exit /b 1
  call :remove_directory obj                                                                                              || exit /b 1
  echo/
  echo/ ========== NuGet ==========
  echo/   Building %cd%
  echo/ ========== NuGet ==========
  echo/ > build.log
  for %%v in (net45 net451 net452 net46 net461 net462 net47 net471 net472 netstandard2.0 netcoreapp2.1) do (
    dotnet.exe build --verbosity normal --no-dependencies -c %BuildConfiguration% --framework "%%v" >> build.log                             
  )
  exit /b 0

:dotnet_pack
setlocal
  echo/
  echo/ ========== NuGet ==========
  echo/   Restoring %cd%
  echo/ ========== NuGet ==========
  dotnet.exe restore --no-cache --packages "%~dp0..\packages\.packages"                                                   || exit /b 1
  call :dotnet_build                                                                                                      || exit /b 1

  dotnet.exe publish -c %BuildConfiguration%                                                                              || exit /b 1
   
  echo/
  echo/ ========== NuGet ==========
  echo/   Packing %cd%
  echo/ ========== NuGet ==========
  set MsBuildArgs=
  set "MsBuildArgs=%MsBuildArgs% --no-build"
  set "MsBuildArgs=%MsBuildArgs% -c %BuildConfiguration%"
  set "MsBuildArgs=%MsBuildArgs% --output "%OutputDirectory%""
  set "MsBuildArgs=%MsBuildArgs% --include-symbols --include-source"
  if defined LV_GIT_HEAD_SHA (
    set "MsBuildArgs=%MsBuildArgs% /p:GitHeadSha=%LV_GIT_HEAD_SHA%"
  )
  dotnet.exe pack %MsBuildArgs% || exit /b 1

  exit /b %errorlevel%

:build_test_coverage
setlocal
  cd /d %~dp0..\
  dotnet.exe restore --no-cache --packages "%~dp0..\packages\.packages"                                                   || exit /b 1
  call coverage                                                                                                           || exit /b 1
  exit /b %errorlevel%

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