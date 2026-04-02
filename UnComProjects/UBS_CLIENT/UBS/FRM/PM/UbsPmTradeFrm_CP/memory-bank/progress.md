# Memory Bank: Progress

## Status Summary

| Phase | Status | Date |
|-------|--------|------|
| VAN (Initialization) | ✅ Complete | 2026-03-24 |
| PLAN (designer v1 — DOB source) | ✅ Complete | 2026-03-24 |
| BUILD (Phase 1 Prep + Designer v1) | ✅ Complete | 2026-03-24 |
| PLAN (designer revision — screens) | ✅ Complete | 2026-03-24 |
| CREATIVE | ⬜ Not started | — |
| BUILD (Designer v2 — screen-corrected) | ✅ Complete | 2026-03-24 |
| REFLECT (designer phase) | ✅ Complete | 2026-03-24 |
| BUILD (TabIndex correction) | ✅ Complete | 2026-03-24 |
| REFLECT (TabIndex correction) | ✅ Complete | 2026-03-24 |
| ARCHIVE | ⬜ Not started | — |
| BUILD (Phase 2 logic — partial bootstrap) | ✅ In progress / milestone | 2026-03-25 |
| REFLECT (Phase 2 bootstrap) | ✅ Complete | 2026-03-25 |
| BUILD (form refactor — regions + utils) | ✅ Complete | 2026-03-26 |
| CREATIVE (8 handler docs) | ✅ Complete | 2026-03-26–27 |
| BUILD (InitDoc full + event handlers) | ✅ Complete | 2026-03-27 |
| REFLECT (Phase 2 event handlers) | ✅ Complete | 2026-03-27 |
| CREATIVE (3 docs: save + calc + oblig) | ✅ Complete | 2026-03-30 |
| BUILD (all 3 creative checklists) | ✅ Complete | 2026-03-30 |
| REFLECT (comprehensive final) | ✅ Complete | 2026-04-01 |

## Detailed Log

### 2026-03-24 — VAN Mode Initialization

- Analyzed legacy form `Pm_Trade_ud.dob` (5365 lines, 6-tab complex form).
- Identified all channel commands (14+ commands), tab structure, control inventory, business logic rules.
- Reviewed successful OP conversion references: UbsOpRetoperFrm_CP (Level 3), UbsOpCommissionFrm_CP (Level 3).
- Created full memory bank (projectbrief, productContext, techContext, systemPatterns, activeContext, progress, tasks, style-guide).
- Determined complexity: **Level 4** (complex multi-tab form).

### 2026-03-24 — PLAN (designer v1)

- Created `plan-trade-designer-conversion.md` from DOB source analysis.
- Mapped 6 tabs, all controls, anchoring strategy, control-array replacement.

### 2026-03-24 — BUILD (Phase 1 Prep + Designer v1)

- Renamed project: `UbsForm1` → `UbsPmTradeFrm` (class, files, namespace).
- Updated `.csproj`: `RootNamespace`/`AssemblyName` = `UbsPmTradeFrm`, added 3 references (UbsCtrlDecimal, UbsCtrlDate, UbsCtrlFields), PostBuildEvent.
- Wrote `UbsPmTradeFrm.Designer.cs` — 6 tabs, 100+ controls, 0 compile errors, DLL 34 KB.
- **Note:** Designer.cs file was lost after session end (Cursor workspace issue). Needs rebuild.

### 2026-03-24 — PLAN (designer revision — screen-based)

- Read all 7 legacy screen screenshots (1.png–7.png).
- Identified **major discrepancies** between DOB-based plan and actual legacy form:
  - All 6 tab names were wrong
  - Tabs 4 ("Поставка") and 5 ("Оплата") content were SWAPPED
  - Tab 1: grpTrade layout (delivery+type on same row); grpContracts layout (code/name on 2nd row)
  - Tab 3: control order changed, GroupBox names changed, massa fields side-by-side, lblObligInfo1/2 move to sub-tab 2, new accounts button
  - Tab 4 "Поставка": new title label, "Номер" label, "выбор хранилища" label; remove lblStorageNote
  - Tab 5 "Оплата": sub-tab order swapped (Buyer first), 14 new labels, txtKS on same row as txtBIK, cmdAccount right of txtRS
  - Bottom strip: simplified to 3 columns (lblAccounts/cmdAccounts → move to tabPage3)
- Created `plan-trade-designer-revision.md` with pixel coordinates and all 28 build sub-steps.

## What Is Already in Place

- **Project renamed:** `UbsPmTradeFrm.cs` + `.csproj` + `.resx` (UbsForm1 renamed).
- **Assembly references:** UbsBase, UbsChannel, UbsCollections, UbsCtrlInfo, UbsForm, UbsFormBase, UbsInterface, UbsCtrlDecimal, UbsCtrlDate, UbsCtrlFields.
- **`RootNamespace`/`AssemblyName`:** `UbsPmTradeFrm`.
- **Legacy screens analyzed:** 7 PNG screenshots covering all 6 tabs.

