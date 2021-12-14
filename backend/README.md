# WishingWell API Backend

They say that for every message you cast into Cortana's well gives someone with a
slayer challenge a little extra luck.

## Building

This project can be built with dotnet or using the [Dockerfile used for deploy](devops/docker/WishingWell.Api.Dockerfile)

## Bypass Auth

This project includes an auth bypass that can be used for testing.

```sh
export DONOTUSE_ONLY_FOR_CI_DISABLE_AUTHENTICATION=true
dotnet run
```

## Setting up storage

This project requires access to a `Storage Queue` and a `Storage Table`. These can both be set up to point at the same storage account if desired.

```text
Storage__TableAccountName
Storage__CoinsTableName
Storage__QueueAccountName
Storage__QueueName
```
