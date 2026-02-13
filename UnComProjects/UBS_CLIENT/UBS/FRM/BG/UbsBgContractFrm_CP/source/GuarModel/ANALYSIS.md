# VB6 GuarModel_ud.dob - Complete Analysis

## Document Information
- **Source File:** `source/GuarModel/GuarModel_ud.dob`
- **File Type:** VB6 UserDocument (ActiveX Document)
- **Total Lines:** 740
- **Project Type:** ActiveX DLL (OleDll)
- **Analysis Date:** 2026-01-26

---

## 1. Project Structure

### 1.1 Project Configuration
- **Type:** OleDll (ActiveX DLL)
- **Name:** GuarModel
- **Version:** 1.0.23
- **Company:** UniSAB
- **Threading Model:** Single-threaded (MaxNumberOfThreads=1)

### 1.2 Dependencies & References
- **UBSChilds.dll** - UBSChild interface implementation
- **UBSParrents.dll** - Parent window management
- **sstabs2.ocx** - ActiveTabs.SSActiveTabs control (tab control)
- **UbsInfo.ocx** - UbsInfo32.Info control (info display)
- **UbsProp.dll** - UBSPROPLibCtl.UbsControlProperty (property grid)
- **UbsChlCtrl.ocx** - UbsChlCtrl.UbsChannel (channel communication)

---

## 2. Form Structure & Layout

### 2.1 UserDocument Properties
- **ClientWidth:** 7350 twips (~490 pixels at 96 DPI)
- **ClientHeight:** 4410 twips (~294 pixels at 96 DPI)
- **KeyPreview:** True (handles keyboard events at form level)
- **HScrollSmallChange:** 225 twips
- **VScrollSmallChange:** 225 twips
- **ScaleMode:** Twips (default)

### 2.2 Control Hierarchy

```
GuarModel_ud (UserDocument)
├── SSActiveTabs1 (ActiveTabs.SSActiveTabs)
│   ├── SSActiveTabPanel1 (Main Tab)
│   │   ├── txtMeaning (VB.TextBox) - Name/Title field
│   │   ├── cmbPattern (VB.ComboBox) - Template/Model selector
│   │   ├── cmbExecut (VB.ComboBox) - Responsible executor
│   │   ├── cmbState (VB.ComboBox) - State selector
│   │   ├── cmbClientType (VB.ComboBox) - Client type selector
│   │   └── Labels (Label1-5) - Field labels
│   └── SSActiveTabPanel2 (Properties Tab)
│       └── ucpParam (UBSPROPLibCtl.UbsControlProperty) - Additional properties
├── cmdSave (VB.CommandButton) - Save button
├── cmdExit (VB.CommandButton) - Exit button
├── Info (UbsInfo32.Info) - Status/info display
└── UbsChannel (UbsChlCtrl.UbsChannel) - Channel communication (invisible)
```

### 2.3 Control Details

#### Main Tab (SSActiveTabPanel1) Controls

| Control | Type | Properties | Purpose |
|---------|------|------------|---------|
| `txtMeaning` | TextBox | MaxLength=255, TabIndex=0 | Name/Title of guarantee model |
| `cmbPattern` | ComboBox | Style=2 (Dropdown List), TabIndex=1 | Template/Model selection |
| `cmbExecut` | ComboBox | Style=2 (Dropdown List), TabIndex=2 | Responsible executor |
| `cmbState` | ComboBox | Style=2 (Dropdown List), TabIndex=3 | State/Status |
| `cmbClientType` | ComboBox | Style=2 (Dropdown List), TabIndex=15 | Client type |
| `Label1` | Label | Alignment=Right, Caption="Наименование" | Name label |
| `Label2` | Label | Alignment=Right, Caption="Шаблон" | Template label |
| `Label3` | Label | Alignment=Right, Caption="Ответственный исполнитель" | Executor label |
| `Label4` | Label | Alignment=Right, Caption="Состояние" | State label |
| `Label5` | Label | Alignment=Right, Caption="Тип клиента" | Client type label |

