variable "username" {
  description = "Nome da instancia RDS"
  type        = string
}

variable "password" {
  description = "Senha da instancia RDS"
  type        = string
}

variable "subnet_name_group" {
  description = "Nome da subnet para o RDS"
  type        = string
}

variable "sg_group_private_id" {
  description = "ID do sg para o RDS"
  type        = string
}