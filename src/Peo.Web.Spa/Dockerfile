# ============================
# STAGE 1 - BUILD (dotnet publish)
# ============================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia o csproj do SPA (ajusta o caminho se na tua solução não tiver a pasta "src")
COPY ["src/Peo.Web.Spa/Peo.Web.Spa.csproj", "src/Peo.Web.Spa/"]

# Se o SPA referenciar outros projetos (ex: Peo.Core, etc.),
# você pode acrescentar mais COPY de csproj aqui, igual faz nos outros serviços.

# Restore das dependências do SPA
RUN dotnet restore "src/Peo.Web.Spa/Peo.Web.Spa.csproj"

# Copia TODO o código da solução
COPY . .

# Entra na pasta do projeto SPA
WORKDIR "/src/src/Peo.Web.Spa"

# Publica a aplicação Blazor WebAssembly
# O resultado final fica em /app/publish/wwwroot
RUN dotnet publish "Peo.Web.Spa.csproj" -c Release -o /app/publish

# ============================
# STAGE 2 - RUNTIME (Nginx)
# ============================
FROM nginx:1.27-alpine AS final

# Apaga o HTML padrão do nginx
RUN rm -rf /usr/share/nginx/html/*

# Copia os arquivos publicados do Blazor
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

COPY src/Peo.Web.Spa/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]

