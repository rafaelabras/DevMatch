output "subnet_id_privada" {
  description = "ID da subnet PRIVADA"
  value       = aws_subnet.subnet_privada.id
}

output "subnet_id_publica" {
  description = "ID da subnet PUBLICA"
  value       = aws_subnet.subnet_publica.id
}
