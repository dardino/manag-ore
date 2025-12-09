````markdown
COPILOT / Assistente — Istruzioni operative per il repo ManagOre

Scopo

Questo file contiene istruzioni operative chiare e ripetibili per gli agenti (Copilot / assistenti) e per i contributori che lavorano su questo repository.
Sono pensate per creare scaffolding, implementare feature, aggiungere test, creare build e immagini docker e aggiornare la ROADMAP.

Lingua e tono

- Risposte e commit messages devono essere in italiano (salvo richieste diverse).
- Ricorda di essere conciso, tecnico e utile.

Principi generali

- Mantieni il repository eseguibile e testato: quando aggiungi o modifichi codice, aggiungi test di unità significativi e CI passante.
- Non aggiungere segreti nei file: usare variabili d'ambiente (.env in dev, secrets in CI) e KeyVault/secret manager in produzione.
- Preferisci convenzioni standard (.NET 8+, EF Core, Angular CLI, pnpm).

Ambiente & prerequisiti

- Local dev (recommendato): macOS o Linux, docker & docker-compose, dotnet 8 SDK, Node.js (v20+), pnpm (>=7).
- Default shell: zsh — usa comandi compatibili con zsh negli esempi.

Struttura repo (rapida)

- src/Api/ManagOre.Api — backend ASP.NET Core
- src/Web/ClientApp — frontend Angular + Angular Material
- tests/Api.Tests — xUnit
- tests/Web.Tests or src/Web/ClientApp/src — Vitest tests
- Dockerfile — multi-stage per single-image
- docker-compose.dev.yml — dev-compose con Postgres
- .github/ROADMAP.md — milestone & tasks
- .github/prompts/plan-managOreScaffoldPlan.prompt.md — piano di implementazione

Regole di scaffolding / implementazione

1) Prima di scrivere codice: controlla `ROADMAP.md` e `plan-managOreScaffoldPlan.prompt.md` per capire milestones e priorità.
2) Usa la struttura stabilita: aggiungi backend in `src/Api/ManagOre.Api`, frontend in `src/Web/ClientApp`.
3) Segui queste scelte tecniche (default):
   - .NET 8, EF Core + Npgsql
   - API versioning route-based: `api/v{version}/[controller]`
   - Swagger via Swashbuckle per ogni versione
   - Authentication: Azure AD (Microsoft.Identity.Web) + msal-browser/msal-angular in frontend
   - Client generation: swagger-typescript-api --useFetch
   - Frontend package manager: pnpm
   - Frontend tests: Vitest (jsdom) and msw for more realistic tests
   - Backend tests: xUnit + TestServer or Testcontainers when DB integration required
4) Tests obbligatori: ogni feature importante ha test di unità; le integrazioni DB hanno tests d'integrazione (testcontainer / in-memory) nel job CI appropriato.

Comandi utili (developer quick start)

# backend
cd src/Api/ManagOre.Api
dotnet restore
dotnet build
dotnet ef migrations add Init
dotnet ef database update
dotnet run

# frontend
cd src/Web/ClientApp
pnpm install
pnpm run generate:api:from-url # se API in locale (o from-file / from-ci)
pnpm start

# docker-compose dev
docker compose -f docker-compose.dev.yml up --build

File e convenzioni da mantenere

- `src/Web/ClientApp/package.json` deve contenere script per:
  - generate:api:from-url
  - generate:api:from-file
  - generate:api:ci
  - test:client (Vitest)
  - build
- Generated client path: src/Web/ClientApp/src/app/api-client
- ApiFetchAdapter: src/Web/ClientApp/src/app/api-client/apiFetchAdapter.ts

CI flow (GitHub Actions)

- api-build:
  - dotnet restore / build / test
  - create swagger artifacts per versione (e.g. artifacts/swagger/v1.json)
  - upload artifact
- web-build:
  - download swagger artifact
  - pnpm install
  - pnpm run generate:api:ci
  - pnpm run test:client
  - ng build --configuration=production
  - upload dist
