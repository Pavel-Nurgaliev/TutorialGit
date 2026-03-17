# Creative Phase: Form Appearance — Legacy Screens

**Focus:** UbsOpCommissionFrm visual and layout match to `legacy-form-screens` (image1, image2)  
**Type:** UI/UX Design  
**Plan reference:** `memory-bank/plan-form-appearance-legacy-screens.md`  
**Date:** 2026-03-16

---

## ENTERING CREATIVE PHASE: UI/UX

**Objective:** Decide how the form title, tab captions, and Описание (description) field are implemented so the form matches the legacy screens and remains maintainable.

**Requirements:**
- Form default title: «Добавить новую» (legacy).
- First tab caption: «Комиссия» (legacy); second: «Набор параметров» (legacy).
- Описание must be a **multi-line** text area (legacy image1).
- Buttons «Сохранить» and «Выход» unchanged.
- Code-behind key for add-fields tab must match the chosen tab caption (UbsCtrlFieldsSupportCollection).

**Constraints:**
- WinForms .NET (no React/Tailwind); standard Windows conventions.
- Existing base: UbsFormBase, panelMain, tableLayoutPanel, TabControl with tabPageMain and tabPageAddFields.
- Constants partial already exists; can add UI strings there if desired.

---

## Design options

### Option A — Minimal literal match (plan as-is)

- **Form title:** Designer `Text = "Добавить новую"` (fixed).
- **Tab captions:** Designer `tabPageMain.Text = "Комиссия"`, `tabPageAddFields.Text = "Набор параметров"` (literals).
- **Описание:** `txbDesc.Multiline = true`, fixed size e.g. `(475, 60)`, `ScrollBars = Vertical`. No anchor.

| Pros | Cons |
|------|------|
| Fast to implement, exactly as plan | Title never changes for Edit mode |
| No new constants | Tab text duplicated if ever used in code |
| Matches legacy screenshot literally | Description area fixed height; doesn’t use extra space when form is resized |

---

### Option B — Literal match + resizable description

- Same as A for title and tab captions.
- **Описание:** `txbDesc.Multiline = true`, `Anchor = Top, Left, Right, Bottom` (or Top, Left, Right), initial height ~60. `ScrollBars = Vertical`. Fills remaining tab space on resize.

| Pros | Cons |
|------|------|
| Better use of space when user resizes form | Slightly more Designer setup (anchor) |
| Still literal match for captions and title | Same as A for static title/tabs |

---

### Option C — Literal match + dynamic form title

- **Form title:** Designer default `Text = "Добавить новую"`. In code (e.g. `InitDoc` or after `ListKey`), set `this.Text = m_command == EditCommand ? "Редактировать" : "Добавить новую"` (or constants).
- Tabs and Описание as in A or B.

| Pros | Cons |
|------|------|
| Clearer UX for Add vs Edit | Requires code change and testing for both modes |
| Aligns with common “Add new” / “Edit” pattern | Not strictly required by legacy screens (both show «Добавить новую») |

---

### Option D — Constants-driven UI strings

- Add to Constants partial: e.g. `FormTitleAdd`, `FormTitleEdit`, `TabTextCommission`, `TabTextParameterSet`.
- Designer keeps literals (Designer doesn’t use constants easily); in constructor or `Load`, set `this.Text`, `tabPageMain.Text`, `tabPageAddFields.Text` from constants. `UbsCtrlFieldsSupportCollection.Add(TabTextParameterSet, ubsCtrlAddFields)`.

| Pros | Cons |
|------|------|
| Single source of truth for UI strings | More code; strings set at runtime |
| Easier localization/maintenance later | Slightly more complex than pure Designer literals |

---

## Recommended approach

**Primary:** **Option B (literal match + resizable description)**  
- Form title: «Добавить новую» in Designer (no dynamic title unless product asks).  
- Tab captions: «Комиссия» and «Набор параметров» in Designer.  
- **txbDesc:** Multiline, Vertical scrollbars, **Anchor = Top, Left, Right, Bottom**, initial height ~60 so the description area grows when the user resizes the form.

**Optional follow-ups (not in initial implementation):**
- **Option C** if product wants different titles for Add vs Edit.
- **Option D** if product wants all UI strings in constants for localization or reuse.

**Rationale:** Option B keeps implementation simple and matches the legacy layout while improving resizing behavior. It satisfies the plan checklist without introducing constants or title logic until needed.

---

## Implementation guidelines

1. **Designer (UbsOpCommissionFrm.Designer.cs)**  
   - Form: `this.Text = "Добавить новую";`  
   - tabPageMain: `this.tabPageMain.Text = "Комиссия";`  
   - tabPageAddFields: `this.tabPageAddFields.Text = "Набор параметров";`  
   - txbDesc:  
     - `Multiline = true`  
     - `ScrollBars = System.Windows.Forms.ScrollBars.Vertical`  
     - `Size` e.g. `(475, 60)` (or keep width, height ~60)  
     - `Anchor = Top | Left | Right | Bottom` (so it expands with the tab)

2. **Code-behind (UbsOpCommissionFrm.cs)**  
   - Replace:  
     `base.UbsCtrlFieldsSupportCollection.Add("Доп. поля", ubsCtrlAddFields);`  
     with:  
     `base.UbsCtrlFieldsSupportCollection.Add("Набор параметров", ubsCtrlAddFields);`

3. **Verification**  
   - Build; open form and compare to legacy-form-screens (title, tabs, multi-line Описание, buttons). Resize form and confirm description area grows.

---

## EXITING CREATIVE PHASE

**Summary:** Form appearance will match legacy screens using literal caption/title strings and a multi-line, resizable Описание field (Option B). Optional dynamic title (Option C) and constants-driven strings (Option D) are deferred.

**Key decisions:**
- Form title: «Добавить новую» in Designer; dynamic Add/Edit title left for later if required.
- Tab texts: «Комиссия», «Набор параметров» in Designer.
- txbDesc: Multiline, Vertical scrollbars, Anchor to fill tab.
- UbsCtrlFieldsSupportCollection key: «Набор параметров».

**Next steps:** Proceed to `/build` to apply changes in Designer and code-behind per plan-form-appearance-legacy-screens.md and this document.
