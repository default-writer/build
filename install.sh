#!/bin/bash

set -e

if [ ! -d "$(PWD)/packages/coveralls.net.0.7.0" ]
nuget install -OutputDirectory packages -Version 0.7.0 coveralls.net
fi

if [ ! -d "$(PWD)/packages/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0" ]
curl -o scanner.zip -L https://github.com/SonarSource/sonar-scanner-msbuild/releases/download/4.2.0.1214/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0.zip
unzip scanner.zip -d $(PWD)/packages/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0
chmod +x $(PWD)/packages/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0/sonar-scanner-3.1.0.1141/bin/sonar-scanner
fi

sonarcube=/.sonarcube
rm -rf $sonarcube
mkdir $sonarcube

coverage=./coverage
rm -rf $coverage
mkdir $coverage

dotnet restore
