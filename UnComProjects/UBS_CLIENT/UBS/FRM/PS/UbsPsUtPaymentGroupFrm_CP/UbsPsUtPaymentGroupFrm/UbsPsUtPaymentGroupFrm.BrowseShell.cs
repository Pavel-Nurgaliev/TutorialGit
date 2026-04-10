using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    public partial class UbsPsUtPaymentGroupFrm
    {
        private enum BrowseListKind
        {
            None,
            ClientNonGuest,
            ClientGuest,
            ReceiverAttribute
        }

        private BrowseListKind m_browseListKind;

        private void UbsPsUtPaymentGroupFrm_Ubs_ActionRunBegin(object sender, UbsActionRunEventArgs args)
        {
            try
            {
                if (m_browseListKind == BrowseListKind.ClientNonGuest
                    && string.Equals(args.Action, ActionListCommonClient, StringComparison.Ordinal))
                {
                    args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                        new KeyValuePair<string, object>("наименование", "Тип"),
                        new KeyValuePair<string, object>("значение по умолчанию", 2),
                        new KeyValuePair<string, object>("условие по умолчанию", "="),
                        new KeyValuePair<string, object>("скрытый", true) }));
                    args.IUbs.Run("UbsItemsRefresh", null);
                    return;
                }

                if (m_browseListKind == BrowseListKind.ReceiverAttribute
                    && string.Equals(args.Action, ActionListReceiver, StringComparison.Ordinal))
                {
                    args.IUbs.Run("UbsItemsRemove", null);
                    args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                        new KeyValuePair<string, object>("наименование", "Идентификатор договора"),
                        new KeyValuePair<string, object>("значение по умолчанию", m_idContract),
                        new KeyValuePair<string, object>("условие по умолчанию", "="),
                        new KeyValuePair<string, object>("скрытый", true) }));
                    args.IUbs.Run("UbsItemsRefresh", null);
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void OpenClientPicker()
        {
            string action = m_blnGuest ? ActionListOcClients : ActionListCommonClient;
            m_browseListKind = m_blnGuest ? BrowseListKind.ClientGuest : BrowseListKind.ClientNonGuest;
            try
            {
                object[] ids = this.Ubs_ActionRun(action, this, true) as object[];
                if (ids != null && ids.Length > 0)
                {
                    ApplySelectedClientFromBrowse(Convert.ToInt32(ids[0]));
                }
            }
            finally
            {
                m_browseListKind = BrowseListKind.None;
            }
        }

        private void BtnListAttributeRecip_ClickImpl()
        {
            if (m_idContract == 0)
            {
                MessageBox.Show(MsgNoRecipientContract, CaptionValidation,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            m_browseListKind = BrowseListKind.ReceiverAttribute;
            try
            {
                object[] ids = this.Ubs_ActionRun(ActionListReceiver, this, true) as object[];
                if (ids != null && ids.Length > 0)
                {
                    ApplySelectedReceiverAttributeFromBrowse(Convert.ToInt32(ids[0]));
                }
            }
            finally
            {
                m_browseListKind = BrowseListKind.None;
            }
        }

        private void ApplySelectedClientFromBrowse(int idClient)
        {
            m_idClient = idClient;

            this.IUbsChannel.ParamIn("IDCLIENT", m_idClient);
            this.IUbsChannel.ParamIn("IsGuest", m_blnGuest);
            this.IUbsChannel.Run("ReadClientFromIdOC");

            string strErr = this.IUbsChannel.ExistParamOut("StrError") ? Convert.ToString(this.IUbsChannel.ParamOut("StrError")) : string.Empty;

            if (!string.IsNullOrEmpty(strErr.Trim()))
            {
                MessageBox.Show(strErr, "ReadClientFromIdOC " + CaptionForm,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtINNPay.Text = Convert.ToString(this.IUbsChannel.ParamOut("INN"));
            txtInfoClient.Text = Convert.ToString(this.IUbsChannel.ParamOut("InfoClient"));
            txtFIOPay.Text = Convert.ToString(this.IUbsChannel.ParamOut("NAME"));
            txtAdressPay.Text = Convert.ToString(this.IUbsChannel.ParamOut("ADRESS"));
            txtNomerCardPay.Text = string.Empty;

            m_strTypeDoc = Convert.ToString(this.IUbsChannel.ParamOut("TypeDoc"));
            m_strDocNumber = Convert.ToString(this.IUbsChannel.ParamOut("NUMBER"));
            m_strDocSeries = Convert.ToString(this.IUbsChannel.ParamOut("SERIES"));

            if (string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal))
            {
                cmbCode.Focus();
            }
        }

        private void ApplySelectedReceiverAttributeFromBrowse(int idAttributeRecip)
        {
            m_idAttributeRecip = idAttributeRecip;

            this.IUbsChannel.ParamIn("IdAttributeRecip", m_idAttributeRecip);
            this.IUbsChannel.Run("ReadRecipFromId");

            txtRecip.Text = Convert.ToString(this.IUbsChannel.ParamOut("Наименование получателя в плат. документах"));
            txtBic.Text = Convert.ToString(this.IUbsChannel.ParamOut("BIC"));
            ucaAccKorr.Text = Convert.ToString(this.IUbsChannel.ParamOut("CORRACC"));
            txtNameBank.Text = Convert.ToString(this.IUbsChannel.ParamOut("Наименование банка"));
            ucaAccClient.Text = Convert.ToString(this.IUbsChannel.ParamOut("ACC"));
            txtINN.Text = Convert.ToString(this.IUbsChannel.ParamOut("INN"));
            cmbPurpose.Text = Convert.ToString(this.IUbsChannel.ParamOut("PURPOSE"));

            ucfAddProperties.Refresh();
        }
    }
}
