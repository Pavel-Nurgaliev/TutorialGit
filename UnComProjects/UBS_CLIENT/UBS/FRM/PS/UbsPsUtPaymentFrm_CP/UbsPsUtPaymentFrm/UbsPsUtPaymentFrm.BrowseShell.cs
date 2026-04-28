using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using UbsService;

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

                string bic = txtRecipientBik.Text.Trim();
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

        private void CreateUserFormArray()
        {
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
                txtRecipientBik.Text = paramOut.GetParamOutString("BIC");
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
                this.IUbsChannel.ParamIn("BIC", txtRecipientBik.Text);
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
            m_bicOld = txtRecipientBik.Text;
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
                    tabPageThirdPerson.Visible = true;
                    txtTaxStatus.Enabled = true;

                    if (cmbThirdPersonKind.SelectedIndex >= 0)
                    {
                        ThirdPersonKindChanged();
                    }
                }
                else
                {
                    tabPageThirdPerson.Visible = false;
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
                    if (tabPageTax.Visible)
                    {
                        tabPayment.SelectedTab = tabPageTax;
                    }
                    else if (tabPageThirdPerson.Visible)
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

            txtRecipientBik.Enter += txtRecipientBik_Enter;
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
                        new KeyValuePair<string, object>("значение по умолчанию", 2),
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
                        string bic = txtRecipientBik.Text.Trim();
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
                                new KeyValuePair<string, object>("значение по умолчанию", txtRecipientBik.Text),
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
