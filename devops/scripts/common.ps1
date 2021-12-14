#!/usr/bin/env pwsh

# Execute a command and check it's return value
# If not successful, throw
function Invoke-Command-Stop {
    param (
        [scriptblock]$Cmd
    )
    & $Cmd
    if ($lastexitcode -gt 0) {
        throw "Command Failed: $($Cmd)"
    }
}
