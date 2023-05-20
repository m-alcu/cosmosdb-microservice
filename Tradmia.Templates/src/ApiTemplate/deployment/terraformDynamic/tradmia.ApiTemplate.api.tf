data "azurerm_resource_group" "ApiTemplate-rg" {
  name     = var.rg-name
}

data "azurerm_service_plan" "ApiTemplate-asp" {
  name                = var.asp-name
  resource_group_name = var.asp-rg-name
}

resource "azurerm_linux_web_app" "ApiTemplate-api" {
  name                = var.app-name
  location            = var.location
  resource_group_name = var.rg-name
  service_plan_id     = data.azurerm_service_plan.ApiTemplate-asp.id
  https_only          = true

  identity {
    type = "SystemAssigned"
  }

  site_config {
    cors {
      allowed_origins = ["*"]
    }
    always_on         = true
    ftps_state        = "Disabled"
  }

  app_settings        = var.app_settings
}

resource "azurerm_storage_account" "ApiTemplate-st" {
  name                     = var.st-name
  resource_group_name      = var.rg-name
  location                 = var.st-location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_linux_function_app" "ApiTemplate-func" {
  name                        = var.func-name
  location                    = var.location
  resource_group_name         = var.rg-name
  service_plan_id             = data.azurerm_service_plan.ApiTemplate-asp.id
  storage_account_name        = azurerm_storage_account.ApiTemplate-st.name
  storage_account_access_key  = azurerm_storage_account.ApiTemplate-st.primary_access_key
  functions_extension_version = "~4"
  https_only                  = true

  identity {
    type                     = "SystemAssigned"
  }  

  site_config {
    always_on         = true
    ftps_state        = "Disabled"

    application_stack {
      dotnet_version              = "7.0"
      use_dotnet_isolated_runtime = true
    }    
  }

  app_settings        = var.func_settings
}