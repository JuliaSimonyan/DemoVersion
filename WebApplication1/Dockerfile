# Use the official .NET runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution file and restore any dependencies (via NuGet)
COPY ["Gyumri.sln", "./"]
COPY ["WebApplication/WebApplication.csproj", "WebApplication/"]
COPY ["Gyumri.Application/Gyumri.Application.csproj", "Gyumri.Application/"]
COPY ["Gyumri.Data/Gyumri.Data.csproj", "Gyumri.Data/"]
COPY ["Gyumri.Common/Gyumri.Common.csproj", "Gyumri.Common/"]

RUN dotnet restore "Gyumri.sln"

# Copy all other source code to the container
COPY . .

# Publish the application to the /app/publish directory
RUN dotnet publish "WebApplication/WebApplication.csproj" -c Release -o /app/publish

# Final Stage: Copy the build artifacts to the runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApplication.dll"]
