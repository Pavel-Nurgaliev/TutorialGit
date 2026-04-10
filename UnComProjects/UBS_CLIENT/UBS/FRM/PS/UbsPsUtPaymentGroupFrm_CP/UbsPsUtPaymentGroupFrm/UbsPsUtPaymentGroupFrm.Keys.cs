using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    public partial class UbsPsUtPaymentGroupFrm
    {

        private bool ProcessEnterKey()
        {
            if (udcSumma.ContainsFocus)
            {
                OnEnterFromSumma();
                return true;
            }

            if (ucaAccClient.ContainsFocus)
            {
                OnEnterFromSettlementAccount();
                return true;
            }

            Control c = this.ActiveControl;
            if (c != null && !(c is Button))
            {
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
                return true;
            }

            return false;
        }

        private void OnEnterFromSumma()
        {
            if (tabPageAddProperties.Enabled
                && ucfAddProperties.Controls.Count > 0)
            {
                tabPayment.SelectedTab = tabPageAddProperties;
                ucfAddProperties.Focus();
            }
            else
            {
                btnSave.Focus();
            }
        }

        private void OnEnterFromSettlementAccount()
        {
            if (!CheckRS())
            {
                return;
            }

            if (m_idContract == 0)
            {
                return;
            }

            this.IUbsChannel.ParamIn("ACC", ucaAccClient.Text);
            this.IUbsChannel.ParamIn("BIC", txtBic.Text);
            this.IUbsChannel.ParamIn("IdContract", m_idContract);
            this.IUbsChannel.Run("UtGetINNFromLastPayment");

            if (txtINN.Enabled)
            {
                txtINN.Text = Convert.ToString(this.IUbsChannel.ParamOut("INN"));
            }

            if (cmbPurpose.Enabled)
            {
                string purpose = Convert.ToString(this.IUbsChannel.ParamOut("PURPOSE"));
                if (!IsComboBoxItemExists(cmbPurpose, purpose)
                    && string.IsNullOrEmpty(purpose.Trim()))
                {
                    cmbPurpose.Items.Add(purpose);
                    cmbPurpose.SelectedIndex = cmbPurpose.Items.Count - 1;
                }
            }

            string idPaymentOut = Convert.ToString(this.IUbsChannel.ParamOut("IDPAYMENT"));
            if (!string.IsNullOrEmpty(idPaymentOut))
            {
                this.IUbsChannel.ParamIn("IDPAYMENT", idPaymentOut);
                this.IUbsChannel.Run("UtGetKPPUPayerLastPayment");
                txtRecip.Text = Convert.ToString(this.IUbsChannel.ParamOut("Recip"));
                ucfAddProperties.Refresh();
            }

            this.IUbsChannel.ParamIn("ACC", ucaAccClient.Text);
            this.SelectNextControl(this.ActiveControl, true, true, true, true);
        }

        private bool CheckRS()
        {
            try
            {
                this.IUbsChannel.Run("UtReadOurBankBik");
                string strOurBankBik = Convert.ToString(this.IUbsChannel.ParamOut("strOurBankBik"));
                if (string.Equals(ucaAccClient.Text, AccountPlaceholder, StringComparison.Ordinal)
                    && string.Equals(strOurBankBik.Trim(), txtBic.Text.Trim(), StringComparison.Ordinal))
                {
                    MessageBox.Show(MsgInvalidSettlementAccount, CaptionForm,
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
                this.IUbsChannel.Run("CheckKey");

                var ck = this.IUbsChannel.ParamsOutParam;
                bool retVal = ck.Contains("RETVAL") && Convert.ToBoolean(ck.Value("RETVAL"));
                if (!retVal)
                {
                    MessageBox.Show(MsgInvalidAccountKey, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabPayment.SelectedTab = tabPageMain;
                    ucaAccClient.Focus();
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

        private static bool IsComboBoxItemExists(ComboBox cmb, string item)
        {
            if (cmb == null || item == null)
            {
                return false;
            }

            for (int i = 0; i < cmb.Items.Count; i++)
            {
                if (string.Equals(Convert.ToString(cmb.Items[i]), item, StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }

        private bool ProcessEscapeKey()
        {
            if (txtFIOPay.ContainsFocus)
            {
                btnExit.Focus();
                return true;
            }

            if (tabPayment.SelectedTab == tabPageAddProperties && tabPageAddProperties.Enabled)
            {
                tabPayment.SelectedTab = tabPageMain;
                udcSumma.Focus();
                return true;
            }

            Control c = this.ActiveControl;
            if (c != null)
            {
                this.SelectNextControl(this.ActiveControl, false, true, true, true);
                return true;
            }

            return false;
        }

        private void txtNomerCardPay_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode != Keys.Enter || e.Modifiers != Keys.None)
                {
                    return;
                }

                e.Handled = true;
                e.SuppressKeyPress = true;

                if (string.IsNullOrEmpty(txtNomerCardPay.Text.Trim()))
                {
                    return;
                }

                this.IUbsChannel.ParamIn("NomerCard", txtNomerCardPay.Text);
                this.IUbsChannel.ParamIn("IsGuest", m_blnGuest);
                this.IUbsChannel.Run("ReadClientFromNomerCard");

                string strErr = Convert.ToString(this.IUbsChannel.ParamOut("StrError"));
                if (!string.IsNullOrEmpty(strErr.Trim()))
                {
                    MessageBox.Show(strErr, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNomerCardPay.Focus();
                    return;
                }

                object idClient = this.IUbsChannel.ParamOut("IDCLIENT");
                this.IUbsChannel.ParamIn("IDCLIENT", idClient);
                this.IUbsChannel.ParamIn("IsGuest", m_blnGuest);
                this.IUbsChannel.Run("ReadClientFromIdOC");

                strErr = Convert.ToString(this.IUbsChannel.ParamOut("StrError"));
                if (!string.IsNullOrEmpty(strErr.Trim()))
                {
                    MessageBox.Show(strErr, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                m_idClient = Convert.ToInt32(idClient);
                txtINNPay.Text = Convert.ToString(this.IUbsChannel.ParamOut("INN"));
                txtInfoClient.Text = Convert.ToString(this.IUbsChannel.ParamOut("InfoClient"));
                txtFIOPay.Text = Convert.ToString(this.IUbsChannel.ParamOut("NAME"));
                txtAdressPay.Text = Convert.ToString(this.IUbsChannel.ParamOut("ADRESS"));

                m_strTypeDoc = Convert.ToString(this.IUbsChannel.ParamOut("TypeDoc"));
                m_strDocNumber = Convert.ToString(this.IUbsChannel.ParamOut("NUMBER"));
                m_strDocSeries = Convert.ToString(this.IUbsChannel.ParamOut("SERIES"));

                if (string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal))
                {
                    cmbCode.Focus();
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void txtINN_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                ApplyDigitsOnlyKeyDown(sender as TextBox, e);
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void txtINNPay_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                ApplyDigitsOnlyKeyDown(sender as TextBox, e);
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private static void ApplyDigitsOnlyKeyDown(TextBox box, KeyEventArgs e)
        {
            if (box == null)
            {
                return;
            }

            if (e.Control || e.Alt)
            {
                return;
            }

            Keys k = e.KeyCode;
            bool allowed =
                (k >= Keys.D0 && k <= Keys.D9)
                || (k >= Keys.NumPad0 && k <= Keys.NumPad9)
                || k == Keys.Back
                || k == Keys.Delete
                || k == Keys.Left
                || k == Keys.Right
                || k == Keys.Home
                || k == Keys.End
                || k == Keys.Tab
                || k == Keys.Enter;

            if (!allowed)
            {
                e.SuppressKeyPress = true;
            }
        }
        private void ucaAccClient_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbCode.SelectedIndex != -1)
                {
                    return;
                }

                bool hasBic = !string.IsNullOrEmpty(txtBic.Text.Trim());
                bool hasAcc = !string.IsNullOrEmpty(ucaAccClient.Text.Trim())
                    && !string.Equals(ucaAccClient.Text, AccountPlaceholder, StringComparison.Ordinal);
                if (!hasBic && !hasAcc)
                {
                    return;
                }

                this.IUbsChannel.ParamIn("BIC", txtBic.Text.Trim());
                this.IUbsChannel.ParamIn("ACC", ucaAccClient.Text);
                this.IUbsChannel.ParamIn("INN", txtINN.Text);
                this.IUbsChannel.Run("FindContrByBicAndAccount");

                int recCount = Convert.ToInt32(this.IUbsChannel.ParamOut("RecCount"));
                object idArr = this.IUbsChannel.ParamOut("IdArray");

                if (recCount == 1 && idArr is object[,])
                {
                    object[,] arr = (object[,])idArr;
                    m_idContract = Convert.ToInt32(arr[0, 0]);
                    FindContractbyId();

                    if (cmbPurpose.Enabled && cmbPurpose.Visible)
                    {
                        cmbPurpose.Focus();
                    }
                    else if (udcSumma.Enabled && udcSumma.Visible)
                    {
                        udcSumma.Focus();
                    }
                }
                else if (recCount > 1)
                {
                    cmbCode.Focus();
                }
                else if (recCount == 0)
                {
                    m_idContract = 0;
                    cmbCode.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }
    }
}
