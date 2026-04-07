# Channel contract: UbsPsUtPaymentGroupFrm

Source of truth derived from `legacy-form\UtPaymentGroup\UtPaymentGroup_F.dob` (UtPaymentGroup_F).  
Script resource: **`VBS:UBS_VBD\PS\UT\UtPaymentF.vbs`** (constant `LoadResource` in `UbsPsUtPaymentGroupFrm.Constants.cs`).  
**Call order / UI on validation failure:** `memory-bank/plan-validation-chain.md`.

## C# usage (project rules)

- Use **explicit string literals** for every `Run` name and every `ParamIn` / `ParamOut` key (mirror VB6 `Parameter("...")` names exactly).
- Prefer `this.IUbsChannel.LoadResource = LoadResource;` after host init; then `this.IUbsChannel.ParamIn["Key"] = value;` / `this.IUbsChannel.Run("Command");` / read `ParamOut` / `ExistParamOut` per `UbsFormBase` API, or the `UbsChannel_ParamIn` / `UbsChannel_Run` / `UbsParam` pattern used in `UbsPsContractFrm` — **key strings must match the table below**.

## Variant matrices → C#

- **GroupContract** (from `InitFormGroup`): VB6 `GroupContract(0, i)` = id, `GroupContract(1, i)` = caption → C# `object[row, 0]`, `object[row, 1]` with `row == i`.
- **IdArray** (`FindContrByBicAndAccount`): VB6 `idArr(0, 0)` → C# `object[0, 0]`.
- **arrPurpose** (`UtReadContract` → `FillPurpose`): VB6 `arrPurpose(0, i)` text per row → C# `object[row, 0]`.
- **UTListAddRead → AddFields**: VB6 loop `varAddFields(1, lngI)` = param name, `varAddFields(7, lngI)` = value → C# columns 1 and 7, row `lngI`.
- **ReadBankBIK** bulk: VB6 `ressql(0, i)` name, `ressql(1, i)` value → C# `object[row, 0/1]` or iterate `CountParameters` / `Parameters` as on `UbsParam` / channel out API.

---

## Commands (alphabetical by name)

### CheckAddFields

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | *(none set in VB6 before Run)* | Channel reads current additional fields from context / `ucfAddProperties` |
| Out | `bRetVal` | Boolean; `False` → validation failed |
| Out | `StrError` | Message |

**Call site:** `CheckPayment` (before save).

---

### CheckKey

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `STRACC` | Settlement account text |
| In | `BIC` | |
| In | `CORRACC` | Correspondent account |
| Out | `RETVAL` | Boolean; `False` → invalid account key |

**Call site:** `CheckRS` (after `UtReadOurBankBik` check).

---

### CheckTerror

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `NAME` | Payer FIO (`txtFIOPay`) |
| In | `IDCONTRACT` | Contract id |
| In | `PURPOSE` | `cmbPurpose.Text` |
| In | `RECIPIENTNAME` | `txtRecip.Text` |
| Out | `RETVAL` | Boolean (optional; if missing, treat as pass) |
| Out | `StrError` | Prompt text |
| Out | `blnErrorPayer` | Optional; payer must be chosen from directory |

**Call site:** `CheckTerror` → used from `CheckPayment`.

---

### FindContrByBicAndAccount

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `BIC` | Trimmed |
| In | `ACC` | `AccClient` |
| In | `INN` | `txtINN` |
| Out | `RecCount` | Integer |
| Out | `IdArray` | Matrix; single match: `idArr(0, 0)` = contract id |

**Call site:** `AccClient_LostFocus` when `cmbCode.ListIndex = -1` and BIC/ACC filled.

---

### InitFormGroup

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `StrCommand` | `ADD`, `GROUP_EDIT`, `GROUP_ADD`, etc. |
| In | `IdPayment` | Group payment id (`m_IdPayment`) |
| Out | `strError` | Non-empty → show message, exit form |
| Out | `ChoiceClient` | `0` ⇒ guest client mode (`blnGuest`) |
| Out | `EndGroup` | Boolean; group closed |
| Out | `GroupContract` | Variant matrix: column 0 = id, 1 = caption for combo |
| Out | `SummaPeni` | Currency → `curPeny` / `udcPeny` |

**Call site:** `InitDoc`.

---

### Payment (read mode)

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `StrCommand` | `"READ"` |
| In | `IdPaym` | Payment id |
| Out | `IdContract`, `IdClient` | |
| Out | `FIOSend`, `PayerINN1`, `AdressSend` | Payer |
| Out | `Comment`, `BIC`, `AccCorr`, `NameBank`, `INNRec1`, `AccRec`, `Note` | Recipient / bank |
| Out | `InfoClient` | |
| Out | `RecipientName` | |
| Out | `SummaPaym`, `SummaRateSend`, `Summa` | Amounts |

**Call site:** `ReadContract` (`GROUP_EDIT` path).

---

