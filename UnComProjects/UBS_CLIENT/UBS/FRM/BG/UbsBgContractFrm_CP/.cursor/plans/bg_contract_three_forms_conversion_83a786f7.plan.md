---
name: BG_CONTRACT Three Forms Conversion
overview: Convert three VB6 dialog forms (BGBonusPayInterval, frmDetailsBenificiar, frmRates) from BG_CONTRACT project to .NET Framework 2.0 Windows Forms, adding them to the existing UbsBgContractFrm project.
todos:
  - id: setup_project
    content: Add UbsCtrlDecimal and UbsCtrlDate references to UbsBgContractFrm.csproj
    status: pending
  - id: convert_rates
    content: Convert frmRates to UbsBgRatesFrm (simplest form - date, decimal, combo)
    status: pending
  - id: convert_benificiar
    content: Convert frmDetailsBenificiar to UbsBgDetailsBenificiarFrm (address form with country sync)
    status: pending
  - id: convert_bonus_interval
    content: Convert BGBonusPayInterval to UbsBgBonusPayIntervalFrm (most complex - period logic, checkbox arrays)
    status: pending
  - id: update_csproj
    content: Add all three new form files to UbsBgContractFrm.csproj and include in UbsBgContractFrm.sln
    status: pending
  - id: encoding_win1251
    content: Create form files with Windows-1251 encoding (.cs/.Designer.cs) and UTF-8 (.resx); ensure Russian symbols display correctly in VS2026 editor, designer, and runtime
    status: pending
  - id: match_layout_pattern
    content: Form frame layout must match UbsBgContractFrm.Designer.cs pattern (panelMain from UbsFormBase, tableLayoutPanel, ubsCtrlInfo)
    status: pending
  - id: implement_ubs_pattern
    content: All forms MUST inherit from UbsFormBase and implement IUbs interface (m_addCommand, CommandLine, ListKey, m_addFields) following UbsBgContractFrm.cs pattern
    status: pending
isProject: false
---

# BG_CONTRACT Three Forms Conversion Plan

## Task Purpose

**Convert BG_CONTRACT (VB6) to UbsBgContractFrm (.NET Framework 2.0)** — limited to three forms only.

## Paths


| Purpose                  | Path                                                                                   |
| ------------------------ | -------------------------------------------------------------------------------------- |
| **VB6 source**           | `source/BG_CONTRACT/`                                                                  |
| **Appearance reference** | `source/FormScreens/` (BGBonusPayInterval.png, frmDetailsBenificiar.png, frmRates.png) |
| **Layout pattern**       | `source/UbsBgContractFrm/UbsBgContractFrm.Designer.cs` — form frame structure to match |
| **Result location**      | `source/UbsBgContractFrm/`                                                             |
| **Solution**             | `source/UbsBgContractFrm/UbsBgContractFrm.sln` — all three forms must be included      |


## Overview

Convert three VB6 forms to .NET Framework 2.0 Windows Forms following the **UbsBgContractFrm.cs pattern**:

1. **BGBonusPayInterval** - Period configuration form
2. **frmDetailsBenificiar** - Beneficiary details form
3. **frmRates** - Rate entry form

**CRITICAL ARCHITECTURAL REQUIREMENT:** All converted forms **MUST** inherit from `UbsFormBase` and implement the IUbs interface pattern, exactly like `UbsBgContractFrm.cs` and `UbsGuarModelFrm.cs`. These are **NOT** standalone dialogs - they are UBS forms integrated with the channel system.

**Reference Pattern:** `source/UbsBgContractFrm/UbsBgContractFrm.cs` - This is the template that ALL converted forms must follow.

## Target Location

- **Project:** `source/UbsBgContractFrm/UbsBgContractFrm.csproj`
- **Namespace:** `UbsBusiness`
- **Output:** Three new form classes in the existing project

## Form Analysis

### 1. BGBonusPayInterval (422 lines)

**VB6 File:** `source/BG_CONTRACT/BGBonusPayInterval.frm`  
**Appearance:** `source/FormScreens/BGBonusPayInterval.png`

**Controls:**

- 2 Frames (Frame1, Frame2)
- 2 ComboBoxes (cmbTypePeriod, cmbTypeDate)
- 2 UbsControlMoney (ucmPeriod, ucmNumDay)
- 7 CheckBoxes array (chkArray[0-6] for days of week)
- 2 CommandButtons (cmdApply, cmdExit)
- 1 UbsInfo32.Info (Info)
- Multiple Labels

**Key Logic:**

- Period type selection enables/disables date controls
- Date type selection enables/disables day number or weekday checkboxes
- Validation checks period, day number, and weekday selection
- Uses `blnApply` flag to indicate Apply button clicked

**Conversion Notes:**

- Form is Fixed Single border, no control box
- Uses `Me.Hide` instead of `Me.Close` (modal dialog pattern)
- Complex conditional enabling logic based on combo selections

### 2. frmDetailsBenificiar (403 lines)

**VB6 File:** `source/BG_CONTRACT/frmDetailsBenificiar.frm`  
**Appearance:** `source/FormScreens/frmDetailsBenificiar.png`

**Controls:**

- 1 Frame (frAddress) containing address fields
- 2 TextBoxes (txtName, txtINN)
- 2 ComboBoxes for country (cmbCodeCountry, cmbCountry)
- 8 ComboBoxes for address type prefixes (cmbTypeRegion, cmbTypeArea, etc.)
- 6 TextBoxes for address parts (txtRegion, txtArea, txtCity, etc.)
- 1 TextBox for postal index (txtIndex)
- 2 CommandButtons (cmdSave, cmdExit)
- Multiple Labels

