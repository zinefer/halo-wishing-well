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

resource "azurerm_resource_group" "main" {
  name     = var.durable_resource_group
  location = var.location
}

resource "azurerm_storage_account" "main" {
  name                     = var.durable_storage_account
  resource_group_name      = azurerm_resource_group.main.name
  location                 = azurerm_resource_group.main.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_storage_container" "main" {
  name                  = "terraform"
  storage_account_name  = azurerm_storage_account.main.name
  container_access_type = "private"
}
