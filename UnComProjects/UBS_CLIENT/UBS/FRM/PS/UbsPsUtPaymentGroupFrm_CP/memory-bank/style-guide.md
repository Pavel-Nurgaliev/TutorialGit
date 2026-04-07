# Style Guide: UbsPsUtPaymentGroupFrm

## Code Style
- Namespace: `UbsBusiness`
- Class: `public partial class UbsPsUtPaymentGroupFrm : UbsFormBase`
- String literals: explicit, no magic strings — all centralized in Constants.cs
- Error handling: `try/catch → this.Ubs_ShowError(ex)`
- No inline comments that merely restate code
- Complex business rules may have explanatory comments

## Control Naming
| Prefix | Type | Example |
|--------|------|---------|
| btn | Button | btnSave, btnExit, btnClient |
| txt | TextBox | txtFIOPay, txtBic, txtINN |
| lbl | Label | lblRecip, lblINN |
| cmb | ComboBox | cmbCode, cmbPurpose |
| grp | GroupBox | grpPayer, grpRecipient |
| tab | TabControl | tabPayment |
| tabPage | TabPage | tabPageMain, tabPageAddProperties |
| pnl | Panel | — |
| tbl | TableLayoutPanel | tblActions |
| uci | UbsCtrlInfo | uciInfo |
| ucd | UbsCtrlDate | — |
| uca | UbsCtrlAccount | ucaAccKorr, ucaAccClient |
| udc | UbsCtrlDecimal | udcSumma, udcPeny, udcSummaRateSend, udcSummaTotal |
| ucf | UbsCtrlFields | ucfAddProperties |

## Channel Call Style
```csharp
this.IUbsChannel.ParamIn["KEY"] = value;
this.IUbsChannel.Run("CommandName");
```

## Constants File Pattern
```csharp
public partial class UbsPsUtPaymentGroupFrm : UbsFormBase
{
    private const string LoadResource = @"VBS:UBS_VBD\PS\UT\UtPaymentF.vbs";
    private const string MsgSaved = "Данные сохранены в БД";
    // ... all other string constants
}
```

## Project File Conventions
- TargetFrameworkVersion: v2.0
- References: `<Private>False</Private>` for all UBS assemblies
- HintPath: `C:\ProgramData\UniSAB\Assembly\Ubs\...`