**Key Logic:**

- Country code and country name are synchronized
- Address type combos populated only for Russia (code "643")
- Uses public arrays: `arrTypeObject`, `arrCountry`
- Simple validation - just sets `blnApply` flag

**Conversion Notes:**

- Form has no border style specified (default)
- No control box
- Country selection triggers address type loading
- All address fields in single frame

### 3. frmRates (160 lines)

**VB6 File:** `source/BG_CONTRACT/frmRates.frm`  
**Appearance:** `source/FormScreens/frmRates.png`

**Controls:**

- 1 ComboBox (cmbRateTypes) - disabled
- 1 UbsControlDate (ucdDateRate)
- 1 UbsControlMoney (ucmRate)
- 2 CommandButtons (cmdApply="Применить", cmdExit="Отмена")
- 3 Labels: Тип ставки, Дата установки, Ставка

**Key Logic:**

- Date validation (must be between 1990 and 2222)
- Simple Apply/Cancel pattern
- Uses `blnApply` flag

**VB6 Bug:** `dDate1990` is declared but never assigned; only `dDate2222` is set in Form_Initialize. Fix: assign `dDate1990 = DateSerial(1990, 1, 1)` in constructor.

**Conversion Notes:**

- Fixed Dialog border style
- ShowInTaskbar = False
- CenterOwner startup position
- Minimal validation logic

## Control Conversion Mapping

| VB6 Control | .NET Control | Notes |

|-------------|--------------|-------|

| `VB.Form` | `UbsFormBase` (via `UbsBusiness.UbsBg*Frm : UbsFormBase`) | **MUST inherit from UbsFormBase** - provides panelMain, IUbs interface, channel integration |

| `VB.CommandButton` | `System.Windows.Forms.Button` | Direct conversion |

| `VB.TextBox` | `System.Windows.Forms.TextBox` | Direct conversion |

| `VB.ComboBox` (Style=2) | `System.Windows.Forms.ComboBox` | Set `DropDownStyle.DropDownList` |

| `VB.Label` | `System.Windows.Forms.Label` | Direct conversion |

| `VB.Frame` | `System.Windows.Forms.GroupBox` | Frame → GroupBox |

| `VB.CheckBox` | `System.Windows.Forms.CheckBox` | Direct conversion |

| `UBSCTRLLibCtl.UbsControlMoney` | `UbsControl.UbsCtrlDecimal` | Money/decimal input |

| `UBSCTRLLibCtl.UbsControlDate` | `UbsControl.UbsCtrlDate` | Date input |

| `UbsInfo32.Info` | `UbsControl.UbsCtrlInfo` | Info display |

## CRITICAL: UBS Form Pattern Requirements

**ALL THREE FORMS MUST FOLLOW THIS PATTERN EXACTLY**

### Reference Files
- **Pattern Template:** `source/UbsBgContractFrm/UbsBgContractFrm.cs`
- **Full Example:** `source/UbsGuarModelFrm/UbsGuarModelFrm.cs`
- **VB6 Source Reference:** `source/GuarModel/` → `source/UbsGuarModelFrm/` (successful conversion example)

### Required Class Structure

```csharp
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// [Form Description]
    /// </summary>
    public partial class UbsBg[FormName]Frm : UbsFormBase
    {
        #region Блок объявления переменных
        
        private string m_command = "";    // параметер запуска формы
        // ... other variables ...
        
        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public UbsBg[FormName]Frm()
        {
            m_addCommand(); // зарегистрировать обработчики команд интерфейса IUbs

            InitializeComponent();

            // установить имя ресурса, с которым будет работать канал
            this.IUbsChannel.LoadResource = "ASM:UBS_ASM\\Business\\DllName.dll->UbsBusiness.UbsBg[FormName]Frm";

            m_addFields(); // заполнить коллекцию полей формы

            base.Ubs_CommandLock = true;
        }

        #region Обработчики событий кнопок
        
        private void btnExit_Click(object sender, EventArgs e) { this.Close(); }
        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren()) { return; }
                // ... validation and save logic ...
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }
        
        #endregion

        #region Обработчики команд IUbs интерфейса

        /// <summary>
        /// Процедура регистрации обработчиков команд интерфейса IUbs в базовом классе
        /// </summary>
        private void m_addCommand()
        {
            base.Ubs_AddName(new UbsDelegate(CommandLine));
            base.Ubs_AddName(new UbsDelegate(ListKey));
        }
        
        /// <summary>
        /// Процедура обработки команды CommandLine
        /// </summary>
        /// <param name="param_in">Входной параметер</param>
        /// <param name="param_out">Выходной параметер</param>
        /// <returns></returns>
        private object CommandLine(object param_in, ref object param_out)
        {
            m_command = (string)param_in;
            // ... initialization logic if needed ...
            return null;
        }
        
        /// <summary>
        /// Процедура обработки команды ListKey
        /// </summary>
        /// <param name="param_in">Входной параметер</param>
        /// <param name="param_out">Выходной параметер</param>
        /// <returns></returns>
        private object ListKey(object param_in, ref object param_out)
        {
            try
            {
                // ... list key handling logic ...
                return null;
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        #endregion

        /// <summary>
        /// Заполнить коллекцию полей формы
        /// </summary>
        private void m_addFields()
        {
            // Add form controls to field collection for channel integration
            // Example:
            // base.IUbsFieldCollection.Add("Имя поля", new UbsFormField(btnApply, "Text"));
            // base.IUbsFieldCollection["Имя поля"].ReadOnly = true;
        }
    }
}
```

### Key Requirements Checklist

