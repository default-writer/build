rem SET VSINSTALLDIR=C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise
rem SET PATH=C:\msbuild\sonar-scanner-msbuild-4.2.0.1214-net46;C:\Users\artur\AppData\Local\Apps\OpenCover;%VSINSTALLDIR%\MSBuild\15.0\Bin\amd64;%PATH%
SonarScanner.MSBuild.exe begin^
 /k:"build-core"^
 /n:"build"^
 /v:"1.0"^
 /d:sonar.cs.opencover.reportsPaths="opencover.xml"^
 /d:sonar.coverage.exclusions="**/*Test*.cs"^
 /d:sonar.organization="hack2root-github"^
 /d:sonar.host.url="https://sonarcloud.io"^
 /d:sonar.login="7eaa0a53a471f0280146430327e6a24d98a850af"
rem MSBuild.exe 
dotnet build --configuration Release
OpenCover.Console.exe^
 -oldstyle^
 -output:"opencover.xml"^
 -register:user^
 -target:"vstest.console.exe"^
 -targetargs:"Build.Tests\bin\Release\netcoreapp2.1\Build.Tests.dll"
%VSINSTALLDIR%\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe
SonarScanner.MSBuild.exe end^
 /d:sonar.login="7eaa0a53a471f0280146430327e6a24d98a850af"
