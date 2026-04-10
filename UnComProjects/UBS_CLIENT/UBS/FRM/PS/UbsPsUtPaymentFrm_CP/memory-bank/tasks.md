# Tasks: UbsPsUtPaymentFrm Conversion

## Complexity Determination
- **Level**: 4 (Complex System / Enterprise)
- **Rationale**:
  - Legacy VB6 document form must be converted to .NET Framework 2.0 WinForms.
  - Main source is `UtPayment.dob` plus child forms `frmCalc.frm`, `frmCashOrd.frm`, and `frmCashSymb.frm`.
  - Project starts from template `UbsFormProject1` and must be renamed to `UbsPsUtPaymentFrm`.
  - UI parity is required against `legacy-form/screens`.
  - Conversion should follow patterns proven in `UbsPsUtPaymentGroupFrm_CP` and `UbsPsContractFrm_CP`.
  - UBS channel integration, event migration, and form-template constraints are expected.
- **Workflow**: VAN -> PLAN -> CREATIVE -> BUILD -> REFLECT -> ARCHIVE

## Current Phase: PLAN - queued (VAN complete)

### PLAN deliverables (Level 4)
- [ ] Define complete .NET file structure for `UbsPsUtPaymentFrm`
- [ ] Inventory all forms and modules from `legacy-form/UtPayment`
- [ ] Map all VB6 controls to .NET controls and target names
- [ ] Plan project rename from `UbsFormProject1` to `UbsPsUtPaymentFrm`
- [ ] Identify all channel calls, `ParamIn` keys, and `ParamOut` keys from VB6 sources
- [ ] Plan child-form strategy for `frmCalc`, `frmCashOrd`, and `frmCashSymb`
- [ ] Compare legacy screens with target designer layout and template constraints
- [ ] Define partial-class split strategy for the .NET form

### Technology validation (PLAN gate)
- **Stack**: .NET Framework 2.0 WinForms class library with UBS assemblies
- **Base pattern**: `UbsFormBase`, `panelMain`, template-aligned table layout
- **References**: use existing successful PS conversions as implementation patterns
- **Designer rules**: keep template structure intact; map VB6 OCX controls to approved .NET controls
- **Data mapping**: VB6 variant matrices map to C# `object[row, column]`

## Complete .NET file structure (planned summary)

**Conversion root** (`UbsPsUtPaymentFrm_CP`): `.cursor/rules`, `legacy-form`, `memory-bank`, `UbsFormProject1` (to be renamed).

**Target project** (`UbsPsUtPaymentFrm/` after rename):

| File | Role |
|------|------|
| `UbsPsUtPaymentFrm.sln` | Solution |
| `UbsPsUtPaymentFrm.csproj` | .NET 2.0 MSBuild project |
| `Properties/AssemblyInfo.cs` | Assembly metadata |
| `UbsPsUtPaymentFrm.cs` | Constructor, fields, top-level event wiring |
| `UbsPsUtPaymentFrm.Constants.cs` | Command names, messages, resource constants |
| `UbsPsUtPaymentFrm.Designer.cs` | `InitializeComponent`, layout, control declarations |
| `UbsPsUtPaymentFrm.resx` | Form resources |
| `UbsPsUtPaymentFrm.Initialization.cs` | Initial load, read helpers, form state setup |
| `UbsPsUtPaymentFrm.Save.cs` | Save pipeline and validation flow |
| `FrmCalc.cs` / `FrmCalc.Designer.cs` | Child dialog converted from `frmCalc.frm` |
| `FrmCashOrd.cs` / `FrmCashOrd.Designer.cs` | Child dialog converted from `frmCashOrd.frm` |
| `FrmCashSymb.cs` / `FrmCashSymb.Designer.cs` | Child dialog converted from `frmCashSymb.frm` |

## Creative phases required (PLAN gate output)
- [ ] Designer layout vs. `legacy-form/screens` and `UbsFormTemplate`
- [ ] Channel contract document for all `Run` calls and parameters
- [ ] Constants inventory for commands, captions, and user messages
- [ ] Child-form interaction design between main form and dialogs
- [ ] Validation / save-flow design if business rules are non-trivial

## Phase 2: CREATIVE - Architecture Decisions
- [ ] Designer layout: panelMain, grouping, tabs, field placement
- [ ] Constants file: resource names, command strings, captions, messages
- [ ] Channel contract: all `Run` calls, `ParamIn`, `ParamOut`
- [ ] Child forms: open mode, data exchange, close/return contract
- [ ] Form appearance: match legacy screenshots in .NET layout

## Phase 3: BUILD - Implementation
- [ ] Rename `UbsFormProject1` project, assembly, namespaces, and form names to `UbsPsUtPaymentFrm`
- [ ] Create or update `.sln` and `.csproj` for final names
- [ ] Create `AssemblyInfo.cs`
- [ ] Create `UbsPsUtPaymentFrm.Designer.cs`
- [ ] Create `UbsPsUtPaymentFrm.Constants.cs`
- [ ] Create `UbsPsUtPaymentFrm.cs`
- [ ] Create `UbsPsUtPaymentFrm.Initialization.cs`
- [ ] Create `UbsPsUtPaymentFrm.Save.cs`
- [ ] Convert child forms `frmCalc`, `frmCashOrd`, `frmCashSymb`
- [ ] Create `.resx`
- [ ] Add final project references and post-build behavior if required by existing PS patterns

## Phase 4: REFLECT - Review
- [ ] Verify all VB6 forms/modules were accounted for
- [ ] Verify all channel commands are mapped
- [ ] Verify UI matches `legacy-form/screens`
- [ ] Verify control naming conventions and template constraints
- [ ] Verify .NET 2.0 compatibility and UBS reference conventions
- [ ] Verify error handling pattern uses `Ubs_ShowError`

## Phase 5: ARCHIVE - Documentation
- [ ] Archive final conversion notes
- [ ] Record deviations from VB6 original
- [ ] Record unresolved gaps or follow-up tasks
