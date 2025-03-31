# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copy solution file
COPY ["Gyumri.sln", "./"]

# 2. Copy MAIN project file
COPY ["WebApplication1/Gyumri.csproj", "WebApplication1/"]

# 3. Copy DEPENDENCY project files
COPY ["Gyumri.Data/Gyumri.Data.csproj", "Gyumri.Data/"]
COPY ["Gyumri.Application/Gyumri.Application.csproj", "Gyumri.Application/"]
COPY ["Gyumri.Common/Gyumri.Common.csproj", "Gyumri.Common/"]

# 4. Restore all packages (this now works because all .csproj files are available)
RUN dotnet restore "Gyumri.sln"

# 5. Copy everything else
COPY . .

# 6. Publish
RUN dotnet publish "WebApplication1/Gyumri.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApplication1.dll"]