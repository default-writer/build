dotnet C:\msbuild\sonar-scanner\SonarScanner.MSBuild.dll begin^
 /k:"build-core"^
 /n:"build"^
 /v:"1.0"^
 /d:sonar.cs.opencover.reportsPaths="opencover.xml"^
 /d:sonar.coverage.exclusions="**/*Test*.cs"^
 /d:sonar.organization="hack2root-github"^
 /d:sonar.host.url="https://sonarcloud.io"^
 /d:sonar.login="7eaa0a53a471f0280146430327e6a24d98a850af"
dotnet build --configuration Release
OpenCover.Console.exe^
 -oldstyle^
 -output:"opencover.xml"^
 -register:user^
 -target:"vstest.console.exe"^
 -targetargs:"Build.Tests\bin\Release\netcoreapp2.1\Build.Tests.dll"
dotnet C:\msbuild\sonar-scanner\SonarScanner.MSBuild.dll end^
 /d:sonar.login="7eaa0a53a471f0280146430327e6a24d98a850af"
