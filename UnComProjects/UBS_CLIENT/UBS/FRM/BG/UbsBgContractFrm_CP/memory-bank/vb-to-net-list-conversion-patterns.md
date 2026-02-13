# VB.NET to .NET Forms List Conversion Patterns

## Overview
This document contains proven patterns for converting VB.NET form list selection handlers to C# .NET forms using the UBS action system. These patterns are based on successful conversions from `BG_Contract.dob` to `UbsBgContractFrm.cs`.

## Last Updated
2026-02-13

---

## Core Conversion Pattern

### Standard List Selection Pattern

**VB.NET Pattern:**
```vb
Private Sub btnX_Click()
On Error GoTo ErrorCode

    Call EnabledCmdControl(False)

    DoEvents
    If NumChildWinX <> 0 Then
        Me.Parent.SetFocusToWindow NumChildWinX
        DoEvents
    Else
        DoEvents
        
        Me.Parent.GetWindow "UBS_FLT\PATH\LIST_NAME.flt", NumChildWinX
        
        If NumChildWinX <> 0 Then
            Dim InitParamGrid As Variant
            Dim GridF As Object
            ReDim InitParamGrid(5)
            
            Set GridF = Me.Parent.ParamInfo(NumChildWinX, "UbsGridList")
            
            ' Grid filter configuration
            intKeyCond = GridF.AddWhereItem("FieldName", 4, value)
            GridF.WhereItemFlags(intKeyCond) = 0 'hidden condition
            
            If Parent.IsExistParam(NumChildWinX, "InitParamGrid") Then
                InitParamGrid(0) = NumWin: InitParamGrid(2) = True: InitParamGrid(5) = 1
                Parent.ParamInfo(NumChildWinX, "InitParamGrid") = InitParamGrid
                If Parent.IsExistParam(NumChildWinX, "SelectItem") Then Parent.ParamInfo(NumChildWinX, "SelectItem") = Empty
                Parent.SetFocusToWindow NumChildWinX
            End If
            Set GridF = Nothing
        End If
    End If
    
    Call EnabledCmdControl(True)
    
    Exit Sub
ErrorCode:
    objErr.UbsErrMsg "Error message", "ошибка выполнения"
End Sub
```

**C# Pattern:**
```csharp
private void linkX_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
{
    EnabledCmdControl(false);

    object[] ids = this.Ubs_ActionRun(ActionConstantName, this, true) as object[];

    if (ids != null && ids.Length > 0)
    {
        m_idX = Convert.ToInt32(ids[0]);

        // Additional data retrieval if needed
        base.IUbsChannel.ParamIn("ID", m_idX);
        base.IUbsChannel.Run("ReadMethodName");

        txtX.Text = Convert.ToString(base.IUbsChannel.ParamOut("Наименование"));
        
        // Set focus to next control
        nextControl.Focus();
    }

    EnabledCmdControl(true);
}
```

---

## Action Name Mapping

### Filter File Path → Action Constant

| Filter File Path | Action Constant Name | Constant Value | Usage |
|------------------|---------------------|---------------|-------|
| `UBS_FLT\BG\LIST_FRAME_CONTRACT.flt` | `ActionUbsBgFrameContractList` | `"UBS_BG_FRAME_CONTRACT_LIST"` | Frame contract selection |
| `UBS_FLT\BG\LIST_MODEL.flt` | `ActionUbsBgListModel` | `"UBS_BG_LIST_MODEL"` | Model selection |
| `UBS_FLT\BG\LIST_AGENT.flt` | `ActionUbsBgListAgent` | `"UBS_BG_LIST_AGENT"` | Agent selection |
| `UBS_FLT\COMMON\client.flt` | `ActionUbsCommonListClient` | `"UBS_COMMON_LIST_CLIENT"` | Client selection (Principal, Beneficiar, Garant) |
| `UBS_FLT\BG\LIST_CONTRACT.flt` | `ActionUbsBgListContract` | `"UBS_BG_LIST_CONTRACT"` | Contract selection (to be determined) |

### Action Constant Declaration Pattern

Add to constants region:
```csharp
private const string ActionUbsXxxListYyy = "UBS_XXX_LIST_YYY";
```

---

## Grid Filter Configuration Patterns

### Pattern 1: Hidden Condition (State = 0)
**VB.NET:**
```vb
intKeyCond = GridF.AddWhereItem("Состояние", 4, 0)
GridF.WhereItemFlags(intKeyCond) = 0 'невидимое условие
```

