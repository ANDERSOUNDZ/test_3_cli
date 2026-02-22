#!/bin/bash
echo "Limpiar migraciones anteriores"
set +e 

echo "Limpiar archivos OBJ y BIN para evitar conflictos"
rm -rf /app/transaction_service/bin /app/transaction_service/obj
rm -rf /app/transaction_service.data/bin /app/transaction_service.data/obj
cd /app/transaction_service

dotnet restore
dotnet build --no-restore

echo "Revisando coneccion a SQL Server"
until dotnet ef database update --project ../transaction_service.data/transaction_service.data.csproj --startup-project transaction_service.csproj 2>/dev/null; do
  echo "SQL Server (Transactions) no disponible... reintentando"
  sleep 2
done

echo "Limpieza e inicio de migraciónes"
if [ ! -d "../transaction_service.data/migrations" ]; then
    echo "--- [TRANSACTION] GENERANDO MIGRACIÓN INICIAL ---"
    dotnet ef migrations add InitialCreate \
      --project ../transaction_service.data/transaction_service.data.csproj \
      --startup-project transaction_service.csproj \
      --output-dir migrations
    
    echo "Agregando la migracion inicial, si la tabla ya existe en la DB, no se detiene el script"
    dotnet ef database update \
      --project ../transaction_service.data/transaction_service.data.csproj \
      --startup-project transaction_service.csproj || echo "Aviso: Conflicto menor en DB de transacciones."
else
    echo "Integrando la migracion"
    dotnet ef database update \
      --project ../transaction_service.data/transaction_service.data.csproj \
      --startup-project transaction_service.csproj || echo "La DB de transacciones ya está actualizada."
fi

echo "Activar modo estrcto para que el contenedor se detenga si hay errores críticos"
set -e
echo "Compilando y ejecutando el servicio de productos"
dotnet clean
dotnet build
echo "Servicio de transacciones levantado."
dotnet watch run --urls http://0.0.0.0:8080 --non-interactive