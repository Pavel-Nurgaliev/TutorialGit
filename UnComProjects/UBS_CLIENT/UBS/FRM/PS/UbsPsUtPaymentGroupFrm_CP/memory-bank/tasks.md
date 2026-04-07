# Tasks: UbsPsUtPaymentGroupFrm Conversion

## Complexity Determination
- **Level**: 4 (Complex System / Enterprise)
- **Rationale**:
  - Large VB6 form: 2683 lines with complex business logic
  - 20+ channel commands to map
  - Complex validation chains (terrorism, IPDL, passport, account keys)
  - Tab control with dynamic additional properties
  - 7 OCX controls to map to .NET equivalents
  - Commission calculation engine with tiered rates
  - Group payment workflow cycle (save → prompt → add another)
  - Client selection via child windows / card reader
  - Recipient attribute management with browse/save
- **Workflow**: VAN → PLAN → CREATIVE → BUILD → REFLECT → ARCHIVE

## Current Phase: VAN (Initialization) - COMPLETE

## Task Breakdown

### Phase 1: PLAN — Detailed Planning
- [ ] Define complete file structure for .NET project
- [ ] Map all VB6 controls to .NET controls with names
- [ ] Plan partial class split strategy
- [ ] Define channel command contract document
- [ ] Plan validation chain architecture
- [ ] Plan commission calculation logic migration
- [ ] Plan group payment cycle implementation

### Phase 2: CREATIVE — Architecture Decisions
- [ ] Designer layout: panelMain, tabs, groupboxes, control placement
- [ ] Constants file: all string constants, command names, messages
- [ ] Channel contract: all Run calls, ParamIn/ParamOut keys
- [ ] Form appearance: match legacy UI layout in .NET

### Phase 3: BUILD — Implementation
- [ ] Create .sln and .csproj
- [ ] Create AssemblyInfo.cs
- [ ] Create Designer.cs (all controls)
- [ ] Create Constants.cs
- [ ] Create main form file (constructor, IUbs delegates)
- [ ] Create Initialization.cs (InitDoc, channel reads)
- [ ] Create Save.cs (save pipeline, all validations)
- [ ] Create Keys.cs (keyboard handling)
- [ ] Create .resx

### Phase 4: REFLECT — Review
- [ ] Verify all channel commands mapped
- [ ] Verify UI matches legacy screenshots
- [ ] Verify control naming conventions
- [ ] Verify error handling patterns

### Phase 5: ARCHIVE — Documentation
- [ ] Archive conversion notes
- [ ] Document deviations from VB6 original
