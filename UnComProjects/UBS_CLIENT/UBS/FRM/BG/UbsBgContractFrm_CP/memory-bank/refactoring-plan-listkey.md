# Refactoring Plan: ListKey() in UbsBgContractFrm.cs

## Current State

- **Method:** `ListKey(object param_in, ref object param_out)` (lines 203–279, ~77 lines)
- **Role:** Entry point for the "ListKey" command. Parses input array, resets state, validates and applies parameters per command (Edit, Copy, AddByFrameContract, AddByFramePrepare), then calls `InitDoc()`.
- **Returns:** `true` on success, `false` on validation failure (after optional message and form close).

## Current Structure (Summary)

1. Parse input: `m_itemArray = param_in as object[]`
2. Reset: `m_idContract = 0`, `m_idContractCopy = 0`, `m_isSecure = true`
3. **EditCommand / CopyCommand:** validate `m_itemArray`, read id, set `m_idContract` or `m_idContractCopy`; on failure show message, optionally `btnExit_Click`, `return false`
4. **AddByFrameContrantCommand:** set `m_idFrameContract` from `m_itemArray`, validate; on failure show message and optionally exit; else `m_command = AddCommand`
5. **AddByFramePrepareCommand:** same as above, then `m_command = PrepareCommand`
6. Call `InitDoc()`, `return true`

## Bug to Fix

In both AddByFrame branches (lines 244 and 260):

```csharp
m_idFrameContract = m_itemArray is null ? Convert.ToInt32(m_itemArray[0]) : 0;
```

When `m_itemArray` **is** null, the code uses `m_itemArray[0]` and throws. The condition is reversed. It should be:

```csharp
m_idFrameContract = (m_itemArray != null && m_itemArray.Length > 0)
    ? Convert.ToInt32(m_itemArray[0])
    : 0;
```

Apply this fix when extracting the logic into helpers (or in a separate small change before refactoring).

---

## Target Structure

Refactor so `ListKey()` is a short orchestration that:

1. Parses input and resets state.
2. Applies and validates params per command via small helpers (each returns `bool`: `true` = continue, `false` = validation failed, caller returns `false`).
3. Calls `InitDoc()` and returns `true`.

Each command-specific block becomes a private method that performs validation, sets fields, shows messages, and optionally calls `btnExit_Click`. Return `false` only when the caller must exit with `return false`.

---

## Proposed New Methods (ListKey Calls These)

### 1. ResetListKeyState()

**Extract:** The three assignments at the start of the command handling (lines 207–209).

**Responsibility:**

- `m_idContract = 0;`
- `m_idContractCopy = 0;`
- `m_isSecure = true;`

**Signature:**

```csharp
private void ResetListKeyState()
```

**Note:** Purely state reset; no parameters, no return value.

---

### 2. TryApplyEditOrCopyListKeyParams()

**Extract:** The entire `if (m_command == EditCommand || m_command == CopyCommand)` block (lines 211–239).

**Responsibility:**

- If `m_command` is not Edit and not Copy → return `true` (nothing to do).
- If `m_itemArray` is null → show "Не выбран договор!", optionally `CheckParamForClose("InitParamForm")` and `btnExit_Click`, return `false`.
- Parse `id = Convert.ToInt32(m_itemArray[0])`; if Edit set `m_idContract = id`, else set `m_idContractCopy = id`.
- If both `m_idContract` and `m_idContractCopy` are 0 → same message and optional exit, return `false`.
- Otherwise return `true`.

**Signature:**

```csharp
private bool TryApplyEditOrCopyListKeyParams()
```

**Returns:** `true` to continue, `false` when validation failed (caller should `return false`).

---

### 3. TryApplyAddByFrameContractListKeyParams()

**Extract:** The `else if (m_command == AddByFrameContrantCommand)` block (lines 242–257).

**Responsibility:**

- If `m_command != AddByFrameContrantCommand` → return `true`.
- Set `m_idFrameContract` from `m_itemArray` using the **corrected** logic:  
  `m_idFrameContract = (m_itemArray != null && m_itemArray.Length > 0) ? Convert.ToInt32(m_itemArray[0]) : 0;`
- If `m_idFrameContract == 0` → show "Не выбран рамочный договор!", optionally `CheckParamForClose` and `btnExit_Click`, return `false`.
- Set `m_command = AddCommand`, return `true`.

**Signature:**

```csharp
private bool TryApplyAddByFrameContractListKeyParams()
```

**Returns:** `true` to continue, `false` when validation failed.

---

### 4. TryApplyAddByFramePrepareListKeyParams()

**Extract:** The `else if (m_command == AddByFramePrepareCommand)` block (lines 258–274).

**Responsibility:**

- If `m_command != AddByFramePrepareCommand` → return `true`.
- Set `m_idFrameContract` from `m_itemArray` with the **same corrected** logic as above.
- If `m_idFrameContract == 0` → show frame contract message, optionally exit, return `false`.
- Set `m_command = PrepareCommand`, return `true`.

**Signature:**

```csharp
private bool TryApplyAddByFramePrepareListKeyParams()
```

**Returns:** `true` to continue, `false` when validation failed.

---

## Resulting ListKey() Skeleton

```csharp
public object ListKey(object param_in, ref object param_out)
{
    m_itemArray = param_in as object[];

    ResetListKeyState();

    if (!TryApplyEditOrCopyListKeyParams())
        return false;
    if (!TryApplyAddByFrameContractListKeyParams())
        return false;
    if (!TryApplyAddByFramePrepareListKeyParams())
        return false;

    InitDoc();

    return true;
}
```

---

## Order of Implementation

1. **Fix the bug** in the two AddByFrame branches (correct null/length check and assignment to `m_idFrameContract`). Optionally do this in a single small edit before or as part of step 2.
2. **ResetListKeyState()** — trivial extraction.
3. **TryApplyEditOrCopyListKeyParams()** — copy the Edit/Copy block, return `true`/`false` as above.
4. **TryApplyAddByFrameContractListKeyParams()** — use corrected `m_idFrameContract` logic.
5. **TryApplyAddByFramePrepareListKeyParams()** — same pattern.
6. **Replace ListKey() body** with the skeleton above.

After each step, run or manually test Edit, Copy, Add by frame contract, and Add by frame prepare to avoid regressions.

---

## Benefits

- **Readability:** `ListKey()` becomes a linear checklist: parse input → reset → apply Edit/Copy params → apply AddByFrame contract params → apply AddByFrame prepare params → init doc.
- **Single responsibility:** Each helper handles one command variant.
- **Testability:** Small methods are easier to reason about and test in isolation (e.g. with different `m_command` and `m_itemArray`).
- **Bug fix:** Corrects the reversed null check and avoids NullReferenceException when opening by frame contract/prepare with null or empty input.
- **Consistency:** Same pattern as InitDoc refactoring (orchestrator + focused helpers).

---

## Notes

- Keep all existing behavior: same messages, same `CheckParamForClose("InitParamForm")` and `btnExit_Click` usage.
- `param_out` is not used in the current method body; no change needed unless callers expect it to be set elsewhere.
- If you prefer shorter names, you could use `TryApplyEditOrCopyParams()`, `TryApplyAddByFrameContractParams()`, `TryApplyAddByFramePrepareParams()` and keep the "ListKey" context in the file/class.
