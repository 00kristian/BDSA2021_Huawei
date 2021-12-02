# https://github.com/dotnet/dotnet-docker/blob/main/samples/aspnetapp/Dockerfile
# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY Core/*.csproj ./Core/
COPY Server/*.csproj ./Server/
COPY Infrastructure/*.csproj ./Infrastructure/
COPY Client/*.csproj ./Client/
WORKDIR /source/Server
RUN dotnet restore "Server.csproj"

# copy everything else and build app
WORKDIR /source
COPY Core/. ./Core/
COPY Server/. ./Server/
COPY Infrastructure/. ./Infrastructure/
COPY Client/. ./Client/
WORKDIR /source/Server
RUN dotnet publish "Server.csproj" -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 7203/tcp
ENV ASPNETCORE_URLS http://*:7203
ENTRYPOINT ["dotnet", "Server.dll"]