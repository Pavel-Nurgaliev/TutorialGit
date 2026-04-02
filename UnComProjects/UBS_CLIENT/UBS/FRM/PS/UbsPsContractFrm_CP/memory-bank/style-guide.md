# Memory Bank: Style Guide

## Authoritative rules

Project-specific conventions are defined in:

- `.cursor/rules/style-rule.mdc` — explicit constants for commands/params, `Ubs_ShowError`, XML doc for channel, `TargetFramework` / references.
- `.cursor/rules/designer-rules.mdc` — `panelMain`, `UbsFormTemplate` height, VB6 OCX → .NET control mapping.
- `.cursor/rules/array-rule.mdc` — `object[row, column]` for 2D arrays.

## Code style

- Match surrounding code in `UbsPsContractFrm` and reference projects (`UbsOpCommissionFrm_CP`, `UbsPmTradeFrm_CP`).
- Namespace `UbsBusiness` for form class; assembly/root namespace `UbsPsContractFrm` in csproj (same pattern as OP commissions).

## Documentation

- Channel contract: document all `.Run` calls and param keys in `memory-bank/creative/creative-ubspcontractfrm-channel-contract.md` (create when conversion starts).
- Avoid comments that only restate the code; document non-obvious business rules.
