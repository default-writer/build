#!/bin/bash

set -e

# Install OpenCover and ReportGenerator, and save the path to their executables.
nuget install -Verbosity quiet -OutputDirectory packages -Version 4.6.519 OpenCover

OPENCOVER=$PWD/packages/OpenCover.4.6.519/tools/OpenCover.Console.exe

CONFIG=Release
# Arguments to use for the build
DOTNET_BUILD_ARGS="-c $CONFIG"
# Arguments to use for the test
DOTNET_TEST_ARGS="$DOTNET_BUILD_ARGS"

echo CLI args: $DOTNET_BUILD_ARGS

echo Restoring

dotnet restore

echo Building

dotnet build $DOTNET_BUILD_ARGS

echo Testing

coverage=./coverage
rm -rf $coverage
mkdir $coverage

dotnet test -f netcoreapp2.1 $DOTNET_TEST_ARGS Build.Tests/Build.Tests.csproj

echo "Calculating coverage with OpenCover"
$OPENCOVER \
  -target:"c:\Program Files\dotnet\dotnet.exe" \
  -targetargs:"test -f netcoreapp2.1 $DOTNET_TEST_ARGS Build.Tests/Build.Tests.csproj" \
  -mergeoutput \
  -hideskipped:File \
  -output:$coverage/coverage.xml \
  -oldStyle \
  -filter:"+[Build*]* -[Build.*Tests*]*" \
  -searchdirs:$testdir/bin/$CONFIG/netcoreapp2.1 \
  -register:user

