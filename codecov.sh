#!/bin/bash

set -e

#curl -s https://codecov.io/bash > codecov
#chmod +x codecov
#./codecov -f "Build.Tests/coverage.xml" -t $CODECOVTOKEN

#./packages/Codecov.1.0.3/tools/codecov.exe "Build.Tests/coverage.xml" -t $CODECOVTOKEN

OpenCover.Console.exe -register:user -target:"%xunit20%\xunit.console.x86.exe" -targetargs:".\Build.Tests\bin\Release\Build.Tests.dll -noshadow" -filter:"+[Build*]* -[*Test*,*Exception*]*" -output:".\Build.Tests\coverage.xml"
codecov -f ".\Build.Tests\coverage.xml" $CODECOVTOKEN
