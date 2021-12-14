resource "azurerm_kubernetes_cluster" "processing" {
  name                = "aks-${var.base_name}"
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  dns_prefix          = "aks-${var.base_name}"

  linux_profile {
    admin_username = "podadmin"

    ssh_key {
      key_data = file("${path.module}/kubernetes-key.pub")
    }
  }

  default_node_pool {
    name       = "default"
    type       = "VirtualMachineScaleSets"
    vm_size    = "Standard_D2_v2"
    node_count = 1
  }

  role_based_access_control {
    enabled = true
  }

  identity {
    type = "SystemAssigned"
  }
}

data "azurerm_resource_group" "node" {
  name = azurerm_kubernetes_cluster.processing.node_resource_group
}

locals {
  aks_identity_system  = azurerm_kubernetes_cluster.processing.identity[0]
  aks_identity_kubelet = azurerm_kubernetes_cluster.processing.kubelet_identity[0]
}

resource "azurerm_role_assignment" "mio_noderg" {
  scope                = data.azurerm_resource_group.node.id
  role_definition_name = "Managed Identity Operator"
  principal_id         = local.aks_identity_kubelet.object_id
}

resource "azurerm_role_assignment" "mio_mainrg" {
  scope                = azurerm_resource_group.main.id
  role_definition_name = "Managed Identity Operator"
  principal_id         = local.aks_identity_kubelet.object_id
}

resource "azurerm_role_assignment" "vmc" {
  role_definition_name = "Virtual Machine Contributor"
  scope                = data.azurerm_resource_group.node.id
  principal_id         = local.aks_identity_kubelet.object_id
}

resource "azurerm_role_assignment" "acrpull" {
  role_definition_name = "acrpull"
  scope                = azurerm_container_registry.main.id
  principal_id         = local.aks_identity_kubelet.object_id
}

resource "azurerm_role_assignment" "k8_storage_table" {
  role_definition_name = "Storage Table Data Contributor"
  scope                = azurerm_storage_account.data.id
  principal_id         = local.aks_identity_kubelet.object_id
}

resource "azurerm_role_assignment" "k8_storage_queue" {
  role_definition_name = "Reader and Data Access"
  scope                = azurerm_storage_account.queue.id
  principal_id         = local.aks_identity_kubelet.object_id
}
