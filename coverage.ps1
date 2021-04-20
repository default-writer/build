& cd .nuget 
& ./NuGet-dotnet-install
& cd ..

$CurrentDir = Convert-Path .

$DotNet_Version = [System.IO.File]::ReadAllText($(Join-Path -Path $CurrentDir -ChildPath ".config/DotNetCliVersion.txt"))

$OpenCover_Version = "4.7.922"
$XUnitRunnerConsole_Version = "2.4.1"
# Moving from version 2.5.1 to 2.6.0 requires /p:Include=[build.*]* /p:Exclude="[xunit*]*,[build.abstractions.*]*"
$CoverletMsbuild_Version = "3.0.3"

$Tools_Path = Join-Path -Path $CurrentDir -ChildPath  "packages/tools"
$DotNet_Path = Join-Path -Path $CurrentDir -ChildPath "packages/dotnet/$($DotNet_Version)"
$NuGet_Path = Join-Path -Path $CurrentDir -ChildPath  "packages/nuget"
$OpenCover_Path = Join-Path -Path $CurrentDir -ChildPath "packages/.packages/OpenCover/$($OpenCover_Version)/tools"
$XUnitRunnerConsole_Path = Join-Path -Path $CurrentDir -ChildPath "packages/tools/xunit.runner.console.$($XUnitRunnerConsole_Version)/tools/netcoreapp2.0"

Set-Item -Path Env:PATH -Value ("$($DotNet_Path);$($NuGet_Path);$($Tools_Path);$($OpenCover_Path);" + $Env:PATH)

Echo $Env:PATH

if(![System.IO.File]::Exists($(Join-Path -Path $CurrentDir -ChildPath "packages/tools/dotnet-sonarscanner.exe"))) {
  dotnet tool install --tool-path packages/tools dotnet-sonarscanner
} else {
  dotnet tool update --tool-path packages/tools dotnet-sonarscanner
}
if(![System.IO.File]::Exists($(Join-Path -Path $CurrentDir -ChildPath "packages/tools/csmacnz.Coveralls.exe"))) {
  dotnet tool install --tool-path packages/tools coveralls.net
} else {
  dotnet tool update --tool-path packages/tools coveralls.net
}

$DOTNET = Join-Path -Path $DotNet_Path -ChildPath "dotnet.exe"
$OPENCOVER = Join-Path -Path $OpenCover_Path -ChildPath "OpenCover.Console.exe"

# This is MUST be included in project to work with SonarScanner
& dotnet add Build.Tests package --package-directory packages/.packages OpenCover --version $OpenCover_Version
& dotnet add Build.Tests package --package-directory packages/.packages coverlet.msbuild --version $CoverletMsbuild_Version

& dotnet-sonarscanner begin /o:"funcelot" /d:sonar.login="$env:SONAR_TOKEN" /k:"build-core" /d:sonar.host.url="https://sonarcloud.io" /n:"build" /v:"1.0" /d:sonar.cs.opencover.reportsPaths="Build.Tests/opencover.xml" /d:sonar.coverage.inclusions="**/*.cs" /d:sonar.coverage.exclusions="**/Interface*.cs,**/*Test*.cs,**/*Exception*.cs,**/*Attribute*.cs,**/Middleware/*.cs,/Pages/*.cs,**/Program.cs,**/Startup.cs,**/sample/*,**/aspnetcore/*,**/wwwroot/*,**/xunit/*,**/*.js,**/coverage.opencover.xml" /d:sonar.sourceEncoding="UTF-8" /d:sonar.language=cs
& dotnet restore --packages packages/.packages
& dotnet build -c Release -f net5.0
& dotnet test -c Release --no-build Build.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:Include="[Build]*" /p:Exclude="[xunit*]*,[Build]*AttributeProvider*" -v:n
& $OPENCOVER `
  -target:"$($DOTNET)" `
  -targetargs:"test -f net5.0 -c Release Build.Tests\Build.Tests.csproj" `
  -mergeoutput `
  -hideskipped:File `
  -output:"Build.Tests\opencover.xml" `
  -oldStyle `
  -filter:"+[Build*]* -[Build.Tests*]* -[Build*]*AttributeProvider*" `
  -searchdirs:"Build.Tests/bin/Release/net5.0" `
  -register:user

& dotnet-sonarscanner end /d:sonar.login="$env:SONAR_TOKEN"

# Try to upload to Coveralls
& csmacnz.Coveralls --opencover -i Build.Tests/coverage.opencover.xml

#& dotnet add Build.Tests package --package-directory packages/.packages OpenCover --version $OpenCover_Version
#
#$DOTNET = Join-Path -Path $DotNet_Path -ChildPath "dotnet.exe"
#$OPENCOVER = Join-Path -Path $OpenCover_Path -ChildPath "OpenCover.Console.exe"
#
#& $OPENCOVER `
#  -target:"$($DOTNET)" `
#  -targetargs:"test -f netcoreapp3.1 -c Release Build.Tests/Build.Tests.csproj" `
#  -mergeoutput `
#  -hideskipped:File `
#  -output:"Build.Tests/opencover.xml" `
#  -oldStyle `
#  -filter:"+[Build*]* -[Build.Tests*]* -[Build*]*AttributeProvider*" `
#  -searchdirs:"Build.Tests/bin/Release/netcoreapp3.1" `
#  -register:user
#
# Upload to coveralls FILERED sources
#& csmacnz.Coveralls --opencover -i "Build.Tests/opencover.xml"  --useRelativePaths