### 2026-03-24 — BUILD (Designer v2 — screen-corrected)

- Recreated `UbsPmTradeFrm.Designer.cs` from scratch combining both plan documents.
- Applied all screen-based corrections: correct tab names, swapped tab 4/5 content, layout fixes.
- Fixed `.csproj`: `RootNamespace`/`AssemblyName` → `UbsPmTradeFrm`; added 3 control references; fixed `UbsForm1` → `UbsPmTradeFrm` compile entries.
- Fixed `UbsPmTradeFrm.cs`: correct class name (`UbsPmTradeFrm`), namespace (`UbsPmTradeFrm`), LoadResource path.
- **Result: 0 errors, 1 XML-doc warning. DLL produced at `bin\Release\UbsPmTradeFrm.dll`.**

### 2026-03-24 — TabIndex Correction Pass

- Discovered Designer.cs had been improved by user between sessions (UbsCtrlDate, LinkLabel, larger form size).
- Audited all TabIndex values across 12 containers (~110 controls).
- Found 7 categories of errors: duplicate=15, form-wide numbering in GroupBoxes, chkCash at 0, linkBuyer/Seller at 0, display fields missing TabStop=false, container siblings both at 0, button style.
- Created `plan-tabindex-order.md` — full before/after table per container.
- Applied ~50 StrReplace edits + 10 TabStop=false additions.
- Verified: `rg 'TabIndex\s*=\s*[2-9][0-9]+'` → 0 matches.
- **Result: 0 errors. Designer.cs now has correct per-container TabIndex order.**
- Appended Addendum section to `reflection-trade-designer.md`.

### 2026-03-25 — Phase 2 logic bootstrap + reflection

- Implemented partial Phase 2 code-behind: `InitDoc`, combo fill from `TradeCombo_FillPM` (`[n,2]` row-major), `FillOurBIK`, EDIT/ADD branching, `chkCash` + `GetInstructionOplataCash` with instruction matrix **`[0, col]`** (VB6 `(fieldIdx,0)` transpose). Documented 2D conventions in `techContext.md`, `CONVERSION-HANDOFF.md`, `systemPatterns.md`, `tasks.md`.
- Reflection: `memory-bank/reflection/reflection-phase2-logic-bootstrap.md`. Full Phase 2 checklist in `tasks.md` still largely pending (`LoadFromParams`, Save, obligations UI, etc.).

### 2026-03-26 — Form refactor (CREATIVE: regions + support extraction)

- Reordered `UbsPmTradeFrm.cs`: `Обработчики команд IUbs` → `Обработчики событий — кнопки` → `— чекбоксы` → support regions (`инициализация`, `обязательства`, `оплата`).
- New internal static helpers: `UbsPmTradeComboUtil` (combo fill / `SetComboByKey`), `UbsPmTradeMatrixUtil` (2D cell readers), `UbsPmTradeObligParamUtil` (`IsObjectParamPart2Key` with prefix parameter).
- **Verification:** `MSBuild` Release, `UbsPmTradeFrm.dll` produced; no new errors (existing CS0414, CS1591).

### 2026-03-27 — CREATIVE (8 handler docs) + BUILD (InitDoc full + event handlers) + REFLECT

- **8 creative documents** produced: `creative-initdoc-full-conversion.md`, `creative-cmb-contract-type-click.md`, `creative-cmb-kind-supply-trade-click.md`, `creative-sstabs-before-tab-click.md`, `creative-chk-is-composit-click.md`, `creative-chk-nds-rate-sum-in-cur.md`, `creative-form-refactor-regions-and-support.md`, `creative-trade-account-control-and-indexes.md`.
- **InitDoc fully converted:** EDIT branch with all 10 channel commands, ADD branch with defaults, common tail (read-only controls, composite visibility, rate fetches, `LockUiOnWasOperation`).
- **All major handlers ported:** `ApplyContractType1/2Change` (commission, kind-of-supply, payment tabs, contract fields, bank-type fill), `ApplyKindSupplyUiState` (tab page visibility, obligation clearing), `chkComposit_CheckedChanged` (direction combo, reverse obligation deletion), `chkRate/SumInCurValue_CheckedChanged` (mutual exclusion), `chkNDS_CheckedChanged` → `UpdateDisplayExport`/`UpdateDisplayNDS`, `tabControl_Selecting` (full guard with `ApplyDataTabUiOnSelecting`, `SyncPaymentInstrTabsFromContractTypes`).
- **Contract pickers wired:** `linkLabel1/2_LinkClicked` with `Ubs_ActionRun`, `FillControlContract` with `out` param.
- **DDX replaced:** `m_mc` dictionary with `FulFillMainCollection` baseline.
- **Reflection:** `memory-bank/reflection/reflection-phase2-event-handlers.md`. Phase 2 estimated ~70% complete.

### 2026-03-30 — CREATIVE (save flow + calc chain)

