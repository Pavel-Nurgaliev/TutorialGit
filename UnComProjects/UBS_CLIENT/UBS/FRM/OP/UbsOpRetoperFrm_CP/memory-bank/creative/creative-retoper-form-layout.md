# CREATIVE: op_ret_oper Form Layout Structure (UI/UX)

**Context:** Legacy `op_ret_oper.dob` → target `UbsOpRetoperFrm`. This doc explores layout container options to achieve the form structure matching `legacy-form/screens/image1.png`.

---

## 🎨🎨🎨 ENTERING CREATIVE PHASE: UI/UX Layout Structure

### Requirements

- GroupBox "Платежный документ" at top (readonly block)
- Rows: payMoney; Выдать (valMinusCB + sumMinusCur + rateCur + NU); Комиссия (valComis + komCur + podNalCur); nKvit; Клиент; Документ; Резидент
- Bottom strip: Info, Save, Exit
- Form size ~6735×4215 twips (≈449×281 px) — VB6 legacy
- Controls must align with labels; no overlap; readable at default DPI

### Constraints

- .NET Framework 2.0
- WinForms only (no WPF)
- Must match UbsOpBlankFrm_CP / UbsOpCommissionFrm_CP structure (panelMain, tableLayoutPanel bottom)

---

## Design Options

### Option A: TableLayoutPanel (Grid)

**Description:** Single TableLayoutPanel with rows and columns. GroupBox in row 0; content rows 1–N; bottom strip in last row.

**Pros:**
- Predictable alignment
- Resize behavior via column/row styles
- Common in UBS forms (UbsOpBlankFrm uses TableLayoutPanel for bottom strip)

**Cons:**
- Complex column/row definitions for irregular layout
- GroupBox content may need nested TableLayoutPanel
- Many cells for sparse layout

### Option B: FlowLayoutPanel (Flow)

**Description:** FlowLayoutPanel; controls added in order; wrap or no wrap.

**Pros:**
- Simple to add controls
- Auto-flow when resizing

**Cons:**
- Poor control over exact positioning
- Labels and inputs may misalign
- Does not match legacy grid-like layout

### Option C: Manual Anchors + Positioning

**Description:** Panel with controls placed by Location/Size; Anchor for resize behavior.

**Pros:**
- Exact pixel match to legacy
- Full control

**Cons:**
- Brittle; DPI/resolution issues
- Hard to maintain
- Not recommended for new layouts

### Option D: Panel + TableLayoutPanel (Hybrid)

**Description:** PanelMain contains: (1) GroupBox "Платежный документ" — fixed at top; (2) main content Panel with TableLayoutPanel (e.g. 2–3 columns: label, control, optional; rows per field); (3) TableLayoutPanel bottom strip (Info | Save | Exit).

**Pros:**
- Clear separation of sections
- TableLayoutPanel for content rows gives label+control alignment
- GroupBox isolates readonly block
- Matches UbsOpBlankFrm pattern (tabControl + tableLayoutPanel)

**Cons:**
- Slightly more nesting
- Some rows have multiple controls (e.g. Выдать: valMinusCB + sumMinusCur + rateCur + NU); may need multi-column layout or nested panels

---

## Analysis and Recommendation

| Criterion | A | B | C | D |
|-----------|---|---|---|---|
| Alignment | ✓ | ✗ | ✓ | ✓ |
| Maintainability | ○ | ✓ | ✗ | ✓ |
| Legacy parity | ○ | ✗ | ✓ | ✓ |
| UBS pattern match | ✓ | ✗ | ✗ | ✓ |

**Recommendation:** **Option D (Hybrid)**

- Use **GroupBox** for "Платежный документ" with internal layout (labels + TextBoxes + UbsCtrlDecimal).
- Use **Panel** for main content with **TableLayoutPanel** (2–4 columns: label, control(s), optional). Rows with multiple controls (e.g. Выдать) use multiple cells or a nested FlowLayoutPanel.
- Use **TableLayoutPanel** for bottom strip (Info | Save | Exit) — same as UbsOpBlankFrm.
- panelMain contains: GroupBox (top), content Panel (fill), tableLayoutPanel (bottom, dock).

---

## Implementation Guidelines

1. **panelMain:** Dock Fill; contains GroupBox (top), contentPanel (fill), tableLayoutPanel (bottom).
2. **GroupBox "Платежный документ":** Enabled=false; 3 rows: (Наименование, plDoc), (Серия, serPDoc | Номер, numPDoc), (Валюта номинала, valNom | Сумма по номиналу, nomPDoc).
3. **Content rows:** TableLayoutPanel 3 columns (label width ~150, control width *, optional). Rows: payMoney; Выдать (valMinusCB, sumMinusCur, "к", rateCur, NU); Комиссия (valComis, komCur, podNalCur); nKvit; Клиент; Документ; Резидент.
4. **Bottom strip:** tableLayoutPanel, 3 columns (Info | Save | Exit), same as UbsOpBlankFrm.

---

## 🎨🎨🎨 EXITING CREATIVE PHASE
