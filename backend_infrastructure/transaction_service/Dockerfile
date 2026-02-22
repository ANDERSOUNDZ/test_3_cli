FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Copiamos todo el contexto (incluyendo la capa .data y el servicio)
COPY . .

# Damos permisos al script
RUN chmod +x ./entrypoint.sh

# El ENTRYPOINT ahora llama al script de automatización
ENTRYPOINT ["/bin/bash", "/app/entrypoint.sh"]