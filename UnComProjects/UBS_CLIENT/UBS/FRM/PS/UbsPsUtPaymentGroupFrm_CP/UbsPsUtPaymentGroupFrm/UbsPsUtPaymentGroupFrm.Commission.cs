using System;
using System.Globalization;
using UbsService;

namespace UbsBusiness
{
    public partial class UbsPsUtPaymentGroupFrm
    {
        private void WireCommissionHandlers()
        {
            udcSumma.TextChanged += UdcSumma_TextChanged;
        }

        private void UdcSumma_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_idContract > 0)
                {
                    CalcSumCommiss();
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void CopyCommissionRateSendFromContract(UbsParam contractOut)
        {
            m_arrRateSend = new object[6];
            m_arrRateSend[0] = contractOut.Value("RateTypeSend");
            m_arrRateSend[1] = contractOut.Value("RatePercentSend");
            m_arrRateSend[2] = contractOut.Value("MinSumSend");
            m_arrRateSend[3] = contractOut.Value("MaxSumSend");
            m_arrRateSend[4] = contractOut.Value("Комиссии с плательщика-признак ставки");
            m_arrRateSend[5] = contractOut.Value("Комиссии с плательщика-тарифная сетка");
        }

        private void RefreshCommissionRatesAfterPaymentRead()
        {
            try
            {
                if (m_idContract <= 0)
                {
                    return;
                }

                this.IUbsChannel.ParamIn("IdContract", m_idContract);
                this.IUbsChannel.Run("UtReadContract");

                UbsParam po = this.IUbsChannel.ParamsOutParam;
                if (!po.Contains("bRetVal") || !Convert.ToBoolean(po.Value("bRetVal")))
                {
                    return;
                }

                CopyCommissionRateSendFromContract(po);
                CalcSumCommiss();
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void CalcSumCommiss()
        {
            try
            {
                if (m_arrRateSend == null || m_arrRateSend.Length < 6)
                {
                    return;
                }

                int intTypeSend = Convert.ToInt32(m_arrRateSend[0]);
                decimal curPerSend = ToMoneyDecimal(m_arrRateSend[1]);
                decimal curMinSumSend = ToMoneyDecimal(m_arrRateSend[2]);
                decimal curMaxSumSend = ToMoneyDecimal(m_arrRateSend[3]);
                object arrStavkaRaw = m_arrRateSend[5];
                string strTarif = Convert.ToString(m_arrRateSend[4]);

                decimal curSumPaym = udcSumma.DecimalValue;

                if (arrStavkaRaw is object[,])
                {
                    object[,] arrStavka = (object[,])arrStavkaRaw;
                    if (arrStavka.GetLength(0) == 0 || arrStavka.GetLength(1) < 2)
                    {
                        return;
                    }

                    if (string.Equals(strTarif, TarifLabelPercent, StringComparison.Ordinal))
                    {
                        intTypeSend = 1;
                    }
                    else if (string.Equals(strTarif, TarifLabelFixed, StringComparison.Ordinal))
                    {
                        intTypeSend = 2;
                    }
                    else
                    {
                        return;
                    }

                    curPerSend = 0m;
                    int rowCount = arrStavka.GetLength(0);
                    int i = rowCount - 1;
                    bool blnFind = false;
                    do
                    {
                        if (ToMoneyDecimal(arrStavka[i, 0]) <= curSumPaym)
                        {
                            curPerSend = ToMoneyDecimal(arrStavka[i, 1]);
                            blnFind = true;
                        }

                        if (i == 0)
                        {
                            blnFind = true;
                        }

                        i--;
                    }
                    while (!blnFind);
                }

                decimal curSumRateSend;
                switch (intTypeSend)
                {
                    case 0:
                        curSumRateSend = 0m;
                        break;
                    case 1:
                        curSumRateSend = RoundMoney(curSumPaym * curPerSend / 100m);
                        break;
                    case 2:
                        curSumRateSend = RoundMoney(curPerSend);
                        break;
                    default:
                        return;
                }

                if (intTypeSend > 0)
                {
                    if (curSumRateSend < curMinSumSend && curMinSumSend > 0m)
                    {
                        curSumRateSend = RoundMoney(curMinSumSend);
                    }

                    if (curMaxSumSend > 0m && curSumRateSend > curMaxSumSend)
                    {
                        curSumRateSend = RoundMoney(curMaxSumSend);
                    }
                }

                decimal curSumTotal = RoundMoney(curSumPaym + curSumRateSend);
                udcSummaRateSend.Text = curSumRateSend.ToString(CultureInfo.CurrentCulture);
                udcSummaTotal.Text = curSumTotal.ToString(CultureInfo.CurrentCulture);
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private static decimal RoundMoney(decimal d)
        {
            return Math.Round(d, 2, MidpointRounding.AwayFromZero);
        }

        private static decimal ToMoneyDecimal(object o)
        {
            if (o == null || ReferenceEquals(o, DBNull.Value))
            {
                return 0m;
            }

            return Convert.ToDecimal(o, CultureInfo.InvariantCulture);
        }
    }
}
