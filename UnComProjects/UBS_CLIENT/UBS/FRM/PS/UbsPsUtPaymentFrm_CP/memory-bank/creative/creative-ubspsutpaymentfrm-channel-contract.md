# Channel contract: UbsPsUtPaymentFrm

Source of truth derived from `legacy-form/UtPayment/UtPayment.dob`, `frmCashOrd.frm`, and `frmCashSymb.frm`.  
Primary script resource: `VBS:UBS_VBD\PS\UT\UtPaymentF.vbs`.  
Alternate print-form resource: `VBS:UBS_VBS\PS\PsCheckPrintForm.vbs`.

## C# usage rules

- Use explicit string literals for every `Run` name and every `ParamIn` / `ParamOut` key.
- Preserve spelling and casing exactly as in VB6.
- Treat similarly named keys as distinct values:
  - `Payment` and `PAYMENT`
  - `StrError`, `strError`, and `Error`
  - `IdContract` and `IDCONTRACT`
  - `AccCode` and `ACCCODE`
  - `Summa` and `SUMMA`
- Use `this.IUbsChannel.LoadResource = LoadResource;` for main payment calls and switch temporarily to `LoadResourcePrintForm` only for the print-form check flow.
- Keep the full command/key inventory documented here even though `Constants.cs` intentionally does not centralize channel literals.

## Variant matrices -> C#

- Legacy variant arrays map to `object[row, column]`.
- `arrPurpose`, `VARPAYMENTS`, `VARCONTRACT`, `VARIDPAYMENTS`, `ArrayCashSymbols`, and similar channel payloads should follow the row/column rule from the workspace array rule.
- `arrDataPayment` is not a matrix; it behaves like a nested parameter bag and should be read key-by-key.

---

## Contract confidence levels

This document uses three confidence tiers:

- `Exact`: call site and concrete in/out keys were directly confirmed from legacy code.
- `Grouped`: call exists and key families are known, but not every branch-specific field was enumerated per call site.
- `Observed`: command was confirmed by `Run(...)`, but detailed per-call payload still needs BUILD-time extraction from the exact branch.

The goal is:

- every `Run` call is listed
- high-risk commands have implementation-ready contracts
- lower-risk commands stay visible and traceable rather than disappearing from scope

---

## Exact contracts: main form core commands

### `FindContrByBicAndAccount`

**Confidence:** Exact

| Direction | Key | Role |
|-----------|-----|------|
| In | `BIC` | Trimmed recipient BIK |
| In | `ACC` | Recipient account |
| In | `INN` | Recipient INN |
| Out | `RecCount` | Match count |
| Out | `IdArray` | Contract-id matrix |

**Call site:** automatic contract resolution by BIK/account.

### `SaveAttributeRecip`

**Confidence:** Exact

| Direction | Key | Role |
|-----------|-----|------|
| In | `IdContract` | Current contract id |
| In | recipient-attribute fields | Recipient name, BIK, corr. account, bank name, account, INN, purpose, and one additional tax field captured by the same save branch |
| Out | `bRetVal` | Success flag |

**Notes:**

- This branch explicitly reloads `VBS:UBS_VBD\PS\UT\UtPaymentF.vbs` before the call.
- Several input keys are Cyrillic and must be copied byte-for-byte from the VB6 branch when BUILD ports the method.

### `Ut_CheckSidPayment`

**Confidence:** Exact

| Direction | Key | Role |
|-----------|-----|------|
| In | `CodePayment` | Payment code |
| In | `IdContract` | Current contract |
| In | `IDKINDPAYMENT` | Payment kind id |
| Out | `strError` | Error text |
| Out | `strMessage` | Question/prompt body |
| Out | `arrDataPayment` | Nested payment data bag |

**Notes:**

- Follow-up logic reads `arrDataPayment` tax, amount, recipient, and payer keys.
- This command is one of the densest payload sources in the form.

### `Ut_GetDataLic`

**Confidence:** Exact

| Direction | Key | Role |
|-----------|-----|------|
| In | `IdPaymentDic` | Payment-dictionary id |
| In | `IdContract` | Current contract |
| In | `IDKINDPAYMENT` | Payment kind id |
| Out | `CodePayment` | Payment code |
| Out | `strError` | Error text |
| Out | `strMessage` | Confirmation text |
| Out | `arrDataPayment` | Nested payment data bag |

### `Ut_GetUserPaymentData`

**Confidence:** Exact

