#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["DockerCoffee.Worker/DockerCoffee.Worker.csproj", "DockerCoffee.Worker/"]
COPY ["DockerCoffee.Shared/DockerCoffee.Shared.csproj", "DockerCoffee.Shared/"]
RUN dotnet restore "DockerCoffee.Worker/DockerCoffee.Worker.csproj"
COPY . .
WORKDIR "/src/DockerCoffee.Worker"
RUN dotnet build "DockerCoffee.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DockerCoffee.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerCoffee.Worker.dll"]