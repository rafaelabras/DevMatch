variable "regiao" {
  description = "regiao dos resources"
  type        = string
  default     = "us-east-1"
}

variable "vpc_cidr_block" {
  description = "CIDR block da VPC"
  type        = string
}

variable "vpc_name" {
  description = "Nome da VPC"
  type        = string
}

variable "nome_subnet_privada" {
  description = "Nome da subnet privada"
  type        = string
}

variable "nome_subnet_publica" {
  description = "Nome da subnet publica"
  type        = string
}

variable "subnet_cidr_publico" {
  description = "cidr block da subnet publica"
  type        = string
}

variable "subnet_cidr_privado" {
  description = "cidr block da subnet privada"
  type        = string
}

variable "rds_username" {
  description = "username do rds"
  type        = string
}

variable "rds_password" {
  description = "password do rds"
  type        = string
}