| Direction | Key | Role |
|-----------|-----|------|
| In | `CodeContract` | Contract code |
| In | `CodePayment` | Payment code |
| In | `NameSection` | UI section identifier |
| In | `BIC` | Recipient BIK if not already provided |
| In | `ACC` | Recipient account if not already provided |
| Out | `bRetVal` | Success flag |
| Out | `StrError` | Error text |
| Out | `StrQuestion` | User confirmation text |
| Out | `arrDataPayment` | Nested payment data bag |

### `ReadClientFromNomerCard`

**Confidence:** Exact

| Direction | Key | Role |
|-----------|-----|------|
| In | `NomerCard` | Card number |
| In | `IsGuest` | Guest-client mode |
| Out | `IDCLIENT` | Resolved client id |
| Out | `StrError` | Error text |

### `ReadClientFromIdOC`

**Confidence:** Exact

| Direction | Key | Role |
|-----------|-----|------|
| In | `IDCLIENT` | Client id |
| In | `IsGuest` | Guest-client mode |
| Out | `StrError` | Error text |
| Out | `INN` | Payer INN |
| Out | `InfoClient` | Additional client info |
| Out | `NAME` | Payer name |
| Out | `ADRESS` | Payer address |
| Out | `NUMBER` | Document number |
| Out | `SERIES` | Document series |

### `InitDialogCalc`

**Confidence:** Exact

| Direction | Key | Role |
|-----------|-----|------|
| Out | `bRetVal` | Init succeeded |
| Out | `bInitDialog` | Whether calculator should open |
| Out | `Summa` | Payment amount passed into calculator |
| Out | `StrError` | Error text |

### `CheckOrEndGroup`

**Confidence:** Exact

| Direction | Key | Role |
|-----------|-----|------|
| In | `IdGroupIncoming` | Group id |
| In | `IdPaym` | Current/new payment id |
| In | `curCommonSumma` | Group common amount |
| Out | `Type` | Group state type |
| Out | `PaymentGroupSum` | Current grouped sum |

### `PAYMENT`

**Confidence:** Exact

| Direction | Key | Role |
|-----------|-----|------|
| In | `StrCommand` | `CLEAR` |

**Notes:** used for the clear/new-record path.

### `Payment`

**Confidence:** Exact

| Direction | Key | Role |
|-----------|-----|------|
| In | `StrCommand` | `READ` |
| In | `IdPaym` | Payment id |
| Out | `IdContract` | Contract id |
| Out | `IdClient` | Client id |
| Out | payer/recipient/bank fields | Includes payer name/INN/address and recipient/bank/account fields |
| Out | amount fields | Includes `SummaPaym`, `SummaRateSend`, `Summa` and related values |

### `InitForm`

**Confidence:** Grouped

**Known outputs used by the form:**

- `bRetVal`
- `StrError`
- `IsFrmPrn`
- `bIsFR`
- `strRegName`
- `strRegNumber`
- `objDevice`
- `bIsScan`
- `objScan`
- `arrCityCode`
- `DateBeg`
- `DateEnd`
- `blnIsErrorKey`
- `ViewPrintForm`
- `UtEnterCashSymbol`
- `ChoiceClient`
- `UtEnterSourceMeans`
- `strOurBankBic`

**Notes:**

- Called during startup in more than one initialization branch.
- One of the highest-risk commands and should anchor BUILD wiring.

### `Payment_Save`

**Confidence:** Grouped

**Known input families used before save:**

- command and mode:
  - `COMMANDIN`
  - `StrCommand`
- identifiers:
  - `IdContract`
  - `ID_CLIENT`
  - `IdPaymentGroup`
- payer:
  - `txtFIOPay`
  - `txtINNPay`
  - `txtAdressPay`
  - `blnGuest`
- recipient and bank:
  - `txtBic`
  - `AccKorr`
  - `AccClient`
  - `txtINN`
  - `cmbPurpose`
  - `txtRecip`
  - `Address`
- amounts:
  - `curSumma`
  - `curSummaRateSend`
  - `curSummaTotal`
- validation flags:
  - `blnTerror`
  - `blnIPDL`
- grouping:
  - `Group`
  - `IdPaymentGroup`

**Known outputs used after save:**

- `StrError`
- `IdPaym`
- `PAYMENTGROUPID`

### `Ps_CheckPrintForm`

**Confidence:** Exact

**Resource:** switches to `VBS:UBS_VBS\PS\PsCheckPrintForm.vbs`

