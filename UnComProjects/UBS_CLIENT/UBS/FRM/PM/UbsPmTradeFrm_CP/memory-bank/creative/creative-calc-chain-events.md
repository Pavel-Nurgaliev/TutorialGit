# CREATIVE: Calculation Chain, LostFocus Handlers & Remaining Combo/Picker Events

**Source:** `Pm_Trade_ud.dob`:
- `txtMassa_LostFocus` (~L2682–2692)
- `txtCostUnit_LostFocus` (~L2694–2712)
- `txtRateCurOblig_LostFocus` (~L2715–2725)
- `txtCostCurOpl_LostFocus` (~L2727–2739)
- `txtTradeDate_LostFocus` (~L2741–2761)
- `cmbCurOblig_Click` (~L1851–1860)
- `cmbCurrencyOpl_Click` (~L1861–1872)
- `cmbCurrencyPost_click` (~L1918–1979)
- `cmbUnit_Click` (~L2107–2114)
- `ucpParam_TextChange` (~L2767–2771)
- `GetMassaPrecision` (~L5272–5281)
- `GetSumOblig`, `GetSumOpl`, `GetMassaGramm`, `GetRateCurOblig` (~L5247–5271)
- `cmdDelObject_Click` (~L4634–4678)
- `FillDataObject` (~L5007–5099)
- `FillKS` (~L4734–4794)
- `CheckKey` (~L4680–4707)
- `CheckINN` (~L4709–4732)
- `cmdListInstr_Click` (~L2380–2416)
- `StartDialog` (~L4329–4368)
- `cmdStorage_Click` (~L4247–4288)
- `cmdAccount_Click` (~L4290–4327)
- `cmdAccounts_Click` (~L2117–2148)
- `cmdAddObject_Click` (~L2150–2189)
- `ExistObject` (~L4977–5005)
- `DefineArrStrNumInPart` (~L5101–5119)

**Scope:** All remaining event handlers, calculation methods, and sub-form integration.

---

## 1. Calculation Chain — Dependency Graph

```
                    ┌──────────┐
        ┌──────────►│GetSumOblig│──────────┐
        │           └──────────┘           │
        │                                  ▼
   txtCostUnit ───────────────────── GetSumOpl ──► ucdSumPayment
        │                                  ▲
        │           ┌───────────┐          │
   txtMassa ───────►│GetMassaGramm│        │
        │           └───────────┘          │
        │                                  │
        └───► (if chkSumInCurValue)        │
                GetRateCurOblig            │
                     │                     │
                     ▼                     │
              ucdRateCurOblig ─────────────┘
                     │
                     ▼
              txtCostCurOpl ───► ucdCostCurOpl
```

### Methods (already designed in creative-call-oblig-lifecycle.md §11, reproduced for completeness)

```csharp
private void GetSumOblig()
{
    ucdSumObligation.Text = Math.Round(
        ucdCostUnit.Value * ucdMass.Value, 2).ToString();
}

private void GetSumOpl()
{
    if (chkSumInCurValue.Checked)
        ucdSumPayment.Text = Math.Round(
            ucdCostCurOpl.Value * ucdMass.Value, 2).ToString();
    else
        ucdSumPayment.Text = Math.Round(
            ucdSumObligation.Value * (double)ucdRateCurOblig.Value, 2).ToString();
}

private void GetMassaGramm()
{
    bool isOunce = (cmbUnit.Text == "унция");
    double factor = isOunce ? 31.1035 : 1.0;
    int decimals = (m_strCB == "A99") ? 0 : 1;
    ucdMassGramm.Text = Math.Round(
        (double)ucdMass.Value * factor, decimals).ToString();
}

private void GetRateCurOblig()
{
    if (ucdCostUnit.Value > 0)
        ucdRateCurOblig.Text = Math.Round(
            (double)ucdCostCurOpl.Value / (double)ucdCostUnit.Value, 10).ToString();
    else
        ucdRateCurOblig.Text = "0";
}

private void GetMassaPrecision()
{
    if (cmbUnit.Text == "грамм")
        ucdMass.Precision = (m_strCB == "A99") ? 0 : 1;
    else if (cmbUnit.Text == "унция")
        ucdMass.Precision = 3;
    else
        ucdMass.Precision = 2;
}
```

