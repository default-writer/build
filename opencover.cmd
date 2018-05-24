set nuget=%userprofile%\.nuget\packages
set OPENCOVER=%nuget%\opencover\4.6.519\tools
%OPENCOVER%\OpenCover.Console.exe -oldstyle -register:user -target:"C:\Program Files\dotnet\dotnet.exe" -targetargs:"test --no-build Build.Tests" -filter:"+[*]* -[*.Tests*]*" -output:opencover.xml


