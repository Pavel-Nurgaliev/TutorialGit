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

- [ ] Create `UbsOpRetoperFrm.Constants.cs` — LoadResource, param names, command names, messages
- [ ] Add UbsCtrlDecimal reference to `UbsOpRetoperFrm.csproj` (C:\ProgramData\UniSAB\Assembly\Ubs\UbsCtrlDecimal.dll)
- [ ] Channel contract doc created (ubsopretoperfrm-channel-contract.md)

### Phase 2 (Main Goal — Conversion)

- [ ] **UI:** GroupBox "Платежный документ" — plDoc, serPDoc, numPDoc, valNom, nomPDoc (readonly)
- [ ] **UI:** payMoney CheckBox, valMinusCB, sumMinusCur, rateCur, NU (row "Выдать")
- [ ] **UI:** valComis, komCur, podNalCur (row "Комиссия / Подоходный налог")
- [ ] **UI:** nKvit, clientTxt, docCB, serTxt, numTxt, resCHB
- [ ] **UI:** Save, Exit, ubsCtrlInfo (bottom strip)
- [ ] **InitDoc:** InitForm, LoadFromParams, fill valMinusCB/valComis/docCB, payMoney logic
- [ ] **ListKey:** pass idOper; CheckCash; InitDoc
- [ ] **valMinusCB_SelectedIndexChanged:** GetOperParam, fill rateCur/NU/komCur/valComis
- [ ] **NU/rateCur change handlers:** recalc sumMinusCur, komCur
- [ ] **btnSave_Click:** BuildSaveParams, Save, show "Данные сохранены!" via ubsCtrlInfo
- [ ] Set LoadResource; use constants everywhere

### Phase 3 (Post-Conversion)

- [ ] Split partials when form grows (~300+ lines)
- [ ] Reflection doc; update progress.md

---

## Next Steps

- **Creative phase complete** — architecture and channel contract documented
- **Proceed to** `/build` command for Phase 1–2 implementation
