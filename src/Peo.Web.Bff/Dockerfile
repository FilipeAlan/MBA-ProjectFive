# Imagem base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Imagem para build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src

# Copia os csproj para restaurar dependências
COPY ["src/Peo.Web.Bff/Peo.Web.Bff.csproj", "src/Peo.Web.Bff/"]
COPY ["src/Peo.Core/Peo.Core.csproj", "src/Peo.Core/"]
COPY ["src/Peo.Core.Web/Peo.Core.Web.csproj", "src/Peo.Core.Web/"]
COPY ["src/Peo.ServiceDefaults/Peo.ServiceDefaults.csproj", "src/Peo.ServiceDefaults/"]

# Restore do BFF (puxa o resto em cascata)
RUN dotnet restore "src/Peo.Web.Bff/Peo.Web.Bff.csproj"

# Copia o resto do código
COPY . .

# Entra na pasta do projeto do BFF
WORKDIR "/src/src/Peo.Web.Bff"

RUN dotnet build "Peo.Web.Bff.csproj" -c $BUILD_CONFIGURATION

# Stage de publish (Release)
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/src/Peo.Web.Bff"
RUN dotnet publish "Peo.Web.Bff.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Peo.Web.Bff.dll"]