#### Properties Tab (SSActiveTabPanel2) Controls

| Control | Type | Properties | Purpose |
|---------|------|------------|---------|
| `ucpParam` | UbsControlProperty | Full width/height | Additional properties grid |

#### Bottom Controls

| Control | Type | Properties | Purpose |
|---------|------|------------|---------|
| `cmdSave` | CommandButton | Caption="Сохранить", TabIndex=5 | Save form data |
| `cmdExit` | CommandButton | Caption="Выход", TabIndex=6 | Close form |
| `Info` | UbsInfo32.Info | Caption="Данные сохранены!", Font=Bold | Status message display |

---

## 3. Variable Declarations

### 3.1 Module-Level Variables

```vb
' Window Management
Dim NumWin As Integer                    ' Current window number
Dim NumParentWindow As Integer           ' Parent window number
Dim NumChildWin As Integer               ' Child window number (not used)
Dim Parent As UBSParrent                ' Parent object reference

' Command & State
Dim StrCommand As String                 ' Command parameter ("Edit", etc.)
Dim idObject As String                   ' Object ID for editing
Dim LoadParam As String                  ' Current load parameter name
Dim blnFlag As Boolean                  ' Flag to prevent recursive events
Dim bNeedRefreshGrid As Boolean         ' Flag to refresh parent grid
Dim bParentWindowClose As Boolean       ' Flag for parent window close

' COM Objects
Dim objErr As Object                     ' Error handler (ToolPubs.IUbsError)
Dim objInterfaces As Object              ' Interface utilities (ToolPubs.Interfaces)
Dim objFormParam As Object               ' Form parameters (ToolPubs.IUbsParam)
Dim objFocusCtrl As Object               ' Currently focused control
Dim objParamIn As Object                 ' Input parameters (ToolPubs.IUbsParam)
Dim objParamOut As Object                ' Output parameters (ToolPubs.IUbsParam)
Dim objStub As Object                    ' Stub object (UbsAddFiledsStub.IUbsAddFiledsStub)
Dim objArray As Object                   ' Array utilities (Lib1.IUbsArray)
Dim ActiveControl As Object              ' Active control reference

' Data Arrays
Dim arrState As Variant                  ' States array (2D: [ID, Name])
Dim arrPattern As Variant                ' Templates array (2D: [ID, Name])
Dim arrExecut As Variant                 ' Executors array (2D: [ID, Name])
Dim arrIdPattern As Variant              ' Template ID mapping (2D: [Index, ID])
Dim arrTypeClient As Variant             ' Client types array (2D: [ID, Name])

' State Flags
Dim blnAddFlChanged As Boolean          ' Additional fields changed flag
```

### 3.2 Variable Usage Patterns
- **COM Objects:** Created via `CreateObject()` in initialization
- **Arrays:** 2D Variant arrays with structure `[ID, Name]` (0-based indexing)
- **Flags:** Boolean flags to control form behavior and prevent recursion

---

## 4. Interface Implementation

### 4.1 UBSChild Interface

The form implements the `UBSChild` interface for integration with the UBS parent system.

#### Key Properties & Methods:

| VB6 Property/Method | Purpose | .NET Equivalent |
|---------------------|---------|-----------------|
| `UBSChild_ParamInfo` (Let) | Receives initialization parameters | `ListKey` method |
| `UBSChild_ParamInfo` (Get) | Returns form parameters | `CommandLine` method |
| `UBSChild_NumWindow` (Let) | Sets window number | Base class property |
| `UBSChild_Parrent` (Let) | Sets parent reference | Base class property |
| `UBSChild_Height` (Get) | Returns fixed height (4410) | Form.Height |
| `UBSChild_Width` (Get) | Returns fixed width (7380) | Form.Width |
| `UBSChild_HeightBrowser` (Let) | Dynamic height adjustment | Form resize handling |
| `UBSChild_WidthBrowser` (Let) | Dynamic width adjustment | Form resize handling |
| `UBSChild_TitleWindow` (Get) | Window title | Form.Text |
| `UBSChild_Sizable` (Get) | Form is resizable (True) | Form.FormBorderStyle |
| `UBSChild_NotifyCloseWindow` | Parent window close notification | Event handler |
| `UBSChild_NotifyFixWindow` | Window pin/unpin notification | Event handler |
| `UBSChild_IsExistParam` | Parameter existence check | Base class method |

