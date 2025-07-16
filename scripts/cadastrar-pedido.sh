#!/bin/bash

# Cores para output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
PURPLE='\033[0;35m'
CYAN='\033[0;36m'
NC='\033[0m' # No Color

# URL base da API
API_URL="http://localhost:5134/api"

# Função para exibir título
show_title() {
    echo -e "${CYAN}"
    echo "╔══════════════════════════════════════════════════════════════╗"
    echo "║                    🛒 CADASTRO DE PEDIDOS                    ║"
    echo "║                    Sistema E-commerce DDD                    ║"
    echo "╚══════════════════════════════════════════════════════════════╝"
    echo -e "${NC}"
}

# Função para exibir menu
show_menu() {
    echo -e "${YELLOW}"
    echo "┌──────────────────────────────────────────────────────────────┐"
    echo "│                        MENU PRINCIPAL                        │"
    echo "├──────────────────────────────────────────────────────────────┤"
    echo "│  1. 📋 Listar pessoas cadastradas                            │"
    echo "│  2. 👤 Cadastrar nova pessoa                                 │"
    echo "│  3. 📦 Listar produtos disponíveis                           │"
    echo "│  4. 🛒 Criar novo pedido                                     │"
    echo "│  5. 📊 Visualizar pedidos existentes                         │"
    echo "│  0. 🚪 Sair                                                  │"
    echo "└──────────────────────────────────────────────────────────────┘"
    echo -e "${NC}"
}

# Função para validar se a API está rodando
check_api() {
    echo -e "${BLUE}🔍 Verificando se a API está rodando...${NC}"
    
    if curl -s "$API_URL/health" > /dev/null 2>&1; then
        echo -e "${GREEN}✅ API está rodando!${NC}"
        return 0
    else
        echo -e "${RED}❌ API não está rodando!${NC}"
        echo -e "${YELLOW}💡 Execute './run.sh' para iniciar a aplicação${NC}"
        return 1
    fi
}

# Função para listar pessoas
list_people() {
    echo -e "${BLUE}📋 Listando pessoas cadastradas...${NC}"
    
    response=$(curl -s "$API_URL/pessoas")
    
    if [ $? -eq 0 ] && [ -n "$response" ]; then
        echo -e "${GREEN}✅ Pessoas encontradas:${NC}"
        echo "$response" | jq -r '.[] | "ID: \(.id) | Nome: \(.nome) | Email: \(.email) | Tipo: \(if .cpf then "Física" else "Jurídica" end)"'
    else
        echo -e "${YELLOW}⚠️  Nenhuma pessoa cadastrada ou erro na consulta${NC}"
    fi
}

# Função para cadastrar nova pessoa
register_person() {
    echo -e "${BLUE}👤 Cadastrando nova pessoa...${NC}"
    
    echo -e "${YELLOW}Escolha o tipo de pessoa:${NC}"
    echo "1. Pessoa Física"
    echo "2. Pessoa Jurídica"
    read -p "Opção: " person_type
    
    case $person_type in
        1)
            register_individual_person
            ;;
        2)
            register_corporate_person
            ;;
        *)
            echo -e "${RED}❌ Opção inválida!${NC}"
            return 1
            ;;
    esac
}

