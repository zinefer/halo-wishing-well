resource "azurerm_storage_account" "queue" {
  name                      = "queue${local.sanitized_base_name}"
  resource_group_name       = azurerm_resource_group.main.name
  location                  = azurerm_resource_group.main.location
  account_kind              = "StorageV2"
  account_tier              = "Standard"
  account_replication_type  = "LRS"
  enable_https_traffic_only = true
}

resource "azurerm_storage_queue" "queue" {
  name                 = var.base_name
  storage_account_name = azurerm_storage_account.queue.name
}