#### Parameter Support:
- `InitParamForm` - Main initialization parameter
- `SetFocus` - Set focus to saved control
- `LostFocus` - Save current focused control
- `WaitShow` - Wait before showing (returns True)

---

## 5. Event Handlers

### 5.1 Form Lifecycle Events

#### `UserDocument_Initialize`
- **Purpose:** Initial COM object creation
- **Actions:**
  - Creates `UbsAddFiledsStub.IUbsAddFiledsStub` (objStub)
  - Creates `ToolPubs.IUbsParam` (objFormParam)
  - Calls `ArrayParam()` to register supported parameters

#### `UserDocument_Hide`
- **Purpose:** Cleanup when form is hidden/closed
- **Actions:**
  - Releases UBS channel via `objStub.ReleaseUbsChannel`
  - Sets all COM objects to Nothing
  - Notifies parent about grid refresh if needed

### 5.2 Button Events

#### `cmdSave_Click`
- **Purpose:** Save form data to server
- **Flow:**
  1. Disable buttons
  2. Validate form (`Check()`)
  3. Clear parameters
  4. Set input parameters:
     - `StrCommand` - Current command
     - `Наименование` - Name from txtMeaning
     - `Шаблон` - Template ID from arrIdPattern
     - `ОИ` - Executor ID from cmbExecut
     - `Состояние` - State index from cmbState
     - `Тип клиента` - Client type ID from cmbClientType (or 0)
  5. Call `UbsChannel.Run "GuarModelEdit"`
  6. Check for errors
  7. Update command to "Edit" if new record
  8. Show success message
  9. Disable cmbPattern (template locked after save)
  10. Re-enable buttons

#### `cmdExit_Click`
- **Purpose:** Close form
- **Action:** `Parent.CloseWindow NumWin`

### 5.3 ComboBox Events

#### `cmbPattern_Click`
- **Purpose:** Load additional fields when template changes
- **Flow:**
  1. Check `blnFlag` to prevent recursion
  2. Set template parameter
  3. Call `UbsChannel.Run "GuarModelInitUcp"`
  4. Refresh `ucpParam` control

### 5.4 Property Control Events

#### `ucpParam_KeyPress`
- **Purpose:** Handle keyboard navigation in property grid
- **Key Handlers:**
  - **Enter (13):** Move to next control if at last property
  - **Esc (27):** Return to main tab, focus cmbState

#### `ucpParam_TextChange`
- **Purpose:** Track changes in additional fields
- **Action:** Set `blnAddFlChanged = True`

### 5.5 Keyboard Events

#### `UserDocument_KeyPress`
- **Purpose:** Global keyboard handling
- **Key Handlers:**
  - **Enter (13):**
    - If `cmbState` active → Go to properties tab or cmdSave
    - Otherwise → Move to next control
  - **Esc (27):**
    - If `cmdExit` → Go to properties tab or cmdSave
    - If `cmdSave` → Go to properties tab or cmbState
    - If `txtMeaning` → Focus cmdExit
    - Otherwise → Move to previous control

#### `UserDocument_KeyDown`
- **Purpose:** Handle Shift+Tab for window navigation
- **Action:** If Shift+Tab pressed, notify parent to move to next window

---

## 6. Business Logic Methods

### 6.1 Initialization Methods

#### `Initialize`
- **Purpose:** Create error and interface objects
- **Creates:**
  - `ToolPubs.IUbsError` (objErr)
  - `ToolPubs.Interfaces` (objInterfaces)
  - `Lib1.IUbsArray` (objArray)

