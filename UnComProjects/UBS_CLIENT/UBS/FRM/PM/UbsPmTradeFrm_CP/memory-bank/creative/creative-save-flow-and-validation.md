# CREATIVE: Save Flow & Validation — `cmdSave_Click`, `CheckData`, `CheckDatesOblig`, Array Serialization

**Source:** `Pm_Trade_ud.dob`:
- `cmdSave_Click` (~L2418–2532)
- `cmdExit_Click` (~L2534–2545)
- `CheckData` (~L4006–4157)
- `CheckDatesOblig` (~L5140–5246)
- `FillArrOblig` (~L4796–4825)
- `FillArrDataInstr` (~L4859–4878)
- `UserDocument_Hide` (~L3285–3317)

**Scope:** The full save pipeline from button click through validation, parameter assembly, `ModifyTrade` channel call, error handling, and form close. Plus the exit/cancel path.

---

## 1. Control Mapping (VB6 → .NET)

| VB6 | .NET | Notes |
|-----|------|-------|
| `DDX.UpdateData(True)` | N/A | DDX replaced by `m_mc`; values live in controls directly |
| `DDX.IsChange` | compare `m_mc` snapshot vs current control values | See §4 |
| `DDX.MemberData("X")` | `m_mc["X"]` | |
| `DDX.ChangeMembersValue(arr)` | build change dict from `m_mc` | |
| `DDX.SetInitMemberData` | re-snapshot `m_mc` from current controls | `FulFillMainCollection(m_mc)` + updated values |
| `objParamIn` / `objParamOut` | `base.IUbsChannel.ParamIn` / `base.IUbsChannel.ParamsOut` | |
| `objParamTrade` | local `UbsParam` for changed fields | |
| `UbsChannel1.Run "ModifyTrade"` | `base.IUbsChannel.Run("ModifyTrade")` | |
| `Info.Caption` / `Info.Show` | `this.Ubs_ShowInfo(MsgSaved)` | UbsCtrlInfo via base |
| `IParent.CloseWindow Numwin` | `this.Ubs_CloseForm()` | |
| `objFormat.Round(x, n)` | `Math.Round(x, n)` | |

---

## 2. `CheckData()` — Trade-Level Validation

Returns `bool`. Shows `MessageBox` on first failure and returns `false`.

| # | Check | Message | Focus | Icon |
|---|-------|---------|-------|------|
| 1 | `cmbContractType1` key == `cmbContractType2` key | `MsgContractTypesMustDiffer` | — | Critical |
| 2 | `cmbKindSupplyTrade.SelectedIndex == -1` | `MsgNoKindSupply` | `cmbKindSupplyTrade` (if tab 1 selected) | Exclamation |
| 3 | `dateTrade` invalid or == `2222-01-01` | `MsgNoTradeDate` | `dateTrade` (if tab 1) | Critical |
| 4 | `txtTradeNum.Text` is empty | "Не указан номер сделки" | navigate to tab 1, focus `txtTradeNum` | Exclamation |
| 5 | `cmbCurrencyPost.SelectedIndex == -1` | `MsgNoCurrencyPost` | `cmbCurrencyPost` (if tab 1) | Exclamation |
| 6 | `cmbCurrencyPayment.SelectedIndex == -1` | "Не выбрана валюта оплаты" | `cmbCurrencyPayment` (if tab 1) | Exclamation |
| 7 | `cmbObligationCurrency.SelectedIndex == -1` | "Не выбрана валюта обязательства" | navigate to tab 3/sub-tab 1, focus `cmbObligationCurrency` | Exclamation |
| 8 | `cmbUnit.SelectedIndex == -1` | "Не указана единица измерения веса" | navigate to tab 3/sub-tab 1, focus `cmbUnit` | Exclamation |
| 9 | `txtContractCode1.Text` empty | "Не выбран продавец" | focus `linkContract1` or `cmbContractType1` | Exclamation |
| 10 | `txtContractCode2.Text` empty | `MsgNoBuyer` | focus `linkContract2` or `cmbContractType2` | Exclamation |
| 11 | `lvwObligation.Items.Count == 0` | "Отсутствуют обязательства" | — | Exclamation |
| 12a | Buyer payment tab visible AND (`txtBIK0` empty OR `ucaKS0 == "0"` OR `ucaRS0 == "0"`) | "Отсутствует инструкция по оплате у покупателя" | navigate to tab 5, buyer sub-tab, focus `linkListInstr0` | Exclamation |
| 12b | Seller payment tab visible+enabled AND (`txtBIK1` empty OR `ucaKS1 == "0"` OR `ucaRS1 == "0"`) | "Отсутствует инструкция по оплате у продавца" | navigate to tab 5, seller sub-tab, focus `linkListInstr1` | Exclamation |
| 13 | Kind == "физическая" AND any obligation missing "Object" key in `m_paramOblig` | "Отсутствует список объектов по обязательству" | — | Exclamation |
| 14 | Storage tab visible AND `txtStorageCode.Text` empty | "Отсутствует инструкция по поставке" | navigate to tab 4, focus `linkStorage` | Exclamation |

