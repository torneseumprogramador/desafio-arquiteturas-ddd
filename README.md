# 🛒 Ecommerce DDD - Desafio de Arquiteturas de Software

## 📚 Sobre o Projeto

Este projeto foi desenvolvido como parte do **Desafio de Arquiteturas de Software** do curso [Arquiteturas de Software Modernas](https://www.torneseumprogramador.com.br/cursos/arquiteturas_software) ministrado pelo **Prof. Danilo Aparecido** na plataforma [Torne-se um Programador](https://www.torneseumprogramador.com.br/).

### 🎯 Objetivo

Implementar um sistema de e-commerce utilizando **Domain-Driven Design (DDD)** com arquitetura em camadas, demonstrando boas práticas de desenvolvimento e organização de código.

## 🏗️ Arquitetura

O projeto segue os princípios do **Domain-Driven Design (DDD)** com uma arquitetura em camadas bem definidas:

```
┌─────────────────────────────────────┐
│              API Layer              │ ← Controllers, Program.cs
├─────────────────────────────────────┤
│           Application Layer         │ ← Services, DTOs, Interfaces
├─────────────────────────────────────┤
│             Domain Layer            │ ← Entities, Value Objects, Interfaces
├─────────────────────────────────────┤
│         Infrastructure Layer        │ ← Repositories, DbContext, Migrations
└─────────────────────────────────────┘
```

### 📁 Estrutura do Projeto

```
EcommerceDDD/
├── EcommerceDDD.API/                 # Camada de Apresentação
│   ├── Controllers/                  # Controllers da API
│   ├── Program.cs                    # Configuração da aplicação
│   └── appsettings.json             # Configurações
├── EcommerceDDD.Application/         # Camada de Aplicação
│   ├── DTOs/                        # Data Transfer Objects
│   ├── Interfaces/                   # Interfaces dos serviços
│   ├── Services/                     # Implementação dos serviços
│   └── ApplicationServiceCollectionExtensions.cs
├── EcommerceDDD.Domain/              # Camada de Domínio
│   ├── Entities/                     # Entidades de domínio
│   ├── Interfaces/                   # Interfaces dos repositórios
│   └── ValueObjects/                 # Value Objects (CPF, CNPJ)
├── EcommerceDDD.Infrastructure/      # Camada de Infraestrutura
│   ├── Data/                         # DbContext e Factory
│   ├── Repositories/                 # Implementação dos repositórios
│   ├── Migrations/                   # Migrations do Entity Framework
│   └── InfrastructureServiceCollectionExtensions.cs
├── docker-compose.yml               # Configuração do SQL Server
├── run.sh                           # Script de execução
└── README.md                        # Esta documentação
```

## 🚀 Tecnologias Utilizadas

- **.NET 9** - Framework de desenvolvimento
- **Entity Framework Core** - ORM para acesso a dados
- **SQL Server** - Banco de dados (via Docker)
- **Docker Compose** - Containerização do banco de dados
- **Swagger/OpenAPI** - Documentação da API
- **Domain-Driven Design (DDD)** - Arquitetura de domínio

## 📋 Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Git](https://git-scm.com/)

## ⚡ Como Executar

### Método Rápido (Recomendado)

```bash
# Clone o repositório
git clone <url-do-repositorio>
cd desafio-arquiteturas-ddd

# Execute o script que faz tudo automaticamente
./run.sh
```

### Método Manual

```bash
# 1. Iniciar SQL Server
docker-compose up -d

# 2. Restaurar pacotes
dotnet restore

# 3. Build da aplicação
dotnet build

# 4. Executar migrations
cd EcommerceDDD.API
dotnet ef database update
cd ..

# 5. Executar a aplicação
cd EcommerceDDD.API
dotnet run
```

### Comandos Disponíveis no Script

```bash
./run.sh              # Executa tudo (Docker + Build + Run)
./run.sh build        # Apenas dotnet build
./run.sh restore      # Apenas dotnet restore
./run.sh clean        # Apenas dotnet clean
./run.sh docker       # Apenas inicia Docker
./run.sh docker-stop  # Para containers Docker
./run.sh migrate      # Executa migrations
./run.sh run          # Apenas executa a API
./run.sh help         # Mostra ajuda
```

## 🌐 Acessando a API

Após executar o projeto, a API estará disponível em:

- **API Base**: http://localhost:5134
- **Swagger UI**: http://localhost:5134/swagger
- **Health Check**: http://localhost:5134/api/health

## 📖 Endpoints da API

### 👥 Pessoas (Person)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/api/person/individual` | Criar pessoa física |
| POST | `/api/person/corporate` | Criar pessoa jurídica |
| GET | `/api/person` | Listar todas as pessoas |
| GET | `/api/person/{id}` | Buscar pessoa por ID |
| GET | `/api/person/email/{email}` | Buscar pessoa por email |
| GET | `/api/person/individuals` | Listar pessoas físicas |
| GET | `/api/person/corporates` | Listar pessoas jurídicas |
| PUT | `/api/person/{id}/individual` | Atualizar pessoa física |
| PUT | `/api/person/{id}/corporate` | Atualizar pessoa jurídica |
| DELETE | `/api/person/{id}` | Remover pessoa |

### 📦 Produtos (Product)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/api/product` | Criar produto |
| GET | `/api/product` | Listar todos os produtos |
| GET | `/api/product/{id}` | Buscar produto por ID |
| GET | `/api/product/stock/{minStock}` | Produtos com estoque mínimo |
| PUT | `/api/product/{id}` | Atualizar produto |
| DELETE | `/api/product/{id}` | Remover produto |

### 🛒 Pedidos (Order)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/api/order` | Criar pedido |
| GET | `/api/order` | Listar todos os pedidos |
| GET | `/api/order/{id}` | Buscar pedido por ID |
| GET | `/api/order/person/{personId}` | Pedidos por pessoa |
| GET | `/api/order/status/{status}` | Pedidos por status |
| POST | `/api/order/{id}/confirm` | Confirmar pedido |
| POST | `/api/order/{id}/ship` | Enviar pedido |
| POST | `/api/order/{id}/deliver` | Entregar pedido |
| POST | `/api/order/{id}/cancel` | Cancelar pedido |
| DELETE | `/api/order/{id}` | Remover pedido |

## 🏛️ Conceitos de DDD Implementados

### 📦 Entidades de Domínio

- **Person** (Pessoa) - Entidade raiz com herança
  - **IndividualPerson** (Pessoa Física)
  - **CorporatePerson** (Pessoa Jurídica)
- **Product** (Produto)
- **Order** (Pedido)
- **OrderProduct** (Produto do Pedido)

### 💎 Value Objects

- **CPF** - Validação e formatação de CPF
- **CNPJ** - Validação e formatação de CNPJ

### 🔄 Estados do Pedido

- **Pendente** - Pedido criado
- **Confirmado** - Pedido confirmado
- **Enviado** - Pedido enviado
- **Entregue** - Pedido entregue
- **Cancelado** - Pedido cancelado

## 🧪 Exemplos de Uso

### Criar Pessoa Física

```bash
curl -X POST "http://localhost:5134/api/person/individual" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "João Silva",
    "email": "joao@email.com",
    "cpf": "12345678901"
  }'
```

### Criar Produto

```bash
curl -X POST "http://localhost:5134/api/product" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Smartphone XYZ",
    "descricao": "Smartphone de última geração",
    "preco": 1299.99,
    "estoque": 50
  }'
```

### Criar Pedido

```bash
curl -X POST "http://localhost:5134/api/order" \
  -H "Content-Type: application/json" \
  -d '{
    "pessoaId": "guid-da-pessoa",
    "produtos": [
      {
        "produtoId": "guid-do-produto",
        "quantidade": 2
      }
    ]
  }'
```

## 🔧 Configuração do Banco de Dados

O projeto utiliza **SQL Server** rodando em **Docker** com as seguintes configurações:

- **Host**: localhost
- **Porta**: 1433
- **Database**: EcommerceDDD
- **Usuário**: sa
- **Senha**: YourStrong@Passw0rd

### Connection String

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=EcommerceDDD;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;"
  }
}
```

## 📝 Migrations

Para criar uma nova migration:

```bash
cd EcommerceDDD.API
dotnet ef migrations add NomeDaMigration
```

Para aplicar migrations:

```bash
cd EcommerceDDD.API
dotnet ef database update
```

## 🎓 Aprendizados do Curso

Este projeto demonstra os seguintes conceitos aprendidos no curso:

1. **Domain-Driven Design (DDD)**
   - Entidades e Value Objects
   - Agregados e Repositórios
   - Serviços de Domínio

2. **Arquitetura em Camadas**
   - Separação de responsabilidades
   - Inversão de dependência
   - Clean Architecture

3. **Padrões de Projeto**
   - Repository Pattern
   - Unit of Work
   - Service Layer

4. **Boas Práticas**
   - SOLID Principles
   - Clean Code
   - Test-Driven Development (TDD)

## 👨‍🏫 Sobre o Professor

**Prof. Danilo Aparecido** é instrutor na plataforma [Torne-se um Programador](https://www.torneseumprogramador.com.br/), especializado em arquiteturas de software e desenvolvimento de sistemas escaláveis.

## 📚 Curso Completo

Para aprender mais sobre arquiteturas de software e aprofundar seus conhecimentos, acesse o curso completo:

**[Arquiteturas de Software Modernas](https://www.torneseumprogramador.com.br/cursos/arquiteturas_software)**

## 🤝 Contribuição

Este projeto foi desenvolvido como parte de um desafio educacional. Contribuições são bem-vindas através de issues e pull requests.

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

**Desenvolvido com ❤️ para o curso de Arquiteturas de Software do [Torne-se um Programador](https://www.torneseumprogramador.com.br/)** 