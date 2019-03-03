Get-Host | Select-Object Version
$container = "dot-net-sdk-3.0.100-preview2"
$image = "dot-net-sdk-3.0.100-preview2:latest"
& dotnet --list-sdks
& docker --version
& docker build -f .appveyour/Dockerfile -t $image . 
& docker run --rm -it --name $container $image