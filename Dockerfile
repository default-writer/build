FROM microsoft/dotnet:2.1.300-preview2-sdk-alpine AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Build/*.csproj ./Build/
COPY Build.Tests/*.csproj ./Build.Tests/
RUN dotnet restore

# copy everything else and build app
COPY . .
WORKDIR /app/Build
RUN dotnet build


FROM build AS testrunner
WORKDIR /app/Build.Tests
ENTRYPOINT ["dotnet", "test", "--logger:trx"]


FROM build AS test
WORKDIR /app/Build.Tests
RUN dotnet test