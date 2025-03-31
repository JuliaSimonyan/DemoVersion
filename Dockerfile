# Use the official .NET runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["YOUR_PROJECT.csproj", "./"]
RUN dotnet restore "Gyumri.sln"
COPY . .
RUN dotnet publish "Gyumri.sln" -c Release -o /app/publish

# Final Stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Gyumri.dll"]
