using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    public partial class UbsPsUtPaymentFrm
    {
        private void UbsPsUtPaymentFrm_Load(object sender, EventArgs e)
        {
            if (m_blnInitialized)
            {
                return;
            }

            m_blnInitialized = true;
            InitDoc();
        }

        private void InitDoc()
        {
            try
            {
                this.IUbsChannel.LoadResource = LoadResource;

                base.UbsInit();

                this.IUbsChannel.ParamIn("StrCommand", m_command);
                this.IUbsChannel.ParamIn("IdPayment", m_idPayment);
                this.IUbsChannel.Run("InitForm");

                string strError = GetParamOutString("StrError");
                if (!string.IsNullOrEmpty(strError))
                {
                    MessageBox.Show(strError, CaptionCheck, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                m_strOurBankBic = GetParamOutString("strOurBankBik");
                if (this.IUbsChannel.ExistParamOut("DateBeg") && this.IUbsChannel.ParamOut("DateBeg") != null)
                {
                    m_dateBeg = Convert.ToDateTime(this.IUbsChannel.ParamOut("DateBeg"));
                }
                if (this.IUbsChannel.ExistParamOut("DateEnd") && this.IUbsChannel.ParamOut("DateEnd") != null)
                {
                    m_dateEnd = Convert.ToDateTime(this.IUbsChannel.ParamOut("DateEnd"));
                }

                ApplyInitialFormState();

                if (string.Equals(m_command, StrCommandRead, StringComparison.Ordinal)
                    || string.Equals(m_command, StrCommandView, StringComparison.Ordinal)
                    || string.Equals(m_command, StrCommandCopy, StringComparison.Ordinal))
                {
                    ReadContract();
                }
                else if (string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal)
                    || string.Equals(m_command, StrCommandClear, StringComparison.Ordinal)
                    || string.IsNullOrEmpty(m_command))
                {
                    this.IUbsChannel.ParamIn("StrCommand", StrCommandClear);
                    this.IUbsChannel.Run("PAYMENT");
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void ApplyInitialFormState()
        {
            this.Text = CaptionForm;
        }

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
                this.IUbsChannel.Run("Payment");

                m_idContract = GetParamOutInt("IdContract");
                m_idClient = GetParamOutInt("IdClient");

                FillDataPayment(this.IUbsChannel.ExistParamOut("arrDataPayment")
                    ? this.IUbsChannel.ParamOut("arrDataPayment")
                    : null);
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void FindContract()
        {
            try
            {
                this.IUbsChannel.ParamIn("BIC", string.Empty);
                this.IUbsChannel.ParamIn("ACC", string.Empty);
                this.IUbsChannel.ParamIn("INN", string.Empty);
                this.IUbsChannel.Run("FindContrByBicAndAccount");
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void FindContractbyId()
        {
            try
            {
                if (m_idContract <= 0)
                {
                    return;
                }

                this.IUbsChannel.ParamIn("IdContract", m_idContract);
                this.IUbsChannel.Run("UtReadContract");
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void FillDataPayment(object arrDataPayment)
        {
            if (arrDataPayment == null)
            {
                return;
            }
        }

        private void FillTariff(object arrTariff)
        {
            if (arrTariff == null)
            {
                return;
            }
        }

        private void FillPayer()
        {
        }

        private void FillPurpose(object arrPurpose)
        {
            if (arrPurpose == null)
            {
                return;
            }
        }

        private string GetParamOutString(string key)
        {
            if (!this.IUbsChannel.ExistParamOut(key) || this.IUbsChannel.ParamOut(key) == null)
            {
                return string.Empty;
            }

            return Convert.ToString(this.IUbsChannel.ParamOut(key));
        }

        private int GetParamOutInt(string key)
        {
            if (!this.IUbsChannel.ExistParamOut(key) || this.IUbsChannel.ParamOut(key) == null)
            {
                return 0;
            }

            return Convert.ToInt32(this.IUbsChannel.ParamOut(key));
        }
    }
}
