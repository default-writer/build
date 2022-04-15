#!/usr/bin/env pwsh
#!C:/Program\ Files/PowerShell/7/pwsh.exe
# https://medium.com/better-programming/how-to-automatically-add-the-ticket-number-in-git-commit-message-bda5426ded05
# https://sqlnotesfromtheunderground.wordpress.com/2018/01/01/git-pre-commit-hook-with-powershell/
# https://gist.github.com/bartoszmajsak/1396344
# https://githooks.com/
[CmdletBinding()]
Param(
  [Parameter(Mandatory = $True, Position = 0)]
  [string]
  $CommitMessageFile
)

$message = Get-Content -Path $CommitMessageFile
$branch = & git rev-parse --abbrev-ref HEAD | Out-String -NoNewline

$ignore_ticket = $message.StartsWith("*")
# special tweak for skipping branch name
if (-Not $ignore_ticket) {
  $message = $message.Trim()
  "[$($branch)] $($message)" | Out-File $CommitMessageFile
}
else
{
    $message = $message.Substring(1).Trim()
  "$($message)" | Out-File $CommitMessageFile
}