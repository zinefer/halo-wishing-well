#!/usr/bin/env pwsh

# Include common utils
. ("$PSScriptRoot\common.ps1")

Push-Location devops/terraform/ephemeral
Invoke-Command-Stop { terraform init }
Invoke-Command-Stop { terraform destroy -auto-approve }
Pop-Location
