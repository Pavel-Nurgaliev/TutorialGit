# CREATIVE: Constants file — UbsPsUtPaymentGroupFrm

**Scope:** `UbsPsUtPaymentGroupFrm.Constants.cs` — resource path, channel `Run` names, `StrCommand` values, `ParamIn`/`ParamOut` keys (where fixed), dialog captions, user messages, COM ProgIds, script paths, UI strings (tabs, group titles).  
**Sources:** `legacy-form\UtPaymentGroup\UtPaymentGroup_F.dob`, `creative/creative-ubspsutpaymentgroupfrm-channel-contract.md`, `plan-group-payment-cycle.md`, `plan-validation-chain.md`.  
**Reference style:** `UbsPsContractFrm_CP\UbsPsContractFrm\UbsPsContractFrm.Constants.cs` (`#region` blocks, PascalCase const names, `Msg*` / `Caption*` for user text).

---

## ENTERING CREATIVE PHASE: Architecture (constants organization)

### Requirements

1. **No magic strings** in business code — workspace `style-guide.md` and `.cursor/rules/style-rule.mdc`.
2. **Channel contract fidelity:** Russian parameter keys and mixed-case keys (`StrError` vs `strError`) must match VB6 exactly when passed to `ParamIn`/`ParamOut` / reading outputs.
3. **Parity:** User-visible Russian text matches VB6 (including **«Платеж»** vs **«Платёж»** where both appear in legacy).
4. **.NET 2.0:** `const` only; no static readonly unless truly needed (prefer not).

### Options

| Option | Description | Pros | Cons |
|--------|-------------|------|------|
| **A** | Messages only in `Constants.cs`; `Run` names and param keys inline at call sites | Smaller constants file | Violates “explicit literals” discipline; typos in keys |
| **B** | Single partial with `#region` blocks: Resource, Commands, ChannelRun, ParamKeys, Messages, Shell, COM — **recommended** | Grep-friendly, matches `UbsPsContractFrm`, one place to audit | Large file (~200+ lines) |
| **C** | Split `Constants.Channel.cs` partial | Smaller files | More files; .NET 2 partial proliferation |

### Decision

**Option B:** One `UbsPsUtPaymentGroupFrm.Constants.cs` with **`#region`** mirroring `UbsPsContractFrm`:

- `Resource` — `LoadResource` (already present).
- `StrCommand` — mode strings passed to channel / local state.
- `ChannelRun` — every `Run("...")` first argument as `private const string`.
- `ParamKey` — grouped subregions or prefixed names (`PkIdContract`, `PkStrError`, …) for keys used **more than once** or **easy to mistype** (Russian). Single-use keys may stay as const next to command consts or in a `ParamKey` block for uniformity — **BUILD choice:** prefer **all fixed keys as const** for channel-heavy forms.
- `Caption` — dialog / message box titles (`m_strCaptionForm` equivalent, «Проверка», …).
- `Message` — `Msg*` user body text.
- `Script` — `ScriptGroupContinuation`, ProgIds.
- `UiText` — tab labels, `GroupBox` captions (optional: can stay on controls in Designer; **recommend const** if same strings referenced from code e.g. `UbsCtrlFields` tab title).
- `Shell` / `List` — if `Ubs_ActionRun` or list names appear (add when porting browse shell).

### Implementation guidelines

1. **Naming:** `MsgXxx` for body, `CaptionXxx` for `MessageBox` title / `Text` suffix; `CmdXxx` or `RunXxx` for channel commands (`RunPaymentSave`, `RunInitFormGroup`, …).
2. **Dynamic text:** Channel `StrError` / `strError` / `Error` — **not** constants; only **title** consts when the legacy concatenates a fixed prefix (e.g. `"Проверка доп. полей. "` + caption).
3. **Info line:** VB6 `Info.Caption = "Платеж сохранен в БД"` — const `MsgPaymentSavedDb` (differs from generic style-guide example «Данные сохранены»; **VB6 wins** for this form).
4. **Duplicate LoadResource:** `btnSaveAttribute` sets `LoadResource` to the same VBS path — use **same** `LoadResource` const; do not duplicate literal.
5. **ReadBankBIK row names:** `BANKNAME`, `CORRACC` as consts for `Select`/matrix compares.

---

