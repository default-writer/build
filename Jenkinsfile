pipeline {
  agent any
  stages {
    stage('build/run') {
      steps {
        sh '''container="dot-net-sdk-6.0.408"
image="dot-net-sdk-6.0.408:latest"
docker --version
docker build -f Dockerfile -t $image .
docker run --rm --name $container $image'''
      }
    }

  }
}