**Note:** `ucdRateCurOblig.Value` returns a `decimal` in UbsCtrlDecimal, but the VB6 code uses `VariantValue` (which treats it as a floating-point variant, not currency). The key difference: rate can have up to 10 decimal places, exceeding `decimal`'s default display. Use `double` for rate computation where VB6 used `VariantValue`. The `.Value` property of UbsCtrlDecimal should be cast appropriately.

---

## 2. LostFocus Event Handlers

### 2.1 `ucdMass_Leave` (txtMassa_LostFocus)

```csharp
private void ucdMass_Leave(object sender, EventArgs e)
{
    try
    {
        GetMassaGramm();
        GetSumOblig();
        GetSumOpl();
    }
    catch (Exception ex) { this.Ubs_ShowError(ex); }
}
```

### 2.2 `ucdCostUnit_Leave` (txtCostUnit_LostFocus)

```csharp
private void ucdCostUnit_Leave(object sender, EventArgs e)
{
    try
    {
        GetSumOblig();
        if (chkSumInCurValue.Checked)
        {
            GetRateCurOblig();
        }
        else
        {
            ucdCostCurOpl.Text = Math.Round(
                (double)ucdCostUnit.Value * (double)ucdRateCurOblig.Value, 4).ToString();
        }
        GetSumOpl();
    }
    catch (Exception ex) { this.Ubs_ShowError(ex); }
}
```

### 2.3 `ucdRateCurOblig_Leave` (txtRateCurOblig_LostFocus)

```csharp
private void ucdRateCurOblig_Leave(object sender, EventArgs e)
{
    try
    {
        ucdCostCurOpl.Text = Math.Round(
            (double)ucdCostUnit.Value * (double)ucdRateCurOblig.Value, 4).ToString();
        GetSumOpl();
    }
    catch (Exception ex) { this.Ubs_ShowError(ex); }
}
```

### 2.4 `ucdCostCurOpl_Leave` (txtCostCurOpl_LostFocus)

```csharp
private void ucdCostCurOpl_Leave(object sender, EventArgs e)
{
    try
    {
        GetRateCurOblig();
        GetSumOpl();
        chkSumInCurValue.Checked = false;
        chkRate.Checked = true;
    }
    catch (Exception ex) { this.Ubs_ShowError(ex); }
}
```

**Note:** Setting `chkRate.Checked = true` and `chkSumInCurValue.Checked = false` here differs from clicking the checkboxes. The VB6 code sets `.Value` directly (which fires Click events). In .NET, setting `.Checked` fires `CheckedChanged`. We already have handlers for those — must verify they don't cause circular triggering. **Decision:** Add a `m_suppressCheckEvents` flag if needed, or accept that the existing handlers are safe (they only toggle enable state, not check state).

### 2.5 `dateTrade_Leave` (txtTradeDate_LostFocus)

```csharp
private bool m_blnSetFocus;

private void dateTrade_Leave(object sender, EventArgs e)
{
    try
    {
        if (IsTradeDateMissingOrInvalid())
        {
            MessageBox.Show("Введите корректную дату.", MsgTitleValidationProps,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            dateTrade.Focus();
            return;
        }

        if (CheckDatesOblig())
        {
            m_mc["DATE_TRADE"] = dateTrade.DateValue;

            int curObligKey, curOplKey, curPostKey;
            bool hasCurOblig = UbsPmTradeComboUtil.TryGetSelectedKey(cmbObligationCurrency, out curObligKey);
            bool hasCurOpl = UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPayment, out curOplKey);
            bool hasCurPost = UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPost, out curPostKey);

            if (hasCurOblig && hasCurOpl && hasCurPost)
            {
                GetRate_CB();
                GetRateForPM();
            }
            m_needSendOblig = true;
        }

        if (m_blnSetFocus)
        {
            dateTrade.Focus();
            m_blnSetFocus = false;
        }
    }
    catch (Exception ex) { this.Ubs_ShowError(ex); }
}
```

