# Refactoring Plan: ReReadDoc() in UbsBgContractFrm.cs

## Current State

- **Method:** `ReReadDoc()` (lines 1263–1721, ~458 lines)
- **Role:** (Re)loads document state: sets initial UI (date, currency, rate buttons), then for **Edit** command runs `BG_Contract_Read`, fills all fields/combos from `m_paramOut`, handles frame contract, agent, guarant list, model type, pay fee guarant, risk/portfolio, contract dates, bonus/cover, pay order and type combos (with validation), and state-dependent visibility. For non-Edit, only clears `dateCloseGarant.Text`. Finally runs common finalization (rates, accounts, UCP refresh, command/control state, date correction).
- **Called from:** `InitDoc()` only.

## Current Structure (Summary)

1. **Preamble (always)**  
   - If not Copy: `dateOpenGarant.DateValue = m_dateToday`  
   - `idCurrency`, set `cmbCurrencyGarant/Cover/PayFee` by `idCurrency`  
   - Disable `btnAddRate`, `btnDelRate`, `btnEditRate`

2. **If EditCommand** (large block, ~365 lines)  
   - SetTabsEnabled, GuarCmdState, clear params, `BG_Contract_Read`  
   - UID, strError → message + `btnExit_Click` + return  
   - Access: `m_isSecure`, `btnSave.Enabled`, `m_arrPeriodPay`, `m_isFixSum`, executor combo when no access  
   - Main texts and IDs (model, agent, beneficiary, guarant, frame, kind, all `m_id*`, SetEnableCombos)  
   - Frame contract block or `txtPrincipal`  
   - Agent Reward/Cost block (if `m_idAgent > 0`)  
   - Обеспечения → `m_arrGuarant`, `FillLvwGuarant`  
   - `m_idModel` tabs, `m_modelType`, linkGarant and currency logic  
   - Вознаграждение (гарант): `m_orderPayFeeGuarant`, `gbPayFeeGuarant`, dates/intervals  
   - Портфель, Тип оценки риска, Группа риска, Ставка резервирования  
   - Combo SelectedValues (NumberDiv, Executor, Warrant, TypeCover, State, CurrencyGarant)  
   - Contract fields (number, dates, datePrincipal, dateEndGarant)  
   - `m_idState == 4`: linkPreviousContract, currency pay/bonus enabled  
   - `txtPreviousContract`  
   - Currency bonus (pay fee / reward guarant), `m_isInitCurrencyBonus`  
   - Sum guarant, cover type/cover sum/currency, contract cover  
   - Pay order / type bonus combos (with validation and optional exit)  
   - `m_idState == 4` currency enable logic again  
   - **Else** (when `m_idState != 4`): `dateCloseGarant.Text = string.Empty`

3. **Common finalization (always)**  
   - `cmbCurrencyGarant.Enabled = CurrencyGarantEnabled()`  
   - `SetBonusCtrlsState()`  
   - `m_arrRateValues = null`, `InitTrvRates(true)`  
   - `linkModel.Enabled` if `txtModel` has text  
   - `UpdateCover()`  
   - `m_arrAccounts`, `FilllstAccounts()`  
   - `m_axUbsControlProperty.UbsAddFields`, `Refresh`  
   - `datePrincipal.Enabled`  
   - If `m_idState != 4 && m_command != Copy`: `EnabledCmdControl(false)` + disable many controls  
   - `cmbCurrencyGarant.Enabled = CurrencyGarantEnabled()`  
   - `CorrectFormDateCtrls()`

## Target Structure

Refactor so `ReReadDoc()` is a short orchestrator that:

1. Runs a preamble (date, currency combos, rate buttons).
2. For Edit: calls helpers that load/validate contract, then apply all data from `m_paramOut` in logical groups (optionally one helper returns `bool` for “exit and return”).
3. For non-Edit: keeps the single line `dateCloseGarant.Text = string.Empty`.
4. Runs a single “finalization” step (rates, accounts, UCP, control state, dates).

Each extracted method should have a clear, single responsibility and stay within a reasonable size (~30–80 lines). The Edit branch can be split into 4–6 helpers rather than 10+ tiny ones.

---

## Proposed New Methods (ReReadDoc Calls These)

### 1. ReReadDocPreamble()

**Extract:** Lines 1265–1275 (always-run start).

**Responsibility:**

- If `m_command != Copy`: `dateOpenGarant.DateValue = m_dateToday`
- `idCurrency = (m_idCurrency > 0) ? m_idCurrency : BasicCurrency`
- Set `cmbCurrencyGarant`, `cmbCurrencyCover`, `cmbCurrencyPayFee` to `idCurrency`
- `btnAddRate.Enabled = false`, `btnDelRate.Enabled = false`, `btnEditRate.Enabled = false`

**Signature:** `private void ReReadDocPreamble()`

