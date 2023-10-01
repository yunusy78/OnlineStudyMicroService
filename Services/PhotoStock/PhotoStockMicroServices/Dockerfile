FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/PhotoStock/PhotoStockMicroServices/PhotoStockMicroServices.csproj", "Services/PhotoStock/PhotoStockMicroServices/"]
RUN dotnet restore "Services/PhotoStock/PhotoStockMicroServices/PhotoStockMicroServices.csproj"
COPY . .
WORKDIR "/src/Services/PhotoStock/PhotoStockMicroServices"
RUN dotnet build "PhotoStockMicroServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhotoStockMicroServices.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhotoStockMicroServices.dll"]
