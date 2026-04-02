# Memory Bank: Style Guide

## Code Style (C# / .NET 2.0)

Follows the same conventions as OP reference conversions (`UbsOpRetoperFrm`, `UbsOpCommissionFrm`).

### Naming

- **Class name:** `UbsPmTradeFrm` (matches assembly/form name)
- **Namespace:** `UbsBusiness`
- **Constants partial file:** `UbsPmTradeFrm.Constants.cs`
- **Private fields:** `m_` prefix (e.g. `m_idTrade`, `m_strRunParam`, `m_intVidTrade`)
- **Controls:** Follow VB6 naming (drop Hungarian OCX prefix): `txtTradeDate`, `cmbCurrencyPost`, `lstViewOblig`, `chkIsComposit`, etc.

### Channel Calls

```csharp
// Always use explicit string literals for command names and param keys
this.IUbsChannel.ParamIn.ClearParameters();
this.IUbsChannel.ParamOut.ClearParameters();
this.IUbsChannel.ParamIn["ID_TRADE"] = m_idTrade;
this.IUbsChannel.Run("GetOneTrade");
```

### Constants Partial Pattern

```csharp
// UbsPmTradeFrm.Constants.cs
public partial class UbsPmTradeFrm : UbsFormBase
{
    private const string LoadResource = VBS:SOURCE;
    private const string MsgSaved = "Данные сохранены";
    private const string MsgNoTradeDate = "Не задана дата сделки";
    // ... other user-facing messages
}
```

### Error Handling

```csharp
try
{
    // channel call or business logic
}
catch (Exception ex) { this.Ubs_ShowError(ex); }
```

### Control Enable/Disable (replacing EnableWindow API)

```csharp
// VB6: Call EnableWindow(SSTabsPanel1.hwnd, 0&)
// .NET:
tabPage1.Enabled = false;  // or panel.Enabled = false
```

## Documentation

- Channel contract: document all `.Run` calls, `ParamIn` keys, `ParamOut` keys in creative doc.
- No inline comments that merely restate the code (e.g. no `// increment counter`).
- Complex business rules (rate calculations, tab guard logic) may have explanatory comments.

## Project File Conventions

- `TargetFrameworkVersion`: `v2.0` (do not upgrade)
- References: `<Private>False</Private>` for all UBS assemblies
- `HintPath`: `C:\ProgramData\UniSAB\Assembly\Ubs\...`

## Linter Requirements

- Zero warnings at `WarningLevel 4`
- No unused variables, no unreachable code
- Remove any unused `using` directives
