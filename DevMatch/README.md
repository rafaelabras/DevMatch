# DevMatch

## Visão Geral
DevMatch é uma plataforma de mentoria voltada para conectar mentores e mentorados na comunidade tecnológica, facilitando o aprendizado e o compartilhamento de conhecimento. Desenvolvida com **ASP.NET Core 8.0**, a aplicação oferece uma API robusta para gerenciamento de usuários, agendamento de sessões de mentoria, chat em tempo real e avaliações de sessões. A plataforma utiliza **SignalR** para comunicação em tempo real, **Entity Framework Core** para operações de banco de dados com SQL Server e autenticação baseada em **JWT** (JSON Web Token) com suporte a **refresh tokens** para maior segurança e conveniência na gestão de sessões de usuário. A autorização é baseada em papéis, com três funções definidas: **Admin**, **Mentor** e **Mentorado**, garantindo controle de acesso granular.

## Funcionalidades
- **Gerenciamento de Usuários**: 
  - Registro e login de usuários com validação de credenciais.
  - Suporte a autenticação JWT com geração de tokens de acesso e refresh tokens para sessões prolongadas.
  - Mudança de papéis (de Mentorado para Mentor) via endpoint seguro.
- **Perfis de Mentores**: 
  - Mentores podem criar, atualizar e excluir perfis contendo informações como bio, stack tecnológica e disponibilidade.
  - Perfis são vinculados a usuários via relação um-para-um com o modelo `User`.
- **Gerenciamento de Sessões**: 
  - Mentores podem criar, atualizar e excluir sessões de mentoria, definindo tópicos, datas de início e fim, e status.
  - Mentorados podem se inscrever em sessões disponíveis.
  - Validação para garantir que datas de sessões não sejam no passado.
- **Chat em Tempo Real**: 
  - Integração com **SignalR** para comunicação em tempo real em sessões, permitindo troca de mensagens com até 500 caracteres.
  - Suporte a notificações de sistema enviadas por mentores ou administradores.
- **Sistema de Avaliação**: 
  - Mentorados podem atribuir notas (de 1 a 5) e comentários (até 500 caracteres) às sessões em que participaram.
  - Funcionalidades para atualizar ou excluir avaliações, com validação para garantir que apenas participantes da sessão possam avaliar.
- **Infraestrutura e Escalabilidade**:
  - Containerização com **Docker** e orquestração via **Docker Compose** para implantação simplificada.
  - Banco de dados SQL Server para armazenamento persistente, com migrações automáticas gerenciadas por Entity Framework Core.
- **Segurança**:
  - Autenticação JWT com refresh tokens para renovação segura de sessões.
  - Validação de tokens configurada com parâmetros rigorosos (emissor, audiência, chave de assinatura).
  - Autorização baseada em papéis para endpoints restritos.

## Pré-requisitos
- **Docker** e **Docker Compose** para implantação containerizada.
- **.NET SDK 8.0** para desenvolvimento e execução local.
- **SQL Server** (pode ser utilizado via container fornecido no Docker Compose).
- **Ferramentas de linha de comando** como `curl` ou clientes HTTP (ex.: Postman) para testar a API.
- **Navegador** para acessar a documentação Swagger (em ambiente de desenvolvimento).

## Instruções de Configuração
1. **Clonar o Repositório**:
   ```bash
   git clone https://github.com/seuusuario/devmatch.git
   cd devmatch
   ```

2. **Configuração do Docker Compose**:
   > **Nota Importante**: O arquivo `docker-compose.yml` original está localizado no diretório pai do projeto devido à hierarquia do Docker Compose. Para executar, navegue até o diretório pai, e copie o docker-compose-para-leitura.yml, renomeie para docker-compose.yml no diretório pai:
   ```bash
   cd ..
   docker-compose up --build
   ```
   O Docker Compose orquestra três serviços:
   - **devmatch**: A aplicação ASP.NET Core rodando na porta 8080.
   - **sqlserver**: Instância do SQL Server na porta 1433.
   - **migrations**: Serviço para aplicar migrações do Entity Framework Core automaticamente.

3. **Aplicar Migrações**:
   - O serviço `migrations` no Docker Compose executa `dotnet ef database update` para configurar o banco de dados automaticamente.
   - Caso queira aplicar migrações manualmente:
     ```bash
     cd DevMatch
     dotnet ef database update
     ```

4. **Acessar a Aplicação**:
   - API: `http://localhost:8080`
   - Documentação Swagger (em desenvolvimento): `http://localhost:8080/swagger`
   - Teste endpoints protegidos utilizando o token JWT gerado no login (formato: `Bearer {seu_token}`).

## Estrutura do Projeto
- **Controllers**:
  - `UserController`: Gerencia registro, login, validação de refresh tokens e mudança de papéis.
  - `MentorController`: Operações CRUD para perfis de mentores.
  - `SessionController`: Gerenciamento de sessões (criação, atualização, exclusão, inscrição).
  - `ChatController`: Envio e recuperação de mensagens de sessões.
  - `RatingController`: Atribuição, atualização e exclusão de avaliações.
- **Hubs**:
  - `ChatHubs`: Hub SignalR para mensagens em tempo real, com suporte a grupos por sessão.
- **Repositories**:
  - Camada de acesso a dados com Entity Framework Core, incluindo `MentorRepository`, `SessionRepository`, `MessageRepository` e `RatingRepository`.
- **DTOs**:
  - Objetos de transferência de dados como `RegisterProfileDto`, `SessionResponseDto`, `MessageResponseDto` e `RatingResponseDto` para comunicação estruturada.
- **Models**:
  - Entidades principais: `User`, `MentorProfile`, `Session`, `Rating` e `ChatMessage`.
  - Configurações de entidades com restrições (ex.: notas entre 1 e 5, limites de caracteres).
- **Dockerfile**:
  - Define a construção da aplicação em camadas (base, build, publish, final) para otimização.
- **Docker Compose**:
  - Orquestra a aplicação, SQL Server e migrações, com volumes para persistência de dados e pacotes NuGet.

## Futuras Melhorias
- **Integração com GitHub Actions**:
  - Planejada implementação de pipelines CI/CD no GitHub Actions para automação de testes unitários, construção da aplicação e implantação em ambientes de staging e produção.
  - Configuração de workflows para verificar a integridade do código e executar migrações automaticamente.
- **Funcionalidades Adicionais**:
  - Filtros de busca avançados para encontrar mentores por stack tecnológica ou disponibilidade.
  - Integração com notificações por e-mail ou push para lembretes de sessões.
  - Interface de usuário frontend para uma experiência mais amigável.
  - Suporte a múltiplos idiomas e internacionalização.
