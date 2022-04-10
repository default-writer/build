& cd .nuget
& ./NuGet-dotnet-install
& cd ..

$OpenCover_Version = "4.7.922"
$CoverletMsbuild_Version = "3.0.3"
$CurrentDir = Convert-Path .
$DotNet_Version = [System.IO.File]::ReadAllText($(Join-Path -Path $CurrentDir -ChildPath ".config/DotNetCliVersion.txt"))

$DotNet_Path = Join-Path -Path $CurrentDir -ChildPath "packages/dotnet/$($DotNet_Version)"
$NuGet_Path = Join-Path -Path $CurrentDir -ChildPath  "packages/nuget"
$Tools_Path = Join-Path -Path $CurrentDir -ChildPath  "packages/tools"

$OpenCover_Path = Join-Path -Path $CurrentDir -ChildPath "packages/.packages/OpenCover/$($OpenCover_Version)/tools"
$DOTNET = Join-Path -Path $DotNet_Path -ChildPath "dotnet.exe"
$OPENCOVER = Join-Path -Path $OpenCover_Path -ChildPath "OpenCover.Console.exe"

Set-Item -Path Env:PATH -Value ("$($DotNet_Path);$($NuGet_Path);$($Tools_Path);" + $Env:PATH)

if(![System.IO.File]::Exists($(Join-Path -Path $CurrentDir -ChildPath "packages/tools/dotnet-sonarscanner.exe"))) {
  dotnet tool install --tool-path packages/tools dotnet-sonarscanner
} else {
  dotnet tool update --tool-path packages/tools dotnet-sonarscanner
}

& dotnet add Build.Tests package --package-directory packages/.packages OpenCover --version $OpenCover_Version
& dotnet add Build.Tests package --package-directory packages/.packages coverlet.msbuild --version $CoverletMsbuild_Version

& dotnet-sonarscanner begin /o:"funcelot" /d:sonar.login="$env:SONAR_TOKEN" /k:"build-core" /d:sonar.host.url="https://sonarcloud.io" /n:"build" /v:"1.0" /d:sonar.cs.opencover.reportsPaths="Build.Tests/coverage.opencover.xml" /d:sonar.coverage.inclusions="**/*.cs" /d:sonar.coverage.exclusions="**/Interface*.cs,**/*Test*.cs,**/*Exception*.cs,**/*Attribute*.cs,**/Middleware/*.cs,/Pages/*.cs,**/Program.cs,**/Startup.cs,**/sample/*,**/aspnetcore/*,**/wwwroot/*,**/xunit/*,**/*.js,**/coverage.opencover.xml,**/opencover.xml" /d:sonar.sourceEncoding="UTF-8" /d:sonar.language=cs
& dotnet restore --packages packages/.packages
& dotnet build -c Release -f net6.0
& dotnet test -c Release --no-restore --verbosity normal --no-build Build.Tests -f net6.0 /p:CollectCoverage=true /p:CoverletOutput="coverage.opencover.xml" /p:CoverletOutputFormat=opencover /p:Include="[Build.*]*" /p:Exclude="[Build]*AttributeProvider*" -v:n /p:CoverletOutputFormat=opencover /p:Threshold=80 /p:ThresholdType=line /p:ThresholdStat=total
& $OPENCOVER `
  -target:"$($DOTNET)" `
  -targetargs:"test -f net6.0 -c Release Build.Tests\Build.Tests.csproj" `
  -mergeoutput `
  -hideskipped:File `
  -output:"Build.Tests\coverage.opencover.xml" `
  -oldStyle `
  -filter:"+[Build*]* -[Build*]*AttributeProvider*" `
  -searchdirs:"Build.Tests/bin/Release/net6.0" `
  -register:user

& dotnet-sonarscanner end /d:sonar.login="$env:SONAR_TOKEN"
