#!/usr/bin/env bash
find . -name "*.*" -exec "sed $'s/\r$//'"
set -e
rm -rf ./Build.Tests/bin
rm -rf ./Build.Abstractions/bin
rm -rf ./Build.Behave/bin
rm -rf ./Build/bin
