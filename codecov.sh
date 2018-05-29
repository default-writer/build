#!/bin/bash

set -e

#curl -s https://codecov.io/bash > codecov
#chmod +x codecov
#./codecov -f "Build.Tests/coverage.xml" -t $CODECOVTOKEN

./packages/Codecov.1.0.3/tools/codecov.exe "Build.Tests/coverage.xml" -t $CODECOVTOKEN
