# CREATIVE: Obligation Lifecycle — `CallOblig`, Add/Edit/View, Apply, Exit, Delete

**Source:** `Pm_Trade_ud.dob`:
- `CallOblig` (~L4382–4487)
- `cmdAddOblig_Click` (~L2191–2215)
- `cmdEditOblig_Click` (~L2331–2356)
- `cmdApplayOblig_Click` (~L2217–2292)
- `cmdExitOblig_Click` (~L2358–2379)
- `cmdDelOblig_Click` (~L2294–2329)
- `CheckDataOblig` (~L4489–4590)
- `SetResultsListOblig` (~L4592–4632)
- `FillDataOblig` (~L4940–4975)
- `ClearDataOblig` (~L4925–4938)
- `FillArrObject` (~L4827–4842)
- `GetSumOblig`, `GetSumOpl`, `GetMassaGramm`, `GetRateCurOblig` (~L5247–5271)

**Scope:** The full Add / Edit / View / Apply / Cancel / Delete flow for obligations on Tab 3, including validation, ListView ↔ controls sync, obligation parameter bag management, and tab enable/disable.

---

## 1. Control Mapping (VB6 → .NET)

| VB6 | .NET (Designer.cs) | Notes |
|-----|---------------------|-------|
| `cmbNaprTrade` | `cmbTradeDirection` | keys 1=прямая, 2=обратная |
| `cmbCurOblig` | `cmbObligationCurrency` | shared currency list |
| `cmbUnit` | `cmbUnit` | keys 1=грамм, 2=унция |
| `txtCostUnit` | `ucdCostUnit` (UbsCtrlDecimal) | price per unit in obligation currency |
| `txtRateCurOblig` | `ucdRateCurOblig` (UbsCtrlDecimal) | exchange rate |
| `txtCostCurOpl` | `ucdCostCurOpl` (UbsCtrlDecimal) | price per unit in payment currency |
| `txtMassa` | `ucdMass` (UbsCtrlDecimal) | mass |
| `txtMassaGramm` | `ucdMassGramm` (UbsCtrlDecimal) | mass in grams |
| `txtSumOblig` | `ucdSumObligation` (UbsCtrlDecimal) | sum in obligation currency |
| `txtSumOpl` | `ucdSumPayment` (UbsCtrlDecimal) | sum in payment currency |
| `txtDateOpl` | `datePayment` (UbsCtrlDate) | payment date |
| `txtDatePost` | `datePost` (UbsCtrlDate) | delivery date |
| `chkRate` | `chkRate` | fixed rate checkbox |
| `chkSumInCurValue` | `chkSumInCurValue` | manual payment-currency price checkbox |
| `lstViewOblig` | `lvwObligation` | obligation list |
| `lstViewObject` | `lvwObject` | objects list (sub-tab 2) |
| `lblObligInfo2` | `lblObligationInfo2` | summary label on objects sub-tab |
| `cmdAddOblig` | `cmdAddObligation` | |
| `cmdEditOblig` | `cmdEditObligation` | |
| `cmdDelOblig` | `cmdDelObligation` | |
| `cmdApplayOblig` | `cmdApplayObligation` | |
| `cmdExitOblig` | `cmdExitObligation` | |
| `cmdAddObject` | `cmdAddObject` | |
| `cmdDelObject` | `cmdDelObject` | |
| `SSTabs` | `tabControl` | main 6-tab |
| `SSTabs1` | `tabControlOblig` | obligation detail tabs |
| `SSTabs.Tabs(1)–(6)` | `tabPage1`–`tabPage6` | 1-based → 0-based |
| `SSTabs1.Tabs(1)` | `tabPageOblig1` | detail |
| `SSTabs1.Tabs(2)` | `tabPageOblig2` | objects |
| `objParamOblig` | `m_paramOblig` (UbsParam) | obligation parameter bag |
| `bNeedSendOblig` | `m_needSendOblig` | |
| `StrNumInPart` | `m_strNumInPart` (new field) | "part.number" key for current obligation |
| `strNaprTrade` | `m_strNaprTrade` (new field) | original direction before edit |
| `blnAddObject` | `m_blnAddObject` (new field) | flag for object-adding mode |

