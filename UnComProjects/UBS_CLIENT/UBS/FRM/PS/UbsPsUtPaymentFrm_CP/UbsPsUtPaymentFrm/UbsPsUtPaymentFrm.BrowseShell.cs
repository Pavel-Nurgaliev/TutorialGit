using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using UbsService;
using static System.Net.Mime.MediaTypeNames;

namespace UbsBusiness
{
    public partial class UbsPsUtPaymentFrm
    {
        private const int MaxRecipientNameLength = 160;
        private const int MaxPurposeLength = 210;
        private const string ListClientGuest = "UBS_OC_CLIENTS";
        private const string ListClientCommon = "UBS_COMMON_LIST_CLIENT";
        private const string ListContract = "UBS_PS_UT_LIST_CONTRACT";
        private const string ListPayment = "UBS_PS_UT_LIST_PAYMENT";
        private const string ListAttributeRecip = "UBS_PS_LIST_ATTRIBUTE_RECIPIENT";
        private const string DefaultPaymDicFilter = "UBS_PS_LIST_PAYM_DIC";

        private enum BrowseListKind
        {
            None,
            ClientNonGuest,
            ClientGuest,
            Contract,
            PaymentDictionary,
            RecipientAttribute,
            ThirdPerson,
            PaymentList
        }

        private BrowseListKind m_browseListKind;

        #region Client selection

