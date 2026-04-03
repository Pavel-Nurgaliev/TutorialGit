using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    public partial class UbsPsContractFrm
    {
        private void EnableSumCommissionControls()
        {
            try
            {
                ApplyPayerCommissionPercentEnabledState();
                ApplyRecipientCommissionPercentEnabledState();
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void ApplyPayerCommissionPercentEnabledState()
        {
            int idx = cmbPayerCommissionType.SelectedIndex;
            if (idx < 0)
            {
                udcPayerCommissionPercent.Enabled = false;
                return;
            }
            if (idx == CommissionComboIndexDisablePercentFirst || idx == CommissionComboIndexDisablePercentSecond)
            {
                udcPayerCommissionPercent.Enabled = false;
                udcPayerCommissionPercent.DecimalValue = 0m;
            }
            else
            {
                udcPayerCommissionPercent.Enabled = true;
            }
        }

        private void ApplyRecipientCommissionPercentEnabledState()
        {
            int idx = cmbRecipientCommissionType.SelectedIndex;
            if (idx < 0)
            {
                udcRecipientCommissionPercent.Enabled = false;
                return;
            }
            if (idx == CommissionComboIndexDisablePercentFirst || idx == CommissionComboIndexDisablePercentSecond)
            {
                udcRecipientCommissionPercent.Enabled = false;
                udcRecipientCommissionPercent.DecimalValue = 0m;
            }
            else
            {
                udcRecipientCommissionPercent.Enabled = true;
            }
        }

        private void cmbPayerCommissionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableSumCommissionControls();
        }

        private void cmbRecipientCommissionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableSumCommissionControls();
        }
    }
}
