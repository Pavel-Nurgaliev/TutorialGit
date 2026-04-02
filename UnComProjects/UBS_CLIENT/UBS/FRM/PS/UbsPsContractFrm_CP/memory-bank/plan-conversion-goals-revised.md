# PLAN: Conversion Goals — UbsPsContractFrm

**Date:** 2026-04-02

---

## 1. Main goal

**Convert the VB6 Contract UserDocument to a .NET Framework 2.0 Windows Form** in this project.

| Role | Source | Target |
|------|--------|--------|
| **Legacy source** | `legacy-form/Contract/Contract.dob` (VB6) | — |
| **Conversion target** | — | `UbsPsContractFrm` (.NET WinForm) in `UbsPsContractFrm/` |

**Current state:** `UbsPsContractFrm` is a **renamed template** (stubs only). It does **not** yet implement Contract UI, channel behavior, or `InitDoc` logic from `Contract.dob`.

---

## 2. Legacy signals (initial grep — expand during inventory)

From `Contract.dob`:

- `UbsChannel.LoadResource = "VBS:UBS_VBD\PS\Contract.vbs"`
- `UbsChannel.Run "InitFormContract", ...` during initialization
- Additional calls include (non-exhaustive): `"Contract"`, `"ReadClient"`, `"ReadAcc"`, `"ReadKind"`, `"CheckKey"`, `"CheckClientAcc"`, `"PSCheckAccounts"`, `"ReadBankBIK"`, `"SearchAccClient"`, `"CheckExistAddFieldContract"`, `"GetNameFilterKindPaym"`, …

**Task:** Produce a full ordered inventory (UI section + `InitDoc` + save/validate paths) before coding.

---

## 3. Phased roadmap

### Phase 1 — Prep

- [ ] Full legacy inventory (`Contract.dob` + `Contract.vbs` if available in repo elsewhere).
- [ ] CREATIVE: channel contract doc + constants design.
- [ ] `UbsPsContractFrm.Constants.cs` with `LoadResource` and all command/param/message keys.

### Phase 2 — Main conversion

- [ ] Designer: tabs/controls mapped per VB6 and `legacy-form/screens/`.
- [ ] `InitDoc` / load path equivalent to legacy (including `InitFormContract` behavior).
- [ ] ListKey / CommandLine / ADD-EDIT modes per legacy.
- [ ] Save and validation; all `Run` calls wired and tested.

### Phase 3 — Post-conversion

- [ ] Split partial class files if the form exceeds manageable size.
- [ ] Appearance pass vs screenshots; tabindex and keyboard behavior.

---

## 4. Related documents

| Document | Role |
|----------|------|
| `plan-legacy-source-conversion.md` | Paths and roles |
| `plan-form-appearance-legacy-screens.md` | UI match to screenshots |
| `projectbrief.md` | Stakeholders and success criteria |

**Main deliverable:** Behavioral and UI parity of **Contract.dob** in **UbsPsContractFrm**, with documented channel usage.