**Note:** `m_blnSetFocus` is set to `true` in `InitDoc` when the form opens in ADD/EDIT mode, to keep focus on the trade date field initially.

### New constant

```
MsgInvalidDate = "Введите корректную дату."
```

---

## 3. Combo Events

### 3.1 `cmbObligationCurrency_SelectedIndexChanged` (cmbCurOblig_Click)

```csharp
private void cmbObligationCurrency_SelectedIndexChanged(object sender, EventArgs e)
{
    try
    {
        int curObligKey, curOplKey;
        if (UbsPmTradeComboUtil.TryGetSelectedKey(cmbObligationCurrency, out curObligKey)
            && UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPayment, out curOplKey))
        {
            GetRate_CB();
            GetRateForPM();
        }
    }
    catch (Exception ex) { this.Ubs_ShowError(ex); }
}
```

### 3.2 `cmbCurrencyPayment_SelectedIndexChanged` (cmbCurrencyOpl_Click)

```csharp
private void cmbCurrencyPayment_SelectedIndexChanged(object sender, EventArgs e)
{
    try
    {
        int curObligKey, curOplKey, curPostKey;
        if (UbsPmTradeComboUtil.TryGetSelectedKey(cmbObligationCurrency, out curObligKey)
            && UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPayment, out curOplKey)
            && UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPost, out curPostKey))
        {
            GetRate_CB();
            GetRateForPM();
        }
    }
    catch (Exception ex) { this.Ubs_ShowError(ex); }
}
```

### 3.3 `cmbCurrencyPost_SelectedIndexChanged` (cmbCurrencyPost_click)

Complex handler with multiple responsibilities:

1. **Early exit** if selection unchanged (track `m_intSaveCurrencyPost`).
2. **Get CB code** via `DefineCodCurrency` → set `m_strCB`.
3. **Set precision** for `ucdMassGramm` based on `m_strCB`.
4. **Call `GetMassaPrecision()`** if unit is selected.
5. **Get PMConvert flag** via `PMConvert` channel → set `m_blnConvert`.
6. **Clear objects** if metal changed and objects exist: `DefineArrStrNumInPart` → remove all "Object" keys from `m_paramOblig`.
7. **Recalculate rate** via `GetRateForPM()`.
8. **Save current selection** to `m_intSaveCurrencyPost`.

```csharp
private int m_intSaveCurrencyPost;
private string m_strCB = string.Empty;
private bool m_blnConvert;
private bool m_blnCurrencyPostClick;  // armed after first selection

private void cmbCurrencyPost_SelectedIndexChanged(object sender, EventArgs e)
{
    try
    {
        int curPostKey;
        if (!UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPost, out curPostKey))
            return;
        if (curPostKey == m_intSaveCurrencyPost)
            return;

        // 1. Get CB code
        base.IUbsChannel.ParamIn("IdCurrency", curPostKey);
        base.IUbsChannel.Run("DefineCodCurrency");
        var codOut = new UbsParam(base.IUbsChannel.ParamsOut);
        m_strCB = codOut.Contains("CodCB") ? Convert.ToString(codOut.Value("CodCB")) : string.Empty;

        // 2. Set precision
        ucdMassGramm.Precision = (m_strCB == "A99") ? 0 : 1;
        if (cmbUnit.SelectedIndex >= 0)
            GetMassaPrecision();

        // 3. Get PMConvert flag
        base.IUbsChannel.ParamIn("CodCB", m_strCB);
        base.IUbsChannel.Run("PMConvert");
        var convOut = new UbsParam(base.IUbsChannel.ParamsOut);
        m_blnConvert = convOut.Contains("Флаг пересчета")
            && Convert.ToBoolean(convOut.Value("Флаг пересчета"));

        // 4. Clear objects if metal changed
        if (m_blnCurrencyPostClick && ExistObject())
        {
            string[] numInParts = DefineArrStrNumInPart();
            if (numInParts != null)
            {
                for (int i = 0; i < numInParts.Length; i++)
                    m_paramOblig.Remove(PrefixObligObject + numInParts[i]);
            }
            m_needSendOblig = true;
        }

        // 5. Recalculate rate
        int curObligKey, curOplKey;
        if (UbsPmTradeComboUtil.TryGetSelectedKey(cmbObligationCurrency, out curObligKey)
            && UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPost, out curOplKey))
        {
            GetRateForPM();
        }

        m_intSaveCurrencyPost = curPostKey;
    }
    catch (Exception ex) { this.Ubs_ShowError(ex); }
}
```

