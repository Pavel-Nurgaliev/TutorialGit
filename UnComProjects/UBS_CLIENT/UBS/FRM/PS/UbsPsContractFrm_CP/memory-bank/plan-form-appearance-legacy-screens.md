# PLAN: Form Appearance — Legacy Screens

**Target file:** `UbsPsContractFrm/UbsPsContractFrm.Designer.cs`  
**Reference folder:** `legacy-form/screens/`  
**Date:** 2026-04-02

---

## 1. Goal

Align the .NET form’s visible layout (title, tabs, control sizes, anchors) with screenshots stored in `legacy-form/screens/`.

---

## 2. Current status

The `legacy-form/screens/` directory is **not present** in the repository yet. When images are added, list them below and map each to Designer regions.

| Screenshot file | What it shows | Designer notes |
|-----------------|---------------|----------------|
| *(add rows)* | | |

---

## 3. Process

1. Add PNG (or agreed format) captures from the VB6 runtime to `legacy-form/screens/`.
2. For each image: note form `Text`, tab captions, multiline fields, grid vs add-fields placement.
3. Update `UbsPsContractFrm.Designer.cs` to match; keep `panelMain` and template height rules per `.cursor/rules/designer-rules.mdc`.
4. Record completion in `memory-bank/progress.md` and CREATIVE doc if non-trivial trade-offs.

---

## 4. Related

- `plan-conversion-goals-revised.md` — Phase 3 appearance pass  
- `projectbrief.md` — success criteria
