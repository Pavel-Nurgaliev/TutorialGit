# Creative phase: EnableFieldsCl (recipient INN / address)

**Type:** UI / form state (parity with VB6)  
**Legacy:** `Contract.dob` — `Sub EnableFieldsCl()` (~2000–2006)  
**Target:** `UbsPsContractFrm.Initialization.cs` (replaces `EnableFieldsClStub`)

---

## Problem

When a contract is tied to a **known client** (`idClient > 0`), INN and address come from the client master data and must not be edited on the contract form. When there is **no client** (`idClient <= 0`), those fields are manual entry. The stub `EnableFieldsClStub` only contained an empty `if (m_blnArbitrary)` block and did not match legacy behavior.

---

## Requirements and constraints

- Match **`EnableFieldsCl`** in `Contract.dob`: enable INN and address only when **`idClient <= 0`**.
- **Control mapping:** `txtINN` → `udcRecipientInn`; `txtAdress` → `txtRecipientAddress`.
- Do **not** toggle BIK / recipient name here: legacy left `txtBic` / `txtClient` commented out; this form already handles arbitrary-BIK and browse disabling in the EDIT branch.
- **Call site:** Keep running after `GetBankNameAcc*` and before `ClearBankFields` on ADD so `m_idClient` reflects loaded contract state for EDIT; for ADD, `m_idClient` remains `0` from `InitDoc` start.
- **Framework:** .NET 2.0, no new dependencies.

---

## Options

### Option A — Single helper, `m_idClient` only (recommended)

**Idea:** `udcRecipientInn.Enabled = (m_idClient <= 0)`; `txtRecipientAddress.Enabled = (m_idClient <= 0)`.

| Pros | Cons |
|------|------|
| One-to-one with VB6 | Must ensure `m_idClient` is updated whenever client changes (future browse) |
| Minimal code, easy to test | None significant |

### Option B — Same logic plus explicit `m_blnArbitrary` branch

**Idea:** If `m_blnArbitrary`, force INN/address enabled regardless of `m_idClient`.

| Pros | Cons |
|------|------|
| Might feel safer for “произвольный” | **Not** in legacy `EnableFieldsCl`; arbitrary contracts can still have `idClient > 0` in theory |
| | Risk of contradicting client-linked read-only rule |

### Option C — Central “recipient field policy” object

**Idea:** Extract all recipient enable rules (BIK, account, INN, address, links) into one method called from Init and from future events.

| Pros | Cons |
|------|------|
| Scales when browse/clear lands | Overkill for current stub replacement; larger refactor |

---

## Decision

**Option A** — implement **`EnableFieldsCl`** as pure **`m_idClient <= 0`** gating for **`udcRecipientInn`** and **`txtRecipientAddress`**. Remove the erroneous **`m_blnArbitrary`** wrapper.

**Rationale:** Preserves legacy semantics; arbitrary-BIK locking is already handled separately; future **`RetFromGrid`** / clear-client flows should set **`m_idClient`** and then call **`EnableFieldsCl`** again (documented as follow-up).

---

## Implementation guidelines

1. Rename **`EnableFieldsClStub`** → **`EnableFieldsCl`** (or keep name and delegate — prefer rename for clarity).
2. Update the call in **`InitDoc`** to **`EnableFieldsCl()`**.
3. Optional local: `bool manual = m_idClient <= 0;` for readability.
4. Do not change **`lblRecipientInn`** / **`lblRecipientAddress`** unless accessibility requires it (legacy did not).

---

## Verification

- **EDIT** with `INTIDCLIENT` > 0: INN and address disabled.  
- **ADD** (`m_idClient == 0`): INN and address enabled after init (then `ClearBankFields` clears values only).  
- **EDIT** arbitrary (empty BIK): BIK/account still disabled by existing branch; INN/address follow **`m_idClient`** only.

---

## Status

- **CREATIVE:** Complete (this document).  
- **BUILD:** Implement in `UbsPsContractFrm.Initialization.cs`.
