pipeline {
  agent any
  stages {
    stage('build/run') {
      steps {
        sh '''container="dot-net-sdk-5.0.201"
image="dot-net-sdk-5.0.201:latest"
docker --version
docker build -f Dockerfile -t $image . 
docker run --rm --name $container $image'''
      }
    }

  }
}