# Cursor workflow prompts (Memory Bank + isolation rules)

**Purpose:** Copy-paste prompts for **new projects** that reuse the same workflow as this repo’s `.cursor/commands` (`van`, `plan`, `creative`, `build`, `reflect`, `archive`). Use with `@CONVERSION-HANDOFF.md` or your own handoff when doing domain work.

**Source:** Derived from  
`UnComProjects/UBS_CLIENT/UBS/FRM/PM/UbsPmTradeFrm_CP/.cursor/commands/*.md`

**Convention:** Replace `{PROJECT_ROOT}` with the folder that contains `.cursor/` and `memory-bank/` (must use `memory-bank/` only under that path per project rules).

---

## Pipeline (order)

```
VAN → PLAN → [CREATIVE if flagged] → BUILD → REFLECT → ARCHIVE → VAN …
```

| Complexity | Typical path |
|------------|----------------|
| Level 1 | VAN → BUILD → (light REFLECT) → ARCHIVE |
| Level 2–4 | VAN → PLAN → optional CREATIVE → BUILD → REFLECT → ARCHIVE |

---

## Shared constraints (every phase)

- Memory Bank lives in **`memory-bank/`** at project root: `tasks.md`, `activeContext.md`, `progress.md`, `projectbrief.md`, etc.
- Creative docs: **`memory-bank/creative/creative-{feature}.md`**
- Reflection: **`memory-bank/reflection/reflection-{task_id}.md`**
- Archive: **`memory-bank/archive/archive-{task_id}.md`**
- Rules path (this template): `{PROJECT_ROOT}/.cursor/rules/isolation_rules/`

---

## 1) VAN — initialize & classify

**When:** New task, empty or stale context, or you need complexity (1–4) and routing.

**In Cursor:** `/van` + your task text.

**Copy-paste prompt:**

```text
You are running the VAN phase for this repo.

1) Detect platform (OS, paths, shell) and use it for all commands.
2) Ensure `memory-bank/` exists with at least: tasks.md, activeContext.md, progress.md, projectbrief.md (create minimal stubs if missing).
3) Read memory-bank/tasks.md if present. Analyze the task below and assign complexity Level 1–4 (1=trivial fix, 4=complex system).
4) Update memory-bank/tasks.md with the complexity and a one-line goal; update memory-bank/activeContext.md with current focus.
5) Route: Level 1 → say “next: BUILD”. Level 2–4 → say “next: PLAN”.

Progressive rules to follow conceptually (paths relative to repo):
- .cursor/rules/isolation_rules/main.mdc
- Core/memory-bank-paths.mdc, platform-awareness.mdc, file-verification.mdc
- visual-maps/van_mode_split/van-mode-map.mdc

Task:
<YOUR TASK DESCRIPTION>
```

---

## 2) PLAN — detailed plan

**When:** Complexity 2–4, or you need a written plan before coding.

**In Cursor:** `/plan`

**Copy-paste prompt:**

```text
You are running the PLAN phase.

1) Read memory-bank/tasks.md (complexity), memory-bank/activeContext.md, memory-bank/projectbrief.md if present. Scan the codebase for affected areas.
2) Produce an implementation plan appropriate to the level:
   - Level 2: files to touch, ordered steps, risks.
   - Level 3: components, dependencies, challenges, verification.
   - Level 4: phased plan, architecture notes, explicit phase boundaries.
3) List any areas that need a CREATIVE phase (architecture, UI/UX, algorithms, ambiguous tradeoffs).
4) Merge the plan into memory-bank/tasks.md and mark planning complete.

Progressive rules (conceptual):
- main.mdc, Core/memory-bank-paths.mdc
- visual-maps/plan-mode-map.mdc
- Level2: Level2/task-tracking-basic.mdc, workflow-level2.mdc
- Level3: Level3/task-tracking-intermediate.mdc, planning-comprehensive.mdc, workflow-level3.mdc
- Level4: Level4/task-tracking-advanced.mdc, architectural-planning.mdc, workflow-level4.mdc

If creative items exist, say “next: CREATIVE”. Else say “next: BUILD”.
```

---

## 3) CREATIVE — design decisions

**When:** PLAN flagged components needing options, pros/cons, and a recorded decision.

**In Cursor:** `/creative`

**Copy-paste prompt:**

```text
You are running the CREATIVE phase.

1) Confirm memory-bank/tasks.md shows planning complete and lists creative items. If not, stop and say “return to PLAN”.
2) For each flagged item: define constraints, produce 2–4 options with pros/cons, recommend one with rationale, add implementation notes.
3) Write memory-bank/creative/creative-<feature_name>.md per major topic; update memory-bank/tasks.md with decisions.
4) Mark creative phase complete when every flagged item is documented.

Progressive rules (conceptual):
- main.mdc, Core/memory-bank-paths.mdc
- visual-maps/creative-mode-map.mdc
- Core/creative-phase-enforcement.mdc, creative-phase-metrics.mdc
- Phases/CreativePhase/creative-phase-architecture.mdc (architecture)
- Phases/CreativePhase/creative-phase-uiux.mdc (UI/UX)
- Phases/CreativePhase/optimized-creative-template.mdc (structured template)

When done, say “next: BUILD”.
```

