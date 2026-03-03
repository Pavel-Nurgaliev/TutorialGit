# Refactoring Plan: InitDoc() in UbsBgContractFrm.cs

## Current State

- **Method:** `InitDoc()` (lines 299–768, ~470 lines)
- **Role:** Initializes the BG contract form: runs server init, fills combos, applies command-specific logic (Add / Edit / Copy / Prepare), then calls `ReReadDoc()`.
- **Already extracted:** `InitCmsAccounts()`, `InitComboBoxes()` are called from `InitDoc()` and are separate methods.

## Target Structure

Refactor so `InitDoc()` becomes a short orchestration method that only:

1. Runs initial channel init and parses output.
2. Sets initial UI (date, accounts, combos).
3. Applies command-dependent control state.
4. Dispatches to command-specific init (Copy vs Add/Prepare vs Edit).
5. Calls `ReReadDoc()` and applies post-load steps.

Each of these steps should be implemented in dedicated private methods that `InitDoc()` calls.

---

## Proposed New Methods (InitDoc Calls These)

### 1. RunBgContractInitAndParseOutput()

**Extract:** Lines 303–341 (inside `InitDoc`).

**Responsibility:**

- Set `LoadResource`, clear params, get current date.
- Call `UbsInit()`, pass command, run `BG_Contract_Init`.
- Fill `m_paramOut` from channel.
- Call `ubsCtrlFields.Refresh()`.
- Parse and assign:
  - `setRekvBen`, limitExcessCheckOn, reflectComIssue, arrExecutEx
  - `m_arrRates` (with message if parameter missing)
  - `m_isInitCurrencyBonus = false`
- Set `dateOpenGarant.DateValue = m_dateToday`.

**Signature (example):**

```csharp
private void RunBgContractInitAndParseOutput()
```

**Note:** All of this uses `m_paramIn`, `m_paramOut`, `m_command`, and form controls; no new parameters needed.

---

### 2. SetControlsStateByCommand()

**Extract:** Lines 351–390 (the first big `if (m_command == ...)` block after init).

**Responsibility:**

- Set `dateNextPayFee.Enabled` from `m_idState`.
- Depending on `m_command` (Add/Prepare, Copy, else):
  - Set `btnReRead.Enabled`.
  - Call `SetTabsEnabled(...)` with appropriate tab indices.
  - Enable/disable: `cmbOrderPayFee`, `cmbTypePayFee`, `cmbOrderPayFeeBonus`, `cmbTypePayFeeBonus`, and for Copy/Edit also `cmbOrderPayFeeGuarant`, `cmbTypePayFeeGuarant`, `cmbCurrencyRewardGuarant`, `cmbCurrencyPayFee`, `linkModel`.

**Signature (example):**

```csharp
private void SetControlsStateByCommand()
```

---

### 3. InitDocForCopyCommand()

**Extract:** The whole `if (m_command.ToUpperInvariant() == CopyCommand)` block, lines 392–707.

**Responsibility:** All logic that runs only when opening the form in “Copy” mode:

- Run `BG_Contract_Copy` and read copy result into `m_paramOut`.
- Assign basic contract fields (model, beneficiary, guarant, frame contract, IDs, division, OI, warrant, state, currency, guarant, frame contract ID, etc.).
- Frame contract block: dates, terms, limit, issue end date, division, then `SetInfoByFrameContract()` or set principal text.
- Intervals and guarant pay (order, type, next date, `m_arrIntervalGuarant`).
- Portfolio, risk type, quality category, rate reservation.
- Division/executor/warrant/state/currency combos and main contract dates/numbers.
- Currency pay fee, sum guarant, cover type/sum/currency, contract cover text.
- Accounts and “Реквизиты оплаты гарантии”.
- `BGReadModelById` and then:
  - `m_arrPeriodPay`, build `m_arrRateValues` from model/paramOut, `InitTrvRates(true)`.
  - Set all `m_axUbsControlProperty` properties (очередность платежей, досрочное гашение, параметры расчета графиков, и т.д.).
- Model text and type, guarant pay group visibility and controls.
- Pay order/type combo init (with validation messages and optional `CheckParamForClose`/exit).
- Bonus controls state: `SetBonusCtrlsState()`, `SetBonusCtrlsBonusState()`, guarant pay combo.
- `SetTabsEnabled(false, 2, 3)`.
- Model-type branch (UBS_GUARANT vs counter): linkGarant, `m_idGarant`, `txtGarant`.
- Call `RunUbsChannelFunction("BG_Contract_Init_Ucp", ...)`, set `m_arrAccounts`, `FilllstAccounts()`, and `dateNextPayFee.DateValue`.

