FROM mcr.microsoft.com/dotnet/sdk:8.0.204 AS build

ARG BUILD_CONFIGURATION=Release
WORKDIR /app

# Copy csproj files
COPY WebApps/Shopping.Web/Shopping.Web.csproj WebApps/Shopping.Web/Shopping.Web.csproj
# Restore 
RUN dotnet restore "./WebApps/Shopping.Web/Shopping.Web.csproj"
COPY . .

# Copy anything else
WORKDIR /app/WebApps/Shopping.Web
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0.4

WORKDIR /app
EXPOSE 7070
EXPOSE 7071

COPY --from=build /app/publish .

ENTRYPOINT [ "dotnet", "Shopping.Web.dll" ]


