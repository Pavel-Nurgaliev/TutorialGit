# PLAN: Complete .NET File Structure — UbsPsUtPaymentGroupFrm

Level 4 conversion plan artifact. Aligns with `UbsPsContractFrm` partial layout and workspace rules (`panelMain`, `TargetFrameworkVersion` v2.0, `Private=False` UBS refs).

**Companion:** VB6 control names → .NET types and C# field names: `memory-bank/plan-vb6-controls-map.md`.  
**Validation:** Save/check order and UI side effects: `memory-bank/plan-validation-chain.md`.  
**Commission:** `memory-bank/plan-commission-migration.md`.

## 1. Conversion project root (`UbsPsUtPaymentGroupFrm_CP`)

```
UbsPsUtPaymentGroupFrm_CP/
├── .cursor/
│   └── rules/                          # Workspace rules (existing)
├── legacy-form/
│   ├── UtPaymentGroup/                 # VB6 source (.dob, .vbp, …)
│   └── screens/                        # Reference screenshots
├── memory-bank/
│   ├── activeContext.md
│   ├── archive/                        # Post-task archives
│   ├── creative/                       # CREATIVE phase outputs (channel contract, UI)
│   ├── plan-dotnet-file-structure.md   # This file
│   ├── productContext.md
│   ├── progress.md
│   ├── projectbrief.md
│   ├── reflection/
│   ├── style-guide.md
│   ├── systemPatterns.md
│   ├── tasks.md                        # Source of truth for phases
│   └── techContext.md
└── UbsPsUtPaymentGroupFrm/             # .NET WinForms library (target tree)
```

## 2. .NET project folder — target file tree

```
UbsPsUtPaymentGroupFrm/
├── Properties/
│   └── AssemblyInfo.cs
├── UbsPsUtPaymentGroupFrm.sln
├── UbsPsUtPaymentGroupFrm.csproj
├── UbsPsUtPaymentGroupFrm.cs                    # Main partial: ctor, fields, IUbs wiring
├── UbsPsUtPaymentGroupFrm.Constants.cs          # String/command constants (partial)
├── UbsPsUtPaymentGroupFrm.Designer.cs           # InitializeComponent, control fields
├── UbsPsUtPaymentGroupFrm.resx                  # Embedded resources (form)
├── UbsPsUtPaymentGroupFrm.Initialization.cs     # InitDoc, channel init, load/read flows
├── UbsPsUtPaymentGroupFrm.Save.cs               # Save, validation pipeline, attribute save
├── UbsPsUtPaymentGroupFrm.Keys.cs               # KeyDown/KeyPress, tab/enter navigation
├── UbsPsUtPaymentGroupFrm.Commission.cs         # Commission/totals (VB6 CalcSumCommiss)
└── UbsPsUtPaymentGroupFrm.BrowseShell.cs        # Client/receiver lists, grid return, filters
```

**Optional later split** (only if `Save.cs` grows unwieldy):

- `UbsPsUtPaymentGroupFrm.Validation.cs` — `CheckPayment`, `CheckRS`, `CheckTerror`, `Ut_CheckUserBeforeSave`, `CheckLockPassport`, `CheckIPDL` (move from `Save.cs`).

**Do not commit** (local / build): `bin/`, `obj/`, `.vs/`, `Backup/`, `UpgradeLog.htm` — same as sibling PS projects.

## 3. `.csproj` compile block (target)

Order and metadata should mirror `UbsPsContractFrm.csproj`:

| Compile item | SubType | DependentUpon |
|--------------|---------|---------------|
| `UbsPsUtPaymentGroupFrm.cs` | Form | — |
| `UbsPsUtPaymentGroupFrm.Designer.cs` | — | `UbsPsUtPaymentGroupFrm.cs` |
| `UbsPsUtPaymentGroupFrm.Constants.cs` | Form | `UbsPsUtPaymentGroupFrm.cs` |
| `UbsPsUtPaymentGroupFrm.Commission.cs` | Form | `UbsPsUtPaymentGroupFrm.cs` |
| `UbsPsUtPaymentGroupFrm.Initialization.cs` | Form | `UbsPsUtPaymentGroupFrm.cs` |
| `UbsPsUtPaymentGroupFrm.BrowseShell.cs` | Form | `UbsPsUtPaymentGroupFrm.cs` |
| `UbsPsUtPaymentGroupFrm.Save.cs` | Form | `UbsPsUtPaymentGroupFrm.cs` |
| `UbsPsUtPaymentGroupFrm.Keys.cs` | Form | `UbsPsUtPaymentGroupFrm.cs` |
| `Properties\AssemblyInfo.cs` | — | — |

EmbeddedResource: `UbsPsUtPaymentGroupFrm.resx` → `DependentUpon` main `.cs`, `SubType` Designer.

