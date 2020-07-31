Get-Host | Select-Object Version
$container = "dot-net-sdk-3.1.302"
$image = "dot-net-sdk-3.1.302:latest"
& dotnet --list-sdks
& docker --version
& docker build -f .appveyour/Dockerfile -t $image . 
& docker run --rm -it --name $container $image