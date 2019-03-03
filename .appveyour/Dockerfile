FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Build.Abstractions/*.csproj ./Build.Abstractions/
COPY Build/*.csproj ./Build/
COPY Build.Tests/*.csproj ./Build.Tests/
RUN dotnet restore

# copy everything else and build app
COPY . .
WORKDIR /app/Build
RUN dotnet build

WORKDIR /app/Build.Tests
ENTRYPOINT ["dotnet","test","--logger:trx"]
