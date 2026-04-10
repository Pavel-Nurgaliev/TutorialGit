using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    public partial class UbsPsUtPaymentGroupFrm
    {
        private sealed class GroupContractItem
        {
            public readonly int Id;
            public readonly string Caption;

            public GroupContractItem(int id, string caption)
            {
                Id = id;
                Caption = caption ?? string.Empty;
            }

            public override string ToString()
            {
                return Caption;
            }
        }

        private void InitDoc()
        {
            try
            {
                btnSaveAttribute.Visible = false;
                btnListAttributeRecip.Visible = false;
                txtRecip.Enabled = false;
                lblRecip.Enabled = false;
                udcPeny.Enabled = false;
                tabPageAddProperties.Enabled = false;

                m_blnSave = false;
                m_blnClickSave = false;

                base.IUbsChannel.Run("FormStart");

                base.UbsInit();

                this.UbsChannel_ParamIn("StrCommand", m_command);
                this.UbsChannel_ParamIn("IdPayment", m_idPayment);
                this.UbsChannel_Run("InitFormGroup");

                string strError = this.UbsChannel_ExistParamOut("strError") ? Convert.ToString(this.UbsChannel_ParamOut("strError")) : string.Empty;

                if (!string.IsNullOrEmpty(strError))
                {
                    MessageBox.Show(strError, CaptionInitCheck,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    this.Close();

                    return;
                }

                m_blnGuest = (Convert.ToInt32(this.UbsChannel_ParamOut("ChoiceClient")) == 0);
                m_blnEndGroup = Convert.ToBoolean(this.UbsChannel_ParamOut("EndGroup"));
                m_groupContract = this.UbsChannel_ParamOut("GroupContract") as object[,];
                udcPeny.Text = Convert.ToString(this.UbsChannel_ParamOut("SummaPeni"));

                ucfAddProperties.Refresh();

                cmbCode.Items.Clear();
                if (m_groupContract != null)
                {
                    int rowCount = m_groupContract.GetLength(0);
                    for (int i = 0; i < rowCount; i++)
                    {
                        int id = Convert.ToInt32(m_groupContract[i, 0]);
                        string caption = Convert.ToString(m_groupContract[i, 1]);
                        cmbCode.Items.Add(new GroupContractItem(id, caption));
                    }
                }

                if (string.Equals(m_command, StrCommandGroupEdit, StringComparison.Ordinal))
                {
                    ReadContract();
                    if (m_blnEndGroup)
                    {
                        SetAllFieldsEnabled(false);
                    }
                    ucfAddProperties.Refresh();
                }
                else if (string.Equals(m_command, StrCommandGroupAdd, StringComparison.Ordinal))
                {
                    SetAllFieldsEnabled(true);
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void ReadContract()
        {
            try
            {
                this.UbsChannel_ParamIn("StrCommand", StrCommandRead);
                this.UbsChannel_ParamIn("IdPaym", m_idPayment);
                this.UbsChannel_Run("Payment");

                m_idContract = Convert.ToInt32(this.UbsChannel_ParamOut("IdContract"));
                m_idClient = Convert.ToInt32(this.UbsChannel_ParamOut("IdClient"));

                txtFIOPay.Text = Convert.ToString(this.UbsChannel_ParamOut("FIOSend"));
                txtINNPay.Text = Convert.ToString(this.UbsChannel_ParamOut("PayerINN1"));
                txtAdressPay.Text = Convert.ToString(this.UbsChannel_ParamOut("AdressSend"));
                txtComment.Text = Convert.ToString(this.UbsChannel_ParamOut("Comment"));
                txtBic.Text = Convert.ToString(this.UbsChannel_ParamOut("BIC"));
                ucaAccKorr.Text = Convert.ToString(this.UbsChannel_ParamOut("AccCorr"));
                txtNameBank.Text = Convert.ToString(this.UbsChannel_ParamOut("NameBank"));
                txtINN.Text = Convert.ToString(this.UbsChannel_ParamOut("INNRec1"));
                ucaAccClient.Text = Convert.ToString(this.UbsChannel_ParamOut("AccRec"));
                cmbPurpose.Text = Convert.ToString(this.UbsChannel_ParamOut("Note"));
                txtInfoClient.Text = Convert.ToString(this.UbsChannel_ParamOut("InfoClient"));

                string strNameRecip = Convert.ToString(this.UbsChannel_ParamOut("RecipientName"));

                udcSumma.Text = Convert.ToString(this.UbsChannel_ParamOut("SummaPaym"));
                udcSummaRateSend.Text = Convert.ToString(this.UbsChannel_ParamOut("SummaRateSend"));
                udcSummaTotal.Text = Convert.ToString(this.UbsChannel_ParamOut("Summa"));

                if (m_groupContract != null)
                {
                    int rowCount = m_groupContract.GetLength(0);
                    for (int i = 0; i < rowCount; i++)
                    {
                        if (Convert.ToInt32(m_groupContract[i, 0]) == m_idContract)
                        {
                            cmbCode.SelectedIndex = i;
                            break;
                        }
                    }
                }

                txtRecip.Text = strNameRecip;

                RefreshCommissionRatesAfterPaymentRead();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void FindContractbyId()
        {
            try
            {
                m_blnArbitrary = false;

                txtRecip.Text = string.Empty;
                txtComment.Text = string.Empty;
                txtBic.Text = string.Empty;
                ucaAccKorr.Text = string.Empty;
                txtINN.Text = string.Empty;
                ucaAccClient.Text = string.Empty;
                txtNameBank.Text = string.Empty;

                if (!m_blnEndGroup)
                {
                    cmbPurpose.Text = string.Empty;
                }

                if (m_idContract > 0)
                {
                    this.IUbsChannel.ParamIn("IdContract", m_idContract);
                    this.IUbsChannel.Run("UtReadContract");

                    var contractOut = this.IUbsChannel.ParamsOutParam;
                    bool bRetVal = contractOut.Contains("bRetVal")
                        && Convert.ToBoolean(contractOut.Value("bRetVal"));
                    string strError = contractOut.Contains("StrError")
                        ? Convert.ToString(contractOut.Value("StrError"))
                        : string.Empty;

                    if (!bRetVal && !string.IsNullOrEmpty(strError))
                    {
                        MessageBox.Show(strError, CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                        m_idContract = 0;

                        return;
                    }

                    if (contractOut.Contains("Комментарий"))
                    {
                        MessageBox.Show(
                            Convert.ToString(contractOut.Value("Комментарий")),
                            CaptionForm,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    if (contractOut.Contains("State")
                        && Convert.ToInt32(contractOut.Value("State")) == 1)
                    {
                        MessageBox.Show(MsgContractClosedWarning, CaptionForm, MessageBoxButtons.OK);
                    }

                    CopyCommissionRateSendFromContract(contractOut);

                    m_intState = Convert.ToInt32(contractOut.Value("State"));

                    txtComment.Text = Convert.ToString(contractOut.Value("Comment"));
                    txtBic.Text = Convert.ToString(contractOut.Value("BIC"));
                    ucaAccKorr.Text = Convert.ToString(contractOut.Value("CorrAcc"));
                    txtINN.Text = Convert.ToString(contractOut.Value("INN"));
                    ucaAccClient.Text = Convert.ToString(contractOut.Value("Acc"));

                    if (string.Equals(Convert.ToString(contractOut.Value("Acc")),
                        AccountPlaceholder, StringComparison.Ordinal))
                    {
                        txtINN.Enabled = true;
                    }

                    m_strAddress = Convert.ToString(contractOut.Value("Adress"));

                    string contractBic = Convert.ToString(contractOut.Value("BIC"));
                    string contractAcc = Convert.ToString(contractOut.Value("Acc"));
                    bool showAttrButtons = m_idContract != 0
                        && string.IsNullOrEmpty(contractBic)
                        && string.Equals(contractAcc, AccountPlaceholder, StringComparison.Ordinal);
                    btnListAttributeRecip.Visible = showAttrButtons;
                    btnSaveAttribute.Visible = showAttrButtons;

                    if (string.IsNullOrEmpty(txtBic.Text.Trim()))
                    {
                        m_blnArbitrary = true;
                    }

                    txtINN.Enabled = contractOut.Contains("IsSeparateDoc")
                        && Convert.ToBoolean(contractOut.Value("IsSeparateDoc"));

                    if (string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal))
                    {
                        object arrPurpose = contractOut.Contains("arrPurpose")
                            ? contractOut.Value("arrPurpose") : null;
                        FillPurpose(arrPurpose);

                        this.IUbsChannel.Run("UtReadSetupLockPurpose");
                        var lockOut = this.IUbsChannel.ParamsOutParam;
                        bool lockRetVal = lockOut.Contains("bRetVal")
                            && Convert.ToBoolean(lockOut.Value("bRetVal"));
                        if (!lockRetVal)
                        {
                            string lockError = lockOut.Contains("strError")
                                ? Convert.ToString(lockOut.Value("strError"))
                                : string.Empty;
                            if (!string.IsNullOrEmpty(lockError))
                            {
                                MessageBox.Show(lockError, CaptionPaymentSpelling, MessageBoxButtons.OK);
                            }
                        }
                        else
                        {
                            cmbPurpose.Enabled = Convert.ToBoolean(lockOut.Value("blnPurposeLock"));
                        }
                    }

                    this.IUbsChannel.ParamIn("BIC", txtBic.Text);
                    this.IUbsChannel.Run("ReadBankBIK");
                    ReadBankBikResult();

                    tabPayment.SelectedTab = tabPageMain;

                    this.IUbsChannel.ParamIn("IdContract", m_idContract);
                    this.IUbsChannel.ParamIn("StrCommand", StrCommandChangeContract);

                    ucfAddProperties.Refresh();
                    tabPageAddProperties.Enabled = (ucfAddProperties.Controls.Count > 0);
                }
                else
                {
                    m_arrRateSend = null;
                    btnListAttributeRecip.Visible = false;
                    btnSaveAttribute.Visible = false;
                }

                txtRecip.Enabled = m_blnArbitrary;
                lblRecip.Enabled = m_blnArbitrary;

                if (m_idContract != 0 && !m_blnEndGroup)
                {
                    txtBic.Enabled = string.IsNullOrEmpty(txtBic.Text);
                    txtINN.Enabled = string.IsNullOrEmpty(txtINN.Text);
                    ucaAccClient.Enabled = string.Equals(
                        ucaAccClient.Text, AccountPlaceholder, StringComparison.Ordinal);

                    if (string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal))
                    {
                        this.IUbsChannel.ParamIn("IdContract", m_idContract);
                        this.IUbsChannel.Run("UtGetKPPU");
                        ucfAddProperties.Refresh();
                    }
                }

                if (m_idContract > 0)
                {
                    CalcSumCommiss();
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void FillPurpose(object arrPurpose)
        {
            try
            {
                cmbPurpose.Items.Clear();

                if (arrPurpose is object[,])
                {
                    object[,] arr = (object[,])arrPurpose;
                    int rowCount = arr.GetLength(0);
                    for (int i = 0; i < rowCount; i++)
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

        private bool GetBankNameACC()
        {
            try
            {
                this.IUbsChannel.ParamIn("BIC", txtBic.Text);
                this.IUbsChannel.Run("UtCheckBIKBank");
                if (!Convert.ToBoolean(this.IUbsChannel.ParamOut("bRetVal")))
                {
                    if (m_blnNoMessage)
                    {
                        MessageBox.Show(
                            Convert.ToString(this.UbsChannel_ParamOut("strError")),
                            CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    return false;
                }

                this.IUbsChannel.ParamIn("BIC", txtBic.Text);
                this.IUbsChannel.Run("UtCheckBIKLimitSharing");
                if (!Convert.ToBoolean(this.UbsChannel_ParamOut("bRetVal")))
                {
                    string errMsg = Convert.ToString(this.IUbsChannel.ParamOut("strError"))
                        + "\n" + MsgBikLimitContinueQuestionSuffix;

                    DialogResult dr = MessageBox.Show(errMsg, CaptionValidation,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr != DialogResult.Yes)
                    {
                        cmbCode.SelectedIndex = -1;
                        txtComment.Text = string.Empty;
                        txtBic.Text = string.Empty;
                        txtBic.Enabled = true;
                        txtBic.Visible = true;
                        ucaAccKorr.Text = AccountPlaceholder;
                        txtNameBank.Text = string.Empty;
                        txtINN.Text = string.Empty;
                        txtINN.Enabled = true;
                        ucaAccClient.Text = AccountPlaceholder;
                        ucaAccClient.Enabled = true;
                        cmbPurpose.Items.Clear();
                        cmbPurpose.Text = string.Empty;
                        txtRecip.Text = string.Empty;

                        return false;
                    }
                }

                this.IUbsChannel.ParamIn("BIC", txtBic.Text);
                this.IUbsChannel.Run("ReadBankBIK");
                ReadBankBikResult();

                return true;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);

                return false;
            }
        }

        private void ReadBankBikResult()
        {
            var bankOut = this.IUbsChannel.ParamsOutParam;
            int num = bankOut.Contains("NUM") ? Convert.ToInt32(bankOut.Value("NUM")) : 0;
            if (num > 0)
            {
                if (bankOut.Contains(BankBikTagBankName))
                {
                    txtNameBank.Text = Convert.ToString(bankOut.Value(BankBikTagBankName));
                }
                if (bankOut.Contains(BankBikTagCorrAcc))
                {
                    ucaAccKorr.Text = Convert.ToString(bankOut.Value(BankBikTagCorrAcc));
                }
            }
        }

        private void SetAllFieldsEnabled(bool enabled)
        {
            txtFIOPay.Enabled = enabled;
            linkFIO.Enabled = enabled;
            txtINNPay.Enabled = enabled;
            txtAdressPay.Enabled = enabled;
            cmbCode.Enabled = enabled;
            txtComment.Enabled = enabled;
            txtBic.Enabled = enabled;
            ucaAccKorr.Enabled = enabled;
            txtNameBank.Enabled = enabled;
            txtINN.Enabled = enabled;
            ucaAccClient.Enabled = enabled;
            cmbPurpose.Enabled = enabled;
            udcSumma.Enabled = enabled;
            btnSave.Enabled = enabled;
            ucfAddProperties.Enabled = enabled;
            txtRecip.Enabled = enabled;
        }

        private void ClearRecFields()
        {
            try
            {
                cmbCode.SelectedIndex = -1;
                txtComment.Text = string.Empty;
                txtBic.Text = string.Empty;
                txtBic.Enabled = true;
                txtBic.Visible = true;
                ucaAccKorr.Text = AccountPlaceholder;
                txtNameBank.Text = string.Empty;
                txtINN.Text = string.Empty;
                ucaAccClient.Text = AccountPlaceholder;
                ucaAccClient.Enabled = true;
                txtINN.Enabled = true;

                this.IUbsChannel.Run("UtReadSetupLockPurpose");
                bool bRetVal = Convert.ToBoolean(this.IUbsChannel.ParamOut("bRetVal"));
                if (!bRetVal)
                {
                    MessageBox.Show(
                        Convert.ToString(this.IUbsChannel.ParamOut("strError")),
                        CaptionPaymentSpelling,
                        MessageBoxButtons.OK);
                }
                else
                {
                    cmbPurpose.Enabled = Convert.ToBoolean(
                        this.IUbsChannel.ParamOut("blnPurposeLock"));
                }

                cmbPurpose.Text = string.Empty;
                udcSumma.Text = "0";
                udcSummaRateSend.Text = "0";
                udcSummaTotal.Text = "0";
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void ClearRecFieldsSend()
        {
            try
            {
                this.IUbsChannel.Run("UtGetStateClearFieldSend");
                bool bRetVal = Convert.ToBoolean(this.IUbsChannel.ParamOut("bRetVal"));
                if (!bRetVal)
                {
                    MessageBox.Show(
                        Convert.ToString(this.IUbsChannel.ParamOut("StrError")),
                        CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (Convert.ToBoolean(this.IUbsChannel.ParamOut("IsClearSend")))
                    {
                        txtFIOPay.Text = string.Empty;
                        txtINNPay.Text = string.Empty;
                        txtAdressPay.Text = string.Empty;
                        m_idClient = 0;
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }
    }
}