---

### 2. ReReadDocLoadAndValidateContract()

**Extract:** Edit-only: run read, show UID, check error and access (lines ~1282–1326).

**Responsibility:**

- If not Edit command: return `true` (no-op).
- `SetTabsEnabled(true, 4)`, `GuarCmdState(true)`
- Clear `m_paramIn`/`m_paramOut`, `m_paramIn.Value("Id", m_idContract)`, `RunUbsChannelFunction("BG_Contract_Read", ...)`
- `lblUID.Visible = true`, `lblUID.Text = ...`
- If `m_paramOut.Contains("strError")`: message, `btnExit_Click`, **return false**
- If `Доступ == 0`: `m_isSecure = false`, message, `btnSave.Enabled = false`
- `m_arrPeriodPay`, `m_isFixSum`
- If no access and paramOut has ОИ: init `cmbExecutor` with single item, set `SelectedValue`
- **Return true**

**Signature:** `private bool ReReadDocLoadAndValidateContract()`

**Returns:** `false` when caller must return (error path); `true` to continue. Caller: `if (!ReReadDocLoadAndValidateContract()) return;`

---

### 3. ReReadDocApplyContractMainData()

**Extract:** Edit-only: texts, IDs, frame contract or principal (lines ~1328–1378).

**Responsibility:**

- All `txt*` and `cmbKindGarant` from `m_paramOut` (model, agent, num/date agent, beneficiary, guarant, frame contract, kind).
- All `m_id*` (model, agent, principal, beneficiary, division, OI, warrant, state, currency, cover type, guarant, frame contract).
- `m_arrDetailsBeneficiar` when `m_idBeneficiar == 0`.
- `SetEnableCombos()`.
- If `m_idFrameContract > 0`: set frame contract dates/terms/limit/issue end/division, `SetInfoByFrameContract()`; else `txtPrincipal.Text = ...`.

**Signature:** `private void ReReadDocApplyContractMainData()`

**Note:** Call only when command is Edit (caller already inside `if (EditCommand)`).

---

### 4. ReReadDocApplyAgentGuarantListAndModel()

**Extract:** Edit-only: agent reward/cost, guarant list, model tabs and guarant link (lines ~1379–1448).

**Responsibility:**

- If `m_idAgent > 0`: dateReward, costAmount, run `AgentRewardAvaliable`, dateAdjustment, paidAmount, transAmount.
- If `m_paramOut.Contains("Обеспечения")`: `m_arrGuarant`, `FillLvwGuarant()`.
- If `m_idModel == 0` then `SetTabsEnabled(false, 2, 3)` else `SetTabsEnabled(true, 2, 3)`.
- `m_modelType = m_paramOut["Шаблон"]`.
- If `m_modelType == UbsGuarantCommand`: linkGarant and currency logic by `m_idState`, `m_idGarant = 0`, `txtGarant.Text = string.Empty`; else `linkGarant.Enabled = true`.

**Signature:** `private void ReReadDocApplyAgentGuarantListAndModel()`

---

### 5. ReReadDocApplyPayFeeGuarantRiskAndCombos()

**Extract:** Edit-only: pay fee guarant, portfolio/risk/rate, combos by ID, contract dates and previous contract (lines ~1449–1540).

**Responsibility:**

- `m_orderPayFeeGuarant`, `gbPayFeeGuarant` visibility, `m_typePayFeeGuarant`, date and `m_arrIntervalGuarant` when “Периодически”.
- Portfolio, type validation risk, quality category, rate reservation from `m_paramOut` (when present).
- Set combo `SelectedValue`: NumberDiv, Executor, Warrant, TypeCover, State, CurrencyGarant.
- Contract fields: txtNumberGarant, dateOpenGarant, dateCloseGarant, dateBeginGarant, datePrincipal, m_dateBegin, dateEndGarant.
- If `m_idState == 4`: linkPreviousContract.Enabled, currency pay/bonus by PercentSumGuarant; else linkPreviousContract.Enabled = false.
- `txtPreviousContract.Text = m_paramOut["InfoPrevContract"]`.

**Signature:** `private void ReReadDocApplyPayFeeGuarantRiskAndCombos()`

---

### 6. ReReadDocApplyBonusCoverAndPayOrderCombos()

**Extract:** Edit-only: bonus currencies, sum/cover, contract cover, pay order and type combos with validation (lines ~1541–1638).

**Responsibility:**

- Currency pay fee and reward guarant IDs, set combos, `m_isInitCurrencyBonus = true`.
- `ucdSumGarant`, `m_idCoverType`, `cmbTypeCover`, `ucdSumCover`, `cmbCurrencyCover`, `m_idContractCover`, `txtContractCover`.
- Pay order combo from `m_paramOut` (with validation message and optional `CheckParamForClose` + `btnExit_Click` + return). Use existing logic (InitComboBox + MakeKvpList for object[]).
- Type bonus combo from `m_paramOut` (same validation pattern).
- When `arrTypeBonus != null`: PercentSumGuarant → disable currency pay fee / reward guarant.
- If `m_idState == 4`: currency pay/bonus enable by PercentSumGuarant again.