---

## 2. New Fields Needed

| Field | Type | Purpose |
|-------|------|---------|
| `m_strNumInPart` | `string` | Current obligation's "part.number" key (e.g. "1.3") |
| `m_strNaprTrade` | `string` | Saved direction text at start of Edit (for direction-change detection in Apply) |
| `m_blnAddObject` | `bool` | Set true in Add flow; used by `FillDataObject` to check cross-obligation duplicate objects |
| `m_sType` | `string` | Current obligation operation: `"Add"` or `"Edit"` (form-level so Apply can read it) |

---

## 3. State Machine

```
                 ┌──────────┐
        ┌────────│  BROWSE   │◄────────────────────┐
        │        │ (tab 2)   │                     │
        │        └────┬──────┘                     │
        │             │                            │
   cmdAdd        cmdEdit        cmdDelete          │
        │             │              │             │
        ▼             ▼              ▼             │
  ┌──────────┐ ┌──────────┐  ┌──────────┐         │
  │ ADD mode │ │EDIT mode │  │ delete   │─────────┤
  │  (tab3)  │ │  (tab3)  │  │ + return │         │
  └────┬─────┘ └────┬─────┘  └──────────┘         │
       │             │                             │
       └──────┬──────┘                             │
              │                                    │
         ┌────▼─────┐                              │
         │ EDITING  │  (tabs 1,2,4,5,6 disabled)   │
         │ tab3 open│                              │
         └──┬───┬───┘                              │
            │   │                                  │
       Apply│   │Cancel                            │
            │   │                                  │
            ▼   └──────────────────────────────────┘
     ┌──────────┐
     │validate  │
     │ + commit │──── success ─────────────────────┘
     │ to list  │
     └──────────┘
```

Also: **View mode** — when `tabControl_Selecting` navigates to tab 3 and `m_blnAddOblig == false && m_blnEditOblig == false`, `CallOblig("Edit")` is invoked in **read-only** mode (Apply/Exit hidden, panels disabled). This is already handled in `ApplyDataTabUiOnSelecting`.

---

## 4. `cmdAddObligation_Click`

**Legacy (~L2191–2215):**

1. `m_blnAddOblig = true`
2. Enable tab 3 (`tabPage3.Enabled = true`)
3. **Disable** all other tabs: tab 1, 2, 4, 5, 6
4. `m_sType = "Add"`
5. `CallOblig("Add")`
6. If `chkComposit.Checked && m_command == CmdEdit`: `cmbTradeDirection.Enabled = true`

**.NET tab disable approach:** Since WinForms `TabPage` has no built-in `.Enabled` that prevents selection, use the `tabControl_Selecting` event to cancel navigation away from tab 3 during editing. Set a flag `m_obligEditingMode = true` and in `tabControl_Selecting`: if this flag is set and `e.TabPage != tabPage3`, cancel. **Alternative:** physically remove other tab pages during editing (more aggressive). **Decision:** use the cancel-in-Selecting approach — it's simpler and does not risk losing tab page ordering.

---

## 5. `cmdEditObligation_Click`

**Legacy (~L2331–2356):**

1. If `lvwObligation.Items.Count == 0`: return
2. `m_blnEditOblig = true`
3. Enable tab 3, disable all others (same as Add)
4. `m_sType = "Edit"`, `m_blnAddObject = false`
5. `CallOblig("Edit")`
6. If `chkComposit.Checked && m_command == CmdEdit`: `cmbTradeDirection.Enabled = false` (direction locked during edit)

---

## 6. `CallOblig(string sType)` — Fill Obligation Detail Controls

### 6.1 Add mode (`sType == "Add"`)

1. `strNapr = "прямая"` (default direction)
2. `m_strNumInPart = ""`
3. **Cash trades** (`m_kindTrade == 0`):
   - `datePayment.DateValue = dateTrade.DateValue`
   - `datePost.DateValue = dateTrade.DateValue`
   - `datePayment.Enabled = false`, `datePost.Enabled = false`
