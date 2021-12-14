#!/usr/bin/env pwsh

# Include common utils
. ("$PSScriptRoot\common.ps1")

# Save image to file and upload as artifact
mkdir docker-artifacts
$api_file = $Env:API_IMAGE.Replace(':', '-')
docker save $Env:API_IMAGE -o "./docker-artifacts/$api_file.tar"
