# Creative phase: Host child lists + shared BIK validation

**Scope:** Event-handler plan **Wave B–C** prerequisites (`tasks.md` § “Creative / spike flags”).  
**Legacy:** `Contract.dob` — `btnClient_Click`, `btnKind_Click`, `btnAcc_Click`, `UBSChild_NotifyCloseWindow`, `UBSChild_ParamInfo`, `UserDocument_KeyPress` on `txtBic`.

---

## Part 1 — Architecture: child window / `RetFromGrid` contract

### Problem

VB6 opens modal/child grids via **`Parent.GetWindow`**, passes **`InitParamGrid`**, **`SelectItem`**, and **`UbsGridList`** where-clauses. Return data flows through **`UBSChild_ParamInfo`** (and related) when the child closes. The .NET solution has **no** `GetWindow` usage in tracked sources yet; **`UbsFormBase`** (referenced from `ProgramData\UniSAB\Assembly\Ubs`) is the likely integration surface but its exact member names are **not** verified in-repo.

### Requirements

- Preserve **one active child per list type** (legacy `NumChildWin`, `NumChildWinAcc`, `NumChildWinKind` — refocus if already open).
- After selection: run the correct **channel** sequence (`ReadClient`, `Contract`+`ReadKind`, `ReadAcc`, etc.) as in `Contract.dob`.
- Work inside **.NET 2.0**, **no** new assemblies unless the host already provides them.

### Options

| Option | Idea | Pros | Cons |
|--------|------|------|------|
| **A — `UbsFormBase` direct** | Call host methods on `base` (or `IUbsChannel` extension) once names are confirmed from `UbsFormBase` / host docs | Minimal layers | Requires API spike on shipped DLL |
| **B — Host adapter interface** | `IContractShellHost { OpenClientList(...); }` with production impl wrapping `UbsFormBase` | Testable, clear seams | Extra type + wiring for one form |
| **C — Stub + delegate** | Keep `MsgBrowseNotImplemented` until host team documents callback; add `Action`/`UbsDelegate` hooks for return | Safe schedule | No real browse until wired |

### Decision

**Option A** as the **target** implementation path, with **Option C** acceptable for interim builds: document the **VB contract** first, implement against **`UbsFormBase` (or loader) reality** after a short **API spike** (object browser / existing PM/OP forms in the same UBS line if they use the same host).

### VB6 contract summary (implementation checklist)

1. **Client** (`UBS_LIST_COMMON_CLIENT`): grid where **«Тип» = 1**; `InitParamGrid(0)=NumWin`, `(2)=True`, `(5)=1`; optional `SelectItem`.
2. **Kind** (`GetNameFilterKindPaym` → `FILTERKINDPAYM` filter name): `GetWindow` with that filter; `InitParamGrid(5)=1` or `2` depending on existing where RS.
3. **Account** (`UBS_LIST_OD_ACCOUNT0`): where **client id** when `idClient > 0**; balance-sheet where line with flags = 1.
4. **Close notify:** clear the matching child id when the window closes; if parent window closes, exit like legacy.

### Implementation guidelines

- Add **string constants** for window / filter keys in `UbsPsContractFrm.Constants.cs` when implementing (match VB literals exactly).
- Centralize **child handle** fields (`m_numChildWinClient`, etc.) on the form partial used for browse/return.
- **Single return entry point** (e.g. `OnGridSelectionReturned(string context, object payload)`) once the host exposes how selection is delivered (may mirror **`ListKey`** composite array pattern or a named `IUbs` command).
- Update **`creative-ubspcontractfrm-channel-contract.md`** with any **new** `Run` / `ParamIn` keys found when **`ReadAcc`** / **`CHANGEKIND`** paths are wired.

---

## Part 2 — Algorithm: BIK Enter vs save validation

### Problem

Legacy **`GetBankNameACC`** runs from **`InitDoc`**, **save**, and **Enter** on BIC (`UserDocument_KeyPress`). Enter and save use **overlapping** failure rules (`БИК не найден`, tab to recipient area, focus) but not identical guards (arbitrary / public payments / EDIT).

### Requirements

- **One** channel path: reuse existing **`GetBankNameAcc()`** (already calls `ReadBankBIK` and updates UI).
- **Context-specific** messaging and focus only where legacy differs.
- **.NET 2.0**; avoid duplicated `ReadBankBIK` calls in the same user action when possible.

### Options

| Option | Idea | Pros | Cons |
|--------|------|------|------|
| **A — Shared helper + context enum** | `BikValidationContext { EnterKey, Save }`; `bool TryValidateBik(BikValidationContext ctx, out string focusHint)` wraps `GetBankNameAcc`, applies legacy conditionals, shows `MsgBikNotFound` | DRY, testable branches | Small enum + one method |
| **B — Only `GetBankNameAcc()` + callers branch** | Callers interpret `bool` and duplicate `m_blnArbitrary` / `m_command` checks | No new type | Copy-paste risk |
| **C — Always validate on Leave** | `Validated` on BIC always calls lookup | Fewer missed lookups | Not legacy; extra server hits |

### Decision

**Option A:** introduce **`BikValidationContext`** (or two bool flags if you prefer zero enums — but enum is clearer) and **`TryValidateBikForForm(BikValidationContext ctx)`** that:

1. Returns **true** if BIC empty **or** `GetBankNameAcc()` returned true **or** legacy says validation can be skipped (arbitrary / same conditions as `btnSave_Click` and `KeyPress` blocks).
2. Returns **false** and applies **message + tab + focus** when legacy would show **`БИК не найден`**.
3. **Enter** success path: select main tab, focus **INN** if enabled else chain per `.dob` (~1630+).

### Implementation guidelines

- Add **`MsgBikNotFound`**, **`MsgBikNotFoundTitle`** (or single `Ubs_ShowErrorBox` overload pattern) to **`Constants.cs`**.
- **`btnSave_Click`** (Phase E) and **BIC Enter handler** both call **`TryValidateBikForForm`**; do **not** call **`ReadBankBIK`** again inside save if Enter just succeeded unless data changed (trust `GetBankNameAcc` idempotency).
- Optional **Leave** on BIC: call **`GetBankNameAcc()`** only (no message box) if product wants live fill without Enter — document as **non-legacy extension** if added.

---

## Verification

- **Host:** Spike documents actual **C# method names** for open/close/return; child id tracking matches “no duplicate window” behavior.
- **BIK:** Same BIC + data → Enter and Save do not double-hit server unnecessarily; arbitrary contract skips strict errors like VB.

---

## Status

- **CREATIVE:** Complete (this document).  
- **BUILD:** Wave C (host) and Wave B/E (BIK/save) per `tasks.md` plan.
