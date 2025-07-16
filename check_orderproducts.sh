#!/bin/bash

# Cores para output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
CYAN='\033[0;36m'
NC='\033[0m' # No Color

echo -e "${CYAN}🔍 Verificando tabelas no banco de dados EcommerceDDD...${NC}"
echo

# Verificar se o banco existe
echo -e "${BLUE}1. 📊 Verificando bancos de dados:${NC}"
docker exec sqlserver2022 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "YourStrong!Passw0rd" -C -Q "SELECT name FROM sys.databases" 2>&1

echo -e "\n${BLUE}2. 📋 Verificando tabelas no banco EcommerceDDD:${NC}"
docker exec sqlserver2022 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "YourStrong!Passw0rd" -d EcommerceDDD -C -Q "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'" 2>&1

echo -e "\n${BLUE}3. 👥 Verificando pessoas cadastradas:${NC}"
docker exec sqlserver2022 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "YourStrong!Passw0rd" -d EcommerceDDD -C -Q "SELECT COUNT(*) as TotalPessoas FROM Users" 2>&1

echo -e "\n${BLUE}4. 📦 Verificando produtos cadastrados:${NC}"
docker exec sqlserver2022 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "YourStrong!Passw0rd" -d EcommerceDDD -C -Q "SELECT COUNT(*) as TotalProdutos FROM Products" 2>&1

echo -e "\n${BLUE}5. 🛒 Verificando pedidos criados:${NC}"
docker exec sqlserver2022 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "YourStrong!Passw0rd" -d EcommerceDDD -C -Q "SELECT COUNT(*) as TotalPedidos FROM Orders" 2>&1

echo -e "\n${BLUE}6. 📊 Verificando dados da tabela OrderProducts:${NC}"
docker exec sqlserver2022 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "YourStrong!Passw0rd" -d EcommerceDDD -C -Q "SELECT * FROM OrderProducts" 2>&1

echo -e "\n${BLUE}7. 💰 Resumo financeiro dos pedidos:${NC}"
docker exec sqlserver2022 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "YourStrong!Passw0rd" -d EcommerceDDD -C -Q "SELECT SUM(TotalPrice) as ValorTotalPedidos FROM OrderProducts" 2>&1

echo -e "\n${GREEN}✅ Verificação concluída!${NC}" 