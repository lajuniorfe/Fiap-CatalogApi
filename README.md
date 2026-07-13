# 🎮 Catalog API

## 📖 Visão Geral

A **Catalog API** é o microsserviço responsável pelo gerenciamento do catálogo de jogos da plataforma.

Sua principal função é disponibilizar operações de consulta e gerenciamento dos jogos disponíveis, além de iniciar o fluxo de compra ao registrar um pedido realizado pelo usuário.

Após a criação de um pedido, a API publica um evento de domínio informando que uma compra foi realizada, permitindo que outros microsserviços processem essa informação de forma assíncrona.

A comunicação entre os microsserviços é realizada utilizando **RabbitMQ**, garantindo baixo acoplamento, escalabilidade e maior resiliência da arquitetura.

---

# 🚀 Responsabilidades

* Gerenciar o catálogo de jogos.
* Disponibilizar consulta dos jogos disponíveis.
* Registrar pedidos de compra.
* Publicar eventos de criação de pedidos.
* Integrar com o fluxo de pagamentos.
* Manter a comunicação assíncrona com outros microsserviços.

---

# 🛠️ Tecnologias Utilizadas

* .NET 10
* ASP.NET Core
* Entity Framework Core
* SQLite
* RabbitMQ
* Docker
* Kubernetes

---

# 🏗️ Arquitetura

O fluxo de compra ocorre conforme o diagrama abaixo:

```text
Usuário
   │
   │ Solicita compra
   ▼
Catalog API
   │
   │ Publica OrderPlacedEvent
   ▼
RabbitMQ (order-placed)
   │
   ▼
Payments API
   │
   │ Processa pagamento
   │
   │ Publica PaymentProcessedEvent
   ▼
RabbitMQ (payment-processed)
   │
   ▼
Notifications API
```

---

# 🔄 Fluxo de Funcionamento

1. O usuário solicita a compra de um jogo através do **Catalog API**.
2. A API valida as informações do pedido.
3. O pedido é registrado.
4. A **Catalog API** publica o evento `OrderPlacedEvent`.
5. O evento é enviado para a fila `order-placed` no RabbitMQ.
6. O **Payments API** consome o evento e realiza o processamento do pagamento.
7. Após o processamento, o fluxo segue para notificação do usuário.

---

# 📨 Filas Utilizadas

## Publica

| Fila | Evento |
|------|--------|
| `order-placed` | `OrderPlacedEvent` |

---

## Banco de Dados

A aplicação utiliza **SQLite** como banco de dados para persistência das informações do catálogo.

Durante a inicialização da aplicação, existe uma rotina de carga inicial responsável por criar jogos padrões no banco de dados, facilitando a execução do projeto e a realização de testes sem a necessidade de cadastrar dados manualmente.

---

# ▶️ Executando Localmente

### Restaurar dependências

```bash
dotnet restore
```

### Executar a aplicação

```bash
dotnet run
```

---

# 🐳 Executando com Docker

A **Catalog API** pode ser executada de forma independente para fins de desenvolvimento e testes.

### Build da imagem

```bash
docker build -t catalog-api .
```

### Executar o container

```bash
docker run -p 5002:8080 catalog-api
```

> **Observação:** Ao executar apenas este microsserviço, o gerenciamento do catálogo estará disponível normalmente. Porém, funcionalidades que dependem da comunicação com outros serviços, como o processamento de pagamentos, necessitam que o RabbitMQ e os demais microsserviços estejam em execução.

## 🚀 Executando a solução completa

Para simular o ambiente da aplicação de forma semelhante à produção, o recomendado é utilizar o repositório **Orchestrator**, responsável por orquestrar todos os microsserviços da plataforma.

O repositório do **Orchestrator** possui um **README** com todas as instruções necessárias para configurar e executar a solução completa. Após seguir as etapas descritas nesse repositório, basta executar:

```bash
docker compose up --build
```

Esse comando iniciará todos os componentes necessários da solução, incluindo:

- Users API
- Catalog API
- Payments API
- Notifications API
- RabbitMQ

Dessa forma, será possível testar toda a comunicação entre os microsserviços por meio de eventos, reproduzindo o funcionamento completo da arquitetura.

---

# ☸️ Executando no Kubernetes

### Aplicar os manifests

```bash
kubectl apply -f k8s/
```

### Verificar os recursos

```bash
kubectl get deployments
kubectl get pods
kubectl get services
```

### Consultar os logs

```bash
kubectl logs -f deployment/catalog-api
```

---

# 📁 Estrutura do Projeto

```text
Catalog.Api
├── Controllers
├── Domain
├── Events
├── Infrastructure
├── Messaging
├── Services
├── Program.cs
├── appsettings.json
└── Dockerfile
```

---

# 🔗 Microsserviços Relacionados

| Microsserviço | Responsabilidade |
|---------------|------------------|
| **Users API** | Gerenciamento de usuários, autenticação e publicação de eventos. |
| **Catalog API** | Gerenciamento do catálogo de jogos e criação dos pedidos. |
| **Payments API** | Processamento dos pagamentos. |
| **Notifications API** | Envio de notificações após eventos do sistema. |

---

# 🎯 Objetivo

Este microsserviço foi desenvolvido como parte de uma arquitetura baseada em **microsserviços**, utilizando comunicação orientada a eventos com **RabbitMQ**, persistência com **SQLite** e conteinerização com **Docker** e **Kubernetes**.

O projeto tem como objetivo demonstrar a aplicação de boas práticas de arquitetura, como:

* Separação de responsabilidades.
* Comunicação assíncrona entre serviços.
* Baixo acoplamento.
* Escalabilidade.
* Processamento orientado a eventos.
* Orquestração de containers com Kubernetes.