**C#:**
```csharp
args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
    new KeyValuePair<string, object>("наименование", "Состояние"),
    new KeyValuePair<string, object>("значение по умолчанию", "0"),
    new KeyValuePair<string, object>("условие по умолчанию", "="),
    new KeyValuePair<string, object>("скрытый", false) }));

args.IUbs.Run("UbsItemsRefresh", null);
```

### Pattern 2: Visible Condition (State = "Открыт")
**VB.NET:**
```vb
intKeyCond = GridF.AddWhereItem("Состояние", 4, "Открыт")
GridF.WhereItemFlags(intKeyCond) = 0 'видимое условие
```

**C#:**
```csharp
args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
    new KeyValuePair<string, object>("наименование", "Состояние"),
    new KeyValuePair<string, object>("значение по умолчанию", "Открыт"),
    new KeyValuePair<string, object>("условие по умолчанию", "="),
    new KeyValuePair<string, object>("скрытый", false) }));

args.IUbs.Run("UbsItemsRefresh", null);
```

### Pattern 3: Array Condition (One Of)
**VB.NET:**
```vb
' Array condition setup
```

**C#:**
```csharp
var kinds = new object[m_termsFrameContract.GetLength(0)];

for (int i = 0; i < m_termsFrameContract.GetLength(0); i++)
{
    kinds[i] = m_termsFrameContract[i, 0];
}

args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
    new KeyValuePair<string, object>("наименование", "Вид гарантии"),
    new KeyValuePair<string, object>("значение по умолчанию", kinds),
    new KeyValuePair<string, object>("условие по умолчанию", "один из"),
    new KeyValuePair<string, object>("скрытый", false) }));

args.IUbs.Run("UbsItemsRefresh", null);
```

### Grid Configuration Handler Pattern

```csharp
private void formName_Ubs_ActionRunBegin(object sender, UbsActionRunEventArgs args)
{
    if (args.Action == ActionConstantName)
    {
        // Grid filter configuration
        args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
            new KeyValuePair<string, object>("наименование", "FieldName"),
            new KeyValuePair<string, object>("значение по умолчанию", defaultValue),
            new KeyValuePair<string, object>("условие по умолчанию", "="),
            new KeyValuePair<string, object>("скрытый", false) }));

        args.IUbs.Run("UbsItemsRefresh", null);
    }
    // Add more action handlers as needed
}
```

---

## Complete Conversion Examples

### Example 1: Simple List Selection (Principal/Beneficiar)

**VB.NET:**
```vb
Private Sub btnPrincipal_Click()
    Call EnabledCmdControl(False)
    Me.Parent.GetWindow "UBS_FLT\COMMON\client.flt", NumChildWinPrincipal
    ' ... window management ...
    Call EnabledCmdControl(True)
End Sub

' In ListKey handler:
Case NumChildWinPrincipal:
    IdPrincipal = RHS(1)(0)
    objParamIn.Parameter("ID") = IdPrincipal
    Call oleUbsChannel.Run("BGReadClientById", objParamIn, objParamOut)
    txtPrincipal.Text = objParamOut.Parameter("Наименование")
```

**C#:**
```csharp
// Constants
private const string ActionUbsCommonListClient = "UBS_COMMON_LIST_CLIENT";

// Handler
private void linkPrincipal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
{
    EnabledCmdControl(false);

    object[] ids = this.Ubs_ActionRun(ActionUbsCommonListClient, this, true) as object[];

    if (ids != null && ids.Length > 0)
    {
        m_idPrincipal = Convert.ToInt32(ids[0]);

        base.IUbsChannel.ParamIn("ID", m_idPrincipal);
        base.IUbsChannel.Run("BGReadClientById");

        txtPrincipal.Text = Convert.ToString(base.IUbsChannel.ParamOut("Наименование"));

        linkBeneficiar.Focus();
    }

    EnabledCmdControl(true);
}
```

### Example 2: List Selection with Grid Filter (Frame Contract)

**VB.NET:**
```vb
Private Sub btnFrameContract_Click()
    ' ... window management ...
    Set GridF = Me.Parent.ParamInfo(NumChildWinFrContract, "UbsGridList")
    intKeyCond = GridF.AddWhereItem("Состояние", 4, 0)
    GridF.WhereItemFlags(intKeyCond) = 0
    ' ...
End Sub

' In ListKey handler:
Case NumChildWinFrContract:
    IdFrameContract = RHS(1)(0)
    objParamIn.Parameter("ID") = IdFrameContract
    Call oleUbsChannel.Run("BGReadFrameContractById", objParamIn, objParamOut)
    txtFrameContract.Text = objParamOut.Parameter("Наименование")
    ' ... multiple fields ...
```

