# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["Gyumri.sln", "./"]
COPY ["WebApplication1/Gyumri.csproj", "WebApplication1/"]
COPY ["Gyumri.Data/Gyumri.Data.csproj", "Gyumri.Data/"]
COPY ["Gyumri.Application/Gyumri.Application.csproj", "Gyumri.Application/"]
COPY ["Gyumri.Common/Gyumri.Common.csproj", "Gyumri.Common/"]
RUN dotnet restore "Gyumri.sln"

# Copy everything else and publish
COPY . .
RUN dotnet publish "WebApplication1/Gyumri.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
# Fix: Copy from the CORRECT publish directory
COPY --from=build /app/publish .
# Verify the DLL name matches your project output
ENTRYPOINT ["dotnet", "Gyumri.dll"]  # ← Changed from WebApplication1.dll