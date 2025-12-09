Title: M2 — Core data model & persistence
Labels: milestone
Milestone: M2

Obiettivo

Definire gli entity model principali (Employee, TimeEntry, Project, ProjectGroup), implementare `ApplicationDbContext` e aggiungere il provider Postgres (Npgsql). Creare la prima migration e test di persistenza di base.

Deliverables

- Entities e DbContext in `src/Api/ManagOre.Api`.
- Pacchetti EFCore + Npgsql configurati.
- Prima migration applicabile a Postgres.

Dipendenze: M1

Stima: 2 giorni
