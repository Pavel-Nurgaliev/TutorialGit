# PLAN: Retoper Conversion Goals (Main Goal)

**Date:** 2026-03-17  
**Project:** UbsOpRetoperFrm_CP

---

## 1. MAIN GOAL

**Convert the VB6 op_ret_oper UserDocument to .NET Windows Form** — same pattern as Blank_ud → UbsOpBlankFrm and Commission_ud → UbsOpCommissionFrm.

| Role | Source | Target |
|------|--------|--------|
| **Legacy source** | `legacy-form/OP_ret_oper/op_ret_oper.dob` (VB6 UserDocument) | — |
| **Conversion target** | — | `UbsOpRetoperFrm` (.NET WinForm) in this project |

**Current state:** UbsOpRetoperFrm is a template/skeleton (panelMain, btnSave, btnExit, ubsCtrlInfo). It does **not** yet implement the op_ret_oper UI, channel calls, or business logic.

**Appearance reference:** `legacy-form/screens/image1.png` — "Выплата возмещения (возврат) клиенту"

---

## 2. NON-GOALS (Out of Scope for This Conversion)

- Converting other OP forms.
- Changing server/VBS contract beyond mapping VB6 → .NET resource path.
- Full UBSChild host integration (only form behavior and channel calls required for parity).
- Client lookup button (Command1 "..." — disabled in legacy; stub acceptable).

---

## 3. LEGACY SOURCE INVENTORY (op_ret_oper.dob)

**Location:** `legacy-form/OP_ret_oper/op_ret_oper.dob` (VB6 UserDocument)

### UI

- **Frame "Платежный документ"** (readonly, Enabled=False): plDoc (Наименование), serPDoc (Серия), numPDoc (Номер), valNom (Валюта номинала), nomPDoc (Сумма по номиналу).
- **CheckBox payMoney:** "Выплатить в денежном эквиваленте" — toggles valMinusCB enable/disable.
- **Row "Выдать":** valMinusCB (ComboBox), sumMinusCur (UbsControlMoney), rateCur, NU (UbsControlMoney), labels "к".
- **Row "Комиссия / Подоходный налог":** valComis (ComboBox), komCur (UbsControlMoney), podNalCur (UbsControlMoney).
- **Row "Номер квитанции":** nKvit (UbsControlMoney, readonly).
- **Row "Клиент":** clientTxt (TextBox readonly), Command1 ("..." disabled).
- **Row "Документ":** docCB, serTxt, numTxt (all readonly).
- **CheckBox resCHB:** "Резидент" (readonly).
- **Buttons:** SaveBtn ("Сохранить"), btnExit ("Выход").
- **Info:** UbsInfo — "Данные сохранены!" after save.

### Control Mapping (VB6 → .NET)

| VB6 | .NET |
|-----|------|
| UBSCTRLLibCtl.UbsControlMoney (8 instances) | UbsControl.UbsCtrlDecimal() |
| UbsInfo.ocx | UbsControl.UbsCtrlInfo |
| VB.Frame | GroupBox |
| VB.TextBox | TextBox |
| VB.ComboBox | ComboBox (DropDownList) |
| VB.CheckBox | CheckBox |

**Note:** No date fields in op_ret_oper; UbsCtrlDate not required for this form.

### Channel

- **LoadResource:** `VBS:UBS_VBS\OP\opers.vbs` (.NET: use ASM equivalent if available).
- **Commands:** CheckCash, InitForm, GetOperParam, Save.
- **InitParamForm:** receives idOper from list; CheckCash; InitDoc (InitForm); focus valMinusCB or payMoney.

### Logic

- **NU_TextChange / rateCur_TextChange:** sumMinusCur = nomPDoc × rateCur / NU; recalc komCur if isKomPer.
- **payMoney_Click:** If unchecked → disable valMinusCB, clear sumMinusCur; else enable valMinusCB.
- **valMinusCB_Click:** GetOperParam; fill rateCur, NU, komCur (or sumKom), valComis selection.
- **SaveBtn_Click:** DDX UpdateData; Check (valMinusCB required if payMoney checked); build params; Save; show Info.
- **Keyboard:** Enter → next ctrl; Esc → prev ctrl.

---

## 4. CONVERSION TASK (Main Goal)

**Task:** Convert op_ret_oper (VB6) to UbsOpRetoperFrm (.NET WinForm)

- Implement UI: GroupBox "Платежный документ", payMoney, valMinusCB, sumMinusCur, rateCur, NU, valComis, komCur, podNalCur, nKvit, clientTxt, docCB, serTxt, numTxt, resCHB, Save, Exit, Info.
- Use **UbsCtrlDecimal** for all 8 decimal/currency fields (numPDoc, nomPDoc, nKvit, NU, rateCur, sumMinusCur, podNalCur, komCur).
- Implement InitDoc: InitForm with Id операции; LoadFromParams; fill valMinusCB, valComis, docCB; payMoney enable/disable by sidOper.
- Implement ListKey / init: pass idOper from list; CheckCash; call InitDoc.
- Implement valMinusCB_SelectedIndexChanged: GetOperParam; fill rate, NU, komCur, valComis.
- Implement NU/rateCur value change handlers: recalc sumMinusCur, komCur.
- Implement btnSave: BuildSaveParams; run Save; show "Данные сохранены!" via ubsCtrlInfo.
- Map LoadResource to OP opers path; constants for all params and commands.

**Complexity:** Level 3 (intermediate) — single form, one legacy source, multiple channel commands, business logic.

---

## 5. PHASED ROADMAP

### Phase 1 (Prep)

- [ ] Constants partial (LoadResource, param names, command names, messages).
- [ ] Channel contract doc (op_ret_oper commands and params).
- [ ] Add UbsCtrlDecimal reference to UbsOpRetoperFrm.csproj.

### Phase 2 (Main Goal — Conversion)

- [ ] Implement UI: GroupBox, labels, 8 UbsCtrlDecimal, TextBoxes, ComboBoxes, CheckBoxes, Save, Exit, Info.
- [ ] Implement InitDoc: InitForm, LoadFromParams, fill combos, payMoney logic.
- [ ] Implement ListKey / init: pass idOper; CheckCash; InitDoc.
- [ ] Implement valMinusCB_SelectedIndexChanged: GetOperParam.
- [ ] Implement NU/rateCur change handlers: recalc sumMinusCur, komCur.
- [ ] Implement btnSave: BuildSaveParams, Save, show info.
- [ ] Set LoadResource; use constants everywhere.

### Phase 3 (Post-Conversion)

- [ ] Split partials when form grows (~300+ lines).
- [ ] Reflection doc and systemPatterns update.

---

## 6. PLAN RELATIONSHIPS

| Plan | Role |
|------|------|
| **plan-retoper-conversion-goals.md** (this) | Main goal: op_ret_oper → UbsOpRetoperFrm |
| **plan-retoper-legacy-source-conversion.md** | Legacy source ↔ target mapping and inventory |
| **creative/creative-retoper-conversion-architecture.md** | Layout, control mapping, data binding decisions |

**Main goal:** Convert op_ret_oper to UbsOpRetoperFrm. Constants and channel contract are prep; the conversion is the primary deliverable.
