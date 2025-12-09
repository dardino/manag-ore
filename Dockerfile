## Stage 1: build Angular
FROM node:20-alpine AS node-build
WORKDIR /src/Web/ClientApp
COPY src/Web/ClientApp/package.json ./
COPY src/Web/ClientApp/ ./
RUN npm ci
RUN npm run build

## Stage 2: build dotnet app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS dotnet-build
WORKDIR /src
COPY . ./
WORKDIR /src/src/Api/ManagOre.Api
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

## Stage 3: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=dotnet-build /app/publish ./
# copy built frontend
COPY --from=node-build /src/Web/ClientApp/dist ./wwwroot

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "ManagOre.Api.dll"]
