resource "azurerm_app_service_plan" "api" {
  name                = "appserviceplan"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name

  kind     = "Linux"
  reserved = true

  sku {
    tier = "Premium"
    size = "P2V2" // NOTE: Free tier does not allow VNET integration
  }
}

resource "azurerm_app_service" "api" {
  name                = "api-${var.base_name}"
  location            = var.location
  resource_group_name = azurerm_resource_group.main.name
  app_service_plan_id = azurerm_app_service_plan.api.id

  site_config {
    linux_fx_version                     = "DOCKER|${azurerm_container_registry.main.name}.azurecr.io/${var.api_image}"
    acr_use_managed_identity_credentials = true
    always_on                            = true
  }

  identity {
    type = "SystemAssigned"
  }

  https_only = true

  lifecycle {
    ignore_changes = [
      # Ignore changes to site_config.linux_fx_version so we don't overwrite
      # the deployed container.
      site_config["linux_fx_version"],
      app_settings,
    ]
  }

  // Environment variables and app config
  app_settings = {
    ApplicationInsights__InstrumentationKey = azurerm_application_insights.main.instrumentation_key
    ASPNETCORE_ENVIRONMENT                  = "Production"
    DOCKER_REGISTRY_SERVER_URL              = "https://${azurerm_container_registry.main.name}.azurecr.io"
  }
}

resource "azurerm_role_assignment" "api_acr" {
  role_definition_name = "AcrPull"
  scope                = azurerm_container_registry.main.id
  principal_id         = azurerm_app_service.api.identity[0].principal_id
}

resource "azurerm_role_assignment" "api_storage_table" {
  role_definition_name = "Storage Table Data Contributor"
  scope                = azurerm_storage_account.data.id
  principal_id         = azurerm_app_service.api.identity[0].principal_id
}

resource "azurerm_role_assignment" "api_storage_queue" {
  role_definition_name = "Reader and Data Access"
  scope                = azurerm_storage_account.queue.id
  principal_id         = azurerm_app_service.api.identity[0].principal_id
}
