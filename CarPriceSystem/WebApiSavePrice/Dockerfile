FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY WebApiSavePrice/WebApiSavePrice.csproj WebApiSavePrice/
RUN dotnet restore WebApiSavePrice/WebApiSavePrice.csproj
COPY . .
WORKDIR /src/WebApiSavePrice
RUN dotnet build WebApiSavePrice.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebApiSavePrice.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApiSavePrice.dll"]
