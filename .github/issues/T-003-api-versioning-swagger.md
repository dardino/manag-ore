Title: T-003 — Aggiungere API versioning e Swashbuckle configuration
Labels: backend, task
Milestone: M3

Descrizione

Configurare Microsoft.AspNetCore.Mvc.Versioning e ApiExplorer ed aggiornare i controller per supportare route `api/v{version}/[controller]`. Configurare Swashbuckle per generare uno swagger.json per versione.

Deliverables

- Config nel Program.cs
- Aggiornare controller con `[ApiVersion]` attributes e route con `v{version:apiVersion}`
- E2E test per verificare le route di versione
