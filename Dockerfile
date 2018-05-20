FROM microsoft/dotnet:2.1.300-preview2-sdk-alpine as builder

WORKDIR /app
COPY . .

RUN dotnet restore

# run tests on docker build
RUN dotnet test --no-build Build.Tests

# run tests on docker run
ENTRYPOINT ["dotnet", "test"]