### 3.4 `cmbUnit_SelectedIndexChanged` (cmbUnit_Click)

```csharp
private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
{
    try
    {
        GetMassaPrecision();
        ucdMass.Text = "0";
        ucdMassGramm.Text = "0";

        int curObligKey, curOplKey;
        if (UbsPmTradeComboUtil.TryGetSelectedKey(cmbObligationCurrency, out curObligKey)
            && UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPayment, out curOplKey))
        {
            GetRateForPM();
        }
    }
    catch (Exception ex) { this.Ubs_ShowError(ex); }
}
```

### 3.5 `ubsCtrlField_TextChange` (ucpParam_TextChange)

```csharp
private void ubsCtrlField_TextChange(object sender, EventArgs e)
{
    try
    {
        m_blnAddFlChanged = true;
    }
    catch (Exception ex) { this.Ubs_ShowError(ex); }
}
```

---

## 4. Sub-Form Pickers

### 4.1 Child Window Architecture in .NET

VB6 uses `IParent.GetWindow` / `IParent.SetFocusToWindow` for child windows. In .NET UBS framework, the equivalent is `Ubs_ActionRun` (for filters/grids) which returns results via `Ubs_RetFromGrid` callback.

**Pattern** (already used for contract pickers):
```csharp
// Open picker
base.Ubs_ActionRun("filterPath", paramArray);

// Results come back via:
// ListKey → CommandLine → or Ubs_RetFromGrid with selected values
```

### 4.2 `cmdListInstr_Click` → Instruction Picker

VB6 flow:
1. Clear payment fields.
2. Validate contract is selected.
3. `TradeFillInstr` channel → get available instructions.
4. `StartDialog(g)` → show modal selection dialog.
5. Fill controls from selection.
6. Check if BIK is our bank → enable/disable RS/Account.

**.NET approach:** Since `StartDialog` uses a modal `frmInstr`, we need a .NET equivalent. **Options:**
- (A) Replicate `frmInstr` as a .NET modal form.
- (B) Use `Ubs_ActionRun` to open an instruction list filter.

**Decision:** The VB6 `frmInstr` is a simple list dialog (`StartForm` → `Show vbModal`). Replicate as a simple .NET `Form` with a `ListView` + OK/Cancel. This is self-contained.

**However**, if the UBS framework provides a standard instruction picker, use that instead. Need to verify with the framework.

**Fallback approach:**
```csharp
private void linkListInstr0_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
{
    try
    {
        ClearPayment(0);
        if (string.IsNullOrEmpty(txtContractCode2.Text.Trim())
            || txtContractCode2.Text.Trim() == TextContractCodeBank)
        {
            MessageBox.Show(MsgFillBuyerData, MsgTitleInputError,
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            m_suppressMainTabSelecting = true;
            tabControl.SelectedTab = tabPage1;
            m_suppressMainTabSelecting = false;
            linkContract2.Focus();
            return;
        }

        base.IUbsChannel.ParamIn("ID_CONTRACT", m_mc["Покупатель"]);
        base.IUbsChannel.Run("TradeFillInstr");
        var instrOut = new UbsParam(base.IUbsChannel.ParamsOut);
        // Show instruction dialog or apply directly
        ShowInstructionDialog(0, instrOut);
    }
    catch (Exception ex) { this.Ubs_ShowError(ex); }
}
```

### 4.3 `cmdStorage_Click` → Storage Picker

