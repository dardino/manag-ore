Title: T-009 — CI pipeline (initial jobs build/test/generate)
Labels: ci, task
Milestone: M7

Descrizione

Creare `.github/workflows/ci.yml` con i job `api-build`, `web-build` e `image-build` che eseguono build/test/generation e pubblicano artefatti.

Deliverables

- `ci.yml` con sequenza di job: build api, produce swagger artifact, build web (generate client + tests + build), image build & push