## EXITING CREATIVE PHASE — Inventory for BUILD

### Resource

| Const | Value |
|-------|--------|
| `LoadResource` | `@"VBS:UBS_VBD\PS\UT\UtPaymentF.vbs"` |

### StrCommand (local / channel In)

| Const | Value |
|-------|--------|
| `StrCommandAdd` | `ADD` |
| `StrCommandGroupEdit` | `GROUP_EDIT` |
| `StrCommandGroupAdd` | `GROUP_ADD` |
| `StrCommandRead` | `READ` |
| `StrCommandClear` | `CLEAR` |
| `StrCommandChangeContract` | `CHANGECONTRACT` |
| `StrCommandChangePart` | `CHANGE_PART` |
| `StrCommandAddForSave` | `ADD` | *(Payment_Save forces this on `ParamIn`)* |

### Channel `Run` names (alphabetical)

Use one const per row; suggested names `RunCheckAddFields`, …

| Value |
|-------|
| `CheckAddFields` |
| `CheckKey` |
| `CheckTerror` |
| `FindContrByBicAndAccount` |
| `InitFormGroup` |
| `Payment` |
| `Payment_Save` |
| `PAYMENT` |
| `ReadBankBIK` |
| `ReadClientFromIdOC` |
| `ReadClientFromNomerCard` |
| `ReadRecipFromId` |
| `SaveAttributeRecip` |
| `UTListAddRead` |
| `Ut_CheckBeforeSave` |
| `UtCheckAccFromBic` |
| `UtCheckBIKBank` |
| `UtCheckBIKLimitSharing` |
| `UtGetAccINNFromLastPayment` |
| `UtGetINNFromLastPayment` |
| `UtGetKPPU` |
| `UtGetKPPUPayerLastPayment` |
| `UtGetStateClearFieldSend` |
| `UtReadContract` |
| `UtReadOurBankBik` |
| `UtReadSetupLockPurpose` |

### High-traffic `ParamIn` / `ParamOut` keys (non-exhaustive; full list in channel contract)

**Prefer const for every key used in C#** — duplicate table from `creative-ubspsutpaymentgroupfrm-channel-contract.md` during BUILD. Critical examples:

- `StrCommand`, `IdPayment`, `IdContract`, `IdPaym`, `COMMANDIN`, `Group`, `IdPaymentGroup`, `blnGuest`, `ID_CLIENT`, `blnTerror`, `blnIPDL`
- `txtFIOPay`, `txtINNPay`, `txtAdressPay`, `txtBic`, `AccKorr`, `AccClient`, `txtINN`, `cmbPurpose`, `txtRecip`, `curSumma`, `curSummaRateSend`, `curSummaTotal`, `Address`
- Outs: `StrError`, `strError`, `IdPaym`, `PAYMENTGROUPID`, `bRetVal`, `RETVAL`, `EndGroup`, `GroupContract`, `ChoiceClient`, `SummaPeni`, `NUM`, `Parameters`
- `Ut_CheckBeforeSave`: `IDPAYMENT`, `IDCONTRACT`, `CODE`, `INN`, `BIC`, `CORRACC`, `ACC`, `IDCLIENT`, `PAYERFIO`, `PAYERINN`, `PAYERADDRESS`, `RECIPIENTNAME`, `PURPOSE`, `SUMMA`, `SUMMARATESEND`, `SUMMATOTAL`, `Error`, `bRet`, `AddFields`
- `CheckTerror`: `NAME`, `IDCONTRACT`, `PURPOSE`, `RECIPIENTNAME`, `blnErrorPayer`
- `CheckKey` / `CheckRS`: `STRACC`, `BIC`, `CORRACC`
- `ReadBankBIK` matrix tags: `BANKNAME`, `CORRACC`
- `SaveAttributeRecip` In: `Наименование получателя`, `БИК`, `Корр. счет`, `Наименование банка`, `Р/с`, `ИНН`, `Назначение`
- `ReadRecipFromId` Out: `Наименование получателя в плат. документах`, etc. (see contract)
- Script / interop: `Parent`, `Key`, `Идентификатор группового платежа`, `Идентификатор основного платежа`, `EndGroup`

### Captions (MessageBox / titles)

