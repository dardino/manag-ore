# ManagOre — Time tracking POC

This repository contains a Proof-of-Concept for a time tracking application (ManagOre).

Stack summary
- Backend: ASP.NET Core Web API (versioned routes api/v{version}) + EF Core + PostgreSQL
- Frontend: Angular + Angular Material (pnpm) — generated typed clients from Swagger using swagger-typescript-api (--useFetch)
- Auth: Azure AD via Microsoft.Identity.Web + msal-angular placeholders
- Container: single multi-stage Docker image (build Angular -> dotnet publish -> copy dist to wwwroot)

Quick dev commands

Local docker compose development (Postgres + app):
```bash
docker compose -f docker-compose.dev.yml up --build
```

Local backend (without docker):
```bash
cd src/Api/ManagOre.Api
dotnet restore
dotnet build
dotnet run
```

Local frontend (without docker):
```bash
cd src/Web/ClientApp
pnpm install
# optionally generate types from local API
pnpm run generate:api:from-url
pnpm start
```
