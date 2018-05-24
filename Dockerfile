FROM microsoft/dotnet-nightly:2.1-runtime-deps-alpine3.7 AS build
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


#FROM build AS publish
#WORKDIR /app/Build
#RUN dotnet publish -c Release -o out
#
#
#FROM microsoft/dotnet:2.1-runtime-alpine AS runtime
#WORKDIR /app
#COPY --from=publish /app/Build/out ./
#ENTRYPOINT ["dotnet", "Build.dll"]