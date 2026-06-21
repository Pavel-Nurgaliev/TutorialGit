# CREATIVE: Phase B — Main tab (InitDoc, layout, grid returns)

**Scope:** Design decisions for implementing Phase B per `plan-phase-b-main-tab.md`.  
**Source:** `legacy-form/Contract/Contract.dob` (`InitDoc`, `UBSChild_ParamInfo` / `RetFromGrid`).  
**Reference:** `UbsOpCommissionFrm` (`ListKey` → `InitDoc`), `creative-ubspcontractfrm-conversion-architecture.md` §7.

---

## CREATIVE PHASE START: Phase B

### 1. Problem

Phase B requires **`tabPageMain`** controls, **`InitDoc`** parity with VB (multi-step channel calls), and **browse** flows that replace VB `RetFromGrid`. Open decisions:

1. **When** does `InitDoc` run relative to `CommandLine` / `ListKey`?
2. How do we **iterate** `Contract` READ output and map keys to controls (DDX replacement)?
3. How do we **fill commission type combos** from `VARSTATE` without indexing bugs?
4. How do we **integrate** host list returns (client / account / kind) before full shell integration?
5. How much **add-fields** wiring belongs in Phase B vs Phase D?

**Constraints:** .NET Framework 2.0; `IUbsChannel.ParamIn` / `Run` / `ParamOut` (or project’s `UbsChannel_*` helpers); constants for command/param keys; `object[row, column]` for 2D arrays.

---

### 2. Options — InitDoc entry point

| Option | Description |
|--------|-------------|
| **A** | Call **`InitDoc()`** at end of **`ListKey`**, after `m_command` / `m_idContract` are set — same pattern as **`UbsOpCommissionFrm`** (`ListKey` → `InitDoc` → `Refresh` add-fields). |
| **B** | Call **`InitDoc()`** from **`Load`** event; `ListKey` only sets ids. Risk: order if `ListKey` fires after `Load`. |
| **C** | **Hybrid:** `ListKey` calls `InitDoc` when `m_command` is non-empty; **`Load`** calls `InitDoc` only if `!m_isInitialized` (fallback for hosts that do not fire `ListKey` before show). |

---

### 3. Analysis — Init entry

| Criterion | A | B | C |
|-----------|---|---|---|
| Consistency with `UbsOpCommissionFrm` | ⭐⭐⭐⭐⭐ | ⭐⭐ | ⭐⭐⭐⭐ |
| Host timing robustness | ⭐⭐⭐ | ⭐⭐ | ⭐⭐⭐⭐⭐ |
| Simplicity | ⭐⭐⭐⭐⭐ | ⭐⭐⭐ | ⭐⭐⭐ |

**Decision:** **Option C (hybrid).**

- Primary path: **`ListKey`** → `InitDoc()` → set **`m_isInitialized = true`** (private bool).
- **`Load`** handler: if **`!m_isInitialized`**, call `InitDoc()` once (covers hosts that initialize after `Load`).
- Guard `InitDoc` with try/catch → `Ubs_ShowError` (per style rule).

---

### 4. Options — Mapping `Contract` READ output to controls

| Option | Description |
|--------|-------------|
| **A** | **`Select Case`** on each key name (string) in a loop over `ParamOut` / `UbsParam` — mirrors VB `Select Case ressql(0, i)`. |
| **B** | **Explicit** assignments: `ParamOut.Value("TXTCODE")` etc. after `Run` — fewer branches, easier to miss keys. |
| **C** | **Hybrid:** `Run` then read known keys by name from channel output; use a **switch** for the `INTTYPESEND` / `INTTYPEREC` combo special cases (like VB `SetFocusCombo`). |

**Decision:** **Option C (hybrid).**

- After `Run("Contract")`, read **documented** keys from creative channel contract; keep **special cases** for combo indices and `Метод расчета комиссии с получателя` in dedicated small methods (`ApplyContractReadToControls`, `ApplyCommissionTypesFromVarState`) to avoid one giant method.

---

### 5. Options — `VARSTATE` combo fill

| Option | Description |
|--------|-------------|
| **A** | Assume **.NET** `object[row, col]` matches server layout after **one** runtime verification (log dimensions in DEBUG). |
| **B** | **Transpose** loop: if server sends VB-like `(column, row)` layout, convert loop indices explicitly. |
| **C** | **Unit test / manual sample:** capture one `VARSTATE` payload shape from test environment and document row/column meaning in this file. |

**Decision:** **A + C** — implement loop with **`object[row, column]`**; **verify** first integration with a real or logged `VARSTATE`; if wrong, apply **B** (transpose) without changing server.

---

### 6. Options — Browse / `RetFromGrid`

| Option | Description |
|--------|-------------|
| **A** | **`Ubs_AddName(new UbsDelegate(RetFromGrid))`** if host registers that name; **single** handler dispatches by `child window id` or `param_in` tag (mirror VB `NumChildWin` / `NumChildWinAcc` / `NumChildWinKind`). |
| **B** | **Three** named handlers (`RetFromGridClient`, …) only if host exposes three IUbs names. |
| **C** | **Stub** until integration: buttons show `MessageBox` / **`uciContract`** “TODO” and return; no crash. |

**Decision:** **A preferred** with **C fallback** for Phase B BUILD until host contract is confirmed.

- Implement **private** methods `ApplyReadClientResult`, `ApplyReadAccResult`, `ApplyChangeKindResult` that contain the channel calls (`ReadClient`, `ReadAcc`, `Contract` + `CHANGEKIND`) so **one** entry point can call them once routing is known.

---

### 7. Options — Add-fields on Phase B

| Option | Description |
|--------|-------------|
| **A** | **Minimal:** `ucfAdditionalFields` on **`tabPageAddFields`** + `Refresh()` after `InitDoc` if stub/channel is wired; full **Phase D** parity later. |
| **B** | **Defer** add-fields UI to Phase D; **InitDoc** skips `AddProperties.Refresh` in .NET until control exists. |

**Decision:** **A** — place control on tab in Phase B; call **`Refresh()`** after `InitDoc` when `IUbsAddFieldsStub` (or project equivalent) is wired; **empty** add-fields acceptable if channel not ready.

---

### 8. Layout (B.1)

**Decision:** Use **`TableLayoutPanel`** on **`tabPageMain`** with **2 columns** (labels | editors) for the top contract block, plus **`grpRecipient`** with its own inner layout — balances **designer-rules** (template) with readability. **Anchor** multiline/comment if needed when screenshots exist (`plan-form-appearance-legacy-screens.md`).

---

### 9. Implementation guidelines

1. Add **`m_isInitialized`** boolean; set **`true`** after first successful `InitDoc` from `ListKey` or `Load`.
2. Split **`InitDoc`** into **`UbsPsContractFrm.Initialization.cs`** partial when file exceeds ~200 lines in this region.
3. **Constants:** every new `Run` argument key in **`UbsPsContractFrm.Constants.cs`** (no inline literals for keys/commands).
4. **Errors:** `catch (Exception ex) { this.Ubs_ShowError(ex); }` around `InitDoc` and browse handlers.
5. **EDIT without id:** already handled in `ListKey`; keep **close** behavior aligned with Commission (optional `Close()` vs error only) — **document** chosen behavior in `progress.md`.

---

## CREATIVE PHASE END

### Verification

- [x] Init entry options (3) + decision.
- [x] Param mapping strategy (3) + decision.
- [x] VARSTATE / array indexing called out.
- [x] RetFromGrid / stub strategy.
- [x] Add-fields scope for Phase B.
- [x] Layout guidance.
