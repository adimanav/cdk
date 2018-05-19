FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY WebApiVehicle/WebApiVehicle.csproj WebApiVehicle/
RUN dotnet restore WebApiVehicle/WebApiVehicle.csproj
COPY . .
WORKDIR /src/WebApiVehicle
RUN dotnet build WebApiVehicle.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebApiVehicle.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApiVehicle.dll"]
