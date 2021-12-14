resource "azurerm_storage_account" "data" {
  name                      = "data${local.sanitized_base_name}"
  resource_group_name       = azurerm_resource_group.main.name
  location                  = azurerm_resource_group.main.location
  account_kind              = "StorageV2"
  account_tier              = "Standard"
  account_replication_type  = "LRS"
  enable_https_traffic_only = true
}

resource "azurerm_storage_container" "data" {
  name                  = "content"
  storage_account_name  = azurerm_storage_account.data.name
  container_access_type = "private"
}

resource "azurerm_storage_table" "data" {
  name                 = "Coins"
  storage_account_name = azurerm_storage_account.data.name
}
