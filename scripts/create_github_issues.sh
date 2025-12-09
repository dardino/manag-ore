#!/usr/bin/env bash
set -euo pipefail

# Script: create_github_issues.sh
# Create GitHub issues from markdown files in .github/issues
# Requires: gh (GitHub CLI) configured with access to the repository

ISSUES_DIR=".github/issues"

if ! command -v gh >/dev/null 2>&1; then
  echo "ERROR: gh (GitHub CLI) is required. Install it and authenticate (gh auth login)."
  exit 1
fi

echo "Scanning ${ISSUES_DIR} for issue files..."

found=0
REPO=$(gh repo view --json nameWithOwner -q .nameWithOwner) || REPO=""
existing_titles=$(gh issue list --repo "$REPO" --state all --json title --jq '.[].title' || true)
for f in ${ISSUES_DIR}/*.md; do
  [ -e "$f" ] || continue
  found=1
  # read title line if present
  titleLine=$(sed -n '1p' "$f" || true)
  # support `Title: ...` first line
  if [[ "$titleLine" =~ ^Title:\ (.*)$ ]]; then
    title="${BASH_REMATCH[1]}"
  else
    # fallback to filename
    title="$(basename "$f")"
  fi

  labels=""
  milestone=""
  # parse labels and milestone if present in first 10 lines
  labelsLine=$(sed -n '2,6p' "$f" | grep -i '^Labels:' || true)
  milestoneLine=$(sed -n '2,8p' "$f" | grep -i '^Milestone:' || true)
  if [ -n "$labelsLine" ]; then
    # Normalize labels: remove prefix, split by comma and trim whitespace
    labels=$(echo "$labelsLine" | sed -e 's/Labels:[[:space:]]*//I' -e 's/,/\n/g' | sed -e 's/^\s*//' -e 's/\s*$//' )
  fi
  if [ -n "$milestoneLine" ]; then
    milestone=$(echo "$milestoneLine" | sed -e 's/Milestone:[[:space:]]*//I')
  fi

  # avoid creating duplicate issues with the same title
  if echo "$existing_titles" | grep -Fxq "$title"; then
    echo "Skipping existing issue with title: $title"
    continue
  fi

  echo "Creating issue: $title"
  cmd=(gh issue create --title "$title" --body-file "$f")
  if [ -n "$labels" ]; then
    # Add one --label argument per label (handles multiple labels)
    while IFS= read -r lab; do
      [ -n "$lab" ] || continue
      cmd+=(--label "$lab")
    done <<< "$labels"
  fi
  if [ -n "$milestone" ]; then
    # Add milestone only if it exists on the remote repo (gh api may not print list reliably)
    if gh api repos/${REPO}/milestones --jq '.[].title' | grep -Fxq "$milestone"; then
      cmd+=(--milestone "$milestone")
    else
      echo "Milestone '$milestone' not found in repo — creating issue without milestone assignment"
    fi
  fi

  # Run the command
  echo "+ ${cmd[*]}"
  "${cmd[@]}"
done

if [ $found -eq 0 ]; then
  echo "No issue files found in ${ISSUES_DIR}"
fi

echo "Done."
