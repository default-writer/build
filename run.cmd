@echo off
powershell -file "./coverage.ps1"
pskill dotnet.exe
powershell -command "rm -recurse -force .sonarqube"