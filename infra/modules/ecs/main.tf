resource "aws_ecs_cluster" "ecs_cluster" {
  name = "cluster-devmatch"

}

resource "aws_ecs_task_definition" "task_definition" {
  family = "devmatch-service"
  network_mode = "awsvpc"
  requires_compatibilities = ["FARGATE"]
  cpu    = 256
  memory = 512
  container_definitions = jsonencode([
    {
      name   = "devmatch"
      image  = "${var.conta}.dkr.ecr.${var.regiao}.amazonaws.com/repositorio-ecr-devmatch:latest"
      cpu       = 256
      memory    = 512
      portMappings = [{
        containerPort = 80
        hostPort      = 80
        protocol      = "tcp"
        }, {
        containerPort = 443
        hostPort      = 443
        protocol      = "tcp"
      }]


    }
  ])

}


resource "aws_ecs_service" "service" {
    name = "devmatch"
    cluster = aws_ecs_cluster.ecs_cluster.id
    task_definition = aws_ecs_task_definition.task_definition.arn
    desired_count = 1

    network_configuration {
      subnets = [var.subnet_publica_devmatch_id]
      security_groups = [var.sg_devmatch_id]
      assign_public_ip = true
    }
}