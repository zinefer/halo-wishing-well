#!/usr/bin/env pwsh

# Include common utils
. ("$PSScriptRoot\common.ps1")

# Load image from saved artifact
$api_file = $Env:API_IMAGE.Replace(':', '-')
docker load --input "docker-artifacts/$api_file.tar"
