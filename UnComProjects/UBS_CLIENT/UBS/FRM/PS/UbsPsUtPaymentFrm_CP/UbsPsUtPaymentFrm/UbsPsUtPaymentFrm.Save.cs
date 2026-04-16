using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    public partial class UbsPsUtPaymentFrm
    {
        private void BtnSave_ClickImpl()
        {
            try
            {
                if (!this.ValidateChildren())
                {
                    return;
                }

                m_isSave = true;
                btnExit.Enabled = false;
                btnSave.Enabled = false;

                if (!CheckPayment())
                {
                    RestoreSaveUiState();
                    return;
                }

                if (!CheckIPDL())
                {
                    MessageBox.Show(MsgIpdlSaveForbidden, CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RestoreSaveUiState();
                    return;
                }

                if (!UtCheckUserBeforeSave())
                {
                    RestoreSaveUiState();
                    return;
                }

                Payment_Save();
            }
            catch (Exception ex)
            {
                RestoreSaveUiState();
                this.Ubs_ShowError(ex);
            }
        }

        private void BtnSaveAttribute_ClickImpl()
        {
            try
            {
                if (m_idContract == 0)
                {
                    MessageBox.Show(MsgRecipientContractRequired, CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.IUbsChannel.ParamIn("IdContract", m_idContract);

                this.IUbsChannel.Run("SaveAttributeRecip");

                var paramOutSaveAttributeRecip = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                if (paramOutSaveAttributeRecip.GetParamOutBool("bRetVal"))
                {
                    MessageBox.Show(MsgRecipientAttributesSaved, CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string strError = paramOutSaveAttributeRecip.GetParamOutString("StrError");
                    if (!string.IsNullOrEmpty(strError))
                    {
                        MessageBox.Show(strError, CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void Payment_Save()
        {
            try
            {
                this.UbsChannel_ParamIn("COMMANDIN", m_command);
                this.UbsChannel_ParamIn("StrCommand", string.IsNullOrEmpty(m_command) ? StrCommandAdd : m_command);
                this.UbsChannel_ParamIn("IdContract", m_idContract);
                this.UbsChannel_ParamIn("ID_CLIENT", m_idClient);
                this.UbsChannel_ParamIn("blnIPDL", m_isIPDL);
                this.UbsChannel_ParamIn("blnTerror", m_isTerror);

                this.UbsChannel_Run("Payment_Save");

                var paramOutPaymentSave = new UbsParamCustom(this.UbsChannel_ParamsOut);

                string strError = paramOutPaymentSave.GetParamOutString("StrError");
                if (!string.IsNullOrEmpty(strError))
                {
                    MessageBox.Show(strError, CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RestoreSaveUiState();
                    return;
                }

                m_idPayment = paramOutPaymentSave.GetParamOutInt("IdPaym");

                this.uciInfo.Show(MsgPaymentSavedDb);

                RestoreSaveUiState();
            }
            catch (Exception ex)
            {
                RestoreSaveUiState();
                this.Ubs_ShowError(ex);
            }
        }

        private bool UtCheckUserBeforeSave()
        {
            try
            {
                this.IUbsChannel.ParamIn("IDCONTRACT", m_idContract);
                this.IUbsChannel.ParamIn("IDCLIENT", m_idClient);
                this.IUbsChannel.Run("Ut_CheckBeforeSave");

                var paramOutUtCheckBeforeSave = new UbsParamCustom(this.UbsChannel_ParamsOut);
                string strError = paramOutUtCheckBeforeSave.GetParamOutString("Error");
                bool bRet = paramOutUtCheckBeforeSave.GetParamOutBool("bRet");

                if (!bRet)
                {
                    if (!string.IsNullOrEmpty(strError))
                    {
                        MessageBox.Show(strError, CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    return false;
                }

                if (!string.IsNullOrEmpty(strError))
                {
                    return MessageBox.Show(strError, CaptionForm, MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                        == DialogResult.Yes;
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
                {
                    return false;
                }

                if (m_idContract == 0)
                {
                    MessageBox.Show(MsgRecipientContractRequired, CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (!CheckTerror())
                {
                    return false;
                }

                if (!CheckAddFields())
                {
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

        private bool CheckAddFields()
        {
            try
            {
                this.IUbsChannel.Run("CheckAddFields");

                var paramOutCheckAddFields = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                if (!paramOutCheckAddFields.GetParamOutBool("bRetVal"))
                {
                    string strError = paramOutCheckAddFields.GetParamOutString("StrError");
                    if (!string.IsNullOrEmpty(strError))
                    {
                        MessageBox.Show(strError, CaptionCheck, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        //проверка
        private bool CheckTerror()
        {
            try
            {
                this.IUbsChannel.ParamIn("IDCONTRACT", m_idContract);
                this.IUbsChannel.ParamIn("RECIPIENTNAME", string.Empty);
                this.IUbsChannel.Run("CheckTerror");

                var paramOutCheckTerror = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                if (!paramOutCheckTerror.Contains("RETVAL"))
                {
                    m_isTerror = false;
                    return true;
                }

                bool bRet = paramOutCheckTerror.GetParamOutBool("RETVAL");
                if (bRet)
                {
                    m_isTerror = false;
                    return true;
                }

                string strError = paramOutCheckTerror.GetParamOutString("StrError");
                if (string.IsNullOrEmpty(strError))
                {
                    return false;
                }

                m_isTerror = MessageBox.Show(
                    strError,
                    CaptionValidation,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK;

                return m_isTerror;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return false;
            }
        }

        private bool CheckLockPassport()
        {
            return true;
        }

        private bool CheckIPDL()
        {
            m_isIPDL = false;
            return true;
        }

        private bool CanCloseForm()
        {
            return !m_isSave;
        }

        private void RestoreSaveUiState()
        {
            m_isSave = false;
            btnExit.Enabled = true;
            btnSave.Enabled = true;
        }
    }
}
