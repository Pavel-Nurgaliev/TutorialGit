# Memory Bank: Tech Context

## Stack

- .NET / C# (Framework 2.0 or as per solution)
- Windows Forms (UbsFormBase or equivalent base)
- UBS client (IUbs, channel, param objects, form field collections)

## Key Files

- `UbsOpBlankFrm/UbsOpBlankFrm.cs` — main form (code-behind)
- `UbsOpBlankFrm/UbsOpBlankFrm.Designer.cs` — designer
- `legacy-form/Blank/Blank_ud.dob` — legacy VB6 UserDocument (source of truth for conversion)

## Dependencies

- UBS form base and channel/param types
- Standard WinForms controls; project-specific controls (e.g. date, info) where available

## Encoding / Locale

- Russian UI strings (e.g. "Принятая ценность", "Сохранить", "Выход"); preserve encoding (e.g. Windows-1251 / project standard).
