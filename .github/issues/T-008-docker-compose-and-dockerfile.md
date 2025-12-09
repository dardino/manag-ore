Title: T-008 — Dockerfile multi-stage & docker-compose.dev
Labels: infra, task
Milestone: M7

Descrizione

Creare il Dockerfile multi-stage che builda il frontend (Node) e pubblica il backend (dotnet), quindi copia il `dist` in `wwwroot`. Creare `docker-compose.dev.yml` con Postgres per dev.

Deliverables

- `Dockerfile` multi-stage
- `docker-compose.dev.yml` con servizi `app` e `postgres`
- Smoke test script per immagine
