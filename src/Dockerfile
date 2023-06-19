FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

COPY *.sln ./
COPY ./Application/*.csproj ./Application/
COPY ./Domain/*.csproj ./Domain/
COPY ./Infrastructure/*.csproj ./Infrastructure/
COPY ./Presentation/API/*.csproj ./Presentation/API/
COPY ./Presentation/Contracts/*.csproj ./Presentation/Contracts/
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

ENV AZURE_TENANT_ID=a49e271b-3af0-4d03-9e72-f61a5642e650
ENV AZURE_CLIENT_ID=c7e055a3-c741-4131-9ebb-fa9ad0c821e4
ENV AZURE_CLIENT_SECRET=xS_8Q~0ViZ5WQyhVqFbhXH95DeS2LAiXsV5rMb.8
ENV AZURE_KEYVAULT_NAME=https://kv-sweden-prod-001.vault.azure.net/


FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "API.dll"]
