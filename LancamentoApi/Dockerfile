FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Carrefour.Desafio.WebApi/Carrefour.Desafio.WebApi.csproj", "src/Carrefour.Desafio.WebApi/"]
COPY ["src/Carrefour.Desafio.IoC/Carrefour.Desafio.IoC.csproj", "src/Carrefour.Desafio.IoC/"]
COPY ["src/Carrefour.Desafio.Domain/Carrefour.Desafio.Domain.csproj", "src/Carrefour.Desafio.Domain/"]
COPY ["src/Carrefour.Desafio.Common/Carrefour.Desafio.Common.csproj", "src/Carrefour.Desafio.Common/"]
COPY ["src/Carrefour.Desafio.Application/Carrefour.Desafio.Application.csproj", "src/Carrefour.Desafio.Application/"]
COPY ["src/Carrefour.Desafio.WebApi.ORM/Carrefour.Desafio.WebApi.ORM.csproj", "src/Carrefour.Desafio.WebApi.ORM/"]


RUN dotnet restore "./src/Carrefour.Desafio.WebApi/Carrefour.Desafio.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Carrefour.Desafio.WebApi"
RUN dotnet build "./Carrefour.Desafio.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Carrefour.Desafio.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Carrefour.Desafio.WebApi.dll"]

