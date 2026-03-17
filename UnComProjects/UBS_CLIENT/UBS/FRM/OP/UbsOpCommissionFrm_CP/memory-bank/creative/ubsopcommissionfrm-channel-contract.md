# UbsOpCommissionFrm — Channel Contract

**Form:** UbsOpCommissionFrm (OP Commission)  
**Source:** Commission_ud (VB6 UserDocument)  
**Context:** UBS channel (ASM), IUbs interface.

---

## Resource

| Item | Value | Notes |
|------|--------|------|
| **LoadResource** | `ASM:UBS_ASM\Business\DllName.dll->UbsBusiness.UbsOpCommissionFrm` | VB6: `VBS:UBS_VBD\OP\Commission.vbs` |

---

## Commands

| Command | Handler | ParamIn | ParamOut | Notes |
|---------|---------|---------|----------|--------|
| **CommandLine** | `CommandLine(object param_in, ref object param_out)` | Command string ("ADD" or "EDIT") | — | Stores in m_command |
| **ListKey** | `ListKey(object param_in, ref object param_out)` | object[] with ID at [0] (for EDIT) or null/empty (for ADD) | — | Extracts m_id; calls InitDoc |

---

## Channel Actions

| Action | ParamIn | ParamOut | Notes |
|--------|---------|----------|--------|
| **Get_Data** | Идентификатор | Наименование, Описание | Load commission data for EDIT |
| **Com_Save** | Действие, Наименование, Описание | — | Save commission |

---

## Field Collection

| Field name (key) | Control | Property | Notes |
|-------------------|--------|----------|--------|
| Наименование | txbName | Text | Required |
| Описание | txbDesc | Text | |

---

## Add-Fields (UbsCtrlFields)

- **Key:** "Доп. поля"
- **Control:** ubsCtrlAddFields (UbsCtrlFields)
- **Stub:** Via UbsCtrlFieldsSupportCollection; base wires channel for dynamic add-fields
