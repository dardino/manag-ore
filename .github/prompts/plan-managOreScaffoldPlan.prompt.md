Plan: ManagOre — full-project scaffold and delivery plan

Overview

This document is a complete plan for scaffolding the ManagOre repository and implementing a minimal, working end-to-end prototype that includes:

- Backend: ASP.NET Core Web API (latest .NET), EF Core + PostgreSQL, API versioning (route-based), Swashbuckle/Swagger, Microsoft.Identity.Web for corporate SSO, unit & integration tests.
- Frontend: Angular (CLI) + Angular Material, pnpm for package management, swagger-typescript-api (Fetch) to generate typed clients, Vitest for unit tests, Playwright for optional E2E tests.
- Dev infra & containers: single multi-stage Docker image that builds the Angular app, publishes the .NET app, copies angular `dist` into `wwwroot`; `docker-compose.dev.yml` for local dev (Postgres + environment).
- CI/CD: GitHub Actions pipeline that coordinates backend & frontend builds, generates swagger artifacts, generates typed clients for the frontend, runs tests and produces & publishes a single container image.

This plan is opinionated to provide a runnable skeleton — every choice can be adjusted after the first scaffold is produced.

Goals

- Produce a runnable seed repository and CI: run locally with `docker-compose -f docker-compose.dev.yml up --build` and in CI produce a single image for deployment.
- Keep the frontend and backend projects separated but integrated through API contracts (Swagger / OpenAPI per version).
- Ensure both codebases have test suites: xUnit for backend; Vitest for frontend (fetch client tests) and Playwright for E2E.
- Use swagger-typescript-api `--useFetch` to generate a typed fetch-based client for the Angular app.
- Configure OAuth2 SSO with Azure AD (Microsoft.Identity.Web + msal-browser) in a minimal, configurable way.

Repository layout

/
  src/
    Api/
      ManagOre.Api/            # ASP.NET Core Web API project
    Web/
      ClientApp/               # Angular application
  tests/
    Api.Tests/                 # xUnit tests for backend
    Web.Tests/                 # Vitest tests for frontend
  Dockerfile                   # multi-stage single image
  docker-compose.dev.yml       # Postgres + environment for local dev
  .github/workflows/ci.yml     # CI pipeline
  README.md

High-level implementation steps

1) Initialize repo and project skeletons
   - Create .NET solution + API project at `src/Api/ManagOre.Api`.
   - Create Angular app using Angular CLI in `src/Web/ClientApp`.
   - Add `README.md` and basic `.gitignore`.

2) Backend: Web API base & DB
   - Implement minimal domain models for time tracking: Employee, TimeEntry, Project (Commesse), ProjectGroup.
   - Add EF Core + Npgsql (Postgres) and initial migration.
   - Implement Data access layer, repository pattern or lightweight DbContext-based services.
   - Configure appsettings and environment variable overrides for connectionstring / Azure AD settings.

3) Backend: API essentials
   - Add Microsoft.AspNetCore.Mvc.Versioning + ApiExplorer to create API versioning and allow routes `api/v{version}/[controller]`.
   - Add Swashbuckle (Swagger) and configure a swagger endpoint per API version: `/swagger/v1/swagger.json` and `/swagger/v2/swagger.json`.
   - Add Microsoft.Identity.Web + JwtBearer to secure API endpoints; create an admin role-based policy for admin endpoints.

4) Backend: Tests, health, and migrations
   - Add xUnit tests for core services.
   - Add integration tests using TestServer or Testcontainers.NET against a disposable Postgres instance.
   - Add a /health endpoint and readiness probes.

5) Frontend: Angular skeleton, DI & UI
   - Create Angular app with routing and Angular Material.
   - Add MSAL Angular integration for login and token acquisition (Auth code + PKCE).
   - Add an `ApiFetchAdapter` for injecting tokens into Fetch and pass it to generated client.
   - Add basic pages: login, timesheet entry, projects list, groups list, admin project setup.

6) Frontend: swagger-typescript-api + pnpm integration
   - Add `swagger-typescript-api` as devDependency and create `pnpm` scripts to generate the fetch client into `src/app/api-client`.
   - Provide three scripts: `generate:api:from-url` (dev, pulls from running API), `generate:api:from-file` (offline), `generate:api:ci` (CI with artifact).

7) Frontend: Tests (Vitest)
   - Configure Vitest (jsdom) and write tests for `ApiFetchAdapter` and a couple of generated client calls.
   - Use `msw` or `vi.stubGlobal('fetch')` for network mocking.

8) Docker + compose
   - Create multi-stage `Dockerfile`:
     - Node stage builds Angular -> produces `dist`.
     - dotnet build/publish stage produces runtime artifacts.
     - final runtime stage copies `dist` into `wwwroot` and runs `dotnet ManagOre.Api.dll`.
   - Create `docker-compose.dev.yml` with services for `app` and `postgres` and environment variables for dev.

