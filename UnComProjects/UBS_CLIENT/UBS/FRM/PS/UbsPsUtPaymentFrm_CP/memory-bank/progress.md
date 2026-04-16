# Progress: UbsPsUtPaymentFrm Conversion

## Overall Progress
| Phase | Status | Notes |
|-------|--------|-------|
| VAN | COMPLETE | Memory Bank initialized and phased task created |
| PLAN | COMPLETE | Planning artifacts, inventory, control mapping, and phase structure are captured in `memory-bank/tasks.md` |
| CREATIVE | COMPLETE | Designer, appearance, constants, channel-contract, and child-form decisions are documented in `memory-bank/creative/` |
| BUILD | IN PROGRESS | Rename scaffold completed; build verification blocked by missing .NET Framework 2.0 targeting pack |
| REFLECT | IN PROGRESS | Designer milestone reflected; full project reflection still depends on remaining BUILD work |
| ARCHIVE | NOT STARTED | Final documentation pending |

## VAN Phase Results
- [x] Platform detected: Windows, PowerShell
- [x] Memory Bank created under `memory-bank/`
- [x] Project goal recorded: VB6 UtPayment -> .NET `UbsPsUtPaymentFrm`
- [x] Rename target recorded: `UbsFormProject1` -> `UbsPsUtPaymentFrm`
- [x] Legacy source set recorded: main form, child forms, support module
- [x] Visual reference path recorded: `legacy-form/screens/`
- [x] Reference conversion projects recorded
- [x] Phased task created in the style of commit `324d60a`

## Known Inputs
- **Main VB6 document:** `legacy-form/UtPayment/UtPayment.dob`
- **Child forms:** `frmCalc.frm`, `frmCashOrd.frm`, `frmCashSymb.frm`
- **Support module:** `modWinAPI.bas`
- **Starter project:** `UbsFormProject1`

## Build Update: 2026-04-10
- [x] Renamed the active WinForms project scaffold from `UbsFormProject1` to `UbsPsUtPaymentFrm`
- [x] Created final-name files: `UbsPsUtPaymentFrm.sln`, `UbsPsUtPaymentFrm.csproj`, `UbsPsUtPaymentFrm.cs`, `UbsPsUtPaymentFrm.Designer.cs`, `UbsPsUtPaymentFrm.resx`, `Properties/AssemblyInfo.cs`
- [x] Removed the old template source files from `UbsFormProject1/`
- [x] Preserved `namespace UbsBusiness` per the rename plan
- [x] Verified there are no remaining `UbsFormProject1`, `UbsForm1`, or `UbsFormTemplate` identifiers under `UbsPsUtPaymentFrm/`
- [x] Created `UbsPsUtPaymentFrm.Constants.cs` with resource constants, local mode strings, captions, messages, UI text, and support key
- [x] Updated `UbsPsUtPaymentFrm.cs` to use `LoadResource`
- [x] Added `UbsPsUtPaymentFrm.Constants.cs` to `UbsPsUtPaymentFrm.csproj`
- [x] Created `UbsPsUtPaymentFrm.Initialization.cs` with first-load orchestration, `InitDoc`, read/find entry points, and shared param-out helpers
- [x] Added minimum shared startup state to `UbsPsUtPaymentFrm.cs` and wired form `Load` to `InitDoc`
- [x] Added `UbsPsUtPaymentFrm.Initialization.cs` to `UbsPsUtPaymentFrm.csproj`
- [x] Created `UbsPsUtPaymentFrm.Save.cs` with explicit save sequencing, `Payment_Save`, validation gates, and save-state UI restore helpers
- [x] Updated `btnSave_Click` in `UbsPsUtPaymentFrm.cs` to delegate into the save partial
- [x] Added `UbsPsUtPaymentFrm.Save.cs` to `UbsPsUtPaymentFrm.csproj`
- [x] Restored `UbsPsUtPaymentFrm.csproj` to planned `.NET Framework 2.0` conventions
- [x] Added designer-required UBS references: `UbsCtrlDecimal`, `UbsCtrlFields`, `UbsCtrlAccount`
- [x] Expanded `UbsPsUtPaymentFrm.Designer.cs` from the template into a six-tab payment surface with:
- [x] preserved `tblActions` footer structure (`uciInfo`, `btnSave`, `btnExit`)
- [x] `tabPayment` with `tabPageGeneral`, `tabPageThirdPerson`, `tabPageTariff`, `tabPageTelephone`, `tabPageTax`, and `tabPageAddFields`
- [x] scrollable general tab with sender group, recipient group, and lower summary/action region
- [x] fill-oriented add-fields tab with `ucfAddProperties`
- [x] Converted `frmCalc.frm` into modal child form `FrmCalc` with explicit input/output properties and legacy cash/change confirmation behavior
- [x] Converted `frmCashOrd.frm` into modal child form `FrmCashOrd` with explicit load phases, preview list, and `AutoExecute`/`WasCreated` result flow
- [x] Converted `frmCashSymb.frm` into modal child form `FrmCashSymb` with `DataGridView` editing, array conversion, total validation, and `UtCheckArrayCashSymbol` channel validation
- [x] Added child form sources/resources to `UbsPsUtPaymentFrm.csproj`
- [ ] Full compile verification

