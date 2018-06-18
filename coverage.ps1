& dotnet tool install --tool-path packages dotnet-sonarscanner
& dotnet tool install --tool-path packages coveralls.net
& dotnet add Build.Tests package --package-directory packages OpenCover
& dotnet add Build.Tests package --package-directory packages coverlet.msbuild
& dotnet restore
& packages/dotnet-sonarscanner begin /d:sonar.login="$env:SONARCLOUDTOKEN" /k:"build-core" /d:sonar.host.url="https://sonarcloud.io" /n:"build" /v:"1.0" /d:sonar.cs.opencover.reportsPaths="Build.Tests/coverage.opencover.xml" /d:sonar.coverage.exclusions="**/MyFun*.cs,**/*Test*.cs,**/*Exception*.cs,**/*Attribute*.cs" /d:sonar.organization="hack2root-github" /d:sonar.sourceEncoding="UTF-8"
& dotnet build --configuration Release
& dotnet test --configuration Release --no-build Build.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:ExcludeByFile=\"**\/MyFun*.cs,**\/*Test*.cs,**\/*Exception*.cs,**\/*Attribute*.cs\"
& packages/dotnet-sonarscanner end /d:sonar.login="$env:SONARCLOUDTOKEN"
& packages/csmacnz.Coveralls --opencover -i Build.Tests/coverage.opencover.xml