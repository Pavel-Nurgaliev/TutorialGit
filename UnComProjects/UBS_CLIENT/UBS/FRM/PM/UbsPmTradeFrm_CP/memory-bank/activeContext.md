# Memory Bank: Active Context

## Current Phase — Phase 2; ~95% complete

**Date:** 2026-03-30

InitDoc fully converted. All major event handlers ported. Contract pickers wired. DDX replaced by `m_mc` dictionary. **Reflection complete.** All 3 creative docs implemented and verified. **BUILD COMPLETE — 0 compilation errors.**

## Current Focus

**BUILD COMPLETE:** All 3 creative checklists fully implemented:
1. `creative-call-oblig-lifecycle.md` — 18/18 items ✅
2. `creative-save-flow-and-validation.md` — 13/13 items ✅
3. `creative-calc-chain-events.md` — 24/24 items ✅

## What Was Just Done

- **BUILD (all phases):** Implemented 7 phases covering all remaining form logic:
  - New fields (`m_strCB`, `m_blnConvert`, `m_initialMc`, etc.) + 22 new constants
  - Helper methods: `GetMassaPrecision`, `CheckKey`, `CheckINN`, `FillKS`, `FillArrOblig`, `FillArrDataInstr`, `IsEqualNumCodeCurr`
  - 5 LostFocus handlers + 5 combo events including complex `cmbCurrencyPost_SelectedIndexChanged`
  - Full validation: `CheckData` (14 checks) + `CheckDatesOblig` (dates + RS currency)
  - Full save flow: `UpdateMcFromControls` + `SnapshotMc` + `IsMainDataChanged` + `cmdSave_Click` (ModifyTrade)
  - Sub-form pickers: instruction, storage, account, objects
  - 17 events wired in Designer.cs
  - Fixed pre-existing build issues (`var` → explicit types, collection initializers, `Ubs_ActionRun` signature)

## Implemented Since Last Reflection (2026-03-27)

| Area | Key Methods |
|------|-------------|
| Calc helpers | `GetMassaPrecision`, `UpdateObligInfoLabel`, `ExistObject`, `DefineArrStrNumInPart` |
| BIK/Key/INN | `FillKS`, `CheckKey`, `CheckINN` (3 channel calls) |
| LostFocus | `ucdMass_Leave`, `ucdCostUnit_Leave`, `ucdRateCurOblig_Leave`, `ucdCostCurOpl_Leave`, `dateTrade_Leave` |
| Combo events | `cmbObligationCurrency/CurrencyPayment/CurrencyPost/Unit_SelectedIndexChanged`, `ubsCtrlField_TextChange` |
| Validation | `CheckData` (14 checks), `CheckDatesOblig` (DefineCodCurrency channel + date loop) |
| Save flow | `UpdateMcFromControls`, `SnapshotMc`, `IsMainDataChanged`, `cmdSave_Click` (ModifyTrade) |
| Serialization | `FillArrOblig` (`object[count,12]`), `FillArrDataInstr` (`object[1,8]`), `IsEqualNumCodeCurr` |
| Sub-form pickers | `linkListInstr0/1`, `linkStorage`, `linkAccountPayment0/1`, `cmdAddObject`, `cmdDelObject` |

## Immediate Next Actions

1. **REFLECT:** Review the full build, identify remaining gaps or edge cases
2. **Phase 3 Post:** Partials split (optional), final reflection, archive

## Open Questions / Risks

- `cmbTradeType` combo items are placeholder ("тип1") — need real data from channel
- `cmdAccounts` button not present in Designer.cs — may need to be added if required
- `ucdMass.Precision` property may not exist on the actual UbsCtrlDecimal control — verify at runtime
- Full integration test requires .NET Framework 2.0 runtime + UBS channel infrastructure
