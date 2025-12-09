Title: M3 — API design, versioning & documentation
Labels: milestone
Milestone: M3

Obiettivo

Implementare endpoints CRUD per risorse chiave (Timesheets, Projects, ProjectGroups), configurare API versioning con route `api/v{version}` e abilitare Swashbuckle per documentare per-versione le API.

Deliverables

- Controllers CRUD in `src/Api/ManagOre.Api/Controllers`.
- API versioning configurata e ApiExplorer registrato.
- Swagger UI con entrypoint per ogni versione (es. `/swagger/v1/swagger.json`).

Dipendenze: M2

Stima: 3 giorni
