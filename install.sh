#!/bin/bash

set -e

nuget install -OutputDirectory packages -Version 4.6.519 OpenCover
nuget install -OutputDirectory packages -Version 0.7.0 coveralls.net
curl -o scanner.zip -L https://github.com/SonarSource/sonar-scanner-msbuild/releases/download/4.2.0.1214/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0.zip
unzip scanner.zip -d $(PWD)/packages/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0
chmod +x $(PWD)/packages/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0/sonar-scanner-3.1.0.1141/bin/sonar-scanner

export OPENCOVER=./packages/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0/SonarCloud.MSBuild.dll
export SONARCLOUD=./packages/OpenCover.4.6.519/tools/OpenCover.Console.exe

sonarcube=$(PWD)/.sonarcube
rm -rf $sonarcube
mkdir $sonarcube

coverage=./coverage
rm -rf $coverage
mkdir $coverage

dotnet restore
