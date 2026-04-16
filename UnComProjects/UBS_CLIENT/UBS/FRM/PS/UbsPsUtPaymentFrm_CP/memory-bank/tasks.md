# Tasks: UbsPsUtPaymentFrm Conversion

## Complexity Determination
- **Level**: 4 (Complex System / Enterprise)
- **Rationale**:
  - Legacy VB6 document form must be converted to .NET Framework 2.0 WinForms.
  - Main source is `UtPayment.dob` plus child forms `frmCalc.frm`, `frmCashOrd.frm`, and `frmCashSymb.frm`.
  - Project starts from template `UbsFormProject1` and must be renamed to `UbsPsUtPaymentFrm`.
  - UI parity is required against `legacy-form/screens`.
  - Conversion should follow patterns proven in `UbsPsUtPaymentGroupFrm_CP` and `UbsPsContractFrm_CP`.
  - UBS channel integration, event migration, and form-template constraints are expected.
- **Workflow**: VAN -> PLAN -> CREATIVE -> BUILD -> REFLECT -> ARCHIVE

## Current Phase: BUILD Wave 2 — core infrastructure and ListKey

### PLAN deliverables (Level 4)
- [x] Define complete .NET file structure for `UbsPsUtPaymentFrm`
- [x] Inventory all forms and modules from `legacy-form/UtPayment`
- [x] Map all VB6 controls to .NET controls and target names
- [x] Plan project rename from `UbsFormProject1` to `UbsPsUtPaymentFrm`
- [x] Identify all channel calls, `ParamIn` keys, and `ParamOut` keys from VB6 sources
- [x] Plan child-form strategy for `frmCalc`, `frmCashOrd`, and `frmCashSymb`
- [x] Compare legacy screens with target designer layout and template constraints
- [x] Define partial-class split strategy for the .NET form

### Technology validation (PLAN gate)
- **Stack**: .NET Framework 2.0 WinForms class library with UBS assemblies
- **Base pattern**: `UbsFormBase`, `panelMain`, template-aligned table layout
- **References**: use existing successful PS conversions as implementation patterns
- **Designer rules**: keep template structure intact; map VB6 OCX controls to approved .NET controls
- **Data mapping**: VB6 variant matrices map to C# `object[row, column]`

## Complete .NET file structure (planned summary)

Reference baseline used for this plan:

- Template source: `UbsFormProject1/`
- Sibling patterns: `UbsPsUtPaymentGroupFrm`, `UbsPsContractFrm`
- Legacy scope: `UtPayment.dob`, `frmCalc.frm`, `frmCashOrd.frm`, `frmCashSymb.frm`, `modWinAPI.bas`

### Conversion root (`UbsPsUtPaymentFrm_CP`)