4. **Other** (`m_kindTrade != 0`):
   - `datePayment.DateValue = DateTime.Today`
   - `datePost.DateValue = DateTime.Today`
5. Clear values: `ucdCostUnit = 0`, `ucdMass = 0`, `ucdMassGramm = 0`, `ucdSumObligation = 0`, `ucdSumPayment = 0`
6. Update `lblObligationInfo2` caption: `"Цена: " + ucdCostUnit.Text + "   Масса: " + ucdMass.Text + "   Сумма: " + ucdSumPayment.Text`
7. `chkRate.Checked = false`
8. **`ClearDataOblig()`**: clear `lvwObject.Items`

### 6.2 Edit mode (`sType == "Edit"`)

1. If `lvwObligation.Items.Count == 0`: return
2. **Cash trades**: `datePayment.Enabled = (m_kindTrade != 0)`, `datePost.Enabled = (m_kindTrade != 0)`
3. Read from selected `lvwObligation` item:

| SubItem index | Field | .NET target |
|---------------|-------|-------------|
| 0 (Text) | Direction | `strNapr` |
| 1 | StrNumInPart | `m_strNumInPart` |
| 2 | DateOpl | `datePayment.DateValue` |
| 3 | DatePost | `datePost.DateValue` |
| 4 | CostUnit | `ucdCostUnit.Text` |
| 5 | Massa | `ucdMass.Text` + call `GetMassaGramm()` |
| 6 | SumOblig | `ucdSumObligation.Text` |
| 7 | CurrencyId | → set `cmbObligationCurrency` by key |
| 8 | Rate | `ucdRateCurOblig.Text` |
| 9 | Unit text | → set `cmbUnit` by text ("грамм"→index 0, "унция"→index 1) |
| 10 | RateFlag | `chkRate.Checked = (value == "1")` |

4. Compute `costCurOpl = Round(ucdCostUnit * ucdRateCurOblig, 4)` → `ucdCostCurOpl.Text`
5. Compute `sumOpl = Round(ucdSumObligation * ucdRateCurOblig, 2)` → `ucdSumPayment.Text`
6. Save `m_strNaprTrade = strNapr` (for direction-change detection in Apply)
7. **`FillDataOblig()`**: load objects from `m_paramOblig["Object" + m_strNumInPart]`

### 6.3 Common tail (both modes)

1. Set `cmbTradeDirection`:
   - If `strNapr` is empty and `chkComposit.Checked`: `cmbTradeDirection.SelectedIndex = -1`
   - Else: set `cmbTradeDirection` by text (match "прямая" or "обратная")
2. If `m_blnAddOblig || m_blnEditOblig` (not view mode):
   - Navigate to tab 3: `tabControl.SelectedTab = tabPage3` (note: this may trigger `tabControl_Selecting`; must handle `m_suppressMainTabSelecting` or let the Selecting handler pass when `m_obligEditingMode` is set)
   - Select `tabPageOblig1` on `tabControlOblig`
   - Focus: if `cmbTradeDirection.Enabled` → focus it; else focus `cmbObligationCurrency`

---

## 7. `cmdApplayObligation_Click` (Apply / Confirm)

**Legacy (~L2217–2292):**

1. **Validate:** `if (!CheckDataOblig()) return;`
2. Reset flags: `m_blnAddOblig = false`, `m_blnEditOblig = false`
3. Select `tabPageOblig1` on inner tab
4. **Disable** tab 3, **enable** all other tabs (reverse of Add/Edit entry)
5. Navigate to tab 2 (obligations list)

### 7.1 Part number assignment

**Add:**
```
NumPart = cmbTradeDirection.SelectedKey  // 1 or 2
if NumPart == 1: m_maxNumPart1++; NumInPart = m_maxNumPart1
if NumPart == 2: m_maxNumPart2++; NumInPart = m_maxNumPart2
m_strNumInPart = NumPart + "." + NumInPart
m_paramOblig["Add" + m_strNumInPart] = true
```

