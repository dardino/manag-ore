<!--
  Pull Request template for ManagOre
  Enforces the branch-per-issue rule and ensures PRs reference and close relevant issues.
-->

## 🧩 Overview

<!-- Describe the goal of this change in 1-2 sentences -->


## Related Issue

- Issue ID: e.g. `T-001` or `M1` — please include the original issue link below.
- Closing keyword: when ready to have the issue auto-closed on merge, include `Closes #<issue-number>` in this description.


## Type of change
- [ ] Bug fix (non-breaking change)
- [ ] New feature (non-breaking change)
- [ ] Breaking change
- [ ] CI / docs / tooling


## Branch naming (mandatory)

Please create your branch from `main` (or the correct target branch) using one of these forms:

- `issue/<ISSUE-ID>-short-description` (e.g. `issue/T-001-scaffold-timesheets`)
- `feat/<ISSUE-ID>-short-description` or `fix/<ISSUE-ID>-short-description`

If you used the issue creation helper, a suggested branch name is appended to the issue body (use that exact slug).


## Checklist (required)
- [ ] I followed the branch naming convention and created a branch from the correct target branch
- [ ] I added/updated tests for the changes and they pass locally
- [ ] CI checks pass for this branch
- [ ] I included a short description in the PR title and the `Related Issue` section above


## Additional notes for reviewers

- Anything the reviewer should pay attention to (migrations, manual steps, environment variables, etc.)
