FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Copiamos los archivos
COPY . .

# Solo damos permisos, no ejecutamos limpiezas pesadas aquí
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["/bin/bash", "/app/entrypoint.sh"]