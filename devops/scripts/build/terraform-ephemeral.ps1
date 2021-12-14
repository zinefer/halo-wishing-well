#!/usr/bin/env pwsh

# Include common utils
. ("$PSScriptRoot\..\common.ps1")

Push-Location devops/terraform/ephemeral
Invoke-Command-Stop { terraform init -backend=false }
Invoke-Command-Stop { terraform validate }
Pop-Location