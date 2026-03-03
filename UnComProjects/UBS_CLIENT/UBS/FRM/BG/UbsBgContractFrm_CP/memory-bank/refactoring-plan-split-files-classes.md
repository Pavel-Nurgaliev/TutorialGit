# Refactoring Plan: Split UbsBgContractFrm into Separate Files/Classes

## Goal

Reduce the size of `UbsBgContractFrm.cs` (~2722 lines) by moving functionality into **separate files and/or classes**, **without changing any behavior** (same public API, same logic, same runtime behavior).

## Current State

- **Main file:** `UbsBgContractFrm.cs` — one large class with constants, fields, constructor, command handlers (ListKey, InitDoc, ReReadDoc), combo/UI helpers, event handlers, rates logic.
- **Existing partial/related:** `UbsBgContractFrm.Designer.cs` (generated), `ComboItem.cs` (separate class in same namespace).
- **Regions in main file:** Блок констант, Блок объявления переменных, Обработчики событий кнопок, Обработчики команд IUbs интерфейса (very large, ~1920 lines), then unregioned event handlers and rates.

---

## Strategy: Partial Classes First (Zero Behavior Change)

Use **partial classes** so that `UbsBgContractFrm` is defined across several `.cs` files. The type remains one class; only the source is split. No new types, no changed call sites, no risk to functionality.

After that, optionally extract **constants** and **static helpers** into separate classes if you want “real” separate classes in addition to file organization.

---

## Proposed File Layout

### 1. Keep in main file: `UbsBgContractFrm.cs`

**Responsibilities:** Core identity of the form.

- **Namespace, partial class declaration**, same base class.
- **All instance fields** (m_command, m_idContract, m_paramIn, m_paramOut, etc.) — keep in one place so state is easy to find.
- **Constructor** (`UbsBgContractFrm()`).
- **Command registration:** `m_addCommand()`, `CommandLine()`.
- **Minimal button handlers** if you prefer to keep them here (e.g. `btnExit_Click`, `btnSave_Click`), or move to partial “EventHandlers” (see below).

Keep this file to roughly **100–200 lines** (fields + constructor + 2–3 methods). Everything else moves to partials or helper classes.

---

### 2. New partial: `UbsBgContractFrm.Constants.cs`

**Content:** The entire `#region Блок констант` (all `private const` and the two `readonly DateTime` if you prefer them with constants).

- Same namespace, `public partial class UbsBgContractFrm`.
- Only constants (and optionally `MinDate`/`MaxDate`).
- **No behavior change:** they remain private members of the same class.

**Approximate size:** ~50 lines.

---

### 3. New partial: `UbsBgContractFrm.ListKey.cs`

**Content:** ListKey and its helpers (already refactored).

- `ListKey()`
- `ResetListKeyState()`
- `TryApplyEditOrCopyListKeyParams()`
- `TryApplyAddByFrameContractListKeyParams()`
- `TryApplyAddByFramePrepareListKeyParams()`
- `CheckParamForClose()`

**Approximate size:** ~120 lines.

---

### 4. New partial: `UbsBgContractFrm.InitDoc.cs`

**Content:** Init and command-based setup.

- `InitDoc()`
- `RunBgContractInitAndParseOutput()`
- `SetControlsStateByCommand()`
- `LoadFrameContractForAddOrPrepare()`
- `ApplyNextPayFeeDatesAndEditLinks()`
- `InitDocForCopyCommand()`
- `RunUbsChannelFunction()` (used by InitDoc, ReReadDoc, and others — one canonical place)
- `SetBonusCtrlsBonusState()`
- `SetTabsEnabled()`

**Approximate size:** ~550 lines.

---

### 5. New partial: `UbsBgContractFrm.ComboBoxes.cs`

**Content:** Combo and list initialization/helpers.

- `InitCmsAccounts()`
- `InitComboBoxes()`
- `SetComboValueText()`
- `FillComboText()`
- `SetComboText()`
- `SetComboById()`
- `ResetKindComboBox()`
- `GetArrayForCombo()`
- `SetComboItem()`
- `InitComboBox()`
- `MakeKvpList(object[,] ...)`
- `MakeKvpList(object[] ...)`
- `SetKindComboBoxByFrame()`
- `SetEnableCombos()`

**Approximate size:** ~320 lines.

