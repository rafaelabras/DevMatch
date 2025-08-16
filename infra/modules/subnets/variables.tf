variable "vpc_id" {
  description = "ID da VPC"
  type        = string
}

variable "cidr_block_publico" {
  description = "bloco CIDR publico da vpc"
  type        = string
}

variable "cidr_block_privado" {
  description = "bloco CIDR privado da vpc"
  type        = string
}

variable "cidr_block_da_vpc" {
  description = "cidr block da vpc"
  type        = string
}

variable "nome_subnet_privada" {
  description = "Nome da VPC"
}

variable "nome_subnet_publica" {
  description = "Nome da VPC"
}