**Signature (example):**

```csharp
private void InitDocForCopyCommand()
```

**Optional sub-extractions** (if this method is still too long):

- `ApplyCopyCommandBasicFieldsFromParamOut()` — all simple field/ID assignments from `m_paramOut` into fields and combos (no UCP, no rates from model).
- `ApplyCopyCommandRatesAndModelProperties()` — `BGReadModelById`, period pay, rate values, `InitTrvRates`, and `m_axUbsControlProperty` assignments.
- `ApplyCopyCommandPayOrderAndBonusCombos()` — pay order/type combos, validation messages, `SetBonusCtrlsState` / `SetBonusCtrlsBonusState` and guarant pay combo.

You can introduce these after the first extraction if you want smaller methods.

---

### 4. LoadFrameContractForAddOrPrepare()

**Extract:** Lines 716–739.

**Responsibility:**

- If `m_command` is Add or Prepare:
  - Set `m_idState = 4`.
  - Set `m_paramIn.Value("ID", m_idFrameContract)` and run `BGReadFrameContractById`.
  - If `m_paramOut` contains "Ошибка": clear `m_idFrameContract`, show message.
  - Else: set frame contract text, dates, terms, limit, issue end date, division from `m_paramOut`, then call `SetInfoByFrameContract()`.

**Signature (example):**

```csharp
private void LoadFrameContractForAddOrPrepare()
```

---

### 5. ApplyNextPayFeeDatesAndEditLinks()

**Extract:** Lines 741–758.

**Responsibility:**

- If `m_dateNextPayFeeValue` in range, set `dateNextPayFee.DateValue`.
- If `m_dateNextPayFeeBonusValue` in range, set `dateNextPayFeeBonus.DateValue`.
- If `m_command == EditCommand && m_idState == 4`: enable and show links (Principal, Model, FrameContract, Agent).

**Signature (example):**

```csharp
private void ApplyNextPayFeeDatesAndEditLinks()
```

---

## Resulting InitDoc() Skeleton

```csharp
private void InitDoc()
{
    try
    {
        RunBgContractInitAndParseOutput();

        InitCmsAccounts();
        InitComboBoxes();

        dateNextPayFee.Enabled = (m_idState >= 2);  // or move into SetControlsStateByCommand
        SetControlsStateByCommand();

        if (m_command.ToUpperInvariant() == CopyCommand)
            InitDocForCopyCommand();

        ReReadDoc();

        if (m_command == CopyCommand)
            linkModel.Enabled = true;

        LoadFrameContractForAddOrPrepare();
        ApplyNextPayFeeDatesAndEditLinks();
    }
    catch (Exception ex)
    {
        base.Ubs_ShowError(ex);
    }
    finally
    {
        this.Cursor = Cursors.Default;
    }
}
```

You may keep the single `dateNextPayFee.Enabled` line in `InitDoc` or move it into `SetControlsStateByCommand()` for consistency.

---

## Order of Implementation

1. **RunBgContractInitAndParseOutput()** — clear boundary, no branching.
2. **SetControlsStateByCommand()** — self-contained UI state.
3. **ApplyNextPayFeeDatesAndEditLinks()** — small, at the end.
4. **LoadFrameContractForAddOrPrepare()** — small, clear block.
5. **InitDocForCopyCommand()** — largest; optionally split into 2–3 helpers as above.

After each step, run existing tests / manual smoke tests (Add, Edit, Copy, Prepare) to avoid regressions.

---

## Benefits

- **Readability:** `InitDoc()` becomes a short checklist of steps.
- **Testability:** Smaller methods are easier to reason about and (where possible) test.
- **Maintainability:** Command-specific logic lives in one place (e.g. Copy in `InitDocForCopyCommand()`).
- **Reuse:** Helpers like `LoadFrameContractForAddOrPrepare()` or rate/combos logic could be reused or called from other flows later if needed.

---

## Notes

- `InitComboBoxes()` already contains a lot of logic (including `CheckParamForClose("InitDoc")` and exit). Further splitting of `InitComboBoxes()` (e.g. by combo group) can be a separate refactoring step.
- Preserve all existing behavior: same order of operations, same messages, same `CheckParamForClose` and `btnExit_Click` calls.
- Consider adding `#region` blocks or comments around each extracted method’s call site in `InitDoc()` until the team is used to the new structure.
