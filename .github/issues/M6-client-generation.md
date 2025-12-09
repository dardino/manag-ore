Title: M6 — Client generation & unit tests
Labels: milestone, frontend
Milestone: M6

Obiettivo

Integrare `swagger-typescript-api --useFetch` nel frontend; creare `ApiFetchAdapter` che inietta token MSAL nelle richieste; aggiungere Vitest tests per adapter e alcuni casi d'uso del client.

Deliverables

- pnpm scripts: generate:api:from-url / from-file / ci
- `ApiFetchAdapter` (testable, DI-friendly)
- Vitest tests per adapter e client behaviour

Dipendenze: M3, M5

Stima: 3 giorni
