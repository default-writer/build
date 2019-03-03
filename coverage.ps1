& cd .nuget 
& ./NuGet-dotnet-install
& cd ..

$CurrentDir = Convert-Path .
$DotNetCliVersionFile = Join-Path -Path $CurrentDir -ChildPath ".config/DotNetCliVersion.txt"
$DotNet_Version = [System.IO.File]::ReadAllText($DotNetCliVersionFile)
$Tools_Path = Join-Path -Path $CurrentDir -ChildPath  "packages/tools"
$DotNet_Path = Join-Path -Path $CurrentDir -ChildPath "packages/dotnet/$DotNet_Version"
$NuGet_Path = Join-Path -Path $CurrentDir -ChildPath  "packages/nuget"
$SonarScanner = Join-Path -Path $CurrentDir -ChildPath "packages/tools/dotnet-sonarscanner.exe"

Set-Item -Path Env:PATH -Value ("$DotNet_Path;$NuGet_Path;$Tools_Path;" + $Env:PATH)

if(![System.IO.File]::Exists($SonarScanner)) {
  # file with path $path doesn't exist
  dotnet tool install --tool-path packages/tools dotnet-sonarscanner
} else {
  dotnet tool update --tool-path packages/tools dotnet-sonarscanner
}
if(![System.IO.File]::Exists($SonarScanner)) {
  # file with path $path doesn't exist
  dotnet tool install --tool-path packages/tools coveralls.net
} else {
  dotnet tool update --tool-path packages/tools coveralls.net
}

#& dotnet add Build.Tests package --package-directory packages/.packages OpenCover
#& dotnet add Build.Tests package --package-directory packages/.packages coverlet.msbuild
& dotnet restore

& dotnet-sonarscanner begin /o:"hack2root-github" /d:sonar.login="$env:SONARCLOUDTOKEN" /k:"build-core" /d:sonar.host.url="https://sonarcloud.io" /n:"build" /v:"1.0" /d:sonar.cs.opencover.reportsPaths="Build.Tests/coverage.opencover.xml" /d:sonar.coverage.exclusions="**/Interface*.cs,**/*Test*.cs,**/*Exception*.cs,**/*Attribute*.cs,**/Middleware/*.cs,/Pages/*.cs,**/Program.cs,**/Startup.cs,**/sample/*,**/aspnetcore/*,**/wwwroot/*,**/*.js,**/coverage.opencover.xml" /d:sonar.sourceEncoding="UTF-8" /d:sonar.language=cs
& dotnet build --configuration Release
& dotnet test --configuration Release --no-build Build.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
& dotnet-sonarscanner end /d:sonar.login="$env:SONARCLOUDTOKEN"

& nuget install xunit.runner.console -OutputDirectory packages/tools -Version 2.3.1
& nuget install OpenCover -OutputDirectory packages/tools -Version 4.6.166
& OpenCover.Console -register:user -target:dotnet.exe "-targetargs:""xunit.runner.console.2.3.1/tools/netcoreapp3.0/xunit.console.dll"" ""Build.Tests/bin/Release/netcoreapp3.0/Build.Tests.dll"" -noshadow -appveyor" -filter:"+[Build*]*" -oldStyle -output:opencoverCoverage.xml
& csmacnz.Coveralls --opencover -i opencoverCoverage.xml --repoToken $env:COVERALLS_REPO_TOKEN --commitId $env:APPVEYOR_REPO_COMMIT --commitBranch $env:APPVEYOR_REPO_BRANCH --commitAuthor $env:APPVEYOR_REPO_COMMIT_AUTHOR --commitEmail $env:APPVEYOR_REPO_COMMIT_AUTHOR_EMAIL --commitMessage $env:APPVEYOR_REPO_COMMIT_MESSAGE --jobId $env:APPVEYOR_JOB_ID
#& csmacnz.Coveralls --opencover -i Build.Tests/coverage.opencover.xml

#& dotnet remove Build.Tests package coverlet.msbuild
#& dotnet remove Build.Tests package OpenCover
