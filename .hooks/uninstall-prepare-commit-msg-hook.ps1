#!/usr/bin/env pwsh
#!C:/Program\ Files/PowerShell/7/pwsh.exe
& Remove-Item $PSScriptRoot/../.git/hooks/prepare-commit-msg
if ($IsLinux) {
    rm -rf $PSScriptRoot/../.git/hooks/prepare-commit-msg
}