### .NET approach

```csharp
private bool CheckData()
{
    int typeKey1, typeKey2;
    UbsPmTradeComboUtil.TryGetSelectedKey(cmbContractType1, out typeKey1);
    UbsPmTradeComboUtil.TryGetSelectedKey(cmbContractType2, out typeKey2);
    if (typeKey1 == typeKey2)
    {
        MessageBox.Show(MsgContractTypesMustDiffer, MsgTitleValidationProps,
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
    }

    // ... each check in sequence, returning false on first failure
}
```

**Navigation helpers:** When a check needs to navigate to a specific tab before focusing a control, use:
```csharp
m_suppressMainTabSelecting = true;
tabControl.SelectedTab = tabPageN;
m_suppressMainTabSelecting = false;
controlToFocus.Focus();
```

### New Constants needed

```
MsgNoTradeNum = "Не указан номер сделки"
MsgNoCurrencyPayment = "Не выбрана валюта оплаты"
MsgNoCurrencyObligation = "Не выбрана валюта обязательства"
MsgNoWeightUnit = "Не указана единица измерения веса"
MsgNoSeller = "Не выбран продавец"
MsgNoObligations = "Отсутствуют обязательства"
MsgNoBuyerInstruction = "Отсутствует инструкция по оплате у покупателя"
MsgNoSellerInstruction = "Отсутствует инструкция по оплате у продавца"
MsgNoObjectsByObligation = "Отсутствует список объектов по обязательству"
MsgNoStorageInstruction = "Отсутствует инструкция по поставке"
MsgSaved = "Данные сохранены"
MsgTitleInputError2 = "Ошибка ввода" (already exists as MsgTitleInputError)
```

---

## 3. `CheckDatesOblig()` — Date & Currency Validation

Returns `bool`. Called from `cmdSave_Click` and `txtTradeDate_LostFocus`.

### 3.1 Trade date check

If `dateTrade` invalid or `== 2222-01-01`:
- Show "Укажите дату сделки" (Critical).
- Navigate to tab 1, focus `dateTrade`.
- Return `false`.

### 3.2 Obligation dates loop

For each item in `lvwObligation`:
- Read `dateOpl` from SubItems[2], `datePost` from SubItems[3].
- **Cash trades** (`m_kindTrade == 0`): overwrite both dates with `dateTrade.DateValue` in the ListView.
- **Forward trades** (`m_kindTrade != 0`):
  - If `datePost < dateTrade`: error "Дата поставки не может быть меньше даты сделки." → return `false`.
  - If `dateOpl < dateTrade`: error "Дата оплаты не может быть меньше даты сделки." → return `false`.

### 3.3 RS currency code validation

After dates pass:
1. Get payment currency CB code:
   ```
   ParamIn("IdCurrency") = paymentCurrencyKey
   Run("DefineCodCurrency")
   strCodCurrencyOpl = ParamOut("CodCB")
   ```
2. **Buyer instruction** (if buyer payment tab visible AND contract type 2 key != 0):
   - Extract RS currency code: `strCodCurrencyRS = ucaRS0.Text.Substring(5, 3)`
   - If `txtBIK0.Text.Trim() == m_ourBIK` (our bank):
     - Call `IsEqualNumCodeCurr(strCodCurrencyOpl, strCodCurrencyRS)` — if not equal: error → return false.
   - Else if `ucaRS0.Text != "00000000000000000000"`:
     - Same check.