### Payment_Save

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `IdContract` | |
| In | `blnGuest` | |
| In | `ID_CLIENT` | |
| In | `txtFIOPay`, `txtINNPay`, `txtAdressPay` | |
| In | `txtBic` | |
| In | `AccKorr`, `AccClient` | |
| In | `txtINN` | |
| In | `cmbPurpose` | Text |
| In | `txtRecip` | If `blnArbitrary` |
| In | `curSumma`, `curSummaRateSend`, `curSummaTotal` | Currency values |
| In | `COMMANDIN` | Current `StrCommand` |
| In | `Address` | `m_strAddress` (contract address) |
| In | `StrCommand` | `"ADD"` (forced for save payload) |
| In | `Group` | `True` |
| In | `IdPaymentGroup` | `m_IdPayment` |
| In | `blnTerror` | |
| In | `blnIPDL` | If `m_IdClient <> 0` after `CheckIPDL` |
| Out | `StrError` | Empty ⇒ success |
| Out | `IdPaym` | New payment id (group continuation) |
| Out | `PAYMENTGROUPID` | For script `PsPaymentIncomingReception.vbs` |

**Call site:** `btnSave_Click` after validations.

---

### PAYMENT (clear)

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `StrCommand` | `"CLEAR"` |
| Out | *(used via channel)* | |

**Call site:** `NewRecord`.

---

### ReadBankBIK

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `BIC` | |
| Out | `NUM` | Count of parameter rows |
| Out | `Parameters` | Matrix: rows with `(0,*)` = name (`BANKNAME`, `CORRACC`), `(1,*)` = value |

**Call sites:** `FindContractbyId` (after `UtReadContract`); `GetBankNameACC` (after BIK checks).

---

### ReadClientFromIdOC

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `IDCLIENT` | |
| In | `IsGuest` | |
| Out | `StrError` | |
| Out | `INN`, `InfoClient`, `NAME`, `ADRESS` | |
| Out | `TypeDoc`, `NUMBER`, `SERIES` | Passport/doc |

**Call sites:** `RetFromGrid` (client list); `txtNomerCardPay` Enter (after `ReadClientFromNomerCard` sets `IDCLIENT` on param in).

---

### ReadClientFromNomerCard

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `NomerCard` | |
| In | `IsGuest` | |
| Out | `IDCLIENT` | Fed into next call |
| Out | `StrError` | |

**Call site:** `txtNomerCardPay_KeyDown` (Enter).

---

### ReadRecipFromId

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `IdAttributeRecip` | From grid selection |
| Out | `Наименование получателя в плат. документах` | → `txtRecip` |
| Out | `BIC`, `CORRACC`, `Наименование банка`, `ACC`, `INN`, `PURPOSE` | |

**Call site:** `RetFromGrid` (receiver attribute list).

---

### SaveAttributeRecip

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `IdContract` | |
| In | `Наименование получателя`, `БИК`, `Корр. счет`, `Наименование банка`, `Р/с`, `ИНН`, `Назначение` | Russian keys as in VB6 |
| Out | `bRetVal` | Success flag |

**Call site:** `btnSaveAttribute_Click`. (VB6 sets `LoadResource` before Run; redundant if channel already loaded.)

---

### UTListAddRead

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | *(cleared)* | |
| Out | `AddFields` | Matrix for `Ut_CheckBeforeSave` merge |

**Call site:** `Ut_CheckUserBeforeSave` (first step).

---

### Ut_CheckBeforeSave

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `IDPAYMENT` | If `StrCommand = "CHANGE_PART"` |
| In | `IDCONTRACT`, `CODE`, `INN`, `BIC`, `CORRACC`, `ACC` | |
| In | `IDCLIENT`, `PAYERFIO`, `PAYERINN`, `PAYERADDRESS` | |
| In | `RECIPIENTNAME`, `PURPOSE` | |
| In | `SUMMA`, `SUMMARATESEND`, `SUMMATOTAL` | |
| In | *Dynamic* | For each row in `AddFields`, `Parameter(name) = value` |
| Out | `Error` | Message |
| Out | `bRet` | Boolean |

**Call site:** `Ut_CheckUserBeforeSave` (user validation before `Payment_Save`).

---

### UtCheckAccFromBic

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `BIC`, `ACC`, `IdContract` | |
| Out | `bRetVal` | |
| Out | `strError` | |

**Call site:** `CheckPayment`.

---

### UtCheckBIKBank

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `BIC` | |
| Out | `bRetVal` | |
| Out | `strError` | |

**Call site:** `GetBankNameACC`.

---

### UtCheckBIKLimitSharing

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `BIC` | |
| Out | `bRetVal` | |
| Out | `strError` | |

**Call site:** `GetBankNameACC`.

---

### UtGetAccINNFromLastPayment

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `IdContract`, `IdClient`, `BIC` | |
| Out | `INN`, `ACC`, `PURPOSE` | Used to prefill when enabled |

**Call site:** `FindContractbyId` (when client and BIC present).

---

### UtGetINNFromLastPayment

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `ACC`, `BIC`, `IdContract` | |
| Out | `INN`, `PURPOSE` | |
| Out | `IDPAYMENT` | If set, triggers `UtGetKPPUPayerLastPayment` |

