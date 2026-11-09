# 1. Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiamos el archivo de proyecto y restauramos dependencias
COPY ["GestorDuctos.csproj", "./"]
RUN dotnet restore "GestorDuctos.csproj"

# Copiamos el resto del código y compilamos
COPY . .
RUN dotnet publish "GestorDuctos.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 2. Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080

# Copiamos los archivos compilados desde la etapa anterior
COPY --from=build /app/publish .

# Comando para iniciar la aplicación
ENTRYPOINT ["dotnet", "GestorDuctos.dll"]