3. **Seller instruction** (if seller payment tab visible AND contract type 1 key != 0):
   - Same as buyer but with `ucaRS1`, `txtBIK1`.

### Channel contract for validation

| Call | ParamIn | ParamOut | Purpose |
|------|---------|----------|---------|
| `DefineCodCurrency` | `IdCurrency` | `CodCB` | Get CB code for currency |

### `IsEqualNumCodeCurr` equivalent

VB6 uses `LibTools.IUbsTools.IsEqualNumCodeCurr`. In .NET, this is typically available as a static method or we replicate:
- Compare two 3-digit currency codes for numeric equivalence.
- **Decision:** Create a helper `IsEqualNumCodeCurr(string code1, string code2)` that either:
  - (A) Creates `LibTools.IUbsTools` via COM interop and calls the method.
  - (B) Implements the comparison directly (both codes represent the same numeric value).
- **Recommendation:** Option A preserves exact server-side logic. If COM is unavailable, Option B as fallback: `int.Parse(code1) == int.Parse(code2)`.

### New Constants

```
MsgSpecifyTradeDate = "Укажите дату сделки"
MsgDeliveryDateBeforeTrade2 = "Дата поставки не может быть меньше даты сделки."  (already exists)
MsgPaymentDateBeforeTrade2 = "Дата оплаты не может быть меньше даты сделки."  (already exists)
MsgBuyerRsCurrencyMismatch = "Валюта расчетного счета инструкции оплаты покупателя должна соответствовать валюте оплаты."
MsgSellerRsCurrencyMismatch = "Валюта расчетного счета инструкции оплаты продавца должна соответствовать валюте оплаты."
```

---

## 4. DDX Change Detection — `m_mc` Snapshot Approach

VB6 uses `DDX.IsChange` / `DDX.ChangeMembersValue` to detect and collect field changes. We replaced DDX with `m_mc` dictionary. Need to:

1. At load time (`InitDoc`), snapshot current field values into `m_mc` (already done).
2. At save time, compare current control values against `m_mc` to detect changes.
3. Build a change set (`UbsParam objParamTrade`) with only changed fields.

### Change detection helper

```csharp
private bool IsMainDataChanged()
{
    // Compare current control values with m_mc snapshot
    // Return true if any field differs
}

private UbsParam BuildChangedFields()
{
    UbsParam changed = new UbsParam();

    // For each tracked field, compare current vs m_mc, add if different
    // Override specific fields with combo keys (not text)

    return changed;
}
```

### Tracked fields and their m_mc keys

| m_mc Key | Control | Value Type |
|----------|---------|------------|
| `DATE_TRADE` | `dateTrade.DateValue` | DateTime |
| `NUM_TRADE` | `txtTradeNum.Text` | string |
| `TYPE_TRADE` | `cmbTradeType` selected key | int |
| `IS_COMPOSIT` | `chkComposit.Checked ? 1 : 0` | int |
| `Идентификатор комиссии` | `cmbComission` selected key (or 0) | int |
| `Идентификатор валюты обязательства` | `cmbObligationCurrency` selected key | int |
| `Идентификатор валюты оплаты` | `cmbCurrencyPayment` selected key | int |
| `Идентификатор драг.металла` | `cmbCurrencyPost` selected key | int |
| `Вид поставки по сделке` | `cmbKindSupplyTrade.Text` | string |
| `Курс ЦБ` | — (rate from channel, not directly editable) | decimal |

### Override fields in objParamTrade

The legacy code explicitly overrides certain fields in the change set with combo key values:
- If `"Идентификатор валюты оплаты"` changed → set to `cmbCurrencyPayment` key.
- If `"Идентификатор драг.металла"` changed → set to `cmbCurrencyPost` key.
- If `"Вид поставки по сделке"` changed → set to `cmbKindSupplyTrade.Text`.
- If `"Идентификатор комиссии"` changed → set to `cmbComission` key (or 0 if unselected).

---