## Reflection Update: 2026-04-13
- [x] Created `memory-bank/reflection/reflection-ubspsutpaymentfrm-designer.md` for the completed designer milestone.
- [x] Recorded the intentional UX choice to use `LinkLabel` controls for several browse-oriented fields instead of relying only on tiny `...` buttons.
- [x] Captured that overall REFLECT remains open because template/footer cleanup, control/event reconciliation, and full build verification are still pending.

## Verification Notes
- `msbuild` is not available on PATH in the current shell environment.
- `dotnet msbuild UbsPsUtPaymentFrm.sln /t:Build /p:Configuration=Debug` reaches project evaluation but fails with `MSB3644` because the machine does not have the `.NETFramework,Version=v2.0` reference assemblies installed.
- IDE linting reports no errors in the updated constants/form/project files.
- IDE linting reports no errors in the new child-form files or project-file updates.
- `UbsPsUtPaymentFrm.Constants.cs` intentionally contains no channel `Run(...)`, `ParamIn[...]`, or `ParamOut[...]` literals, matching the workspace rule and creative decision.
- `UbsPsUtPaymentFrm.Initialization.cs` is currently a scaffold-first partial: startup channel flow and planned method boundaries are in place, while UI-population details remain deferred until the main designer/business controls are converted.
- `UbsPsUtPaymentFrm.Save.cs` is also scaffold-first: save ordering and channel boundaries are now explicit, while field-heavy payload mapping, add-fields integration, and external passport/IPDL validation wiring remain deferred until the business controls and required UBS references are present.
- The save partial currently avoids `UbsComValidateLibrary` references because the project does not yet include that assembly.
- The designer now reflects the planned screenshot-aware tab structure, but event wiring and field names in the logic partials still need to be reconciled with the newly expanded control set.

## BUILD Wave Plan (2026-04-10)

Full next-phase plan captured in `memory-bank/tasks.md` §Phase 3 (Waves 2–9).

| Wave | Scope | Key files | Depends on |
|------|-------|-----------|------------|
| **2** | Core infrastructure, ListKey, generic name fix, missing state, NativeMethods | `.cs`, `.Designer.cs`, `NativeMethods.cs` | — |
| **3** | Initialization — full `InitDoc`, data fillers, tab visibility | `.Initialization.cs` | Wave 2 |
| **4** | Save pipeline — full field collection, `CheckIPDL`, `NewRecord`, group save | `.Save.cs` | Wave 3 |
| **5** | Keys.cs — keyboard navigation, Enter/Escape, F7 shortcut | `.Keys.cs` (new) | Wave 2 |
| **6** | Commission.cs — amount/commission/NDS/penalty, timer recalc | `.Commission.cs` (new) | Wave 3 |
| **7** | BrowseShell.cs — client/contract/dictionary/pattern browse + return | `.BrowseShell.cs` (new) | Wave 3 |
| **8** | Cash.cs — FrmCalc/FrmCashSymb/FrmCashOrd orchestration | `.Cash.cs` (new) | Wave 6 + Wave 7 |
| **9** | Designer event wiring polish and final lint | `.Designer.cs` | All waves |

