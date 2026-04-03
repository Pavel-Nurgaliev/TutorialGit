# CREATIVE: UbsPsContractFrm — Channel contract

**Scope:** Document all `IUbsChannel.Run` targets, `LoadResource`, and ParamIn/ParamOut keys for PS Contract (legacy `Contract.dob`).  
**Date:** 2026-04-02

---

## CREATIVE PHASE START: Channel contract

### 1. Problem

The legacy form calls many channel commands with overlapping parameter names (`STRCOMMAND`, `IDCONTRACT`, DDX field keys). The .NET form must use **explicit constants** for command names and param keys (per `style-rule.mdc`) and a **single reference** for implementation and tests.

**Control identifiers:** C# **field names** for controls follow **prefix-based (Hungarian-style) naming** — see **`creative-ubspcontractfrm-conversion-architecture.md` §7** (`btn`, `txt`, `lbl`, `cmb`, `ucd`/`udc`/`uca`/`uci`/`ucf`, etc.). **Channel / `ParamIn` keys** (`TXTCODE`, Cyrillic add-field names) stay **byte-for-byte** as the server expects; do not rename parameter keys to match control names.

### 2. Resource

| Item | Legacy (VB6) | .NET (target) |
|------|----------------|---------------|
| **LoadResource** | `VBS:UBS_VBD\PS\Contract.vbs` | `ASM:...` — **TBD** with ASM/PS team; store in `UbsPsContractFrm.Constants.cs` as `LoadResource` |

### 3. IUbs surface commands (handlers)

These are **not** `UbsChannel.Run` — they are **shell → form** entry points. Map host behavior to `Ubs_AddName` delegates (exact names to match UBS host; verify with integration).

| Logical name | Legacy | Purpose |
|--------------|--------|---------|
| Init / command | `UBSChild_ParamInfo("InitParamForm", RHS)` | `RHS(0)` parent, `RHS(1)` item array (contract id), `RHS(2)` `ADD` / `EDIT` |
| Return from lists | `UBSChild_ParamInfo("RetFromGrid", RHS)` | Client picker, account picker, kind picker |

**Design note:** See `creative-ubspcontractfrm-conversion-architecture.md` for mapping CommandLine / ListKey / optional named handlers.

---

### 4. `UbsChannel.Run` inventory

#### 4.1 `InitFormContract`

| When | ParamIn | ParamOut (examples from legacy) |
|------|---------|-----------------------------------|
| Start of `InitDoc` after `LoadResource` | (cleared / minimal) | `BIKBANK`, `nIdOI`, `varExecutors`, `VARSTATE` (matrix for commission type combos: loop `UBound(varState, 2)` — **VB column index**; in .NET use **row, column** per `array-rule.mdc`) |

#### 4.2 `Contract`

| STRCOMMAND | Purpose | ParamIn (typical) | ParamOut |
|------------|---------|-------------------|----------|
| `READ` | Load contract for EDIT | `IDCONTRACT`, `STRCOMMAND` | Many DDX keys + `nIdOI`, `Метод расчета комиссии с получателя`, `TXTBIC`, `TXTINN`, `STATE`, `DATECLOSE`, `Parameters` collection |
| `READF` | Uniqueness check before save | All changed fields + `STRCOMMAND` = `READF` | `IDCONTRACT` (compare to current id) |
| *(save)* | Persist | `STRCOMMAND` = `ADD` or `EDIT`, `IDCONTRACT` (EDIT), `DATECLOSE`, `nIdOI`, `TXTBIC`, `TXTINN`, `STATE`, DDX keys, `Метод расчета комиссии с получателя` | `IDCONTRACT`, `strError` |
| `CHANGEKIND` | Kind changed from grid | `INTIDKIND`, `STRCOMMAND` | `StrError`, refresh add-fields |

**DDX field keys** (must appear in constants): `TXTCODE`, `TXTCOMMENT`, `CURPERCENTSEND`, `CURPERCENTREC`, `TXTNUM`, `TXTINN`, `TXTADRESS`, `TXTBIC`, `TXTACCCORR`, `TXTACC`, `DATECONTRACT`, `INTIDCLIENT`, `TXTKIND`, `INTTYPESEND`, `INTTYPEREC`, etc.

#### 4.3 `ReadClient`

| ParamIn | ParamOut |
|---------|----------|
| `IDCLIENT`, `STRCOMMAND` = `READ` | `NAME`, `BIC`, `ADRESS`, `INN`, `StrError`, optional add-field `КППУ` |

#### 4.4 `ReadAcc`

| ParamIn | ParamOut |
|---------|----------|
| `IDACC` | `TXTACC`, `StrError` |

