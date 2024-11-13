FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS base
WORKDIR /app

FROM base AS develop
ENTRYPOINT ["dotnet", "watch", "run", "--no-launch-profile", "--non-interactive", "--no-hot-reload"]

# Dev environment doesn't run this stage or beyond
FROM base AS build
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build -c Release --no-restore

FROM build AS publish
RUN dotnet publish -c Release -o published --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
EXPOSE 5000
WORKDIR /app
COPY --from=publish /app/published /app
ENTRYPOINT ["dotnet", "app.dll"]