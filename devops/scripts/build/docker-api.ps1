# Include common utils
. ("$PSScriptRoot\..\common.ps1")

Push-Location backend
Invoke-Command-Stop {
    docker build -t "wishingwell.api:dev" `
        -f ../devops/docker/WishingWell.Api.Dockerfile .
}
Pop-Location
