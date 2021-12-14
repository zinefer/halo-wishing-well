#!/usr/bin/env pwsh

# npm writes its warnings to stderr which confuses pwsh
npx --loglevel=error markdownlint-cli --dot .