9) CI pipeline
   - Stages:
     a) api-build: build & test backend, produce per-version swagger.json artifacts (e.g., `swagger/v1.json`), upload artifact.
     b) web-build: download swagger artifact, run `pnpm` to generate client, run Vitest, build Angular and upload `dist` artifact.
     c) image-build: multi-stage build, copy `dist` into wwwroot, produce final image, run smoke tests, push image.
   - Add an optional `verify:api-client` step that fails CI if generated client doesn't match checked-in files (if you choose to commit generated files).

10) DB migrations & deployments
   - CI may generate SQL migration scripts; a deployment job should apply migrations safely (manual/automated approval). For dev/test, the container or startup routine can run `dbContext.Database.Migrate()`.

Scaffold detail & file examples

I will scaffold a minimal runnable prototype. Each created file will include tests so you can run them locally.

Backend highlights

- `Program.cs` bootstraps the Web API, EF Core + Postgres connection, API versioning, Swagger generation for versions, Microsoft.Identity.Web configuration placeholders.
- Controllers: `AuthController` (admin-only endpoints to manage projects/groups, requiring admin role), `TimesheetsController` (user endpoints for time entry), `ProjectsController` and `ProjectGroupsController` (admin API).
- `ApplicationDbContext` with EF Core entity sets.

Frontend highlights

- `package.json` includes the generator scripts for swagger-typescript-api and Vitest configuration.
- `ApiFetchAdapter` (dependency-injectable and tested) to attach Authorization header.
- Generated client placed at `src/app/api-client` and a thin Angular service wrapper around the generated client.
- Example component `timesheet/list` and `admin/projects` that use the generated client.

Tests

- Backend: xUnit tests under `tests/Api.Tests`, including example integration tests.
- Frontend: Vitest tests under `tests/Web.Tests` (or inside `src/Web/ClientApp/src`), including adapter & generated client tests.

Developer quick start (after scaffold)

Local dev using docker-compose:

```bash
git clone <repo>
cd manag-ore
docker-compose -f docker-compose.dev.yml up --build
# API will be available on http://localhost:5000, UI on http://localhost:4200 (proxy or served from wwwroot)
```

Local dev without Docker:

Backend:
```bash
cd src/Api/ManagOre.Api
dotnet restore
dotnet ef database update
dotnet run
```

Frontend (dev):
```bash
cd src/Web/ClientApp
pnpm install
# generate types from running API
pnpm run generate:api:from-url
pnpm start
```

CI commands (summary)

- Backend build + tests: `dotnet test` (with coverage) and produce `swagger/v1.json` artifact.
- Frontend: `pnpm run generate:api:ci && pnpm run test:client && pnpm run build`.
- Image build: multi-stage Docker build that copies `dist` to `wwwroot` then publish image.

Decisions for you to confirm or change before scaffolding

1) Auth provider: default to **Azure AD** with Microsoft.Identity.Web + msal-angular — confirm or propose another IdP.
2) Generated client commit policy: commit generated files to repo (offline friendly) or generate in CI only (deterministic CI)?
3) Tests and E2E: include Playwright for E2E or stick to unit/integration tests only initially?

If you confirm these, I will scaffold the *entire monorepo* now with minimal but functional implementations for each area including tests, Dockerfile, docker-compose, and GitHub Actions workflow.

If you prefer an incremental approach I can scaffold only the frontend skeleton with fetch-client generation + Vitest tests first.

Roadmap

I added a roadmap file to the repository to track steps and milestones in one place: `.github/ROADMAP.md`.
Use that file to list and update actionable tasks (ID, owner, status, estimates, dependencies). I can also create GitHub issues/PRs for each roadmap task if you prefer.

End of plan.
*** End Patch

Notes & choices

- We use `--useFetch` to make generated code rely on fetch and be simple to test and polyfill.
- Adapter keeps auth concerns separated from generated client code. The adapter should be small and unit-tested via Vitest.
- Use `msw` for more realistic tests when appropriate; `vi.stubGlobal('fetch')` is sufficient for unit tests.
- Decide whether to commit generated files to repo. Recommended: generate in CI and optionally commit for offline dev if your team prefers.

Next steps

1) Do you want me to scaffold the `src/Web` skeleton with:
   - `package.json` containing the scripts above,
   - a minimal placeholder `ApiFetchAdapter` implementation,
   - a sample generated-client placeholder and a small Vitest test file?

2) Or would you prefer I scaffold the entire monorepo (backend + frontend + Dockerfile + CI pipeline) now?

Pick which you'd like and I will proceed.
