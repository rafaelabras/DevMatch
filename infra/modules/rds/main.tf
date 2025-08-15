resource "aws_db_instance" "db_sqlserver" {
  allocated_storage      = 20
  db_name                = "mysqlserver"
  engine                 = "sqlserver-ex"
  engine_version         = "16.0.4205.1"
  multi_az               = false
  instance_class         = "db.t3.micro"
  username               = var.username
  password               = var.password
  db_subnet_group_name   = var.subnet_name_group
  skip_final_snapshot    = true
  license_model          = "license-included"
  storage_type           = "gp2"
  vpc_security_group_ids = [var.sg_group_private_id]
}