        private void BtnClient_ClickImpl()
        {
            try
            {
                string listSid = m_isGuest ? ListClientGuest : ListClientCommon;
                m_browseListKind = m_isGuest ? BrowseListKind.ClientGuest : BrowseListKind.ClientNonGuest;

                try
                {
                    object[] ids = this.Ubs_ActionRun(listSid, this, true) as object[];
                    if (ids == null || ids.Length == 0)
                        return;

                    int selectedId = Convert.ToInt32(ids[0]);
                    if (selectedId <= 0)
                        return;

                    m_idClient = selectedId;

                    if (m_idClient > 0)
                    {
                        linkFindFilter.Visible = true;
                    }

                    FillPayer();
                    CheckPayer(false);

                    m_docNumber = string.Empty;
                    m_docSeries = string.Empty;

                    if (string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal)
                        && txtContractCode.Enabled)
                    {
                        txtContractCode.Focus();
                    }
                }
                finally
                {
                    m_browseListKind = BrowseListKind.None;
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region Contract selection

        private void BtnContract_ClickImpl()
        {
            try
            {
                m_browseListKind = BrowseListKind.Contract;
                try
                {
                    object[] ids = this.Ubs_ActionRun(ListContract, this, true) as object[];
                    if (ids == null || ids.Length == 0)
                        return;

                    int selectedId = Convert.ToInt32(ids[0]);
                    if (selectedId > 0)
                    {
                        m_idContract = selectedId;
                        FindContractbyId();
                    }
                }
                finally
                {
                    m_browseListKind = BrowseListKind.None;
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void ucaRecipientAccount_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtContractCode.Text.Trim().Length > 0)
                    return;

                string bic = txtRecipientBic.Text.Trim();
                string acc = ucaRecipientAccount.Text.Trim();

                if (bic.Length == 0 && (acc.Length == 0 || acc == "00000000000000000000"))
                    return;

                this.IUbsChannel.ParamIn("BIC", bic);
                this.IUbsChannel.ParamIn("ACC", ucaRecipientAccount.Text);
                this.IUbsChannel.ParamIn("INN", txtRecipientInn.Text);

                this.IUbsChannel.Run("FindContrByBicAndAccount");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                int recCount = paramOut.GetParamOutInt("RecCount");

                if (recCount == 1)
                {
                    object idArr = paramOut.Value("IdArray");
                    if (idArr is object[,])
                    {
                        m_idContract = Convert.ToInt32(((object[,])idArr)[0, 0]);
                    }

                    if (m_idContract > 0)
                    {
                        FindContractbyId();
                    }

                    if (m_isForward)
                    {
                        if (cmbPurpose.Enabled && cmbPurpose.Visible)
                            cmbPurpose.Focus();
                        else if (txtPaymentCode.Enabled && txtPaymentCode.Visible)
                            txtPaymentCode.Focus();
                        else if (udcPaymentAmount.Enabled && udcPaymentAmount.Visible)
                            udcPaymentAmount.Focus();
                    }
                }
                else if (recCount > 1)
                {
                    BtnContract_ClickImpl();
                }
                else
                {
                    m_idContract = 0;
                    txtContractCode.Text = string.Empty;
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region Pattern / user form

        private void BtnPattern_ClickImpl()
        {
            try
            {
                CreateUserFormArray();
                DefineRunUserForm(true);
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        /// <summary>
        /// Builds <see cref="m_varUFArray"/> from the form state and writes it
        /// so that <c>DefineRunUserForm</c> can pass it to <c>UTPUFT.vbs</c>
        /// as the <c>InitArray</c> parameter. Port of VB6
        /// <c>CreateUserFormArray</c> (UtPayment.dob lines 1482–1609).
        ///
        /// The script consumes the legacy VB6 control names, so column-0 keys
        /// preserve the original VB6 identifiers even where the .NET control
        /// was renamed (for example, <c>txtKSPayment</c> reads from the
        /// renamed <c>txtCashSymbolPayment</c> TextBox).
        ///
        /// Three modes:
        ///   <list type="bullet">
        ///     <item>0 — control value (text, decimal, or synthesized period date)</item>
        ///     <item>1 — variable (<c>IdClient</c> when non-zero)</item>
        ///     <item>3 — additional field from <c>ucfAddProperties.Collection</c></item>
        ///   </list>
        /// </summary>
        private void CreateUserFormArray()
        {
            try
            {
                const int modeControl = 0;
                const int modeVariable = 1;
                const int modeAddField = 3;

                List<object[]> rows = new List<object[]>();

                AppendControlString(rows, "txtFIOPay", txtPayerFullName.Text);
                AppendControlString(rows, "txtINNPay", txtPayerInn.Text);
                AppendControlString(rows, "txtAdressPay", txtPayerAddress.Text);
                AppendControlString(rows, "txtCode", txtContractCode.Text);
                AppendControlString(rows, "txtBic", txtRecipientBic.Text);
                AppendControlString(rows, "AccClient", ucaRecipientAccount.Text);
                AppendControlString(rows, "txtINN", txtRecipientInn.Text);
                AppendControlString(rows, "cmbPurpose", cmbPurpose.Text);
                AppendControlString(rows, "txtRecip", txtRecipientName.Text);
                AppendControlString(rows, "txtKSPayment", txtCashSymbolPayment.Text);
                AppendControlString(rows, "txtKSRate", txtCashSymbolCommission.Text);
                AppendControlString(rows, "txtCodePayment", txtPaymentCode.Text);
                AppendControlDecimal(rows, "curSumma", udcPaymentAmount.DecimalValue);
                AppendControlPeriodDate(rows, "txtDateBegin",
                    txtPeriodYearBeg.Text, txtPeriodMonthBeg.Text, txtPeriodDayBeg.Text);
                AppendControlPeriodDate(rows, "txtDateEnd",
                    txtPeriodYearEnd.Text, txtPeriodMonthEnd.Text, txtPeriodDayEnd.Text);
                AppendControlString(rows, "AccPay", txtPayerAccount.Text);
                AppendControlString(rows, "txtCheckSum", txtCheckSum.Text);
                AppendControlString(rows, "cmbTariff", cmbTariff.Text);
                AppendControlString(rows, "cmbPhone", cmbPhone.Text);
                AppendControlString(rows, "txtNote", txtRecipientNote.Text);
                AppendControlString(rows, "txtNameBank", txtRecipientBankName.Text);
                AppendControlString(rows, "txtComment", txtRecipientComment.Text);
                AppendControlString(rows, "AccKorr", ucaRecipientCorrAccount.Text);
                AppendControlDecimal(rows, "curSummaRateSend", udcPayerRateAmount.DecimalValue);
                AppendControlDecimal(rows, "curSummaTotal", udcAmountWithRate.DecimalValue);
                AppendControlDecimal(rows, "curPeny", udcPenaltyAmount.DecimalValue);
                AppendControlString(rows, "AccClientPay", ucaPayerAccount.Text);

                // Tag every control row with mode = 0 (must run after appends).
                for (int i = 0; i < rows.Count; i++)
                {
                    rows[i][2] = modeControl;
                }

                if (m_idClient != 0)
                {
                    rows.Add(new object[] { "IdClient", m_idClient, modeVariable });
                }

                if (ucfAddProperties.Collection.Count > 0)
                {
                    foreach (UbsControl.UbsCtrlFields.UbsAddField field in ucfAddProperties.Collection)
                    {
                        rows.Add(new object[] { field.Name, field.Value, modeAddField });
                    }
                }

                m_varUFArray = new object[rows.Count, 3];
                for (int r = 0; r < rows.Count; r++)
                {
                    m_varUFArray[r, 0] = rows[r][0];
                    m_varUFArray[r, 1] = rows[r][1];
                    m_varUFArray[r, 2] = rows[r][2];
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        /// <summary>
        /// Appends a control-text row only when the trimmed text is non-empty,
        /// matching VB6 <c>If Len(Trim(Controls(...).Text)) &gt; 0</c>.
        /// Mode column is left as null and is set by the caller in bulk.
        /// </summary>
        private static void AppendControlString(List<object[]> rows, string key, string value)
        {
            if (value == null) return;
            if (value.Trim().Length == 0) return;
            rows.Add(new object[] { key, value, null });
        }

        /// <summary>
        /// Appends a decimal/currency row only when the value is non-zero,
        /// matching VB6 <c>If Controls(...).CurrencyValue &lt;&gt; 0</c>.
        /// </summary>
        private static void AppendControlDecimal(List<object[]> rows, string key, decimal value)
        {
            if (value == 0m) return;
            rows.Add(new object[] { key, value, null });
        }

        /// <summary>
        /// Synthesizes a period date from year / month / day text fields,
        /// matching VB6 <c>DateSerial(CLng(txtYearX), CLng(txtMonthX), CLng(txtDayX))</c>.
        /// Year &lt; 100 is resolved as 2000+year, matching <c>CollectPeriodDates</c>.
        /// Skips the row when year is empty or any component fails to parse.
        /// </summary>
        private static void AppendControlPeriodDate(List<object[]> rows, string key,
            string yearText, string monthText, string dayText)
        {
            if (yearText == null || yearText.Length == 0) return;
            int year, month, day;
            if (!int.TryParse(yearText, out year)) return;
            if (!int.TryParse(monthText, out month)) return;
            if (!int.TryParse(dayText, out day)) return;
            if (year < 100) year += 2000;
            try
            {
                DateTime value = new DateTime(year, month, day);
                rows.Add(new object[] { key, value, null });
            }
            catch (ArgumentOutOfRangeException) { /* invalid date — skip row, matching VB6 Err_ */ }
        }

        #endregion

        #region Payment dictionary / CheckPaymDic

        private void BtnPaymDic_ClickImpl()
        {
            try
            {
                string filterSid = (m_SIDFilter != null && m_SIDFilter.Length > 0)
                    ? m_SIDFilter
                    : DefaultPaymDicFilter;

                m_browseListKind = BrowseListKind.PaymentDictionary;
                try
                {
                    object[] ids = this.Ubs_ActionRun(filterSid, this, true) as object[];
                    if (ids == null || ids.Length == 0)
                        return;

                    int selectedId = Convert.ToInt32(ids[0]);
                    if (selectedId > 0)
                    {
                        m_idPaymentDic = selectedId;
                        FillDataPayment("FILTER");
                    }
                }
                finally
                {
                    m_browseListKind = BrowseListKind.None;
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void CheckPaymDic()
        {
            try
            {
                if (ucaPayerAccount.Text.Trim().Length == 0)
                    return;

                this.IUbsChannel.ParamIn("IdKindPayment", m_idKindPaym);
                this.IUbsChannel.ParamIn("accLic", ucaPayerAccount.Text);

                this.IUbsChannel.Run("CheckPaymDic");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                int idOneRecord = paramOut.GetParamOutInt("OneRecord");

                if (idOneRecord != 0)
                {
                    m_idPaymentDic = idOneRecord;
                    FillDataPayment("FILTER");
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region Filtered payment list

        private void BtnFindFilter_ClickImpl()
        {
            try
            {
                m_browseListKind = BrowseListKind.PaymentList;
                try
                {
                    object[] ids = this.Ubs_ActionRun(ListPayment, this, true) as object[];
                    if (ids == null || ids.Length == 0)
                        return;

                    if (ids != null && ids.Length > 0)
                    {
                        m_idPaymentCopy = Convert.ToInt32(ids[0]);
                    }
                    else
                    {
                        MessageBox.Show("Платеж не выбран", "Копирование платежа", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }

                    if (m_idPaymentCopy > 0)
                    {
                        var curSumTemp = udcPaymentAmount.DecimalValue;

                        m_command = "ADD";
                        m_idPayment = m_idPaymentCopy;
                        m_command = "COPY";

                        InitDoc();
                        m_command = "ADD";

                        udcPaymentAmount.DecimalValue = curSumTemp;
                        m_prefCalcRate = "_2 7";
                        CalcSumCommiss_2();
                    }
                }
                finally
                {
                    m_browseListKind = BrowseListKind.None;
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region Recipient attributes

        private void BtnRecipientAttributeList_ClickImpl()
        {
            try
            {
                this.IUbsChannel.ParamIn("IdContract", m_idContract);
                this.IUbsChannel.Run("GetRecipAttributeList");

                m_browseListKind = BrowseListKind.RecipientAttribute;
                object[] ids;
                try
                {
                    ids = this.Ubs_ActionRun(ListAttributeRecip, this, true) as object[];
                }
                finally
                {
                    m_browseListKind = BrowseListKind.None;
                }

                if (ids == null || ids.Length == 0)
                    return;

                int selectedId = Convert.ToInt32(ids[0]);
                if (selectedId <= 0)
                    return;

                m_idAttributeRecip = selectedId;

                this.IUbsChannel.ParamIn("IdAttributeRecip", m_idAttributeRecip);
                this.IUbsChannel.Run("ReadRecipFromId");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                txtRecipientName.Text = paramOut.GetParamOutString("Наименование получателя в плат. документах");
                txtRecipientBic.Text = paramOut.GetParamOutString("BIC");
                ucaRecipientCorrAccount.Text = paramOut.GetParamOutString("CORRACC");
                txtRecipientBankName.Text = paramOut.GetParamOutString("Наименование банка");
                ucaRecipientAccount.Text = paramOut.GetParamOutString("ACC");
                txtRecipientInn.Text = paramOut.GetParamOutString("INN");
                cmbPurpose.Text = paramOut.GetParamOutString("PURPOSE");

                if (string.Equals(m_sidPattern, PatternNalog, StringComparison.Ordinal))
                {
                    txtTaxOkato.Text = paramOut.GetParamOutString("OKATO");
                }

                ucfAddProperties.Refresh();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void BtnSaveRecipientAttribute_ClickImpl()
        {
            try
            {
                if (m_idContract <= 0)
                {
                    MessageBox.Show(MsgRecipientContractRequired, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.IUbsChannel.ParamIn("IdContract", m_idContract);
                this.IUbsChannel.ParamIn("IdAttributeRecip", m_idAttributeRecip);
                this.IUbsChannel.ParamIn("Наименование получателя в плат. документах", txtRecipientName.Text);
                this.IUbsChannel.ParamIn("BIC", txtRecipientBic.Text);
                this.IUbsChannel.ParamIn("CORRACC", ucaRecipientCorrAccount.Text);
                this.IUbsChannel.ParamIn("Наименование банка", txtRecipientBankName.Text);
                this.IUbsChannel.ParamIn("ACC", ucaRecipientAccount.Text);
                this.IUbsChannel.ParamIn("INN", txtRecipientInn.Text);
                this.IUbsChannel.ParamIn("PURPOSE", cmbPurpose.Text);

                this.IUbsChannel.Run("SaveRecipAttribute");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                if (paramOut.GetParamOutBool("bRetVal"))
                {
                    m_idAttributeRecip = paramOut.GetParamOutInt("IdAttributeRecip");
                    MessageBox.Show(MsgRecipientAttributesSaved, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string err = paramOut.GetParamOutString("StrError");
                    if (err.Length > 0)
                    {
                        MessageBox.Show(err, CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region Bank name by BIC

        private void txtRecipientBik_Enter(object sender, EventArgs e)
        {
            m_bicOld = txtRecipientBic.Text;
        }

        #endregion

        #region Third person selection

        private void BtnSelectThirdPerson_ClickImpl()
        {
            try
            {
                string listSid = m_isGuest ? ListClientGuest : ListClientCommon;
                m_browseListKind = BrowseListKind.ThirdPerson;

                object[] ids;
                try
                {
                    ids = this.Ubs_ActionRun(listSid, this, true) as object[];
                }
                finally
                {
                    m_browseListKind = BrowseListKind.None;
                }

                if (ids == null || ids.Length == 0)
                    return;

                int thirdPersonId = Convert.ToInt32(ids[0]);
                if (thirdPersonId <= 0)
                    return;

                this.IUbsChannel.ParamIn("IDCLIENT", thirdPersonId);
                this.IUbsChannel.ParamIn("IsGuest", m_isGuest);

                this.IUbsChannel.Run("ReadClientFromIdOC");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                string readErr = paramOut.GetParamOutString("StrError");
                if (readErr.Length > 0)
                {
                    MessageBox.Show(readErr, "ReadClientFromIdOC " + CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                txtThirdPersonInn.Text = paramOut.GetParamOutString("INN");
                txtThirdPersonName.Text = paramOut.GetParamOutString("NAME");
                txtThirdPersonKpp.Text = paramOut.GetParamOutString("KPP");

                int clientKind = paramOut.GetParamOutInt("CLIENT_KIND");
                if (clientKind >= 0 && clientKind < cmbThirdPersonKind.Items.Count)
                {
                    cmbThirdPersonKind.SelectedIndex = clientKind;
                }

                if (cmbThirdPersonKind.SelectedIndex == 2
                    && !string.Equals(m_command, StrCommandView, StringComparison.Ordinal))
                {
                    txtThirdPersonKpp.Enabled = true;
                }

                ThirdPersonKindChanged();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region Payer account browse

        private void BtnPaymentAccount_ClickImpl()
        {
            BtnFindFilter_ClickImpl();
        }

        #endregion

        #region Text change tracking

        private void txtRecipientName_TextChanged(object sender, EventArgs e)
        {
            int remaining = MaxRecipientNameLength - txtRecipientName.Text.Length;
            lblCharCount160.ForeColor = (remaining < 0) ? Color.Red : Color.Black;
            lblCharCount160.Text = remaining.ToString();
        }

        private void cmbPurpose_TextChanged(object sender, EventArgs e)
        {
            int remaining = MaxPurposeLength - cmbPurpose.Text.Length;
            lblCharCount210.ForeColor = (remaining < 0) ? Color.Red : Color.Black;
            lblCharCount210.Text = remaining.ToString();
        }

        #endregion

        #region Third person checkbox

        private void ChkThirdPerson_CheckedChangedImpl()
        {
            try
            {
                if (chkThirdPerson.Checked)
                {
                    ShowTabPage(tabPageThirdPerson);
                    txtTaxStatus.Enabled = true;

                    if (cmbThirdPersonKind.SelectedIndex >= 0)
                    {
                        ThirdPersonKindChanged();
                    }
                }
                else
                {
                    HideTabPage(tabPageThirdPerson);
                    if (m_forbidTaxStatusChanges)
                    {
                        txtTaxStatus.Text = m_savedTaxStatusValue;
                    }
                    else
                    {
                        txtTaxStatus.Enabled = true;
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void ThirdPersonKindChanged()
        {
            if (cmbThirdPersonKind.SelectedIndex == 2)
            {
                txtThirdPersonKpp.Enabled = true;
            }
            else
            {
                txtThirdPersonKpp.Enabled = false;
                txtThirdPersonKpp.Text = "0";
            }

            switch (cmbThirdPersonKind.SelectedIndex)
            {
                case 0:
                    txtTaxStatus.Text = m_formReestToDocs ? "09" : "20";
                    break;
                case 1:
                    txtTaxStatus.Text = m_formReestToDocs ? "13" : "20";
                    break;
                case 2:
                    txtTaxStatus.Text = m_formReestToDocs ? "01" : "20";
                    break;
            }
        }

        private void cmbThirdPersonKind_Leave(object sender, EventArgs e)
        {
            ThirdPersonKindChanged();
        }

        #endregion

        #region Benefits checkbox

        private void chkBenefits_CheckedChanged(object sender, EventArgs e)
        {
        }

        #endregion

        #region UbsCtrlFields events

        private void ucfAddProperties_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Escape)
                {
                    e.Handled = true;
                    if (IsTabPageShown(tabPageTax))
                    {
                        tabPayment.SelectedTab = tabPageTax;
                    }
                    else if (IsTabPageShown(tabPageThirdPerson))
                    {
                        tabPayment.SelectedTab = tabPageThirdPerson;
                    }
                    else
                    {
                        tabPayment.SelectedTab = tabPageGeneral;
                        if (txtCheckSum.Visible)
                            txtCheckSum.Focus();
                        else if (ucaPayerAccount.Visible)
                            ucaPayerAccount.Focus();
                        else
                            udcPaymentAmount.Focus();
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void ucfAddProperties_ValueChange(object sender, EventArgs e)
        {
            try
            {
                this.IUbsChannel.Run("UserAddField");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                if (!paramOut.GetParamOutBool("bRetVal"))
                {
                    string err = paramOut.GetParamOutString("StrError");
                    MessageBox.Show(err, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ucfAddProperties.Refresh();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region Wire browse events

        private void WireBrowseEvents()
        {
            txtRecipientName.TextChanged += txtRecipientName_TextChanged;
            cmbPurpose.TextChanged += cmbPurpose_TextChanged;

            txtRecipientBic.Enter += txtRecipientBik_Enter;
            ucaRecipientAccount.Leave += ucaRecipientAccount_Leave;
            cmbThirdPersonKind.Leave += cmbThirdPersonKind_Leave;
            chkBenefits.CheckedChanged += chkBenefits_CheckedChanged;

            ucfAddProperties.KeyPress += ucfAddProperties_KeyPress;

            this.Ubs_ActionRunBegin += new UbsActionRunBeginEventHandler(UbsPsUtPaymentFrm_Ubs_ActionRunBegin);
        }

        private void UbsPsUtPaymentFrm_Ubs_ActionRunBegin(object sender, UbsActionRunEventArgs args)
        {
            try
            {
                if (m_browseListKind == BrowseListKind.PaymentList
                                    && string.Equals(args.Action, ListPayment, StringComparison.Ordinal))
                {
                    args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                        new KeyValuePair<string, object>("наименование", m_isGuest ? "ID Посетитель" : "ID Клиент банка"),
                        new KeyValuePair<string, object>("значение по умолчанию", m_idClient),
                        new KeyValuePair<string, object>("условие по умолчанию", "="),
                        new KeyValuePair<string, object>("скрытый", true) }));
                    args.IUbs.Run("UbsItemsRefresh", null);
                    return;
                }

                if (m_browseListKind == BrowseListKind.ClientNonGuest
                    && string.Equals(args.Action, ListClientCommon, StringComparison.Ordinal))
                {
                    args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                        new KeyValuePair<string, object>("наименование", "Тип"),
                        new KeyValuePair<string, object>("значение по умолчанию", 2),
                        new KeyValuePair<string, object>("условие по умолчанию", "="),
                        new KeyValuePair<string, object>("скрытый", true) }));
                    args.IUbs.Run("UbsItemsRefresh", null);
                    return;
                }

                if (m_browseListKind == BrowseListKind.RecipientAttribute
                    && string.Equals(args.Action, ListAttributeRecip, StringComparison.Ordinal))
                {
                    args.IUbs.Run("UbsItemsRemove", null);
                    args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                        new KeyValuePair<string, object>("наименование", "Идентификатор договора"),
                        new KeyValuePair<string, object>("значение по умолчанию", m_idContract),
                        new KeyValuePair<string, object>("условие по умолчанию", "="),
                        new KeyValuePair<string, object>("скрытый", true) }));
                    args.IUbs.Run("UbsItemsRefresh", null);
                    return;
                }

                if (m_browseListKind == BrowseListKind.PaymentDictionary)
                {
                    args.IUbs.Run("UbsItemsRemove", null);
                    args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                        new KeyValuePair<string, object>("наименование", "л/с плательщика"),
                        new KeyValuePair<string, object>("значение по умолчанию", txtPayerAccount.Text),
                        new KeyValuePair<string, object>("условие по умолчанию", "="),
                        new KeyValuePair<string, object>("скрытый", false) }));
                    if (m_idContract > 0)
                    {
                        args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                            new KeyValuePair<string, object>("наименование", "Идентификатор договора"),
                            new KeyValuePair<string, object>("значение по умолчанию", m_idContract),
                            new KeyValuePair<string, object>("условие по умолчанию", "="),
                            new KeyValuePair<string, object>("скрытый", false) }));
                    }
                    args.IUbs.Run("UbsItemsRefresh", null);
                    return;
                }

                if (m_browseListKind == BrowseListKind.Contract
                    && string.Equals(args.Action, ListContract, StringComparison.Ordinal))
                {
                    args.IUbs.Run("UbsItemsRemove", null);

                    if (m_calledFromFrontOffice)
                    {
                        args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                            new KeyValuePair<string, object>("наименование", "Доступен для единой формы обслуживания"),
                            new KeyValuePair<string, object>("значение по умолчанию", 1),
                            new KeyValuePair<string, object>("условие по умолчанию", "="),
                            new KeyValuePair<string, object>("скрытый", true) }));
                    }

                    if (!m_contractFilterLimitations)
                    {
                        string bic = txtRecipientBic.Text.Trim();
                        string acc = ucaRecipientAccount.Text.Trim();
                        string inn = txtRecipientInn.Text.Trim();

                        if (bic.Length > 0 || (acc.Length > 0 && acc != "00000000000000000000"))
                        {
                            if (bic.Length > 0)
                            {
                                args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                                    new KeyValuePair<string, object>("наименование", "БИК"),
                                    new KeyValuePair<string, object>("значение по умолчанию", bic),
                                    new KeyValuePair<string, object>("условие по умолчанию", "="),
                                    new KeyValuePair<string, object>("скрытый", false) }));
                            }
                            if (acc.Length > 0 && acc != "00000000000000000000")
                            {
                                args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                                    new KeyValuePair<string, object>("наименование", "Расчетный счет"),
                                    new KeyValuePair<string, object>("значение по умолчанию", acc),
                                    new KeyValuePair<string, object>("условие по умолчанию", "="),
                                    new KeyValuePair<string, object>("скрытый", false) }));
                            }
                            if (inn.Length > 0)
                            {
                                args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                                    new KeyValuePair<string, object>("наименование", "ИНН"),
                                    new KeyValuePair<string, object>("значение по умолчанию", inn),
                                    new KeyValuePair<string, object>("условие по умолчанию", "="),
                                    new KeyValuePair<string, object>("скрытый", false) }));
                            }
                        }
                    }
                    else
                    {
                        if (string.Equals(m_searchKBK, "Да", StringComparison.Ordinal))
                        {
                            args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                                new KeyValuePair<string, object>("наименование", "КБК"),
                                new KeyValuePair<string, object>("значение по умолчанию", txtTaxKbk.Text),
                                new KeyValuePair<string, object>("условие по умолчанию", "="),
                                new KeyValuePair<string, object>("скрытый", true) }));
                        }

                        if (m_searchTemplate.Length > 0
                            && !string.Equals(m_searchTemplate, "*", StringComparison.Ordinal))
                        {
                            args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                                new KeyValuePair<string, object>("наименование", "Шаблон"),
                                new KeyValuePair<string, object>("значение по умолчанию", m_searchTemplate),
                                new KeyValuePair<string, object>("условие по умолчанию", "="),
                                new KeyValuePair<string, object>("скрытый", true) }));
                        }

                        if (string.Equals(m_searchBIK, "Да", StringComparison.Ordinal))
                        {
                            args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                                new KeyValuePair<string, object>("наименование", "БИК"),
                                new KeyValuePair<string, object>("значение по умолчанию", txtRecipientBic.Text),
                                new KeyValuePair<string, object>("условие по умолчанию", "="),
                                new KeyValuePair<string, object>("скрытый", true) }));
                        }
                        else if (m_searchBIK.Length == 0)
                        {
                            args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                                new KeyValuePair<string, object>("наименование", "БИК"),
                                new KeyValuePair<string, object>("значение по умолчанию", string.Empty),
                                new KeyValuePair<string, object>("условие по умолчанию", "="),
                                new KeyValuePair<string, object>("скрытый", true) }));
                        }
                    }

                    args.IUbs.Run("UbsItemsRefresh", null);
                    return;
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        #endregion
    }
}
