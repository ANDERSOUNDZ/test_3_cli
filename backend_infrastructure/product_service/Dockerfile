FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app

COPY ["product_service.csproj", "./"]
COPY ["product_service.data/product_service.data.csproj", "product_service.data/"]
COPY ["product_service.domain/product_service.domain.csproj", "product_service.domain/"]
COPY ["product_service.ports/product_service.ports.csproj", "product_service.ports/"]
COPY ["product_service.application/product_service.application.csproj", "product_service.application/"]

RUN dotnet restore "product_service.csproj"

COPY . .

ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://0.0.0.0:8080", "--non-interactive"]