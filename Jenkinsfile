pipeline {
  agent any
  stages {
    stage('restore') {
      steps {
        pwsh(script: './docker-init.ps1', returnStdout: true)
      }
    }

  }
}