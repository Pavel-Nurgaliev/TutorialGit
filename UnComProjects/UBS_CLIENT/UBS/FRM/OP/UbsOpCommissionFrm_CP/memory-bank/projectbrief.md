# Memory Bank: Project Brief

## Project Overview
**UbsOpCommissionFrm** is a Windows Forms library component for the UBS (UniSAB) system, designed for OP Commission form management. The project provides a form template that extends the UbsFormBase class and implements the IUbs interface for integration with the UBS channel communication system.

**Conversion:** Legacy source = `Commission/Commission_ud.dob` (VB6 UserDocument). Target = `UbsOpCommissionFrm` (.NET WinForm). Main goal: convert Commission_ud → UbsOpCommissionFrm; see `memory-bank/plan-conversion-goals-revised.md`.

## Objectives
- Provide a reusable form template for OP Commission management
- Integrate with UBS channel system for resource loading and command handling
- Support field collection management and validation
- Enable command-based interactions through IUbs interface
- Apply the same code quality and structural goals as UbsBgContractFrm (constants, partials, documentation)

## Scope
- **Current State:** Template form with basic structure (~104 lines)
- **Components:**
  - Main form class: `UbsOpCommissionFrm.cs`
  - Form designer: `UbsOpCommissionFrm.Designer.cs`
  - Resource file: `UbsOpCommissionFrm.resx`
- **Target:** Same patterns as UbsBgContractFrm_CP — constants file, channel contract docs, partial split when form grows

## Key Stakeholders
- UBS system developers
- OP Commission users
- UniSAB system administrators

## Success Criteria
- Form successfully integrates with UBS channel system
- Commands (CommandLine, ListKey) function correctly
- Field collection management works as expected
- Form validation and error handling are robust
- No magic strings; all in Constants partial
- Channel contract documented
- Zero linter errors; 100% UBS architecture compliance
