# Memory Bank: Tasks

## Current Task

**Main goal:** Convert `Pm_Trade_ud` (VB6 UserDocument) → `UbsPmTradeFrm` (.NET WinForm)

**Complexity:** Level 4 (Complex System)

**Plans:**
- `memory-bank/plan-trade-designer-conversion.md` ✅ — **Designer first**: full control inventory for all 6 tabs, anchor strategy, control-array replacement, 14 build steps
- `memory-bank/plan-trade-designer-revision.md` ✅ — **Screen-based correction**: 7 screenshot analysis, all layout/name/order differences documented, 8 build steps
- `memory-bank/plan-trade-conversion-goals.md` — (to create) main goal, full conversion roadmap
- `memory-bank/plan-trade-legacy-source-conversion.md` — (to create) legacy source ↔ target mapping

---

## Phase 1 (Prep) — Not Started

- [ ] Rename `UbsForm1` → `UbsPmTradeFrm` (class, file, Designer, resx)
- [ ] Update `RootNamespace` / `AssemblyName` in `.csproj` to match UBS convention
- [ ] Constants partial: `UbsPmTradeFrm.Constants.cs` (LoadResource, user-facing messages only)
- [ ] Channel contract doc: `memory-bank/creative/ubspmatradefrm-channel-contract.md`
- [ ] Add `UbsCtrlDecimal` reference to `.csproj`
- [ ] Add `UbsCtrlDate` reference to `.csproj` (for date fields)
- [ ] Add `UbsCtrlAddFields` reference to `.csproj` (for ucpParam)

---

## Creative Phase — Not Started

**Required creative decisions:**

- [ ] Layout architecture for 6-tab form (Option A: direct TabControl; Option B: nested panels; Option C: UserControl per tab)
- [ ] Sub-form strategy (contract lookup, object picker, account picker, instruction picker) — modal dialog vs inline
- [ ] Data model for obligations list (DataTable vs BindingList<ObligationRow> vs plain ListView items)
- [ ] Payment instructions model (indexed arrays vs separate objects for buyer/seller)
- [ ] Tab-disable mechanism (.NET alternative to `EnableWindow` API)

**Creative documents to produce:**
- `memory-bank/creative/creative-trade-conversion-architecture.md`
- `memory-bank/creative/creative-trade-form-layout.md`
- `memory-bank/creative/creative-trade-obligations-model.md`

---

## Phase 2 (Main Goal — Conversion) — Not Started

### 2.1 Form Skeleton & Tab Structure
- [ ] Implement 6-tab `TabControl` with correct tab names
- [ ] Add panel/GroupBox structure per tab based on creative decision

### 2.2 Tab 1 — Main Trade Data
- [ ] Implement: txtTradeDate (UbsCtrlDate), txtTradeNum (TextBox)
- [ ] Implement: cmbTradeType (ComboBox), cmbKindSupplyTrade (ComboBox)
- [ ] Implement: cmbCurrencyPost, cmbCurrencyOpl (ComboBox)
- [ ] Implement: cmbContractType1/2 + txtContractCode1/2 + txtClientName1/2 + cmdContract1/2 (Button "...")
- [ ] Implement: chkIsComposit (CheckBox), cmbNaprTrade (ComboBox)
- [ ] Implement: cmbComission (ComboBox), chkNDS, chkExport (CheckBox)
- [ ] Implement nested payment instructions (SSActiveTabs1 equivalent):
  - [ ] Buyer side: txtBIK(0), txtRS(0), txtKS(0), txtName(0), txtClient(0), txtNote(0), txtINN(0), chkNotAkcept(0), chkCash(0), cmdListInstr(0), cmdAccount(0)
  - [ ] Seller side: txtBIK(1), txtRS(1), txtKS(1), txtName(1), txtClient(1), txtNote(1), txtINN(1), chkNotAkcept(1), chkCash(1), cmdListInstr(1), cmdAccount(1)

### 2.3 Tab 2 — Obligations List
- [ ] Implement `lstViewOblig` (ListView, 7 visible columns + hidden columns for id, rate, unit, rateFl)
- [ ] Implement cmdAddOblig, cmdEditOblig, cmdDelOblig (Buttons)

### 2.4 Tab 3 — Obligation Detail (nested TabControl)
- [ ] Sub-tab 1: cmbNaprTrade, cmbCurOblig, chkRate, chkSumInCurValue, txtRateCurOblig, txtCostCurOpl, txtCostUnit, txtMassa, txtMassaGramm, cmbUnit, txtDateOpl, txtDatePost, txtSumOblig, txtSumOpl, lblObligInfo1/2
- [ ] Sub-tab 2 (Objects): lstViewObject, cmdAddObject, cmdDelObject
- [ ] Implement cmdApplayOblig, cmdExitOblig (Apply/Cancel for obligation editing)

### 2.5 Tab 4 — Precious Metals Characteristics
- [ ] Group "Характеристики металла": txtDatePost, txtMassa, txtMassaGramm
- [ ] Group "Характеристики металла (доставка)": txtDateOpl, txtSumOblig
- [ ] Note: some controls span tabs 3 and 4 in the legacy form

### 2.6 Tab 5 — Storage
- [ ] Implement chkExternalStorage (CheckBox)
- [ ] Implement txtStorageCode (TextBox, ReadOnly), txtStorageName (TextBox, ReadOnly)
- [ ] Implement cmdStorage (Button "...")

### 2.7 Tab 6 — Parameters (AddFields)
- [ ] Implement ucpParam → `UbsCtrlAddFields` or equivalent

