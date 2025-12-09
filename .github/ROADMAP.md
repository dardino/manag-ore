ROADMAP — ManagOre

Questo file contiene la roadmap dei task e milestone principali per il progetto ManagOre. Usalo per tracciare gli step da svolgere, assegnare responsabilità e tenere aggiornato lo stato.

Formato suggerito (tutti i campi sono facoltativi ma utili):

- ID: identificativo univoco
- Titolo: breve descrizione
- Descrizione: dettagli tecnici e obiettivi
- Owner: chi è responsabile
- Stato: backlog | in-progress | blocked | review | done
- Stima: tempo stimato (giorni/ore)
- Dipendenze: altri ID da completare prima
- Note: eventuali dettagli/URL/PR
- Data completamento: YYYY-MM-DD (quando done)

Milestones principali

Questa sezione è stata aggiornata per fornire milestone più granulari e sequenziali. Ogni milestone include attività e dipendenze per rendere il progresso misurabile.

M1 — Scaffold iniziale (repo pronto-to-run)
   - ID: M1
   - Obiettivo: scaffolding completo di repository — backend (API + EF/Postgres), frontend (Angular + pnpm), Docker multi-stage e `docker-compose.dev.yml`.
   - Output: progetto clonabile e avviabile localmente.
   - Stato: backlog

M2 — Core data model & persistence
   - ID: M2
   - Obiettivo: definire gli entity model (Employee, TimeEntry, Project, ProjectGroup), ApplicationDbContext, EF Core + Npgsql e prima migration.
   - Dipendenze: M1
   - Stato: backlog

M3 — API design, versioning & documentation
   - ID: M3
   - Obiettivo: implementare controllers CRUD per risorse chiave, API versioning (`api/v{version}`), Swashbuckle per versione (es. `/swagger/v1/swagger.json`).
   - Dipendenze: M2
   - Stato: backlog

M4 — Authentication & Authorization
   - ID: M4
   - Obiettivo: integrare corporate SSO (Azure AD) con Microsoft.Identity.Web e msal-angular, implementare policies (admin/user) e role checks per endpoints admin.
   - Dipendenze: M3
   - Stato: backlog

M5 — Frontend core UI & flows
   - ID: M5
   - Obiettivo: implementare SPA Angular con pagine principali: login, timesheets (entry/list), projects & groups, admin configuration. Includere Angular Material.
   - Dipendenze: M1, M3
   - Stato: backlog

M6 — Client generation & unit tests
   - ID: M6
   - Obiettivo: integrare `swagger-typescript-api` con `--useFetch`, creare `ApiFetchAdapter`, aggiungere Vitest tests per adapter e client; generazione CI-friendly.
   - Dipendenze: M3, M5
   - Stato: backlog

M7 — CI, build artifacts & single-image
   - ID: M7
   - Obiettivo: GitHub Actions pipeline: api-build → produce swagger artifacts → web-build (generate client + tests + build) → image-build → push image. Configure multi-stage Dockerfile and docker-compose.
   - Dipendenze: M1, M6
   - Stato: backlog

M8 — DB migrations, seeding & deploy strategy
   - ID: M8
   - Obiettivo: definire strategy per migrazioni (CI-generated SQL or controlled apply), seed data scripts, and safe deploy flow for applying migrations in production.
   - Dipendenze: M2, M7
   - Stato: backlog

M9 — Observability, security hardening & production readiness
   - ID: M9
   - Obiettivo: health/readiness endpoints, logging/structured logs, metrics (optional), secrets management (KeyVault), security review and penetration checklist.
   - Dipendenze: M7, M8
   - Stato: backlog

M10 — E2E & release preparation
   - ID: M10
   - Obiettivo: add end-to-end tests (Playwright), final smoke tests (staging), release checklist, production image validation and roll-out plan.
   - Dipendenze: M7, M9
   - Stato: backlog

Esempio di task (da aggiungere/modificare via PR):

- ID: T-001
- Titolo: Scaffold backend api/v1 con controller Timesheets
- Descrizione: Creare progetto dotnet, aggiungere controller Timesheets con endpoints CRUD e modello TimeEntry
- Owner: @dev
- Stato: in-progress
- Stima: 2 giorni
- Dipendenze: M1

Uso e aggiornamento

1. Aggiungi task e milestone qui quando necessario.
2. Mantieni lo stato aggiornato (backlog → in-progress → review → done).
3. Per task lunghi, linka PR o issue nel campo Note.

Nota: questo file è una traccia collaborativa — sentiti libero di richiedere che crei issue/PR per ogni task o di usare un tracker esterno (GitHub Issues/Projects) se preferisci.
