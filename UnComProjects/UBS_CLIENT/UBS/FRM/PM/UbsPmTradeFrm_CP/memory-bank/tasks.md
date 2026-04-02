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
- [x] Implement `ListKey`: extract `ID_TRADE`, `strRunParam` (CmdEdit/CmdAdd), `intVidTrade` from params; call `InitDoc`
- [x] Implement `InitDoc`: FillCombos, FillOurBIK, GetOneTrade (CmdEdit) / default init (CmdAdd), apply states
- [x] Implement `FillCombos`: call `TradeCombo_FillPM`; populate cmbTradeType, cmbNaprTrade, cmbUnit, cmbContractType1/2, cmbCurrencyPost, cmbCurrencyOpl/cmbCurOblig (shared list), cmbKindSupplyTrade, cmbComission
- [x] Implement `LoadFromParams`: field mapping integrated directly into `InitDoc` EDIT branch (no separate method; all `tradeOut.Contains/Value` calls map to controls)
- [ ] Implement `BuildSaveParams`: assemble all params for `ModifyTrade`
- [x] Implement `PMCheckOperationByTrade` + lock logic (disable all when Was_Operation=true)

### 2.10 Event Handlers
- [x] `cmbContractType1/2_SelectedIndexChanged`: show/hide commission, update buyer/seller tab, handle ClearContr logic
- [x] `chkCash_Click` / `CheckedChanged`: toggle cash vs manual instruction; `GetInstructionOplataCash`
- [x] `chkIsComposit_CheckedChanged`: enable/disable cmbTradeDirection; handle obligation direction clearing
- [x] `chkRate_CheckedChanged`: toggle ucdRateCurOblig enable; clear chkSumInCurValue
- [x] `chkSumInCurValue_CheckedChanged`: toggle ucdCostCurOpl enable; clear chkRate
- [ ] `txtMassa_LostFocus`: GetMassaGramm, GetSumOblig, GetSumOpl
- [ ] `txtCostUnit_LostFocus`: GetSumOblig, GetRateCurOblig (if chkSumInCurValue), GetSumOpl
- [ ] `txtRateCurOblig_LostFocus`: recalc ucdCostCurOpl, GetSumOpl
- [ ] `txtCostCurOpl_LostFocus`: GetRateCurOblig, GetSumOpl; set chkRate=1, chkSumInCurValue=0
- [ ] `txtTradeDate_LostFocus`: CheckDatesOblig, GetRate_CB, GetRateForPM
- [x] `tabControl_Selecting` (tab change guard): validate required fields before tab 2→tab 3 switch; handle obligation viewing on tab 3
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

- [x] **VAN initialization** — memory bank created.
- [x] **PLAN (designer v1)** — `plan-trade-designer-conversion.md` created.
- [x] **BUILD (Phase 1 Prep + Designer v1)** — Designer.cs v1 built. *(Lost; rebuilt in v2)*
- [x] **PLAN (designer revision)** — `plan-trade-designer-revision.md` from 7 screenshots.
- [x] **BUILD (designer v2)** — Designer.cs v2: 0 errors, DLL at `bin\Release\`.
- [x] **REFLECT (designer phase)** — `reflection-trade-designer.md`.
- [x] **BUILD (TabIndex correction)** — 50+ TabIndex fixes, 10 `TabStop=false`.
- [x] **REFLECT (TabIndex correction)** — Addendum to `reflection-trade-designer.md`.
- [x] **BUILD (Phase 2 bootstrap)** — `ListKey`/`CommandLine`, `InitDoc` skeleton, `chkCash`, `FillCombos`.
- [x] **REFLECT (Phase 2 bootstrap)** — `reflection-phase2-logic-bootstrap.md`.
- [x] **BUILD (form refactor)** — regions + `UbsPmTradeComboUtil` / `UbsPmTradeMatrixUtil` / `UbsPmTradeObligParamUtil`.
- [x] **CREATIVE (8 handler docs)** — `creative-initdoc-full-conversion.md`, `creative-cmb-contract-type-click.md`, `creative-cmb-kind-supply-trade-click.md`, `creative-sstabs-before-tab-click.md`, `creative-chk-is-composit-click.md`, `creative-chk-nds-rate-sum-in-cur.md`, `creative-form-refactor-regions-and-support.md`, `creative-trade-account-control-and-indexes.md`.
- [x] **BUILD (InitDoc full + event handlers)** — InitDoc EDIT/ADD full parity, all major handlers ported (`ApplyContractType1/2Change`, `ApplyKindSupplyUiState`, `chkComposit_CheckedChanged`, `chkRate/SumInCurValue_CheckedChanged`, `chkNDS/UpdateDisplayExport/UpdateDisplayNDS`, `tabControl_Selecting` guard, `ApplyDataTabUiOnSelecting`), contract pickers wired, DDX replaced by `m_mc`, payment tab management. **~1780 lines, build OK.**
- [x] **REFLECT (Phase 2 event handlers)** — `reflection-phase2-event-handlers.md` (2026-03-27). Phase 2 ~70% complete.
- [x] **CREATIVE (3 docs: save flow, calc chain, oblig lifecycle)** — `creative-save-flow-and-validation.md`, `creative-calc-chain-events.md`, `creative-call-oblig-lifecycle.md`.
- [x] **BUILD (all 3 creative checklists)** — Fields + constants, helpers, 5 LostFocus + 5 combo events, validation (CheckData 14 checks + CheckDatesOblig), save flow (UpdateMcFromControls + cmdSave_Click + ModifyTrade), sub-form pickers, 17 events wired. **3126 lines, 0 errors, DLL 100 KB.**
- [x] **REFLECT (comprehensive final)** — `reflection-trade-conversion.md` (2026-04-01). Phase 2 ~95% complete.
- [ ] **ARCHIVE** — `archive-trade-conversion.md`

## Reflection Highlights

- **What Went Well**: Creative-first discipline (11 docs, zero rework); two-phase designer planning; incremental reflections; static utility extraction
- **Challenges**: Designer file loss (mitigated by plan docs); 2D array transposition (documented convention); contract-type cascade (split into Apply* methods); .NET 2.0 restrictions
- **Lessons Learned**: Screenshot review mandatory; document array shapes before coding; use TabPage object refs not indices; suppress flags with try/finally
- **Next Steps**: Integration testing with real channel data; partial class split when needed; archive
