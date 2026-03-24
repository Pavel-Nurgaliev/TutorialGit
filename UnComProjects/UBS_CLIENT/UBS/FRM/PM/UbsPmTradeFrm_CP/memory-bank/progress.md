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

## What Remains

- [ ] PLAN (full): `plan-trade-conversion-goals.md` + `plan-trade-legacy-source-conversion.md`
- [ ] CREATIVE: Architecture decisions (sub-forms, obligations model, tab-disable)
- [ ] Phase 2 Conversion: InitDoc, ListKey, FillCombos, all event handlers, Save logic
- [ ] Phase 3 Post: Partials, reflection, archive
