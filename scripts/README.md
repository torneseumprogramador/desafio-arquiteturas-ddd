# 📁 Scripts de Carga - Ecommerce DDD

Esta pasta contém scripts para popular o banco de dados com dados de teste.

## 🚀 Scripts Disponíveis

### 1. `cadastrar-notebooks.sh`
Cadastra uma lista de notebooks pré-definidos:
- 10 notebooks de diferentes marcas
- Preços variando de R$ 3.999,99 a R$ 18.999,99
- Total de 188 unidades em estoque

```bash
./scripts/cadastrar-notebooks.sh
```

### 2. `cadastrar-pessoa.sh` (Interativo)
Script interativo para cadastrar pessoas:
- Menu com opções para pessoa física ou jurídica
- Validação de dados em tempo real
- Confirmação antes do cadastro
- Visualização de pessoas cadastradas

```bash
./scripts/cadastrar-pessoa.sh
```

## 📋 Pré-requisitos

1. **API rodando**: A API deve estar executando em `http://localhost:5134`
2. **Banco de dados**: SQL Server deve estar rodando via Docker
3. **Migrations**: As migrations devem estar aplicadas

## 🔧 Como Executar

### Método 1: Cadastro de Notebooks
```bash
# 1. Iniciar a API
cd EcommerceDDD.API
dotnet run

# 2. Em outro terminal, cadastrar notebooks
./scripts/cadastrar-notebooks.sh
```

### Método 2: Cadastro Interativo de Pessoas
```bash
# 1. Iniciar a API
cd EcommerceDDD.API
dotnet run

# 2. Em outro terminal, cadastrar pessoas
./scripts/cadastrar-pessoa.sh
```

## 📦 Notebooks Cadastrados

1. **MacBook Pro 14" M3 Pro** - R$ 15.999,99
2. **Dell XPS 13 Plus** - R$ 8.999,99
3. **Lenovo ThinkPad X1 Carbon** - R$ 12.499,99
4. **HP Spectre x360 14** - R$ 10.999,99
5. **ASUS ROG Zephyrus G14** - R$ 13.999,99
6. **Acer Swift 3** - R$ 3.999,99
7. **Microsoft Surface Laptop 5** - R$ 9.499,99
8. **Razer Blade 15** - R$ 18.999,99
9. **MSI Creator Z16** - R$ 16.999,99
10. **Samsung Galaxy Book3 Pro** - R$ 7.999,99

## 👥 Cadastro de Pessoas

O script interativo oferece:

### 📋 Menu Principal
- **Opção 1**: Cadastrar Pessoa Física
- **Opção 2**: Cadastrar Pessoa Jurídica
- **Opção 3**: Ver pessoas cadastradas
- **Opção 4**: Sair

### ✅ Validações Incluídas
- **Nome/Razão Social**: Campo obrigatório
- **Email**: Formato válido (usuario@dominio.com)
- **Senha**: Mínimo 6 caracteres
- **CPF**: Formato 123.456.789-09
- **CNPJ**: Formato 12.345.678/0001-90

### 💡 Exemplos de Documentos Válidos
**CPFs:**
- 123.456.789-09
- 987.654.321-00
- 456.789.123-45

**CNPJs:**
- 11.222.333/0001-81
- 22.333.444/0001-92
- 33.444.555/0001-03

## 🔍 Verificação

Após executar os scripts, você pode verificar os dados:

```bash
# Verificar todos os produtos
curl -X GET "http://localhost:5134/api/produtos"

# Verificar produtos com estoque mínimo
curl -X GET "http://localhost:5134/api/produtos/estoque/10"

# Verificar todas as pessoas
curl -X GET "http://localhost:5134/api/pessoas"

# Verificar pessoas físicas
curl -X GET "http://localhost:5134/api/pessoas/fisicas"

# Verificar pessoas jurídicas
curl -X GET "http://localhost:5134/api/pessoas/juridicas"
```

## 🛠️ Características dos Scripts

- ✅ **Verificação de API**: Verifica se a API está rodando antes de executar
- ✅ **Tratamento de erros**: Exibe mensagens claras em caso de erro
- ✅ **Cores no terminal**: Interface colorida para melhor visualização
- ✅ **Códigos HTTP**: Verifica códigos de resposta HTTP
- ✅ **IDs retornados**: Exibe os IDs dos registros criados
- ✅ **Validação de dados**: Validação em tempo real dos campos
- ✅ **Interface interativa**: Menu amigável para cadastro de pessoas

## 🎯 Próximos Passos

Após executar a carga:

1. **Acesse o Swagger UI**: http://localhost:5134/swagger
2. **Crie pedidos**: Use os produtos e pessoas cadastradas
3. **Teste os status**: Confirme, envie, entregue e cancele pedidos
4. **Explore a API**: Teste todos os endpoints disponíveis

## 📝 Notas

- Os preços dos notebooks são realistas para o mercado brasileiro
- Os emails são fictícios e seguem padrões comuns
- As senhas são simples (123456) para facilitar testes
- Os documentos (CPF/CNPJ) são válidos matematicamente

---

**Desenvolvido com ❤️ para o curso de Arquiteturas de Software** 