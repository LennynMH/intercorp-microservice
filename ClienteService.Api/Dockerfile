#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ClienteService.Api/ClienteService.Api.csproj", "ClienteService.Api/"]
COPY ["ClienteService.Infrastructure/ClienteService.Infrastructure.csproj", "ClienteService.Infrastructure/"]
COPY ["ClienteService.Core/ClienteService.Core.csproj", "ClienteService.Core/"]
RUN dotnet restore "ClienteService.Api/ClienteService.Api.csproj"
COPY . .
WORKDIR "/src/ClienteService.Api"
RUN dotnet build "ClienteService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClienteService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClienteService.Api.dll"]