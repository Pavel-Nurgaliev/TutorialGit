# PLAN: Form Appearance — Match Legacy Screens

**Target file:** `UbsOpCommissionFrm/UbsOpCommissionFrm.Designer.cs`  
**Reference:** `legacy-form-screens/image1.png`, `legacy-form-screens/image2.png`  
**Date:** 2026-03-16

---

## 1. Goal

Change the form’s appearance in the Designer so it matches the legacy screens in `legacy-form-screens`: window title, tab names, control types, and layout.

---

## 2. Legacy Screen Summary

| Screen   | Title           | Tabs (active first) | First tab content                    | Buttons   |
|----------|-----------------|----------------------|--------------------------------------|-----------|
| **image1** | «Добавить новую» | Комиссия, Набор параметров | Наименование (single-line), Описание (**multi-line**) | Сохранить, Выход |
| **image2** | «Добавить новую» | Комиссия, Набор параметров | Grid: наименование (10.9), значение, !              | Сохранить, Выход |

- **image1** defines the main layout to match: title, tab captions, and **multi-line** Описание.
- **image2** shows a parameter grid on the «Комиссия» tab; that grid may be the same as the content of the second tab (Набор параметров / `ubsCtrlAddFields`). If the grid must appear on the first tab in this form, treat that as a separate follow-up task.

---

## 3. Current vs Legacy

| Element        | Current (Designer) | Legacy (screens)     | Action |
|----------------|--------------------|----------------------|--------|
| Form title     | «Комиссия»         | «Добавить новую»     | Change default `Text` to «Добавить новую». (Code can set title by mode, e.g. Edit = «Редактировать».) |
| Tab 1 text     | «Основные»         | «Комиссия»           | Set `tabPageMain.Text = "Комиссия"`. |
| Tab 2 text     | «Доп. поля»        | «Набор параметров»   | Set `tabPageAddFields.Text = "Набор параметров"`. |
| Описание field | Single-line `TextBox` (Height 20) | Multi-line text area | Set `txbDesc.Multiline = true`, increase height (e.g. 2–3 lines), set `ScrollBars` (e.g. Vertical). Optionally anchor so it resizes with the tab. |
| Buttons        | Сохранить, Выход   | Same                 | No change. |

---

## 4. Recommended Designer Changes (UbsOpCommissionFrm.Designer.cs)

### 4.1 Form title

- In the form’s `// UbsOpCommissionFrm` section, set:
  - `this.Text = "Добавить новую";`
- If the product requires different titles for Add vs Edit, keep this as the default and set `this.Text` in code (e.g. in `InitDoc` or after `ListKey`) based on `m_command`.

### 4.2 Tab captions

- **tabPageMain:** set `this.tabPageMain.Text = "Комиссия";` (replace «Основные»).
- **tabPageAddFields:** set `this.tabPageAddFields.Text = "Набор параметров";` (replace «Доп. поля»).

### 4.3 Description textbox (multi-line)

- **txbDesc:**
  - `Multiline = true`
  - Increase height (e.g. `Size = new System.Drawing.Size(475, 60)` or similar for 2–3 lines; keep width or make it anchor to fill).
  - `ScrollBars = System.Windows.Forms.ScrollBars.Vertical` (optional, for long text).
  - Optionally set `Anchor = Top | Left | Right | Bottom` (or Left, Right, Top) so it resizes with the tab.

### 4.4 Code-behind dependency

- `UbsOpCommissionFrm.cs` registers the add-fields tab with:
  - `base.UbsCtrlFieldsSupportCollection.Add("Доп. поля", ubsCtrlAddFields);`
- After changing the tab text to «Набор параметров», update that call to use the same caption:
  - `base.UbsCtrlFieldsSupportCollection.Add("Набор параметров", ubsCtrlAddFields);`
- So the key used in the collection matches the tab’s `Text`.

---

## 5. Checklist (implementation)

- [ ] **Designer:** Form `Text` = «Добавить новую».
- [ ] **Designer:** `tabPageMain.Text` = «Комиссия».
- [ ] **Designer:** `tabPageAddFields.Text` = «Набор параметров».
- [ ] **Designer:** `txbDesc`: Multiline = true, height increased, ScrollBars = Vertical (if desired), Anchor set (if desired).
- [ ] **Code-behind:** `UbsCtrlFieldsSupportCollection.Add("Набор параметров", ubsCtrlAddFields);` (replace «Доп. поля»).
- [ ] Build and visual check against legacy screens.

---

## 6. Out of scope (this plan)

- Adding the parameter grid from image2 to the first tab (only if product confirms that the grid must be on «Комиссия» in this form).
- Changing form size or button layout unless needed to match the screens.

---

## 7. Relation to other docs

- **plan-legacy-source-conversion.md** — Commission_ud → UbsOpCommissionFrm conversion; this plan only adjusts appearance to match legacy screens.
- **progress.md** — After implementation, add a short note that form appearance was aligned with legacy-form-screens.
