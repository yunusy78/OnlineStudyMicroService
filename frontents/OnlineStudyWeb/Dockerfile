FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["frontents/OnlineStudyWeb/OnlineStudyWeb.csproj", "frontents/OnlineStudyWeb/"]
COPY ["frontents/Business/Business.csproj", "frontents/Business/"]
COPY ["Shared/OnlineStudyShared/OnlineStudyShared.csproj", "Shared/OnlineStudyShared/"]
RUN dotnet restore "frontents/OnlineStudyWeb/OnlineStudyWeb.csproj"
COPY . .
WORKDIR "/src/frontents/OnlineStudyWeb"
RUN dotnet build "OnlineStudyWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnlineStudyWeb.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineStudyWeb.dll"]
