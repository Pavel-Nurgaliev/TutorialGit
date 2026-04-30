# Memory Bank: Active Context

## Current Focus

**Main goal:** VB6 `UtPayment.dob` -> .NET `UbsPsUtPaymentFrm`.

Wave 2 is nearly complete (B2.1–B2.4 done; B2.5–B2.7 remain). Wave 3 (Initialization) has substantial progress: the core initialization functions (`FillNalog`, `FillPayer`, `FillPurpose`, `FillTariff`, `FillPhone`, `CalcSumCommiss_2`, `GetBankNameACC`, `IsAutoPeriod`, `DefineRunUserForm`, `CheckPeni`, `CheckPayer`, `FindContract`, `FindContractbyId`, `AddProcInit`) are all fully implemented. The next focus is completing Wave 2 (NativeMethods) and then the remaining Wave 3 items (FillDataPayment full, ApplyInitialFormState, third-person fill).

## Status

- **VAN:** Complete
- **PLAN:** Complete and captured in `memory-bank/tasks.md`
- **CREATIVE:** Complete for current build inputs
- **BUILD:** Wave 1 complete; Wave 2 nearly complete (B2.1–B2.4 done); Wave 3 substantially progressed (15+ functions implemented)
- **REFLECT:** Designer milestone reflected; full project reflection pending on Wave 9
- **ARCHIVE:** Not started

## Current References

- Legacy sources: `legacy-form/UtPayment/UtPayment.dob`, `frmCalc.frm`, `frmCashOrd.frm`, `frmCashSymb.frm`, `modWinAPI.bas`
- Visual reference: `legacy-form/screens/`
- Pattern projects: `UbsPsUtPaymentGroupFrm_CP`, `UbsPsContractFrm_CP`

## Latest Changes

- **B2.4 complete**: Wired `Form_Load` (sets `m_isInitialized`) and `FormClosing` (guards with `CanCloseForm`).
- **15+ initialization functions fully implemented** in `UbsPsUtPaymentFrm.Initialization.cs`:
  - `CheckPeni`, `CheckPayer`, `FillPayer`, `FillNalog`, `FillPurpose`, `FillTariff`, `FillPhone`
  - `CalcSumCommiss_2`, `GetBankNameACC`, `IsAutoPeriod`, `GetDayEnd`, `DefineRunUserForm`
  - `GetIdClientFromGroupPayment`, `AddProcInit` (full), `FindContract` (full), `FindContractbyId` (full)
- Added 15 new shared state fields for commission, period, tax, and payer tracking.
- Remaining B2.5–B2.7 and Wave 3 fine-grained items (FillDataPayment, ApplyInitialFormState, third-person) are next.
