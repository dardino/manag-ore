Title: M7 — CI, build artifacts & single-image
Labels: milestone, ci
Milestone: M7

Obiettivo

Implementare pipeline GitHub Actions che produce artefatti swagger per le versioni API, genera client nel job web-build, corre i test, e costruisce l'immagine Docker multi-stage pronta per il deploy.

Deliverables

- `.github/workflows/ci.yml` con job: api-build, web-build, image-build
- Configurazione per creazione artefatti swagger e download nel job frontend
- Dockerfile multi-stage e script di smoke-tests

Dipendenze: M1, M6

Stima: 3 giorni
