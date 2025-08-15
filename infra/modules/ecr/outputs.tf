output "ecr_repo_id" {
  description = "ID do ecr"
  value       = aws_ecr_repository.ecr_repo.id

}