**Edit:**
```
if direction changed (cmbTradeDirection.Text != m_strNaprTrade):
    NumPart = cmbTradeDirection.SelectedKey
    increment m_maxNumPart1 or m_maxNumPart2
    m_strNumInPart = NumPart + "." + NumInPart
else:
    m_strNumInPart = lvwObligation.SelectedItems[0].SubItems[1].Text  // keep original
m_paramOblig["Edit" + m_strNumInPart] = true
```

### 7.2 Save objects array

```
FillArrObject(out objectArray)
m_paramOblig["Object" + m_strNumInPart] = objectArray
```

`FillArrObject`: build `object[1, count]` where `[0, i]` = object ID from `lvwObject.Items[i].SubItems[6].Text`.

**VB6 note:** `ReDim varArr(0, count-1)` → VB shape `(0, n-1)` → **one row, N columns**. In .NET this transposes to `object[count, 1]` or keep as `object[1, count]` if server expects VB-style. **Decision:** match server expectation — use `object[1, count]` (single-row, one column per object ID) since the server reads this array with VB-compatible indexing `(0, i)`.

### 7.3 Update ListView

`SetResultsListOblig(sType)`:

**Add:** add new `ListViewItem` with subitems:
```
[0] Text = cmbTradeDirection.Text
[1] = m_strNumInPart
[2] = datePayment.DateValue
[3] = datePost.DateValue
[4] = ucdCostUnit decimal value
[5] = ucdMass decimal value
[6] = ucdSumObligation decimal value
[7] = cmbObligationCurrency selected key
[8] = ucdRateCurOblig decimal value
[9] = cmbUnit.Text
[10] = chkRate.Checked ? "1" : "0"
```

**Edit:** update the currently selected item's subitems in place.

### 7.4 Set dirty flag, focus

```
m_needSendOblig = true
cmdAddObligation.Focus()
```

---

## 8. `cmdExitObligation_Click` (Cancel)

**Legacy (~L2358–2379):**

1. `m_blnAddOblig = false`, `m_blnEditOblig = false`
2. Select `tabPageOblig1` on inner tab
3. Disable tab 3, enable all other tabs
4. Navigate to tab 2

No data changes — user cancelled.

---

## 9. `cmdDelObligation_Click` (Delete)

**Legacy (~L2294–2329):**

1. If `lvwObligation.Items.Count == 0`: return (or check `SelectedItems`)
2. `strNumInPartDel = selectedItem.SubItems[1].Text`
3. Remove object params: `m_paramOblig.Remove("Object" + strNumInPartDel)`
4. Mark for server deletion: `m_paramOblig["Delete" + strNumInPartDel] = true`
5. Remove item from `lvwObligation`
6. `m_needSendOblig = true`
7. `tabPage3.Enabled = (lvwObligation.Items.Count > 0)`
8. Focus `cmdAddObligation`

---

## 10. `CheckDataOblig()` — Obligation Validation

Returns `true` if all fields valid, `false` otherwise. All messages use `MessageBoxIcon.Error` + `MsgTitleValidationProps`.

| # | Check | Message | Focus |
|---|-------|---------|-------|
| 1 | `cmbTradeDirection.SelectedIndex == -1` | "Не указано направление." | — |
| 2 | `ucdCostUnit == 0` | "Не указана цена за единицу." | `ucdCostUnit` |
| 3 | `datePost` missing/invalid or `== 2222-01-01` | "Не указана дата поставки." | `datePost` |
| 4 | `datePost < dateTrade` | "Дата поставки не может быть меньше даты сделки." | `datePost` |
| 5 | `datePayment` missing/invalid or `== 2222-01-01` | "Не указана дата оплаты." | `datePayment` |
| 6 | `datePayment < dateTrade` | "Дата оплаты не может быть меньше даты сделки." | `datePayment` |
| 7 | `ucdRateCurOblig == 0` | "Не указан курс пересчета" | `ucdRateCurOblig` |
| 8 | Kind == "обезличенная" (key 1) AND `ucdMass == 0` | "Не указана масса металла." | `ucdMass` |
| 9 | Kind == "физическая" (key 2) AND `lvwObject.Items.Count == 0` | "Не заполнен список объектов по обязательству" | `cmdAddObject` on `tabPageOblig2` |