| Const | Value (VB6) |
|-------|-------------|
| `CaptionForm` | `Платеж` | *(replaces `m_strCaptionForm`)* |
| `CaptionError` | `Ошибка` |
| `CaptionInitCheck` | `Проверка` | *(InitFormGroup `strError`)* |
| `CaptionValidation` | `Проверка корректности данных` |
| `CaptionGroupInput` | `Ввод группы платежей` |
| `CaptionPaymentSpelling` | `Платёж` | *(UtReadSetupLockPurpose / clearRecFields errors — note ё)* |
| `CaptionReadClientPrefix` | `ReadClientFromIdOC ` | *(concat + `CaptionForm`)* |
| `CaptionClearClientDataPrefix` | `Очищать данные клиента. ` | *(concat + `CaptionForm`)* |
| `CaptionCheckAddFieldsPrefix` | `Проверка доп. полей. ` | *(concat + `CaptionForm`)* |

### Messages (body)

| Const | Value |
|-------|--------|
| `MsgIpdlSaveForbidden` | `Проверка ИПДЛ, сохранение запрещено!` |
| `MsgPaymentSavedDb` | `Платеж сохранен в БД` |
| `MsgRecipientRequisitesMissing` | `Реквизиты получателя не заданы!` |
| `MsgRecipientRequisitesSaved` | `Реквизиты получателя сохранены в справочнике` |
| `MsgGroupPaymentListEmpty` | `Список групповых платежей пуст!` |
| `MsgGroupContinue` | `Вы хотите продолжить ввод платежей в группу?` |
| `MsgNoRecipientContract` | `Не выбран договор с получателем!` |
| `MsgPayerFioRequired` | `Введите ФИО плательщика.` |
| `MsgInvalidPaymentAmount` | `Некорректная сумма платежа!` |
| `MsgInvalidTotalAmount` | `Некорректная общая сумма платежа!` |
| `MsgBikLimitContinueQuestionSuffix` | `Продолжить ввод платежа?` | *(preceded by dynamic `strError` + newline)* |
| `MsgInvalidSettlementAccount` | `Некорректный расчетный счет!` |
| `MsgInvalidAccountKey` | `Некорректный ключ счета!` |
| `MsgTerrorChooseClientFromDirectory` | `Клиента подозреваемого в террористической деятельности надо выбрать из справочника!!!` |
| `MsgContractClosedWarning` | `Внимание!!! Договор закрыт.` |
| `MsgRetFromGridIdsEmpty` | `Массив идентификаторов пуст` | *(UbsErrMsg)* |

*Note:* `Ut_CheckUserBeforeSave` uses **dynamic** `strError` for Yes/No and critical dialogs — no const for body.

### Script / COM

| Const | Value |
|-------|--------|
| `ScriptGroupContinuation` | `@"UBS_VBS\PS\PsPaymentIncomingReception.vbs"` |
| `ProgIdUbsRunScript` | `URunScr.IUbsRunScript` |
| `ProgIdUbsComCheck` | `UbsCheck.UbsComCheck` |

### UI strings (when referenced from code)

| Const | Value |
|-------|--------|
| `TabTextMain` | `Основные` |
| `TabTextAddProperties` | `Дополнительные свойства` |
| `GroupPayer` | `Плательщик` |
| `GroupRecipient` | `Получатель` |
| `AddFieldsSupportKey` | Match stub / `UbsCtrlFields` registration (e.g. `Доп. поля` if same as Contract — confirm at BUILD) |

### Placeholders

| Const | Value |
|-------|--------|
| `CorrespondentAccountPlaceholder` | `00000000000000000000` | *(same as Contract pattern)* |

---

## Verification

- [ ] Every `MsgBox` / fixed `Info.Caption` from `UtPaymentGroup_F.dob` maps to a const in the table above.
- [ ] Every `UbsChannel.Run` first argument has a `Run*` const.
- [ ] `StrCommand` comparisons use `StrCommand*` consts.
- [ ] Russian / Latin parameter keys match `creative-ubspsutpaymentgroupfrm-channel-contract.md` byte-for-byte.
- [ ] `CaptionPaymentSpelling` (`Платёж`) kept separate from `CaptionForm` (`Платеж`) per legacy.

---

**Next:** `/build` — paste regions into `UbsPsUtPaymentGroupFrm.Constants.cs` and replace literals as partials are implemented.
