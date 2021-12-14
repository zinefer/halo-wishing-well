# Workflow Pipelines

Github actions does not have good mechanisms for workflow re-use. I've decided to use powershell scripts to DRY things up as well as explose functionality for local development.

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