Uses `Ubs_ActionRun` pattern with filter path:
- External storage: `"UBS_FLT\\PM\\EXTERNAL_STORAGES_LIST.flt"`
- Internal storage: `"UBS_FLT\\PM\\STORAGE.flt"`

### 4.4 `cmdAccount_Click` → Account Picker

Uses `Ubs_ActionRun` with `"UBS_FLT\\OD\\account0.flt"`, filtering by client ID and currency.

### 4.5 `cmdAccounts_Click` → Obligation Accounts Viewer

Uses `Ubs_ActionRun` with `"UBS_FLT\\PM\\PM_ACCOUNTS_BY_OBLIGATION.flt"`, filtering by obligation ID from selected ListView item's Tag.

### 4.6 `cmdAddObject_Click` → Object Picker

Uses `Ubs_ActionRun` with:
- Sale: `"UBS_FLT\\PM\\PM_FOR_OPERATION_SALE.flt"` (if cmbContractType1 index == 0)
- Purchase: `"UBS_FLT\\PM\\PM_FOR_OPERATION.flt"`
- Filter by metal currency: `"Наименование драгоценного металла"`.

### 4.7 `cmdDelObject_Click` → Remove Object from List

```csharp
private void cmdDelObject_Click(object sender, EventArgs e)
{
    try
    {
        if (lvwObject.Items.Count == 0 || lvwObject.SelectedItems.Count == 0)
            return;

        ListViewItem selected = lvwObject.SelectedItems[0];
        decimal ligMassa = Convert.ToDecimal(selected.SubItems[2].Text);
        decimal proba = Convert.ToDecimal(selected.SubItems[3].Text);

        lvwObject.Items.Remove(selected);

        if (lvwObject.Items.Count == 0)
        {
            ucdMass.Text = "0";
        }
        else
        {
            decimal adjustedMass = ligMassa;
            if (m_blnConvert)
            {
                if (cmbUnit.Text == "грамм")
                    adjustedMass = (m_strCB == "A99")
                        ? Math.Round(ligMassa * proba / 100m, 0)
                        : Math.Round(ligMassa * proba / 100m, 1);
                else
                    adjustedMass = Math.Round(ligMassa * proba / 100m, 2);
            }
            ucdMass.Text = ((decimal)ucdMass.Value - adjustedMass).ToString();
        }

        GetMassaGramm();
        GetSumOblig();
        GetSumOpl();

        UpdateObligInfoLabel();
    }
    catch (Exception ex) { this.Ubs_ShowError(ex); }
}
```

### 4.8 `FillDataObject(long idObject)` — Add Object to ListView

Called from `FillDataOblig` (edit mode) and from the object picker callback.

1. **Duplicate check** (same list): if `idObject` already in `lvwObject` → error.
2. **Cross-obligation check** (add mode only): if `idObject` exists in another obligation of same direction → error.
3. **Get object data**: `GetObjectPM` channel → `"Данные объекта"` 2D array.
4. **Add to list**: create ListViewItem from array data.
5. **Adjust mass**: apply conversion (if `m_blnConvert`): `ligMassa * proba / 100`, with precision based on unit and `m_strCB`.
6. **Accumulate mass**: `ucdMass += adjustedMass`.
7. **Recalculate**: `GetMassaGramm()`, `GetSumOblig()`, `GetSumOpl()`.
8. **Update info label**: `UpdateObligInfoLabel()`.

### 4.9 `UpdateObligInfoLabel()`

```csharp
private void UpdateObligInfoLabel()
{
    lblObligationInfo2.Text = "Цена: " + ucdCostUnit.Text
        + "   Масса: " + ucdMass.Text
        + "   Сумма: " + ucdSumPayment.Text;
}
```

---

## 5. Helper Methods

### 5.1 `ExistObject()` — Check if Any Obligation Has Objects

```csharp
private bool ExistObject()
{
    // Check m_paramOblig for any key starting with "Object"
    foreach (string key in m_paramOblig.Keys)
    {
        if (key.StartsWith(PrefixObligObject))
            return true;
    }
    return false;
}
```

