# PLAN: Legacy Source — Contract

**Legacy source:** `legacy-form/Contract/Contract.dob` (VB6 UserDocument, `Implements UBSChild`)  
**Target:** `UbsPsContractFrm` (.NET WinForm)  
**Full conversion plan:** **`plan-ubspcontractfrm-conversion.md`**

---

## Roles

| Role | Path |
|------|------|
| Legacy | `legacy-form/Contract/Contract.dob` |
| Target | `UbsPsContractFrm/UbsPsContractFrm.cs` (+ Designer, Constants, partials) |
| Visual reference | `legacy-form/screens/` (when populated) |

---

## Notes

- VB **UserDocument** → **WinForm** + `UbsFormBase`; height/width from `UBSChild_Height` / `UBSChild_Width` guide layout.
- **LoadResource:** `VBS:UBS_VBD\PS\Contract.vbs` — .NET ASM path defined in channel creative doc.

---

## Next step

Execute phases in **`plan-ubspcontractfrm-conversion.md`** starting with Phase A (channel contract + constants).
