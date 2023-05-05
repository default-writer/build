Get-Host | Select-Object Version
$container = "dot-net-sdk-6.0.408"
$image = "dot-net-sdk-6.0.408:latest"
& docker --version
& docker build -f .appveyour/Dockerfile -t $image .
& docker run --rm --name $container $image