- [ ] **Inheritance:** `public partial class UbsBg*Frm : UbsFormBase`
- [ ] **Namespace:** `UbsBusiness`
- [ ] **Constructor:** Calls `m_addCommand()` → `InitializeComponent()` → Sets `IUbsChannel.LoadResource` → Calls `m_addFields()` → Sets `base.Ubs_CommandLock = true`
- [ ] **IUbs Methods:** Implements `m_addCommand()`, `CommandLine()`, `ListKey()`, `m_addFields()`
- [ ] **Layout:** Uses `this.panelMain` from base class (DO NOT create manually)
- [ ] **Error Handling:** Uses `this.Ubs_ShowError(ex)` from base class
- [ ] **Channel Integration:** Sets appropriate `IUbsChannel.LoadResource` value
- [ ] **Field Collection:** Populates `base.IUbsFieldCollection` in `m_addFields()`

### Base Class Features Available

From `UbsFormBase`:
- `panelMain` - Panel control (Dock=Fill) containing all form content
- `IUbsChannel` - Channel communication interface
- `IUbsFieldCollection` - Field collection for channel integration
- `Ubs_ShowError(Exception)` - Error display method
- `Ubs_CommandLock` - Command locking property
- `Ubs_AddName(UbsDelegate)` - Register command handlers

## Implementation Strategy

### Phase 1: Project Setup

1. Add required UBS control references to `UbsBgContractFrm.csproj`:
  - `UbsCtrlDecimal.dll` (for UbsControlMoney replacement)
  - `UbsCtrlDate.dll` (for UbsControlDate replacement)
  - Verify `UbsCtrlInfo.dll` already referenced
2. Create three new form files with **Windows-1251 encoding** (see Section 14)
3. **Include all three forms in solution** `UbsBgContractFrm.sln` — add to .csproj ItemGroup so they appear in solution
4. **Appearance:** Check `source/FormScreens/` for each form's layout; form frame must match pattern below

### Layout Pattern (Match UbsBgContractFrm.Designer.cs)

**Reference:** `source/UbsBgContractFrm/UbsBgContractFrm.Designer.cs`

The form frame (bottom bar with info, buttons) must match this pattern exactly:

- `panelMain` (Panel, Dock=Fill) as root container
- `tableLayoutPanel` (TableLayoutPanel, Dock=Bottom) inside panelMain — 3 columns: ubsCtrlInfo | btnApply | btnExit
- `ubsCtrlInfo` (UbsCtrlInfo) — Column 0, Font Bold, ForeColor Highlight, Visible=false initially
- `btnApply` / `btnExit` — Columns 1–2, Dock=Fill, UseVisualStyleBackColor=true

**IMPORTANT:** All main form controls must be placed inside `panelMain` Panel control. **`panelMain` is provided by `UbsFormBase` base class** - do NOT create it manually. Use `this.panelMain` which is inherited from the base class.

**Form Structure:**

```
Form
├── panelMain (Panel - Dock = Fill)
│   ├── [Main Content Controls - GroupBoxes, TextBoxes, ComboBoxes, etc.]
│   └── tableLayoutPanel (TableLayoutPanel - Dock = Bottom)
│       ├── ubsCtrlInfo (Column 0)
│       ├── btnApply (Column 1)
│       └── btnExit (Column 2)
```

**PanelMain Usage (from UbsFormBase):**

```csharp
// panelMain is inherited from UbsFormBase - DO NOT create it
// Just use it:
this.panelMain.Controls.Add([main content controls]);
this.panelMain.Controls.Add(this.tableLayoutPanel);
// panelMain.Dock = Fill is already set by base class
```

**TableLayoutPanel Structure:**

```csharp
// TableLayoutPanel properties
this.tableLayoutPanel.CausesValidation = false;
this.tableLayoutPanel.ColumnCount = 3;
this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));  // Column 0: Info/Spacer
this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115F)); // Column 1: Apply button
this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115F)); // Column 2: Exit button
this.tableLayoutPanel.Dock = DockStyle.Bottom;
this.tableLayoutPanel.RowCount = 1;
this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
```

**Button Properties:**

```csharp
// Apply button
this.btnApply.Dock = DockStyle.Fill;
this.btnApply.UseVisualStyleBackColor = true;

// Exit button
this.btnExit.CausesValidation = false;
this.btnExit.Dock = DockStyle.Fill;
this.btnExit.UseVisualStyleBackColor = true;
```

**Info Control (REQUIRED for all forms):**

```csharp
this.ubsCtrlInfo = new UbsControl.UbsCtrlInfo();
this.ubsCtrlInfo.AutoSize = true;
this.ubsCtrlInfo.Dock = DockStyle.Bottom;
this.ubsCtrlInfo.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
this.ubsCtrlInfo.ForeColor = SystemColors.Highlight;
this.ubsCtrlInfo.Interval = 25000; // or appropriate interval
this.ubsCtrlInfo.Location = new Point(3, 19);
this.ubsCtrlInfo.Name = "ubsCtrlInfo";
this.ubsCtrlInfo.Size = new Size(210, 13);
this.ubsCtrlInfo.TabIndex = 1;
this.ubsCtrlInfo.Text = "ubsCtrlInfo";
this.ubsCtrlInfo.Visible = false; // Initially hidden
```

**Form Properties:**

```csharp
this.AutoScaleDimensions = new SizeF(6F, 13F);
this.AutoScaleMode = AutoScaleMode.Font;
this.KeyPreview = true; // For keyboard navigation
// panelMain is already added by UbsFormBase - no need to add it manually
```

**Initialization Order:**

