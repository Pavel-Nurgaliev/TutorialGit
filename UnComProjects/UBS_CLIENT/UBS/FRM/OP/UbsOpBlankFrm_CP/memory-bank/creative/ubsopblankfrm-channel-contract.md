# UbsOpBlankFrm — Channel Contract

**Purpose:** Single reference for channel resource, commands, and param in/out. Align with legacy Blank_ud.dob and ASM.

---

## Resource

- **LoadResource:** ASM equivalent of `VBS:UBS_VBD\OP\Blank.vbs` (set in Constants partial as `LoadResource`).
- Used by form to load script/assembly for Get_Data and Blank_Save.

---

## Commands

| Command     | Direction | Purpose |
|------------|-----------|---------|
| **Get_Data**   | Form → channel | Load one accepted-value record by ID. |
| **Blank_Save** | Form → channel | Save Состояние (and add-fields via stub) for current ID. |

---

## Get_Data

- **ParamIn:** `Идентификатор` (int) — record ID.
- **ParamOut:**  
  `Дата учета`, `Наименование ценности`, `Вид ценности`, `Идентификатор вида`, `Серия`, `Номер`, `Состояние`.

Form uses ParamOut to fill controls and FillCombo (by Идентификатор вида and Состояние).

---

## Blank_Save

- **ParamIn:** `Идентификатор` (int), `Состояние` (int — from cmbState selected value). Add-fields sent via stub/ucpParam equivalent.
- **ParamOut:** (none required for current behavior; form shows "Данные сохранены!" after success.)

---

## Init Flow

- Form is opened with **ListKey(param_in)**; param_in contains ID (from list selection). Empty list → form closes with error "Список принятых ценностей пуст!".
- **InitDoc:** Set channel LoadResource; call Get_Data with Идентификатор; LoadFromParams(ParamOut); FillCombo; bind add-fields via stub.

---

*Update this doc when contract or constants change.*
