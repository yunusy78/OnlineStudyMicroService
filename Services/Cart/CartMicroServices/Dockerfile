FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Cart/CartMicroServices/CartMicroServices.csproj", "Services/Cart/CartMicroServices/"]
RUN dotnet restore "Services/Cart/CartMicroServices/CartMicroServices.csproj"
COPY . .
WORKDIR "/src/Services/Cart/CartMicroServices"
RUN dotnet build "CartMicroServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CartMicroServices.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CartMicroServices.dll"]
