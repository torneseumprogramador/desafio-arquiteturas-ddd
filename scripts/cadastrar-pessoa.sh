#!/bin/bash

# Script interativo para cadastrar pessoas na API Ecommerce DDD
# Autor: Prof. Danilo Aparecido
# Data: $(date +%Y-%m-%d)

# Configurações
API_BASE_URL="http://localhost:5134"
API_ENDPOINT_FISICA="/api/pessoas/fisica"
API_ENDPOINT_JURIDICA="/api/pessoas/juridica"

# Cores para output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
PURPLE='\033[0;35m'
NC='\033[0m' # No Color

echo -e "${BLUE}👥 Cadastro Interativo de Pessoas - Ecommerce DDD${NC}"
echo -e "${BLUE}===============================================${NC}"
echo ""

# Verificar se a API está rodando
echo -e "${BLUE}🔍 Verificando se a API está rodando...${NC}"
if curl -s "$API_BASE_URL/api/saude" > /dev/null; then
    echo -e "${GREEN}✅ API está rodando!${NC}"
    echo ""
else
    echo -e "${RED}❌ API não está rodando!${NC}"
    echo -e "${YELLOW}💡 Execute: cd EcommerceDDD.API && dotnet run${NC}"
    exit 1
fi

# Função para cadastrar pessoa física
cadastrar_pessoa_fisica() {
    local nome="$1"
    local email="$2"
    local senha="$3"
    local cpf="$4"
    
    echo -e "${YELLOW}👤 Cadastrando pessoa física: $nome${NC}"
    
    response=$(curl -s -w "\n%{http_code}" -X POST "$API_BASE_URL$API_ENDPOINT_FISICA" \
        -H "Content-Type: application/json" \
        -d "{
            \"nome\": \"$nome\",
            \"email\": \"$email\",
            \"senha\": \"$senha\",
            \"cpf\": \"$cpf\"
        }")
    
    # Separar o corpo da resposta do código HTTP
    http_code=$(echo "$response" | tail -n1)
    response_body=$(echo "$response" | sed '$d')
    
    if [ "$http_code" -eq 201 ]; then
        echo -e "${GREEN}✅ Sucesso! Pessoa física cadastrada.${NC}"
        echo -e "${GREEN}   ID: $(echo $response_body | grep -o '"id":"[^"]*"' | cut -d'"' -f4)${NC}"
    else
        echo -e "${RED}❌ Erro! Código HTTP: $http_code${NC}"
        echo -e "${RED}   Resposta: $response_body${NC}"
    fi
    echo ""
}

# Função para cadastrar pessoa jurídica
cadastrar_pessoa_juridica() {
    local nome="$1"
    local email="$2"
    local senha="$3"
    local cnpj="$4"
    
    echo -e "${YELLOW}🏢 Cadastrando pessoa jurídica: $nome${NC}"
    
    response=$(curl -s -w "\n%{http_code}" -X POST "$API_BASE_URL$API_ENDPOINT_JURIDICA" \
        -H "Content-Type: application/json" \
        -d "{
            \"nome\": \"$nome\",
            \"email\": \"$email\",
            \"senha\": \"$senha\",
            \"cnpj\": \"$cnpj\"
        }")
    
    # Separar o corpo da resposta do código HTTP
    http_code=$(echo "$response" | tail -n1)
    response_body=$(echo "$response" | sed '$d')
    
    if [ "$http_code" -eq 201 ]; then
        echo -e "${GREEN}✅ Sucesso! Pessoa jurídica cadastrada.${NC}"
        echo -e "${GREEN}   ID: $(echo $response_body | grep -o '"id":"[^"]*"' | cut -d'"' -f4)${NC}"
    else
        echo -e "${RED}❌ Erro! Código HTTP: $http_code${NC}"
        echo -e "${RED}   Resposta: $response_body${NC}"
    fi
    echo ""
}

# Função para validar email
validar_email() {
    local email="$1"
    if [[ $email =~ ^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$ ]]; then
        return 0
    else
        return 1
    fi
}

# Função para validar CPF (formato básico)
validar_formato_cpf() {
    local cpf="$1"
    if [[ $cpf =~ ^[0-9]{3}\.[0-9]{3}\.[0-9]{3}-[0-9]{2}$ ]]; then
        return 0
    else
        return 1
    fi
}

# Função para validar CNPJ (formato básico)
validar_formato_cnpj() {
    local cnpj="$1"
    if [[ $cnpj =~ ^[0-9]{2}\.[0-9]{3}\.[0-9]{3}/[0-9]{4}-[0-9]{2}$ ]]; then
        return 0
    else
        return 1
    fi
}

# Menu principal
while true; do
    echo -e "${PURPLE}📋 Menu de Cadastro${NC}"
    echo -e "${PURPLE}================${NC}"
    echo "1. Cadastrar Pessoa Física"
    echo "2. Cadastrar Pessoa Jurídica"
    echo "3. Ver pessoas cadastradas"
    echo "4. Sair"
    echo ""
    read -p "Escolha uma opção (1-4): " opcao
    
    case $opcao in
        1)
            echo ""
            echo -e "${BLUE}👤 Cadastro de Pessoa Física${NC}"
            echo -e "${BLUE}==========================${NC}"
            echo ""
            
            # Solicitar nome
            while true; do
                read -p "Nome completo: " nome
                if [ -n "$nome" ]; then
                    break
                else
                    echo -e "${RED}❌ Nome é obrigatório!${NC}"
                fi
            done
            
            # Solicitar email
            while true; do
                read -p "Email: " email
                if validar_email "$email"; then
                    break
                else
                    echo -e "${RED}❌ Email inválido! Use o formato: usuario@dominio.com${NC}"
                fi
            done
            
            # Solicitar senha
            while true; do
                read -s -p "Senha (mínimo 6 caracteres): " senha
                echo ""
                if [ ${#senha} -ge 6 ]; then
                    break
                else
                    echo -e "${RED}❌ Senha deve ter pelo menos 6 caracteres!${NC}"
                fi
            done
            
            # Solicitar CPF
            while true; do
                read -p "CPF (formato: 123.456.789-09): " cpf
                if validar_formato_cpf "$cpf"; then
                    break
                else
                    echo -e "${RED}❌ CPF inválido! Use o formato: 123.456.789-09${NC}"
                    echo -e "${YELLOW}💡 Exemplos de CPFs válidos:${NC}"
                    echo -e "${YELLOW}   • 123.456.789-09${NC}"
                    echo -e "${YELLOW}   • 987.654.321-00${NC}"
                    echo -e "${YELLOW}   • 456.789.123-45${NC}"
                fi
            done
            
            echo ""
            echo -e "${YELLOW}📝 Resumo dos dados:${NC}"
            echo -e "${YELLOW}   Nome: $nome${NC}"
            echo -e "${YELLOW}   Email: $email${NC}"
            echo -e "${YELLOW}   CPF: $cpf${NC}"
            echo ""
            
            read -p "Confirmar cadastro? (s/N): " confirmar
            if [[ $confirmar =~ ^[Ss]$ ]]; then
                cadastrar_pessoa_fisica "$nome" "$email" "$senha" "$cpf"
            else
                echo -e "${YELLOW}❌ Cadastro cancelado.${NC}"
            fi
            ;;
            
        2)
            echo ""
            echo -e "${BLUE}🏢 Cadastro de Pessoa Jurídica${NC}"
            echo -e "${BLUE}============================${NC}"
            echo ""
            
            # Solicitar nome
            while true; do
                read -p "Razão Social: " nome
                if [ -n "$nome" ]; then
                    break
                else
                    echo -e "${RED}❌ Razão Social é obrigatória!${NC}"
                fi
            done
            
            # Solicitar email
            while true; do
                read -p "Email: " email
                if validar_email "$email"; then
                    break
                else
                    echo -e "${RED}❌ Email inválido! Use o formato: usuario@dominio.com${NC}"
                fi
            done
            
            # Solicitar senha
            while true; do
                read -s -p "Senha (mínimo 6 caracteres): " senha
                echo ""
                if [ ${#senha} -ge 6 ]; then
                    break
                else
                    echo -e "${RED}❌ Senha deve ter pelo menos 6 caracteres!${NC}"
                fi
            done
            
            # Solicitar CNPJ
            while true; do
                read -p "CNPJ (formato: 12.345.678/0001-90): " cnpj
                if validar_formato_cnpj "$cnpj"; then
                    break
                else
                    echo -e "${RED}❌ CNPJ inválido! Use o formato: 12.345.678/0001-90${NC}"
                    echo -e "${YELLOW}💡 Exemplos de CNPJs válidos:${NC}"
                    echo -e "${YELLOW}   • 11.222.333/0001-81${NC}"
                    echo -e "${YELLOW}   • 22.333.444/0001-92${NC}"
                    echo -e "${YELLOW}   • 33.444.555/0001-03${NC}"
                fi
            done
            
            echo ""
            echo -e "${YELLOW}📝 Resumo dos dados:${NC}"
            echo -e "${YELLOW}   Razão Social: $nome${NC}"
            echo -e "${YELLOW}   Email: $email${NC}"
            echo -e "${YELLOW}   CNPJ: $cnpj${NC}"
            echo ""
            
            read -p "Confirmar cadastro? (s/N): " confirmar
            if [[ $confirmar =~ ^[Ss]$ ]]; then
                cadastrar_pessoa_juridica "$nome" "$email" "$senha" "$cnpj"
            else
                echo -e "${YELLOW}❌ Cadastro cancelado.${NC}"
            fi
            ;;
            
        3)
            echo ""
            echo -e "${BLUE}📊 Pessoas Cadastradas${NC}"
            echo -e "${BLUE}====================${NC}"
            echo ""
            
            echo -e "${YELLOW}👥 Todas as pessoas:${NC}"
            curl -s "$API_BASE_URL/api/pessoas" | python3 -m json.tool 2>/dev/null || echo "Nenhuma pessoa cadastrada"
            echo ""
            
            echo -e "${YELLOW}👤 Pessoas físicas:${NC}"
            curl -s "$API_BASE_URL/api/pessoas/fisicas" | python3 -m json.tool 2>/dev/null || echo "Nenhuma pessoa física cadastrada"
            echo ""
            
            echo -e "${YELLOW}🏢 Pessoas jurídicas:${NC}"
            curl -s "$API_BASE_URL/api/pessoas/juridicas" | python3 -m json.tool 2>/dev/null || echo "Nenhuma pessoa jurídica cadastrada"
            echo ""
            ;;
            
        4)
            echo ""
            echo -e "${GREEN}👋 Obrigado por usar o sistema!${NC}"
            exit 0
            ;;
            
        *)
            echo -e "${RED}❌ Opção inválida! Escolha 1, 2, 3 ou 4.${NC}"
            ;;
    esac
    
    echo ""
    read -p "Pressione ENTER para continuar..."
    echo ""
done 