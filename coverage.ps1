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

& $OPENCOVER `
 -target:"c:\Program Files\dotnet\dotnet.exe" `
 -targetargs:"test -f netcoreapp2.1 Build.Tests/Build.Tests.csproj" `
 -mergeoutput `
 -hideskipped:File `
 -output:coverage/coverage.xml `
 -oldStyle `
 -filter:"+[Build*]* -[Build.Tests*]*" `
 -searchdirs:Build.Tests/bin/$CONFIG/netcoreapp2.1 `
 -register:user

& $DOTNET $SONARCLOUD end /d:sonar.login=$SONARCLOUDTOKEN"