## 5. `FillArrOblig()` — Serialize Obligations to 2D Array

VB6 shape: `ReDim varArr(11, count-1)` → `variant(field_idx, oblig_idx)`.

**.NET shape** (per array rule — VB `(col, row)` → .NET `[row, col]`):
`object[count, 12]` where `[oblig_idx, field_idx]`.

### Field mapping

| .NET col | VB col | Source | Description |
|----------|--------|--------|-------------|
| 0 | 0 | `item.Text` | Direction |
| 1 | 1 | `item.SubItems[1].Text` | NumInPart |
| 2 | 2 | `item.SubItems[2].Text` | DateOpl |
| 3 | 3 | `item.SubItems[3].Text` | DatePost |
| 4 | 4 | `Convert.ToDecimal(item.SubItems[4].Text)` | CostUnit |
| 5 | 5 | `Convert.ToDecimal(item.SubItems[5].Text)` | Massa |
| 6 | 6 | `Convert.ToDecimal(item.SubItems[6].Text)` | SumOblig |
| 7 | 7 | `cmbCurrencyPayment` selected key | Id currency payment (global, not per-oblig) |
| 8 | 8 | `cmbCurrencyPost` selected key | Id currency post (global) |
| 9 | 9 | `item.SubItems[7].Text` | Id currency obligation (per-oblig) |
| 10 | 10 | If rateFlag `SubItems[10]=="1"`: `Convert.ToDouble(SubItems[8])`, else `0` | Fixed rate (or 0) |
| 11 | 11 | `item.SubItems[9].Text` | Unit text |

```csharp
private object[,] FillArrOblig()
{
    int count = lvwObligation.Items.Count;
    if (count == 0) return null;

    object[,] arr = new object[count, 12];

    int paymentKey, postKey;
    UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPayment, out paymentKey);
    UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPost, out postKey);

    for (int n = 0; n < count; n++)
    {
        ListViewItem item = lvwObligation.Items[n];
        arr[n, 0] = item.Text;
        arr[n, 1] = item.SubItems[1].Text;
        arr[n, 2] = item.SubItems[2].Text;
        arr[n, 3] = item.SubItems[3].Text;
        arr[n, 4] = Convert.ToDecimal(item.SubItems[4].Text);
        arr[n, 5] = Convert.ToDecimal(item.SubItems[5].Text);
        arr[n, 6] = Convert.ToDecimal(item.SubItems[6].Text);
        arr[n, 7] = paymentKey;
        arr[n, 8] = postKey;
        arr[n, 9] = item.SubItems[7].Text;  // obligation currency id

        bool isFixedRate = (item.SubItems[10].Text == "1");
        arr[n, 10] = isFixedRate ? Convert.ToDouble(item.SubItems[8].Text) : (object)0m;
        arr[n, 11] = item.SubItems[9].Text;  // unit
    }

    return arr;
}
```

---

## 6. `FillArrDataInstr()` — Serialize Payment Instruction

VB6 shape: `ReDim varArr(7, 0)` → `variant(field_idx, 0)`.

**.NET shape**: `object[1, 8]` where `[row=0, col=field_idx]`.

```csharp
private object[,] FillArrDataInstr(int index)
{
    object[,] arr = new object[1, 8];

    if (index == 0)
    {
        arr[0, 0] = txtBIK0.Text;
        arr[0, 1] = txtName0.Text;
        arr[0, 2] = ucaKS0.Text;
        arr[0, 3] = ucaRS0.Text;
        arr[0, 4] = txtClient0.Text;
        arr[0, 5] = txtNote0.Text;
        arr[0, 6] = txtINN0.Text;
        arr[0, 7] = Math.Abs(chkNotAkcept0.Checked ? 1 : 0);
    }
    else
    {
        arr[0, 0] = txtBIK1.Text;
        arr[0, 1] = txtName1.Text;
        arr[0, 2] = ucaKS1.Text;
        arr[0, 3] = ucaRS1.Text;
        arr[0, 4] = txtClient1.Text;
        arr[0, 5] = txtNote1.Text;
        arr[0, 6] = txtINN1.Text;
        arr[0, 7] = Math.Abs(chkNotAkcept1.Checked ? 1 : 0);
    }

    return arr;
}
```

