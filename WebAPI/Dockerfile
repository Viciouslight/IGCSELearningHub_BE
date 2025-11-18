FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["IGCSELearningHub.sln", "./"]
COPY ["WebAPI/IGCSELearningHub.WebAPI.csproj", "WebAPI/"]
COPY ["Infrastructure/IGCSELearningHub.Infrastructure.csproj", "Infrastructure/"]
COPY ["Application/IGCSELearningHub.Application.csproj", "Application/"]
COPY ["Domain/IGCSELearningHub.Domain.csproj", "Domain/"]
RUN dotnet restore "IGCSELearningHub.sln"

COPY . .
RUN dotnet build "IGCSELearningHub.sln" -c $BUILD_CONFIGURATION -o /app/build
WORKDIR "/src/WebAPI"

# publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./IGCSELearningHub.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# runtime stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IGCSELearningHub.WebAPI.dll"]
