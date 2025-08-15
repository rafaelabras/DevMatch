resource "aws_subnet" "subnet_publica" {
    vpc_id = var.vpc_id
    cidr_block = var.cidr_block

    tags = {
        Name = var.nome_vpc_publica
    }
}

resource "aws_subnet" "subnet_privada" {
    vpc_id = var.vpc_id
    cidr_block = var.cidr_block

    tags = {
        Name = var.nome_vpc_privada
    }
}

resource "aws_internet_gateway" "igw" {
    vpc_id = var.vpc_id
}

resource "aws_route_table" "route_table_publica" {
  vpc_id = var.vpc_id

  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = aws_internet_gateway.igw.id
  }
}

resource "aws_route_table" "route_table_privada" {
  vpc_id = var.vpc_id

  route {
    cidr_block = var.cidr_block
  }

}

resource "aws_route_table_association" "association_internet" {
   subnet_id = aws_subnet.subnet_publica.id
   route_table_id = aws_route_table.route_table_publica.id    
}

resource "aws_route_table_association" "association privada" {
   subnet_id = aws_subnet.subnet_privada.id
   route_table_id = aws_route_table.route_table_privada.id  
}


