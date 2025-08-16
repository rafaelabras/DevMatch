output "arn_iam_role" {
  description = "ARN da iam role"
  value       = aws_iam_role.role.arn
}