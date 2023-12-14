# Start with the base ASP.NET image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

# Use the SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

# Copy the necessary project files
COPY ../AccountingApp/AccountApi/AccountApi.csproj ./AccountApi/
COPY ../AccountingApp/AccountDatabase/AccountDatabase.csproj ./AccountDatabase/

WORKDIR /app/AccountApi

# Restore dependencies for AccountApi
RUN dotnet restore

# Copy the entire solution directory
COPY . ./

# Build the application
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Create the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountApi.dll"]