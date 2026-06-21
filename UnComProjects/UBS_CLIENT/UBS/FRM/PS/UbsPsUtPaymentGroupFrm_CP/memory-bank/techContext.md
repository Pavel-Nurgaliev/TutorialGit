# Tech Context: UbsPsUtPaymentGroupFrm

## Target Technology Stack
- **.NET Framework**: v2.0 (no LINQ, no extension methods, no var keyword preferred)
- **Language**: C# 2.0
- **Project type**: Class Library (WinForms UserControl/Form)
- **IDE**: Visual Studio (any version supporting .NET 2.0)

## Assembly References

### Framework Assemblies
- System
- System.Data
- System.Drawing
- System.Windows.Forms
- System.Xml

### UBS Assemblies (Private=False, HintPath=C:\ProgramData\UniSAB\Assembly\Ubs\)
| Assembly | HintPath Subfolder | Purpose |
|----------|-------------------|---------|
| UbsBase | UbsBase.dll | Base UBS types |
| UbsChannel | UbsChannel.dll | Channel communication |
| UbsCollections | Lib\UbsCollections.dll | Collection types |
| UbsCtrlInfo | UbsCtrlInfo.dll | UbsCtrlInfo control |
| UbsCtrlDate | UbsCtrlDate.dll | Date control (if needed) |
| UbsCtrlDecimal | UbsCtrlDecimal.dll | Decimal/money control |
| UbsCtrlFields | UbsCtrlFields.dll | Additional fields control |
| UbsCtrlAccount | UbsCtrlAccount.dll | Account number control |
| UbsForm | UbsForm.dll | Form utilities |
| UbsFormBase | UbsFormBase.dll | Base form class |
| UbsInterface | UbsInterface.dll | IUbs, IUbsChannel interfaces |

## VB6 OCX → .NET Control Mapping

| VB6 Control | VB6 Name | .NET Control | .NET Prefix |
|-------------|----------|-------------|-------------|
| SSActiveTabs | subPayment | TabControl | tab |
| UbsControlMoney | curSumma, curPeny, etc. | UbsCtrlDecimal | udc |
| UbsControlAccount | AccKorr, AccClient | UbsCtrlAccount | uca |
| UbsControlProperty | AddProperties | UbsCtrlFields | ucf |
| UbsInfo | Info | UbsCtrlInfo | uci |
| UbsComboEditControl | cmbPurpose | ComboBox | cmb |
| UbsChannel | UbsChannel | (via base.IUbsChannel) | — |
| VB.CommandButton | btn* | Button | btn |
| VB.TextBox | txt* | TextBox | txt |
| VB.Label | Label* | Label | lbl |
| VB.ComboBox | cmb* | ComboBox | cmb |
| VB.Frame | frm* | GroupBox | grp |

## Project File Structure (target)
```
UbsPsUtPaymentGroupFrm/
├── UbsPsUtPaymentGroupFrm.sln
├── UbsPsUtPaymentGroupFrm.csproj
├── UbsPsUtPaymentGroupFrm.cs              (main form logic)
├── UbsPsUtPaymentGroupFrm.Designer.cs     (InitializeComponent)
├── UbsPsUtPaymentGroupFrm.Constants.cs    (string constants)
├── UbsPsUtPaymentGroupFrm.Initialization.cs (InitDoc, channel reads)
├── UbsPsUtPaymentGroupFrm.Save.cs         (save, validation)
├── UbsPsUtPaymentGroupFrm.Keys.cs         (keyboard handling)
├── UbsPsUtPaymentGroupFrm.resx
└── Properties/
    └── AssemblyInfo.cs
```

## Post-Build Event
```
copy /Y "$(TargetDir)$(TargetName).*" "\\Develop\ubs_nt\UBS_CLIENT\UBS\FRM\PS\"
copy /Y "$(TargetDir)$(TargetName).*" "C:\ProgramData\UniSAB\UBS\FRM\PS\"
```

## VB6 → C# Pattern Mapping

### Channel Calls
```
VB6:  UbsChannel.Run "CommandName", objParamIn, objParamOut
C#:   base.IUbsChannel.Run("CommandName");
      (with ParamIn/ParamOut via base.IUbsChannel.ParamIn["key"] / base.IUbsChannel.ParamOut("key"))
```

### Error Handling
```
VB6:  On Error GoTo ErrorCode / objErr.UbsErrMsg "...", "..."
C#:   try { ... } catch (Exception ex) { this.Ubs_ShowError(ex); }
```

### Array/Variant Mapping
```
VB6:  variant(fieldIndex, recordIndex) — e.g. GroupContract(0, i) = id, GroupContract(1, i) = caption
C#:   object[rowIndex, columnIndex] — e.g. arr[rowIndex, 0] = id, arr[rowIndex, 1] = caption
```
