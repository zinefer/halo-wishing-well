# Include common utils
. ("$PSScriptRoot\..\common.ps1")

Push-Location frontend\wishing-well
npm install --loglevel=error
Invoke-Command-Stop { npm run-script build }
Pop-Location