- image-build:
  - multi-stage Docker build that copies `dist` into wwwroot and publishes final image
  - run smoke tests and push image to registry

DB migrations strategy

- In dev: fine applicare `dbContext.Database.Migrate()` all'avvio con retry semplici.
- In CI/prod: preferibile generare SQL in CI (`dotnet ef migrations script`) e avere una fase deploy separata che applica le migrazioni con controlli/approvazioni.

Swagger / DTO generation (frontend)

- Use `swagger-typescript-api --useFetch` to generate typed clients.
- CI: backend job must expose swagger/v{version}.json artifacts for frontend job.
- Local: allow `pnpm run generate:api:from-url` to pull swagger from `http://localhost:5000/swagger/v1/swagger.json`.

Testing the generated client with Vitest

- Use `vi.stubGlobal('fetch')` for simple unit tests; prefer MSW when you need realistic network mocking.
- Test adapter separately: stub `getAccessToken` and `fetchImpl`.

Quality & code style

- Backend: follow .NET naming conventions; use nullable reference types; include XML docs for public APIs.
- Frontend: use Angular best practices (services, modules), and keep generated code separate from handwritten wrappers.
- Always run unit tests locally and in CI.

Pull requests / commits

- Use clear PR titles and descriptions; link to ROADMAP and relevant milestone.
- Small PRs > easier to review. Tests must pass.
- Commits: use imperative style in English/Italian (e.g., "Add TimesheetsController and model TimeEntry").

Note: A Pull Request template is available at `.github/PULL_REQUEST_TEMPLATE.md` — please fill it out when opening PRs.

Branch & PR rule for Issues

- Every issue MUST have a corresponding branch created from `main` (or the correct target branch) **before** starting work.
  - Branch naming convention: `issue/<ISSUE-ID>-short-description` or `feat/<ISSUE-ID>-short-description` / `fix/<ISSUE-ID>-short-description`.
  - Example: `issue/T-001-scaffold-timesheets` or `feat/T-006-apifetchadapter`.
- Work on the feature must be pushed on that branch and a Pull Request (PR) must be opened to merge into `main` (or the appropriate release branch`).
  - The PR **must reference** the issue it implements and include a closing keyword when ready to close it, e.g., `Closes #13` or `Fixes #13` in the PR description. This will automatically close the issue when the PR is merged.
  - PR title should contain the issue id and a short description, e.g. `T-001: Scaffold TimesheetsController`.
- Each PR should include at least one unit test or a clear note why tests are not applicable.
- Do not push work directly to `main` — always use a branch + PR so reviewers can review and CI runs.

Automations & helpers

- The helper script `scripts/create_github_issues.sh` appends a "Suggested branch" line to each created issue body to help you follow the branch naming convention. Use that suggested branch when starting work on the issue.
- When opening a PR, use the PR template and include `Closes #<issue-number>` (or similar) so GitHub will auto-close the issue on merge.

Se vuoi, posso:
- creare tutte le GitHub Issues per le milestone M1..M10 in `ROADMAP.md`;
- scaffoldare l'intero progetto (M1..M4) o solo il frontend (M5..M6) a seconda della priorità;
- aggiungere configurazioni di CI dettagliate per ogni job.

---

Sei pronto per la prossima azione? Scegli tra:
1) Creo le Issues per le milestone (M1..M10).
2) Scaffold completo del monorepo.
3) Scaffold frontend (ClientApp) ora.

Creazione automatica delle issue

Abbiamo aggiunto un piccolo helper per creare tutte le issue locali sul repository GitHub usando la GitHub CLI `gh`.

Per eseguire le issue creation locali sulla repository remota eseguire i seguenti comandi (assumendo `gh` autenticato):

```bash
# rendi lo script eseguibile
chmod +x scripts/create_github_issues.sh

# esegui lo script
scripts/create_github_issues.sh
```

Note: lo script usa i file Markdown in `.github/issues`.
````
