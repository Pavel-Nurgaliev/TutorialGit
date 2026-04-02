# CREATIVE: `chkNDS_Click`, `chkRate_Click`, `chkSumInCurValue_Click`

**Source:** `legacy-form/Pm_Trade/Pm_Trade_ud.dob` (~L1650–1672).

**Scope:** Contracts tab VAT checkbox drives export visibility via server check; obligation-tab checkboxes mutually control **rate** vs **price in payment currency** fields (`UbsCtrlDecimal`).

---

## 1. Control mapping (VB6 → .NET)

| VB6 | .NET (`UbsPmTradeFrm.Designer.cs`) |
|-----|-------------------------------------|
| `chkNDS` | `chkNDS` (currently `Visible = false` until `UpdateDisplayNDS` is ported) |
| `chkExport` | `chkExport` |
| `chkRate` | `chkRate` |
| `txtRateCurOblig` | `ucdRateCurOblig` |
| `chkSumInCurValue` | `chkSumInCurValue` |
| `txtCostCurOpl` | `ucdCostCurOpl` |

Legacy `IdClient2` (long): second counterparty client id set when contract 2 is filled — **pending** as `m_idClient2` (or equivalent) until `FillControlContract` / contract pickers are wired.

---

## 2. `chkNDS_Click` → `chkNDS_CheckedChanged`

**Legacy:** single call to `UpdateDisplayExport`.

### 2.1 `UpdateDisplayExport` (must exist for this handler)

**Source:** `Pm_Trade_ud.dob` ~L2012–2037.

**Legacy artifact:** the subroutine starts with `MsgBox "пам.3", ...` — treat as **debug noise**; **do not** port to .NET.

**Logic:**

1. If `IdClient2 = 0`: set `IsCheck = False` (skip channel).
2. Else:
   - `ParamIn["ClientId"] = IdClient2`
   - `Run("IsClientNotResident")`
   - `IsNotResident` ← `ParamsOut` / `UbsParam` key **`Result`**
   - `IsCheck = chkNDS.Checked And IsNotResident` (VB used `CBool(chkNDS.Value) And IsNotResident`).
3. If `IsCheck`: `chkExport.Visible = True`.
4. Else: `chkExport.Visible = False`, `chkExport.Checked = False` (VB `Value = 0`).

**Error handling:** legacy `On Error` → `UbsErrMsg "UpdateDisplayExport", ...`. **.NET:** `try` / `catch` → `this.Ubs_ShowError(ex)`.

### 2.2 Channel contract — `IsClientNotResident`

| Call | ParamIn | ParamOut |
|------|---------|----------|
| `IsClientNotResident` | `ClientId` (int/long, second party) | `Result` — use as non-resident flag (legacy assigns to `IsNotResident` and ANDs with NDS) |

Use explicit string literals for command and keys per project style.

### 2.3 Related calls (not part of these three handlers, but same helper)

Legacy also calls `UpdateDisplayExport` from:

- `cmbContractType2_Click` (after `UpdateDisplayNDS`)
- contract-2 close / return from picker (~L3063–3064)
- load path when export flag not pre-set (~L3716)

When implementing `UpdateDisplayExport`, expose it as a **private method** callable from those flows once contract/client state exists.

### 2.4 Sibling: `UpdateDisplayNDS`

Controls **visibility** of `chkNDS` itself (and clears it when hidden). **Out of scope** for this doc’s three handlers except: until `UpdateDisplayNDS` exists, `chkNDS` may stay invisible in the designer; **`chkNDS_CheckedChanged` + `UpdateDisplayExport` remain valid** for when NDS is shown.

---

## 3. `chkRate_Click` → `chkRate_CheckedChanged`

**Legacy (~L1654–1661):**

- If rate checkbox **checked** (`Value = 1`):
  - `txtRateCurOblig.Enabled = True`
  - `chkSumInCurValue.Value = False`
  - `txtCostCurOpl.Enabled = False`
- Else:
  - `txtRateCurOblig.Enabled = False`

**.NET:** same using `ucdRateCurOblig.Enabled`, `chkSumInCurValue.Checked = false`, `ucdCostCurOpl.Enabled`.

**Note:** Programmatically clearing `chkSumInCurValue` runs its `CheckedChanged` handler. Order matches legacy: `chkSumInCurValue` false → else branch disables `txtCostCurOpl`, consistent with rate-on state. If flicker or double logic appears, use a short **`m_suppressObligPriceModeEvent`** (or single guard for both checkboxes) while syncing the peer checkbox.

---

## 4. `chkSumInCurValue_Click` → `chkSumInCurValue_CheckedChanged`

**Legacy (~L1664–1671):**

- If **checked**:
  - `chkRate.Value = False`
  - `txtRateCurOblig.Enabled = False`
  - `txtCostCurOpl.Enabled = True`
- Else:
  - `txtCostCurOpl.Enabled = False`

**.NET:** same with `chkRate.Checked`, `ucdRateCurOblig`, `ucdCostCurOpl`.

**Interaction with §3:** mutual exclusion is symmetric; programmatic `chkRate.Checked = false` triggers `chkRate_CheckedChanged` → else branch keeps rate control disabled — OK.

---

## 5. WinForms wiring

- Prefer **`CheckedChanged`** over `Click` for parity with VB6 toggle semantics.
- Wire all three in `UbsPmTradeFrm.Designer.cs` after controls are fully initialized (post-`FillCombos` is not required for these handlers).

---

## 6. Implementation checklist

- [ ] Add `m_idClient2` (or bind from existing contract-2 field) before `UpdateDisplayExport` can work end-to-end.
- [ ] Implement `UpdateDisplayExport()` with `IUbsChannel.ParamIn` / `Run("IsClientNotResident")` / `UbsParam` for `Result`; **omit** legacy `MsgBox "пам.3"`.
- [ ] `chkNDS_CheckedChanged` → call `UpdateDisplayExport()` inside `try` / `catch`.
- [ ] `chkRate_CheckedChanged` and `chkSumInCurValue_CheckedChanged` → enable/disable `ucdRateCurOblig` and `ucdCostCurOpl`, clear peer checkbox as above; optional suppress flag if events fight.
- [ ] From contract-type / load code paths, call `UpdateDisplayExport` when `IdClient2` or `chkNDS` visibility changes (see §2.3).
- [ ] When porting save/load, mirror legacy `IsNDS` / `IsExport` channel params (`~L2453–2454` area) — document in obligation/contract creative if split.
