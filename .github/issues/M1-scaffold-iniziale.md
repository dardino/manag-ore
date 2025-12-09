Title: M1 — Scaffold iniziale (repo pronto-to-run)
Labels: milestone
Milestone: M1

Obiettivo

Creare lo scaffolding iniziale del repository con backend ASP.NET Core (src/Api/ManagOre.Api), frontend Angular (src/Web/ClientApp), un Dockerfile multi-stage e `docker-compose.dev.yml` che avvii Postgres e l'app per sviluppo locale.

Deliverables

- Progetto .NET minimale in `src/Api/ManagOre.Api` con Program.cs e un controller placeholder.
- Angular app in `src/Web/ClientApp` creata con Angular CLI (routing e styles) e pnpm.
- `Dockerfile` multi-stage che builda l'app e copia `dist` in wwwroot.
- `docker-compose.dev.yml` con Postgres configurato e variabili d'ambiente.

Dipendenze: nessuna

Stima: 3 giorni
