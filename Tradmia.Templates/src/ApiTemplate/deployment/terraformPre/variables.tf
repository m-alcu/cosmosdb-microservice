variable "location" {
  type        = string
  description = "Azure Region Location"
  default     = "westeurope"
}

variable "asp-rg-name" {
  default = "juno-rg-pre"
}

variable "asp-name" {
  default = "juno-asp-linux-pre"
}

variable "rg-name" {
  default = "junoApiTemplate-rg-pre"
}

variable "app-name" {
  default = "junoApiTemplate-api-pre"
}

variable "st-name" {
  default = "junoApiTemplateapistpre"
}

variable "func-name" {
  default = "junoApiTemplate-func-pre"
}

variable "app_settings" { 
  type = map(string)

  default = {
    "ASPNETCORE_ENVIRONMENT"                = "Preproduction"
    "DOCKER_REGISTRY_SERVER_URL"            = "https://junocr.azurecr.io/"
    "DOCKER_REGISTRY_SERVER_USERNAME"       = "junocr"
    "DOCKER_REGISTRY_SERVER_PASSWORD"       = "@Microsoft.KeyVault(SecretUri=https://juno-kv-pre.vault.azure.net/secrets/DOCKER-REGISTRY-SERVER-PASSWORD/ccea702430ea44448183c9d473aa032e)"
  }
}

variable "func_settings" { 
  type = map(string)

  default = {
    "APPINSIGHTS_INSTRUMENTATIONKEY"        = "23d46b43-0f8d-4c0e-9b61-6f9bca4da861"
    "APPLICATIONINSIGHTS_CONNECTION_STRING" = "InstrumentationKey=23d46b43-0f8d-4c0e-9b61-6f9bca4da861;IngestionEndpoint=https://westeurope-5.in.applicationinsights.azure.com/"    
    "AZURE_FUNCTIONS_ENVIRONMENT"           = "Preproduction"
    "FUNCTIONS_EXTENSION_VERSION"           = "~4"
  }
}