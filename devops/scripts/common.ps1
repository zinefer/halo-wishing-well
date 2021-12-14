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

# Execute every script in a directory and throw if not successful
function Invoke-Directory-Parallel {
    param (
        [string]$Path,
        [string]$Operation
    )
    $success = $true
    Get-ChildItem "$($Path)\*.ps1" | ForEach-Object -parallel { 
        echo "!* $($using:Operation) $($_.Basename )"
        try {
            & $_.FullName
        }
        catch {
            $success = $false
            throw "!* $($using:Operation) Failed: $($_.Basename)"
        }
        finally {
            if ($lastexitcode -gt 0) {
                $success = $false
                throw "!* $($using:Operation) Failed: $($_.Basename)"
            }
        }
        echo "!* $($using:Operation) Success: $($_.Basename)"
    }
    Get-Job | Wait-Job
}
