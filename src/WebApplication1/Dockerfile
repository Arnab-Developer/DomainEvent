FROM mcr.microsoft.com/dotnet/aspnet:7.0
COPY src/WebApplication1/bin/publish/ ./app
WORKDIR /app
ENTRYPOINT ["dotnet", "WebApplication1.dll"]