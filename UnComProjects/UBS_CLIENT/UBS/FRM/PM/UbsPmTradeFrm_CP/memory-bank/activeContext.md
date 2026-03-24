# Memory Bank: Active Context

## Current Phase

**VAN MODE — Initialization Complete**  
Date: 2026-03-24

Memory bank created. This is a **Level 4 (Complex System)** task. Per VAN mode rules, implementation requires PLAN → CREATIVE → VAN QA → BUILD progression.

## Current Focus

- Memory bank initialized with full legacy source analysis.
- Next step: **PLAN mode** — create detailed conversion plan documents.

## What Was Just Done

- Analyzed VB6 legacy form `Pm_Trade_ud.dob` (5365 lines).
- Identified all 6 tabs, key controls, channel commands, business logic.
- Reviewed successful OP conversion references (UbsOpRetoperFrm, UbsOpCommissionFrm).
- Created all core memory bank files.

## Key Decisions Made During VAN

- **Complexity:** Level 4 — complex, multi-tab form with obligations list, sub-forms, multiple channel calls, state machine.
- **Pattern:** Follow OP conversion pattern: Constants partial + channel contract doc + phased PLAN/CREATIVE/BUILD.
- **Form rename:** `UbsForm1` → `UbsPmTradeFrm` (class and file rename required).
- **Namespace:** Current is `UbsFormTemplate` / `UbsBusiness` — should be `UbsBusiness` per UBS convention.

## Immediate Next Actions

1. Switch to **PLAN mode**.
2. Create `memory-bank/plan-trade-conversion-goals.md` — main conversion goal and phased roadmap.
3. Create `memory-bank/plan-trade-legacy-source-conversion.md` — legacy source ↔ target mapping inventory.
4. Proceed to CREATIVE phase for layout and architecture decisions.

## Open Questions / Risks

- **Sub-form strategy:** VB6 opens child windows (contract lookup, object search, account picker, instruction picker). In .NET these need to be modal dialogs or inline panels. Decision needed in CREATIVE phase.
- **ucpParam (.UbsControlProperty):** The UbsCtrlAddFields / UbsCtrlFieldsSupport equivalent — confirm which .NET control to use and how to wire it.
- **UbsCtrlDecimal vs UbsCtrlDate:** Need to confirm both are available in the existing project references or add HintPaths.
- **`EnableWindow` API calls:** VB6 uses `Declare Function EnableWindow Lib "user32"` to disable entire tab panels. .NET equivalent: set `panel.Enabled = false` recursively. Decision needed.
- **Obligation objects sub-form:** VB6 uses a child window for object selection (`GetObjectPM`). .NET approach TBD.
- **Rate calculation precision:** `txtRateCurOblig.VariantValue` used (not `.CurrencyValue`) for 10-decimal precision. `.NET UbsCtrlDecimal` must support `VariantValue`-equivalent (likely `.Value` or `.DecimalValue`).
