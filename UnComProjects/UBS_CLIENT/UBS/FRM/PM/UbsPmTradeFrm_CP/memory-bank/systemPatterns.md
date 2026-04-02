# Memory Bank: System Patterns

## Architecture

- **Legacy → .NET conversion:** Phased approach per OP reference conversions:
  - Phase 1 (Prep): Constants partial, channel contract doc, add missing assembly references
  - Phase 2 (Conversion): UI designer, InitDoc (CmdEdit/CmdAdd), ListKey/CommandLine, channel handlers, Save
  - Phase 3 (Post): Partials split when file grows, Reflection doc, systemPatterns update
- **Form pattern:** `UbsFormBase` (or equivalent); IUbs (`ListKey`/`CommandLine` for init params); `TabControl` for 6-tab layout; bottom strip (Save, Exit, Info).

## Conventions (from OP Reference Conversions)

- All user-facing strings (MessageBox, form title, info messages) go in a **Constants partial** (e.g. `UbsPmTradeFrm.Constants.cs`). No magic strings in code-behind (except channel commands and param keys which use explicit literals).
- **Channel contract** documented in `memory-bank/` — covers LoadResource, all `.Run` commands, and In/Out param keys. Code uses explicit literal strings for commands and params (not constants).
- **Progress** logged in `progress.md` with date and phase; **tasks** in `tasks.md` with checkboxes and links to plan/creative docs.

## Patterns in Use

- **Explicit data binding:** Replace VB6 DDX with `LoadFromParams(paramOut)` and `BuildSaveParams()` for form↔channel param mapping.
- **ListKey for list-opened forms:** `ListKey` handler receives the ID (`RSIdent(0) = ID_TRADE`) and run param (`strRunParam`). `InitDoc` uses it for `GetOneTrade` (`CmdEdit`) or default init (`CmdAdd`).
- **Tab 5 payment (Оплата):** Sub-tabs **Покупатель (0)** / **Продавец (1)**; RS/KS → **`UbsCtrlAccount`**. **TabIndex** is **per `TabPage`**: KS **4**, RS **6**, `TabStop=false` when display-only — see `memory-bank/plan-tabindex-order.md` §11 and `memory-bank/creative/creative-trade-account-control-and-indexes.md`.
- **InitDoc pattern:**
  1. FillCombos — call `TradeCombo_FillPM` and populate all ComboBox controls
  2. `FillOurBIK` — get own BIK for payment instruction validation
  3. If CmdEdit: call `GetOneTrade`, populate all fields via `LoadFromParams`, fill obligation list
  4. If CmdAdd: set default DDX values, default currencies, default dates
  5. Apply enabled/disabled states based on mode and trade type
  6. If CmdEdit and `Was_Operation=true`: lock all editing (disable Save, all panels)
- **Save pattern (cmdSave_Click):**
  1. Validate all fields (`CheckData`, `CheckDatesOblig`)
  2. Build params: `ID_TRADE`, obligation array, instruction arrays, DDX changed fields, AddFields changes
  3. Call `ModifyTrade`; on success show info, close form, signal parent to refresh
- **ComboBox data binding:** Use `KeyValuePair<int, string>` + `DataSource` (align with UbsOpBlankFrm/UbsOpRetoperFrm). Fill from channel 2D output **`object[row, column]`** with combo rows **`[n, 2]`**: id at **`[r, 0]`**, text at **`[r, 1]`** (see `FillComboFrom2DArray`).
- **Array parameters (Variant arrays):** VB6 used 2D `Variant` with index order that may differ from .NET. Server returns **`object[row, column]`**; map legacy `(i, j)` carefully (e.g. instruction strip: VB `varOplata(k, 0)` → .NET `[0, k]`). Channel contract / `techContext.md` defines field order per param.
- **Sub-form integration:** VB6 opens child windows via `IParent.LoadForm`. .NET equivalent TBD during CREATIVE phase (may use modal dialogs or inline panels).
- **UbsCtrlDecimal for money fields:** All `UbsControlMoney` instances → `UbsCtrlDecimal` (same as OP forms).
- **ListView for obligations:** `System.Windows.Forms.ListView` with ColumnHeaders. 7 columns: Direction, PartNumber, DateOpl, DatePost, PriceUnit, Mass, Sum, CurrencyId, Rate, Unit, RateFlag.
- **Tab enable/disable logic:** During obligation add/edit, disable all tabs except the obligation detail tab (Tab 3). Re-enable on confirm/cancel.

## Form State Machine

| State | Description |
|-------|-------------|
| EDIT (no ops) | All fields editable; Save enabled |
| EDIT (Was_Operation=true) | All panels disabled; Save disabled; only Exit |
| ADD | All fields editable; default currencies set |
| Obligation editing | Only Tab 3 active; Save/Exit hidden; Apply/Cancel shown |

## Tab Structure (6 tabs)

| Tab# | Name | Content |
|------|------|---------|
| 1 | Основные данные | Trade date, number, type, contracts (buyer/seller), currencies, supply type, commission, NDS/Export flags, nested buyer/seller payment instructions |
| 2 | Обязательства | ListView `lstViewOblig` (obligations list) + Add/Edit/Delete buttons |
| 3 | Детали обяз-ва | Nested `SSTabs1`: sub-tab 1 = obligation detail fields (direction, currency, rate, price, mass, dates); sub-tab 2 = objects list (`lstViewObject`) |
| 4 | Хар-ки металла | Precious metals characteristics frame (DatePost, Massa, MassaGramm) + delivery frame (DateOpl, SumOblig) |
| 5 | Хранилище | Storage info (chkExternalStorage, Code, Name, browse button) |
| 6 | Параметры | UbsControlProperty / AddFields (ucpParam) — additional system parameters |

## Key Business Logic Rules

1. **Type checking (cmbContractType):** Buyer contract type must ≠ seller contract type.
2. **Cash instructions:** If `chkCash` checked → call `GetInstructionOplataCash` to auto-fill payment instructions; if unchecked → show manual BIK/RS fields.
3. **Composite trade:** `chkIsComposit` toggles `cmbNaprTrade` (Direction). Only visible when `intVidTrade ≠ 0`.
4. **Rate/CostCurOpl toggle:** `chkRate` and `chkSumInCurValue` are mutually exclusive; each enables/disables `txtRateCurOblig`/`txtCostCurOpl` respectively.
5. **Obligation sum calculations:** `GetSumOblig()`, `GetSumOpl()`, `GetMassaGramm()`, `GetRateCurOblig()` recalculate on LostFocus of price/mass/rate fields.
6. **Tab navigation guard (`SSTabs_BeforeTabClick`):** Going to tab 3 (obligations) requires trade date, delivery currency, seller contract, supply type to be set. Going to tab 3 during obligation editing opens the currently selected obligation.
7. **Lock on operations:** If `PMCheckOperationByTrade` returns `Was_Operation=true`, disable all input panels via `EnableWindow` API.
8. **Currency code validation (CheckDatesOblig):** RS account code digits 6-8 must match payment currency ISO code (using `DefineCodCurrency` + `LibTools.IsEqualNumCodeCurr`).

## Creative Decisions (TBD)

- Layout options for the 6-tab structure (direct TabControl vs split)
- Sub-form strategy for contract lookup, instruction selection, objects, accounts
- DataGridView vs ListView for obligations list
- Data model for obligations list (DataTable vs List<ObligationRow>)
