#!/bin/bash -e
cd "${0%/*}"
dotnet add ./Build.Behave.csproj package NUnit
dotnet add ./Build.Behave.csproj package SpecFlow.NUnit
#for old versions, use
#dotnet add ./Build.Behave.csproj package NUnit -v 3.12.0 -s https://api.nuget.org/v3/index.js
#dotnet add ./Build.Behave.csproj package SpecFlow.NUnit -v 3.1.97 -s https://api.nuget.org/v3/index.json
