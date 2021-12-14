#!/usr/bin/env pwsh

# Include common utils
. ("$PSScriptRoot\..\common.ps1")

Push-Location backend/WishingWell.Api
Invoke-Command-Stop { 
    dotnet publish -c Release
}
Pop-Location