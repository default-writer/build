#!/usr/bin/env bash
set -e
dotnet nuget add source --username ${GITHUB_USERNAME} --password ${GITHUB_TOKEN} --store-password-in-clear-text --name github "https://nuget.pkg.github.com/${GITHUB_USERNAME}/index.json"