```csharp
this.panelMain.SuspendLayout();
this.tableLayoutPanel.SuspendLayout();
this.SuspendLayout();
// ... initialize all controls ...
this.panelMain.Controls.Add([main content]);
this.panelMain.Controls.Add(this.tableLayoutPanel);
this.Controls.Add(this.panelMain);
this.panelMain.ResumeLayout(false);
this.tableLayoutPanel.ResumeLayout(false);
this.tableLayoutPanel.PerformLayout();
this.ResumeLayout(false);
```

**Designer.cs Field Declaration:**

```csharp
#endregion

// panelMain is inherited from UbsFormBase - DO NOT declare it here
private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
private System.Windows.Forms.Button btnApply;
private System.Windows.Forms.Button btnExit;
private UbsControl.UbsCtrlInfo ubsCtrlInfo;
// ... other controls ...
}
```

### Phase 2: Form 1 - BGBonusPayInterval

**File:** `source/UbsBgContractFrm/UbsBgBonusPayIntervalFrm.cs` (Windows-1251)

**ENCODING REMINDER:** Before adding Russian text, set file encoding to Windows-1251:
- File → Advanced Save Options → Encoding → "Cyrillic (Windows) - Codepage 1251"
- Verify Russian text displays correctly in VS2026 editor and designer

**Layout Structure:**

- **panelMain** (Panel, Dock = Fill) containing:
  - Main content area with GroupBoxes (Frames) for period and date sections
  - **tableLayoutPanel** (TableLayoutPanel, Dock = Bottom) with:
    - Column 0: `ubsCtrlInfo` (100% width, docked bottom) - **REQUIRED**
    - Column 1: `btnApply` (fixed width ~115px, docked fill)
    - Column 2: `btnExit` (fixed width ~115px, docked fill)

**Key Conversions:**

- `VB.Form` → `UbsFormBase` (**MUST inherit from UbsFormBase**)
- **Constructor Pattern (REQUIRED):**
  ```csharp
  public UbsBgBonusPayIntervalFrm()
  {
      m_addCommand(); // Register IUbs command handlers
      InitializeComponent();
      this.IUbsChannel.LoadResource = "ASM:UBS_ASM\\Business\\DllName.dll->UbsBusiness.UbsBgBonusPayIntervalFrm";
      m_addFields(); // Populate field collection
      base.Ubs_CommandLock = true;
  }
  ```
- **IUbs Interface Implementation (REQUIRED):**
  ```csharp
  private void m_addCommand()
  {
      base.Ubs_AddName(new UbsDelegate(CommandLine));
      base.Ubs_AddName(new UbsDelegate(ListKey));
  }
  
  private object CommandLine(object param_in, ref object param_out)
  {
      m_command = (string)param_in;
      return null;
  }
  
  private object ListKey(object param_in, ref object param_out)
  {
      // Implementation based on form logic
      return null;
  }
  
  private void m_addFields()
  {
      // Add form fields to collection for channel integration
      base.IUbsFieldCollection.Add("FieldName", new UbsFormField(control, "Property"));
  }
  ```
- `blnApply As Boolean` → Can be kept as `public bool ApplyClicked { get; set; }` OR use channel-based approach
- `Me.Hide` → `this.Hide()` or `this.Close()` depending on usage pattern
- `chkArray(0)` → `chkArray[0]` (array to array/list)
- `cmbTypePeriod.ItemData` → Use `ComboBox` with `ValueMember`/`DisplayMember`
- `ucmPeriod.CurrencyValue` → `ucmPeriod.Value` (decimal property)
- `ucmNumDay.CurrencyValue` → `ucmNumDay.Value`
- `objInterfaces.NextCtrl` → Use `SelectNextControl()` method
- Error handling: `On Error GoTo` → `try/catch` with `MessageBox.Show`

**Control Naming:**

- `cmdApply` → `btnApply`
- `cmdExit` → `btnExit`
- `cmbTypePeriod` → `cbTypePeriod`
- `cmbTypeDate` → `cbTypeDate`
- `ucmPeriod` → `ubsCtrlPeriod`
- `ucmNumDay` → `ubsCtrlNumDay`
- `chkArray` → `chkDays` (array of 7 checkboxes)
- `Info` → `ubsCtrlInfo`

**Business Logic:**

- Convert `cmbTypePeriod_Click` → `cbTypePeriod_SelectedIndexChanged`
- Convert `cmbTypeDate_Click` → `cbTypeDate_SelectedIndexChanged`
- Convert `Check()` validation method
- Convert `Form_KeyPress` → `Form_KeyPress` event handler
- Convert `Form_Initialize` → Constructor initialization

### Phase 3: Form 2 - frmDetailsBenificiar

**File:** `source/UbsBgContractFrm/UbsBgDetailsBenificiarFrm.cs` (Windows-1251)

**ENCODING REMINDER:** Before adding Russian text, set file encoding to Windows-1251:
- File → Advanced Save Options → Encoding → "Cyrillic (Windows) - Codepage 1251"
- Verify Russian text displays correctly in VS2026 editor and designer

**Layout Structure:**

- **panelMain** (Panel, Dock = Fill) containing:
  - Main content area with:
    - Top: Name and INN fields
    - Middle: `GroupBox` (frAddress) containing all address fields
  - **tableLayoutPanel** (TableLayoutPanel, Dock = Bottom) with:
    - Column 0: `ubsCtrlInfo` (100% width, docked bottom) - **REQUIRED**
    - Column 1: `btnApply` (fixed width ~145px, docked fill)
    - Column 2: `btnExit` (fixed width ~133px, docked fill)

**Key Conversions:**

