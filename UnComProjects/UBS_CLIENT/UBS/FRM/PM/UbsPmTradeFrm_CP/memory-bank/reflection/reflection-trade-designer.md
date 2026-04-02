# Reflection: UbsPmTradeFrm — Form Designer Phase

**Date:** 2026-03-24  
**Phase covered:** VAN → PLAN (v1) → BUILD (v1) → PLAN (revision) → BUILD (v2)  
**Complexity:** Level 4  
**Outcome:** ✅ `UbsPmTradeFrm.Designer.cs` compiled — 0 errors, DLL produced

---

## 1. Summary

The designer phase converted the layout of `Pm_Trade_ud.dob` (5365-line VB6 UserDocument) into a
.NET Framework 2.0 `TabControl`-based `InitializeComponent()`. The work required two full planning
cycles and two build attempts before reaching a clean compile:

| Iteration | Basis | Result |
|-----------|-------|--------|
| Plan v1 | DOB source code analysis alone | Produced a buildable Designer.cs, but with systematic layout errors |
| Plan v2 (revision) | 7 legacy screenshot analysis | Identified and corrected all errors; Designer.cs v2 compiled 0 errors |

---

## 2. What Went Well

### 2.1 Two-phase planning strategy was effective
Starting from the DOB source established the control inventory (names, types, relationships).
The subsequent screenshot pass then corrected visual layout without having to re-read 5365 lines
of VB6. The two-document approach (`plan-trade-designer-conversion.md` +
`plan-trade-designer-revision.md`) created a clear audit trail of changes.

### 2.2 Screenshot analysis revealed systematic errors that code-reading missed
The VB6 `SSActiveTabs` control stores tab captions as properties, not as immediately visible
string literals in the code. Reading only the DOB produced wrong tab names on every tab.
Screenshots verified ground truth in minutes, catching:
- All 6 tab captions wrong
- Tabs 4 and 5 content completely swapped
- 14 missing label controls in Tab 5

### 2.3 Control-array → named controls pattern was applied consistently
VB6 `txtBIK(0)` / `txtBIK(1)` control arrays → `txtBIK_0` / `txtBIK_1` named fields, with
matching parallel naming for all 19 controls in the payment instruction sub-tabs. This makes
the eventual business logic straightforward to implement (index-based loops → switch on suffix).

### 2.4 Anchor strategy matched the legacy form's resize behaviour
Using `Anchor=TLR` (Top+Left+Right) on text inputs and `Anchor=All` on ListViews gives the
same stretching behaviour as VB6's `Left`/`Width` expressions in `UserDocument_Resize`.

### 2.5 SuspendLayout depth was correct
All 11 nested containers (form, panelMain, tableLayoutPanel, tabControl, 6 tab pages,
tabControlOblig, 2 inner pages, tabControlInstr, 2 inner pages, 4 GroupBoxes) received
`SuspendLayout`/`ResumeLayout` pairs, avoiding the layout-thrash bug that caused an earlier
version to lose controls at runtime.

---

## 3. Challenges

### 3.1 Tab captions are invisible in DOB source
VB6 `SSActiveTabs` persists tab captions in binary `.dob` sections. The text appears in the
file as property values inside `BeginProperty Tab(n)` blocks, but they are formatted differently
from other string properties. Searching for the six tab names without knowing them in advance
required reading large sections of the DOB header. **Solution:** Screenshots provided
ground truth instantly.