---

## 11. Calculation Methods

Already partially present in the codebase (`GetRate_CB`, `GetRateForPM`). These are obligation-level recalculations:

### `GetSumOblig()`
```csharp
ucdSumObligation.Text = Math.Round(ucdCostUnit.Value * ucdMass.Value, 2).ToString();
```

### `GetSumOpl()`
```csharp
if (chkSumInCurValue.Checked)
    ucdSumPayment.Text = Math.Round(ucdCostCurOpl.Value * ucdMass.Value, 2).ToString();
else
    ucdSumPayment.Text = Math.Round(ucdSumObligation.Value * ucdRateCurOblig.Value, 2).ToString();
```

### `GetMassaGramm()`
```csharp
int unitKey;
bool isOunce = UbsPmTradeComboUtil.TryGetSelectedKey(cmbUnit, out unitKey) && unitKey == 2;
decimal factor = isOunce ? 31.1035m : 1m;
// Legacy uses strCB == "A99" for rounding; we may need m_strCB field or simplify
ucdMassGramm.Text = Math.Round(ucdMass.Value * factor, 1).ToString();
```

### `GetRateCurOblig()`
```csharp
if (ucdCostUnit.Value > 0)
    ucdRateCurOblig.Text = Math.Round(ucdCostCurOpl.Value / (double)ucdCostUnit.Value, 10).ToString();
else
    ucdRateCurOblig.Text = "0";
```

---

## 12. `FillDataOblig()` — Load Objects into ListView for Edit

When editing an obligation, objects are loaded from `m_paramOblig["Object" + m_strNumInPart]`:

1. If key exists and value is an array:
   - Clear `lvwObject.Items`
   - `ucdMass = 0`
   - For each object ID in the array: call `FillDataObject(idObject)` which calls `GetObjectPM` channel command and adds a row to `lvwObject`
2. If no objects AND kind is "физическая": clear mass, sum, update `lblObligationInfo2`

---

## 13. `ClearDataOblig()` — Clear Objects

Simply: `lvwObject.Items.Clear()`

---

## 14. Tab Enable/Disable Strategy

**Problem:** WinForms `TabPage.Enabled` does not prevent selection by clicking the tab header. Legacy VB6 uses `SSTabs.Tabs(n).Enabled = False` which grays out and prevents selection.

**Options:**

| Option | Mechanism | Pros | Cons |
|--------|-----------|------|------|
| **A — Cancel in Selecting** | Set `m_obligEditingMode` flag; in `tabControl_Selecting`, cancel if flag set and target != tabPage3 | Simple, no tab page removal | Tab headers still look clickable |
| **B — Remove/re-insert pages** | Remove all pages except tabPage3; re-insert on exit | Tabs truly hidden | Must track insertion indices; fragile |
| **C — Disable + cancel** | `tabPage.Enabled = false` for graying content + cancel in Selecting for navigation block | Visual feedback (grayed) + block | Requires both mechanisms |

**Decision:** **Option A** — cancel in `tabControl_Selecting`. This is consistent with the existing `m_suppressMainTabSelecting` pattern and requires minimal changes. The existing `tabControl_Selecting` handler already has complex logic; add `m_obligEditingMode` check at the top as an early return.

During obligation editing:
```csharp
if (m_obligEditingMode && e.TabPage != tabPage3)
{
    e.Cancel = true;
    return;
}
```

Set `m_obligEditingMode = true` in `cmdAddObligation_Click` / `cmdEditObligation_Click`.
Set `m_obligEditingMode = false` in `cmdApplayObligation_Click` / `cmdExitObligation_Click`.

---

## 15. Channel Contract

