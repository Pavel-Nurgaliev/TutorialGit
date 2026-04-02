# Memory Bank: Project Brief

## Project Overview

**UbsPsContractFrm** is a Windows Forms library component for the UBS (UniSAB) system, **PS (Портфель / back-office)** area — **Contract** management. The project converts the VB6 `Contract` UserDocument (`Contract.dob`) to a **.NET Framework 2.0** WinForm that extends `UbsFormBase` and implements the UBS channel (`IUbs`, `IUbsChannel`).

**Conversion:** Legacy source = `legacy-form/Contract/Contract.dob` (VB6 UserDocument). Target = `UbsPsContractFrm` (.NET WinForm) under `UbsPsContractFrm/`. Visual reference (when present): `legacy-form/screens/`.

**Reference conversions (patterns and memory-bank usage):**

- `D:/Repositories/TutorialGit/UnComProjects/UBS_CLIENT/UBS/FRM/OP/UbsOpCommissionFrm_CP`
- `D:/Repositories/TutorialGit/UnComProjects/UBS_CLIENT/UBS/FRM/OP/UbsOpBlankFrm_CP`
- `D:/Repositories/TutorialGit/UnComProjects/UBS_CLIENT/UBS/FRM/PM/UbsPmTradeFrm_CP`

## Objectives

- Convert VB6 `Contract.dob` to `UbsPsContractFrm` with functional and visual parity where required.
- Integrate with UBS channel (`LoadResource`, documented `.Run` calls, `ParamIn`/`ParamOut` keys).
- Preserve layout per `legacy-form/screens/` and designer rules (`panelMain`, template height alignment with `UbsFormTemplate` from `TMP_CP`).
- Apply OP/PM conversion quality: **constants partial**, **channel contract** creative doc, no magic strings for commands/params/messages.

## Scope

- **Current state:** Skeleton `UbsPsContractFrm` (template stubs: `CommandLine`, `ListKey`, `btnSave`/`btnExit`, `m_addFields` example).
- **Legacy:** `Contract.dob` — large form; `LoadResource = "VBS:UBS_VBD\PS\Contract.vbs"`; initialization via `InitFormContract`; multiple channel calls (e.g. `Contract`, `ReadClient`, `ReadAcc`, `CheckKey`, …) to be inventoried during conversion.
- **Target components:** `UbsPsContractFrm.cs`, `UbsPsContractFrm.Designer.cs`, `UbsPsContractFrm.Constants.cs` (when started), optional partials as the form grows.

## Environment

- **Platform:** Windows (win32).
- **Repository root:** `D:/Repositories/TutorialGit`
- **Project path:** `UnComProjects/UBS_CLIENT/UBS/FRM/PS/UbsPsContractFrm_CP`

## Success Criteria

- Form builds targeting **.NET Framework 2.0**; integrates with UBS assemblies (`Private=False`, standard `HintPath`).
- Legacy behavior reproduced: InitDoc/load/save/validation flows per `Contract.dob` and channel scripts.
- All channel command names and param keys documented; user-facing strings and command keys live in constants partial.
- Zero linter issues on touched sources; alignment with workspace rules (`.cursor/rules/`, designer-rules, style-rule).
