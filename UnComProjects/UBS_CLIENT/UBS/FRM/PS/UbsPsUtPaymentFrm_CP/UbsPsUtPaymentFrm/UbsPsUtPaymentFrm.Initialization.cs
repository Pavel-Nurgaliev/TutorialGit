using System;
using System.Net.Sockets;
using System.Windows.Forms;
using UbsControl;
using UbsService;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace UbsBusiness
{
    public partial class UbsPsUtPaymentFrm
    {
        private object[,] m_arrCashSymb;
        private object[,] m_arrDataTypeCashSymbol;
        private string m_strAccCode;
        private string m_strCheckSum;
        private string m_strCheckType;
        private int m_idKindPaym;
        private bool m_formReestToDocs;
        private string m_SIDFilter;
        private object[] m_arrKbkForCheck;
        private object[] m_arrKbkForCheckY;
        private int m_idContractSecond;
        private object m_arrCashSymbSecond;
        private bool m_isLetter;
        private object[,] m_arrSubContracts;
        private bool m_isEditRecip;

        /// <summary>
        /// InitDoc: сначала устанавливаются настройки из ListKey, потом инициализация полей формы в специальных режимах (VIEW/COPY/CHANGE_PART/ADD).
        /// </summary>
        private bool InitDoc()
        {
            try
            {
                m_idContractOld = 0;
                m_isLic = false;

                txtRecipientName.Enabled = false;
                m_isAlready = false;
                chkThirdPerson.Enabled = false;
                chkThirdPerson.Checked = false;
                tabPageAddFields.Enabled = false;
                btnCashSymb.Visible = false;
                txtPaymentCode.Visible = false;
                lblPaymentCode.Visible = false;

                if (string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal))
                {
                    linkFindFilter.Visible = false;
                }

                txtCheckSum.Visible = false;
                cmbCityCode.Visible = false;
                cmbCityCode.Enabled = false;
                tabPageTariff.Hide();
                tabPageTelephone.Hide();
                tabPageTax.Hide();
                tabPageThirdPerson.Hide();
                m_numTabAddFl = 5;

                m_isSave = false;
                m_isClickSave = false;

                this.IUbsChannel.Run("FormStart");

                base.UbsInit();

                // --- Cashier check for ADD commands ---
                if (string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal)
                    || string.Equals(m_commandSource, StrCommandAddFromClient, StringComparison.Ordinal)
                    || string.Equals(m_commandSource, StrCommandAddParam, StringComparison.Ordinal))
                {
                    if (m_foSettingValue > -1)
                    {
                        this.IUbsChannel.ParamIn("FROMFO", m_foSettingValue);
                    }

                    this.IUbsChannel.Run("PS_UserIsCashier");

                    var paramOutCashier = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                    string cashierErr = paramOutCashier.GetParamOutString("StrError");
                    if (cashierErr.Length > 0)
                    {
                        m_isSave = false;
                        MessageBox.Show(cashierErr, CaptionPaymentAccept,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    if (paramOutCashier.GetParamOutBool("NotCashier"))
                    {
                        m_isSave = false;
                        MessageBox.Show(MsgNotCashier, CaptionPaymentAccept,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                        this.btnExit_Click(this, EventArgs.Empty);

                        return false;
                    }

                    m_isCashier = true;
                    if (paramOutCashier.GetParamOutBool("bRet"))
                    {
                        m_isCashier = false;
                    }
                }

                // Card number visibility
                if (m_isAddClient)
                {
                    txtPayerCardNumber.Visible = false;
                    lblPayerCardNumber.Visible = false;
                }
                else
                {
                    txtPayerCardNumber.Visible = true;
                    lblPayerCardNumber.Visible = true;
                }

                // --- Run InitForm ---
                this.UbsChannel_ParamIn("StrCommand", m_command);
                this.UbsChannel_ParamIn("IdPayment", m_idPayment);
                this.UbsChannel_ParamIn("IdMainIncoming", m_idMainIncoming);
                this.UbsChannel_Run("InitForm");

                var paramOutInitForm = new UbsParamCustom(this.UbsChannel_ParamsOut);

                m_checkPayer = paramOutInitForm.GetParamOutString("Клиент.Проверки по справочникам");

                if (paramOutInitForm.Contains("ChangeCommand"))
                {
                    MessageBox.Show(paramOutInitForm.GetParamOutString("StrErrorCh"), CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    m_command = StrCommandView;
                }

                if (paramOutInitForm.Contains("CloseForm"))
                {
                    string strError = paramOutInitForm.GetParamOutString("StrError");
                    string msg = (strError.Length == 0) ? MsgPaymentBlocked : strError;
                    string cap = (strError.Length == 0) ? CaptionBlockCheck : CaptionCheck;
                    MessageBox.Show(msg, cap, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.btnExit_Click(this, EventArgs.Empty);

                    return false;
                }

                if (string.Equals(m_commandSource, StrCommandChangePartIncoming, StringComparison.Ordinal))
                {
                    m_isEndGroup = paramOutInitForm.GetParamOutBool("EndGroup");
                    m_command = m_isEndGroup ? StrCommandView : StrCommandChangePart;
                }

                bool bRetVal = paramOutInitForm.GetParamOutBool("bRetVal");
                bool bMsgBoxYesNo = paramOutInitForm.GetParamOutBool("bMsgBoxYesNo");
                if (bRetVal != bMsgBoxYesNo)
                {
                    FillCityCode(paramOutInitForm.Contains("arrCityCode")
                        ? paramOutInitForm.Value("arrCityCode") : null);

                    m_dateBeg = paramOutInitForm.GetParamOutDateTime("DateBeg");
                    m_dateEnd = paramOutInitForm.GetParamOutDateTime("DateEnd");

                    m_isErrorKey = paramOutInitForm.GetParamOutBool("blnIsErrorKey");
                }
                else
                {
                    if (!bMsgBoxYesNo)
                    {
                        string strError = paramOutInitForm.GetParamOutString("StrError");
                        MessageBox.Show(strError, CaptionInitForm + ". " + CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (paramOutInitForm.Contains("Льготный клиент"))
                {
                    if (paramOutInitForm.GetParamOutBool("Льготный клиент"))
                    {
                        chkBenefits.Checked = true;
                    }
                }
                if (paramOutInitForm.Contains("Обоснование"))
                {
                    txtBenefitReason.Text = paramOutInitForm.GetParamOutString("Обоснование");
                }

                ucfAddProperties.Refresh();

                // Print form checkbox
                chkPrintForms.Checked = false;
                chkPrintForms.Visible = false;
                if (string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal))
                {
                    if (paramOutInitForm.GetParamOutBool("ViewPrintForm"))
                    {
                        chkPrintForms.Visible = true;
                        if (m_isIsFrmPrn)
                        {
                            chkPrintForms.Checked = true;
                        }
                    }
                }

                if (paramOutInitForm.GetParamOutInt("DocumentsExists") == 1)
                {
                    m_command = StrCommandView;
                    uciInfo.Show(MsgDocumentsExistViewOnly);
                    uciInfo.Show();
                }

                this.IUbsChannel.Run("UtReadSettingEnterCashSymbol");

                var paramOutUtReadSettingEnterCashSymbol = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                if (!paramOutInitForm.GetParamOutBool("bRetVal") && paramOutInitForm.GetParamOutString("strError").Length > 0)
                {
                    MessageBox.Show(paramOutInitForm.GetParamOutString("strError"), CaptionInitForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int checkKSSymb = paramOutInitForm.GetParamOutInt("UtEnterCashSymbol");
                    bool ksVisible = (checkKSSymb != 0);
                    txtCashSymbolPayment.Visible = ksVisible;
                    txtCashSymbolCommission.Visible = ksVisible;
                    txtCashSymbolNds.Visible = ksVisible;
                    m_isRegimCashSymb = ksVisible;
                }

                // --- Setting: choice client (guest mode) ---
                if (m_isAddClient)
                {
                    m_isGuest = false;
                }
                else
                {
                    this.IUbsChannel.Run("UtReadSettingChoiceClient");
                    if (!paramOutInitForm.GetParamOutBool("bRetVal") && paramOutInitForm.GetParamOutString("strError").Length > 0)
                    {
                        MessageBox.Show(paramOutInitForm.GetParamOutString("strError"), CaptionInitForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        m_isGuest = (paramOutInitForm.GetParamOutInt("ChoiceClient") == 0);
                    }
                }

                // --- Setting: source means ---
                this.IUbsChannel.Run("UtReadSettingSourceMeans");

                var paramOutUtReadSettingSourceMeans = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                if (!paramOutInitForm.GetParamOutBool("bRetVal") && paramOutInitForm.GetParamOutString("strError").Length > 0)
                {
                    MessageBox.Show(paramOutInitForm.GetParamOutString("strError"), CaptionInitForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    bool showSourceMeans = (paramOutUtReadSettingSourceMeans.GetParamOutInt("UtEnterSourceMeans") == 1)
                        || m_calledFromFrontOffice;
                    m_isSourceMeansVisible = showSourceMeans;
                    m_isSourceMeans = showSourceMeans;
                }

                // --- Command-specific branch ---
                if (string.Equals(m_command, StrCommandView, StringComparison.Ordinal))
                {
                    InitDoc_View();
                }
                else if (string.Equals(m_command, StrCommandCopy, StringComparison.Ordinal))
                {
                    InitDoc_Copy();
                }
                else if (string.Equals(m_command, StrCommandChangePart, StringComparison.Ordinal))
                {
                    InitDoc_ChangePart(paramOutUtReadSettingSourceMeans);
                }
                else if (string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal))
                {
                    InitDoc_Add();
                }

                // Incoming group → disable payer fields
                if (m_idGroupIncoming > 0)
                {
                    txtPayerFullName.Enabled = false;
                    linkPayerFullName.Enabled = false;
                    txtPayerInn.Enabled = false;
                    txtPayerAddress.Enabled = false;
                    txtPayerCardNumber.Enabled = false;
                }

                // Period dates
                if (m_dateBeg != DateTime.MinValue)
                {
                    txtPeriodDayBeg.Text = m_dateBeg.Day.ToString();
                    txtPeriodMonthBeg.Text = m_dateBeg.Month.ToString();
                    txtPeriodYearBeg.Text = (m_dateBeg.Year % 100).ToString();
                }
                if (m_dateEnd != DateTime.MinValue)
                {
                    txtPeriodDayEnd.Text = m_dateEnd.Day.ToString();
                    txtPeriodMonthEnd.Text = m_dateEnd.Month.ToString();
                    txtPeriodYearEnd.Text = (m_dateEnd.Year % 100).ToString();
                }

                CheckPeni();
                CheckPayer(false);
                DefineRunUserForm(false);

                return true;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);

                return false;
            }
        }

        private void InitDoc_View()
        {
            btnCashSymb.Enabled = false;
            ReadContract();
            DisableAllFields();
            ucfAddProperties.Refresh();
        }

        private void InitDoc_Copy()
        {
            EnableAllFields();
            ReadContract();
            ucfAddProperties.Refresh();
        }

        private void InitDoc_ChangePart(UbsParamCustom paramOut)
        {
            if (paramOut.Contains("ListSymbols"))
            {
                m_arrCashSymb = paramOut.Value("ListSymbols") as object[,];
            }
            if (paramOut.Contains("arrTypeCashSymbol"))
            {
                m_arrDataTypeCashSymbol = paramOut.Value("arrTypeCashSymbol") as object[,];
            }

            ReadContract();

            txtContractCode.Enabled = false;
            udcPaymentAmount.Enabled = false;
            udcPenaltyAmount.Enabled = false;

            if (txtCashSymbolPayment.Text.Length > 0)
            {
                txtCashSymbolPayment.Enabled = true;
            }
            else
            {
                lblCashSymbolPayment.Visible = false;
                txtCashSymbolPayment.Visible = false;
                m_isRegimCashSymb = false;
            }

            if (txtCashSymbolCommission.Text.Length > 0)
            {
                txtCashSymbolCommission.Enabled = true;
            }
            else
            {
                lblCashSymbolCommission.Visible = false;
                txtCashSymbolCommission.Visible = false;
            }

            if (txtCashSymbolNds.Text.Length > 0)
            {
                txtCashSymbolNds.Enabled = true;
            }
            else
            {
                lblCashSymbolCommission.Visible = false;
                txtCashSymbolNds.Visible = false;
            }

            ucfAddProperties.Refresh();
            txtPeriodDayBeg.Enabled = true;
            txtPeriodDayEnd.Enabled = true;
        }

        private void InitDoc_Add()
        {
            if (m_idGroupIncoming > 0)
            {
                GetIdClientFromGroupPayment();
            }

            if (m_isAddClient || m_idGroupIncoming > 0)
            {
                this.IUbsChannel.ParamIn("IDCLIENT", m_idClient);
                this.IUbsChannel.ParamIn("IsGuest",
                    (m_idGroupIncoming > 0) ? (object)m_isGuest : (object)false);
                this.IUbsChannel.Run("ReadClientFromIdOC");

                var paramOutReadClientFromIdOC = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                string readErr = paramOutReadClientFromIdOC.GetParamOutString("StrError");
                if (readErr.Length > 0)
                {
                    MessageBox.Show(readErr, "ReadClientFromIdOC " + CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                txtPayerFullName.Text = paramOutReadClientFromIdOC.GetParamOutString("NAME");
                txtPayerAddress.Text = paramOutReadClientFromIdOC.GetParamOutString("ADRESS");
                txtPayerInn.Text = paramOutReadClientFromIdOC.GetParamOutString("INN");
                txtPayerClientInfo.Text = paramOutReadClientFromIdOC.GetParamOutString("InfoClient");
            }
            else
            {
                txtPayerFullName.Text = string.Empty;
                txtPayerAddress.Text = string.Empty;
                txtPayerInn.Text = string.Empty;
                txtPayerClientInfo.Text = string.Empty;
            }

            txtContractCode.Text = string.Empty;
            txtPaymentCode.Text = string.Empty;
            txtRecipientComment.Text = string.Empty;
            txtRecipientBik.Text = string.Empty;
            txtRecipientInn.Text = string.Empty;
            ucaRecipientCorrAccount.Text = string.Empty;
            txtRecipientBankName.Text = string.Empty;
            ucaRecipientAccount.Text = string.Empty;
            cmbPurpose.Items.Clear();
            cmbPurpose.Text = string.Empty;
            txtCheckSum.Text = string.Empty;

            EnableAllFields();
        }

        /// <summary>
        /// ReadContract Прочитать договор. Читает платеж, заполняет контролы значениями и устанавливает состояния
        /// </summary>
        private void ReadContract()
        {
            try
            {
                if (m_idPayment <= 0)
                {
                    return;
                }

                this.IUbsChannel.ParamIn("StrCommand", StrCommandRead);
                this.IUbsChannel.ParamIn("IdPaym", m_idPayment);

                if (m_isGroup)
                {
                    this.IUbsChannel.ParamIn("PAYMENTGROUP", true);
                }

                if (string.Equals(m_command, StrCommandCopy, StringComparison.Ordinal))
                {
                    this.IUbsChannel.ParamIn("ClearPayment", true);
                }
                else
                {
                    txtCashSymbolPayment.Enabled = false;
                    txtCashSymbolCommission.Enabled = false;
                    txtCashSymbolNds.Enabled = false;
                }

                this.IUbsChannel.Run("Payment");

                var paramOutPayment = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                // Cash symbols
                if (paramOutPayment.Contains("CashSymbol"))
                {
                    object varCS = paramOutPayment.Value("CashSymbol");
                    if (varCS is object[,])
                    {
                        object[,] cs = (object[,])varCS;
                        txtCashSymbolPayment.Text = Convert.ToString(cs[0, 0]);
                        txtCashSymbolCommission.Text = Convert.ToString(cs[0, 1]);
                        txtCashSymbolNds.Text = Convert.ToString(cs[0, 2]);
                    }
                }

                if (paramOutPayment.Contains("ArrayCashSymbols"))
                {
                    object arrCS = paramOutPayment.Value("ArrayCashSymbols");
                    if (arrCS is Array)
                    {
                        lblCashSymbolPayment.Visible = false;
                        txtCashSymbolPayment.Visible = false;
                        m_isRegimCashSymb = false;
                    }
                }

                m_isPenyPresent = paramOutPayment.GetParamOutBool("PeniPresent");
                m_isComissPeniPayer = paramOutPayment.GetParamOutBool("PeniComissPayer");
                CheckPeni(paramOutPayment.GetParamOutString("SUMMAPENI"));

                if (m_isGroup)
                {
                    m_idGroup = paramOutPayment.GetParamOutInt("PAYMENTGROUPID");
                    UpdateGroupInfo();
                }

                // Payer / recipient data
                txtPayerFullName.Text = paramOutPayment.GetParamOutString("FIOSend");
                txtPayerInn.Text = paramOutPayment.GetParamOutString("PayerINN1");
                txtPayerAddress.Text = paramOutPayment.GetParamOutString("AdressSend");
                txtContractCode.Text = paramOutPayment.GetParamOutString("Code");
                txtPaymentCode.Text = paramOutPayment.GetParamOutString("CodePayment");
                txtRecipientComment.Text = paramOutPayment.GetParamOutString("Comment");
                txtRecipientBik.Text = paramOutPayment.GetParamOutString("BIC");
                ucaRecipientCorrAccount.Text = paramOutPayment.GetParamOutString("AccCorr");
                txtRecipientBankName.Text = paramOutPayment.GetParamOutString("NameBank");
                txtRecipientInn.Text = paramOutPayment.GetParamOutString("INNRec1");
                ucaRecipientAccount.Text = paramOutPayment.GetParamOutString("AccRec");

                // Third person
                if (paramOutPayment.Contains("THIRDPERSON_NAME"))
                {
                    chkThirdPerson.Checked = true;
                    txtThirdPersonName.Text = paramOutPayment.GetParamOutString("THIRDPERSON_NAME");
                    if (paramOutPayment.Contains("THIRDPERSON_KIND"))
                    {
                        int kindIdx = paramOutPayment.GetParamOutInt("THIRDPERSON_KIND");
                        if (kindIdx >= 0 && kindIdx < cmbThirdPersonKind.Items.Count)
                        {
                            cmbThirdPersonKind.SelectedIndex = kindIdx;
                        }
                    }
                    if (paramOutPayment.Contains("THIRDPERSON_INN"))
                    {
                        txtThirdPersonInn.Text = paramOutPayment.GetParamOutString("THIRDPERSON_INN");
                    }
                    if (paramOutPayment.Contains("THIRDPERSON_KPP"))
                    {
                        txtThirdPersonKpp.Text = paramOutPayment.GetParamOutString("THIRDPERSON_KPP");
                    }

                    chkThirdPerson_CheckedChanged(this, EventArgs.Empty);
                }

                // Purpose / payer info — ADD_PARAM fills only empty
                string strNameRecip;
                if (string.Equals(m_commandSource, StrCommandAddParam, StringComparison.Ordinal))
                {
                    if (cmbPurpose.Text.Length == 0)
                    {
                        cmbPurpose.Text = paramOutPayment.GetParamOutString("Note");
                    }
                    if (ucaPayerAccount.Visible && ucaPayerAccount.Text.Length == 0)
                    {
                        ucaPayerAccount.Text = paramOutPayment.GetParamOutString("AccSend");
                    }
                    if (txtPayerClientInfo.Text.Length == 0)
                    {
                        txtPayerClientInfo.Text = paramOutPayment.GetParamOutString("InfoClient");
                    }
                    strNameRecip = (txtRecipientName.Text.Length == 0)
                        ? paramOutPayment.GetParamOutString("RecipientName")
                        : txtRecipientName.Text;
                }
                else
                {
                    cmbPurpose.Text = paramOutPayment.GetParamOutString("Note");
                    if (ucaPayerAccount.Visible)
                    {
                        ucaPayerAccount.Text = paramOutPayment.GetParamOutString("AccSend");
                    }
                    txtPayerClientInfo.Text = paramOutPayment.GetParamOutString("InfoClient");
                    txtRecipientName.Text = strNameRecip = paramOutPayment.GetParamOutString("RecipientName");
                }

                // Code payment visibility
                string visCode = paramOutPayment.GetParamOutString("VisibleCodePayment");
                bool showCodePayment = string.Equals(visCode, "присутствует", StringComparison.Ordinal);
                txtPaymentCode.Visible = showCodePayment;
                lblPaymentCode.Visible = showCodePayment;

                // Account code
                string strAccCode = paramOutPayment.GetParamOutString("AccCode");
                if (strAccCode.Trim().Length > 0)
                {
                    string includeKey = paramOutPayment.GetParamOutString("IncludeKey");
                    if (string.Equals(includeKey, "входит в счет", StringComparison.Ordinal))
                    {
                        txtSubPaymentCount.Text = strAccCode + paramOutPayment.GetParamOutString("CheckSum");
                    }
                    else
                    {
                        txtSubPaymentCount.Text = strAccCode;
                        string cityCode = paramOutPayment.GetParamOutString("CityCode");
                        for (int i = 0; i < cmbCityCode.Items.Count; i++)
                        {
                            if (string.Equals(cmbCityCode.Items[i].ToString(), cityCode, StringComparison.Ordinal))
                            {
                                cmbCityCode.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }

                txtCheckSum.Text = paramOutPayment.GetParamOutString("CheckSum");
                udcPaymentAmount.Text = paramOutPayment.GetParamOutString("SummaPaym");
                udcPayerRateAmount.Text = paramOutPayment.GetParamOutString("SummaRateSend");

                if (txtRecipientKpp.Text.Trim() == string.Empty)
                {
                    txtRecipientKpp.Text = paramOutPayment.GetParamOutString("КППУ");
                }

                m_curSumRec = paramOutPayment.GetParamOutDecimal("SummaRec");
                m_curSumRateRec = paramOutPayment.GetParamOutDecimal("SummaRateRec");

                udcTotalAmount.Text = paramOutPayment.GetParamOutString("Summa");

                m_dateBeg = paramOutPayment.GetParamOutDateTime("DateBeg");
                m_dateEnd = paramOutPayment.GetParamOutDateTime("DateEnd");

                m_idContract = paramOutPayment.GetParamOutInt("IdContract");
                m_idTariff = paramOutPayment.GetParamOutInt("IdTariff");
                m_idClient = paramOutPayment.GetParamOutInt("IdClient");
                m_idPhone = paramOutPayment.GetParamOutInt("IdPhone");
                m_sidPattern = paramOutPayment.GetParamOutString("IdPattern");

                FindContractbyId();

                txtRecipientName.Text = strNameRecip;

                chkThirdPerson.Enabled = false;
                if (string.Equals(m_sidPattern, PatternEnergy, StringComparison.Ordinal))
                {
                    txtSubPaymentCount.Text = strAccCode;
                    tabPageTax.Show();
                    chkThirdPerson.Enabled = true;
                    if (chkThirdPerson.Checked)
                    {
                        tabPageThirdPerson.Show();
                    }
                }
                else if (string.Equals(m_sidPattern, PatternPhone, StringComparison.Ordinal))
                {
                    cmbCityCode.Visible = true;
                    cmbCityCode.Enabled = true;
                    tabPageTelephone.Show();
                }
                else if (string.Equals(m_sidPattern, PatternNalog, StringComparison.Ordinal))
                {
                    tabPageTax.Show();
                    chkThirdPerson.Enabled = true;
                    if (chkThirdPerson.Checked)
                    {
                        tabPageThirdPerson.Show();
                    }
                }
                else if (string.Equals(m_sidPattern, PatternPhoneAcc, StringComparison.Ordinal))
                {
                    tabPageTelephone.Show();
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        /// <summary>
        /// FindContract найти договор. Находит ид договора по коду договора.
        /// </summary>
        private void FindContract()
        {
            try
            {
                if (txtContractCode.Text.Trim().Length == 0)
                {
                    return;
                }

                if (m_isAlready)
                {
                    return;
                }
                m_isAlready = true;

                this.IUbsChannel.ParamIn("CODECONTRACT", txtContractCode.Text);
                this.IUbsChannel.ParamIn("FROMFO", m_calledFromFrontOffice);
                this.IUbsChannel.Run("ReadContractbyCode");

                var paramOutReadContractbyCode = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                string err = paramOutReadContractbyCode.GetParamOutString("error");
                if (err.Length > 0)
                {
                    MessageBox.Show(err, MsgGettingDataContract,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    m_idContract = paramOutReadContractbyCode.GetParamOutInt("IDCONTRACT");
                    if (m_idContract > 0)
                    {
                        FindContractbyId();
                    }
                }

                m_isAlready = false;
            }
            catch (Exception ex)
            {
                m_isAlready = false;
                this.Ubs_ShowError(ex);
            }
        }

        /// <summary>
        /// FindContractbyId: найти договор. Находи договор по иду договора и заполняет соответствующие поля формы
        /// </summary>
        private void FindContractbyId()
        {
            try
            {
                if (m_idContract <= 0)
                {
                    return;
                }

                if (m_isChangeContract)
                {
                    txtPayerAccount.Text = string.Empty;
                    txtCheckSum.Text = string.Empty;
                    m_strAccCode = string.Empty;
                    m_strCheckSum = string.Empty;
                    m_strCheckType = string.Empty;
                    m_isChangeContract = false;
                }

                chkThirdPerson.Enabled = false;
                tabPageTariff.Hide();
                tabPageTelephone.Hide();
                tabPageTax.Hide();
                tabPageThirdPerson.Hide();
                cmbCityCode.Visible = false;
                cmbCityCode.Enabled = false;
                linkPaymentAccount.Visible = false;

                if (!string.Equals(m_commandSource, StrCommandAddParam, StringComparison.Ordinal))
                {
                    txtRecipientName.Text = string.Empty;
                    txtRecipientComment.Text = string.Empty;
                    txtRecipientBik.Text = string.Empty;
                    ucaRecipientCorrAccount.Text = string.Empty;
                    txtRecipientInn.Text = string.Empty;
                    ucaRecipientAccount.Text = string.Empty;
                    txtRecipientBankName.Text = string.Empty;
                    m_idKindPaym = 0;

                    if (!string.Equals(m_command, StrCommandChangePart, StringComparison.Ordinal)
                        && !string.Equals(m_command, StrCommandView, StringComparison.Ordinal)
                        && !string.Equals(m_command, StrCommandCopy, StringComparison.Ordinal))
                    {
                        cmbPurpose.Text = string.Empty;
                    }
                }

                if (string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal))
                {
                    udcPenaltyAmount.DecimalValue = 0;
                    m_idTariff = 0;
                    m_idPhone = 0;
                }

                this.IUbsChannel.ParamIn("IdContract", m_idContract);

                this.IUbsChannel.Run("UtReadTypePayment");

                var paramOutUtReadTypePayment = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                if (!paramOutUtReadTypePayment.GetParamOutBool("bRetVal"))
                {
                    string typeErr = paramOutUtReadTypePayment.GetParamOutString("StrError");
                    if (typeErr.Length > 0)
                    {
                        MessageBox.Show(typeErr, CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    m_idContract = 0;
                    return;
                }

                if (!m_isGroup)
                {
                    m_arrDataTypeCashSymbol = paramOutUtReadTypePayment.Value("arrTypeCashSymbol") as object[,];
                }

                if (string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal) && m_isRegimCashSymb)
                {
                    txtCashSymbolPayment.Text = paramOutUtReadTypePayment.GetParamOutString("CASHSYM");
                    txtCashSymbolCommission.Text = paramOutUtReadTypePayment.GetParamOutString("CASHSYMRATESEND");
                    txtCashSymbolNds.Text = paramOutUtReadTypePayment.GetParamOutString("CASHSYMNDS");
                }

                btnCashSymb.Visible = (m_arrDataTypeCashSymbol != null);
                lblCashSymbolPayment.Visible =
                txtCashSymbolPayment.Visible =
                m_isRegimCashSymb = !(m_arrDataTypeCashSymbol != null);

                this.IUbsChannel.ParamIn("IdContract", m_idContract);
                this.IUbsChannel.Run("UtReadContract");

                var paramOutUtReadContract = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                if (paramOutUtReadContract.Contains("Формирование реестра к сводным документам"))
                {
                    m_formReestToDocs = paramOutUtReadContract.GetParamOutBool("Формирование реестра к сводным документам");
                }
                else
                {
                    m_formReestToDocs = false;
                }

                if (!paramOutUtReadContract.GetParamOutBool("bRetVal") && paramOutUtReadContract.GetParamOutString("StrError").Length > 0)
                {
                    MessageBox.Show(paramOutUtReadContract.GetParamOutString("StrError"), CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    m_idContract = 0;

                    return;
                }

                m_isLast = paramOutUtReadContract.GetParamOutBool("Данные из предыдущего платежа");

                var comment = paramOutUtReadContract.GetParamOutString("Комментарий");
                if (comment.Length > 0)
                {
                    MessageBox.Show(comment, CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                m_SIDFilter = paramOutUtReadContract.GetParamOutString("Словарь платежей (SID)");

                m_arrKbkForCheck = paramOutUtReadContract.Value("Символы КБК для контроля") as object[];
                m_arrKbkForCheckY = paramOutUtReadContract.Value("Символы КБК для контроля (разрешение)") as object[];

                if (m_isSecondPayment == 0)
                {
                    m_isSecondPayment = 1;
                }

                if (paramOutUtReadContract.Contains("Идентификатор договора (дополнительный платеж)"))
                {
                    m_idContractSecond = paramOutUtReadContract.GetParamOutInt("Идентификатор договора (дополнительный платеж)");
                    m_arrCashSymbSecond = paramOutUtReadContract.Value("Кассовые символа дополнительный платеж");
                }
                else
                {
                    m_idContractSecond = 0;
                }

                m_isLetter = paramOutUtReadContract.GetParamOutBool("blnLetter");

                if (m_isGroup)
                {
                    m_arrSubContracts = paramOutUtReadContract.Value("Субдоговоры") as object[,];

                    if (!CheckSubContracts(paramOutUtReadContract))
                    {
                        MessageBox.Show(paramOutUtReadContract.GetParamOutString("StrError"), CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        m_idContract = 0;
                        return;
                    }
                }

                lblCashSymbolCommission.Visible =
                txtCashSymbolCommission.Visible = (paramOutUtReadContract.GetParamOutInt("RateTypeSend") == 0);

                lblCashSymbolNds.Visible =
                lblCashSymbolNds.Visible = (paramOutUtReadContract.GetParamOutInt("ShowNDS") == 0);

                if (!paramOutUtReadContract.GetParamOutBool("bRetVal") && paramOutUtReadContract.GetParamOutString("StrError").Length > 0)
                {
                    MessageBox.Show(paramOutUtReadContract.GetParamOutString("StrError"), CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    m_idContract = 0;

                    return;
                }

                m_arrRateSend = new object[6];

                m_arrRateSend[0] = paramOutUtReadContract.Contains("RateTypeSend") ? paramOutUtReadContract.Value("RateTypeSend") : 0;
                m_arrRateSend[1] = paramOutUtReadContract.Contains("RatePercentSend") ? paramOutUtReadContract.Value("RatePercentSend") : 0m;
                m_arrRateSend[2] = paramOutUtReadContract.Contains("MinSumSend") ? paramOutUtReadContract.Value("MinSumSend") : 0m;
                m_arrRateSend[3] = paramOutUtReadContract.Contains("MaxSumSend") ? paramOutUtReadContract.Value("MaxSumSend") : 0m;
                m_arrRateSend[4] = paramOutUtReadContract.GetParamOutString("Комиссии с плательщика-признак ставки");
                m_arrRateSend[5] = paramOutUtReadContract.Contains("Комиссии с плательщика-тарифная сетка")
                        ? this.IUbsChannel.ParamOut("Комиссии с плательщика-тарифная сетка") : null;

                bool showCodePayment = string.Equals(paramOutUtReadContract.GetParamOutString("VisibleCodePayment"), "присутствует", StringComparison.Ordinal);

                txtPaymentCode.Visible = showCodePayment;
                txtPaymentCode.Enabled = showCodePayment;
                lblPaymentCode.Visible = showCodePayment;

                m_isPenyPresent = paramOutUtReadContract.GetParamOutBool("PeniPresent");
                m_isComissPeniPayer = paramOutUtReadContract.GetParamOutBool("PeniComissPayer");
                CheckPeni();

                if (string.Equals(m_commandSource, StrCommandAddParam, StringComparison.Ordinal)
                    && txtContractCode.Text.Length == 0
                    && paramOutUtReadContract.GetParamOutString("Code").Length > 0)
                {
                    txtContractCode.Text = paramOutUtReadTypePayment.GetParamOutString("Code");
                }
                else if (!string.Equals(m_commandSource, StrCommandAddParam, StringComparison.Ordinal))
                {
                    txtContractCode.Text = paramOutUtReadTypePayment.GetParamOutString("Code");
                }

                if (string.Equals(m_commandSource, StrCommandAddParam, StringComparison.Ordinal)
                    && txtRecipientComment.Text.Length == 0
                    && paramOutUtReadContract.GetParamOutString("Comment").Length > 0)
                {
                    txtRecipientComment.Text = paramOutUtReadTypePayment.GetParamOutString("Comment");
                }
                else if (!string.Equals(m_commandSource, StrCommandAddParam, StringComparison.Ordinal))
                {
                    txtRecipientComment.Text = paramOutUtReadTypePayment.GetParamOutString("Comment");
                }

                if (string.Equals(m_commandSource, StrCommandAddParam, StringComparison.Ordinal)
                    && txtRecipientBik.Text.Length == 0
                    && paramOutUtReadContract.GetParamOutString("BIC").Length > 0)
                {
                    txtRecipientBik.Text = paramOutUtReadTypePayment.GetParamOutString("BIC");
                }
                else if (!string.Equals(m_commandSource, StrCommandAddParam, StringComparison.Ordinal))
                {
                    txtRecipientBik.Text = paramOutUtReadTypePayment.GetParamOutString("BIC");
                }

                if (string.Equals(m_commandSource, StrCommandAddParam, StringComparison.Ordinal)
                    && ucaRecipientCorrAccount.Text.Length == 0
                    && paramOutUtReadContract.GetParamOutString("CorrAcc").Length > 0)
                {
                    ucaRecipientCorrAccount.Text = paramOutUtReadTypePayment.GetParamOutString("CorrAcc");
                }
                else if (!string.Equals(m_commandSource, StrCommandAddParam, StringComparison.Ordinal))
                {
                    ucaRecipientCorrAccount.Text = paramOutUtReadTypePayment.GetParamOutString("CorrAcc");
                }

                if (string.Equals(m_commandSource, StrCommandAddParam, StringComparison.Ordinal)
                    && txtRecipientInn.Text.Length == 0
                    && paramOutUtReadContract.GetParamOutString("INN").Length > 0)
                {
                    txtRecipientInn.Text = paramOutUtReadTypePayment.GetParamOutString("INN");
                }
                else if (!string.Equals(m_commandSource, StrCommandAddParam, StringComparison.Ordinal))
                {
                    txtRecipientInn.Text = paramOutUtReadTypePayment.GetParamOutString("INN");
                }

                string accFromContract = paramOutUtReadContract.GetParamOutString("Acc");
                if (string.Equals(m_commandSource, StrCommandAddParam, StringComparison.Ordinal)
                    && (ucaRecipientAccount.Text.Length == 0
                        || string.Equals(ucaRecipientAccount.Text, "00000000000000000000", StringComparison.Ordinal))
                    && accFromContract.Length > 0)
                {
                    ucaRecipientAccount.Text = accFromContract;
                }
                else if (!string.Equals(m_commandSource, StrCommandAddParam, StringComparison.Ordinal))
                {
                    ucaRecipientAccount.Text = accFromContract;
                }

                var isSepDoc = paramOutUtReadContract.GetParamOutString("IsSeparateDoc");
                m_sidPattern = paramOutUtReadContract.GetParamOutString("IdPattern");
                m_strAddress = paramOutUtReadContract.GetParamOutString("Adress");

                bool noRecipBik = (paramOutUtReadContract.GetParamOutString("BIC").Length == 0);
                bool noRecipAcc = string.Equals(accFromContract, "00000000000000000000", StringComparison.Ordinal);

                linkRecipientBankName.Enabled = btnSaveRecipientAttribute.Enabled = (m_idContract != 0 && noRecipBik && noRecipAcc);

                bool isArbitrary = (txtRecipientBik.Text.Trim().Length == 0);

                if ((paramOutUtReadContract.GetParamOutInt("IdClient") == 0 || string.Equals(ucaRecipientAccount.Text, "00000000000000000000", StringComparison.Ordinal))
                    && accFromContract.Length > 0)
                {
                    m_isEditRecip = true;
                }

                int contractState = paramOutUtReadContract.GetParamOutInt("State");
                if (contractState == 1)
                {
                    MessageBox.Show(MsgContractClosedWarning, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.None);
                }

                SetPropBank(isSepDoc);

                if (accFromContract == "00000000000000000000")
                {
                    txtRecipientInn.Enabled = true;
                }

                m_idKindPaym = paramOutUtReadContract.GetParamOutInt("IdKindPayment");

                if (string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal))
                {
                    if (string.Equals(m_commandSource, StrCommandAddParam, StringComparison.Ordinal)
                        && cmbPurpose.Text.Length == 0)
                    {
                        object arrPurpose = paramOutUtReadContract.Contains("arrPurpose")
                            ? paramOutUtReadContract.Value("arrPurpose") : null;
                        FillPurpose(arrPurpose);
                    }
                    else if (!string.Equals(m_commandSource, StrCommandAddParam, StringComparison.Ordinal))
                    {
                        object arrPurpose = paramOutUtReadContract.Contains("arrPurpose")
                            ? paramOutUtReadContract.Value("arrPurpose") : null;
                        FillPurpose(arrPurpose);
                    }

                    base.IUbsChannel.Run("UtReadSetupLockPurpose");

                    var paramOutUtReadSetupLockPurpose = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                    if (!paramOutUtReadSetupLockPurpose.GetParamOutBool("bRetVal") && paramOutUtReadSetupLockPurpose.GetParamOutString("StrError").Length > 0)
                    {
                        cmbPurpose.Enabled = paramOutUtReadSetupLockPurpose.GetParamOutBool("blnPurposeLock"); ;
                    }
                }

                GetBankNameACC();

                if (m_idKindPaym != 0)
                {
                    SetParamAccCodeKey(m_idKindPaym);
                }

                tabPayment.SelectedTab = tabPageGeneral;

                if (tabPayment.SelectedIndex == 1 && m_isForward)
                {
                    if (txtRecipientInn.Visible && txtRecipientInn.Enabled && !(isArbitrary || m_isEditRecip))
                    {
                        txtRecipientInn.Focus();
                    }
                    else if (txtRecipientBik.Visible && txtRecipientBik.Enabled && (isArbitrary || m_isEditRecip))
                    {
                        txtRecipientBik.Focus();
                    }
                    else if (cmbPurpose.Visible && cmbPurpose.Enabled)
                    {
                        cmbPurpose.Focus();
                    }
                }

                // params
                paramOutUtReadContract["AccClientPay"] = ucaPayerAccount.Text;
                paramOutUtReadContract["IdClient"] = m_idClient;
                paramOutUtReadContract["INN"] = txtRecipientInn.Text;
                paramOutUtReadContract["Kppu"] = txtRecipientKpp.Text;
                paramOutUtReadContract["AccClient"] = ucaRecipientAccount.Text;
                paramOutUtReadContract["IsGuest"] = m_isGuest;

                if (chkBenefits.Checked)
                {
                    if (ucaPayerAccount.Text == "00000000000000000000")
                    {
                        paramOutUtReadContract["AccClientPay"] = "20200";
                    }

                    this.IUbsChannel.Run("UtCheckPaymentBenefits");

                    var paramOutUtCheckPaymentBenefits = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                    if ((bool)paramOutUtCheckPaymentBenefits["isBenefits"])
                    {
                        m_arrRateSend[0] = 0;

                        if (udcAmountWithRate.DecimalValue > 0)
                        {
                            udcAmountWithRate.DecimalValue = udcPaymentAmount.DecimalValue + udcPenaltyAmount.DecimalValue;
                        }

                        udcPayerRateAmount.DecimalValue = 0;
                    }
                    else
                    {
                        CalcSumCommiss_2();
                    }
                }
                else
                {
                    CalcSumCommiss_2();
                }

                if (m_idContract != 0)
                {
                    this.IUbsChannel.ParamIn("IdContract", m_idContract);
                    this.IUbsChannel.ParamIn("StrCommand", StrCommandChangeContract);
                    this.IUbsChannel.Run("Payment");
                    CheckPeni(paramOutUtReadContract.GetParamOutString("SUMMAPENI"));
                }

                ucfAddProperties.Refresh();

                if (ucfAddProperties.Collection.Count == 0)
                {
                    tabPageAddFields.Hide();
                }
                else
                {
                    tabPageAddFields.Show();
                }

                chkThirdPerson.Enabled = false;
                tabPageTariff.Hide();
                tabPageTelephone.Hide();
                tabPageTax.Hide();
                tabPageThirdPerson.Hide();
                cmbCityCode.Visible = false;
                cmbCityCode.Enabled = false;

                switch (m_sidPattern)
                {
                    case PatternEnergy:
                        tabPageTariff.Show();
                        FillTariff(m_idTariff);

                        if (tabPayment.SelectedTab == tabPageAddFields)
                        {
                            if (m_codeEnergy == 0)
                            {
                                ucfAddProperties.TabIndex = 1;
                            }
                            else
                            {
                                ucfAddProperties.TabIndex = m_codeEnergy;
                            }
                        }

                        break;
                    case PatternPhone:
                        cmbCityCode.Visible = true;
                        cmbCityCode.Enabled = true;
                        tabPageTelephone.Show();
                        FillPhone(m_idPhone);
                        break;
                    case PatternPhoneAcc:
                        tabPageTelephone.Show();
                        FillPhone(m_idPhone);
                        break;
                    case PatternNalog:
                        tabPageTax.Show();
                        chkThirdPerson.Enabled = true;
                        FillNalog(false, 0);
                        break;
                }

                txtRecipientName.Enabled =
                lblRecipientName.Enabled = isArbitrary || m_isEditRecip;

                if (m_idContract != 0 && !string.Equals(m_command, StrCommandView, StringComparison.Ordinal))
                {
                    txtRecipientBik.Enabled = (txtRecipientBik.Text.Length == 0);
                    txtRecipientInn.Enabled = (txtRecipientInn.Text.Length == 0);
                    ucaRecipientAccount.Enabled =
                        string.Equals(ucaRecipientAccount.Text, "00000000000000000000", StringComparison.Ordinal)
                        || ucaRecipientAccount.Text.Length == 0;
                }

                if (m_isForward)
                {
                    if (txtRecipientBik.Enabled)
                    {
                        txtRecipientBik.Focus();
                    }
                    else if (ucaRecipientAccount.Enabled)
                    {
                        ucaRecipientAccount.Focus();
                    }
                    else if (txtRecipientInn.Enabled)
                    {
                        txtRecipientInn.Focus();
                    }
                    else if (cmbPurpose.Enabled)
                    {
                        cmbPurpose.Focus();
                    }
                    else if (txtPaymentCode.Enabled)
                    {
                        txtPaymentCode.Focus();
                    }
                    else
                    {
                        udcPaymentAmount.Focus();
                    }
                }

                if (string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal)
                    && string.Equals(m_sidPattern, PatternNalog, StringComparison.Ordinal)
                    && m_idClient != 0 && txtRecipientBik.Text.Trim().Length > 0)
                {
                    FillNalog(true, m_idClient);
                }

                this.IUbsChannel.ParamIn("IdContract", m_idContract);
                this.IUbsChannel.ParamIn("IdClient", m_idContract);
                this.IUbsChannel.ParamIn("BIC", txtRecipientBik.Text);
                this.IUbsChannel.Run("UtGetAccINNFromLastPayment");

                var paramOutUtGetAccINNFromLastPayment = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                if (txtRecipientInn.Enabled)
                {
                    txtRecipientInn.Text = paramOutUtGetAccINNFromLastPayment.GetParamOutString("INN");
                }

                //'Переносим данные р/с
                if (ucaRecipientAccount.Enabled)
                {
                    ucaRecipientAccount.Text = paramOutUtGetAccINNFromLastPayment.GetParamOutString("ACC");
                }

                //'Заносятся данные назначение платежа из последнего платежа
                if (cmbPurpose.Enabled && string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal))
                {
                    var strPurpose = paramOutUtGetAccINNFromLastPayment.GetParamOutString("PURPOSE");

                    if (!IsComboBoxItemExists(strPurpose) && strPurpose.Trim().Length > 0)
                    {
                        cmbPurpose.Items.Add(strPurpose);
                        cmbPurpose.SelectedIndex = cmbPurpose.Items.Count - 1;
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private bool IsComboBoxItemExists(string strPurpose)
        {
            throw new NotImplementedException();
        }

        private void SetParamAccCodeKey(int m_idKindPaym)
        {
            throw new NotImplementedException();
        }

        private void SetPropBank(string isSepDoc)
        {
            throw new NotImplementedException();
        }

        private bool CheckSubContracts(UbsParamCustom paramOutUtReadContract)
        {
            throw new NotImplementedException();
        }

        private void FillPhone(int idPhone)
        {
            try
            {
                this.IUbsChannel.Run("ReadPhone");

                var paramOutReadPhone = new UbsParamCustom(base.IUbsChannel.ParamsOut);

                object varPhone = null;
                if (paramOutReadPhone.Contains("Phone_Code_Name"))
                {
                    varPhone = paramOutReadPhone.Value("Phone_Code_Name");
                }

                int idPhoneDefault = paramOutReadPhone.GetParamOutInt("Phone_Code_Name_Default");
                if (idPhone != 0)
                {
                    idPhoneDefault = idPhone;
                }

                if (varPhone is object[,])
                {
                    object[,] arr = (object[,])varPhone;
                    int rows = arr.GetLength(0);

                    cmbPhone.Items.Clear();
                    int selectedIndex = -1;

                    for (int i = 0; i < rows; i++)
                    {
                        int phoneId = Convert.ToInt32(arr[i, 0]);
                        string display = Convert.ToString(arr[i, 1]) + ", " + Convert.ToString(arr[i, 2]);
                        cmbPhone.Items.Add(display);

                        if (idPhoneDefault != 0 && phoneId == idPhoneDefault)
                        {
                            selectedIndex = i;
                        }
                    }

                    if (selectedIndex >= 0)
                    {
                        cmbPhone.SelectedIndex = selectedIndex;
                    }
                    else if (cmbPhone.Items.Count > 0)
                    {
                        cmbPhone.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void DisableAllFields()
        {
            SetAllFieldsEnabled(false);
        }

        private void EnableAllFields()
        {
            SetAllFieldsEnabled(true);
        }

        private void SetAllFieldsEnabled(bool enabled)
        {
            ucaPayerAccount.Enabled =
            txtPayerFullName.Enabled =
            linkPayerFullName.Enabled =
            txtPayerInn.Enabled =
            txtPayerAddress.Enabled =
            txtContractCode.Enabled =
            txtRecipientComment.Enabled =
            linkContractCode.Enabled =
            txtRecipientBik.Enabled =
            ucaRecipientCorrAccount.Enabled =
            txtRecipientBankName.Enabled =
            txtRecipientInn.Enabled =
            ucaRecipientAccount.Enabled =
            cmbPurpose.Enabled =
            udcPaymentAmount.Enabled =
            txtCheckSum.Enabled =
            udcPaymentAmount.Enabled =
            udcPenaltyAmount.Enabled =
            btnSave.Enabled =
            btnCalc.Enabled =
            cmbCityCode.Enabled =
            cmbPurpose.Enabled =
            cmbTariff.Enabled =
            txtThirdPersonName.Enabled =
            cmbThirdPersonKind.Enabled =
            txtThirdPersonInn.Enabled =
            txtThirdPersonKpp.Enabled =
            linkThirdPersonName.Enabled =
            chkThirdPerson.Enabled =
            ucfAddProperties.Enabled =
            txtRecipientName.Enabled = enabled;

            if (!Convert.ToBoolean(ucfAddProperties.Command("ReadOnlySet", true)))
            {
                ucfAddProperties.Enabled = enabled;
            }

            txtPeriodDayBeg.Enabled =
            txtPeriodMonthBeg.Enabled =
            txtPeriodYearBeg.Enabled =
            txtPeriodDayEnd.Enabled =
            txtPeriodMonthEnd.Enabled =
            txtPeriodYearEnd.Enabled =
            txtPaymentCode.Enabled =
            txtRecipientName.Enabled =
            txtRecipientKpp.Enabled =
            txtTaxStatus.Enabled =
            txtTaxKbk.Enabled =
            txtTaxKbkNote.Enabled =
            txtTaxOkato.Enabled =
            txtTaxOkato.Enabled =
            txtTaxPeriodCode.Enabled =
            txtTaxDocumentNumber.Enabled =
            txtTaxDocumentDate.Enabled =
            txtTaxType.Enabled =
            txtTaxImns.Enabled = enabled;
        }

        private void FillCityCode(object arrCityCode)
        {
            cmbCityCode.Items.Clear();
            if (arrCityCode == null)
            {
                return;
            }

            if (arrCityCode is object[,])
            {
                object[,] arr = (object[,])arrCityCode;
                int rows = arr.GetLength(0);
                for (int i = 0; i < rows; i++)
                {
                    cmbCityCode.Items.Add(Convert.ToString(arr[i, 0]));
                }
            }
        }

        private void CheckPeni(string summaPeni = "")
        {
            lblPenaltyAmount.Visible = m_isPenyPresent;
            udcPenaltyAmount.Visible = m_isPenyPresent;

            if (summaPeni != null && summaPeni.Length > 0)
            {
                udcPenaltyAmount.Text = summaPeni;
            }
        }

        /// <summary>
        /// DefineRunUserForm: вызывает скрипт пользовательской формы,
        /// устанавливает заголовок и видимость btnPattern.
        /// </summary>
        private void DefineRunUserForm(bool runFlag)
        {
            try
            {
                var scripter = base.Ubs_VBScriptRunner();

                scripter.LoadFiles(@"UBS_VBD\PS\UT\UTPUFT.vbs");

                var paramInScripter = new UbsParam();

                UbsParent oParent = new UbsParent();
                oParent.Form32 = this;
                oParent.Loader = new UbsLoader();
                UbsParentStub oParentStub = new UbsParentStub(oParent);
                UbsUserDocument oUserDocument = new UbsUserDocument(oParentStub);

                paramInScripter.Value("DocObj", oParent);

                var paramInCallingUserFormPattern = new UbsParam();

                paramInCallingUserFormPattern.Value("DocHandle", UbsShortNumerator.Number);
                paramInCallingUserFormPattern.Value("IdContract", m_idContract);
                paramInCallingUserFormPattern.Value("RunUserForm", runFlag);

                scripter.UbsScriptParam = paramInScripter;

                var paramOutCallingUserFormPattern = new UbsParamCustom();

                scripter.Run("CallingUserFormPattern", paramInCallingUserFormPattern, paramOutCallingUserFormPattern);

                string btnCaption = paramOutCallingUserFormPattern.GetParamOutString("ButtonCaption");
                if (btnCaption.Length > 0)
                {
                    btnPattern.Text = btnCaption;
                }

                if (!paramOutCallingUserFormPattern.GetParamOutBool("bRetVal") && paramOutCallingUserFormPattern.GetParamOutString("StrError").Length > 0)
                {
                    MessageBox.Show(paramOutCallingUserFormPattern.GetParamOutString("StrError"), CaptionError,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal))
                {
                    if (runFlag)
                    {
                        FillControlsByPattern(paramOutCallingUserFormPattern.Items);
                    }
                    else
                    {
                        btnPattern.Visible = paramOutCallingUserFormPattern.GetParamOutBool("OptionOK");
                    }
                }
                else
                {
                    btnPattern.Visible = false;
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void FillControlsByPattern(object[,] items)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GetIdClientFromGroupPayment: находит клиента по входящей группе.
        /// </summary>
        private void GetIdClientFromGroupPayment()
        {
            try
            {
                if (m_idGroupIncoming > 0)
                {
                    this.IUbsChannel.ParamIn("IdGroupIncoming", m_idGroupIncoming);
                    this.IUbsChannel.Run("GetIdClientFromGroupPayment");

                    var paramOutGetIdClientFromGroupPayment = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                    m_idClient = paramOutGetIdClientFromGroupPayment.GetParamOutInt("IdClient");
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void FillDataPayment(object arrDataPayment)
        {
            if (arrDataPayment == null)
            {
                return;
            }

            // TODO B3.3: parse arrDataPayment variant matrix and populate
            // all General-tab controls (payer, recipient, amounts, cash symbols,
            // purpose, tax fields, periods, etc.)
        }

        /// <summary>
        /// FillTariff: читает тариф и заполняет cmbTariff.
        /// </summary>
        private void FillTariff(int idTariff)
        {
            try
            {
                this.IUbsChannel.ParamIn("IdPaym", m_idPayment);
                this.IUbsChannel.Run("ReadTariff");

                var paramOutReadTariff = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                object varTariffOut = null;
                if (paramOutReadTariff.Contains("Tarif_Name_Rate"))
                {
                    varTariffOut = paramOutReadTariff.Value("Tarif_Name_Rate");
                }

                int idTarDefault = paramOutReadTariff.GetParamOutInt("Tarif_Name_Rate_Default");
                m_codeEnergy = paramOutReadTariff.GetParamOutInt("Code_Energy");

                if (idTariff != 0)
                {
                    idTarDefault = idTariff;
                }

                m_varTariff = varTariffOut;

                if (varTariffOut is object[,])
                {
                    object[,] arr = (object[,])varTariffOut;
                    int rows = arr.GetLength(0);

                    cmbTariff.Items.Clear();
                    int selectedIndex = -1;

                    for (int i = 0; i < rows; i++)
                    {
                        int tarId = Convert.ToInt32(arr[i, 0]);
                        string display = Convert.ToString(arr[i, 1]) + ", " + Convert.ToString(arr[i, 2]);
                        cmbTariff.Items.Add(display);

                        if (idTarDefault != 0 && tarId == idTarDefault)
                        {
                            selectedIndex = i;
                        }
                    }

                    if (selectedIndex >= 0)
                    {
                        cmbTariff.SelectedIndex = selectedIndex;
                    }
                    else if (cmbTariff.Items.Count > 0)
                    {
                        cmbTariff.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        /// <summary>
        /// AddProcInit: инициализирует форму в ADD режиме
        /// </summary>
        private bool AddProcInit()
        {
            return AddProcInit(true);
        }

        private bool AddProcInit(bool blnMsgIsVisible)
        {
            try
            {
                m_isCheckIncoming = false;

                this.IUbsChannel.ParamIn("NameSection", "Оплата услуг");
                this.IUbsChannel.Run("Ps_CheckPrintForm");

                var paramOutPsCheckPrintForm = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                m_isIsFrmPrn = paramOutPsCheckPrintForm.GetParamOutBool("IsFrmPrn");
                if (!paramOutPsCheckPrintForm.GetParamOutBool("bRetVal"))
                {
                    string pfErr = paramOutPsCheckPrintForm.GetParamOutString("StrError");
                    if (pfErr.Length > 0)
                    {
                        MessageBox.Show(pfErr, "ПФ. " + CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.btnExit_Click(this, EventArgs.Empty);
                        return false;
                    }
                }

                cmbPurpose.Items.Clear();
                cmbPurpose.Text = string.Empty;
                m_idContract = 0;
                m_idPayment = 0;

                this.IUbsChannel.Run("InitForm");

                var paramOutInitForm = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                if (!paramOutInitForm.GetParamOutBool("bRetVal") && paramOutInitForm.GetParamOutBool("bMsgBoxYesNo") && blnMsgIsVisible)
                {
                    if (MessageBox.Show(paramOutInitForm.GetParamOutString("StrError"), CaptionForm,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                    {
                        this.btnExit_Click(this, EventArgs.Empty);
                        return false;
                    }
                }

                if (!InitDoc())
                {
                    return false;
                }

                if (paramOutInitForm.GetParamOutInt("DocumentsExists") == 1)
                {
                    m_command = StrCommandView;
                    uciInfo.Show(MsgDocumentsExistViewOnly);
                    uciInfo.Show();
                }

                if (m_idGroupIncoming == 0)
                {
                    var scripter = base.Ubs_VBScriptRunner();

                    scripter.LoadFiles(@"UBS_VBS\PS\PsDevice.vbs");

                    var paramInFormInitDevice = new UbsParam();

                    paramInFormInitDevice.Value("blnGroup", m_isGroup);
                    paramInFormInitDevice.Value("strBusinessName", "Оплата услуг");

                    var paramOutFormInitDevice = new UbsParamCustom();

                    scripter.Run("FormInitDevice", paramInFormInitDevice, paramOutFormInitDevice);

                    if (!paramOutFormInitDevice.GetParamOutBool("bRetVal"))
                    {
                        string deviceErr = paramOutFormInitDevice.GetParamOutString("StrError");
                        if (deviceErr.Length > 0)
                        {
                            MessageBox.Show(deviceErr, "ФР и сканер. " + CaptionForm,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                            this.btnExit_Click(this, EventArgs.Empty);

                            return false;
                        }
                    }

                    m_isFR = paramOutFormInitDevice.GetParamOutBool("bIsFR");
                    m_RegNumber = paramOutFormInitDevice.GetParamOutString("strRegNumber");
                    if (paramOutFormInitDevice.Contains("objDevice"))
                    {
                        m_objDevice = paramOutFormInitDevice.Value("objDevice");
                    }

                    if (paramOutFormInitDevice.Contains("objScan"))
                    {
                        m_isScan = paramOutFormInitDevice.GetParamOutBool("bIsScan");
                        m_objScanner = paramOutFormInitDevice.Value("objScan");
                    }
                }

                m_isCheckIncoming = true;

                return true;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);

                return false;
            }
        }

        /// <summary>
        /// IsAutoPeriod: проверяет и применяет автоматическую настройку периодов.
        /// Calls UtGetAutoFillPeriod; if IsPeriod = true, clears all period fields.
        /// </summary>
        private void IsAutoPeriod()
        {
            try
            {
                this.IUbsChannel.Run("UtGetAutoFillPeriod");

                m_isAutoPeriodFlag = false;

                var paramOutUtGetAutoFillPeriod = new UbsParamCustom(this.UbsChannel_ParamsOut);

                if (paramOutUtGetAutoFillPeriod.GetParamOutBool("bRetVal"))
                {
                    bool isPeriod = paramOutUtGetAutoFillPeriod.GetParamOutBool("IsPeriod");
                    m_isAutoPeriodFlag = isPeriod;

                    if (isPeriod)
                    {
                        txtPeriodYearBeg.Text = string.Empty;
                        txtPeriodMonthBeg.Text = string.Empty;
                        txtPeriodDayBeg.Text = string.Empty;

                        txtPeriodYearEnd.Text = string.Empty;
                        txtPeriodMonthEnd.Text = string.Empty;
                        txtPeriodDayEnd.Text = string.Empty;
                    }
                }
                else
                {
                    string err = paramOutUtGetAutoFillPeriod.GetParamOutString("StrError");
                    if (err.Length > 0)
                    {
                        MessageBox.Show(err, "Автозаполнение периода. " + CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        /// <summary>
        /// GetDayEnd: возвращает последний день месяца для заданного месяца и года.
        /// </summary>
        private int GetDayEnd(string strMonth, string strYear)
        {
            try
            {
                int month = Convert.ToInt32(strMonth);
                int year = Convert.ToInt32(strYear);
                DateTime firstOfMonth = new DateTime(year, month, 1);
                DateTime firstOfNext = firstOfMonth.AddMonths(1);
                DateTime lastDay = firstOfNext.AddDays(-1);
                return lastDay.Day;
            }
            catch
            {
                return 1;
            }
        }

        private void GetGroupIDByPaymentID(int paymentId)
        {
            try
            {
                if (m_isGroup && paymentId > 0)
                {
                    this.IUbsChannel.ParamIn("IdPaym", paymentId);
                    this.IUbsChannel.Run("GetGroupIDByPaymentID");

                    var paramOutGetGroupIDByPaymentID = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                    m_idGroup = paramOutGetGroupIDByPaymentID.GetParamOutInt("PAYMENTGROUPID");
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        /// <summary>
        /// UpdateGroupInfo: заполняет информацию группового платежа на форме.
        /// </summary>
        private void UpdateGroupInfo()
        {
            //todo: implement UpdateGroupInfo
            try
            {
                this.IUbsChannel.ParamIn("IdGroup", m_idGroup);
                this.IUbsChannel.Run("UpdateGroupInfo");
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        /// <summary>
        /// Обработка кортежа из param_in в режиме ADD_PARAM.
        /// Each element of itemArray is object[] { name, value, enabled }.
        /// </summary>
        private void ProcessAddParam(object[] itemArray)
        {
            if (itemArray == null || itemArray.Length == 0)
            {
                return;
            }

            try
            {
                for (int i = 0; i < itemArray.Length; i++)
                {
                    object[] tuple = itemArray[i] as object[];
                    if (tuple == null || tuple.Length < 2)
                    {
                        continue;
                    }

                    string name = Convert.ToString(tuple[0]);
                    object value = tuple[1];
                    bool enabled = (tuple.Length >= 3) && Convert.ToBoolean(tuple[2]);

                    if (string.Equals(name, "ФИО", StringComparison.Ordinal))
                    {
                        txtPayerFullName.Text = Convert.ToString(value);
                        txtPayerFullName.Enabled = enabled;
                    }
                    else if (string.Equals(name, "Идентификатор клиента", StringComparison.Ordinal))
                    {
                        m_idClient = Convert.ToInt32(value);
                        m_isGuest = enabled;
                        FillPayer();
                        CheckPayer(false);
                    }
                    else if (string.Equals(name, "Адрес", StringComparison.Ordinal))
                    {
                        txtPayerAddress.Text = Convert.ToString(value);
                        txtPayerAddress.Enabled = enabled;
                    }
                    else if (string.Equals(name, "Реквизиты плательщика", StringComparison.Ordinal))
                    {
                        txtPayerClientInfo.Text = Convert.ToString(value);
                    }
                    else if (string.Equals(name, "Код договора", StringComparison.Ordinal))
                    {
                        txtContractCode.Text = Convert.ToString(value);
                        txtContractCode.Enabled = enabled;
                        txtRecipientComment.Enabled = enabled;
                        linkContractCode.Enabled = enabled;
                        m_isCodeEnter = true;
                        FindContract();
                    }
                    else if (string.Equals(name, "БИК", StringComparison.Ordinal))
                    {
                        txtRecipientBik.Text = Convert.ToString(value);
                        txtRecipientBik.Enabled = enabled;
                        GetBankNameACC();
                    }
                    else if (string.Equals(name, "Р/с", StringComparison.Ordinal))
                    {
                        ucaRecipientAccount.Text = Convert.ToString(value);
                        ucaRecipientAccount.Enabled = enabled;
                    }
                    else if (string.Equals(name, "Корр. счет банка получателя", StringComparison.Ordinal))
                    {
                        ucaRecipientCorrAccount.Text = Convert.ToString(value);
                        ucaRecipientCorrAccount.Enabled = enabled;
                    }
                    else if (string.Equals(name, "Счет плательщика", StringComparison.Ordinal))
                    {
                        txtPayerAccount.Text = Convert.ToString(value);
                        txtPayerAccount.Enabled = enabled;
                    }
                    else if (string.Equals(name, "Назначение", StringComparison.Ordinal))
                    {
                        cmbPurpose.Text = Convert.ToString(value);
                        cmbPurpose.Enabled = enabled;
                    }
                    else if (string.Equals(name, "Наименование получателя", StringComparison.Ordinal))
                    {
                        txtRecipientName.Text = Convert.ToString(value);
                        txtRecipientName.Enabled = enabled;
                    }
                    else if (string.Equals(name, "Наименование банка получателя", StringComparison.Ordinal))
                    {
                        txtRecipientBankName.Text = Convert.ToString(value);
                        txtRecipientBankName.Enabled = enabled;
                    }
                    else if (string.Equals(name, "ИНН", StringComparison.Ordinal))
                    {
                        txtRecipientInn.Text = Convert.ToString(value);
                        txtRecipientInn.Enabled = enabled;
                    }
                    else if (string.Equals(name, "ИНН плательщика", StringComparison.Ordinal))
                    {
                        txtPayerInn.Text = Convert.ToString(value);
                        txtPayerInn.Enabled = enabled;
                    }
                    else if (string.Equals(name, "Сумма", StringComparison.Ordinal))
                    {
                        udcPaymentAmount.DecimalValue = Convert.ToDecimal(value);
                        udcPaymentAmount.Enabled = enabled;
                    }
                    else if (string.Equals(name, "Комиссия с плательщика", StringComparison.Ordinal))
                    {
                        udcPayerRateAmount.DecimalValue = Convert.ToDecimal(value);
                        udcPayerRateAmount.Enabled = enabled;
                        m_prefCalcRate = "_2 5";
                        CalcSumCommiss_2();
                    }
                    else if (string.Equals(name, "Параметры поиска", StringComparison.Ordinal))
                    {
                        m_contractFilterLimitations = true;
                        if (tuple.Length >= 2) m_searchTemplate = Convert.ToString(tuple[1]);
                        if (tuple.Length >= 3) m_searchKBK = Convert.ToString(tuple[2]);
                        if (tuple.Length >= 4) m_searchBIK = Convert.ToString(tuple[3]);
                    }
                }

                linkContractCode.Enabled = true;

                if (m_outerSystemInfo)
                {
                    FillNalog(false, 0);
                    btnPattern.Visible = false;
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        /// <summary>
        /// CheckPayer: когда showMsg=true информация о плательщике не изменилась
        /// </summary>
        private bool CheckPayer(bool showMsg)
        {
            if (showMsg)
            {
                if (string.Equals(m_strFIOOld, txtPayerFullName.Text, StringComparison.Ordinal)
                    && string.Equals(m_strAddressOld, txtPayerAddress.Text, StringComparison.Ordinal))
                {
                    return true;
                }
                return false;
            }
            else
            {
                m_idClientOld = m_idClient;
                m_strFIOOld = txtPayerFullName.Text;
                m_strAddressOld = txtPayerAddress.Text;
                m_strINNOld = txtPayerInn.Text;
                return true;
            }
        }

        /// <summary>
        /// GetBankNameACC: Проверяет BIC и присваивает наименование банка и кор. счет.
        /// </summary>
        private bool GetBankNameACC()
        {
            try
            {
                this.IUbsChannel.ParamIn("BIC", txtRecipientBik.Text);
                this.IUbsChannel.Run("UtCheckBIKBank");

                var paramOutUtCheckBIKBank = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                if (!paramOutUtCheckBIKBank.GetParamOutBool("bRetVal"))
                {
                    if (m_isNoMessage)
                    {
                        MessageBox.Show(paramOutUtCheckBIKBank.GetParamOutString("strError"), CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return false;
                }

                this.IUbsChannel.ParamIn("BIC", txtRecipientBik.Text);
                this.IUbsChannel.Run("UtCheckBIKLimitSharing");

                if (!paramOutUtCheckBIKBank.GetParamOutBool("bRetVal"))
                {
                    string limitMsg = paramOutUtCheckBIKBank.GetParamOutString("strError") + Environment.NewLine + "Продолжить ввод платежа?";
                    if (MessageBox.Show(limitMsg, CaptionValidation,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        txtContractCode.Text = string.Empty;
                        txtRecipientComment.Text = string.Empty;
                        txtRecipientBik.Text = string.Empty;
                        txtRecipientBik.Enabled = true;
                        ucaRecipientCorrAccount.Text = "00000000000000000000";
                        txtRecipientBankName.Text = string.Empty;
                        txtRecipientInn.Text = string.Empty;
                        txtRecipientInn.Enabled = true;
                        ucaRecipientAccount.Text = "00000000000000000000";
                        ucaRecipientAccount.Enabled = true;
                        cmbPurpose.Items.Clear();
                        cmbPurpose.Text = string.Empty;
                        txtRecipientName.Text = string.Empty;
                        return false;
                    }
                }

                this.IUbsChannel.ParamIn("BIC", txtRecipientBik.Text);
                this.IUbsChannel.Run("ReadBankBIK");

                var paramOutReadBankBIK = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                int num = paramOutReadBankBIK.GetParamOutInt("NUM");
                if (num > 0)
                {
                    object pars = null;
                    if (paramOutReadBankBIK.Contains("Parameters"))
                    {
                        pars = paramOutReadBankBIK.Value("Parameters");
                    }

                    if (pars is object[,])
                    {
                        object[,] arr = (object[,])pars;
                        int rows = arr.GetLength(0);
                        bool bicChanged = !string.Equals(txtRecipientBik.Text, m_bicOld, StringComparison.Ordinal);

                        for (int i = 0; i < rows; i++)
                        {
                            string key = Convert.ToString(arr[i, 0]);
                            string val = Convert.ToString(arr[i, 1]);

                            if (string.Equals(key, "BANKNAME", StringComparison.Ordinal))
                            {
                                if (txtRecipientBankName.Text.Trim().Length == 0 || bicChanged)
                                {
                                    txtRecipientBankName.Text = val;
                                }
                            }
                            else if (string.Equals(key, "CORRACC", StringComparison.Ordinal))
                            {
                                string corrText = ucaRecipientCorrAccount.Text.Trim();
                                if (corrText.Length == 0
                                    || string.Equals(corrText, "00000000000000000000", StringComparison.Ordinal)
                                    || bicChanged)
                                {
                                    ucaRecipientCorrAccount.Text = val;
                                }
                            }
                        }
                    }
                    else
                    {
                        string bankName = paramOutReadBankBIK.GetParamOutString("BANKNAME");
                        string corrAcc = paramOutUtCheckBIKBank.GetParamOutString("CORRACC");
                        bool bicChanged = !string.Equals(txtRecipientBik.Text, m_bicOld, StringComparison.Ordinal);

                        if (txtRecipientBankName.Text.Trim().Length == 0 || bicChanged)
                        {
                            txtRecipientBankName.Text = bankName;
                        }

                        string corrText = ucaRecipientCorrAccount.Text.Trim();
                        if (corrText.Length == 0
                            || string.Equals(corrText, "00000000000000000000", StringComparison.Ordinal)
                            || bicChanged)
                        {
                            ucaRecipientCorrAccount.Text = corrAcc;
                        }
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return false;
            }
        }

        /// <summary>
        /// CalcSumCommiss_2: 
        /// пересчитывает комиссию от плательщика на основе m_arrRateSend(rate type, percentage, min, max, tariff scale) затем изменяет udcPayerRateAmount and udcAmountWithRate.
        /// </summary>
        private void CalcSumCommiss_2()
        {
            try
            {
                btnSave.Enabled = false;

                if (m_arrRateSend == null || !(m_arrRateSend is object[]))
                {
                    btnSave.Enabled = true;
                    return;
                }

                object[] rateArr = (object[])m_arrRateSend;
                if (rateArr.Length < 4)
                {
                    btnSave.Enabled = true;
                    return;
                }

                int intTypeSend = Convert.ToInt32(rateArr[0]);
                decimal curPerSend = Convert.ToDecimal(rateArr[1]);
                decimal curMinSumSend = Convert.ToDecimal(rateArr[2]);
                decimal curMaxSumSend = Convert.ToDecimal(rateArr[3]);

                decimal curSumPaym = udcPaymentAmount.DecimalValue;

                var toolTip = new ToolTip();
                toolTip.SetToolTip(udcPaymentAmount, $"Ком с плат: тип={intTypeSend}, ставка={curPerSend}, мин={curMinSumSend}, макс={curMaxSumSend}, от суммы={curSumPaym}, польз форма={m_isAddparam}, ком на польз форме={udcPayerRateAmount.DecimalValue}");

                if (rateArr.Length >= 6)
                {
                    object arrStavka = rateArr[5];
                    string strTarif = Convert.ToString(rateArr[4]);

                    if (arrStavka is object[,])
                    {
                        if (string.Equals(strTarif, "ставка, %", StringComparison.Ordinal))
                        {
                            intTypeSend = 1;
                        }
                        else if (string.Equals(strTarif, "фиксированная сумма", StringComparison.Ordinal))
                        {
                            intTypeSend = 2;
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            return;
                        }

                        object[,] scale = (object[,])arrStavka;
                        int scaleRows = scale.GetLength(0);

                        curPerSend = 0;
                        for (int i = scaleRows - 1; i >= 0; i--)
                        {
                            if (Convert.ToDecimal(scale[i, 0]) <= curSumPaym)
                            {
                                curPerSend = Convert.ToDecimal(scale[i, 1]);
                                break;
                            }
                        }
                    }
                }

                if (tabPageTax.Visible && m_isSecondPayment == 2)
                {
                    intTypeSend = 0;
                }

                decimal curSumRateSend;
                switch (intTypeSend)
                {
                    case 1:
                        if (m_isComissPeniPayer)
                        {
                            curSumRateSend = Math.Round(
                                (curSumPaym + udcPenaltyAmount.DecimalValue) * curPerSend / 100m, 2);
                        }
                        else
                        {
                            curSumRateSend = Math.Round(curSumPaym * curPerSend / 100m, 2);
                        }
                        break;
                    case 2:
                        curSumRateSend = Math.Round(curPerSend, 2);
                        break;
                    default:
                        curSumRateSend = 0m;
                        break;
                }

                if (intTypeSend > 0)
                {
                    if (curSumRateSend < curMinSumSend && curMinSumSend > 0)
                    {
                        curSumRateSend = Math.Round(curMinSumSend, 2);
                    }
                    if (curSumRateSend > curMaxSumSend && curMaxSumSend > 0)
                    {
                        curSumRateSend = Math.Round(curMaxSumSend, 2);
                    }
                }

                if (m_isAddparam && udcPayerRateAmount.DecimalValue > 0)
                {
                    curSumRateSend = udcPayerRateAmount.DecimalValue;
                }

                decimal curSumTotal = Math.Round(curSumPaym + curSumRateSend, 2);

                if (m_isPenyPresent)
                {
                    curSumTotal = curSumTotal + udcPenaltyAmount.DecimalValue;
                }

                udcPayerRateAmount.DecimalValue = curSumRateSend;
                udcAmountWithRate.DecimalValue = curSumTotal;

                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                btnSave.Enabled = true;
                this.Ubs_ShowError(ex);
            }
        }

        /// <summary>
        /// FillNalog: заполняет контролы формы
        /// </summary>
        private void FillNalog(bool useLast, int idClient)
        {
            try
            {
                this.IUbsChannel.ParamIn("LAST", useLast);

                if (m_outerSystemInfo)
                {
                    this.IUbsChannel.ParamIn("StrCommand", string.Empty);
                }
                else
                {
                    this.IUbsChannel.ParamIn("StrCommand", m_command);
                }

                this.IUbsChannel.ParamIn("IdContract", m_idContract);
                this.IUbsChannel.ParamIn("IdClient", idClient);
                this.IUbsChannel.ParamIn("blnGuest", m_isGuest);

                this.IUbsChannel.Run("ReadNalog");

                var paramOutReadNalog = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                txtTaxStatus.Text = paramOutReadNalog.GetParamOutString("Статус составителя");

                m_forbidTaxStatusChanges = paramOutReadNalog.GetParamOutBool("Запрет изменения статуса составителя");
                if (m_forbidTaxStatusChanges)
                {
                    txtTaxStatus.Enabled = false;
                    m_savedTaxStatusValue = paramOutReadNalog.GetParamOutString("Статус составителя из договора");
                }

                txtTaxReasonCode.Text = paramOutReadNalog.GetParamOutString("Код ОКАТО");
                txtTaxPeriodCode.Text = paramOutReadNalog.GetParamOutString("Основание налогового платежа");
                txtTaxDocumentNumber.Text = paramOutReadNalog.GetParamOutString("Налоговый период");
                txtTaxDocumentDate.Text = paramOutReadNalog.GetParamOutString("Номер налогового документа");
                txtTaxType.Text = paramOutReadNalog.GetParamOutString("Дата налогового документа");
                txtTaxImns.Text = paramOutReadNalog.GetParamOutString("Тип налогового платежа");
                txtTaxKbkNote.Text = paramOutReadNalog.GetParamOutString("ИМНС");
                txtTaxOkato.Text = paramOutReadNalog.GetParamOutString("Расшифровка КБК");
                txtTaxKbk.Text = paramOutReadNalog.GetParamOutString("Код бюджетной классификации");

                if (string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal)
                    || string.Equals(m_command, StrCommandCopy, StringComparison.Ordinal)
                    || txtTaxImns.Text.Length == 0)
                {
                    txtTaxImns.Text = string.Empty;
                    txtTaxImns.Enabled = false;
                }

                ucfAddProperties.Refresh();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        /// <summary>
        /// FillPayer: читает client data по m_idClient;
        /// заполняет контролы формы (INN, InfoClient, FIO, Address, benefits).
        /// </summary>
        private void FillPayer()
        {
            try
            {
                this.IUbsChannel.ParamIn("IDCLIENT", m_idClient);
                this.IUbsChannel.ParamIn("IsGuest", m_isGuest);

                this.IUbsChannel.Run("ReadClientFromIdOC");

                var paramOutReadClientFromIdOC = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                string readErr = paramOutReadClientFromIdOC.GetParamOutString("StrError");
                if (readErr.Length > 0)
                {
                    MessageBox.Show(readErr, "ReadClientFromIdOC " + CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                txtPayerInn.Text = paramOutReadClientFromIdOC.GetParamOutString("INN");
                txtPayerClientInfo.Text = paramOutReadClientFromIdOC.GetParamOutString("InfoClient");
                txtPayerFullName.Text = paramOutReadClientFromIdOC.GetParamOutString("NAME");
                txtPayerAddress.Text = paramOutReadClientFromIdOC.GetParamOutString("ADRESS");

                if (paramOutReadClientFromIdOC.GetParamOutBool("Benefits"))
                {
                    chkBenefits.Checked = true;
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        /// <summary>
        /// FillPurpose: заполняет cmbPurpose.
        /// </summary>
        private void FillPurpose(object arrPurpose)
        {
            if (arrPurpose == null)
            {
                return;
            }

            try
            {
                cmbPurpose.Items.Clear();

                if (arrPurpose is object[,])
                {
                    object[,] arr = (object[,])arrPurpose;
                    int rows = arr.GetLength(0);

                    for (int i = 0; i < rows; i++)
                    {
                        cmbPurpose.Items.Add(Convert.ToString(arr[i, 0]));
                    }

                    if (cmbPurpose.Items.Count > 0)
                    {
                        cmbPurpose.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }
    }
}
