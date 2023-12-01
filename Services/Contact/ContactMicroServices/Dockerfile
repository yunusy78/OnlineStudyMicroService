FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Contact/ContactMicroServices/ContactMicroServices.csproj", "Services/Contact/ContactMicroServices/"]
COPY ["Services/Contact/Business/Business.csproj", "Services/Contact/Business/"]
COPY ["Shared/OnlineStudyShared/OnlineStudyShared.csproj", "Shared/OnlineStudyShared/"]
COPY ["Services/Contact/EntityLayer/EntityLayer.csproj", "Services/Contact/EntityLayer/"]
COPY ["Services/Contact/DataAccess/DataAccess.csproj", "Services/Contact/DataAccess/"]
RUN dotnet restore "Services/Contact/ContactMicroServices/ContactMicroServices.csproj"
COPY . .
WORKDIR "/src/Services/Contact/ContactMicroServices"
RUN dotnet build "ContactMicroServices.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactMicroServices.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactMicroServices.dll"]
