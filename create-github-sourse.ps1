& dotnet nuget add source --username ${env:GITHUB_USERNAME} --password ${env:GITHUB_TOKEN} --store-password-in-clear-text --name github "https://nuget.pkg.github.com/funcelot/index.json"