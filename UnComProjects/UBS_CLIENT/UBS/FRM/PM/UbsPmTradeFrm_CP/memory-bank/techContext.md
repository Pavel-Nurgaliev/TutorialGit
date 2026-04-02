# Memory Bank: Tech Context

## Stack

- .NET Framework 2.0, C#
- Windows Forms (UbsFormBase base class)
- UBS client libraries: UbsBase, UbsChannel, UbsCollections, UbsCtrlInfo, UbsForm, UbsFormBase, UbsInterface

## Key Files

| File | Role |
|------|------|
| `UbsPmTradeFrm/UbsForm1.cs` | Current skeleton — to be renamed/replaced with `UbsPmTradeFrm.cs` |
| `UbsPmTradeFrm/UbsForm1.Designer.cs` | Designer file — to be replaced with `UbsPmTradeFrm.Designer.cs` |
| `UbsPmTradeFrm/UbsPmTradeFrm.csproj` | Project file (TargetFrameworkVersion v2.0, RootNamespace `UbsFormTemplate`) |
| `legacy-form/Pm_Trade/Pm_Trade_ud.dob` | VB6 UserDocument source (5365 lines) |
| `legacy-form/Pm_Trade/frmInstr.frm` | VB6 helper form (payment instruction dialog) |

## Legacy VB6 Specifics

- **Interface:** `UBSChild` (Implements UBSChild)
- **Entry point:** `UBSChild_ParamInfo("InitParamForm")` receives `(NumParentWindow, RSIdent(), strRunParam)` where `RSIdent(0) = ID_TRADE` and `strRunParam = CmdEdit` (i.e. `"EDIT"`) or `strRunParam = CmdAdd + "#<vidTrade>"` (i.e. `"ADD#<vidTrade>"`)
- **Data binding:** DDX (UbsDDXCtrl) — replaced by explicit LoadFromParams/BuildSaveParams in .NET
- **OCX controls used:**
  - `UbsDDXCtrl.ocx` — data binding (DDX) → replaced by .NET data mapping
  - `SSActiveTabs (sstabs2.ocx)` — tabbed panels → replaced by `TabControl`
  - `UbsCtrl.dll` — `UbsControlMoney`, `UbsControlDate` → replaced by `UbsCtrlDecimal`, `UbsCtrlDate` (or `DateTimePicker`)
  - `MSCOMCTL.OCX` — `ListView` → replaced by `ListView`
  - `UbsInfo.ocx` — info label → replaced by `UbsCtrlInfo`
  - `UbsChlCtrl.ocx` — channel control → replaced by `IUbsChannel`
  - `UbsProp.dll` — `UbsControlProperty` (ucpParam) → replaced by `UbsCtrlFieldsSupport` / `UbsCtrlAddFields`

## Channel Contract

- **LoadResource:** `"VBS:UBS_VBD\PM\Pm_Trade.vbs"` (VB6) → .NET ASM path TBD (pattern: `"ASM:UBS_ASM\PM\DllName.dll->UbsBusiness.ClassName"`)

### 2D arrays from the server (`object[,]`)

- The .NET channel exposes 2D data as **`object[row, column]`** (row index first, column index second).
- **VB6 legacy** often indexed the same logical data as **`variant(firstIndex, secondIndex)`** where the first dimension was *not* always “row” in the .NET sense (e.g. `FillControlInstrOplata` used `varOplata(fieldIndex, 0)`).
- When porting, **transpose mentally**: map VB `(i, 0)` to .NET `[0, i]` when the server packs one record as a single row and fields as columns.
- **Combo list arrays** from `TradeCombo_FillPM`: shape **`[n, 2]`** — row `r` has id at `[r, 0]` and text at `[r, 1]`.

### Channel Commands