#### `InitChannel`
- **Purpose:** Configure UBS channel
- **Actions:**
  - Set `UbsChannel.HostAddress` from parent
  - Set `UbsChannel.UserGUID` from parent
  - Set `UbsChannel.ParentHwnd` to form handle
  - Set `UbsChannel.LoadResource` to "VBS:UBS_VBD\GUAR\GuarModel.vbs"
  - Create `objParamIn` and `objParamOut` objects

#### `InitDoc`
- **Purpose:** Load form data and initialize controls
- **Flow:**
  1. Set `objStub.UbsChannel = UbsChannel`
  2. Call `UbsChannel.Run "GuarModelInit"`
  3. Load templates (arrPattern) into cmbPattern
  4. Create arrIdPattern mapping array
  5. Load client types (arrTypeClient)
  6. Load executors (arrExecut) into cmbExecut
  7. Load states (arrState) into cmbState
  8. Call `cmbPattern_Click` to initialize properties
  9. If Edit mode:
     - Call `GuarModelRead` to load existing data
     - Populate all fields
     - Disable cmbPattern
  10. Initialize cmbClientType with client types
  11. Set `ucpParam.UbsAddFields = objStub`
  12. Refresh `ucpParam`

#### `InitCmdClientType`
- **Purpose:** Initialize client type combo box
- **Parameters:**
  - `arrTypeClient` - Client types array
  - `clType` - Current client type ID
- **Logic:**
  - If Edit mode and clType=0, prepend empty option
  - Populate combo box
  - Set selected value

### 6.2 Validation Methods

#### `Check`
- **Purpose:** Validate form before save
- **Validation:**
  - `txtMeaning.Text` must have length >= 1
- **On Error:**
  - Show message "Задайте наименование."
  - Focus txtMeaning
  - Select main tab
  - Return False

### 6.3 Helper Methods

#### `ArrayParam`
- **Purpose:** Register supported parameters with form parameter object
- **Parameters Registered:**
  - `WaitShow` = True
  - `InitParamForm` = True
  - `SetFocus` = True
  - `LostFocus` = True

#### `CheckParamForClose`
- **Purpose:** Determine if form should close on error
- **Parameters that allow close:**
  - `LoadFromMenu`
  - `InitParamForm`
  - `InitParamGrid`
- **Returns:** Boolean

---

## 7. Data Flow & Channel Communication

### 7.1 Channel Initialization
```
InitChannel()
  ├── Set HostAddress from parent
  ├── Set UserGUID from parent
  ├── Set ParentHwnd
  ├── Set LoadResource = "VBS:UBS_VBD\GUAR\GuarModel.vbs"
  └── Create objParamIn and objParamOut
```

### 7.2 Channel Operations

#### `GuarModelInit`
- **Purpose:** Initialize form with master data
- **Input:** None (cleared parameters)
- **Output:**
  - `Шаблоны` - Templates array [ID, Name]
  - `Типы клиентов` - Client types array [ID, Name]
  - `ОИ` - Executors array [ID, Name]
  - `Состояния` - States array [ID, Name]

#### `GuarModelInitUcp`
- **Purpose:** Initialize additional properties for selected template
- **Input:**
  - `Шаблон` - Template ID
- **Output:** (Used by ucpParam control)

#### `GuarModelRead`
- **Purpose:** Load existing record for editing
- **Input:**
  - `Id` - Object ID
- **Output:**
  - `Наименование` - Name
  - `Шаблон` - Template ID
  - `ОИ` - Executor ID
  - `Состояние` - State index
  - `Тип клиента` - Client type ID (optional)

#### `GuarModelEdit`
- **Purpose:** Save form data (create or update)
- **Input:**
  - `StrCommand` - Command ("Edit" or empty)
  - `Наименование` - Name
  - `Шаблон` - Template ID
  - `ОИ` - Executor ID
  - `Состояние` - State index
  - `Тип клиента` - Client type ID
