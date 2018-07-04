if(![System.IO.File]::Exists("packages\tools\dotnet-sonarscanner.exe")) {
  # file with path $path doesn't exist
  dotnet tool install --tool-path packages\tools dotnet-sonarscanner
} else {
  dotnet tool update --tool-path packages\tools dotnet-sonarscanner
}
if(![System.IO.File]::Exists("packages\tools\csmacnz.Coveralls.exe")) {
  # file with path $path doesn't exist
  dotnet tool install --tool-path packages\tools coveralls.net
} else {
  dotnet tool update --tool-path packages\tools coveralls.net
}
& dotnet add Build.Tests package --package-directory packages\.packages OpenCover
& dotnet add Build.Tests package --package-directory packages\.packages coverlet.msbuild
& dotnet restore
& dotnet-sonarscanner begin /d:sonar.login="$env:SONARCLOUDTOKEN" /k:"build-core" /d:sonar.host.url="https://sonarcloud.io" /n:"build" /v:"1.0" /d:sonar.cs.opencover.reportsPaths="Build.Tests/coverage.opencover.xml" /d:sonar.coverage.exclusions="**/Interface*.cs,**/*Test*.cs,**/*Exception*.cs,**/*Attribute*.cs,**/Middleware/*.cs,/Pages/*.cs,**/Program.cs,**/Startup.cs,**/sample/*,**/aspnetcore/*,**/wwwroot/*,**/*.js,**/coverage.opencover.xml" /d:sonar.organization="hack2root-github" /d:sonar.sourceEncoding="UTF-8" /d:sonar.language=cs
& dotnet build --configuration Release
& dotnet test --configuration Release --no-build Build.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
& dotnet-sonarscanner end /d:sonar.login="$env:SONARCLOUDTOKEN"
& csmacnz.Coveralls --opencover -i Build.Tests/coverage.opencover.xml
& dotnet remove Build.Tests package coverlet.msbuild
& dotnet remove Build.Tests package OpenCover