FROM mcr.microsoft.com/dotnet/sdk:8.0.204 AS build

ARG BUILD_CONFIGURATION=Release
WORKDIR /app

# Copy csproj files
COPY Services/Catalog/Catalog.Api/Catalog.Api.csproj Services/Catalog/Catalog.Api/Catalog.Api.csproj
COPY BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj
# Restore 
RUN dotnet restore "./Services/Catalog/Catalog.Api/Catalog.Api.csproj"
COPY . .

# Copy anything else
WORKDIR /app/Services/Catalog/Catalog.Api
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0.4

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=build /app/publish .

ENTRYPOINT [ "dotnet", "Catalog.Api.dll" ]


