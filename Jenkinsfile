pipeline {
  agent any
  stages {
    stage('restore') {
      steps {
        sh '''container="dot-net-sdk-5.0.201"
image="dot-net-sdk-5.0.201:latest"
docker --version
docker build -f Dockerfile -t $image . 
docker run --rm -it --name $container $image'''
      }
    }

  }
}