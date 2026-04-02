# CREATIVE: InitDoc — Full VB6-to-C# Conversion

**Source:** `Pm_Trade_ud.dob`, `Private Sub InitDoc` (lines 3499–3867)
**Target:** `UbsPmTradeFrm.cs`, `private void InitDoc()`

---

## 1. Channel Calls Used by InitDoc

| # | Command | ParamIn | ParamOut | When |
|---|---------|---------|----------|------|
| 1 | `InitForm` | — | — | Always |
| 2 | `FillOurBIK` | — | `Наш БИК` | Always |
| 3 | `GetOneTrade` | `ID_TRADE` | All trade fields | EDIT |
| 4 | `GetStorage` | `IsExternalStorage`, `Id` | `Code`, `Name` | EDIT |
| 5 | `FillBaseCurrency` | — | `Идентификатор базовой валюты` | Always |
| 6 | `GetContractPM` | `ID_CONTRACT` | `CODE_CONTRACT`, `LONG_NAME`, `ID_CLIENT`, `TYPE_CONTRACT` | EDIT (×2) |
| 7 | `FillObligPM` | `ID_TRADE` | `Обязательства сделки`, `Обязательства сделки2` | EDIT |
| 8 | `PMCheckOperationByTrade` | `ID_TRADE` | `Was_Operation` | EDIT |
| 9 | `GetRate_CB` | `Id_Currency_Opl`, `Id_Currency_Oblig`, `Date` | `Rate` | Always (tail) |
| 10 | `GetRateForPM` | `Id_Pm`, `Id_Currency_Oblig`, `Date` | `Rate_PM` | Always (tail) |

## 2. New Fields

| Field | Type | Purpose |
|-------|------|---------|
| `m_ourBIK` | `string` | Bank BIK for cash register comparison |
| `m_idBaseCurrency` | `int` | Base currency ID |
| `m_wasOperation` | `bool` | Flag: operations exist for trade |
| `m_maxNumPart1` | `int` | Max obligation index for part 1 |

## 3. New/Modified Methods

| Method | Action | Purpose |
|--------|--------|---------|
| `FillOurBIK` | Modified | Store to `m_ourBIK` instead of text boxes |
| `FillControlStorage` | New | Load storage code/name from channel |
| `FillListOblig` | New | Populate obligation ListView from 2D array |
| `GetRate_CB` | New | Fetch obligation exchange rate, set ucdRateCurOblig |
| `GetRateForPM` | New | Fetch PM rate, set ucdCostUnit |
| `LockUiOnWasOperation` | Modified | Per-panel granularity per VB6 `EnableWindow` calls |

## 4. Execution Order

```
InitDoc
├─ InitForm + UbsInit
├─ Reset flags (blnAddOblig, blnEditOblig, etc.)
├─ FillCombos + FillOurBIK
├─ if EDIT:
│   ├─ GetOneTrade → extract all fields into UbsParam
│   ├─ Map: DATE_TRADE, NUM_TRADE, IS_COMPOSIT, Вид сделки
│   ├─ Extract: IsNDS, IsExport, IsExternalStorage, payment instr arrays
│   ├─ FillControlStorage
│   ├─ SetComboByKey: TYPE_TRADE, Вид поставки, валюты, драг.металл, комиссия
│   ├─ FillBaseCurrency → cmbObligationCurrency
│   ├─ FillContractRowFromPm × 2 → get TYPE_CONTRACT for each side
│   ├─ Set cmbContractType1/2 (m_suppressContractTypeEvent = true)
│   ├─ FillObligPM → FillListOblig
│   ├─ FillControlInstrPayment × 2 (buyer, seller)
│   └─ PMCheckOperationByTrade → m_wasOperation
├─ if ADD:
│   ├─ dateTrade = today, default precious metal (1001)
│   ├─ FillBaseCurrency → payment + obligation currency
│   ├─ cmbContractType1/2 = index 0 (suppressed)
│   └─ m_maxNumPart1 = m_maxNumPart2 = 0
├─ COMMON:
│   ├─ Read-only controls (txtContractCode, txtClientName, txtStorageCode, etc.)
│   ├─ chkComposit.Visible = (m_kindTrade != 0)
│   ├─ cmbTradeDirection: "прямая"/disabled or enabled if composit checked
│   ├─ ucdMass.Enabled = false
│   ├─ ApplyContractType1/2Change(false) → handles btnContract, cash visibility, commission, payment tabs
│   ├─ m_checkCommissionRestoreType2 = true
│   ├─ EDIT: override NDS/Export from server (isNDS==1 → show+check)
│   ├─ EDIT: cash register logic (BIK/RS check → chkCash visible+checked)
│   ├─ tabPage3.Enabled = obligations exist
│   ├─ GetRate_CB + GetRateForPM
│   └─ EDIT && wasOper: LockUiOnWasOperation
└─ end try/catch
```

## 5. LockUiOnWasOperation — Panel Mapping

| VB6 | C# | Description |
|-----|-----|-------------|
| `SSTabsPanel1` | `tabPage1` | Основные |
| `SSTabsPanel4` | `tabPage4` | Поставка |
| `SSActiveTabPanel1` | `tabPage5` | Оплата |
| `SSActiveTabPanel2` | `tabPage6` | Дополнительные |
| `SSTabs1Panel1` | `tabPageOblig1` | Обязательство (detail) |
| `SSTabs1Panel4` | `tabPageOblig2` | Объекты |
| `cmdAddOblig/Edit/Del` | `cmdAdd/Edit/DelObligation` | Obligation buttons |
| `cmdSave` | `btnSave` | Save button |

Note: `SSTabsPanel2` (Обязательства list) is NOT disabled — commented out in VB6.

## 6. Array Transpose Rule

VB6 `variant(fieldIdx, rowIdx)` → C# `object[rowIdx, fieldIdx]`

- `FillControlInstrPayment`: VB `varOplata(i, 0)` → C# `arr[0, i]` — already implemented correctly.
- `FillListOblig`: VB `arrOblig(fieldIdx, n)` → C# `arr[n, fieldIdx]`.
- `GetOneTrade` params: accessed by name via `UbsParam.Contains/Value`, no transpose needed.

## 7. Implementation Checklist

- [x] Creative document
- [ ] Add new fields (m_ourBIK, m_idBaseCurrency, m_wasOperation, m_maxNumPart1)
- [ ] Fix FillOurBIK
- [ ] New method: FillControlStorage
- [ ] New method: FillListOblig
- [ ] New method: GetRate_CB
- [ ] New method: GetRateForPM
- [ ] Rewrite InitDoc (EDIT branch)
- [ ] Rewrite InitDoc (ADD branch)
- [ ] Rewrite InitDoc (common tail)
- [ ] Refine LockUiOnWasOperation
