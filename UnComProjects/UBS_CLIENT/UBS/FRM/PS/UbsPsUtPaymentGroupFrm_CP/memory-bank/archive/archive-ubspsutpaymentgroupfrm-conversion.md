# TASK ARCHIVE: UbsPsUtPaymentGroupFrm (VB6 → .NET WinForms)

## METADATA

| Field | Value |
|--------|--------|
| **Task ID** | `ubspsutpaymentgroupfrm-conversion` |
| **Complexity** | Level 4 (Complex System / Enterprise) |
| **Workflow** | VAN → PLAN → CREATIVE → BUILD → REFLECT → ARCHIVE (complete) |
| **Legacy source** | `legacy-form/UtPaymentGroup/UtPaymentGroup_F.dob` (~2683 lines) |
| **Script resource** | `VBS:UBS_VBD\PS\UT\UtPaymentF.vbs` (`LoadResource` in `Constants.cs`) |
| **.NET project** | `UbsPsUtPaymentGroupFrm/UbsPsUtPaymentGroupFrm.csproj` (.NET Framework 2.0, `UbsBusiness`, `UbsFormBase`) |
| **Archive date** | 2026-04-10 |
| **Reflection** | `memory-bank/reflection/reflection-ubspsutpaymentgroupfrm-conversion.md` |

## SUMMARY

