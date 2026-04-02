using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// Форма операции возмещения (op_ret_oper).
    /// </summary>
    public partial class UbsOpRetoperFrm : UbsFormBase
    {
        #region Поля формы

        private string m_command = string.Empty;
        private int m_idOper = 0;
        private string m_sidOper = string.Empty;
        private int m_idPayDoc = 0;
        private int m_idFOper = 0;
        private int m_selectedClient = 0;
        private int m_idPov = 0;
        private decimal m_sumKom = 0m;
        private bool m_isKomPer = false;
        private bool m_isKomCur = false;
        private Array m_operArr = null;
        private Array m_valArr = null;
        private int m_idOperComponent;

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public UbsOpRetoperFrm()
        {
            m_addCommand();
            InitializeComponent();

            base.Ubs_CommandLock = true;
        }

        #region Обработчики кнопок

        private void btnExit_Click(object sender, EventArgs e) { this.Close(); }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren()) { return; }
                if (!CheckSave()) { return; }

                BuildSaveParams();
                base.IUbsChannel.Run("Save");

                var paramOut = new UbsParam(base.IUbsChannel.ParamsOut);
                string err = paramOut.Contains("Error") ? Convert.ToString(paramOut.Value("Error")) : "";
                if (!string.IsNullOrEmpty(err))
                {
                    base.Ubs_ShowErrorBox(err);
                    return;
                }

                ubsCtrlInfo.Show(MsgSaved);
                cmbValueMinus.Focus();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void linkClient_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            object[] ids = this.Ubs_ActionRun("UBS_COMMON_LIST_CLIENT", this, true) as object[];

            if (ids != null && ids.Length > 0)
            {
                m_selectedClient = Convert.ToInt32(ids[0]);

                base.IUbsChannel.ParamIn("IdClient", m_selectedClient);
                base.IUbsChannel.Run("GetNameClient");

                if (base.IUbsChannel.ExistParamOut("Имя клиента"))
                {
                    txtClient.Text = Convert.ToString(base.IUbsChannel.ParamOut("Имя клиента"));
                }
            }
        }

        #endregion

        #region Обработчики событий

        private void chkPayMoney_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkPayMoney.Checked)
            {
                cmbValueMinus.Enabled = false;
                ucdValueMinus.DecimalValue = 0m;
            }
            else
            {
                cmbValueMinus.Enabled = true;
            }
        }

        private void cmbValueMinus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbValueMinus.SelectedIndex == -1)
                    return;

                var valMinusId = GetValMinusId();

                base.IUbsChannel.ParamIn("idPov", m_idPov);
                base.IUbsChannel.ParamIn("SID Операции", "BUY");
                base.IUbsChannel.ParamIn("Валюта принятая", txtCurrencyNominal.Text != string.Empty ? Convert.ToInt32(txtCurrencyNominal.Text) : 0);
                base.IUbsChannel.ParamIn("Валюта выданная", valMinusId);
                base.IUbsChannel.ParamIn("Платежный документ", m_idPayDoc);

                int rows = m_operArr.GetLength(0);

                for (int i = 0; i < rows; i++)
                {
                    string operSid = Convert.ToString(m_operArr.GetValue(i, 2));

                    if (m_sidOper == "INCASH" && operSid == "INCASH_RET")
                    {
                        base.IUbsChannel.ParamIn("SID операции", "INCASH_RET");
                        base.IUbsChannel.ParamIn("Операция", m_operArr.GetValue(i, 0));
                    }

                    if (m_sidOper == "INCASH_PAYDOC" && operSid == "INCASH_RET_PAYDOC")
                    {
                        base.IUbsChannel.ParamIn("SID операции", "INCASH_RET");
                        base.IUbsChannel.ParamIn("Операция", m_operArr.GetValue(i, 0));
                    }

                    if (m_sidOper == "EXPERT" && operSid == "EXPERT_RET")
                    {
                        base.IUbsChannel.ParamIn("SID операции", "EXPERT_RET");
                        base.IUbsChannel.ParamIn("Операция", m_operArr.GetValue(i, 0));
                    }

                    if (m_sidOper == "EXPERT_PAYDOC" && operSid == "EXPERT_RET_PAYDOC")
                    {
                        base.IUbsChannel.ParamIn("SID операции", "EXPERT_RET_PAYDOC");
                        base.IUbsChannel.ParamIn("Операция", m_operArr.GetValue(i, 0));
                    }
                }

                base.IUbsChannel.Run("GetOperParam");

                var pOut = new UbsParam(base.IUbsChannel.ParamsOut);

                m_sumKom = pOut.Contains("Комиссия") ? Convert.ToDecimal(pOut.Value("Комиссия")) : 0m;
                m_isKomCur = pOut.Contains("Комиссия в валюте") && Convert.ToBoolean(pOut.Value("Комиссия в валюте"));
                m_isKomPer = pOut.Contains("Комиссия в процентах") && Convert.ToBoolean(pOut.Value("Комиссия в процентах"));

                if (!m_isKomCur)
                {
                    SetComboSelection(cmbComission, 810);
                }
                else
                {
                    SetComboSelection(cmbComission, valMinusId);
                }

                if (!m_isKomPer)
                {
                    ucdCommission.DecimalValue = m_sumKom;
                }

                if (pOut.Contains("Курс"))
                {
                    ucdRate.DecimalValue = Convert.ToDecimal(pOut.Value("Курс"));
                    ucdRatePer.DecimalValue = Convert.ToDecimal(pOut.Value("Курс за(единиц)"));
                }
                else
                {
                    ucdRate.DecimalValue = 1m;
                    ucdRatePer.DecimalValue = 1m;
                }
                RecalcucdValueMinus();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void ucdRatePer_TextChanged(object sender, EventArgs e)
        {
            RecalcucdValueMinus();
        }

        private void ucdRate_TextChanged(object sender, EventArgs e)
        {
            RecalcucdValueMinus();
        }

        #endregion

        #region IUbs команды

        private void m_addCommand()
        {
            base.Ubs_AddName(new UbsDelegate(CommandLine));
            base.Ubs_AddName(new UbsDelegate(ListKey));
        }

        private object CommandLine(object param_in, ref object param_out)
        {
            m_command = (string)param_in;
            return null;
        }

        private object ListKey(object param_in, ref object param_out)
        {
            try
            {
                m_idOper = (param_in != null && param_in is object[] && ((object[])param_in).Length > 0)
                    ? Convert.ToInt32(((object[])param_in)[0])
                    : 0;

                this.IUbsChannel.LoadResource = LoadResource;

                if (m_idOper == 0)
                {
                    base.Ubs_ShowErrorBox(MsgEmptyList);
                    this.Close();
                    return null;
                }

                base.IUbsChannel.Run("CheckCash");
                var checkOut = new UbsParam(base.IUbsChannel.ParamsOut);
                string checkErr = checkOut.Contains("Error") ? Convert.ToString(checkOut.Value("Error")) : "";
                if (!string.IsNullOrEmpty(checkErr))
                {
                    base.Ubs_ShowErrorBox(checkErr);
                    this.Close();
                    return null;
                }

                InitDoc();

                if (cmbValueMinus.Enabled)
                    cmbValueMinus.Focus();
                else
                    chkPayMoney.Focus();

                return null;
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        #endregion

        #region InitDoc, LoadFromParams, FillCombos

        private void InitDoc()
        {
            try
            {
                base.IUbsChannel.ParamIn("Тип операции", "ret");
                base.IUbsChannel.ParamIn("initedDoc", false);
                base.IUbsChannel.ParamIn("Id операции", m_idOper);
                base.IUbsChannel.Run("InitForm");

                var paramOut = new UbsParam(base.IUbsChannel.ParamsOut);
                string errStr = paramOut.Contains("Error") ? Convert.ToString(paramOut.Value("Error")) : "";
                if (!string.IsNullOrEmpty(errStr))
                {
                    base.Ubs_ShowErrorBox(errStr);
                    this.Close();
                    return;
                }

                m_sidOper = paramOut.Contains("SID Операции") ? Convert.ToString(paramOut.Value("SID Операции")) : "";
                m_idFOper = paramOut.Contains("Id операции") ? Convert.ToInt32(paramOut.Value("Id операции")) : 0;
                m_idPayDoc = paramOut.Contains("Id ценности") ? Convert.ToInt32(paramOut.Value("Id ценности")) : 0;
                m_selectedClient = paramOut.Contains("Id клиента") ? Convert.ToInt32(paramOut.Value("Id клиента")) : 0;

                if (paramOut.Contains("Список операций"))
                    m_operArr = paramOut.Value("Список операций") as Array;

                if (m_sidOper == "EXPERT_PAYDOC")
                {
                    chkPayMoney.Enabled = true;
                    chkPayMoney.Checked = false;
                    cmbValueMinus.Enabled = false;
                }
                else
                {
                    chkPayMoney.Enabled = false;
                    chkPayMoney.Checked = true;
                }

                FillDocCB(paramOut);
                FillValCombos(paramOut);
                LoadFromParams(paramOut);
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void FillDocCB(UbsParam paramOut)
        {
            cmbDocument.Items.Clear();
            if (!paramOut.Contains("Список документов")) return;
            var docArr = paramOut.Value("Список документов") as Array;
            if (docArr == null || docArr.Rank < 2) return;

            int row = docArr.GetLength(0);
            for (int i = 0; i < row; i++)
            {
                object v = docArr.GetValue(i, 0);
                cmbDocument.Items.Add(v != null ? Convert.ToString(v) : "");
            }

            if (cmbDocument.Items.Count > 0) cmbDocument.SelectedIndex = 0;
        }

        private void FillValCombos(UbsParam paramOut)
        {
            cmbValueMinus.Items.Clear();
            cmbComission.Items.Clear();
            if (!paramOut.Contains("Список валют")) return;
            var arr = paramOut.Value("Список валют") as Array;
            if (arr == null || arr.Rank < 2) return;
            m_valArr = arr;
            var list = new List<KeyValuePair<int, string>>();

            int rows = arr.GetLength(0);
            for (int i = 0; i < rows; i++)
            {
                object o0 = arr.GetValue(i, 0);
                object o2 = arr.GetLength(1) > 2 ? arr.GetValue(i, 2) : null;
                int id = o0 != null ? Convert.ToInt32(o0) : 0;
                string text = o2 != null ? Convert.ToString(o2) : string.Empty;

                list.Add(new KeyValuePair<int, string>(id, text));
            }

            if (list.Count > 0)
            {
                var data = new List<KeyValuePair<int, string>>(list);
                cmbValueMinus.DataSource = data;
                cmbValueMinus.ValueMember = "Key";
                cmbValueMinus.DisplayMember = "Value";
                cmbValueMinus.SelectedIndex = -1;
                cmbComission.DataSource = new List<KeyValuePair<int, string>>(list);
                cmbComission.ValueMember = "Key";
                cmbComission.DisplayMember = "Value";
            }
        }

        private void LoadFromParams(UbsParam paramOut)
        {
            if (paramOut.Contains("ФИО")) txtClient.Text = Convert.ToString(paramOut.Value("ФИО"));
            if (paramOut.Contains("Комиссия")) ucdCommission.DecimalValue = ToDecimal(paramOut.Value("Комиссия"));
            if (paramOut.Contains("Номер квитанции(справки)")) ucdNumReceipt.DecimalValue = ToDecimal(paramOut.Value("Номер квитанции(справки)"));
            if (paramOut.Contains("Номинал")) ucdSumNominal.DecimalValue = ToDecimal(paramOut.Value("Номинал"));
            if (paramOut.Contains("Курс за(единиц)")) ucdRatePer.DecimalValue = ToDecimal(paramOut.Value("Курс за(единиц)"));
            if (paramOut.Contains("Номер платежного документа")) ucdNumPayDoc.DecimalValue = ToDecimal(paramOut.Value("Номер платежного документа"));
            if (paramOut.Contains("Номер документа")) txtNumDocument.Text = Convert.ToString(paramOut.Value("Номер документа"));
            if (paramOut.Contains("Платежный документ")) txtPayDoc.Text = Convert.ToString(paramOut.Value("Платежный документ"));
            if (paramOut.Contains("Подоходный налог")) ucdIncomeTax.DecimalValue = ToDecimal(paramOut.Value("Подоходный налог"));
            if (paramOut.Contains("Курс")) ucdRate.DecimalValue = ToDecimal(paramOut.Value("Курс"));
            if (paramOut.Contains("Резидент")) chkResident.Checked = ToDecimal(paramOut.Value("Резидент")) != 0;
            if (paramOut.Contains("Серия платежного документа")) txtSerPayDoc.Text = Convert.ToString(paramOut.Value("Серия платежного документа"));
            if (paramOut.Contains("Серия документа")) txtSerDocument.Text = Convert.ToString(paramOut.Value("Серия документа"));
            if (paramOut.Contains("Сумма выданная")) ucdValueMinus.DecimalValue = ToDecimal(paramOut.Value("Сумма выданная"));
            if (paramOut.Contains("Валюта номинала")) txtCurrencyNominal.Text = Convert.ToString(paramOut.Value("Валюта номинала"));
            if (paramOut.Contains("Идентификатор составной операции"))
            {
                m_idOperComponent = Convert.ToInt32(paramOut.Value("Идентификатор составной операции"));
            }

            if (string.IsNullOrEmpty(txtClient.Text)) txtClient.Text = string.Empty;
            if (string.IsNullOrEmpty(txtSerDocument.Text)) txtSerDocument.Text = "XXX";
            if (string.IsNullOrEmpty(txtNumDocument.Text)) txtNumDocument.Text = "XXX";
            ucdRate.DecimalValue = ucdRate.DecimalValue == 0m ? 1m : ucdRate.DecimalValue;
            ucdRatePer.DecimalValue = ucdRatePer.DecimalValue == 0m ? 1m : ucdRatePer.DecimalValue;

            RecalcucdValueMinus();
        }

        #endregion

        #region Save, BuildSaveParams, Recalc

        private bool CheckSave()
        {
            if (cmbValueMinus.SelectedIndex < 0 && chkPayMoney.Checked)
            {
                base.Ubs_ShowErrorBox(MsgSelectCurrency);
                cmbValueMinus.Focus();
                return false;
            }
            return true;
        }

        private void BuildSaveParams()
        {
            base.IUbsChannel.ParamIn("ФИО", txtClient.Text);
            base.IUbsChannel.ParamIn("Документ", cmbDocument.SelectedItem != null ? cmbDocument.SelectedItem.ToString() : "");
            base.IUbsChannel.ParamIn("Комиссия", ucdCommission.DecimalValue);
            base.IUbsChannel.ParamIn("Номер квитанции(справки)", ucdNumReceipt.DecimalValue);
            base.IUbsChannel.ParamIn("Номинал", ucdSumNominal.DecimalValue);
            base.IUbsChannel.ParamIn("Курс за(единиц)", ucdRatePer.DecimalValue);
            base.IUbsChannel.ParamIn("Номер платежного документа", ucdSumNominal.DecimalValue);
            base.IUbsChannel.ParamIn("Номер документа", txtNumDocument.Text);
            base.IUbsChannel.ParamIn("Платежный документ", txtPayDoc.Text);
            base.IUbsChannel.ParamIn("Подоходный налог", ucdIncomeTax.DecimalValue);
            base.IUbsChannel.ParamIn("Курс", ucdRate.DecimalValue);
            base.IUbsChannel.ParamIn("Резидент", chkResident.Checked ? 1 : 0);
            base.IUbsChannel.ParamIn("Серия платежного документа", txtSerPayDoc.Text);
            base.IUbsChannel.ParamIn("Серия документа", txtSerDocument.Text);
            base.IUbsChannel.ParamIn("Сумма выданная", ucdValueMinus.DecimalValue);
            base.IUbsChannel.ParamIn("Валюта номинала", txtCurrencyNominal.Text);

            int valMinusId = GetValMinusId();
            base.IUbsChannel.ParamIn("Валюта выданная", valMinusId >= 0 ? valMinusId : 0);
            int cmbComissionId = GetcmbComissionId();
            base.IUbsChannel.ParamIn("Валюта комиссии", cmbComissionId >= 0 ? cmbComissionId : 0);
            base.IUbsChannel.ParamIn("Валюта принятая", txtCurrencyNominal.Text != string.Empty ? Convert.ToInt32(txtCurrencyNominal.Text) : 0);
            base.IUbsChannel.ParamIn("Сумма принятая", ucdSumNominal.DecimalValue);
            base.IUbsChannel.ParamIn("Платежный документ", 0);

            if (!chkPayMoney.Checked)
            {
                base.IUbsChannel.ParamIn("Валюта выданная", ParseInt(txtCurrencyNominal.Text));
                base.IUbsChannel.ParamIn("Сумма выданная", ucdSumNominal.DecimalValue);
                base.IUbsChannel.ParamIn("Платежный документ", m_idPayDoc);
            }

            base.IUbsChannel.ParamIn("Идентификатор составной операции", m_idOperComponent);
            base.IUbsChannel.ParamIn("Номер квитанции(справки)", m_idFOper);
            base.IUbsChannel.ParamIn("Идентификатор клиента", m_selectedClient);
            SetOperParamFromOperArr();
        }

        private void SetOperParamFromOperArr()
        {
            if (m_operArr == null) return;

            for (int i = 0; i < m_operArr.GetLength(0); i++)
            {
                object op0 = m_operArr.GetValue(i, 0);
                object op2 = m_operArr.GetValue(i, 2);
                string sid = op2 != null ? Convert.ToString(op2) : string.Empty;
                if ((m_sidOper == "INCASH" && sid == "INCASH_RET") ||
                    (m_sidOper == "INCASH_PAYDOC" && sid == "INCASH_RET_PAYDOC") ||
                    (m_sidOper == "EXPERT" && sid == "EXPERT_RET") ||
                    (m_sidOper == "EXPERT_PAYDOC" && sid == "EXPERT_RET_PAYDOC"))
                {
                    base.IUbsChannel.ParamIn("SID Операции", sid);
                    base.IUbsChannel.ParamIn("Операция", op0);
                    break;
                }
            }
        }

        private void RecalcucdValueMinus()
        {
            decimal nu = ucdRatePer.DecimalValue;
            if (nu == 0m) return;
            ucdValueMinus.DecimalValue = ucdSumNominal.DecimalValue * ucdRate.DecimalValue / nu;
            if (m_isKomPer && cmbValueMinus.SelectedItem != null)
            {
                int vid = GetValMinusId();
                if ((vid == 810 && !m_isKomCur) || (vid != 810 && m_isKomCur))
                    ucdCommission.DecimalValue = ucdValueMinus.DecimalValue * m_sumKom / 100m;
            }
        }

        #endregion

        #region Helpers

        private int GetValMinusId()
        {
            if (cmbValueMinus.SelectedItem == null) return -1;
            return ((KeyValuePair<int, string>)cmbValueMinus.SelectedItem).Key;
        }

        private int GetcmbComissionId()
        {
            if (cmbComission.SelectedItem == null) return -1;
            return ((KeyValuePair<int, string>)cmbComission.SelectedItem).Key;
        }

        private void SetComboSelection(ComboBox cmb, int targetId)
        {
            foreach (object item in cmb.Items)
            {
                if (item is KeyValuePair<int, string> && ((KeyValuePair<int, string>)item).Key == targetId)
                {
                    cmb.SelectedItem = item;
                    return;
                }
            }
        }

        private static decimal ToDecimal(object v)
        {
            if (v == null || v == DBNull.Value) return 0m;
            decimal result;
            return decimal.TryParse(Convert.ToString(v), out result) ? result : 0m;
        }

        private static int ParseInt(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;
            int r;
            return int.TryParse(s, out r) ? r : 0;
        }

        #endregion
    }
}
