# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["AccountApi/AccountApi.csproj", "AccountApi/"]
COPY ["AccountDatabase/AccountDatabase.csproj", "AccountDatabase/"]
RUN dotnet restore "./AccountApi/AccountApi.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/AccountApi"
RUN dotnet build "./AccountApi.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "./AccountApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "AccountApi.dll"]
