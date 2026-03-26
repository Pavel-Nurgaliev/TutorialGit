# CREATIVE: `UbsPmTradeFrm` — event-handler regions + support extraction

**Scope:** Reorganize `UbsPmTradeFrm.cs` so WinForms entry points live in clear `#region` blocks, and non-handler logic moves out of the main partial file via small support types or partials.

**Non-goals:** Changing behaviour, renaming public API, or upgrading framework.

**Constraints:** .NET **2.0** (no LINQ), `TargetFrameworkVersion` unchanged; explicit string literals for channel command names and `ParamIn`/`ParamOut` keys; `Ubs_ShowError` for handler errors; `object[row, column]` for 2D channel arrays.

---

## 1. Problem statement

Today the form mixes:

- Declarations and ctor.
- Some handlers inside regions (`btn*`, IUbs delegates).
- Other handlers **below** long private methods (`chkComposit_CheckedChanged`, `chkCash_Click`).
- Support code interleaved: init (`InitDoc`), combo/BIK fill, obligation cleanup, payment matrix fill/clear, static combo/matrix helpers.

This grows hard to navigate as legacy handlers are ported.

---

## 2. Event-handler regions — options

| Option | Description | Pros | Cons |
|--------|-------------|------|------|
| **A — Single mega-region** | One `#region Обработчики событий` for all WinForms hooks | Simple | Becomes huge |
| **B — Grouped by control kind (recommended)** | Separate regions: кнопки, чекбоксы, списки/гриды, комбо/текст (as needed) | Easy to find; scales with tabs | Must agree on naming |
| **C — Grouped by tab** | Region per `TabPage` | Matches UI layout | Handlers for shared controls (toolbar) awkward |

**Decision:** **Option B.** Keep **IUbs command registration** (`m_addCommand`, `CommandLine`, `ListKey`) in its own region — not WinForms events, but same “thin entry point” discipline.

**Suggested region order (top → bottom of class body, after ctor):**

1. `Блок объявления переменных` (existing).
2. `Обработчики команд IUbs` (existing pattern).
3. `Обработчики событий — кнопки`.
4. `Обработчики событий — чекбоксы` (and similar toggles).
5. `Обработчики событий — списки` / `LinkLabel` / etc. as ported.
6. *(Later)* `Обработчики событий — комбо и поля ввода` if many `SelectedIndexChanged` / `Validated`.

**Handler body rule:** prefer **thin** handlers: guard clauses, call 1–2 private/support methods, `try`/`catch` → `Ubs_ShowError` where appropriate.

---

## 3. Support extraction — options

| Option | Description | Pros | Cons |
|--------|-------------|------|------|
| **D — Static utility classes** | e.g. `UbsPmTradeComboUtil`, `UbsPmTradeMatrixUtil` with static methods | No lifecycle; easy to test; no form reference | Only for stateless helpers |
| **E — Partial class files** | `UbsPmTradeFrm.Payment.cs`, `UbsPmTradeFrm.Init.cs` — same type, split files | No new public types; IDE navigates by file | Still one large type in metadata |
| **F — Instance helper with form/channel ref** | e.g. `UbsPmTradePaymentSupport` holding `UbsPmTradeFrm` or `IUbsChannel` + controls | Encapsulates control-heavy flows | Coupling risk if boundary unclear |

**Decision (layered):**

| Layer | Home | Examples |
|-------|------|----------|
| Stateless | **Static util class(es)** in same assembly | `FillComboFrom2DArray`, `SetComboByKey`, `GetMatrixCellString`, `GetMatrixCellInt`; `IsObjectParamPart2Key` with `prefix` parameter or shared const type |
| Orchestration + channel glue | **Form** (or partial `UbsPmTradeFrm.*.cs`) | `InitDoc`, `FillCombos`, `FillOurBIK`, `LockUiOnWasOperation` |
| Control-bound payment / obligation UI | **Form partial** first; promote to **F** only if a second consumer appears | `FillControlInstrPayment`, `ClearPayment`, `GetInstrLink`, `GetInstrAccountLink`, `RemoveReverseObligationListItems`, `ClearObligObjectParamsPart2` |

**Naming:** util classes live next to the form project, namespace `UbsPmTradeFrm`, file names like `UbsPmTradeComboUtil.cs` — register in `.csproj`.

---

## 4. Dependencies and visibility

- Util methods take **explicit parameters** (`ComboBox`, `UbsParam`, `string keyParam`, `Array`) — no hidden reads of `m_*` form fields.
- Form keeps **fields** (`m_idTrade`, `m_paramOblig`, `m_suppressCompositEvent`, …) on the form; helpers that need them either stay on the form or receive `Dictionary<string, object>` / callbacks passed from the form.
- **Constants:** user-facing strings stay in `UbsPmTradeFrm.Constants.cs`; channel keys/commands use string literals at call sites per style rule; shared non-UI literals (e.g. obligation key prefix) can live on Constants or a small internal static class if both form and util need them.

---

## 5. Channel contract (support code only)

Support extraction **does not** add new `.Run` calls by itself. When moving code, **preserve** existing command names and `ParamIn`/`ParamOut` keys; update the main channel creative / contract doc if any call moves to a new type (same strings, new location).

---

## 6. Migration order (incremental)

1. Add missing **event regions** and **move** existing handlers into them (no logic change).
2. Extract **static** combo + matrix helpers to util class(es); update call sites.
3. Optionally split form into **partial files** by concern (Init/Combos, Payment, Obligations).
4. Introduce instance **F** only where a partial file still exceeds a comfortable size or logic is duplicated.

---

## 7. Implementation checklist

- [x] All WinForms handlers under named `#region` blocks per §2.
- [x] IUbs handlers remain in `Обработчики команд IUbs`.
- [x] Static helpers moved to util class(es) with no form instance dependency.
- [x] New `.cs` files added to `UbsPmTradeFrm.csproj`.
- [x] Build verified on **.NET 2.0** toolchain; no LINQ or 3.5+ APIs.
- [ ] Channel contract doc updated if any `Run`/param usage moves file (optional same sprint).

---

## 8. Open points (defer to other creative docs)

- Tab-level layout and obligation **data model** remain in `creative-trade-*` architecture docs.
- This doc only governs **structure** of `UbsPmTradeFrm` and small support types.
