using System;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    public partial class UbsPsUtPaymentFrm
    {

        #region Cash symbol dialog

        private void BtnCashSymb_ClickImpl()
        {
            try
            {
                if (udcPaymentAmount.DecimalValue == 0)
                {
                    MessageBox.Show(MsgPaymentAmountEmpty, CaptionError,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (m_arrDataTypeCashSymbol == null)
                {
                    MessageBox.Show(MsgCashSymbolTypeEmpty, CaptionError,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var frm = new FrmCashSymb())
                {
                    frm.UbsChannelRef = this.IUbsChannel;
                    frm.CashSymbolsSource = m_arrCashSymb;
                    frm.AllowedCashSymbols = m_arrDataTypeCashSymbol;
                    frm.ExpectedTotal = udcPaymentAmount.DecimalValue + udcPenaltyAmount.DecimalValue;

                    if (frm.ShowDialog(this) == DialogResult.OK && frm.IsConfirmed)
                    {
                        m_arrCashSymb = frm.CashSymbolsResult;
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region Calculator dialog

        private void BtnCalc_ClickImpl()
        {
            try
            {
                btnCalc.Enabled = false;

                this.IUbsChannel.Run("InitDialogCalc");

                var paramOutInit = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                if (!paramOutInit.GetParamOutBool("bRetVal"))
                {
                    btnCalc.Enabled = true;
                    string err = paramOutInit.GetParamOutString("StrError");
                    MessageBox.Show(err, CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtPayerInn.Text.Length > 0 && txtPayerInn.Text != "0")
                {
                    if (!CheckKeyInn(txtPayerInn.Text))
                    {
                        MessageBox.Show(
                            string.Format(MsgInnPayerCheckFailed, txtPayerInn.Text),
                            CaptionPaymentAccept,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnCalc.Enabled = true;
                        return;
                    }
                }

                if (txtRecipientInn.Text.Length > 0)
                {
                    if (!CheckKeyInn(txtRecipientInn.Text))
                    {
                        MessageBox.Show(
                            string.Format(MsgInnRecipientCheckFailed, txtRecipientInn.Text),
                            CaptionPaymentAccept,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnCalc.Enabled = true;
                        return;
                    }
                }

                btnCalc.Enabled = true;

                if (!paramOutInit.GetParamOutBool("bInitDialog"))
                    return;

                decimal sumEc = paramOutInit.GetParamOutDecimal("Summa");

                using (var frmCalc = new FrmCalc())
                {
                    frmCalc.PaymentAmount = sumEc;

                    if (frmCalc.ShowDialog(this) != DialogResult.OK || !frmCalc.IsConfirmed)
                        return;

                    this.IUbsChannel.Run("GetGlobal_ParamBaseCurrency");
                    var paramOutCur = new UbsParamCustom(this.IUbsChannel.ParamsOut);
                    int idCurrency = paramOutCur.GetParamOutInt("Идентификатор базовой валюты");

                    RunEcOperation("DB", sumEc, idCurrency);

                    this.IUbsChannel.Run("PutAddFlCalc");
                    var paramOutCalc = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                    if (paramOutCalc.GetParamOutBool("bRetVal"))
                    {
                        NewRecordCalc();

                        udcCommonAmount.DecimalValue = 0;
                        udcTotalAmount.DecimalValue = 0;

                        m_idClient = 0;
                        linkFindFilter.Visible = false;
                        txtPayerInn.Text = string.Empty;
                        txtPayerFullName.Text = string.Empty;
                        txtPayerAddress.Text = string.Empty;
                        txtPayerClientInfo.Text = string.Empty;

                        MessageBox.Show(MsgCalcDoneInfo, CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string err = paramOutCalc.GetParamOutString("StrError");
                        MessageBox.Show(err, CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion
    }
}
