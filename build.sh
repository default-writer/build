#!/usr/bin/env bash
set -e
dotnet restore
dotnet build
dotnet test --no-restore --verbosity normal --no-build Build.Tests --framework net6.0