### Wave 2 Progress
- [x] **B2.1 complete**: renamed generic controls in `UbsPsUtPaymentFrm.Designer.cs` (`btnCalc`, `btnPattern`, `btnCashSymb`, `lblCharCount210`, `lblCharCount160`, `lblBatchNumber`, `lblCityCode`, `txtBatchNumber`, `txtCheckSum`, `chkThirdPerson`)
- [x] Verified no legacy generic identifiers (`button1..3`, `label1..4`, `textBox1..2`, `checkBox1`) remain in `UbsPsUtPaymentFrm/`
- [x] IDE lint check on `UbsPsUtPaymentFrm.Designer.cs`: no errors
- [x] **B2.2 complete**: added planned shared-state fields to `UbsPsUtPaymentFrm.cs` (`m_blnChangeContract`, group/add-param flags, browse/filter state, document/FR state, and related ids/strings)
- [x] IDE lint check on `UbsPsUtPaymentFrm.cs`: no errors
- [x] **B2.3 complete — ListKey fully rewritten** from VB6 `UBSChild_ParamInfo("InitParamForm")`:
  - Main `ListKey` resets state (`m_commandSource`, contract/group/addclient/addparam/FO flags`) then dispatches to branch methods
  - `ListKey_Add` — ADD / GROUP_ADD / GROUP_PROCEED / ADD_FROM_CLIENT with per-command sub-routing
  - `ListKey_AddIncoming` — ADD_INCOMING (extracts group/main IDs from itemArray)
  - `ListKey_AddParam` — ADD_PARAM (delegates to `ProcessAddParam` for parameter-tuple filling)
  - `ListKey_View` — VIEW / GROUP_VIEW
  - `ListKey_Copy` — COPY (AddProcInit → extract ID → InitDoc with COPY → revert to ADD)
  - `ListKey_ChangePart` — CHANGE_PART / GROUP_CHANGE / CHANGE_PART_INCOMING
  - Post-routing: `lblCommonAmount`/`udcCommonAmount`/`udcTotalAmount` visibility + `txtPayerFullName.Focus()`
- [x] Added `m_commandSource`, `fromFoAsClient`, `blnCheckIncoming`, `IdPaymentCopy` fields
- [x] Added message constants: `MsgNotBankClient`, `MsgPaymentNotSelected`, `MsgPaymentNotSelectedForAdd`, `MsgPaymentCancelled`, `MsgNotCashier`
- [x] Added `StrCommandAddIncoming` constant
- [x] Added stubs in `Initialization.cs`: `AddProcInit`, `IsAutoPeriod`, `GetGroupIDByPaymentID`, `UpdateGroupInfo`, `ProcessAddParam`, `CheckPayer`, `GetBankNameACC`, `CalcSumCommiss_2`, `FillNalog`
- [x] `ProcessAddParam` fully converted: iterates parameter tuples and fills 16 named control/state slots matching VB6
- [x] `CommandLine` uses safe `Convert.ToString(param_in)` with null guard
- [x] All three edited files lint-clean
- [x] **InitDoc fully rewritten** from VB6 (lines 5793–6162):
  - Pre-InitForm resets (control visibility, save flags, tab hide)
  - Cashier check for ADD commands (`PS_UserIsCashier` → `NotCashier` / `bRet`)
  - `InitForm` channel call with `StrCommand`, `IdPayment`, `IdMainIncoming`
  - Post-InitForm: `ChangeCommand`→VIEW, `CloseForm`→close, `CHANGE_PART_INCOMING`→EndGroup
  - XOR check (`bRetVal`/`bMsgBoxYesNo`), `FillCityCode`, dates, error key
  - Benefits client, benefit reason, AddProperties refresh, print form checkbox
  - `DocumentsExists`→force VIEW with info bar
  - Settings runs: `UtReadSettingEnterCashSymbol`, `UtReadSettingChoiceClient`, `UtReadSettingSourceMeans`
  - Command branches: `InitDoc_View`, `InitDoc_Copy`, `InitDoc_ChangePart`, `InitDoc_Add`
  - Post-branch: incoming group disable, period dates, `CheckPeni`, `CheckPayer`, `DefineRunUserForm`
- [x] **ReadContract fully rewritten** from VB6 (lines 6166–6362):
  - Group/Copy param handling, cash symbols, penalty info
  - Full payer/recipient data fill from channel output
  - Third person handling, ADD_PARAM conditional fill
  - Code payment visibility, account code/city code
  - Amounts, dates, contract/tariff/phone/pattern IDs
  - `FindContractbyId`, pattern-based tab visibility (Energy/Phone/Nalog/PhoneAcc)
- [x] Added 15 state fields: `m_idTariff`, `m_idPhone`, `m_isEndGroup`, `m_isErrorKey`, `m_isSourceMeansVisible`, `m_isSourceMeans`, `m_isAlready`, `m_isPenyPresent`, `m_isComissPeniPayer`, `m_isRegimCashSymb`, `m_curSumRec`, `m_curSumRateRec`, `m_numTabAddFl`
- [x] Added constants: `CaptionPaymentAccept`, `CaptionInitForm`, `CaptionBlockCheck`, `MsgPaymentBlocked`, `MsgDocumentsExistViewOnly`, `PatternEnergy`, `PatternPhone`, `PatternNalog`, `PatternPhoneAcc`
- [x] Added stubs: `DisableAllFields`, `EnableAllFields`, `SetAllFieldsEnabled`, `FillCityCode`, `CheckPeni` (2 overloads), `DefineRunUserForm`, `GetIdClientFromGroupPayment`
- [x] Added `GetParamOutBool` helper
- [x] All files lint-clean

### Wave 2 continued + Wave 3 partial (2026-04-14)
- [x] **B2.4 complete — Wire Form_Load**: connected `Load` event to `m_isInitialized = true`
- [x] **B2.5 complete — Wire Form_Closing**: wired `FormClosing` handler with `CanCloseForm()` save-in-progress guard (`e.Cancel = true` while saving)
- [x] **B2.6 complete — Create NativeMethods.cs**: added `NativeMethods` class with `POINT` struct and P/Invoke declarations for `GetCursorPos` and `Sleep` from `modWinAPI.bas`
- [x] Added missing shared state fields: `m_idClientOld`, `m_strFIOOld`, `m_strAddressOld`, `m_strINNOld`, `m_arrRateSend`, `m_isAutoPeriodFlag`, `m_forbidTaxStatusChanges`, `m_savedTaxStatusValue`, `m_bicOld`, `m_isNoMessage`, `m_blnSecondPayment`, `m_isPeriodEnable`, `m_codeEnergy`, `m_varTariff`, `m_isForward`
- [x] **CheckPeni** (both overloads) — shows/hides penalty label+control, optional sets amount
- [x] **CheckPayer** — caches FIO/address/INN for change detection, returns unchanged flag
- [x] **FillPayer** — calls `ReadClientFromIdOC`, fills payer fields + benefits checkbox
- [x] **FillNalog** — calls `ReadNalog`, fills all 11 tax tab controls with VB6→.NET mapping, handles tax status lock and type field disable
- [x] **FillPurpose** — clears+populates `cmbPurpose` from variant array
- [x] **FillTariff** — calls `ReadTariff`, fills `cmbTariff` with name+rate display, selects by ID
- [x] **FillPhone** — calls `ReadPhone`, fills `cmbPhone` with code+name display, selects by ID
- [x] **CalcSumCommiss_2** — full commission calculation: rate type routing (%, fixed, scale), min/max bounds, penalty inclusion, add-param override, updates `udcPayerRateAmount` and `udcAmountWithRate`
- [x] **GetBankNameACC** — validates BIC via `UtCheckBIKBank` and `UtCheckBIKLimitSharing`, reads bank name + corr. account via `ReadBankBIK`
- [x] **IsAutoPeriod** — calls `UtGetAutoFillPeriod`, clears period fields when auto
- [x] **GetDayEnd** — returns last day of month for given month/year strings
- [x] **DefineRunUserForm** — calls user-form pattern script, sets `btnPattern` caption/visibility
- [x] **GetIdClientFromGroupPayment** — resolves client ID from incoming group via channel
- [x] **AddProcInit** (full) — print-form check, clear state, InitForm call with yes/no fallback, calls InitDoc, and VB6 device initialization (`PsDevice.vbs` / `FormInitDevice`) with FR/scanner state capture
- [x] **FindContract** (full rewrite) — searches by code via `ReadContractbyCode`, calls `FindContractbyId`
- [x] **FindContractbyId** (full rewrite) — reads `UtReadTypePayment` + `UtReadContract`, populates recipient fields (ADD_PARAM-aware), rate array, code payment visibility, penalties, purpose, bank name, commission calc, pattern-based tab visibility (Energy→FillTariff, Phone→FillPhone, Nalog→FillNalog)
- [x] All files lint-clean

## Next Step
- Complete **B2.7** (add `NativeMethods.cs` to `.csproj`)
- Proceed to Wave 3 remaining items: B3.3 (FillDataPayment full), B3.10 (ApplyInitialFormState), B3.11 (Third-person)
- Wave 4 (Save pipeline) and Wave 5 (Keys) can proceed in parallel after Wave 3
- Re-run compile verification after installing the .NET Framework 2.0 targeting pack
