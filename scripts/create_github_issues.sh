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
    labels=$(echo "$labelsLine" | sed -e 's/Labels:[[:space:]]*//I' -e 's/,/ /g')
  fi
  if [ -n "$milestoneLine" ]; then
    milestone=$(echo "$milestoneLine" | sed -e 's/Milestone:[[:space:]]*//I')
  fi

  echo "Creating issue: $title"
  cmd=(gh issue create --title "$title" --body-file "$f")
  if [ -n "$labels" ]; then
    cmd+=(--label "$labels")
  fi
  if [ -n "$milestone" ]; then
    # gh milestone list shows available milestones; attempt to set
    cmd+=(--milestone "$milestone")
  fi

  # Run the command
  echo "+ ${cmd[*]}"
  "${cmd[@]}"
done

if [ $found -eq 0 ]; then
  echo "No issue files found in ${ISSUES_DIR}"
fi

echo "Done."