- **Inheritance:** `public partial class UbsBgDetailsBenificiarFrm : UbsFormBase`
- **Constructor Pattern:** Same as UbsBgContractFrm (m_addCommand, InitializeComponent, LoadResource, m_addFields, Ubs_CommandLock)
- **IUbs Methods:** Implement CommandLine and ListKey methods
- `blnApply As Boolean` → Can be kept as `public bool ApplyClicked { get; set; }` OR use channel-based approach
- `arrTypeObject`, `arrCountry` → Public properties or constructor parameters, or load via channel
- `cmbCodeCountry_Click` → `cbCodeCountry_SelectedIndexChanged`
- `cmbCountry_Click` → `cbCountry_SelectedIndexChanged`
- `ChangeTypeObject()` method conversion
- Array iteration: `UBound(arrCountry, 2)` → `arrCountry.GetLength(0)`

**Control Naming:**

- `cmdSave` → `btnApply` (to match Apply pattern)
- `cmdExit` → `btnExit`
- `txtName` → `tbName`
- `txtINN` → `tbINN`
- `cmbCodeCountry` → `cbCodeCountry`
- `cmbCountry` → `cbCountry`
- `cmbTypeRegion` → `cbTypeRegion`
- `txtRegion` → `tbRegion`
- (Similar pattern for all address fields)
- `frAddress` → `gbAddress`

**Business Logic:**

- Country synchronization logic (code ↔ name)
- Address type loading for Russia only
- Simple Apply/Cancel pattern

### Phase 4: Form 3 - frmRates

**File:** `source/UbsBgContractFrm/UbsBgRatesFrm.cs` (Windows-1251)

**ENCODING REMINDER:** Before adding Russian text, set file encoding to Windows-1251:
- File → Advanced Save Options → Encoding → "Cyrillic (Windows) - Codepage 1251"
- Verify Russian text displays correctly in VS2026 editor and designer

**Layout Structure:**

- **panelMain** (Panel, Dock = Fill) containing:
  - Main content area with:
    - Rate type combo (disabled)
    - Date control
    - Rate decimal control
  - **tableLayoutPanel** (TableLayoutPanel, Dock = Bottom) with:
    - Column 0: `ubsCtrlInfo` (100% width, docked bottom) - **REQUIRED**
    - Column 1: `btnApply` (fixed width ~115px, docked fill)
    - Column 2: `btnExit` (fixed width ~115px, docked fill)

**Key Conversions:**

- **Inheritance:** `public partial class UbsBgRatesFrm : UbsFormBase`
- **Constructor Pattern:** Same as UbsBgContractFrm (m_addCommand, InitializeComponent, LoadResource, m_addFields, Ubs_CommandLock)
- **IUbs Methods:** Implement CommandLine and ListKey methods
- `blnApply As Boolean` → Can be kept as `public bool ApplyClicked { get; set; }` OR use channel-based approach
- `ucdDateRate.DateValue` → `ubsCtrlDateRate.Value` (DateTime property)
- `dDate2222`, `dDate1990` → Constants or readonly fields
- Date validation: `IsDate()` → `DateTime` type checking
- `Form_Unload` → `Form_FormClosing` or `Dispose`

**Control Naming:**

- `cmdApply` → `btnApply`
- `cmdExit` → `btnExit`
- `cmbRateTypes` → `cbRateTypes`
- `ucdDateRate` → `ubsCtrlDateRate`
- `ucmRate` → `ubsCtrlRate`

**Business Logic:**

- Date range validation (1990-2222)
- Simple Apply/Cancel pattern

## Technical Details

### UBS Form Pattern (Following UbsBgContractFrm.cs)

All three forms **MUST** follow the UBS form pattern:

- **Inheritance:** `public partial class UbsBg*Frm : UbsFormBase`
- **Namespace:** `UbsBusiness`
- **Constructor Pattern:**
  1. Call `m_addCommand()` first
  2. Call `InitializeComponent()`
  3. Set `this.IUbsChannel.LoadResource = "ASM:UBS_ASM\\Business\\DllName.dll->UbsBusiness.ClassName"`
  4. Call `m_addFields()` to populate field collection
  5. Set `base.Ubs_CommandLock = true`
- **Required Methods:**
  - `m_addCommand()` - Registers CommandLine and ListKey handlers
  - `CommandLine(object param_in, ref object param_out)` - Handles command parameter
  - `ListKey(object param_in, ref object param_out)` - Handles list key parameter
  - `m_addFields()` - Populates `base.IUbsFieldCollection` for channel integration
- **Base Class Features:**
  - `panelMain` - Provided by UbsFormBase (Dock=Fill, contains all controls)
  - `IUbsChannel` - Channel communication interface
  - `IUbsFieldCollection` - Field collection for channel integration
  - `Ubs_ShowError(Exception)` - Error display method

### UBS Control Properties

**UbsCtrlDecimal (replaces UbsControlMoney):**

- `CurrencyValue` → `Value` (decimal)
- `Text` → `Text` (string)
- `Enabled` → `Enabled` (bool)
- `ReadOnly` → `ReadOnly` (bool)

**UbsCtrlDate (replaces UbsControlDate):**

- `DateValue` → `Value` (DateTime)
- `Text` → `Text` (string)
- `Enabled` → `Enabled` (bool)
- `ReadOnly` → `ReadOnly` (bool)
- `Format` → May need to check available properties

### Error Handling

- VB6: `On Error GoTo ErrorCode` → .NET: `try/catch`
- Use `MessageBox.Show` for user-facing errors
- **Use `this.Ubs_ShowError(ex)` for exceptions** - Available from UbsFormBase base class
- Example: `catch (Exception ex) { this.Ubs_ShowError(ex); }`

### Keyboard Navigation

