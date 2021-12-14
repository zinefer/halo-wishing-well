# Halo Wishing Well

This is an (incomplete) sample project for 343 industries.

- [Getting Started](#getting-started)
- [Architecture](#architecture)
- [Devops Pipeline](.github/workflows/README.md)
    - Powershell scripts
        - Expose release-pipeline functionality for developer convenience
        - Keep release-pipeline DRY
    - [Terraform IaC](devops/terraform/README.md)
        - Split into `durable`/`ephemeral`
            - Allows for saving Terraform state
            - Great for a future release pipeline (deploying is a feature!)
            - Automated bootstrapping
- [C# API Backend](backend/README.md)
    - AAD JWT Auth
    - Services/Repository pattern
    - DI for testability
    - Swagger for machine-readability (although this seems to be the default in asp.net now)
    - Deployed to App Service with [Docker](devops/docker/WishingWell.Api.Dockerfile)
- [Going Further](#going-further)

## Getting Started

### Lint

Dependencies: `npm`, `yamllint`, `terraform`

```sh
./devops/scripts/lint.ps1
```

## Build

Dependencies: `npm`, `docker`, `terraform`

```sh
./devops/scripts/build.ps1
```

## Architecture

This project is intended to be a sample ingestion and processing pipeline. Queue items are ingested via an API with SPA frontend. Queue items are watched via KEDA and processed in a pod inside kubernetes. "Results" get saved to a storage table. The frontend and backend both use AAD JWT token auth.

![architecture](docs/architecture.png)

## Going further

Things I would improve if this were a real project.

- Better dependency documentation
- Better aggregate the number of coins (or queue messages processed)
- Integrate ;)

### Devops

- Better expose and document deploy scripts
- End to End test
- Create github-actions to leverege the release-pipeline and stand up an instance per PR for ease of review

### Backend

- More Error handling :(
- Wrap `QueueClient` and `TableClient` in an interface to better facilitate testing
