# Creative phase: GetBankNameACC / `ReadBankBIK` (BIC → bank name, corr. account)

**Type:** Algorithm + channel parity (VB6 `Contract.dob`)  
**Legacy:** `Function GetBankNameACC()` (~2010–2045), `Sub setSignOurBIK()` (~2138–2147)  
**Target:** `UbsPsContractFrm.Initialization.cs` (replaces `GetBankNameAccStub`); `UbsPsContractFrm.cs` (`m_blnIsOurBik`, `SetSignOurBik`)

---

## Problem

After loading contract / client data, the form must resolve BIC via **`ReadBankBIK`**, fill **bank name** and **correspondent account** when allowed, expose **`m_blnIsOurBik`** for later **`SearchAccClient`**, and enable the **«Р/с»** link only when the BIC matches **`m_strOurBik`** from **`InitFormContract`**. The stub only contained an empty `m_blnMayBeArbitrary` check, which is **not** in legacy `GetBankNameACC`.

---

## Requirements and constraints

- **Channel:** `ParamIn("BIC", …)`, `Run("ReadBankBIK")`; success gate **`NUM` > 0** (same as VB6).
- **Outputs:** rows keyed **`BANKNAME`** (always assign `txtBankName`), **`CORRACC`** (assign `ucaCorrespondentAccount` only if empty or placeholder **`00000000000000000000`**).
- **Return value:** `true` iff trimmed BIC non-empty **and** `NUM` > 0 (for future save validation).
- **Always** call **`SetSignOurBik`** after the lookup attempt (VB6 calls it even when BIC empty or `NUM` = 0).
- **`SetSignOurBik`:** `m_blnIsOurBik` = normalized equality of recipient BIC and `m_strOurBik`; enable **`linkRecipientClient`** only when our BIC **and** not **`m_blnArbitrary`** (arbitrary contract path keeps browse disabled).
- **Errors:** `try` / `catch` → `Ubs_ShowError(ex)` (project style); no duplicate legacy `UbsErrMsg` unless product asks.
- **Interop:** `ParamOut` may expose rows as VB **`Parameters`** matrix **or** flat `BANKNAME` / `CORRACC` keys after marshaling — handle both.
- **Arrays:** `object[row, 0]` = key, `object[row, 1]` = value, or transposed **`[2, n]`** — support both shapes (defensive).
- **.NET 2.0**, no LINQ.

---

## Options

### Option A — Matrix-first + flat fallback (recommended)

**Idea:** If `UbsParam` contains **`Parameters`** as rank-2 `Array`, walk rows/columns per shapes above; else read **`BANKNAME`** / **`CORRACC`** if **`Contains`**.

| Pros | Cons |
|------|------|
| Matches VB6 `ressql(0,i)` / `ressql(1,i)` intent | Matrix key name must match runtime (`Parameters` assumed; verify live) |
| Resilient if COM flattens to named params | Slightly more code |

### Option B — Flat params only

**Idea:** Assume `ReadBankBIK` always exposes scalar `BANKNAME`, `CORRACC`, `NUM`.

| Pros | Cons |
|------|------|
| Minimal | Fails if server only fills the VB matrix |
| | Not faithful to `Contract.dob` loop |

### Option C — Shared helper assembly

**Idea:** Move BIC resolution to a common UBS helper used by PM trade / PS contract.

| Pros | Cons |
|------|------|
| DRY across products | Out of scope; different command names (`FillRekv` vs `ReadBankBIK`) |

---

## Decision

**Option A:** Implement **`GetBankNameAcc()`** with **`NUM` > 0** gate, **`ApplyReadBankBikFields`** (matrix via **`Parameters`**, then flat fallback), **`SetSignOurBik()`** in **`finally`**.

**Rationale:** Parity with `Contract.dob`; fallback covers marshaller differences; **`SetSignOurBik`** + **`m_blnArbitrary`** preserves the arbitrary-contract UX already in `InitDoc`.

---

## Implementation guidelines

1. **Constants:** `CmdReadBankBik`, `ParamBic`, `ParamNum`, `ParamBankName`, `ParamCorrAcc`, `ParamParameters`, `CorrespondentAccountPlaceholder` in `UbsPsContractFrm.Constants.cs`.
2. **Field:** `m_blnIsOurBik` next to other `m_bln*` flags in `UbsPsContractFrm.cs`.
3. **Call site:** `InitDoc` calls **`GetBankNameAcc()`** (discard bool until save phase).
4. **Later:** **`SearchAccClient`** when `m_blnIsOurBik && m_idClient > 0` (post–`ReadClient` path in VB6); wire BIC **Leave** to re-run lookup when needed.

---

## Verification

- Non-empty BIC, `NUM` > 0: bank name updated; corr. account filled only from empty/placeholder.
- Empty BIC: no channel call; `SetSignOurBik` clears «our» state and disables link (unless arbitrary already forced off).
- Our BIC, not arbitrary: `linkRecipientClient.Enabled == true`.
- Arbitrary edit: link stays disabled regardless of BIC match.

---

## Status

- **CREATIVE:** Complete (this document).  
- **BUILD:** `GetBankNameAcc`, `SetSignOurBik`, constants; channel doc §4.10 expanded.