| Call | ParamIn | ParamOut | Used In |
|------|---------|----------|---------|
| `GetObjectPM` | `ID_OBJECT` | `ДанныеОбъекта` (2D array) | `FillDataObject` (object picker → objects list) |

`CallOblig` itself makes **no** channel calls. All channel interaction is in:
- `FillDataObject` → `GetObjectPM` (called from `FillDataOblig` during Edit to reload objects)
- `CheckDataOblig` → no channel calls (pure validation)
- `SetResultsListOblig` → no channel calls (pure ListView update)

---

## 16. User-Facing Strings (Constants.cs)

Add to `UbsPmTradeFrm.Constants.cs`:

```
MsgNoDirection = "Не указано направление."
MsgNoCostUnit = "Не указана цена за единицу."
MsgNoDeliveryDate = "Не указана дата поставки."
MsgDeliveryDateBeforeTrade = "Дата поставки не может быть меньше даты сделки."
MsgNoPaymentDate = "Не указана дата оплаты."
MsgPaymentDateBeforeTrade = "Дата оплаты не может быть меньше даты сделки."
MsgNoExchangeRate = "Не указан курс пересчета"
MsgNoMass = "Не указана масса металла."
MsgNoObjects = "Не заполнен список объектов по обязательству"
```

---

## 17. Implementation Order

1. **Fields:** Add `m_strNumInPart`, `m_strNaprTrade`, `m_blnAddObject`, `m_sType`, `m_obligEditingMode`
2. **Constants:** Add validation messages to `Constants.cs`
3. **Tab guard update:** Add `m_obligEditingMode` check to `tabControl_Selecting`
4. **Calculation methods:** `GetSumOblig`, `GetSumOpl`, `GetMassaGramm`, `GetRateCurOblig`
5. **`CallOblig`:** Add and Edit branches + common tail
6. **`ClearDataOblig` / `FillDataOblig`:** Object list management
7. **`CheckDataOblig`:** Validation
8. **`SetResultsListOblig`:** ListView add/update
9. **`FillArrObject`:** Build objects array for param bag
10. **Button handlers:** `cmdAddObligation_Click`, `cmdEditObligation_Click`, `cmdApplayObligation_Click`, `cmdExitObligation_Click`, `cmdDelObligation_Click`
11. **Wire events** in Designer.cs for the 5 button Click handlers

---

## 18. Implementation Checklist

- [ ] Add new fields: `m_strNumInPart`, `m_strNaprTrade`, `m_blnAddObject`, `m_sType`, `m_obligEditingMode`
- [ ] Add validation constants to `Constants.cs`
- [ ] Update `tabControl_Selecting` with `m_obligEditingMode` guard
- [ ] Implement `GetSumOblig()`, `GetSumOpl()`, `GetMassaGramm()`, `GetRateCurOblig()`
- [ ] Implement `CallOblig(string sType)` — Add branch
- [ ] Implement `CallOblig(string sType)` — Edit branch (read from ListView, fill controls)
- [ ] Implement `CallOblig(string sType)` — common tail (direction combo, tab navigation)
- [ ] Implement `ClearDataOblig()` → clear `lvwObject`
- [ ] Implement `FillDataOblig()` → load objects from `m_paramOblig`
- [ ] Implement `CheckDataOblig()` → 9 validation checks
- [ ] Implement `SetResultsListOblig(string sType)` → add/update ListView item
- [ ] Implement `FillArrObject()` → build `object[1, count]` from `lvwObject`
- [ ] Implement `cmdAddObligation_Click` — set flags, disable tabs, call `CallOblig("Add")`
- [ ] Implement `cmdEditObligation_Click` — set flags, disable tabs, call `CallOblig("Edit")`
- [ ] Implement `cmdApplayObligation_Click` — validate, assign part number, save objects, update list, re-enable tabs
- [ ] Implement `cmdExitObligation_Click` — reset flags, re-enable tabs, navigate to tab 2
- [ ] Implement `cmdDelObligation_Click` — remove from list + param bag, set dirty
- [ ] Wire 5 button Click events in Designer.cs
- [ ] Verify build: 0 new errors
