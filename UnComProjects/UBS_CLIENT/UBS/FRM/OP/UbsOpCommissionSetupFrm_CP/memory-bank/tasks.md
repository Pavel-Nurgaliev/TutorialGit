# Memory Bank: Tasks

## Current Task

**Main goal:** Convert op_ret_oper (VB6 UserDocument) → UbsOpRetoperFrm (.NET WinForm)

**Complexity:** Level 3 (intermediate)

**Plans:**
- `memory-bank/plan-retoper-conversion-goals.md` — main goal, conversion roadmap
- `memory-bank/plan-retoper-legacy-source-conversion.md` — legacy source (op_ret_oper.dob) → target (UbsOpRetoperFrm)
- `memory-bank/creative/creative-retoper-conversion-architecture.md` — layout, control mapping, data binding
- `memory-bank/creative/creative-retoper-form-layout.md` — UI/UX layout structure (Option D: Hybrid)
- `memory-bank/creative/creative-retoper-combobox-datastorage.md` — ComboBox ItemData → KeyValuePair DataSource
- `memory-bank/creative/ubsopretoperfrm-channel-contract.md` — channel commands and params

---

## Implementation Plan

### Phase 1 (Prep)

- [x] Create `UbsOpRetoperFrm.Constants.cs` — LoadResource, user-facing messages only (explicit literals for channel commands/params)
- [x] Add UbsCtrlDecimal reference to `UbsOpRetoperFrm.csproj` (C:\ProgramData\UniSAB\Assembly\Ubs\UbsCtrlDecimal.dll)
- [x] Channel contract doc created (ubsopretoperfrm-channel-contract.md)

### Phase 2 (Main Goal — Conversion)

- [x] **UI:** GroupBox "Платежный документ" — plDoc, serPDoc, numPDoc, valNom, nomPDoc (readonly)
- [x] **UI:** payMoney CheckBox, valMinusCB, sumMinusCur, rateCur, NU (row "Выдать")
- [x] **UI:** valComis, komCur, podNalCur (row "Комиссия / Подоходный налог")
- [x] **UI:** nKvit, clientTxt, docCB, serTxt, numTxt, resCHB
- [x] **UI:** Save, Exit, ubsCtrlInfo (bottom strip)
- [x] **InitDoc:** InitForm, LoadFromParams, fill valMinusCB/valComis/docCB, payMoney logic
- [x] **ListKey:** pass idOper; CheckCash; InitDoc
- [x] **valMinusCB_SelectedIndexChanged:** GetOperParam, fill rateCur/NU/komCur/valComis
- [x] **NU/rateCur change handlers:** recalc sumMinusCur, komCur
- [x] **btnSave_Click:** BuildSaveParams, Save, show "Данные сохранены!" via ubsCtrlInfo
- [x] Set LoadResource; explicit literals for channel .Run and param keys

### Phase 3 (Post-Conversion)

- [ ] Split partials when form grows (~300+ lines)
- [x] Reflection doc; update progress.md
- [x] Archiving

---

## Status

- [x] Planning complete
- [x] Creative phases complete
- [x] Implementation complete (Phase 1–2)
- [x] Reflection complete
- [x] Archiving complete

## Archive

- **Date:** 2026-03-18
- **Archive document:** [memory-bank/archive/archive-retoper-conversion.md](archive/archive-retoper-conversion.md)
- **Status:** COMPLETED

---

## Next Task

Memory Bank is ready for the next task. To start a new task, use VAN mode.
