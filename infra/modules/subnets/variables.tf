variable "vpc_id" {
    description = "ID da VPC"
    type = string
}

variable "cidr_block" {
    description = "bloco CIDR da vpc"
    type = string
}

variable "nome_vpc_privada" {
    description = "Nome da VPC"
}

variable "nome_vpc_publica" {
    description = "Nome da VPC"
}