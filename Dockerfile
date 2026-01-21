# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["UnattendBuilder.Web.csproj", "./"]
RUN dotnet restore "UnattendBuilder.Web.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "UnattendBuilder.Web.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "UnattendBuilder.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UnattendBuilder.Web.dll"]
