# Usa la imagen oficial de .NET SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copia los archivos .csproj y restaura cualquier dependencia (vía NuGet)
COPY *.csproj ./
RUN dotnet restore

# Copia todo y construye la aplicación
COPY . ./
RUN dotnet publish -c Release -o out

# Genera la imagen de runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ChallengeN5.dll"]
