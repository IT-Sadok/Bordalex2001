# -----------------------------
# 1. Build stage
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["BookingSystemApi/BookingSystemApi.sln", "BookingSystemApi/"]
COPY ["BookingSystemApi/WebApi/WebApi.csproj", "BookingSystemApi/WebApi/"]
COPY ["BookingSystemApi/Domain/Domain.csproj", "BookingSystemApi/Domain/"]
COPY ["BookingSystemApi/Infrastructure/Infrastructure.csproj", "BookingSystemApi/Infrastructure/"]
COPY ["BookingSystemApi/Application/Application.csproj", "BookingSystemApi/Application/"]

# Restore dependencies
RUN dotnet restore "BookingSystemApi/BookingSystemApi.sln"

# Copy the rest of the source code
COPY . .

# Build & publish the application
WORKDIR "/src/BookingSystemApi/WebApi"
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# -----------------------------
# 2. Runtime stage (SLIM)
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app

# Create a non-root user to run the application
RUN addgroup -S appgroup && adduser -S appuser -G appgroup

# Copy the build output
COPY --from=build /app/publish .

# Set permissions
RUN chown -R appuser:appgroup /app

# Switch to non-root user
USER appuser

# Azure Web App uses port 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

ENTRYPOINT ["dotnet", "WebApi.dll"]