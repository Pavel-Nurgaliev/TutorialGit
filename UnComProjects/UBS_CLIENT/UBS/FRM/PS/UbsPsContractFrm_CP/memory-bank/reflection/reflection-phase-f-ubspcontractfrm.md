# Reflection: UbsPsContractFrm — Phase F (Polish)

**Task ID:** `phase-f-ubspcontractfrm`  
**Date:** 2026-04-02  
**Scope:** Tab order and focus polish (`UbsPsContractFrm.Designer.cs`), footer bar tab indices, commission label **`TabStop`**, **`UbsPsContractFrm.Keys.cs`** (Esc → previous tab), constructor **`KeyPreview`** / **`AcceptButton`**, **`UbsPsContractFrm_Load`** hybrid init.

---

## Summary

Phase F aligned **main-tab** **`TabIndex`** with a top-to-bottom flow: contract code → executor → number → contract date → payment kind link and kind fields → status → close date → recipient group → comment. **Recipient** group order follows layout: client link → client text → clear → BIK → correspondent account → bank name → INN → «Р/с» link → recipient account → address. Static **labels** use **`TabStop = false`** and high indices so they do not steal focus. **Commission** tab labels are **`TabStop = false`**. **`tblActions`** children use **0 / 1 / 2** (info, save, exit) with **`tblActions.TabIndex = 1`** next to **`tabContract.TabIndex = 0`** on **`panelMain`**.

**Keyboard:** **`UbsPsContractFrm_KeyDown`** (partial **`Keys.cs`**) maps **Esc** from **Дополнительные свойства** → **Комиссия** → **Основные**, approximating legacy tab keyboard routing without full **`UserDocument_KeyPress`** parity. **`SuppressKeyPress`** is used after handling Esc (available on **`KeyEventArgs`** in .NET Framework 2.0).

**Partials:** **`InitDoc.cs`** remains a single large partial; only **`Keys.cs`** was added as a small split. Further splits (**`.Save.cs`**, etc.) remain appropriate when Phase E save work lands.

**Verification:** **MSBuild** Debug and Release succeed (VS 2022 **MSBuild**).

---

## What Went Well

- **Scoped polish:** Designer-only tab metadata plus one small partial avoids churn in **`InitDoc`** / channel code.
- **`AcceptButton` + `KeyPreview`:** Standard WinForms patterns for Enter-to-save and form-level Esc handling.

---

## Challenges

- **WinForms tab semantics:** **`TabIndex`** on **`GroupBox`** and z-order of controls inside **`pnlMainScroll`** must stay consistent; recipient controls were renumbered as a block.
- **Legacy parity:** Full legacy keyboard and picker wiring (Enter on BIK, browse buttons vs link-only UI) is still future work (Phase B notes / Phase D–E).

---

## Lessons Learned

- After bulk **`TabIndex`** edits, run **Release** build when **`DocumentationFile`** is enabled — catches any accidental typos in designer-generated lines.
- Keep **footer** **`TabIndex`** values contiguous within **`tblActions`** so focus order matches visual left-to-right (info → save → exit).
