# System Patterns: UbsPsUtPaymentGroupFrm

## Proven Conversion Architecture (from reference projects)

### 1. Partial Class Split Pattern
All successful conversions split the form into multiple partial files by concern:

```
FormName.cs                 → Constructor, fields, IUbs delegates (CommandLine, ListKey), button handlers
FormName.Designer.cs        → InitializeComponent, Dispose, control fields
FormName.Constants.cs       → String constants (LoadResource, commands, messages)
FormName.Initialization.cs  → InitDoc, channel reads, form population
FormName.Save.cs            → Save pipeline, validation, CheckData
FormName.Keys.cs            → KeyDown/KeyPress handling
```

### 2. Constructor Pattern
```csharp
public partial class UbsPsUtPaymentGroupFrm : UbsFormBase
{
    private string m_command;
    private long m_idPayment;
    // ... fields ...

    public UbsPsUtPaymentGroupFrm()
    {
        m_addCommand();
        InitializeComponent();
        base.UbsCtrlFieldsSupportCollection.Add("Доп. свойства", ucfAddProperties);
        base.Ubs_CommandLock = true;
    }

    private void m_addCommand()
    {
        base.Ubs_AddName(new UbsDelegate(CommandLine));
        base.Ubs_AddName(new UbsDelegate(ListKey));
    }
}
```

### 3. Channel Call Pattern
```csharp
// LoadResource assignment
this.IUbsChannel.LoadResource = LoadResource;

// Direct channel calls
base.IUbsChannel.ParamIn["key"] = value;
base.IUbsChannel.Run("CommandName");
object result = base.IUbsChannel.ParamOut("key");

// Wrapped calls via UbsParam
UbsParam paramOut = base.UbsChannel_ParamsOut;
base.UbsChannel_ParamIn("key", value);
base.UbsChannel_Run("CommandName");
string val = paramOut.Parameter("key");
```

### 4. Designer Template Pattern
- `panelMain` from UbsFormBase (NOT re-declared)
- `TabControl` docked Fill in panelMain
- `TableLayoutPanel` docked Bottom in panelMain (buttons + UbsCtrlInfo)
- panelMain.Size matches UbsFormTemplate dimensions

### 5. IUbs Entry Points
```csharp
private object CommandLine(string name, object param)
{
    if (name == "InitParamForm") { /* initialization */ }
    if (name == "RetFromGrid") { /* return from grid selection */ }
    return null;
}

private object ListKey(string name, object param)
{
    // Delete confirmation + channel call
    return null;
}
```

### 6. Error Handling Pattern
```csharp
try { /* business logic */ }
catch (Exception ex) { this.Ubs_ShowError(ex); }
```

### 7. Control Naming Convention
| Prefix | Control Type |
|--------|-------------|
| btn | Button |
| txt | TextBox |
| lbl | Label |
| cmb | ComboBox |
| grp | GroupBox |
| tab / tabPage | TabControl / TabPage |
| pnl | Panel |
| tbl | TableLayoutPanel |
| uci | UbsCtrlInfo |
| ucd | UbsCtrlDate |
| uca | UbsCtrlAccount |
| udc | UbsCtrlDecimal |
| ucf | UbsCtrlFields |

## Channel Commands Catalog (UtPaymentGroup_F)

### Init/Read Commands
| Command | Purpose | Key Params In | Key Params Out |
|---------|---------|--------------|----------------|
| InitFormGroup | Initialize form | StrCommand, IdPayment | ChoiceClient, EndGroup, GroupContract, SummaPeni, strError |
| Payment (READ) | Read payment | StrCommand="READ", IdPaym | All payment fields |
| UtReadContract | Read contract | IdContract | BIC, CorrAcc, INN, Acc, Code, Comment, State, RateType*, arrPurpose |
| ReadBankBIK | Read bank by BIC | BIC | BANKNAME, CORRACC, NUM |
| ReadClientFromIdOC | Read client by ID | IDCLIENT, IsGuest | INN, InfoClient, NAME, ADRESS, TypeDoc, NUMBER, SERIES |
| ReadClientFromNomerCard | Read client by card | NomerCard, IsGuest | IDCLIENT, StrError |
| ReadRecipFromId | Read recipient | IdAttributeRecip | All recipient fields |
| FindContrByBicAndAccount | Find contract | BIC, ACC, INN | RecCount, IdArray |

### Save Commands
| Command | Purpose |
|---------|---------|
| Payment_Save | Save payment (main save) |
| SaveAttributeRecip | Save recipient attributes |

### Validation Commands
| Command | Purpose |
|---------|---------|
| UtCheckBIKBank | Validate BIC |
| UtCheckBIKLimitSharing | Check BIC limit sharing |
| UtCheckAccFromBic | Validate account for BIC |
| CheckKey | Validate account key digit |
| CheckTerror | Terrorism list check |
| CheckAddFields | Validate additional fields |
| Ut_CheckBeforeSave | Pre-save user validation |
| UTListAddRead | Read additional fields list |

### Utility Commands
| Command | Purpose |
|---------|---------|
| UtGetINNFromLastPayment | Get INN from last payment |
| UtGetKPPUPayerLastPayment | Get KPPU payer last payment |
| UtGetAccINNFromLastPayment | Get account/INN from last payment |
| UtGetKPPU | Get KPPU |
| UtReadSetupLockPurpose | Check if purpose field is locked |
| UtGetStateClearFieldSend | Check if sender fields should be cleared |
| UtReadOurBankBik | Read our bank BIC |
| PAYMENT (CLEAR) | Clear form for new entry |
