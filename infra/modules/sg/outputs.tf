output "sg_web_id" {
  description = "ID do sg web"
  value       = aws_security_group.sg_web.id
}

output "sg_sqlserver_id" {
  description = "ID do sg sqlserver"
  value       = aws_security_group.sg_sqlserver.id
}

output "sg_rds_instance" {
  description = "ID da instancia rds"
  value       = aws_security_group.sg_sqlserver.id
}