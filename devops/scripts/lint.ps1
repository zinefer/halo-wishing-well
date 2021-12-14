#!/usr/bin/env pwsh

# Include common utils
. ("$PSScriptRoot\common.ps1")

Invoke-Directory-Parallel "devops/scripts/lint" "Linting"
