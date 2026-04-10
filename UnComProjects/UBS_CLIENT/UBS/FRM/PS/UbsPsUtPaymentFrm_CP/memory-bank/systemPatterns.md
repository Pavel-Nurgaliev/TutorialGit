# Memory Bank: System Patterns

## Expected Architecture

- WinForms form based on `UbsFormBase`
- Designer and code-behind separated into partial classes
- Optional partials for constants, initialization, save logic, and child-form helpers

## UI Patterns

- Preserve `panelMain`
- Keep layout aligned with the UBS form template
- Use project naming prefixes for controls
- Map VB6 controls to approved .NET or UBS controls

## Integration Patterns

- Use `IUbsChannel.Run("...")` with explicit string literals
- Document all `ParamIn` and `ParamOut` usage
- Route legacy variant arrays to C# `object[row, column]`

## Conversion Reference Pattern

Use the phased conversion approach seen in `UbsPsUtPaymentGroupFrm_CP` and `UbsPsContractFrm_CP`:
- PLAN for inventory and structure
- CREATIVE for design decisions and channel contract
- BUILD for rename plus implementation
- REFLECT for parity review
- ARCHIVE for final notes
