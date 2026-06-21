# PLAN: Conversion Goals — UbsPsContractFrm (summary)

**Date:** 2026-04-02  
**Detailed roadmap:** **`plan-ubspcontractfrm-conversion.md`** — phased conversion (tabs, DDX, channel, IUbs, risks).  
**Phase B (main tab):** **`plan-phase-b-main-tab.md`**.

---

## 1. Main goal

| Role | Source | Target |
|------|--------|--------|
| **Legacy** | `legacy-form/Contract/Contract.dob` | — |
| **Target** | — | `UbsPsContractFrm` (.NET 2.0 WinForm) |

**Current state:** Renamed template only; **no** Contract logic yet.

---

## 2. Legacy at a glance

- **3 tabs:** Main (contract + recipient), Commission (payer/receiver commission), Additional fields (`UbsControlProperty` / add-fields).
- **UbsDDX** binds 15 logical members to accounts, combos, money, dates, text.
- **Init:** `LoadResource` → `InitFormContract`; **EDIT:** `Contract` READ + `ReadKind` + `ReadClient` + add-fields.
- **Save:** `Contract` with `STRCOMMAND` = `READF` (uniqueness) then `ADD`/`EDIT`; many params including commission method, `STATE`, `DATECLOSE`, `nIdOI`.
- **Shell:** `UBSChild_ParamInfo("InitParamForm")` and `RetFromGrid` for client/account/kind pickers.

---

## 3. Success criteria (short)

- Parity with `Contract.dob` for Init, save, validation, and child returns.
- `memory-bank/creative/creative-ubspcontractfrm-channel-contract.md` complete.
- `UbsPsContractFrm.Constants.cs` — commands, param keys, messages.
- Designer matches `legacy-form/screens/` when present; `panelMain` preserved.

---

## 4. Phases (see detailed plan)

| Phase | Focus |
|-------|--------|
| **A** | Channel creative doc, constants, IUbs mapping, designer shell |
| **B** | Main tab + InitDoc + child pickers |
| **C** | Commission tab |
| **D** | Add-fields tab |
| **E** | Save + validation |
| **F** | Screenshots, partials, reflect |

**Complexity:** Level 4.
