terraform {
  required_version = "~> 1.1.0"
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=2.88.1"
    }
  }
}

provider "azurerm" {
  features {}
}

locals {
  sanitized_base_name = lower(replace(var.base_name, "/[^A-Za-z0-9]/", ""))
}

terraform {
  backend "azurerm" {
    container_name = "terraform"
  }
}

resource "azurerm_resource_group" "main" {
  name     = var.base_name
  location = var.location
}

resource "azurerm_application_insights" "main" {
  name                = "ai-${var.base_name}"
  resource_group_name = azurerm_resource_group.main.name
  location            = azurerm_resource_group.main.location
  application_type    = "web"
}

resource "azurerm_container_registry" "main" {
  name                = "acr${local.sanitized_base_name}"
  resource_group_name = azurerm_resource_group.main.name
  location            = azurerm_resource_group.main.location
  sku                 = "Basic"
}