**Signature:** `private bool ReReadDocApplyBonusCoverAndPayOrderCombos()`

**Returns:** `false` when validation failed and caller should return; `true` otherwise. Caller: `if (!ReReadDocApplyBonusCoverAndPayOrderCombos()) return;`

---

### 7. ReReadDocApplyEditNonDraftVisibility()

**Extract:** Edit-only: the final `else` that clears dateCloseGarant when not state 4 (lines ~1639–1642).

**Responsibility:**

- `else { dateCloseGarant.Text = string.Empty; }` (the branch for `m_idState != 4` after the last `if (m_idState == 4)` block).

**Signature:** `private void ReReadDocApplyEditNonDraftVisibility()`

**Note:** This keeps current behavior. Alternatively this line can be inlined in the caller or merged into the previous helper.

---

### 8. ReReadDocFinalize()

**Extract:** Common tail (lines 1644–1720).

**Responsibility:**

- `cmbCurrencyGarant.Enabled = CurrencyGarantEnabled()`
- `SetBonusCtrlsState()`
- `m_arrRateValues = null`, `InitTrvRates(true)`
- If `txtModel.Text.Length > 0`: `linkModel.Enabled = false`
- `UpdateCover()`
- `m_arrAccounts = m_paramOut.Value("Счета")`, `FilllstAccounts()`
- `m_axUbsControlProperty.UbsAddFields = ubsCtrlFields`, `Refresh`
- If `m_idState != 2 && m_idState != 4`: `datePrincipal.Enabled = false`
- If `m_idState != 4 && m_command != Copy`: full block (EnabledCmdControl, disable many controls, btnSave/ReRead when state 1)
- `cmbCurrencyGarant.Enabled = CurrencyGarantEnabled()`
- `CorrectFormDateCtrls()`

**Signature:** `private void ReReadDocFinalize()`

---

## Resulting ReReadDoc() Skeleton

```csharp
private void ReReadDoc()
{
    ReReadDocPreamble();

    if (m_command.ToUpperInvariant() == EditCommand)
    {
        if (!ReReadDocLoadAndValidateContract())
            return;

        ReReadDocApplyContractMainData();
        ReReadDocApplyAgentGuarantListAndModel();
        ReReadDocApplyPayFeeGuarantRiskAndCombos();

        if (!ReReadDocApplyBonusCoverAndPayOrderCombos())
            return;

        ReReadDocApplyEditNonDraftVisibility();
    }
    else
    {
        dateCloseGarant.Text = string.Empty;
    }

    ReReadDocFinalize();
}
```

---

## Order of Implementation

1. **ReReadDocPreamble()** — small, no branching.
2. **ReReadDocFinalize()** — clear boundary at end.
3. **ReReadDocLoadAndValidateContract()** — returns bool, contains the only “return” in the Edit path besides bonus combos.
4. **ReReadDocApplyContractMainData()** — one coherent block.
5. **ReReadDocApplyAgentGuarantListAndModel()** — next coherent block.
6. **ReReadDocApplyPayFeeGuarantRiskAndCombos()** — next block.
7. **ReReadDocApplyBonusCoverAndPayOrderCombos()** — returns bool for validation/exit.
8. **ReReadDocApplyEditNonDraftVisibility()** — single else (or merge into previous).
9. **Replace ReReadDoc() body** with the skeleton above.

After each step, run or manually test opening the form in Edit and non-Edit modes to avoid regressions.

---

## Benefits

- **Readability:** ReReadDoc becomes a short sequence: preamble → Edit (load/validate → apply main → agent/guarant/model → pay fee/risk/combos → bonus/cover/combos → non-draft visibility) or else clear date → finalize.
- **Maintainability:** Each helper has a clear theme (load/validate, main data, agent/guarant/model, pay fee and risk, bonus/cover/combos, finalize).
- **Testability:** Smaller methods are easier to reason about; load/validate and bonus/combos can be tested for error paths.
- **Consistency:** Same orchestration style as InitDoc and ListKey.

---

## Notes

- Preserve all existing behavior: same messages, same `CheckParamForClose("InitDoc")` and `btnExit_Click` in pay order/type combo blocks.
- `ReReadDocApplyEditNonDraftVisibility()` is a single else; you can inline it in the skeleton or keep it for symmetry.
- If any helper grows too large (e.g. ApplyPayFeeGuarantRiskAndCombos), split further in a second pass (e.g. separate “risk/portfolio” from “contract combos and dates”).