---

## 7. `cmdSave_Click` — Orchestration

### Flow

```
cmdSave_Click()
├─ UpdateMcFromControls()        // sync m_mc from controls (replaces DDX.UpdateData)
├─ if (!CheckData()) return
├─ if (!CheckDatesOblig()) return
├─ ClearChannelParams()
├─ ParamIn("ID_TRADE") = (EDIT ? m_mc["ID_TRADE"] : 0)
│
├─ if (buyer payment tab visible):
│   ├─ FillArrDataInstr(0) → ParamIn("Инструкция по оплате для покупателя")
├─ if (seller payment tab visible):
│   ├─ FillArrDataInstr(1) → ParamIn("Инструкция по оплате для продавца")
│
├─ ParamIn("IsNDS")              = chkNDS.Checked ? 1 : 0
├─ ParamIn("IsExport")           = chkExport.Checked ? 1 : 0
├─ ParamIn("IsExternalStorage")  = chkExternalStorage.Checked ? 1 : 0
│
├─ if (IsMainDataChanged()):
│   ├─ BuildChangedFields() → objParamTrade
│   ├─ ParamIn("Основные данные") = objParamTrade
│   └─ blnRun = true
│
├─ if (m_needSendOblig):
│   ├─ ParamIn("ID_CLIENT1") = m_idClient1
│   ├─ ParamIn("ID_CLIENT2") = m_idClient2
│   ├─ FillArrOblig() → ParamIn("Обязательства сделки")
│   ├─ ParamIn("Обязательства сделки2") = m_paramOblig
│   └─ blnRun = true
│
├─ if (blnAddFlChanged && m_command == CmdEdit):
│   └─ blnRun = true
│
├─ if (blnRun):
│   ├─ IUbsChannel.Run("ModifyTrade")
│   ├─ if ParamOut has "Ошибка":
│   │   └─ MessageBox error
│   ├─ else:
│   │   ├─ m_mc["ID_TRADE"] = ParamOut("ID_TRADE")
│   │   ├─ m_command = CmdEdit
│   │   ├─ blnNeedRefresh = true, blnNeedSelect = true
│   │   ├─ blnCloseForm = true
│   │   ├─ blnAddFlChanged = false
│   │   ├─ Ubs_ShowInfo(MsgSaved)
│   │   └─ re-snapshot m_mc
│
├─ if (blnCloseForm):
│   └─ Ubs_CloseForm()
└─ catch → Ubs_ShowError(ex)
```

### New fields needed

| Field | Type | Purpose |
|-------|------|---------|
| `m_blnAddFlChanged` | `bool` | Dirty flag for additional fields (ucpParam) |
| `m_blnNeedRefresh` | `bool` | Tell parent grid to refresh |
| `m_blnNeedSelect` | `bool` | Tell parent grid to select saved item |
| `m_blnCloseForm` | `bool` | Close form after successful save |

### Payment tab visibility check

In VB6: `SSActiveTabs1.Tabs(1).Visible`. In .NET, the buyer/seller sub-tabs within the payment tab control. We need to check if the sub-tab page is visible in the collection.

**Decision:** Use `tabControlPayment.TabPages.Contains(tabPageBuyer)` or a bool field `m_buyerTabVisible` / `m_sellerTabVisible` set during `SetPaymentInstrTabs*` methods (already managing tab pages by adding/removing them).

---

## 8. `cmdExit_Click` — Close Form

```csharp
private void cmdExit_Click(object sender, EventArgs e)
{
    try
    {
        this.Ubs_CloseForm();
    }
    catch (Exception ex) { this.Ubs_ShowError(ex); }
}
```

---

## 9. Form Cleanup (UserDocument_Hide Equivalent)

VB6's `UserDocument_Hide` does:
1. Release DDX, channel, COM objects.
2. Notify parent: refresh grid if needed, select saved item.

In .NET, these are handled by `UbsFormBase`:
- `Ubs_CloseForm()` triggers the parent notification mechanism.
- `m_blnNeedRefresh` / `m_blnNeedSelect` are communicated via `Ubs_RefreshList` and `Ubs_RetSelect` (check base class API).

