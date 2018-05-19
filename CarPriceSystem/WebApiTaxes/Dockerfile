FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY WebApiTaxes/WebApiTaxes.csproj WebApiTaxes/
RUN dotnet restore WebApiTaxes/WebApiTaxes.csproj
COPY . .
WORKDIR /src/WebApiTaxes
RUN dotnet build WebApiTaxes.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebApiTaxes.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApiTaxes.dll"]
