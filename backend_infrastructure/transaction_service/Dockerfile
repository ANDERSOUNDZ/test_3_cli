FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app

# 1. Copiar los archivos de proyecto con los nuevos nombres
COPY ["transaction_service.csproj", "./"]
COPY ["transaction_service.data/transaction_service.data.csproj", "transaction_service.data/"]
COPY ["transaction_service.domain/transaction_service.domain.csproj", "transaction_service.domain/"]
COPY ["transaction_service.ports/transaction_service.ports.csproj", "transaction_service.ports/"]
COPY ["transaction_service.application/transaction_service.application.csproj", "transaction_service.application/"]

RUN dotnet restore "transaction_service.csproj"

COPY . .

# 4. Ejecutar
ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:8080", "--non-interactive"]