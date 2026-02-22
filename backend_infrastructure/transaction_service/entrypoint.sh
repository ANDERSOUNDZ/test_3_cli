#!/bin/bash
# Desactivamos set -e para que los reintentos de migración no maten el contenedor
set +e 

# 1. Limpieza rápida de carpetas de compilación
rm -rf /app/transaction_service/bin /app/transaction_service/obj
rm -rf /app/transaction_service.data/bin /app/transaction_service.data/obj

cd /app/transaction_service

echo "--- [TRANSACTION] RESTAURANDO Y COMPILANDO ---"
dotnet restore
dotnet build --no-restore

echo "--- [TRANSACTION] ESPERANDO A SQL SERVER ---"
# Bucle de espera hasta que la DB responda a una actualización de EF
until dotnet ef database update --project ../transaction_service.data/transaction_service.data.csproj --startup-project transaction_service.csproj 2>/dev/null; do
  echo "SQL Server (Transactions) no disponible... reintentando"
  sleep 2
done

# 2. Lógica de Migraciones
if [ ! -d "../transaction_service.data/migrations" ]; then
    echo "--- [TRANSACTION] GENERANDO MIGRACIÓN INICIAL ---"
    dotnet ef migrations add InitialCreate \
      --project ../transaction_service.data/transaction_service.data.csproj \
      --startup-project transaction_service.csproj \
      --output-dir migrations
    
    echo "--- [TRANSACTION] APLICANDO MIGRACIÓN ---"
    dotnet ef database update \
      --project ../transaction_service.data/transaction_service.data.csproj \
      --startup-project transaction_service.csproj || echo "Aviso: Conflicto menor en DB de transacciones."
else
    echo "--- [TRANSACTION] MIGRACIONES DETECTADAS: SINCRONIZANDO ---"
    dotnet ef database update \
      --project ../transaction_service.data/transaction_service.data.csproj \
      --startup-project transaction_service.csproj || echo "La DB de transacciones ya está al día."
fi

set -e
echo "--- TRANSACTION SERVICE LISTO ---"
dotnet watch run --urls http://0.0.0.0:8080 --non-interactive