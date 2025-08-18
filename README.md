 O **DevMatch** é uma plataforma de mentoria voltada para conectar **mentores** e **mentorados** da comunidade tecnológica, facilitando o aprendizado e o compartilhamento de conhecimento.  

A aplicação é desenvolvida em **ASP.NET Core 8.0**, containerizada com **Docker**, integrada ao **SQL Server** e infra construida na **AWS (ECS + ECR + RDS)** via **Terraform**.  

Conta com:  
- Autenticação **JWT** segura (com refresh tokens).  
- **Chat em tempo real** via SignalR.  
- Sistema completo de **sessões de mentoria e avaliações**.  
- ⚙**CI/CD automatizado** no GitHub Actions com testes e infraestrutura para deploy na AWS.  

---

##  Funcionalidades

###  Gerenciamento de Usuários
- Registro e login com autenticação JWT + refresh tokens.  
- Alteração de papéis (**Admin**, **Mentor**, **Mentorado**).  

### Perfis de Mentores
- CRUD de perfis (bio, stack, disponibilidade).  
- Relacionamento **1–1 com usuário**.  

###  Sessões de Mentoria
- CRUD de sessões com tópicos, datas e status.  
- Inscrição de mentorados.  
- Validação de datas.  

###  Chat em Tempo Real (SignalR)
- Mensagens em sessões (até 500 caracteres).  
- Notificações enviadas por mentores/admins.  

###  Sistema de Avaliação
- Notas (1–5) + comentários (até 500 caracteres).  
- Apenas participantes podem avaliar.  

###  Segurança
- Autenticação JWT com refresh tokens.  
- Validação de emissor, audiência e assinatura.  
- Autorização baseada em papéis.  

---

##  Tecnologias Utilizadas
- **ASP.NET Core 8.0 (API)**  
- **Entity Framework Core (ORM)**  
- **SQL Server (Banco de dados)**  
- **SignalR (Chat em tempo real)**  
- **JWT (Autenticação/autorização)**  
- **NUnit + JUnitXml.TestLogger (Testes e relatórios)**  
- **Docker + Docker Compose (Orquestração local)**  
- **Terraform (Provisionamento de Infra AWS)**  
- **AWS ECS + ECR + RDS (Infra feita com terraform para dev)**  
- **GitHub Actions (CI/CD)**  

---

##  Pré-requisitos
- .NET SDK 8.0  
- Docker e Docker Compose  
- SQL Server local ou containerizado  
- Terraform (se for provisionar na AWS)  
- Postman/Insomnia para testar a API  

---
## Execução Local sem docker:

```)
git clone https://github.com/seuusuario/devmatch.git
cd devmatch
dotnet restore
dotnet ef database update
dotnet run --project DevMatch
```

## Estrutura do projeto:

DevMatch/       
DevMatch.Tests/        
infra/                  
.github/workflows/      
docker-compose.yml      


# CI/CD (GitHub Actions + Terraform + ECS)

##  Pipeline de Integração Contínua (CI)
- Build & Restore do projeto  
- Execução de Testes Unitários NUnit  
- Geração de relatórios de teste em **JUnit XML**  
- Cache de pacotes **NuGet** para otimizar builds  

## Pipeline de Deploy Contínuo (CD)
- `terraform apply` → provisiona/atualiza infra na **AWS**  
- Build da imagem **Docker**  
- Push para o **ECR**  
- Força novo deployment no **ECS Service**  

