# Use the official .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Set the working directory in the container
WORKDIR /app

# Copy the .csproj and restore any dependencies (optimization for caching)
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining source code to the container
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Use a lightweight image for the final stage
FROM mcr.microsoft.com/dotnet/runtime:7.0 AS runtime

# Set the working directory in the container
WORKDIR /app

# Copy the built application from the previous stage
COPY --from=build-env /app/out .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "TestConnection.dll"]
