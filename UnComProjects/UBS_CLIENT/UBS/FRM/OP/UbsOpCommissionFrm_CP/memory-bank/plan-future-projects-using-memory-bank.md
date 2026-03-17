## Plan: Future Projects Using Memory Bank

### Purpose
- Capture a reusable approach so future legacy-to-.NET conversions (and other UBS client projects) can follow the same memory-bank structure: `plan-*`, `creative-*`, `progress`, `tasks`, `reflection`.

### Candidate Future Projects
- Additional VB6 UserDocuments → WinForms conversions following the `Commission_ud → UbsOpCommissionFrm` pattern.
- Other UBS legacy forms that should adopt the `UbsBgContractFrm` / `UbsOpCommissionFrm` architecture (constants partial, channel contract, code-behind).
- Non-UI components that still benefit from phased planning (PLAN → CREATIVE → BUILD → REFLECT).

### Standard Memory-Bank Artifacts per Project
- **Project brief**: `memory-bank/projectbrief.md` — describe business goal, scope, constraints, success criteria.
- **Tech/product context**: `memory-bank/techContext.md`, `memory-bank/productContext.md` — add per-project subsections as needed.
- **System patterns**: `memory-bank/systemPatterns.md` — record reusable patterns (e.g., constants partial, channel contract, validation flow).
- **Plans** (`plan-*`):
  - Goals and acceptance criteria for the conversion/feature.
  - Mapping from legacy artifacts (forms, modules) to .NET counterparts.
  - Phased approach (Phase 1: constants/channel, Phase 2: UI & wiring, etc.).
- **Creative docs** (`creative-*`):
  - Architecture decisions, form layout notes, and design trade-offs.
  - Reusable abstractions that should be applied in later projects.
- **Tasks**: `memory-bank/tasks.md` — concrete checklist for the active project only.
- **Progress log**: `memory-bank/progress.md` — append dated entries by phase.
- **Reflection**: `memory-bank/reflection/reflection-[task_id].md` — what went well, risks, improvements; feed back into `systemPatterns.md`.

### Step-by-Step Template for a New Project
1. **Initialize brief and context**
   - Add a new subsection to `projectbrief.md` describing the new project.
   - Update `techContext.md` and `productContext.md` with any new technologies or product constraints.
2. **Define conversion/feature plan**
   - Create `memory-bank/plan-[short-project-name].md` with:
     - Main goal and non-goals.
     - Legacy sources (paths, modules, screens).
     - Target .NET artifacts (forms, classes, projects).
     - Phases (PLAN, CREATIVE, BUILD, REFLECT) aligned with existing plans like `plan-legacy-source-conversion.md`.
3. **Capture creative/architecture work**
   - Create `memory-bank/creative/creative-[short-project-name]-architecture.md`.
   - Reference and extend patterns from:
     - `creative-commission-conversion-architecture.md`
     - `creative-ubsopcommissionfrm-constants-channel.md`
4. **Drive execution with tasks and progress**
   - Use `tasks.md` for the current project’s active checklist only; clear or archive into `memory-bank/archive/` when done.
   - Log milestones and changes in `progress.md` with dates and phase tags.
5. **Reflect and feed patterns back**
   - After each project, create `memory-bank/reflection/reflection-[short-project-name].md`.
   - Summarize what should become a reusable pattern and add it to `systemPatterns.md`.

### How to Reuse for Future Legacy Form Conversions
- Start from this document and:
  - Copy `plan-conversion-goals-revised.md` and `plan-legacy-source-conversion.md` as templates.
  - Create new `plan-*` files per form or form-group.
  - Ensure every project touches all four phases (PLAN, CREATIVE, BUILD, REFLECT) and updates the shared context files instead of reinventing structure.

