$container = "dot-net-sdk-3.0.100-preview2-nanoserver-1709"
$image = "dot-net-sdk-3.0.100-preview2-nanoserver-1709:latest"
& docker --version
& docker build -f .appveyour/Dockerfile -t $image . 
& docker run --rm -it --name $container $image
