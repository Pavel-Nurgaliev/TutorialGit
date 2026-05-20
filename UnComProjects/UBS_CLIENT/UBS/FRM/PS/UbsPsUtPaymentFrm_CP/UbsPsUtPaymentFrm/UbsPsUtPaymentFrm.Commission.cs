using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    public partial class UbsPsUtPaymentFrm
    {
        private const int TimerInterval = 250;
        private const string CaptionCalcCommission = "–асчет комиссий. ";
        private const string CaptionCalcNds = "–асчет Ќƒ—. ";
        private const string MsgEcOperationFailed = "–асчет с клиентом не произведен! ¬озникли проблемы взаимодействи€ с электронным кассиром.";

        /// <summary>
        /// Wires commission-related events from the constructor.
        /// </summary>
        private void WireCommissionEvents()
        {
            timer1.Interval = TimerInterval;
            timer1.Enabled = false;
            timer1.Tick += timer1_Tick;

            udcPaymentAmount.TextChanged += udcPaymentAmount_TextChanged;
            udcPenaltyAmount.TextChanged += udcPenaltyAmount_TextChanged;
        }

        #region Amount change handlers

        /// <summary>
        /// Payment amount changed Ч resets and restarts the debounce timer.
        /// VB6: curSumma_TextChange (lines 4705Ц4709).
        /// </summary>
        private void udcPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Enabled = true;
        }

        /// <summary>
        /// Penalty amount changed Ч triggers immediate commission recalculation.
        /// VB6: curPeny_TextChange (lines 2440Ц2445).
        /// </summary>
        private void udcPenaltyAmount_TextChanged(object sender, EventArgs e)
        {
            if (m_idContract > 0)
            {
                m_prefCalcRate = "_2 1";
                CalcSumCommiss_2();
            }
        }

        #endregion

        #region Timer Ч debounced recalculation

        /// <summary>
        /// Fires after 250ms of no payment-amount changes.
        /// Calls CalcSumCommiss_2 for client-side commission recalculation.
        /// VB6: Timer1_Timer (lines 2448Ц2460).
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;

                if (m_idContract > 0)
                {
                    m_prefCalcRate = "_2 2 Timer";
                    CalcSumCommiss_2();
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region CalcSumCommiss Ч server-side commission via UtCalcSumCommiss

        /// <summary>
        /// Full server-side commission calculation via UtCalcSumCommiss channel.
        /// Sets m_curSumRateRec, m_curSumRec, udcPayerRateAmount, udcAmountWithRate.
        /// VB6: Function CalcSumCommiss() (lines 7256Ц7312).
        /// </summary>
        private bool CalcSumCommiss()
        {
            try
            {
                if (udcPaymentAmount.Text.Length == 0)
                {
                    udcPaymentAmount.Text = "0";
                }

                this.IUbsChannel.ParamIn("IdContract", m_idContract);
                this.IUbsChannel.ParamIn("SumPaym", udcPaymentAmount.DecimalValue);
                this.IUbsChannel.ParamIn("SUMMAPENI", udcPenaltyAmount.DecimalValue);
                this.IUbsChannel.ParamIn("blnComissPeniPayer", m_isComissPeniPayer);
                this.IUbsChannel.ParamIn("AccClientPay", ucaPayerAccount.Text);
                this.IUbsChannel.ParamIn("IdClient", m_idClient);
                this.IUbsChannel.ParamIn("INN", txtRecipientInn.Text);
                this.IUbsChannel.ParamIn("Kppu", txtTaxKbkNote.Text);
                this.IUbsChannel.ParamIn("AccClient", ucaRecipientAccount.Text);

                bool blnSecondPayment = true;
                if (tabPageTax.Visible)
                {
                    blnSecondPayment = (m_isSecondPayment == 1);
                }
                this.IUbsChannel.ParamIn("blnSecondPayment", blnSecondPayment);

                if (m_isAddparam && udcPayerRateAmount.DecimalValue > 0)
                {
                    this.IUbsChannel.ParamIn(" омисси€ задана", udcPayerRateAmount.DecimalValue);
                }

                this.IUbsChannel.ParamIn("IdGroupIncoming", m_idGroupIncoming);
                this.IUbsChannel.ParamIn("IsGuest", m_isGuest);
                this.IUbsChannel.ParamIn("Benefits", chkBenefits.Checked);

                this.IUbsChannel.Run("UtCalcSumCommiss");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                if (paramOut.GetParamOutBool("bRetVal"))
                {
                    m_curSumRateRec = paramOut.GetParamOutDecimal("SummaRateRec");
                    m_curSumRec = paramOut.GetParamOutDecimal("SummaRec");
                    udcPayerRateAmount.DecimalValue = paramOut.GetParamOutDecimal("SummaRateSend");
                    udcAmountWithRate.DecimalValue = paramOut.GetParamOutDecimal("SummaTotal");

                    return true;
                }
                else
                {
                    if (m_isClickSave)
                    {
                        string err = paramOut.GetParamOutString("StrError");
                        MessageBox.Show(err, CaptionCalcCommission + CaptionForm,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return false;
            }
        }

        #endregion

        #region CalcSumNDS Ч VAT calculation via UtCalcSumNDS

        /// <summary>
        /// Server-side NDS (VAT) calculation via UtCalcSumNDS channel.
        /// VB6: Function CalcSumNDS() (lines 7316Ц7350).
        /// </summary>
        private bool CalcSumNDS()
        {
            try
            {
                this.IUbsChannel.ParamIn("IDCONTRACT", m_idContract);
                this.IUbsChannel.ParamIn("SUMMA", udcPaymentAmount.DecimalValue);
                this.IUbsChannel.ParamIn("IDKINDPAYMENT", m_idKindPaym);
                this.IUbsChannel.ParamIn("SUMMARATESEND", udcPayerRateAmount.DecimalValue);
                this.IUbsChannel.ParamIn("SUMMARATEREC", m_curSumRateRec);

                this.IUbsChannel.Run("UtCalcSumNDS");

                var paramOut = new UbsParamCustom(this.IUbsChannel.ParamsOut);

                if (paramOut.GetParamOutBool("bRetVal"))
                {
                    m_curSumNDSSend = paramOut.GetParamOutDecimal("SumNDSSend");
                    m_curSumNDSPaym = paramOut.GetParamOutDecimal("SumNDSPaym");
                    m_curSumNDSRec = paramOut.GetParamOutDecimal("SumNDSRec");
                    return true;
                }
                else
                {
                    string err = paramOut.GetParamOutString("StrError");
                    MessageBox.Show(err, CaptionCalcNds + CaptionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return false;
            }
        }

        #endregion

        #region RunEcOperation Ч electronic cashier

        /// <summary>
        /// Opens the electronic cashier operation form (UBS_OC_FRM_EC_OPERATION).
        /// VB6: Private Sub RunEcOperation (lines 10107Ц10128).
        /// </summary>
        private void RunEcOperation(string dbOrCr, decimal amount, int idCurrency)
        {
            try
            {
                this.IUbsChannel.ParamIn("DbOrCr", dbOrCr);
                this.IUbsChannel.ParamIn("Oborot", amount);
                this.IUbsChannel.ParamIn("IdCurrency", idCurrency);

                this.IUbsChannel.Run("RunEcOperation");
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion
    }
}