---

### 6. New partial: `UbsBgContractFrm.ReReadDoc.cs`

**Content:** ReRead document and related UI/data application.

- `ReReadDoc()`
- `ReReadDocLoadAndValidateContract()`
- `ReReadDocApplyContractMainData()`
- `ReReadDocApplyAgentGuarantListAndModel()`
- `ReReadDocApplyPayFeeGuarantRiskAndCombos()`
- `ReReadDocApplyBonusCoverAndPayOrderCombos()`
- `ReReadDocApplyEditNonDraftVisibility()`
- `ReReadDocPreamble()`
- `ReReadDocFinalize()`
- `FilllstAccounts()`
- `UpdateCover()`
- `CorrectFormDateCtrls()`
- `SetBonusCtrlsState()`
- `CurrencyGarantEnabled()`
- `FillLvwGuarant()`
- `SetInfoByFrameContract()`

**Approximate size:** ~530 lines.

---

### 7. New partial: `UbsBgContractFrm.GuarantAndCommands.cs`

**Content:** Guarant list and command-state logic.

- `GuarCmdState()`
- `EnabledCmdControl()`
- `IsCmbValutaEnabled()` (if present)
- Any other small helpers that only serve command/guarant state.

**Approximate size:** ~80 lines.

---

### 8. New partial: `UbsBgContractFrm.EventHandlers.cs`

**Content:** All UI event handlers that are not in the Designer.

- `linkFrameContract_LinkClicked`, `btnFrameContractDel_Click`
- `linkModel_LinkClicked`
- `linkAgent_LinkClicked`, `btnAgentDel_Click`
- `linkPrincipal_LinkClicked`
- `linkBeneficiar_LinkClicked`
- `ubsBgContractFrm_Ubs_ActionRunBegin`
- `linkGarant_LinkClicked`
- `linkPreviousContract_LinkClicked`
- `btnManualBenificiar_Click`
- Any other `*_Click`, `*_LinkClicked`, `*_ActionRunBegin` handlers.

**Approximate size:** ~400 lines (depends on how many handlers and their length).

---

### 9. New partial: `UbsBgContractFrm.Rates.cs`

**Content:** Rates tree and rate commands.

- `InitTrvRates()`
- `btnAddRate_Click()`
- `GetRate()`
- Any other rate-specific helpers (e.g. `btnDelRate_Click`, `btnEditRate_Click` if present).

**Approximate size:** ~90 lines.

---

## Summary Table

| File | Purpose | Approx. lines |
|------|---------|----------------|
| `UbsBgContractFrm.cs` | Fields, constructor, `m_addCommand`, `CommandLine` | ~150 |
| `UbsBgContractFrm.Constants.cs` | All constants (and optionally MinDate/MaxDate) | ~50 |
| `UbsBgContractFrm.ListKey.cs` | ListKey + param application + CheckParamForClose | ~120 |
| `UbsBgContractFrm.InitDoc.cs` | InitDoc and init/copy/frame helpers, RunUbsChannelFunction, SetTabsEnabled | ~550 |
| `UbsBgContractFrm.ComboBoxes.cs` | Combo/list init and helpers, MakeKvpList, SetEnableCombos | ~320 |
| `UbsBgContractFrm.ReReadDoc.cs` | ReReadDoc and all ReReadDoc* + FilllstAccounts, SetInfoByFrameContract, etc. | ~530 |
| `UbsBgContractFrm.GuarantAndCommands.cs` | GuarCmdState, EnabledCmdControl | ~80 |
| `UbsBgContractFrm.EventHandlers.cs` | Link/button and Ubs_ActionRun event handlers | ~400 |
| `UbsBgContractFrm.Rates.cs` | InitTrvRates, GetRate, btnAddRate_Click | ~90 |
| **Total** | | **~2390** (rest in Designer) |

Main `.cs` goes from ~2722 lines to ~150; the rest is spread across 8 new partial files. **No functionality change:** same class, same members, only split across files.

---

## Implementation Order

1. **Create `UbsBgContractFrm.Constants.cs`**  
   - New file, same namespace, `public partial class UbsBgContractFrm { #region Блок констант ... #endregion }`.  
   - Remove that region from `UbsBgContractFrm.cs`.  
   - Build and test.

