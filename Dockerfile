# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy and restore project dependencies
COPY ["UserService/UserService.csproj", "./"]
RUN dotnet restore

# Copy everything and build the app
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Runtime Stage 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UserService.Api"]