# Memory Bank: Tasks

## Current Task
**Main goal:** Convert Commission_ud (VB6 UserDocument) → UbsOpCommissionFrm (.NET WinForm)

**Plans:**
- `memory-bank/plan-conversion-goals-revised.md` — **main goal**, conversion roadmap
- `memory-bank/plan-legacy-source-conversion.md` — legacy source (Commission_ud.dob) → target (UbsOpCommissionFrm)
- `memory-bank/plan-ubsobgoals-apply-to-op-commission.md` — supporting goals (constants, partials)

**Phase 1 (Prep — Done):**
- [x] Constants partial, channel contract doc, projectbrief

**Creative phase:** ✅ Complete  
- **Doc:** `memory-bank/creative/creative-commission-conversion-architecture.md` — UBSChild→IUbs mapping, control mapping, tab order, field binding, LoadResource, constants

**Phase 2 (Main goal — Conversion):**
- [x] Add Commission/ as source reference (exists in project)
- [x] Implement UI: TabControl, txbName, txbDesc, ubsCtrlAddFields, btnSave, btnExit
- [x] Implement InitDoc (Get_Data for EDIT, clear for ADD)
- [x] Implement ListKey (pass ID from list)
- [x] Implement btnSave (CheckData, Com_Save, params Наименование/Описание)
- [x] Update LoadResource to OP Commission ASM path
- [x] Add constants for Commission params, commands, messages

**Phase 3 (Post-conversion):**
- [ ] Split partials when form grows

**Form appearance (legacy screens):**
- Plan: `memory-bank/plan-form-appearance-legacy-screens.md`
- **Creative phase:** ✅ Complete  
- **Doc:** `memory-bank/creative/creative-form-appearance-legacy-screens.md` — Option B (literal title/tabs + resizable multiline Описание, Anchor); UbsCtrlFieldsSupportCollection key «Набор параметров»
- [x] BUILD: Apply Designer + code-behind per plan and creative doc

## Status
- **Phase 2 BUILD complete** — Commission_ud converted to UbsOpCommissionFrm. TabControl, txbName, txbDesc, ubsCtrlAddFields, InitDoc, ListKey, btnSave, CheckData, constants. Linter clean.
- **Form appearance BUILD complete** — Designer: title «Добавить новую», tabs «Комиссия»/«Набор параметров», txbDesc multiline + Anchor + ScrollBars. Code-behind: UbsCtrlFieldsSupportCollection key «Набор параметров». Linter clean.
