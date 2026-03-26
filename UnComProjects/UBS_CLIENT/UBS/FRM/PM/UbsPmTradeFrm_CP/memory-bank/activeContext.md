# Memory Bank: Active Context

## Current Phase — Phase 2 (partial); form refactor BUILD complete

**Date:** 2026-03-26

Designer phase (v2 + TabIndex) is done. **Business logic bootstrap** is in progress: entry handlers, `InitDoc` skeleton, `chkCash`, and **channel 2D array convention** (`object[row, column]`, VB transpose documented).

## Current Focus

**BUILD (Phase 2 — remainder):** `LoadFromParams`, obligations UI, Save / `ModifyTrade`, remaining event handlers, CREATIVE decisions.

## What Was Just Done

- **BUILD (structure):** `UbsPmTradeFrm.cs` reorganized per `creative-form-refactor-regions-and-support.md`; util classes `UbsPmTradeComboUtil`, `UbsPmTradeMatrixUtil`, `UbsPmTradeObligParamUtil`; Release build OK.
- **CREATIVE (TabIndex + account control):** `memory-bank/creative/creative-trade-account-control-and-indexes.md` — per-container tab order for Tab 5; **`UbsCtrlAccount`** at TabIndex **4/6**; plans updated (`plan-trade-designer-revision.md` §10, `plan-tabindex-order.md` §11, `plan-trade-designer-conversion.md` §8, `CONVERSION-HANDOFF.md` §11).
- Partial Phase 2 implementation in `UbsPmTradeFrm.cs`: `ListKey`/`CommandLine`, `InitDoc`, `FillCombos` / `FillOurBIK`, EDIT vs ADD, `PMCheckOperationByTrade` coarse lock, `chkCash_Click` + Designer wiring.
- **2D arrays:** combos from `TradeCombo_FillPM` as `[n, 2]`; cash instruction from `GetInstructionOplataCash` as **`[0, 0..7]`** (not VB `(col, 0)` order).
- Plan/docs: `CONVERSION-HANDOFF.md`, `techContext.md`, `systemPatterns.md`, `tasks.md` updated.
- **Reflection:** `memory-bank/reflection/reflection-phase2-logic-bootstrap.md` (milestone only — full Phase 2 checklists still mostly open).

## Immediate Next Actions

1. **PLAN:** `plan-trade-conversion-goals.md` + `plan-trade-legacy-source-conversion.md`
2. **CREATIVE:** sub-forms, obligations model, tab-disable scope
3. **BUILD:** `LoadFromParams`, `BuildSaveParams`, obligations `ListView`, `cmdSave_Click`, remaining §2.10 handlers

## Open Questions / Risks

- Sub-form strategy (contract / instruction / account / object / storage pickers) — CREATIVE.
- Finer `Was_Operation` UI lock vs VB6 `EnableWindow` per panel.
- Full build verification requires .NET Framework 2.0 targeting pack / VS toolchain on contributor machines.
