resource "aws_security_group" "sg_web" {
  name        = "Security group para permitir trafego web"
  description = "Permitir http e https"
  vpc_id      = var.vpc_id

  ingress {
  cidr_blocks = ["0.0.0.0/0"]
  from_port = 443
  protocol = "tcp"
  to_port = 443
  }

  
  ingress {
  cidr_blocks = ["0.0.0.0/0"]
  from_port = 80
  protocol = "tcp"
  to_port = 80
  }

  egress {
   from_port = 0
   to_port = 0
  cidr_blocks = ["0.0.0.0/0"]
  protocol = "-1"
  }

  tags = {
    Name = "sg_web"
  }
}



resource "aws_security_group" "sg_sqlserver" {
  name        = "Security group para permitir trafego no sql server"
  description = "permitir request 1433 vindos apenas da vpc"
  vpc_id      = var.vpc_id

  ingress {
  cidr_blocks = [var.vpc_cidr_block]
  from_port = 1433
  protocol = "tcp"
  to_port = 1433
  }

  egress {
   from_port = 0
   to_port = 0
  cidr_blocks = [var.vpc_cidr_block]
  protocol = "-1"
  }

  tags = {
    Name = "sg_sqlserver"
  }
}


