Title: M8 — DB migrations, seeding & deploy strategy
Labels: milestone, infra
Milestone: M8

Obiettivo

Definire procedura di migrazione e seeding dati. Implementare script per generare SQL dagli EF migrations e definire approccio per applicazione controllata in produzione.

Deliverables

- Script CI per `dotnet ef migrations script`
- Dev-seed script (idempotent)
- Documentazione della strategy deploy per migrazioni

Dipendenze: M2, M7

Stima: 2 giorni
