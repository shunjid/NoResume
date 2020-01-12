FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /app
COPY . .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet NoResume.dll