**PostBuildEvent** (match PS sibling): copy `$(TargetDir)$(ProjectName)*` to `\\Develop\ubs_nt\UBS_CLIENT\UBS\FRM\PS` and `C:\ProgramData\UniSAB\UBS\FRM\PS\`.

## 4. Responsibility map (which partial owns what)

### `UbsPsUtPaymentGroupFrm.cs`
- Private fields (command, ids, flags, rate array, guest, etc.)
- Constructor: `m_addCommand`, `InitializeComponent`, `UbsCtrlFieldsSupportCollection.Add` for additional-properties tab, `Ubs_CommandLock`, optional `Ubs_ActionRunBegin`
- `CommandLine` / `ListKey` (host entry points)
- Thin handlers that only delegate: e.g. `btnExit_Click` → `Close`
- `Dispose` stays in Designer unless overridden in main file

### `UbsPsUtPaymentGroupFrm.Constants.cs`
- `LoadResource`, channel command name constants, FLT/shell keys, Russian `Msg*` strings, window keys (`UBS_LIST_COMMON_CLIENT`, `UBS_FLT\PS\LIST_RECEIVER.flt`, etc.)

### `UbsPsUtPaymentGroupFrm.Designer.cs`
- `panelMain` usage only (inherited from `UbsFormBase`; do not re-declare)
- `TabControl` + two tab pages; `GroupBox` payer/recipient; all labels/text/combos/buttons
- `UbsCtrlDecimal` (sum, penalty, commission, total), `UbsCtrlAccount` (corr + client), `UbsCtrlFields`, `UbsCtrlInfo`
- Bottom `TableLayoutPanel` + Save/Exit + info strip (template pattern)
- Client size aligned with legacy (~8505×6435 twips → scale to pixels per CREATIVE/layout rules)

### `UbsPsUtPaymentGroupFrm.Initialization.cs`
- `UbsInit` / post-channel init if used
- `InitDoc`, `InitChannel` equivalent, `ReadContract`, `FindContractbyId`, `FillPurpose`
- `GetBankNameACC`, `clearRecFields`, `clearRecFieldsSend`, `DisableAllFields`, `EnableAllFields`, `NewRecord` (if retained)
- `AccClient_LostFocus`, `txtNomerCardPay` Enter/card read path, `cmbCode` selection changed
- Combo fill from `GroupContract` matrix (`object[row, col]`)

### `UbsPsUtPaymentGroupFrm.Save.cs`
- `btnSave_Click`, `btnSaveAttribute_Click`
- `CheckPayment`, `CheckRS`, `CheckTerror`, `Ut_CheckUserBeforeSave`, `CheckLockPassport`, `CheckIPDL`
- `IsComboBoxItemExists` and similar helpers used only in save/validate path (or move to Initialization if shared)

### `UbsPsUtPaymentGroupFrm.Keys.cs`
- Form `KeyDown` / `KeyPress` (Enter/Esc, tab focus rules)
- `UbsCtrlFields` key handling if exposed by control API

### `UbsPsUtPaymentGroupFrm.Commission.cs`
- `CalcSumCommiss` and `udcSumma` (or equivalent) change handler
- Uses formatting/rounding consistent with VB6 `objFormat.Round` (document in CREATIVE if BCL-only)

### `UbsPsUtPaymentGroupFrm.BrowseShell.cs`
- `btnClient_Click`, `btnListAttributeRecip_Click`
- Return-from-grid / `RetFromGrid` handling (client id, recipient attribute id)
- Any `Ubs_ActionRunBegin` adjustments for list filters (INN, contract id, etc.)

## 5. Assembly references (target set)

Already in template or to align with `UbsPsContractFrm`:

- BCL: System, System.Data, System.Drawing, System.Windows.Forms, System.Xml
- UBS: UbsBase, UbsChannel, UbsCollections (Lib), UbsCtrlInfo, UbsForm, UbsFormBase, UbsInterface
- Controls: UbsCtrlDecimal, UbsCtrlFields, UbsCtrlAccount
- **UbsCtrlDate**: add only if CREATIVE adds a date control (legacy group form has none in main analysis).

## 6. Namespace and visibility

- **Namespace**: `UbsBusiness` (form class), matching PS/OP conversions.
- **RootNamespace / AssemblyName** in `.csproj`: `UbsPsUtPaymentGroupFrm`.
- Partials: one `public partial class UbsPsUtPaymentGroupFrm : UbsFormBase`; Designer may omit `public` (same as Contract).

## 7. CREATIVE phase follow-ups (not file renames — design decisions)

- Exact pixel layout vs `UbsFormTemplate` height rules
- `UbsComboEditControl` → `ComboBox` vs host-specific combo (document in `memory-bank/creative/`)
- `URunScr` / `PsPaymentIncomingReception.vbs` group loop — interop approach in .NET 2.0
- Full channel contract doc: `memory-bank/creative/creative-ubspsutpaymentgroupfrm-channel-contract.md` (planned)

## 8. Technology validation (Level 4 gate)

| Check | Status |
|-------|--------|
| Target | .NET Framework 2.0 class library |
| UBS DLLs | `C:\ProgramData\UniSAB\Assembly\Ubs\` (machine-dependent) |
| Pattern | Matches `UbsPsContractFrm` partial + `panelMain` template |
| Build | Run `msbuild UbsPsUtPaymentGroupFrm.sln` after all partials and Designer are complete |

---

**Next:** `/creative` for layout, channel contract, and script/group-loop decisions; then `/build` to add missing `.cs` files and expand Designer.
