# Memory Bank: Active Context

## Current Focus

**Main goal:** VB6 `Contract.dob` → .NET **`UbsPsContractFrm`**.

**Plans:** `plan-ubspcontractfrm-conversion.md` (phases A–F). **Phase B:** `plan-phase-b-main-tab.md`. Summary: `plan-conversion-goals-revised.md`.

## Status

- **CREATIVE (Phase A):** Complete — channel + architecture (`prefix` control naming in architecture §7).
- **BUILD (Phase A–C, F):** `InitDoc`, main/add-fields UI, commission tab **`EnableSum`** parity (`UbsPsContractFrm.Commission.cs`), `CommandLine`/`ListKey`/`Load` + **`m_addFields`**; channel doc **§7**; **Phase F** — designer tab order, `UbsPsContractFrm.Keys.cs` (Esc between tabs), `KeyPreview` / `AcceptButton`.
- **Next:** **Phase D** (add-fields rules), **Phase E** (save incl. `Метод расчета комиссии с получателя`); host **`RetFromGrid`** when known.

## Latest Changes

- **2026-04-02:** `/build` **Phase F** — tabindex/label `TabStop`, `Keys.cs`, reflection `reflection-phase-f-ubspcontractfrm.md`.
- **2026-04-02:** `/build` **Phase C** — `EnableSumCommissionControls`, combo events, `InitDoc` hook; restored `Load`/`m_addFields`.
- **2026-04-02:** `/build` — `MSBuild` Debug + Release verified; Release **CS1591** resolved (XML on ctor + `Dispose`).
- **2026-04-02:** CREATIVE — `creative-phase-b-main-tab.md` (InitDoc hybrid, mapping, stubs).
- **2026-04-02:** PLAN — `plan-phase-b-main-tab.md` (B.1–B.7).