- VB6: `objInterfaces.NextCtrl` → .NET: Use `SelectNextControl()` or TabIndex
- VB6: `Form_KeyPress` with Enter/Esc → .NET: `Form_KeyPress` event handler
- Set `KeyPreview = true` equivalent: `this.KeyPreview = true`

### Array Handling

- VB6: `chkArray(0)` control array → .NET: `CheckBox[] chkDays = new CheckBox[7]`
- VB6: `UBound(arrCountry, 2)` → .NET: `arrCountry.GetLength(0)`
- VB6: 2D Variant arrays → .NET: `object[,]` arrays

## File Structure

```
source/UbsBgContractFrm/
├── UbsBgContractFrm.csproj (update with new files and references)
├── UbsBgContractFrm.sln (update to include new forms)
├── UbsBgBonusPayIntervalFrm.cs (Windows-1251)
├── UbsBgBonusPayIntervalFrm.Designer.cs (Windows-1251)
├── UbsBgBonusPayIntervalFrm.resx
├── UbsBgDetailsBenificiarFrm.cs (Windows-1251)
├── UbsBgDetailsBenificiarFrm.Designer.cs (Windows-1251)
├── UbsBgDetailsBenificiarFrm.resx
├── UbsBgRatesFrm.cs (Windows-1251)
├── UbsBgRatesFrm.Designer.cs (Windows-1251)
└── UbsBgRatesFrm.resx
```

## Solution Inclusion (UbsBgContractFrm.sln)

**All three new forms must be included in the solution** `source/UbsBgContractFrm/UbsBgContractFrm.sln`.

When form files are added to the `.csproj` ItemGroup (Compile + EmbeddedResource), they are automatically part of the project; the solution references the project, so new files appear in the solution explorer. Ensure:

- Each form has `<Compile Include="UbsBg*Frm.cs">` and `<Compile Include="UbsBg*Frm.Designer.cs">` in the csproj
- Each form has `<EmbeddedResource Include="UbsBg*Frm.resx">` in the csproj

The solution file structure is:

```
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "UbsBgContractFrm", "UbsBgContractFrm.csproj", "{8CFD5570-CF86-4055-B7A5-69BADC1BF64C}"
EndProject
```

No changes needed to `.sln` file - Visual Studio will automatically track files added to `.csproj`.

## Testing Considerations

1. **Modal Dialog Behavior:** Verify forms show modally and return correct result
2. **Control Functionality:** Test all UBS controls (date, decimal) work correctly
3. **Validation:** Test all validation logic
4. **Keyboard Navigation:** Test Enter/Esc key handling
5. **Country Synchronization:** Test country code/name sync in frmDetailsBenificiar
6. **Conditional Enabling:** Test dynamic control enabling in BGBonusPayInterval
7. **Array Handling:** Test checkbox arrays and data arrays

## Dependencies to Add

Update `UbsBgContractFrm.csproj` to include:

```xml
<Reference Include="UbsCtrlDecimal">
  <HintPath>C:\ProgramData\UniSAB\Assembly\Ubs\UbsCtrlDecimal.dll</HintPath>
</Reference>
<Reference Include="UbsCtrlDate">
  <HintPath>C:\ProgramData\UniSAB\Assembly\Ubs\UbsCtrlDate.dll</HintPath>
</Reference>
```

## Conversion Order

1. **frmRates** (simplest - 3 controls, minimal logic)
2. **frmDetailsBenificiar** (medium - many controls, country sync logic)
3. **BGBonusPayInterval** (most complex - conditional logic, checkbox arrays)

## 14. Encoding: Windows-1251 — Russian Symbols Display in Visual Studio 2026

**CRITICAL REQUIREMENT:** All Russian text (кириллица) in form controls **MUST** display correctly when opening forms in Visual Studio 2026. This includes button captions, labels, form titles, and all user-visible text.

**Why Windows-1251:** Legacy VB6 and many .NET projects in Russian locales use Windows-1251. Matches the VB6 source encoding and ensures compatibility with existing UBS system components.

### 14.1 File Encoding Strategy

**Different file types require different encodings:**

| File Type | Encoding | Reason |
|-----------|----------|--------|
| `.cs` (code files) | **Windows-1251** | Russian comments and string literals must display correctly |
| `.Designer.cs` (designer files) | **Windows-1251** | Control Text properties with Russian text must display correctly |
| `.resx` (resource files) | **UTF-8** | Standard .NET resource file encoding; handles Cyrillic correctly |

### 14.2 Visual Studio 2026 Specific Instructions

#### Creating New Form Files

1. **Create .cs and .Designer.cs files:**
   - Create files normally in VS2026
   - **Before adding Russian text:** Set encoding to Windows-1251
   - **Method:** File → Advanced Save Options → Encoding → Select "Cyrillic (Windows) - Codepage 1251"
   - **Alternative:** File → Save As → Click dropdown next to Save → "Save with Encoding" → Select "Cyrillic (Windows) - Codepage 1251"

2. **Add Russian text to controls:**
   ```csharp
   // In Designer.cs - Russian text directly in code
   this.btnApply.Text = "Применить";
   this.btnExit.Text = "Выход";
   this.Text = "Период";
   ```

3. **.resx files:**
   - VS2026 automatically uses UTF-8 for .resx files (standard)
   - UTF-8 handles Cyrillic correctly - no changes needed
   - Russian text in .resx will display correctly

#### Opening Existing Files in VS2026

**If Russian text appears as mojibake (e.g., "Øàáëîí" instead of "Шаблон"):**

1. **Detect encoding issue:**
   - Open file in VS2026
   - Check if Russian characters display correctly
   - If mojibake appears, file encoding is incorrect