**Known outputs used:**

- `bRetVal`
- `StrError`

### `UtCheckAccFromBic`

**Confidence:** Exact

| Direction | Key | Role |
|-----------|-----|------|
| In | `BIC` | BIK |
| In | `ACC` | Account |
| In | `IdContract` | Contract id |
| Out | `bRetVal` | Valid flag |
| Out | `strError` | Error text |

### `UtCheckCloseAccout`

**Confidence:** Exact

**Known outputs used:** error/boolean contract in the settlement-account validation branch.

### `PsCheckTofkAcc`

**Confidence:** Exact

**Known validation role:** treasury-account restrictions on settlement-account flow.

### `CheckKey`

**Confidence:** Exact

| Direction | Key | Role |
|-----------|-----|------|
| In | `STRACC` or account text | Settlement account |
| In | `BIC` | BIK |
| In | `CORRACC` | Corr. account |
| Out | `RETVAL` or `bIsCheckKey` | Validity result |

### `CheckAddFields`

**Confidence:** Exact

**Known outputs used:**

- `bRetVal`
- `StrError`

### `CheckSumPayment`

**Confidence:** Exact

**Known role:** validation of sum/total consistency before save.

### `UtCheckCashSymbol`

**Confidence:** Exact

**Known role:** validates cash symbol against current payment amount.

### `UtCheckCashSymbolComiss`

**Confidence:** Exact

**Known role:** validates commission-related cash symbol state.

### `UtCheckCashSymbolNDS`

**Confidence:** Exact

**Known role:** validates NDS-related cash symbol state.

### `UtCalcSumCommiss`

**Confidence:** Grouped

**Known inputs:** amount-related fields and contract/rate context.  
**Known outputs:** commission totals used to update payment amount editors.

### `UtCalcSumNDS`

**Confidence:** Grouped

**Known inputs:** amount and NDS context.  
**Known outputs:** NDS totals used in amount recalculation.

### `UtReadSetupLockPurpose`

**Confidence:** Exact

**Known outputs used:**

- `bRetVal`
- `StrError`
- `strError`
- `blnPurposeLock`

### `UtGetStateClearFieldSend`

**Confidence:** Exact

**Known outputs used:**

- `bRetVal`
- `IsClearSend`
- `StrError`

### `UtGetAutoFillPeriod`

**Confidence:** Grouped

**Known role:** period defaults/auto-fill.

### `GetPropAccCode`

**Confidence:** Grouped

**Known role:** derives payer account code / related props.

### `UtCheckBIKBank`

**Confidence:** Exact

**Known outputs used:**

- `bRetVal`
- `strError`

### `UtCheckBIKLimitSharing`

**Confidence:** Exact

**Known outputs used:**

- `bRetVal`
- `strError`

### `ReadBankBIK`

**Confidence:** Exact

| Direction | Key | Role |
|-----------|-----|------|
| In | `BIC` | Bank BIK |
| Out | `NUM` or parameter rows | Count |
| Out | `Parameters` or equivalent rows | Name/value bank metadata |

**Known output tags read by the form:**

- `BANKNAME`
- `CORRACC`

### `ReadContractbyCode`

**Confidence:** Grouped

**Known role:** resolve contract by code before later `UtReadContract`.

### `UtReadTypePayment`

**Confidence:** Grouped

**Known role:** fills payment-type-dependent state.

### `UtReadContract`

**Confidence:** Grouped

**Known input:**

- `IdContract`

**Known outputs used by the form:**

- `bRetVal`
- `StrError`
- `State`
- `Comment`
- `BIC`
- `CorrAcc`
- `INN`
- `Acc`
- `Adress`
- `RateTypeSend`
- `RatePercentSend`
- `MinSumSend`
- `MaxSumSend`
- `arrPurpose`
- separate commission/tariff-related payload values

### `UtCheckPaymentBenefits`

**Confidence:** Grouped

**Known role:** benefits validation / enablement in payer workflow.

### `UtGetKPPU`

**Confidence:** Exact

**Known input:**

- `IdContract`

### `UtGetAccINNFromLastPayment`

**Confidence:** Exact

**Known inputs:**

- `IdContract`
- `IdClient`
- `BIC`

**Known outputs used:**

- `INN`
- `ACC`
- `PURPOSE`

### `Ps_GetArrayPrepareCashOrd`

**Confidence:** Grouped

**Known role:** build payment matrix for cash-order flow.

