FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Catalog/CatalogMicroServices/CatalogMicroServices.csproj", "Services/Catalog/CatalogMicroServices/"]
COPY ["Shared/OnlineStudyShared/OnlineStudyShared.csproj", "Shared/OnlineStudyShared/"]
RUN dotnet restore "Services/Catalog/CatalogMicroServices/CatalogMicroServices.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/CatalogMicroServices"
RUN dotnet build "CatalogMicroServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CatalogMicroServices.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "CatalogMicroServices.dll"]
