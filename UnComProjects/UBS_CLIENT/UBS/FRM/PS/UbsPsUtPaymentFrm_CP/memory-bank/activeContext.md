# Memory Bank: Active Context

## Current Focus

**Main goal:** VB6 `UtPayment.dob` -> .NET `UbsPsUtPaymentFrm`.

The local .NET starter project has been renamed to `UbsPsUtPaymentFrm`. The current focus remains BUILD completion: the form-designer milestone has now been reflected, while the next work is footer/template cleanup, control/event reconciliation, and final verification once a machine with .NET Framework 2.0 targeting assemblies is available.

## Status

- **VAN:** Complete
- **PLAN:** Complete and captured in `memory-bank/tasks.md`
- **CREATIVE:** Complete for current build inputs
- **BUILD:** In progress; rename scaffold completed
- **REFLECT:** In progress for the designer milestone; full project reflection still pending
- **ARCHIVE:** Not started

## Current References

- Legacy sources: `legacy-form/UtPayment/UtPayment.dob`, `frmCalc.frm`, `frmCashOrd.frm`, `frmCashSymb.frm`, `modWinAPI.bas`
- Visual reference: `legacy-form/screens/`
- Pattern projects: `UbsPsUtPaymentGroupFrm_CP`, `UbsPsContractFrm_CP`

## Latest Changes

- Reinitialized the Memory Bank for this workspace.
- Created phased conversion task structure modeled after the branch reference commit.
- Renamed the active project scaffold from `UbsFormProject1` to `UbsPsUtPaymentFrm`.
- Created final-name solution, project, form, resource, and assembly-info files.
- Verified that no template identifiers remain under `UbsPsUtPaymentFrm/`.
- Compile verification is currently blocked by missing `.NET Framework 2.0` reference assemblies on this machine.
- Created `memory-bank/reflection/reflection-ubspsutpaymentfrm-designer.md` for the form-designer milestone.
- Recorded the intentional designer choice to use `LinkLabel` controls instead of only small `...` browse buttons for several lookup-oriented fields.
