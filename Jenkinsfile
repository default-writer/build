pipeline {
  agent any
  stages {
    stage('restore') {
      steps {
        sh '''container = "dot-net-sdk-5.0.100"
image = "dot-net-sdk-5.0.100:latest"
docker --version
docker build -f Dockerfile -t $image . 
docker run --rm -it --name $container $image'''
      }
    }

  }
}