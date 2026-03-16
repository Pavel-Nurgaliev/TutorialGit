# Memory Bank: Project Brief

## Project Overview

**UbsOpBlankFrm** is a Windows Forms component for the UBS (UniSAB) system, used for OP "Принятая ценность" (accepted value) management. The project provides a form that extends the UBS form base and implements the UBS channel interface for loading and saving accepted-value data.

**Conversion:** Legacy source = `legacy-form/Blank/Blank_ud.dob` (VB6 UserDocument). Target = `UbsOpBlankFrm` (.NET WinForm). Main goal: convert Blank_ud → UbsOpBlankFrm; see `memory-bank/plan-blank-conversion-goals.md`.

## Objectives

- Convert VB6 Blank_ud UserDocument to .NET WinForms (UbsOpBlankFrm).
- Integrate with UBS channel (Get_Data, Blank_Save, LoadResource).
- Preserve UI (tabs, readonly fields, cmbState, add-fields, Save/Exit) and behavior (keyboard, focus).
- Apply same code quality as other UBS conversions (constants partial, channel contract, phased PLAN → CREATIVE → BUILD → REFLECT).

## Scope

- **Current state:** Template/skeleton form in `UbsOpBlankFrm/` (UbsForm1).
- **Components:** UbsOpBlankFrm.cs, UbsOpBlankFrm.Designer.cs, resource file.
- **Target:** Functional parity with Blank_ud; constants file; channel contract doc; optional partials when form grows.

## Environment

- **Platform:** Windows (win32).
- **Repository root:** D:/Repositories/TutorialGit
- **Project path:** UnComProjects/UBS_CLIENT/UBS/FRM/OP/UbsOpBlankFrm_CP

## Success Criteria

- Form builds and runs; InitDoc loads data; Save runs Blank_Save and shows info message.
- All legacy param keys and commands documented; no magic strings in code (constants partial).
- Zero linter errors; structure aligned with UbsOpCommissionFrm_CP memory-bank usage.
