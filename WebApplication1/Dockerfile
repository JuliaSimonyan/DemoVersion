# Base image (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# (1) Copy the SOLUTION FILE from the parent directory
COPY ["../Gyumri.sln", "./"]

# (2) Copy the main project file (Gyumri.csproj)
COPY ["Gyumri.csproj", "WebApplication1/"]  # Adjust path if needed

# (3) Restore dependencies (NuGet)
RUN dotnet restore "Gyumri.sln"

# (4) Copy the rest of the source code
COPY . .

# (5) Publish the app
RUN dotnet publish "WebApplication1/Gyumri.csproj" -c Release -o /app/publish

# Final stage (runtime)
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Gyumri.dll"]