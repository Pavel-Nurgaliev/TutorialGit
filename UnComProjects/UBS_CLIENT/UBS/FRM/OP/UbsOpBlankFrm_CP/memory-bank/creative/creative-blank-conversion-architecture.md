# CREATIVE PHASE: Blank_ud → UbsOpBlankFrm Conversion Architecture

**Scope:** Design decisions for converting Blank_ud (VB6 UserDocument) to UbsOpBlankFrm (.NET WinForm).  
**Context:** VB6 UBSChild, InitParamForm, DDX, UbsControlProperty; .NET IUbs, UbsFormBase.  
**Reference:** UbsOpCommissionFrm_CP conversion pattern; `creative-commission-conversion-architecture.md`.

---

## 1. IUbs / InitParamForm Mapping

**VB6:** InitParamForm receives (NumParentWindow, IdArray). ID = IdArray(0). No ADD/EDIT command — Blank is always opened with an ID from the list (empty list closes form).

**Decision:** **ListKey-only for init.** ListKey(param_in) receives ID (or list selection). Extract m_id; call InitDoc. No CommandLine needed for mode; Blank has no ADD mode. Matches Commission ListKey usage for EDIT.

---

## 2. Control Mapping

| VB6 | .NET |
|-----|------|
| VB.UserDocument | UbsFormBase (or existing base in project) |
| SSActiveTabs | TabControl |
| SSActiveTabPanel | TabPage |
| txbDateCalc (UbsControlDate) | DateTimePicker or project date control |
| txbSer, txbNum, txbNameVal, txbKindVal | TextBox (readonly) |
| cmbState | ComboBox (DropdownList) |
| ucpParam (UbsControlProperty) | Add-fields control + stub (UbsCtrlFieldsSupportCollection key per plan) |
| btnSave, btnExit | Button |
| UbsInfo | UbsCtrlInfo (or equivalent) |
| UbsDDXControl | Explicit LoadFromParams / BuildSaveParams (no DDX control) |

---

## 3. Field Binding (DDX Replacement)

**Decision:** Explicit methods — **LoadFromParams(param_out)** to populate controls from Get_Data result; **BuildSaveParams()** to fill param_in for Blank_Save (Состояние from cmbState selected value). No DDX abstraction in C#.

---

## 4. LoadResource and Channel

- **LoadResource:** Set to OP Blank ASM path (equivalent to `VBS:UBS_VBD\OP\Blank.vbs`).
- **Commands:** Get_Data (in: Идентификатор); Blank_Save (in: Идентификатор, Состояние; add-fields via stub).
- All param and command names in Constants partial; channel contract documented.

---

## 5. Tab Order and Keyboard (Optional)

- Enter: next control; from cmbState → switch to tab 2 and focus add-fields.
- Esc: previous control; from add-fields tab → switch to tab 1 and focus cmbState.
- Implement via form KeyPreview and KeyDown/KeyPress if parity required.

---

## 6. Relation to Other Creative Docs

- **creative-blank-ud-conversion.md** — Layout strategy (WinForms containers), control replacements, state combo FillCombo logic (KindVal 3/4, states 10–17).
- **creative-commission-conversion-architecture.md** (UbsOpCommissionFrm_CP) — Reference for IUbs ListKey, InitDoc, constants, channel contract.

---

**CREATIVE PHASE COMPLETE** — Use this and creative-blank-ud-conversion.md during BUILD.
