# 📁 Scripts de Carga - Ecommerce DDD

Esta pasta contém scripts para facilitar o teste e uso da API do sistema de e-commerce.

## 🚀 Scripts Disponíveis

### 📦 `cadastrar-notebooks.sh`
Cadastra automaticamente 10 notebooks de diferentes marcas no sistema.

**Execução:**
```bash
./scripts/cadastrar-notebooks.sh
```

**Produtos incluídos:**
- MacBook Pro 13" M2
- MacBook Pro 16" M2 Pro
- Dell XPS 13
- Dell Inspiron 15
- Lenovo ThinkPad X1 Carbon
- Lenovo IdeaPad 3
- HP Pavilion 15
- HP EliteBook 840
- ASUS ROG Strix G15
- ASUS ZenBook 14

**Total:** 188 unidades em estoque, preços de R$ 3.999,99 a R$ 18.999,99

### 👤 `cadastrar-pessoa.sh`
Script interativo para cadastrar pessoas físicas e jurídicas.

**Execução:**
```bash
./scripts/cadastrar-pessoa.sh
```

**Funcionalidades:**
- Menu interativo com opções
- Cadastro de pessoa física (nome, email, CPF)
- Cadastro de pessoa jurídica (razão social, email, CNPJ)
- Validações básicas de formato
- Confirmação antes de salvar
- Visualização dos dados cadastrados

### 🛒 `cadastrar-pedido.sh`
Script interativo completo para criar pedidos no sistema.

**Execução:**
```bash
./scripts/cadastrar-pedido.sh
```

**Funcionalidades:**
- Menu principal com múltiplas opções
- Listagem de pessoas cadastradas
- Cadastro de novas pessoas (física/jurídica)
- Listagem de produtos disponíveis
- Criação de pedidos com múltiplos produtos
- Visualização de pedidos existentes
- Validações de estoque
- Confirmação antes de finalizar

**Menu Principal:**
1. 📋 Listar pessoas cadastradas
2. 👤 Cadastrar nova pessoa
3. 📦 Listar produtos disponíveis
4. 🛒 Criar novo pedido
5. 📊 Visualizar pedidos existentes
0. 🚪 Sair

**Fluxo de Criação de Pedido:**
1. Seleção de pessoa (existente ou nova)
2. Seleção de produtos da lista
3. Definição de quantidades
4. Validação de estoque
5. Confirmação do pedido
6. Criação e exibição dos detalhes

## 📋 Pré-requisitos

### 🔧 Dependências
- **curl** - Para requisições HTTP
- **jq** - Para processamento de JSON
- **bash** - Shell script

### 🚀 API Rodando
Certifique-se de que a API está rodando:
```bash
./run.sh
```

### 📦 Produtos Cadastrados
Para usar o script de pedidos, é recomendado ter produtos cadastrados:
```bash
./scripts/cadastrar-notebooks.sh
```

## 🎯 Casos de Uso

### 🆕 Primeira Execução
1. Iniciar a API: `./run.sh`
2. Cadastrar produtos: `./scripts/cadastrar-notebooks.sh`
3. Cadastrar pessoas: `./scripts/cadastrar-pessoa.sh`
4. Criar pedidos: `./scripts/cadastrar-pedido.sh`

### 🛒 Fluxo Completo de Pedido
1. Execute: `./scripts/cadastrar-pedido.sh`
2. Escolha opção 4: "Criar novo pedido"
3. Selecione uma pessoa existente ou cadastre uma nova
4. Escolha produtos da lista e defina quantidades
5. Confirme o pedido
6. Visualize os detalhes criados

### 📊 Monitoramento
- Use a opção 5 para visualizar todos os pedidos
- Use a opção 1 para ver pessoas cadastradas
- Use a opção 3 para ver produtos disponíveis

## 🛡️ Validações Implementadas

### 👤 Pessoas
- Campos obrigatórios
- Formato de CPF (11 dígitos)
- Formato de CNPJ (14 dígitos)
- Validação de documentos pela API

### 📦 Produtos
- Verificação de estoque disponível
- Quantidade mínima de 1
- Validação de IDs existentes

### 🛒 Pedidos
- Pelo menos um produto
- Pessoa válida
- Estoque suficiente
- Confirmação antes de finalizar

## 🎨 Interface

Todos os scripts possuem:
- **Cores** para melhor visualização
- **Emojis** para identificação rápida
- **Mensagens claras** de sucesso/erro
- **Validações** em tempo real
- **Confirmações** antes de ações importantes

## 🔧 Personalização

### URLs da API
Os scripts usam a URL padrão: `http://localhost:5134/api`

Para alterar, edite a variável `API_URL` no início de cada script.

### Validações
As validações básicas estão nos scripts. Validações mais complexas (CPF/CNPJ) são feitas pela API.

## 📝 Logs

Os scripts exibem:
- Status das operações
- IDs dos recursos criados
- Mensagens de erro detalhadas
- Resumos das operações

## 🚨 Troubleshooting

### API não responde
```bash
./run.sh
```

### Erro de permissão
```bash
chmod +x scripts/*.sh
```

### Erro de dependência
```bash
# macOS
brew install jq

# Ubuntu/Debian
sudo apt-get install jq

# CentOS/RHEL
sudo yum install jq
```

---

**Desenvolvido para facilitar os testes do sistema Ecommerce DDD** 🛒 