#!/bin/env/bash -e
dotnet nuget add source --username ${GITHUB_USERNAME} --password ${GITHUB_TOKEN} --store-password-in-clear-text --name github "https://nuget.pkg.github.com/${GINTHUB_USERNAME}/index.json"
