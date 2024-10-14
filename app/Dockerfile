FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS base
WORKDIR /src
COPY *.csproj .
RUN dotnet restore

FROM base AS build
WORKDIR /src
COPY . .
RUN dotnet build -c Release --no-restore

FROM build AS publish
WORKDIR /src
RUN dotnet publish -c Release -o published --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
EXPOSE 5000
WORKDIR /app
COPY --from=publish /src/published /app
ENTRYPOINT ["dotnet", "app.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS develop
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "run", "--no-launch-profile"]

