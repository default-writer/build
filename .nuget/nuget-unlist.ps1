$apikey = Get-ChildItem Env:NUGET_ACCESSTOKEN
$source = "https://www.nuget.org/packages"
$package = "Build.DependencyInjection.Abstractions"
$version = "1.0.0.20"
& ../packages/nuget/nuget delete $package $version -Source $source -apikey $apikey -NonInteractive