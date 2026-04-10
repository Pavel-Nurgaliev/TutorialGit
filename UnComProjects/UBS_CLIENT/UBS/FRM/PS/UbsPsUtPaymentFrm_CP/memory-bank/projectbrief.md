# Memory Bank: Project Brief

## Project Overview

**UbsPsUtPaymentFrm** is the target Windows Forms library component for the UBS PS area. The project converts the VB6 UtPayment document to a .NET Framework 2.0 WinForms implementation based on the local template project `UbsFormProject1`, which must be renamed to `UbsPsUtPaymentFrm`.

**Conversion:** Legacy source = `legacy-form/UtPayment/UtPayment.dob` with child forms `frmCalc.frm`, `frmCashOrd.frm`, and `frmCashSymb.frm`. Target = `UbsPsUtPaymentFrm` under this conversion workspace.

## Objectives

- Convert the legacy VB6 UtPayment form to .NET Framework 2.0 with functional parity.
- Preserve the visual structure using `legacy-form/screens/` as the appearance reference.
- Follow successful PS conversion patterns from `UbsPsUtPaymentGroupFrm_CP` and `UbsPsContractFrm_CP`.
- Keep compatibility with UBS channel and UBS form-template rules.
- Rename `UbsFormProject1` cleanly to `UbsPsUtPaymentFrm`.

## Scope

- **Main legacy form:** `legacy-form/UtPayment/UtPayment.dob`
- **Child forms:** `legacy-form/UtPayment/frmCalc.frm`, `legacy-form/UtPayment/frmCashOrd.frm`, `legacy-form/UtPayment/frmCashSymb.frm`
- **Support module:** `legacy-form/UtPayment/modWinAPI.bas`
- **Visual references:** `legacy-form/screens/1.png` through `legacy-form/screens/6.png`
- **Starting .NET project:** `UbsFormProject1/`

## Environment

- **Platform:** Windows
- **Shell:** PowerShell
- **Repository root:** `D:/Repositories/TutorialGit`
- **Project path:** `UnComProjects/UBS_CLIENT/UBS/FRM/PS/UbsPsUtPaymentFrm_CP`

## Constraints

- Target framework must remain **.NET Framework 2.0**.
- Keep template structure intact: `panelMain` must exist and layout must stay aligned with `UbsFormTemplate`.
- Use explicit string literals for channel command names and parameter keys.
- Map VB6 variant matrices to C# `object[row, column]`.
- Follow UBS control naming and OCX-to-.NET mapping rules from workspace guidance.

## Success Criteria

- The renamed project builds as `UbsPsUtPaymentFrm`.
- Main form and child forms are ported with legacy behavior preserved.
- Layout is acceptably close to the legacy screenshots.
- Channel commands and parameter contracts are documented before or during BUILD.