**C#:**
```csharp
// Constants
private const string ActionUbsBgFrameContractList = "UBS_BG_FRAME_CONTRACT_LIST";

// Handler
private void linkFrameContract_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
{
    EnabledCmdControl(false);

    object[] ids = this.Ubs_ActionRun(ActionUbsBgFrameContractList, this, true) as object[];

    if (ids != null && ids.Length > 0)
    {
        m_idFrameContract = Convert.ToInt32(ids[0]);

        base.IUbsChannel.ParamIn("ID", m_idFrameContract);
        base.IUbsChannel.Run("BGReadFrameContractById");

        txtFrameContract.Text = Convert.ToString(base.IUbsChannel.ParamOut("Наименование"));
        m_dateBeginFrameContract = Convert.ToDateTime(base.IUbsChannel.ParamOut("Дата начала действия рамочного договора"));
        // ... more fields ...

        SetInfoByFrameContract();
        linkModel.Focus();
    }
}

// Grid configuration
private void formName_Ubs_ActionRunBegin(object sender, UbsActionRunEventArgs args)
{
    if (args.Action == ActionUbsBgFrameContractList)
    {
        args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
            new KeyValuePair<string, object>("наименование", "Состояние"),
            new KeyValuePair<string, object>("значение по умолчанию", "0"),
            new KeyValuePair<string, object>("условие по умолчанию", "="),
            new KeyValuePair<string, object>("скрытый", false) }));

        args.IUbs.Run("UbsItemsRefresh", null);
    }
}
```

### Example 3: Complex Selection with Multiple Channel Calls (Agent)

**VB.NET:**
```vb
' In ListKey handler:
Case NumChildWinListAgent:
    IdAgent = RHS(1)(0)
    ' Multiple channel calls
    objParamIn.Parameter("DateBegin") = dateOpenGarant.DateValue
    objParamIn.Parameter("IdAgent") = IdAgent
    Call oleUbsChannel.Run("GetDatePayment", objParamIn, objParamOut)
    ' ... more calls ...
```

**C#:**
```csharp
private void linkAgent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
{
    EnabledCmdControl(false);

    object[] ids = this.Ubs_ActionRun(ActionUbsBgListAgent, this, true) as object[];

    if (ids != null && ids.Length > 0)
    {
        m_idAgent = Convert.ToInt32(ids[0]);

        base.IUbsChannel.ParamIn("DateBegin", dateOpenGarant.DateValue);
        base.IUbsChannel.ParamIn("IdAgent", m_idAgent);
        base.IUbsChannel.Run("GetDatePayment");

        dateReward.DateValue = Convert.ToDateTime(base.IUbsChannel.ParamOut("DatePayment"));

        if (m_command == EditCommand)
        {
            base.IUbsChannel.Run("AgentRewardAvaliable");
            dateReward.Enabled = costAmount.Enabled = 
                Convert.ToBoolean(base.IUbsChannel.ParamOut("Avaliable"));
        }
        else
        {
            dateReward.Enabled = true;
            costAmount.Enabled = true;
        }

        base.IUbsChannel.ParamIn("ID", m_idAgent);
        base.IUbsChannel.Run("BGReadAgContr");

        var idAgClient = Convert.ToInt32(base.IUbsChannel.ParamOut("Ид клиента"));
        var numAg = Convert.ToString(base.IUbsChannel.ParamOut("Номер договора агента"));
        var dateAg = Convert.ToDateTime(base.IUbsChannel.ParamOut("Дата договора агента"));

        txtNumAgent.Text = numAg;
        dateAgent.DateValue = dateAg;

        base.IUbsChannel.ParamIn("ID", idAgClient);
        base.IUbsChannel.Run("BGReadClientById");

        txtAgent.Text = Convert.ToString(base.IUbsChannel.ParamOut("Наименование"));
    }

    EnabledCmdControl(true);
}
```

---

## Delete Button Pattern

**VB.NET:**
```vb
Private Sub btnXDel_Click()
    IdX = 0
    txtX.Text = ""
    ' Clear related fields
    field1.Text = ""
    field2.Text = ""
    field1.Enabled = False
End Sub
```

