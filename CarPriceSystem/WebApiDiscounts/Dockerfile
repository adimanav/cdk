FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY WebApiDiscounts/WebApiDiscounts.csproj WebApiDiscounts/
RUN dotnet restore WebApiDiscounts/WebApiDiscounts.csproj
COPY . .
WORKDIR /src/WebApiDiscounts
RUN dotnet build WebApiDiscounts.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebApiDiscounts.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApiDiscounts.dll"]
