#!/bin/bash

set -e

if [ -n "$COVERALLS_REPO_TOKEN" ]
then
  packages/coveralls.net.0.7.0/tools/csmacnz.Coveralls.exe --opencover -i $(PWD)/Build.Tests/coverage.xml
fi