### `Ps_GetArrayPrepareCashOrdSecond`

**Confidence:** Grouped

**Known role:** build secondary/grouped cash-order matrix.

### `Ps_PreparePlatDoc`

**Confidence:** Grouped

**Known role:** prepare payment document before cash-order request.

### `Ps_GetStatePrepareCashOrd2`

**Confidence:** Grouped

**Known role:** read state for second-stage cash-order preparation.

### `UtGetGlobalUserData`

**Confidence:** Grouped

**Known outputs used elsewhere in cash flow:**

- `IDUSER`
- `Division`

### `Ps_GetStateRequestFormCashOrd`

**Confidence:** Grouped

**Known role:** decide whether cash-order preview form should open.

### `ReadTariff`

**Confidence:** Grouped

**Known outputs used:**

- `Tarif_Name_Rate`
- `Tarif_Name_Rate_Default`
- `Code_Energy`

### `ReadNalog`

**Confidence:** Grouped

**Known role:** tax-tab data and tax-specific payment payload.

### `ReadPhone`

**Confidence:** Grouped

**Known outputs used:**

- `Phone_Code_Name`
- `Phone_Code_Name_Default`

### `UtCheckAndSplitAccount`

**Confidence:** Grouped

**Known role:** account normalization / split behavior.

### `UtCalcKey`

**Confidence:** Grouped

**Known role:** derive payment checksum / key.

### `CheckTerror`

**Confidence:** Exact

**Known inputs:**

- `NAME`
- `IDCONTRACT`
- `PURPOSE`
- `RECIPIENTNAME`

**Known outputs:**

- `RETVAL`
- `StrError`
- optional payer-related flag

### `UtReadOurBankBik`

**Confidence:** Exact

**Known outputs:**

- `strOurBankBik`

### `UTListAddRead`

**Confidence:** Exact

**Known outputs:**

- `AddFields`

### `Ut_CheckBeforeSave`

**Confidence:** Grouped

**Known inputs used:**

- `IDPAYMENT`
- `IDCONTRACT`
- `CODE`
- `INN`
- `BIC`
- `CORRACC`
- `ACC`
- `IDCLIENT`
- `PAYERFIO`
- `PAYERINN`
- `PAYERADDRESS`
- `RECIPIENTNAME`
- `PURPOSE`
- `SUMMA`
- `SUMMARATESEND`
- `SUMMATOTAL`
- dynamically merged add-fields keys

**Known outputs used:**

- `Error`
- `bRet`

### `UtGetINNFromLastPayment`

**Confidence:** Exact

**Known inputs:**

- `ACC`
- `BIC`
- `IdContract`

**Known outputs used:**

- `INN`
- `PURPOSE`
- `IDPAYMENT`

### `UtGetKPPUPayerLastPayment`

**Confidence:** Exact

**Known input:**

- `IDPAYMENT`

**Known output used:**

- recipient-name-related field used to update payer/recipient UI

### `GetGroupIDByPayment`

**Confidence:** Grouped

**Known role:** resolve group id from payment id for grouped flows.

### `UtReadLic`

**Confidence:** Grouped

**Known role:** license-related payment payload.

### `GetIdClientFromGroupPayment`

**Confidence:** Grouped

**Known role:** derive client id from group payment.

### `CheckPaymDic`

**Confidence:** Grouped

**Known role:** payment-dictionary validation / enablement.

### `UserAddField`

**Confidence:** Grouped

**Known role:** user-form/additional-field follow-up.

---

## Exact contracts: child forms

### `frmCashOrd.frm`

#### `UtGetGlobalUserData`

**Known outputs used:**

- `DATEDOC`
- `bRetVal`
- `StrError`
- `StrAccCash`
- `NumDiv`

#### `Ps_FindAccCash1`

**Confidence:** Observed

**Role:** cash debit account lookup during dialog load.

#### `UtGetDataCashOrder`

**Confidence:** Exact

| Direction | Key | Role |
|-----------|-----|------|
| In | `REGIM` | Mode |
| In | `NUMDIV` | Division |
| In | `DATEDOC` | Doc date |
| In | `DATETRN` | Transaction date |
| In | `ACCDB` | Debit cash account |
| In | `VARPAYMENTS` | Payments matrix |
| In | `VARCONTRACT` | Contracts matrix |
| Out | `BRETVAL` | Success flag |
| Out | `StrError` | Error text |
| Out | `VARDOC` | Docs matrix |
| Out | Cyrillic documents list key | Preview list payload |

