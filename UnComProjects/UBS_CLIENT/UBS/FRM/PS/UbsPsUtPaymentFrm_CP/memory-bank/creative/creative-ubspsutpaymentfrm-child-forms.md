# CREATIVE: Child forms � UbsPsUtPaymentFrm

**Scope:** Open mode, data exchange, close/return contract, and parent ownership for `FrmCalc`, `FrmCashOrd`, and `FrmCashSymb`.  
**Sources:** `memory-bank/tasks.md`, `legacy-form/UtPayment/frmCalc.frm`, `frmCashOrd.frm`, `frmCashSymb.frm`, `creative-ubspsutpaymentfrm-channel-contract.md`.

---

## ENTERING CREATIVE PHASE: Architecture (child-form contracts)

### Requirements and constraints

1. All three child forms must remain separate WinForms dialogs under the main payment project.
2. The parent form must stay the orchestration owner; child dialogs must not directly mutate main-form controls.
3. Legacy `TypeExit` behavior must be preserved semantically, but mapped to an idiomatic .NET result contract.
4. Array payloads coming from VB6 must follow the project `object[row, column]` rule.
5. `FrmCashOrd` must support both preview-and-confirm behavior and the legacy auto-execute path.
6. `FrmCashSymb` must preserve the grid-edit -> validate -> return-array flow.
7. `FrmCalc` should stay intentionally lightweight and not become a mini business-service layer.

### Options

#### Option A � Let dialogs write directly into parent fields/controls

- **Description:** mirror VB6 informality by passing references and letting dialogs update parent state directly.
- **Pros:** closer to ad hoc VB6 coupling, minimal adapter code.
- **Cons:** fragile, hard to test, poor partial-class boundaries, higher regression risk.

**Verdict:** reject.

#### Option B � Strongly owned modal dialogs with explicit input/output properties (**recommended**)

- **Description:** the parent builds dialog inputs, shows the form modally, and reads explicit outputs only on success.
- **Pros:** clear contracts, aligns with .NET WinForms, keeps ownership in `UbsPsUtPaymentFrm.Cash.cs` / `Commission.cs`, makes array conversion manageable.
- **Cons:** requires small adapter code and result mapping.

**Verdict:** **selected.**

#### Option C � Replace dialogs with static helper/service methods

- **Description:** move business logic to helper classes and reduce dialogs to thin shells or remove them.
- **Pros:** more separated logic.
- **Cons:** over-abstracts the migration, weak parity with legacy interaction flow, complicates Designer ownership.

**Verdict:** not appropriate for the first conversion pass.

---

## EXITING CREATIVE PHASE � Decision summary

**Chosen approach:** Option B.

## Shared contract pattern

### Open mode

- Open all three child forms with modal WinForms semantics: `ShowDialog(this)`.
- Parent allocates a fresh dialog instance per interaction.
- Parent sets all input properties before calling `ShowDialog`.
- Child form loads from its own input properties during `Load` / initialization helpers.

### Close / return model

Legacy pattern:

- `TypeExit = 1` + `Hide` means success
- `TypeExit = 0` + `Hide` means cancel

Selected .NET mapping:

- success -> `DialogResult.OK`
- cancel/close -> `DialogResult.Cancel`
- optional result flags remain as strongly named output properties

Rule:

- Parent should check `ShowDialog(...) == DialogResult.OK` first.
- If needed, the parent may also read a secondary property such as `WasCreated` or `LoadedSuccessfully`.

### Ownership rule

- `FrmCalc` orchestration belongs in `UbsPsUtPaymentFrm.Commission.cs` or the main `.cs`.
- `FrmCashOrd` and `FrmCashSymb` orchestration belongs in `UbsPsUtPaymentFrm.Cash.cs`.
- Child forms may own their internal UI state and local validation, but not parent UI updates.

### Data-shape rule

- Incoming VB6 matrices become `object[row, column]`.
- Dialogs may convert matrices into local row models for editing.
- On success, dialogs convert back to `object[row, column]` before returning to the parent.

