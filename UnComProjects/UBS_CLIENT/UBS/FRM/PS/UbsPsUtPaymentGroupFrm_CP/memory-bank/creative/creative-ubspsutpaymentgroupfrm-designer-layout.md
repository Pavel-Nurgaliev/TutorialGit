# CREATIVE: Designer layout — UbsPsUtPaymentGroupFrm

**Feature:** WinForms `panelMain`, `TabControl`, `GroupBox` regions, control placement for VB6 `UtPaymentGroup_F` → `UbsPsUtPaymentGroupFrm`.  
**Inputs:** `memory-bank/plan-vb6-controls-map.md`, `designer-rules.mdc` (keep `panelMain`; bottom `TableLayoutPanel` height = template), `UbsPsContractFrm.Designer.cs` reference, legacy `UtPaymentGroup_F.dob` geometry.

---

## ENTERING CREATIVE PHASE: UI/UX

### Requirements and constraints

1. **`panelMain` must exist** and remain the primary client surface (template / `UbsFormBase` rule).
2. **Bottom action strip:** 32 px row height, three columns (info stretch, Save 88px, Exit 88px), **Dock Bottom** — match `TMP_CP\UbsFormProject1\UbsForm1.Designer.cs` / `UbsPsContractFrm` (`tblActions` block).
3. **Do not redesign template semantics:** tabs + bottom bar pattern like other PS conversions; SSActiveTabs → `TabControl`.
4. **Naming:** Prefer PS reference names (`tblActions`, `uciInfo`) over template placeholder names (`tableLayoutPanel`, `ubsCtrlInfo`) for consistency with `plan-vb6-controls-map.md` and `UbsPsContractFrm`.
5. **VB6 proportions:** UserDocument ~8505×6435 twips (~567×429 px content); content is **taller than wide** (payer + recipient + money row); scrolling likely required.
6. **Single status control:** One `UbsCtrlInfo` in `tblActions`, not a second duplicate on the main tab (VB6 had `Info` on tab — .NET consolidates to bottom bar per plan).

### Options analyzed

#### Option A — Template-only skeleton (392×273, only bottom strip)

- **Pros:** Zero layout decisions; compiles immediately.
- **Cons:** unusable for real data entry; conflicts with conversion goal.

**Verdict:** reject for production BUILD; acceptable only as interim checkpoint.

#### Option B — VB6 pixel-scale layout without scroll

- **Pros:** closest static snapshot to `.dob` positions.
- **Cons:** fixed `ClientSize` brittle in host; amount row may clip on different DPI; duplicates maintenance of twip math.

**Verdict:** possible for REFLECT screenshot diff; not primary.

#### Option C — `UbsPsContractFrm` pattern: `TabControl` Fill + `tblActions` Bottom + scroll panel on main tab (**recommended**)

- **Pros:** Proven in PS line; `AutoScroll` handles payer + recipient + amounts; obeys `panelMain` + 32 px bar; aligns with `grpPayer` / `grpRecipient` stacked vertically inside `pnlMainScroll`.
- **Cons:** exact pixel positions differ from VB6; BUILD must place controls with anchors/margins iteratively.

**Verdict:** **selected.**

#### Option D — `TableLayoutPanel` for entire main tab (rows for payer, recipient, amounts)

- **Pros:** rigid alignment; easy resize columns.
- **Cons:** heavy Designer churn; nested table layouts harder to match legacy grouping; more merge pain with partials.

**Verdict:** defer; use GroupBoxes + anchors first.

---

## EXITING CREATIVE PHASE — Decision summary

**Chosen approach: Option C** (same structural pattern as `UbsPsContractFrm`).

### Hierarchy (implementation guideline)

```
Form
└── panelMain (Dock Fill, sized by host — target ~735×600 ClientSize scale like UbsPsContractFrm unless host dictates)
    ├── tabPayment (TabControl, Dock Fill)
    │   ├── tabPageMain — Text = "Основные"
    │   │   └── pnlMainScroll (Panel, Dock Fill, AutoScroll = true)
    │   │       ├── grpPayer ("Плательщик") — Dock Top or Top + Left anchor
    │   │       ├── grpRecipient ("Получатель") — below grpPayer (Top below payer)
    │   │       └── [amount row] labels + udc* — below grpRecipient
    │   └── tabPageAddProperties — Text = "Дополнительные свойства"
    │       └── ucfAddProperties (Dock Fill, Margin ~6–12)
    └── tblActions (TableLayoutPanel, Dock Bottom, Height 32, columns Percent 100 | 88 | 88)
        ├── uciInfo (col 0, Dock Bottom)
        ├── btnSave (col 1)
        └── btnExit (col 2)
```

### Tab indices and VB6 parity

- Add **`tabPageMain` first**, then **`tabPageAddProperties`** so **`SelectedIndex == 0`** is «Основные».
- VB6 `subPayment.Tabs(1).Selected = True` is **1-based** «first tab» in many SSActiveTabs builds → maps to **main tab** = .NET index **0**. **BUILD verify** once against screenshot or runtime.

### Rename from template (BUILD)

| Current (template)   | Target (plan / UbsPsContractFrm style) |
|----------------------|----------------------------------------|
| `tableLayoutPanel`   | `tblActions`                           |
| `ubsCtrlInfo`        | `uciInfo`                              |

Column widths and **32** row height: copy literals from template **or** `UbsPsContractFrm` `tblActions` block; they are the same pattern.

### Group box internals

- **grpPayer:** Two-column feel: labels left column, fields right — mirror `.dob` order (FIO + client button + INN row; Address; Requisites/info; Card). Use **Anchor** Top/Left on labels and text boxes; fixed heights typical WinForms 23–21 px.
- **grpRecipient:** Contract code row (`lblContractCode`, `cmbCode`), comment/`txtComment`, BIC/bank/accounts (`ucaAccKorr`, `ucaAccClient`), INN, purpose, recipient name; attribute buttons top-right as in VB6.
- **Amount row:** `udcSumma`, `udcPeny`, `udcSummaRateSend`, `udcSummaTotal` with `lbl*` captions in one horizontal band below `grpRecipient` (inside `pnlMainScroll`).

### Z-order and `panelMain.Controls.Add` order

Match **Contract:** add **`tabPayment` first**, then **`tblActions`**, so bottom panel paints docked correctly (or rely on Dock — adding Fill then Bottom is the Contract order).

### Form `ClientSize`

- **Initial recommendation:** **~735 × 600** (or match sibling PS forms in solution) so `panelMain` matches `UbsPsContractFrm` scale; increase if screenshot review shows clipping.
- If shell embeds form: keep **`MinimumSize`** reasonable so scroll still works.

### Verification (REFLECT)

- [ ] `tblActions.Height == 32`
- [ ] `uciInfo` only in bottom row
- [ ] All controls from `plan-vb6-controls-map.md` present with **C# names**
- [ ] Legacy screenshots (`legacy-form\screens\`) — visual pass after BUILD

---

**Recommended next step:** `/build` — expand `UbsPsUtPaymentGroupFrm.Designer.cs` using this hierarchy; rename `tableLayoutPanel`/`ubsCtrlInfo` when touching the file.
