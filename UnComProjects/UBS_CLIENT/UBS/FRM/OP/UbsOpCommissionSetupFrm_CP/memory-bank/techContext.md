# Memory Bank: Tech Context

## Stack

- .NET / C# (Framework 2.0 or as per solution)
- Windows Forms (UbsFormBase or equivalent base)
- UBS client (IUbs, channel, param objects, form field collections)

## Key Files

- `UbsOpRetoperFrm/UbsOpRetoperFrm.cs` — main form (code-behind)
- `UbsOpRetoperFrm/UbsOpRetoperFrm.Designer.cs` — designer
- `legacy-form/Op_ret_oper/op_ret_oper.dob` — legacy VB6 UserDocument (source of truth for conversion)

## Dependencies

- UBS form base and channel/param types
- Standard WinForms controls; project-specific controls (e.g. date, info) where available
