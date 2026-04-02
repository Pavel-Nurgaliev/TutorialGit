# Memory Bank: System Patterns

## Architecture

- **Shell integration:** Form implements `IUbs` via `Ubs_AddName` / `UbsDelegate` handlers (e.g. `CommandLine`, `ListKey`).
- **Channel:** `IUbsChannel.LoadResource` points to ASM business resource; `Run(command)` with `ParamIn`/`ParamOut` — document every call in creative channel contract.
- **Fields:** `IUbsFieldCollection` / `UbsFormField` for DDX-style binding where applicable; `UbsCtrlFields` / add-fields for dynamic columns per legacy `ucpParam` patterns.

## Conventions (this repo)

- Explicit string literals for **command names** and **param keys** only inside **constants** partial (see `style-rule.mdc`).
- `object[,]` from channel data: **row, column** indexing in .NET (VB6 variant was column, row).
- Error handling: `try` / `catch` → `this.Ubs_ShowError(ex)` on channel and UI logic.

## Patterns in use

Follow **UbsOpCommissionFrm_CP** / **UbsPmTradeFrm_CP**: optional `Form.Constants.cs` partial, `memory-bank/creative/*-channel-contract.md`, phased PLAN → CREATIVE → BUILD → REFLECT.