| Command | In Parameters | Out Parameters | Usage |
|---------|--------------|----------------|-------|
| `GetOneTrade` | `ID_TRADE` | All trade fields (as param array), obligations, instr | Load trade in EDIT |
| `ModifyTrade` | `ID_TRADE`, `АтрибутыСделки`, `ID_CLIENT1`, `ID_CLIENT2`, `ОбязательстваМассив`, `ОбязательстваПараметры`, `ИнструкцияПоДоговоруЗаПокупателя`, `ИнструкцияПоДоговоруЗаПродавца`, `IsNDS`, `IsExport`, `IsExternalStorage` | `ID_TRADE`, optionally `Ошибка` | Save/create trade |
| `TradeCombo_FillPM` | `ID_PATTERN=1` | `ТипыКонтрагентов`, `СписокВалют`, `СписокКомиссий`, `КурсыВалют` | Fill all combos on init |
| `FillOurBIK` | — | `БИК НКО` | Get own bank BIK on init |
| `PMCheckOperationByTrade` | `ID_TRADE` | `Was_Operation` (bool) | Check if ops exist; lock form if true |
| `FillBaseCurrency` | — | `ИдентификаторБазовойВалюты` | Default currency selection in ADD |
| `GetContractPM` | `ID_CONTRACT` | `CODE_CONTRACT`, `LONG_NAME`, `ID_CLIENT` | Load contract details |
| `TradeFillInstr` | `ID_CONTRACT` | Instruction array | Fill payment instruction from contract |
| `GetInstructionOplataCash` | — | `Инструкции по оплате для расчета через кассу` (2D: `[0..n-1, 0..7]`; single row uses row `0`, cols `0`=BIK … `7`=безакцепт) | Load cash instruction for `chkCash` |
| `GetObjectPM` | `ID_OBJECT` | `ДанныеОбъекта` | Load precious metal object details |
| `GetStorage` | `IsExternalStorage`, `Id` | `Code`, `Name` | Load storage info |
| `GetRate_CB` | `Id_Currency_Opl`, `Id_Currency_Oblig`, `Date` | `Rate` | Exchange rate for obligation |
| `GetRateForPM` | (currency ids, date) | Rate data | PM-specific rate fetch |
| `TradeCheckKey` | `БИК`, `РС`, `КС` | `Ошибка` | Validate account key |
| `TradeCheckINN` | `ИНН` | `Ошибка` | Validate INN |
| `FillRekv` | `БИК` | `Наименование`, `КС`, `Is_CBR` | Get bank requisites by BIK |
| `DefineCodCurrency` | `IdCurrency` | `CodCB` | Get ISO currency code for validation |

## .NET Project References

```xml
<Reference Include="UbsBase" HintPath="C:\ProgramData\UniSAB\Assembly\Ubs\UbsBase.dll" />
<Reference Include="UbsChannel" HintPath="C:\ProgramData\UniSAB\Assembly\Ubs\UbsChannel.dll" />
<Reference Include="UbsCollections" HintPath="C:\ProgramData\UniSAB\Assembly\Ubs\Lib\UbsCollections.dll" />
<Reference Include="UbsCtrlInfo" HintPath="C:\ProgramData\UniSAB\Assembly\Ubs\UbsCtrlInfo.dll" />
<Reference Include="UbsForm" HintPath="C:\ProgramData\UniSAB\Assembly\Ubs\UbsForm.dll" />
<Reference Include="UbsFormBase" HintPath="C:\ProgramData\UniSAB\Assembly\Ubs\UbsFormBase.dll" />
<Reference Include="UbsInterface" HintPath="C:\ProgramData\UniSAB\Assembly\Ubs\UbsInterface.dll" />
```

**Additional references needed:**
- `UbsCtrlDecimal` — for UbsControlMoney replacements (decimal/currency fields)
- `UbsCtrlDate` — for UbsControlDate replacements (date fields)
- `UbsCtrlAddFields` — for `UbsControlProperty` (ucpParam) replacement

## Control Mapping (VB6 → .NET)

| VB6 Control | .NET Control |
|-------------|-------------|
| `UbsControlMoney` (UBSCTRLLibCtl) | `UbsCtrlDecimal` |
| `UbsControlDate` (UBSCTRLLibCtl) | `UbsCtrlDate` or `DateTimePicker` |
| `ActiveTabs.SSActiveTabs` | `TabControl` |
| `MSComctlLib.ListView` | `ListView` |
| `VB.ComboBox` | `ComboBox` (DropDownList style) |
| `VB.CheckBox` | `CheckBox` |
| `VB.TextBox` | `TextBox` |
| `VB.CommandButton` | `Button` |
| `VB.Frame` | `GroupBox` |
| `VB.Label` | `Label` |
| `UbsInfo.ocx` | `UbsCtrlInfo` |
| `UbsControlProperty` (ucpParam) | `UbsCtrlAddFields` or equivalent |
| `UbsChlCtrl` (UbsChannel1) | `IUbsChannel` (via `this.IUbsChannel`) |
