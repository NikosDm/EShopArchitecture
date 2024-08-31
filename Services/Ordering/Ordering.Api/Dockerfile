FROM mcr.microsoft.com/dotnet/sdk:8.0.204 AS build

ARG BUILD_CONFIGURATION=Release
WORKDIR /app

# Copy csproj files
COPY Services/Ordering/Ordering.Api/Ordering.Api.csproj Services/Ordering/Ordering.Api/
COPY Services/Ordering/Ordering.Application/Ordering.Application.csproj Services/Ordering/Ordering.Application/
COPY Services/Ordering/Ordering.Domain/Ordering.Domain.csproj Services/Ordering/Ordering.Domain/
COPY Services/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj Services/Ordering/Ordering.Infrastructure/
COPY BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj
# Restore 
RUN dotnet restore "./Services/Ordering/Ordering.Api/Ordering.Api.csproj"
COPY . .

# Copy anything else
WORKDIR /app/Services/Ordering/Ordering.Api
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0.4

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=build /app/publish .

ENTRYPOINT [ "dotnet", "Ordering.Api.dll" ]


