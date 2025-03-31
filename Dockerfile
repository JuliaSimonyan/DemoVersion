FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Gyumri.sln", "./"]
COPY ["WebApplication1/Gyumri.csproj", "WebApplication1/"]
RUN dotnet restore "Gyumri.sln"
COPY . .
RUN dotnet publish "WebApplication1/Gyumri.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApplication1.dll"]