#### `UtMainCashOrder`

**Confidence:** Exact

| Direction | Key | Role |
|-----------|-----|------|
| In | `VARPAYMENTS` | Payments matrix |
| In | `ACCDB` | Cash debit account |
| In | `DATEDOC` | Doc date |
| In | `NUMDIV` | Division |
| In | `IsBySeparPaym` | Separate-payment mode |
| In | `IdPayment` | One payment id or payment-id array |
| In | Cyrillic documents key | Document payload |
| Out | `BRETVAL` | Success flag |
| Out | `StrError` | Error text |

### `frmCashSymb.frm`

#### `UtCheckArrayCashSymbol`

**Confidence:** Exact

| Direction | Key | Role |
|-----------|-----|------|
| In | `arrCashSymb` | Cash-symbol matrix |
| In | `arrTypeCashSymbol` | Allowed symbol matrix |
| Out | `bRetValCheck` | Validation success |
| Out | `strError` | Error text |
| Out | `arrCashSymb` | Normalized/validated symbol matrix |

---

## Observed run inventory not yet expanded per branch

These commands were directly confirmed in the legacy sources and are in scope even where this document only captured grouped usage above:

- `UtInitAddFields`
- `UT_AplyUserPaymentData`
- `ReadKBK`
- `GetGlobal_ParamBaseCurrency`
- `PutAddFlCalc`
- `PS_UserIsCashier`
- `GetNalogNumber`
- `CheckKBK`
- `CheckRegex108Field`
- `DeleteDocForm`
- `NalogAddFieldSave`
- `PS_GetSummaControl`
- `PS_GetState`
- `UTIsMoveValByAccountA`
- `UtGetDoublePaymentsPlatDocExt_Form`
- `UtGetPaymentsPlatDocExt_Form`
- `IsAskEdit`
- `RunScrFromSetting`
- `UtGetAddField`
- `UTReadSettingChechkClient`
- `UTCheckBankRecipient`

These are valid runtime calls and must not be dropped during BUILD, even where exact key tables still need to be lifted from their specific branches.

---

## High-traffic ParamIn families

These key families recur across the form and should be treated as the core mental model during BUILD:

### Mode and shell context

- `StrCommand`
- `COMMANDIN`
- `StrCommand_Source`
- `NameSection`
- `SIDPATTERN`
- `FROMFO`
- `CalledFromFrontOffice`
- `fromFoAsClient`
- `Test`

### Identifiers

- `IdContract`
- `IDCONTRACT`
- `IdPayment`
- `IDPAYMENT`
- `IdPaym`
- `PAYMENTGROUPID`
- `IdGroupIncoming`
- `IdPaymentDic`
- `ID_DOUBLE_PAYMENT`
- `IdTariff`
- `IdPhone`
- `IdPattern`
- `IdAttributeRecip`
- `IdClient`
- `IDCLIENT`
- `ID_CLIENT`
- `IDKINDPAYMENT`

### Payer / recipient / bank

- `BIC`
- `ACC`
- `AccClient`
- `AccClientPay`
- `AccCode`
- `ACCCODE`
- `CORRACC`
- `RecipientName`
- `RECIPIENTNAME`
- `Purpose`
- `PURPOSE`
- `CodeContract`
- `CODECONTRACT`
- `CodePayment`
- `CODEPAYMENT`
- `INN`
- `Kppu`
- `PAYERFIO`
- `PAYERINN`
- `PAYERADDRESS`
- `NAME`
- `NomerCard`
- `ThirdPerson_Name`
- `ThirdPerson_Kind`
- `ThirdPerson_INN`
- `ThirdPerson_KPP`

### Amounts / totals / periods

- `SUMMA`
- `SUMMAPENI`
- `SUMMARATESEND`
- `SUMMARATEREC`
- `SUMMAREC`
- `SUMMATOTAL`
- `SUMMANDSSEND`
- `SUMMANDSREC`
- `SummaRateRec`
- `SummaRec`
- `SummaNDSRec`
- `SummaNDSSend`
- `SummaNDSPaym`
- `CheckSum`
- `DateBeg`
- `DateEnd`
- `DATEBEGIN`
- `DATEEND`

### Validation / compliance / runtime flags

- `Field108`
- `blnIPDL`
- `blnTerror`
- `NeedControl`
- `bIsPeriodEnable`
- `blnGuest`
- `bIsFR`
- `blnGroup`
- `State`
- `varAddFields`

