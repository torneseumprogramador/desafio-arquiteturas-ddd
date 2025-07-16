#!/bin/bash

# Script para cadastrar notebooks na API Ecommerce DDD
# Autor: Prof. Danilo Aparecido
# Data: $(date +%Y-%m-%d)

# Configurações
API_BASE_URL="http://localhost:5134"
API_ENDPOINT="/api/produtos"

# Cores para output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

echo -e "${BLUE}🛒 Script de Carga - Cadastro de Notebooks${NC}"
echo -e "${BLUE}============================================${NC}"
echo ""

# Função para cadastrar um produto
cadastrar_produto() {
    local nome="$1"
    local descricao="$2"
    local preco="$3"
    local estoque="$4"
    
    echo -e "${YELLOW}📦 Cadastrando: $nome${NC}"
    
    response=$(curl -s -w "\n%{http_code}" -X POST "$API_BASE_URL$API_ENDPOINT" \
        -H "Content-Type: application/json" \
        -d "{
            \"nome\": \"$nome\",
            \"descricao\": \"$descricao\",
            \"preco\": $preco,
            \"estoque\": $estoque
        }")
    
    # Separar o corpo da resposta do código HTTP
    http_code=$(echo "$response" | tail -n1)
    response_body=$(echo "$response" | head -n -1)
    
    if [ "$http_code" -eq 201 ]; then
        echo -e "${GREEN}✅ Sucesso! Produto cadastrado.${NC}"
        echo -e "${GREEN}   ID: $(echo $response_body | grep -o '"id":"[^"]*"' | cut -d'"' -f4)${NC}"
    else
        echo -e "${RED}❌ Erro! Código HTTP: $http_code${NC}"
        echo -e "${RED}   Resposta: $response_body${NC}"
    fi
    echo ""
}

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

# Lista de notebooks para cadastrar
echo -e "${BLUE}🚀 Iniciando cadastro de notebooks...${NC}"
echo ""

# Notebook 1 - MacBook Pro
cadastrar_produto \
    "MacBook Pro 14\" M3 Pro" \
    "Notebook Apple com chip M3 Pro, 14 polegadas, 16GB RAM, 512GB SSD, macOS Sonoma" \
    15999.99 \
    15

# Notebook 2 - Dell XPS
cadastrar_produto \
    "Dell XPS 13 Plus" \
    "Notebook Dell XPS 13 Plus, Intel Core i7-1360P, 16GB RAM, 512GB SSD, Windows 11 Pro" \
    8999.99 \
    25

# Notebook 3 - Lenovo ThinkPad
cadastrar_produto \
    "Lenovo ThinkPad X1 Carbon" \
    "Notebook Lenovo ThinkPad X1 Carbon, Intel Core i7-1355U, 16GB RAM, 1TB SSD, Windows 11 Pro" \
    12499.99 \
    20

# Notebook 4 - HP Spectre
cadastrar_produto \
    "HP Spectre x360 14" \
    "Notebook conversível HP Spectre x360, Intel Core i7-1355U, 16GB RAM, 512GB SSD, Windows 11" \
    10999.99 \
    18

# Notebook 5 - ASUS ROG
cadastrar_produto \
    "ASUS ROG Zephyrus G14" \
    "Notebook gamer ASUS ROG Zephyrus G14, AMD Ryzen 9 7940HS, RTX 4060, 16GB RAM, 1TB SSD" \
    13999.99 \
    12

# Notebook 6 - Acer Swift
cadastrar_produto \
    "Acer Swift 3" \
    "Notebook Acer Swift 3, Intel Core i5-1235U, 8GB RAM, 512GB SSD, Windows 11 Home" \
    3999.99 \
    30

# Notebook 7 - Microsoft Surface
cadastrar_produto \
    "Microsoft Surface Laptop 5" \
    "Notebook Microsoft Surface Laptop 5, Intel Core i7-1255U, 16GB RAM, 512GB SSD, Windows 11" \
    9499.99 \
    22

# Notebook 8 - Razer Blade
cadastrar_produto \
    "Razer Blade 15" \
    "Notebook gamer Razer Blade 15, Intel Core i7-13700H, RTX 4070, 16GB RAM, 1TB SSD" \
    18999.99 \
    8

# Notebook 9 - MSI Creator
cadastrar_produto \
    "MSI Creator Z16" \
    "Notebook para criadores MSI Creator Z16, Intel Core i7-12700H, RTX 3060, 32GB RAM, 1TB SSD" \
    16999.99 \
    10

# Notebook 10 - Samsung Galaxy Book
cadastrar_produto \
    "Samsung Galaxy Book3 Pro" \
    "Notebook Samsung Galaxy Book3 Pro, Intel Core i7-1360P, 16GB RAM, 512GB SSD, Windows 11" \
    7999.99 \
    28

echo -e "${GREEN}🎉 Cadastro de notebooks concluído!${NC}"
echo ""
echo -e "${BLUE}📊 Resumo:${NC}"
echo -e "${BLUE}   • 10 notebooks cadastrados${NC}"
echo -e "${BLUE}   • Preços variando de R$ 3.999,99 a R$ 18.999,99${NC}"
echo -e "${BLUE}   • Total de produtos: 188 unidades${NC}"
echo ""
echo -e "${YELLOW}💡 Para verificar os produtos cadastrados:${NC}"
echo -e "${YELLOW}   curl -X GET \"$API_BASE_URL$API_ENDPOINT\"${NC}"
echo ""
echo -e "${BLUE}✨ Script executado com sucesso!${NC}" 