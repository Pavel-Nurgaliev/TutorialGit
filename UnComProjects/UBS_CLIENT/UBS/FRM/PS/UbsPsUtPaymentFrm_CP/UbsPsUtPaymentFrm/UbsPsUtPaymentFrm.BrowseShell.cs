using System;
using System.Drawing;
using System.Windows.Forms;

namespace UbsBusiness
{
    public partial class UbsPsUtPaymentFrm
    {
        private const int MaxRecipientNameLength = 160;
        private const int MaxPurposeLength = 210;
        private const string ListClientGuest = "UBS_LIST_OC_OC_CLIENTS";
        private const string ListClientCommon = "UBS_LIST_COMMON_CLIENT";
        private const string ListContract = @"UBS_PS_UT_LIST_CONTRACT";
        private const string ListPayment = @"UBS_PS_UT_LIST_PAYMENT";
        private const string ListAttributeRecip = @"UBS_PS_LIST_ATTRIBUTE_RECIPIENT";
        private const string DefaultPaymDicFilter = @"UBS_PS_LIST_PAYM_DIC";

        #region Client selection

        private void BtnClient_ClickImpl()
        {
            try
            {
                string listSid = m_isGuest ? ListClientGuest : ListClientCommon;

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
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region Contract selection

        private void BtnContract_ClickImpl()
        {
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

                object[] ids = this.Ubs_ActionRun(ListAttributeRecip, this, true) as object[];
                if (ids == null || ids.Length == 0)
                    return;

                int selectedId = Convert.ToInt32(ids[0]);
                if (selectedId <= 0)
                    return;

                m_idAttributeRecip = selectedId;

                this.IUbsChannel.ParamIn("IdAttributeRecip", m_idAttributeRecip);
                this.IUbsChannel.Run("ReadRecipFromId");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                txtRecipientName.Text = paramOut.GetParamOutString("\u041D\u0430\u0438\u043C\u0435\u043D\u043E\u0432\u0430\u043D\u0438\u0435 \u043F\u043E\u043B\u0443\u0447\u0430\u0442\u0435\u043B\u044F \u0432 \u043F\u043B\u0430\u0442. \u0434\u043E\u043A\u0443\u043C\u0435\u043D\u0442\u0430\u0445");
                txtRecipientBik.Text = paramOut.GetParamOutString("BIC");
                ucaRecipientCorrAccount.Text = paramOut.GetParamOutString("CORRACC");
                txtRecipientBankName.Text = paramOut.GetParamOutString("\u041D\u0430\u0438\u043C\u0435\u043D\u043E\u0432\u0430\u043D\u0438\u0435 \u0431\u0430\u043D\u043A\u0430");
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
                this.IUbsChannel.ParamIn("\u041D\u0430\u0438\u043C\u0435\u043D\u043E\u0432\u0430\u043D\u0438\u0435 \u043F\u043E\u043B\u0443\u0447\u0430\u0442\u0435\u043B\u044F \u0432 \u043F\u043B\u0430\u0442. \u0434\u043E\u043A\u0443\u043C\u0435\u043D\u0442\u0430\u0445", txtRecipientName.Text);
                this.IUbsChannel.ParamIn("BIC", txtRecipientBik.Text);
                this.IUbsChannel.ParamIn("CORRACC", ucaRecipientCorrAccount.Text);
                this.IUbsChannel.ParamIn("\u041D\u0430\u0438\u043C\u0435\u043D\u043E\u0432\u0430\u043D\u0438\u0435 \u0431\u0430\u043D\u043A\u0430", txtRecipientBankName.Text);
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

                object[] ids = this.Ubs_ActionRun(listSid, this, true) as object[];
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
            btnSave.Click += btnSave_Click;
            btnExit.Click += btnExit_Click;
            btnPattern.Click += delegate { BtnPattern_ClickImpl(); };
            btnCashSymb.Click += delegate { BtnCashSymb_ClickImpl(); };
            btnCalc.Click += delegate { BtnCalc_ClickImpl(); };
            btnSaveRecipientAttribute.Click += delegate { BtnSaveRecipientAttribute_ClickImpl(); };

            linkThirdPersonName.LinkClicked += delegate { BtnSelectThirdPerson_ClickImpl(); };

            txtRecipientName.TextChanged += txtRecipientName_TextChanged;
            cmbPurpose.TextChanged += cmbPurpose_TextChanged;

            txtRecipientBik.Enter += txtRecipientBik_Enter;
            ucaRecipientAccount.Leave += ucaRecipientAccount_Leave;
            cmbThirdPersonKind.Leave += cmbThirdPersonKind_Leave;
            chkBenefits.CheckedChanged += chkBenefits_CheckedChanged;

            ucfAddProperties.KeyPress += ucfAddProperties_KeyPress;
        }

        #endregion
    }
}
