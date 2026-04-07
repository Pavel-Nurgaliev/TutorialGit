# Active Context: UbsPsUtPaymentGroupFrm

## Current Phase
**VAN** — Initialization Complete, Template Renamed

## Current Focus
Memory Bank deployed. VB6 form fully analyzed. Complexity Level 4. Template UbsFormProject1 renamed to UbsPsUtPaymentGroupFrm. Ready for PLAN mode.

## Key Decisions Made
1. **Complexity Level 4** — Large form with 20+ channel commands, complex validation, commission calculations, group payment workflow
2. **Target structure**: 6-8 partial class files following UbsPsContractFrm pattern
3. **Template renamed**: UbsFormProject1 → UbsPsUtPaymentGroupFrm (DONE)

## Template Renaming Summary
Source: `TMP_CP\UbsFormProject1\` → Target: `UbsPsUtPaymentGroupFrm_CP\UbsPsUtPaymentGroupFrm\`

| Template Name | Renamed To |
|---------------|------------|
| UbsFormProject1.csproj | UbsPsUtPaymentGroupFrm.csproj |
| UbsFormProject1.slnx | UbsPsUtPaymentGroupFrm.sln |
| UbsForm1.cs | UbsPsUtPaymentGroupFrm.cs |
| UbsForm1.Designer.cs | UbsPsUtPaymentGroupFrm.Designer.cs |
| UbsForm1.resx | UbsPsUtPaymentGroupFrm.resx |
| (new) | UbsPsUtPaymentGroupFrm.Constants.cs |

Key changes inside files:
- Class: `UbsForm1` → `UbsPsUtPaymentGroupFrm`
- RootNamespace: `UbsFormTemplate` → `UbsPsUtPaymentGroupFrm`
- AssemblyName: `UbsFormTemplate` → `UbsPsUtPaymentGroupFrm`
- DocumentationFile: `UbsFormTemplate.XML` → `UbsPsUtPaymentGroupFrm.XML`
- Form title: `"Шаблон формы"` → `"Групповой платеж"`
- LoadResource: template placeholder → `@"VBS:UBS_VBD\PS\UT\UtPaymentF.vbs"`
- Added references: UbsCtrlDecimal, UbsCtrlFields, UbsCtrlAccount
- Added Constants.cs partial with LoadResource constant

## What Was Analyzed
- Full VB6 form code (2683 lines) — all controls, business logic, channel calls
- Legacy form screenshots (2 tabs)
- 3 successful conversions: UbsPsContractFrm, UbsOpCommissionFrm, UbsOpBlankFrm
- Template project structure (TMP_CP\UbsFormProject1 — fully read and renamed)

## Next Steps
- Transition to **PLAN mode** for detailed conversion planning
- Create detailed control mapping document
- Define partial class split with method assignments
- Create channel contract document

## Status
```
✓ PLATFORM CHECKPOINT: Windows 10, PowerShell, backslash paths
✓ MEMORY BANK CHECKPOINT: All core files created
✓ COMPLEXITY CHECKPOINT: Level 4 — Complex System
✓ TEMPLATE RENAMED: UbsFormProject1 → UbsPsUtPaymentGroupFrm
→ NEXT: PLAN mode (forced transition for Level 2-4)
```
