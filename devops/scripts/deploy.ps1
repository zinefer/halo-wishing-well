#!/usr/bin/env pwsh

# Include common utils
. ("$PSScriptRoot\common.ps1")

& ("$PSScriptRoot\load-docker.ps1")
& ("$PSScriptRoot\push-docker.ps1")