2. **Create `UbsBgContractFrm.ListKey.cs`**  
   - Move ListKey + the five helper methods.  
   - Remove them from main file.  
   - Build and test.

3. **Create `UbsBgContractFrm.ComboBoxes.cs`**  
   - Move all combo-related methods (list above).  
   - Remove from main file.  
   - Build and test.

4. **Create `UbsBgContractFrm.InitDoc.cs`**  
   - Move InitDoc and all InitDoc-related methods including `RunUbsChannelFunction`, `SetTabsEnabled`, `SetBonusCtrlsBonusState`.  
   - Remove from main file.  
   - Build and test.

5. **Create `UbsBgContractFrm.ReReadDoc.cs`**  
   - Move ReReadDoc and all ReReadDoc* and related methods (FilllstAccounts, UpdateCover, CorrectFormDateCtrls, SetBonusCtrlsState, CurrencyGarantEnabled, FillLvwGuarant, SetInfoByFrameContract).  
   - Remove from main file.  
   - Build and test.

6. **Create `UbsBgContractFrm.GuarantAndCommands.cs`**  
   - Move GuarCmdState, EnabledCmdControl, IsCmbValutaEnabled.  
   - Remove from main file.  
   - Build and test.

7. **Create `UbsBgContractFrm.EventHandlers.cs`**  
   - Move all event handlers (link*, btn*, ubsBgContractFrm_Ubs_ActionRunBegin, etc.).  
   - Remove from main file.  
   - Build and test.

8. **Create `UbsBgContractFrm.Rates.cs`**  
   - Move InitTrvRates, GetRate, btnAddRate_Click (and any other rate handlers).  
   - Remove from main file.  
   - Build and test.

After each step, run the application and test: open form (Add/Edit/Copy/Prepare), change combos, save, links, rates, etc., to confirm no regressions.

---

## Optional: Extract Real Separate Classes (Still No Logic Change)

If you want **separate types** (not only partials), you can do the following **after** the partial split, without changing behavior:

### A. Static constants class

- Add `UbsBgContractFrmConstants.cs` with a **static** class (e.g. `internal static class UbsBgContractFrmConstants`).
- Move all const strings and command names there as `public const` or `internal const`.
- In the form (and partials), replace `MsgContractNotSelected` with `UbsBgContractFrmConstants.MsgContractNotSelected` (and same for all constants).
- Remove the constants region from the partial Constants file (or leave it empty and delete later).

**Result:** One small class (~50 lines), form references stay the same logically.

### B. Static combo helpers class

- Add `UbsBgContractFrmComboHelper.cs` with a **static** class.
- Move `MakeKvpList(object[,] ...)`, `MakeKvpList(object[])`, and optionally `GetArrayForCombo()` (if it only uses its parameter).
- Form (and partials) call `UbsBgContractFrmComboHelper.MakeKvpList(...)` instead of `MakeKvpList(...)`.
- Remove those methods from `UbsBgContractFrm.ComboBoxes.cs`.

**Result:** Pure helpers in a separate class; behavior unchanged.

---

## What Stays in One Place

- **Instance fields** — keep all in `UbsBgContractFrm.cs` (or a single partial like `UbsBgContractFrm.Fields.cs`) so the form’s state is not scattered.
- **Designer file** — do not move controls or designer-generated code; only split the hand-written logic.
- **Base class and interface** — `UbsBgContractFrm : UbsFormBase` and any IUbs implementation stay as they are; partials do not change that.

---

## Benefits

- **Main file** becomes short and readable (~150 lines: fields + constructor + 2–3 methods).
- **Navigation** by feature: ListKey, InitDoc, ComboBoxes, ReReadDoc, EventHandlers, Rates.
- **No behavior or API change:** same class, same members, same behavior.
- **Safe steps:** one partial (or one class) at a time, build and test after each step.
- **Optional:** constants and static helpers can later be moved to separate classes for clearer boundaries.

---

## Notes

- Every new partial file must use the **same namespace** (`UbsBusiness`) and **same class name** (`public partial class UbsBgContractFrm : UbsFormBase`). Include the same `using` directives as needed (each partial can have its own usings).
- If a method in partial A calls a method in partial B, no change is needed — they are the same class.
- The Designer file remains the single place for `InitializeComponent()` and control declarations; do not duplicate or move those.
