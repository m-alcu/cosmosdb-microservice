variable "location" {
  type        = string
  description = "Azure Region Location"
  default     = "westeurope"
}

variable "st-location" {
  type        = string
  description = "Azure Region Location"
  default     = "francecentral"
}

variable "asp-rg-name" {
  default = "juno-asp-rg-qa"
}

variable "asp-name" {
  default = "juno-asp-linux-dynamic"
}

variable "rg-name" {
  default = "junoApiTemplatepany-rg-dynamic"
}

variable "app-name" {
  default = "ApiTemplateVERSIONNAME-api-dynamic"
}

variable "st-name" {
  default = "ApiTemplateVERSIONNAMEapistdynamic"
}

variable "func-name" {
  default = "ApiTemplateVERSIONNAME-func-dynamic"
}

variable "app_settings" { 
  type = map(string)

  default = {
    "ASPNETCORE_ENVIRONMENT"                = "Dynamic"
    "DOCKER_REGISTRY_SERVER_URL"            = "https://junocr.azurecr.io/"
    "DOCKER_REGISTRY_SERVER_USERNAME"       = "junocr"
    "DOCKER_REGISTRY_SERVER_PASSWORD"       = "@Microsoft.KeyVault(SecretUri=https://juno-kv-qa.vault.azure.net/secrets/DOCKER-REGISTRY-SERVER-PASSWORD/cff16c70515d426fadc4c14c3f221a79)"
  }
}

variable "func_settings" { 
  type = map(string)

  default = {
    "APPINSIGHTS_INSTRUMENTATIONKEY"        = "bd4ba500-2864-4f1e-8419-3f5f3c7c27e5"
    "APPLICATIONINSIGHTS_CONNECTION_STRING" = "InstrumentationKey=bd4ba500-2864-4f1e-8419-3f5f3c7c27e5;IngestionEndpoint=https://westeurope-5.in.applicationinsights.azure.com/"    
    "AZURE_FUNCTIONS_ENVIRONMENT"           = "Dynamic"
    "FUNCTIONS_EXTENSION_VERSION"           = "~4"
  }
}