# PLAN: Blank Conversion Goals (Main Goal)

**Date:** 2026-03-16  
**Project:** UbsOpBlankFrm_CP

---

## 1. MAIN GOAL

**Convert the VB6 Blank_ud UserDocument to .NET Windows Form** — same pattern as Commission_ud → UbsOpCommissionFrm.

| Role | Source | Target |
|------|--------|--------|
| **Legacy source** | `legacy-form/Blank/Blank_ud.dob` (VB6 UserDocument) | — |
| **Conversion target** | — | `UbsOpBlankFrm` (.NET WinForm) in this project |

**Current state:** UbsOpBlankFrm is a template/skeleton (e.g. UbsForm1 with btnSave, btnExit, panel). It does **not** yet implement the Blank_ud UI, channel calls, or keyboard behavior.

---

## 2. NON-GOALS (Out of Scope for This Conversion)

- Converting other OP forms.
- Changing server/ASM contract beyond mapping VB6 → .NET resource path.
- Full UBSChild host integration (only form behavior and channel calls required for parity).

---

## 3. LEGACY SOURCE INVENTORY (Blank_ud.dob)

**Location:** `legacy-form/Blank/Blank_ud.dob` (VB6 UserDocument)

### UI

- **Tab 1 (main):** txbDateCalc (Дата принятия ценности), txbSer (Серия), txbNum (Номер), txbNameVal (Наименование ценности), txbKindVal (Вид ценности), cmbState (Состояние); labels for each.
- **Tab 2:** ucpParam (UbsControlProperty — dynamic add-fields).
- **Buttons:** btnExit ("Выход"), btnSave ("Сохранить").
- **Info:** UbsInfo ("Данные сохранены!").

### Channel

- **LoadResource:** `"VBS:UBS_VBD\OP\Blank.vbs"` (.NET: use ASM equivalent).
- **Commands:** Get_Data (load), Blank_Save (save).
- **Params (in/out):** Идентификатор, Дата учета, Наименование ценности, Вид ценности, Идентификатор вида, Серия, Номер, Состояние.

### Logic

- **InitParamForm:** receives ID from list; InitDoc loads via Get_Data; FillCombo by KindVal/Состояние (states 10–17).
- **btnSave_Click:** DDX UpdateData(true); set Состояние from cmbState ItemData; UbsChannel.Run "Blank_Save"; show Info.
- **Keyboard:** Enter — next control or tab2+ucpParam focus; Esc — prev control or tab1+cmbState focus.

### VB6 → .NET Mapping

- VB.UserDocument → UbsFormBase (or existing base in project).
- UBSChild / InitParamForm → IUbs ListKey/InitDoc pattern.
- SSActiveTabs → TabControl.
- UbsDDXControl → explicit LoadFromParams / BuildSaveParams (no DDX control).
- UbsControlDate → DateTimePicker or project date control.
- UbsControlProperty (ucpParam) → add-fields control + stub (UbsAddFieldsStub equivalent).
- UbsChannel.LoadResource → IUbsChannel.LoadResource (ASM path for .NET).

---

## 4. CONVERSION TASK (Main Goal)

**Task:** Convert Blank_ud (VB6) to UbsOpBlankFrm (.NET WinForm)

- Implement TabControl: tab1 = main fields (date, series, number, name, kind, state combo); tab2 = add-fields.
- Implement InitDoc: Get_Data with Идентификатор; fill controls; FillCombo (state items by KindVal/Состояние).
- Implement btnSave: read Состояние from combo; run Blank_Save; show "Данные сохранены!" (or equivalent via ubsCtrlInfo).
- Map LoadResource to OP Blank ASM path; constants for all params and commands.
- Optional: Enter/Esc keyboard behavior for tab/control navigation.

**Complexity:** Level 3 (intermediate) — single form, one legacy source.

---

## 5. PHASED ROADMAP

### Phase 1 (Prep)

- [ ] Constants partial (LoadResource, param names, command names, messages).
- [ ] Channel contract doc (Blank_ud commands and params).

### Phase 2 (Main Goal — Conversion)

- [ ] Implement UI: TabControl, txbDateCalc, txbSer, txbNum, txbNameVal, txbKindVal, cmbState, add-fields control, btnSave, btnExit, info control.
- [ ] Implement InitDoc: Get_Data, fill controls, FillCombo.
- [ ] Implement ListKey / init: pass ID from list; call InitDoc.
- [ ] Implement btnSave: build params (Состояние), run Blank_Save, show info.
- [ ] Set LoadResource to OP Blank ASM path; use constants everywhere.

### Phase 3 (Post-Conversion)

- [ ] Split partials when form grows (~300+ lines).
- [ ] Reflection doc and systemPatterns update.

---

## 6. PLAN RELATIONSHIPS

| Plan | Role |
|------|------|
| **plan-blank-conversion-goals.md** (this) | Main goal: Blank_ud → UbsOpBlankFrm |
| **plan-blank-legacy-source-conversion.md** | Legacy source ↔ target mapping and inventory |

**Main goal:** Convert Blank_ud to UbsOpBlankFrm. Constants and channel contract are prep; the conversion is the primary deliverable.