```text
UbsPsUtPaymentFrm_CP/
|-- .cursor/
|   `-- rules/
|-- legacy-form/
|   |-- UtPayment/
|   |   |-- UtPayment.dob
|   |   |-- UtPayment.vbp
|   |   |-- UtPayment.vbw
|   |   |-- UtPayment.PDM
|   |   |-- frmCalc.frm
|   |   |-- frmCashOrd.frm
|   |   |-- frmCashSymb.frm
|   |   `-- modWinAPI.bas
|   `-- screens/
|-- memory-bank/
|   |-- activeContext.md
|   |-- archive/
|   |-- creative/
|   |-- progress.md
|   |-- projectbrief.md
|   |-- reflection/
|   |-- style-guide.md
|   |-- systemPatterns.md
|   |-- tasks.md
|   `-- techContext.md
`-- UbsPsUtPaymentFrm/                      # renamed from UbsFormProject1
```

### Target project tree (`UbsPsUtPaymentFrm/`)

```text
UbsPsUtPaymentFrm/
|-- Properties/
|   `-- AssemblyInfo.cs
|-- UbsPsUtPaymentFrm.sln
|-- UbsPsUtPaymentFrm.csproj
|-- UbsPsUtPaymentFrm.cs                    # ctor, fields, IUbs entry points, shared handlers
|-- UbsPsUtPaymentFrm.Constants.cs          # command strings, parameter keys, captions, messages
|-- UbsPsUtPaymentFrm.Designer.cs           # panelMain layout, tabs, groups, controls
|-- UbsPsUtPaymentFrm.resx                  # main form resources
|-- UbsPsUtPaymentFrm.Initialization.cs     # InitDoc, read/load paths, enable/disable, fillers
|-- UbsPsUtPaymentFrm.Save.cs               # save flow, validation chain, add-fields save
|-- UbsPsUtPaymentFrm.Keys.cs               # KeyDown/KeyPress, Enter/Tab navigation
|-- UbsPsUtPaymentFrm.Commission.cs         # penalty/commission/NDS calculations
|-- UbsPsUtPaymentFrm.BrowseShell.cs        # dictionaries, browse-return handlers, contract/client search
|-- UbsPsUtPaymentFrm.Cash.cs               # cash-order creation, cash-symbol dialog orchestration
|-- NativeMethods.cs                        # POINT struct + user32/kernel32 P/Invoke from modWinAPI.bas
|-- FrmCalc.cs
|-- FrmCalc.Designer.cs
|-- FrmCalc.resx
|-- FrmCashOrd.cs
|-- FrmCashOrd.Designer.cs
|-- FrmCashOrd.resx
|-- FrmCashSymb.cs
|-- FrmCashSymb.Designer.cs
`-- FrmCashSymb.resx
```

### Main form partial-class ownership

| File | Planned responsibility |
|------|------------------------|
| `UbsPsUtPaymentFrm.cs` | `m_addCommand`, `CommandLine`, `ListKey`, shared fields/flags/ids, constructor, top-level button handlers |
| `UbsPsUtPaymentFrm.Constants.cs` | `LoadResource`, channel command names, `ParamIn`/`ParamOut` keys, user-facing messages, shell keys |
| `UbsPsUtPaymentFrm.Designer.cs` | `panelMain`, bottom action `TableLayoutPanel`, tab host, group boxes, all control declarations, event wire-up |
| `UbsPsUtPaymentFrm.Initialization.cs` | `Initialize`, `InitDoc`, `ReadContract`, `FindContract`, `FindContractbyId`, `FillDataPayment`, `FillTariff`, `FillPayer`, `FillPurpose`, field state setup |
| `UbsPsUtPaymentFrm.Save.cs` | `btnSave_Click`, `btnSaveAttribute_Click`, `CheckPayment`, `CheckRS`, `CheckTerror`, `Ut_CheckUserBeforeSave`, `CheckLockPassport`, `CheckIPDL`, close-guard checks |
| `UbsPsUtPaymentFrm.Keys.cs` | `UserDocument_KeyDown`, `UserDocument_KeyPress`, `FormKeyPress`, `GoToNextControl`, tab navigation helpers |
| `UbsPsUtPaymentFrm.Commission.cs` | `CheckPeni`, `CalcSumCommiss`, `CalcSumCommiss_2`, `CalcSumNDS`, amount/penalty recalculation handlers |
| `UbsPsUtPaymentFrm.BrowseShell.cs` | `btnClient_Click`, `btnContract_Click`, `btnPattern_Click`, `btnPaymDic_Click`, `btnFindFilter_Click`, `btnListAttributeRecip_Click`, browse-return and dictionary helpers |
| `UbsPsUtPaymentFrm.Cash.cs` | `cmdCashSymb_Click`, `CreateCashOrd`, cash-order follow-up, cash-symbol arrays and dialog result handling |
| `NativeMethods.cs` | C# replacement for `modWinAPI.bas` (`POINT`, `GetCursorPos`, `Sleep`) |

### Child dialog plan

| File set | Legacy source | Planned role |
|----------|---------------|--------------|
| `FrmCalc.*` | `frmCalc.frm` | Modal calculator used by `btnCalc_Click` / payment amount workflows |
| `FrmCashOrd.*` | `frmCashOrd.frm` | Cash order creation/move dialog invoked from main save/cash-order flow |
| `FrmCashSymb.*` | `frmCashSymb.frm` | Cash symbol selection dialog opened by `cmdCashSymb_Click` |

### `.csproj` include plan

- Keep `TargetFrameworkVersion` = `v2.0`.
- Rename `RootNamespace` and `AssemblyName` to `UbsPsUtPaymentFrm`.
- Compile items should include the main form, all planned partials, all three child dialog forms, and `NativeMethods.cs`.
- `DependentUpon` should point each partial to `UbsPsUtPaymentFrm.cs` and each dialog designer/resx to its dialog main file.
- UBS references should follow sibling PS projects with `<Private>False</Private>`.
- Expected reference set: `System`, `System.Data`, `System.Drawing`, `System.Windows.Forms`, `System.Xml`, `UbsBase`, `UbsChannel`, `UbsCollections`, `UbsForm`, `UbsFormBase`, `UbsInterface`, plus form-specific UBS controls such as `UbsCtrlInfo`, `UbsCtrlDecimal`, `UbsCtrlFields`, `UbsCtrlAccount`, `UbsCtrlDate`, and `UbsComValidateLibrary` when required by migrated logic.

### Notes / exclusions

- `panelMain` remains inherited from the template; do not redefine it.
- `bin/`, `obj/`, `.vs/`, backup files, and upgrade logs are not part of the planned target structure.
- If `Save.cs` or `Initialization.cs` becomes too large during BUILD, a later split to `UbsPsUtPaymentFrm.Validation.cs` is allowed, but it is not required for the initial file structure.

## Legacy artifact inventory (`legacy-form/UtPayment`)

### Inventory summary

| Legacy artifact | Kind | Conversion status | Planned .NET destination / handling |
|-----------------|------|-------------------|--------------------------------------|
| `UtPayment.dob` | Main VB6 `UserDocument` | Convert | `UbsPsUtPaymentFrm` main WinForms form with partial-class split |
| `frmCalc.frm` | Child VB6 form | Convert | `FrmCalc.cs` + `.Designer.cs` + `.resx` |
| `frmCashOrd.frm` | Child VB6 form | Convert | `FrmCashOrd.cs` + `.Designer.cs` + `.resx` |
| `frmCashSymb.frm` | Child VB6 form | Convert | `FrmCashSymb.cs` + `.Designer.cs` + `.resx` |
| `modWinAPI.bas` | Support module | Convert selectively | `NativeMethods.cs` for P/Invoke declarations used by migrated code |
| `UtPayment.vbp` | VB6 project manifest | Reference only | Planning/build reference for source composition and OCX dependencies |
| `UtPayment.vbw` | VB6 workspace state | Reference only | No .NET equivalent required |
| `UtPayment.PDM` | VB6 design metadata | Reference only | Keep only as legacy reference; no direct .NET output |

### Main legacy document

| File | Role in VB6 system | Important migration notes |
|------|--------------------|---------------------------|
| `UtPayment.dob` | Primary payment-entry `UserDocument` with 6-tab UI, UBS channel host, validation, save pipeline, contract lookup, browse/dictionary actions, commission/NDS logic, cash-order and cash-symbol orchestration | Maps to the main `UbsPsUtPaymentFrm` partial set. OCX surface includes `UbsCtrl`, `UbsProp`, `UbsDDXCtrl`, `UbsInfo`, `SSActiveTabs`, `UbsChlCtrl`, and `UbsComboEditCtrl`; these drive future control-mapping and CREATIVE decisions. |

### Child forms and modules

| File | Kind | VB6 responsibility | Public state / contract seen from main form | Notable channel usage / dependencies |
|------|------|--------------------|---------------------------------------------|--------------------------------------|
| `frmCalc.frm` | Modal child form | Cash calculation helper: compares entered cash amount vs accepted payment amount and returns change | `curSumPaym`, `TypeExit`; opened modally from `btnCalc_Click` flow | No direct `UbsChannel` usage; depends on `ToolPubs.IUbsFormat` and error helpers |
| `frmCashOrd.frm` | Modal child form | Prepares and launches cash-order creation for one payment or grouped payments, showing a document list before apply | `TypeExit`, `UbsChannel`, `varPaymIn`, `varContractIn`, `m_nIdPayment`, `m_nIdPaymentArray`, `m_bIsLoadOK`, `blnCreate` | Runs `UtGetGlobalUserData`, `Ps_FindAccCash1`, `UtGetDataCashOrder`, `UtMainCashOrder`; uses `MSComctlLib.ListView` |
| `frmCashSymb.frm` | Modal child form | Maintains cash-symbol rows and validates total amount against the payment total before returning an array | `arrCashSymb`, `ArrayData`, `curSummaTotal`, `UbsChannel`, `arrTypeCashSymbol`, `TypeExit` | Runs `UtCheckArrayCashSymbol`; uses `MSFlexGrid` and `UbsInfo` help text |
| `modWinAPI.bas` | Support module | Small native helper module for cursor position and sleep timing | `POINTAPI`, `GetCursorPos`, `Sleep` | Replace only if referenced by migrated C# code; place in `NativeMethods.cs` |

### Legacy project manifest findings

`UtPayment.vbp` confirms the runtime composition of the VB6 project:

- `UserDocument=UtPayment.dob`
- `Form=frmCashSymb.frm`
- `Form=frmCalc.frm`
- `Form=frmCashOrd.frm`

It also confirms the OCX/library surface the migration must account for:

- `UbsCtrl.dll`
- `UBSChilds.dll`
- `UBSParrents.dll`
- `UbsCtrlV.dll`
- `UbsProp.dll`
- `UbsPrMan.dll`
- `UbsDDXCtrl.ocx`
- `UbsInfo.ocx`
- `MSCOMCTL.OCX`
- `sstabs2.ocx`
- `UbsChlCtrl.ocx`
- `MSFLXGRD.OCX`
- `UbsComboEditCtrl.ocx`

### Inventory conclusions for BUILD planning

- Runtime-converted legacy artifacts are exactly five files: `UtPayment.dob`, `frmCalc.frm`, `frmCashOrd.frm`, `frmCashSymb.frm`, and `modWinAPI.bas`.
- Metadata-only legacy artifacts are `UtPayment.vbp`, `UtPayment.vbw`, and `UtPayment.PDM`; they inform planning but should not produce .NET files.
- Child-form integration is non-trivial because both `frmCashOrd.frm` and `frmCashSymb.frm` exchange arrays/flags with the main form and invoke UBS channel commands.
- The inventory confirms the planned destination files from the file-structure section are complete for the known VB6 runtime surface.

## VB6 controls -> .NET controls and target names

### Mapping rules used for this plan

- Naming uses prefix-based C# field names: `btn`, `txt`, `lbl`, `cmb`, `chk`, `grp`, `tab`, `tabPage`, `tbl`, `udc`, `uca`, `ucf`, `uci`.
- `SSActiveTabs` -> `TabControl`; `SSActiveTabPanel` -> `TabPage`.
- `UbsControlMoney` -> `UbsCtrlDecimal`.
- `UbsControlAccount` -> `UbsCtrlAccount`.
- `UbsControlProperty` -> `UbsCtrlFields`.
- `UbsInfo` -> `UbsCtrlInfo`.
- `UbsComboEditControl` is planned as editable `ComboBox` unless a later CREATIVE decision requires a custom UBS host control.
- `UbsChannel` and `UbsDDXControl` are not planned as visible WinForms controls; their behavior moves into channel wiring and manual binding code.

### Main form shell and non-visual components

| Legacy control | VB6 type | Target .NET type | Target name | Notes |
|----------------|----------|------------------|-------------|-------|
| `UtPayment_F` | `VB.UserDocument` | `UbsFormBase` descendant | `UbsPsUtPaymentFrm` | Main converted form |
| `subPayment` | `SSActiveTabs` | `TabControl` | `tabPayment` | Six-tab shell |
| `TabMain` | `SSActiveTabPanel` | `TabPage` | `tabPageGeneral` | General payment tab |
| `TabAdd` | `SSActiveTabPanel` | `TabPage` | `tabPageTariff` | Tariff tab |
| `SSActiveTabPanel1` | `SSActiveTabPanel` | `TabPage` | `tabPageTelephone` | Telephone tab |
| `SSActiveTabPanel2` | `SSActiveTabPanel` | `TabPage` | `tabPageAddFields` | Additional fields tab |
| `SSActiveTabPanel3` | `SSActiveTabPanel` | `TabPage` | `tabPageTax` | Tax details tab |
| `SSActiveTabPanel4` | `SSActiveTabPanel` | `TabPage` | `tabPageThirdPerson` | Third-person payer tab |
| `chkPrintForm` | `VB.CheckBox` | `CheckBox` | `chkPrintForms` | Print-form toggle |
| `cmdCashSymb` | `VB.CommandButton` | `Button` | `btnCashSymbols` | Opens `FrmCashSymb` |
| `txtGRPId` | `VB.TextBox` | `TextBox` | `txtGroupPaymentId` | Hidden/read-only group id |
| `curCommonSumma` | `UbsControlMoney` | `UbsCtrlDecimal` | `udcCommonAmount` | Common amount summary |
| `txtQttySubs` | `VB.TextBox` | `TextBox` | `txtSubPaymentCount` | Read-only sub-payment count |
| `btnPattern` | `VB.CommandButton` | `Button` | `btnUserPattern` | Opens custom pattern/user form |
| `btnCalc` | `VB.CommandButton` | `Button` | `btnCalcCash` | Opens `FrmCalc` |
| `UbsChannel` | `UbsChannel` | non-visual channel integration | `IUbsChannel` / base channel use | Keep logic, not Designer control |
| `btnExit` | `VB.CommandButton` | `Button` | `btnExit` | Main exit button |
| `UbsDDX` | `UbsDDXControl` | no direct control | manual binding helpers | Replace with explicit read/write code |
| `curTotalSumma` | `UbsControlMoney` | `UbsCtrlDecimal` | `udcTotalAmount` | Bottom total amount |
| `btnSave` | `VB.CommandButton` | `Button` | `btnSave` | Main save button |
| `lblCommonSumma` | `VB.Label` | `Label` | `lblCommonAmount` | Label for common amount |
| `lblQttySubs` | `VB.Label` | `Label` | `lblSubPaymentCount` | Label for sub-payment count |
| `Timer1` | `VB.Timer` | `System.Windows.Forms.Timer` | `tmrForm` | Periodic UI/business refresh |
| `Info` | `UbsInfo32.Info` | `UbsCtrlInfo` | `uciPaymentInfo` | Main info/message strip |

### General tab (`tabPageGeneral`)

#### Sender / general area

| Legacy control | VB6 type | Target .NET type | Target name |
|----------------|----------|------------------|-------------|
| `tb_NumFolder` | `VB.TextBox` | `TextBox` | `txtFolderNumber` |
| `cb_ThirdPerson` | `VB.CheckBox` | `CheckBox` | `chkThirdPerson` |
| `btnPaymDic` | `VB.CommandButton` | `Button` | `linkPaymentAccount` |
| `txtKSNDS` | `VB.TextBox` | `TextBox` | `txtKsNds` |
| `txtKSRate` | `VB.TextBox` | `TextBox` | `txtKsRate` |
| `txtKSPayment` | `VB.TextBox` | `TextBox` | `txtKsPayment` |
| `txtCheckSum` | `VB.TextBox` | `TextBox` | `txtCheckSum` |
| `AccPay` | `VB.TextBox` | `TextBox` | `txtPayerAccount` |
| `cboCityCode` | `VB.ComboBox` | `ComboBox` | `cmbCityCode` |
| `txtDayBeg` | `VB.TextBox` | `TextBox` | `txtPeriodDayBeg` |
| `txtMonthBeg` | `VB.TextBox` | `TextBox` | `txtPeriodMonthBeg` |
| `txtYearBeg` | `VB.TextBox` | `TextBox` | `txtPeriodYearBeg` |
| `txtDayEnd` | `VB.TextBox` | `TextBox` | `txtPeriodDayEnd` |
| `txtMonthEnd` | `VB.TextBox` | `TextBox` | `txtPeriodMonthEnd` |
| `txtYearEnd` | `VB.TextBox` | `TextBox` | `txtPeriodYearEnd` |
| `txtCodePayment` | `VB.TextBox` | `TextBox` | `txtPaymentCode` |
| `curPeny` | `UbsControlMoney` | `UbsCtrlDecimal` | `udcPenaltyAmount` |
| `curSummaTotal` | `UbsControlMoney` | `UbsCtrlDecimal` | `udcAmountWithRate` |
| `curSummaRateSend` | `UbsControlMoney` | `UbsCtrlDecimal` | `udcPayerRateAmount` |
| `curSumma` | `UbsControlMoney` | `UbsCtrlDecimal` | `udcPaymentAmount` |
| `lb_NumFolder` | `VB.Label` | `Label` | `lblFolderNumber` |
| `lblNalog` | `VB.Label` | `Label` | `lblTaxTabHint` |
| `lblKSNDS` | `VB.Label` | `Label` | `lblKsNds` |
| `lblKSRate` | `VB.Label` | `Label` | `lblKsRate` |
| `lblKSPayment` | `VB.Label` | `Label` | `lblKsPayment` |
| `lblFR` | `VB.Label` | `Label` | `lblCashRegister` |
| `Label6` | `VB.Label` | `Label` | `lblPaymentAccount` |
| `Label7` | `VB.Label` | `Label` | `lblCheckSum` |
| `Label8` | `VB.Label` | `Label` | `lblCityCode` |
| `lblAcc` | `VB.Label` | `Label` | `linkPaymentAccount` |
| `lblPeriod1` | `VB.Label` | `Label` | `lblPeriodBeg` |
| `lblPeriod2` | `VB.Label` | `Label` | `lblPeriodEnd` |
| `lblCodePayment` | `VB.Label` | `Label` | `lblPaymentCode` |
| `lblPeny` | `VB.Label` | `Label` | `lblPenaltyAmount` |

#### Recipient group (`frmRec` -> `grpRecipient`)

| Legacy control | VB6 type | Target .NET type | Target name |
|----------------|----------|------------------|-------------|
| `frmRec` | `VB.Frame` | `GroupBox` | `grpRecipient` |
| `txtKppu` | `VB.TextBox` | `TextBox` | `txtRecipientKpp` |
| `cmbPurpose` | `UbsComboEditControl` | `ComboBox` | `cmbPurpose` |
| `btnSaveAttribute` | `VB.CommandButton` | `Button` | `btnSaveRecipientAttribute` |
| `btnListAttributeRecip` | `VB.CommandButton` | `Button` | `btnRecipientAttributeList` |
| `txtNameBank` | `VB.TextBox` | `TextBox` | `txtRecipientBankName` |
| `btnContract` | `VB.CommandButton` | `Button` | `linkContractCode` |
| `txtCode` | `VB.TextBox` | `TextBox` | `txtContractCode` |
| `txtComment` | `VB.TextBox` | `TextBox` | `txtRecipientComment` |
| `txtNote` | `VB.TextBox` | `TextBox` | `txtRecipientNote` |
| `txtBic` | `VB.TextBox` | `TextBox` | `txtRecipientBik` |
| `txtINN` | `VB.TextBox` | `TextBox` | `txtRecipientInn` |
| `txtRecip` | `VB.TextBox` | `TextBox` | `txtRecipientName` |
| `AccKorr` | `UbsControlAccount` | `UbsCtrlAccount` | `ucaRecipientCorrAccount` |
| `AccClient` | `UbsControlAccount` | `UbsCtrlAccount` | `ucaRecipientAccount` |
| `Label26` | `VB.Label` | `Label` | `lblRecipientKpp` |
| `lblNameCnt` | `VB.Label` | `Label` | `lblRecipientBankName` |
| `lblNoteCnt` | `VB.Label` | `Label` | `lblRecipientNote` |
| `Label11` | `VB.Label` | `Label` | `lblRecipientBik` |
| `Label12` | `VB.Label` | `Label` | `lblRecipientInn` |
| `Label13` | `VB.Label` | `Label` | `lblRecipientName` |
| `Label16` | `VB.Label` | `Label` | `lblRecipientPurpose` |
| `Label14` | `VB.Label` | `Label` | `lblRecipientComment` |
| `Label2` | `VB.Label` | `Label` | `lblRecipientCorrAccount` |
| `Label3` | `VB.Label` | `Label` | `lblRecipientAccount` |
| `lblRecip` | `VB.Label` | `Label` | `lblRecipientGroupTitle` |

#### Sender group (`frmSend` -> `grpSender`)

| Legacy control | VB6 type | Target .NET type | Target name |
|----------------|----------|------------------|-------------|
| `frmSend` | `VB.Frame` | `GroupBox` | `grpSender` |
| `TextReason` | `VB.TextBox` | `TextBox` | `txtBenefitReason` |
| `CheckBenefits` | `VB.CheckBox` | `CheckBox` | `chkBenefits` |
| `btnFindFilter` | `VB.CommandButton` | `Button` | `linkFindFilter` |
| `txtNomerCardPay` | `VB.TextBox` | `TextBox` | `txtPayerCardNumber` |
| `AccClientPay` | `UbsControlAccount` | `UbsCtrlAccount` | `ucaPayerAccount` |
| `txtInfoClient` | `VB.TextBox` | `TextBox` | `txtPayerClientInfo` |
| `txtAdressPay` | `VB.TextBox` | `TextBox` | `txtPayerAddress` |
| `txtFIOPay` | `VB.TextBox` | `TextBox` | `txtPayerFullName` |
| `btnClient` | `VB.CommandButton` | `Button` | `linkPayerFullName` |
| `txtINNPay` | `VB.TextBox` | `TextBox` | `txtPayerInn` |
| `Label30` | `VB.Label` | `Label` | `lblBenefitReason` |
| `lblFindFilter` | `VB.Label` | `Label` | `lblFindFilter` |
| `lblNomerCardPay` | `VB.Label` | `Label` | `lblPayerCardNumber` |
| `lblAccClientPay` | `VB.Label` | `Label` | `lblPayerAccount` |
| `Label4` | `VB.Label` | `Label` | `lblPayerClientInfo` |
| `Label15` | `VB.Label` | `Label` | `lblPayerAddress` |
| `Label5` | `VB.Label` | `Label` | `lblPayerFullName` |
| `Label1` | `VB.Label` | `Label` | `lblPayerInn` |

### Tariff tab (`tabPageTariff`)

| Legacy control | VB6 type | Target .NET type | Target name |
|----------------|----------|------------------|-------------|
| `cmbTariff` | `UbsComboEditControl` | `ComboBox` | `cmbTariff` |
| `lblTariff` | `VB.Label` | `Label` | `lblTariff` |

### Telephone tab (`tabPageTelephone`)

| Legacy control | VB6 type | Target .NET type | Target name |
|----------------|----------|------------------|-------------|
| `cmbPhone` | `UbsComboEditControl` | `ComboBox` | `cmbPhone` |
| `lblPhone` | `VB.Label` | `Label` | `lblPhone` |

### Additional fields tab (`tabPageAddFields`)

| Legacy control | VB6 type | Target .NET type | Target name | Notes |
|----------------|----------|------------------|-------------|-------|
| `AddProperties` | `UbsControlProperty` | `UbsCtrlFields` | `ucfAddProperties` | Requires add-fields support collection |

### Tax tab (`tabPageTax`)

| Legacy control | VB6 type | Target .NET type | Target name |
|----------------|----------|------------------|-------------|
| `txtKBKNoteNalog` | `VB.TextBox` | `TextBox` | `txtTaxKbkNote` |
| `txtIMNSNalog` | `VB.TextBox` | `TextBox` | `txtTaxImns` |
| `txTypeNalog` | `VB.TextBox` | `TextBox` | `txtTaxType` |
| `txtDateNalog` | `VB.TextBox` | `TextBox` | `txtTaxDocumentDate` |
| `txtNomerNalog` | `VB.TextBox` | `TextBox` | `txtTaxDocumentNumber` |
| `txtPeriodNalog` | `VB.TextBox` | `TextBox` | `txtTaxPeriodCode` |
| `txtReasonNalog` | `VB.TextBox` | `TextBox` | `txtTaxReasonCode` |
| `txtOKATONalog` | `VB.TextBox` | `TextBox` | `txtTaxOkato` |
| `txtKBKNalog` | `VB.TextBox` | `TextBox` | `txtTaxKbk` |
| `txtStatusNalog` | `VB.TextBox` | `TextBox` | `txtTaxStatus` |
| `Label25` | `VB.Label` | `Label` | `lblTaxKbkNote` |
| `Label24` | `VB.Label` | `Label` | `lblTaxImns` |
| `Label23` | `VB.Label` | `Label` | `lblTaxType` |
| `Label22` | `VB.Label` | `Label` | `lblTaxDocumentDate` |
| `Label21` | `VB.Label` | `Label` | `lblTaxDocumentNumber` |
| `Label20` | `VB.Label` | `Label` | `lblTaxPeriodCode` |
| `Label19` | `VB.Label` | `Label` | `lblTaxReasonCode` |
| `Label18` | `VB.Label` | `Label` | `lblTaxOkato` |
| `Label17` | `VB.Label` | `Label` | `lblTaxKbk` |
| `Label10` | `VB.Label` | `Label` | `lblTaxStatus` |

### Third-person tab (`tabPageThirdPerson`)

| Legacy control | VB6 type | Target .NET type | Target name |
|----------------|----------|------------------|-------------|
| `cmb_ThirdPersonKind` | `VB.ComboBox` | `ComboBox` | `cmbThirdPersonKind` |
| `btnSelectThirdPerson` | `VB.CommandButton` | `Button` | `linkThirdPersonName` |
| `txt_ThirdPersonKpp` | `VB.TextBox` | `TextBox` | `txtThirdPersonKpp` |
| `txt_ThirdPersonInn` | `VB.TextBox` | `TextBox` | `txtThirdPersonInn` |
| `txt_ThirdPersonName` | `VB.TextBox` | `TextBox` | `txtThirdPersonName` |
| `Label29` | `VB.Label` | `Label` | `lblThirdPersonKpp` |
| `Label28` | `VB.Label` | `Label` | `lblThirdPersonInn` |
| `Label27` | `VB.Label` | `Label` | `lblThirdPersonName` |
| `Label9` | `VB.Label` | `Label` | `lblThirdPersonKind` |

### Child form `frmCalc.frm`

| Legacy control | VB6 type | Target .NET type | Target name |
|----------------|----------|------------------|-------------|
| `frmCalc` | `VB.Form` | `Form` | `FrmCalc` |
| `cmdExit` | `VB.CommandButton` | `Button` | `btnExit` |
| `cmdCalcSum` | `VB.CommandButton` | `Button` | `btnCalculate` |
| `curSummaNal` | `UbsControlMoney` | `UbsCtrlDecimal` | `udcCashAmount` |
| `curSummaChange` | `VB.Label` | `Label` | `lblChangeAmountValue` |
| `curSummaPaym` | `VB.Label` | `Label` | `lblPaymentAmountValue` |
| `Label4` | `VB.Label` | `Label` | `lblChangeAmount` |
| `Label3` | `VB.Label` | `Label` | `lblCashAmount` |
| `Label2` | `VB.Label` | `Label` | `lblAcceptedPaymentsAmount` |

### Child form `frmCashOrd.frm`

| Legacy control | VB6 type | Target .NET type | Target name |
|----------------|----------|------------------|-------------|
| `frmCashOrd` | `VB.Form` | `Form` | `FrmCashOrd` |
| `btnMove` | `VB.CommandButton` | `Button` | `btnExecute` |
| `btnExit` | `VB.CommandButton` | `Button` | `btnExit` |
| `lstDoc` | `MSComctlLib.ListView` | `System.Windows.Forms.ListView` | `lvwDocuments` |

### Child form `frmCashSymb.frm`

| Legacy control | VB6 type | Target .NET type | Target name | Notes |
|----------------|----------|------------------|-------------|-------|
| `frmCashSymb` | `VB.Form` | `Form` | `FrmCashSymb` | Child cash-symbol editor |
| `cmdExit` | `VB.CommandButton` | `Button` | `btnExit` | Close without save |
| `cmdSave` | `VB.CommandButton` | `Button` | `btnSave` | Validate and return values |
| `FlxGrdDemo` | `MSFlexGrid` | `DataGridView` | `grdCashSymbols` | Editable 2-column grid |
| `lblHelpPay` | `UbsInfo32.Info` | `UbsCtrlInfo` | `uciHelp` | Static keyboard-help panel |

### Control-mapping conclusions

- The main form contains one tab shell, six tab pages, two group containers, four non-visual/runtime helper components, and the payment-entry fields that should live in `UbsPsUtPaymentFrm.Designer.cs`.
- The .NET naming plan deliberately converts anonymous labels like `Label17` into semantic names so the final Designer is maintainable.
- Two VB6 controls require design decisions rather than one-to-one replacement: `UbsComboEditControl` and `UbsDDXControl`.
- `frmCashSymb.frm` is the only artifact that likely benefits from a `DataGridView` rather than a direct UBS control replacement because the legacy `MSFlexGrid` is editable grid UI.

## Project rename plan: `UbsFormProject1` -> `UbsPsUtPaymentFrm`

### Current template state

The starter project still contains mixed template naming:

- Folder: `UbsFormProject1/`
- Project file: `UbsFormProject1.csproj`
- Solution file: `UbsFormProject1.slnx`
- Main form files: `UbsForm1.cs`, `UbsForm1.Designer.cs`, `UbsForm1.resx`
- Class name: `UbsForm1`
- `.csproj` metadata still uses `UbsFormTemplate` for `RootNamespace` and `AssemblyName`
- Namespace in code is already `UbsBusiness`, which should be preserved

### Rename objective

Standardize the template into the final PS conversion target so every user-visible and build-relevant artifact is aligned on `UbsPsUtPaymentFrm`.

### File and symbol rename matrix

| Current artifact | Target artifact | Action |
|------------------|-----------------|--------|
| `UbsFormProject1/` | `UbsPsUtPaymentFrm/` | Rename folder |
| `UbsFormProject1.csproj` | `UbsPsUtPaymentFrm.csproj` | Rename file and update project metadata |
| `UbsFormProject1.slnx` | `UbsPsUtPaymentFrm.sln` | Replace template solution with standard sibling-style `.sln` |
| `UbsForm1.cs` | `UbsPsUtPaymentFrm.cs` | Rename file and main partial class |
| `UbsForm1.Designer.cs` | `UbsPsUtPaymentFrm.Designer.cs` | Rename file, partial class, and `DependentUpon` |
| `UbsForm1.resx` | `UbsPsUtPaymentFrm.resx` | Rename resource file and `DependentUpon` |
| `UbsForm1` class | `UbsPsUtPaymentFrm` class | Rename main form type |
| Template partial expansion | `UbsPsUtPaymentFrm.*.cs` | Add planned partial files under final form name |

### `.csproj` metadata changes

| Property / item | Current value | Target value / policy |
|-----------------|---------------|------------------------|
| `RootNamespace` | `UbsFormTemplate` | `UbsPsUtPaymentFrm` |
| `AssemblyName` | `UbsFormTemplate` | `UbsPsUtPaymentFrm` |
| `Compile Include` main form | `UbsForm1.cs` | `UbsPsUtPaymentFrm.cs` |
| `Compile Include` designer | `UbsForm1.Designer.cs` | `UbsPsUtPaymentFrm.Designer.cs` |
| `EmbeddedResource Include` | `UbsForm1.resx` | `UbsPsUtPaymentFrm.resx` |
| `DependentUpon` references | `UbsForm1.cs` | `UbsPsUtPaymentFrm.cs` |
| Partial compile items | none beyond Designer | add all planned partials and child forms |
| `TargetFrameworkVersion` | `v2.0` | keep unchanged |
| `ProjectGuid` | existing template guid | keep unless build/tooling requires regeneration |

### Code-level rename scope

| Location | Current | Target |
|----------|---------|--------|
| Main class declaration | `public partial class UbsForm1 : UbsFormBase` | `public partial class UbsPsUtPaymentFrm : UbsFormBase` |
| Constructor | `public UbsForm1()` | `public UbsPsUtPaymentFrm()` |
| Designer partial | `partial class UbsForm1` | `partial class UbsPsUtPaymentFrm` |
| Form `Name` property | `UbsForm1` | `UbsPsUtPaymentFrm` |
| Form caption text | template caption | replace with payment-form caption during BUILD/layout work |
| Channel load resource placeholder | `DllName.dll->UbsBusiness.NameClass` | explicit final resource string in `UbsPsUtPaymentFrm.Constants.cs` |

### `AssemblyInfo.cs` alignment

Update assembly metadata to remove template leftovers:

- `AssemblyTitle("UbsPsUtPaymentFrm")`
- `AssemblyProduct("UbsPsUtPaymentFrm")`
- Keep company/copyright only if they match sibling PS projects; otherwise normalize during BUILD
- Keep versioning unchanged unless the local PS pattern requires a specific value

### Solution / local-state handling

- Do not carry forward `.vs/` template state into the planned target structure.
- Prefer sibling-style `.sln` instead of the template `.slnx`, because the reference PS projects use `.sln`.
- Any local `DocumentLayout` files remain excluded from the rename plan.

### Planned BUILD order for the rename

1. Rename the project folder to `UbsPsUtPaymentFrm`.
2. Rename `UbsFormProject1.csproj` to `UbsPsUtPaymentFrm.csproj`.
3. Rename the main form files from `UbsForm1.*` to `UbsPsUtPaymentFrm.*`.
4. Update class names, constructor name, designer partial name, and form `Name`.
5. Update `.csproj` `RootNamespace`, `AssemblyName`, compile includes, resource includes, and `DependentUpon`.
6. Replace the template solution file with `UbsPsUtPaymentFrm.sln`.
7. Update `AssemblyInfo.cs` to final assembly metadata.
8. Add the planned partial-class files and child-form files under final names.
9. Verify that no references to `UbsFormProject1`, `UbsForm1`, or `UbsFormTemplate` remain in the target project.

### Validation checklist for the rename

- No remaining build-relevant references to `UbsFormProject1`
- No remaining type references to `UbsForm1`
- No remaining template assembly metadata `UbsFormTemplate`
- Final project folder, `.csproj`, solution, form type, and resource names all match `UbsPsUtPaymentFrm`
- `.csproj` still targets `.NET Framework 2.0`
- `namespace UbsBusiness` remains intact unless BUILD uncovers a host requirement

### Rename-plan conclusion

- The rename is not a simple folder rename; it is a coordinated template-cleanup step across folder names, file names, class names, assembly metadata, and solution format.
- The largest hidden risk is the template metadata mismatch (`UbsFormProject1` file names but `UbsFormTemplate` assembly properties), so BUILD must verify both strings are eliminated.

## Channel calls and parameter contract from VB6 sources

### Contract rules observed in VB6

- Command names and parameter keys must be treated as exact string literals.
- The legacy code uses case-sensitive variants that must not be normalized during migration.
- Known examples of distinct spellings/casing that must be preserved:
  - `Payment` and `PAYMENT`
  - `StrError`, `strError`, and `error`
  - `IdContract` and `IDCONTRACT`
  - `AccCode` and `ACCCODE`
  - `Summa` and `SUMMA`

### Channel command inventory

#### Main form: `UtPayment.dob`

Observed unique runtime calls:

- `UtInitAddFields`
- `FindContrByBicAndAccount`
- `SaveAttributeRecip`
- `Ut_CheckSidPayment`
- `Ut_GetDataLic`
- `Ut_GetUserPaymentData`
- `UT_AplyUserPaymentData`
- `ReadKBK`
- `ReadClientFromNomerCard`
- `ReadClientFromIdOC`
- `InitDialogCalc`
- `GetGlobal_ParamBaseCurrency`
- `PutAddFlCalc`
- `CheckOrEndGroup`
- `PAYMENT`
- `Payment`
- `PS_UserIsCashier`
- `GetNalogNumber`
- `CheckKBK`
- `CheckRegex108Field`
- `DeleteDocForm`
- `NalogAddFieldSave`
- `PS_GetSummaControl`
- `PS_GetState`
- `UTGetNewPaymGroupID`
- `Payment_Save`
- `UTIsMoveValByAccountA`
- `UtGetDoublePaymentsPlatDocExt_Form`
- `UtGetPaymentsPlatDocExt_Form`
- `IsAskEdit`
- `RunScrFromSetting`
- `UtGetAddField`
- `ReadRecipFromId`
- `Ps_CheckPrintForm`
- `InitForm`
- `UtReadSettingEnterCashSymbol`
- `UtReadSettingChoiceClient`
- `UtReadSettingSourceMeans`
- `UTReadSettingChechkClient`
- `UTCheckBankRecipient`
- `UtCheckAccFromBic`
- `UtCheckCloseAccout`
- `PsCheckTofkAcc`
- `CheckKey`
- `UtReadSumForCashControl`
- `CheckAddFields`
- `CheckSumPayment`
- `UtCheckCashSymbol`
- `UtCheckCashSymbolComiss`
- `UtCheckCashSymbolNDS`
- `UtCalcSumCommiss`
- `UtCalcSumNDS`
- `UtReadSetupLockPurpose`
- `UtGetStateClearFieldSend`
- `UtGetAutoFillPeriod`
- `GetPropAccCode`
- `UtCheckBIKBank`
- `UtCheckBIKLimitSharing`
- `ReadBankBIK`
- `ReadContractbyCode`
- `UtReadTypePayment`
- `UtReadContract`
- `UtCheckPaymentBenefits`
- `UtGetKPPU`
- `UtGetAccINNFromLastPayment`
- `Ps_GetArrayPrepareCashOrd`
- `Ps_GetArrayPrepareCashOrdSecond`
- `Ps_PreparePlatDoc`
- `Ps_GetStatePrepareCashOrd2`
- `UtGetGlobalUserData`
- `Ps_GetStateRequestFormCashOrd`
- `ReadTariff`
- `ReadNalog`
- `ReadPhone`
- `UtCheckAndSplitAccount`
- `UtCalcKey`
- `CheckTerror`
- `UtReadOurBankBik`
- `UTListAddRead`
- `Ut_CheckBeforeSave`
- `UtGetINNFromLastPayment`
- `UtGetKPPUPayerLastPayment`
- `UtReadGroupInfo`
- `GetGroupIDByPayment`
- `UtReadLic`
- `GetIdClientFromGroupPayment`
- `CheckPaymDic`
- `UserAddField`

Observed external script-runner call tied to the same form workflow:

- `CallingUserFormPattern` via `objScrRun.Run(...)`

#### Child form: `frmCashOrd.frm`

- `UtGetGlobalUserData`
- `Ps_FindAccCash1`
- `UtGetDataCashOrder`
- `UtMainCashOrder`

#### Child form: `frmCashSymb.frm`

- `UtCheckArrayCashSymbol`

### Observed `ParamIn` keys

#### Main form: high-confidence direct `ParamIn` keys

Identifiers / command context:

- `StrCommand`
- `COMMANDIN`
- `StrCommand_Source`
- `NameSection`
- `SIDPATTERN`
- `FROMFO`
- `CalledFromFrontOffice`
- `fromFoAsClient`
- `Test`

Contract/payment/group ids:

- `IdContract`
- `IDCONTRACT`
- `IdContractSecond`
- `IdPayment`
- `IDPAYMENT`
- `IdPaym`
- `IdMainIncoming`
- `IdGroupIncoming`
- `PAYMENTGROUPID`
- `IdGroup`
- `IdPaymentDic`
- `ID_DOUBLE_PAYMENT`
- `IdTariff`
- `IdPhone`
- `IdPattern`
- `IdAttributeRecip`
- `IdClient`
- `IDCLIENT`
- `ID_CLIENT`
- `IDKINDPAYMENT`

Recipient / bank / account data:

- `BIC`
- `BicExtBank`
- `ACC`
- `Account_R`
- `AccClient`
- `AccClientPay`
- `ACCCLIENTPAY`
- `AccCode`
- `ACCCODE`
- `CORRACC`
- `RecipientName`
- `RECIPIENTNAME`
- `Purpose`
- `PURPOSE`
- `CodeContract`
- `CODECONTRACT`
- `CodePayment`
- `CODEPAYMENT`
- `CODE`
- `INN`
- `Kppu`
- `����`
- `<��� ����� ����������>,<���� ����������>`

Payer / person / lookup data:

- `PAYERFIO`
- `PAYERINN`
- `PAYERADDRESS`
- `NAME`
- `NomerCard`
- `IsGuest`
- `blnGuest`
- `txtINNPay`
- `ThirdPerson_Name`
- `ThirdPerson_Kind`
- `ThirdPerson_INN`
- `ThirdPerson_KPP`
- `RunUserForm`
- `DocHandle`
- `InitArray`

Amounts / rates / totals:

- `SUMMA`
- `SUMMAPENI`
- `SUMMARATESEND`
- `SUMMARATEREC`
- `SUMMAREC`
- `SUMMATOTAL`
- `SUMMANDSSEND`
- `SUMMANDSREC`
- `SummaRateRec`
- `SummaRec`
- `SummaNDSRec`
- `SummaNDSSend`
- `SummaNDSPaym`
- `Sum`
- `mSumma`
- `curCommonSumma`
- `NeedControl`

Period / registry / derived account data:

- `DateBeg`
- `DateEnd`
- `DATEBEGIN`
- `DATEEND`
- `RegNumber`
- `Address`
- `CheckSum`
- `SignCreateKey`
- `bIsPeriodEnable`
- `nLenKey`
- `strSignKey`
- `strNameProcAcc`
- `strNameProcKey`
- `LAST`

Tax / regulatory / compliance data:

- `Field108`
- `blnIPDL`
- `blnTerror`
- `������ �����������`
- `��� ��������� �������������`
- `��� �����`
- `��������� ���������� �������`
- `��������� ������`
- `����� ���������� ���������`
- `���� ���������� ���������`
- `��� ���������� �������`
- `����`
- `����� �����`
- `�������� ������� �������������� ������`

Miscellaneous runtime objects:

- `DocObj`
- `objDevice`
- `nIdPayment`
- `bIsFR`
- `nPaymentGroupId`
- `nPaymentGroupQtty`
- `nPaymentGroupSUMMA`
- `nPaymentGroupTOTAL`
- `curSUMMAPENI`
- `blnGroup`
- `strBusinessName`
- `State`
- `BicExtBank`
- `AccountExtBank`
- `NameExtBank`
- `varAddFields`

#### Child form: `frmCashOrd.frm` `ParamIn`

- `REGIM`
- `NUMDIV`
- `DATEDOC`
- `DATETRN`
- `ACCDB`
- `VARPAYMENTS`
- `VARCONTRACT`
- `IsBySeparPaym`
- `IdPayment`
- `���������`

#### Child form: `frmCashSymb.frm` `ParamIn`

- `arrCashSymb`
- `arrTypeCashSymbol`

### Observed `ParamOut` keys

#### Main form: direct `ParamOut` keys

General status / errors:

- `bRetVal`
- `bRet`
- `RETVAL`
- `StrError`
- `strError`
- `error`
- `Error`
- `StrQuestion`
- `strMessage`
- `bMsgBoxYesNo`

Lookup/read results:

- `RecCount`
- `IdArray`
- `IDCLIENT`
- `INN`
- `InfoClient`
- `NAME`
- `ADRESS`
- `NUMBER`
- `SERIES`
- `NalogNumber`
- `isNalog`
- `Result`
- `DocumentsExists`

Initialization / setting outputs:

- `IsFrmPrn`
- `bIsFR`
- `strRegName`
- `strRegNumber`
- `objDevice`
- `bIsScan`
- `objScan`
- `EndGroup`
- `arrCityCode`
- `DateBeg`
- `DateEnd`
- `blnIsErrorKey`
- `ViewPrintForm`
- `UtEnterCashSymbol`
- `ChoiceClient`
- `UtEnterSourceMeans`
- `strOurBankBic`

Payment and save outputs:

- `CodePayment`
- `arrDataPayment`
- `IdPaym`
- `Type`
- `PaymentGroupSum`
- `PAYMENTGROUPID`
- `ID_DOUBLE_PAYMENT`
- `IdPaymentGroup`
- `State`
- `SumControl`
- `NotCashier`
- `blnPeparePlatDoc`
- `StateCashOrd`
- `StateCashRate`
- `IDUSER`
- `Division`
- `ISFRMPREVIEW`

Business arrays / lists:

- `ListSymbols`
- `arrTypeCashSymbol`
- `CashSymbol`
- `ArrayCashSymbols`
- `arrPurpose`
- `Tarif_Name_Rate`
- `Tarif_Name_Rate_Default`
- `Code_Energy`
- `Phone_Code_Name`
- `Phone_Code_Name_Default`
- `VARPAYMENTS`
- `VARCONTRACT`
- `VARIDPAYMENTS`

Contract / payment detail outputs:

- `Code`
- `Comment`
- `BIC`
- `AccCorr`
- `NameBank`
- `INNRec1`
- `AccRec`
- `Note`
- `RecipientName`
- `VisibleCodePayment`
- `AccCode`
- `IncludeKey`
- `CityCode`
- `SummaPaym`
- `Summa`
- `SummaRateSend`
- `SummaTotal`
- `SummaRec`
- `SummaRateRec`
- `IdContract`
- `IDCONTRACT`
- `IdTariff`
- `IdClient`
- `IdPhone`
- `IdPattern`
- `RateTypeSend`
- `RatePercentSend`
- `MinSumSend`
- `MaxSumSend`
- `ShowNDS`
- `blnLetter`
- `PeniPresent`
- `PeniComissPayer`
- `CASHSYM`
- `CASHSYMRATESEND`
- `CASHSYMNDS`

Validation / derived account outputs:

- `ErrNum`
- `CheckSum`
- `bIsCheckKey`
- `TypeTerror`
- `isBenefits`
- `Benefits`
- `AddFields`

External user-form/script outputs:

- `ButtonCaption`
- `OptionOK`
- `Results`

#### Nested output object: `arrDataPayment`

The main form treats `arrDataPayment` as a parameter bag with its own keys. Observed nested keys include:

- `ACCCODE`
- `SUMMA`
- `THIRDPERSON_NAME`
- `THIRDPERSON_KIND`
- `THIRDPERSON_INN`
- `THIRDPERSON_KPP`
- `SUMMARATESEND`
- `SUMMATOTAL`
- `SUMMARATEREC`
- `CORRACC`
- `ACC`
- `INN`
- `PURPOSE`
- `PAYERFIO`
- `PAYERINN`
- `PAYERADDRESS`
- `BIC`
- `Summa`
- `SummaRateSend`
- `SummaTotal`
- `CheckSum`
- `AccCode`
- `Period`
- `DateBegin`
- `DateEnd`
- `AccClient`

#### Child form: `frmCashOrd.frm` `ParamOut`

- `DATEDOC`
- `bRetVal`
- `BRETVAL`
- `StrError`
- `StrAccCash`
- `NumDiv`
- `VARDOC`
- `���������`

#### Child form: `frmCashSymb.frm` `ParamOut`

- `bRetValCheck`
- `strError`
- `arrCashSymb`

### Commands with the densest contracts

These calls should be documented first in a later dedicated creative contract file because they drive most of the migration risk:

- `InitForm`
- `Payment` / `PAYMENT`
- `Payment_Save`
- `UtReadContract`
- `Ut_GetUserPaymentData`
- `Ut_CheckBeforeSave`
- `ReadClientFromIdOC`
- `ReadBankBIK`
- `UtReadTypePayment`
- `Ps_GetArrayPrepareCashOrd`
- `Ps_GetArrayPrepareCashOrdSecond`
- `UtGetDataCashOrder`
- `UtMainCashOrder`
- `UtCheckArrayCashSymbol`

### Contract conclusions for BUILD / CREATIVE

- The VB6 form uses a broad channel surface with more than 80 distinct command names across the main form and child dialogs.
- The save/read contract is array-heavy: `arrDataPayment`, `AddFields`, `VARPAYMENTS`, `VARCONTRACT`, `VARIDPAYMENTS`, `ArrayCashSymbols`, and `arrPurpose` need careful .NET representation.
- Key spelling and case are inconsistent by design, so migration must preserve exact parameter strings rather than normalizing them.
- `Payment`, `Payment_Save`, `InitForm`, and `Ut_CheckBeforeSave` form the core contract that should anchor the later CREATIVE channel-contract document.

## Child-form strategy for `frmCalc`, `frmCashOrd`, and `frmCashSymb`

### Shared strategy for all child dialogs

- Keep all three dialogs as separate modal WinForms classes under `UbsPsUtPaymentFrm/`.
- Preserve the legacy ownership pattern: the main form prepares dialog input, shows the dialog modally, then consumes a small returned state object or result properties.
- Do not let child dialogs directly update main-form controls; the parent form remains the orchestration owner.
- In .NET, replace the VB6 `TypeExit` integer convention with a small explicit result contract while keeping the same business meaning.
- Dialog-launch code belongs in `UbsPsUtPaymentFrm.Cash.cs` for cash-order and cash-symbol flows, and in `UbsPsUtPaymentFrm.Commission.cs` or `UbsPsUtPaymentFrm.cs` for the calculator flow.

### Result-contract pattern

Preferred .NET pattern for all three dialogs:

- Dialog exposes strongly named input properties before `ShowDialog(this)`.
- Dialog sets a semantic result on successful completion.
- Parent reads output properties only when the dialog result indicates success.

Recommended common result model:

| Legacy | .NET plan |
|--------|-----------|
| `TypeExit = 1` | `DialogResult.OK` or `IsConfirmed = true` |
| `TypeExit = 0` | `DialogResult.Cancel` or `IsConfirmed = false` |

### `FrmCalc` strategy

#### Legacy interaction

- Main form sets `frmCalc.curSumPaym = objParamOut.Parameter("Summa")`.
- Main form opens `frmCalc` modally.
- On success (`TypeExit = 1`), the dialog simply confirms the cash amount is sufficient and computes change internally.
- No channel calls occur inside the dialog.

#### .NET responsibility

- Keep `FrmCalc` as a lightweight modal calculator dialog with no direct channel access.
- Treat it as a pure UI helper that validates entered cash amount against a passed-in payment amount.
- Keep formatting/error behavior local to the dialog.

#### Planned .NET contract

Inputs:

- `PaymentAmount` (`decimal`)

Outputs:

- `CashAmount` (`decimal`)
- `ChangeAmount` (`decimal`)
- `IsConfirmed` (`bool`)

Planned parent flow:

1. Main form computes or reads the payable sum.
2. Parent creates `FrmCalc`, assigns `PaymentAmount`, and opens it with `ShowDialog`.
3. If confirmed, parent proceeds with the cash workflow; otherwise it leaves state unchanged.

BUILD note:

- `FrmCalc` should not write back into main-form controls automatically; the parent decides whether any UI update is needed after confirmation.

### `FrmCashSymb` strategy

#### Legacy interaction

- Main form passes:
  - `UbsChannel`
  - `ArrayData`
  - `arrTypeCashSymbol`
  - `curSummaTotal`
- Dialog edits an in-memory grid, validates total amount, then calls `UtCheckArrayCashSymbol`.
- On success it returns `arrCashSymb` to the main form and closes with `TypeExit = 1`.

#### .NET responsibility

- Keep `FrmCashSymb` as a modal editor/validator for cash-symbol rows.
- It owns only local grid editing and channel validation for that grid.
- It should not know the main form�s controls, only the passed array-like data and totals.

#### Planned .NET contract

Inputs:

- `IUbsChannel` or channel reference
- `CashSymbolsSource` (`object[row, column]` or dialog-specific row model list)
- `AllowedCashSymbols` (`object[row, column]`)
- `ExpectedTotal` (`decimal`)

Outputs:

- `CashSymbolsResult` (`object[row, column]`)
- `IsConfirmed` (`bool`)

UI/data strategy:

- Replace `MSFlexGrid` with `DataGridView`.
- Preserve the two-column editing model: cash symbol + amount.
- Preserve keyboard-oriented editing as much as practical, but exact shortcut behavior can be refined in CREATIVE/BUILD.
- Continue using channel validation through `UtCheckArrayCashSymbol` before success is returned.

Owner strategy:

- Parent method in `UbsPsUtPaymentFrm.Cash.cs` builds the initial array model.
- Parent opens `FrmCashSymb`.
- If confirmed, parent stores returned `CashSymbolsResult` into the main-form field that currently receives `arrCashSymb`.

### `FrmCashOrd` strategy

#### Legacy interaction

- Main form passes:
  - `UbsChannel`
  - either single-payment or grouped-payment arrays (`varPaymIn`, `varContractIn`)
  - `m_nIdPayment` or `m_nIdPaymentArray`
- Dialog loads itself by:
  1. reading global user data
  2. finding the cash debit account
  3. reading cash-order documents
  4. populating the document list
- Two usage modes exist:
  - interactive modal preview when `lstDoc` has rows
  - automatic execution path where the main form calls `Form_Load`, checks `m_bIsLoadOK`, then directly calls `btnMove_Click`

#### .NET responsibility

- Keep `FrmCashOrd` as the dedicated cash-order preview/execute dialog.
- Preserve both modes:
  - preview mode for user confirmation
  - silent/automatic mode for immediate execution after pre-load succeeds

#### Planned .NET contract

Inputs:

- `IUbsChannel` or channel reference
- `PaymentsData` (`object[row, column]`)
- `ContractsData` (`object[row, column]`)
- `PaymentId` (`long`)
- `PaymentIdArray` (`object[row, column]` or array wrapper) for grouped payments
- `AutoExecute` (`bool`)

Outputs:

- `IsConfirmed` (`bool`)
- `WasCreated` (`bool`)
- `LoadedSuccessfully` (`bool`)

Internal dialog phases:

1. `LoadContext()`:
   - `UtGetGlobalUserData`
   - `Ps_FindAccCash1`
2. `LoadDocuments()`:
   - `UtGetDataCashOrder`
3. `ExecuteCashOrder()`:
   - `UtMainCashOrder`

UI strategy:

- Replace the VB6 list view with `System.Windows.Forms.ListView`.
- Keep the four visible columns:
  - payer account
  - recipient account
  - amount
  - note

Owner strategy:

- The parent opens `FrmCashOrd` only when preview is required and there are documents to show.
- The parent uses `AutoExecute = true` when the legacy path bypasses user preview.
- The parent reads `WasCreated` after the dialog closes and updates `m_blnCreateCashOrd`-style state itself.

### Recommended .NET APIs by dialog

| Dialog | Recommended entry point | Why |
|--------|-------------------------|-----|
| `FrmCalc` | properties + `ShowDialog` | simplest modal helper |
| `FrmCashSymb` | properties + `ShowDialog` | modal editor that returns validated data |
| `FrmCashOrd` | properties + `ShowDialog`, plus internal `AutoExecute` path | needs both preview and silent execution modes |

### Array/data representation guidance

- When child dialogs consume VB6 variant matrices, store them in C# as `object[row, column]`.
- Where a dialog needs friendlier local editing, convert the incoming array into an internal row model for the UI, then convert back to `object[row, column]` on success.
- `FrmCashSymb` is the clearest candidate for an internal row model because of grid editing.

### Main-form ownership plan

| Main-form file | Dialog orchestration responsibility |
|----------------|-------------------------------------|
| `UbsPsUtPaymentFrm.Cash.cs` | open `FrmCashSymb`; open/auto-run `FrmCashOrd`; store returned cash-symbol and cash-order state |
| `UbsPsUtPaymentFrm.Commission.cs` or main `.cs` | open `FrmCalc`; consume success result |

### Risks and mitigation

- `FrmCashOrd` has the highest migration risk because it mixes load-time channel reads, preview UI, and execution logic. Keep its phases explicit in .NET instead of hiding them in event handlers.
- `FrmCashSymb` has the highest UI-parity risk because `MSFlexGrid` editing is keyboard-driven. Preserve the business result first; exact key choreography can be refined later.
- `FrmCalc` is low risk and should be kept intentionally simple to avoid over-design.

### Child-form strategy conclusion

- `FrmCalc` should remain a pure modal calculator dialog.
- `FrmCashSymb` should become a modal grid editor with local validation plus one channel-based validation call.
- `FrmCashOrd` should become a modal preview/execute dialog with an optional auto-execute mode, and its channel phases should be explicit methods rather than implicit VB6 event sequencing.

## Legacy screens vs target designer layout and template constraints

### Template baseline

The local and canonical `UbsFormProject1` template confirm these fixed layout constraints:

- `panelMain` remains the primary inherited content surface.
- The bottom action strip is a `TableLayoutPanel` docked to bottom inside `panelMain`.
- Bottom strip structure is fixed to three columns:
  - stretch info column
  - `Save` button column with width `88`
  - `Exit` button column with width `88`
- Bottom strip height is `32`.
- `UbsCtrlInfo` belongs in the bottom strip, not as a second separate info area outside the template pattern.

### What the legacy screenshots show

Common structure visible across all six screenshots:

- A top tab strip with six tabs:
  - `��������`
  - `�������� � ������� ����`
  - `�����`
  - `���������� ����`
  - `�����`
  - `�������������� ��������`
- A persistent footer area under the tab content.
- The footer contains:
  - payment count field
  - two summary amount fields
  - `�������� �����` checkbox
  - action buttons: `���� �������`, `���������������� �����`, `���������`, `������`, `�����`
- The main screen is significantly denser than the template and therefore drives the converted form size.

### Screen-by-screen comparison

| Legacy screen | Visible content | Designer implication |
|---------------|-----------------|----------------------|
| `1.png` | Dense main tab with two stacked group regions (`����������`, `����������`) plus a lower amount/action band | Main tab needs a scrollable content host or generous client area; grouped layout is mandatory |
| `2.png` | Third-person tab with simple vertical label/field stack | `tabPageThirdPerson` can be a simple fixed vertical form without scroll in normal size |
| `3.png` | Tariff tab with one label + combo | `tabPageTariff` is sparse and can use simple anchored placement |
| `4.png` | Telephone tab with one label + combo | `tabPageTelephone` is sparse and can use simple anchored placement |
| `5.png` | Tax tab with a long left-label / right-editor form | `tabPageTax` needs a clean two-column form layout; vertical spacing matters more than complex containers |
| `6.png` | Additional properties tab with full-height grid/list surface | `tabPageAddFields` should dedicate most of the page to `UbsCtrlFields` / add-fields host and keep it fill-oriented |

### Main tab comparison against template

Legacy `1.png` versus template `UbsForm1.Designer.cs`:

- Template currently provides only the footer strip and no content hierarchy.
- Legacy main tab needs:
  - top `TabControl` filling the client area above the footer
  - `����������` group at the top
  - `����������` group below it
  - a lower band containing cash-symbol fields, payment amount fields, penalty, totals, period, payer account, and the calculator button
- Because the legacy main tab is visually packed, the converted Designer should prefer:
  - `tabPayment.Dock = Fill`
  - a `Panel` with `AutoScroll = true` on the main tab
  - anchored `GroupBox` layout inside that panel

### Footer comparison

Legacy footer versus template footer:

- Legacy footer is wider and richer than the current template footer.
- Template footer already matches the essential pattern for:
  - save/exit button placement
  - bottom docking
  - info/status zone
- Required adaptation:
  - preserve the template `tblActions` row as-is in height and docking semantics
  - place the legacy extra footer controls just above or integrated with the main content area, not by breaking the bottom template strip

Planned footer strategy:

- Keep the template bottom row only for:
  - `uciInfo`
  - `btnSave`
  - `btnExit`
- Place these legacy footer-era controls in the main tab content just above `tblActions`:
  - payment count
  - summary amount fields
  - `chkPrintForms`
  - `btnCashSymbols`
  - `btnUserPattern`
  - `btnCalcCash`

### Tab-layout implications

- `tabPayment` must be the primary content organizer in the converted form.
- All six tabs seen in the screenshots should exist in the first designer milestone, even if some start with minimal content.
- The first tab (`��������`) is the largest and should drive form size and scroll behavior.
- Tabs `�����`, `���������� ����`, and `�������� � ������� ����` are visually simple and can use direct label/control placement rather than nested layout containers.
- `�������������� ��������` should prioritize fill behavior over fixed coordinates.

### Template constraints to preserve

- Do not remove or redefine `panelMain`.
- Do not change the bottom action strip height away from `32`.
- Do not replace the template bottom strip with a custom footer panel.
- Preserve a standard WinForms docking order:
  - content (`TabControl`) fills
  - action strip docks bottom
- Keep `Save` and `Exit` in the bottom template row even though legacy screenshots show more buttons nearby.

### Designer decisions implied by the comparison

- Final form size must be substantially larger than the raw template starter size (`392x273`) to be usable.
- The main tab should use a scrollable container because of the payer/recipient density and lower amount band.
- The tax tab should use a consistent two-column label/editor layout.
- The add-fields tab should use `Dock = Fill` for its main editor surface.
- The third-person, tariff, and telephone tabs can remain structurally simple.

### Risks revealed by the screenshot comparison

- If the converted form tries to force all main-tab content into a fixed non-scroll layout, controls will clip or become unreadable.
- If extra footer controls are pushed into the template bottom strip, the designer will diverge from the required UBS template structure.
- If the add-fields tab does not get a fill-oriented host, it will not match the legacy screen�s dominant grid area.

### Layout comparison conclusion

- The template already gives the correct structural footer pattern, but almost none of the required content structure.
- The legacy screens confirm that the converted Designer should use:
  - `panelMain`
  - bottom `tblActions`
  - a fill `TabControl`
  - a scrollable main tab
  - simple vertical forms for the sparse tabs
  - a fill-oriented add-fields tab

## Partial-class split strategy for the .NET form

### Why the form should be split

`UtPayment.dob` combines several concerns that would be hard to maintain in a single C# file:

- IUbs shell entry points
- channel initialization and read flows
- large save/validation pipeline
- keyboard and focus behavior
- commission and NDS recalculation logic
- browse shell and dictionary callbacks
- cash-order and cash-symbol orchestration
- dense Designer code

The sibling payment-form pattern already proves that a multi-partial form is the right fit for this type of PS conversion.

### Selected split strategy

Use one `public partial class UbsPsUtPaymentFrm : UbsFormBase` spread across targeted responsibility files:

| File | Primary purpose | Must not own |
|------|-----------------|--------------|
| `UbsPsUtPaymentFrm.cs` | constructor, shared state, IUbs command registration, shell entry handlers, top-level form events | detailed save pipeline, large channel-read helpers, Designer code |
| `UbsPsUtPaymentFrm.Constants.cs` | `const string` command names, parameter keys, captions, message text, add-fields support keys | runtime logic, mutable state |
| `UbsPsUtPaymentFrm.Designer.cs` | controls, layout, event hookup, template footer and tabs | business logic, channel calls |
| `UbsPsUtPaymentFrm.Initialization.cs` | `InitDoc`, startup sequencing, read/lookup helpers, field enable/disable, form population | final save execution |
| `UbsPsUtPaymentFrm.Save.cs` | save entry point, validation chain, add-fields save, close guards tied to save state | browse UI launches, layout code |
| `UbsPsUtPaymentFrm.Keys.cs` | keyboard routing, focus progression, Enter/Escape behavior, account/key helper entry points triggered from key events | general initialization or full save flow |
| `UbsPsUtPaymentFrm.Commission.cs` | amount, commission, NDS, penalty calculations and their local event handlers | contract lookup, browse shell logic |
| `UbsPsUtPaymentFrm.BrowseShell.cs` | client/contract/dictionary open actions and return-path handlers | generic save validation, layout code |
| `UbsPsUtPaymentFrm.Cash.cs` | cash-symbol dialog, calculator integration if cash-specific, cash-order preparation and follow-up state | unrelated read/init helpers |

### Ownership rules per partial

#### `UbsPsUtPaymentFrm.cs`

This file should stay small and act as the form root:

- class declaration
- constructor
- `m_addCommand`
- `CommandLine`
- `ListKey`
- top-level shared event handlers such as `Load` and simple `btnExit_Click`
- private fields and flags shared across multiple partials

Shared-state examples that belong here:

- command/mode fields
- current ids (`m_idPayment`, `m_idContract`, group-payment ids)
- flags like initialized state, dirty state, cash-order state, print-form state
- dialog return caches used by more than one partial

Rule:

- If a field is used by two or more partials, declare it in `UbsPsUtPaymentFrm.cs`.

#### `UbsPsUtPaymentFrm.Constants.cs`

This file centralizes literal stability:

- `LoadResource`
- all `IUbsChannel.Run("...")` command names
- all channel key names for `ParamIn` / `ParamOut`
- user-facing messages and captions that are reused
- special shell keys and add-fields support keys

Rules:

- Keep constants grouped by domain: commands, keys, messages, captions.
- Do not mix mutable runtime state into this file.
- Preserve exact legacy casing and spelling for command names and parameter keys.

#### `UbsPsUtPaymentFrm.Designer.cs`

This file owns only the visual tree:

- template footer
- `tabPayment`
- six tab pages
- group boxes and field declarations
- event subscriptions
- `InitializeComponent`

Rules:

- No channel calls.
- No initialization logic beyond property assignment.
- No hand-written helper methods unless absolutely required for designer support.

#### `UbsPsUtPaymentFrm.Initialization.cs`

This file should gather the read and startup path:

- `InitDoc`
- first-load orchestration after `CommandLine` / `ListKey`
- `InitForm` and startup channel calls
- `ReadContract`, `ReadTariff`, `ReadPhone`, `ReadNalog`
- `FindContract`, `FindContractbyId`
- `FillDataPayment`, `FillTariff`, `FillPayer`, `FillPurpose`
- field-state toggles and enable/disable helpers

Rules:

- Use this file for "load/read/populate" behavior.
- If a method mainly reads data from the channel and writes it into controls, it belongs here.
- Post-read recalculation calls into `Commission.cs` are allowed, but the read orchestration stays here.

#### `UbsPsUtPaymentFrm.Save.cs`

This file owns the transactional path from user intent to channel save:

- `btnSave_Click`
- `btnSaveAttribute_Click`
- `Payment_Save`
- `Ut_CheckBeforeSave`
- `CheckPayment`
- `CheckAddFields`
- `CheckTerror`
- `CheckLockPassport`
- `CheckIPDL`
- close/save guards that block form shutdown with unsaved or invalid state

Rules:

- Save ordering and validation sequencing must be visible and explicit here.
- If a method decides whether save may continue, it belongs here even if it calls helpers in other partials.
- Channel writes for save belong here, not in `Initialization.cs`.

#### `UbsPsUtPaymentFrm.Keys.cs`

This file isolates keyboard behavior that otherwise clutters business logic:

- `KeyDown` / `KeyPress` handlers
- Enter-as-Tab logic
- tab/page navigation helpers
- `CheckRS` and similar field-level actions triggered directly from key flow
- focus redirection after validation failures

Rules:

- Keep only key-driven logic here.
- If a helper exists only because a key event needs it, keep it here.
- If the same helper is also used from save or initialization, move the helper to the more business-oriented partial and call it from `Keys.cs`.

#### `UbsPsUtPaymentFrm.Commission.cs`

This file owns local amount math and related UI reactions:

- `CheckPeni`
- `CalcSumCommiss`
- `CalcSumCommiss_2`
- `CalcSumNDS`
- recalculation handlers for amount, commission, NDS, and penalty editors
- helper methods that normalize money fields after recalculation

Rules:

- Keep numerical recalculation cohesive here even when triggered from multiple controls.
- This file may read constants and form fields, but should not take ownership of the broader save flow.

#### `UbsPsUtPaymentFrm.BrowseShell.cs`

This file owns browse actions and return paths:

- open payer search
- open contract search
- open payment dictionary
- open user-pattern / script launcher
- list filters and recipient-attribute selection
- apply returned browse data to controls

Rules:

- If a method starts with a browse/list/dictionary action or consumes its return payload, it belongs here.
- Any final persistence still routes back to `Save.cs`.

#### `UbsPsUtPaymentFrm.Cash.cs`

This file owns cash-specific workflows:

- `btnCashSymbols_Click`
- calculator dialog launch if kept in the cash workflow
- `CreateCashOrd`
- cash-order preparation arrays
- modal dialog orchestration for `FrmCashOrd` and `FrmCashSymb`
- storage of returned cash-symbol matrix and cash-order creation flags

Rules:

- Keep all `object[row, column]` cash-array adaptation local to this file unless a child dialog needs its own converter.
- Channel calls specific to cash order preparation/execution should stay here or inside the child dialog, not in unrelated partials.

### Cross-partial collaboration rules

- `UbsPsUtPaymentFrm.cs` defines shared fields.
- `Constants.cs` is read by every other partial but should not depend on them.
- `Initialization.cs` may call calculation helpers from `Commission.cs`.
- `Save.cs` may call validators or field checks from `Keys.cs` or `Commission.cs` when they are genuinely reusable, but save sequencing stays in `Save.cs`.
- `BrowseShell.cs` may call `Initialization.cs` helpers to reuse control-population logic after a browse return.
- `Cash.cs` may call `Commission.cs` after cash-symbol or calculator changes affect totals.

### Visibility and method-shape policy

- Keep methods `private` by default.
- Use `private static` only for pure helpers that do not depend on form state.
- Avoid `public` methods on the main form except where the host or child dialogs explicitly require them.
- Prefer a few medium-sized orchestration methods over many tiny one-line wrappers.

### When to create another partial

The initial split above is enough for BUILD. Add a new partial only if one area becomes distinctly oversized or too mixed in responsibility.

Allowed follow-up splits:

- `UbsPsUtPaymentFrm.Validation.cs`
  - create only if `Save.cs` becomes too large because of passport/IPDL/terror/add-fields checks
- `UbsPsUtPaymentFrm.ChannelAdapters.cs`
  - create only if channel parameter bag construction becomes large and duplicated
- `UbsPsUtPaymentFrm.State.cs`
  - create only if shared field declarations become unusually large and hurt readability

Do not split further just to mirror every VB6 region. Prefer stable responsibility boundaries over many tiny files.

### BUILD order for the partials

1. Create `UbsPsUtPaymentFrm.cs`, `Constants.cs`, and `Designer.cs`.
2. Add `Initialization.cs` so the form can load and populate.
3. Add `Commission.cs` for amount behavior needed by initialization and user edits.
4. Add `BrowseShell.cs` for lookup-driven population.
5. Add `Cash.cs` for calculator, cash symbols, and cash-order flows.
6. Add `Save.cs` after read/populate paths are stable enough to build the outgoing parameter bag.
7. Add `Keys.cs` after the main save/read flows work, so keyboard logic does not obscure core migration debugging.

### Partial-split conclusion

- The selected split matches the proven payment-group conversion pattern and extends it with a dedicated cash workflow file because this form has heavier cash-specific behavior.
- The main class file should remain the form root and shared-state home, while each other partial owns one coherent behavior area.
- This strategy is detailed enough for BUILD and does not require any additional partials unless implementation size proves it necessary.

## Creative phases required (PLAN gate output)
- [x] Designer layout vs. `legacy-form/screens` and `UbsFormTemplate` -> `memory-bank/creative/creative-ubspsutpaymentfrm-designer-layout.md`
- [x] Channel contract document for all `Run` calls and parameters -> `memory-bank/creative/creative-ubspsutpaymentfrm-channel-contract.md`
- [x] Constants inventory for commands, captions, and user messages -> `memory-bank/creative/creative-ubspsutpaymentfrm-constants.md`
- [x] Child-form interaction design between main form and dialogs -> `memory-bank/creative/creative-ubspsutpaymentfrm-child-forms.md`
- [ ] Validation / save-flow design if business rules are non-trivial

## Phase 2: CREATIVE - Architecture Decisions
- [x] Designer layout: panelMain, grouping, tabs, field placement -> `memory-bank/creative/creative-ubspsutpaymentfrm-designer-layout.md`
- [x] Constants file: resource names, command strings, captions, messages -> `memory-bank/creative/creative-ubspsutpaymentfrm-constants.md`
- [x] Channel contract: all `Run` calls, `ParamIn`, `ParamOut` -> `memory-bank/creative/creative-ubspsutpaymentfrm-channel-contract.md`
- [x] Child forms: open mode, data exchange, close/return contract -> `memory-bank/creative/creative-ubspsutpaymentfrm-child-forms.md`
- [x] Form appearance: match legacy screenshots in .NET layout -> `memory-bank/creative/creative-ubspsutpaymentfrm-form-appearance.md`

## Form appearance plan: match legacy screenshots in .NET layout

### Visual baseline

The appearance target is defined by `legacy-form/screens/1.png` through `6.png`.

These screenshots represent the minimum parity set for the converted main form:

- `1.png` -> dense `��������` tab with payer block, recipient block, lower totals/action region
- `2.png` -> `�������� � ������� ����` tab
- `3.png` -> `�����` tab
- `4.png` -> `���������� ����` tab
- `5.png` -> `�����` tab
- `6.png` -> `�������������� ��������` tab

### Appearance goals

The .NET form should visually preserve:

- the six-tab shell and tab order
- the two major framed group regions on the main tab
- the dense lower action/summary region on the main tab
- the sparse top-left layouts of the third-person, tariff, and telephone tabs
- the stacked tax-field layout on the tax tab
- the dominant fill/grid-style surface on the additional-properties tab
- the bottom footer presence with summary controls, print toggle, and action buttons above the template footer row

### What must match closely

High-priority visual parity items:

- Tab captions and order exactly matching the screenshots
- Main-tab grouping:
  - `����������`
  - `����������`
- Relative reading order of fields inside both main-tab groups
- Bottom-row business controls above the template footer:
  - payment count
  - two summary amount areas
  - print-form checkbox
  - `���� �������`
  - `���������������� �����`
  - `���������`
  - `������`
  - `�����`
- Tax-tab label order and one-label / one-editor row structure
- Additional-properties tab using nearly all available client space

### What may be approximated

Acceptable controlled differences in .NET:

- exact VB6 pixel coordinates
- exact border thickness and classic control rendering
- the precise footer compression of the VB6 form, because the .NET version must preserve the template footer row
- exact width distribution of the lower main-tab controls, if readability improves and no business control is hidden
- the exact keyboard look-and-feel of the old editable grid on the additional-properties and cash-symbol surfaces

### Screen-by-screen appearance plan

#### Screen `1.png` � `��������`

Target appearance:

- top tab strip remains visible at all times
- `����������` group appears first and spans most of the width
- `����������` group appears directly below and also spans most of the width
- lower area includes:
  - cash-symbol and payment-code fields
  - amount / penalty / commission totals
  - period controls
  - summary count and totals
  - print toggle
  - cash/user/calc/save/exit-style buttons

Implementation appearance rule:

- if space is tight, preserve reading order and grouping before chasing exact coordinate parity

#### Screen `2.png` � `�������� � ������� ����`

Target appearance:

- one simple vertical form near the top-left
- name field first with browse button
- type combo next
- INN
- KPP

Implementation appearance rule:

- leave ample empty space below, just like the legacy screen

#### Screen `3.png` � `�����`

Target appearance:

- one label and one combo aligned near the top-left
- minimal extra UI

#### Screen `4.png` � `���������� ����`

Target appearance:

- one label and one combo aligned near the top-left
- same visual simplicity as the tariff tab

#### Screen `5.png` � `�����`

Target appearance:

- a vertically stacked list of tax labels on the left
- one editor per row on the right
- evenly spaced rows

Implementation appearance rule:

- consistency of row width and spacing matters more than reproducing VB6 control sizes exactly

#### Screen `6.png` � `�������������� ��������`

Target appearance:

- almost all client area dedicated to the add-fields host
- table/grid-like visual dominance
- no unnecessary extra containers

### Template interaction rule

The appearance plan must coexist with the template constraints:

- keep inherited `panelMain`
- keep the template bottom action strip at height `32`
- keep `uciInfo`, `btnSave`, and `btnExit` inside that strip
- place the screenshot footer-era business controls above the template strip in the main content area

This means screenshot parity should be judged by business-region arrangement, not by forcing the template footer to look identical to VB6.

### Visual priority order during BUILD

1. Make all six tabs exist with correct captions and order.
2. Make the `��������` tab readable and grouped correctly.
3. Make the tax tab row layout visually clean.
4. Make the additional-properties tab fill correctly.
5. Tune sparse-tab spacing.
6. Fine-tune widths and button spacing for closer screenshot parity.

### REFLECT acceptance checklist

- [ ] Six tabs exist and match screenshot order/captions
- [ ] `��������` visually reads as payer group, recipient group, then lower summary/action region
- [ ] `�������� � ������� ����` remains a simple top-left vertical form
- [ ] `�����` and `���������� ����` remain sparse single-combo tabs
- [ ] `�����` matches the stacked left-label/right-editor rhythm
- [ ] `�������������� ��������` is dominated by the add-fields host
- [ ] No critical control from the screenshots is missing or visually buried
- [ ] Template footer remains intact and does not absorb extra legacy footer controls

### Plan conclusion

- Screenshot parity is a BUILD and REFLECT quality target rather than a request for one-to-one VB6 pixel copying.
- The converted form should preserve layout intent, grouping, reading order, and tab identity first.
- The largest parity focus is the `��������` tab; the easiest wins are the sparse tabs and add-fields tab.

## Phase 3: BUILD - Implementation

### Wave 1: Scaffold (COMPLETE)
- [x] Rename `UbsFormProject1` project, assembly, namespaces, and form names to `UbsPsUtPaymentFrm`
- [x] Create or update `.sln` and `.csproj` for final names
- [x] Create `AssemblyInfo.cs`
- [x] Create `UbsPsUtPaymentFrm.Designer.cs`
- [x] Create `UbsPsUtPaymentFrm.Constants.cs`
- [x] Create `UbsPsUtPaymentFrm.cs`
- [x] Create `UbsPsUtPaymentFrm.Initialization.cs`
- [x] Create `UbsPsUtPaymentFrm.Save.cs`
- [x] Convert child forms `frmCalc`, `frmCashOrd`, `frmCashSymb`
- [x] Create `.resx`
- [x] Add final project references and post-build behavior if required by existing PS patterns

### Wave 2: Core infrastructure and ListKey (NEXT)

Fixes foundational issues that block every other wave.

- [x] **B2.1 — Fix generic designer names** — renamed `button1`→`btnCalc`, `button2`→`btnPattern`, `button3`→`btnCashSymb`, `label1`/`label2`→`lblCharCount210`/`lblCharCount160`, `label3`→`lblBatchNumber`, `label4`→`lblCityCode`, `textBox1`→`txtBatchNumber`, `textBox2`→`txtCheckSum`, `checkBox1`→`chkThirdPerson` in `.Designer.cs` and field declarations; verified no generic-name references remain in `UbsPsUtPaymentFrm/`
- [x] **B2.2 — Add missing shared state** — added all planned shared fields to `UbsPsUtPaymentFrm.cs` with safe defaults (`string.Empty`, `DateTime.MinValue`, `FoSettingValue = -1` where required) and verified lint status
- [x] **B2.3 — Implement `ListKey`** — fully rewritten from VB6 `UBSChild_ParamInfo("InitParamForm")` with full command routing: `ListKey_Add` (ADD/GROUP_ADD/GROUP_PROCEED/ADD_FROM_CLIENT), `ListKey_AddIncoming`, `ListKey_AddParam` (with `ProcessAddParam` filling 16 named control slots), `ListKey_View` (VIEW/GROUP_VIEW), `ListKey_Copy`, `ListKey_ChangePart` (CHANGE_PART/GROUP_CHANGE/CHANGE_PART_INCOMING); added `m_commandSource`/`fromFoAsClient`/`blnCheckIncoming`/`IdPaymentCopy` fields; added stubs: `AddProcInit`, `IsAutoPeriod`, `GetGroupIDByPaymentID`, `UpdateGroupInfo`, `ProcessAddParam`, `CheckPayer`, `GetBankNameACC`, `CalcSumCommiss_2`, `FillNalog`
- [x] **B2.4 — Wire `Form_Load`** — connected `Load` event (`m_isInitialized`) and `FormClosing` (`CanCloseForm` guard); implemented all core initialization functions: `CheckPeni`, `CheckPayer`, `FillPayer`, `FillNalog`, `FillPurpose`, `FillTariff`, `FillPhone`, `CalcSumCommiss_2`, `GetBankNameACC`, `IsAutoPeriod`, `GetDayEnd`, `DefineRunUserForm`, `GetIdClientFromGroupPayment`, `AddProcInit` (full), `FindContract` (full), `FindContractbyId` (full)
- [ ] **B2.5 — Wire `Form_Closing`** — implement `FormClosing` with `CanCloseForm` / save-in-progress guard
- [ ] **B2.6 — Create `NativeMethods.cs`** — `POINT` struct, `GetCursorPos`, `Sleep` P/Invoke from `modWinAPI.bas`
- [ ] **B2.7 — Add `NativeMethods.cs` to `.csproj`**

### Wave 3: Initialization — full `InitDoc` and data fillers

Completes the form startup so the form actually displays data.

- [x] **B3.1 — Full `InitDoc`** — fully rewritten from VB6 lines 5793–6162: cashier check, InitForm call, post-InitForm processing (ChangeCommand/CloseForm/EndGroup/XOR gate), settings runs (CashSymbol/ChoiceClient/SourceMeans), command branches (View/Copy/ChangePart/Add), period dates, CheckPeni/CheckPayer/DefineRunUserForm. ReadContract also fully converted (lines 6166–6362) with all output param handling and pattern-based tab visibility
- [ ] **B3.2 — `AddProcInit`** — print-form check (`Ps_CheckPrintForm`), script device initialization, FR/scanner setup, `InitForm` call for ADD mode
- [ ] **B3.3 — `FillDataPayment`** — parse `arrDataPayment` variant matrix and populate all General-tab controls (payer, recipient, amounts, cash symbols, purpose, tax fields, periods, etc.)
- [ ] **B3.4 — `FillTariff`** — parse tariff array, populate `cmbTariff`, show/hide tariff tab
- [x] **B3.5 — `FillPurpose`** — clears+populates `cmbPurpose` from variant array, selects first item
- [x] **B3.6 — `FillPayer`** — calls `ReadClientFromIdOC`, populates payer fields (FIO, INN, address, client info, benefits)
- [x] **B3.7 — `FillCityCode`** — already implemented (populates `cmbCityCode` from variant array)
- [x] **B3.8 — `FillNalog`** — calls `ReadNalog`, fills all 11 tax tab controls with VB6→.NET mapping, handles tax status lock and type field disable
- [x] **B3.9 — `FindContract` / `FindContractbyId`** — full rewrite: reads `UtReadTypePayment` + `UtReadContract`, populates recipient fields (ADD_PARAM-aware), rate array, penalties, purpose, bank name, commission calc, pattern-based tab visibility
- [x] **B3.10 — `ApplyInitialFormState` full** — hide/show controls and tabs based on command, set caption, configure group-payment display
- [x] **B3.11 — Third-person fill** — `ThirdPersonKindChanged`, `cmb_ThirdPersonKind_LostFocus` equivalent, third-person tab population
- [x] **B3.12 — `IsAutoPeriod` / `GetDayEnd`** — calls `UtGetAutoFillPeriod`, clears period fields when auto; `GetDayEnd` returns last day of month
- [x] **B3.13 — `UpdateGroupInfo`** — group payment summary display
- [x] **B3.14 — `GetDataClientFromLic`** — populate payer data from personal account
- [x] **B3.15 — `DefineRunUserForm`** — calls user-form pattern script, sets btnPattern caption/visibility (CreateUserFormArray deferred to B7.4)

### Wave 4: Save pipeline — complete validation and field collection

- [ ] **B4.1 — `Payment_Save` full** — collect all control values into `ParamIn` / `arrDataPayment` array before `Run("Payment_Save")`; handle `arrCashSymb`, tax fields, third-person data, add-fields
- [ ] **B4.2 — `CheckLockPassport`** — actual channel call to lock/validate passport
- [ ] **B4.3 — `CheckIPDL`** — actual IPDL check with channel call and `UbsComValidateLibrary` reference (add assembly reference when needed)
- [ ] **B4.4 — `CheckAccPayment`** — settlement account key validation (`CheckKeyInn` for INN, account key check)
- [ ] **B4.5 — `CheckKeyInn`** — INN checksum validation (10-digit and 12-digit)
- [ ] **B4.6 — `NewRecord` / `NewRecordCalc`** — save→new-record flow (clear form, re-init for next payment in group)
- [ ] **B4.7 — Post-save group handling** — `CheckOrEndGroup`, group continuation prompt, `UpdateGroupInfo` after save
- [ ] **B4.8 — Print form logic** — `Ps_CheckPrintForm` post-save, `chkPrintForms` integration, FR device calls
- [ ] **B4.9 — `Form_Closing` save guard** — integrate `CanCloseForm` with `CheckOrEndGroup` for group-mode exit

### Wave 5: `UbsPsUtPaymentFrm.Keys.cs` — keyboard navigation

- [ ] **B5.1 — Create `Keys.cs`** file and add to `.csproj`
- [ ] **B5.2 — `ProcessCmdKey` or `KeyDown`** — F7→`btnCalc_Click`, Ctrl+Tab→next window
- [ ] **B5.3 — Enter-as-Tab** — forward navigation on Enter key (map from `UserDocument_KeyPress` Case 13)
- [ ] **B5.4 — Escape backward navigation** — field-specific reverse focus chain (map from `UserDocument_KeyPress` Case 27 `Select Case ActiveControl.Name`)
- [ ] **B5.5 — `CheckInt` helper** — digits-only input filter for INN, period, and cash-symbol fields
- [ ] **B5.6 — Period field validation** — `txtMonthBeg_Leave`, `txtMonthEnd_Leave`, `txtYearBeg_Leave`, `txtYearEnd_Leave` with range checks and `GetDayEnd` auto-fill
- [ ] **B5.7 — `txtContractCode_Leave`** — trigger `FindContract` when code loses focus
- [ ] **B5.8 — `gotoPreviousTab`** — reverse tab-page navigation helper
- [ ] **B5.9 — Wire key events in Designer** — connect `KeyDown`/`KeyPress`/`Leave` events to handlers

### Wave 6: `UbsPsUtPaymentFrm.Commission.cs` — amount calculations

- [ ] **B6.1 — Create `Commission.cs`** file and add to `.csproj`
- [ ] **B6.2 — `CalcSumCommiss_2`** — main commission calculation with channel call, populate `udcPayerRateAmount`
- [ ] **B6.3 — `CalcSumNDS`** — NDS calculation
- [ ] **B6.4 — `CheckPeni`** — penalty validation/calculation
- [ ] **B6.5 — Amount change handlers** — `udcPaymentAmount_TextChanged` → timer enable, `udcPenaltyAmount_TextChanged`
- [ ] **B6.6 — `timer1_Tick`** — recalculation trigger (debounced sum/commission recalc)
- [ ] **B6.7 — Total amount computation** — `udcAmountWithRate` = payment + commission; `udcCommonAmount`/`udcTotalAmount` for group
- [ ] **B6.8 — `RunEcOperation`** — electronic cashier operation helper
- [ ] **B6.9 — Wire events in Designer** — `TextChanged`, `timer1.Tick`

### Wave 7: `UbsPsUtPaymentFrm.BrowseShell.cs` — browse and dictionary actions

- [ ] **B7.1 — Create `BrowseShell.cs`** file and add to `.csproj`
- [ ] **B7.2 — `linkPayerFullName_LinkClicked` / `btnClient_Click`** — open client/payer selection via `IUbsChannel` browse, consume returned `m_IdClient`, call `FillPayer` / `ReadClientFromIdOC`
- [ ] **B7.3 — `linkContractCode_LinkClicked` / `btnFindContract_Click` / `btnContract_Click`** — open contract list with filter logic (BIC, ACC, INN, КБК, template conditions), consume selected `m_IdContract`, call `FindContractbyId`
- [ ] **B7.4 — `btnPattern_Click` / `DefineRunUserForm`** — user-pattern / script launcher
- [ ] **B7.5 — `btnPaymDic_Click` / `CheckPaymDic`** — payment dictionary open, consume selection into `FillDataPayment("FILTER")`
- [ ] **B7.6 — `linkFindFilter_LinkClicked` / `btnFindFilter_Click`** — open filtered payment list for current payer
- [ ] **B7.7 — `btnRecipientAttributeList_Click` / `btnListAttributeRecip_Click`** — recipient attribute list, consume `ReadRecipFromId` result
- [ ] **B7.8 — `btnSaveRecipientAttribute_Click`** — wire to existing `BtnSaveAttribute_ClickImpl`
- [ ] **B7.9 — `linkRecipientBankName_LinkClicked`** — open bank search via BIC
- [ ] **B7.10 — `linkThirdPersonName_LinkClicked` / `btnSelectThirdPerson_Click`** — open third-person client search
- [ ] **B7.11 — `linkPaymentAccount_LinkClicked`** — open payer account browse
- [ ] **B7.12 — `FillControlsByPattern`** — fill controls from selected pattern/dictionary result
- [ ] **B7.13 — `GetBankNameACC`** — BIC→bank name lookup on BIC `GotFocus`/`Leave`
- [ ] **B7.14 — `txtRecip_Change` equivalent** — recipient name change tracking
- [ ] **B7.15 — Wire browse events in Designer**

### Wave 8: `UbsPsUtPaymentFrm.Cash.cs` — cash workflows

- [ ] **B8.1 — Create `Cash.cs`** file and add to `.csproj`
- [ ] **B8.2 — `btnCashSymb_Click` / `cmdCashSymb_Click`** — build initial cash-symbol array, open `FrmCashSymb`, consume result back into main form fields (`txtKsPayment`, `txtKsRate`, `txtKsNds`)
- [ ] **B8.3 — `btnCalc_Click`** — `InitDialogCalc` channel check, INN validation, open `FrmCalc`, on success call `PutAddFlCalc`, `RunEcOperation`, `NewRecordCalc`
- [ ] **B8.4 — `CreateCashOrd`** — build payment/contract arrays, open `FrmCashOrd`, handle auto-execute vs. preview mode, consume `WasCreated` result
- [ ] **B8.5 — Cash-order post-save integration** — call `CreateCashOrd` after successful `Payment_Save` when applicable
- [ ] **B8.6 — Wire cash events in Designer** — `btnCashSymb.Click`, `btnCalc.Click` (after rename from `button3`/`button1`)

### Wave 9: Designer event wiring and polish

Final pass to ensure every control event is connected.

- [ ] **B9.1 — Verify all button Click events** are wired in `.Designer.cs`
- [ ] **B9.2 — Verify all ComboBox** `SelectedIndexChanged`/`TextChanged` events (`cmbPurpose`, `cmbTariff`, `cmbPhone`, `cmbThirdPersonKind`, `cmbCityCode`)
- [ ] **B9.3 — Verify all CheckBox** `CheckedChanged` events (`chkBenefits`, `chkThirdPerson`, `chkPrintForms`)
- [ ] **B9.4 — Verify `UbsCtrlFields`** `AddProperties_KeyPress` / `AddProperties_ValueChange` wiring
- [ ] **B9.5 — Verify `UbsCtrlAccount`** validation events for `ucaRecipientAccount`, `ucaRecipientCorrAccount`
- [ ] **B9.6 — Remove or document the commented-out `WndProc`** override in Designer
- [ ] **B9.7 — Final lint check** across all partial files

## Phase 4: REFLECT - Review
- [x] Document form-designer reflection -> `memory-bank/reflection/reflection-ubspsutpaymentfrm-designer.md`
- [ ] Verify all VB6 forms/modules were accounted for
- [ ] Verify all channel commands are mapped
- [ ] Verify UI matches `legacy-form/screens`
- [ ] Verify control naming conventions and template constraints
- [ ] Verify .NET 2.0 compatibility and UBS reference conventions
- [ ] Verify error handling pattern uses `Ubs_ShowError`

## Reflection Highlights
- **Designer milestone recorded**: `memory-bank/reflection/reflection-ubspsutpaymentfrm-designer.md`
- **Intentional UI decision**: browse-oriented fields may use `LinkLabel` controls instead of small `...` buttons where discoverability is better.
- **Open follow-up**: overall REFLECT remains blocked on footer/template cleanup, control/event reconciliation, and final build verification on a machine with .NET Framework 2.0 targeting assemblies.

## Phase 5: ARCHIVE - Documentation
- [ ] Archive final conversion notes
- [ ] Record deviations from VB6 original
- [ ] Record unresolved gaps or follow-up tasks
