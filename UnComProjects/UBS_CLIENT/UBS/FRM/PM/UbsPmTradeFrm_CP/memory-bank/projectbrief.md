# Memory Bank: Project Brief

## Project Overview

**UbsPmTradeFrm** is a Windows Forms component for the UBS (UniSAB) system, used for PM (Portfolio Management) trade management — **Сделка** (Trade/Deal). The project converts the VB6 `Pm_Trade_ud` UserDocument to a .NET 2.0 WinForm that integrates with the UBS channel for loading and saving trade data.

**Conversion:** Legacy source = `legacy-form/Pm_Trade/Pm_Trade_ud.dob` (VB6 UserDocument). Target = `UbsPmTradeFrm` (.NET WinForm) in `UbsPmTradeFrm/UbsForm1.cs` (to be renamed to `UbsPmTradeFrm.cs`).

## Objectives

- Convert VB6 `Pm_Trade_ud` UserDocument to .NET WinForms `UbsPmTradeFrm`.
- Integrate with UBS channel (`GetOneTrade`, `ModifyTrade`, `TradeCombo_FillPM`, and supporting commands).
- Preserve UI (6-tab layout, contracts buyer/seller, obligations list, obligation detail with sub-tabs, storage, parameters) and behavior (keyboard navigation, field enable/disable, combo fill).
- Apply same code quality as OP conversions: constants partial, channel contract doc, phased PLAN → CREATIVE → BUILD → REFLECT.

## Scope

- **Current state:** Skeleton form `UbsForm1` in `UbsPmTradeFrm/` (template with CommandLine, ListKey, btnSave, btnExit stubs).
- **Legacy source:** `Pm_Trade_ud.dob` — 5365-line VB6 UserDocument with complex multi-tab UI, DDX data binding, obligations/objects list, and multiple channel calls.
- **Target components:** `UbsPmTradeFrm.cs`, `UbsPmTradeFrm.Designer.cs`, `UbsPmTradeFrm.Constants.cs`, channel contract doc.

## Environment

- **Platform:** Windows (win32).
- **Repository root:** D:/Repositories/TutorialGit
- **Project path:** UnComProjects/UBS_CLIENT/UBS/FRM/PM/UbsPmTradeFrm_CP

## Success Criteria

- Form builds and runs; InitDoc loads trade data for EDIT; Save runs ModifyTrade and shows info message.
- All legacy param keys and commands documented; no magic strings in code (constants partial).
- Zero linter errors; structure aligned with `UbsOpRetoperFrm_CP` and `UbsOpCommissionFrm_CP` memory-bank usage.

## Reference Conversions (Successful OP Examples)

- `D:/Repositories/TutorialGit/UnComProjects/UBS_CLIENT/UBS/FRM/OP/UbsOpRetoperFrm_CP` — Level 3 conversion (completed)
- `D:/Repositories/TutorialGit/UnComProjects/UBS_CLIENT/UBS/FRM/OP/UbsOpCommissionFrm_CP` — Level 3 conversion (completed)
- `D:/Repositories/TutorialGit/UnComProjects/UBS_CLIENT/UBS/FRM/OP/UbsOpCommissionSetupFrm_CP` — Level 2 conversion (completed)
