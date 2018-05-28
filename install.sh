#!/bin/bash

set -e

nuget install -OutputDirectory packages -Version 4.6.519 OpenCover
nuget install -OutputDirectory packages -Version 0.7.0 coveralls.net

curl -o scanner.zip -L https://github.com/SonarSource/sonar-scanner-msbuild/releases/download/4.2.0.1214/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0.zip
unzip scanner.zip -d $(PWD)/packages/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0
chmod +x $(PWD)/packages/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0/sonar-scanner-3.1.0.1141/bin/sonar-scanner

sonarcube=/.sonarcube
rm -rf $sonarcube
mkdir $sonarcube

coverage=./coverage
rm -rf $coverage
mkdir $coverage

dotnet restore

dotnet packages/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0/SonarScanner.MSBuild.dll begin /d:sonar.login="$SONARCLOUDTOKEN" /k:"build-core" /d:sonar.host.url="https://sonarcloud.io" /n:"build" /v:"1.0" /d:sonar.cs.opencover.reportsPaths="Build.Tests/coverage.xml" /d:sonar.coverage.exclusions="**/*Test*.cs" /d:sonar.organization="hack2root-github" /d:sonar.verbose=true 
dotnet test --no-build Build.Tests /p:CollectCoverage=true /p:CoverletOutputFormat=opencover --configuration Release 
dotnet packages/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0/SonarScanner.MSBuild.dll end /d:sonar.login="$SONARCLOUDTOKEN" 
