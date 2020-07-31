#!/usr/bin/env bash
set -e
# Step 1: Authenticate (if this is the first time)
dotnet nuget add source https://nuget.pkg.github.com/hack2root/index.json -n github
# Step 2: Pack
dotnet pack --configuration Release
# Step 3: Publish
dotnet nuget push "bin/Release/build.1.0.0.28.nupkg" --source "github"