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

# * Kubernetes 
az aks get-credentials --name $Env:AKS_NAME --resource-group $Env:RESOURCE_GROUP

helm repo add aad-pod-identity https://raw.githubusercontent.com/Azure/aad-pod-identity/master/charts
helm repo add kedacore https://kedacore.github.io/charts
helm dependency build ./devops/helm/wishing-well
helm upgrade --install "wishingwell-dev" ./devops/helm/wishing-well

sleep 30s

kubectl get crds
kubectl get all
kubectl get all -n keda
kubectl get pods --all-namespaces -o jsonpath="{.items[*].spec.containers[*].image}" | tr -s '[[:space:]]' '\n' | sort | uniq -c
