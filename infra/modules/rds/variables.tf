variable "username" {
  description = "Nome da instancia RDS"
  type        = string
}

variable "password" {
  description = "Senha da instancia RDS"
  type        = string
}

variable "subnet_id_privada" {
  description = "ID da subnet privada"
  type        = string
}

variable "sg_group_private_id" {
  description = "ID do sg para o RDS"
  type        = string
}