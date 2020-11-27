Get-Host | Select-Object Version
$container = "dot-net-sdk-5.0.100"
$image = "dot-net-sdk-5.0.100:latest"
& dotnet --list-sdks
& docker --version
& docker build -f .appveyour/Dockerfile -t $image . 
& docker run --rm -it --name $container $image