---

## High-traffic ParamOut families

### General status

- `bRetVal`
- `bRet`
- `RETVAL`
- `StrError`
- `strError`
- `error`
- `Error`
- `StrQuestion`
- `strMessage`
- `bMsgBoxYesNo`

### Lookup and initialization

- `RecCount`
- `IdArray`
- `IDCLIENT`
- `INN`
- `InfoClient`
- `NAME`
- `ADRESS`
- `NUMBER`
- `SERIES`
- `NalogNumber`
- `Result`
- `DocumentsExists`
- `IsFrmPrn`
- `bIsFR`
- `strRegName`
- `strRegNumber`
- `objDevice`
- `bIsScan`
- `objScan`
- `EndGroup`
- `arrCityCode`
- `DateBeg`
- `DateEnd`
- `blnIsErrorKey`
- `ViewPrintForm`
- `UtEnterCashSymbol`
- `ChoiceClient`
- `UtEnterSourceMeans`
- `strOurBankBik`

### Payment and save

- `CodePayment`
- `arrDataPayment`
- `IdPaym`
- `Type`
- `PaymentGroupSum`
- `PAYMENTGROUPID`
- `ID_DOUBLE_PAYMENT`
- `IdPaymentGroup`
- `State`
- `SumControl`
- `NotCashier`
- `blnPeparePlatDoc`
- `StateCashOrd`
- `StateCashRate`
- `IDUSER`
- `Division`
- `ISFRMPREVIEW`

### Arrays and list payloads

- `ListSymbols`
- `arrTypeCashSymbol`
- `CashSymbol`
- `ArrayCashSymbols`
- `arrPurpose`
- `Tarif_Name_Rate`
- `Tarif_Name_Rate_Default`
- `Code_Energy`
- `Phone_Code_Name`
- `Phone_Code_Name_Default`
- `VARPAYMENTS`
- `VARCONTRACT`
- `VARIDPAYMENTS`

---

## Nested payload: `arrDataPayment`

`arrDataPayment` is central to this form and should be implemented as a nested parameter bag rather than flattened.

Observed nested keys include:

- `ACCCODE`
- `SUMMA`
- `THIRDPERSON_NAME`
- `THIRDPERSON_KIND`
- `THIRDPERSON_INN`
- `THIRDPERSON_KPP`
- `SUMMARATESEND`
- `SUMMATOTAL`
- `SUMMARATEREC`
- `CORRACC`
- `ACC`
- `INN`
- `PURPOSE`
- `PAYERFIO`
- `PAYERINN`
- `PAYERADDRESS`
- `BIC`
- `Summa`
- `SummaRateSend`
- `SummaTotal`
- `CheckSum`
- `AccCode`
- `Period`
- `DateBegin`
- `DateEnd`
- `AccClient`

Implementation rule:

- Read existence first, then consume key-by-key.
- Preserve both uppercase and mixed-case variants; they are not interchangeable.

---

## Non-channel COM / script calls still required for parity

| Component | Purpose |
|-----------|---------|
| `URunScr.IUbsRunScript` | user-form pattern script flow (`CallingUserFormPattern`) and post-save group-continuation script |
| `UbsCheck.UbsComCheck` | passport / IPDL validation checks using the active UBS channel |

Known script parameters used outside normal channel calls:

- `DocObj`
- `DocHandle`
- `IdContract`
- `RunUserForm`
- `InitArray`
- `ButtonCaption`
- `Results`
- `OptionOK`
- `Key`
- group-payment identifiers
- `Parent`
- `EndGroup`

---

## BUILD guidance

1. Start implementation with the `Exact` and `Grouped` command sections above.
2. Keep command names and keys as inline string literals in code.
3. Use this document, not `Constants.cs`, as the audit surface for channel fidelity.
4. When a BUILD branch uncovers additional per-call keys for an `Observed` command, extend this document rather than inventing names from memory.
5. For any Cyrillic key that appears garbled in modern reads, copy it byte-for-byte from the source branch being ported.

---

## Verification

- [x] Every observed `Run(...)` command is listed in this document.
- [x] The highest-risk commands have implementation-ready contracts.
- [x] Child-form channel contracts are included.
- [x] `arrDataPayment` is called out as a nested parameter bag.
- [x] Explicit-literal project rules are preserved.

---

**Recommended next step:** `/creative Child forms: open mode, data exchange, close/return contract`
