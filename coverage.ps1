$DOTNET = "dotnet"

$OPENCOVER=$env:OPENCOVER
$SONARCLOUD=$env:SONARCLOUD
$SONARCLOUDTOKEN=$env:SONARCLOUDTOKEN

& $DOTNET $SONARCLOUD begin `
 /k:build-core `
 /d:sonar.host.url=https://sonarcloud.io `
 /d:sonar.coverage.exclusions=Build.Tests/** `
 /d:sonar.login=$SONARCLOUDTOKEN `
 /d:sonar.verbose=true `
 /d:sonar.cs.opencover.reportsPaths=coverage/coverage.xml

& $OPENCOVER `
 -target:"c:\Program Files\dotnet\dotnet.exe" `
 -targetargs:"test -f netcoreapp2.1 Build.Tests/Build.Tests.csproj" `
 -mergeoutput `
 -hideskipped:File `
 -output:coverage/coverage.xml `
 -oldStyle `
 -filter:"+[Build*]* -[Build.Tests*]*" `
 -searchdirs:$testdir/bin/$CONFIG/netcoreapp2.1 `
 -register:user

& $DOTNET $SONARCLOUD end /d:sonar.login=$env:SONARCLOUDTOKEN"