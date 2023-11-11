FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Order/OrderMicroServicesWebAPI/OrderMicroServicesWebAPI.csproj", "Services/Order/OrderMicroServicesWebAPI/"]
COPY ["Shared/OnlineStudyShared/OnlineStudyShared.csproj", "Shared/OnlineStudyShared/"]
COPY ["Services/Order/OrderCore/OrderCore.csproj", "Services/Order/OrderCore/"]
COPY ["Services/Order/OrderDomain/OrderDomain.csproj", "Services/Order/OrderDomain/"]
COPY ["Services/Order/OrderInfrastructure/OrderInfrastructure.csproj", "Services/Order/OrderInfrastructure/"]
COPY ["Services/Order/OrderApplication1/OrderApplication1.csproj", "Services/Order/OrderApplication1/"]
RUN dotnet restore "Services/Order/OrderMicroServicesWebAPI/OrderMicroServicesWebAPI.csproj"
COPY . .
WORKDIR "/src/Services/Order/OrderMicroServicesWebAPI"
RUN dotnet build "OrderMicroServicesWebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderMicroServicesWebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderMicroServicesWebAPI.dll"]
