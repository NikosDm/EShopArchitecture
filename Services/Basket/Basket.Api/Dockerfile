FROM mcr.microsoft.com/dotnet/sdk:8.0.204 AS build

ARG BUILD_CONFIGURATION=Release
WORKDIR /app

# Copy csproj files
COPY Services/Basket/Basket.Api/Basket.Api.csproj Services/Basket/Basket.Api/Basket.Api.csproj
COPY BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj
COPY BuildingBlocks/BuildingBlocks.Messaging/BuildingBlocks.Messaging.csproj BuildingBlocks/BuildingBlocks.Messaging/BuildingBlocks.Messaging.csproj
# Restore 
RUN dotnet restore "./Services/Basket/Basket.Api/Basket.Api.csproj"
COPY . .

# Copy anything else
WORKDIR /app/Services/Basket/Basket.Api
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0.4

WORKDIR /app
EXPOSE 7070
EXPOSE 7071

COPY --from=build /app/publish .

ENTRYPOINT [ "dotnet", "Basket.Api.dll" ]


