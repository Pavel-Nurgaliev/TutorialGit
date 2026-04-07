using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    public partial class UbsPsContractFrm
    {
        private void UbsPsContractFrm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape && e.Modifiers == Keys.None)
                {
                    if (TryHandleEscapeTabNavigation())
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    return;
                }
                if (e.KeyCode != Keys.Enter || e.Modifiers != Keys.None)
                {
                    return;
                }
                if (!txtRecipientBik.ContainsFocus)
                {
                    return;
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
                OnRecipientBikEnterKey();
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private bool TryHandleEscapeTabNavigation()
        {
            if (tabContract.SelectedTab == tabPageAddFields)
            {
                tabContract.SelectedTab = tabPageCommission;
                if (udcRecipientCommissionPercent.Enabled)
                {
                    udcRecipientCommissionPercent.Focus();
                }
                else
                {
                    cmbRecipientCommissionType.Focus();
                }
                return true;
            }
            if (tabContract.SelectedTab == tabPageCommission)
            {
                tabContract.SelectedTab = tabPageMain;
                return true;
            }
            return false;
        }

        private void ucfAdditionalFields_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\u001b')
                {
                    if (TryHandleEscapeTabNavigation())
                    {
                        e.Handled = true;
                    }
                    return;
                }
                if (e.KeyChar == '\r')
                {
                    btnSave.Focus();
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }
    }
}
