
[![CI - Build, Test e SonarCloud](https://github.com/FilipeAlan/MBA-ProjectFive/actions/workflows/ci-cd-sonar.yml/badge.svg)](https://github.com/FilipeAlan/MBA-ProjectFive/actions/workflows/ci-cd-sonar.yml)
[![.NET](https://img.shields.io/badge/.NET-9.0-blueviolet?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
![SonarCloud Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=FilipeAlan_MBA-ProjectFive&metric=alert_status)
![Coverage](https://sonarcloud.io/api/project_badges/measure?project=FilipeAlan_MBA-ProjectFive&metric=coverage)
![Bugs](https://sonarcloud.io/api/project_badges/measure?project=FilipeAlan_MBA-ProjectFive&metric=bugs)


# **PEO - Plataforma de Educação Online (MBA Project Five)**

## **Apresentação**

Bem-vindo ao repositório do projeto **PEO (Plataforma de Educação Online)**.  
Este projeto é uma entrega do **MBA DevXpert Full Stack .NET** (Desenvolvedor.IO) e nasceu no terceiro módulo, sendo evoluído no **quinto módulo** para incorporar práticas de **DevOps, CI/CD, Docker e Kubernetes**.

O objetivo principal é desenvolver uma **plataforma educacional online** com múltiplos bounded contexts (BC), aplicando:

- DDD  
- TDD  
- CQRS  
- Padrões arquiteturais modernos  
- Boas práticas de **qualidade de código** e **entrega contínua**

---

### **Autor**
- **Filipe Alan Elias**

---

## **Proposta do Projeto**

O projeto consiste em:

- **APIs RESTful:** Exposição dos endpoints necessários para os casos de uso.
- **Autenticação e Autorização:** Implementação de controle de acesso, diferenciando administradores e alunos.
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM.
- **Integração com DevOps:** Pipelines automatizados, análise de código, containerização e orquestração.

---

## **Tecnologias Utilizadas**

### **Linguagem e Frameworks**
- **C#**
- **ASP.NET Core Web API**
- **ASP.NET Core MVC** (quando aplicável)
- **Entity Framework Core**

### **Banco de Dados**
- **SQL Server**
- **SQLite** (para testes e desenvolvimento)

### **Autenticação e Autorização**
- **ASP.NET Core Identity**
- **JWT (JSON Web Token)**

### **Documentação**
- **Swagger / OpenAPI**

### **DevOps / Infra**
- **GitHub Actions — CI/CD**
- **SonarCloud — Análise Estática**
- **Docker**
- **Docker Hub**
- **Kubernetes (Kind / Minikube)**
- **docker-compose**

---

## **Estrutura do Projeto**

```
src/                  Código-fonte dos microsserviços
tests/                Testes automatizados (unidade e integração)
k8s/                  Manifests Kubernetes (Deployments, Services, etc.)
docs/                 Documentação detalhada do projeto
README.md             Documentação principal
FEEDBACK.md           Feedbacks do instrutor (não editar)
DEVELOPMENT.md        Notas adicionais do desenvolvimento
docker-compose.yml    Ambiente completo para desenvolvimento
```

---

# **Como Executar o Projeto**

## **Pré-requisitos**

- .NET SDK **9.0**
- Docker Desktop
- SQL Server ou SQLite
- Visual Studio 2022 / VS Code / Rider
- Git

---

## **1️⃣ Clonar o Repositório**

```bash
git clone https://github.com/FilipeAlan/MBA-ProjectFive.git
cd MBA-ProjectFive
```

---

## **2️⃣ Configuração do Banco de Dados**

No arquivo:

```
src/Peo.Web.Api/appsettings.json
```

Configure a connection string do **SQL Server**.

Ao executar a API pela primeira vez, o **Seed** criará a base e populará dados básicos.

---

## **3️⃣ Executar a API (modo local)**

```bash
cd src/Peo.Web.Api
dotnet run --launch-profile "https"
```

Acesse a documentação da API:

```
https://localhost:7113/swagger
```

---

# **Execução via Docker Compose (DevOps / Módulo 5)**

Para subir o ambiente completo:

```bash
docker-compose up --build
```

O docker-compose inicia:

- Banco de dados  
- Auth API  
- Conteúdo API  
- Alunos API  
- Pagamentos API  
- BFF (Backend For Frontend)  

---

# **Configurações Importantes**

### 🔐 JWT  
As chaves ficam em:

```
src/<Serviço>/appsettings.json
```

### 🧩 Migrações
O EF Core cria e popula o banco automaticamente via Seed.

---

# **Documentação da API**

## Documentação da API

Documentação completa:  
👉 **[📘 Abrir documentação completa](/docs/README.md)**

Swagger:  

```
https://localhost:<porta>/swagger
```

---

# **Testes, Coverage e CI/CD**

Este repositório utiliza **dois pipelines**:

### ✔ `dotnet.yml` — Pipeline antigo  
- Build  
- Testes  
- Relatório dotCover  
- Upload como artefato  

### ✔ `ci-sonarcloud.yml` — Pipeline novo (DevOps/Módulo 5)  
- Build .NET 9  
- Testes com cobertura  
- Análise no **SonarCloud**  
- Quality Gate  
- Integração contínua automática  

Cobertura manual:

```
scripts/run-tests-with-coverage.ps1
```

Relatório:

```
scripts/report.html
```

SonarCloud:

👉 https://sonarcloud.io/project/overview?id=FilipeAlan_MBA-ProjectFive

---

# **Avaliação do Projeto**

Este projeto faz parte do MBA DevXpert e será avaliado considerando:

- Funcionalidades DevOps  
- Qualidade do Código  
- Kubernetes  
- Observabilidade  
- Documentação  
- Resolução de Feedbacks  

O arquivo **FEEDBACK.md** será atualizado pelo instrutor.

---

# **📬 Contato**

Para dúvidas ou sugestões, utilize as **Issues do GitHub**.
