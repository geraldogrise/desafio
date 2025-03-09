# Base runtime para execução do serviço
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

# Fase de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Corrigindo o caminho do Worker.csproj
COPY ["Worker.csproj", "./"]
RUN dotnet restore "Worker.csproj"

COPY . .
WORKDIR "/src"
RUN dotnet build "Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase de publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final de produção
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Definição do usuário para execução segura
USER $APP_UID

ENTRYPOINT ["dotnet", "Worker.dll"]
