@echo off
set PATH=;%LOCALAPPDATA%\Apps\OpenCover\;C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin;C:\msbuild\sonar-scanner-msbuild-4.2.0.1214-net46;%PATH%
set VSINSTALLDIR=C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise

SonarScanner.MSBuild.exe begin /k:"build-core" /n:"build" /v:"1.0" /d:sonar.cs.opencover.reportsPaths="%CD%\opencover.xml" /d:sonar.coverage.exclusions="**/*Test*.cs" /d:sonar.organization="hack2root-github" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="7eaa0a53a471f0280146430327e6a24d98a850af"
dotnet build
OpenCover.Console.exe -oldstyle -output:"%CD%\opencover.xml" -register:user -target:"%VSINSTALLDIR%\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" -targetargs:"Build.Tests\bin\Debug\netcoreapp2.1\Build.Tests.dll"
SonarScanner.MSBuild.exe end /d:sonar.login="7eaa0a53a471f0280146430327e6a24d98a850af"