- **Output:**
  - `Error` - Error message (if any)

### 7.3 Array Handling

#### Template Array Structure
```vb
arrPattern(0, q) = Template ID
arrPattern(1, q) = Template Name

arrIdPattern(0, q) = ComboBox Index
arrIdPattern(1, q) = Template ID
```

#### Other Arrays Structure
```vb
arrExecut(0, q) = Executor ID
arrExecut(1, q) = Executor Name

arrState(0, q) = State ID
arrState(1, q) = State Name

arrTypeClient(0, q) = Client Type ID
arrTypeClient(1, q) = Client Type Name
```

---

## 8. Control Conversion Mapping

### 8.1 VB6 to .NET Control Mapping

| VB6 Control | .NET Control | Conversion Notes |
|-------------|--------------|------------------|
| `VB.UserDocument` | `UbsFormBase` | Inherit from base class |
| `VB.CommandButton` | `System.Windows.Forms.Button` | Direct conversion |
| `VB.TextBox` | `System.Windows.Forms.TextBox` | Direct conversion |
| `VB.ComboBox` (Style=2) | `System.Windows.Forms.ComboBox` | Set `DropDownStyle.DropDownList` |
| `VB.Label` | `System.Windows.Forms.Label` | Direct conversion |
| `ActiveTabs.SSActiveTabs` | `System.Windows.Forms.TabControl` | Manual conversion |
| `ActiveTabs.SSActiveTabPanel` | `System.Windows.Forms.TabPage` | Manual conversion |
| `UbsInfo32.Info` | `UbsControl.UbsCtrlInfo` | Already .NET control |
| `UBSPROPLibCtl.UbsControlProperty` | `UbsControl.UbsCtrlFields` | Already .NET control |
| `UbsChlCtrl.UbsChannel` | `IUbsChannel` (from base) | Use base class channel |

### 8.2 Control Naming Conversion

| VB6 Name | .NET Name | Notes |
|----------|-----------|-------|
| `cmdSave` | `btnSave` | Button naming convention |
| `cmdExit` | `btnExit` | Button naming convention |
| `txtMeaning` | `tbName` | TextBox naming convention |
| `cmbPattern` | `cbModel` | ComboBox naming convention |
| `cmbExecut` | `cbExecutor` | ComboBox naming convention |
| `cmbState` | `cbState` | ComboBox naming convention |
| `cmbClientType` | `cbClientType` | Keep same |
| `Info` | `ubsCtrlInfo` | UBS control naming |
| `ucpParam` | `ubsCtrlAddFields` | UBS control naming |
| `SSActiveTabs1` | `tabControl1` | TabControl naming |
| `SSActiveTabPanel1` | `tabPage1` | TabPage naming |
| `SSActiveTabPanel2` | `tabPage2` | TabPage naming |
| `Label1-5` | `label1-5` | Keep same |

---

## 9. Key Conversion Challenges

### 9.1 UBSChild Interface
- **Challenge:** VB6 uses property procedures, .NET uses methods
- **Solution:** Map `UBSChild_ParamInfo` to `CommandLine`/`ListKey` methods
- **Implementation:** Use base class `Ubs_AddName()` to register handlers

### 9.2 ActiveX Tab Control
- **Challenge:** `SSActiveTabs` is ActiveX, needs manual conversion
- **Solution:** Use standard `TabControl` with `TabPage` controls
- **Note:** Tab selection logic needs conversion

### 9.3 COM Object Creation
- **Challenge:** VB6 uses `CreateObject()`, .NET needs different approach
- **Solution:** Use base class helper methods or `IUbsChannel` interface
- **Note:** Some objects (objStub, objArray) may need wrapper classes

### 9.4 Array Indexing
- **Challenge:** VB6 arrays can be 0-based or 1-based
- **Solution:** All arrays in this code are 0-based (UBound with 2nd dimension)
- **Note:** Verify all array access uses 0-based indexing

