using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// Шаблон класса формы
    /// </summary>
    public partial class UbsBgContractFrm : UbsFormBase
    {
        #region Блок констант

        private const int BasicCurrency = 810;

        private const string MsgContractNotSelected = "Не выбран договор!";
        private const string MsgFrameContractNotSelected = "Не выбран рамочный договор!";
        private const string MsgRateTypesParameterMissing = "Параметр <Список типов процентных ставок> отсутствует.";
        private const string MsgWarning = "Предупреждение";
        private const string MsgNoRightsToCreateContract = "Нет прав на создание договора!";
        private const string MsgAccessError = "Ошибка доступа";
        private const string MsgRiskSettingNotFilled = "Установка <Риск (величина резерва)> не заполнена";
        private const string MsgRiskAssessmentTypesEmpty = "Список типов оценок риска пуст";
        private const string MsgCoverTypesEmpty = "Список типов покрытия пуст";
        private const string MsgCurrenciesEmpty = "Список валют пуст";
        private const string MsgInitializationError = "Ошибка инициализации";
        private const string MsgStatesEmpty = "Список состояний пуст";
        private const string MsgDivisionsEmpty = "Список отделений пуст";
        private const string MsgContractGuarantee = "Договор гарантии";
        private const string MsgNoEditAccess = "Доступ на редактирование отсутствует! Форма запущена в режиме просмотра";
        private const string MsgPayOrderParameterEmpty = "Параметр 'Порядок уплаты вознаграждения (список)' пуст!";
        private const string MsgPayTypeParameterEmpty = "Параметр 'Тип вознаграждения (список)' пуст!";

        private const string AddCommand = "ADD";
        private const string EditCommand = "EDIT";
        private const string CopyCommand = "COPY";
        private const string PrepareCommand = "PREPARE";

        private const string AddByFrameContrantCommand = "ADD_BY_FRAME_CONTRACT";
        private const string AddByFramePrepareCommand = "ADD_BY_FRAME_PREPARE";

        private const string UbsCounterGuarantTypeCommand = "UBS_COUNTER_GUARANT";

        private const string ActionUbsBgFrameContractList = "UBS_BG_FRAME_CONTRACT_LIST";
        private const string ActionUbsBgListModel = "UBS_BG_LIST_MODEL";
        private const string ActionUbsBgListAgent = "UBS_BG_LIST_AGENT";
        private const string ActionUbsCommonListClient = "UBS_COMMON_LIST_CLIENT";
        private const string ActionUbsBgListContract = "UBS_BG_LIST_CONTRACT";
        #endregion
        #region Блок объявления переменных

        private string m_command = "";

        private object[] m_itemArray;

        private int m_idContract;
        private int m_idContractCopy;
        private int m_idFrameContract;

        private bool m_isSecure;
        private DateTime m_minDate = DateTime.MinValue;
        private DateTime m_issueEndDate = DateTime.MinValue;
        private int m_divisionFrameContract;
        private DateTime m_dateToday = DateTime.MinValue;

        private string m_strCaptionForm = "Изменить договор гарантии";
        private object[] m_kindAddflSense;
        private int m_idCurrency;
        private int m_idCoverType;
        private int m_idGarant;
        private int m_idState;
        private int m_idModel;
        private int m_idAgent;
        private int m_idPrincipal;
        private int m_idBeneficiar;
        private int m_idPrevContract;
        private object[,] m_arrRates;
        private bool m_isInitCurrencyBonus;
        private object m_arrPeriodPay;
        private bool m_isFixSum;
        private object[,] m_arrDetailsBeneficiar;
        private int m_idDivision;
        private int m_idOI;
        private int m_idWarrant;
        private bool cmbCurrencyGarantEnabled;
        private DateTime m_dateBeginFrameContract;
        private DateTime m_dateEndFrameContract;
        private object[,] m_termsFrameContract;
        private decimal m_limitSaldoFrameContract;
        private object[,] m_arrGuarant;
        private int m_idContractCover;
        private DateTime m_dateBegin;
        private string m_modelType;
        private string m_orderPayFeeGuarant;
        private string m_typePayFeeGuarant;
        private object[,] m_arrIntervalGuarant;
        private int m_setRekvBen;

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public UbsBgContractFrm()
        {
            m_addCommand(); //зарегистрировать обработчики команд интерфейса IUbs

            InitializeComponent();

            m_minDate = m_issueEndDate = new DateTime(2222, 1, 1);

            base.UbsCtrlFieldsSupportCollection.Add("Доп. поля", ubsCtrlFields);

            base.Ubs_CommandLock = true;
        }


        #region Обработчики событий кнопок (с примерами)

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


        #region Обработчики команд IUbs интерфейса (с примерами)

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
        /// <param name="param_in">Входной параметер</param>
        /// <param name="param_out">Выходной параметер</param>
        /// <returns></returns>
        private object CommandLine(object param_in, ref object param_out)
        {
            m_command = (string)param_in;

            return null;
        }
        /// <summary>
        /// Процедура обработки команды ListKey
        /// </summary>
        /// <param name="param_in">Входной параметер</param>
        /// <param name="param_out">Выходной параметер</param>
        /// <returns></returns>
        public object ListKey(object param_in, ref object param_out)
        {
            m_itemArray = param_in as object[];

            m_idContract = 0;
            m_idContractCopy = 0;
            m_isSecure = true;

            if (m_command == EditCommand || m_command == CopyCommand)
            {
                if (m_itemArray is null)
                {
                    MessageBox.Show(MsgContractNotSelected, m_strCaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (CheckParamForClose("InitParamForm"))
                    {
                        btnExit_Click(this, EventArgs.Empty);
                        return false;
                    }
                }

                int id = Convert.ToInt32(m_itemArray[0]);
                if (m_command == EditCommand)
                    m_idContract = id;
                else
                    m_idContractCopy = id;

                if (m_idContract == 0 && m_idContractCopy == 0)
                {
                    MessageBox.Show(MsgContractNotSelected, m_strCaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (CheckParamForClose("InitParamForm"))
                    {
                        btnExit_Click(this, EventArgs.Empty);
                        return false;
                    }
                }
            }

            else if (m_command == AddByFrameContrantCommand)
            {
                m_idFrameContract = m_itemArray is null ? Convert.ToInt32(m_itemArray[0]) : 0;

                if (m_idFrameContract == 0)
                {
                    MessageBox.Show(MsgFrameContractNotSelected, m_strCaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (CheckParamForClose("InitParamForm"))
                    {
                        btnExit_Click(this, EventArgs.Empty);
                        return false;
                    }
                }

                m_command = AddCommand;
            }
            else if (m_command == AddByFramePrepareCommand)
            {
                m_idFrameContract = m_itemArray is null ? Convert.ToInt32(m_itemArray[0]) : 0;

                if (m_idFrameContract == 0)
                {
                    MessageBox.Show(MsgFrameContractNotSelected, m_strCaptionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (CheckParamForClose("InitParamForm"))
                    {
                        btnExit_Click(this, EventArgs.Empty);
                        return false;
                    }
                }

                m_command = PrepareCommand;
            }

            InitDoc();

            return true;
        }

        private bool CheckParamForClose(string nameParam)
        {
            if (string.IsNullOrEmpty(nameParam))
                return false;

            if (nameParam == "LoadFromMenu" ||
                nameParam == "InitParamForm" ||
                nameParam == "InitParamGrid")
            {
                return true;
            }

            return false;
        }

        private void InitDoc()
        {
            try
            {
                base.IUbsChannel.LoadResource = "VBS:UBS_VBD\\BG\\BG_CONTRACT.vbs";

                m_dateToday = GetCurrentDate();

                base.UbsInit();

                base.UbsChannel_ParamIn("StrCommand", m_command);

                base.UbsChannel_Run("BG_Contract_Init");

                ubsCtrlFields.Refresh();

                int setRekvBen = Convert.ToInt32(UbsChannel_ParamOut("setRekvBen"));

                bool limitExcessCheckOn = Convert.ToBoolean(UbsChannel_ParamOut("Контроль лимитов"));
                int reflectComIssue = Convert.ToInt32(UbsChannel_ParamOut("Режим учета вознаграждений"));

                object[,] arrExecutEx = UbsChannel_ParamOut("ОИ вып.оп.") as object[,];

                // "Список типов процентных ставок"
                m_arrRates = null;

                if (UbsChannel_ExistParamOut("Список типов процентных ставок"))
                    m_arrRates = UbsChannel_ParamOut("Список типов процентных ставок") as object[,];
                else
                {
                    MessageBox.Show(MsgRateTypesParameterMissing,
                        MsgWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    m_arrRates = null;
                }

                m_isInitCurrencyBonus = false;

                dateOpenGarant.DateValue = m_dateToday;

                InitCmsAccounts();

                InitComboBoxes();

                // === Заголовки lvwGuarant ===
                dateNextPayFee.Enabled = (m_idState >= 2);

                if (m_command == AddCommand || m_command.ToUpperInvariant() == PrepareCommand)
                {
                    btnReRead.Enabled = false;
                    SetTabsEnabled(false, 2, 3, 4);
                }
                else if (m_command == CopyCommand)
                {
                    btnReRead.Enabled = false;
                    SetTabsEnabled(true, 2, 3, 4);
                }
                else
                {
                    btnReRead.Enabled = true;
                    SetTabsEnabled(true, 2, 3, 4);
                }

                if (m_command.ToUpperInvariant() == CopyCommand)
                {
                    UbsChannel_ParamIn("Id", m_idContractCopy);
                    UbsChannel_Run("BG_Contract_Copy");

                    txtModel.Text = Convert.ToString(UbsChannel_ParamOut("Типовой договор"));

                    txtBeneficiar.Text = Convert.ToString(UbsChannel_ParamOut("Бенефициар"));
                    txtGarant.Text = Convert.ToString(UbsChannel_ParamOut("Гарант"));
                    txtFrameContract.Text = Convert.ToString(UbsChannel_ParamOut("Наименование рамочного договора"));

                    m_idModel = Convert.ToInt32(UbsChannel_ParamOut("Идентификатор типового договора"));
                    m_idPrincipal = Convert.ToInt32(UbsChannel_ParamOut("Идентификатор клиента-принципала"));
                    m_idBeneficiar = Convert.ToInt32(UbsChannel_ParamOut("Идентификатор клиента-бенефициара"));

                    m_idState = 2;

                    SetComboById(cmbNumberDiv,
                        Convert.ToInt32(UbsChannel_ParamOut("Номер отделения")));
                    SetComboById(cmbExecutor,
                        Convert.ToInt32(UbsChannel_ParamOut("Идентификатор ОИ")));
                    SetComboById(cmbWarrant,
                        Convert.ToInt32(UbsChannel_ParamOut("Идентификатор доверенности")));
                    SetComboById(cmbState, m_idState);
                    SetComboById(cmbCurrencyGarant,
                        Convert.ToInt32(UbsChannel_ParamOut("Идентификатор валюты")));

                    txtNumberGarant.Text = Convert.ToString(UbsChannel_ParamOut("Номер договора"));
                    dateOpenGarant.Text = Convert.ToString(UbsChannel_ParamOut("Дата заключения"));
                    dateCloseGarant.Text = Convert.ToString(UbsChannel_ParamOut("Дата закрытия"));
                    dateBeginGarant.Text = Convert.ToString(UbsChannel_ParamOut("Дата начала действия"));
                    dateEndGarant.Text = Convert.ToString(UbsChannel_ParamOut("Дата окончания действия"));
                }

                ReReadDoc();
            }
            catch (Exception ex)
            {
                base.Ubs_ShowError(ex);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void InitCmsAccounts()
        {
            object[,] arrListRprByAcc = UbsChannel_ParamOut("Перечень отчетов по счету") as object[,];

            if (arrListRprByAcc != null)
            {
                int rows = arrListRprByAcc.GetLength(0);
                for (int r = 0; r < rows; r++)
                {
                    string caption = Convert.ToString(arrListRprByAcc[r, 1]);

                    cmsAccounts.Items.Add(caption);
                }
            }
        }

        private void InitComboBoxes()
        {// === Инициализация комбобокса ОИ ===
            object[,] arrExecut = UbsChannel_ParamOut("ОИ") as object[,];

            if (arrExecut != null)
            {
                var kvpList = MakeKvpList(arrExecut);

                InitComboBox(cmbExecutor, kvpList);
            }
            else
            {
                if (m_command == AddCommand || m_command.ToUpperInvariant() == PrepareCommand)
                {
                    MessageBox.Show(MsgNoRightsToCreateContract, MsgAccessError, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    btnExit_Click(this, EventArgs.Empty);

                    return;
                }
            }

            // === Инициализация комбобокса (УЛБ) ===
            object[,] arrWarrant = UbsChannel_ParamOut("УЛБ") as object[,];

            if (arrWarrant != null)
            {
                var kvpList = MakeKvpList(arrWarrant);

                InitComboBox(cmbWarrant, kvpList);
            }

            object[] arrKindAddflSense = UbsChannel_ParamOut("Виды гарантии") as object[];

            if (arrKindAddflSense != null)
            {
                object[] extended = new object[arrKindAddflSense.Length + 1];

                extended[extended.Length - 1] = string.Empty;

                for (int i = 0; i < arrKindAddflSense.Length; i++)
                {
                    extended[i + 1] = arrKindAddflSense[i];
                }

                arrKindAddflSense = extended;
            }
            else
            {
                arrKindAddflSense = new object[] { string.Empty };
            }

            m_kindAddflSense = arrKindAddflSense;

            ResetKindComboBox();

            // === Категории качества ===
            object[,] arrQualityCategory = UbsChannel_ParamOut("Категории качества") as object[,];

            if (arrQualityCategory != null)
            {
                var kvpList = MakeKvpList(arrQualityCategory, 0, 0);

                InitComboBox(cmbQualityCategory, kvpList);
            }
            else
            {
                MessageBox.Show(MsgRiskSettingNotFilled,
                    MsgWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // === Портфели ===
            cmbPortfolio.Items.Clear();

            object[,] arrPortfolio = UbsChannel_ParamOut("Портфели") as object[,];

            if (arrPortfolio != null)
            {
                var kvpList = MakeKvpList(arrPortfolio);

                kvpList.Insert(0, new KeyValuePair<int, string>(0, "Портфель не определен"));

                InitComboBox(cmbPortfolio, kvpList);
            }

            cmbPortfolio.SelectedIndex = 0;

            // === Тип оценки риска ===
            object[,] arrTypeValidationRisk = UbsChannel_ParamOut("Тип оценки риска") as object[,];

            if (arrTypeValidationRisk != null)
            {
                var kvpList = MakeKvpList(arrTypeValidationRisk);

                InitComboBox(cmbTypeValidationRisk, kvpList);
            }
            else
            {
                MessageBox.Show(MsgRiskAssessmentTypesEmpty,
                    MsgWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // === Типы покрытия ===
            object[,] arrCoverTypes = UbsChannel_ParamOut("Типы покрытия") as object[,];

            if (arrCoverTypes != null)
            {
                var kvpList = MakeKvpList(arrCoverTypes);

                InitComboBox(cmbTypeCover, kvpList);
            }
            else
            {
                MessageBox.Show(MsgCoverTypesEmpty,
                    MsgWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // === Валюты ===
            object[,] arrCurrency = UbsChannel_ParamOut("Валюты") as object[,];
            if (arrCurrency != null)
            {
                int idxCurrency = 0;
                int idCurrency = m_idCurrency > 0 ? m_idCurrency : 810;

                cmbCurrencyGarant.Items.Clear();
                cmbCurrencyCover.Items.Clear();
                cmbCurrencyPayFee.Items.Clear();
                cmbCurrencyRewardGuarant.Items.Clear();

                int rows = arrCurrency.GetLength(0);
                for (int r = 0; r < rows; r++)
                {
                    int id = Convert.ToInt32(arrCurrency[r, 0]);
                    string text = Convert.ToString(arrCurrency[r, 3]);

                    cmbCurrencyGarant.Items.Add(new ComboItem(id, text));
                    cmbCurrencyCover.Items.Add(new ComboItem(id, text));
                    cmbCurrencyPayFee.Items.Add(new ComboItem(id, text));
                    cmbCurrencyRewardGuarant.Items.Add(new ComboItem(id, text));

                    if (id == idCurrency) idxCurrency = r;
                }

                if (cmbCurrencyGarant.Items.Count > 0)
                {
                    cmbCurrencyGarant.SelectedIndex = idxCurrency;
                    cmbCurrencyCover.SelectedIndex = idxCurrency;
                    cmbCurrencyPayFee.SelectedIndex = idxCurrency;
                    cmbCurrencyRewardGuarant.SelectedIndex = idxCurrency;
                }
            }
            else
            {
                if (CheckParamForClose("InitDoc"))
                {
                    MessageBox.Show(MsgCurrenciesEmpty, MsgInitializationError,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnExit_Click(this, EventArgs.Empty);
                    return;
                }

                MessageBox.Show(MsgCurrenciesEmpty, MsgWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // === Состояния ===
            object[,] arrState = UbsChannel_ParamOut("Состояния") as object[,];

            if (arrState != null)
            {
                int idxState = 0;
                m_idState = 4;

                cmbState.Items.Clear();

                var kvpList = MakeKvpList(arrState);

                InitComboBox(cmbState, kvpList);

                for (int i = 0; i < arrState.GetLength(0); i++)
                {
                    if (Convert.ToInt32(arrState[i, 0]) == m_idState)
                        idxState = i;
                }

                if (cmbState.Items.Count > 0)
                    cmbState.SelectedIndex = idxState;

                cmbState.Enabled = false;
            }
            else
            {
                MessageBox.Show(MsgStatesEmpty, MsgWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // === Отделения ===
            object[,] arrDivs = UbsChannel_ParamOut("Отделения") as object[,];

            if (arrDivs != null)
            {
                int idxDiv = 0;
                int gudiv = Convert.ToInt32(UbsChannel_ParamOut("GUDiv"));

                cmbNumberDiv.Items.Clear();

                var kvpList = MakeKvpList(arrDivs);

                InitComboBox(cmbNumberDiv, kvpList);

                for (int i = 0; i < kvpList.Count; i++)
                {
                    if (kvpList[i].Key == gudiv)
                    {
                        idxDiv = i;
                    }
                }

                if (cmbNumberDiv.Items.Count > 0)
                    cmbNumberDiv.SelectedIndex = idxDiv;

                if (m_command.ToUpperInvariant() != AddCommand)
                {
                    if (m_command == EditCommand && m_idState == 2)
                    {
                        cmbNumberDiv.Enabled = true;

                        if (cmbTypePayFee.SelectedText == "Процент суммы гарантии")
                        {
                            cmbCurrencyPayFee.SelectedIndex = cmbCurrencyGarant.SelectedIndex;

                            cmbCurrencyPayFee.Enabled = false;
                            cmbCurrencyRewardGuarant.Enabled = false;
                        }
                        else
                        {
                            cmbCurrencyPayFee.Enabled = true;
                            cmbCurrencyRewardGuarant.Enabled = true;
                        }

                        if (cmbTypePayFee.SelectedText == "Процент суммы гарантии")
                        {
                            cmbCurrencyPayFee.SelectedIndex = cmbCurrencyGarant.SelectedIndex;

                            cmbCurrencyRewardGuarant.Enabled = false;
                        }
                        else
                        {
                            cmbCurrencyRewardGuarant.Enabled = true;
                        }
                    }
                    else
                    {
                        cmbNumberDiv.Enabled = false;
                    }
                }

                if (m_command.ToUpperInvariant() == PrepareCommand)
                    cmbNumberDiv.Enabled = true;
            }
            else
            {
                MessageBox.Show(MsgDivisionsEmpty, MsgWarning,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private static void SetComboById(ComboBox combo, int id)
        {
            for (int i = 0; i < combo.Items.Count; i++)
            {
                ComboItem it = combo.Items[i] as ComboItem;
                if (it != null && it.Id == id)
                {
                    combo.SelectedIndex = i;
                    return;
                }
            }
        }
        private void SetTabsEnabled(bool enabled, params int[] tabIndexes)
        {
            for (int i = 0; i < tabIndexes.Length; i++)
            {
                int idx = tabIndexes[i];
                if (idx >= 0 && idx < tabControl.TabPages.Count)
                {
                    foreach (Control c in tabControl.TabPages[idx].Controls)
                        c.Enabled = enabled;
                }
            }
        }
        private DateTime GetCurrentDate()
        {
            base.IUbsChannel.ParamIn("NameSetting", "Server");
            base.IUbsChannel.Run("GetCommonDate");

            return Convert.ToDateTime(base.IUbsChannel.ParamOut("DataSetting"));
        }
        private void ResetKindComboBox()
        {
            object selectedItem = null;
            if (cmbKindGarant.SelectedItem != null)
            {
                selectedItem =
                    (KeyValuePair<int, string>)cmbKindGarant.SelectedItem;
            }

            cmbKindGarant.Items.Clear();

            var tmpKindGarantArr = GetArrayForCombo(m_kindAddflSense);
            var kvpList = MakeKvpList(tmpKindGarantArr);

            InitComboBox(cmbKindGarant, kvpList);

            cmbKindGarant.SelectedIndex = cmbKindGarant.Items.Count - 1;
            if (cmbKindGarant.SelectedItem != null)
            {
                SetComboItem(cmbKindGarant, (KeyValuePair<int, string>)cmbKindGarant.SelectedItem);
            }
        }

        private object[,] GetArrayForCombo(object[] kindAddflSense)
        {
            var result = new object[kindAddflSense.Length, 2];

            for (int i = 0; i < kindAddflSense.Length; i++)
            {
                result[i, 0] = i;
                result[i, 1] = kindAddflSense[i];
            }

            return result;
        }
        private void SetComboItem(ComboBox comboBox, KeyValuePair<int, string> selectedItem)
        {
            foreach (KeyValuePair<int, string> item in comboBox.Items)
            {
                if (item.Key == selectedItem.Key)
                {
                    comboBox.SelectedItem = item;
                }
            }
        }

        private void ReReadDoc()
        {
            if (m_command.ToUpperInvariant() != CopyCommand)
                dateOpenGarant.DateValue = m_dateToday;

            int idCurrency = (m_idCurrency > 0) ? m_idCurrency : BasicCurrency;

            cmbCurrencyGarant.SelectedValue = idCurrency;
            cmbCurrencyCover.SelectedValue = idCurrency;
            cmbCurrencyPayFee.SelectedValue = idCurrency;

            btnAddRate.Enabled = false;
            btnDelRate.Enabled = false;
            btnEditRate.Enabled = false;

            if (m_command.ToUpperInvariant() == EditCommand)
            {
                SetTabsEnabled(true, 4);

                GuarCmdState(true);

                base.UbsChannel_ParamIn("Id", m_idContract);
                base.UbsChannel_Run("BG_Contract_Read");

                var paramOut = new UbsParam(base.UbsChannel_ParamsOut);
                lblUID.Visible = true;
                lblUID.Text = $"{lblUID.Text} {Convert.ToString(paramOut["UID"])}";

                if (base.UbsChannel_ExistParamOut("strError"))
                {
                    MessageBox.Show(Convert.ToString(paramOut["strError"]),
                        MsgContractGuarantee, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnExit_Click(this, EventArgs.Empty);
                    return;
                }

                if (Convert.ToInt32(paramOut["Доступ"]) == 0)
                {
                    m_isSecure = false;

                    MessageBox.Show(MsgNoEditAccess,
                        MsgContractGuarantee, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    btnSave.Enabled = false;
                }

                m_arrPeriodPay = paramOut["Срок погашения оплаченной суммы"];

                m_isFixSum = (Convert.ToString(paramOut["Тип вознаграждения"]) == "Фиксированная сумма");

                if (Convert.ToInt32(paramOut["Доступ"]) == 0)
                {
                    if (base.UbsChannel_ExistParamOut("ОИ"))
                    {
                        var one = new List<KeyValuePair<int, string>>();
                        one.Add(new KeyValuePair<int, string>(
                            Convert.ToInt32(paramOut["ОИ"]),
                            Convert.ToString(paramOut["Наименование ОИ"])));

                        InitComboBox(cmbExecutor, one);

                        cmbExecutor.SelectedValue = Convert.ToInt32(paramOut["ОИ"]);
                    }
                }

                // Тексты
                txtModel.Text = Convert.ToString(paramOut["Типовой договор"]);
                txtAgent.Text = Convert.ToString(paramOut["Агент"]);

                if (true)
                {
                    txtNumAgent.Text = Convert.ToString(paramOut["Номер договора агента"]);
                }
                dateAgent.Text = Convert.ToString(paramOut["Дата договора агента"]);

                txtBeneficiar.Text = Convert.ToString(paramOut["Бенефициар"]);
                txtGarant.Text = Convert.ToString(paramOut["Гарант"]);
                txtFrameContract.Text = Convert.ToString(paramOut["Наименование рамочного договора"]);

                cmbKindGarant.Text = Convert.ToString(paramOut["Вид гарантии"]);

                m_idModel = Convert.ToInt32(paramOut["Идентификатор типового договора"]);
                m_idAgent = Convert.ToInt32(paramOut["Идентификатор договора агента"]);
                m_idPrincipal = Convert.ToInt32(paramOut["Идентификатор клиента-принципала"]);
                m_idBeneficiar = Convert.ToInt32(paramOut["Идентификатор клиента-бенефициара"]);

                if (m_idBeneficiar == 0)
                    m_arrDetailsBeneficiar = paramOut["Реквизиты бенефициара"] as object[,];

                m_idDivision = Convert.ToInt32(paramOut["Номер отделения"]);
                m_idOI = Convert.ToInt32(paramOut["Идентификатор ОИ"]);
                m_idWarrant = Convert.ToInt32(paramOut["Идентификатор доверенности"]);

                m_idState = Convert.ToInt32(paramOut["Состояние"]);

                SetEnableCombos();

                m_idCurrency = Convert.ToInt32(paramOut["Идентификатор валюты"]);
                m_idCoverType = Convert.ToInt32(paramOut["Тип покрытия"]);

                m_idGarant = Convert.ToInt32(paramOut["Идентификатор клиента-гаранта"]);
                m_idFrameContract = Convert.ToInt32(paramOut["Идентификатор рамочного договора"]);

                // ===== Данные рамочного договора =====
                if (m_idFrameContract > 0)
                {
                    m_dateBeginFrameContract = Convert.ToDateTime(paramOut["Дата начала действия рамочного договора"]);
                    m_dateEndFrameContract = Convert.ToDateTime(paramOut["Дата окончания действия рамочного договора"]);
                    m_termsFrameContract = paramOut["Срок гарантии рамочного договора"] as object[,];
                    m_limitSaldoFrameContract = Convert.ToDecimal(paramOut["Остаток лимита рамочного договора"]);
                    m_issueEndDate = Convert.ToDateTime(paramOut["Дата окончания выдачи"]);
                    m_divisionFrameContract = Convert.ToInt32(paramOut["Отделение рамочного договора"]);

                    SetInfoByFrameContract();
                }
                else
                {
                    txtPrincipal.Text = Convert.ToString(paramOut["Принципал"]);
                }

                // ===== Агент: Reward/Cost блок =====
                if (m_idAgent > 0)
                {
                    dateReward.Enabled = true;
                    dateReward.Text = Convert.ToString(paramOut["Дата вознаграждения"]);

                    costAmount.Enabled = true;

                    costAmount.DecimalValue = Convert.ToDecimal(paramOut["Сумма затрат"]);

                    base.IUbsChannel.ParamIn("IdAgent", m_idAgent);
                    base.IUbsChannel.Run("AgentRewardAvaliable");

                    bool avaliable = Convert.ToBoolean(paramOut["Avaliable"]);
                    dateReward.Enabled = avaliable;
                    costAmount.Enabled = avaliable;

                    var pOut = base.IUbsChannel.ParamsOutParam;
                    dateAdjustment.DateValue = Convert.ToDateTime(pOut["Дата по корректировке"]);
                    paidAmount.DecimalValue = Convert.ToDecimal(pOut["Уплаченная сумма"]);
                    transAmount.DecimalValue = Convert.ToDecimal(pOut["Перечисленная сумма"]);
                }

                if (base.UbsChannel_ExistParamOut("Обеспечения"))
                {
                    m_arrGuarant = paramOut["Обеспечения"] as object[,];

                    FillLvwGuarant();
                }

                if (m_idModel == 0)
                    SetTabsEnabled(false, 2, 3);
                else
                    SetTabsEnabled(true, 2, 3);

                m_modelType = Convert.ToString(paramOut["Шаблон"]);

                if (m_modelType == "UBS_GUARANT")
                {
                    if (m_idState == 4)
                    {
                        linkGarant.Enabled = true;

                        if (cmbTypePayFee.Text == "Процент суммы гарантии")
                        {
                            cmbCurrencyPayFee.SelectedValue = Convert.ToInt32(cmbCurrencyGarant.SelectedValue);
                            cmbCurrencyPayFee.Enabled = false;
                        }

                        if (cmbTypePayFeeBonus.Text == "Процент суммы гарантии")
                        {
                            cmbCurrencyRewardGuarant.SelectedValue = Convert.ToInt32(cmbCurrencyGarant.SelectedValue);
                            cmbCurrencyRewardGuarant.Enabled = false;
                        }
                    }
                    else
                    {
                        linkGarant.Enabled = false;
                    }

                    m_idGarant = 0;
                    txtGarant.Text = "";
                }
                else
                {
                    linkGarant.Enabled = true;
                }

                // ===================== Вознаграждение (гарант) =====================
                m_orderPayFeeGuarant = Convert.ToString(paramOut["Порядок уплаты вознаграждения (гарант)"]);

                if (m_orderPayFeeGuarant != null && m_orderPayFeeGuarant != string.Empty)
                {
                    gbPayFeeGuarant.Visible = true;

                    m_typePayFeeGuarant = Convert.ToString(paramOut["Тип вознаграждения (гарант)"]);

                    if (m_orderPayFeeGuarant == "Периодически")
                    {
                        dateNextPayFeeGuarant.DateValue = Convert.ToDateTime(paramOut["Дата след. уплаты вознаграждения (гарант)"]);

                        m_arrIntervalGuarant = paramOut["Вознаграждение (гарант)"] as object[,];
                    }
                }
                else
                {
                    gbPayFeeGuarant.Visible = false;
                }

                // ===================== Портфель =====================
                if (paramOut.Contains("Идентификатор портфеля"))
                {
                    cmbPortfolio.SelectedValue = Convert.ToInt32(paramOut["Идентификатор портфеля"]);
                }

                // ===================== Тип оценки риска =====================
                if (paramOut.Contains("Тип оценки риска"))
                {
                    cmbTypeValidationRisk.SelectedValue = Convert.ToInt32(paramOut["Тип оценки риска"]);
                }

                // ===================== Группа риска =====================
                if (paramOut.Contains("Группа риска"))
                {
                    cmbQualityCategory.SelectedValue = Convert.ToInt32(paramOut["Группа риска"]);
                }
                // ===================== Группа риска =====================
                if (paramOut.Contains("Ставка резервирования"))
                {
                    ucdRateReservation.DecimalValue = Convert.ToDecimal(paramOut["Ставка резервирования"]);
                }

                // ===== Выбор комбо по ID через SelectedValue (аналог SetComboText) =====
                cmbNumberDiv.SelectedValue = m_idDivision;
                cmbExecutor.SelectedValue = m_idOI;
                cmbWarrant.SelectedValue = m_idWarrant;
                cmbTypeCover.SelectedValue = m_idCoverType;
                cmbState.SelectedValue = m_idState;
                cmbCurrencyGarant.SelectedValue = m_idCurrency;

                // Поля договора
                txtNumberGarant.Text = Convert.ToString(paramOut["Номер договора"]);
                dateOpenGarant.DateValue = Convert.ToDateTime(paramOut["Дата заключения"]);
                dateCloseGarant.DateValue = Convert.ToDateTime(paramOut["Дата закрытия"]);
                dateBeginGarant.DateValue = Convert.ToDateTime(paramOut["Дата начала действия"]);
                datePrincipal.DateValue = Convert.ToDateTime(paramOut["Дата возникн. обязательства Принципала"]);

                m_dateBegin = Convert.ToDateTime(paramOut["Дата начала действия"]);
                dateEndGarant.DateValue = Convert.ToDateTime(paramOut["Дата окончания действия"]);

                if (m_idState == 4)
                    linkPreviousContract.Enabled = true;
                else
                    linkPreviousContract.Enabled = false;

                txtPreviousContract.Enabled = false;
                txtPreviousContract.Text = Convert.ToString(paramOut["InfoPrevContract"]);

                // ===== Доп.поля по валютам вознаграждения =====
                int idBonusValuta = Convert.ToInt32(paramOut["Идентификатор валюты вознаграждения"]);
                cmbCurrencyPayFee.SelectedValue = idBonusValuta;

                int idBonusValutaBonus = Convert.ToInt32(paramOut["Идентификатор валюты вознаграждения за выдачу"]);

                cmbCurrencyRewardGuarant.SelectedValue = idBonusValutaBonus;

                m_isInitCurrencyBonus = true;

                ucdSumGarant.DecimalValue = Convert.ToDecimal(paramOut["Сумма гарантии"]);

                m_idCoverType = Convert.ToInt32(paramOut["Тип покрытия"]);

                cmbTypeCover.SelectedValue = m_idCoverType;

                ucdSumCover.DecimalValue = Convert.ToDecimal(paramOut["Сумма покрытия"]);

                int idCoverValuta = Convert.ToInt32(paramOut["Идентификатор валюты покрытия"]);

                cmbCurrencyCover.SelectedValue = idCoverValuta;

                m_idContractCover = Convert.ToInt32(paramOut["Идентификатор договора покрытия"]);
                if (base.UbsChannel_ExistParamOut("Договор покрытия"))
                    txtContractCover.Text = Convert.ToString(paramOut["Договор покрытия"]);

                // ===== Списки порядка/типа вознаграждения =====
                object[] arrPayOrder = paramOut["Порядок уплаты вознаграждения (список)"] as object[];
                if (arrPayOrder != null)
                {
                    InitComboBox(cmbOrderPayFee, MakeKvpList(arrPayOrder));
                    cmbOrderPayFee.Text = Convert.ToString(paramOut["Порядок уплаты вознаграждения"]);

                    InitComboBox(cmbOrderPayFeeBonus, MakeKvpList(arrPayOrder));
                    cmbOrderPayFeeBonus.Text = Convert.ToString(paramOut["Порядок уплаты вознаграждения за выдачу"]);
                }
                else
                {
                    MessageBox.Show(MsgPayOrderParameterEmpty,
                        MsgContractGuarantee, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (CheckParamForClose("InitDoc"))
                    {
                        btnExit_Click(this, EventArgs.Empty);
                        return;
                    }
                }

                object[] arrTypeBonus = paramOut["Тип вознаграждения (список)"] as object[];
                if (arrTypeBonus != null)
                {
                    InitComboBox(cmbTypePayFee, MakeKvpList(arrTypeBonus));
                    cmbTypePayFee.Text = Convert.ToString(paramOut["Тип вознаграждения"]);

                    InitComboBox(cmbTypePayFeeBonus, MakeKvpList(arrTypeBonus));
                    cmbTypePayFeeBonus.Text = Convert.ToString(paramOut["Тип вознаграждения за выдачу"]);

                    if (cmbTypePayFee.Text == "Процент суммы гарантии")
                        cmbCurrencyPayFee.Enabled = false;
                    if (cmbTypePayFeeBonus.Text == "Процент суммы гарантии")
                        cmbCurrencyRewardGuarant.Enabled = false;
                }
                else
                {
                    MessageBox.Show(MsgPayTypeParameterEmpty,
                        MsgContractGuarantee, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (CheckParamForClose("InitDoc"))
                    {
                        btnExit_Click(this, EventArgs.Empty);
                        return;
                    }
                }
            }
            else
            {
                dateCloseGarant.Text = "";
            }
        }

        private void FillLvwGuarant()
        {
            lvwGuarant.Items.Clear();

            if (m_arrGuarant != null)
            {
                for (int i = 0; i < m_arrGuarant.GetLength(0); i++)
                {
                    string idText = Convert.ToString(m_arrGuarant[i, 0]);

                    ListViewItem item = new ListViewItem(idText);
                    item.Name = "Key: " + idText;
                    item.Tag = m_arrGuarant[i, 0];

                    item.SubItems.Add(Convert.ToString(m_arrGuarant[i, 1]));
                    item.SubItems.Add(Convert.ToString(m_arrGuarant[i, 2]));

                    lvwGuarant.Items.Add(item);
                }
            }
        }

        private void SetInfoByFrameContract()
        {
            m_idPrincipal = Convert.ToInt32(base.UbsChannel_ParamOut("Идентификатор клиента-принципала"));
            txtPrincipal.Text = Convert.ToString(base.UbsChannel_ParamOut("Принципал"));
            linkPrincipal.Enabled = false;

            if (m_termsFrameContract != null && m_termsFrameContract.GetType().IsArray)
                SetKindComboBoxByFrame();
            else
                ResetKindComboBox();

            SetTabsEnabled(false, 1);

            m_idCurrency = Convert.ToInt32(base.UbsChannel_ParamOut("Идентификатор валюты рамочного договора"));

            if (m_idState == 4)
            {
                if (cmbTypePayFee.Text == "Процент суммы гарантии")
                {
                    cmbCurrencyPayFee.SelectedValue = Convert.ToInt32(cmbCurrencyGarant.SelectedValue);
                    cmbCurrencyPayFee.Enabled = false;
                }
                else
                {
                    cmbCurrencyPayFee.Enabled = true;
                }

                if (cmbTypePayFeeBonus.Text == "Процент суммы гарантии")
                {
                    cmbCurrencyRewardGuarant.SelectedValue = Convert.ToInt32(cmbCurrencyGarant.SelectedValue);
                    cmbCurrencyRewardGuarant.Enabled = false;
                }
                else
                {
                    cmbCurrencyRewardGuarant.Enabled = true;
                }

                cmbKindGarant.Enabled = true;

                if (m_divisionFrameContract > 0)
                {
                    cmbNumberDiv.SelectedValue = Convert.ToInt32(m_divisionFrameContract);
                }
            }
            else if (m_idState == 2 && m_command.ToUpperInvariant() == EditCommand)
            {
                cmbKindGarant.Enabled = true;
            }
            else
            {
                cmbKindGarant.Enabled = false;
            }

            cmbCurrencyGarant.Enabled = cmbCurrencyGarantEnabled;

            cmbCurrencyGarant.SelectedValue = m_idCurrency;
        }
        private void SetKindComboBoxByFrame()
        {
            string selected = cmbKindGarant.Text;

            cmbKindGarant.BeginUpdate();
            try
            {
                cmbKindGarant.Items.Clear();

                if (m_termsFrameContract != null)
                {
                    int rows = m_termsFrameContract.GetLength(0);

                    for (int i = 0; i < rows; i++)
                    {
                        cmbKindGarant.Items.Add(Convert.ToString(m_termsFrameContract[i, 0]));
                    }
                }

                cmbKindGarant.Items.Add(string.Empty);

                if (cmbKindGarant.Items.Count > 0)
                    cmbKindGarant.SelectedIndex = cmbKindGarant.Items.Count - 1;

                cmbKindGarant.Text = string.Empty;

                if (!string.IsNullOrEmpty(selected))
                    cmbKindGarant.Text = selected;
            }
            finally
            {
                cmbKindGarant.EndUpdate();
            }
        }

        private void SetEnableCombos()
        {
            if (m_idState == 4)
            {
                btnReRead.Enabled = true;

                SetTabsEnabled(true, 2, 3, 4);

                cmbOrderPayFee.Enabled = true;
                cmbTypePayFee.Enabled = true;
                cmbOrderPayFeeBonus.Enabled = true;
                cmbTypePayFeeBonus.Enabled = true;
                cmbOrderPayFeeGuarant.Enabled = true;
                cmbTypePayFeeGuarant.Enabled = true;

                linkGarant.Enabled = true;
                cmbNumberDiv.Enabled = true;
                linkPreviousContract.Enabled = true;
            }

            cmbCurrencyGarant.Enabled = cmbCurrencyGarantEnabled;

        }

        private void GuarCmdState(bool inState, bool inLock = false)
        {
            if (inLock)
                inState = false;

            btnAddGuarant.Enabled = inState;
            btnInclude.Enabled = inState;
            btnListGuarantOperDog.Enabled = inState;

            if (lvwGuarant.SelectedItems.Count == 0 || inLock)
                inState = false;

            btnEditGuarant.Enabled = inState;
        }


        private void InitComboBox(ComboBox cmb, object list)
        {
            cmb.DataSource = null;
            cmb.DataSource = list;
            cmb.ValueMember = "Key";
            cmb.DisplayMember = "Value";
            if (cmb.Items.Count > 0)
                cmb.SelectedIndex = 0;
        }

        private static List<KeyValuePair<int, string>> MakeKvpList(object[,] vbArr, int colId = 0, int colText = 1)
        {
            var res = new List<KeyValuePair<int, string>>();

            if (vbArr == null)
                return res;

            int rows = vbArr.GetLength(0);

            for (int r = 0; r < rows; r++)
            {
                int id = Convert.ToInt32(vbArr[r, colId]);
                string text = Convert.ToString(vbArr[r, colText]);

                res.Add(new KeyValuePair<int, string>(id, text));
            }

            return res;
        }

        private static List<KeyValuePair<int, string>> MakeKvpList(object[] vbArr)
        {
            var res = new List<KeyValuePair<int, string>>();

            if (vbArr == null)
                return res;

            int rows = vbArr.GetLength(0);

            for (int r = 0; r < rows; r++)
            {
                int id = r;
                string text = Convert.ToString(vbArr[r]);

                res.Add(new KeyValuePair<int, string>(id, text));
            }

            return res;
        }
        #endregion

        private void linkFrameContract_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EnabledCmdControl(false);

            object[] ids = this.Ubs_ActionRun(ActionUbsBgFrameContractList, this, true) as object[];

            if (ids != null && ids.Length > 0)
            {
                m_idFrameContract = Convert.ToInt32(ids[0]);

                base.IUbsChannel.ParamIn("ID", m_idFrameContract);

                base.IUbsChannel.Run("BGReadFrameContractById");

                txtFrameContract.Text = Convert.ToString(base.IUbsChannel.ParamOut("Наименование"));
                m_dateBeginFrameContract = Convert.ToDateTime(base.IUbsChannel.ParamOut("Дата начала действия рамочного договора"));
                m_dateEndFrameContract = Convert.ToDateTime(base.IUbsChannel.ParamOut("Дата окончания действия рамочного договора"));
                m_termsFrameContract = base.IUbsChannel.ParamOut("Срок гарантии рамочного договора") as object[,];
                m_limitSaldoFrameContract = Convert.ToDecimal(base.IUbsChannel.ParamOut("Остаток лимита"));
                m_issueEndDate = Convert.ToDateTime(base.IUbsChannel.ParamOut("Дата окончания выдачи"));
                m_divisionFrameContract = Convert.ToInt32(base.IUbsChannel.ParamOut("Номер отделения"));

                SetInfoByFrameContract();

                linkModel.Focus();
            }
        }
        private void btnFrameContractDel_Click(object sender, EventArgs e)
        {
            m_idFrameContract = 0;
            txtFrameContract.Text = string.Empty;
            linkPrincipal.Enabled = true;
            cmbCurrencyGarant.Enabled = IsCmbValutaEnabled();
            m_dateBeginFrameContract =
                m_dateEndFrameContract = m_minDate;
            m_termsFrameContract = null;
            m_limitSaldoFrameContract = 0m;
            m_issueEndDate = m_minDate;
            m_divisionFrameContract = 0;

            ResetKindComboBox();

            cmbKindGarant.Enabled = true;
            tabPage1.Enabled = true;
        }

        private void linkModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EnabledCmdControl(false);

            object[] ids = this.Ubs_ActionRun(ActionUbsBgListModel, this, true) as object[];

            if (ids != null && ids.Length > 0)
            {
                m_idModel = Convert.ToInt32(ids[0]);

            }

            EnabledCmdControl(true);
        }
        private void linkAgent_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EnabledCmdControl(false);

            object[] ids = this.Ubs_ActionRun(ActionUbsBgListAgent, this, true) as object[];

            if (ids != null && ids.Length > 0)
            {
                m_idAgent = Convert.ToInt32(ids[0]);

                base.IUbsChannel.ParamIn("DateBegin", dateOpenGarant.DateValue);
                base.IUbsChannel.ParamIn("IdAgent", m_idAgent);

                base.IUbsChannel.Run("GetDatePayment");

                dateReward.DateValue = Convert.ToDateTime(base.IUbsChannel.ParamOut("DatePayment"));

                if (m_command == EditCommand)
                {
                    base.IUbsChannel.Run("AgentRewardAvaliable");

                    dateReward.Enabled =
                        costAmount.Enabled =
                        Convert.ToBoolean(base.IUbsChannel.ParamOut("Avaliable"));
                }
                else
                {
                    dateReward.Enabled = true;
                    costAmount.Enabled = true;
                }

                base.IUbsChannel.ParamIn("ID", m_idAgent);//ЗДЕСЬ ЛЕЖИТ ДОГОВОР С АГЕНТОМ - ID_AG_CONTRACT

                base.IUbsChannel.Run("BGReadAgContr");

                var idAgClient = Convert.ToInt32(base.IUbsChannel.ParamOut("Ид клиента"));
                var numAg = Convert.ToString(base.IUbsChannel.ParamOut("Номер договора агента"));
                var dateAg = Convert.ToDateTime(base.IUbsChannel.ParamOut("Дата договора агента"));

                txtNumAgent.Text = numAg;
                dateAgent.DateValue = dateAg;

                base.IUbsChannel.ParamIn("ID", idAgClient);

                base.IUbsChannel.Run("BGReadClientById");

                txtAgent.Text = Convert.ToString(base.IUbsChannel.ParamOut("Наименование"));
            }

            EnabledCmdControl(true);
        }

        private void btnAgentDel_Click(object sender, EventArgs e)
        {
            m_idAgent = 0;
            txtAgent.Text = string.Empty;
            txtNumAgent.Text = string.Empty;
            dateAgent.Text = string.Empty;
            dateReward.Text = string.Empty;
            dateAdjustment.Text = string.Empty;
            costAmount.DecimalValue = 0m;
            paidAmount.DecimalValue = 0m;
            transAmount.DecimalValue = 0m;
            dateReward.Enabled = false;
            costAmount.Enabled = false;
        }

        private void linkPrincipal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EnabledCmdControl(false);

            object[] ids = this.Ubs_ActionRun(ActionUbsCommonListClient, this, true) as object[];

            if (ids != null && ids.Length > 0)
            {
                m_idPrincipal = Convert.ToInt32(ids[0]);

                base.IUbsChannel.ParamIn("ID", m_idPrincipal);

                base.IUbsChannel.Run("BGReadClientById");

                txtPrincipal.Text = Convert.ToString(base.IUbsChannel.ParamOut("Наименование"));

                linkBeneficiar.Focus();
            }

            EnabledCmdControl(true);
        }
        private void linkBeneficiar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EnabledCmdControl(false);

            object[] ids = this.Ubs_ActionRun(ActionUbsCommonListClient, this, true) as object[];

            if (ids != null && ids.Length > 0)
            {
                m_idBeneficiar = Convert.ToInt32(ids[0]);
                m_arrDetailsBeneficiar = null;

                base.IUbsChannel.ParamIn("ID", m_idBeneficiar);

                base.IUbsChannel.Run("BGReadClientById");

                txtBeneficiar.Text = Convert.ToString(base.IUbsChannel.ParamOut("Наименование"));

                if (linkGarant.Enabled)
                    linkGarant.Focus();
                else
                    txtNumberGarant.Focus();
            }

            EnabledCmdControl(true);
        }
        private void EnabledCmdControl(bool isEnabled)
        {
            linkBeneficiar.Enabled = isEnabled;

            if (m_idState != 0 && m_idState != 2)
            {
                btnManualBenificiar.Enabled = isEnabled;
            }
            if (m_modelType == UbsCounterGuarantTypeCommand)
            {
                linkGarant.Enabled = isEnabled;
            }

            linkModel.Enabled = isEnabled;
            linkAgent.Enabled = isEnabled;
            linkPreviousContract.Enabled = isEnabled;

            if (m_idFrameContract == 0)
            {
                txtPrincipal.Enabled = isEnabled;
            }
            if (linkFrameContract.Visible)
            {
                linkFrameContract.Enabled = isEnabled;
            }
            if (btnFrameContractDel.Visible)
            {
                btnFrameContractDel.Enabled = isEnabled;
            }
            if (linkAgent.Visible)
            {
                linkAgent.Enabled = isEnabled;
            }
            if (btnAgentDel.Visible)
            {
                btnAgentDel.Enabled = isEnabled;
            }

            if (m_setRekvBen == 1)
            {
                btnManualBenificiar.Enabled = false;
            }
        }
        private void ubsBgContractFrm_Ubs_ActionRunBegin(object sender, UbsActionRunEventArgs args)
        {
            if (args.Action == ActionUbsBgListAgent)
            {
                args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                    new KeyValuePair<string, object>("наименование", "Состояние"),
                    new KeyValuePair<string, object>("значение по умолчанию", "Открыт"),
                    new KeyValuePair<string, object>("условие по умолчанию", "="),
                    new KeyValuePair<string, object>("скрытый", false) }));

                args.IUbs.Run("UbsItemsRefresh", null);
            }
            else if (args.Action == ActionUbsBgListModel)
            {
                args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                    new KeyValuePair<string, object>("наименование", "Состояние"),
                    new KeyValuePair<string, object>("значение по умолчанию", "0"),
                    new KeyValuePair<string, object>("условие по умолчанию", "="),
                    new KeyValuePair<string, object>("скрытый", true) }));

                if (m_idFrameContract > 0 && m_termsFrameContract != null)
                {
                    var kinds = new object[m_termsFrameContract.GetLength(0)];

                    for (int i = 0; i < m_termsFrameContract.GetLength(0); i++)
                    {
                        kinds[i] = m_termsFrameContract[i, 0];
                    }

                    args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                    new KeyValuePair<string, object>("наименование", "Вид гарантии"),
                    new KeyValuePair<string, object>("значение по умолчанию", kinds),
                    new KeyValuePair<string, object>("условие по умолчанию", "один из"),
                    new KeyValuePair<string, object>("скрытый", false) }));
                }


                args.IUbs.Run("UbsItemsRefresh", null);
            }
            else if (args.Action == ActionUbsBgFrameContractList)
            {
                args.IUbs.Run("UbsItemSet", new UbsParam(new KeyValuePair<string, object>[] {
                    new KeyValuePair<string, object>("наименование", "Состояние"),
                    new KeyValuePair<string, object>("значение по умолчанию", "0"),
                    new KeyValuePair<string, object>("условие по умолчанию", "="),
                    new KeyValuePair<string, object>("скрытый", false) }));

                args.IUbs.Run("UbsItemsRefresh", null);
            }
        }
        private bool IsCmbValutaEnabled() => (m_idState == 4 && m_idFrameContract == 0);

        private void linkGarant_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EnabledCmdControl(false);

            object[] ids = this.Ubs_ActionRun(ActionUbsCommonListClient, this, true) as object[];

            if (ids != null && ids.Length > 0)
            {
                m_idGarant = Convert.ToInt32(ids[0]);

                base.IUbsChannel.ParamIn("ID", m_idGarant);

                base.IUbsChannel.Run("BGReadClientById");

                txtGarant.Text = Convert.ToString(base.IUbsChannel.ParamOut("Наименование"));

                txtNumberGarant.Focus();
            }

            EnabledCmdControl(true);
        }

        private void linkPreviousContract_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EnabledCmdControl(false);

            object[] ids = this.Ubs_ActionRun(ActionUbsBgListContract, this, true) as object[];

            if (ids != null && ids.Length > 0)
            {
                m_idPrevContract = Convert.ToInt32(ids[0]);

                base.IUbsChannel.ParamIn("IdPrevContract", m_idPrevContract);

                base.IUbsChannel.Run("BGReadPreviuosContract");

                txtPreviousContract.Text = Convert.ToString(base.IUbsChannel.ParamOut("InfoPrevContract"));

                if (tabControl.TabPages.Count > 2 && tabControl.TabPages[2].Enabled)
                {
                    tabControl.SelectedIndex = 2;
                    cmbPortfolio.Focus();
                }
                else if (tabControl.TabPages.Count > 3)
                {
                    tabControl.SelectedIndex = 3;
                }
            }

            EnabledCmdControl(true);
        }
    }
}