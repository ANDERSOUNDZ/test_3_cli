#!/bin/bash
echo "Limpiar migraciones anteriores"
set +e 

echo "Limpiar archivos OBJ y BIN para evitar conflictos"
rm -rf /app/product_service/bin /app/product_service/obj
rm -rf /app/product_service.data/bin /app/product_service.data/obj
cd /app/product_service

dotnet restore
dotnet build --no-restore

echo "Revisando coneccion a SQL Server"
until dotnet ef database update --project ../product_service.data/product_service.data.csproj --startup-project product_service.csproj 2>/dev/null; do
  echo "SQL Server no disponible... reintentando"
  sleep 2
done

echo "Limpieza e inicio de migraciónes"
if [ ! -d "../product_service.data/migrations" ]; then
    echo "Carpeta migración no encontrada, se esta creando la carpeta y generando la migración inicial"
    dotnet ef migrations add InitialCreate \
      --project ../product_service.data/product_service.data.csproj \
      --startup-project product_service.csproj \
      --output-dir migrations
    
    echo "Agregando la migracion inicial, si la tabla ya existe en la DB, no se detiene el script"
    dotnet ef database update \
      --project ../product_service.data/product_service.data.csproj \
      --startup-project product_service.csproj || echo "Aviso: La base de datos ya tenía estructura o hubo un conflicto menor."
else
    echo "Integrando la migracion"
    dotnet ef database update \
      --project ../product_service.data/product_service.data.csproj \
      --startup-project product_service.csproj || echo "La base de datos ya está actualizada."
fi

echo "Activar modo estrcto para que el contenedor se detenga si hay errores críticos"
set -e
echo "Compilando y ejecutando el servicio de productos"
dotnet clean
dotnet build
echo "Servicio de productos levantado."
dotnet watch run --urls http://0.0.0.0:8080 --non-interactive