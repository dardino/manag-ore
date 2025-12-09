# ManagOre — Time tracking POC

This repository contains a Proof-of-Concept for a time tracking application (ManagOre).

Stack summary
- Backend: ASP.NET Core Web API (versioned routes api/v{version}) + EF Core + PostgreSQL
- Frontend: Angular + Angular Material (pnpm) — generated typed clients from Swagger using swagger-typescript-api (--useFetch)
- Auth: Azure AD via Microsoft.Identity.Web + msal-angular placeholders
- Container: single multi-stage Docker image (build Angular -> dotnet publish -> copy dist to wwwroot)

Requirements
- .NET SDK 9.0 (or higher) — the repo now targets net9.0. Install via Homebrew or from Microsoft downloads.
- Node.js 20+ and pnpm >=7 for frontend tasks

Postgres integration tests (local)
If you want to run the Postgres integration tests locally (they are enabled in CI), start a local Postgres and export the connection string and the toggle environment variable before running tests:

Using docker-compose (recommended):

```bash
docker compose -f docker-compose.dev.yml up -d postgres
# Wait for startup, then run tests (example connection string values match the compose file):
export POSTGRES_INTEGRATION=true
export POSTGRES_CONNECTION_STRING="Host=localhost;Port=5432;Database=managore_tests;Username=dev;Password=dev"
dotnet test tests/Api.Tests --filter "FullyQualifiedName~PostgresIntegrationTests"
```

Using docker run (quick):

```bash
docker run --rm -e POSTGRES_DB=managore_tests -e POSTGRES_USER=test -e POSTGRES_PASSWORD=test -p 5432:5432 postgres:15-alpine &
export POSTGRES_INTEGRATION=true
export POSTGRES_CONNECTION_STRING="Host=localhost;Port=5432;Database=managore_tests;Username=test;Password=test"
dotnet test tests/Api.Tests --filter "FullyQualifiedName~PostgresIntegrationTests"
```

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
