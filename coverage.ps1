$DOTNET = "dotnet"

$OPENCOVER="packages/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0/SonarCloud.MSBuild.dll"
$SONARCLOUD="packages/OpenCover.4.6.519/tools/OpenCover.Console.exe"
$SONARCLOUDTOKEN=$env:SONARCLOUDTOKEN
$CONFIG = "Release"

& $DOTNET $SONARCLOUD begin `
 /k:"build-core" `
 /d:sonar.host.url="https://sonarcloud.io" `
 /d:sonar.coverage.exclusions="Build.Tests/**" `
 /d:sonar.login="$SONARCLOUDTOKEN" `
 /d:sonar.verbose="true" `
 /d:sonar.cs.opencover.reportsPaths="coverage/coverage.xml""

 $DOTNET build --configuration $CONFIG 

& $DOTNET $SONARCLOUD end /d:sonar.login=$SONARCLOUDTOKEN"
