#!/usr/bin/env pwsh

# Include common utils
. ("$PSScriptRoot\common.ps1")

& ("$PSScriptRoot\load-docker.ps1")
& ("$PSScriptRoot\push-docker.ps1")

# Unfortunately, we need to rebuild the
# * Frontend
Push-Location frontend\wishing-well
npm install --loglevel=error
# With variables from the provision
Invoke-Command-Stop { npm run-script build }
Invoke-Command-Stop {
    # And upload it
    az storage blob upload-batch --account-name $Env:FRONTEND_STORAGE -s build -d '$web'
}
Pop-Location

