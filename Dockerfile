FROM microsoft/dotnet:2.1-sdk
WORKDIR /

# copy everything and build app
COPY . .
RUN dotnet restore
RUN dotnet build
RUN dotnet test