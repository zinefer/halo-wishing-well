#!/usr/bin/env pwsh

# Include common utils
. ("$PSScriptRoot\common.ps1")

Push-Location devops/terraform/ephemeral
terraform init
echo "RESOURCE_GROUP=$(terraform output resource_group)" >> $Env:GITHUB_ENV
echo "ACR_NAME=$(terraform output acr_name)" >> $Env:GITHUB_ENV
echo "FRONTEND_STORAGE=$(terraform output frontend_storage_account)" >> $Env:GITHUB_ENV
echo "QUEUE_STORAGE=$(terraform output queue_storage_account)" >> $Env:GITHUB_ENV
echo "QUEUE_NAME=$(terraform output queue_name)" >> $Env:GITHUB_ENV
echo "TABLE_STORAGE=$(terraform output table_storage_account)" >> $Env:GITHUB_ENV
echo "TABLE_NAME=$(terraform output coins_table_name)" >> $Env:GITHUB_ENV
Pop-Location