- **2 creative documents** produced: `creative-save-flow-and-validation.md`, `creative-calc-chain-events.md`.
- **Save flow creative:** `CheckData` (14 validation checks with navigation helpers), `CheckDatesOblig` (date loop + RS currency code via DefineCodCurrency channel + IsEqualNumCodeCurr), `FillArrOblig` (serialize to `object[count, 12]` per array rule), `FillArrDataInstr` (serialize to `object[1, 8]`), DDX change detection via `m_mc` snapshot, `cmdSave_Click` full orchestration with ModifyTrade channel, `cmdExit_Click`, form cleanup. 13-item checklist.
- **Calc chain creative:** 5 LostFocus handlers with full dependency graph, 5 combo events (including complex `cmbCurrencyPost_SelectedIndexChanged` with DefineCodCurrency/PMConvert/object clearing), sub-form pickers (instruction/storage/account/objects), `FillDataObject` with dual duplicate checks and mass accumulation, `cmdDelObject_Click`, 6 helper methods. 7 new channel calls. 24-item checklist.

### 2026-03-30 — BUILD (all 3 creative checklists implemented)

- **Phase 1 (fields + constants):** Added `m_strCB`, `m_blnConvert`, `m_intSaveCurrencyPost`, `m_blnCurrencyPostClick`, `m_blnSetFocus`, `m_blnAddFlChanged`, `m_initialMc`. 22 new string constants in Constants.cs.
- **Phase 2 (helpers):** `GetMassaPrecision`, `UpdateObligInfoLabel`, `ExistObject`, `DefineArrStrNumInPart`, `CheckKey` (TradeCheckKey channel), `CheckINN` (TradeCheckINN channel), `FillKS` (FillRekv channel + BIK validation), `FillArrOblig` (→`object[count,12]`), `FillArrDataInstr` (→`object[1,8]`), `IsEqualNumCodeCurr`.
- **Phase 3 (events):** 5 LostFocus handlers (`ucdMass_Leave`, `ucdCostUnit_Leave`, `ucdRateCurOblig_Leave`, `ucdCostCurOpl_Leave`, `dateTrade_Leave`), 5 combo events (`cmbObligationCurrency`, `cmbCurrencyPayment`, `cmbCurrencyPost` with DefineCodCurrency/PMConvert/object clearing, `cmbUnit`, `ubsCtrlField_TextChange`).
- **Phase 4 (validation):** `CheckData` (14 trade-level checks with tab navigation), `CheckDatesOblig` (date loop + RS currency code matching via DefineCodCurrency channel).
- **Phase 5 (save flow):** `UpdateMcFromControls`, `SnapshotMc`, `IsMainDataChanged`, full `cmdSave_Click` (change detection, ParamIn assembly, ModifyTrade channel, error handling).
- **Phase 6 (sub-forms):** `linkListInstr0/1_LinkClicked` (TradeFillInstr channel), `linkStorage_LinkClicked`, `linkAccountPayment0/1_LinkClicked`, `cmdAddObject_Click` (filter picker + FillDataObject), `cmdDelObject_Click` (mass subtraction + conversion), `cmdAccounts_Click`.
- **Phase 7 (wiring):** 17 events wired in Designer.cs (5 Leave, 4 SelectedIndexChanged, 6 LinkClicked, 2 Click). Fixed pre-existing `var` → explicit types, collection initializer → Add() calls, `Ubs_ActionRun` signature.
- **Build verification:** 0 compilation errors. DLL produced: `UbsPmTradeFrm.dll` (100 KB).

### 2026-04-01 — REFLECT (comprehensive final)

- Produced comprehensive Level 4 reflection: `memory-bank/reflection/reflection-trade-conversion.md`.
- Reviewed full project lifecycle: 7 calendar days, ~8 sessions, 9 C# files, ~5670 total lines.
- Documented 7 key achievements (creative-first discipline, two-phase designer planning, incremental reflections, static utility extraction, suppress-flag discipline, full InitDoc parity, save flow with change detection).
- Documented 6 challenges with resolutions (file loss, 2D transposition, contract cascade, tab visibility, .NET 2.0 restrictions, CallOblig stub).
- Catalogued 11 creative documents, 4 plan documents, 4 reflection documents as knowledge transfer artifacts.
- Updated tasks.md with reflection status and highlights.

## What Remains

- [x] CREATIVE: Obligation add/edit/view lifecycle — `creative-call-oblig-lifecycle.md`
- [x] CREATIVE: Save flow + validation — `creative-save-flow-and-validation.md`
- [x] CREATIVE: Calc chain + events + pickers — `creative-calc-chain-events.md`
- [x] BUILD: Obligation lifecycle (18 items)
- [x] BUILD: Calculation chain + LostFocus handlers (24 items)
- [x] BUILD: Save flow + validation (13 items)
- [x] REFLECT: Comprehensive final reflection — `reflection-trade-conversion.md`
- [ ] Phase 3 Post: Partials split (optional), archive
