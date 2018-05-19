FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY WebApiCredit/WebApiCredit.csproj WebApiCredit/
RUN dotnet restore WebApiCredit/WebApiCredit.csproj
COPY . .
WORKDIR /src/WebApiCredit
RUN dotnet build WebApiCredit.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebApiCredit.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApiCredit.dll"]
