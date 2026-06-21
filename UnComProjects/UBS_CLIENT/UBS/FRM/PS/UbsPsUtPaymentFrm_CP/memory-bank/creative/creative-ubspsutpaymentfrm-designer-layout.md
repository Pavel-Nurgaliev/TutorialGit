# CREATIVE: Designer layout � UbsPsUtPaymentFrm

**Feature:** WinForms `panelMain`, `TabControl`, group regions, footer placement, and field placement strategy for VB6 `UtPayment.dob` -> `UbsPsUtPaymentFrm`.  
**Inputs:** `memory-bank/tasks.md`, `legacy-form/screens/`, `designer-rules.mdc`, `TMP_CP/UbsFormProject1/UbsForm1.Designer.cs`, `UbsPsUtPaymentGroupFrm` designer-layout creative doc.  
**Visual baseline:** No dedicated `memory-bank/style-guide.md` exists in this workspace, so visual consistency is derived from the UBS template, legacy screenshots, and sibling PS conversions.

---

## ENTERING CREATIVE PHASE: UI/UX

### Requirements and constraints

1. **`panelMain` must exist** and remain the inherited primary client surface.
2. **Bottom action strip must preserve the template structure:** `TableLayoutPanel`, `Dock = Bottom`, height `32`, columns `Percent 100 | 88 | 88`.
3. **Do not redesign the template semantics:** use `TabControl` for the six legacy tabs and keep `Save` / `Exit` in the template footer.
4. **Legacy screen parity matters:** the six screenshots are the functional layout baseline, especially the dense `��������` tab.
5. **Main-tab density requires breathing room:** payer block, recipient block, and lower amount/action region will not fit safely in the raw template size.
6. **Sparse tabs should stay simple:** `�����`, `���������� ����`, and `�������� � ������� ����` should not be over-containerized.
7. **Additional-properties tab should be fill-oriented:** it visually behaves like a large host/editor surface.
8. **Footer-era auxiliary controls from VB6 must not break template structure:** payment count, totals, print checkbox, and extra action buttons belong above the template footer, not inside it.
9. **Naming should match the PS convention:** use names like `tblActions`, `uciInfo`, `tabPayment`, `grpPayer`, `grpRecipient`, `pnlGeneralScroll`.

### Options analyzed

#### Option A � Template skeleton only

- **Description:** keep the starter template almost unchanged, add tabs later, and defer the dense layout.
- **Pros:** fastest first compile, minimal Designer churn.
- **Cons:** not useful for real data entry, does not answer the layout problem, too little parity with screenshots.

**Verdict:** reject for the actual conversion baseline.

#### Option B � Fixed absolute-position recreation of the VB6 screens

- **Description:** convert VB6 geometry almost one-to-one into WinForms coordinates without scroll containers.
- **Pros:** closest static visual match to screenshots.
- **Cons:** brittle on different host sizes and DPI settings, hard to maintain, easy to clip the dense bottom region on the main tab.

**Verdict:** not recommended as the primary structure.

#### Option C � Template footer + fill `TabControl` + scrollable main tab + simple sparse tabs (**recommended**)

- **Description:** keep the template footer intact, dock a `TabControl` above it, place a scrollable panel on `tabPageGeneral`, and use simpler anchored layouts on the remaining tabs.
- **Pros:** preserves template rules, handles the dense main tab safely, keeps sparse tabs readable, and matches the proven sibling PS pattern.
- **Cons:** some exact VB6 pixel relationships will be approximated rather than reproduced literally.

**Verdict:** **selected.**

#### Option D � Nested `TableLayoutPanel` structure for the whole form

- **Description:** model every tab and region with nested table layouts.
- **Pros:** strong row/column alignment, resize-friendly.
- **Cons:** heavy Designer complexity, harder to maintain in .NET 2.0 WinForms, more cumbersome for the irregular VB6 main tab.

**Verdict:** defer; use targeted anchors and only minimal layout containers.

---

## EXITING CREATIVE PHASE � Decision summary

**Chosen approach:** Option C.

### Selected hierarchy

```text
Form
`-- panelMain (Dock Fill)
    |-- tabPayment (TabControl, Dock Fill)
    |   |-- tabPageGeneral ("��������")
    |   |   `-- pnlGeneralScroll (Panel, Dock Fill, AutoScroll = true)
    |   |       |-- grpPayer
    |   |       |-- grpRecipient
    |   |       `-- pnlGeneralFooterArea
    |   |           |-- summary/count controls
    |   |           |-- print-form checkbox
    |   |           `-- extra action buttons (`btnCashSymbols`, `btnUserPattern`, `btnCalcCash`)
    |   |-- tabPageThirdPerson ("�������� � ������� ����")
    |   |   `-- simple anchored label/control form
    |   |-- tabPageTariff ("�����")
    |   |   `-- simple anchored label/combo layout
    |   |-- tabPageTelephone ("���������� ����")
    |   |   `-- simple anchored label/combo layout
    |   |-- tabPageTax ("�����")
    |   |   `-- two-column vertical form
    |   `-- tabPageAddFields ("�������������� ��������")
    |       `-- ucfAddProperties (Dock Fill)
    `-- tblActions (TableLayoutPanel, Dock Bottom, Height 32)
        |-- uciInfo
        |-- btnSave
        `-- btnExit
```

