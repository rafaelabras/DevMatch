variable "conta" {
  description = "Nome da conta"
  type        = string
}

variable "regiao" {
  description = "Região da conta"
  type        = string
}

variable "subnet_publica_devmatch_id" {
  description = "ID da SUBNET publica do devmatch"
  type        = string
}

variable "sg_devmatch_id" {
  description = "ID SG do devmatch"
  type        = string
}

variable "role_arn_to_ecr" {
  description = "role para o ecs poder ter contato com o ecr"
  type        = string
}
