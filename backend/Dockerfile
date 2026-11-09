FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Kioskitos.sln ./
COPY src/Kioskitos.API/Kioskitos.API.csproj src/Kioskitos.API/
COPY src/Kioskitos.Application/Kioskitos.Application.csproj src/Kioskitos.Application/
COPY src/Kioskitos.Domain/Kioskitos.Domain.csproj src/Kioskitos.Domain/
COPY src/Kioskitos.Infrastructure/Kioskitos.Infrastructure.csproj src/Kioskitos.Infrastructure/
RUN dotnet restore src/Kioskitos.API/Kioskitos.API.csproj

COPY src/ src/
RUN dotnet publish src/Kioskitos.API/Kioskitos.API.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

RUN apt-get update \
    && apt-get upgrade -y \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Kioskitos.API.dll"]
