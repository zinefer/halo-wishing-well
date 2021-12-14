output "base_name" {
  value = var.base_name
}

output "resource_group" {
  value = azurerm_resource_group.main.name
}

output "acr_name" {
  value = azurerm_container_registry.main.name
}

output "queue_storage_account" {
  value = azurerm_storage_account.queue.name
}

output "queue_name" {
  value = azurerm_storage_queue.queue.name
}

output "table_storage_account" {
  value = azurerm_storage_account.data.name
}

output "coins_table_name" {
  value = azurerm_storage_table.data.name
}

