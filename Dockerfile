FROM microsoft/dotnet:2.1.300-preview2-sdk-alpine AS build
#FROM microsoft/dotnet:2.1-sdk
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Build/*.csproj ./dotnetapp/
COPY Build.Test/*.csproj ./tests/
RUN dotnet restore

# copy everything else and build app
COPY . .
WORKDIR /app/dotnetapp
RUN dotnet build


FROM build AS testrunner
WORKDIR /app/tests
ENTRYPOINT ["dotnet", "test", "--logger:trx"]


FROM build AS test
WORKDIR /app/tests
RUN dotnet test


#FROM build AS publish
#WORKDIR /app/dotnetapp
#RUN dotnet publish -c Release -o out


#FROM microsoft/dotnet:2.1-runtime AS runtime
#WORKDIR /app
#COPY --from=publish /app/dotnetapp/out ./
#ENTRYPOINT ["dotnet", "Build.dll"]