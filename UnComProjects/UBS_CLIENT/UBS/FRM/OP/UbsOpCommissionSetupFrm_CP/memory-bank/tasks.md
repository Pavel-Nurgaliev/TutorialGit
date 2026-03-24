# Memory Bank: Tasks

## Current Task

**Main goal:** Convert Commission_Setup_ud (VB6 UserDocument) → UbsOpCommissionSetupFrm (.NET WinForm)

**Complexity:** Level 2 (straightforward — 5 ComboBox dropdowns, ADD/EDIT modes, no tabs)

---

## Implementation Plan

### Phase 1 (Prep)
- [x] VAN analysis: legacy source, reference forms, memory bank reviewed
- [x] Create `UbsOpCommissionSetupFrm.Constants.cs` — LoadResource, commands, messages
- [x] Add `Constants.cs` to `UbsOpCommissionSetupFrm.csproj`

### Phase 2 (Conversion)

- [x] **UI:** 5 ComboBox rows — lblCom/cmbCom, lblOper/cmbOper, lblVal/cmbVal, lblDiv/cmbDiv, lblCur/cmbCur
- [x] **UI:** Bottom strip — ubsCtrlInfo, btnSave, btnExit (tableLayoutPanel, Dock=Bottom)
- [x] **Designer:** Form size 440×197, Text = "Установка комиссии"
- [x] **ListKey:** extract m_id; set LoadResource; guard empty EDIT list; InitDoc; cmbCom.Focus
- [x] **InitDoc:** FillCombos → ADD: clear all, disable cmbVal; EDIT: Get_Data, SetComboByKey for all 5 combos
- [x] **FillCombos:** Run "Combo_fill"; FillCombo for Комиссии, Операции, Ценности (+Все), Кассы (+Все), Валюты (+Все)
- [x] **cmbOper_SelectedIndexChanged:** Run "Check_PayDoc"; enable/disable cmbVal based on "Документ" param
- [x] **btnSave_Click:** CheckData; build params (Действие, Идентификатор, Комиссия, Ценность, Валюта, Касса, Операция); Run "Save_Set"; check "Запись существует"; update m_id/m_command; ubsCtrlInfo.Show
- [x] **CheckData:** cmbCom required; cmbOper required

### Phase 3 (Post)
- [x] Build and verify — **0 warnings, 0 errors** (MSB3073 PostBuild copy is deploy-only, expected in dev)
- [x] Reflection doc; update progress.md (`memory-bank/reflection/reflection-ubs-op-commission-setup.md`, `memory-bank/progress.md`)
- [ ] Archiving

---

## Channel Contract

- **LoadResource:** `VBS:UBS_VBD\OP\Commission_Setup.vbs`
- **Combo_fill** → out: Комиссии[2,N], Операции[2,N], Ценности[2,N], Кассы[2,N], Валюты[2,N]
  - Array layout: `arr.GetValue(0,n)`=ID, `arr.GetValue(1,n)`=Text, `arr.GetLength(1)`=count
- **Get_Data** → in: Идентификатор; out: Комиссия, Ценность, Валюта, Касса, Операция (IDs)
- **Save_Set** → in: Действие (ADD/EDIT), Идентификатор (EDIT only), Комиссия, Ценность, Валюта, Касса, Операция; out: Запись существует (bool), Идентификатор (new ID on ADD)
- **Check_PayDoc** → in: Операция; out: Документ (bool) — controls cmbVal.Enabled

---

## Status

- [x] VAN analysis complete
- [x] Planning complete (implementation plan in this file)
- [x] Implementation complete (Phase 1–2)
- [x] Build verification — clean (2026-03-18)
- [x] Reflection complete (2026-03-24) — `memory-bank/reflection/reflection-ubs-op-commission-setup.md`
- [ ] Archiving

## Reflection highlights

- **What went well:** Channel contract documented early; reuse of `UbsFormBase` / `ListKey` / `FillCombo` patterns; UI scope held to five combos + bottom strip.
- **Challenges:** `m_filling` guard for combo events; empty EDIT list handling; value combo enable/disable and save params aligned with `Check_PayDoc` / `Документ`.
- **Lessons learned:** Treat combo side-effect guards as part of the plan; distinguish dev PostBuild copy issues from compile failures.
- **Next steps:** Run `/archive`; optional live-channel smoke tests in target environment.
