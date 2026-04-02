# PLAN: Legacy Source — Retoper Conversion Form

**Legacy source:** `legacy-form/OP_ret_oper/op_ret_oper.dob` (VB6 UserDocument)  
**Target:** `UbsOpRetoperFrm` (.NET WinForm) in this project  
**Date:** 2026-03-17

---

## 1. ROLES

| Role | Path / description |
|------|--------------------|
| **Legacy source** | `legacy-form/OP_ret_oper/op_ret_oper.dob` (VB6 UserDocument) — the form to convert from. |
| **Appearance reference** | `legacy-form/screens/image1.png` — "Выплата возмещения (возврат) клиенту" |
| **Conversion form (target)** | `UbsOpRetoperFrm` (.NET WinForm) in this project — the converted form. |

---

## 2. IMPLICATIONS

- **Legacy source** = op_ret_oper.dob (VB6). **Target** = UbsOpRetoperFrm (.NET) in this project.
- **Main goal:** Convert op_ret_oper → UbsOpRetoperFrm (same pattern as Blank_ud → UbsOpBlankFrm).
- **Constants and channel contract** are prep; the **conversion** (implementing op_ret_oper UI/logic in UbsOpRetoperFrm) is the primary deliverable.
- **Control rule:** Decimal/currency values → `UbsControl.UbsCtrlDecimal()`; date values → `UbsControl.UbsCtrlDate()` (no date fields in op_ret_oper).

---

## 3. LEGACY SOURCE INVENTORY (op_ret_oper.dob)

### UI Controls

| VB6 Control | Name | .NET Replacement | Notes |
|-------------|------|-------------------|-------|
| VB.Frame | Frame1 "Платежный документ" | GroupBox | Enabled=false |
| VB.TextBox | plDoc | TextBox | Readonly |
| VB.TextBox | serPDoc | TextBox | Readonly |
| VB.TextBox | valNom | TextBox | Readonly |
| UbsControlMoney | numPDoc | UbsCtrlDecimal | Номер платежного документа |
| UbsControlMoney | nomPDoc | UbsCtrlDecimal | Сумма по номиналу |
| VB.CheckBox | payMoney | CheckBox | "Выплатить в денежном эквиваленте" |
| VB.ComboBox | valMinusCB | ComboBox | DropDownList, Валюта выданная |
| UbsControlMoney | sumMinusCur | UbsCtrlDecimal | Сумма выданная, readonly |
| UbsControlMoney | rateCur | UbsCtrlDecimal | Курс, readonly |
| UbsControlMoney | NU | UbsCtrlDecimal | Курс за(единиц), readonly |
| VB.ComboBox | valComis | ComboBox | DropDownList, Валюта комиссии |
| UbsControlMoney | komCur | UbsCtrlDecimal | Комиссия |
| UbsControlMoney | podNalCur | UbsCtrlDecimal | Подоходный налог |
| UbsControlMoney | nKvit | UbsCtrlDecimal | Номер квитанции, readonly |
| VB.TextBox | clientTxt | TextBox | Readonly |
| VB.CommandButton | Command1 | Button | "..." disabled, stub |
| VB.ComboBox | docCB | ComboBox | Readonly |
| VB.TextBox | serTxt | TextBox | Readonly |
| VB.TextBox | numTxt | TextBox | Readonly |
| VB.CheckBox | resCHB | CheckBox | Резидент, readonly |
| VB.CommandButton | SaveBtn | Button | Сохранить |
| VB.CommandButton | btnExit | Button | Выход |
| UbsInfo | Info | UbsCtrlInfo | Status |

### DDX Param Mapping (17 members)

| Param Key | VB6 Control | Type | .NET Control |
|-----------|-------------|------|--------------|
| ФИО | clientTxt | String | TextBox |
| Документ | docCB | String | ComboBox |
| Комиссия | komCur | Currency | UbsCtrlDecimal |
| Номер квитанции(справки) | nKvit | Currency | UbsCtrlDecimal |
| Номинал | nomPDoc | Currency | UbsCtrlDecimal |
| Курс за(единиц) | NU | Currency | UbsCtrlDecimal |
| Номер платежного документа | numPDoc | Currency | UbsCtrlDecimal |
| Номер документа | numTxt | String | TextBox |
| Платежный документ | plDoc | String | TextBox |
| Подоходный налог | podNalCur | Currency | UbsCtrlDecimal |
| Курс | rateCur | Currency | UbsCtrlDecimal |
| Резидент | resCHB | Integer | CheckBox |
| Серия платежного документа | serPDoc | String | TextBox |
| Серия документа | serTxt | String | TextBox |
| Сумма выданная | sumMinusCur | Currency | UbsCtrlDecimal |
| valMinusCB | valMinusCB | Integer | ComboBox (ItemData) |
| Валюта номинала | valNom | String | TextBox |

### Channel

| Item | Value |
|------|-------|
| LoadResource | `VBS:UBS_VBS\OP\opers.vbs` |
| Commands | CheckCash, InitForm, GetOperParam, Save |
| InitParamForm | idOper from list; CheckCash; InitDoc (InitForm) |
| Title | "Операция возмещения" | 

---

## 4. CONVERSION TARGET

- **Target:** `UbsOpRetoperFrm` (.NET WinForm) in this project.
- **Conversion steps:** See `memory-bank/plan-retoper-conversion-goals.md` Phase 2.
- **Reference projects:** UbsOpCommissionFrm_CP, UbsOpBlankFrm_CP (structure, memory-bank usage).

---

## 5. RECOMMENDED NEXT STEPS

1. **Execute conversion** — Implement op_ret_oper behavior in UbsOpRetoperFrm (Phase 2 in plan-retoper-conversion-goals.md).
2. **Channel contract** — Document CheckCash, InitForm, GetOperParam, Save; param names; LoadResource.
3. **Constants** — LoadResource and user-facing messages only. Channel commands and params: use explicit strings in code (e.g. `"Save"`, `"Id операции"`).

---

## 6. CHANNEL IMPLEMENTATION RULE

**Explicit literals:** `UbsChannel_Run` and `UbsChannel_ParamIn`/`ParamOut` keys must use explicit string literals in code, not constants. Example: `base.UbsChannel_Run("Save")`, `base.UbsChannel_ParamIn("Id операции", m_idOper)`.

---

## 7. RELATION TO OTHER PLANS

- **plan-retoper-conversion-goals.md** — Main goal and phased roadmap (op_ret_oper → UbsOpRetoperFrm).
- **creative-retoper-conversion-architecture.md** — Layout, control mapping, data binding decisions.
