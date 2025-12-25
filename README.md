# 📘 **PEO - Plataforma de Educação Online (MBA Project Five)**

[![CI - Build, Test e SonarCloud](https://github.com/FilipeAlan/MBA-ProjectFive/actions/workflows/ci-cd-sonar.yml/badge.svg)](https://github.com/FilipeAlan/MBA-ProjectFive/actions/workflows/ci-cd-sonar.yml)
[![.NET](https://img.shields.io/badge/.NET-9.0-blueviolet?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=FilipeAlan_MBA-ProjectFive&metric=coverage)](https://sonarcloud.io/api/project_badges/measure?project=FilipeAlan_MBA-ProjectFive&metric=coverage)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=FilipeAlan_MBA-ProjectFive&metric=bugs)

# **Apresentação**

Bem-vindo ao repositório do projeto **PEO (Plataforma de Educação Online)**.

Este projeto é uma entrega do **MBA DevXpert Full Stack .NET – Desenvolvedor.IO**, iniciado no **Módulo 5** e evoluído no **Módulo 5** com foco em:

- DevOps  
- CI/CD  
- Docker  
- Kubernetes  
- Observabilidade  
- Boas práticas de engenharia de software  

---

# 👤 **Autor**
**Filipe Alan Elias**

---

# 🎯 **Proposta do Projeto**

Uma plataforma educacional moderna composta por múltiplos **microsserviços**, organizados por **bounded contexts**, contemplando:

- APIs RESTful  
- Autenticação e autorização (Identity + JWT)  
- Regras de negócio separadas por contexto  
- Integração entre serviços via BFF  
- Persistência com EF Core  
- Pipelines automatizados  
- Orquestração em Kubernetes  

---

# 🛠 **Tecnologias Utilizadas**

### **Backend**
- C# / .NET 9  
- ASP.NET Core Web API  
- ASP.NET Core MVC  
- Entity Framework Core  
- FluentValidation  

### **Frontend**
- SPA (JavaScript)

### **Banco de Dados**
- SQL Server  
- SQLite (testes)

### **Autenticação**
- ASP.NET Core Identity  
- JWT  

### **Infra & DevOps**
- Docker  
- Docker Hub  
- docker-compose  
- Kubernetes (Docker Desktop)  
- GitHub Actions (CI/CD)  
- SonarCloud  

---

# 📂 **Estrutura do Projeto**

```
src/                  Código-fonte dos microsserviços
tests/                Testes automatizados
k8s/                  Manifestos Kubernetes
docs/                 Documentação adicional
README.md             Documentação principal
FEEDBACK.md           Feedback do instrutor
DEVELOPMENT.md        Notas e decisões do desenvolvimento
docker-compose.yml    Ambiente completo via Docker Compose
```

---

# 🚀 **Como Executar o Projeto**

## 1️⃣ Pré-requisitos

- .NET SDK 9  
- Docker Desktop  
- SQL Server ou SQLite  
- Git  
- VS Code / Visual Studio / Rider  

---

## 2️⃣ Clonar o Repositório

```bash
git clone https://github.com/FilipeAlan/MBA-ProjectFive.git
cd MBA-ProjectFive
```

---

## 3️⃣ Configuração do Banco de Dados

No arquivo:

```
src/<Serviço>/appsettings.json
```

Defina a connection string do SQL Server.  
As bases são criadas automaticamente via **Seed**.

---

## 4️⃣ Subir o Ambiente com Docker Compose

```bash
docker-compose up --build
```

Isso iniciará:

- Identity API  
- Conteúdo API  
- Alunos API  
- Faturamento API  
- BFF  
- Frontend  
- Banco de Dados  

---

# 🧠 Documentação das APIs

Acesse o Swagger de qualquer API:

```
https://localhost:<PORTA>/swagger
```

Documentação adicional está em:

```
/docs
```

---

# ☸️ Execução no Kubernetes

Os manifestos Kubernetes estão em:

```
k8s/
```

## Criar namespace

```bash
kubectl apply -f k8s/00-namespace.yaml
```

## Aplicar todos os recursos

```bash
kubectl apply -f k8s/
```

## Verificar pods

```bash
kubectl get pods -n peo
```

---

# 🌐 Acessar Serviços no Kubernetes

### Frontend SPA
```bash
kubectl port-forward svc/peo-frontend -n peo 5100:80
```
Acesse:
```
http://localhost:5100
```

### BFF
```bash
kubectl port-forward svc/peo-web-bff -n peo 5000:8080
```

---

# 🔧 ConfigMaps e Secrets

As configurações são divididas entre:

- **ConfigMaps** → URLs internas, chaves não sensíveis  
- **Secrets** → connection strings, tokens JWT, dados sigilosos  

Exemplo de uso:

```yaml
env:
  - name: IDENTITY_URL
    valueFrom:
      configMapKeyRef:
        name: peo-config
        key: identityUrl
```

---

# ❤️‍🩹 Health Checks (Liveness / Readiness)

Todas as APIs fornecem health check interno:

```
/health
```

Exemplo do Kubernetes:

```yaml
livenessProbe:
  httpGet:
    path: /health
    port: 8080
readinessProbe:
  httpGet:
    path: /health
    port: 8080
```

Isso garante que o Kubernetes saiba quando:

- reiniciar um container (liveness)  
- enviá-lo ou não tráfego (readiness)  

---

# 🔄 CI/CD – Integração Contínua e Entrega Contínua

O projeto possui **dois workflows GitHub Actions**:

## ✔ CI – Build, Test e SonarCloud
Arquivo:
```
.github/workflows/ci-cd-sonar.yml
```

Executa:

- Restore  
- Build .NET  
- Testes com cobertura  
- Análise SonarCloud  
- Quality Gate  

## ✔ CD – Deploy Automático para o Docker Hub
Arquivo:
```
.github/workflows/cd-DockerHubDeploy.yml
```

Executa:

- Build das imagens Docker dos serviços  
- Login automático no Docker Hub  
- Push das imagens (`latest`)  

Secrets necessários:

- `DOCKERHUB_USERNAME`  
- `DOCKERHUB_TOKEN`  

---

# 🔁 Fluxos Funcionais Validados

Testados integralmente dentro do Kubernetes:

- Registro de usuário  
- Autenticação (JWT)  
- Listagem e matrícula em cursos  
- Publicação e acesso a conteúdos  
- Fluxo de pagamentos (mock)  
- Geração de certificado  
- Comunicação via BFF para o Frontend SPA  

---

# 🏗 Arquitetura da Solução

```
[ Frontend SPA ] → [ BFF ]
                       ↓
              ┌────────┼────────┬────────┐
              │        │        │        │
      Identity API   Alunos   Conteúdo   Faturamento
```

- Cada serviço está isolado em **um Deployment**
- Comunicação interna ocorre por **Services**
- BFF centraliza toda comunicação com o frontend  
- Kubernetes gerencia disponibilidade e escalabilidade  

---

# 📊 Observabilidade

- Logs estruturados (ILogger)  
- Health check endpoints  
- Detecção automática de falhas via probes  

---

# 📑 Avaliação do Projeto

Critérios atendidos:

- DevOps (CI/CD completo)  
- Qualidade do código (SonarCloud)  
- Docker e Kubernetes  
- Documentação adequada  
- Feedbacks aplicados  
- Fluxos funcionais implementados  

O arquivo **FEEDBACK.md** será atualizado pelo instrutor após avaliação.

---

# 📬 Contato

Para dúvidas, sugestões ou melhorias, utilize:

👉 **Issues do GitHub**

---

# 🎉 Obrigado por visitar o projeto!
