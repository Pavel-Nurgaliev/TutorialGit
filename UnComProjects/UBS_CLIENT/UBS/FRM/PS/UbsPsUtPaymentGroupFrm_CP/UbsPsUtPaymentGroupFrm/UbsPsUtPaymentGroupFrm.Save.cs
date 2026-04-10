using System;
using System.Reflection;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    public partial class UbsPsUtPaymentGroupFrm
    {
        private void BtnSave_ClickImpl()
        {
            try
            {
                if (!this.ValidateChildren())
                {
                    return;
                }

                m_blnNoMessage = true;
                m_blnSave = true;
                btnExit.Enabled = false;
                btnSave.Enabled = false;

                if (string.IsNullOrEmpty(txtFIOPay.Text.Trim())
                    && string.IsNullOrEmpty(txtINNPay.Text.Trim())
                    && string.IsNullOrEmpty(txtAdressPay.Text.Trim()))
                {
                    m_idClient = 0;
                }

                if (!CheckPayment())
                {
                    m_blnSave = false;
                    btnExit.Enabled = true;
                    btnSave.Enabled = true;

                    return;
                }

                btnExit.Enabled = false;
                btnSave.Enabled = false;

                if (m_idClient != 0)
                {
                    if (!CheckIPDL())
                    {
                        MessageBox.Show(MsgIpdlSaveForbidden, CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        m_blnSave = false;
                        btnExit.Enabled = true;
                        btnSave.Enabled = true;

                        return;
                    }

                    this.IUbsChannel.ParamIn("blnIPDL", m_blnIPDL);
                }

                if (!UtCheckUserBeforeSave())
                {
                    m_blnSave = false;
                    btnExit.Enabled = true;
                    btnSave.Enabled = true;

                    return;
                }

                m_blnClickSave = false;

                this.IUbsChannel.ParamIn("IdContract", m_idContract);
                this.IUbsChannel.ParamIn("blnGuest", m_blnGuest);
                this.IUbsChannel.ParamIn("ID_CLIENT", m_idClient);
                this.IUbsChannel.ParamIn("txtFIOPay", txtFIOPay.Text);
                this.IUbsChannel.ParamIn("txtINNPay", txtINNPay.Text);
                this.IUbsChannel.ParamIn("txtAdressPay", txtAdressPay.Text);
                this.IUbsChannel.ParamIn("txtBic", txtBic.Text);
                this.IUbsChannel.ParamIn("AccKorr", ucaAccKorr.Text);
                this.IUbsChannel.ParamIn("AccClient", ucaAccClient.Text);
                this.IUbsChannel.ParamIn("txtINN", txtINN.Text);
                this.IUbsChannel.ParamIn("cmbPurpose", cmbPurpose.Text);
                if (m_blnArbitrary)
                {
                    this.IUbsChannel.ParamIn("txtRecip", txtRecip.Text);
                }

                this.IUbsChannel.ParamIn("curSumma", udcSumma.DecimalValue);
                this.IUbsChannel.ParamIn("curSummaRateSend", udcSummaRateSend.DecimalValue);
                this.IUbsChannel.ParamIn("curSummaTotal", udcSummaTotal.DecimalValue);

                this.IUbsChannel.ParamIn("COMMANDIN", m_command);
                this.IUbsChannel.ParamIn("Address", m_strAddress);
                this.IUbsChannel.ParamIn("StrCommand", StrCommandAdd);
                this.IUbsChannel.ParamIn("Group", true);
                this.IUbsChannel.ParamIn("IdPaymentGroup", m_idPayment);
                this.IUbsChannel.ParamIn("blnTerror", m_blnTerror);

                this.IUbsChannel.Run("Payment_Save");

                string strErrOut = this.IUbsChannel.ExistParamOut("StrError") ? Convert.ToString(this.IUbsChannel.ParamOut("StrError")) : string.Empty;
                if (string.IsNullOrEmpty(strErrOut))
                {
                    m_blnNeedRefreshGrid = true;
                    uciInfo.Text = MsgPaymentSavedDb;
                    uciInfo.Show();
                }

                if (!string.IsNullOrEmpty(strErrOut))
                {
                    MessageBox.Show(strErrOut, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    uciInfo.Show(strErrOut);
                }

                linkFIO.Enabled = false;
                tabPayment.SelectedTab = tabPageMain;
                m_blnSave = false;
                btnExit.Enabled = true;

                if (MessageBox.Show(MsgGroupContinue, CaptionGroupInput,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    m_idPayment = Convert.ToInt32(this.IUbsChannel.ParamOut("IdPaym"));
                    object paymentGroupId = this.IUbsChannel.ParamOut("PAYMENTGROUPID");
                    RunGroupContinuationScript(m_idPayment, paymentGroupId);
                }

                m_command = StrCommandGroupEdit;
                linkFIO.Enabled = true;
                cmbCode.Enabled = true;
            }
            catch (Exception ex)
            {
                btnExit.Enabled = true;
                btnSave.Enabled = true;
                m_blnSave = false;
                this.Ubs_ShowError(ex);
            }
        }

        private void BtnSaveAttribute_ClickImpl()
        {
            try
            {
                this.IUbsChannel.ParamIn("IdContract", m_idContract);

                if (string.IsNullOrEmpty(txtRecip.Text)
                    && string.IsNullOrEmpty(txtBic.Text)
                    && string.Equals(ucaAccKorr.Text, AccountPlaceholder, StringComparison.Ordinal)
                    && string.IsNullOrEmpty(txtNameBank.Text)
                    && string.Equals(ucaAccClient.Text, AccountPlaceholder, StringComparison.Ordinal)
                    && string.IsNullOrEmpty(txtINN.Text)
                    && string.IsNullOrEmpty(cmbPurpose.Text))
                {
                    MessageBox.Show(MsgRecipientRequisitesMissing, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    this.IUbsChannel.ParamIn("Наименование получателя", txtRecip.Text);
                    this.IUbsChannel.ParamIn("БИК", txtBic.Text);
                    this.IUbsChannel.ParamIn("Корр. счет", ucaAccKorr.Text);
                    this.IUbsChannel.ParamIn("Наименование банка", txtNameBank.Text);
                    this.IUbsChannel.ParamIn("Р/с", ucaAccClient.Text);
                    this.IUbsChannel.ParamIn("ИНН", txtINN.Text);
                    this.IUbsChannel.ParamIn("Назначение", cmbPurpose.Text);

                    this.IUbsChannel.Run("SaveAttributeRecip");

                    if (Convert.ToBoolean(this.IUbsChannel.ParamOut("bRetVal")))
                    {
                        MessageBox.Show(MsgRecipientRequisitesSaved, CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private bool CheckPayment()
        {
            try
            {
                if (!CheckLockPassport())
                {
                    return false;
                }

                if (m_intState == 1)
                {
                    MessageBox.Show(MsgNoRecipientContract, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabPayment.SelectedTab = tabPageMain;
                    cmbCode.Focus();

                    return false;
                }

                if (m_idContract == 0)
                {
                    MessageBox.Show(MsgNoRecipientContract, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabPayment.SelectedTab = tabPageMain;
                    cmbCode.Focus();

                    return false;
                }

                if (!CheckTerror())
                {
                    return false;
                }

                if (string.IsNullOrEmpty(txtFIOPay.Text))
                {
                    MessageBox.Show(MsgPayerFioRequired, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabPayment.SelectedTab = tabPageMain;
                    txtFIOPay.Focus();

                    return false;
                }

                if (!GetBankNameACC())
                {
                    tabPayment.SelectedTab = tabPageMain;
                    if (txtBic.Enabled)
                    {
                        txtBic.Focus();
                    }

                    return false;
                }

                this.IUbsChannel.ParamIn("BIC", txtBic.Text != null ? txtBic.Text.Trim() : string.Empty);
                this.IUbsChannel.ParamIn("ACC", ucaAccClient.Text);
                this.IUbsChannel.ParamIn("IdContract", m_idContract);
                this.IUbsChannel.Run("UtCheckAccFromBic");

                if (!Convert.ToBoolean(this.IUbsChannel.ParamOut("bRetVal")))
                {
                    MessageBox.Show(
                        Convert.ToString(this.IUbsChannel.ParamOut("strError")),
                        CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabPayment.SelectedTab = tabPageMain;
                    if (ucaAccClient.Enabled)
                    {
                        ucaAccClient.Focus();
                    }
                    else
                    {
                        cmbCode.Focus();
                    }

                    return false;
                }

                this.IUbsChannel.ParamIn("STRACC", ucaAccClient.Text);
                this.IUbsChannel.ParamIn("BIC", txtBic.Text);
                this.IUbsChannel.ParamIn("CORRACC", ucaAccKorr.Text);

                decimal sumPay = udcSumma.DecimalValue;
                if (sumPay == 0m)
                {
                    MessageBox.Show(MsgInvalidPaymentAmount, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    tabPayment.SelectedTab = tabPageMain;
                    udcSumma.Focus();

                    return false;
                }

                if (udcSummaTotal.DecimalValue < 0m)
                {
                    MessageBox.Show(MsgInvalidTotalAmount, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    tabPayment.SelectedTab = tabPageMain;
                    udcSumma.Focus();

                    return false;
                }

                this.IUbsChannel.Run("CheckAddFields");
                var addFieldsOut = this.IUbsChannel.ParamsOutParam;
                if (addFieldsOut.Contains("bRetVal")
                    && !Convert.ToBoolean(addFieldsOut.Value("bRetVal")))
                {
                    MessageBox.Show(
                        Convert.ToString(addFieldsOut.Value("StrError")),
                        CaptionCheckAddFieldsPrefix + CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabPayment.SelectedTab = tabPageAddProperties;
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
                this.IUbsChannel.ParamIn("NAME", txtFIOPay.Text);
                this.IUbsChannel.ParamIn("IDCONTRACT", m_idContract);
                this.IUbsChannel.ParamIn("PURPOSE", cmbPurpose.Text);
                this.IUbsChannel.ParamIn("RECIPIENTNAME", txtRecip.Text);
                this.IUbsChannel.Run("CheckTerror");

                var p = this.IUbsChannel.ParamsOutParam;
                if (!p.Contains("RETVAL"))
                {
                    return true;
                }

                if (Convert.ToBoolean(p.Value("RETVAL")))
                {
                    return true;
                }

                DialogResult dr = MessageBox.Show(
                    Convert.ToString(p.Value("StrError")),
                    CaptionValidation,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    m_blnTerror = true;
                    if (m_idClient == 0 && p.Contains("blnErrorPayer"))
                    {
                        tabPayment.SelectedTab = tabPageMain;
                        linkFIO.Focus();

                        MessageBox.Show(
                            MsgTerrorChooseClientFromDirectory,
                            CaptionValidation,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        OpenClientPicker();

                        return false;
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

        private bool UtCheckUserBeforeSave()
        {
            try
            {
                this.IUbsChannel.Run("UTListAddRead");
                var listOut = this.IUbsChannel.ParamsOutParam;

                if (string.Equals(m_command, StrCommandChangePart, StringComparison.Ordinal))
                {
                    this.IUbsChannel.ParamIn(ParamKeyIdPayment, m_idPayment);
                }

                this.IUbsChannel.ParamIn("IDCONTRACT", m_idContract);
                this.IUbsChannel.ParamIn("CODE", cmbCode.Text);
                this.IUbsChannel.ParamIn("INN", txtINN.Text);
                this.IUbsChannel.ParamIn("BIC", txtBic.Text);
                this.IUbsChannel.ParamIn("CORRACC", ucaAccKorr.Text);
                this.IUbsChannel.ParamIn("ACC", ucaAccClient.Text);
                this.IUbsChannel.ParamIn("IDCLIENT", m_idClient);
                this.IUbsChannel.ParamIn("PAYERFIO", txtFIOPay.Text);
                this.IUbsChannel.ParamIn("PAYERINN", txtINNPay.Text);
                this.IUbsChannel.ParamIn("PAYERADDRESS", txtAdressPay.Text);
                this.IUbsChannel.ParamIn("RECIPIENTNAME", txtRecip.Text);
                this.IUbsChannel.ParamIn("PURPOSE", cmbPurpose.Text);
                this.IUbsChannel.ParamIn("SUMMA", udcSumma.DecimalValue);
                this.IUbsChannel.ParamIn("SUMMARATESEND", udcSummaRateSend.DecimalValue);
                this.IUbsChannel.ParamIn("SUMMATOTAL", udcSummaTotal.DecimalValue);

                if (listOut.Contains("AddFields"))
                {
                    object raw = listOut.Value("AddFields");
                    if (raw is object[,])
                    {
                        object[,] arrAddFields = (object[,])raw;
                        int rows = arrAddFields.GetLength(0);
                        for (int row = 0; row < rows; row++)
                        {
                            string name = Convert.ToString(arrAddFields[row, 1]);
                            object val = arrAddFields[row, 7];
                            this.IUbsChannel.ParamIn(name, val);
                        }
                    }
                }

                this.IUbsChannel.Run("Ut_CheckBeforeSave");
                var userOut = this.IUbsChannel.ParamsOutParam;

                btnExit.Enabled = true;

                string strError = userOut.Contains("Error")
                    ? Convert.ToString(userOut.Value("Error"))
                    : string.Empty;
                bool blnRes = userOut.Contains("bRet")
                    && Convert.ToBoolean(userOut.Value("bRet"));

                if (!string.IsNullOrEmpty(strError) && blnRes)
                {
                    if (MessageBox.Show(strError, CaptionForm,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        return false;
                    }
                }
                else if (!blnRes)
                {
                    if (!string.IsNullOrEmpty(strError))
                    {
                        MessageBox.Show(strError, CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    btnSave.Enabled = false;

                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                m_blnSave = false;

                return false;
            }
        }

        private bool CheckLockPassport()
        {
            if (string.IsNullOrEmpty(m_strDocNumber.Trim())
                && string.IsNullOrEmpty(m_strDocSeries.Trim()))
            {
                return true;
            }

            try
            {
                return UbsComValidateLibrary.ValidateDocumentGISMU4Vb(this,
                base.IUbsChannel, m_idClient);
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return false;
            }
        }

        private bool CheckIPDL()
        {
            try
            {
                var ret = UbsComValidateLibrary.ValidateIDPLVb(this, base.IUbsChannel);

                if (ret == 0)
                {
                    m_blnIPDL = false;
                }
                else if (ret == 1)
                {
                    m_blnIPDL = true;
                }
                else
                {
                    m_blnIPDL = true;

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

        private void RunGroupContinuationScript(int idPaym, object paymentGroupId)
        {
            //var scripter = base.Ubs_VBScriptRunner();

            //try
            //{
            //    object[] keyArr = new object[] { idPaym };

            //    var paramIn = new UbsParam();

            //    paramIn.Value("Parent", base.IUbs);
            //    paramIn.Value("Key", keyArr);
            //    paramIn.Value("Идентификатор группового платежа", paymentGroupId);
            //    paramIn.Value("Идентификатор основного платежа", idPaym);

            //    scripter.LoadFiles(ScriptGroupContinuation);

            //    scripter.UbsScriptParam = paramIn;

            //    scripter.ExecuteScript();

            //    var paramOut = scripter.UbsScriptParam;

            //    bool endGroup = Convert.ToBoolean(paramOut.Value("EndGroup"));

            //    if (endGroup)
            //    {
            //        btnSave.Enabled = false;
            //    }
            //    else
            //    {
            //        btnSave.Enabled = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.Ubs_ShowError(ex);
            //    btnSave.Enabled = true;
            //}
        }

        private static void InvokeScriptParameterSet(Type scriptType, object script, string name, object value)
        {
            scriptType.InvokeMember(
                "Parameter",
                BindingFlags.SetProperty,
                null,
                script,
                new object[] { name, value });
        }

        private static object InvokeScriptParameterGet(Type scriptType, object script, string name)
        {
            return scriptType.InvokeMember(
                "Parameter",
                BindingFlags.GetProperty,
                null,
                script,
                new object[] { name });
        }
    }
}
