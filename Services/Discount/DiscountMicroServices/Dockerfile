FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Discount/DiscountMicroServices/DiscountMicroServices.csproj", "Services/Discount/DiscountMicroServices/"]
COPY ["Shared/OnlineStudyShared/OnlineStudyShared.csproj", "Shared/OnlineStudyShared/"]
COPY ["Services/Discount/Business/Business.csproj", "Services/Discount/Business/"]
COPY ["Services/Discount/DataAccess/DataAccess.csproj", "Services/Discount/DataAccess/"]
COPY ["Services/Discount/Entity/Entity.csproj", "Services/Discount/Entity/"]
RUN dotnet restore "Services/Discount/DiscountMicroServices/DiscountMicroServices.csproj"
COPY . .
WORKDIR "/src/Services/Discount/DiscountMicroServices"
RUN dotnet build "DiscountMicroServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DiscountMicroServices.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiscountMicroServices.dll"]
