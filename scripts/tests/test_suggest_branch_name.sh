#!/usr/bin/env bash
set -euo pipefail

fail() { echo "FAIL: $*" >&2; exit 1; }

make_suggested_branch() {
  local title="$1"
  local id_part
  if [[ "$title" =~ ^([TM]-?[0-9A-Za-z]+) ]]; then
    id_part="${BASH_REMATCH[1]}"
  else
    id_part="$(echo "$title" | awk '{print $1}')"
  fi

  slug=$(echo "$title" | sed -E "s/^${id_part}[[:space:]—-]*//" | tr '[:upper:]' '[:lower:]' | sed -E 's/[^a-z0-9]+/-/g' | sed -E 's/^-|-$//g')

  if [[ "$id_part" =~ ^T- ]]; then
    echo "issue/${id_part}-${slug}"
  elif [[ "$id_part" =~ ^M ]]; then
    echo "milestone/${id_part}-${slug}"
  else
    echo "issue/${id_part}-${slug}"
  fi
}

assert_eq() {
  expected="$1"; got="$2"; case="$3"
  if [[ "$expected" != "$got" ]]; then
    fail "[$case] expected '$expected' but got '$got'"
  else
    echo "ok: $case -> $got"
  fi
}

echo "Running suggested-branch tests..."

assert_eq "issue/T-001-scaffold-timesheets" "$(make_suggested_branch "T-001 Scaffold Timesheets")" "simple T-001"

assert_eq "milestone/M1-improve-api-versioning" "$(make_suggested_branch "M1 Improve API versioning")" "simple M1"

assert_eq "issue/T-abc-some-complex-title-with-punctuation" "$(make_suggested_branch "T-abc Some Complex Title — with punctuation!")" "punctuation"

assert_eq "issue/UnknownPrefix-do-some-work" "$(make_suggested_branch "UnknownPrefix Do some work")" "fallback"

echo "All suggested-branch tests passed."
