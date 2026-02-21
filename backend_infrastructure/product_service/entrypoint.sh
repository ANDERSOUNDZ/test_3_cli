#!/bin/bash
# Eliminamos 'set -e' para que un error en las migraciones no detenga todo el contenedor
set +e 

# 1. Limpieza rápida (rutas directas)
rm -rf /app/product_service/bin /app/product_service/obj
rm -rf /app/product_service.data/bin /app/product_service.data/obj

cd /app/product_service

echo "--- RESTAURANDO Y COMPILANDO ---"
dotnet restore
dotnet build --no-restore

echo "--- ESPERANDO A SQL SERVER ---"
# Bucle de espera simple
until dotnet ef database update --project ../product_service.data/product_service.data.csproj --startup-project product_service.csproj 2>/dev/null; do
  echo "SQL Server no disponible... reintentando"
  sleep 2
done

# 2. CONDICIONAL INTELIGENTE
# Solo borramos y generamos si la carpeta NO existe físicamente.
# Si ya existe, asumimos que ya hay una estructura.
if [ ! -d "../product_service.data/migrations" ]; then
    echo "--- CARPETA MIGRATIONS NO ENCONTRADA: GENERANDO ---"
    dotnet ef migrations add InitialCreate \
      --project ../product_service.data/product_service.data.csproj \
      --startup-project product_service.csproj \
      --output-dir migrations
    
    echo "--- INTENTANDO APLICAR MIGRACIÓN ---"
    # Usamos || true para que si la tabla ya existe en la DB, no detenga el script
    dotnet ef database update \
      --project ../product_service.data/product_service.data.csproj \
      --startup-project product_service.csproj || echo "Aviso: La base de datos ya tenía estructura o hubo un conflicto menor."
else
    echo "--- MIGRACIONES DETECTADAS: SINCRONIZANDO ---"
    dotnet ef database update \
      --project ../product_service.data/product_service.data.csproj \
      --startup-project product_service.csproj || echo "La base de datos ya está actualizada."
fi

# Volvemos a activar el modo estricto para la ejecución de la app
set -e

echo "--- PRODUCT SERVICE LISTO ---"
dotnet watch run --urls http://0.0.0.0:8080 --non-interactive