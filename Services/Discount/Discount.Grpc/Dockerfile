FROM mcr.microsoft.com/dotnet/sdk:8.0.204 AS build

ARG BUILD_CONFIGURATION=Release
WORKDIR /app

# Copy csproj files
COPY Services/Discount/Discount.Grpc/Discount.Grpc.csproj Services/Discount/Discount.Grpc/Discount.Grpc.csproj
# Restore 
RUN dotnet restore "./Services/Discount/Discount.Grpc/Discount.Grpc.csproj"
COPY . .

# Copy anything else
WORKDIR /app/Services/Discount/Discount.Grpc
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0.4

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=build /app/publish .

ENTRYPOINT [ "dotnet", "Discount.Grpc.dll" ]