### 9.5 Variant Type Handling
- **Challenge:** VB6 Variant is flexible, .NET needs strong typing
- **Solution:** Use `object` with `as` casting and null checks
- **Note:** 2D arrays use `object[,]` type

### 9.6 ComboBox Data Binding
- **Challenge:** VB6 uses `AddItem` and `ItemData`, .NET uses `DataSource`
- **Solution:** Use `KeyValuePair<short, string>` list with `ValueMember`/`DisplayMember`
- **Note:** Need helper method `InitComboBox()`

### 9.7 Keyboard Navigation
- **Challenge:** VB6 `KeyPress` events, .NET uses different event model
- **Solution:** Use `Form_KeyPress` or override `ProcessDialogKey`
- **Note:** `KeyPreview = True` equivalent is form-level event handling

### 9.8 Dynamic Sizing
- **Challenge:** VB6 `UBSChild_HeightBrowser`/`WidthBrowser` properties
- **Solution:** Handle `Form_Resize` event or override sizing methods
- **Note:** Minimum sizes: Width=7380, Height=4410 (twips)

---

## 10. Error Handling Patterns

### 10.1 VB6 Error Handling
```vb
On Error GoTo ErrorCode
    ' Code here
    Exit Sub
ErrorCode:
    objErr.UbsErrMsg "MethodName", "error description"
```

### 10.2 .NET Error Handling
```csharp
try {
    // Code here
} catch (Exception ex) {
    this.Ubs_ShowError(ex);
}
```

### 10.3 Error Handling Locations
- All event handlers use error handling
- `InitDoc` has error handling with close check
- `cmdSave_Click` re-enables buttons on error
- `InitChannel` has error handling

---

## 11. State Management

### 11.1 Form States
- **New Record:** `StrCommand` = "" (empty)
- **Edit Record:** `StrCommand` = "Edit"
- **After Save:** `StrCommand` = "Edit", `cmbPattern.Enabled = False`

### 11.2 Flags
- **blnFlag:** Prevents recursive `cmbPattern_Click` during initialization
- **blnAddFlChanged:** Tracks changes in additional fields
- **bNeedRefreshGrid:** Signals parent to refresh grid on close
- **bParentWindowClose:** Indicates parent window is closing

### 11.3 Control States
- **txtMeaning.Enabled:** False in Edit mode
- **cmbPattern.Enabled:** False after save or in Edit mode
- **cmdSave.Enabled:** Temporarily disabled during save operation
- **cmdExit.Enabled:** Temporarily disabled during save operation

---

## 12. Data Validation Rules

### 12.1 Required Fields
- **Наименование (txtMeaning):** Must have length >= 1

### 12.2 Validation Flow
1. `Check()` method validates required fields
2. On validation failure:
   - Show error message
   - Focus invalid field
   - Select appropriate tab
   - Re-enable buttons
   - Return False

### 12.3 Server-Side Validation
- Server may return `Error` parameter from `GuarModelEdit`
- Error message displayed in MessageBox
- Buttons re-enabled on error

---

## 13. Integration Points

### 13.1 Parent Communication
- **Parent Object:** `UBSParrent` interface
- **Methods Used:**
  - `Parent.CloseWindow(NumWin)` - Close form
  - `Parent.LoaderParamInfo(NumWin, "HostAddress")` - Get host address
  - `Parent.LoaderParamInfo(NumWin, "GuidSession")` - Get session GUID
  - `Parent.WindowDraw(NumWin) = True` - Enable window drawing
  - `Parent.ParamInfo(NumParentWindow, "NeedRefreshGrid")` - Notify refresh

### 13.2 Window Management
- **NumWin:** Current window identifier
- **NumParentWindow:** Parent window identifier (for notifications)
- **Window Notifications:**
  - `UBSChild_NotifyCloseWindow` - Parent window closing
  - `UBSChild_NotifyFixWindow` - Window pin/unpin

