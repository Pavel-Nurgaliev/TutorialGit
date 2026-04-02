# CREATIVE: `cmbKindSupplyTrade_click` → `cmbKindSupplyTrade_SelectedIndexChanged`

**Source:** `legacy-form/Pm_Trade/Pm_Trade_ud.dob` (`Private Sub cmbKindSupplyTrade_click`, ~L2040–2068).  
**Related:** `DelOblig` (~L2075–2106) called from this handler.

**Scope:** Toggling **вид поставки по сделке** clears storage fields, shows or hides **«Поставка»** and obligation **«Объекты»** sub-tabs, clears all obligations (with EDIT «first change arms, second clears»), then refreshes **НДС** / **экспорт** visibility.

---

## 1. Control mapping (VB6 → .NET)

| VB6 | .NET |
|-----|------|
| `cmbKindSupplyTrade` | `cmbKindSupplyTrade` (`DropDownList`, keys **1** = «обезличенная», **2** = «физическая» — `FillCombos`) |
| `txtStorageCode` / `txtStorageName` | `txtStorageCode` / `txtStorageName` |
| `SSTabs.Tabs(4)` («Поставка») | `tabPage4` |
| `SSTabs1.Tabs(2)` («Объекты») | `tabPageOblig2` |
| `SSTabs.Tabs(3)` («Данные») | `tabPage3` |
| `lstViewOblig` | `lvwObligation` |
| `objParamOblig` | `m_paramOblig` + delete markers |
| `bNeedSendOblig` | `m_needSendOblig` |
| `strRunParam` | `m_command` (`CmdEdit`) |
| `blnClearOblig` | **`m_armedClearObligationsOnKindChange`** (or equivalent) |

Prefer **`TryGetSelectedKey`** / key **1** / **2** instead of string compares where possible; legacy used **`Text = "обезличенная"`** / **`"физическая"`** — display strings match `FillCombos`.

---

## 2. Channel contract (inside this click handler)

| Call | ParamIn | ParamOut |
|------|---------|----------|
| *(none directly)* | — | — |

**Indirect:** **`UpdateDisplayNDS()`** and **`UpdateDisplayExport()`** (already on form) perform their own **`Run`** calls.

---

## 3. Behaviour (verbatim logic)

### 3.1 Branch by kind of supply

**«обезличенная»** (key **1**):

1. `txtStorageCode.Text = ""`, `txtStorageName.Text = ""`.
2. **Hide** main tab **«Поставка»**: legacy `SSTabs.Tabs(4).Visible = False`.  
   **.NET:** use **`tabControl.TabPages.Remove(tabPage4)`** / re-**`Insert`** when showing again, or **`tabPage4.Enabled = false`** + exclude from navigation — **pick one pattern** and align with `tabControl_Selecting` so users cannot land on a dead tab index. Document chosen approach in code comments only if non-obvious.
3. If nested **«Объекты»** is visible: hide it — legacy `SSTabs1.Tabs(2).Visible = False`.  
   **.NET:** **`tabControlOblig.TabPages.Remove(tabPageOblig2)`** or keep page but **`tabPageOblig2.Enabled = false`** / hide; if only one page remains, **`SelectedTab = tabPageOblig1`**.

**«физическая»** (key **2**):

1. Show **`tabPage4`** (`SSTabs.Tabs(4).Visible = True`).
2. Show **`tabPageOblig2`** (`SSTabs1.Tabs(2).Visible = True`).

### 3.2 `DelOblig` — clear all obligations

When **`IsExistOblig()`** (list has items):

1. For each row, read **«Номер в части»** from **`SubItems(1)`** (same column index as elsewhere).
2. For each `StrNumInPartDel`: **`objParamOblig.ClearParameter("Object" + StrNumInPartDel)`** and **`objParamOblig.Parameter("Delete" & StrNumInPartDel) = True`**.
3. **`lstViewOblig.ListItems.Clear`**, refresh list.
4. **`bNeedSendOblig = True`**.

**.NET:** clear **`lvwObligation.Items`**, mirror **`m_paramOblig`** removals + delete flags (same string keys as legacy), set **`m_needSendOblig = true`**. Reset **`m_maxNumPart1` / `m_maxNumPart2`** to **0** if those counters are used for composite / part logic.

### 3.3 When to call `DelOblig`

- **`m_command != CmdEdit` (ADD):** always **`DelOblig()`** after UI branch.
- **EDIT:**
  - If **`m_armedClearObligationsOnKindChange`** is **true**: **`DelOblig()`**; if **`tabPage3.Enabled`** then set **`tabPage3.Enabled = false`** (legacy: disable «Данные» after wipe).
  - Else: set **`m_armedClearObligationsOnKindChange = true`** only (no delete yet — first kind change after load preserves obligations).

Initialize **`m_armedClearObligationsOnKindChange`** appropriately on **EDIT** load (e.g. **false** until first user change), consistent with **`blnClearContr*`** pattern.

### 3.4 After obligation logic

- **`UpdateDisplayNDS()`**
- **`UpdateDisplayExport()`**

---

## 4. WinForms event

- Use **`SelectedIndexChanged`** (or **`SelectionChangeCommitted`**) on **`DropDownList`**; not **`Click`**.
- During **`FillCombos`** / load, use **`m_suppressKindSupplyEvent`** (or reuse a broader suppress flag) so programmatic set does not fire **`DelOblig`** or double **`UpdateDisplay`**.

---

## 5. Error handling

Legacy: **`On Error` → `UbsErrMsg "cmbKindSupplyTrade_click", ...`**.

**.NET:** **`try` / `catch` → `this.Ubs_ShowError(ex)`**.

---

## 6. Cross-links

- **`UpdateDisplayNDS`** depends on kind **физическая** + contract codes + client legal flag — already implemented; changing kind will re-evaluate **НДС** visibility.
- **`tabControl_Selecting`** already applies mass/unit rules when entering **«Обязательства»**; kind change should stay consistent (optional: call shared **`ApplyKindSupplyMassUnitForObligationsTab()`** from this handler when selection changes).

---

## 7. Implementation checklist

- [ ] Implement **`DelOblig()`** (or **`ClearAllObligationsFromUiAndParams()`**) if not already shared with delete-command handlers.
- [ ] Implement **`SetSupplyAndObjectsTabVisibility(bool physical)`** (or two helpers) for **`tabPage4`** / **`tabPageOblig2`** without breaking **`tabControl` / `tabControlOblig`** index order.
- [ ] Add **`m_armedClearObligationsOnKindChange`**; reset on **EDIT** open after **`GetOneTrade`** mapping.
- [ ] Wire **`cmbKindSupplyTrade_SelectedIndexChanged`** with suppress during init.
- [ ] Call **`UpdateDisplayNDS`** and **`UpdateDisplayExport`** after obligation block.
