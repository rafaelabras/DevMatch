resource "aws_ecr_repository" "ecr_repo" {
  name                 = "repositorio-ECR-devmatch"
  image_tag_mutability = "MUTABLE"

}