**Note:** `m_paramOblig.Keys` — need to verify `UbsParam` exposes enumerable keys. If not, use the `Parameters` property which returns a 2D array (VB6-style), or maintain a parallel list.

### 5.2 `DefineArrStrNumInPart()` — Get All Part Numbers

```csharp
private string[] DefineArrStrNumInPart()
{
    int count = lvwObligation.Items.Count;
    if (count == 0) return null;

    string[] result = new string[count];
    for (int i = 0; i < count; i++)
        result[i] = lvwObligation.Items[i].SubItems[1].Text;
    return result;
}
```

### 5.3 `FillKS(int g)` — Fill Bank Details from BIK

```csharp
private bool FillKS(int g)
{
    // Unlock client/note fields
    if (g == 0) { txtClient0.ReadOnly = false; txtNote0.ReadOnly = false; }
    else        { txtClient1.ReadOnly = false; txtNote1.ReadOnly = false; }

    string bik = (g == 0) ? txtBIK0.Text : txtBIK1.Text;

    base.IUbsChannel.ParamIn("БИК", bik);
    base.IUbsChannel.Run("FillRekv");
    var rekvOut = new UbsParam(base.IUbsChannel.ParamsOut);

    if (rekvOut.Contains("Отчет"))
    {
        MessageBox.Show(Convert.ToString(rekvOut.Value("Отчет")),
            "Проверка БИК", MessageBoxButtons.OK, MessageBoxIcon.Error);
        // Clear name, KS; focus BIK
        if (g == 0) { txtName0.Text = ""; ucaKS0.Text = ""; txtBIK0.Focus(); }
        else        { txtName1.Text = ""; ucaKS1.Text = ""; txtBIK1.Focus(); }
        return false;
    }

    // Fill bank name
    string bankName = Convert.ToString(rekvOut.Value("Банк"));
    if (g == 0) txtName0.Text = bankName; else txtName1.Text = bankName;

    // Fill KS
    if (rekvOut.Contains("КС"))
    {
        string ks = Convert.ToString(rekvOut.Value("КС"));
        if (g == 0) ucaKS0.Text = ks; else ucaKS1.Text = ks;
    }

    // Our BIK check
    bool isOurBIK = rekvOut.Contains("Наш БИК")
        && Convert.ToBoolean(rekvOut.Value("Наш БИК"));

    if (isOurBIK)
    {
        if (g == 0) { ucaRS0.ReadOnly = true; linkAccountPayment0.Enabled = true; }
        else        { ucaRS1.ReadOnly = true; linkAccountPayment1.Enabled = true; }
    }
    else
    {
        if (g == 0) { ucaRS0.ReadOnly = false; linkAccountPayment0.Enabled = false; }
        else        { ucaRS1.ReadOnly = false; linkAccountPayment1.Enabled = false; }
    }

    // Check key
    if (!CheckKey(g))
    {
        if (g == 0)
        {
            if (!ucaRS0.ReadOnly) ucaRS0.Focus();
            else linkAccountPayment0.Focus();
        }
        else
        {
            if (!ucaRS1.ReadOnly) ucaRS1.Focus();
            else linkAccountPayment1.Focus();
        }
    }

    return true;
}
```

### 5.4 `CheckKey(int g)` — Validate BIK+RS+KS Key

```csharp
private bool CheckKey(int g)
{
    string bik = (g == 0) ? txtBIK0.Text : txtBIK1.Text;
    string rs  = (g == 0) ? ucaRS0.Text  : ucaRS1.Text;
    string ks  = (g == 0) ? ucaKS0.Text  : ucaKS1.Text;

    if (bik.Length > 0 && rs.Length > 0)
    {
        base.IUbsChannel.ParamIn("БИК", bik);
        base.IUbsChannel.ParamIn("РС", rs);
        base.IUbsChannel.ParamIn("КС", ks);
        base.IUbsChannel.Run("TradeCheckKey");
        var keyOut = new UbsParam(base.IUbsChannel.ParamsOut);

        string report = keyOut.Contains("Отчет")
            ? Convert.ToString(keyOut.Value("Отчет")) : string.Empty;
        if (report.Length > 0)
        {
            MessageBox.Show(report, "Проверка ключа",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }
    return true;
}
```

