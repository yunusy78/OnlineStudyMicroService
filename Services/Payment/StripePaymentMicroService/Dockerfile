FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Payment/StripePaymentMicroService/StripePaymentMicroService.csproj", "Services/Payment/StripePaymentMicroService/"]
RUN dotnet restore "Services/Payment/StripePaymentMicroService/StripePaymentMicroService.csproj"
COPY . .
WORKDIR "/src/Services/Payment/StripePaymentMicroService"
RUN dotnet build "StripePaymentMicroService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StripePaymentMicroService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StripePaymentMicroService.dll"]
