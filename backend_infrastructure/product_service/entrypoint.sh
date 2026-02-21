#!/bin/bash
set -e

# 1. Limpieza absoluta
rm -rf /app/product_service/bin /app/product_service/obj
rm -rf /app/product_service.data/bin /app/product_service.data/obj

cd /app/product_service

echo "--- RESTAURANDO ---"
dotnet restore

echo "--- CLEAN + BUILD ---"
dotnet clean
dotnet build --no-restore

echo "--- ESPERANDO CONEXIÓN ---"
until dotnet ef database update \
  --project ../product_service.data/product_service.data.csproj \
  --startup-project product_service.csproj 2>/dev/null; do
  sleep 2
done

# 2. FORZAR GENERACIÓN
echo "--- GENERANDO MIGRACIONES DESDE CERO ---"
rm -rf ../product_service.data/migrations

dotnet ef migrations add InitialCreate \
  --project ../product_service.data/product_service.data.csproj \
  --startup-project product_service.csproj \
  --output-dir migrations

echo "--- APLICANDO A BASE DE DATOS ---"
dotnet ef database update \
  --project ../product_service.data/product_service.data.csproj \
  --startup-project product_service.csproj

echo "--- PRODUCT SERVICE LISTO Y CON MIGRACIONES ---"
dotnet watch run --urls http://0.0.0.0:8080 --non-interactive