### 5.5 `CheckINN(int g)` — Validate INN

```csharp
private bool CheckINN(int g)
{
    string inn = (g == 0) ? txtINN0.Text : txtINN1.Text;

    base.IUbsChannel.ParamIn("ИНН", inn);
    base.IUbsChannel.Run("TradeCheckINN");
    var innOut = new UbsParam(base.IUbsChannel.ParamsOut);

    string report = innOut.Contains("Отчет")
        ? Convert.ToString(innOut.Value("Отчет")) : string.Empty;
    if (report.Length > 0)
    {
        MessageBox.Show(report, "Проверка ИНН",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
    }
    return true;
}
```

---

## 6. Channel Contract (New Calls)

| Call | ParamIn | ParamOut | Used In |
|------|---------|----------|---------|
| `DefineCodCurrency` | `IdCurrency` | `CodCB` | `cmbCurrencyPost_SelectedIndexChanged`, `CheckDatesOblig` |
| `PMConvert` | `CodCB` | `Флаг пересчета` (bool) | `cmbCurrencyPost_SelectedIndexChanged` |
| `GetObjectPM` | `ID_OBJECT` | `Данные объекта` (2D array) | `FillDataObject` |
| `TradeFillInstr` | `ID_CONTRACT` | `ArrInstr` (2D array) | `cmdListInstr_Click` |
| `FillRekv` | `БИК` | `Банк`, `КС`, `Наш БИК`, `Отчет` (error) | `FillKS` |
| `TradeCheckKey` | `БИК`, `РС`, `КС` | `Отчет` (error) | `CheckKey` |
| `TradeCheckINN` | `ИНН` | `Отчет` (error) | `CheckINN` |

---

## 7. New Fields Summary

| Field | Type | Purpose |
|-------|------|---------|
| `m_strCB` | `string` | CB code of selected metal (e.g. "A99" for silver) |
| `m_blnConvert` | `bool` | PMConvert flag — whether metal needs purity conversion |
| `m_intSaveCurrencyPost` | `int` | Previous selection key for early-exit check |
| `m_blnCurrencyPostClick` | `bool` | Armed flag (set after first load to avoid clearing objects during init) |
| `m_blnSetFocus` | `bool` | Keep focus on dateTrade after init |
| `m_blnAddFlChanged` | `bool` | Dirty flag for additional fields |

**Note:** `m_strCB` and `m_blnConvert` are also needed by `FillDataObject`, `cmdDelObject_Click`, and `GetMassaGramm`. They are global form fields.

---

## 8. New Constants

```
MsgInvalidDate = "Введите корректную дату."
MsgFillBuyerData = "Заполните данные о покупателе"
MsgFillSellerData = "Заполните данные о продавце"
MsgDuplicateObject = "Объект Учета с Идентификатором {0} в списке уже существует. Выберите другой объект пожалуйста!"
MsgDuplicateObjectInObligation = "Объект Учета с Идентификатором {0} уже существует в обязательстве {1}. Выберите другой объект пожалуйста!"
```

---

## 9. Event Wiring (Designer.cs)