# Função para cadastrar pessoa física
register_individual_person() {
    echo -e "${BLUE}👤 Cadastrando pessoa física...${NC}"
    
    read -p "Nome completo: " name
    read -p "Email: " email
    read -p "CPF (apenas números): " cpf
    
    # Validações básicas
    if [ -z "$name" ] || [ -z "$email" ] || [ -z "$cpf" ]; then
        echo -e "${RED}❌ Todos os campos são obrigatórios!${NC}"
        return 1
    fi
    
    if [ ${#cpf} -ne 11 ]; then
        echo -e "${RED}❌ CPF deve ter 11 dígitos!${NC}"
        return 1
    fi
    
    # Criar JSON
    json_data="{
        \"nome\": \"$name\",
        \"email\": \"$email\",
        \"cpf\": \"$cpf\"
    }"
    
    # Enviar requisição
    response=$(curl -s -X POST "$API_URL/pessoas/fisica" \
        -H "Content-Type: application/json" \
        -d "$json_data")
    
    if echo "$response" | jq -e '.id' > /dev/null 2>&1; then
        person_id=$(echo "$response" | jq -r '.id')
        echo -e "${GREEN}✅ Pessoa física cadastrada com sucesso!${NC}"
        echo -e "${CYAN}ID: $person_id${NC}"
        return 0
    else
        error_msg=$(echo "$response" | jq -r '.message // "Erro desconhecido"')
        echo -e "${RED}❌ Erro ao cadastrar: $error_msg${NC}"
        return 1
    fi
}

# Função para cadastrar pessoa jurídica
register_corporate_person() {
    echo -e "${BLUE}🏢 Cadastrando pessoa jurídica...${NC}"
    
    read -p "Razão social: " name
    read -p "Email: " email
    read -p "CNPJ (apenas números): " cnpj
    
    # Validações básicas
    if [ -z "$name" ] || [ -z "$email" ] || [ -z "$cnpj" ]; then
        echo -e "${RED}❌ Todos os campos são obrigatórios!${NC}"
        return 1
    fi
    
    if [ ${#cnpj} -ne 14 ]; then
        echo -e "${RED}❌ CNPJ deve ter 14 dígitos!${NC}"
        return 1
    fi
    
    # Criar JSON
    json_data="{
        \"nome\": \"$name\",
        \"email\": \"$email\",
        \"cnpj\": \"$cnpj\"
    }"
    
    # Enviar requisição
    response=$(curl -s -X POST "$API_URL/pessoas/juridica" \
        -H "Content-Type: application/json" \
        -d "$json_data")
    
    if echo "$response" | jq -e '.id' > /dev/null 2>&1; then
        person_id=$(echo "$response" | jq -r '.id')
        echo -e "${GREEN}✅ Pessoa jurídica cadastrada com sucesso!${NC}"
        echo -e "${CYAN}ID: $person_id${NC}"
        return 0
    else
        error_msg=$(echo "$response" | jq -r '.message // "Erro desconhecido"')
        echo -e "${RED}❌ Erro ao cadastrar: $error_msg${NC}"
        return 1
    fi
}

# Função para listar produtos
list_products() {
    echo -e "${BLUE}📦 Listando produtos disponíveis...${NC}"
    
    response=$(curl -s "$API_URL/produtos")
    
    if [ $? -eq 0 ] && [ -n "$response" ]; then
        echo -e "${GREEN}✅ Produtos encontrados:${NC}"
        echo "$response" | jq -r '.[] | "ID: \(.id) | Nome: \(.nome) | Preço: R$ \(.preco) | Estoque: \(.estoque)"'
    else
        echo -e "${YELLOW}⚠️  Nenhum produto cadastrado ou erro na consulta${NC}"
    fi
}

# Função para selecionar pessoa
select_person() {
    echo -e "${BLUE}👤 Selecionando pessoa para o pedido...${NC}"
    
    # Listar pessoas
    response=$(curl -s "$API_URL/pessoas")
    
    if [ $? -ne 0 ] || [ -z "$response" ]; then
        echo -e "${YELLOW}⚠️  Nenhuma pessoa cadastrada. Vamos cadastrar uma nova!${NC}"
        register_person
        if [ $? -eq 0 ]; then
            # Buscar a pessoa recém cadastrada
            response=$(curl -s "$API_URL/pessoas")
        else
            return 1
        fi
    fi
    
    # Mostrar pessoas disponíveis
    echo -e "${GREEN}📋 Pessoas disponíveis:${NC}"
    echo "$response" | jq -r '.[] | "\(.id) | \(.nome) | \(.email)"'
    
    echo -e "${YELLOW}Opções:${NC}"
    echo "1. Selecionar pessoa existente"
    echo "2. Cadastrar nova pessoa"
    read -p "Escolha: " option
    
    case $option in
        1)
            read -p "Digite o ID da pessoa: " person_id
            # Validar se o ID existe
            if echo "$response" | jq -e ".[] | select(.id == \"$person_id\")" > /dev/null 2>&1; then
                echo -e "${GREEN}✅ Pessoa selecionada!${NC}"
                return 0
            else
                echo -e "${RED}❌ ID inválido!${NC}"
                return 1
            fi
            ;;
        2)
            register_person
            if [ $? -eq 0 ]; then
                # Buscar a pessoa recém cadastrada
                response=$(curl -s "$API_URL/pessoas")
                person_id=$(echo "$response" | jq -r '.[-1].id')
                echo -e "${GREEN}✅ Nova pessoa selecionada!${NC}"
                return 0
            else
                return 1
            fi
            ;;
        *)
            echo -e "${RED}❌ Opção inválida!${NC}"
            return 1
            ;;
    esac
}

