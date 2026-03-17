# PLAN: Legacy Source — Conversion Form

**Legacy source:** `Commission/Commission_ud.dob` (VB6 UserDocument)  
**Target:** `UbsOpCommissionFrm` (.NET WinForm) in this project  
**Date:** 2026-03-14

---

## 1. ROLES

| Role | Path / description |
|------|--------------------|
| **Legacy source** | `Commission/Commission_ud.dob` (VB6 UserDocument) — the form to convert from. |
| **Conversion form (target)** | `UbsOpCommissionFrm` (.NET WinForm) in this project — the converted form. |

---

## 2. IMPLICATIONS

- **Legacy source** = Commission_ud.dob (VB6). **Target** = UbsOpCommissionFrm (.NET) in this project.
- **Main goal:** Convert Commission_ud → UbsOpCommissionFrm (same pattern as BG_CONTRACT: VB6 → .NET).
- **Constants, channel contract** are prep; the **conversion** (implementing Commission UI/logic in UbsOpCommissionFrm) is the primary deliverable.

---

## 3. LEGACY SOURCE INVENTORY (Commission_ud.dob)

**Legacy source:** `Commission/Commission_ud.dob` (VB6 UserDocument)

| Item | VB6 | Notes |
|------|-----|--------|
| UI | Tab 1: txbName, txbDesc; Tab 2: ucpParam | Наименование, Описание, dynamic add-fields |
| Buttons | btnExit ("Выход"), btnSave ("Сохранить") | |
| LoadResource | `VBS:UBS_VBD\OP\Commission.vbs` | .NET: ASM equivalent |
| Commands | Get_Data, Com_Save | ADD, EDIT modes |
| Params | Действие, Наименование, Описание, Идентификатор | |
| InitDoc | Load for EDIT, clear for ADD | |
| CheckData | txbName not empty | |

**Target:** UbsOpCommissionFrm — must implement the above in .NET.

---

## 4. CONVERSION TARGET

- **Target:** `UbsOpCommissionFrm` (.NET Framework 2.0 WinForm) in this project.
- **Conversion steps:** See `memory-bank/plan-conversion-goals-revised.md` Phase 2.

---

## 5. RECOMMENDED NEXT STEPS

1. **Execute conversion** — Implement Commission_ud behavior in UbsOpCommissionFrm (Phase 2 in plan-conversion-goals-revised.md).
2. **Update channel contract** — Add Commission-specific commands, params, LoadResource.
3. **Add constants** — All Commission params, commands, messages in Constants partial.

---

## 6. RELATION TO OTHER PLANS

- **plan-conversion-goals-revised.md** — Main goal and conversion roadmap (Commission_ud → UbsOpCommissionFrm).
- **plan-ubsobgoals-apply-to-op-commission.md** — Supporting goals (constants, partials) applied during/after conversion.
