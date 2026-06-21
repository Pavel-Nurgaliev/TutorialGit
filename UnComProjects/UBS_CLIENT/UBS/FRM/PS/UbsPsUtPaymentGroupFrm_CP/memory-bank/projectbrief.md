# Project Brief: UbsPsUtPaymentGroupFrm

## Goal
Convert the legacy VB6 form **UtPaymentGroup_F** (Group Payment form) to a .NET Framework 2.0 WinForms library **UbsPsUtPaymentGroupFrm**, following the established conversion patterns from successful projects (UbsPsContractFrm, UbsOpCommissionFrm, UbsOpBlankFrm).

## Source
- **VB6 form**: `legacy-form\UtPaymentGroup\UtPaymentGroup_F.dob` (2683 lines)
- **VB6 type**: UserDocument implementing UBSChild interface
- **VBS resource**: `VBS:UBS_VBD\PS\UT\UtPaymentF.vbs`
- **Form title**: "Групповой платеж" (Group Payment)
- **Screenshots**: `legacy-form\screens\1.png`, `legacy-form\screens\2.png`

## Target
- **Project name**: UbsPsUtPaymentGroupFrm
- **Namespace**: UbsBusiness
- **Base class**: UbsFormBase (from UbsFormBase.dll)
- **Target Framework**: .NET Framework v2.0
- **Output type**: Library (Class Library DLL)
- **Template source**: TMP_CP\UbsFormProject1 (rename to UbsPsUtPaymentGroupFrm)

## Reference Conversions
1. **UbsPsContractFrm_CP** (`FRM\PS\`) — same PS module, closest reference
2. **UbsOpCommissionFrm_CP** (`FRM\OP\`) — commission form pattern
3. **UbsOpBlankFrm_CP** (`FRM\OP\`) — blank form pattern

## Key Constraints
- TargetFrameworkVersion: v2.0 (no LINQ, no modern C# features)
- All UBS assembly references: `<Private>False</Private>`
- HintPath: `C:\ProgramData\UniSAB\Assembly\Ubs\...`
- panelMain from UbsFormBase template must be preserved
- Hungarian-style control naming prefixes
- Explicit string literals for command names and param keys
- Error handling: `try/catch → this.Ubs_ShowError(ex)`