### 3.2 Designer.cs file loss after BUILD v1
After the first successful compile, the `UbsPmTradeFrm.Designer.cs` file disappeared from
disk (Cursor workspace sync issue). The DLL was verified in `bin\Release\` but the source file
was gone. **Impact:** A full rewrite was required for v2. **Mitigation adopted:** From this
point on, the memory bank planning documents are detailed enough to reconstruct the
Designer.cs from scratch in a single pass without needing to read the DLL back.

### 3.3 .csproj was not updated in sync with the first rename
The `.csproj` retained `UbsForm1` compile/resx entries AND `UbsFormTemplate` for
`RootNamespace`/`AssemblyName` even after the class file was renamed. This caused two
separate compile errors in BUILD v2. **Root cause:** The rename was done only on the `.cs`
file, not on the four `.csproj` entry points. **Pattern to document:** Renaming a form in a
template project requires changing (a) class file, (b) Designer file, (c) resx file, (d) 
`<Compile>` entries, (e) `<EmbeddedResource>` entry, (f) `<RootNamespace>`,
(g) `<AssemblyName>`, (h) `<DocumentationFile>`.

### 3.4 UbsCtrlDecimal / UbsCtrlDate / UbsCtrlFields references were missing from csproj
The custom control assemblies had been added in the first session but were lost along with the
Designer.cs when the workspace reset. BUILD v2 produced 11 "type or namespace not found" errors
before the `<Reference>` entries were restored. **Pattern:** Any project derived from the
`UbsFormTemplate` template needs the three control references added immediately during the
rename step, before any designer work begins.

### 3.5 grpMetalChar / massa field layout required careful width arithmetic
The two "Масса" fields (`ucdMassa`, `ucdMassaGramm`) needed to share one GroupBox row with
their labels. With an inner tab page width of ~452 px, the layout:
`label1(8..107) + field1(110..209) + label2(220..299) + field2(316..427)` requires explicit
calculation to avoid overlap. **Lesson:** For side-by-side decimal controls within a GroupBox,
pre-calculate pixel positions rather than relying on visual designer.

### 3.6 Tab 3 bottom button positioning with Anchor=BR
`cmdExitOblig` and `cmdApplayOblig` are anchored Bottom+Right, and `lblAccountsOblig` /
`cmdAccountsOblig` are anchored Bottom+Left. With `tabControlOblig` occupying y=6..426 and
tab page height=477, the button strip at y=432 leaves only 39 px below the inner tab control.
This is tight; the tab control height may need a 4–6 px reduction if the form is resized
very small.

---

## 4. Lessons Learned

### L1 — Screenshot verification is mandatory before the first build
For VB6 → .NET conversions, **always read screenshots before writing any designer code**.
DOB source establishes the control inventory; screenshots establish the visual layout.
Attempting to build from source alone guarantees layout errors that require a second cycle.
**Workflow change:** Future forms should have a explicit "screenshot review" checkpoint
between PLAN and BUILD, producing a `plan-[form]-designer-revision.md` as a matter of course.

### L2 — Template project rename is a 8-point checklist
See §3.3 above. Document this as a standard first step in the project's `systemPatterns.md`
so it is never partially applied again.

### L3 — Control-array naming convention: `_0` / `_1` suffix
VB6 `Control(index)` → .NET `Control_index`. For dual-panel forms (buyer/seller, two
instructions, two contracts), always use `_0`/`_1` suffix on both the field declaration and
the `Controls.Add()` calls. Never use separate classes/structs at the designer level —
the suffix pattern keeps `LoadFromParams` and `BuildSaveParams` naturally index-based.

### L4 — UbsFormBase does not expose a virtual Dispose
The designer `Dispose(bool)` override generates a CS1591 XML-doc warning (not an error)
because `UbsFormBase` declares `Dispose` but without an XML comment. This is an upstream
library issue; suppress at the project level with `<NoWarn>1591</NoWarn>` in a future
cleanup pass.

### L5 — Keep plan documents pixel-accurate for GroupBox interiors
For nested container controls (GroupBox inside TabPage inside inner TabControl inside outer
TabPage), pixel coordinates must be relative to the immediate parent. The revision plan's
coordinate tables were written relative to the correct parent in each section, which made
the translation to C# `Location = new Point(x, y)` direct and error-free.

---

## 5. Process Improvements

| # | Improvement | Apply to |
|---|-------------|----------|
| P1 | Add "screenshot review" as a mandatory BUILD pre-step for all VB6 conversions | `memory-bank/systemPatterns.md` |
| P2 | Add 8-point rename checklist to template project documentation | `memory-bank/systemPatterns.md` |
| P3 | Always create `reflection/` + `archive/` directories during VAN initialization | VAN workflow |
| P4 | Document custom control `<Reference>` additions as part of Phase 1 Prep, not after-the-fact | `tasks.md` Phase 1 checklist |
| P5 | Write `plan-*-designer-revision.md` as the default; `plan-*-designer-conversion.md` is the input to the revision, not the final spec | Planning workflow |

---

## 6. Technical Improvements

| # | Improvement | Priority |
|---|-------------|----------|
| T1 | Add `<NoWarn>1591</NoWarn>` to Release config to suppress XML-doc warning on Dispose | Low |
| T2 | Add `PostBuildEvent` to copy DLL to UBS deployment paths once environment is available | Medium |
| T3 | Consider adding `<Nullable>enable</Nullable>` (or at minimum null-guards) for Phase 2 logic | Medium |
| T4 | Validate that `ucpParam (UbsCtrlFields)` `.ReadOnly` property name matches the actual DLL API | High — before Phase 2 |
| T5 | Measure actual pixel widths at runtime for the massa GroupBox row and adjust if overflow observed | Low |

---

## 7. Designer Decisions Made (Reference for Phase 2)

These decisions were locked in during the designer phase and **must not be changed** in
Phase 2 without updating the Designer.cs:

| Decision | Choice made | Rationale |
|----------|-------------|-----------|
| Tab structure | Flat `TabControl` (6 pages), not UserControl-per-tab | Matches existing OP-series pattern |
| Inner tabs | `tabControlOblig` (2 pages) in tabPage3; `tabControlInstr` (2 pages) in tabPage5 | Directly mirrors VB6 `SSActiveTabs` nesting |
| Control-array | `_0`/`_1` suffix | Simple, consistent, index-translatable |
| Bottom strip | `TableLayoutPanel` 3 cols: info / Сохранить / Выход | Same as OP conversions; Accounts button moved to tabPage3 |
| Form size | `ClientSize = (480, 535)` | Matches legacy screenshot proportions |
| Anchor policy | Labels: none; TextBoxes/Combos: TLR; ListView: All; GroupBox: TL+R | Standard UBS form anchoring |
| Hidden controls | `chkNDS`, `chkExport` (Visible=false); `lblAccountsOblig`, `cmdAccountsOblig`, `cmdApplayOblig`, `cmdExitOblig` (Visible=false) | Shown/hidden by runtime logic in Phase 2 |

---

## 8. Next Steps (Phase 2 Entry Point)

Before starting Phase 2 (business logic conversion), the following must be in place:

- [ ] **PLAN:** Create `plan-trade-conversion-goals.md` — full conversion roadmap, channel
  command inventory, DOB → .NET method mapping table
- [ ] **PLAN:** Create `plan-trade-legacy-source-conversion.md` — section-by-section mapping
  of each VB6 procedure to its .NET equivalent method
- [ ] **CREATIVE:** Decide obligations data model (`DataTable` vs `BindingList<T>` vs plain
  `ListViewItem` tags)
- [ ] **CREATIVE:** Decide sub-form strategy (modal dialogs vs embedded panels for contract
  lookup, instruction picker, account picker, object picker)
- [ ] **Phase 2 first step:** Implement `ListKey` + `InitDoc` skeleton and `FillCombos` —
  the minimum needed for an end-to-end channel smoke test

→ Proceed to `/plan plan-trade-conversion-goals.md` to start Phase 2 planning.

---

---

# Addendum: TabIndex Correction Pass

**Date:** 2026-03-24  
**Scope:** `UbsPmTradeFrm.Designer.cs` — TabIndex audit and repair  
**Outcome:** ✅ All TabIndex values corrected, 10 display-only controls get `TabStop=false`, build 0 errors

---

## A1. What Was Discovered

When re-entering the file for this pass, it was immediately clear that the Designer.cs **had been
modified** between sessions. The user had made deliberate improvements to the file independently:

| Original (my write) | Evolved (in file) |
|---------------------|-------------------|
| `txtTradeDate` (TextBox) | `dateTrade` (UbsCtrlDate) |
| `txtDatePost` / `txtDateOpl` | `datePost` / `dateOpl` (UbsCtrlDate) |
| `lblSeller` / `lblBuyer` (Label) | `linkSeller` / `linkBuyer` (LinkLabel) |
| `cmdContract1/2` | `btnContract1/2` |
| `cmdStorage` (Button) | `linkStorage` (LinkLabel) |
| `cmdListInstr_0/1` / `cmdAccount_0/1` (Buttons) | `linkListInstr/linkListInstrSeller` + `linkAccountPay/linkAccountSeller` (LinkLabels) |
| `ucpParam` (UbsCtrlFields) | `ubsCtrlField` |
| Bottom strip anchored `linkLabel` | `linkAccountsOblig` (LinkLabel) inside tabPage3 |
| Form `ClientSize = (480, 535)` | `ClientSize = (676, 623)` (enlarged) |

These are good improvements (UbsCtrlDate for dates, LinkLabel for lookup triggers, larger form).
The lesson: **always read the actual file before editing, even if you wrote it.**

---

## A2. TabIndex Problems Found

Seven categories of error were identified:

| # | Category | Example | Impact |
|---|----------|---------|--------|
| 1 | Duplicate value | `lstViewOblig = 15` AND `cmdAddOblig = 15` | Runtime unpredictable tab order |
| 2 | Form-wide sequential in GroupBox | `txtContractCode1 = 128`, `btnContract1 = 132` | Tab order skips all GroupBox contents |
| 3 | `chkCash_0/1 = 0` | Collides with all labels at 0 in same container | Checkbox focus position undefined |
| 4 | Interactive LinkLabels at 0 | `linkSeller = 0`, `linkBuyer = 0` | Tab skips over clickable links |
| 5 | Display TextBoxes with `TabStop = true` | `txtContractCode1`, `txtKS_0`, `txtRS_0`, etc. | User tabs into read-only fields unnecessarily |
| 6 | Container siblings both at 0 | `grpMetalChar = 0`, `grpMetalCharPost = 0` in same parent | Entry order between the two groups undefined |
| 7 | Bottom buttons 101/102 | Inconsistent with per-container 0-based style | Style issue, not functional |

Total controls changed: **~50 TabIndex assignments** + **10 `TabStop = false` additions**.

---

## A3. What Went Well

### A3.1 Plan-first approach paid off at this scale
With ~110 controls across 12 containers, attempting to apply changes without a documented table
would have risked introducing new conflicts. Writing `plan-tabindex-order.md` first provided
a reference to cross-check each StrReplace against.

### A3.2 Grep verification before build
Running `rg 'TabIndex\s*=\s*[2-9][0-9]+'` after all edits confirmed zero remaining
high numbers before triggering the build. This saved a potential build-debug cycle.

### A3.3 Per-container restart rule eliminates all ambiguity
The rule "TabIndex restarts at 0 inside each container; labels at 0; interactive controls 1,2,3..."
produces unambiguous results even on a form this complex. No lookup tables or global tracking needed.

---

## A4. Lessons Learned

### L1 — Read the file first, every time
Never assume a file matches what was written in a previous session. The file may have been
improved by the user. Reading it first takes < 1 minute and prevents editing the wrong content.

### L2 — WinForms TabIndex is per-container, not per-form
This is a common mistake even among experienced WinForms developers. The VS designer resets
numbering when it regenerates code, which is why regenerated files are always clean. Manual
edits accumulate errors over time. The audit must be container-aware, not just a flat scan.

### L3 — Display-only TextBoxes always need `TabStop = false`
`ReadOnly = true` removes the edit cursor but does NOT remove `TabStop`. An `Enabled = false`
control is already non-tabbable at runtime, but setting `TabStop = false` is still better practice
as it signals intent and makes the property explicit in the designer file.

### L4 — LinkLabel controls need explicit `TabIndex` position
Unlike Labels (which have `TabStop = false` by default), LinkLabel has `TabStop = true` by
default. A LinkLabel set to `TabIndex = 0` competes with labels at 0. All interactive
LinkLabels must receive a proper non-zero position matching their visual slot.

---

## A5. Process Improvement

| # | Improvement | When to apply |
|---|-------------|---------------|
| A1 | Add "TabIndex audit" as mandatory step at end of every Designer.cs write | During BUILD, before first compile |
| A2 | Use `plan-tabindex-order.md` template (problems table + per-container table) | Part of PLAN for any large Designer.cs |
| A3 | Grep `TabIndex\s*=\s*[2-9][0-9]+` as post-edit verification step | After all TabIndex edits |
| A4 | Grep `TabStop` after any display-only TextBox is added (ReadOnly/Disabled) | Whenever ReadOnly or Disabled is set |

---

## A6. Updated Designer Decisions

Two decisions changed from the original designer build due to user improvements:

| Decision | Original | Updated | Reason |
|----------|----------|---------|--------|
| Date fields | `TextBox` (masked) | `UbsCtrlDate` | Correct UBS control for dates |
| Lookup triggers | `Button ("...")` / `Label` | `LinkLabel` | Better UX: clickable inline text, no button chrome |
| Form size | `(480, 535)` | `(676, 623)` | Wider layout accommodates longer field names |

---

## A7. Updated Next Steps

The TabIndex correction completes the designer quality baseline. The Designer.cs is now:
- Correct layout (from screenshot-based revision)
- Correct tab order (from this TabIndex pass)
- 0 compile errors

→ Proceed to `/plan plan-trade-conversion-goals.md` to start Phase 2 planning.
