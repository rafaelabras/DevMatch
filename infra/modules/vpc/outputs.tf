output "main_vpc_id" {
    description = "ID da vpc main"
    value = aws_vpc.main.id
}

output "main_vpc_cidr" {
    description = "cidr da vpc principal"
    value = aws_vpc.main.cidr_block
}