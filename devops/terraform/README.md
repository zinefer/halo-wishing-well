# Terraform

The IaC is split into two pieces, `durable` and `ephemeral`. Currently, `durable` just bootstraps our environment with a storage account we can use as a terraform state backend.

`ephemeral` sets up all the resources needed for the project and is only referred to as ephemeral because it is generally destroyed after an integration pipeline run.