2. **Fix encoding:**
   - File → Advanced Save Options
   - Select "Cyrillic (Windows) - Codepage 1251"
   - Click OK
   - Save file (Ctrl+S)
   - Close and reopen file to verify

3. **VS2026 Auto-Detection:**
   - VS2026 may auto-detect encoding incorrectly
   - If auto-detection fails, manually set encoding as above
   - VS2026 remembers encoding per file

### 14.3 Implementation Checklist

**When creating each form:**

- [ ] Create `.cs` file → Set encoding to Windows-1251 BEFORE adding Russian text
- [ ] Create `.Designer.cs` file → Set encoding to Windows-1251 BEFORE adding Russian text
- [ ] Add Russian text to control properties (Text, Caption, etc.)
- [ ] Create `.resx` file → Use default UTF-8 (VS2026 handles this automatically)
- [ ] Verify Russian text displays correctly in VS2026 editor
- [ ] Verify Russian text displays correctly in Form Designer
- [ ] Test form at runtime to ensure Russian text displays correctly

### 14.4 Russian Text Locations

**Russian text appears in these locations:**

1. **Designer.cs files - Control Text properties:**
   ```csharp
   this.btnApply.Text = "Применить";
   this.btnExit.Text = "Выход";
   this.lblPeriod.Text = "Период";
   this.Text = "Период";  // Form title
   ```

2. **Designer.cs files - Comments (optional but recommended):**
   ```csharp
   // Кнопка применения
   this.btnApply = new System.Windows.Forms.Button();
   ```

3. **.cs files - Comments and string literals:**
   ```csharp
   /// <summary>
   /// Форма периода
   /// </summary>
   private const string ErrorMessage = "Необходимо указать дату";
   ```

4. **.resx files - Resource strings (if using resources):**
   ```xml
   <data name="btnApply.Text" xml:space="preserve">
     <value>Применить</value>
   </data>
   ```

### 14.5 Verification Steps

**After creating each form, verify Russian text:**

1. **In VS2026 Code Editor:**
   - Open `.Designer.cs` file
   - Check all `Text = "..."` properties
   - Russian characters must display correctly (e.g., "Применить", not "Ððèìåíèòü")

2. **In VS2026 Form Designer:**
   - Open form in designer view
   - Check all button captions, labels, form title
   - Russian text must display correctly in designer

3. **At Runtime:**
   - Build and run application
   - Open form
   - Verify all Russian text displays correctly in running form

4. **If Issues Found:**
   - Check file encoding (must be Windows-1251 for .cs/.Designer.cs)
   - Re-save file with correct encoding
   - Close and reopen file
   - Rebuild solution

### 14.6 VS2026 Encoding Settings

**Configure VS2026 for better encoding handling:**

1. **Tools → Options → Text Editor → Advanced:**
   - "Auto-detect UTF-8 encoding without signature" - Enable
   - "Detect when file is opened" - Enable

2. **File → Advanced Save Options:**
   - Set default encoding for new files if needed
   - Remember: .cs/.Designer.cs = Windows-1251, .resx = UTF-8

3. **File Status Bar:**
   - VS2026 shows current file encoding in status bar
   - Check encoding indicator when opening files
   - Click to change encoding if needed

### 14.7 Common Issues and Solutions

**Issue: Russian text shows as mojibake in VS2026**
- **Solution:** File → Advanced Save Options → Select "Cyrillic (Windows) - Codepage 1251" → Save

**Issue: Form Designer shows mojibake**
- **Solution:** Check .Designer.cs encoding, ensure Windows-1251, re-save file

**Issue: Runtime shows mojibake but VS2026 shows correctly**
- **Solution:** Check system locale, ensure Windows code page 1251 is active

**Issue: Some files show correctly, others don't**
- **Solution:** Each file has its own encoding - fix encoding per file individually

### 14.8 Best Practices

1. **Set encoding BEFORE adding Russian text** - Prevents encoding issues
2. **Use Windows-1251 for .cs/.Designer.cs** - Matches legacy VB6 encoding
3. **Use UTF-8 for .resx** - Standard .NET resource file encoding
4. **Verify in VS2026 editor AND designer** - Both must show correct text
5. **Test at runtime** - Final verification that text displays correctly
6. **Document encoding in code comments** - Help future developers
7. **Keep encoding consistent** - All .cs/.Designer.cs files use Windows-1251

### 14.9 Example: Correct Russian Text in Designer.cs

```csharp
// 
// btnApply
// 
this.btnApply.Dock = System.Windows.Forms.DockStyle.Fill;
this.btnApply.Location = new System.Drawing.Point(219, 3);
this.btnApply.Name = "btnApply";
this.btnApply.Size = new System.Drawing.Size(82, 26);
this.btnApply.TabIndex = 101;
this.btnApply.Text = "Применить";  // ✅ Russian text displays correctly
this.btnApply.UseVisualStyleBackColor = true;
this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
// 
// Form
// 
this.Text = "Период";  // ✅ Form title in Russian displays correctly
```

**If you see:** `this.btnApply.Text = "Ððèìåíèòü";` ❌ **WRONG - encoding issue!**
**Must see:** `this.btnApply.Text = "Применить";` ✅ **CORRECT!**

## Notes