### Tab order and captions

The `TabControl` should follow the screenshot order:

1. `tabPageGeneral` -> `��������`
2. `tabPageThirdPerson` -> `�������� � ������� ����`
3. `tabPageTariff` -> `�����`
4. `tabPageTelephone` -> `���������� ����`
5. `tabPageTax` -> `�����`
6. `tabPageAddFields` -> `�������������� ��������`

This order should be explicit in `Designer.cs` rather than inferred from legacy source declaration order.

### Main-tab layout decision

The main tab is the dominant layout problem, so it gets the most structure:

- Use `pnlGeneralScroll` with `AutoScroll = true`.
- Place `grpPayer` first near the top.
- Place `grpRecipient` directly below `grpPayer`.
- Place a dedicated lower region below `grpRecipient` for:
  - amount editors
  - penalty / commission totals
  - period fields
  - summary count / total controls
  - print-form checkbox
  - `btnCashSymbols`
  - `btnUserPattern`
  - `btnCalcCash`
- Keep the lower region inside the scrollable content area so the template footer stays untouched.

### Grouping decision

#### `grpPayer`

Keep it visually similar to the legacy `����������` block:

- name + client browse button
- INN
- address
- requisites/info text
- source means / list-payment fields
- card number
- benefits checkbox and benefit reason

This region should use anchored controls and small local alignment, not a deep nested layout grid.

#### `grpRecipient`

Keep it visually similar to the legacy `����������` block:

- contract code and browse button
- BIK
- bank name
- correspondent account
- recipient account
- INN / KPP
- purpose combo
- recipient name and note/comment
- attribute buttons near the top-right area

This block is also better served by anchored controls than by a large table layout.

### Sparse-tab strategy

#### `tabPageThirdPerson`

- simple top-aligned form
- fields: name, third-person kind, INN, KPP
- browse/select button at the end of the name row

#### `tabPageTariff`

- one label and one combo near the top-left
- no extra grouping container needed

#### `tabPageTelephone`

- one label and one combo near the top-left
- keep spacing consistent with the tariff tab

#### `tabPageTax`

- left column for labels
- right column for text editors
- vertically stacked rows matching the screenshot order
- consistent control width is more important than container complexity

#### `tabPageAddFields`

- reserve almost all content area for `ucfAddProperties`
- `Dock = Fill`
- margins only; no extra decorative containers

### Footer decision

Keep the template footer strictly limited to:

- `uciInfo`
- `btnSave`
- `btnExit`

Do **not** move these legacy controls into `tblActions`:

- payment-count controls
- summary totals
- `chkPrintForms`
- `btnCashSymbols`
- `btnUserPattern`
- `btnCalcCash`

They belong in the content region above the footer so the form remains template-compliant.

### Sizing decision

- Start from a substantially larger client area than the starter template.
- Recommended initial `ClientSize`: approximately `560-620` wide by `520-600` high.
- The exact first-pass size can be refined during BUILD after the initial `Designer.cs` is rendered.
- Scroll behavior is the primary protection against clipping, so sizing does not need to perfectly reproduce the VB6 window.

### Control-hosting rules

- `tabPayment` should be added before `tblActions` or otherwise configured so docking resolves as fill content above the footer.
- Only one `UbsCtrlInfo` should exist on the converted main form, hosted in `tblActions`.
- `panelMain` must not be redefined.
- Avoid unnecessary nesting beyond:
  - `TabControl`
  - main scroll panel
  - optional small region panels where alignment helps readability

### BUILD implementation guidelines

1. Rename template footer controls to PS-style names:
   - `tableLayoutPanel` -> `tblActions`
   - `ubsCtrlInfo` -> `uciInfo`
2. Create `tabPayment` and all six tab pages in screenshot order.
3. Add `pnlGeneralScroll` on the general tab before placing payer/recipient controls.
4. Place `grpPayer` and `grpRecipient` first; add lower summary/action controls only after both groups fit cleanly.
5. Keep `tabPageTariff`, `tabPageTelephone`, and `tabPageThirdPerson` intentionally simple.
6. Let `tabPageAddFields` be the easiest tab: one fill host.
7. Defer exact tab order polish and keyboard movement details to `Keys.cs` and later BUILD passes.

### Verification

- [x] Multiple layout options considered.
- [x] Template constraints preserved.
- [x] Main-tab density handled explicitly.
- [x] Tab-by-tab strategy documented.
- [x] BUILD guidance recorded.

---

**Recommended next step:** `/creative Constants file: resource names, command strings, captions, messages`
