FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine
WORKDIR /app
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT ["dotnet-ef", "database", "update", "--project", "/app/"]