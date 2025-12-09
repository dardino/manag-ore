Title: T-006 — Implementare ApiFetchAdapter e generator scripts
Labels: frontend, task
Milestone: M6

Descrizione

Implementare `ApiFetchAdapter` (inietta token e permette fetch swapping) e aggiungere pnpm scripts per `swagger-typescript-api` generation (from-url, from-file, ci).

Deliverables

- `src/Web/ClientApp/src/app/api-client/apiFetchAdapter.ts`
- `package.json` scripts: generate:api:from-url/from-file/ci
- Tests per l'adapter (Vitest)