---

## `FrmCalc`

### Legacy behavior

Observed in VB6:

- public input: `curSumPaym`
- public result flag: `TypeExit`
- success path:
  - computes change
  - sets `TypeExit = 1`
  - `Hide`
- cancel path:
  - sets `TypeExit = 0`
  - `Hide`

### Selected .NET contract

**Inputs**

- `PaymentAmount : decimal`

**Outputs**

- `CashAmount : decimal`
- `ChangeAmount : decimal`
- `IsConfirmed : bool`

### Open behavior

- Parent creates `FrmCalc`.
- Parent sets `PaymentAmount`.
- Dialog formats and displays the payable amount on load.

### Save/confirm behavior

- Dialog validates entered cash amount.
- If cash amount is enough, it computes change, sets output properties, sets `IsConfirmed = true`, and closes with `DialogResult.OK`.
- If validation fails, it stays open and shows the error locally.

### Cancel behavior

- On exit/cancel, set `IsConfirmed = false` and close with `DialogResult.Cancel`.

### Parent-consumption rule

- Parent reads `CashAmount` and `ChangeAmount` only when `DialogResult.OK`.
- Parent decides whether any main-form fields should be updated.

### Why this mapping is chosen

- It preserves the legacy success/cancel semantics exactly.
- It keeps `FrmCalc` as a pure UI helper with no channel dependency.

---

## `FrmCashSymb`

### Legacy behavior

Observed in VB6:

- public inputs/state:
  - `ArrayData`
  - `curSummaTotal`
  - `arrTypeCashSymbol`
- public output:
  - `arrCashSymb`
- local temp output:
  - `arrCashSymbTemp`
- save flow:
  - reads grid rows
  - builds array
  - validates via `UtCheckArrayCashSymbol`
  - writes validated array back to `arrCashSymb`
  - sets `TypeExit = 1`
  - `Hide`
- cancel flow:
  - `TypeExit = 0`
  - `Hide`

### Selected .NET contract

**Inputs**

- `IUbsChannel Channel`
- `object[,] CashSymbolsSource`
- `object[,] AllowedCashSymbols`
- `decimal ExpectedTotal`

**Outputs**

- `object[,] CashSymbolsResult`
- `bool IsConfirmed`

### Internal representation

Selected approach:

- use a local row model for the grid UI
- convert from `object[row, column]` -> row model on load
- convert from row model -> `object[row, column]` before validation and return

Suggested row model:

```csharp
private sealed class CashSymbolRow
{
    public string Symbol;
    public decimal Amount;
}
```

### Open behavior

- Parent creates dialog and assigns all inputs.
- Dialog loads the local editable rows from `CashSymbolsSource`.
- Dialog shows a `DataGridView` with symbol and amount columns.

### Confirm behavior

1. Build the outgoing `object[row, column]` matrix from current rows.
2. Verify total equals `ExpectedTotal`.
3. Call channel validation `UtCheckArrayCashSymbol`.
4. If validation succeeds:
   - assign validated result to `CashSymbolsResult`
   - set `IsConfirmed = true`
   - close with `DialogResult.OK`

### Cancel behavior

- Set `IsConfirmed = false`.
- Do not mutate parent state.
- Close with `DialogResult.Cancel`.

### Parent-consumption rule

- Parent replaces its stored cash-symbol matrix only on `DialogResult.OK`.
- Parent may trigger recalculation afterward if totals are affected.

### Why this mapping is chosen

- It preserves the legacy �edit grid -> validate -> return array� contract.
- It lets the UI use a friendlier row model without breaking the UBS array convention.

---

## `FrmCashOrd`

### Legacy behavior

Observed in VB6:

- public state:
  - `TypeExit`
  - `m_bIsLoadOK`
  - `blnCreate`
  - payment / contract arrays
  - one payment id or payment-id array
- `Form_Load` performs load-time channel work and sets `m_bIsLoadOK`
- exit path:
  - `TypeExit = 0`
  - `Hide`
