# PLAN: Implementation Differences — VB6 to .NET Form Conversion

**Purpose:** Capture implementation patterns and corrections observed in the last two commits (Commission form conversion) for reuse in future VB6 → .NET form conversions.  
**Date:** 2026-03-16  
**Source commits:** `8961bb1` (Implemented form), `8cd576e` (Changed several functions for adoption to .NET form)

---

## 1. Commit Summary

| Commit   | Message | Scope |
|----------|---------|--------|
| **8961bb1** | [Pavel-Nurgaliev] Implemented form. | Initial conversion: legacy Commission_ud.dob reference, Constants partial, Designer (tabs, txbName/txbDesc, ubsCtrlAddFields), form logic (InitDoc, ListKey, CommandLine, CheckData, btnSave), Load → InitDoc, m_addFields (UbsFormField). |
| **8cd576e** | [Pavel-Nurgaliev] Changed several functions for adoption to .NET form | Naming, captions, removal of Form Load init and UbsFormField registration; InitForm call; channel API usage (UbsChannel_* / UbsParam). |

---

## 2. Implementation Differences (Adoption to .NET)

These are the **changes made after the first implementation** to better align with .NET and the existing UBS form infrastructure. Apply as a checklist for future conversions.

### 2.1 Control Naming

| VB6 / Initial .NET | Adopted .NET | Notes |
|-------------------|--------------|--------|
| `txbName`, `txbDesc` | `txtName`, `txtDesc` | Prefer `txt*` for TextBox controls in .NET Designer and code. Update all references (Designer, event handlers, InitDoc, CheckData, Focus). |

**Checklist:** After mapping VB6 controls to .NET, decide naming convention (`txt*` vs `txb*`) and apply consistently; rename in Designer first, then code.

### 2.2 Form and Tab Captions

| Element | Initial | Adopted | Notes |
|---------|---------|---------|--------|
| Form `Text` | "Добавить новую" | "Комиссия" | Form title = form purpose; "Add new" can be set in code by mode if needed. |
| Tab 1 (main) | "Комиссия" | "Основные" | Or keep "Комиссия" to match legacy; document choice. |
| Tab 2 (add fields) | "Набор параметров" | "Доп. поля" | Shorter caption for .NET UI. |

**Checklist:** Define form title and tab captions; align with legacy screens (see plan-form-appearance-legacy-screens.md) or product naming.

### 2.3 Initialization Flow (Form Load vs Command Flow)

| Approach | Commit | Description |
|----------|--------|-------------|
| Form Load | 8961bb1 | `this.Load += UbsOpCommissionFrm_Load`; in handler: if not initialized → InitDoc(), ubsCtrlAddFields.Refresh(), set m_isInitialized. |
| Command flow only | 8cd576e | No Form Load handler. InitDoc() and Refresh() are called from ListKey (and when opening for Edit). |

**Checklist:** Prefer **no Form Load** for InitDoc when form is driven by IUbs commands (CommandLine, ListKey). Initialize in ListKey (or equivalent) so server/form contract controls when data is loaded.

### 2.4 Field Registration (UbsFormField / m_addFields)

| Approach | Commit | Description |
|----------|--------|-------------|
| With m_addFields | 8961bb1 | `m_addFields()` in constructor: `base.IUbsFieldCollection.Add(ParamName, new UbsFormField(txbName, "Text"))` for each bound control. |
| Without m_addFields | 8cd576e | m_addFields() and UbsFormField registration for main fields removed. Binding done explicitly in InitDoc / btnSave. |

**Checklist:** If the base form does not require UbsFormField for these controls, **omit** m_addFields for simple text fields; bind in InitDoc and save in button handler. Keep UbsCtrlFieldsSupportCollection for add-fields control.

### 2.5 Channel API Usage

| Usage | Before (8961bb1) | After (8cd576e) |
|-------|-------------------|------------------|
| Run action | `base.IUbsChannel.Run(GetDataAction)` | `base.UbsChannel_Run(GetDataAction)` |
| Param in | `base.IUbsChannel.ParamIn(ParamId, m_id)` | `base.UbsChannel_ParamIn(ParamId, m_id)` |
| Params out | `base.IUbsChannel.ExistParamOut(ParamName)`, `base.IUbsChannel.ParamOut(ParamName)` | `new UbsParam(base.UbsChannel_ParamsOut)` then `paramOut.Contains(ParamName)`, `paramOut.Value(ParamName)` |
| Init before load | — | `base.IUbsChannel.Run("InitForm")` before Get_Data (for add-fields init). |

**Checklist:** Use **base.UbsChannel_Run** and **base.UbsChannel_ParamIn** where provided by base class; use **UbsParam** wrapper for params out (Contains, Value). Add **InitForm** (or equivalent) run before first data load if server requires it for dynamic fields.

### 2.6 Description Field (Multiline vs Single-Line)

| Version | Designer |
|---------|----------|
| 8961bb1 | txbDesc: Multiline = true, ScrollBars = Vertical, Anchor = All, Height ~60. |
| 8cd576e | txtDesc: single-line (Height 20), no Multiline/ScrollBars/Anchor. |

**Checklist:** Match legacy: if legacy has multi-line "Описание", keep **Multiline**, **ScrollBars**, and **Anchor** in .NET; if product prefers single-line, document and use single-line.

### 2.7 Empty String Literals

| Before | After |
|--------|--------|
| `txbName.Text = ""` | `txtName.Text = string.Empty` |

**Checklist:** Prefer `string.Empty` in C# for empty string literals.

### 2.8 File Encoding (BOM)

Designer.cs was changed from `using System;` (no BOM) to `using System;` (with BOM). Optional for consistency with project standards.

---

## 3. Reusable Checklist for Future VB6 → .NET Conversions

1. **Constants:** Put LoadResource, commands, params, messages in a Constants partial (e.g. `*.Constants.cs`).
2. **Control names:** Choose convention (e.g. `txt*` for TextBox) and apply in Designer + all code.
3. **Captions:** Form title and tab names — match legacy or product; document in plan-form-appearance-legacy-screens.md if needed.
4. **Initialization:** Prefer command-driven init (ListKey) over Form Load for IUbs-driven forms; add InitForm (or equivalent) run if server requires it.
5. **Field binding:** Use UbsFormField only if base form requires it; otherwise bind in InitDoc and in save handler.
6. **Channel API:** Use base.UbsChannel_Run, base.UbsChannel_ParamIn, and UbsParam for params out.
7. **Data types:** Use `string.Empty`, explicit conversions (e.g. Convert.ToString) for channel params.
8. **Legacy reference:** Keep VB6 source (e.g. Commission_ud.dob) in project as reference; formatting-only changes are acceptable.

---

## 4. Relation to Other Plans

| Plan | Role |
|------|------|
| **plan-conversion-goals-revised.md** | Main conversion roadmap (Commission_ud → UbsOpCommissionFrm). |
| **plan-legacy-source-conversion.md** | Legacy source and target roles. |
| **plan-form-appearance-legacy-screens.md** | Form/tab captions and control layout vs legacy screens. |
| **plan-implementation-vb6-to-dotnet.md** (this) | Implementation differences and checklist for future VB6 → .NET conversions. |