#### 4.5 `ReadKind`

| ParamIn | ParamOut |
|---------|----------|
| `IDKINDPAYMENT` or `idKind`, `STRCOMMAND` = `READ` | `TXTCODE`, `TXTCOMMENT`, `StrError`; in `FillKind` also `INTTYPESEND`, `INTTYPEREC`, `CURPERCENTSEND`, `CURPERCENTREC`, `MayBeArbitrary`, add-field keys for commission labels |

#### 4.6 `GetNameFilterKindPaym`

| ParamIn | ParamOut |
|---------|----------|
| `FILTERCONTRACT` | `FILTERKINDPAYM` |

#### 4.7 `CheckKey`

| ParamIn | ParamOut |
|---------|----------|
| `STRACC`, `BIC`, `CORRACC` | `RETVAL` (bool) |

#### 4.8 `CheckClientAcc`

| ParamIn | ParamOut |
|---------|----------|
| `IDCLIENT`, `STRACC` | `IDCLIENT` (validation) |

#### 4.9 `PSCheckAccounts`

| ParamIn | ParamOut |
|---------|----------|
| `varAccounts` (from add-field **«Транзитные счета»**) | `RETVAL`, `strError` |

#### 4.10 `ReadBankBIK`

| ParamIn | ParamOut |
|---------|----------|
| `BIC` | `NUM` (int; **> 0** means success like VB6) |

**Client fill rules (legacy `GetBankNameACC`):**

- **`BANKNAME`** → `txtBankName` (overwrite).
- **`CORRACC`** → `ucaCorrespondentAccount` only if current value is empty or **`00000000000000000000`**.

**Payload shape:** VB6 reads `objParamOut.Parameters` as a 2D matrix: keys in one dimension, values in the other (`BANKNAME`, `CORRACC` rows). The .NET client tries, in order: matrix under out-key **`Parameters`** (rank 2, either **`[n, 2]`** or **`[2, n]`**), then scalar out-keys **`BANKNAME`** / **`CORRACC`** if the marshaller flattens them.

**Post-step:** `setSignOurBIK` — compare BIC to `BIKBANK` from `InitFormContract`; enable account browse (`linkRecipientClient`) only when equal and not arbitrary-contract mode.

#### 4.11 `SearchAccClient`

| ParamIn | ParamOut |
|---------|----------|
| `IDCLIENT` | `ACCCLIENT` |

#### 4.12 `CheckExistAddFieldContract`

| ParamIn | ParamOut |
|---------|----------|
| `NAMEFIELD` | `BLNEXIST` |

---

### 5. User-visible messages (constants)

| Key | Example text (legacy) |
|-----|------------------------|
| Save success | `Договор сохранен в БД` (Info caption) |
| Duplicate code | `Объект с таким кодом уже существует!` |
| Edit without selection | `Не выбран договор` |
| Close date required | `Не заполнена дата закрытия договора` |
| BIK not found | `БИК не найден` |

---

### 6. Decision

- **Single source of truth:** This file + `UbsPsContractFrm.Constants.cs` (implementation).
- **LoadResource:** Placeholder ASM string until PS assembly path is confirmed; **must not** ship with `DllName` template.
- **Param keys:** Use **exact** Cyrillic keys from legacy for `Parameter("...")` where the server expects them (e.g. `Метод расчета комиссии с получателя`, add-field names).

---

### 7. Commission tab — `EnableSum` (Phase C BUILD)

Legacy **`Sub EnableSum`** (`Contract.dob`): **`cboTypeSend.ListIndex`** in **{0, 3}** → disable **`curPercentSend`** and zero value; **`cboTypeRec.ListIndex`** in **{0, 3}** → disable **`curPercentRec`** and zero; otherwise enable. Wired as **`EnableSumCommissionControls`** on **`cmbPayerCommissionType`** / **`cmbRecipientCommissionType`** **`SelectedIndexChanged`** and after **`InitDoc`** data load. Indices **0** and **3** are constants **`CommissionComboIndexDisablePercentFirst`** / **`Second`** in `UbsPsContractFrm.Constants.cs`.

**`Метод расчета комиссии с получателя`:** **READ** maps to **`chkRecipientCommissionReverse`** in **`InitDoc`**; **WRITE** on save remains **Phase E** (`btnSave_Click` / `Contract` `ParamIn`).

---

## CREATIVE PHASE END

### Verification

- [x] All `Run` targets from `Contract.dob` listed (grep-verified).
- [x] STRCOMMAND variants for `Contract` documented.
- [x] LoadResource and handler surface separated.