**Call site:** `UserDocument_KeyPress` Enter on `AccClient`.

---

### UtGetKPPU

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `IdContract` | |
| Out | *(refreshes additional fields via `AddProperties.Refresh`)* | |

**Call site:** `FindContractbyId` when `StrCommand = "ADD"`.

---

### UtGetKPPUPayerLastPayment

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `IDPAYMENT` | From prior `UtGetINNFromLastPayment` out |
| Out | `Recip` | → `txtRecip` |

**Call site:** `UserDocument_KeyPress` after `UtGetINNFromLastPayment`.

---

### UtGetStateClearFieldSend

| Direction | Key | Type / role |
|-----------|-----|-------------|
| Out | `bRetVal` | |
| Out | `IsClearSend` | Clear payer fields |
| Out | `StrError` | |

**Call site:** `clearRecFieldsSend`.

---

### UtReadContract

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | `IdContract` | |
| Out | `bRetVal`, `StrError` | |
| Out | `Комментарий` | Optional messagebox |
| Out | `State` | Contract state (`1` = closed) |
| Out | `RateTypeSend`, `RatePercentSend`, `MinSumSend`, `MaxSumSend` | Commission |
| Out | `Комиссии с плательщика-признак ставки`, `Комиссии с плательщика-тарифная сетка` | |
| Out | `Comment`, `BIC`, `CorrAcc`, `INN`, `Acc` | |
| Out | `Adress` | → `m_strAddress` |
| Out | `IsSeparateDoc` | → `txtINN.Enabled` |
| Out | `arrPurpose` | Purpose list |
| Out | *(other fields as used)* | |

**Call site:** `FindContractbyId`.

---

### UtReadOurBankBik

| Direction | Key | Type / role |
|-----------|-----|-------------|
| Out | `strOurBankBik` | |

**Call site:** `CheckRS`.

---

### UtReadSetupLockPurpose

| Direction | Key | Type / role |
|-----------|-----|-------------|
| In | *(may carry `IdContract` from prior calls)* | |
| Out | `bRetVal`, `StrError` / `strError` | VB6 uses both casings in messages |
| Out | `blnPurposeLock` | Maps to `cmbPurpose.Enabled` (semantic: lock = enable state) |

**Call sites:** `FindContractbyId` (`ADD`); `clearRecFields`.

---

## Non-channel / COM (still required for parity)

| Component | Purpose |
|-----------|---------|
| `CreateObject("UbsCheck.UbsComCheck")` | `CommonCheckPassport4GISMU(UbsChannel, m_IdClient, …)`, `CommonCheckIPDL(UbsChannel)` |
| `CreateObject("URunScr.IUbsRunScript")` | After save: `LoadFiles "UBS_VBS\PS\PsPaymentIncomingReception.vbs"`, parameters `Key`, `Идентификатор группового платежа`, `Идентификатор основного платежа`, `Parent`; `EndGroup` out |

**PLAN note:** .NET interop strategy for `IUbsRunScript` and `UbsComCheck` is a BUILD/CREATIVE follow-up (same ProgIds / RCW).

---

## VB6 commands not used (commented / dead)

- `UtReadSettingChoiceClient`
- `UtReadTypePayment`
- `ReadContractbyCode`
- `UTReadSettingChechkClient`

Documented for traceability; do not wire unless product restores them.

---

## Index: `Run` first argument → section

| Run | Section |
|-----|---------|
| `CheckAddFields` | CheckAddFields |
| `CheckKey` | CheckKey |
| `CheckTerror` | CheckTerror |
| `FindContrByBicAndAccount` | FindContrByBicAndAccount |
| `InitFormGroup` | InitFormGroup |
| `Payment` | Payment (read) |
| `Payment_Save` | Payment_Save |
| `PAYMENT` | PAYMENT (clear) |
| `ReadBankBIK` | ReadBankBIK |
| `ReadClientFromIdOC` | ReadClientFromIdOC |
| `ReadClientFromNomerCard` | ReadClientFromNomerCard |
| `ReadRecipFromId` | ReadRecipFromId |
| `SaveAttributeRecip` | SaveAttributeRecip |
| `UTListAddRead` | UTListAddRead |
| `Ut_CheckBeforeSave` | Ut_CheckBeforeSave |
| `UtCheckAccFromBic` | UtCheckAccFromBic |
| `UtCheckBIKBank` | UtCheckBIKBank |
| `UtCheckBIKLimitSharing` | UtCheckBIKLimitSharing |
| `UtGetAccINNFromLastPayment` | UtGetAccINNFromLastPayment |
| `UtGetINNFromLastPayment` | UtGetINNFromLastPayment |
| `UtGetKPPU` | UtGetKPPU |
| `UtGetKPPUPayerLastPayment` | UtGetKPPUPayerLastPayment |
| `UtGetStateClearFieldSend` | UtGetStateClearFieldSend |
| `UtReadContract` | UtReadContract |
| `UtReadOurBankBik` | UtReadOurBankBik |
| `UtReadSetupLockPurpose` | UtReadSetupLockPurpose |
