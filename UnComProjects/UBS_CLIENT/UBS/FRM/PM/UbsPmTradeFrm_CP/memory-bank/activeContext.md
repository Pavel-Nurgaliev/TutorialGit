# Memory Bank: Active Context

## Current Phase — Phase 2; InitDoc full conversion complete

**Date:** 2026-03-27

Designer phase (v2 + TabIndex) is done. **InitDoc is now fully converted** from VB6 to C# with full EDIT/ADD parity.

## Current Focus

**BUILD (Phase 2 — remainder):** Save / `ModifyTrade`, remaining event handlers, sub-form pickers, `LoadFromParams` for non-InitDoc flows.

## What Was Just Done

- **InitDoc full conversion** (`creative-initdoc-full-conversion.md`):
  - EDIT branch: `GetOneTrade` output consumed — all trade fields mapped to controls (DATE_TRADE, NUM_TRADE, IS_COMPOSIT, combos, contracts, payment instructions, NDS/Export/ExternalStorage flags).
  - `FillControlStorage`: new method loading storage code/name from channel.
  - `FillListOblig`: new method populating obligation ListView from 2D array with rate computation.
  - `FillControlContract` for seller/buyer via `FillContractRowFromPm` + TYPE_CONTRACT extraction.
  - Contract type combos set from server data (suppressed events).
  - `FillObligPM` → `FillListOblig` with obligation list fill.
  - Payment instructions filled for both buyer and seller.
  - Cash register logic: BIK/RS comparison for `chkCash` display.
  - NDS/Export overrides from server flags after `ApplyContractType`.
  - ADD branch: default precious metal (1001), contract type combos = index 0, max part numbers = 0, date = today.
  - Common tail: read-only controls, chkComposit visibility, cmbTradeDirection state, tab enable, `GetRate_CB()`, `GetRateForPM()`.
  - `LockUiOnWasOperation` refined to per-panel granularity matching VB6 `EnableWindow` calls.
  - `FillOurBIK` fixed to store `m_ourBIK` field instead of writing text boxes.
  - New fields: `m_ourBIK`, `m_idBaseCurrency`, `m_wasOperation`, `m_maxNumPart1`.
  - New methods: `FillControlStorage`, `FillListOblig`, `GetRate_CB`, `GetRateForPM`.

## Immediate Next Actions

1. **BUILD:** `BuildSaveParams` / `ModifyTrade` (Save flow)
2. **CREATIVE:** sub-forms (contract / instruction / account / object / storage pickers)
3. **BUILD:** remaining event handlers (`txtTradeDate_LostFocus`, `ucpParam_TextChange`, obligation add/edit/delete)

## Open Questions / Risks

- Sub-form strategy (contract / instruction / account / object / storage pickers) — CREATIVE.
- `Обязательства сделки2` parameter copying into `m_paramOblig` dictionary — not implemented (format unknown).
- Full build verification requires .NET Framework 2.0 targeting pack.
