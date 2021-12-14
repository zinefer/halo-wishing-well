#!/usr/bin/env pwsh

# Include common utils
. ("$PSScriptRoot\common.ps1")

Get-AzResourceGroup -Name $Env:TF_VAR_durable_resource_group -ErrorVariable notExists -ErrorAction SilentlyContinue
if ( $notExists ) {
    Push-Location devops/terraform/durable
    Invoke-Command-Stop { terraform init }
    Invoke-Command-Stop { terraform apply }
    Pop-Location
}
