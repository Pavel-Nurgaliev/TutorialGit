using System;
using System.Collections.Generic;
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
        private bool m_checkCommissionRestoreType2;
        private bool m_armedClearContract1FieldsOnTypeChange;
        private bool m_armedClearContract2FieldsOnTypeChange;
        private int m_idComission1;
        private int m_idComission2;

        private bool m_suppressCompositEvent;
        private bool m_needSendOblig;
        private int m_maxNumPart2;
        private readonly Dictionary<string, object> m_paramOblig = new Dictionary<string, object>();

        private const string PrefixObligObject = "Object";

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public UbsPmTradeFrm()
        {
            m_addCommand();

            InitializeComponent();

            base.UbsCtrlFieldsSupportCollection.Add("Доп. поля", ubsCtrlField);

            base.Ubs_CommandLock = true;
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

                // EDIT: при пустом ID_TRADE показываем сообщение и закрываем форму
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

        private void btnExit_Click(object sender, EventArgs e) { this.Close(); }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren()) { return; }

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
                    var paramOut = new UbsParam(base.IUbsChannel.ParamsOut);
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
                    ucdCostCurOpl.Enabled = false;
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
                    ucdCostCurOpl.Enabled = true;
                }
                else
                {
                    ucdCostCurOpl.Enabled = false;
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
                    var paramOut = new UbsParam(base.IUbsChannel.ParamsOut);
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

        #region Вспомогательные методы — инициализация

        private void InitDoc()
        {
            try
            {
                base.IUbsChannel.Run("InitForm");

                base.UbsInit();

                m_checkCommissionRestoreType2 = false;
                FillCombos();
                FillOurBIK();

                bool isEdit = string.Equals(m_command, CmdEdit, StringComparison.Ordinal);

                if (isEdit)
                {
                    m_armedClearContract1FieldsOnTypeChange = false;
                    m_armedClearContract2FieldsOnTypeChange = false;

                    base.IUbsChannel.ParamIn("ID_TRADE", m_idTrade);
                    base.IUbsChannel.Run("GetOneTrade");

                    base.IUbsChannel.ParamIn("ID_TRADE", m_idTrade);
                    base.IUbsChannel.Run("PMCheckOperationByTrade");

                    var opOut = new UbsParam(base.IUbsChannel.ParamsOut);
                    bool wasOper = opOut.Contains("Was_Operation") && Convert.ToBoolean(opOut.Value("Was_Operation"));

                    if (wasOper)
                        LockUiOnWasOperation();
                }
                else
                {
                    m_armedClearContract1FieldsOnTypeChange = false;
                    m_armedClearContract2FieldsOnTypeChange = false;

                    chkComposit.Visible = m_kindTrade != 0;

                    UbsPmTradeComboUtil.SetComboByKey(cmbTradeDirection, 1);
                    cmbTradeDirection.Enabled = false;

                    base.IUbsChannel.Run("FillBaseCurrency");
                    var baseCurOut = new UbsParam(base.IUbsChannel.ParamsOut);
                    if (baseCurOut.Contains("Идентификатор базовой валюты"))
                    {
                        int idBase = Convert.ToInt32(baseCurOut.Value("Идентификатор базовой валюты"));
                        UbsPmTradeComboUtil.SetComboByKey(cmbCurrencyPayment, idBase);
                        UbsPmTradeComboUtil.SetComboByKey(cmbObligationCurrency, idBase);
                    }
                }

                ApplyContractType1Change(false);
                ApplyContractType2Change(false);
                m_checkCommissionRestoreType2 = true;
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void FillCombos()
        {
            m_suppressContractTypeEvent = true;
            try
            {
            cmbTradeType.DataSource = null;
            cmbTradeDirection.DataSource = null;
            cmbUnit.DataSource = null;
            cmbContractType1.DataSource = null;
            cmbContractType2.DataSource = null;
            cmbCurrencyPost.DataSource = null;
            cmbCurrencyPayment.DataSource = null;
            cmbObligationCurrency.DataSource = null;
            cmbKindSupplyTrade.DataSource = null;
            cmbComission.DataSource = null;

            cmbTradeType.Items.Clear();
            cmbTradeDirection.Items.Clear();
            cmbUnit.Items.Clear();
            cmbContractType1.Items.Clear();
            cmbContractType2.Items.Clear();
            cmbCurrencyPost.Items.Clear();
            cmbCurrencyPayment.Items.Clear();
            cmbObligationCurrency.Items.Clear();
            cmbKindSupplyTrade.Items.Clear();
            cmbComission.Items.Clear();

            chkCash0.Visible = false;
            chkCash1.Visible = false;

            cmbTradeType.DataSource = new List<KeyValuePair<int, string>> { new KeyValuePair<int, string>(1, "тип1") };
            cmbTradeType.ValueMember = "Key";
            cmbTradeType.DisplayMember = "Value";

            cmbTradeDirection.DataSource = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "прямая"),
                new KeyValuePair<int, string>(2, "обратная")
            };
            cmbTradeDirection.ValueMember = "Key";
            cmbTradeDirection.DisplayMember = "Value";

            cmbUnit.DataSource = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "грамм"),
                new KeyValuePair<int, string>(2, "унция")
            };
            cmbUnit.ValueMember = "Key";
            cmbUnit.DisplayMember = "Value";

            cmbKindSupplyTrade.DataSource = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "обезличенная"),
                new KeyValuePair<int, string>(2, "физическая")
            };
            cmbKindSupplyTrade.ValueMember = "Key";
            cmbKindSupplyTrade.DisplayMember = "Value";

            base.IUbsChannel.ParamIn("ID_PATTERN", 1);
            base.IUbsChannel.Run("TradeCombo_FillPM");
            var paramOut = new UbsParam(base.IUbsChannel.ParamsOut);

            UbsPmTradeComboUtil.FillComboFrom2DArray(cmbContractType1, paramOut, "Типы договоров");
            UbsPmTradeComboUtil.FillComboFrom2DArray(cmbContractType2, paramOut, "Типы договоров");

            UbsPmTradeComboUtil.FillComboFrom2DArray(cmbCurrencyPost, paramOut, "Валюты поставки");

            UbsPmTradeComboUtil.FillComboFrom2DArray(cmbCurrencyPayment, paramOut, "Валюты оплаты");
            UbsPmTradeComboUtil.FillComboFrom2DArray(cmbObligationCurrency, paramOut, "Валюты оплаты");

            UbsPmTradeComboUtil.FillComboFrom2DArray(cmbComission, paramOut, "Список комиссий");
            }
            finally
            {
                m_suppressContractTypeEvent = false;
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
                    SetPaymentInstrTabsSellerOnly();
                else
                    SetPaymentInstrTabsBothBuyerSelected();

                bool isEdit = string.Equals(m_command, CmdEdit, StringComparison.Ordinal);
                if (key1 != 0)
                {
                    if (!isEdit)
                    {
                        txtContractCode1.Text = "";
                        txtClientName1.Text = "";
                    }
                    else
                    {
                        if (m_armedClearContract1FieldsOnTypeChange)
                        {
                            txtContractCode1.Text = "";
                            txtClientName1.Text = "";
                        }
                        else
                            m_armedClearContract1FieldsOnTypeChange = true;
                    }
                    btnContract1.Enabled = true;
                }
                else
                {
                    m_idClient1 = FillContractRowFromPm(0, txtContractCode1, txtClientName1);
                    btnContract1.Enabled = false;
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
                        txtContractCode2.Text = "";
                        txtClientName2.Text = "";
                    }
                    else
                    {
                        if (m_armedClearContract2FieldsOnTypeChange)
                        {
                            txtContractCode2.Text = "";
                            txtClientName2.Text = "";
                        }
                        else
                            m_armedClearContract2FieldsOnTypeChange = true;
                    }
                    btnContract2.Enabled = true;
                }
                else
                {
                    m_idClient2 = FillContractRowFromPm(0, txtContractCode2, txtClientName2);
                    btnContract2.Enabled = false;
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

            var po = new UbsParam(base.IUbsChannel.ParamsOut);

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

                    var po = new UbsParam(base.IUbsChannel.ParamsOut);

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
            try
            {
                cmbComission.SelectedIndex = -1;
            }
            catch (ArgumentOutOfRangeException)
            {
            }
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
            var paramOut = new UbsParam(base.IUbsChannel.ParamsOut);

            if (!paramOut.Contains("Наш БИК"))
                return;

            string ourBik = Convert.ToString(paramOut.Value("Наш БИК"));

            if (txtBIK0 != null) txtBIK0.Text = ourBik;
            if (txtBIK1 != null) txtBIK1.Text = ourBik;
        }

        private void LockUiOnWasOperation()
        {
            btnSave.Enabled = false;
            tabControl.Enabled = false;
            btnExit.Enabled = true;
        }

        #endregion

        #region Вспомогательные методы — обязательства

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
            if (arr.GetLength(0) < 1 || arr.GetLength(1) < 8) return;

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
                txtBIK0.Text = "";
                txtName0.Text = "";
                ucaKS0.Text = "0";
                ucaRS0.Text = "0";
                txtClient0.Text = "";
                txtNote0.Text = "";
                txtINN0.Text = "";
                chkNotAkcept0.Checked = false;
            }
            else
            {
                txtBIK1.Text = "";
                txtName1.Text = "";
                ucaKS1.Text = "0";
                ucaRS1.Text = "0";
                txtClient1.Text = "";
                txtNote1.Text = "";
                txtINN1.Text = "";
                chkNotAkcept1.Checked = false;
            }
        }

        #endregion
    }
}
