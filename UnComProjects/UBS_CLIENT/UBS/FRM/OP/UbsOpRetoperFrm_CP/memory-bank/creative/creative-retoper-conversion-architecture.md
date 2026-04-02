# CREATIVE: op_ret_oper Layout and Behavior (Design Decisions)

**Context:** Legacy `op_ret_oper.dob` → target `UbsOpRetoperFrm`. This doc records layout, control replacement, data binding, and UX decisions.

---

## 1. Layout Strategy

**Option A — Absolute positioning (VB6-like):** Quick visual parity; brittle resizing, DPI issues.  
**Option B — WinForms layout containers (recommended):** GroupBox for "Платежный документ"; FlowLayoutPanel or TableLayoutPanel for rows; bottom strip for Save/Exit/Info. Resilient and maintainable.

**Decision:** **Option B.** Single-form layout (no TabControl — op_ret_oper has no tabs).

---

## 2. Control Replacements

- **VB.Frame** → WinForms `GroupBox` (Enabled=false for readonly block).
- **UbsControlMoney** (8 instances) → `UbsControl.UbsCtrlDecimal()` — decimal/currency input with validation.
- **UbsInfo** → `UbsControl.UbsCtrlInfo` — show "Данные сохранены!" after save.
- **UbsDDXControl** → explicit C# binding (LoadFromParams / BuildSaveParams).
- **Command1 ("...")** → stub Button (Enabled=false in legacy; keep disabled or omit).

**Note:** No date fields in op_ret_oper; UbsCtrlDate not used.

### UbsCtrlDecimal Precision/Range (from VB6)

| Control | VB6 Range | VB6 Precision | Use |
|---------|-----------|---------------|-----|
| numPDoc | 14 | 0 | Номер платежного документа |
| nomPDoc | 14 | 2 | Сумма по номиналу |
| nKvit | 14 | 0 | Номер квитанции |
| NU | 4 | 4 | Курс за(единиц) |
| rateCur | 4 | 4 | Курс |
| sumMinusCur | 14 | 2 | Сумма выданная |
| podNalCur | 14 | 2 | Подоходный налог |
| komCur | 14 | 2 | Комиссия |

Configure each UbsCtrlDecimal with equivalent precision/range if API supports it; otherwise use default and validate in code.

---

## 3. Data Binding (DDX Migration)

**Decision:** Explicit mapping — **LoadFromParams(objParamOut)** and **BuildSaveParams()**; no DDX abstraction in C#.

Map all 17 DDX members plus channel-specific params (Валюта выданная, Валюта комиссии, idClient, SID операции, Операция, etc.).

---

## 4. Channel Integration

- **LoadResource:** `VBS:UBS_VBS\OP\opers.vbs` (or ASM equivalent).
- **Commands:** CheckCash (pre-init), InitForm (load), GetOperParam (valMinusCB change), Save (btnSave).
- Param keys identical to VB6 (Russian names). Document in channel contract.
- **Explicit literals:** Use literal strings for `.Run` commands and param keys in code; do not use constants.

---

## 5. Business Logic

### payMoney CheckBox

- If **payMoney unchecked (0):** valMinusCB disabled; sumMinusCur = 0; on Save: Валюта выданная = valNom, Сумма выданная = nomPDoc, Платежный документ = idPayDoc.
- If **payMoney checked (1):** valMinusCB enabled; user selects currency; GetOperParam fills rate/NU/komCur.

### valMinusCB Selection

- Call GetOperParam with idPov, Валюта принятая (valNom), Валюта выданная (combo ItemData), Платежный документ (idPayDoc).
- Fill rateCur, NU from ParamOut; recalc sumMinusCur via NU_TextChange logic.
- Set valComis by isKomCur (810 or valMinusCB); set komCur by isKomPer (percent) or sumKom (fixed).

### NU / rateCur Change

- sumMinusCur = nomPDoc × rateCur / NU.
- If isKomPer: komCur = sumMinusCur × sumKom / 100 (when currency match).

---

## 6. Implementation Guidelines

- Add **GroupBox** "Платежный документ" with readonly TextBoxes and UbsCtrlDecimal (numPDoc, nomPDoc).
- Form-level: m_idOper, sidOper, operArr, valArr, idPayDoc, idFOper, selectedClient, isKomPer, isKomCur, sumKom.
- **InitDoc:** InitForm → LoadFromParams; fill valMinusCB, valComis, docCB; set payMoney enable/disable by sidOper (EXPERT_PAYDOC → payMoney enabled, else disabled).
- **valMinusCB_SelectedIndexChanged:** GetOperParam; fill rateCur, NU, komCur, valComis; trigger recalc.
- **NU_ValueChanged / rateCur_ValueChanged:** Recalc sumMinusCur, komCur.
- **btnSave_Click:** BuildSaveParams; run Save; show "Данные сохранены!" via ubsCtrlInfo.
- Keep user-facing strings in Constants partial; use **explicit literals** for channel commands and param keys (document in channel contract).
