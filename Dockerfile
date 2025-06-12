FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia só o arquivo csproj para pasta /src/DevMatch
COPY DevMatch/DevMatch.csproj DevMatch/

# Restaura usando o caminho correto
RUN dotnet restore DevMatch/DevMatch.csproj

# Copia tudo para /src
COPY . .

WORKDIR /src/DevMatch

RUN dotnet build DevMatch.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish DevMatch.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
RUN apt-get update && apt-get install -y curl ca-certificates
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_LOGGING__LOGLEVEL__DEFAULT=Information
ENTRYPOINT ["dotnet", "DevMatch.dll"]
