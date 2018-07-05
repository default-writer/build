@echo off
@if defined _echo echo on

:main
setlocal enabledelayedexpansion
  set errorlevel=

  set BuildConfiguration=%~1
  if "%BuildConfiguration%"=="" set BuildConfiguration=Release

  set BuildVersion=%~2
  if "%BuildVersion%"=="" set /p BuildVersion=<"%~dp0..\.config\BuildVersion.txt"

  set OutputDirectory=%~dp0myget
  call :remove_directory "%OutputDirectory%" || exit /b 1

  rem Don't fall back to machine-installed versions of dotnet, only use repo-local version
  set DOTNET_MULTILEVEL_LOOKUP=0

  call "%~dp0MyGet-dotnet-install.cmd" || exit /b 1

  set GitHeadSha=
  if defined LV_GIT_HEAD_SHA (
    set "GitHeadSha=%LV_GIT_HEAD_SHA%"
  )

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
  cd /d %~dp0..\Build
  echo/
  echo/ ========== MyGet ==========
  echo/   Building %cd%
  echo/ ========== MyGet ==========
  call :dotnet_pack
  exit /b %errorlevel%

:build_test
setlocal
  cd /d %~dp0..\Build.Tests
  call :remove_directory bin                                                                                              || exit /b 1
  call :remove_directory obj                                                                                              || exit /b 1
  echo/
  echo/ ========== MyGet ==========
  echo/   Restoring %cd%
  echo/ ========== MyGet ==========
  dotnet.exe restore --no-cache --packages "%~dp0..\packages\.packages"                                                   || exit /b 1
  echo/
  echo/ ========== MyGet ==========
  echo/   Building %cd%
  echo/ ========== MyGet ==========
  dotnet.exe build  --verbosity normal -c %BuildConfiguration% > build.log                                                || exit /b 1
  echo/
  echo/ ========== MyGet ==========
  echo/   Testing %cd%
  echo/ ========== MyGet ==========
  dotnet.exe test --no-build -c %BuildConfiguration%                                                                      || exit /b 1
  exit /b %errorlevel%

:build_myget
setlocal
  cd /d %~dp0..\
  for /f "tokens=* usebackq" %%f in (`dir /B .myget\*.nuspec`) do (
    nuget pack .myget\%%f -Properties Configuration=Release;BuildVersion=%BuildVersion%;GitHeadSha=%GitHeadSha% -OutputDirectory "%OutputDirectory%"
  )
  if "%MYGET_ACCESSTOKEN%" == "" (
    echo/ 
    echo/ ========== MyGet ==========
    echo/ Missing MyGet access token environment variable API key
    echo/ ========== MyGet ==========
  )
  if not "%MYGET_ACCESSTOKEN%" == "" (
    for /f "tokens=* usebackq" %%f in (`dir /B %OutputDirectory%\*.nupkg`) do (
      echo/ 
      echo/ ========== MyGet ==========
      echo/ Uploading MyGet package %OutputDirectory%\%%f
      echo/ ========== MyGet ==========
      nuget push %OutputDirectory%\%%f %MYGET_ACCESSTOKEN% -Source https://www.myget.org/F/build-core/api/v2/package
    )
  )
  call :remove_directory %~dp0..\packages\.packages
  exit /b 0

:dotnet_build
  call :remove_directory bin                                                                                              || exit /b 1
  call :remove_directory obj                                                                                              || exit /b 1
  echo/
  echo/ ========== MyGet ==========
  echo/   Building %cd%
  echo/ ========== MyGet ==========
  echo/ > build.log
  for %%v in (net45 net451 net452 net46 net461 net462 net47 net471 netstandard2.0 netcoreapp2.1) do (
    dotnet.exe build --verbosity normal --no-dependencies -c %BuildConfiguration% --framework "%%v" >> build.log
  )
  exit /b 0

:dotnet_pack
setlocal
  echo/
  echo/ ========== MyGet ==========
  echo/   Restoring %cd%
  echo/ ========== MyGet ==========
  dotnet.exe restore --no-cache --packages "%~dp0..\packages\.packages"                                                   || exit /b 1
  call :dotnet_build                                                                                                      || exit /b 1
  echo/
  echo/ ========== MyGet ==========
  echo/   Publishing %cd%
  echo/ ========== MyGet ==========
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