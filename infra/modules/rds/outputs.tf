output "rds_id" {
  description = "ID da instancia do RDS"
  value       = aws_db_instance.db_sqlserver.id
}

output "rds_endpoint" {
  value = aws_db_instance.db_sqlserver.endpoint
}

output "rds_port" {
  value = aws_db_instance.db_sqlserver.port
}

output "rds_username" {
  value = aws_db_instance.db_sqlserver.username
}

output "rds_db_name" {
  value = aws_db_instance.db_sqlserver.db_name
}