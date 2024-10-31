FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS base
WORKDIR /app

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

FROM base AS develop
ENTRYPOINT ["dotnet", "watch", "run", "--no-launch-profile"]

FROM base AS migrations
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT ["dotnet-ef", "database", "update", "--project", "/app/"]

