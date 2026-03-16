# CREATIVE PHASE: Commission_ud → UbsOpCommissionFrm Conversion Architecture

**Scope:** Design decisions for converting Commission_ud (VB6 UserDocument) to UbsOpCommissionFrm (.NET WinForm).  
**Context:** VB6 UBSChild, DDX, UbsControlProperty; .NET IUbs, UbsFormBase, UbsCtrlFields.  
**Reference:** GuarModel ANALYSIS.md, BG_CONTRACT conversion pattern.

---

## CREATIVE PHASE START: Commission Conversion Architecture

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

### 1️⃣ PROBLEM

**Description:** Commission_ud uses VB6 patterns (UBSChild, InitParamForm, DDX, UbsControlProperty) that do not map 1:1 to .NET IUbs (CommandLine, ListKey). We need to decide: (1) how to map UBSChild param flow to CommandLine/ListKey, (2) how to bind txbName/txbDesc (DDX → .NET), (3) which .NET control replaces UbsControlProperty, (4) tab order and layout.

**Requirements:**
- Preserve behavior: ADD/EDIT modes, Get_Data, Com_Save, CheckData
- Follow UBS form pattern (UbsFormBase, IUbs, panelMain, tableLayoutPanel)
- Compatible with channel (params Действие, Наименование, Описание, Идентификатор)
- Russian text preserved

**Constraints:**
- .NET Framework 2.0, Windows Forms
- Must use existing UBS .NET controls (UbsCtrlInfo, UbsCtrlFields, etc.)
- No breaking channel contract

---

### 2️⃣ OPTIONS

**Option A: Direct IUbs mapping (CommandLine = command, ListKey = ID/ItemArray)**  
CommandLine receives command string (ADD/EDIT). ListKey receives param_in with ID (for EDIT) or empty (for ADD). InitDoc reads m_command and m_id from fields set by these handlers. Same pattern as BG_CONTRACT.

**Option B: Single CommandLine with composite param**  
CommandLine receives an object with command + ID + optional ItemArray. ListKey unused or minimal. Simpler handler count but diverges from BG pattern.

**Option C: ListKey-only for init params**  
ListKey receives all init data (command, ID, ItemArray). CommandLine only stores command string. InitDoc reads from m_command and m_paramIn/m_paramOut. Closer to VB6 InitParamForm which receives composite data.

**Option D: Hybrid — CommandLine for command, ListKey for list selection**  
CommandLine: command string. ListKey: when opened from list, param_in contains ID (and optionally ItemArray). When ADD, ListKey not called or param_in empty. Matches BG_CONTRACT ListKey usage.

---

### 3️⃣ ANALYSIS

| Criterion           | Option A | Option B | Option C | Option D |
|---------------------|----------|----------|----------|----------|
| Consistency with BG | ⭐⭐⭐⭐⭐  | ⭐⭐      | ⭐⭐⭐     | ⭐⭐⭐⭐⭐  |
| Simplicity          | ⭐⭐⭐⭐   | ⭐⭐⭐     | ⭐⭐⭐     | ⭐⭐⭐⭐   |
| VB6 parity          | ⭐⭐⭐⭐   | ⭐⭐⭐     | ⭐⭐⭐⭐   | ⭐⭐⭐⭐⭐  |
| Channel compatibility | ⭐⭐⭐⭐⭐ | ⭐⭐⭐⭐   | ⭐⭐⭐⭐   | ⭐⭐⭐⭐⭐  |

**Insights:**
- Option A and D are equivalent for Commission: CommandLine = command, ListKey = ID when from list. BG_CONTRACT uses ListKey to apply list selection (ID, etc.); CommandLine for command. **Option D (same as A)** is the standard.
- Option B diverges from UBS form pattern; Option C overcomplicates.

---

### 4️⃣ DECISION

**Selected:** **Option A/D — Direct IUbs mapping.**

**Rationale:**
- **CommandLine(param_in, param_out):** param_in = command string ("ADD" or "EDIT"). Store in m_command.
- **ListKey(param_in, param_out):** param_in = object with ID (for EDIT) or empty/null (for ADD). Extract m_id from param_in when present. Matches VB6 InitParamForm: RHS(2)=StrCommand, RHS(1)=ItemArray with ID.
- **InitDoc:** Called after CommandLine+ListKey. Uses m_command and m_id. For ADD: m_id=0, clear fields. For EDIT: call Get_Data with Идентификатор=m_id, fill txbName, txbDesc, ucpParam.

---

### 5️⃣ IMPLEMENTATION NOTES

#### 5.1 Control Mapping (from GuarModel ANALYSIS.md)

| VB6 | .NET |
|-----|------|
| VB.UserDocument | UbsFormBase |
| SSActiveTabs | TabControl |
| SSActiveTabPanel | TabPage |
| VB.TextBox (txbName, txbDesc) | TextBox |
| UBSPROPLibCtl.UbsControlProperty (ucpParam) | UbsControl.UbsCtrlFields |
| UbsInfo32.Info | UbsControl.UbsCtrlInfo |
| UbsDDXControl | Manual: m_addFields + read/write in InitDoc/btnSave |

**Add to csproj:** UbsCtrlFields reference (for ucpParam replacement).

#### 5.2 Tab Order

- **Tab 1 (first, selected by default):** txbName, txbDesc (Наименование, Описание) — main data. Matches VB6 SSTabs.Tabs(1).Selected = True, txbName.SetFocus.
- **Tab 2:** ubsCtrlAddFields (UbsCtrlFields) — dynamic add-fields. Matches VB6 Tab 2 with ucpParam.

#### 5.3 Field Binding (DDX → .NET)

- **m_addFields:** Add txbName and txbDesc to IUbsFieldCollection with keys "Наименование", "Описание".
- **InitDoc:** For EDIT, read Наименование/Описание from objParamOut after Get_Data; assign to txbName.Text, txbDesc.Text. For ADD, clear.
- **btnSave:** Read txbName.Text, txbDesc.Text; validate (CheckData); set objParamIn.Parameter("Наименование"), objParamIn.Parameter("Описание"); run Com_Save.
- **ucpParam / ubsCtrlAddFields:** Set UbsAddFields from stub/channel; call Refresh. Same pattern as Commission_ud: ucpParam.UbsAddFields = objStub, ucpParam.Refresh.

#### 5.4 LoadResource

- **VB6:** `VBS:UBS_VBD\OP\Commission.vbs`
- **.NET:** ASM equivalent. Template has `ASM:UBS_ASM\Business\DllName.dll->UbsBusiness.NameClass`. For OP Commission, use `ASM:UBS_ASM\Business\DllName.dll->UbsBusiness.UbsOpCommissionFrm` or verify with server/ASM configuration. Document as constant; update when correct path is confirmed.

#### 5.5 Constants to Add

- Commands: AddCommand, EditCommand
- Channel actions: GetDataAction, ComSaveAction
- Params: ParamAction, ParamName, ParamDesc, ParamId
- Messages: MsgCommissionListEmpty, MsgNameRequired, MsgCheckProps, MsgDataSaved, MsgError

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━  
CREATIVE PHASE END

---

## CREATIVE PHASE VERIFICATION

- [x] Problem clearly defined  
- [x] Multiple options considered (4)  
- [x] Decision made with rationale  
- [x] Implementation guidance provided (control mapping, tab order, field binding, LoadResource, constants)  
- [x] Document saved to `memory-bank/creative/`

**→ NEXT: BUILD** (Phase 2 conversion)
