#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["DockerCoffee.Web/DockerCoffee.Web.csproj", "DockerCoffee.Web/"]
COPY ["DockerCoffee.Shared/DockerCoffee.Shared.csproj", "DockerCoffee.Shared/"]
RUN dotnet restore "DockerCoffee.Web/DockerCoffee.Web.csproj"
COPY . .
WORKDIR "/src/DockerCoffee.Web"
RUN dotnet build "DockerCoffee.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DockerCoffee.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerCoffee.Web.dll"]