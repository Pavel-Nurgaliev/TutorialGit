# Reflection: UbsPsContractFrm - Phases D-E (Save + add-fields keyboard)

**Task ID:** `phase-d-e-ubspcontractfrm`  
**Date:** 2026-04-07  
**Scope:** Wave D-E completion review for `UbsPsContractFrm.Save.cs`, save validation parity vs `Contract.dob`, add-fields keyboard routing, and post-save UI state normalization.

---

## Summary

Phases D-E are implemented as a dedicated save flow in `UbsPsContractFrm.Save.cs` and integrated from `btnSave_Click`. The port now covers the expected sequence: arbitrary flags setup, BIK validation gate (`TryValidateBikForSave`), close-date validation, add-fields dependency checks (`CheckArbitraryAddFl` / `FillDependFields`), business-rule validation (`TryValidateContractBusinessRules`), uniqueness/read-before-write via `READF`, and final `ADD`/`EDIT` execution with legacy-style `strError`/`StrError` handling.

Keyboard behavior around add-fields is also aligned: Enter on `ucfAdditionalFields` routes to `btnSave`, Esc follows the same tab fallback chain as the form-level logic in `UbsPsContractFrm.Keys.cs` (add-fields -> commission -> main). After successful save/load transitions, `ApplyContractKindAndCodeEditability` is reused to keep editability state consistent with document mode.

**Verification check during reflection:** current git workspace has no pending code diff for this project slice, and implementation status in `tasks.md` + `progress.md` is consistent with completed Waves D-E.

---

## What Went Well

- Save logic extraction into `UbsPsContractFrm.Save.cs` reduced risk of bloating `UbsPsContractFrm.cs` and made rule ordering explicit.
- Reuse of shared validators/helpers (`TryValidateBikForSave`, `TryValidateContractBusinessRules`) keeps Enter-vs-Save behavior synchronized.
- Add-fields keyboard parity was delivered without changing the template structure (`panelMain`, tab shell, footer layout intact).
- Post-save state reconciliation through `ApplyContractKindAndCodeEditability` prevents stale enabled/disabled control states.

---

## Challenges

- Legacy save flow mixes UI checks, channel calls, and message logic; preserving call order during porting is more important than local method elegance.
- Some host-specific behaviors (`RetFromGrid` naming/history) remain outside D-E scope, so save flow had to be completed with those assumptions unchanged.
- Maintaining parity with VB6 error branching (`strError` and `StrError`) requires careful mapping to avoid losing original user-facing behavior.

---

## Lessons Learned

- For VB6-to-WinForms save ports, lock the validation and channel-call order first, then refactor into helpers; doing the opposite increases parity regressions.
- Keep keyboard parity (`Enter`, `Esc`) in the same milestone as save; users perceive them as one flow during data entry.
- Reflection is easier when `tasks.md` wave checklist and `progress.md` narrative are updated immediately after each implementation wave.

---

## Process Improvements

- Add a mini "save contract" checklist in `tasks.md` for future forms: pre-save gates, `READF`, write command, error mapping, post-save UI state.
- During each wave close, include one explicit parity note: "what is intentionally deferred" to reduce ambiguity in later reflection/archive phases.

---

## Technical Improvements (follow-up)

- Add a small manual regression script for save scenarios: ADD success, EDIT success, duplicate contract (`READF` fail), missing close date, and BIK invalid path.
- Consider extracting a lightweight result object for save outcomes (success/message/channel error) to simplify UI messaging paths without changing behavior.

---

## Next Steps

1. Run `/archive` to fold D-E reflection into milestone archive docs.
2. Keep Phase A host-integration pending item visible until command naming confirmation is closed.
3. If host API clarifications arrive, schedule a focused spike for remaining integration parity items.

---

## Reflection phase

- [x] Compared D-E implementation against `tasks.md` waves and `progress.md` entries.
- [x] Documented completed scope, known assumptions, and follow-up technical/process improvements.
