FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Build/*.csproj ./build/
COPY Build.Tests/*.csproj ./tests/
RUN dotnet restore

# copy everything else and build app
COPY . .
WORKDIR /app
RUN dotnet build


FROM build AS testrunner
WORKDIR /app/tests
ENTRYPOINT ["dotnet", "test", "--logger:trx"]


FROM build AS test
WORKDIR /app/tests
RUN dotnet test


#FROM build AS publish
#WORKDIR /app/build
#RUN dotnet publish -c Release -o out


#FROM microsoft/dotnet:2.1-runtime AS runtime
#WORKDIR /app
#COPY --from=publish /app/build/out ./
#ENTRYPOINT ["dotnet", "build.dll"]