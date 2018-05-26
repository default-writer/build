#!/bin/bash

set -e

# Install OpenCover and ReportGenerator, and save the path to their executables.
nuget install -Verbosity quiet -OutputDirectory packages -Version 4.6.519 OpenCover
nuget install -Verbosity quiet -OutputDirectory packages -Version 4.2.0 MSBuild.SonarQube.Runner.Tool
curl -o $PWD\.sonarcube\scanner.zip -L https://github.com/SonarSource/sonar-scanner-msbuild/releases/download/4.2.0.1214/sonar-scanner-msbuild-4.2.0.1214-netcoreapp2.0.zip
unzip $PWD\.sonarcube\scanner.zip
chmod +x $PWD\.sonarcube\sonar-scanner-3.1.0.1141/bin/sonar-scanner

OPENCOVER=$PWD/packages/OpenCover.4.6.519/MSBuild/OpenCover.Console.exe
SONARCLOUD=SonarScanner.MSBuild.dll

author=hack2root-github
key=build-core

CONFIG=Release
# Arguments to use for the build
DOTNET_BUILD_ARGS="-c $CONFIG"
# Arguments to use for the test
DOTNET_TEST_ARGS="$DOTNET_BUILD_ARGS"

echo CLI args: $DOTNET_BUILD_ARGS

echo Restoring

dotnet restore

sonarcube=./sonarcube
rm -rf $sonarcube
mkdir $sonarcube

if [ -n "$SONARCLOUDTOKEN" ]
then
dotnet $SONARCLOUD begin \
    /key:$key \
    /d:sonar.cs.opencover.reportsPaths="$(find . -name coverage.xml | tr '\n' ',')" \
    /d:sonar.coverage.exclusions="Build.Tests/**" \
    /d:sonar.cs.vstest.reportsPaths="$(pwd)/.output/*.trx" \
    /d:sonar.verbose=true \
    /d:sonar.organization=$author \
    /d:sonar.host.url="https://sonarcloud.io" \
    /d:sonar.login=$SONARCLOUDTOKEN
fi

echo Building

dotnet build $DOTNET_BUILD_ARGS

echo Testing

coverage=./coverage
rm -rf $coverage
mkdir $coverage

# dotnet test -f netcoreapp2.1 $DOTNET_TEST_ARGS Build.Tests/Build.Tests.csproj

echo "Calculating coverage with OpenCover"
$OPENCOVER \
  -target:"c:\Program Files\dotnet\dotnet.exe" \
  -targetargs:"test -f netcoreapp2.1 $DOTNET_TEST_ARGS Build.Tests/Build.Tests.csproj" \
  -mergeoutput \
  -hideskipped:File \
  -output:$coverage/coverage.xml \
  -oldStyle \
  -filter:"+[Build*]* -[Build.Tests*]*" \
  -searchdirs:$testdir/bin/$CONFIG/netcoreapp2.1 \
  -register:user

if [ -n "$SONARCLOUDTOKEN" ]
then
dotnet $SONARCLOUD end \
    /d:sonar.login=$SONARCLOUDTOKEN
fi
