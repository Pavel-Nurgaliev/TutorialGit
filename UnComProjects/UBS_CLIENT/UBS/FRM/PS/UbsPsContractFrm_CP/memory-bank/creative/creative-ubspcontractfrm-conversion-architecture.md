# CREATIVE: UbsPsContractFrm — Conversion architecture

**Scope:** IUbs entry mapping, DDX strategy, **control naming**, tab order, implementation guidelines for VB6 `Contract.dob` → `UbsPsContractFrm`.  
**Date:** 2026-04-02 (§7 prefix convention: 2026-04-02)

---

## CREATIVE PHASE START: Architecture

### 1. Problem

Legacy **Contract** is a `UserDocument` with `UBSChild_ParamInfo`, `UbsChannel.Run`, and `UbsDDX`. The .NET form must:

- Receive **ADD/EDIT** and **record id** the way the UBS shell expects (`CommandLine` / `ListKey` or equivalent).
- Replace **DDX** with explicit param build (see §6).
- Name **WinForms controls** consistently (§7).
- Keep **channel param keys** identical to legacy (Cyrillic / `TXTCODE`, etc.).

### 2. Shell → form mapping (high level)

| Legacy | .NET (typical) |
|--------|----------------|
| `InitParamForm` | `CommandLine` (mode) + `ListKey` (id) or dedicated `Ubs_AddName` handler — **verify** host |
| `RetFromGrid` | Same pattern as other UBS forms: one or more `Ubs_AddName` delegates; see `creative-phase-b-main-tab.md` §6 |

### 3. Init order

**Hybrid (Phase B decision):** `ListKey` calls `InitDoc()` when `m_command` / id are set; `Load` calls `InitDoc()` only if `!m_isInitialized`. See `creative-phase-b-main-tab.md` §3.

### 4. Partial classes

- `UbsPsContractFrm.Constants.cs` — commands, param keys, messages.
- `UbsPsContractFrm.Initialization.cs` — initialization channel flow.
- `UbsPsContractFrm.cs` — ctor, IUbs handlers, button handlers, `m_addFields`.

### 5. Namespace / assembly

- Class: `UbsBusiness.UbsPsContractFrm` : `UbsFormBase`.
- Assembly / root namespace: `UbsPsContractFrm` (csproj).

### 6. Decision — DDX

**Selected:** **D2** — explicit change matrix or `List` of `(key, value)` for save, with **InitDoc** setting initial snapshot for optional dirty-check; **minimum** for first milestone: **always send full** `ParamIn` set as legacy does after `UbsDDX.UpdateData True` / `MembersValue` (simpler, less risk).

**Rationale:** Legacy `btnSave` uses `UbsDDX.MembersValue RSChange` then loops `RSChange`; parity can be achieved by **building the same structure** from controls without storing OCX state.

---

### 7. Control naming — prefix-based (Hungarian-style)

**Rules (this project)**

| Rule | Apply |
|------|--------|
| **Casing** | **camelCase** after the prefix (e.g. `btnSave`, `txtContractCode`). |
| **Prefixes** | Standard WinForms Hungarian: `btn`, `lbl`, `txt`, `cmb`, `chk`, `grp`, `tab`, `tabPage`, `pnl`, `tbl`. |
| **UBS controls** | `ucd` = `UbsCtrlDate`, `udc` = `UbsCtrlDecimal`, `uca` = `UbsCtrlAccount`, `uci` = `UbsCtrlInfo`, `ucf` = `UbsCtrlFields`. |
| **Channel / DDX** | **Parameter keys** (`TXTCODE`, Cyrillic add-field names) stay **byte-for-byte** as the server expects; only **C# field names** use prefixes. |
| **Events** | `btnSave_Click`, `btnExit_Click`, … (prefix + role + `_Click`). |

**Tab shell**

| Legacy (VB6) | .NET field name | WinForms type |
|--------------|-----------------|---------------|
| `subContract` | `tabContract` | `TabControl` |
| `TabMain` | `tabPageMain` | `TabPage` |
| `TabCom` | `tabPageCommission` | `TabPage` |
| `TabAdd` | `tabPageAddFields` | `TabPage` |

**Footer / template**

| Role | .NET field name | Type |
|------|-----------------|------|
| Bottom actions layout | `tblActions` | `TableLayoutPanel` |
| Save | `btnSave` | `Button` |
| Exit | `btnExit` | `Button` |
| Info line | `uciContract` | `UbsCtrlInfo` |

**Main tab — fields**

