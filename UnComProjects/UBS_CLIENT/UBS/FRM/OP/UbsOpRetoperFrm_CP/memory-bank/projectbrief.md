# Memory Bank: Project Brief

## Project Overview

**UbsOpRetoperFrm** is a Windows Forms component for the UBS (UniSAB) system, used for OP "Принятая ценность" (accepted value) management. The project provides a form that extends the UBS form base and implements the UBS channel interface for loading and saving accepted-value data.

**Conversion:** Legacy source = `legacy-form/OP_ret_oper/op_ret_oper.dob` (VB6 UserDocument). Target = `UbsOpRetoperFrm` (.NET WinForm). Main goal: convert op_ret_oper → UbsOpRetoperFrm; see `memory-bank/plan-retoper-conversion-goals.md`.

## Objectives

- Convert VB6 op_ret_oper UserDocument to .NET WinForms (UbsOpRetoperFrm).
- Integrate with UBS channel (Get_Data, Retoper_Save, LoadResource).
- Preserve UI (tabs, readonly fields, cmbState, add-fields, Save/Exit) and behavior (keyboard, focus).
- Apply same code quality as other UBS conversions (constants partial, channel contract, phased PLAN → CREATIVE → BUILD → REFLECT).

## Scope

- **Current state:** Template/skeleton form in `UbsOpRetoperFrm/` (UbsOpRetoperFrm).
- **Components:** UbsOpRetoperFrm.cs, UbsOpRetoperFrm.Designer.cs, resource file.
- **Target:** Functional parity with op_ret_oper; constants file; channel contract doc; optional partials when form grows.

## Environment

- **Platform:** Windows (win32).
- **Repository root:** D:/Repositories/TutorialGit
- **Project path:** UnComProjects/UBS_CLIENT/UBS/FRM/OP/UbsOpRetoperFrm_CP

## Success Criteria

- Form builds and runs; InitDoc loads data; Save runs retoper_Save and shows info message.
- All legacy param keys and commands documented; no magic strings in code (constants partial).
- Zero linter errors; structure aligned with UbsOpCommissionFrm_CP and UbsOpBlankFrm_CP memory-bank usage.