**Decision:** The `UbsFormBase` infrastructure handles parent notification. We just need to set the appropriate base properties before close:
```csharp
base.Ubs_RefreshList = m_blnNeedRefresh;
base.Ubs_RetSelect = m_mc["ID_TRADE"];
```

---

## 10. `UpdateMcFromControls()` — Sync Dictionary Before Save

Replaces `DDX.UpdateData(True)`.

```csharp
private void UpdateMcFromControls()
{
    m_mc["DATE_TRADE"] = dateTrade.DateValue;
    m_mc["NUM_TRADE"] = txtTradeNum.Text;

    int tradeTypeKey;
    UbsPmTradeComboUtil.TryGetSelectedKey(cmbTradeType, out tradeTypeKey);
    m_mc["TYPE_TRADE"] = tradeTypeKey;

    m_mc["IS_COMPOSIT"] = chkComposit.Checked ? 1 : 0;

    int commKey;
    if (UbsPmTradeComboUtil.TryGetSelectedKey(cmbComission, out commKey))
        m_mc["Идентификатор комиссии"] = commKey;
    else
        m_mc["Идентификатор комиссии"] = 0;

    int curObligKey;
    UbsPmTradeComboUtil.TryGetSelectedKey(cmbObligationCurrency, out curObligKey);
    m_mc["Идентификатор валюты обязательства"] = curObligKey;

    int curOplKey;
    UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPayment, out curOplKey);
    m_mc["Идентификатор валюты оплаты"] = curOplKey;

    int curPostKey;
    UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPost, out curPostKey);
    m_mc["Идентификатор драг.металла"] = curPostKey;

    m_mc["Вид поставки по сделке"] = cmbKindSupplyTrade.SelectedIndex >= 0
        ? cmbKindSupplyTrade.Text : string.Empty;
}
```

---

## 11. Channel Contract

| Call | ParamIn | ParamOut | Used In |
|------|---------|----------|---------|
| `ModifyTrade` | `ID_TRADE`, `Основные данные` (UbsParam), `Обязательства сделки` (2D array), `Обязательства сделки2` (UbsParam), `Инструкция по оплате для покупателя` (2D array), `Инструкция по оплате для продавца` (2D array), `ID_CLIENT1`, `ID_CLIENT2`, `IsNDS`, `IsExport`, `IsExternalStorage` | `ID_TRADE`, `Ошибка` (if error) | `cmdSave_Click` |
| `DefineCodCurrency` | `IdCurrency` | `CodCB` | `CheckDatesOblig` |

---

## 12. Implementation Checklist

- [ ] Add new constants to `Constants.cs` (MsgNoTradeNum, MsgNoCurrencyPayment, MsgNoCurrencyObligation, MsgNoWeightUnit, MsgNoSeller, MsgNoObligations, MsgNoBuyerInstruction, MsgNoSellerInstruction, MsgNoObjectsByObligation, MsgNoStorageInstruction, MsgSaved, MsgSpecifyTradeDate, MsgBuyerRsCurrencyMismatch, MsgSellerRsCurrencyMismatch)
- [ ] Add new fields: `m_blnAddFlChanged`, `m_blnNeedRefresh`, `m_blnNeedSelect`, `m_blnCloseForm`, `m_strCB` (CB code), `m_blnConvert` (PMConvert flag), `m_initialMc` (snapshot copy for change detection)
- [ ] Implement `CheckData()` — 14 validation checks
- [ ] Implement `CheckDatesOblig()` — date loop + RS currency code check
- [ ] Implement `IsEqualNumCodeCurr()` helper
- [ ] Implement `UpdateMcFromControls()`
- [ ] Implement `IsMainDataChanged()` + `BuildChangedFields()`
- [ ] Implement `FillArrOblig()` — serialize to `object[count, 12]`
- [ ] Implement `FillArrDataInstr(int index)` — serialize to `object[1, 8]`
- [ ] Implement `cmdSave_Click` — full orchestration
- [ ] Implement `cmdExit_Click` — close form
- [ ] Wire `cmdSave.Click` and `cmdExit.Click` events in Designer.cs
- [ ] Verify build: 0 new errors
