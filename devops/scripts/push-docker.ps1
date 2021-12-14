#!/usr/bin/env pwsh

# Include common utils
. ("$PSScriptRoot\common.ps1")

az acr login --name $Env:ACR_NAME

$API_REGISTRY_TAG = $Env:ACR_NAME + 
                    ".azurecr.io/" + $Env:API_IMAGE

echo $Env:API_IMAGE $API_REGISTRY_TAG
docker tag $Env:API_IMAGE $API_REGISTRY_TAG
Invoke-Command-Stop { docker push $API_REGISTRY_TAG }
