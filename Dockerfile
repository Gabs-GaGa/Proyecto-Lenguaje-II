# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ControlInventario/ControlInventario.csproj ControlInventario/
RUN dotnet restore ControlInventario/ControlInventario.csproj

COPY ControlInventario/ ControlInventario/
RUN dotnet publish ControlInventario/ControlInventario.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "ControlInventario.dll"]