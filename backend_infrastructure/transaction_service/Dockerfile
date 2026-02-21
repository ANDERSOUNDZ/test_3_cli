FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

COPY . .

# Limpieza y restauración forzada dentro del contenedor
RUN find . -type d -name "obj" -exec rm -rf {} + && \
    find . -type d -name "bin" -exec rm -rf {} + && \
    dotnet restore "transaction_service/transaction_service.csproj"

WORKDIR /app/transaction_service

# Ejecutamos con limpieza previa en el comando de inicio por seguridad
ENTRYPOINT ["sh", "-c", "dotnet restore && dotnet watch run --urls http://0.0.0.0:8080 --non-interactive"]