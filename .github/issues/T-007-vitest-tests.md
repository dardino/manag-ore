Title: T-007 — Partire con Vitest tests per client & adapter
Labels: frontend, test
Milestone: M6

Descrizione

Configurare Vitest e aggiungere test di unità per `ApiFetchAdapter` e alcuni test simulati contro il client generato (usando `vi.stubGlobal('fetch')` o MSW).

Deliverables

- `vitest.config.ts` + `vitest.setup.ts`
- Tests in `src/Web/ClientApp/src/app/api-client/__tests__`
