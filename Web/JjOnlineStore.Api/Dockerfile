FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 7314
EXPOSE 44340

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Web/JjOnlineStore.Api/JjOnlineStore.Api.csproj Web/JjOnlineStore.Api/
RUN dotnet restore Web/JjOnlineStore.Api/JjOnlineStore.Api.csproj
COPY . .
WORKDIR /src/Web/JjOnlineStore.Api
RUN dotnet build JjOnlineStore.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish JjOnlineStore.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "JjOnlineStore.Api.dll"]
