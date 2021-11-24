# https://github.com/dotnet/dotnet-docker/blob/main/samples/aspnetapp/Dockerfile
# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY API/*.csproj ./API/
COPY EF_ProjectBank/*.csproj ./EF_ProjectBank/
WORKDIR /source/API
RUN dotnet restore "API.csproj"

# copy everything else and build app
WORKDIR /source
COPY API/. ./API/
COPY EF_ProjectBank/. ./EF_ProjectBank/
WORKDIR /source/API
RUN dotnet publish "API.csproj" -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "API.dll"]