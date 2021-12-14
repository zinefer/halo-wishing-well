# Workflow Pipelines

Github actions does not have good mechanisms for workflow re-use. I've decided to use powershell scripts to DRY things up as well as explose functionality for local development.

Github actions is unfortunately [limited](https://github.com/actions/starter-workflows/issues/245) in mechanisms to reuse code so `TF_CLI_ARGS_init` is not as dry as I'd like.

## Non-Main

- Continuous integration
- Enforces style
    - Yaml
    - Markdown
- Builds projects
- Runs unit tests

## Main

- Non-Main pipeline
- Continuous deploy
- End-To-End test
- Clean up resources

## Secrets

This repo requires some azure secrets to be able to bootstrap and due to some inconsistency in the azure actions, we need to supply it in two different formats.

- `AZURE_CREDENTIALS` - [azure/login's guide to define](https://github.com/marketplace/actions/azure-login#configure-a-service-principal-with-a-secret)

Also, define the following (from the json output of the commands above if you followed the guide):

- `ARM_CLIENT_ID`
- `ARM_CLIENT_SECRET`
- `ARM_SUBSCRIPTION_ID`
- `ARM_TENANT_ID`