| Control | Event | Handler |
|---------|-------|---------|
| `ucdMass` | `Leave` | `ucdMass_Leave` |
| `ucdCostUnit` | `Leave` | `ucdCostUnit_Leave` |
| `ucdRateCurOblig` | `Leave` | `ucdRateCurOblig_Leave` |
| `ucdCostCurOpl` | `Leave` | `ucdCostCurOpl_Leave` |
| `dateTrade` | `Leave` | `dateTrade_Leave` |
| `cmbObligationCurrency` | `SelectedIndexChanged` | `cmbObligationCurrency_SelectedIndexChanged` |
| `cmbCurrencyPayment` | `SelectedIndexChanged` | `cmbCurrencyPayment_SelectedIndexChanged` |
| `cmbCurrencyPost` | `SelectedIndexChanged` | `cmbCurrencyPost_SelectedIndexChanged` |
| `cmbUnit` | `SelectedIndexChanged` | `cmbUnit_SelectedIndexChanged` |
| `ubsCtrlField` | `TextChange` | `ubsCtrlField_TextChange` |
| `linkListInstr0` | `LinkClicked` | `linkListInstr0_LinkClicked` |
| `linkListInstr1` | `LinkClicked` | `linkListInstr1_LinkClicked` |
| `linkStorage` | `LinkClicked` | `linkStorage_LinkClicked` |
| `linkAccountPayment0` | `LinkClicked` | `linkAccountPayment0_LinkClicked` |
| `linkAccountPayment1` | `LinkClicked` | `linkAccountPayment1_LinkClicked` |
| `cmdAddObject` | `Click` | `cmdAddObject_Click` |
| `cmdDelObject` | `Click` | `cmdDelObject_Click` |
| `cmdAccounts` | `Click` | `cmdAccounts_Click` |

---

## 10. Implementation Order

1. **Fields & constants** — add all new fields and constants
2. **Calculation methods** — `GetSumOblig`, `GetSumOpl`, `GetMassaGramm`, `GetRateCurOblig`, `GetMassaPrecision`
3. **Helper methods** — `ExistObject`, `DefineArrStrNumInPart`, `UpdateObligInfoLabel`, `CheckKey`, `CheckINN`, `FillKS`
4. **LostFocus handlers** — 5 Leave events
5. **Combo events** — 5 SelectedIndexChanged events + TextChange
6. **Object operations** — `FillDataObject`, `cmdDelObject_Click`, `cmdAddObject_Click`
7. **Instruction picker** — `cmdListInstr_Click`, `StartDialog` equivalent
8. **Storage/Account pickers** — `cmdStorage_Click`, `cmdAccount_Click`, `cmdAccounts_Click`
9. **Wire events** in Designer.cs
10. **Verify build**

---

## 11. Implementation Checklist

- [ ] Add new fields (`m_strCB`, `m_blnConvert`, `m_intSaveCurrencyPost`, `m_blnCurrencyPostClick`, `m_blnSetFocus`, `m_blnAddFlChanged`)
- [ ] Add new constants to `Constants.cs`
- [ ] Implement calculation methods (`GetSumOblig`, `GetSumOpl`, `GetMassaGramm`, `GetRateCurOblig`, `GetMassaPrecision`)
- [ ] Implement `UpdateObligInfoLabel()`
- [ ] Implement `ExistObject()`, `DefineArrStrNumInPart()`
- [ ] Implement `CheckKey(int g)`, `CheckINN(int g)`, `FillKS(int g)`
- [ ] Implement `ucdMass_Leave`
- [ ] Implement `ucdCostUnit_Leave`
- [ ] Implement `ucdRateCurOblig_Leave`
- [ ] Implement `ucdCostCurOpl_Leave`
- [ ] Implement `dateTrade_Leave`
- [ ] Implement `cmbObligationCurrency_SelectedIndexChanged`
- [ ] Implement `cmbCurrencyPayment_SelectedIndexChanged`
- [ ] Implement `cmbCurrencyPost_SelectedIndexChanged` (complex: DefineCodCurrency + PMConvert + object clearing)
- [ ] Implement `cmbUnit_SelectedIndexChanged`
- [ ] Implement `ubsCtrlField_TextChange`
- [ ] Implement `FillDataObject(long idObject)` with duplicate checks + mass accumulation
- [ ] Implement `cmdDelObject_Click` with mass subtraction
- [ ] Implement `cmdAddObject_Click` (object picker via Ubs_ActionRun)
- [ ] Implement instruction picker (`linkListInstr0/1_LinkClicked`)
- [ ] Implement storage picker (`linkStorage_LinkClicked`)
- [ ] Implement account picker (`linkAccountPayment0/1_LinkClicked`)
- [ ] Implement obligation accounts viewer (`cmdAccounts_Click`)
- [ ] Wire all events in Designer.cs
- [ ] Verify build: 0 new errors