| Legacy | .NET field name | .NET type |
|--------|-----------------|-----------|
| `txtCode` | `txtContractCode` | `TextBox` |
| `cmbNameOI` | `cmbExecutor` | `ComboBox` |
| `txtNum` | `txtContractNumber` | `TextBox` |
| `dateContract` | `ucdContract` | `UbsCtrlDate` |
| `cmbStatus` | `cmbContractStatus` | `ComboBox` |
| `txtDateClose` | `ucdContractClose` | `UbsCtrlDate` |
| `txtKind` | `txtPaymentKindCode` | `TextBox` |
| `txtNoteKind` | `txtPaymentKindComment` | `TextBox` |
| `btnKind` | `btnPaymentKind` | `Button` |
| `txtComment` | `txtComment` | `TextBox` |
| `frmRec` | `grpRecipient` | `GroupBox` |
| `txtClient` | `txtRecipientClient` | `TextBox` |
| `btnClient` | `btnRecipientClient` | `Button` |
| `btnDelClient` | `btnRecipientClientClear` | `Button` |
| `txtBic` | `udcRecipientBik` | `UbsCtrlDecimal` |
| `AccKorr` | `ucaCorrespondentAccount` | `UbsCtrlAccount` |
| `txtNameBank` | `txtBankName` | `TextBox` |
| `AccClient` | `ucaRecipientAccount` | `UbsCtrlAccount` |
| `txtINN` | `udcRecipientInn` | `UbsCtrlDecimal` |
| `txtAdress` | `txtRecipientAddress` | `TextBox` |
| `btnAcc` | `btnRecipientAccount` | `Button` |
| *(arbitrary title)* | `lblArbitraryContract` | `Label` |
| Main scroll host | `pnlMainScroll` | `Panel` |
| Labels | `lblContractCode`, `lblExecutor`, … | `Label` |

**Commission tab**

| Legacy | .NET field name | .NET type |
|--------|-----------------|-----------|
| `frmCommis` | `grpPayerCommission` | `GroupBox` |
| `cboTypeSend` | `cmbPayerCommissionType` | `ComboBox` |
| `curPercentSend` | `udcPayerCommissionPercent` | `UbsCtrlDecimal` |
| `frmCommissR` | `grpRecipientCommission` | `GroupBox` |
| `cboTypeRec` | `cmbRecipientCommissionType` | `ComboBox` |
| `curPercentRec` | `udcRecipientCommissionPercent` | `UbsCtrlDecimal` |
| `cb_comissType` | `chkRecipientCommissionReverse` | `CheckBox` |

**Add-fields tab**

| Legacy | .NET field name | .NET type |
|--------|-----------------|-----------|
| `AddProperties` | `ucfAdditionalFields` | `UbsCtrlFields` (+ `UbsCtrlFieldsSupportCollection`) |

---

### 8. Control type mapping (VB6 OCX → .NET control type)

| VB6 | .NET type |
|-----|------|
| `SSActiveTabs` / `SSActiveTabPanel` | `TabControl` / `TabPage` |
| `UbsControlMoney` | `UbsCtrlDecimal` |
| `UbsControlDate` | `UbsCtrlDate` |
| `UbsControlAccount` | `UbsCtrlAccount` |
| `UbsControlProperty` | `UbsCtrlFields` + `UbsCtrlFieldsSupportCollection` |
| `UbsInfo` | `UbsCtrlInfo` |
| `UbsDDXControl` | **D2** / manual binding |

---

### 9. Tab structure (order)

Legacy order in source: `TabAdd`, `TabCom`, `TabMain`. **Runtime** tab indices in VB use `subContract.Tabs(1..3)` for navigation — **do not** assume Designer order equals runtime index; **map by caption** after loading strings or **legacy screens**.

**Recommended:** Use **`tabContract`** with pages **`tabPageMain`**, **`tabPageCommission`**, **`tabPageAddFields`**; set **`Text`** from constants or Designer to match screenshots.

---

### 10. Implementation guidelines

1. Add **Constants** partial before large logic: commands, `STRCOMMAND` values, param keys, messages.
2. Implement **InitDoc** in a **partial class** file when line count grows (`UbsPsContractFrm.Initialization.cs`).
3. **Add-fields:** Wire `UbsCtrlFieldsSupportCollection` with key **`"Доп. поля"`** (constant `AddFieldsSupportKey`) — tab caption may differ.
4. **Keyboard:** Port `UserDocument_KeyPress` tab order **after** main save path works; event handlers **`btnSave_Click`**, **`btnExit_Click`**, etc.

---

## CREATIVE PHASE END

### Verification

- [x] Multiple options for IUbs and DDX (3+2) — see Phase B creative for Init/mapping detail.
- [x] Recommended approach documented.
- [x] Control mapping aligned with `designer-rules.mdc`.
- [x] **Control naming:** prefix-based (§7); channel keys unchanged.
