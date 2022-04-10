#!/usr/bin/env bash
set -e
dotnet restore
dotnet test
dotnet build
