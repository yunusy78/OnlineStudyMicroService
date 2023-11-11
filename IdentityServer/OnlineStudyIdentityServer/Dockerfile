FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["IdentityServer/OnlineStudyIdentityServer/OnlineStudyIdentityServer.csproj", "IdentityServer/OnlineStudyIdentityServer/"]
RUN dotnet restore "IdentityServer/OnlineStudyIdentityServer/OnlineStudyIdentityServer.csproj"
COPY . .
WORKDIR "/src/IdentityServer/OnlineStudyIdentityServer"
RUN dotnet build "OnlineStudyIdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnlineStudyIdentityServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineStudyIdentityServer.dll"]
