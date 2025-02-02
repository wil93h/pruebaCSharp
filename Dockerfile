# Usar la imagen base de .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Fase de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Api/Api.csproj", "src/Api/"]
COPY ["src/DataService/DataService.csproj", "src/DataService/"]
COPY ["src/Entities/Entities.csproj", "src/Entities/"]
COPY ["src/Services/Services.csproj", "src/Services/"]
COPY ["src/Utilities/Utilities.csproj", "src/Utilities/"]
RUN dotnet restore "src/Api/Api.csproj"
COPY . .
WORKDIR "/src/src/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

# Fase de publicación
FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

# Fase final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]