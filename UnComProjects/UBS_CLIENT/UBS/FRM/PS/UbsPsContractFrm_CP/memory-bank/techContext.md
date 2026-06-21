# Memory Bank: Tech Context

## Stack

- C# / **.NET Framework 2.0**
- Windows Forms — `UbsFormBase`, `UbsService` (`UbsDelegate`, `UbsFormField`, …)
- UBS assemblies from `C:\ProgramData\UniSAB\Assembly\Ubs\` (references `Private=False`)

## Key paths

| Item | Path |
|------|------|
| WinForm project | `UbsPsContractFrm_CP/UbsPsContractFrm/` |
| Main form | `UbsPsContractFrm.cs`, `UbsPsContractFrm.Designer.cs` |
| VB6 source | `legacy-form/Contract/Contract.dob` |
| Legacy VBS (reference) | `VBS:UBS_VBD\PS\Contract.vbs` (from `.dob`; .NET uses ASM equivalent when known) |
| Screens (optional) | `legacy-form/screens/` |

## Solution

- `UbsPsContractFrm.sln` → `UbsPsContractFrm.csproj`

## Dependencies

UbsBase, UbsChannel, UbsCollections, UbsCtrlInfo, UbsForm, UbsFormBase, UbsInterface — plus additional controls as conversion requires (e.g. `UbsCtrlDecimal`, `UbsCtrlDate`, `TabControl`, `UbsCtrlFields` per designer-rules).
