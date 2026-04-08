# Tasks: UbsPsUtPaymentGroupFrm Conversion

## Complexity Determination
- **Level**: 4 (Complex System / Enterprise)
- **Rationale**:
  - Large VB6 form: 2683 lines with complex business logic
  - 20+ channel commands to map
  - Complex validation chains (terrorism, IPDL, passport, account keys)
  - Tab control with dynamic additional properties
  - 7 OCX controls to map to .NET equivalents
  - Commission calculation engine with tiered rates
  - Group payment workflow cycle (save → prompt → add another)
  - Client selection via child windows / card reader
  - Recipient attribute management with browse/save
- **Workflow**: VAN → PLAN → CREATIVE → BUILD → REFLECT → ARCHIVE

## Current Phase: CREATIVE — Next (PLAN complete)

### PLAN deliverables (Level 4)
- [x] Define complete file structure for .NET project — see `memory-bank/plan-dotnet-file-structure.md`
- [x] Map all VB6 controls to .NET controls with names — see `memory-bank/plan-vb6-controls-map.md`
- [x] Plan partial class split strategy — see `memory-bank/plan-dotnet-file-structure.md` section 4
- [x] Define channel command contract document — `memory-bank/creative/creative-ubspsutpaymentgroupfrm-channel-contract.md`
- [x] Plan validation chain architecture — `memory-bank/plan-validation-chain.md`
- [x] Plan commission calculation logic migration — `memory-bank/plan-commission-migration.md`
- [x] Plan group payment cycle implementation — see `memory-bank/plan-group-payment-cycle.md`

### Technology validation (PLAN gate)
- **Stack**: .NET Framework 2.0 WinForms class library; UBS assemblies from `ProgramData`; namespace `UbsBusiness`; `UbsFormBase` + `panelMain` template.
- **Proof**: Template project compiles today; full form compiles after BUILD adds all partials and Designer controls.
- **Dependencies**: UbsCtrlDecimal, UbsCtrlFields, UbsCtrlAccount (+ existing UbsBase, Channel, Form, FormBase, Interface, CtrlInfo, Collections).
- **Post-build**: Match `UbsPsContractFrm` — copy outputs to `\\Develop\ubs_nt\UBS_CLIENT\UBS\FRM\PS` and `C:\ProgramData\UniSAB\UBS\FRM\PS\` (add in `.csproj` when stabilizing build).

## Complete .NET file structure (summary)

**Conversion root** (`UbsPsUtPaymentGroupFrm_CP`): `.cursor/rules`, `legacy-form`, `memory-bank`, `UbsPsUtPaymentGroupFrm`.

**Project** (`UbsPsUtPaymentGroupFrm/`):

| File | Role |
|------|------|
| `UbsPsUtPaymentGroupFrm.sln` | Solution |
| `UbsPsUtPaymentGroupFrm.csproj` | MSBuild project; add `DependentUpon` partials per plan |
| `Properties/AssemblyInfo.cs` | Assembly metadata |
| `UbsPsUtPaymentGroupFrm.cs` | Ctor, fields, `m_addCommand`, `CommandLine`, `ListKey`, thin handlers |
| `UbsPsUtPaymentGroupFrm.Constants.cs` | `LoadResource`, commands, messages, list keys |
| `UbsPsUtPaymentGroupFrm.Designer.cs` | `InitializeComponent`, controls, `panelMain` layout |
| `UbsPsUtPaymentGroupFrm.resx` | Form resources |
| `UbsPsUtPaymentGroupFrm.Initialization.cs` | InitDoc, ReadContract, FindContractbyId, FillPurpose, bank/account helpers |
| `UbsPsUtPaymentGroupFrm.Save.cs` | Save, CheckPayment, CheckTerror, pre-save checks, attribute save |
| `UbsPsUtPaymentGroupFrm.Keys.cs` | Keyboard / focus navigation |
| `UbsPsUtPaymentGroupFrm.Commission.cs` | CalcSumCommiss, amount changed |
| `UbsPsUtPaymentGroupFrm.BrowseShell.cs` | Client list, receiver list, RetFromGrid |

**Optional**: `UbsPsUtPaymentGroupFrm.Validation.cs` if `Save.cs` is too large.

Full tree, `.csproj` compile order, and responsibility map: **`memory-bank/plan-dotnet-file-structure.md`**.

## Creative phases required (PLAN gate passed)
- [x] Designer layout vs `UbsFormTemplate` / legacy screenshots — see `creative/creative-ubspsutpaymentgroupfrm-designer-layout.md`
- Channel contract: **draft complete** in `creative/creative-ubspsutpaymentgroupfrm-channel-contract.md` — refine during BUILD if VBS differs
- `UbsComboEditControl` mapping and group script interop — see `memory-bank/plan-group-payment-cycle.md` (IUbsRunScript, `Parent`, `PsPaymentIncomingReception.vbs`)
- [x] Constants inventory (resource, Run names, StrCommand, captions, messages, script ProgIds) — see `creative/creative-ubspsutpaymentgroupfrm-constants.md`

## Phase 2: CREATIVE — Architecture Decisions
- [x] Designer layout: panelMain, tabs, groupboxes, control placement — see `creative/creative-ubspsutpaymentgroupfrm-designer-layout.md`
- [x] Constants file: all string constants, command names, messages — inventory in `creative/creative-ubspsutpaymentgroupfrm-constants.md`; paste into `Constants.cs` during BUILD
- [x] Channel contract: all Run calls, ParamIn/ParamOut keys — see `creative/creative-ubspsutpaymentgroupfrm-channel-contract.md`
- [x] Form appearance: match legacy UI layout in .NET — structure per designer-layout creative doc; pixel polish in REFLECT

## Phase 3: BUILD — Implementation
- [x] Create .sln and .csproj (baseline exists; extend per plan)
- [x] Create AssemblyInfo.cs
- [x] Create Designer.cs (all controls) — 46 controls matching plan-vb6-controls-map.md; pattern per creative-ubspsutpaymentgroupfrm-designer-layout.md
- [x] Create Constants.cs (expand) — full constants from inventory: Resource, StrCommand, Run names, Captions, Messages, Script/COM, BankBIK tags, AccountPlaceholder, AddFieldsSupportKey
- [x] Create main form file (constructor, IUbs delegates) — fields, ListKey with InitDoc call, UbsCtrlFieldsSupportCollection setup
- [x] Create Initialization.cs (InitDoc, channel reads) — 11 methods: InitDoc, ReadContract, FindContractbyId, FillPurpose, GetBankNameACC, ReadBankBikResult, DisableAllFields, EnableAllFields, ClearRecFields, ClearRecFieldsSend + GroupContractItem helper class
- [x] Create Save.cs (save pipeline, all validations) — BtnSave_ClickImpl, CheckPayment, CheckTerror, UtCheckUserBeforeSave, CheckLockPassport, CheckIPDL, RunGroupContinuationScript (IUbsRunScript late-bind), BtnSaveAttribute_ClickImpl; `linkFIO` used where VB6 had `btnClient`
- [ ] Create Keys.cs (keyboard handling)
- [ ] Create Commission.cs
- [ ] Create BrowseShell.cs
- [x] Create .resx
- [ ] Add PostBuildEvent to `.csproj` (PS paths)

## Phase 4: REFLECT — Review
- [ ] Verify all channel commands mapped
- [ ] Verify UI matches legacy screenshots
- [ ] Verify control naming conventions
- [ ] Verify error handling patterns

## Phase 5: ARCHIVE — Documentation
- [ ] Archive conversion notes
- [ ] Document deviations from VB6 original