# Função para selecionar produtos
select_products() {
    echo -e "${BLUE}📦 Selecionando produtos para o pedido...${NC}"
    
    # Listar produtos
    response=$(curl -s "$API_URL/produtos")
    
    if [ $? -ne 0 ] || [ -z "$response" ]; then
        echo -e "${RED}❌ Nenhum produto cadastrado!${NC}"
        echo -e "${YELLOW}💡 Execute './scripts/cadastrar-notebooks.sh' para cadastrar produtos${NC}"
        return 1
    fi
    
    # Mostrar produtos disponíveis
    echo -e "${GREEN}📦 Produtos disponíveis:${NC}"
    echo "$response" | jq -r '.[] | "\(.id) | \(.nome) | R$ \(.preco) | Estoque: \(.estoque)"'
    
    products_json="[]"
    
    while true; do
        echo -e "${YELLOW}Adicionar produto ao pedido:${NC}"
        read -p "Digite o ID do produto (ou '0' para finalizar): " product_id
        
        if [ "$product_id" = "0" ]; then
            break
        fi
        
        # Validar se o produto existe
        if echo "$response" | jq -e ".[] | select(.id == \"$product_id\")" > /dev/null 2>&1; then
            read -p "Quantidade: " quantity
            
            if [[ "$quantity" =~ ^[0-9]+$ ]] && [ "$quantity" -gt 0 ]; then
                # Verificar estoque
                stock=$(echo "$response" | jq -r ".[] | select(.id == \"$product_id\") | .estoque")
                if [ "$quantity" -le "$stock" ]; then
                    # Adicionar produto ao JSON
                    product_json="{\"id_produto\": \"$product_id\", \"quantidade\": $quantity}"
                    products_json=$(echo "$products_json" | jq ". += [$product_json]")
                    echo -e "${GREEN}✅ Produto adicionado!${NC}"
                else
                    echo -e "${RED}❌ Quantidade excede o estoque disponível ($stock)${NC}"
                fi
            else
                echo -e "${RED}❌ Quantidade inválida!${NC}"
            fi
        else
            echo -e "${RED}❌ ID de produto inválido!${NC}"
        fi
    done
    
    # Verificar se há produtos no pedido
    product_count=$(echo "$products_json" | jq 'length')
    if [ "$product_count" -eq 0 ]; then
        echo -e "${RED}❌ Pedido deve ter pelo menos um produto!${NC}"
        return 1
    fi
    
    echo -e "${GREEN}✅ Produtos selecionados: $product_count produto(s)${NC}"
    return 0
}

# Função para criar pedido
create_order() {
    echo -e "${BLUE}🛒 Criando novo pedido...${NC}"
    
    # Selecionar pessoa
    select_person
    if [ $? -ne 0 ]; then
        return 1
    fi
    
    # Selecionar produtos
    select_products
    if [ $? -ne 0 ]; then
        return 1
    fi
    
    # Criar JSON do pedido
    order_json="{
        \"id_pessoa\": \"$person_id\",
        \"produtos\": $products_json
    }"
    
    echo -e "${YELLOW}📋 Resumo do pedido:${NC}"
    echo "$order_json" | jq '.'
    
    read -p "Confirmar pedido? (s/N): " confirm
    
    if [[ "$confirm" =~ ^[Ss]$ ]]; then
        # Enviar requisição
        response=$(curl -s -X POST "$API_URL/pedidos" \
            -H "Content-Type: application/json" \
            -d "$order_json")
        
        if echo "$response" | jq -e '.id' > /dev/null 2>&1; then
            order_id=$(echo "$response" | jq -r '.id')
            total_amount=$(echo "$response" | jq -r '.valor_total')
            echo -e "${GREEN}✅ Pedido criado com sucesso!${NC}"
            echo -e "${CYAN}ID do Pedido: $order_id${NC}"
            echo -e "${CYAN}Valor Total: R$ $total_amount${NC}"
            
            # Mostrar detalhes do pedido
            echo -e "${BLUE}📋 Detalhes do pedido:${NC}"
            echo "$response" | jq -r '.'
        else
            error_msg=$(echo "$response" | jq -r '.message // "Erro desconhecido"')
            echo -e "${RED}❌ Erro ao criar pedido: $error_msg${NC}"
            return 1
        fi
    else
        echo -e "${YELLOW}❌ Pedido cancelado${NC}"
        return 1
    fi
}

# Função para visualizar pedidos
view_orders() {
    echo -e "${BLUE}📊 Visualizando pedidos existentes...${NC}"
    
    response=$(curl -s "$API_URL/pedidos")
    
    if [ $? -eq 0 ] && [ -n "$response" ]; then
        echo -e "${GREEN}✅ Pedidos encontrados:${NC}"
        echo "$response" | jq -r '.[] | "ID: \(.id) | Pessoa: \(.nome_pessoa) | Status: \(.status) | Total: R$ \(.valor_total) | Data: \(.data_criacao)"'
    else
        echo -e "${YELLOW}⚠️  Nenhum pedido encontrado${NC}"
    fi
}

# Função principal
main() {
    show_title
    
    # Verificar se a API está rodando
    if ! check_api; then
        exit 1
    fi
    
    while true; do
        echo
        show_menu
        read -p "Escolha uma opção: " choice
        
        case $choice in
            1)
                list_people
                ;;
            2)
                register_person
                ;;
            3)
                list_products
                ;;
            4)
                create_order
                ;;
            5)
                view_orders
                ;;
            0)
                echo -e "${GREEN}👋 Obrigado por usar o sistema de pedidos!${NC}"
                exit 0
                ;;
            *)
                echo -e "${RED}❌ Opção inválida!${NC}"
                ;;
        esac
        
        echo
        read -p "Pressione ENTER para continuar..."
    done
}

# Executar função principal
main 