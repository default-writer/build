# Build image
#FROM microsoft/dotnet:2.1.300-preview2-sdk-alpine AS builder 
#WORKDIR /
#COPY . .
# Copy all the csproj files and restore to cache the layer for faster builds
# The dotnet_build.sh script does this anyway, so superfluous, but docker can 
# cache the intermediate images so _much_ faster
#COPY ./src/AspNetCoreInDocker.Lib/AspNetCoreInDocker.Lib.csproj  ./src/AspNetCoreInDocker.Lib/AspNetCoreInDocker.Lib.csproj  
#COPY ./src/AspNetCoreInDocker.Web/AspNetCoreInDocker.Web.csproj  ./src/AspNetCoreInDocker.Web/AspNetCoreInDocker.Web.csproj  
#COPY ./test/AspNetCoreInDocker.Web.Tests/AspNetCoreInDocker.Web.Tests.csproj  ./test/AspNetCoreInDocker.Web.Tests/AspNetCoreInDocker.Web.Tests.csproj  
#RUN dotnet restore

#COPY ./test ./test  
#COPY ./src ./src  
#RUN dotnet build

#RUN dotnet test "./test/AspNetCoreInDocker.Web.Tests/AspNetCoreInDocker.Web.Tests.csproj" -c Release --no-build --no-restore
#RUN dotnet publish "./src/AspNetCoreInDocker.Web/AspNetCoreInDocker.Web.csproj" -c Release -o "../../dist" --no-restore

#Build the app image
#FROM microsoft/aspnetcore:2.0.3  
#WORKDIR /app  
#ENV ASPNETCORE_ENVIRONMENT Local  
#ENTRYPOINT ["dotnet", "AspNetCoreInDocker.Web.dll"]  
#COPY --from=builder /sln/dist . 

FROM microsoft/dotnet:2.1.300-preview2-sdk-alpine as builder
WORKDIR /

COPY ./TypeBuilder.sln ./TypeBuilder.sln
COPY ./Build/Build.csproj ./Build/Build.csproj
COPY ./Build.Tests/Build.Tests.csproj ./Build/Build.Tests.csproj
RUN dotnet restore

COPY ./Build ./Build
COPY ./Build.Test ./Build.Test
RUN dotnet build
RUN dotnet test --no-build Build.Tests