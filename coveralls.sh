#!/bin/bash

if [ -n "$COVERALLSREPOTOKEN" ]
then
  packages/coveralls.net.0.7.0/tools/csmacnz.Coveralls.exe --opencover -i coverage/coverage.xml --useRelativePaths
fi
