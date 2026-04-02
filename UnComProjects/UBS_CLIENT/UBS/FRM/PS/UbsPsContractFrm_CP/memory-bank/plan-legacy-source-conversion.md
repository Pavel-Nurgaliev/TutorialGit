# PLAN: Legacy Source — Contract

**Legacy source:** `legacy-form/Contract/Contract.dob` (VB6 UserDocument)  
**Target:** `UbsPsContractFrm` (.NET WinForm) in `UbsPsContractFrm/`  
**Date:** 2026-04-02

---

## 1. Roles

| Role | Path / description |
|------|--------------------|
| **Legacy source** | `legacy-form/Contract/Contract.dob` — form to convert from. |
| **Conversion form** | `UbsPsContractFrm` — .NET WinForm in this repo. |
| **Visual reference** | `legacy-form/screens/` — place legacy UI screenshots here when available. |

---

## 2. Implications

- VB6 **UserDocument** maps to a **WinForm** derived from `UbsFormBase` (not ActiveX document host).
- **LoadResource** in legacy uses `VBS:UBS_VBD\PS\Contract.vbs`; .NET must use the **ASM** resource path agreed with the PS assembly (document in channel contract when known).
- **Main goal** is implementing Contract behavior in `UbsPsContractFrm`, not only renaming the template.

---

## 3. Inventory status

**Pending:** Full control list, tab structure, and complete `UbsChannel.Run` catalog from `Contract.dob` (see `plan-conversion-goals-revised.md`).

---

## 4. Next steps

1. Complete legacy inventory.
2. Add screenshots under `legacy-form/screens/` and update `plan-form-appearance-legacy-screens.md`.
3. Author `memory-bank/creative/creative-ubspcontractfrm-channel-contract.md`.