**C#:**
```csharp
private void btnXDel_Click(object sender, EventArgs e)
{
    m_idX = 0;
    txtX.Text = string.Empty;
    // Clear related fields
    field1.Text = string.Empty;
    field2.Text = string.Empty;
    // For decimal controls
    decimalControl.DecimalValue = 0m;
    // Enable/disable controls
    field1.Enabled = false;
}
```

---

## Conversion Checklist

### Step 1: Identify VB.NET Handler
- [ ] Find `btnX_Click()` handler
- [ ] Find corresponding `Case NumChildWinX:` in ListKey handler
- [ ] Identify filter file path (`UBS_FLT\...`)

### Step 2: Create Action Constant
- [ ] Map filter file path to action name
- [ ] Add constant to constants region: `private const string ActionXxx = "UBS_XXX";`
- [ ] Follow naming convention: `ActionUbs[Module][List][Entity]`

### Step 3: Convert Event Handler
- [ ] Change `Private Sub btnX_Click()` → `private void linkX_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)`
- [ ] Replace window management with `Ubs_ActionRun(ActionConstant, this, true)`
- [ ] Process returned `object[] ids` array
- [ ] Convert data retrieval from ListKey handler to event handler
- [ ] Update control references (`btnX` → `linkX`)
- [ ] Update variable names (`IdX` → `m_idX`)

### Step 4: Grid Configuration (if needed)
- [ ] Identify grid filter conditions in VB.NET code
- [ ] Add handler to `Ubs_ActionRunBegin` event
- [ ] Convert `GridF.AddWhereItem` → `UbsItemSet` pattern
- [ ] Add `UbsItemsRefresh` call

### Step 5: Update Designer
- [ ] Wire `LinkClicked` event in Designer.cs
- [ ] Ensure LinkLabel control exists (convert Button → LinkLabel if needed)

### Step 6: Error Handling
- [ ] Remove `On Error GoTo` statements
- [ ] Add `try-catch` blocks if needed (usually handled by base class)

### Step 7: Cleanup
- [ ] Remove `DoEvents` calls
- [ ] Remove window tracking variables (`NumChildWinX`)
- [ ] Remove grid object references

---

## Key Conversion Rules

1. **Control Type**: Navigation buttons → LinkLabels
2. **Event Signature**: `btnX_Click()` → `linkX_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)`
3. **Window Management**: `Parent.GetWindow` → `Ubs_ActionRun`
4. **Data Flow**: Asynchronous ListKey handler → Synchronous event handler
5. **Grid Configuration**: Inline setup → `Ubs_ActionRunBegin` event handler
6. **Error Handling**: `On Error GoTo` → `try-catch` (or implicit via base class)
7. **Variable Naming**: `IdX` → `m_idX` (member variable convention)
8. **Boolean Values**: `False/True` → `false/true`
9. **String Clearing**: `""` → `string.Empty`
10. **Decimal Controls**: `.Text = ""` → `.DecimalValue = 0m`

---

## Common Channel Methods

| VB.NET | C# |
|--------|-----|
| `oleUbsChannel.Run("MethodName", objParamIn, objParamOut)` | `base.IUbsChannel.Run("MethodName")` |
| `objParamIn.Parameter("Name") = value` | `base.IUbsChannel.ParamIn("Name", value)` |
| `objParamOut.Parameter("Name")` | `base.IUbsChannel.ParamOut("Name")` |

---

## Focus Management Patterns

**VB.NET:**
```vb
Set objFocusCtrl = btnNext
' or
btnNext.SetFocus
```

**C#:**
```csharp
nextControl.Focus();
// Conditional focus
if (linkNext.Enabled)
    linkNext.Focus();
else
    txtNext.Focus();
```

---

## Related Files

- **VB.NET Source**: `source\BG_CONTRACT\BG_Contract.dob`
- **C# Target**: `source\UbsBgContractFrm\UbsBgContractFrm.cs`
- **Designer**: `source\UbsBgContractFrm\UbsBgContractFrm.Designer.cs`
- **Conversion Plan**: `.cursor\plans\vb_to_net_conversion_structure_analysis.plan.md`

---

## Notes

- All action strings should be defined as constants in the constants region
- Grid configuration happens **before** the action runs (in `Ubs_ActionRunBegin`)
- Data retrieval happens **immediately** after action returns (in `LinkClicked` handler)
- The `Ubs_ActionRun` method handles window lifecycle internally
- Focus management should match VB.NET behavior for consistency

---

*This document is maintained based on successful conversions from BG_Contract.dob to UbsBgContractFrm.cs*
