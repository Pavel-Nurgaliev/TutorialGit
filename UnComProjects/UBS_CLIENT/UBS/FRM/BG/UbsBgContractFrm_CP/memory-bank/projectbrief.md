# Memory Bank: Project Brief

## Project Overview
**UbsBgContractFrm** is a Windows Forms library component for the UBS (UniSAB) system, specifically designed for Bank Guarantee (BG) Contract form management. The project provides a form template that extends the UbsFormBase class and implements the IUbs interface for integration with the UBS channel communication system.

## Objectives
- Provide a reusable form template for BG Contract management
- Integrate with UBS channel system for resource loading and command handling
- Support field collection management and validation
- Enable command-based interactions through IUbs interface

## Scope
- **Current State:** Template form with basic structure
- **Components:**
  - Main form class: `UbsBgContractFrm.cs`
  - Form designer: `UbsBgContractFrm.Designer.cs`
  - Resource file: `UbsBgContractFrm.resx`
  - Legacy VB6 forms in `source/BG_CONTRACT/` directory
  - Related form: `UbsGuarModelFrm` (Guarantee Model form)

## Key Stakeholders
- UBS system developers
- Bank guarantee contract management users
- UniSAB system administrators

## Success Criteria
- Form successfully integrates with UBS channel system
- Commands (CommandLine, ListKey) function correctly
- Field collection management works as expected
- Form validation and error handling are robust
