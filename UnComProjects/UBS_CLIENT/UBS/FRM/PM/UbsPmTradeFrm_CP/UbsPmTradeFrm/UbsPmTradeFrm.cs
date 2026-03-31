using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// Форма сделки с драгоценными металлами
    /// </summary>
    public partial class UbsPmTradeFrm : UbsFormBase
    {
        #region Блок объявления переменных

        private int m_idTrade = 0;
        private string m_command = string.Empty;
        private int m_kindTrade = 0;

        private int m_idClient1;
        private int m_idClient2;

        private bool m_suppressContractTypeEvent;
        private bool m_suppressKindSupplyEvent;
        private bool m_armedClearObligationsOnKindChange;
        private bool m_checkCommissionRestoreType2;
        private bool m_armedClearContract1FieldsOnTypeChange;
        private bool m_armedClearContract2FieldsOnTypeChange;
        private int m_idComission1;
        private int m_idComission2;

        private bool m_suppressMainTabSelecting;
        private bool m_blnAddOblig;
        private bool m_blnEditOblig;
        private bool m_obligEditingMode;
        private string m_sType = string.Empty;
        private string m_strNumInPart = string.Empty;
        private string m_strNaprTrade = string.Empty;
        private bool m_blnAddObject;

        private bool m_suppressCompositEvent;
        private bool m_needSendOblig;
        private int m_maxNumPart1;
        private int m_maxNumPart2;
        private readonly UbsParam m_paramOblig = new UbsParam();

        private string m_ourBIK = string.Empty;
        private int m_idBaseCurrency;
        private bool m_wasOperation;

        private string m_strCB = string.Empty;
        private bool m_blnConvert;
        private int m_intSaveCurrencyPost;
        private bool m_blnCurrencyPostClick;
        private bool m_blnSetFocus;
        private bool m_blnAddFlChanged;

        private Dictionary<string, object> m_initialMc;

        private const string PrefixObligObject = "Object";
        private const string PrefixDeleteOblig = "Delete";

        private readonly Dictionary<string, object> m_mc = new Dictionary<string, object>();

        private readonly DateTime MinDate = new DateTime(2222, 1, 1);
        private DateTime m_dateToday = new DateTime(2222, 1, 1);

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public UbsPmTradeFrm()
        {
            m_addCommand();

            InitializeComponent();

            base.UbsCtrlFieldsSupportCollection.Add("Доп. поля", ubsCtrlField);

            FulFillMainCollection(m_mc);

            base.Ubs_CommandLock = true;
        }

        private void FulFillMainCollection(Dictionary<string, object> mc)
        {
            mc["IS_COMPOSIT"] = 0;
            mc["Идентификатор комиссии"] = 0;
            mc["Идентификатор валюты обязательства"] = 0;
            mc["Идентификатор валюты оплаты"] = 0;
            mc["Идентификатор драг.металла"] = 0;
            mc["Вид поставки по сделке"] = 0;
            mc["TYPE_TRADE"] = 0;
            mc["Курс ЦБ"] = 0m;
            mc["DATE_TRADE"] = new DateTime(2222, 1, 1);
            mc["NUM_TRADE"] = string.Empty;
        }

        #region Обработчики команд IUbs

        /// <summary>
        /// Процедура регистрации обработчиков команд интерфейса IUbs в базовом классе
        /// </summary>
        private void m_addCommand()
        {
            base.Ubs_AddName(new UbsDelegate(CommandLine));
            base.Ubs_AddName(new UbsDelegate(ListKey));
        }

        /// <summary>
        /// Процедура обработки команды CommandLine
        /// </summary>
        private object CommandLine(object param_in, ref object param_out)
        {
            m_command = param_in != null ? Convert.ToString(param_in) : string.Empty;
            return null;
        }

        /// <summary>
        /// Процедура обработки команды ListKey
        /// </summary>
        private object ListKey(object param_in, ref object param_out)
        {
            try
            {
                // Входные параметры ListKey: RSIdent(0) = ID_TRADE
                m_idTrade = (param_in != null && param_in is object[] && ((object[])param_in).Length > 0)
                    ? Convert.ToInt32(((object[])param_in)[0])
                    : 0;

                this.IUbsChannel.LoadResource = LoadResource;

                // ADD: strRunParam = "ADD#<vidTrade>"
                m_kindTrade = 0;
                if (!string.Equals(m_command, CmdEdit, StringComparison.Ordinal))
                {
                    int hashPos = m_command != null ? m_command.IndexOf('#') : -1;
                    if (hashPos >= 0 && hashPos + 1 < m_command.Length)
                    {
                        string tail = m_command.Substring(hashPos + 1).Trim();
                        int parsed;
                        if (int.TryParse(tail, out parsed))
                            m_kindTrade = parsed;
                    }
                }

                if (string.Equals(m_command, CmdEdit, StringComparison.Ordinal) && m_idTrade == 0)
                {
                    base.Ubs_ShowErrorBox(MsgListEmpty);
                    this.Close();
                    return null;
                }

                InitDoc();

                this.ubsCtrlField.Refresh();

                if (dateTrade != null) dateTrade.Focus();
                return null;
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        #endregion

        #region Обработчики событий — кнопки

        private void btnExit_Click(object sender, EventArgs e)
        {
            try { this.Close(); }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateMcFromControls();

                if (!CheckData()) return;
                if (!CheckDatesOblig()) return;

                bool blnRun = false;

                if (string.Equals(m_command, CmdEdit, StringComparison.Ordinal))
                    base.IUbsChannel.ParamIn("ID_TRADE", m_idTrade);
                else
                    base.IUbsChannel.ParamIn("ID_TRADE", 0);

                if (tabControlInstr.TabPages.Contains(tabPageInstr1))
                    base.IUbsChannel.ParamIn("Инструкция по оплате для покупателя", FillArrDataInstr(0));
                if (tabControlInstr.TabPages.Contains(tabPageInstr2))
                    base.IUbsChannel.ParamIn("Инструкция по оплате для продавца", FillArrDataInstr(1));

                base.IUbsChannel.ParamIn("IsNDS", chkNDS.Checked ? 1 : 0);
                base.IUbsChannel.ParamIn("IsExport", chkExport.Checked ? 1 : 0);
                base.IUbsChannel.ParamIn("IsExternalStorage", chkExternalStorage.Checked ? 1 : 0);

                if (IsMainDataChanged())
                {
                    UbsParam objParamTrade = new UbsParam();
                    foreach (KeyValuePair<string, object> kv in m_mc)
                    {
                        object oldVal;
                        if (m_initialMc != null && m_initialMc.TryGetValue(kv.Key, out oldVal))
                        {
                            if (kv.Value != null && kv.Value.Equals(oldVal))
                                continue;
                            if (kv.Value == null && oldVal == null)
                                continue;
                        }
                        objParamTrade[kv.Key] = kv.Value;
                    }

                    int ck;
                    if (objParamTrade.Contains("Идентификатор валюты оплаты"))
                    {
                        UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPayment, out ck);
                        objParamTrade["Идентификатор валюты оплаты"] = ck;
                    }
                    if (objParamTrade.Contains("Идентификатор драг.металла"))
                    {
                        UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPost, out ck);
                        objParamTrade["Идентификатор драг.металла"] = ck;
                    }
                    if (objParamTrade.Contains("Вид поставки по сделке"))
                        objParamTrade["Вид поставки по сделке"] = cmbKindSupplyTrade.Text;
                    if (objParamTrade.Contains("Идентификатор комиссии"))
                    {
                        if (UbsPmTradeComboUtil.TryGetSelectedKey(cmbComission, out ck))
                            objParamTrade["Идентификатор комиссии"] = ck;
                        else
                            objParamTrade["Идентификатор комиссии"] = 0;
                    }

                    base.IUbsChannel.ParamIn("Основные данные", objParamTrade);
                    blnRun = true;
                }

                if (m_needSendOblig)
                {
                    base.IUbsChannel.ParamIn("ID_CLIENT1", m_idClient1);
                    base.IUbsChannel.ParamIn("ID_CLIENT2", m_idClient2);

                    object[,] arrOblig = FillArrOblig();
                    base.IUbsChannel.ParamIn("Обязательства сделки", arrOblig);
                    base.IUbsChannel.ParamIn("Обязательства сделки2", m_paramOblig);
                    blnRun = true;
                }

                if (m_blnAddFlChanged && string.Equals(m_command, CmdEdit, StringComparison.Ordinal))
                    blnRun = true;

                if (blnRun)
                {
                    base.IUbsChannel.Run("ModifyTrade");
                    UbsParam saveOut = base.IUbsChannel.ParamsOutParam;

                    if (saveOut.Contains("Ошибка"))
                    {
                        MessageBox.Show(this, Convert.ToString(saveOut.Value("Ошибка")),
                            MsgErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        if (saveOut.Contains("ID_TRADE"))
                            m_idTrade = Convert.ToInt32(saveOut.Value("ID_TRADE"));

                        m_command = CmdEdit;
                        m_blnAddFlChanged = false;

                        SnapshotMc();
                        this.Close();
                    }
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void cmdAddObligation_Click(object sender, EventArgs e)
        {
            try
            {
                m_blnAddOblig = true;
                m_sType = PrefixAddOblig;
                EnableObligEditMode();
                CallOblig(PrefixAddOblig);

                if (chkComposit.Checked
                    && string.Equals(m_command, CmdEdit, StringComparison.Ordinal))
                {
                    cmbTradeDirection.Enabled = true;
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void cmdEditObligation_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwObligation.Items.Count == 0)
                    return;

                m_blnEditOblig = true;
                m_sType = PrefixEditOblig;
                m_blnAddObject = false;
                EnableObligEditMode();
                CallOblig(PrefixEditOblig);

                if (chkComposit.Checked
                    && string.Equals(m_command, CmdEdit, StringComparison.Ordinal))
                {
                    cmbTradeDirection.Enabled = false;
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void cmdApplayObligation_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckDataOblig())
                    return;

                int dirKey = 0;
                if (cmbTradeDirection.SelectedItem is KeyValuePair<int, string>)
                    dirKey = ((KeyValuePair<int, string>)cmbTradeDirection.SelectedItem).Key;

                if (string.Equals(m_sType, PrefixAddOblig, StringComparison.Ordinal))
                {
                    if (dirKey == 1)
                    {
                        m_maxNumPart1++;
                        m_strNumInPart = "1." + m_maxNumPart1.ToString();
                    }
                    else
                    {
                        m_maxNumPart2++;
                        m_strNumInPart = "2." + m_maxNumPart2.ToString();
                    }
                    m_paramOblig[PrefixAddOblig + m_strNumInPart] = true;
                }
                else
                {
                    string currentDir = string.Empty;
                    if (cmbTradeDirection.SelectedItem is KeyValuePair<int, string>)
                        currentDir = ((KeyValuePair<int, string>)cmbTradeDirection.SelectedItem).Value;

                    if (!string.Equals(currentDir, m_strNaprTrade, StringComparison.Ordinal))
                    {
                        if (dirKey == 1)
                        {
                            m_maxNumPart1++;
                            m_strNumInPart = "1." + m_maxNumPart1.ToString();
                        }
                        else
                        {
                            m_maxNumPart2++;
                            m_strNumInPart = "2." + m_maxNumPart2.ToString();
                        }
                    }
                    else
                    {
                        if (lvwObligation.SelectedItems.Count > 0)
                            m_strNumInPart = lvwObligation.SelectedItems[0].SubItems[1].Text;
                    }
                    m_paramOblig[PrefixEditOblig + m_strNumInPart] = true;
                }

                object[,] objectArray;
                FillArrObject(out objectArray);
                m_paramOblig[PrefixObligObject + m_strNumInPart] = objectArray;

                SetResultsListOblig(m_sType);

                m_needSendOblig = true;
                tabPage3.Enabled = (lvwObligation.Items.Count > 0);

                DisableObligEditMode();
                cmdAddObligation.Focus();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void cmdExitObligation_Click(object sender, EventArgs e)
        {
            try
            {
                DisableObligEditMode();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void cmdDelObligation_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwObligation.Items.Count == 0 || lvwObligation.SelectedItems.Count == 0)
                    return;

                ListViewItem sel = lvwObligation.SelectedItems[0];
                string strNumInPartDel = sel.SubItems[1].Text;

                string objKey = PrefixObligObject + strNumInPartDel;
                if (m_paramOblig.Contains(objKey))
                    m_paramOblig.Remove(objKey);
                m_paramOblig[PrefixDeleteOblig + strNumInPartDel] = true;

                lvwObligation.Items.Remove(sel);
                m_needSendOblig = true;
                tabPage3.Enabled = (lvwObligation.Items.Count > 0);

                cmdAddObligation.Focus();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region Обработчики событий — чекбоксы

        private void chkComposit_CheckedChanged(object sender, EventArgs e)
        {
            if (m_suppressCompositEvent)
                return;
            try
            {
                if (chkComposit.Checked)
                {
                    cmbTradeDirection.Enabled = true;
                    cmbTradeDirection.SelectedIndex = -1;
                }
                else
                {
                    if (m_maxNumPart2 > 0)
                    {
                        DialogResult dr = MessageBox.Show(this, MsgDeleteReverseObligations, MsgWarningTitle,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.Yes)
                        {
                            m_needSendOblig = true;
                            ClearObligObjectParamsPart2();
                            RemoveReverseObligationListItems();
                            m_maxNumPart2 = 0;
                            UbsPmTradeComboUtil.SetComboByKey(cmbTradeDirection, 1);
                            cmbTradeDirection.Enabled = false;
                        }
                        else
                        {
                            m_suppressCompositEvent = true;
                            try
                            {
                                chkComposit.Checked = true;
                            }
                            finally
                            {
                                m_suppressCompositEvent = false;
                            }
                        }
                    }
                    else
                    {
                        UbsPmTradeComboUtil.SetComboByKey(cmbTradeDirection, 1);
                        cmbTradeDirection.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void chkNDS_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDisplayExport();
        }

        private void UpdateDisplayExport()
        {
            try
            {
                bool isCheck = false;
                if (m_idClient2 != 0)
                {
                    base.IUbsChannel.ParamIn("ClientId", m_idClient2);
                    base.IUbsChannel.Run("IsClientNotResident");
                    UbsParam paramOut = base.IUbsChannel.ParamsOutParam;
                    bool isNotResident = false;
                    if (paramOut.Contains("Result"))
                    {
                        object r = paramOut.Value("Result");
                        if (r != null && r != DBNull.Value)
                        {
                            if (r is bool)
                                isNotResident = (bool)r;
                            else
                                isNotResident = Convert.ToInt32(r) != 0;
                        }
                    }
                    isCheck = chkNDS.Checked && isNotResident;
                }
                if (isCheck)
                {
                    chkExport.Visible = true;
                }
                else
                {
                    chkExport.Visible = false;
                    chkExport.Checked = false;
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                chkExport.Visible = false;
                chkExport.Checked = false;
            }
        }

        private void chkRate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkRate.Checked)
                {
                    ucdRateCurOblig.Enabled = true;
                    chkSumInCurValue.Checked = false;
                    ucdCostCurPayment.Enabled = false;
                }
                else
                {
                    ucdRateCurOblig.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void chkSumInCurValue_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSumInCurValue.Checked)
                {
                    chkRate.Checked = false;
                    ucdRateCurOblig.Enabled = false;
                    ucdCostCurPayment.Enabled = true;
                }
                else
                {
                    ucdCostCurPayment.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void chkCash_Click(object sender, EventArgs e)
        {
            try
            {
                int index = -1;
                if (sender == chkCash0) index = 0;
                else if (sender == chkCash1) index = 1;
                else return;

                bool isCash = index == 0 ? chkCash0.Checked : chkCash1.Checked;

                GetInstrLink(index).Visible = !isCash;
                GetInstrAccountLink(index).Visible = !isCash;

                if (isCash)
                {
                    base.IUbsChannel.Run("GetInstructionOplataCash");
                    UbsParam paramOut = base.IUbsChannel.ParamsOutParam;
                    const string keyCashInstr = "Инструкции по оплате для расчета через кассу";
                    if (paramOut.Contains(keyCashInstr))
                        FillControlInstrPayment(index, paramOut.Value(keyCashInstr));
                }
                else
                {
                    ClearPayment(index);
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        #endregion

        #region Обработчики событий — расчёты и комбо

        private void ucdMass_Leave(object sender, EventArgs e)
        {
            try
            {
                GetMassGramm();
                GetSumOblig();
                GetSumPayment();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void ucdCostUnit_Leave(object sender, EventArgs e)
        {
            try
            {
                GetSumOblig();
                if (chkSumInCurValue.Checked)
                {
                    GetRateCurOblig();
                }
                else
                {
                    decimal costUnit, rateCur;
                    if (decimal.TryParse(ucdCostUnit.Text, out costUnit)
                        && decimal.TryParse(ucdRateCurOblig.Text, out rateCur))
                    {
                        ucdCostCurPayment.Text = Math.Round(costUnit * rateCur, 4).ToString();
                    }
                }
                GetSumPayment();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void ucdRateCurOblig_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal costUnit, rateCur;
                if (decimal.TryParse(ucdCostUnit.Text, out costUnit)
                    && decimal.TryParse(ucdRateCurOblig.Text, out rateCur))
                {
                    ucdCostCurPayment.Text = Math.Round(costUnit * rateCur, 4).ToString();
                }
                GetSumPayment();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void ucdCostCurOpl_Leave(object sender, EventArgs e)
        {
            try
            {
                GetRateCurOblig();
                GetSumPayment();
                chkSumInCurValue.Checked = false;
                chkRate.Checked = true;
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void dateTrade_Leave(object sender, EventArgs e)
        {
            try
            {
                if (IsTradeDateMissingOrInvalid())
                {
                    MessageBox.Show(this, MsgInvalidDate, MsgTitleValidationProps,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dateTrade.Focus();
                    return;
                }

                if (CheckDatesOblig())
                {
                    m_mc["DATE_TRADE"] = dateTrade.DateValue;

                    int curObligKey, curOplKey, curPostKey;
                    bool a = UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyObligation, out curObligKey);
                    bool b = UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPayment, out curOplKey);
                    bool c = UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPost, out curPostKey);

                    if (a && b && c)
                    {
                        GetRateCB();
                        GetRateForPM();
                    }
                    m_needSendOblig = true;
                }

                if (m_blnSetFocus)
                {
                    dateTrade.Focus();
                    m_blnSetFocus = false;
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void cmbObligationCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int curObligKey, curOplKey;
                if (UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyObligation, out curObligKey)
                    && UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPayment, out curOplKey))
                {
                    GetRateCB();
                    GetRateForPM();
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void cmbCurrencyPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int curObligKey, curOplKey, curPostKey;
                if (UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyObligation, out curObligKey)
                    && UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPayment, out curOplKey)
                    && UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPost, out curPostKey))
                {
                    GetRateCB();
                    GetRateForPM();
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void cmbCurrencyPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int curPostKey;
                if (!UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPost, out curPostKey))
                    return;
                if (curPostKey == m_intSaveCurrencyPost)
                    return;

                base.IUbsChannel.ParamIn("IdCurrency", curPostKey);
                base.IUbsChannel.Run("DefineCodCurrency");
                UbsParam codOut = base.IUbsChannel.ParamsOutParam;
                m_strCB = codOut.Contains("CodCB")
                    ? Convert.ToString(codOut.Value("CodCB")) : string.Empty;

                ucdMassGramm.Precision = (m_strCB == "A99") ? 0 : 1;
                if (cmbUnit.SelectedIndex >= 0)
                    GetMassaPrecision();

                base.IUbsChannel.ParamIn("CodCB", m_strCB);
                base.IUbsChannel.Run("PMConvert");
                UbsParam convOut = base.IUbsChannel.ParamsOutParam;
                m_blnConvert = convOut.Contains("Флаг пересчета")
                    && Convert.ToBoolean(convOut.Value("Флаг пересчета"));

                if (m_blnCurrencyPostClick && ExistObject())
                {
                    string[] numInParts = DefineArrStrNumInPart();
                    if (numInParts != null)
                    {
                        for (int i = 0; i < numInParts.Length; i++)
                            m_paramOblig.Remove(PrefixObligObject + numInParts[i]);
                    }
                    m_needSendOblig = true;
                }

                int curObligKey;
                if (UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyObligation, out curObligKey)
                    && cmbCurrencyPost.SelectedIndex >= 0)
                {
                    GetRateForPM();
                }

                m_intSaveCurrencyPost = curPostKey;
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetMassaPrecision();
                ucdMass.Text = "0";
                ucdMassGramm.Text = "0";

                int curObligKey, curOplKey;
                if (UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyObligation, out curObligKey)
                    && UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPayment, out curOplKey))
                {
                    GetRateForPM();
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void ubsCtrlField_TextChange(object sender, EventArgs e)
        {
            m_blnAddFlChanged = true;
        }

        #endregion

        #region Вспомогательные методы — инициализация
        private DateTime GetCurrentDate()
        {
            base.IUbsChannel.ParamIn("NameSetting", "Server");
            base.IUbsChannel.Run("GetCommonDate");

            return Convert.ToDateTime(base.IUbsChannel.ParamOut("DataSetting"));
        }
        private void InitDoc()
        {
            try
            {
                base.IUbsChannel.Run("InitForm");
                base.UbsInit();

                m_dateToday = GetCurrentDate();

                m_blnAddOblig = false;
                m_blnEditOblig = false;
                m_checkCommissionRestoreType2 = false;

                chkCash0.Visible = false;
                //возможность расчета через кассу только для наличных сделок с клиентом (брокерский договор с клиентом)
                chkCash1.Visible = false;
                //возможность расчета через кассу только для наличных сделок с клиентом (брокерский договор с клиентом)

                FillCombos();
                FillOurBIK();

                bool isEdit = string.Equals(m_command, CmdEdit, StringComparison.Ordinal);
                int isNDS = 0;
                int isExport = 0;

                if (isEdit)
                {
                    m_armedClearContract1FieldsOnTypeChange = false;
                    m_armedClearContract2FieldsOnTypeChange = false;
                    m_armedClearObligationsOnKindChange = false;

                    base.UbsChannel_ParamIn("ID_TRADE", m_idTrade);
                    base.UbsChannel_Run("GetOneTrade");
                    UbsParam tradeOut = new UbsParam(base.UbsChannel_ParamsOut);

                    if (tradeOut.Contains("IsNDS"))
                        isNDS = Math.Abs(Convert.ToInt32(tradeOut.Value("IsNDS")));
                    if (tradeOut.Contains("IsExport"))
                        isExport = Math.Abs(Convert.ToInt32(tradeOut.Value("IsExport")));
                    if (tradeOut.Contains("IsExternalStorage"))
                        chkExternalStorage.Checked = Math.Abs(Convert.ToInt32(tradeOut.Value("IsExternalStorage"))) != 0;

                    if (tradeOut.Contains("DATE_TRADE"))
                        dateTrade.DateValue = Convert.ToDateTime(tradeOut.Value("DATE_TRADE"));
                    if (tradeOut.Contains("NUM_TRADE"))
                        txtTradeNum.Text = Convert.ToString(tradeOut.Value("NUM_TRADE"));

                    m_suppressCompositEvent = true;
                    try
                    {
                        if (tradeOut.Contains("IS_COMPOSIT"))
                        {
                            object cv = tradeOut.Value("IS_COMPOSIT");
                            chkComposit.Checked = cv != null && cv != DBNull.Value && Convert.ToInt32(cv) != 0;
                        }
                    }
                    finally { m_suppressCompositEvent = false; }

                    if (tradeOut.Contains("Вид сделки"))
                        m_kindTrade = Convert.ToInt32(tradeOut.Value("Вид сделки"));

                    object varOplBuyer = null;
                    object varOplSeller = null;
                    if (tradeOut.Contains("Инструкция по оплате для покупателя"))
                        varOplBuyer = tradeOut.Value("Инструкция по оплате для покупателя");
                    if (tradeOut.Contains("Инструкция по оплате для продавца"))
                        varOplSeller = tradeOut.Value("Инструкция по оплате для продавца");

                    int storageId = 0;
                    if (tradeOut.Contains("Инструкция по поставке"))
                    {
                        object sv = tradeOut.Value("Инструкция по поставке");
                        if (sv != null && sv != DBNull.Value)
                            storageId = Convert.ToInt32(sv);
                    }
                    FillControlStorage(storageId, chkExternalStorage.Checked);

                    if (tradeOut.Contains("TYPE_TRADE"))
                        UbsPmTradeComboUtil.SetComboByKey(cmbTradeType, Convert.ToInt32(tradeOut.Value("TYPE_TRADE")));
                    m_suppressKindSupplyEvent = true;
                    try
                    {
                        if (tradeOut.Contains("Вид поставки по сделке"))
                            UbsPmTradeComboUtil.SetComboByKey(cmbKindSupplyTrade, Convert.ToInt32(tradeOut.Value("Вид поставки по сделке")));
                    }
                    finally
                    {
                        m_suppressKindSupplyEvent = false;
                    }
                    if (tradeOut.Contains("Идентификатор валюты оплаты"))
                        UbsPmTradeComboUtil.SetComboByKey(cmbCurrencyPayment, Convert.ToInt32(tradeOut.Value("Идентификатор валюты оплаты")));
                    if (tradeOut.Contains("Идентификатор драг.металла"))
                        UbsPmTradeComboUtil.SetComboByKey(cmbCurrencyPost, Convert.ToInt32(tradeOut.Value("Идентификатор драг.металла")));
                    if (tradeOut.Contains("Идентификатор комиссии"))
                        TrySelectComissionById(Convert.ToInt32(tradeOut.Value("Идентификатор комиссии")));

                    base.IUbsChannel.Run("FillBaseCurrency");
                    UbsParam baseCurOut = base.IUbsChannel.ParamsOutParam;
                    if (baseCurOut.Contains("Идентификатор базовой валюты"))
                    {
                        m_idBaseCurrency = Convert.ToInt32(baseCurOut.Value("Идентификатор базовой валюты"));
                        UbsPmTradeComboUtil.SetComboByKey(cmbCurrencyObligation, m_idBaseCurrency);
                    }

                    int contractType1 = 0;
                    int contractType2 = 0;
                    if (tradeOut.Contains("Продавец"))
                    {
                        int sellerId = Convert.ToInt32(tradeOut.Value("Продавец"));
                        m_idClient1 = FillContractRowFromPm(sellerId, txtContractCode1, txtClientName1);
                        UbsParam sellerOut = base.IUbsChannel.ParamsOutParam;
                        if (sellerOut.Contains("TYPE_CONTRACT"))
                            contractType1 = Convert.ToInt32(sellerOut.Value("TYPE_CONTRACT"));
                    }
                    if (tradeOut.Contains("Покупатель"))
                    {
                        int buyerId = Convert.ToInt32(tradeOut.Value("Покупатель"));
                        m_idClient2 = FillContractRowFromPm(buyerId, txtContractCode2, txtClientName2);
                        UbsParam buyerOut = base.IUbsChannel.ParamsOutParam;
                        if (buyerOut.Contains("TYPE_CONTRACT"))
                            contractType2 = Convert.ToInt32(buyerOut.Value("TYPE_CONTRACT"));
                    }

                    m_suppressContractTypeEvent = true;
                    try
                    {
                        UbsPmTradeComboUtil.SetComboByKey(cmbContractType1, contractType1);
                        UbsPmTradeComboUtil.SetComboByKey(cmbContractType2, contractType2);
                    }
                    finally { m_suppressContractTypeEvent = false; }

                    base.IUbsChannel.ParamIn("ID_TRADE", m_idTrade);

                    base.IUbsChannel.Run("FillObligPM");

                    UbsParam obligOut = base.IUbsChannel.ParamsOutParam;
                    if (obligOut.Contains("Обязательства сделки"))
                        FillListOblig(obligOut.Value("Обязательства сделки"));

                    //при вынесении инструкций из обязательства, параметром обязательства остаются _
                    //одни объекты, которых при обезличенной сделке нет, следовательно параметра "Обязательства сделки2" может и не быть
                    if (obligOut.Contains("Обязательства сделки2"))
                    {
                        object[,] oblig = obligOut.Value("Обязательства сделки2") as object[,];
                        m_paramOblig.Items = oblig;
                    }

                    //заполняем Инструкции по Оплате
                    if (varOplBuyer != null)
                        FillControlInstrPayment(0, varOplBuyer);
                    if (varOplSeller != null)
                        FillControlInstrPayment(1, varOplSeller);

                    base.IUbsChannel.ParamIn("ID_TRADE", m_idTrade);
                    base.IUbsChannel.Run("PMCheckOperationByTrade");
                    UbsParam opOut = base.IUbsChannel.ParamsOutParam;
                    m_wasOperation = opOut.Contains("Was_Operation")
                        && Convert.ToBoolean(opOut.Value("Was_Operation"));
                }
                else
                {
                    m_armedClearContract1FieldsOnTypeChange = false;
                    m_armedClearContract2FieldsOnTypeChange = false;
                    m_armedClearObligationsOnKindChange = false;

                    dateTrade.DateValue = m_dateToday;

                    UbsPmTradeComboUtil.SetComboByKey(cmbCurrencyPost, 1001);

                    base.IUbsChannel.Run("FillBaseCurrency");
                    UbsParam baseCurOut = base.IUbsChannel.ParamsOutParam;
                    if (baseCurOut.Contains("Идентификатор базовой валюты"))
                    {
                        m_idBaseCurrency = Convert.ToInt32(baseCurOut.Value("Идентификатор базовой валюты"));
                        UbsPmTradeComboUtil.SetComboByKey(cmbCurrencyPayment, m_idBaseCurrency);
                        UbsPmTradeComboUtil.SetComboByKey(cmbCurrencyObligation, m_idBaseCurrency);
                    }

                    m_suppressContractTypeEvent = true;
                    try
                    {
                        if (cmbContractType1.Items.Count > 0)
                            cmbContractType1.SelectedIndex = 0;
                        if (cmbContractType2.Items.Count > 0)
                            cmbContractType2.SelectedIndex = 0;
                    }
                    finally { m_suppressContractTypeEvent = false; }

                    m_maxNumPart1 = 0;
                    m_maxNumPart2 = 0;
                }

                txtContractCode1.Enabled = false;
                txtClientName1.Enabled = false;
                txtContractCode2.Enabled = false;
                txtClientName2.Enabled = false;
                ucdSumPayment.Enabled = false;
                ucaKS0.Enabled = false;
                txtName0.Enabled = false;
                ucaKS1.Enabled = false;
                txtName1.Enabled = false;
                txtStorageCode.Enabled = false;
                txtStorageName.Enabled = false;

                chkComposit.Visible = (m_kindTrade != 0);

                ucdMass.Enabled = false;
                ucaRS0.Enabled = false;
                ucaRS1.Enabled = false;

                if (chkComposit.Checked)
                {
                    cmbTradeDirection.Enabled = true;
                    cmbTradeDirection.SelectedIndex = -1;
                }
                else
                {
                    UbsPmTradeComboUtil.SetComboByKey(cmbTradeDirection, 1);
                    cmbTradeDirection.Enabled = false;
                }

                m_suppressKindSupplyEvent = true;
                try
                {
                    ApplyContractType1Change(false);
                    ApplyContractType2Change(false);
                }
                finally
                {
                    m_suppressKindSupplyEvent = false;
                }

                ApplyKindSupplyUiState(false);
                m_checkCommissionRestoreType2 = true;

                if (isEdit)
                {
                    if (isNDS == 1)
                    {
                        chkNDS.Visible = true;
                        chkNDS.Checked = true;
                    }

                    if (isExport == 1)
                    {
                        chkExport.Visible = true;
                        chkExport.Checked = true;
                    }

                    int ct1;
                    int ct2;
                    UbsPmTradeComboUtil.TryGetSelectedKey(cmbContractType1, out ct1);
                    UbsPmTradeComboUtil.TryGetSelectedKey(cmbContractType2, out ct2);

                    if (m_kindTrade == 0)
                    {
                        if (ct1 == 1
                            && string.Equals(txtBIK1.Text, m_ourBIK, StringComparison.Ordinal)
                            && ucaRS1.Text.Length >= 5
                            && ucaRS1.Text.Substring(0, 5) == "20202")
                        {
                            chkCash1.Visible = true;
                            chkCash1.Checked = true;
                            GetInstrLink(1).Visible = false;
                            GetInstrAccountLink(1).Visible = false;
                        }
                        if (ct2 == 1
                            && string.Equals(txtBIK0.Text, m_ourBIK, StringComparison.Ordinal)
                            && ucaRS0.Text.Length >= 5
                            && ucaRS0.Text.Substring(0, 5) == "20202")
                        {
                            chkCash0.Visible = true;
                            chkCash0.Checked = true;
                            GetInstrLink(0).Visible = false;
                            GetInstrAccountLink(0).Visible = false;
                        }
                    }
                }

                tabPage3.Enabled = (lvwObligation.Items.Count > 0);

                GetRateCB();
                GetRateForPM();

                if (isEdit && m_wasOperation)
                    LockUiOnWasOperation();

                UpdateMcFromControls();
                SnapshotMc();
                m_blnCurrencyPostClick = true;
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void FillCombos()
        {
            m_suppressContractTypeEvent = true;
            m_suppressKindSupplyEvent = true;
            try
            {
                cmbTradeType.DataSource = null;
                cmbTradeDirection.DataSource = null;
                cmbUnit.DataSource = null;
                cmbContractType1.DataSource = null;
                cmbContractType2.DataSource = null;
                cmbCurrencyPost.DataSource = null;
                cmbCurrencyPayment.DataSource = null;
                cmbCurrencyObligation.DataSource = null;
                cmbKindSupplyTrade.DataSource = null;
                cmbComission.DataSource = null;

                cmbTradeType.Items.Clear();
                cmbTradeDirection.Items.Clear();
                cmbUnit.Items.Clear();
                cmbContractType1.Items.Clear();
                cmbContractType2.Items.Clear();
                cmbCurrencyPost.Items.Clear();
                cmbCurrencyPayment.Items.Clear();
                cmbCurrencyObligation.Items.Clear();
                cmbKindSupplyTrade.Items.Clear();
                cmbComission.Items.Clear();

                List<KeyValuePair<int, string>> tradeTypeItems = new List<KeyValuePair<int, string>>();
                tradeTypeItems.Add(new KeyValuePair<int, string>(1, "тип1"));
                cmbTradeType.DataSource = tradeTypeItems;
                cmbTradeType.ValueMember = "Key";
                cmbTradeType.DisplayMember = "Value";

                List<KeyValuePair<int, string>> dirItems = new List<KeyValuePair<int, string>>();
                dirItems.Add(new KeyValuePair<int, string>(1, "прямая"));
                dirItems.Add(new KeyValuePair<int, string>(2, "обратная"));
                cmbTradeDirection.DataSource = dirItems;
                cmbTradeDirection.ValueMember = "Key";
                cmbTradeDirection.DisplayMember = "Value";

                List<KeyValuePair<int, string>> unitItems = new List<KeyValuePair<int, string>>();
                unitItems.Add(new KeyValuePair<int, string>(1, "грамм"));
                unitItems.Add(new KeyValuePair<int, string>(2, "унция"));
                cmbUnit.DataSource = unitItems;
                cmbUnit.ValueMember = "Key";
                cmbUnit.DisplayMember = "Value";

                List<KeyValuePair<int, string>> kindItems = new List<KeyValuePair<int, string>>();
                kindItems.Add(new KeyValuePair<int, string>(1, "обезличенная"));
                kindItems.Add(new KeyValuePair<int, string>(2, "физическая"));
                cmbKindSupplyTrade.DataSource = kindItems;
                cmbKindSupplyTrade.ValueMember = "Key";
                cmbKindSupplyTrade.DisplayMember = "Value";

                UbsPmTradeComboUtil.SetComboByKey(cmbKindSupplyTrade, 2);

                base.IUbsChannel.ParamIn("ID_PATTERN", 1);
                base.IUbsChannel.Run("TradeCombo_FillPM");
                UbsParam paramOut = base.IUbsChannel.ParamsOutParam;

                UbsPmTradeComboUtil.FillComboFrom2DArray(cmbContractType1, paramOut, "Типы договоров");
                UbsPmTradeComboUtil.FillComboFrom2DArray(cmbContractType2, paramOut, "Типы договоров");

                UbsPmTradeComboUtil.FillComboFrom2DArray(cmbCurrencyPost, paramOut, "Валюты поставки");

                UbsPmTradeComboUtil.FillComboFrom2DArray(cmbCurrencyPayment, paramOut, "Валюты оплаты");
                UbsPmTradeComboUtil.FillComboFrom2DArray(cmbCurrencyObligation, paramOut, "Валюты оплаты");

                UbsPmTradeComboUtil.FillComboFrom2DArray(cmbComission, paramOut, "Список комиссий");
            }
            finally
            {
                m_suppressContractTypeEvent = false;
                m_suppressKindSupplyEvent = false;
            }
        }

        private void cmbKindSupplyTrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_suppressKindSupplyEvent)
                return;
            try
            {
                ApplyKindSupplyUiState(true);
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void ApplyKindSupplyUiState(bool applyObligationClearRules)
        {
            int kk;
            if (!UbsPmTradeComboUtil.TryGetSelectedKey(cmbKindSupplyTrade, out kk))
                return;

            if (kk == 1)
            {
                if (applyObligationClearRules)
                {
                    txtStorageCode.Text = string.Empty;
                    txtStorageName.Text = string.Empty;
                }
                SetMainTabSupplyPageVisible(false);
                SetObligObjectsTabPageVisible(false);
            }
            else if (kk == 2)
            {
                SetMainTabSupplyPageVisible(true);
                SetObligObjectsTabPageVisible(true);
            }

            if (applyObligationClearRules)
            {
                bool isEdit = string.Equals(m_command, CmdEdit, StringComparison.Ordinal);
                if (!isEdit)
                {
                    DelOblig();
                    tabPage3.Enabled = (lvwObligation.Items.Count > 0);
                }
                else
                {
                    if (m_armedClearObligationsOnKindChange)
                    {
                        DelOblig();
                        tabPage3.Enabled = (lvwObligation.Items.Count > 0);
                    }
                    else
                        m_armedClearObligationsOnKindChange = true;
                }
            }

            ApplyKindSupplyMassUnitForObligationsTab();
            UpdateDisplayNDS();
            UpdateDisplayExport();
        }

        private void SetMainTabSupplyPageVisible(bool visible)
        {
            m_suppressMainTabSelecting = true;
            try
            {
                if (visible)
                {
                    if (!tabControl.TabPages.Contains(tabPage4))
                        tabControl.TabPages.Insert(3, tabPage4);
                }
                else
                {
                    if (tabControl.TabPages.Contains(tabPage4))
                    {
                        if (tabControl.SelectedTab == tabPage4)
                            tabControl.SelectedTab = tabPage1;
                        tabControl.TabPages.Remove(tabPage4);
                    }
                }
            }
            finally
            {
                m_suppressMainTabSelecting = false;
            }
        }

        private void SetObligObjectsTabPageVisible(bool visible)
        {
            if (visible)
            {
                if (!tabControlOblig.TabPages.Contains(tabPageOblig2))
                    tabControlOblig.TabPages.Add(tabPageOblig2);
            }
            else
            {
                if (tabControlOblig.TabPages.Contains(tabPageOblig2))
                {
                    if (tabControlOblig.SelectedTab == tabPageOblig2)
                        tabControlOblig.SelectedTab = tabPageOblig1;
                    tabControlOblig.TabPages.Remove(tabPageOblig2);
                }
            }
        }

        private void cmbContractType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyContractType1Change(true);
        }

        private void cmbContractType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyContractType2Change(true);
        }

        private void ApplyContractType1Change(bool clearPayment)
        {
            if (m_suppressContractTypeEvent)
                return;
            try
            {
                int key1;
                if (!UbsPmTradeComboUtil.TryGetSelectedKey(cmbContractType1, out key1))
                    return;

                if (clearPayment)
                    ClearPayment(1);
                chkCash1.Visible = false;

                int key2;
                bool hasType2 = UbsPmTradeComboUtil.TryGetSelectedKey(cmbContractType2, out key2);

                if (key1 == 1)
                {
                    if (hasType2)
                    {
                        lblCommission.Visible = (key2 != 0);
                        cmbComission.Visible = (key2 != 0);
                        UbsPmTradeComboUtil.SetComboByKey(cmbKindSupplyTrade, 1);
                        cmbKindSupplyTrade.Enabled = false;
                    }
                    if (m_kindTrade == 0)
                    {
                        chkCash1.Visible = true;
                        chkCash1.Checked = false;
                        GetInstrLink(1).Visible = true;
                        GetInstrAccountLink(1).Visible = true;
                    }
                }
                else
                {
                    if (hasType2)
                    {
                        if (key2 == 1)
                        {
                            lblCommission.Visible = (key1 != 0);
                            cmbComission.Visible = (key1 != 0);
                            UbsPmTradeComboUtil.SetComboByKey(cmbKindSupplyTrade, 1);
                            cmbKindSupplyTrade.Enabled = false;
                        }
                        else
                        {
                            lblCommission.Visible = false;
                            cmbComission.Visible = false;
                            cmbKindSupplyTrade.Enabled = true;
                        }
                    }
                }

                linkListInstr1.Visible = true;

                if (key1 == 0)
                    SetPaymentInstrTabsBuyerOnly();
                else
                    SetPaymentInstrTabsBothBuyerSelected();

                bool isEdit = string.Equals(m_command, CmdEdit, StringComparison.Ordinal);
                if (key1 != 0)
                {
                    if (!isEdit)
                    {
                        txtContractCode1.Text = string.Empty;
                        txtClientName1.Text = string.Empty;
                    }
                    else
                    {
                        if (m_armedClearContract1FieldsOnTypeChange)
                        {
                            txtContractCode1.Text = string.Empty;
                            txtClientName1.Text = string.Empty;
                        }
                        else
                            m_armedClearContract1FieldsOnTypeChange = true;
                    }
                    linkContract1.Enabled = true;
                }
                else
                {
                    m_idClient1 = FillContractRowFromPm(0, txtContractCode1, txtClientName1);
                    linkContract1.Enabled = false;
                    ClearComissionSelection();
                    if (m_checkCommissionRestoreType2 && hasType2 && key2 == 1)
                        TrySelectComissionById(m_idComission2);
                    m_armedClearContract1FieldsOnTypeChange = true;
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void ApplyContractType2Change(bool clearPayment)
        {
            if (m_suppressContractTypeEvent)
                return;
            try
            {
                int key2;
                if (!UbsPmTradeComboUtil.TryGetSelectedKey(cmbContractType2, out key2))
                    return;

                if (clearPayment)
                    ClearPayment(0);
                chkCash0.Visible = false;

                int key1;
                bool hasType1 = UbsPmTradeComboUtil.TryGetSelectedKey(cmbContractType1, out key1);

                if (key2 == 1)
                {
                    if (hasType1)
                    {
                        lblCommission.Visible = (key1 != 0);
                        cmbComission.Visible = (key1 != 0);
                        UbsPmTradeComboUtil.SetComboByKey(cmbKindSupplyTrade, 1);
                        cmbKindSupplyTrade.Enabled = false;
                    }
                    if (m_kindTrade == 0)
                    {
                        chkCash0.Visible = true;
                        chkCash0.Checked = false;
                        GetInstrLink(0).Visible = true;
                        GetInstrAccountLink(0).Visible = true;
                    }
                }
                else
                {
                    if (hasType1 && key1 == 1)
                    {
                        lblCommission.Visible = (key2 != 0);
                        cmbComission.Visible = (key2 != 0);
                        UbsPmTradeComboUtil.SetComboByKey(cmbKindSupplyTrade, 1);
                        cmbKindSupplyTrade.Enabled = false;
                    }
                    else
                    {
                        lblCommission.Visible = false;
                        cmbComission.Visible = false;
                        cmbKindSupplyTrade.Enabled = true;
                    }
                }

                linkListInstr0.Visible = true;

                if (key2 == 0)
                    SetPaymentInstrTabsSellerOnly();
                else
                    SetPaymentInstrTabsBothBuyerSelected();

                bool isEdit = string.Equals(m_command, CmdEdit, StringComparison.Ordinal);
                if (key2 != 0)
                {
                    if (!isEdit)
                    {
                        txtContractCode2.Text = string.Empty;
                        txtClientName2.Text = string.Empty;
                    }
                    else
                    {
                        if (m_armedClearContract2FieldsOnTypeChange)
                        {
                            txtContractCode2.Text = string.Empty;
                            txtClientName2.Text = string.Empty;
                        }
                        else
                            m_armedClearContract2FieldsOnTypeChange = true;
                    }
                    linkContract2.Enabled = true;
                }
                else
                {
                    m_idClient2 = FillContractRowFromPm(0, txtContractCode2, txtClientName2);
                    linkContract2.Enabled = false;
                    if (hasType1 && key1 == 1)
                        TrySelectComissionById(m_idComission1);
                    else
                        ClearComissionSelection();
                    m_armedClearContract2FieldsOnTypeChange = true;
                }

                UpdateDisplayNDS();
                UpdateDisplayExport();
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void SetPaymentInstrTabsBuyerOnly()
        {
            tabControlInstr.SuspendLayout();
            try
            {
                tabControlInstr.TabPages.Clear();
                tabControlInstr.TabPages.Add(tabPageInstr1);
                tabControlInstr.SelectedTab = tabPageInstr1;
            }
            finally
            {
                tabControlInstr.ResumeLayout(true);
            }
        }

        private void SetPaymentInstrTabsSellerOnly()
        {
            tabControlInstr.SuspendLayout();
            try
            {
                tabControlInstr.TabPages.Clear();
                tabControlInstr.TabPages.Add(tabPageInstr2);
                tabControlInstr.SelectedTab = tabPageInstr2;
            }
            finally
            {
                tabControlInstr.ResumeLayout(true);
            }
        }

        private void SetPaymentInstrTabsBothBuyerSelected()
        {
            tabControlInstr.SuspendLayout();
            try
            {
                tabControlInstr.TabPages.Clear();
                tabControlInstr.TabPages.Add(tabPageInstr1);
                tabControlInstr.TabPages.Add(tabPageInstr2);
                tabControlInstr.SelectedTab = tabPageInstr1;
            }
            finally
            {
                tabControlInstr.ResumeLayout(true);
            }
        }

        private int FillContractRowFromPm(int idContract, TextBox contractCode, TextBox clientName)
        {
            base.IUbsChannel.ParamIn("ID_CONTRACT", idContract);

            base.IUbsChannel.Run("GetContractPM");

            UbsParam po = base.IUbsChannel.ParamsOutParam;

            if (contractCode != null && po.Contains("CODE_CONTRACT"))
                contractCode.Text = Convert.ToString(po.Value("CODE_CONTRACT"));

            if (clientName != null && po.Contains("LONG_NAME"))
                clientName.Text = Convert.ToString(po.Value("LONG_NAME"));

            if (!po.Contains("ID_CLIENT"))
                return 0;

            return Convert.ToInt32(po.Value("ID_CLIENT"));
        }

        private void UpdateDisplayNDS()
        {
            try
            {
                bool isCheck = false;
                if (m_idClient2 != 0)
                {
                    base.IUbsChannel.ParamIn("ClientId", m_idClient2);

                    base.IUbsChannel.Run("IsClientLegalEnterprise");

                    UbsParam po = base.IUbsChannel.ParamsOutParam;

                    bool isLegal = false;
                    if (po.Contains("Result"))
                    {
                        object r = po.Value("Result");
                        if (r != null && r != DBNull.Value)
                        {
                            if (r is bool)
                                isLegal = (bool)r;
                            else
                                isLegal = Convert.ToInt32(r) != 0;
                        }
                    }

                    int kindKey;
                    bool kindPhysical = UbsPmTradeComboUtil.TryGetSelectedKey(cmbKindSupplyTrade, out kindKey)
                        && kindKey == 2;
                    bool sellerBank = string.Equals(txtContractCode1.Text.Trim(), TextContractCodeBank, StringComparison.OrdinalIgnoreCase);
                    bool buyerBank = string.Equals(txtContractCode2.Text.Trim(), TextContractCodeBank, StringComparison.OrdinalIgnoreCase);
                    isCheck = kindPhysical && sellerBank && !buyerBank && isLegal;
                }
                if (isCheck)
                    chkNDS.Visible = true;
                else
                {
                    chkNDS.Visible = false;
                    chkNDS.Checked = false;
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                chkNDS.Visible = false;
                chkNDS.Checked = false;
            }
        }

        private void ClearComissionSelection()
        {
            if (cmbComission.Items == null || cmbComission.Items.Count == 0)
                return;

            cmbComission.SelectedIndex = -1;
        }

        private bool TrySelectComissionById(int commissionId)
        {
            if (commissionId == 0 || cmbComission.Items == null)
                return false;

            foreach (object item in cmbComission.Items)
            {
                if (item is KeyValuePair<int, string>)
                {
                    KeyValuePair<int, string> kv = (KeyValuePair<int, string>)item;
                    if (kv.Key == commissionId)
                    {
                        cmbComission.SelectedItem = item;

                        return true;
                    }
                }
            }

            return false;
        }

        private void FillOurBIK()
        {
            base.IUbsChannel.Run("FillOurBIK");
            UbsParam paramOut = base.IUbsChannel.ParamsOutParam;

            if (paramOut.Contains("Наш БИК"))
                m_ourBIK = Convert.ToString(paramOut.Value("Наш БИК"));
        }

        private void FillControlStorage(int storageId, bool isExternalStorage)
        {
            base.IUbsChannel.ParamIn("IsExternalStorage", isExternalStorage ? 1 : 0);
            base.IUbsChannel.ParamIn("Id", storageId);

            base.IUbsChannel.Run("GetStorage");

            UbsParam po = base.IUbsChannel.ParamsOutParam;
            if (po.Contains("Code"))
                txtStorageCode.Text = Convert.ToString(po.Value("Code"));
            if (po.Contains("Name"))
                txtStorageName.Text = Convert.ToString(po.Value("Name"));
        }

        private void FillListOblig(object arrObligData)
        {
            Array arr = arrObligData as Array;
            if (arr == null || arr.Rank != 2)
                return;

            int rowCount = arr.GetLength(0);
            int colCount = arr.GetLength(1);
            if (colCount < 10)
                return;

            lvwObligation.Items.Clear();

            for (int n = 0; n < rowCount; n++)
            {
                string direction = Convert.ToString(arr.GetValue(n, 0));
                ListViewItem item = new ListViewItem(direction);
                item.SubItems.Add(Convert.ToString(arr.GetValue(n, 1)));
                item.SubItems.Add(Convert.ToString(arr.GetValue(n, 2)));
                item.SubItems.Add(Convert.ToString(arr.GetValue(n, 3)));
                item.SubItems.Add(Convert.ToString(arr.GetValue(n, 4)));
                item.SubItems.Add(Convert.ToString(arr.GetValue(n, 5)));
                item.SubItems.Add(Convert.ToString(arr.GetValue(n, 6)));

                int curId = 0;
                object curIdObj = arr.GetValue(n, 7);
                if (curIdObj != null && curIdObj != DBNull.Value)
                    curId = Convert.ToInt32(curIdObj);
                item.SubItems.Add(Convert.ToString(curId));

                double rateFromServer = 0;
                object rateObj = arr.GetValue(n, 8);
                if (rateObj != null && rateObj != DBNull.Value)
                    rateFromServer = Convert.ToDouble(rateObj);

                if (rateFromServer != 0)
                {
                    item.SubItems.Add(Convert.ToString(rateFromServer));
                }
                else
                {
                    string computedRate = "0";
                    int paymentCurKey;
                    if (UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPayment, out paymentCurKey)
                        && !IsTradeDateMissingOrInvalid())
                    {
                        base.IUbsChannel.ParamIn("Id_Currency_Opl", paymentCurKey);
                        base.IUbsChannel.ParamIn("Id_Currency_Oblig", curId);
                        base.IUbsChannel.ParamIn("Date", dateTrade.DateValue);

                        base.IUbsChannel.Run("GetRate_CB");

                        UbsParam rateOut = base.IUbsChannel.ParamsOutParam;
                        if (rateOut.Contains("Rate"))
                        {
                            decimal rate = Convert.ToDecimal(rateOut.Value("Rate"));
                            computedRate = Math.Round(rate, 10).ToString();
                        }
                    }
                    item.SubItems.Add(computedRate);
                }

                item.SubItems.Add(Convert.ToString(arr.GetValue(n, 9)));
                item.SubItems.Add(rateFromServer != 0 ? "1" : "0");

                if (colCount > 11)
                    item.Tag = arr.GetValue(n, 11);

                lvwObligation.Items.Add(item);
            }
        }

        private void GetRateCB()
        {
            try
            {
                int paymentCurKey;
                int obligCurKey;
                if (!UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPayment, out paymentCurKey))
                    return;
                if (!UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyObligation, out obligCurKey))
                    return;
                if (IsTradeDateMissingOrInvalid())
                    return;

                base.IUbsChannel.ParamIn("Id_Currency_Opl", paymentCurKey);
                base.IUbsChannel.ParamIn("Id_Currency_Oblig", obligCurKey);
                base.IUbsChannel.ParamIn("Date", dateTrade.DateValue);
                base.IUbsChannel.Run("GetRate_CB");

                UbsParam rateOut = base.IUbsChannel.ParamsOutParam;
                if (!rateOut.Contains("Rate"))
                    return;

                decimal rate = Convert.ToDecimal(rateOut.Value("Rate"));
                ucdRateCurOblig.DecimalValue = Math.Round(rate, 10);

                if (ucdCostUnit.DecimalValue != 0 && rate != 0)
                    ucdCostCurPayment.Text = Math.Round(ucdCostUnit.DecimalValue * rate, 4).ToString();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void GetRateForPM()
        {
            try
            {
                int pmKey;
                int obligCurKey;
                if (!UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPost, out pmKey))
                    return;
                if (!UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyObligation, out obligCurKey))
                    return;
                if (IsTradeDateMissingOrInvalid())
                    return;

                base.IUbsChannel.ParamIn("Id_Pm", pmKey);
                base.IUbsChannel.ParamIn("Id_Currency_Oblig", obligCurKey);
                base.IUbsChannel.ParamIn("Date", dateTrade.DateValue);
                base.IUbsChannel.Run("GetRateForPM");

                UbsParam rateOut = base.IUbsChannel.ParamsOutParam;
                if (!rateOut.Contains("Rate_PM"))
                    return;

                decimal ratePm = Convert.ToDecimal(rateOut.Value("Rate_PM"));

                int unitKey;
                bool isOunce = UbsPmTradeComboUtil.TryGetSelectedKey(cmbUnit, out unitKey) && unitKey == 2;
                decimal costUnit = isOunce
                    ? Math.Round(ratePm * 31.1035m, 4)
                    : Math.Round(ratePm, 4);

                ucdCostUnit.DecimalValue = costUnit;

                if (ucdRateCurOblig.DecimalValue != 0 && costUnit != 0)
                    ucdCostCurPayment.Text = Math.Round(costUnit * ucdRateCurOblig.DecimalValue, 4).ToString();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void LockUiOnWasOperation()
        {
            tabPage4.Enabled = false;
            tabPage5.Enabled = false;
            tabPage6.Enabled = false;
            tabPageOblig1.Enabled = false;
            cmdAddObligation.Enabled = false;
            cmdEditObligation.Enabled = false;
            cmdDelObligation.Enabled = false;
            tabPageOblig2.Enabled = false;
            tabPage1.Enabled = false;
            btnSave.Enabled = false;
        }

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (m_suppressMainTabSelecting)
                return;
            if (m_obligEditingMode && e.TabPage != tabPage3)
            {
                e.Cancel = true;
                return;
            }

            try
            {
                TabPage target = e.TabPage;

                int k1;
                int k2;
                bool hasK1 = UbsPmTradeComboUtil.TryGetSelectedKey(cmbContractType1, out k1);
                bool hasK2 = UbsPmTradeComboUtil.TryGetSelectedKey(cmbContractType2, out k2);

                if (hasK1 && hasK2 && k1 == k2)
                {
                    MessageBox.Show(this, MsgContractTypesMustDiffer, MsgTitleValidationProps, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbContractType1.Focus();
                    e.Cancel = true;
                    return;
                }

                if (target == tabPage2)
                {
                    if (IsTradeDateMissingOrInvalid())
                    {
                        MessageBox.Show(this, MsgNoTradeDate, MsgTitleValidationProps, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        if (tabControl.SelectedTab == tabPage1 && dateTrade != null)
                            dateTrade.Focus();
                        e.Cancel = true;
                        return;
                    }
                    if (cmbCurrencyPost.SelectedIndex < 0)
                    {
                        MessageBox.Show(this, MsgNoCurrencyPost, MsgTitleValidationProps, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        cmbCurrencyPost.Focus();
                        e.Cancel = true;
                        return;
                    }
                    if (IsBuyerContractMissing())
                    {
                        MessageBox.Show(this, MsgNoBuyer, MsgTitleInputError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        if (linkContract2.Enabled)
                            linkContract2.Focus();
                        else if (cmbContractType2.Enabled)
                            cmbContractType2.Focus();

                        e.Cancel = true;
                        return;
                    }
                    if (cmbKindSupplyTrade.SelectedIndex < 0)
                    {
                        MessageBox.Show(this, MsgNoKindSupply, MsgTitleInputError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        cmbKindSupplyTrade.Focus();
                        e.Cancel = true;
                        return;
                    }

                    ApplyKindSupplyMassUnitForObligationsTab();
                }

                if (target == tabPage3)
                    ApplyDataTabUiOnSelecting();
                else
                {
                    btnSave.Visible = true;
                    btnExit.Visible = true;
                }

                SyncPaymentInstrTabsFromContractTypes();
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                e.Cancel = true;
            }
        }

        private static bool IsTradeDateMissingOrInvalid(UbsControl.UbsCtrlDate ctrl)
        {
            if (ctrl == null)
                return true;
            try
            {
                if (!ctrl.IsValidDate())
                {
                    return true;
                }
                DateTime date = ctrl.DateValue;

                if (date != null)
                {
                    if (date.Year == 2222 && date.Month == 1 && date.Day == 1)
                        return true;

                    return false;
                }
                return true;
            }
            catch
            {
                return true;
            }
        }

        private bool IsTradeDateMissingOrInvalid()
        {
            return IsTradeDateMissingOrInvalid(dateTrade);
        }

        private bool IsBuyerContractMissing()
        {
            int k2;
            if (!UbsPmTradeComboUtil.TryGetSelectedKey(cmbContractType2, out k2))
                return true;
            if (k2 == 0)
                return false;

            return string.IsNullOrEmpty(txtContractCode2.Text.Trim());
        }

        private void ApplyKindSupplyMassUnitForObligationsTab()
        {
            int kk;
            if (!UbsPmTradeComboUtil.TryGetSelectedKey(cmbKindSupplyTrade, out kk))
                return;

            if (kk == 2)
            {
                if (cmbUnit.Items.Count > 0)
                    cmbUnit.SelectedIndex = 0;

                cmbUnit.Enabled = false;
                ucdMass.Enabled = false;
            }
            else if (kk == 1)
            {
                ucdMass.Enabled = true;
                cmbUnit.Enabled = true;
            }
        }

        private void ApplyDataTabUiOnSelecting()
        {
            if (lvwObligation.Items.Count == 0)
                return;

            btnSave.Visible = false;
            btnExit.Visible = false;

            if (!m_blnAddOblig && !m_blnEditOblig)
            {
                cmdApplayObligation.Visible = false;
                cmdExitObligation.Visible = false;

                tabPageOblig1.Enabled = false;
                tabPageOblig2.Enabled = false;

                cmdAddObject.Enabled = false;
                cmdDelObject.Enabled = false;

                tabControlOblig.SelectedTab = tabPageOblig1;

                if (lvwObligation.SelectedItems.Count == 0 && lvwObligation.Items.Count > 0)
                    lvwObligation.Items[0].Selected = true;

                if (lvwObligation.SelectedItems.Count > 0)
                {
                    ListViewItem sel = lvwObligation.SelectedItems[0];
                    CallOblig("Edit");
                }
            }
            else
            {
                cmdApplayObligation.Visible = true;
                cmdExitObligation.Visible = true;

                tabPageOblig1.Enabled = true;
                tabPageOblig2.Enabled = true;

                int kk;
                if (UbsPmTradeComboUtil.TryGetSelectedKey(cmbKindSupplyTrade, out kk) && kk == 2)
                {
                    cmdAddObject.Enabled = true;
                    cmdDelObject.Enabled = true;
                }
                else
                {
                    cmdAddObject.Enabled = false;
                    cmdDelObject.Enabled = false;
                }
            }
        }

        private void SyncPaymentInstrTabsFromContractTypes()
        {
            int k1;
            int k2;
            if (!UbsPmTradeComboUtil.TryGetSelectedKey(cmbContractType1, out k1))
                return;
            if (!UbsPmTradeComboUtil.TryGetSelectedKey(cmbContractType2, out k2))
                return;
            if (k1 == 0)
                SetPaymentInstrTabsBuyerOnly();
            else if (k2 == 0)
                SetPaymentInstrTabsSellerOnly();
            else
                SetPaymentInstrTabsBothBuyerSelected();
        }

        private void CallOblig(string type)
        {
            string direction = string.Empty;

            if (string.Equals(type, PrefixAddOblig, StringComparison.Ordinal))
            {
                direction = TextTradeDirectionDirect;
                m_strNumInPart = string.Empty;

                if (m_kindTrade == 0)
                {//наличные сделки
                    datePayment.DateValue = dateTrade.DateValue;
                    datePost.DateValue = dateTrade.DateValue;
                    datePayment.Enabled = false;
                    datePost.Enabled = false;
                }
                else
                {
                    datePayment.DateValue = m_dateToday;
                    datePost.DateValue = m_dateToday;
                }

                ucdCostUnit.Text = "0";
                ucdMass.Text = "0";
                ucdMassGramm.Text = "0";
                ucdSumObligation.Text = "0";
                ucdSumPayment.Text = "0";

                lblObligationInfo2.Text = "Цена: " + ucdCostUnit.Text
                    + "   Масса: " + ucdMass.Text
                    + "   Сумма: " + ucdSumPayment.Text;

                chkRate.Checked = false;
                ClearDataOblig();
            }
            else
            {
                if (lvwObligation.Items.Count == 0)
                    return;

                datePayment.Enabled = (m_kindTrade != 0);
                datePost.Enabled = (m_kindTrade != 0);

                ListViewItem sel;
                if (lvwObligation.SelectedItems.Count > 0)
                    sel = lvwObligation.SelectedItems[0];
                else
                {
                    lvwObligation.Items[0].Selected = true;
                    sel = lvwObligation.Items[0];
                }

                direction = sel.Text;
                m_strNumInPart = sel.SubItems[1].Text;

                DateTime dt;
                if (DateTime.TryParse(sel.SubItems[2].Text, out dt))
                    datePayment.DateValue = dt;
                if (DateTime.TryParse(sel.SubItems[3].Text, out dt))
                    datePost.DateValue = dt;

                ucdCostUnit.Text = sel.SubItems[4].Text;
                ucdMass.Text = sel.SubItems[5].Text;
                GetMassGramm();
                ucdSumObligation.Text = sel.SubItems[6].Text;

                int curId;
                if (int.TryParse(sel.SubItems[7].Text, out curId))
                    UbsPmTradeComboUtil.SetComboByKey(cmbCurrencyObligation, curId);

                ucdRateCurOblig.Text = sel.SubItems[8].Text;

                for (int i = 0; i < cmbUnit.Items.Count; i++)
                {
                    if (cmbUnit.Items[i] is KeyValuePair<int, string>)
                    {
                        KeyValuePair<int, string> kv = (KeyValuePair<int, string>)cmbUnit.Items[i];
                        if (string.Equals(kv.Value, sel.SubItems[9].Text, StringComparison.OrdinalIgnoreCase))
                        {
                            cmbUnit.SelectedIndex = i;
                            break;
                        }
                    }
                }

                chkRate.Checked = (sel.SubItems.Count > 10 && sel.SubItems[10].Text == "1");

                decimal costUnit;
                decimal rateCur;
                if (decimal.TryParse(ucdCostUnit.Text, out costUnit)
                    && decimal.TryParse(ucdRateCurOblig.Text, out rateCur))
                {
                    ucdCostCurPayment.Text = Math.Round(costUnit * rateCur, 4).ToString();
                    decimal sumOblig;
                    if (decimal.TryParse(ucdSumObligation.Text, out sumOblig))
                        ucdSumPayment.Text = Math.Round(sumOblig * rateCur, 2).ToString();
                }

                m_strNaprTrade = direction;
                FillDataOblig();
            }

            if (string.IsNullOrEmpty(direction) && chkComposit.Checked)
            {
                cmbTradeDirection.SelectedIndex = -1;
            }
            else
            {
                for (int i = 0; i < cmbTradeDirection.Items.Count; i++)
                {
                    if (cmbTradeDirection.Items[i] is KeyValuePair<int, string>)
                    {
                        KeyValuePair<int, string> kv = (KeyValuePair<int, string>)cmbTradeDirection.Items[i];
                        if (string.Equals(kv.Value, direction, StringComparison.OrdinalIgnoreCase))
                        {
                            cmbTradeDirection.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }

            if (m_blnAddOblig || m_blnEditOblig)
            {
                m_suppressMainTabSelecting = true;
                try
                {
                    tabControl.SelectedTab = tabPage3;
                }
                finally
                {
                    m_suppressMainTabSelecting = false;
                }
                tabControlOblig.SelectedTab = tabPageOblig1;

                if (cmbTradeDirection.Enabled)
                    cmbTradeDirection.Focus();
                else
                    cmbCurrencyObligation.Focus();
            }
        }

        #endregion

        #region Вспомогательные методы — обязательства

        private void DelOblig()
        {
            if (lvwObligation == null || lvwObligation.Items.Count == 0)
                return;

            List<string> nums = new List<string>();
            foreach (ListViewItem it in lvwObligation.Items)
            {
                if (it.SubItems.Count > 1)
                    nums.Add(it.SubItems[1].Text);
                else
                    nums.Add(it.Text);
            }

            for (int i = 0; i < nums.Count; i++)
            {
                string s = nums[i];
                string objKey = PrefixObligObject + s;
                if (m_paramOblig.Contains(objKey))
                    m_paramOblig.Remove(objKey);
                m_paramOblig[PrefixDeleteOblig + s] = true;
            }

            lvwObligation.Items.Clear();
            m_needSendOblig = true;
            m_maxNumPart1 = 0;
            m_maxNumPart2 = 0;
        }

        private void ClearObligObjectParamsPart2()
        {
            if (m_paramOblig.Count == 0)
                return;

            List<string> keys = new List<string>();
            foreach (KeyValuePair<string, object> kv in m_paramOblig)
            {
                if (UbsPmTradeObligParamUtil.IsObjectParamPart2Key(kv.Key, PrefixObligObject))
                    keys.Add(kv.Key);
            }
            for (int i = 0; i < keys.Count; i++)
                m_paramOblig.Remove(keys[i]);
        }

        private void RemoveReverseObligationListItems()
        {
            if (lvwObligation == null || lvwObligation.Items.Count == 0)
                return;

            for (int i = lvwObligation.Items.Count - 1; i >= 0; i--)
            {
                ListViewItem it = lvwObligation.Items[i];
                if (string.Equals(it.Text, TextTradeDirectionReverse, StringComparison.Ordinal))
                    lvwObligation.Items.RemoveAt(i);
            }
        }

        private void GetSumOblig()
        {
            ucdSumObligation.Text = Math.Round(ucdCostUnit.DecimalValue * ucdMass.DecimalValue, 2).ToString();
        }

        private void GetSumPayment()
        {
            if (chkSumInCurValue.Checked)
            {
                ucdSumPayment.Text = Math.Round(ucdCostCurPayment.DecimalValue * ucdMass.DecimalValue, 2).ToString();
            }
            else
            {
                ucdSumPayment.Text = Math.Round(ucdSumObligation.DecimalValue * ucdSumObligation.DecimalValue, 2).ToString();
            }
        }

        private void GetMassGramm()
        {
            int unitKey;
            bool isOunce = UbsPmTradeComboUtil.TryGetSelectedKey(cmbUnit, out unitKey) && unitKey == 2;
            decimal factor = isOunce ? 31.1035m : 1m;

            ucdMassGramm.DecimalValue = Math.Round(ucdMass.DecimalValue * factor, 1);
        }

        private void GetRateCurOblig()
        {
            if (ucdCostUnit.DecimalValue > 0)
            {
                ucdRateCurOblig.DecimalValue = Math.Round(ucdCostCurPayment.DecimalValue / ucdCostUnit.DecimalValue, 10);
            }
            else
            {
                ucdRateCurOblig.DecimalValue = 0m;
            }
        }

        private void ClearDataOblig()
        {
            lvwObject.Items.Clear();
        }

        private void FillDataOblig()
        {
            string key = PrefixObligObject + m_strNumInPart;
            if (!m_paramOblig.Contains(key))
            {
                int kk;
                if (UbsPmTradeComboUtil.TryGetSelectedKey(cmbKindSupplyTrade, out kk) && kk == 2)
                {
                    ucdMass.Text = "0";
                    ucdMassGramm.Text = "0";
                    GetSumOblig();
                    GetSumPayment();
                    lblObligationInfo2.Text = "Цена: " + ucdCostUnit.Text
                        + "   Масса: " + ucdMass.Text
                        + "   Сумма: " + ucdSumPayment.Text;
                }
                return;
            }

            object val = m_paramOblig[key];
            Array arr = val as Array;
            if (arr == null || arr.Rank != 2)
                return;

            lvwObject.Items.Clear();
            ucdMass.Text = "0";

            int rowCount = arr.GetLength(0);
            for (int i = 0; i < rowCount; i++)
            {
                object idObj = arr.GetValue(i, 0);
                if (idObj == null || idObj == DBNull.Value)
                    continue;
                int idObject = Convert.ToInt32(idObj);
                FillDataObject(idObject);
            }

            lblObligationInfo2.Text = "Цена: " + ucdCostUnit.Text
                + "   Масса: " + ucdMass.Text
                + "   Сумма: " + ucdSumPayment.Text;
        }

        /// <summary>
        /// Загружает данные одного объекта из канала и добавляет строку в lvwObject.
        /// </summary>
        private void FillDataObject(int idObject)
        {
            base.IUbsChannel.ParamIn("ID_OBJECT", idObject);
            base.IUbsChannel.Run("GetObjectPM");
            var po = base.IUbsChannel.ParamsOutParam;

            if (!po.Contains("ДанныеОбъекта"))
                return;

            Array arr = po.Value("ДанныеОбъекта") as Array;
            if (arr == null || arr.Rank != 2)
                return;

            int cols = arr.GetLength(1);
            ListViewItem item = new ListViewItem(cols > 0 ? Convert.ToString(arr.GetValue(0, 0)) : string.Empty);
            for (int c = 1; c < cols; c++)
                item.SubItems.Add(Convert.ToString(arr.GetValue(0, c)));

            lvwObject.Items.Add(item);

            decimal objMass = 0;
            if (cols > 2)
            {
                object mv = arr.GetValue(0, 2);
                if (mv != null && mv != DBNull.Value)
                    objMass = Convert.ToDecimal(mv);
            }

            ucdMass.Text = (ucdMass.DecimalValue + objMass).ToString();

            GetMassGramm();
            GetSumOblig();
            GetSumPayment();

            UpdateObligInfoLabel();
        }

        private bool CheckDataOblig()
        {
            if (cmbTradeDirection.SelectedIndex < 0)
            {
                MessageBox.Show(this, MsgNoDirection, MsgTitleValidationProps,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            decimal costUnit;
            if (!decimal.TryParse(ucdCostUnit.Text, out costUnit) || costUnit == 0)
            {
                MessageBox.Show(this, MsgNoCostUnit, MsgTitleValidationProps,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                ucdCostUnit.Focus();
                return false;
            }

            if (IsTradeDateMissingOrInvalid(datePost))
            {
                MessageBox.Show(this, MsgNoDeliveryDate, MsgTitleValidationProps,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                datePost.Focus();
                return false;
            }

            if (datePost.DateValue < dateTrade.DateValue)
            {
                MessageBox.Show(this, MsgDeliveryDateBeforeTrade, MsgTitleValidationProps,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                datePost.Focus();
                return false;
            }

            if (IsTradeDateMissingOrInvalid(datePayment))
            {
                MessageBox.Show(this, MsgNoPaymentDate, MsgTitleValidationProps,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                datePayment.Focus();
                return false;
            }

            if (datePayment.DateValue < dateTrade.DateValue)
            {
                MessageBox.Show(this, MsgPaymentDateBeforeTrade, MsgTitleValidationProps,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                datePayment.Focus();
                return false;
            }

            decimal rateCur;
            if (!decimal.TryParse(ucdRateCurOblig.Text, out rateCur) || rateCur == 0)
            {
                MessageBox.Show(this, MsgNoExchangeRate, MsgTitleValidationProps,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                ucdRateCurOblig.Focus();
                return false;
            }

            int kk;
            UbsPmTradeComboUtil.TryGetSelectedKey(cmbKindSupplyTrade, out kk);

            if (kk == 1)
            {
                decimal mass;
                if (!decimal.TryParse(ucdMass.Text, out mass) || mass == 0)
                {
                    MessageBox.Show(this, MsgNoMass, MsgTitleValidationProps,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ucdMass.Focus();
                    return false;
                }
            }

            if (kk == 2)
            {
                if (lvwObject.Items.Count == 0)
                {
                    MessageBox.Show(this, MsgNoObjects, MsgTitleValidationProps,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tabControlOblig.SelectedTab = tabPageOblig2;
                    cmdAddObject.Focus();
                    return false;
                }
            }

            return true;
        }

        private void SetResultsListOblig(string sType)
        {
            string direction = string.Empty;
            if (cmbTradeDirection.SelectedItem is KeyValuePair<int, string>)
                direction = ((KeyValuePair<int, string>)cmbTradeDirection.SelectedItem).Value;

            string strDatePayment = datePayment.DateValue.ToShortDateString();
            string strDatePost = datePost.DateValue.ToShortDateString();
            string strCostUnit = ucdCostUnit.Text;
            string strMass = ucdMass.Text;
            string strSumOblig = ucdSumObligation.Text;

            int obligCurKey = 0;
            UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyObligation, out obligCurKey);
            string strCurId = obligCurKey.ToString();
            string strRate = ucdRateCurOblig.Text;

            string unitText = string.Empty;
            if (cmbUnit.SelectedItem is KeyValuePair<int, string>)
                unitText = ((KeyValuePair<int, string>)cmbUnit.SelectedItem).Value;

            string rateFlag = chkRate.Checked ? "1" : "0";

            if (string.Equals(sType, PrefixAddOblig, StringComparison.Ordinal))
            {
                ListViewItem item = new ListViewItem(direction);
                item.SubItems.Add(m_strNumInPart);
                item.SubItems.Add(strDatePayment);
                item.SubItems.Add(strDatePost);
                item.SubItems.Add(strCostUnit);
                item.SubItems.Add(strMass);
                item.SubItems.Add(strSumOblig);
                item.SubItems.Add(strCurId);
                item.SubItems.Add(strRate);
                item.SubItems.Add(unitText);
                item.SubItems.Add(rateFlag);
                lvwObligation.Items.Add(item);
            }
            else
            {
                if (lvwObligation.SelectedItems.Count == 0)
                    return;
                ListViewItem sel = lvwObligation.SelectedItems[0];
                sel.Text = direction;
                sel.SubItems[1].Text = m_strNumInPart;
                sel.SubItems[2].Text = strDatePayment;
                sel.SubItems[3].Text = strDatePost;
                sel.SubItems[4].Text = strCostUnit;
                sel.SubItems[5].Text = strMass;
                sel.SubItems[6].Text = strSumOblig;
                sel.SubItems[7].Text = strCurId;
                sel.SubItems[8].Text = strRate;
                sel.SubItems[9].Text = unitText;
                sel.SubItems[10].Text = rateFlag;
            }
        }

        private void FillArrObject(out object[,] objectArray)
        {
            int count = lvwObject.Items.Count;
            if (count == 0)
            {
                objectArray = new object[0, 0];
                return;
            }
            objectArray = new object[count, 1];
            for (int i = 0; i < count; i++)
            {
                ListViewItem item = lvwObject.Items[i];
                int idObject = 0;
                if (item.SubItems.Count > 6)
                    int.TryParse(item.SubItems[6].Text, out idObject);
                objectArray[i, 0] = idObject;
            }
        }

        private void EnableObligEditMode()
        {
            m_obligEditingMode = true;
            tabPage3.Enabled = true;
            btnSave.Visible = false;
            btnExit.Visible = false;
            cmdApplayObligation.Visible = true;
            cmdExitObligation.Visible = true;
            tabPageOblig1.Enabled = true;
            tabPageOblig2.Enabled = true;

            int kk;
            if (UbsPmTradeComboUtil.TryGetSelectedKey(cmbKindSupplyTrade, out kk) && kk == 2)
            {
                cmdAddObject.Enabled = true;
                cmdDelObject.Enabled = true;
            }
        }

        private void DisableObligEditMode()
        {
            m_obligEditingMode = false;
            m_blnAddOblig = false;
            m_blnEditOblig = false;

            tabControlOblig.SelectedTab = tabPageOblig1;
            tabPageOblig1.Enabled = false;
            tabPageOblig2.Enabled = false;
            cmdApplayObligation.Visible = false;
            cmdExitObligation.Visible = false;

            btnSave.Visible = true;
            btnExit.Visible = true;

            m_suppressMainTabSelecting = true;
            try
            {
                tabControl.SelectedTab = tabPage2;
            }
            finally
            {
                m_suppressMainTabSelecting = false;
            }
        }

        private void GetMassaPrecision()
        {
            if (cmbUnit.Text == "грамм")
                ucdMass.Precision = (m_strCB == "A99") ? 0 : 1;
            else if (cmbUnit.Text == "унция")
                ucdMass.Precision = 3;
            else
                ucdMass.Precision = 2;
        }

        private void UpdateObligInfoLabel()
        {
            lblObligationInfo2.Text = "Цена: " + ucdCostUnit.Text
                + "   Масса: " + ucdMass.Text
                + "   Сумма: " + ucdSumPayment.Text;
        }

        private bool ExistObject()
        {
            foreach (KeyValuePair<string, object> kv in m_paramOblig)
            {
                if (kv.Key.StartsWith(PrefixObligObject))
                    return true;
            }
            return false;
        }

        private string[] DefineArrStrNumInPart()
        {
            int count = lvwObligation.Items.Count;
            if (count == 0) return null;

            string[] result = new string[count];
            for (int i = 0; i < count; i++)
                result[i] = lvwObligation.Items[i].SubItems[1].Text;
            return result;
        }

        private bool CheckKey(int g)
        {
            string bik = (g == 0) ? txtBIK0.Text : txtBIK1.Text;
            string rs = (g == 0) ? ucaRS0.Text : ucaRS1.Text;
            string ks = (g == 0) ? ucaKS0.Text : ucaKS1.Text;

            if (bik.Length > 0 && rs.Length > 0)
            {
                base.IUbsChannel.ParamIn("БИК", bik);
                base.IUbsChannel.ParamIn("РС", rs);
                base.IUbsChannel.ParamIn("КС", ks);
                base.IUbsChannel.Run("TradeCheckKey");
                UbsParam keyOut = base.IUbsChannel.ParamsOutParam;

                string report = keyOut.Contains("Отчет")
                    ? Convert.ToString(keyOut.Value("Отчет")) : string.Empty;
                if (report.Length > 0)
                {
                    MessageBox.Show(this, report, MsgCheckKey,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private bool CheckINN(int g)
        {
            string inn = (g == 0) ? txtINN0.Text : txtINN1.Text;

            base.IUbsChannel.ParamIn("ИНН", inn);
            base.IUbsChannel.Run("TradeCheckINN");
            UbsParam innOut = base.IUbsChannel.ParamsOutParam;

            string report = innOut.Contains("Отчет")
                ? Convert.ToString(innOut.Value("Отчет")) : string.Empty;
            if (report.Length > 0)
            {
                MessageBox.Show(this, report, MsgCheckINN,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool FillKS(int g)
        {
            string bik = (g == 0) ? txtBIK0.Text : txtBIK1.Text;

            base.IUbsChannel.ParamIn("БИК", bik);
            base.IUbsChannel.Run("FillRekv");
            UbsParam rekvOut = base.IUbsChannel.ParamsOutParam;

            if (rekvOut.Contains("Отчет"))
            {
                MessageBox.Show(this, Convert.ToString(rekvOut.Value("Отчет")),
                    MsgCheckBIK, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (g == 0) { txtName0.Text = string.Empty; ucaKS0.Text = string.Empty; txtBIK0.Focus(); }
                else { txtName1.Text = string.Empty; ucaKS1.Text = string.Empty; txtBIK1.Focus(); }
                return false;
            }

            string bankName = Convert.ToString(rekvOut.Value("Банк"));
            if (g == 0) txtName0.Text = bankName;
            else txtName1.Text = bankName;

            if (rekvOut.Contains("КС"))
            {
                string ks = Convert.ToString(rekvOut.Value("КС"));
                if (g == 0) ucaKS0.Text = ks;
                else ucaKS1.Text = ks;
            }

            bool isOurBIK = rekvOut.Contains("Наш БИК")
                && Convert.ToBoolean(rekvOut.Value("Наш БИК"));

            if (isOurBIK)
            {
                if (g == 0) { ucaRS0.ReadOnly = true; linkAccountPayment0.Enabled = true; }
                else { ucaRS1.ReadOnly = true; linkAccountPayment1.Enabled = true; }
            }
            else
            {
                if (g == 0) { ucaRS0.ReadOnly = false; linkAccountPayment0.Enabled = false; }
                else { ucaRS1.ReadOnly = false; linkAccountPayment1.Enabled = false; }
            }

            if (!CheckKey(g))
            {
                if (g == 0) { if (!ucaRS0.ReadOnly) ucaRS0.Focus(); else linkAccountPayment0.Focus(); }
                else { if (!ucaRS1.ReadOnly) ucaRS1.Focus(); else linkAccountPayment1.Focus(); }
            }

            return true;
        }

        private object[,] FillArrOblig()
        {
            int count = lvwObligation.Items.Count;
            if (count == 0) return null;

            object[,] arr = new object[count, 12];

            int paymentKey = 0, postKey = 0;
            UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPayment, out paymentKey);
            UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPost, out postKey);

            for (int n = 0; n < count; n++)
            {
                ListViewItem item = lvwObligation.Items[n];
                arr[n, 0] = item.Text;
                arr[n, 1] = item.SubItems[1].Text;
                arr[n, 2] = item.SubItems[2].Text;
                arr[n, 3] = item.SubItems[3].Text;

                decimal dv;
                arr[n, 4] = decimal.TryParse(item.SubItems[4].Text, out dv) ? dv : 0m;
                arr[n, 5] = decimal.TryParse(item.SubItems[5].Text, out dv) ? dv : 0m;
                arr[n, 6] = decimal.TryParse(item.SubItems[6].Text, out dv) ? dv : 0m;

                arr[n, 7] = paymentKey;
                arr[n, 8] = postKey;
                arr[n, 9] = item.SubItems[7].Text;

                bool isFixedRate = (item.SubItems.Count > 10 && item.SubItems[10].Text == "1");
                if (isFixedRate)
                {
                    double rd;
                    arr[n, 10] = double.TryParse(item.SubItems[8].Text, out rd) ? rd : 0.0;
                }
                else
                {
                    arr[n, 10] = 0m;
                }
                arr[n, 11] = item.SubItems[9].Text;
            }

            return arr;
        }

        private object[,] FillArrDataInstr(int index)
        {
            object[,] arr = new object[1, 8];
            if (index == 0)
            {
                arr[0, 0] = txtBIK0.Text;
                arr[0, 1] = txtName0.Text;
                arr[0, 2] = ucaKS0.Text;
                arr[0, 3] = ucaRS0.Text;
                arr[0, 4] = txtClient0.Text;
                arr[0, 5] = txtNote0.Text;
                arr[0, 6] = txtINN0.Text;
                arr[0, 7] = Math.Abs(chkNotAkcept0.Checked ? 1 : 0);
            }
            else
            {
                arr[0, 0] = txtBIK1.Text;
                arr[0, 1] = txtName1.Text;
                arr[0, 2] = ucaKS1.Text;
                arr[0, 3] = ucaRS1.Text;
                arr[0, 4] = txtClient1.Text;
                arr[0, 5] = txtNote1.Text;
                arr[0, 6] = txtINN1.Text;
                arr[0, 7] = Math.Abs(chkNotAkcept1.Checked ? 1 : 0);
            }
            return arr;
        }

        #endregion

        #region Валидация и сохранение

        private bool CheckData()
        {
            int typeKey1 = 0, typeKey2 = 0;
            UbsPmTradeComboUtil.TryGetSelectedKey(cmbContractType1, out typeKey1);
            UbsPmTradeComboUtil.TryGetSelectedKey(cmbContractType2, out typeKey2);
            if (typeKey1 == typeKey2)
            {
                MessageBox.Show(this, MsgContractTypesMustDiffer, MsgTitleValidationProps,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (cmbKindSupplyTrade.SelectedIndex < 0)
            {
                MessageBox.Show(this, MsgNoKindSupply, MsgTitleInputError,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (tabControl.SelectedTab == tabPage1) cmbKindSupplyTrade.Focus();
                return false;
            }

            if (IsTradeDateMissingOrInvalid())
            {
                MessageBox.Show(this, MsgNoTradeDate, MsgTitleValidationProps,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (tabControl.SelectedTab == tabPage1) dateTrade.Focus();
                return false;
            }

            if (txtTradeNum.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, MsgNoTradeNum, MsgTitleInputError,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                m_suppressMainTabSelecting = true;
                tabControl.SelectedTab = tabPage1;
                m_suppressMainTabSelecting = false;
                txtTradeNum.Focus();
                return false;
            }

            if (cmbCurrencyPost.SelectedIndex < 0)
            {
                MessageBox.Show(this, MsgNoCurrencyPost, MsgTitleInputError,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (tabControl.SelectedTab == tabPage1) cmbCurrencyPost.Focus();
                return false;
            }

            if (cmbCurrencyPayment.SelectedIndex < 0)
            {
                MessageBox.Show(this, MsgNoCurrencyPayment, MsgTitleInputError,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (tabControl.SelectedTab == tabPage1) cmbCurrencyPayment.Focus();
                return false;
            }

            if (cmbCurrencyObligation.SelectedIndex < 0)
            {
                MessageBox.Show(this, MsgNoCurrencyObligation, MsgTitleInputError,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                m_suppressMainTabSelecting = true;
                tabControl.SelectedTab = tabPage3;
                m_suppressMainTabSelecting = false;
                tabControlOblig.SelectedTab = tabPageOblig1;
                cmbCurrencyObligation.Focus();
                return false;
            }

            if (cmbUnit.SelectedIndex < 0)
            {
                MessageBox.Show(this, MsgNoWeightUnit, MsgTitleInputError,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                m_suppressMainTabSelecting = true;
                tabControl.SelectedTab = tabPage3;
                m_suppressMainTabSelecting = false;
                tabControlOblig.SelectedTab = tabPageOblig1;
                cmbUnit.Focus();
                return false;
            }

            if (txtContractCode1.Text.Length == 0)
            {
                MessageBox.Show(this, MsgNoSeller, MsgTitleInputError,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (tabControl.SelectedTab == tabPage1) linkContract1.Focus();
                return false;
            }

            if (txtContractCode2.Text.Length == 0)
            {
                MessageBox.Show(this, MsgNoBuyer, MsgTitleInputError,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (tabControl.SelectedTab == tabPage1) linkContract2.Focus();
                return false;
            }

            if (lvwObligation.Items.Count == 0)
            {
                MessageBox.Show(this, MsgNoObligations, MsgTitleInputError,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (tabControlInstr.TabPages.Contains(tabPageInstr1))
            {
                if (txtBIK0.Text.Trim().Length == 0
                    || ucaKS0.Text == "0"
                    || ucaRS0.Text == "0")
                {
                    MessageBox.Show(this, MsgNoBuyerInstruction, MsgTitleInputError,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    m_suppressMainTabSelecting = true;
                    tabControl.SelectedTab = tabPage5;
                    m_suppressMainTabSelecting = false;
                    tabControlInstr.SelectedTab = tabPageInstr1;
                    linkListInstr0.Focus();
                    return false;
                }
            }

            if (tabControlInstr.TabPages.Contains(tabPageInstr2)
                && tabPageInstr2.Enabled)
            {
                if (txtBIK1.Text.Trim().Length == 0
                    || ucaKS1.Text == "0"
                    || ucaRS1.Text == "0")
                {
                    MessageBox.Show(this, MsgNoSellerInstruction, MsgTitleInputError,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    m_suppressMainTabSelecting = true;
                    tabControl.SelectedTab = tabPage5;
                    m_suppressMainTabSelecting = false;
                    tabControlInstr.SelectedTab = tabPageInstr2;
                    linkListInstr1.Focus();
                    return false;
                }
            }

            if (cmbKindSupplyTrade.Text == TextKindPhysical)
            {
                for (int n = 0; n < lvwObligation.Items.Count; n++)
                {
                    string numKey = PrefixObligObject + lvwObligation.Items[n].SubItems[1].Text;
                    if (!m_paramOblig.Contains(numKey))
                    {
                        MessageBox.Show(this, MsgNoObjectsByObligation, MsgTitleInputError,
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                }
            }

            if (tabPage4.Parent == tabControl)
            {
                if (txtStorageCode.Text.Trim().Length == 0)
                {
                    MessageBox.Show(this, MsgNoStorageInstruction, MsgTitleInputError,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    m_suppressMainTabSelecting = true;
                    tabControl.SelectedTab = tabPage4;
                    m_suppressMainTabSelecting = false;
                    linkStorage.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool CheckDatesOblig()
        {
            if (IsTradeDateMissingOrInvalid())
            {
                MessageBox.Show(this, MsgSpecifyTradeDate, MsgTitleValidationProps,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                m_suppressMainTabSelecting = true;
                tabControl.SelectedTab = tabPage1;
                m_suppressMainTabSelecting = false;
                dateTrade.Focus();
                return false;
            }

            for (int n = 0; n < lvwObligation.Items.Count; n++)
            {
                ListViewItem item = lvwObligation.Items[n];
                DateTime dDateOpl, dDatePost;
                DateTime.TryParse(item.SubItems[2].Text, out dDateOpl);
                DateTime.TryParse(item.SubItems[3].Text, out dDatePost);

                if (m_kindTrade == 0)
                {
                    item.SubItems[2].Text = dateTrade.DateValue.ToShortDateString();
                    item.SubItems[3].Text = dateTrade.DateValue.ToShortDateString();
                }
                else
                {
                    if (dDatePost < dateTrade.DateValue)
                    {
                        MessageBox.Show(this, MsgDeliveryDateBeforeTrade, MsgTitleValidationProps,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    if (dDateOpl < dateTrade.DateValue)
                    {
                        MessageBox.Show(this, MsgPaymentDateBeforeTrade, MsgTitleValidationProps,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            int paymentCurKey = 0;
            UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPayment, out paymentCurKey);

            base.IUbsChannel.ParamIn("IdCurrency", paymentCurKey);
            base.IUbsChannel.Run("DefineCodCurrency");
            UbsParam codOut = base.IUbsChannel.ParamsOutParam;
            string strCodCurrencyOpl = codOut.Contains("CodCB")
                ? Convert.ToString(codOut.Value("CodCB")) : string.Empty;

            int ctKey2 = 0;
            UbsPmTradeComboUtil.TryGetSelectedKey(cmbContractType2, out ctKey2);
            if (tabControlInstr.TabPages.Contains(tabPageInstr1) && ctKey2 != 0)
            {
                string strRS = ucaRS0.Text;
                if (strRS.Length >= 8)
                {
                    string strCodCurrencyRS = strRS.Substring(5, 3);
                    if (txtBIK0.Text.Trim() == m_ourBIK)
                    {
                        if (!IsEqualNumCodeCurr(strCodCurrencyOpl, strCodCurrencyRS))
                        {
                            MessageBox.Show(this, MsgBuyerRsCurrencyMismatch, MsgTitleValidationProps,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    else if (strRS != "00000000000000000000")
                    {
                        if (!IsEqualNumCodeCurr(strCodCurrencyOpl, strCodCurrencyRS))
                        {
                            MessageBox.Show(this, MsgBuyerRsCurrencyMismatch, MsgTitleValidationProps,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }

            int ctKey1 = 0;
            UbsPmTradeComboUtil.TryGetSelectedKey(cmbContractType1, out ctKey1);
            if (tabControlInstr.TabPages.Contains(tabPageInstr2) && ctKey1 != 0)
            {
                string strRS = ucaRS1.Text;
                if (strRS.Length >= 8)
                {
                    string strCodRS1 = strRS.Substring(5, 3);
                    if (txtBIK1.Text.Trim() == m_ourBIK)
                    {
                        if (!IsEqualNumCodeCurr(strCodCurrencyOpl, strCodRS1))
                        {
                            MessageBox.Show(this, MsgSellerRsCurrencyMismatch, MsgTitleValidationProps,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    else if (strRS != "00000000000000000000")
                    {
                        if (!IsEqualNumCodeCurr(strCodCurrencyOpl, strCodRS1))
                        {
                            MessageBox.Show(this, MsgSellerRsCurrencyMismatch, MsgTitleValidationProps,
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private static bool IsEqualNumCodeCurr(string code1, string code2)
        {
            int n1, n2;
            if (int.TryParse(code1, out n1) && int.TryParse(code2, out n2))
                return n1 == n2;
            return string.Equals(code1, code2, StringComparison.Ordinal);
        }

        private void UpdateMcFromControls()
        {
            m_mc["DATE_TRADE"] = dateTrade.DateValue;
            m_mc["NUM_TRADE"] = txtTradeNum.Text;

            int tradeTypeKey;
            UbsPmTradeComboUtil.TryGetSelectedKey(cmbTradeType, out tradeTypeKey);
            m_mc["TYPE_TRADE"] = tradeTypeKey;

            m_mc["IS_COMPOSIT"] = chkComposit.Checked ? 1 : 0;

            int commKey;
            if (UbsPmTradeComboUtil.TryGetSelectedKey(cmbComission, out commKey))
                m_mc["Идентификатор комиссии"] = commKey;
            else
                m_mc["Идентификатор комиссии"] = 0;

            int curObligKey;
            UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyObligation, out curObligKey);
            m_mc["Идентификатор валюты обязательства"] = curObligKey;

            int curOplKey;
            UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPayment, out curOplKey);
            m_mc["Идентификатор валюты оплаты"] = curOplKey;

            int curPostKey;
            UbsPmTradeComboUtil.TryGetSelectedKey(cmbCurrencyPost, out curPostKey);
            m_mc["Идентификатор драг.металла"] = curPostKey;

            m_mc["Вид поставки по сделке"] = cmbKindSupplyTrade.SelectedIndex >= 0
                ? cmbKindSupplyTrade.Text : string.Empty;
        }

        private void SnapshotMc()
        {
            m_initialMc = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> kv in m_mc)
                m_initialMc[kv.Key] = kv.Value;
        }

        private bool IsMainDataChanged()
        {
            if (m_initialMc == null)
                return true;
            foreach (KeyValuePair<string, object> kv in m_mc)
            {
                object oldVal;
                if (!m_initialMc.TryGetValue(kv.Key, out oldVal))
                    return true;
                if (kv.Value == null && oldVal == null) continue;
                if (kv.Value == null || oldVal == null) return true;
                if (!kv.Value.Equals(oldVal)) return true;
            }
            return false;
        }

        #endregion

        #region Вспомогательные методы — оплата

        private LinkLabel GetInstrLink(int index)
        {
            return index == 0 ? linkListInstr0 : linkListInstr1;
        }

        private LinkLabel GetInstrAccountLink(int index)
        {
            return index == 0 ? linkAccountPayment0 : linkAccountPayment1;
        }

        private void FillControlInstrPayment(int index, object varOplata)
        {
            Array arr = varOplata as Array;
            if (arr == null || arr.Rank != 2) return;

            const int instrRow = 0;

            if (index == 0)
            {
                txtBIK0.Text = UbsPmTradeMatrixUtil.GetMatrixCellString(arr, instrRow, 0);
                txtName0.Text = UbsPmTradeMatrixUtil.GetMatrixCellString(arr, instrRow, 1);
                ucaKS0.Text = UbsPmTradeMatrixUtil.GetMatrixCellString(arr, instrRow, 2);
                ucaRS0.Text = UbsPmTradeMatrixUtil.GetMatrixCellString(arr, instrRow, 3);
                txtClient0.Text = UbsPmTradeMatrixUtil.GetMatrixCellString(arr, instrRow, 4);
                txtNote0.Text = UbsPmTradeMatrixUtil.GetMatrixCellString(arr, instrRow, 5);
                txtINN0.Text = UbsPmTradeMatrixUtil.GetMatrixCellString(arr, instrRow, 6);
                chkNotAkcept0.Checked = UbsPmTradeMatrixUtil.GetMatrixCellInt(arr, instrRow, 7) != 0;
            }
            else
            {
                txtBIK1.Text = UbsPmTradeMatrixUtil.GetMatrixCellString(arr, instrRow, 0);
                txtName1.Text = UbsPmTradeMatrixUtil.GetMatrixCellString(arr, instrRow, 1);
                ucaKS1.Text = UbsPmTradeMatrixUtil.GetMatrixCellString(arr, instrRow, 2);
                ucaRS1.Text = UbsPmTradeMatrixUtil.GetMatrixCellString(arr, instrRow, 3);
                txtClient1.Text = UbsPmTradeMatrixUtil.GetMatrixCellString(arr, instrRow, 4);
                txtNote1.Text = UbsPmTradeMatrixUtil.GetMatrixCellString(arr, instrRow, 5);
                txtINN1.Text = UbsPmTradeMatrixUtil.GetMatrixCellString(arr, instrRow, 6);
                chkNotAkcept1.Checked = UbsPmTradeMatrixUtil.GetMatrixCellInt(arr, instrRow, 7) != 0;
            }
        }

        private void ClearPayment(int index)
        {
            if (index == 0)
            {
                txtBIK0.Text = string.Empty;
                txtName0.Text = string.Empty;
                ucaKS0.Text = "0";
                ucaRS0.Text = "0";
                txtClient0.Text = string.Empty;
                txtNote0.Text = string.Empty;
                txtINN0.Text = string.Empty;
                chkNotAkcept0.Checked = false;
            }
            else
            {
                txtBIK1.Text = string.Empty;
                txtName1.Text = string.Empty;
                ucaKS1.Text = "0";
                ucaRS1.Text = "0";
                txtClient1.Text = string.Empty;
                txtNote1.Text = string.Empty;
                txtINN1.Text = string.Empty;
                chkNotAkcept1.Checked = false;
            }
        }

        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string filterAction = string.Empty;

            switch (((KeyValuePair<int, string>)cmbContractType1.SelectedItem).Key)
            {
                case 1:
                    filterAction = @"UBS_PM_BROKER_LIST";
                    break;
                case 2:
                    filterAction = @"UBS_PM_CONTRACT_LIST";
                    break;
            }

            object[] ids = this.Ubs_ActionRun(filterAction, this, null) as object[];

            if (ids != null && ids.Length > 0)
            {
                m_mc["Продавец"] = (int)ids[0];
                //получить данные контракта
                FillControlContract((int)m_mc["Продавец"], txtContractCode1, txtClientName1, out m_idClient1);

                UbsParam paramOut = base.IUbsChannel.ParamsOutParam;
                m_idComission1 = Convert.ToInt32(paramOut.Value("Идентификатор комиссии"));

                if (cmbContractType1.SelectedIndex == 1 && cmbContractType2.SelectedIndex != 1)
                {
                    foreach (KeyValuePair<int, string> item in cmbContractType1.Items)
                    {
                        if (item.Key == m_idComission1)
                        {
                            cmbContractType1.SelectedItem = item;
                        }
                    }
                }
                else
                {
                    cmbContractType1.SelectedIndex = -1;
                }
            }
            else
            {
                MessageBox.Show(MsgFltRecordsIsNotSelected, ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillControlContract(int idContract, TextBox txtContractCode, TextBox txtClientName, out int idClient)
        {
            base.IUbsChannel.ParamIn("ID_CONTRACT", idContract);
            base.IUbsChannel.Run("GetContractPM");

            txtContractCode.Text = Convert.ToString(base.IUbsChannel.ParamOut("CODE_CONTRACT"));
            txtClientName.Text = Convert.ToString(base.IUbsChannel.ParamOut("LONG_NAME"));
            idClient = Convert.ToInt32(base.IUbsChannel.ParamOut("ID_CLIENT"));
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string filterAction = string.Empty;

            if (cmbContractType1.SelectedIndex == 0)
            {
                filterAction = @"UBS_PM_BROKER_LIST";
            }
            else if (cmbContractType1.SelectedIndex == 1)
            {
                filterAction = @"UBS_PM_CONTRACT_LIST";
            }

            object[] ids = this.Ubs_ActionRun(filterAction, this, null) as object[];

            if (ids != null && ids.Length > 0)
            {
                m_mc["Покупатель"] = (int)ids[0];
                //получить данные контракта
                FillControlContract((int)m_mc["Покупатель"], txtContractCode2, txtClientName2, out m_idClient2);

                UbsParam paramOut = base.IUbsChannel.ParamsOutParam;
                m_idComission2 = Convert.ToInt32(paramOut.Value("Идентификатор комиссии"));

                if (cmbContractType2.SelectedIndex == 1 && cmbContractType1.SelectedIndex != 1)
                {
                    foreach (KeyValuePair<int, string> item in cmbContractType2.Items)
                    {
                        if (item.Key == m_idComission2)
                        {
                            cmbContractType2.SelectedItem = item;
                        }
                    }
                }
                else
                {
                    cmbContractType2.SelectedIndex = -1;
                }

                UpdateDisplayNDS();
                UpdateDisplayExport();
            }
            else
            {
                MessageBox.Show(MsgFltRecordsIsNotSelected, ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Обработчики событий — подформы и объекты

        private void linkListInstr0_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ClearPayment(0);
                if (txtContractCode2.Text.Trim().Length == 0
                    || txtContractCode2.Text.Trim() == TextContractCodeBank)
                {
                    MessageBox.Show(this, MsgFillBuyerData, MsgTitleInputError,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    m_suppressMainTabSelecting = true;
                    tabControl.SelectedTab = tabPage1;
                    m_suppressMainTabSelecting = false;
                    linkContract2.Focus();
                    return;
                }

                base.IUbsChannel.ParamIn("ID_CONTRACT", m_mc["Покупатель"]);
                base.IUbsChannel.Run("TradeFillInstr");
                UbsParam instrOut = base.IUbsChannel.ParamsOutParam;

                ShowDialog(0, base.IUbsChannel.ParamsOutParam);
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void linkListInstr1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ClearPayment(1);
                if (txtContractCode1.Text.Trim().Length == 0
                    || txtContractCode1.Text.Trim() == TextContractCodeBank)
                {
                    MessageBox.Show(this, MsgFillSellerData, MsgTitleInputError,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    m_suppressMainTabSelecting = true;
                    tabControl.SelectedTab = tabPage1;
                    m_suppressMainTabSelecting = false;
                    linkContract1.Focus();
                    return;
                }

                base.IUbsChannel.ParamIn("ID_CONTRACT", m_mc["Продавец"]);
                base.IUbsChannel.Run("TradeFillInstr");

               ShowDialog(1, base.IUbsChannel.ParamsOutParam);
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void ShowDialog(int index, UbsParam paramsOutParam)
        {
            Load frmInstr
    Call frmInstr.StartForm(objError, objParamOut.Parameter("ArrInstr"))
    frmInstr.Show vbModal, Me

    If frmInstr.IsChoice Then 'нажали кнопку "Выбор"
        ArrOut = frmInstr.GetInstr 'считываем выбранные данные

        'заполняем контролы выбранными данными
        txtBIK(g).Text = ArrOut(0)
        txtName(g).Text = ArrOut(1)
        txtKS(g).Text = ArrOut(2)
        txtRS(g).Text = ArrOut(3)
        txtClient(g).Text = ArrOut(4)
        txtNote(g).Text = ArrOut(5)
        txtINN(g).Text = ArrOut(6)
        chkNotAkcept(g).Value = CInt(ArrOut(7))

        If txtBIK(g).Text = strOurBIK Then 'наш БИК
            txtRS(g).Enabled = False
            cmdAccount(g).Enabled = True
            cmdAccount(g).SetFocus
        Else
            txtRS(g).Enabled = True
            cmdAccount(g).Enabled = False
            txtRS(g).SetFocus
        End If
    End If
    Unload frmInstr
        }

        private void linkStorage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                txtStorageCode.Text = string.Empty;
                txtStorageName.Text = string.Empty;

                string filterPath = chkExternalStorage.Checked
                    ? @"UBS_FLT\PM\EXTERNAL_STORAGES_LIST.flt"
                    : @"UBS_FLT\PM\STORAGE.flt";

                object[] ids = this.Ubs_ActionRun(filterPath, this, null) as object[];
                if (ids != null && ids.Length > 0)
                {
                    int storageId = Convert.ToInt32(ids[0]);
                    m_mc["Инструкция по поставке"] = storageId;
                    FillControlStorage(storageId, chkExternalStorage.Checked);
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void linkAccountPayment0_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                object[] ids = this.Ubs_ActionRun(@"UBS_FLT\OD\account0.flt", this, null) as object[];
                if (ids != null && ids.Length > 0)
                    ucaRS0.Text = Convert.ToString(ids[0]);
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void linkAccountPayment1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                object[] ids = this.Ubs_ActionRun(ACCOUNT0_FLT, this, null) as object[];
                if (ids != null && ids.Length > 0)
                    ucaRS1.Text = Convert.ToString(ids[0]);
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void cmdAddObject_Click(object sender, EventArgs e)
        {
            try
            {
                int ctKey1 = 0;
                UbsPmTradeComboUtil.TryGetSelectedKey(cmbContractType1, out ctKey1);

                string filterPath = (ctKey1 == 0)
                    ? PM_FOR_OPERATION_SALE
                    : PM_FOR_OPERATION;

                object[] ids = this.Ubs_ActionRun(filterPath, this, null) as object[];
                if (ids != null && ids.Length > 0)
                {
                    int idObject = Convert.ToInt32(ids[0]);
                    m_blnAddObject = true;
                    FillDataObject(idObject);
                    UpdateObligInfoLabel();
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void cmdDelObject_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwObject.Items.Count == 0 || lvwObject.SelectedItems.Count == 0)
                    return;

                ListViewItem selected = lvwObject.SelectedItems[0];
                decimal ligMassa = 0, proba = 0;
                if (selected.SubItems.Count > 2)
                    decimal.TryParse(selected.SubItems[2].Text, out ligMassa);
                if (selected.SubItems.Count > 3)
                    decimal.TryParse(selected.SubItems[3].Text, out proba);

                lvwObject.Items.Remove(selected);

                if (lvwObject.Items.Count == 0)
                {
                    ucdMass.Text = "0";
                }
                else
                {
                    decimal adjustedMass = ligMassa;
                    if (m_blnConvert)
                    {
                        if (cmbUnit.Text == "грамм")
                            adjustedMass = (m_strCB == "A99")
                                ? Math.Round(ligMassa * proba / 100m, 0)
                                : Math.Round(ligMassa * proba / 100m, 1);
                        else
                            adjustedMass = Math.Round(ligMassa * proba / 100m, 2);
                    }
                    decimal currentMass;
                    if (!decimal.TryParse(ucdMass.Text, out currentMass))
                        currentMass = 0;
                    ucdMass.Text = (currentMass - adjustedMass).ToString();
                }

                GetMassGramm();
                GetSumOblig();
                GetSumPayment();
                UpdateObligInfoLabel();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void cmdAccounts_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwObligation.SelectedItems.Count == 0) return;

                object obligTag = lvwObligation.SelectedItems[0].Tag;
                if (obligTag == null) return;

                this.Ubs_ActionRun(PM_ACCOUNTS_BY_OBLIGATION, this, null);
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        private void tabControlOblig_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                TabPage target = e.TabPage;

                if (target == tabPageOblig2) // переход на вкладку "Объекты"
                {
                    // заполняем данные по обязательству
                    lblObligationInfo1.Text = $"Дата оплаты: {datePayment.DateValue} Дата поставки: {datePost.DateValue}";

                    UpdateObligInfoLabel();
                }
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }
    }
}
