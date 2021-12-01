# https://github.com/dotnet/dotnet-docker/blob/main/samples/aspnetapp/Dockerfile
# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY Server/*.csproj ./Server/
COPY EF_ProjectBank/*.csproj ./EF_ProjectBank/
COPY Client/*.csproj ./Client/
COPY Shared/*.csproj ./Shared/
WORKDIR /source/Server
RUN dotnet restore "BlazorApp.Server.csproj"

# copy everything else and build app
WORKDIR /source
COPY Server/. ./Server/
COPY EF_ProjectBank/. ./EF_ProjectBank/
COPY Client/. ./Client/
COPY Shared/. ./Shared/
WORKDIR /source/Server
RUN dotnet publish "BlazorApp.Server.csproj" -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 7203/tcp
ENV ASPNETCORE_URLS http://*:7203
ENTRYPOINT ["dotnet", "BlazorApp.Server.dll"]