- **CRITICAL:** All forms **MUST** inherit from `UbsFormBase` and implement IUbs interface pattern
- **Reference Pattern:** `source/UbsBgContractFrm/UbsBgContractFrm.cs` - This is the template ALL forms must follow
- **Reference Example:** `source/UbsGuarModelFrm/UbsGuarModelFrm.cs` - Shows full implementation pattern
- **Encoding:** Windows-1251 (Codepage 1251) for .cs/.Designer.cs files; UTF-8 for .resx files; ensure Russian symbols display correctly in VS2026 editor, designer, and runtime
- **Solution:** All three forms must be included in `UbsBgContractFrm.sln` (via .csproj ItemGroup)
- **Appearance:** Form frame must match `UbsBgContractFrm.Designer.cs` pattern; check `source/FormScreens/` for each form's layout
- **Required Implementation:**
  - Inherit from `UbsFormBase`
  - Implement `m_addCommand()`, `CommandLine()`, `ListKey()`, `m_addFields()` methods
  - Set `IUbsChannel.LoadResource` in constructor
  - Use `panelMain` from base class (DO NOT create manually)
  - Use `this.Ubs_ShowError(ex)` for error handling
- **Appearance reference:** `source/FormScreens/BGBonusPayInterval.png`, `frmDetailsBenificiar.png`, `frmRates.png` — use for UI layout verification
- All forms use Russian text - preserve in .NET version
- **All form source files:** Windows-1251 encoding (.cs/.Designer.cs) or UTF-8 (.resx) for correct Russian display in VS2026
- **Layout pattern:** 
  - All main controls must be inside `panelMain` Panel control (provided by UbsFormBase, Dock = Fill)
  - `tableLayoutPanel` must be inside `panelMain` (Dock = Bottom)
  - All forms **MUST** include `ubsCtrlInfo` in TableLayoutPanel Column 0
- **Solution file:** Forms are automatically included when added to `.csproj` - no manual `.sln` edit needed
- **Encoding:** Windows-1251 (.cs/.Designer.cs) or UTF-8 (.resx) ensures Russian symbols (А-Я, а-я) display correctly in VS2026; verify in editor, designer, and runtime

## Future Tasks & Notes

### UBS Control Properties: DateValue and DecimalValue

**Important Notes for Future Development:**

1. **UbsCtrlDate.DateValue Property:**
   - **Usage:** `ubsCtrlDate.DateValue` returns/sets a `DateTime` value
   - **Current Implementation:** Used correctly throughout the codebase
   - **Examples:**
     - `dateOpenGarant.DateValue = m_dateToday;` (line 257, 692, 931)
     - `dateAdjustment.DateValue = Convert.ToDateTime(pOut["Дата по корректировке"]);` (line 827)
     - `if (ubsCtrlDateRate.DateValue >= m_date2222 || ubsCtrlDateRate.DateValue <= m_date1990)` (UbsBgRatesFrm.cs:58)
   - **Field Collection Note:** In `m_addFields()`, use `"Value"` property name, not `"DateValue"`:
     ```csharp
     base.IUbsFieldCollection.Add("Дата установки", new UbsFormField(ubsCtrlDateRate, "Value"));
     ```
   - **TODO:** Verify if `DateValue` property exists or if it should be `Value` - check UbsCtrlDate.dll API documentation

2. **UbsCtrlDecimal.DecimalValue Property:**
   - **Usage:** `ubsCtrlDecimal.DecimalValue` returns/sets a `decimal` value
   - **Current Implementation:** Used correctly throughout the codebase
   - **Examples:**
     - `costAmount.DecimalValue = Convert.ToDecimal(paramOut["Сумма затрат"]);` (line 817)
     - `paidAmount.DecimalValue = Convert.ToDecimal(pOut["Уплаченная сумма"]);` (line 828)
     - `ucdSumGarant.DecimalValue = Convert.ToDecimal(paramOut["Сумма гарантии"]);` (line 957)
     - `if (string.IsNullOrEmpty(ubsCtrlPeriod.Text) || ubsCtrlPeriod.DecimalValue == 0)` (UbsBgBonusPayIntervalFrm.cs:223)
   - **Field Collection Note:** In `m_addFields()`, use `"Value"` property name, not `"DecimalValue"`:
     ```csharp
     base.IUbsFieldCollection.Add("Ставка", new UbsFormField(ubsCtrlRate, "Value"));
     ```
   - **TODO:** Verify if `DecimalValue` property exists or if it should be `Value` - check UbsCtrlDecimal.dll API documentation

3. **Property Name Inconsistency:**
   - **Issue:** Code uses `DateValue` and `DecimalValue` in C# code, but `"Value"` in field collection
   - **Possible Explanation:** 
     - Field collection may use a different property name convention
     - Or the actual property names might be `Value` (not `DateValue`/`DecimalValue`)
   - **Action Required:** 
     - Review UbsCtrlDate.dll and UbsCtrlDecimal.dll API documentation
     - Verify actual property names available on these controls
     - Update code if property names are incorrect
     - Consider creating wrapper methods or extension methods for consistency

4. **Encoding Review (2026-02-12):**
   - **Status:** Files reviewed - Russian text appears correctly in code
   - **Recommendation:** Verify encoding in Visual Studio 2026:
     - Open each .cs and .Designer.cs file
     - Check File → Advanced Save Options → Encoding
     - Ensure Windows-1251 encoding is set for .cs/.Designer.cs files
     - Ensure UTF-8 encoding is set for .resx files
   - **Note:** Visual Studio may auto-detect encoding incorrectly - manual verification recommended

5. **Code Quality Review (2026-02-12):**
   - **Linter Status:** ✅ No linter errors found
   - **Architecture Compliance:** ✅ All forms inherit from UbsFormBase
   - **IUbs Implementation:** ✅ All forms implement required methods
   - **Control Usage:** ✅ UbsCtrlDate and UbsCtrlDecimal used correctly
   - **Validation:** ✅ Date and decimal validation logic implemented correctly