### 2.8 Bottom Strip
- [ ] Implement cmdSave, cmdExit (Buttons), Info (UbsCtrlInfo)
- [ ] Implement lblAccounts, cmdAccounts (visible only for intVidTrade ≠ 0)

### 2.9 Core Logic
- [ ] Implement `ListKey`: extract `ID_TRADE`, `strRunParam`, `intVidTrade` from params; call `InitDoc`
- [ ] Implement `InitDoc`: FillCombos, FillOurBIK, GetOneTrade (EDIT) / default init (ADD), apply states
- [ ] Implement `FillCombos`: call `TradeCombo_FillPM`; populate cmbTradeType, cmbNaprTrade, cmbUnit, cmbContractType1/2, cmbCurrencyPost, cmbCurrencyOpl/cmbCurOblig (shared list), cmbKindSupplyTrade, cmbComission
- [ ] Implement `LoadFromParams`: map paramOut fields to form controls and DDX-equivalent fields
- [ ] Implement `BuildSaveParams`: assemble all params for `ModifyTrade`
- [ ] Implement `PMCheckOperationByTrade` + lock logic (disable all when Was_Operation=true)

### 2.10 Event Handlers
- [ ] `cmbContractType1/2_SelectedIndexChanged`: show/hide commission, update buyer/seller tab, handle ClearContr logic
- [ ] `chkCash_CheckedChanged(index)`: toggle cash/manual instruction mode; call GetInstructionOplataCash if checked
- [ ] `chkIsComposit_CheckedChanged`: enable/disable cmbNaprTrade; handle obligation direction clearing
- [ ] `chkRate_CheckedChanged`: toggle txtRateCurOblig enable; clear chkSumInCurValue
- [ ] `chkSumInCurValue_CheckedChanged`: toggle txtCostCurOpl enable; clear chkRate
- [ ] `txtMassa_LostFocus`: GetMassaGramm, GetSumOblig, GetSumOpl
- [ ] `txtCostUnit_LostFocus`: GetSumOblig, GetRateCurOblig (if chkSumInCurValue), GetSumOpl
- [ ] `txtRateCurOblig_LostFocus`: recalc txtCostCurOpl, GetSumOpl
- [ ] `txtCostCurOpl_LostFocus`: GetRateCurOblig, GetSumOpl; set chkRate=1, chkSumInCurValue=0
- [ ] `txtTradeDate_LostFocus`: CheckDatesOblig, GetRate_CB, GetRateForPM
- [ ] `SSTabs_SelectedIndexChanged` (tab change guard): validate required fields before tab 2→tab 3 switch; handle obligation viewing on tab 3
- [ ] `ucpParam_TextChange`: set blnAddFlChanged = true

### 2.11 Save Logic
- [ ] `cmdSave_Click`: CheckData, CheckDatesOblig, BuildSaveParams, ModifyTrade, handle Ошибка/success, show info, close form
- [ ] `CheckData`: validate contract types ≠, supply type set, trade date valid, trade number not empty, currencies selected, obligations not empty
- [ ] `CheckDatesOblig`: validate obligation dates ≥ trade date; validate RS currency code matches payment currency
- [ ] `CheckDataOblig`: validate individual obligation (direction, price, delivery date, payment date, rate, mass, objects)

### 2.12 Sub-Form Integration (TBD in CREATIVE)
- [ ] Contract lookup dialog (cmdContract1/2 → "..." button → child window or modal)
- [ ] Instruction picker (cmdListInstr → TradeFillInstr → selection dialog)
- [ ] Account picker (cmdAccount → selection from bank accounts)
- [ ] Object picker (cmdAddObject → GetObjectPM → selection dialog)
- [ ] Storage picker (cmdStorage → selection)

### 2.13 Channel integration
- [ ] Set `LoadResource` from `Constants.LoadResource`
- [ ] Implement all channel calls with explicit literal strings for commands and param keys

---

## Phase 3 (Post-Conversion) — Not Started

- [ ] Split partial classes when main file grows beyond ~300 lines
- [ ] Reflection doc: `memory-bank/reflection/reflection-trade-conversion.md`
- [ ] Update `systemPatterns.md` with lessons learned
- [ ] Archive: `memory-bank/archive/archive-trade-conversion.md`

---

## Status

- **VAN initialization complete** — memory bank created.
- **PLAN (designer v1) complete** — `plan-trade-designer-conversion.md` created. Full 6-tab control inventory, 14-step build plan.
- **BUILD (Phase 1 Prep + Designer v1) complete** — `UbsPmTradeFrm.Designer.cs` built: 0 errors, DLL 34 KB. *(File lost after session; needs rebuild)*
- **PLAN (designer revision) complete** — `plan-trade-designer-revision.md` created from 7 legacy screens. All layout/name/order corrections documented with pixel coordinates.
- **BUILD (designer v2) complete** — `UbsPmTradeFrm.Designer.cs` built: 0 errors, DLL produced at `bin\Release\UbsPmTradeFrm.dll`.
- **REFLECT (designer phase) complete** — `memory-bank/reflection/reflection-trade-designer.md` created.
- **BUILD (TabIndex correction) complete** — all 50+ TabIndex values corrected per-container; 10 display-only controls given `TabStop=false`; `plan-tabindex-order.md` created; 0 errors.
- **REFLECT (TabIndex correction) complete** — Addendum section appended to `reflection-trade-designer.md`.
- **NEXT: PLAN** — create `plan-trade-conversion-goals.md` to begin Phase 2 (business logic).
