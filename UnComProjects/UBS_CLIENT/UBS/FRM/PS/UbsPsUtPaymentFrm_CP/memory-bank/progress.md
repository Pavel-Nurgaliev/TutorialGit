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

## Next Step
- Continue BUILD by reconciling `Initialization.cs` and `Save.cs` with the expanded designer control names and by fleshing out the remaining main-form behavior partials
- Add remaining project references such as `UbsComValidateLibrary` and any additional UBS control assemblies only when their code paths are introduced
- Re-run compile verification after installing the .NET Framework 2.0 targeting pack or using a machine that already has it
