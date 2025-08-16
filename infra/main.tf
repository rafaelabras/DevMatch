module "vpc" {
  source     = "./modules/vpc"
  cidr_block = var.vpc_cidr_block
  vpc_name   = var.vpc_name
}

module "subnets" {
  source              = "./modules/subnets"
  vpc_id              = module.vpc.main_vpc_id
  nome_subnet_privada = var.nome_subnet_privada
  nome_subnet_publica = var.nome_subnet_publica
  cidr_block_da_vpc   = module.vpc.main_vpc_cidr
  cidr_block_publico  = var.subnet_cidr_publico
  cidr_block_privado  = var.subnet_cidr_privado
}

module "sg_group" {
  source         = "./modules/sg"
  vpc_id         = module.vpc.main_vpc_id
  vpc_cidr_block = var.vpc_cidr_block
}

module "rds" {
  source              = "./modules/rds"
  username            = var.rds_username
  password            = var.rds_password
  subnet_id_privada   = module.subnets.subnet_id_privada
  sg_group_private_id = module.sg_group.sg_sqlserver_id
}

module "ecr" {
  source = "./modules/ecr"
}

data "aws_caller_identity" "current" {}

module "iam_role" {
  source = "./modules/iam_role"
}

module "ecs" {
  source                     = "./modules/ecs"
  depends_on                 = [module.ecr]
  conta                      = data.aws_caller_identity.current.account_id
  regiao                     = var.regiao
  subnet_publica_devmatch_id = module.subnets.subnet_id_publica
  sg_devmatch_id             = module.sg_group.sg_web_id
  role_arn_to_ecr            = module.iam_role.arn_iam_role
}