Converted the **UtPaymentGroup** VB6 user document into a **WinForms partial-class library** following the `UbsPsContractFrm` pattern: `panelMain` template, tabbed main + additional properties, explicit channel command strings, variant matrices as `object[row, column]`, and PostBuild copy to the dev PS share (if present) and `C:\ProgramData\UniSAB\UBS\FRM\PS\`. The solution **compiles (MSBuild Debug)** and implements the bulk of init, read, save validation, commission math, keyboard chains, and browse shells.

**Known gaps vs VB6** (must stay in release notes until closed):

1. **`RunGroupContinuationScript`** — call site exists; implementation **commented out** (`Save.cs`). Legacy used `IUbsRunScript` + `PsPaymentIncomingReception.vbs` after save for group continuation / `EndGroup` behavior.
2. **`UtGetAccINNFromLastPayment`** — VB6 runs it after contract resolution when payer and BIC are set (`UtPaymentGroup_F.dob` ~1783–1809) to prefill INN, settlement account, and purpose from the last payment. **Not implemented** in `FindContractbyId()` in `UbsPsUtPaymentGroupFrm.Initialization.cs`.
3. **UI pixel parity** — structure matches creative designer doc; side-by-side verification against legacy screenshots was **not** completed in REFLECT.

## REQUIREMENTS

- Port group payment form: contracts combo, payer/recipient/bank fields, amounts, commission, additional properties (`UbsCtrlFields`), validations (terrorism, add-fields, account/BIC, passport, IPDL, user-before-save), client/receiver lists, card read.
- Preserve channel **parameter names** and **command names** as in VB6 / VBS.
- Target **.NET 2.0**, no LINQ; UBS control mappings per project rules (e.g. `UbsCtrlDecimal`, `UbsCtrlAccount`, `UbsCtrlFields`, `TabControl`).
- Maintain **template constraints**: `panelMain`, table layout height aligned with `UbsFormTemplate` pattern.
- Document channel contract in Memory Bank creative doc.

## IMPLEMENTATION

### Delivered artifacts (code)

| Area | Files | Notes |
|------|--------|--------|
| Shell | `UbsPsUtPaymentGroupFrm.sln`, `.csproj`, `AssemblyInfo.cs`, `.resx` | PostBuild: conditional `\\Develop\ubs_nt\UBS_CLIENT\UBS\FRM\PS\` + `ProgramData\UniSAB\UBS\FRM\PS\` |
| UI | `UbsPsUtPaymentGroupFrm.Designer.cs` | ~46 controls; `linkFIO` replaces VB `btnClient` pattern |
| Constants | `UbsPsUtPaymentGroupFrm.Constants.cs` | Commands, messages, script ProgIds, placeholders |
| Lifecycle / contract | `UbsPsUtPaymentGroupFrm.Initialization.cs` | `InitDoc` (`FormStart`, `InitFormGroup`, `GroupContract` → combo), `ReadContract`, `FindContractbyId`, purpose/bank helpers, `GroupContractItem` |
| Save / validation | `UbsPsUtPaymentGroupFrm.Save.cs` | `Payment_Save`, `CheckPayment`, `CheckTerror`, `CheckAddFields`, `UtCheckAccFromBic`, `UTListAddRead`, `Ut_CheckBeforeSave`, `SaveAttributeRecip`; passport/IPDL via **`UbsComValidateLibrary`** |
| Input / focus | `UbsPsUtPaymentGroupFrm.Keys.cs` | Enter/Escape, `CheckRS`, `UtGetINNFromLastPayment` / `UtGetKPPUPayerLastPayment`, card/INN, `FindContrByBicAndAccount` |
| Commission | `UbsPsUtPaymentGroupFrm.Commission.cs` | `UtReadContract`, tier matrix, rounding |
| Browse | `UbsPsUtPaymentGroupFrm.BrowseShell.cs` | `Ubs_ActionRun`, list filters, `ReadClientFromIdOC`, `ReadRecipFromId`, shell `UbsItem*` calls |
| Host wiring | `UbsPsUtPaymentGroupFrm.cs` | ctor, `ListKey` → `InitDoc`, delegates |

### Planning / creative (Memory Bank)

- Structure: `plan-dotnet-file-structure.md`, `plan-vb6-controls-map.md`, `plan-validation-chain.md`, `plan-commission-migration.md`, `plan-group-payment-cycle.md`
- Creative: `creative/creative-ubspsutpaymentgroupfrm-designer-layout.md`, `creative-ubspsutpaymentgroupfrm-channel-contract.md`, `creative-ubspsutpaymentgroupfrm-constants.md`

### Intentional or documented VB6 omissions

- Channel contract lists **unused** VB commands (`UtReadSettingChoiceClient`, `UtReadTypePayment`, etc.) — do not wire unless product restores them.

## TESTING

- **Build**: MSBuild **Debug** reported OK during BUILD (per `tasks.md` / reflection).
- **Automated tests**: None recorded for this form.
- **Recommended UAT** (from reflection): `GROUP_EDIT` / `GROUP_ADD`, full save pipeline, commission refresh, browse shells, card path; after enabling script runner, regression on **group continuation** and **`EndGroup`**.

## LESSONS LEARNED

- Keep **`Run("` audit** in sync with `creative-ubspsutpaymentgroupfrm-channel-contract.md` after each BUILD slice.
- **Stubbed interop** must be listed in archive/reflection so it is not mistaken for parity.
- **`object[row, col]`** mapping for VB `variant(field, record)` avoids systematic indexing bugs.
- **Conditional PostBuild UNC** improves developer machines without the team share.

## REFERENCES

| Document | Path |
|----------|------|
| Reflection | `memory-bank/reflection/reflection-ubspsutpaymentgroupfrm-conversion.md` |
| Channel contract | `memory-bank/creative/creative-ubspsutpaymentgroupfrm-channel-contract.md` |
| Constants inventory | `memory-bank/creative/creative-ubspsutpaymentgroupfrm-constants.md` |
| Designer layout | `memory-bank/creative/creative-ubspsutpaymentgroupfrm-designer-layout.md` |
| Progress snapshot (at archive) | `memory-bank/progress.md` |

## DEVIATIONS FROM VB6 (CHECKLIST)

| # | Topic | VB6 / spec | .NET as archived |
|---|--------|------------|------------------|
| 1 | Post-save group script | `IUbsRunScript`, `PsPaymentIncomingReception.vbs`, `EndGroup` | **Stubbed** (`RunGroupContinuationScript` body commented) |
| 2 | Last-payment prefetch | `UtGetAccINNFromLastPayment` when client + BIC after contract | **Missing** in `FindContractbyId` |
| 3 | Client picker control | `btnClient` | **`LinkLabel` `linkFIO`** (functional equivalent) |
| 4 | Passport / IPDL | `UbsComCheck` COM | **`UbsComValidateLibrary`** static helpers |
| 5 | UI layout | Legacy screenshots | **Not runtime-verified** vs screenshots at archive time |

---

*End of archive. Start the next task with `/van`.*
