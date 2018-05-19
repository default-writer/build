FROM microsoft/dotnet:2.1.300-preview2-sdk-alpine  AS build
WORKDIR /
RUN dotnet restore
RUN dotnet build
RUN dotnet test --nobuild Build.Test