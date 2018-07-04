if(![System.IO.File]::Exists("packages\dotnet-sonarscanner.exe")) {
  # file with path $path doesn't exist
  dotnet tool install --tool-path packages dotnet-sonarscanner
} else {
  dotnet tool update --tool-path packages dotnet-sonarscanner
}
if(![System.IO.File]::Exists("packages\csmacnz.Coveralls.exe")) {
  # file with path $path doesn't exist
  dotnet tool install --tool-path packages coveralls.net
} else {
  dotnet tool update --tool-path packages coveralls.net
}
& dotnet add Build.Tests package --package-directory packages OpenCover
& dotnet add Build.Tests package --package-directory packages coverlet.msbuild
& dotnet restore
& packages/dotnet-sonarscanner begin /d:sonar.login="$env:SONARCLOUDTOKEN" /k:"build-core" /d:sonar.host.url="https://sonarcloud.io" /n:"build" /v:"1.0" /d:sonar.cs.opencover.reportsPaths="Build.Tests/coverage.opencover.xml" /d:sonar.coverage.exclusions="**/Interface*.cs,**/*Test*.cs,**/*Exception*.cs,**/*Attribute*.cs,**/Middleware/*.cs,/Pages/*.cs,**/Program.cs,**/Startup.cs,**/sample/*,**/aspnetcore/*,**/wwwroot/*,**/*.js,**/*.xml,**/*,!*.cs" /d:sonar.organization="hack2root-github" /d:sonar.sourceEncoding="UTF-8" /d:sonar.language=cs
& dotnet build --configuration Release
& dotnet test --configuration Release --no-build Build.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
& packages/dotnet-sonarscanner end /d:sonar.login="$env:SONARCLOUDTOKEN"
& packages/csmacnz.Coveralls --opencover -i Build.Tests/coverage.opencover.xml
& dotnet remove Build.Tests package coverlet.msbuild
& dotnet remove Build.Tests package OpenCover