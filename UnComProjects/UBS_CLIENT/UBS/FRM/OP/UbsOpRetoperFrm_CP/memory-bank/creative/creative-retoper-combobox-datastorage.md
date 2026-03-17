# CREATIVE: ComboBox Value Storage (ItemData Pattern)

**Context:** Legacy VB6 uses `ComboBox.ItemData(index)` to store integer IDs (currency codes, document IDs). .NET ComboBox has no ItemData. Need a pattern for valMinusCB, valComis, docCB.

---

## 🎨🎨🎨 ENTERING CREATIVE PHASE: Algorithm / Data Structure

### Requirements

- valMinusCB: display currency name (e.g. "RUB"), store currency ID (int)
- valComis: same
- docCB: display document type name, store document ID (if used; legacy has ItemData commented out)
- Must support: SelectedIndex → get ID; set selection by ID (e.g. SetFocusCombo equivalent)
- .NET Framework 2.0 compatible

---

## Design Options

### Option A: Tag per Item

**Description:** Add each item with `AddItem(text)`; store ID in `Items[i].Tag` or use a parallel `List<int>` keyed by index.

**Pros:**
- Simple
- Works with DropDownList

**Cons:**
- Tag is object; need cast
- No built-in "find by value"; must iterate
- Parallel list risks index drift

### Option B: DataSource (DisplayMember / ValueMember)

**Description:** Create `List<KeyValuePair<int, string>>` or custom class; set ComboBox.DataSource, DisplayMember, ValueMember.

**Pros:**
- Standard .NET pattern
- SelectedValue returns ID
- Clean separation

**Cons:**
- Requires DataSource setup
- SelectedValue type may be object; need cast
- .NET 2.0: ValueMember works with property names; KeyValuePair may need wrapper class

### Option C: ComboBoxItem Helper Class

**Description:** Custom class `class ComboBoxItem { int Id; string Text; }`; add to Items; override ToString for display. Access via `(ComboBoxItem)combo.SelectedItem`.

**Pros:**
- Explicit control
- Clear semantics
- Works with DropDownList

**Cons:**
- Custom class per form or shared
- Manual AddItem in loop

### Option D: Dictionary<int, string> + Index

**Description:** `Dictionary<int, string>` for id→text; ComboBox.Items.AddRange(dict.Values.ToArray()); parallel `List<int>` for index→id; or store ids in Tag of a wrapper.

**Pros:**
- Direct lookup
- Simple

**Cons:**
- Order of Values may not match desired order
- Dictionary iteration order not guaranteed

---

## Analysis and Recommendation

| Criterion | A | B | C | D |
|-----------|---|---|---|---|
| Simplicity | ✓ | ○ | ○ | ○ |
| SelectedValue | ✗ | ✓ | ✓ | ○ |
| Set by ID | ✗ | ✓ | ✓ | ○ |
| .NET 2.0 | ✓ | ✓ | ✓ | ✓ |

**Recommendation:** **Option B (DataSource with KeyValuePair)**

**Reference:** UbsOpBlankFrm uses `List<KeyValuePair<int, string>>` with `cmb.DataSource = list`; selected ID via `((KeyValuePair<int, string>)cmbState.SelectedItem).Key`. Align with this pattern for consistency.

- Create `List<KeyValuePair<int, string>>`; add items (id, text).
- Set `ComboBox.DataSource = list`; `DisplayMember = "Value"`; `ValueMember = "Key"` (or use SelectedItem.Key since KeyValuePair displays Value by default).
- Get selected ID: `((KeyValuePair<int, string>)valMinusCB.SelectedItem).Key`
- Set by ID: `foreach (KeyValuePair<int, string> item in valMinusCB.Items) { if (item.Key == targetId) { valMinusCB.SelectedItem = item; break; } }`

---

## Implementation Guidelines

1. Use `List<KeyValuePair<int, string>>` (same as UbsOpBlankFrm cmbState).
2. Fill valMinusCB from valArr: `for (i = 0; i <= UBound(valArr, 2); i++) list.Add(new KeyValuePair<int, string>(valArr[0,i], valArr[2,i]));` then `valMinusCB.DataSource = list;`
3. Get selected: `if (valMinusCB.SelectedItem != null) id = ((KeyValuePair<int, string>)valMinusCB.SelectedItem).Key;`
4. Set by ID: `foreach (KeyValuePair<int, string> kvp in valMinusCB.Items) { if (kvp.Key == targetId) { valMinusCB.SelectedItem = kvp; break; } }`
5. docCB: legacy fills from docArr(1,i) — display text only, no ItemData; docCB is readonly. Use `List<string>` or `List<KeyValuePair<int, string>>` with id=0; no selected-ID logic needed for save.

---

## 🎨🎨🎨 EXITING CREATIVE PHASE
