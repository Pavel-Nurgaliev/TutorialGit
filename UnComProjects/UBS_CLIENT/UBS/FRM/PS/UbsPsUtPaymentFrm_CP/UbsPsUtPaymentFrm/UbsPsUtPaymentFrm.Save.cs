using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    public partial class UbsPsUtPaymentFrm
    {
        /// <summary>
        /// Main save button handler.
        /// VB6: btnSave_Click (lines 3811–4694).
        /// </summary>
        private void BtnSave_ClickImpl()
        {
            try
            {
                m_isNoMessage = true;

                if (!ValidateInnBeforeSave())
                    return;

                if (!ValidateCashierBeforeSave())
                    return;

                if (!ValidateBatchNumber())
                    return;

                if (!ValidateThirdPersonBeforeSave())
                    return;

                if (!ValidateTaxBeforeSave())
                    return;

                SaveOldPayerValues();

                m_isSave = true;
                SetButtonsEnabled(false);

                if (m_isStopPrint)
                {
                    this.UbsChannel_ParamIn("IdPayment", m_idPayment);
                    this.UbsChannel_Run("DeleteDocForm");
                }

                if (!ValidatePurposeBeforeSave())
                {
                    RestoreSaveUiState();
                    return;
                }

                if (!ValidateRecipientNameLength())
                {
                    RestoreSaveUiState();
                    return;
                }

                if (tabPageTax.Visible)
                {
                    if (txtRecipientInn.Text.Trim().Length == 0 || txtRecipientInn.Text.Trim() == "0")
                    {
                        MessageBox.Show(MsgTaxInnRecipientRequired, CaptionPaymentAccept,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        RestoreSaveUiState();
                        return;
                    }
                    SaveNalogAddFields();
                }

                if (!ValidateThirdPersonInnKpp())
                {
                    RestoreSaveUiState();
                    return;
                }

                ClearClientIfPayerEmpty();

                if (!CheckPayment())
                {
                    RestoreSaveUiState();
                    return;
                }

                if (!CheckAccPayment())
                {
                    RestoreSaveUiState();
                    return;
                }

                SetButtonsEnabled(false);

                m_isClickSave = true;

                m_prefCalcRate = "_1 0";
                if (!string.Equals(m_command, StrCommandChangePart, StringComparison.Ordinal)
                    && !string.Equals(m_command, StrCommandGroupChange, StringComparison.Ordinal)
                    && !string.Equals(m_commandSource, StrCommandChangePartIncoming, StringComparison.Ordinal))
                {
                    if (!CalcSumCommiss())
                    {
                        m_isClickSave = false;
                        RestoreSaveUiState();
                        return;
                    }
                }

                m_isClickSave = false;

                if (!CalcSumNDS())
                {
                    m_isClickSave = false;
                    RestoreSaveUiState();
                    return;
                }

                if (!CheckSumControl())
                {
                    RestoreSaveUiState();
                    return;
                }

                if (!string.Equals(m_commandSource, StrCommandChangePart, StringComparison.Ordinal))
                {
                    GetPaymentState();
                }

                if (!UtCheckUserBeforeSave())
                {
                    RestoreSaveUiState();
                    return;
                }

                m_isClickSave = false;
                m_isCreateCashOrd = false;

                CollectPaymentParamIn();

                if (!ValidatePayerAccountNotEmpty())
                {
                    RestoreSaveUiState();
                    return;
                }

                if (!ValidatePayerCheckSumNotEmpty())
                {
                    RestoreSaveUiState();
                    return;
                }

                CollectPatternSpecificParamIn();
                CollectGroupParamIn();

                if (m_idClient != 0)
                {
                    if (!CheckIPDL())
                    {
                        MessageBox.Show(MsgIpdlSaveForbidden, CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        RestoreSaveUiState();
                        return;
                    }
                    this.UbsChannel_ParamIn("blnIPDL", m_isIPDL);
                }

                CollectSecondPaymentParamIn();
                CollectThirdPersonParamIn();
                CollectTaxParamIn();
                CollectFrontOfficeParamIn();

                this.UbsChannel_ParamIn("StrCommand_Source", m_commandSource);
                this.UbsChannel_ParamIn("txtINNPay", txtPayerInn.Text);

                Payment_Save();
            }
            catch (Exception ex)
            {
                RestoreSaveUiState();
                this.Ubs_ShowError(ex);
            }
        }

        #region Pre-save validations

        private bool ValidateInnBeforeSave()
        {
            if (txtPayerInn.Text.Length > 0 && txtPayerInn.Text != "0")
            {
                if (!CheckKeyInn(txtPayerInn.Text))
                {
                    MessageBox.Show(string.Format(MsgInnPayerCheckFailed, txtPayerInn.Text),
                        CaptionPaymentAccept, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (txtRecipientInn.Text.Length > 0)
            {
                if (!CheckKeyInn(txtRecipientInn.Text))
                {
                    MessageBox.Show(string.Format(MsgInnRecipientCheckFailed, txtRecipientInn.Text),
                        CaptionPaymentAccept, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private bool ValidateCashierBeforeSave()
        {
            try
            {
                if (m_foSettingValue > -1)
                {
                    this.IUbsChannel.ParamIn("FROMFO", m_foSettingValue);
                }

                this.IUbsChannel.Run("PS_UserIsCashier");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                string strError = paramOut.GetParamOutString("StrError");
                if (strError.Length > 0)
                {
                    MessageBox.Show(strError, CaptionPaymentAccept,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (paramOut.GetParamOutBool("NotCashier"))
                {
                    MessageBox.Show(MsgNotCashier, CaptionPaymentAccept,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.btnExit_Click(this, EventArgs.Empty);
                    return false;
                }

                m_isCashier = !paramOut.GetParamOutBool("bRet");

                return true;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return false;
            }
        }

        private bool ValidateBatchNumber()
        {
            if (txtBatchNumber.Text.Trim().Length == 0 && m_calledFromFrontOffice)
            {
                MessageBox.Show(MsgBatchNumberRequired, CaptionPaymentAccept,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool ValidateThirdPersonBeforeSave()
        {
            if (txtThirdPersonName.Text.Trim().Length > 0)
            {
                if (txtThirdPersonInn.Text.Length == 0 || txtThirdPersonInn.Text == "0")
                {
                    MessageBox.Show(MsgThirdPersonInnRequired, CaptionPaymentAccept,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private bool ValidateTaxBeforeSave()
        {
            if (!tabPageTax.Visible) return true;

            try
            {
                if (txtTaxReasonCode.Text == "ТП" || txtTaxReasonCode.Text == "ЗД")
                {
                    txtTaxDocumentNumber.Text = "0";
                }
                else
                {
                    if (m_idClient != 0)
                    {
                        if ((txtPayerInn.Text.Trim().Length == 0 || txtPayerInn.Text.Trim() == "0")
                            && (txtTaxDocumentNumber.Text.Trim().Length == 0 || txtTaxDocumentNumber.Text.Trim() == "0"))
                        {
                            this.IUbsChannel.ParamIn("idClient", m_idClient);
                            this.IUbsChannel.Run("GetNalogNumber");
                            var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                            txtTaxDocumentNumber.Text = paramOut.GetParamOutString("NalogNumber");
                        }
                    }
                }

                if (txtTaxKbk.Text.Trim().Length > 0)
                {
                    this.IUbsChannel.ParamIn("КБК", txtTaxKbk.Text);
                    this.IUbsChannel.Run("CheckKBK");
                    var paramOutKbk = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                    bool isNalog = paramOutKbk.GetParamOutBool("isNalog");

                    if (isNalog)
                    {
                        this.IUbsChannel.ParamIn("Field108", txtTaxDocumentNumber.Text);
                        this.IUbsChannel.ParamIn("IdClient", m_idClient);
                        this.IUbsChannel.ParamIn("Guest", m_isGuest);
                        this.IUbsChannel.Run("CheckRegex108Field");
                        var paramOutRegex = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                        if ((txtPayerInn.Text.Trim().Length == 0 || txtPayerInn.Text.Trim() == "0")
                            && !paramOutRegex.GetParamOutBool("Result"))
                        {
                            MessageBox.Show(MsgTaxInnPayerRequired, CaptionPaymentAccept,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            m_isSave = false;
                            SetButtonsEnabled(true);
                            tabPayment.SelectedTab = tabPageGeneral;
                            if (txtPayerInn.Visible && txtPayerInn.Enabled) txtPayerInn.Focus();
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }

            return true;
        }

        private bool ValidateThirdPersonInnKpp()
        {
            if (!chkThirdPerson.Checked) return true;

            if (txtThirdPersonName.Text.Length == 0)
            {
                MessageBox.Show(MsgThirdPersonNameRequired, CaptionPaymentAccept,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            switch (cmbThirdPersonKind.SelectedIndex)
            {
                case 0:
                    if (txtThirdPersonInn.Text == "0" || txtThirdPersonInn.Text.Length != 12)
                    {
                        MessageBox.Show(MsgThirdPersonInn12Required, CaptionPaymentAccept,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    break;
                case 1:
                    if (txtThirdPersonInn.Text != "0" && txtThirdPersonInn.Text.Length != 12)
                    {
                        MessageBox.Show(MsgThirdPersonInn12Or0Required, CaptionPaymentAccept,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    break;
                case 2:
                    if (txtThirdPersonInn.Text == "0" || txtThirdPersonInn.Text.Length != 10)
                    {
                        MessageBox.Show(MsgThirdPersonInn10Required, CaptionPaymentAccept,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (txtThirdPersonKpp.Text == "0" || txtThirdPersonKpp.Text.Length != 9)
                    {
                        MessageBox.Show(MsgThirdPersonKpp9Required, CaptionPaymentAccept,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    break;
            }

            return true;
        }

        private bool ValidatePurposeBeforeSave()
        {
            if (cmbPurpose.Text.Trim().Length == 0)
            {
                MessageBox.Show(MsgPurposeEmpty, CaptionPaymentAccept,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbPurpose.Text.Length > 210)
            {
                MessageBox.Show(MsgPurposeTooLong, CaptionPaymentAccept,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool ValidateRecipientNameLength()
        {
            if (txtRecipientName.Text.Length > 160)
            {
                MessageBox.Show(MsgRecipientNameTooLong, CaptionPaymentAccept,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool ValidatePayerAccountNotEmpty()
        {
            if (txtPayerAccount.Enabled && m_idPaymentDic == 0)
            {
                if (m_strAccCode == null || m_strAccCode.Trim().Length == 0)
                {
                    MessageBox.Show(MsgPayerAccountEmpty, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private bool ValidatePayerCheckSumNotEmpty()
        {
            if (txtCheckSum.Visible)
            {
                if (m_strCheckSum == null || m_strCheckSum.Trim().Length == 0)
                {
                    MessageBox.Show(MsgPayerCheckSumEmpty, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region ParamIn collection

        private void SaveOldPayerValues()
        {
            m_strFIOOld = txtPayerFullName.Text;
            m_strINNOld = txtPayerInn.Text;
            m_strAddressOld = txtPayerAddress.Text;
        }

        private void SaveNalogAddFields()
        {
            try
            {
                this.IUbsChannel.ParamIn("Статус составителя", txtTaxStatus.Text);
                this.IUbsChannel.ParamIn("Код бюджетной классификации", txtTaxKbk.Text);
                this.IUbsChannel.ParamIn("Код ОКАТО", txtTaxOkato.Text);
                this.IUbsChannel.ParamIn("Основание налогового платежа", txtTaxReasonCode.Text);
                this.IUbsChannel.ParamIn("Налоговый период", txtTaxPeriodCode.Text);
                this.IUbsChannel.ParamIn("Номер налогового документа", txtTaxDocumentNumber.Text);
                this.IUbsChannel.ParamIn("Дата налогового документа", txtTaxDocumentDate.Text);
                this.IUbsChannel.ParamIn("Тип налогового платежа", txtTaxType.Text);
                this.IUbsChannel.ParamIn("ИМНС", txtTaxImns.Text);
                this.IUbsChannel.ParamIn("КППУ", txtTaxKbkNote.Text.Trim());
                this.IUbsChannel.Run("NalogAddFieldSave");
                ucfAddProperties.Refresh();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void ClearClientIfPayerEmpty()
        {
            bool payerEmpty = (txtPayerFullName.Text.Trim().Length == 0
                && txtPayerInn.Text.Trim().Length == 0
                && txtPayerAddress.Text.Trim().Length == 0);

            if (payerEmpty || !CheckPayer(true))
            {
                m_idClient = 0;
                linkFindFilter.Visible = false;
            }
        }

        /// <summary>
        /// Builds the main set of ParamIn keys before Payment_Save.
        /// VB6: objParamIn assembly block (lines 4071–4351).
        /// </summary>
        private void CollectPaymentParamIn()
        {
            this.UbsChannel_ParamIn("FROMFO", m_calledFromFrontOffice);

            CollectPeriodDates();

            this.UbsChannel_ParamIn("IdContract", m_idContract);
            this.UbsChannel_ParamIn("SummaRateRec", m_curSumRateRec);
            this.UbsChannel_ParamIn("SummaRec", m_curSumRec);
            this.UbsChannel_ParamIn("SummaNDSRec", m_curSumNDSRec);
            this.UbsChannel_ParamIn("SummaNDSSend", m_curSumNDSSend);
            this.UbsChannel_ParamIn("SummaNDSPaym", m_curSumNDSPaym);

            if (m_strCheckType == null || m_strCheckType.Trim().Length == 0)
            {
                if (m_strCheckSum != null && m_strCheckSum.Length > 0)
                {
                    m_strCheckType = "ключ сформирован автоматически";
                }
            }
            this.UbsChannel_ParamIn("SignCreateKey", m_strCheckType);
            this.UbsChannel_ParamIn("bIsPeriodEnable", m_isPeriodEnable);
            this.UbsChannel_ParamIn("RegNumber", m_RegNumber);
            this.UbsChannel_ParamIn("Address", m_strAddress);
            this.UbsChannel_ParamIn("AccCode", m_strAccCode);

            if (m_idPaymentDic != 0)
            {
                m_strCheckSum = txtCheckSum.Text;
            }
            this.UbsChannel_ParamIn("CheckSum", m_strCheckSum);
            this.UbsChannel_ParamIn("RecipientName", txtRecipientName.Text);
            this.UbsChannel_ParamIn("Льготный клиент", chkBenefits.Checked);
            this.UbsChannel_ParamIn("Основание", txtBenefitReason.Text);
            this.UbsChannel_ParamIn("blnGuest", m_isGuest);
            this.UbsChannel_ParamIn("ID_CLIENT", m_idClient);

            if (!m_isGuest && m_idClient > 0)
            {
                this.UbsChannel_ParamIn("Идентификатор клиента, инициирующего действие", m_idClient);
            }

            CollectCashSymbolsParamIn();

            this.UbsChannel_ParamIn("Purpose", cmbPurpose.Text);
            this.UbsChannel_ParamIn("StrCommand", StrCommandAdd);
            this.UbsChannel_ParamIn("COMMANDIN", m_command);

            if (m_isPenyPresent)
            {
                this.UbsChannel_ParamIn("SUMMAPENI", udcPenaltyAmount.DecimalValue);
            }
            else
            {
                this.UbsChannel_ParamIn("SUMMAPENI", 0);
            }

            if (ucaPayerAccount.Visible)
            {
                this.UbsChannel_ParamIn("AccClientPay", ucaPayerAccount.Text);
            }

            this.UbsChannel_ParamIn("NeedControl", m_isControlSum);
            this.UbsChannel_ParamIn("blnTerror", m_isTerror);

            if (m_idPaymentDic != 0)
            {
                this.UbsChannel_ParamIn("IdPaymentDic", true);
            }

            if (m_idGroupIncoming > 0)
            {
                this.UbsChannel_ParamIn("IdGroupIncoming", m_idGroupIncoming);
            }
        }

        private void CollectPeriodDates()
        {
            if (txtPeriodYearBeg.Text.Length > 0 && txtPeriodMonthBeg.Text.Length > 0 && txtPeriodDayBeg.Text.Length > 0
                && txtPeriodYearEnd.Text.Length > 0 && txtPeriodMonthEnd.Text.Length > 0 && txtPeriodDayEnd.Text.Length > 0)
            {
                int yBeg, mBeg, dBeg, yEnd, mEnd, dEnd;
                if (int.TryParse(txtPeriodYearBeg.Text, out yBeg)
                    && int.TryParse(txtPeriodMonthBeg.Text, out mBeg)
                    && int.TryParse(txtPeriodDayBeg.Text, out dBeg)
                    && int.TryParse(txtPeriodYearEnd.Text, out yEnd)
                    && int.TryParse(txtPeriodMonthEnd.Text, out mEnd)
                    && int.TryParse(txtPeriodDayEnd.Text, out dEnd))
                {
                    if (yBeg < 100) yBeg += 2000;
                    if (yEnd < 100) yEnd += 2000;

                    DateTime dateBeg = new DateTime(yBeg, mBeg, dBeg);
                    DateTime dateEnd = new DateTime(yEnd, mEnd, dEnd);
                    this.UbsChannel_ParamIn("DateBeg", dateBeg);
                    this.UbsChannel_ParamIn("DateEnd", dateEnd);
                }
            }
        }

        private void CollectCashSymbolsParamIn()
        {
            if (m_isRegimCashSymb)
            {
                if (txtCashSymbolPayment.Text.Length > 0
                    || txtCashSymbolCommission.Text.Length > 0
                    || txtCashSymbolNds.Text.Length > 0)
                {
                    object[,] varCashSymbol = new object[1, 3];
                    varCashSymbol[0, 0] = txtCashSymbolPayment.Text;
                    varCashSymbol[0, 1] = txtCashSymbolCommission.Text;
                    varCashSymbol[0, 2] = txtCashSymbolNds.Text;
                    this.UbsChannel_ParamIn("Кассовые символа", varCashSymbol);
                }
            }

            if (m_arrCashSymb != null)
            {
                this.UbsChannel_ParamIn("Массив кассовых символов", m_arrCashSymb);
                if (txtCashSymbolCommission.Text.Length > 0 || txtCashSymbolNds.Text.Length > 0)
                {
                    object[,] varCS = new object[1, 3];
                    varCS[0, 0] = string.Empty;
                    varCS[0, 1] = txtCashSymbolCommission.Text;
                    varCS[0, 2] = txtCashSymbolNds.Text;
                    this.UbsChannel_ParamIn("Кассовые символа", varCS);
                }
            }
        }

        private void CollectPatternSpecificParamIn()
        {
            if (string.Equals(m_sidPattern, PatternEnergy, StringComparison.Ordinal))
            {
                if (cmbTariff.SelectedIndex >= 0)
                {
                    this.UbsChannel_ParamIn("IdTariff", m_idTariff);
                }
            }
            else if (string.Equals(m_sidPattern, PatternPhone, StringComparison.Ordinal)
                || string.Equals(m_sidPattern, PatternPhoneAcc, StringComparison.Ordinal))
            {
                if (cmbPhone.SelectedIndex >= 0)
                {
                    this.UbsChannel_ParamIn("IdPhone", m_idPhone);
                }
            }
        }

        private void CollectGroupParamIn()
        {
            if (m_isGroup)
            {
                if (m_idGroup == 0)
                {
                    this.IUbsChannel.Run("UTGetNewPaymGroupID");
                    var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                    m_idGroup = paramOut.GetParamOutInt("PAYMENTGROUPID");
                }
                this.UbsChannel_ParamIn("PAYMENTGROUPID", m_idGroup);
            }
        }

        private void CollectSecondPaymentParamIn()
        {
            if (tabPageTax.Visible && m_isSecondPayment == 2)
            {
                m_idContractSecond = 0;
                this.UbsChannel_ParamIn("SummaRateSend", 0);
                this.UbsChannel_ParamIn("SummaNDSSend", 0);
            }

            if (m_idContractSecond > 0)
            {
                this.UbsChannel_ParamIn("IdContractSecond", m_idContractSecond);
                this.UbsChannel_ParamIn("Кассовые символа дополнительный платеж", m_arrCashSymbSecond);
            }
        }

        private void CollectThirdPersonParamIn()
        {
            if (chkThirdPerson.Checked)
            {
                this.UbsChannel_ParamIn("ThirdPerson_Name", txtThirdPersonName.Text);
                this.UbsChannel_ParamIn("ThirdPerson_Kind", cmbThirdPersonKind.SelectedIndex);
                this.UbsChannel_ParamIn("ThirdPerson_INN", txtThirdPersonInn.Text);
                this.UbsChannel_ParamIn("ThirdPerson_KPP", txtThirdPersonKpp.Text);
            }
        }

        private void CollectTaxParamIn()
        {
            if (!tabPageTax.Visible) return;

            this.UbsChannel_ParamIn("Статус составителя", txtTaxStatus.Text);
            this.UbsChannel_ParamIn("Код бюджетной классификации", txtTaxKbk.Text);
            this.UbsChannel_ParamIn("Код ОКАТО", txtTaxOkato.Text);
            this.UbsChannel_ParamIn("Основание налогового платежа", txtTaxReasonCode.Text);
            this.UbsChannel_ParamIn("Налоговый период", txtTaxPeriodCode.Text);
            this.UbsChannel_ParamIn("Номер налогового документа", txtTaxDocumentNumber.Text);
            this.UbsChannel_ParamIn("Дата налогового документа", txtTaxDocumentDate.Text);
            this.UbsChannel_ParamIn("Тип налогового платежа", txtTaxType.Text);
            this.UbsChannel_ParamIn("ИМНС", txtTaxImns.Text);
            this.UbsChannel_ParamIn("КППУ", txtTaxKbkNote.Text.Trim());
        }

        private void CollectFrontOfficeParamIn()
        {
            if (m_calledFromFrontOffice)
            {
                this.UbsChannel_ParamIn("CalledFromFrontOffice", 1);
                this.UbsChannel_ParamIn("Номер пачки", txtBatchNumber.Text);
            }

            if (m_fromFoAsClient != -1)
            {
                this.UbsChannel_ParamIn("fromFoAsClient", m_fromFoAsClient);
            }
        }

        #endregion

        #region Payment_Save execution and post-save

        /// <summary>
        /// Executes Payment_Save channel and handles post-save flow:
        /// updates counters, group handling, print, cash orders, new record.
        /// VB6: lines 4351–4694 in btnSave_Click.
        /// </summary>
        private void Payment_Save()
        {
            try
            {
                this.UbsChannel_Run("Payment_Save");

                var paramOut = new UbsParamCustom(this.UbsChannel_ParamsOut);

                string strError = paramOut.GetParamOutString("StrError");
                if (strError.Length > 0)
                {
                    MessageBox.Show(strError, CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    uciInfo.Show(strError);
                    RestoreSaveUiState();
                    return;
                }

                m_needRefreshGrid = true;
                uciInfo.Show(MsgPaymentSavedDb);

                if (!m_isGroup)
                {
                    if (!string.Equals(m_command, StrCommandChangePart, StringComparison.Ordinal))
                    {
                        m_countPaper++;
                        txtSubPaymentCount.Text = m_countPaper.ToString();
                        udcCommonAmount.DecimalValue += udcPaymentAmount.DecimalValue;
                        udcTotalAmount.DecimalValue += udcAmountWithRate.DecimalValue;
                    }
                }

                bool isAdd = string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal);
                if ((isAdd || m_isStopPrint) && !m_isControlSum)
                {
                    m_idPayment = paramOut.GetParamOutInt("IdPaym");

                    if (!m_calledFromFrontOffice)
                    {
                        m_arrSecondPaym = paramOut.Contains("ID_DOUBLE_PAYMENT")
                            ? paramOut.Value("ID_DOUBLE_PAYMENT") : null;

                        HandlePostSaveNonGroup();
                        HandlePostSaveGroup(paramOut);
                        HandlePostSaveIncomingGroup();
                        PrepareExternalDocuments();

                        UpdatePrintFormFlag();
                    }

                    HandlePrintForms();
                    HandleFiscalRegister(paramOut);
                }

                HandlePostSaveUserScript();
                HandleCheckOrEndGroup(paramOut);

                PostSaveUiRestore();

                if (string.Equals(m_command, StrCommandChangePart, StringComparison.Ordinal))
                {
                    txtSubPaymentCount.Text = "0";
                    udcCommonAmount.DecimalValue = 0;
                    udcTotalAmount.DecimalValue = 0;
                    txtGroupPaymentId.Text = "0";
                    m_idGroup = 0;
                }

                m_command = StrCommandAdd;

                NewRecord();
            }
            catch (Exception ex)
            {
                RestoreSaveUiState();
                this.Ubs_ShowError(ex);
            }
        }

        private void HandlePostSaveNonGroup()
        {
            if (m_isGroup) return;
            if (ucaPayerAccount.Text.Length > 0
                && !string.Equals(ucaPayerAccount.Text, "00000000000000000000", StringComparison.Ordinal))
            {
                return;
            }

            try
            {
                this.IUbsChannel.ParamIn("Sum", udcAmountWithRate.DecimalValue);
                this.IUbsChannel.Run("UTIsMoveValByAccountA");
                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                string err = paramOut.GetParamOutString("Error");
                if (err.Length > 0)
                {
                    MessageBox.Show(err, CaptionPaymentAccept, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }

            CreateCashOrd();
        }

        private void HandlePostSaveGroup(UbsParamCustom paramOutSave)
        {
            if (!m_isGroup) return;

            m_oldGroupId = m_idGroup;
            UpdateGroupInfo();

            string msg = string.Format(MsgGroupCountSummary,
                txtSubPaymentCount.Text, udcTotalAmount.DecimalValue);

            if (MessageBox.Show(msg, MsgGroupInputCaption,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                try
                {
                    this.IUbsChannel.ParamIn("Sum", udcTotalAmount.DecimalValue);
                    this.IUbsChannel.ParamIn("IdGroup", m_idGroup);
                    this.IUbsChannel.Run("UTIsMoveValByAccountA");
                    var p = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                    string err = p.GetParamOutString("Error");
                    if (err.Length > 0)
                    {
                        MessageBox.Show(err, CaptionPaymentAccept, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex) { this.Ubs_ShowError(ex); }

                this.UbsChannel_ParamIn("PAYMENTGROUPID", m_idGroup);
                CreateCashOrd();

                m_idGroup = 0;
                UpdateGroupInfo();
            }
        }

        private void HandlePostSaveIncomingGroup()
        {
            if (m_idGroupIncoming > 0)
            {
                UpdateGroupInfo();
            }
        }

        private void PrepareExternalDocuments()
        {
            try
            {
                this.IUbsChannel.ParamIn("blnTerror", m_isTerror);
                this.IUbsChannel.ParamIn("IdPayment", m_idPayment);
                this.IUbsChannel.ParamIn("IdContract", m_idContract);

                if (m_idContractSecond != 0 && m_arrSecondPaym != null)
                {
                    this.IUbsChannel.ParamIn("arrSecondPaym", m_arrSecondPaym);
                    this.IUbsChannel.Run("UtGetDoublePaymentsPlatDocExt_Form");
                }
                else
                {
                    this.IUbsChannel.Run("UtGetPaymentsPlatDocExt_Form");
                }

                var p = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                string err = p.GetParamOutString("StrError");
                if (err.Length > 0)
                {
                    MessageBox.Show(err, CaptionPrintDocPrep, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void UpdatePrintFormFlag()
        {
            if (chkPrintForms.Visible)
            {
                m_isIsFrmPrn = chkPrintForms.Checked;
            }
        }

        /// <summary>
        /// Calls Ps_CheckPrintForm and PsPrintForm.vbs via RunScript for post-save printing.
        /// VB6: lines 4479–4537 in btnSave_Click.
        /// </summary>
        private void HandlePrintForms()
        {
            if (!m_isIsFrmPrn || m_idGroupIncoming > 0) return;

            try
            {
                this.IUbsChannel.ParamIn("IdPayment", m_idPayment);
                this.IUbsChannel.ParamIn("NameSection", "Оплата услуг");
                this.IUbsChannel.Run("FormPrintPayment");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                bool isAskEdit = paramOut.GetParamOutBool("IsAskEdit");

                if (isAskEdit)
                {
                    this.IUbsChannel.Run("IsAskEdit");
                    var pEdit = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                    if (pEdit.Contains("EditPaym"))
                    {
                        if (MessageBox.Show(MsgReturnToEdit, CaptionValidation,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            RestoreSaveUiState();
                            m_command = StrCommandChangePart;
                            m_commandSource = StrCommandChangePart;
                            m_isStopPrint = true;
                            m_isNoMessage = false;
                            InitDoc();
                            return;
                        }
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        /// <summary>
        /// Prints payment on fiscal register (FR) if applicable.
        /// VB6: lines 4537–4572 in btnSave_Click.
        /// </summary>
        private void HandleFiscalRegister(UbsParamCustom paramOutSave)
        {
            if (!m_isCashier || m_idGroupIncoming > 0) return;

            bool frCondition = m_isFR && !m_isGroup;
            bool frGroupCondition = m_isFR && m_isGroup && m_idGroup == 0 && m_oldGroupId != 0;

            if (!frCondition && !frGroupCondition) return;

            try
            {
                if (!CheckStateFR())
                {
                    MessageBox.Show(MsgFrDisconnected, CaptionPaymentAccept,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RestoreSaveUiState();
                    return;
                }

                this.IUbsChannel.ParamIn("objDevice", m_objDevice);
                this.IUbsChannel.ParamIn("nIdPayment", m_idPayment);
                this.IUbsChannel.ParamIn("bIsFR", m_isFR);

                if (m_isGroup)
                {
                    this.IUbsChannel.ParamIn("nPaymentGroupId", m_oldGroupId);
                    this.IUbsChannel.ParamIn("nPaymentGroupQtty", m_countPaper);
                    this.IUbsChannel.ParamIn("nPaymentGroupSUMMA", udcCommonAmount.DecimalValue);
                    this.IUbsChannel.ParamIn("nPaymentGroupTOTAL", udcTotalAmount.DecimalValue);
                }

                this.IUbsChannel.ParamIn("curSUMMAPENI", udcPenaltyAmount.DecimalValue);

                if (m_idContractSecond != 0 && m_arrSecondPaym != null)
                {
                    this.IUbsChannel.ParamIn("arrSecondPaym", m_arrSecondPaym);
                    this.IUbsChannel.Run("FormDoublePrintPayment");
                }
                else
                {
                    this.IUbsChannel.Run("FormPrintPayment");
                }

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                if (!paramOut.GetParamOutBool("bRetVal"))
                {
                    MessageBox.Show(paramOut.GetParamOutString("StrError"),
                        CaptionFrPrint + CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RestoreSaveUiState();
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        /// <summary>
        /// Runs user post-save script (RunScrFromSetting).
        /// VB6: lines 4583–4600.
        /// </summary>
        private void HandlePostSaveUserScript()
        {
            if (m_isGroup && !(m_idGroup == 0 && m_oldGroupId != 0)) return;

            try
            {
                this.IUbsChannel.ParamIn("IdPayment", m_idPayment);
                this.IUbsChannel.ParamIn("blnGroup", m_idGroupIncoming > 0 || m_isGroup);
                this.IUbsChannel.Run("RunScrFromSetting");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                string err = paramOut.GetParamOutString("StrError");
                if (err.Trim().Length > 0)
                {
                    MessageBox.Show(err, CaptionPaymentAccept,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    NewRecord();
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        /// <summary>
        /// CheckOrEndGroup: for incoming-group mode, verifies payment sum vs group total.
        /// VB6: lines 4605–4637 in btnSave_Click.
        /// </summary>
        private void HandleCheckOrEndGroup(UbsParamCustom paramOutSave)
        {
            if (m_idGroupIncoming <= 0) return;

            try
            {
                this.IUbsChannel.ParamIn("IdGroupIncoming", m_idGroupIncoming);
                this.IUbsChannel.ParamIn("IdPaym", paramOutSave.GetParamOutInt("IdPaym"));
                this.IUbsChannel.ParamIn("curCommonSumma", udcCommonAmount.DecimalValue);

                this.IUbsChannel.Run("CheckOrEndGroup");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                int type = paramOut.GetParamOutInt("Type");

                if (type == 1)
                {
                    RunGroupEndScript(paramOut);
                    RestoreSaveUiState();
                    this.btnExit_Click(this, EventArgs.Empty);
                    return;
                }
                else if (type == 2)
                {
                    string msg = string.Format(MsgGroupSumExceeds,
                        udcCommonAmount.DecimalValue, paramOut.GetParamOutString("PaymentGroupSum"));
                    if (MessageBox.Show(msg, CaptionCheckSumPayment,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        RestoreSaveUiState();
                        this.btnExit_Click(this, EventArgs.Empty);
                        return;
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void RunGroupEndScript(UbsParamCustom paramOut)
        {
            try
            {
                int idPaymentGroup = paramOut.GetParamOutInt("IdPaymentGroup");
                this.IUbsChannel.ParamIn("Key", idPaymentGroup);
                this.IUbsChannel.ParamIn("IsReception", true);
                this.IUbsChannel.ParamIn("ViewPrintForm", m_isIsFrmPrn);
                this.IUbsChannel.Run("PaymentGroupEnd");
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void PostSaveUiRestore()
        {
            btnExit.Enabled = true;
            btnCalc.Enabled = true;
            btnPattern.Enabled = true;
            btnCashSymb.Enabled = true;
            chkPrintForms.Enabled = true;
            tabPayment.SelectedTab = tabPageGeneral;
            m_isSave = false;
        }

        #endregion

        #region Save attribute

        private void BtnSaveAttribute_ClickImpl()
        {
            try
            {
                if (m_idContract == 0)
                {
                    MessageBox.Show(MsgRecipientContractRequired, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.IUbsChannel.ParamIn("IdContract", m_idContract);
                this.IUbsChannel.Run("SaveAttributeRecip");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                if (paramOut.GetParamOutBool("bRetVal"))
                {
                    MessageBox.Show(MsgRecipientAttributesSaved, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string strError = paramOut.GetParamOutString("StrError");
                    if (strError.Length > 0)
                    {
                        MessageBox.Show(strError, CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region Pre-save checks

        private bool UtCheckUserBeforeSave()
        {
            try
            {
                this.IUbsChannel.ParamIn("IDCONTRACT", m_idContract);
                this.IUbsChannel.ParamIn("IDCLIENT", m_idClient);
                this.IUbsChannel.Run("Ut_CheckBeforeSave");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                string strError = paramOut.GetParamOutString("Error");
                bool bRet = paramOut.GetParamOutBool("bRet");

                if (!bRet)
                {
                    if (strError.Length > 0)
                    {
                        MessageBox.Show(strError, CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return false;
                }

                if (strError.Length > 0)
                {
                    return MessageBox.Show(strError, CaptionForm,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes;
                }

                return true;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return false;
            }
        }

        private bool CheckPayment()
        {
            try
            {
                if (!CheckLockPassport())
                    return false;

                if (m_idContract == 0)
                {
                    MessageBox.Show(MsgRecipientContractRequired, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (!CheckRecipientAccountKey())
                    return false;

                if (!CheckTerror())
                    return false;

                if (!CheckAddFields())
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return false;
            }
        }

        /// <summary>
        /// Validates recipient account key via CheckKey channel.
        /// VB6: CheckPayment lines 6770–6796.
        /// </summary>
        private bool CheckRecipientAccountKey()
        {
            try
            {
                string acc = ucaRecipientAccount.Text;
                if (acc.Length == 0) return true;

                string prefix = acc.Length >= 2 ? acc.Substring(0, 2) : string.Empty;
                string currCode = acc.Length >= 8 ? acc.Substring(5, 3) : string.Empty;

                if (prefix != "03" && currCode != "643")
                {
                    this.IUbsChannel.ParamIn("STRACC", acc);
                    this.IUbsChannel.ParamIn("BIC", txtRecipientBik.Text);
                    this.IUbsChannel.ParamIn("CORRACC", ucaRecipientCorrAccount.Text);
                    this.IUbsChannel.Run("CheckKey");

                    var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                    if (!paramOut.GetParamOutBool("RETVAL"))
                    {
                        MessageBox.Show(MsgInvalidAccountKey, CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tabPayment.SelectedTab = tabPageGeneral;
                        return false;
                    }
                }
                else
                {
                    if ((prefix == "03" && currCode != "643")
                        || (prefix != "03" && currCode == "643"))
                    {
                        MessageBox.Show(MsgInvalidCurrencyCode, CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tabPayment.SelectedTab = tabPageGeneral;
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return false;
            }
        }

        private bool CheckAddFields()
        {
            try
            {
                this.IUbsChannel.Run("CheckAddFields");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                if (!paramOut.GetParamOutBool("bRetVal"))
                {
                    string strError = paramOut.GetParamOutString("StrError");
                    if (strError.Length > 0)
                    {
                        MessageBox.Show(strError, CaptionCheck,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return false;
            }
        }

        private bool CheckTerror()
        {
            try
            {
                this.IUbsChannel.ParamIn("IDCONTRACT", m_idContract);
                this.IUbsChannel.ParamIn("RECIPIENTNAME", string.Empty);
                this.IUbsChannel.Run("CheckTerror");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                if (!paramOut.Contains("RETVAL"))
                {
                    m_isTerror = false;
                    return true;
                }

                if (paramOut.GetParamOutBool("RETVAL"))
                {
                    m_isTerror = false;
                    return true;
                }

                string strError = paramOut.GetParamOutString("StrError");
                if (strError.Length == 0)
                    return false;

                m_isTerror = MessageBox.Show(strError, CaptionValidation,
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;

                return m_isTerror;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return false;
            }
        }

        /// <summary>
        /// CheckLockPassport: verifies passport lock via UbsComCheck.CommonCheckPassport.
        /// VB6: Function CheckLockPassport() (lines 7039–7052).
        /// Note: this check is commented out in VB6 CheckPayment; kept for completeness.
        /// </summary>
        private bool CheckLockPassport()
        {
            try
            {
                if (m_docNumber.Trim().Length == 0 && m_docSeries.Trim().Length == 0)
                    return true;

                this.IUbsChannel.ParamIn("DocSeries", m_docSeries.Trim());
                this.IUbsChannel.ParamIn("DocNumber", m_docNumber.Trim());
                this.IUbsChannel.ParamIn("FIO", txtPayerFullName.Text);
                this.IUbsChannel.Run("CommonCheckPassport");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                return paramOut.GetParamOutBool("bRetVal");
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return true;
            }
        }

        /// <summary>
        /// CheckIPDL: checks if client is a politically exposed person (ИПДЛ).
        /// VB6: Function CheckIPDL() (lines 10001–10019).
        /// </summary>
        private bool CheckIPDL()
        {
            try
            {
                this.IUbsChannel.Run("CommonCheckIPDL");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                int ret = paramOut.GetParamOutInt("RetVal");

                if (ret == 0)
                {
                    m_isIPDL = false;
                    return true;
                }
                else if (ret == 1)
                {
                    m_isIPDL = true;
                    return true;
                }
                else
                {
                    m_isIPDL = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                m_isIPDL = false;
                return true;
            }
        }

        /// <summary>
        /// CheckAccPayment: validates payer settlement account with key calculation.
        /// VB6: Function CheckAccPayment() (lines 7092–7137).
        /// </summary>
        private bool CheckAccPayment()
        {
            try
            {
                if (!txtPayerAccount.Enabled)
                    return true;

                if (!CheckAndSplitAccount())
                    return false;

                if (m_bIncludeKey)
                {
                    if (!PrepareAccount())
                        return false;
                }

                if (txtCheckSum.Enabled && !m_bIncludeKey)
                {
                    string savedType = m_strCheckType;
                    if (!PrepareAccount())
                        return false;
                    if (savedType == "ключ сформирован автоматически")
                    {
                        m_strCheckType = savedType;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return false;
            }
        }

        /// <summary>
        /// CheckAndSplitAccount: calls UtCheckAndSplitAccount channel to validate and split payer account.
        /// VB6: Function CheckAndSplitAccount() (lines 8944–9026).
        /// </summary>
        private bool CheckAndSplitAccount()
        {
            try
            {
                if (string.Equals(m_strSignAccCode, "отсутствует", StringComparison.Ordinal))
                    return true;

                if (txtPayerAccount.Text.Trim().Length == 0
                    || txtPayerAccount.Text.Trim().Length < txtPayerAccount.MaxLength)
                {
                    tabPayment.SelectedTab = tabPageGeneral;
                    MessageBox.Show(string.Format(MsgCheckAccountBadLength, txtPayerAccount.MaxLength),
                        CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPayerAccount.Focus();
                    return false;
                }

                string accCode = cmbCityCode.Enabled
                    ? cmbCityCode.Text + txtPayerAccount.Text.Trim()
                    : txtPayerAccount.Text.Trim();

                this.IUbsChannel.ParamIn("mSumma", udcPaymentAmount.DecimalValue);
                this.IUbsChannel.ParamIn("AccCode", accCode);
                this.IUbsChannel.ParamIn("nLenKey", m_nLenKey);
                this.IUbsChannel.ParamIn("strSignKey", m_strSignKey);
                this.IUbsChannel.ParamIn("strNameProcAcc", m_strNameProcAcc);
                this.IUbsChannel.ParamIn("IDKINDPAYMENT", m_idKindPaym);

                this.IUbsChannel.Run("UtCheckAndSplitAccount");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                if (!paramOut.GetParamOutBool("bRetVal"))
                {
                    int errNum = paramOut.GetParamOutInt("ErrNum");
                    string errMsg = paramOut.GetParamOutString("StrError");

                    m_strAccCode = paramOut.GetParamOutString("AccCode");
                    m_strCheckSum = paramOut.GetParamOutString("CheckSum");

                    if (errNum != 0)
                    {
                        uciInfo.Show(errMsg);
                    }
                    else
                    {
                        tabPayment.SelectedTab = tabPageGeneral;
                        MessageBox.Show(errMsg, CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPayerAccount.Focus();
                    }

                    return false;
                }

                if (paramOut.Contains("StrQuestion"))
                {
                    if (MessageBox.Show(paramOut.GetParamOutString("StrQuestion"), "Внимание!",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        return false;
                    }
                }

                m_strAccCode = paramOut.GetParamOutString("AccCode");
                m_strCheckSum = paramOut.GetParamOutString("CheckSum");

                if (m_idGroupIncoming == 0)
                {
                    GetDataClientFromLic();
                }

                return true;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return false;
            }
        }

        /// <summary>
        /// PrepareAccount: validates/calculates payer account key via CalcKey channel.
        /// VB6: Function PrepareAccount() (lines 9031–9135).
        /// </summary>
        private bool PrepareAccount()
        {
            try
            {
                if (string.Equals(m_strSignKey, "отсутствует", StringComparison.Ordinal))
                {
                    m_strCheckSum = string.Empty;
                    m_strCheckType = string.Empty;
                    return true;
                }

                if (string.Equals(m_strSignKey, "отдельно от счета", StringComparison.Ordinal)
                    && txtCheckSum.Text.Trim().Length == 0)
                {
                    string calcedKey;
                    if (!CalcKey(out calcedKey)) return false;
                    m_strCheckSum = calcedKey;
                    m_strCheckType = "ключ сформирован автоматически";
                    txtCheckSum.Text = calcedKey;
                    return true;
                }

                string key;
                if (!CalcKey(out key)) return false;

                if (!m_bIsCheckKey)
                {
                    m_strCheckSum = txtCheckSum.Text;
                    return true;
                }

                if (m_bIncludeKey)
                {
                    if (m_strCheckSum == key)
                    {
                        m_strCheckType = "ключ набран операционистом и совпадает с расч. ключом";
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        if (!m_isErrorKey)
                        {
                            tabPayment.SelectedTab = tabPageGeneral;
                            MessageBox.Show(string.Format(MsgCheckKeyError, key),
                                CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPayerAccount.Focus();
                            return false;
                        }

                        if (MessageBox.Show(string.Format(MsgCheckKeySaveWrongKey, key),
                            CaptionForm, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            tabPayment.SelectedTab = tabPageGeneral;
                            txtPayerAccount.Focus();
                            return false;
                        }

                        m_strCheckType = "ключ набран операционистом и не совпад. с расч. ключом";
                        btnSave.Enabled = true;
                    }
                }
                else
                {
                    if (txtCheckSum.Text == key)
                    {
                        m_strCheckSum = key;
                        m_strCheckType = "ключ набран операционистом и совпадает с расч. ключом";
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        if (!m_isErrorKey)
                        {
                            tabPayment.SelectedTab = tabPageGeneral;
                            MessageBox.Show(string.Format(MsgCheckKeyError, key),
                                CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtCheckSum.Focus();
                            return false;
                        }

                        if (MessageBox.Show(string.Format(MsgCheckKeySaveWrongKey, key),
                            CaptionForm, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            tabPayment.SelectedTab = tabPageGeneral;
                            txtCheckSum.Focus();
                            return false;
                        }

                        m_strCheckSum = txtCheckSum.Text;
                        m_strCheckType = "ключ набран операционистом и не совпад. с расч. ключом";
                        btnSave.Enabled = true;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return false;
            }
        }

        private bool CalcKey(out string key)
        {
            key = string.Empty;
            try
            {
                this.IUbsChannel.ParamIn("AccCode", m_strAccCode);
                this.IUbsChannel.ParamIn("mSumma", udcPaymentAmount.DecimalValue);
                this.IUbsChannel.ParamIn("IDKINDPAYMENT", m_idKindPaym);
                this.IUbsChannel.Run("CalcKey");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                if (!paramOut.GetParamOutBool("bRetVal"))
                {
                    string err = paramOut.GetParamOutString("StrError");
                    if (err.Length > 0)
                    {
                        MessageBox.Show(err, CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return false;
                }

                key = paramOut.GetParamOutString("Key");
                return true;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return false;
            }
        }

        private void GetDataClientFromLic()
        {
            try
            {
                this.IUbsChannel.ParamIn("AccCode", m_strAccCode);
                this.IUbsChannel.Run("GetDataClietFromLic");
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        /// <summary>
        /// CheckKeyInn: validates INN checksum for 10-digit and 12-digit values.
        /// VB6: Private Function CheckKeyInn(inn As String) (lines 5608–5678).
        /// </summary>
        private static bool CheckKeyInn(string inn)
        {
            if (inn == null) return false;

            if (inn.Length == 10)
            {
                if (inn.Substring(0, 2).ToLower() == "00")
                    return true;

                int[] w = { 2, 4, 10, 3, 5, 9, 4, 6, 8 };
                int sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    int d;
                    if (!int.TryParse(inn.Substring(i, 1), out d)) return false;
                    sum += w[i] * d;
                }
                int check = (sum % 11) % 10;
                int last;
                if (!int.TryParse(inn.Substring(9, 1), out last)) return false;
                return check == last;
            }

            if (inn.Length == 12)
            {
                if (inn.Substring(0, 2).ToLower() == "00")
                    return true;

                int[] w11 = { 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };
                int sum1 = 0;
                for (int i = 0; i < 10; i++)
                {
                    int d;
                    if (!int.TryParse(inn.Substring(i, 1), out d)) return false;
                    sum1 += w11[i] * d;
                }
                int check1 = (sum1 % 11) % 10;
                int d11;
                if (!int.TryParse(inn.Substring(10, 1), out d11)) return false;
                if (check1 != d11) return false;

                int[] w12 = { 3, 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };
                int sum2 = 0;
                for (int i = 0; i < 11; i++)
                {
                    int d;
                    if (!int.TryParse(inn.Substring(i, 1), out d)) return false;
                    sum2 += w12[i] * d;
                }
                int check2 = (sum2 % 11) % 10;
                int d12;
                if (!int.TryParse(inn.Substring(11, 1), out d12)) return false;
                return check2 == d12;
            }

            if (inn.Length == 5)
                return true;

            return false;
        }

        /// <summary>
        /// CheckSumControl: PS_GetSummaControl — checks if payment amount requires extra control.
        /// VB6: lines 4071–4098 in btnSave_Click.
        /// </summary>
        private bool CheckSumControl()
        {
            try
            {
                m_isControlSum = false;

                this.IUbsChannel.ParamIn("BicExtBank", txtRecipientBik.Text);
                this.IUbsChannel.ParamIn("Account_R", ucaRecipientAccount.Text);
                this.IUbsChannel.Run("PS_GetSummaControl");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                if (paramOut.GetParamOutBool("Внутренний документ"))
                {
                    m_isControlSum = false;
                    return true;
                }

                decimal sumControl = paramOut.GetParamOutDecimal("SumControl");
                if (sumControl == 0)
                {
                    m_isControlSum = false;
                }
                else if (udcPaymentAmount.DecimalValue > sumControl)
                {
                    MessageBox.Show(MsgSumControlExceeded, CaptionPaymentAccept,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_isControlSum = true;
                }
                else
                {
                    m_isControlSum = false;
                }

                return true;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return true;
            }
        }

        private void GetPaymentState()
        {
            try
            {
                this.IUbsChannel.ParamIn("IsCashier", m_isCashier);
                this.IUbsChannel.ParamIn("IdContract", m_idContract);
                this.IUbsChannel.Run("PS_GetState");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                this.UbsChannel_ParamIn("State", paramOut.GetParamOutString("State"));
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        // CalcSumCommiss and CalcSumNDS are in UbsPsUtPaymentFrm.Commission.cs

        private bool CheckStateFR()
        {
            if (m_objDevice == null) return false;

            try
            {
                this.IUbsChannel.ParamIn("objDevice", m_objDevice);
                this.IUbsChannel.Run("CheckStateFR");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                return paramOut.GetParamOutBool("bRetVal");
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Builds payment/contract arrays, opens FrmCashOrd, handles auto-execute vs preview.
        /// VB6: CreateCashOrd (lines 8390-8548).
        /// </summary>
        private void CreateCashOrd()
        {
            try
            {
                this.IUbsChannel.ParamIn("IdContract", m_idContract);
                this.IUbsChannel.Run("Ps_PreparePlatDoc");

                var paramOutPlat = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                bool blnPlatDoc = paramOutPlat.GetParamOutBool("blnPeparePlatDoc");

                this.IUbsChannel.Run("Ps_GetStatePrepareCashOrd2");

                var paramOutState = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                int intRegim = paramOutState.GetParamOutInt("StateCashOrd");
                int intRegimRate = paramOutState.GetParamOutInt("StateCashRate");

                if (intRegim == 0 && intRegimRate == 0 && !blnPlatDoc)
                    return;

                this.IUbsChannel.Run("UtGetGlobalUserData");
                var paramOutUser = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                int lngIdUser = paramOutUser.GetParamOutInt("IDUSER");
                int idDivision = paramOutUser.GetParamOutInt("Division");

                this.IUbsChannel.ParamIn("NameSection", "Оплата услуг");
                this.IUbsChannel.Run("Ps_GetStateRequestFormCashOrd");

                var paramOutReq = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                string reqErr = paramOutReq.GetParamOutString("StrError");
                int intReq = 0;
                if (reqErr.Length == 0)
                {
                    intReq = paramOutReq.GetParamOutInt("ISFRMPREVIEW");
                }

                object[,] varPaym;
                object[,] varContr;

                if (m_isGroup || m_idContractSecond != 0)
                {
                    string channelName = (m_idContractSecond != 0)
                        ? "Ps_GetArrayPrepareCashOrdSecond"
                        : "Ps_GetArrayPrepareCashOrd";

                    this.IUbsChannel.Run(channelName);

                    var paramOutArr = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                    varPaym = paramOutArr.Value("VARPAYMENTS") as object[,];
                    varContr = paramOutArr.Value("VARCONTRACT") as object[,];
                }
                else
                {
                    varPaym = new object[1, 14];
                    varPaym[0, 0] = m_idPayment;
                    varPaym[0, 1] = m_idContract;
                    varPaym[0, 2] = udcAmountWithRate.DecimalValue;
                    varPaym[0, 3] = udcPayerRateAmount.DecimalValue;
                    varPaym[0, 4] = m_curSumNDSSend;
                    varPaym[0, 5] = txtPayerFullName.Text;

                    object[,] varCashSymbol = new object[1, 3];
                    varCashSymbol[0, 0] = txtCashSymbolPayment.Text;
                    varCashSymbol[0, 1] = txtCashSymbolCommission.Text;
                    varCashSymbol[0, 2] = txtCashSymbolNds.Text;
                    varPaym[0, 6] = varCashSymbol;

                    varPaym[0, 7] = idDivision;
                    if (m_isCashier)
                    {
                        varPaym[0, 8] = lngIdUser;
                    }
                    varPaym[0, 9] = m_curSumNDSPaym;
                    varPaym[0, 10] = udcPaymentAmount.DecimalValue;
                    varPaym[0, 11] = ucaPayerAccount.Text;
                    varPaym[0, 12] = m_curSumRateRec;
                    varPaym[0, 13] = m_curSumNDSRec;

                    varContr = new object[1, 2];
                    varContr[0, 0] = m_idContract;
                    varContr[0, 1] = txtContractCode.Text;
                }

                using (var frmCashOrd = new FrmCashOrd())
                {
                    frmCashOrd.UbsChannelRef = this.IUbsChannel;
                    frmCashOrd.PaymentsData = varPaym;
                    frmCashOrd.ContractsData = varContr;
                    frmCashOrd.PaymentId = m_idPayment;
                    frmCashOrd.AutoExecute = (intReq != 1);

                    frmCashOrd.ShowDialog(this);
                    m_isCreateCashOrd = frmCashOrd.WasCreated;
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region NewRecord / NewRecordCalc

        /// <summary>
        /// NewRecord: clears form for next payment entry after save.
        /// VB6: Function NewRecord() (lines 3730–3805).
        /// </summary>
        private void NewRecord()
        {
            try
            {
                btnSave.Enabled = true;
                chkThirdPerson.Enabled = false;
                chkThirdPerson.Checked = false;
                btnCashSymb.Enabled = true;

                this.UbsChannel_ParamIn("StrCommand", StrCommandClear);
                this.UbsChannel_Run("PAYMENT");

                cmbPurpose.Items.Clear();
                cmbPurpose.Text = string.Empty;

                m_idPayment = 0;
                m_isForward = true;
                m_isNoMessage = false;

                InitDoc();

                if (m_idGroupIncoming == 0 && !m_isAddClient)
                {
                    ClearRecipientFields();
                }
                ClearRecipientFieldsLocal();

                if (txtPayerFullName.Enabled)
                {
                    txtPayerFullName.Focus();
                }

                m_arrCashSymb = null;

                IsAutoPeriod();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        /// <summary>
        /// NewRecordCalc: lighter clear for calc-triggered new record.
        /// VB6: Function NewRecordCalc() (lines 3800–3805).
        /// </summary>
        private void NewRecordCalc()
        {
            btnSave.Enabled = true;
            btnCashSymb.Enabled = true;

            m_idPayment = 0;

            ClearRecipientFieldsLocal();
            txtPayerInn.Text = string.Empty;
            txtPayerFullName.Text = string.Empty;
            txtPayerAddress.Text = string.Empty;

            if (ucaPayerAccount.Visible)
            {
                ucaPayerAccount.Text = "00000000000000000000";
            }
            txtPayerClientInfo.Text = string.Empty;

            if (txtPayerFullName.Enabled)
            {
                txtPayerFullName.Focus();
            }
        }

        private void ClearRecipientFields()
        {
            txtContractCode.Text = string.Empty;
            txtRecipientComment.Text = string.Empty;
            txtRecipientBik.Text = string.Empty;
            ucaRecipientCorrAccount.Text = string.Empty;
            txtRecipientBankName.Text = string.Empty;
            ucaRecipientAccount.Text = string.Empty;
            txtRecipientInn.Text = string.Empty;
            txtRecipientKpp.Text = string.Empty;
            txtRecipientName.Text = string.Empty;
            cmbPurpose.Items.Clear();
            cmbPurpose.Text = string.Empty;
        }

        private void ClearRecipientFieldsLocal()
        {
            txtRecipientNote.Text = string.Empty;
        }

        #endregion

        #region Form_Closing save guard

        /// <summary>
        /// CanCloseForm: prevents close while save is in progress.
        /// Also handles incoming-group CheckOrEndGroup on exit.
        /// VB6: btnExit_Click lines 3708–3726.
        /// </summary>
        private bool CanCloseForm()
        {
            if (m_isSave)
                return false;

            if (m_idGroupIncoming > 0)
            {
                try
                {
                    this.IUbsChannel.ParamIn("IdGroupIncoming", m_idGroupIncoming);
                    this.IUbsChannel.ParamIn("curCommonSumma", udcCommonAmount.DecimalValue);
                    this.IUbsChannel.Run("CheckOrEndGroup");

                    var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                    int type = paramOut.GetParamOutInt("Type");

                    if (type == 3)
                    {
                        string msg = string.Format(MsgGroupSumUnder,
                            udcCommonAmount.DecimalValue, paramOut.GetParamOutString("PaymentGroupSum"));
                        if (MessageBox.Show(msg, CaptionCheckSumPayment,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex) { this.Ubs_ShowError(ex); }
            }

            return true;
        }

        #endregion

        #region UI helpers

        private void SetButtonsEnabled(bool enabled)
        {
            btnSave.Enabled = enabled;
            btnExit.Enabled = enabled;
        }

        private void RestoreSaveUiState()
        {
            m_isSave = false;
            btnExit.Enabled = true;
            btnSave.Enabled = true;
        }

        #endregion
    }
}
