$SONARCLOUDTOKEN=$env:SONARCLOUDTOKEN

& dotnet packages/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0/SonarScanner.MSBuild.dll begin /d:sonar.login="$SONARCLOUDTOKEN" /k:"build-core" /d:sonar.host.url="https://sonarcloud.io" /n:"build" /v:"1.0" /d:sonar.cs.opencover.reportsPaths="Build.Tests/coverage.xml" /d:sonar.coverage.exclusions="**/MyFun*.cs,**/*Test*.cs,**/*Exception*.cs,**/*Attribute*.cs" /d:sonar.organization="hack2root-github" /d:sonar.sourceEncoding="UTF-8"
& dotnet build --configuration Release
& dotnet test --configuration Release  --no-build Build.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
& dotnet packages/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0/SonarScanner.MSBuild.dll end /d:sonar.login="$SONARCLOUDTOKEN"
