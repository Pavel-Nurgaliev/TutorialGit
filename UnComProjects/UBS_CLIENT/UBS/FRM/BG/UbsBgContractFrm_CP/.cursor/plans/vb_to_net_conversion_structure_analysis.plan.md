# VB.NET to .NET Forms Conversion Structure Analysis

## Overview
This document analyzes the conversion pattern from VB.NET forms to .NET (C#) forms, based on the successful conversion example of `btnFrameContract_Click` → `linkFrameContract_LinkClicked`.

## Case Study: btnFrameContract_Click → linkFrameContract_LinkClicked

### Source: VB.NET (BG_Contract.dob)
**Location:** Lines 2342-2384

```vb
Private Sub btnFrameContract_Click()
Dim intKeyCond As Integer
On Error GoTo ErrorCode

    Call EnabledCmdControl(False)

    DoEvents
    If NumChildWinFrContract <> 0 Then
        Me.Parent.SetFocusToWindow NumChildWinFrContract
        DoEvents
    Else
        DoEvents
        
        Me.Parent.GetWindow "UBS_FLT\BG\LIST_FRAME_CONTRACT.flt", NumChildWinFrContract
        
        If NumChildWinFrContract <> 0 Then
            Dim InitParamGrid As Variant
            Dim GridF As Object
            Dim varCond As Variant
            ReDim InitParamGrid(5)
            
            Set GridF = Me.Parent.ParamInfo(NumChildWinFrContract, "UbsGridList")
            'GridF.ClearWhereList
        
            intKeyCond = GridF.AddWhereItem("Состояние", 4, 0)
            GridF.WhereItemFlags(intKeyCond) = 0 'невидимое условие
            
            If Parent.IsExistParam(NumChildWinFrContract, "InitParamGrid") Then
                InitParamGrid(0) = NumWin: InitParamGrid(2) = True: InitParamGrid(5) = 1
                Parent.ParamInfo(NumChildWinFrContract, "InitParamGrid") = InitParamGrid
                If Parent.IsExistParam(NumChildWinFrContract, "SelectItem") Then Parent.ParamInfo(NumChildWinFrContract, "SelectItem") = Empty
                Parent.SetFocusToWindow NumChildWinFrContract
            End If
            Set GridF = Nothing
        End If
    End If
    
    Call EnabledCmdControl(True)
    
    Exit Sub
ErrorCode:
    objErr.UbsErrMsg "Выбор из списка рамочного договора", "ошибка выполнения"
End Sub
```

### Target: C# (.NET) (UbsBgContractFrm.cs)
**Location:** Lines 1261-1287

```csharp
private void linkFrameContract_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
{
    EnabledCmdControl(false);

    object[] ids = this.Ubs_ActionRun("UBS_BG_FRAME_CONTRACT_LIST", this, true) as object[];

    if (ids != null && ids.Length > 0)
    {
        m_idFrameContract = Convert.ToInt32(ids[0]);

        base.IUbsChannel.ParamIn("ID", m_idFrameContract);

        base.IUbsChannel.Run("BGReadFrameContractById");

        txtFrameContract.Text = Convert.ToString(base.IUbsChannel.ParamOut("Наименование"));
        m_dateBeginFrameContract = Convert.ToDateTime(base.IUbsChannel.ParamOut("Дата начала действия рамочного договора"));
        m_dateEndFrameContract = Convert.ToDateTime(base.IUbsChannel.ParamOut("Дата окончания действия рамочного договора"));
        m_termsFrameContract = base.IUbsChannel.ParamOut("Срок гарантии рамочного договора") as object[,];
        m_limitSaldoFrameContract = Convert.ToDecimal(base.IUbsChannel.ParamOut("Остаток лимита"));
        m_issueEndDate = Convert.ToDateTime(base.IUbsChannel.ParamOut("Дата окончания выдачи"));
        m_divisionFrameContract = Convert.ToInt32(base.IUbsChannel.ParamOut("Номер отделения"));

        SetInfoByFrameContract();

        linkModel.Focus();
    }
}
```

---

## Conversion Patterns Identified

### 1. Control Type Conversion

| VB.NET Control | .NET Control | Rationale |
|----------------|--------------|-----------|
| `btnFrameContract` (Button) | `linkFrameContract` (LinkLabel) | UI modernization - links are more appropriate for navigation actions |
| `btnPrincipal` | `linkPrincipal` | Same pattern |
| `btnModel` | `linkModel` | Same pattern |
| `btnAgent` | `linkAgent` | Same pattern |

**Pattern:** Navigation buttons → LinkLabels

### 2. Event Handler Signature Conversion

| VB.NET | C# |
|--------|-----|
| `Private Sub btnX_Click()` | `private void linkX_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)` |

**Key Changes:**
- `Private Sub` → `private void`
- No parameters → Standard event handler signature with `sender` and `e`
- `_Click` suffix → `_LinkClicked` suffix
- Event args: `EventArgs` → `LinkLabelLinkClickedEventArgs`

### 3. Window Management Pattern

#### VB.NET Pattern:
```vb
If NumChildWinFrContract <> 0 Then
    Me.Parent.SetFocusToWindow NumChildWinFrContract
    DoEvents
Else
    Me.Parent.GetWindow "UBS_FLT\BG\LIST_FRAME_CONTRACT.flt", NumChildWinFrContract
    
    If NumChildWinFrContract <> 0 Then
        ' Grid configuration
        Set GridF = Me.Parent.ParamInfo(NumChildWinFrContract, "UbsGridList")
        intKeyCond = GridF.AddWhereItem("Состояние", 4, 0)
        GridF.WhereItemFlags(intKeyCond) = 0
        
        ' InitParamGrid setup
        If Parent.IsExistParam(NumChildWinFrContract, "InitParamGrid") Then
            InitParamGrid(0) = NumWin: InitParamGrid(2) = True: InitParamGrid(5) = 1
            Parent.ParamInfo(NumChildWinFrContract, "InitParamGrid") = InitParamGrid
            Parent.SetFocusToWindow NumChildWinFrContract
        End If
    End If
End If
```

#### C# Pattern:
```csharp
object[] ids = this.Ubs_ActionRun("UBS_BG_FRAME_CONTRACT_LIST", this, true) as object[];

if (ids != null && ids.Length > 0)
{
    m_idFrameContract = Convert.ToInt32(ids[0]);
    // Direct data retrieval using IUbsChannel
    base.IUbsChannel.ParamIn("ID", m_idFrameContract);
    base.IUbsChannel.Run("BGReadFrameContractById");
    // Process results...
}
```

**Key Transformation:**
- **VB.NET:** Manual window management (`GetWindow`, `SetFocusToWindow`) + Grid configuration + Window state tracking (`NumChildWinFrContract`)
- **C#:** Unified action system (`Ubs_ActionRun`) that handles window management internally + Direct data retrieval

**Action Name Mapping:**
- Filter file path → Action name:
  - `"UBS_FLT\BG\LIST_FRAME_CONTRACT.flt"` → `"UBS_BG_FRAME_CONTRACT_LIST"`
  - `"UBS_FLT\BG\LIST_MODEL.flt"` → `"UBS_BG_LIST_MODEL"`
  - `"UBS_FLT\BG\LIST_AGENT.flt"` → `"UBS_BG_LIST_AGENT"`

### 4. Grid Filter Configuration Pattern

#### VB.NET:
Grid filtering is done **before** window display:
```vb
Set GridF = Me.Parent.ParamInfo(NumChildWinFrContract, "UbsGridList")
intKeyCond = GridF.AddWhereItem("Состояние", 4, 0)
GridF.WhereItemFlags(intKeyCond) = 0 'невидимое условие
```

#### C#:
Grid filtering is done **via event handler** (`Ubs_ActionRunBegin`):
```csharp
private void ubsBgContractFrm_Ubs_ActionRunBegin(object sender, UbsActionRunEventArgs args)
{
    if (args.Action == "UBS_BG_FRAME_CONTRACT_LIST")
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

**Key Transformation:**
- Grid configuration moved from inline code to event-driven pattern
- Uses `UbsItemSet` and `UbsItemsRefresh` instead of direct grid manipulation
- Configuration happens **before** the action runs, not during window creation

### 5. Error Handling Conversion

#### VB.NET:
```vb
On Error GoTo ErrorCode
' ... code ...
Exit Sub
ErrorCode:
    objErr.UbsErrMsg "Выбор из списка рамочного договора", "ошибка выполнения"
End Sub
```

#### C#:
```csharp
// Implicit try-catch via base class error handling
// Or explicit:
try
{
    // ... code ...
}
catch (Exception ex)
{
    base.Ubs_ShowError(ex);
}
```

**Key Transformation:**
- `On Error GoTo` → `try-catch` blocks
- `objErr.UbsErrMsg` → `base.Ubs_ShowError(ex)`
- Error handling is more structured and type-safe

### 6. Method Call Syntax

| VB.NET | C# |
|--------|-----|
| `Call EnabledCmdControl(False)` | `EnabledCmdControl(false)` |
| `Call Method()` | `Method()` |

**Key Changes:**
- `Call` keyword removed
- `False` → `false` (lowercase boolean)
- `True` → `true`

### 7. Variable Naming Conventions

| VB.NET | C# |
|--------|-----|
| `IdFrameContract` | `m_idFrameContract` |
| `NumWin` | (removed - not needed) |
| `NumChildWinFrContract` | (removed - handled by action system) |

**Pattern:**
- Member variables prefixed with `m_`
- Window tracking variables eliminated (handled by framework)
- PascalCase → camelCase for private members

### 8. Control Reference Updates

| VB.NET | C# |
|--------|-----|
| `btnPrincipal.Enabled = True` | `linkPrincipal.Enabled = true` |
| `btnPrincipal` | `linkPrincipal` |

**Pattern:** All button references updated to link references

### 9. Data Retrieval Pattern

#### VB.NET:
Data retrieval happens **after** window closes, via `ListKey` handler:
```vb
' In ListKey handler:
IdFrameContract = RHS(1)(0)
' Then call BGReadFrameContractById
```

#### C#:
Data retrieval happens **immediately** after action returns:
```csharp
object[] ids = this.Ubs_ActionRun("UBS_BG_FRAME_CONTRACT_LIST", this, true) as object[];

if (ids != null && ids.Length > 0)
{
    m_idFrameContract = Convert.ToInt32(ids[0]);
    base.IUbsChannel.ParamIn("ID", m_idFrameContract);
    base.IUbsChannel.Run("BGReadFrameContractById");
    // Process all results immediately
}
```

**Key Transformation:**
- Synchronous data retrieval instead of asynchronous window-based selection
- Results processed immediately in the event handler
- No need for separate `ListKey` handler for this specific case

### 10. DoEvents Removal

| VB.NET | C# |
|--------|-----|
| `DoEvents` (multiple calls) | Removed (not needed) |

**Rationale:** .NET event handling is more efficient, `DoEvents` not needed

---

## Conversion Checklist Template

When converting a VB.NET button click handler to C# link label handler:

### Step 1: Identify Control Type
- [ ] Is this a navigation button? → Convert to LinkLabel
- [ ] Is this an action button? → Keep as Button

### Step 2: Update Event Handler Signature
- [ ] Change `Private Sub btnX_Click()` → `private void linkX_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)`
- [ ] Update Designer.cs to wire `LinkClicked` event instead of `Click`

### Step 3: Replace Window Management
- [ ] Identify filter file path (e.g., `"UBS_FLT\BG\LIST_FRAME_CONTRACT.flt"`)
- [ ] Map to action name (e.g., `"UBS_BG_FRAME_CONTRACT_LIST"`)
- [ ] Replace `Parent.GetWindow` + window management → `this.Ubs_ActionRun(actionName, this, true)`
- [ ] Process returned `object[] ids` array

### Step 4: Move Grid Configuration
- [ ] Find grid filter setup code (`GridF.AddWhereItem`, etc.)
- [ ] Move to `Ubs_ActionRunBegin` event handler
- [ ] Convert to `UbsItemSet` + `UbsItemsRefresh` pattern
- [ ] Use `args.Action` to identify which action to configure

### Step 5: Update Data Retrieval
- [ ] If data retrieval was in `ListKey` handler, move to event handler
- [ ] Use `base.IUbsChannel.ParamIn/Run/ParamOut` for data operations
- [ ] Process all results immediately after action returns

### Step 6: Update Error Handling
- [ ] Replace `On Error GoTo` → `try-catch` block
- [ ] Replace `objErr.UbsErrMsg` → `base.Ubs_ShowError(ex)`

### Step 7: Update Syntax
- [ ] Remove `Call` keyword
- [ ] Change `False/True` → `false/true`
- [ ] Update variable names (`IdX` → `m_idX`)
- [ ] Update control references (`btnX` → `linkX`)

### Step 8: Remove Obsolete Code
- [ ] Remove `DoEvents` calls
- [ ] Remove window tracking variables (`NumChildWinX`)
- [ ] Remove grid object references (`GridF`, `Set GridF = Nothing`)

---

## Action Name Mapping Reference

| Filter File Path | Action Name | Notes |
|------------------|-------------|-------|
| `UBS_FLT\BG\LIST_FRAME_CONTRACT.flt` | `UBS_BG_FRAME_CONTRACT_LIST` | Frame contract selection |
| `UBS_FLT\BG\LIST_MODEL.flt` | `UBS_BG_LIST_MODEL` | Model selection |
| `UBS_FLT\BG\LIST_AGENT.flt` | `UBS_BG_LIST_AGENT` | Agent selection |
| `UBS_FLT\COMMON\client.flt` | (varies) | Client selection (Principal, Beneficiar, Garant) |
| `UBS_FLT\BG\LIST_CONTRACT.flt` | (to be determined) | Contract selection |
| `UBS_FLT\BG\BG_GUAR_CONTRACT.flt` | (to be determined) | Guarantee contract list |
| `UBS_FLT\GUAR\LIST_OPERATION_LOG.flt` | (to be determined) | Operation log list |

---

## Grid Filter Configuration Patterns

### Pattern 1: Hidden Condition (State = 0)
```csharp
args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
    new KeyValuePair<string, object>("наименование", "Состояние"),
    new KeyValuePair<string, object>("значение по умолчанию", "0"),
    new KeyValuePair<string, object>("условие по умолчанию", "="),
    new KeyValuePair<string, object>("скрытый", false) }));
