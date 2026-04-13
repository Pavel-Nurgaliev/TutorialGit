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

                m_blnSave = true;
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

                if (this.IUbsChannel.ExistParamOut("bRetVal")
                    && Convert.ToBoolean(this.IUbsChannel.ParamOut("bRetVal")))
                {
                    MessageBox.Show(MsgRecipientAttributesSaved, CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string strError = GetParamOutString("StrError");
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
                this.IUbsChannel.LoadResource = LoadResource;
                this.IUbsChannel.ParamIn("COMMANDIN", m_command);
                this.IUbsChannel.ParamIn("StrCommand", string.IsNullOrEmpty(m_command) ? StrCommandAdd : m_command);
                this.IUbsChannel.ParamIn("IdContract", m_idContract);
                this.IUbsChannel.ParamIn("ID_CLIENT", m_idClient);
                this.IUbsChannel.ParamIn("blnIPDL", m_blnIPDL);
                this.IUbsChannel.ParamIn("blnTerror", m_blnTerror);
                this.IUbsChannel.Run("Payment_Save");

                string strError = GetParamOutString("StrError");
                if (!string.IsNullOrEmpty(strError))
                {
                    MessageBox.Show(strError, CaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RestoreSaveUiState();
                    return;
                }

                if (this.IUbsChannel.ExistParamOut("IdPaym") && this.IUbsChannel.ParamOut("IdPaym") != null)
                {
                    m_idPayment = Convert.ToInt32(this.IUbsChannel.ParamOut("IdPaym"));
                }

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

                string strError = GetParamOutString("Error");
                bool bRet = !this.IUbsChannel.ExistParamOut("bRet")
                    || Convert.ToBoolean(this.IUbsChannel.ParamOut("bRet"));

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
                if (this.IUbsChannel.ExistParamOut("bRetVal")
                    && !Convert.ToBoolean(this.IUbsChannel.ParamOut("bRetVal")))
                {
                    string strError = GetParamOutString("StrError");
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
                this.IUbsChannel.ParamIn("IDCONTRACT", m_idContract) ;
                this.IUbsChannel.ParamIn("RECIPIENTNAME", string.Empty);
                this.IUbsChannel.Run("CheckTerror");

                if (!this.IUbsChannel.ExistParamOut("RETVAL"))
                {
                    m_blnTerror = false;
                    return true;
                }

                bool bRet = Convert.ToBoolean(this.IUbsChannel.ParamOut("RETVAL"));
                if (bRet)
                {
                    m_blnTerror = false;
                    return true;
                }

                string strError = GetParamOutString("StrError");
                if (string.IsNullOrEmpty(strError))
                {
                    return false;
                }

                m_blnTerror = MessageBox.Show(
                    strError,
                    CaptionValidation,
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.OK;

                return m_blnTerror;
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
            m_blnIPDL = false;
            return true;
        }

        private bool CanCloseForm()
        {
            return !m_blnSave;
        }

        private void RestoreSaveUiState()
        {
            m_blnSave = false;
            btnExit.Enabled = true;
            btnSave.Enabled = true;
        }
    }
}