### 13.3 Interface Utilities
- **objInterfaces:** `ToolPubs.Interfaces` object
- **Methods Used:**
  - `NextCtrl` - Move to next control
  - `PrevCtrl` - Move to previous control
  - `GoToCtrl` - Focus specific control
  - `SetFocusCombo` - Set combo box selection by value

---

## 14. Resource Strings

### 14.1 Hardcoded Strings (Russian)
- **Window Title:** "Типовой договор обеспечения"
- **Button Captions:**
  - "Сохранить" (Save)
  - "Выход" (Exit)
- **Labels:**
  - "Наименование" (Name)
  - "Шаблон" (Template)
  - "Ответственный исполнитель" (Responsible Executor)
  - "Состояние" (State)
  - "Тип клиента" (Client Type)
- **Messages:**
  - "Данные сохранены!" (Data saved!)
  - "Не выбран типовой договор!" (No template selected!)
  - "Массив шаблонов пуст!" (Templates array is empty!)
  - "Массив ответственных исполнителей пуст!" (Executors array is empty!)
  - "Массив состояний пуст!" (States array is empty!)
  - "Массив типов клиента пуст!" (Client types array is empty!)
  - "Задайте наименование." (Enter name.)

### 14.2 String Localization
- All strings are hardcoded in Russian
- Consider moving to resource files for .NET conversion
- Error messages use `objErr.UbsErrMsg` with method name and description

---

## 15. Conversion Priority Checklist

### Priority 1 - Core Structure ✓
- [x] Analyze VB6 structure
- [ ] Create .NET project file
- [ ] Create form class inheriting `UbsFormBase`
- [ ] Implement `CommandLine` and `ListKey` methods
- [ ] Convert constructor and basic initialization

### Priority 2 - UI Controls
- [ ] Convert TabControl structure
- [ ] Convert all input controls (TextBox, ComboBox)
- [ ] Convert buttons and labels
- [ ] Convert UBS controls (UbsCtrlInfo, UbsCtrlFields)

### Priority 3 - Business Logic
- [ ] Convert `InitForm` method
- [ ] Convert `InitDoc` equivalent
- [ ] Convert `cmdSave_Click` logic
- [ ] Convert validation (`Check` method)
- [ ] Convert combo box initialization

### Priority 4 - Event Handlers
- [ ] Convert all button click handlers
- [ ] Convert combo box change handlers
- [ ] Convert keyboard event handlers
- [ ] Convert UBS control events

### Priority 5 - Polish
- [ ] Error handling refinement
- [ ] Resource string management
- [ ] Code comments and documentation
- [ ] Testing and debugging

---

## 16. Notes & Observations

### 16.1 Code Quality
- **Error Handling:** Comprehensive error handling in all methods
- **Code Organization:** Well-structured with clear method separation
- **Comments:** Minimal comments, mostly in Russian
- **Naming:** Hungarian notation (cmd, txt, cmb prefixes)

### 16.2 Potential Issues
- **Array Bounds:** All arrays use 0-based indexing (verified)
- **Null Handling:** Some checks for `IsEmpty()` and `IsArray()`
- **Recursion Prevention:** `blnFlag` prevents recursive template loading
- **Focus Management:** Complex focus handling for keyboard navigation

### 16.3 Differences from Pattern
- **Pattern File:** `UbsGuarModelFrm.cs` already exists and is converted
- **This Analysis:** For reference and verification of conversion completeness
- **Additional Features:** Some features in converted version (rights checking) not in VB6

### 16.4 Testing Considerations
- Test all combo box initialization
- Test template change triggers property refresh
- Test save operation in both new and edit modes
- Test keyboard navigation (Enter, Esc, Tab)
- Test form resize handling
- Test error scenarios (empty arrays, server errors)
- Test parent window close notification

---

## End of Analysis

This analysis document provides a complete reference for converting the VB6 `GuarModel_ud.dob` UserDocument to .NET Windows Forms. All controls, events, business logic, and integration points have been documented for reference during the conversion process.
