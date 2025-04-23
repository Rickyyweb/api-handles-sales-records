
# Developer Evaluation Project - Henrique Fernandes

Este projeto foi desenvolvido como parte de um desafio técnico da equipe DeveloperStore, focado em aplicar arquitetura DDD, padrões modernos de desenvolvimento, e boas práticas de organização e testes.
Fiz algumas rotas a mais, pensando em facilitar o uso do frontend, caso fosse uma aplicação real. E também para facilitar o avaliador do teste, evitando que ele precisasse ir diversas vezes ao banco de dados para pegar os dados.

## ✅ O que foi implementado

A API de gerenciamento de vendas contempla as seguintes funcionalidades:

### 📌 Funcionalidades principais
- CRUD completo de vendas
- Estrutura baseada em **DDD**, com separação clara entre camadas
- Denormalização de entidades com padrão *External Identities*
- Aplicação de descontos com base na quantidade de itens vendidos
- Publicação de eventos de domínio no log de aplicação:

### 🗂️ Eventos publicados (via Serilog)
- `SaleCreatedEvent`
- `SaleModifiedEvent`
- `SaleCancelledEvent`
- `SaleProductCreatedEvent`
- `SaleProductModifiedEvent`
- `SaleProductCancelledEvent`

> Todos os eventos são logados para auditoria e rastreamento no fluxo da aplicação.

### 🧠 Regras de Negócio
- Compras acima de 4 itens idênticos → **10% de desconto**
- Compras entre 10 e 20 itens idênticos → **20% de desconto**
- Limite de 20 unidades por item (venda acima disso não é permitida)
- Compras com menos de 4 unidades **não recebem desconto**

### 🌐 Rotas implementadas

| Método | Rota                                               | Descrição                            |
|--------|----------------------------------------------------|--------------------------------------|
| POST   | `/api/Sales`                                       | Criar nova venda                     |
| GET    | `/api/Sales/{id}`                                  | Consultar venda por ID               |
| PUT    | `/api/Sales/{id}`                                  | Alterar dados da venda               |
| PUT    | `/api/Sales/{id}/products/{productId}`             | Atualizar produto da venda           |
| DELETE | `/api/Sales/{id}/products/{productId}`             | Remover produto da venda             |
| POST   | `/api/Sales/{saleId}/products`                     | Incluir novo produto à venda         |
| DELETE | `/api/Sales/{id}`                                  | Cancelar venda                       |

---

## ⚙️ Como rodar o projeto

### 1. Clone o repositório
```bash
git clone https://github.com/seu-usuario/seu-repo.git
cd seu-repo
```

### 2. Configure o banco de dados

Certifique-se de que o PostgreSQL está rodando (via Docker ou local) e que a string de conexão está correta no arquivo `appsettings.json`.

### 3. Rode as migrations
Com o EF Core CLI instalado, execute:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Se necessário, para reiniciar o banco:
```bash
dotnet ef database drop --force
dotnet ef database update
```

---

## 🧪 Testes

- Testes escritos com **xUnit**, **NSubstitute**, **FluentAssertions**, e **Bogus**
- Testes cobrem:
  - Regras de negócio
  - Comandos
  - Handlers
  - Specifications
  - Validações

---

## 🛠️ Stack utilizada

- ASP.NET Core 8
- DDD + MediatR + FluentValidation
- AutoMapper
- EF Core + PostgreSQL
- Docker + Docker Compose
- Serilog para logs estruturados
- Testes: xUnit + NSubstitute + Bogus

---

## 📁 Organização do Projeto

- `Domain` → Entidades, enums, validações e regras
- `Application` → Commands, Handlers, Responses, Validators
- `WebApi` → Controllers, Middlewares, Logging
- `ORM` → Configuração EF Core (DbContext + Mapping)
- `IoC` → Injeção de dependência
- `Tests` → Separados em Unit, Integration e Functional

---

Feito com dedicação por **Henrique Fernandes** 👨‍💻🚀
