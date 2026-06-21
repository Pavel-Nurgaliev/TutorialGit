# CREATIVE: Form appearance � UbsPsUtPaymentFrm

**Scope:** Define how closely the .NET WinForms form should match `legacy-form/screens/1.png` through `6.png`, including visual priorities, acceptable deviations, and BUILD/REFLECT parity rules.  
**Sources:** `legacy-form/screens/`, `memory-bank/tasks.md`, `creative-ubspsutpaymentfrm-designer-layout.md`, template footer constraints from `UbsFormProject1`.  
**Style-guide note:** no workspace `memory-bank/style-guide.md` exists, so the visual baseline is the legacy screenshots plus UBS template constraints.

---

## ENTERING CREATIVE PHASE: UI/UX

### Requirements and constraints

1. The converted form must be recognizable as the same business form shown in the six legacy screenshots.
2. The six-tab shell, tab order, and tab identities must remain intact.
3. The visual layout must obey the inherited UBS template structure:
   - `panelMain` preserved
   - bottom action strip preserved
   - `tblActions` height `32`
4. The dense `��������` tab is the primary parity target.
5. The sparse tabs should preserve their intentional emptiness and top-left alignment rather than being artificially filled.
6. The .NET form does **not** need pixel-perfect VB6 rendering if that would violate template or usability constraints.

### Options

#### Option A � Pixel-faithful VB6 recreation

- **Description:** try to match VB6 coordinates, control compression, and footer layout as literally as possible.
- **Pros:** closest side-by-side visual match.
- **Cons:** fights the template footer rules, brittle under WinForms rendering and DPI, increases Designer fragility.

**Verdict:** reject.

#### Option B � Structural parity with template-aware adaptation (**recommended**)

- **Description:** preserve the business grouping, tab identity, reading order, and relative density from the screenshots while adapting footer placement and spacing to the .NET template.
- **Pros:** respects both user expectation and project rules, gives a usable WinForms result, and creates a stable parity target for BUILD and REFLECT.
- **Cons:** some control spacing and footer relationships will differ from the original.

**Verdict:** **selected.**

#### Option C � Modernized visual redesign

- **Description:** preserve only the business fields while redesigning the entire screen around modern layout principles.
- **Pros:** potentially cleaner UI.
- **Cons:** too much product drift, poor migration fidelity, invalidates the screenshot baseline.

**Verdict:** reject.

---

## EXITING CREATIVE PHASE � Decision summary

**Chosen approach:** Option B.

## Visual parity standard

The converted form should match the legacy screenshots in these dimensions:

- same tab shell and tab order
- same major field groupings
- same reading order inside each tab
- same visual density profile per tab
- same business-control prominence

The converted form may differ in these dimensions:

- precise pixel spacing
- exact old VB6 border/3D rendering
- exact footer compression, because the template footer must remain intact
- exact control widths where WinForms readability improves

## Screen-by-screen visual targets

### Screen `1.png` � `��������`

This is the highest-priority screen and defines the overall look of the form.

Required visual traits:

- top tab strip visible and dominant
- first major framed area reads as payer input
- second major framed area reads as recipient input
- lower band reads as money/period/summary/action region
- business buttons remain grouped toward the lower portion
- summary/payment-count fields remain visually connected to the lower region

Selected parity rule:

- preserve grouping and reading order first
- preserve approximate horizontal relationships second
- preserve exact widths only when they do not harm clarity

### Screen `2.png` � `�������� � ������� ����`

Required visual traits:

- narrow, simple, top-left form
- large empty area below
- no unnecessary framing or filler controls

Selected parity rule:

- this tab should feel intentionally sparse

### Screen `3.png` � `�����`

Required visual traits:

- one label and one combo
- both positioned near the upper-left
- almost all remaining space empty

### Screen `4.png` � `���������� ����`

Required visual traits:

- same sparse visual rhythm as the tariff tab
- single label/combo pair near the upper-left

### Screen `5.png` � `�����`

Required visual traits:

- left-aligned label column
- right-side editor column
- evenly spaced stacked rows
- visually repetitive tax-field form with no extra decoration

Selected parity rule:

- row rhythm and label/editor pattern matter more than VB6 control dimensions

### Screen `6.png` � `�������������� ��������`

Required visual traits:

- dominant table/grid/editor surface
- almost full-tab usage by the add-fields host
- only minimal padding

Selected parity rule:

- this tab should visually read as �data surface first�

## Priority of visual fidelity

### Tier 1 � must match

- six-tab order and captions
- `��������` tab grouping:
  - payer
  - recipient
  - lower business region
- tax-tab stacked structure
- add-fields tab fill dominance

### Tier 2 � should match

- sparse tabs staying sparse
- rough lower-button ordering and grouping
- relative placement of summary fields on the main tab

### Tier 3 � may adapt

- exact control widths
- exact row heights
- exact classic VB6 spacing and border feel

## Template-aware appearance rule

The most important visual adaptation is the footer:

- the template footer keeps only:
  - `uciInfo`
  - `btnSave`
  - `btnExit`
- the screenshot-era footer business controls stay above the template footer in the content area

Resulting policy:

- judge parity by business-region composition, not by forcing the template footer to imitate the VB6 footer literally

## BUILD appearance guidelines

1. Build the screen in the screenshot tab order from the start.
2. Tune `tabPageGeneral` first, because it dominates user perception of parity.
3. Keep sparse tabs intentionally under-filled.
4. Prefer consistent alignment and readable spacing over exact coordinate copying.
5. Use anchors and limited helper panels rather than complex nested layouts.
6. When choosing between exact VB6 density and clipped WinForms controls, choose readability.

## REFLECT acceptance criteria

- [x] A clear visual standard is defined before BUILD.
- [ ] Side-by-side review of the .NET form against all six screenshots is performed.
- [ ] The converted `��������` tab clearly reads as payer -> recipient -> lower business region.
- [ ] `�������� � ������� ����`, `�����`, and `���������� ����` remain sparse and top-left oriented.
- [ ] `�����` preserves the stacked form rhythm.
- [ ] `�������������� ��������` is visually dominated by the add-fields host.
- [ ] No screenshot-critical control is hidden, collapsed, or visually de-emphasized by the template adaptation.
- [ ] Template footer constraints are still satisfied.

## Creative conclusion

- The correct target is not pixel-perfect VB6 cloning.
- The correct target is screenshot-recognizable business parity implemented inside the approved UBS WinForms template.
- The success condition is that a user familiar with the VB6 screens can immediately recognize each tab and find fields in the same visual order.

---

**Recommended next step:** `/build`
