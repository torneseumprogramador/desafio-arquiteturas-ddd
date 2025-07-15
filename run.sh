#!/bin/bash

# Cores para output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Função para mostrar ajuda
show_help() {
    echo -e "${BLUE}📖 Uso do script:${NC}"
    echo -e "  ${GREEN}./run.sh${NC}                    - Executa tudo (Docker + Build + Run)"
    echo -e "  ${GREEN}./run.sh build${NC}              - Apenas dotnet build"
    echo -e "  ${GREEN}./run.sh restore${NC}            - Apenas dotnet restore"
    echo -e "  ${GREEN}./run.sh clean${NC}              - Apenas dotnet clean"
    echo -e "  ${GREEN}./run.sh test${NC}               - Executa testes (se existirem)"
    echo -e "  ${GREEN}./run.sh docker${NC}             - Apenas inicia Docker (SQL Server)"
    echo -e "  ${GREEN}./run.sh docker-stop${NC}        - Para containers Docker"
    echo -e "  ${GREEN}./run.sh migrate${NC}            - Executa migrations"
    echo -e "  ${GREEN}./run.sh run${NC}                - Apenas executa a API"
    echo -e "  ${GREEN}./run.sh help${NC}               - Mostra esta ajuda"
    echo ""
    echo -e "${YELLOW}💡 Exemplos:${NC}"
    echo -e "  ${GREEN}./run.sh build${NC}              - Para fazer apenas o build"
    echo -e "  ${GREEN}./run.sh docker && ./run.sh build${NC} - Inicia Docker e depois faz build"
}

# Verificar se o .NET 9 está instalado
check_dotnet() {
    if ! dotnet --version > /dev/null 2>&1; then
        echo -e "${RED}❌ .NET 9 não está instalado. Por favor, instale o .NET 9 e tente novamente.${NC}"
        exit 1
    fi
}

# Verificar se o Docker está rodando
check_docker() {
    if ! docker info > /dev/null 2>&1; then
        echo -e "${RED}❌ Docker não está rodando. Por favor, inicie o Docker e tente novamente.${NC}"
        exit 1
    fi
}

# Função para iniciar Docker
start_docker() {
    echo -e "${YELLOW}🐳 Iniciando SQL Server...${NC}"
    docker-compose up -d
    
    echo -e "${YELLOW}⏳ Aguardando SQL Server estar pronto...${NC}"
    sleep 10
    
    echo -e "${YELLOW}🔍 Verificando conexão com SQL Server...${NC}"
    for i in {1..30}; do
        if docker exec ecommerce-sql-server /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "YourStrong@Passw0rd" -Q "SELECT 1" > /dev/null 2>&1; then
            echo -e "${GREEN}✅ SQL Server está pronto!${NC}"
            return 0
        fi
        if [ $i -eq 30 ]; then
            echo -e "${RED}❌ Timeout aguardando SQL Server${NC}"
            return 1
        fi
        echo -e "${YELLOW}⏳ Tentativa $i/30...${NC}"
        sleep 2
    done
}

# Função para executar migrations
run_migrations() {
    echo -e "${YELLOW}🗄️ Executando migrations...${NC}"
    cd EcommerceDDD.API
    dotnet ef database update
    cd ..
}

# Função para executar a API
run_api() {
    echo -e "${GREEN}🎯 Iniciando a API...${NC}"
    echo -e "${BLUE}📱 A API estará disponível em: http://localhost:5134${NC}"
    echo -e "${BLUE}📚 Swagger estará disponível em: http://localhost:5134/swagger${NC}"
    echo -e "${YELLOW}⏹️ Pressione Ctrl+C para parar${NC}"
    
    cd EcommerceDDD.API
    dotnet run
}

# Verificar argumentos
case "${1:-}" in
    "build")
        echo -e "${BLUE}🔨 Executando dotnet build...${NC}"
        check_dotnet
        dotnet build
        ;;
    "restore")
        echo -e "${BLUE}📦 Executando dotnet restore...${NC}"
        check_dotnet
        dotnet restore
        ;;
    "clean")
        echo -e "${BLUE}🧹 Executando dotnet clean...${NC}"
        check_dotnet
        dotnet clean
        ;;
    "test")
        echo -e "${BLUE}🧪 Executando testes...${NC}"
        check_dotnet
        dotnet test
        ;;
    "docker")
        echo -e "${BLUE}🐳 Iniciando Docker...${NC}"
        check_docker
        docker-compose down
        start_docker
        ;;
    "docker-stop")
        echo -e "${BLUE}🛑 Parando Docker...${NC}"
        docker-compose down
        ;;
    "migrate")
        echo -e "${BLUE}🗄️ Executando migrations...${NC}"
        check_dotnet
        run_migrations
        ;;
    "run")
        echo -e "${BLUE}🎯 Executando API...${NC}"
        check_dotnet
        run_api
        ;;
    "help"|"-h"|"--help")
        show_help
        ;;
    "")
        # Execução completa (comportamento padrão)
        echo -e "${BLUE}🚀 Iniciando Ecommerce DDD...${NC}"
        
        check_docker
        check_dotnet
        echo -e "${GREEN}✅ Docker e .NET 9 verificados${NC}"
        
        # Parar containers existentes
        echo -e "${YELLOW}🛑 Parando containers existentes...${NC}"
        docker-compose down
        
        # Iniciar Docker
        if ! start_docker; then
            exit 1
        fi
        
        # Restaurar pacotes NuGet
        echo -e "${YELLOW}📦 Restaurando pacotes NuGet...${NC}"
        dotnet restore
        
        # Build da aplicação
        echo -e "${YELLOW}🔨 Fazendo build da aplicação...${NC}"
        if ! dotnet build; then
            echo -e "${RED}❌ Build falhou${NC}"
            exit 1
        fi
        
        echo -e "${GREEN}✅ Build concluído com sucesso!${NC}"
        
        # Executar migrations
        run_migrations
        
        # Executar a aplicação
        run_api
        ;;
    *)
        echo -e "${RED}❌ Comando desconhecido: $1${NC}"
        echo ""
        show_help
        exit 1
        ;;
esac 