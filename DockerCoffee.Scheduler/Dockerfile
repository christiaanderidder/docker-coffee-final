#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["DockerCoffee.Scheduler/DockerCoffee.Scheduler.csproj", "DockerCoffee.Scheduler/"]
COPY ["DockerCoffee.Shared/DockerCoffee.Shared.csproj", "DockerCoffee.Shared/"]
RUN dotnet restore "DockerCoffee.Scheduler/DockerCoffee.Scheduler.csproj"
COPY . .
WORKDIR "/src/DockerCoffee.Scheduler"
RUN dotnet build "DockerCoffee.Scheduler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DockerCoffee.Scheduler.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerCoffee.Scheduler.dll"]