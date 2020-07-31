@echo off
@if defined _echo echo on

:main
setlocal EnableDelayedExpansion

  set errorlevel=
  set BuildConfiguration=Release

  set BuildSolution=Build.sln

  set /p BuildVersion=<"%~dp0.config\BuildVersion.txt"

  set LV_GIT_HEAD_SHA=
  for /f %%c in ('git rev-parse HEAD') do set "LV_GIT_HEAD_SHA=%%c"

  echo/ ==================
  echo/  %LV_GIT_HEAD_SHA%
  echo/ ==================

  echo/ ==================
  echo/  Building %BuildVersion% %BuildConfiguration% version of NuGet packages.
  echo/ ==================
  
  REM Check that git is on path.
  where.exe /Q git.exe || (
    echo ERROR: git.exe is not in the path.
    exit /b 1
  )

  set /a count = 0
  for /f "tokens=1* delims=, " %%l in ('tasklist') do (
    if "%%l" == "dotnet.exe" (
      set /a count += 1
    )
  )
  if %count% neq 0 (
    call taskkill /IM dotnet.exe /F > nul
  )

  set /a count = 0
  for /f %%l in ('git clean -xdn') do set /a count += 1
  for /f %%l in ('git status --porcelain') do set /a count += 1
  if %count% neq 0 (
    git clean -xdf
  )

  set /a count = 0
  for /f %%l in ('git clean -xdn') do set /a count += 1
  for /f %%l in ('git status --porcelain') do set /a count += 1
  if %count% neq 0 (
    choice.exe /T 10 /D N /C YN /M "WARNING: The repo contains uncommitted changes and you are building for publication. Press Y to continue or N to stop. "
    if !errorlevel! neq 1 exit /b 1
  )

  set LV_GIT_HEAD_SHA=
  for /f %%c in ('git rev-parse HEAD') do set "LV_GIT_HEAD_SHA=%%c"

  set LocalDotNet_PackagesDir=%~dp0packages
  if exist "%LocalDotNet_PackagesDir%" rmdir /s /q "%LocalDotNet_PackagesDir%"
  if exist "%LocalDotNet_PackagesDir%" (
    echo ERROR: Failed to remove "%LocalDotNet_PackagesDir%" folder.
    exit /b 1
  )

  echo/ ==================
  echo/  %LV_GIT_HEAD_SHA%
  echo/ ==================

  echo/ ==================
  echo/  Building %BuildVersion% %BuildConfiguration% version of NuGet packages.
  echo/ ==================

  call .nuget\_NuGet.cmd %BuildConfiguration% %BuildVersion%
  set OutputDirectory=%~dp0..\.nuget\nuget
  call :remove_directory "%OutputDirectory%" || exit /b 1

  set /a count = 0
  for /f "tokens=1* delims=, " %%l in ('tasklist') do (
    if "%%l" == "dotnet.exe" (
      set /a count += 1
    )
  )
  if %count% neq 0 (
    call taskkill /IM dotnet.exe /F > nul
  )

  call .myget\_MyGet.cmd %BuildConfiguration% %BuildVersion%
  set OutputDirectory=%~dp0.myget\myget
  call :remove_directory "%OutputDirectory%" || exit /b 1

  set /a count = 0
  for /f "tokens=1* delims=, " %%l in ('tasklist') do (
    if "%%l" == "dotnet.exe" (
      set /a count += 1
    )
  )
  if %count% neq 0 (
    call taskkill /IM dotnet.exe /F > nul
  )

endlocal&  exit /b %errorlevel%

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