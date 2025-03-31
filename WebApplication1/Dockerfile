# Use the official .NET runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution file (Gyumri.sln) from the root directory
COPY ["../Gyumri.sln", "./"]  # Gyumri.sln is in the root directory

# Copy the project files for each project
COPY ["../WebApplication1/WebApplication1.csproj", "WebApplication1/"]
COPY ["../Gyumri.Application/Gyumri.Application.csproj", "Gyumri.Application/"]
COPY ["../Gyumri.Data/Gyumri.Data.csproj", "Gyumri.Data/"]
COPY ["../Gyumri.Common/Gyumri.Common.csproj", "Gyumri.Common/"]

# Restore dependencies for the entire solution
RUN dotnet restore "../Gyumri.sln"  # Reference Gyumri.sln correctly

# Copy all the other files into the container
COPY .. .

# Publish the WebApplication1 project
RUN dotnet publish "WebApplication1/WebApplication1.csproj" -c Release -o /app/publish

# Final Stage: Copy the build artifacts to the runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApplication1.dll"]
