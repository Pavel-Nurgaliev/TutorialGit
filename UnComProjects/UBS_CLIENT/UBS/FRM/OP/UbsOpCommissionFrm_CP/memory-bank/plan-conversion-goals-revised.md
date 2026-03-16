# PLAN: Conversion Goals — Revised (Main Goal)

**Date:** 2026-03-14  
**Issue:** Current implementation does not address the main goal of the task.

---

## 1. MAIN GOAL (What We Actually Need)

**Convert the VB6 Commission UserDocument to .NET Framework 2.0 Windows Form** — same as UbsBgContractFrm converted VB6 forms to .NET.

| Role | Source | Target |
|------|--------|--------|
| **Legacy source** | `Commission/Commission_ud.dob` (VB6 UserDocument) | — |
| **Conversion target** | — | `UbsOpCommissionFrm` (.NET WinForm) in this project |

**Current state:** UbsOpCommissionFrm is a **template skeleton** (CommandLine, ListKey stubs, one example field). It does **not** implement the Commission UI, logic, or channel behavior from the legacy VB6.

---

## 2. WHAT WE DID vs WHAT WE NEED

| Done | Main goal? |
|------|------------|
| Constants partial (LoadResource, FieldNameExample) | ❌ No — supporting/prep |
| Channel contract doc | ❌ No — supporting/prep |
| Legacy source role documented | ❌ No — role was wrong |

| Needed | Main goal? |
|--------|------------|
| **Convert Commission_ud → UbsOpCommissionFrm** | ✅ **Yes** |

---

## 3. LEGACY SOURCE INVENTORY (Commission_ud.dob)

**Location:** `Commission/Commission_ud.dob` (VB6 UserDocument)

### UI
- **Tab 1 (main):** txbName (Наименование), txbDesc (Описание), labels
- **Tab 2:** ucpParam (UbsControlProperty — dynamic add-fields)
- **Buttons:** btnExit ("Выход"), btnSave ("Сохранить")
- **Info:** UbsInfo ("Данные сохранены!")

### Channel
- **LoadResource:** `"VBS:UBS_VBD\OP\Commission.vbs"` (VBS script; .NET will use ASM equivalent)
- **Commands:** Get_Data (load), Com_Save (save)
- **Params:** Действие, Наименование, Описание, Идентификатор

### Logic
- **InitDoc:** Loads data for EDIT (ID), clears for ADD
- **btnSave_Click:** DDX.UpdateData, CheckData, DDX.ChangeMembersValue → objParamIn (Наименование, Описание), UbsChannel.Run "Com_Save"
- **CheckData:** Validate txbName not empty
- **Commands:** ADD, EDIT

### VB6 → .NET Mapping
- VB.UserDocument → UbsFormBase (form, not UserDocument)
- UBSChild → IUbs (CommandLine, ListKey)
- SSActiveTabs → TabControl or similar
- UbsDDXControl → manual binding or UbsFormField collection
- UbsControlProperty → m_axUbsControlProperty or equivalent
- UbsChannel.LoadResource → IUbsChannel.LoadResource (ASM path for .NET)

---

## 4. CONVERSION TASK (Main Goal)

**Task:** Convert Commission_ud (VB6) to UbsOpCommissionFrm (.NET WinForm)
- Implement tabs, txbName, txbDesc, ucpParam, InitDoc, btnSave (Com_Save), Get_Data, CheckData
- Map VB6 channel/resource to .NET ASM resource
- Follow UBS form pattern (UbsFormBase, IUbs, panelMain, m_addFields)
- Preserve Russian text, encoding (Windows-1251)

**Complexity:** Level 3 (Intermediate) — similar to BG_CONTRACT single-form conversion

---

## 5. REVISED ROADMAP

### Phase 1 (Done — Prep)
- [x] Constants partial
- [x] Channel contract doc (update for Commission_ud specifics)

### Phase 2 (Main Goal — Conversion)
- [ ] Add Commission/ to project as source reference (like BG_CONTRACT)
- [ ] Implement UbsOpCommissionFrm UI: TabControl, txbName, txbDesc, ucpParam, btnSave, btnExit
- [ ] Implement InitDoc: Get_Data for EDIT, clear for ADD
- [ ] Implement ListKey: pass ID from list selection
- [ ] Implement btnSave: CheckData, Com_Save, params Наименование/Описание
- [ ] Update LoadResource to OP Commission ASM path
- [ ] Add constants for all Commission params, commands, messages

### Phase 3 (Post-Conversion)
- [ ] Split partials when form grows
- [ ] Refactoring plans for large methods

---

## 6. CORRECTED PLAN RELATIONSHIPS

| Plan | Role |
|------|------|
| **plan-conversion-goals-revised.md** (this) | Defines main goal: conversion Commission_ud → UbsOpCommissionFrm |
| **plan-legacy-source-conversion.md** | Update: legacy source = Commission/Commission_ud.dob; target = UbsOpCommissionFrm |
| **plan-ubsobgoals-apply-to-op-commission.md** | Supporting goals (constants, partials) — applied during/after conversion |
| **plan-implementation-vb6-to-dotnet.md** | Implementation differences from last 2 commits; checklist for future VB6 → .NET conversions |

**Main goal:** Convert Commission_ud to UbsOpCommissionFrm. Constants and docs are preparation; the conversion is the primary deliverable.