- execute path:
  - `TypeExit = 1`
  - `blnCreate = False` initially
  - `blnCreate = True` on successful creation
- caller sometimes uses the dialog interactively
- caller sometimes triggers load and then immediate move/execute behavior

### Selected .NET contract

**Inputs**

- `IUbsChannel Channel`
- `object[,] PaymentsData`
- `object[,] ContractsData`
- `long PaymentId`
- `object[,] PaymentIdArray`
- `bool AutoExecute`

**Outputs**

- `bool IsConfirmed`
- `bool LoadedSuccessfully`
- `bool WasCreated`
- optional `object[,] DocumentsData` internal-only if needed for list binding

### Internal phases

Explicit methods:

1. `LoadContext()`
   - `UtGetGlobalUserData`
   - `Ps_FindAccCash1`
2. `LoadDocuments()`
   - `UtGetDataCashOrder`
3. `ExecuteCashOrder()`
   - `UtMainCashOrder`

### Open behavior

#### Preview mode

- `AutoExecute = false`
- Dialog loads context and documents in `Load`.
- If documents exist and load succeeded, dialog displays the `ListView`.
- User confirms with execute button.

#### Auto-execute mode

- `AutoExecute = true`
- Dialog still performs `LoadContext()` and `LoadDocuments()`.
- If `LoadedSuccessfully` is true, it immediately calls `ExecuteCashOrder()`.
- It may close itself automatically with `DialogResult.OK` / `Cancel` depending on outcome.

### Confirm behavior

- If execution succeeds:
  - `IsConfirmed = true`
  - `WasCreated = true`
  - close with `DialogResult.OK`

### Cancel/failure behavior

- If user exits preview mode:
  - `IsConfirmed = false`
  - `WasCreated = false`
  - close with `DialogResult.Cancel`
- If load fails:
  - `LoadedSuccessfully = false`
  - `WasCreated = false`
  - close with `DialogResult.Cancel` after showing the error

### Parent-consumption rule

- Parent should distinguish:
  - dialog confirmed
  - dialog loaded but no creation happened
  - load failed
- Parent updates the legacy `m_blnCreateCashOrd`-style state from `WasCreated`, not from `DialogResult` alone.

### Why this mapping is chosen

- It preserves the unusual VB6 dual-mode behavior without hiding it in event side effects.
- It makes the highest-risk dialog phase-driven and testable.

---

## Close semantics and disposal policy

Selected policy:

- Use `Close()` in .NET rather than mimicking VB6 `Hide`.
- Parent uses `using` or deterministic disposal after each modal interaction.
- State that must survive closing is copied into explicit output properties before closing.

Why:

- A fresh dialog instance per interaction is simpler and safer than retaining hidden form state.
- It matches the partial-class ownership model and reduces reuse bugs.

---

## Main-form adapter pattern

### `FrmCalc`

- Launch helper returns a small result object or reads dialog properties directly.
- No shared mutable arrays required.

### `FrmCashSymb`

- Launch helper:
  - builds input matrix
  - opens dialog
  - writes returned matrix back into main-form state

### `FrmCashOrd`

- Launch helper:
  - builds payment/contract matrices
  - chooses preview vs auto-execute
  - opens dialog
  - stores `WasCreated` and any follow-up state

---

## Risks and mitigations

- `FrmCashOrd` mixes loading and execution:
  - mitigation: keep explicit phase methods and separate result flags
- `FrmCashSymb` uses editable matrix data:
  - mitigation: local row model plus explicit conversion at boundaries
- `FrmCalc` can accidentally grow beyond its purpose:
  - mitigation: keep it channel-free and parent-owned

---

## Verification

- [x] Multiple child-form contract options considered.
- [x] Legacy `TypeExit` semantics mapped to .NET modal results.
- [x] `FrmCashOrd` dual-mode behavior documented.
- [x] `FrmCashSymb` array conversion boundary documented.
- [x] Parent ownership rules defined.

---

**Recommended next step:** `/creative Form appearance: match legacy screenshots in .NET layout`
