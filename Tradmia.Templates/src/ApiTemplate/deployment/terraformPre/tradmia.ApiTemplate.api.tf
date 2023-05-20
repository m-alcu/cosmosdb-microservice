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

    ip_restriction {
        name = "junogw-apim-pre"
        priority = 1
        action = "Allow"
        ip_address = "40.67.220.105/32"
    }

    ip_restriction {
        name = "ctaima-office"
        priority = 2
        action = "Allow"
        ip_address = "213.96.18.140/32"
    }

     ip_restriction {
        name = "junoaggservice-api-pre-ip1"
        priority = 3
        action = "Allow"
        ip_address = "51.138.102.94/32"
    }

    ip_restriction {
        name = "junoaggservice-api-pre-ip2"
        priority = 3
        action = "Allow"
        ip_address = "51.138.103.181/32"
    }

    ip_restriction {
        name = "junoaggservice-api-pre--ip3"
        priority = 3
        action = "Allow"
        ip_address = "51.138.103.186/32"
    }

    ip_restriction {
        name = "junoaggservice-api-pre--ip4"
        priority = 3
        action = "Allow"
        ip_address = "20.50.152.150/32"
    }

    ip_restriction {
        name = "junoaggservice-api-pre-ip5"
        priority = 3
        action = "Allow"
        ip_address = "20.50.154.94/32"
    }

    ip_restriction {
        name = "junoaggservice-api-pre-ip6"
        priority = 3
        action = "Allow"
        ip_address = "20.50.159.86/32"
    }

    ip_restriction {
        name = "junoaggservice-api-pre-ip7"
        priority = 3
        action = "Allow"
        ip_address = "20.50.2.2/32"
    }
  }

  app_settings        = var.app_settings
}

resource "azurerm_storage_account" "ApiTemplate-st" {
  name                     = var.st-name
  resource_group_name      = var.rg-name
  location                 = var.location
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