*Note: The bundled `creative.md` command mentions `creative-phase-algorithm.mdc`; this repo’s `Phases/CreativePhase/` folder contains the three files above only—add an algorithm rule file if you need that branch.*

---

## 4) BUILD — implement

**When:** Plan (and creative, if any) is done; you are writing code and verifying.

**In Cursor:** `/build`

**Copy-paste prompt:**

```text
You are running the BUILD phase.

1) Read memory-bank/tasks.md, memory-bank/activeContext.md, and any memory-bank/creative/creative-*.md for Level 3–4.
2) Implement per plan. Prefer minimal, focused diffs; match existing project style.
3) Test-driven gate (as in project BUILD command): for each phase or milestone in tasks.md, define success criteria, add/run tests where the project has them; do not claim phase complete if tests fail. If the repo has no test harness, state verification explicitly (build, manual steps, linter) and record results in memory-bank/progress.md.
4) Update memory-bank/tasks.md checklists and memory-bank/progress.md with what ran and outcomes.

Progressive rules (conceptual):
- main.mdc, Core/memory-bank-paths.mdc, Core/command-execution.mdc
- visual-maps/build-mode-map.mdc
- Level1: Level1/workflow-level1.mdc, optimized-workflow-level1.mdc
- Level2: Level2/workflow-level2.mdc
- Level3–4: Level3/implementation-intermediate.mdc, Level4/phased-implementation.mdc

When implementation is done, say “next: REFLECT”.
```

---

## 5) REFLECT — lessons learned

**When:** BUILD is complete; you want a structured retrospective.

**In Cursor:** `/reflect`

**Copy-paste prompt:**

```text
You are running the REFLECT phase.

1) Confirm memory-bank/tasks.md shows implementation complete. If not, say “return to BUILD”.
2) Compare result to plan (and creative docs). Note what worked, challenges, lessons, process/technical improvements.
3) Create memory-bank/reflection/reflection-<task_id>.md with at least: Summary, What went well, Challenges, Lessons learned, Process improvements, Technical improvements, Next steps (adjust depth for complexity Level 1–4).
4) Update memory-bank/tasks.md reflection status.

Progressive rules (conceptual):
- main.mdc, Core/memory-bank-paths.mdc
- visual-maps/reflect-mode-map.mdc
- Level1: Level1/quick-documentation.mdc
- Level2: Level2/reflection-basic.mdc
- Level3: Level3/reflection-intermediate.mdc
- Level4: Level4/reflection-comprehensive.mdc

When done, say “next: ARCHIVE”.
```

---

## 6) ARCHIVE — close the task

**When:** REFLECT is done; merge task narrative into long-term archive and reset for the next task.

**In Cursor:** `/archive`

**Copy-paste prompt:**

```text
You are running the ARCHIVE phase.

1) Require memory-bank/reflection/reflection-<task_id>.md. If missing, say “return to REFLECT”.
2) Create memory-bank/archive/archive-<task_id>.md. For Level 3–4 include: METADATA, SUMMARY, REQUIREMENTS, IMPLEMENTATION, TESTING, LESSONS LEARNED, REFERENCES (link reflection + creative docs).
3) Mark task COMPLETE in memory-bank/tasks.md; add archive pointer to memory-bank/progress.md; reset memory-bank/activeContext.md for the next task; clear completed checklist body from tasks.md while keeping structure (per your project’s archive rules).

Progressive rules (conceptual):
- main.mdc, Core/memory-bank-paths.mdc
- visual-maps/archive-mode-map.mdc
- Level1: Level1/quick-documentation.mdc
- Level2: Level2/archive-basic.mdc
- Level3: Level3/archive-intermediate.mdc
- Level4: Level4/archive-comprehensive.mdc

When done, say “next: VAN for new task”.
```

---

## One-shot “bootstrap a new repo like this one”

```text
Copy `.cursor/commands` and `.cursor/rules/isolation_rules` from the reference project into this repo’s `.cursor/`. Create `memory-bank/` with tasks.md, activeContext.md, progress.md, projectbrief.md. Then run VAN with the new task description and obey memory-bank-paths (all core memory files only under memory-bank/).
```

---

## Quick reference: rule load order by command

| Command | Mode map | Extra Core / Level |
|---------|----------|---------------------|
| VAN | `van_mode_split/van-mode-map.mdc` | memory-bank-paths, platform-awareness, file-verification → then Level1 or transition to PLAN |
| PLAN | `plan-mode-map.mdc` | Level2/3/4 planning + task-tracking |
| CREATIVE | `creative-mode-map.mdc` | creative-phase-enforcement, creative-phase-metrics + CreativePhase/* |
| BUILD | `build-mode-map.mdc` | command-execution + Level implementation |
| REFLECT | `reflect-mode-map.mdc` | Level reflection* |
| ARCHIVE | `archive-mode-map.mdc` | Level archive* |

---

*Last generated from `.cursor/commands` in UbsPmTradeFrm_CP: 2026-03-24.*
