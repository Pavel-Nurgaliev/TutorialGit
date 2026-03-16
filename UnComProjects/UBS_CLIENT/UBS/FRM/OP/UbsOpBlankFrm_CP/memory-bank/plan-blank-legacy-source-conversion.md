# PLAN: Legacy Source — Blank Conversion Form

**Legacy source:** `legacy-form/Blank/Blank_ud.dob` (VB6 UserDocument)  
**Target:** `UbsOpBlankFrm` (.NET WinForm) in this project  
**Date:** 2026-03-16

---

## 1. ROLES

| Role | Path / description |
|------|--------------------|
| **Legacy source** | `legacy-form/Blank/Blank_ud.dob` (VB6 UserDocument) — the form to convert from. |
| **Conversion form (target)** | `UbsOpBlankFrm` (.NET WinForm) in this project — the converted form. |

---

## 2. IMPLICATIONS

- **Legacy source** = Blank_ud.dob (VB6). **Target** = UbsOpBlankFrm (.NET) in this project.
- **Main goal:** Convert Blank_ud → UbsOpBlankFrm (same pattern as Commission_ud → UbsOpCommissionFrm).
- **Constants and channel contract** are prep; the **conversion** (implementing Blank UI/logic in UbsOpBlankFrm) is the primary deliverable.

---

## 3. LEGACY SOURCE INVENTORY (Blank_ud.dob)

| Item | VB6 | Notes |
|------|-----|--------|
| UI Tab 1 | txbDateCalc, txbSer, txbNum, txbNameVal, txbKindVal, cmbState + labels | Readonly except cmbState (when enabled) |
| UI Tab 2 | ucpParam (UbsControlProperty) | Add-fields; key "Набор параметров" or per creative doc |
| Buttons | btnExit ("Выход"), btnSave ("Сохранить") | |
| Info | UbsInfo ("Данные сохранены!") | Show after save |
| LoadResource | `VBS:UBS_VBD\OP\Blank.vbs` | .NET: ASM equivalent |
| Commands | Get_Data, Blank_Save | |
| Params (Get_Data out) | Дата учета, Наименование ценности, Вид ценности, Идентификатор вида, Серия, Номер, Состояние | |
| Params (Blank_Save in) | Идентификатор, Состояние (from cmbState ItemData) | Plus add-fields via stub |
| InitDoc | ID from InitParamForm; Get_Data; FillCombo by KindVal/Состояние | State combo items 10–17; KindVal 3/4 variants |
| Title | "Принятая ценность" | Form/window title |

**Target:** UbsOpBlankFrm must implement the above in .NET.

---

## 4. CONVERSION TARGET

- **Target:** `UbsOpBlankFrm` (.NET WinForm) in this project.
- **Conversion steps:** See `memory-bank/plan-blank-conversion-goals.md` Phase 2.

---

## 5. RECOMMENDED NEXT STEPS

1. **Execute conversion** — Implement Blank_ud behavior in UbsOpBlankFrm (Phase 2 in plan-blank-conversion-goals.md).
2. **Channel contract** — Document Get_Data, Blank_Save, param names, LoadResource.
3. **Constants** — All param names, command names, messages in Constants partial.

---

## 6. RELATION TO OTHER PLANS

- **plan-blank-conversion-goals.md** — Main goal and phased roadmap (Blank_ud → UbsOpBlankFrm).
- **plan-future-projects-using-memory-bank.md** (from UbsOpCommissionFrm_CP) — Template used to create this project’s memory-bank structure.