```

**VB.NET Equivalent:**
```vb
intKeyCond = GridF.AddWhereItem("Состояние", 4, 0)
GridF.WhereItemFlags(intKeyCond) = 0 'невидимое условие
```

### Pattern 2: Visible Condition (State = "Открыт")
```csharp
args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
    new KeyValuePair<string, object>("наименование", "Состояние"),
    new KeyValuePair<string, object>("значение по умолчанию", "Открыт"),
    new KeyValuePair<string, object>("условие по умолчанию", "="),
    new KeyValuePair<string, object>("скрытый", false) }));
```

**VB.NET Equivalent:**
```vb
intKeyCond = GridF.AddWhereItem("Состояние", 4, "Открыт")
GridF.WhereItemFlags(intKeyCond) = 0 'видимое условие
```

### Pattern 3: Array Condition (One Of)
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
```

---

## Summary of Key Architectural Changes

### 1. **Unified Action System**
- **Before:** Manual window management with `GetWindow`/`SetFocusToWindow`
- **After:** Unified `Ubs_ActionRun` method that handles window lifecycle

### 2. **Event-Driven Grid Configuration**
- **Before:** Grid configuration inline during window creation
- **After:** Grid configuration via `Ubs_ActionRunBegin` event handler

### 3. **Synchronous Data Flow**
- **Before:** Asynchronous window-based selection with `ListKey` handler
- **After:** Synchronous action-based selection with immediate data processing

### 4. **Simplified Error Handling**
- **Before:** `On Error GoTo` with error labels
- **After:** Structured `try-catch` blocks

### 5. **Modern UI Controls**
- **Before:** Buttons for navigation
- **After:** LinkLabels for navigation (more semantic)

### 6. **Reduced State Management**
- **Before:** Window tracking variables (`NumChildWinX`)
- **After:** Framework handles window state internally

---

## Next Steps for Conversion

1. **Identify all button click handlers** that need conversion
2. **Categorize handlers:**
   - Navigation buttons → Convert to LinkLabels
   - Action buttons → Keep as Buttons
3. **Map filter files to action names** (create mapping table)
4. **Create `Ubs_ActionRunBegin` handler** for grid configurations
5. **Update Designer.cs** files to wire new events
6. **Test each conversion** to ensure functionality matches

---

## Related Files

- **VB.NET Source:** `source\BG_CONTRACT\BG_Contract.dob`
- **C# Target:** `source\UbsBgContractFrm\UbsBgContractFrm.cs`
- **Designer:** `source\UbsBgContractFrm\UbsBgContractFrm.Designer.cs`

---

*Document created: 2026-02-13*
*Based on analysis of successful conversion: btnFrameContract_Click → linkFrameContract_LinkClicked*
