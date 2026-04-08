# Active Context: UbsPsUtPaymentGroupFrm

## Current Phase
**BUILD** — Save.cs complete (Payment_Save pipeline, CheckPayment chain, COM checks, group continuation script). Next: Keys.cs, Commission.cs, BrowseShell.cs.

## Current Focus
Save.cs implements the full save pipeline per `plan-validation-chain.md`: payer clear-id rule, CheckPayment (passport COM, contract state/id, CheckTerror, FIO, GetBankNameACC, UtCheckAccFromBic, amounts, CheckAddFields), IPDL COM, UtCheckUserBeforeSave (UTListAddRead + AddFields merge + Ut_CheckBeforeSave), Payment_Save, uciInfo feedback, group continuation MsgBox and late-bound `IUbsRunScript`, `StrCommand` → GROUP_EDIT. Payer “client” control is `linkFIO` + `OpenClientPicker()` (BrowseShell later). Next: Keys.cs (`CheckRS`), Commission.cs, BrowseShell.cs.

## Key Decisions Made
1. **Complexity Level 4** — Large form with 20+ channel commands, complex validation, commission calculations, group payment workflow
2. **Target structure**: 6-8 partial class files following UbsPsContractFrm pattern
3. **Template renamed**: UbsFormProject1 → UbsPsUtPaymentGroupFrm (DONE)
4. **Designer layout**: `panelMain` → `tabPayment` (Fill) + `tblActions` (Bottom, 32px); `tabPageMain` contains `pnlMainScroll` (AutoScroll) with `grpPayer`, `grpRecipient`, amount row; `tabPageAddProperties` hosts `ucfAddProperties`; rename template `tableLayoutPanel`/`ubsCtrlInfo` → `tblActions`/`uciInfo` on BUILD — see `creative/creative-ubspsutpaymentgroupfrm-designer-layout.md`
5. **Constants**: single `Constants.cs` with `#region` blocks like `UbsPsContractFrm`; full inventory (Run names, `StrCommand`, captions, messages, script path/ProgIds) — `creative/creative-ubspsutpaymentgroupfrm-constants.md`; keep `Платеж` vs `Платёж` caption variants as separate consts

## Template Renaming Summary
Source: `TMP_CP\UbsFormProject1\` → Target: `UbsPsUtPaymentGroupFrm_CP\UbsPsUtPaymentGroupFrm\`

| Template Name | Renamed To |
|---------------|------------|
| UbsFormProject1.csproj | UbsPsUtPaymentGroupFrm.csproj |
| UbsFormProject1.slnx | UbsPsUtPaymentGroupFrm.sln |
| UbsForm1.cs | UbsPsUtPaymentGroupFrm.cs |
| UbsForm1.Designer.cs | UbsPsUtPaymentGroupFrm.Designer.cs |
| UbsForm1.resx | UbsPsUtPaymentGroupFrm.resx |
| (new) | UbsPsUtPaymentGroupFrm.Constants.cs |

Key changes inside files:
- Class: `UbsForm1` → `UbsPsUtPaymentGroupFrm`
- RootNamespace: `UbsFormTemplate` → `UbsPsUtPaymentGroupFrm`
- AssemblyName: `UbsFormTemplate` → `UbsPsUtPaymentGroupFrm`
- DocumentationFile: `UbsFormTemplate.XML` → `UbsPsUtPaymentGroupFrm.XML`
- Form title: `"Шаблон формы"` → `"Групповой платеж"`
- LoadResource: template placeholder → `@"VBS:UBS_VBD\PS\UT\UtPaymentF.vbs"`
- Added references: UbsCtrlDecimal, UbsCtrlFields, UbsCtrlAccount
- Added Constants.cs partial with LoadResource constant

## What Was Analyzed
- Full VB6 form code (2683 lines) — all controls, business logic, channel calls
- Legacy form screenshots (2 tabs)
- 3 successful conversions: UbsPsContractFrm, UbsOpCommissionFrm, UbsOpBlankFrm
- Template project structure (TMP_CP\UbsFormProject1 — fully read and renamed)

## Next Steps
- **BUILD**: `Save.cs`, `Keys.cs`, `Commission.cs`, `BrowseShell.cs`, PostBuildEvent

## Status
```
✓ PLATFORM CHECKPOINT: Windows 10, PowerShell, backslash paths
✓ MEMORY BANK CHECKPOINT: All core files created
✓ COMPLEXITY CHECKPOINT: Level 4 — Complex System
✓ TEMPLATE RENAMED: UbsFormProject1 → UbsPsUtPaymentGroupFrm
✓ PLAN: File structure → plan-dotnet-file-structure.md
✓ PLAN: VB6→.NET controls → plan-vb6-controls-map.md
✓ PLAN: Channel contract → creative/creative-ubspsutpaymentgroupfrm-channel-contract.md
✓ PLAN: Validation chain → plan-validation-chain.md
✓ PLAN: Commission migration → plan-commission-migration.md
✓ PLAN: Group payment cycle → plan-group-payment-cycle.md
✓ CREATIVE: Designer layout → creative/creative-ubspsutpaymentgroupfrm-designer-layout.md
✓ CREATIVE: Constants inventory → creative/creative-ubspsutpaymentgroupfrm-constants.md
✓ BUILD: Designer.cs — 46 controls, panelMain→tabPayment(Fill)+tblActions(Bottom,32px), pnlMainScroll(AutoScroll), grpPayer, grpRecipient, amounts, ucfAddProperties
✓ BUILD: Constants.cs — full inventory (28 Run names, 6 captions, 13 messages, 3 COM/script, AccountPlaceholder, AddFieldsSupportKey)
✓ BUILD: Main .cs — 18 fields, ListKey→InitDoc, UbsCtrlFieldsSupportCollection
✓ BUILD: Initialization.cs — InitDoc, ReadContract, FindContractbyId, FillPurpose, GetBankNameACC, ReadBankBikResult, DisableAllFields, EnableAllFields, ClearRecFields, ClearRecFieldsSend, GroupContractItem
✓ BUILD: Save.cs — btn save, CheckPayment, CheckTerror, UtCheckUserBeforeSave, passport/IPDL COM, Payment_Save, uciInfo, group MsgBox + IUbsRunScript; SaveAttributeRecip; terror path uses `OpenClientPicker` / `linkFIO` (Designer has LinkLabel instead of `btnClient`)
→ NEXT: BUILD Keys.cs, Commission.cs, BrowseShell.cs, PostBuildEvent
```
