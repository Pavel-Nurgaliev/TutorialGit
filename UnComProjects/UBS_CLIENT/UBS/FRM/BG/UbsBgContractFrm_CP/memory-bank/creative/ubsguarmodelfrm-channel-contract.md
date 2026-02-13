# UbsGuarModelFrm Channel Contract Documentation

**Form:** `UbsGuarModelFrm` (Guarantee Model Form)  
**Namespace:** `UbsBusiness`  
**Base Class:** `UbsFormBase`  
**Interface:** `IUbs`

---

## Channel Resource

**Primary Resource:** `VBS:UBS_VBD\\GUAR\\GuarModel.vbs`

**Delete Resource:** `VBS:UBS_VBS\\SYSTEM\\del_simple_object_channel.vbs` (used for delete operations)

---

## Channel Commands

### 1. `GuarModelInit`
**Purpose:** Initialize form data (templates, client types, executors, states)

**Resource:** `VBS:UBS_VBD\\GUAR\\GuarModel.vbs`

**Parameters In:** None

**Parameters Out:**
- `Шаблоны` (Templates) - `object[,]` - Array of available templates
- `Типы клиентов` (Client Types) - `object[,]` - Array of client types
- `ОИ` (OI) - `object[,]` - Array of executors (optional)
- `Состояния` (States) - `object[,]` - Array of states

**Usage:** Called in `InitForm()` method during form initialization.

---

### 2. `GuarModelEdit`
**Purpose:** Save/create guarantee model record

**Resource:** `VBS:UBS_VBD\\GUAR\\GuarModel.vbs`

**Parameters In:**
- `StrCommand` - `string` - Command type ("Edit" or empty for new)
- `Наименование` (Name) - `string` - Model name (from `tbName.Text`)
- `Шаблон` (Template) - `string` - Selected template ID (from `m_models[cbModel.SelectedIndex, 1]`)
- `ОИ` (OI) - `object` - Selected executor ID (from `cbExecutor.SelectedValue`)
- `Состояние` (State) - `object` - Selected state ID (from `cbState.SelectedValue`)
- `Тип клиента` (Client Type) - `object` - Selected client type ID (from `cbClientType.SelectedValue`)

**Parameters Out:**
- `Error` - `string` (optional) - Error message if operation fails

**Usage:** Called in `btnSave_Click()` method when saving form data.

---

### 3. `GuarModelRead`
**Purpose:** Read existing guarantee model record for editing

**Resource:** `VBS:UBS_VBD\\GUAR\\GuarModel.vbs`

**Parameters In:**
- `Id` - `int` - Record ID (`m_idObject`)

**Parameters Out:**
- `Наименование` (Name) - `string` - Model name
- `ОИ` (OI) - `object` - Executor ID
- `Состояние` (State) - `object` - State ID
- `Тип клиента` (Client Type) - `object` (optional) - Client type ID
- `Шаблон` (Template) - `string` - Template ID

**Usage:** Called in `InitForm()` when `m_command == EditCommand`.

---

### 4. `GuarModelCheckRights`
**Purpose:** Check user access rights for the guarantee model

**Resource:** `VBS:UBS_VBD\\GUAR\\GuarModel.vbs`

**Parameters In:**
- `Идентификатор договора` (Contract Identifier) - `int` - Contract ID (`m_idObject`)
- `Идентификатор ОИ` (OI Identifier) - `short` - Executor ID (from `cbExecutor.SelectedValue`)

**Parameters Out:**
- `Права` (Rights) - `int` - Access rights bitmask
  - Bit 2 (value 4): View rights
  - Bit 1 (value 2): Edit rights
  - Returns `-2` if no view rights
  - Returns `-1` if no edit rights (but has view)

**Usage:** Called in `CheckRights()` method during form initialization.

---

### 5. `GuarModelInitUcp`
**Purpose:** Initialize UCP (User Control Panel) when template selection changes

**Resource:** `VBS:UBS_VBD\\GUAR\\GuarModel.vbs`

**Parameters In:**
- `Шаблон` (Template) - `string` - Selected template ID (from `m_models[cbModel.SelectedIndex, 1]`)

**Parameters Out:** Not documented (likely updates internal state)

**Usage:** Called in `cbModel_SelectedIndexChanged()` event handler.

---

### 6. `DeleteInstances`
**Purpose:** Delete guarantee model records

**Resource:** `VBS:UBS_VBS\\SYSTEM\\del_simple_object_channel.vbs`

**Parameters In:**
- `KeyArray` - `object[]` - Array of record IDs to delete
- `ProgId` - `string` - Programmatic identifier: `"UbsGuarantyModel"`

**Parameters Out:** None (success/failure handled via exceptions or return values)

**Usage:** Called in `ListKey()` method when `m_command == DeleteCommand`.

---

## Control-to-Parameter Mapping

| Control | Parameter Name | Parameter Type | Direction |
|---------|---------------|----------------|-----------|
| `tbName` (TextBox) | `Наименование` | `string` | In/Out |
| `cbModel` (ComboBox) | `Шаблон` | `string` | In/Out |
| `cbExecutor` (ComboBox) | `ОИ` | `object` (short) | In/Out |
| `cbState` (ComboBox) | `Состояние` | `object` (short) | In/Out |
| `cbClientType` (ComboBox) | `Тип клиента` | `object` (short) | In/Out |
| `ubsCtrlAddFields` | Dynamic fields | Collection | In/Out |

---

## IUbs Interface Commands

### `CommandLine`
**Purpose:** Set command mode (Edit/Delete/New)

**Parameter In:** `string` - Command name ("Edit", "Delete", or empty for new)

**Usage:** Called by UBS system before opening form.

---

### `ListKey`
**Purpose:** Initialize form with record ID or handle delete operation

**Parameter In:** `object[]` - Array containing record ID `[0]` (or `null` for new record)

**Returns:** 
- `false` - If delete operation completed or failed
- `null` - If form initialized successfully

**Usage:** Called by UBS system to open form for specific record or handle delete.

---

## Field Collection

**Collection Name:** `"Доп. поля"` (Additional Fields)

**Control:** `ubsCtrlAddFields` (UbsCtrlFields)

**Required Field:** `"Тип счета учета"` (Account Type) - Must be filled before save.

**Usage:** Dynamic fields managed by UBS system, validated in `IsAddFieldFilled()` method.

---

## Notes

- All Cyrillic parameter names must match exactly as defined in VBS channel scripts.
- Channel operations use both `UbsChannel_*` (for UBS channel) and `IUbsChannel.*` (for IUbs channel) methods.
- Form state is managed via `m_command` variable ("Edit", "Delete", or empty).
- Template selection is disabled (`cbModel.Enabled = false`) after first save in Edit mode.
- Rights checking determines if form opens in view-only mode (`btnSave.Visible = false`).

---

**Last Updated:** Phase 1 - Constants & Documentation  
**Related Document:** `memory-bank/creative/creative-ubsguarmodelfrm.md`
