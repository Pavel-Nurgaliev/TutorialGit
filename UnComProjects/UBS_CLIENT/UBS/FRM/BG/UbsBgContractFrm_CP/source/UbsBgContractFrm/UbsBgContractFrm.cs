using AxUBSPROPLib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using UbsControl;
using UbsService;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

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
        private const string MsgRateTypesArrayEmpty = "Массив типов процентных ставок пуст!";
        private const string MsgAccountsListEmpty = "Список счетов пуст!";
        private const string MsgContractGuaranteeCaption = "Договор банковских гарантий";
        private const string UbsGuarantCommand = "UBS_GUARANT";
        private const string PercentSumGuarant = "Процент суммы гарантии";

        /// <summary>Префикс типа ставки для канала (процентные ставки).</summary>
        private const string StrRatePrefix = "[Процентные ставки].";

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

        private string m_captionForm = "Изменить договор гарантии";

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
        private object[] m_arrRates;
        /// <summary>Значения ставок по типам: каждый элемент — object[,] [row, col] (col 0 = дата, col 1 = значение) или null.</summary>
        private object[] m_arrRateValues;
        private bool m_isInitCurrencyBonus;
        private object[,] m_arrPeriodPay;
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
        private object[,] m_arrCountry;
        private object[,] m_arrTypeObject;
        private object[,] m_arrAccounts;

        private readonly DateTime MinDate = new DateTime(1990, 1, 1);
        private readonly DateTime MaxDate = new DateTime(2222, 1, 1);

        private readonly UbsParam m_paramIn = new UbsParam();
        private readonly UbsParam m_paramOut = new UbsParam();
        private readonly AxUbsControlProperty m_axUbsControlProperty
            = new AxUbsControlProperty();

        private UbsAddFiledsStub.IUbsAddFiledsStubClass m_objStub = null;
        private int m_idFrameContractDivision;
        private object[,] m_arrInterval;
        private int m_idCurrencyPayFee;
        private int m_idCurrencyCover;

        private string m_loadParam;
        private object[] m_arrPayOrder;
        private object[] m_arrTypeBonus;
        private DateTime m_dateNextPayFeeValue;
        private DateTime m_dateNextPayFeeBonusValue;
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

            m_objStub = new UbsAddFiledsStub.IUbsAddFiledsStubClass();
            m_objStub.UbsChannel = this.IUbsChannel;

            m_axUbsControlProperty.UbsAddFields = m_objStub;

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
                    MessageBox.Show(MsgContractNotSelected, m_captionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                    MessageBox.Show(MsgContractNotSelected, m_captionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                    MessageBox.Show(MsgFrameContractNotSelected, m_captionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                    MessageBox.Show(MsgFrameContractNotSelected, m_captionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                RunBgContractInitAndParseOutput();

                InitCmsAccounts();
                InitComboBoxes();

                SetControlsStateByCommand();

                if (m_command.ToUpperInvariant() == CopyCommand)
                    InitDocForCopyCommand();

                ReReadDoc();

                if (m_command == CopyCommand)
                    linkModel.Enabled = true;

                LoadFrameContractForAddOrPrepare();
                ApplyNextPayFeeDatesAndEditLinks();
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

        private void RunBgContractInitAndParseOutput()
        {
            base.IUbsChannel.LoadResource = "VBS:UBS_VBD\\BG\\BG_CONTRACT.vbs";

            m_paramIn.Clear();
            m_paramOut.Clear();

            m_dateToday = GetCurrentDate();

            base.UbsInit();

            base.UbsChannel_ParamIn("StrCommand", m_command);

            base.UbsChannel_Run("BG_Contract_Init");

            m_paramOut.Clear();
            m_paramOut.ItemsVector = base.UbsChannel_ParamsOut;

            ubsCtrlFields.Refresh();

            int setRekvBen =
                Convert.ToInt32(m_paramOut.Value("setRekvBen"));

            bool limitExcessCheckOn =
                Convert.ToBoolean(m_paramOut.Value("Контроль лимитов"));
            int reflectComIssue =
                Convert.ToInt32(m_paramOut.Value("Режим учета вознаграждений"));

            object[,] arrExecutEx = m_paramOut.Value("ОИ вып.оп.") as object[,];

            // "Список типов процентных ставок"
            m_arrRates = null;

            if (m_paramOut.Contains("Список типов процентных ставок"))
                m_arrRates = m_paramOut.Value("Список типов процентных ставок") as object[];
            else
            {
                MessageBox.Show(MsgRateTypesParameterMissing,
                    MsgWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                m_arrRates = null;
            }

            m_isInitCurrencyBonus = false;

            dateOpenGarant.DateValue = m_dateToday;
        }

        private void SetControlsStateByCommand()
        {
            dateNextPayFee.Enabled = (m_idState >= 2);

            if (m_command == AddCommand || m_command.ToUpperInvariant() == PrepareCommand)
            {
                btnReRead.Enabled = false;
                SetTabsEnabled(false, 2, 3, 4);

                cmbOrderPayFee.Enabled = true;
                cmbTypePayFee.Enabled = true;
                cmbOrderPayFeeBonus.Enabled = true;
                cmbTypePayFeeBonus.Enabled = true;
            }
            else if (m_command == CopyCommand)
            {
                btnReRead.Enabled = false;
                SetTabsEnabled(true, 2, 3, 4);

                cmbOrderPayFee.Enabled = true;
                cmbTypePayFee.Enabled = true;
                cmbOrderPayFeeBonus.Enabled = true;
                cmbTypePayFeeBonus.Enabled = true;
                cmbOrderPayFeeGuarant.Enabled = true;
                cmbTypePayFeeGuarant.Enabled = true;
                linkModel.Enabled = true;
            }
            else
            {
                btnReRead.Enabled = true;
                SetTabsEnabled(true, 2, 3, 4);

                cmbOrderPayFee.Enabled = false;
                cmbTypePayFee.Enabled = false;
                cmbOrderPayFeeBonus.Enabled = false;
                cmbTypePayFeeBonus.Enabled = false;
                cmbOrderPayFeeGuarant.Enabled = false;
                cmbTypePayFeeGuarant.Enabled = false;
                cmbCurrencyRewardGuarant.Enabled = false;
                cmbCurrencyPayFee.Enabled = false;
            }
        }

        private void LoadFrameContractForAddOrPrepare()
        {
            if (m_command == AddCommand || m_command == PrepareCommand)
            {
                m_idState = 4;

                m_paramIn.Value("ID", m_idFrameContract);
                RunUbsChannelFunction("BGReadFrameContractById", m_paramIn, m_paramOut);

                if (m_paramOut.Contains("Ошибка"))
                {
                    m_idFrameContract = 0;
                    MessageBox.Show(Convert.ToString(m_paramOut.Value("Ошибка")), m_captionForm, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    txtFrameContract.Text = Convert.ToString(m_paramOut.Value("Наименование"));
                    m_dateBeginFrameContract = Convert.ToDateTime(m_paramOut.Value("Дата начала действия рамочного договора"));
                    m_dateEndFrameContract = Convert.ToDateTime(m_paramOut.Value("Дата окончания действия рамочного договора"));
                    m_termsFrameContract = m_paramOut.Value("Срок гарантии рамочного договора") as object[,];
                    m_limitSaldoFrameContract = Convert.ToDecimal(m_paramOut.Value("Остаток лимита"));
                    m_issueEndDate = Convert.ToDateTime(m_paramOut.Value("Дата окончания выдачи"));
                    m_idFrameContractDivision = Convert.ToInt32(m_paramOut.Value("Номер отделения"));

                    SetInfoByFrameContract();
                }
            }
        }

        private void ApplyNextPayFeeDatesAndEditLinks()
        {
            if (m_dateNextPayFeeValue > DateTime.MinValue && m_dateNextPayFeeValue > MinDate)
            {
                dateNextPayFee.DateValue = m_dateNextPayFeeValue;
            }
            if (m_dateNextPayFeeBonusValue > DateTime.MinValue && m_dateNextPayFeeBonusValue > MinDate)
            {
                dateNextPayFeeBonus.DateValue = m_dateNextPayFeeBonusValue;
            }

            if (m_command == EditCommand && m_idState == 4)
            {
                linkPrincipal.Enabled = true;
                linkModel.Enabled = true;
                linkFrameContract.Visible = true;
                linkFrameContract.Enabled = true;
                linkAgent.Visible = true;
                linkAgent.Enabled = true;
            }
        }

        private void InitDocForCopyCommand()
        {
            UbsChannel_ParamIn("Id", m_idContractCopy);
            UbsChannel_Run("BG_Contract_Copy");

            m_arrPeriodPay = m_paramOut.Value("Срок погашения оплаченной суммы") as object[,];

            m_isFixSum = Convert.ToString(m_paramOut.Value("Тип вознаграждения")) == "Фиксированная сумма";

            m_axUbsControlProperty.UbsAddFields = m_objStub;
            m_axUbsControlProperty.Refresh();

            txtModel.Text = Convert.ToString(m_paramOut.Value("Типовой договор"));
            txtBeneficiar.Text = Convert.ToString(m_paramOut.Value("Бенефициар"));
            txtGarant.Text = Convert.ToString(m_paramOut.Value("Гарант"));
            txtFrameContract.Text = Convert.ToString(m_paramOut.Value("Наименование рамочного договора"));

            m_idModel = Convert.ToInt32(m_paramOut.Value("Идентификатор типового договора"));
            m_idPrincipal = Convert.ToInt32(m_paramOut.Value("Идентификатор клиента-принципала"));
            m_idBeneficiar = Convert.ToInt32(m_paramOut.Value("Идентификатор клиента-бенефициара"));

            if (m_idBeneficiar == 0)
            {
                m_arrDetailsBeneficiar = m_paramOut.Value("Реквизиты бенефициара") as object[,];
            }
            var paymentOrder = Convert.ToString(m_paramOut.Value("Порядок уплаты вознаграждения"));

            m_idDivision = Convert.ToInt32(m_paramOut.Value("Номер отделения"));
            m_idOI = Convert.ToInt32(m_paramOut.Value("Идентификатор ОИ"));
            m_idWarrant = Convert.ToInt32(m_paramOut.Value("Идентификатор доверенности"));

            m_idState = 2;

            m_idCurrency = Convert.ToInt32(m_paramOut.Value("Идентификатор валюты"));

            m_idGarant = Convert.ToInt32(m_paramOut.Value("Идентификатор клиента-гаранта"));
            m_idFrameContract = Convert.ToInt32(m_paramOut.Value("Идентификатор рамочного договора"));

            if (m_idFrameContract > 0)
            {
                m_dateBeginFrameContract = Convert.ToDateTime(m_paramOut.Value("Дата начала действия рамочного договора"));
                m_dateEndFrameContract = Convert.ToDateTime(m_paramOut.Value("Дата окончания действия рамочного договора"));
                m_termsFrameContract = m_paramOut.Value("Срок гарантии рамочного договора") as object[,];
                m_limitSaldoFrameContract = Convert.ToDecimal(m_paramOut.Value("Остаток лимита рамочного договора"));
                m_issueEndDate = Convert.ToDateTime(m_paramOut.Value("Дата окончания выдачи"));
                m_idFrameContractDivision = Convert.ToInt32(m_paramOut.Value("Отделение рамочного договора"));

                SetInfoByFrameContract();
            }
            else
            {
                txtPrincipal.Text = Convert.ToString(m_paramOut.Value("Принципал"));
            }

            m_arrInterval = new object[1, 4];

            m_arrInterval[0, 0] =
                m_paramOut.Value("Вознаграждение. Тип периода");
            m_arrInterval[0, 1] =
                m_paramOut.Value("Вознаграждение. Период");
            m_arrInterval[0, 2] =
                m_paramOut.Value("Вознаграждение. Тип даты");
            m_arrInterval[0, 3] =
                m_paramOut.Value("Вознаграждение. Номер дня");

            m_orderPayFeeGuarant =
                Convert.ToString(m_paramOut.Value("Порядок уплаты вознаграждения (гарант)"));

            if (m_orderPayFeeGuarant != string.Empty)
            {
                gbPayFeeGuarant.Visible = true;
                var typePayFeeGuarant = Convert.ToString(m_paramOut.Value("Тип вознаграждения (гарант)"));

                if (m_orderPayFeeGuarant == "Периодически")
                {
                    dateNextPayFeeGuarant.DateValue = Convert.ToDateTime(m_paramOut.Value("Дата след. уплаты вознаграждения (гарант)"));
                    m_arrIntervalGuarant = m_paramOut.Value("Вознаграждение (гарант)") as object[,];
                }
            }

            if (m_paramOut.Contains("Идентификатор портфеля"))
            {
                SetComboText(cmbPortfolio, Convert.ToInt32(m_paramOut.Value("Идентификатор портфеля")));
            }
            if (m_paramOut.Contains("Тип оценки риска"))
            {
                SetComboText(cmbTypeValidationRisk, Convert.ToInt32(m_paramOut.Value("Тип оценки риска")));
            }
            if (m_paramOut.Contains("Группа риска"))
            {
                cmbQualityCategory.Focus();

                cmbQualityCategory.Text = Convert.ToString(m_paramOut.Value("Группа риска"));
            }
            if (m_paramOut.Contains("Ставка резервирования"))
            {
                ucdRateReservation.Focus();

                ucdRateReservation.DecimalValue = Convert.ToDecimal(m_paramOut.Value("Ставка резервирования"));
            }

            SetComboById(cmbNumberDiv,
                Convert.ToInt32(m_idDivision));
            SetComboById(cmbExecutor,
                m_idOI);
            SetComboById(cmbWarrant, m_idWarrant);
            SetComboById(cmbState, m_idState);
            SetComboById(cmbCurrencyGarant, m_idCurrency);

            txtNumberGarant.Text = Convert.ToString(m_paramOut.Value("Номер договора"));
            dateOpenGarant.Text = Convert.ToString(m_paramOut.Value("Дата заключения"));
            dateCloseGarant.Text = Convert.ToString(m_paramOut.Value("Дата закрытия"));
            dateBeginGarant.Text = Convert.ToString(m_paramOut.Value("Дата начала действия"));
            dateEndGarant.Text = Convert.ToString(m_paramOut.Value("Дата окончания действия"));

            m_idCurrencyPayFee = Convert.ToInt32(m_paramOut.Value("Идентификатор валюты вознаграждения"));
            SetComboText(cmbCurrencyPayFee, m_idCurrencyPayFee);

            m_isInitCurrencyBonus = true;

            ucdSumGarant.DecimalValue = Convert.ToDecimal(m_paramOut.Value("Сумма гарантии"));
            m_idCoverType = Convert.ToInt32(m_paramOut.Value("Тип покрытия"));

            SetComboText(cmbTypeCover, m_idCoverType);

            ucdSumCover.DecimalValue = Convert.ToDecimal(m_paramOut.Value("Сумма покрытия"));
            m_idCurrencyCover = Convert.ToInt32(m_paramOut.Value("Идентификатор валюты покрытия"));
            SetComboText(cmbCurrencyCover, m_idCurrencyCover);

            if (m_paramOut.Contains("Договор покрытия"))
            {
                txtContractCover.Text = Convert.ToString(m_paramOut.Value("Договор покрытия"));
            }

            var accs = m_paramOut.Value("Счета") as object[,];
            m_axUbsControlProperty.set_PropertyByName("Реквизиты оплаты гарантии", m_paramOut.Value("Реквизиты оплаты гарантии"));

            m_paramIn.Value("ID", m_idModel);
            RunUbsChannelFunction("BGReadModelById", m_paramIn, m_paramOut);

            m_arrPeriodPay = m_paramOut.Value("Срок погашения оплаченной суммы") as object[,];

            if (m_arrPeriodPay != null)
            {
                var arrRatesFromModel = new object[1, 2];

                if (m_arrRateValues != null)
                {
                    if (m_arrRateValues.Length != m_arrRates.Length)
                    {
                        Array.Resize(ref m_arrRateValues, m_arrRates.GetLength(0));
                    }
                }
                else
                {
                    m_arrRateValues = new object[m_arrRates.GetLength(0)];
                }

                arrRatesFromModel[0, 0] = dateBeginGarant.DateValue;

                for (int i = 0; i < m_arrRates.Length; i++)
                {
                    switch (m_arrRates[i].ToString())
                    {
                        case "Ставка % по оплаченной гарантии":
                            arrRatesFromModel[0, 1] = m_arrPeriodPay[0, 2];
                            m_arrRateValues[i] = arrRatesFromModel;
                            break;

                        case "Ставка % по просроченной оплате гарантии":
                            arrRatesFromModel[0, 1] = m_arrPeriodPay[0, 3];
                            m_arrRateValues[i] = arrRatesFromModel;
                            break;

                        case "Ставка пени за просрочку погашения процентов по оплаченной гарантии":
                        case "Ставка пени за просрочку погашения оплаченной гарантии":
                            arrRatesFromModel[0, 1] = m_arrPeriodPay[0, 4];
                            m_arrRateValues[i] = arrRatesFromModel;
                            break;

                        case "Ставка вознаграждения за пользование гарантией":
                            arrRatesFromModel[0, 1] =
                                Convert.ToDouble(m_paramOut.Value("Ставка вознаграждения"));
                            m_arrRateValues[i] = arrRatesFromModel;
                            break;

                        case "Ставка вознаграждения (гарант)":
                            arrRatesFromModel[0, 1] =
                                Convert.ToDouble(m_paramOut.Value("Ставка вознаграждения (гарант)"));
                            m_arrRateValues[i] = arrRatesFromModel;
                            break;

                        case "Ставка вознаграждения за выдачу гарантии":
                            arrRatesFromModel[0, 1] =
                                Convert.ToDouble(m_paramOut.Value("Ставка вознаграждения за выдачу"));
                            m_arrRateValues[i] = arrRatesFromModel;
                            break;
                    }
                }

                InitTrvRates(true);
            }

            m_axUbsControlProperty.set_PropertyByName("Очередность платежей", m_paramOut.Value("Очередность платежей"));
            m_axUbsControlProperty.set_PropertyByName("Досрочное гашение в групповом режиме", m_paramOut.Value("Досрочное гашение в групповом режиме"));
            m_axUbsControlProperty.set_PropertyByName("Параметры расчета графиков", m_paramOut.Value("Параметры расчета графиков"));
            m_axUbsControlProperty.set_PropertyByName("Сумма платежа", m_paramOut.Value("Сумма платежа"));
            m_axUbsControlProperty.set_PropertyByName("Алгоритм расчета процентов", m_paramOut.Value("Алгоритм расчета процентов"));
            m_axUbsControlProperty.set_PropertyByName("Льготный период", m_paramOut.Value("Льготный период"));
            m_axUbsControlProperty.set_PropertyByName("Режим смещения выходных дней", m_paramOut.Value("Режим смещения выходных дней"));
            m_axUbsControlProperty.set_PropertyByName("Алгоритм расчета суммы платежа", m_paramOut.Value("Алгоритм расчета суммы платежа"));
            m_axUbsControlProperty.set_PropertyByName("Исключить неполный первый период", m_paramOut.Value("Исключить неполный первый период"));
            m_axUbsControlProperty.set_PropertyByName("Отсрочка", m_paramOut.Value("Отсрочка"));

            txtModel.Text = Convert.ToString(m_paramOut.Value("Наименование"));
            m_modelType = Convert.ToString(m_paramOut.Value("Идентификатор шаблона"));
            m_orderPayFeeGuarant = Convert.ToString(m_paramOut.Value("Порядок уплаты вознаграждения (гарант)"));

            if (m_orderPayFeeGuarant != string.Empty)
            {
                gbPayFeeGuarant.Visible = true;
                if (m_orderPayFeeGuarant == "Периодически")
                {
                    dateNextPayFeeGuarant.Visible = true;
                    btnPeriodPayFeeGuarant.Visible = true;
                    lblDateNextPayFeeGuarant.Visible = true;
                }
            }
            else
            {
                gbPayFeeGuarant.Visible = false;
            }

            if (m_paramOut.Contains("Порядок уплаты вознаграждения (список)") || m_paramOut.Value("Порядок уплаты вознаграждения (список)") == null)
            {
                MessageBox.Show(MsgPayOrderParameterEmpty, MsgContractGuarantee, MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (CheckParamForClose(m_loadParam))
                {
                    btnExit_Click(this, EventArgs.Empty);
                }
            }

            m_arrPayOrder = m_paramOut.Value("Порядок уплаты вознаграждения (список)") as object[];
            FillComboText(cmbOrderPayFee, m_arrPayOrder);
            SetComboValueText(cmbOrderPayFee, paymentOrder);

            FillComboText(cmbOrderPayFeeBonus, m_arrPayOrder);
            SetComboValueText(cmbOrderPayFeeBonus, paymentOrder);

            if (m_orderPayFeeGuarant != string.Empty)
            {
                FillComboText(cmbOrderPayFeeGuarant, m_arrPayOrder);
                SetComboValueText(cmbOrderPayFeeGuarant, m_orderPayFeeGuarant);
            }

            if (m_paramOut.Contains("Тип вознаграждения (список)") || m_paramOut.Value("Порядок уплаты вознаграждения (список)") == null)
            {
                MessageBox.Show(MsgPayTypeParameterEmpty, MsgContractGuarantee, MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (CheckParamForClose(m_loadParam))
                {
                    btnExit_Click(this, EventArgs.Empty);
                }
            }

            m_arrTypeBonus = m_paramOut.Value("Тип вознаграждения (список)") as object[];
            FillComboText(cmbTypePayFee, m_arrTypeBonus);
            SetComboValueText(cmbTypePayFee, Convert.ToString(m_paramOut.Value("Тип вознаграждения")));

            FillComboText(cmbTypePayFeeBonus, m_arrTypeBonus);
            SetComboValueText(cmbTypePayFeeBonus, Convert.ToString(m_paramOut.Value("Тип вознаграждения за выдачу")));

            SetBonusCtrlsState();
            SetBonusCtrlsBonusState();

            if (m_orderPayFeeGuarant != string.Empty)
            {
                FillComboText(cmbTypePayFeeGuarant, m_arrTypeBonus);
                SetComboValueText(cmbTypePayFeeGuarant, Convert.ToString(m_paramOut.Value("Тип вознаграждения (гарант)")));
            }

            SetTabsEnabled(false, 2, 3);

            m_paramIn.Value("Идентификатор типового договора", m_idModel);
            m_paramIn.Value("Порядок уплаты вознаграждения", cmbOrderPayFee.Text);

            if (m_modelType == "UBS_GUARANT")
            {
                if (m_idState != 2)
                {
                    linkGarant.Enabled = false;
                }
                else
                {
                    linkGarant.Enabled = true;
                }

                m_idGarant = 0;
                txtGarant.Text = string.Empty;
            }
            else
            {
                linkGarant.Enabled = true;
            }

            RunUbsChannelFunction("BG_Contract_Init_Ucp", m_paramIn, m_paramOut);

            m_arrAccounts = accs;
            FilllstAccounts();

            dateNextPayFee.DateValue = Convert.ToDateTime(m_paramOut.Value("Дата следующей уплаты вознаграждения"));
        }

        private void RunUbsChannelFunction(string functionName, UbsParam paramIn, UbsParam paramOut, bool needAddFields = false)
        {
            if (!needAddFields)
            {
                base.IUbsChannel.ParamsInParam = paramIn;

                base.IUbsChannel.Run(functionName);

                m_paramOut.ItemsVector = base.IUbsChannel.ParamsOut;
            }
            else
            {
                base.UbsChannel_ParamsIn = paramIn.ItemsVector;

                base.IUbsChannel.Run(functionName);

                m_paramOut.ItemsVector = base.UbsChannel_ParamsOut;
            }
        }

        private void SetBonusCtrlsBonusState()
        {
            if (m_idState == 4)
            {
                if (cmbTypePayFee.Text == PercentSumGuarant)
                {
                    cmbCurrencyPayFee.SelectedValue = cmbCurrencyGarant.SelectedValue;
                    cmbCurrencyPayFee.Enabled = false;
                }
                else
                {
                    cmbCurrencyPayFee.Enabled = true;
                }

                if (cmbTypePayFeeBonus.Text == PercentSumGuarant)
                {
                    cmbCurrencyRewardGuarant.SelectedValue = cmbCurrencyGarant.SelectedValue;
                    cmbCurrencyRewardGuarant.Enabled = false;
                }
                else
                {
                    cmbCurrencyRewardGuarant.Enabled = true;
                }
            }

            if (cmbOrderPayFeeBonus.Text == "Периодически")
            {
                if (Convert.ToInt32(cmbState.SelectedValue) == 4)
                {
                    dateNextPayFeeBonus.Enabled = true;
                }
                else
                {
                    dateNextPayFeeBonus.Enabled = false;
                }

                btnPeriodPayFeeBonus.Enabled = true;
            }
            else
            {
                btnPeriodPayFeeBonus.Enabled = false;
                dateNextPayFeeBonus.Enabled = false;
            }
        }

        private void SetComboValueText(ComboBox cmb, string value)
        {
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                if (((KeyValuePair<int, string>)cmb.Items[i]).Value == value)
                {
                    cmb.SelectedIndex = i;
                }
            }
        }

        private void FillComboText(ComboBox cmbControl, object[] arrData)
        {
            List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();
            for (int i = 0; i < arrData.GetLength(0); i++)
                list.Add(new KeyValuePair<int, string>(i, (string)arrData[i]));

            cmbControl.DataSource = list;
            cmbControl.ValueMember = "Key";
            cmbControl.DisplayMember = "Value";

            cmbControl.SelectedIndex = 0;
        }

        private void SetComboText(ComboBox cmb, int v)
        {
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                if ((int)cmb.SelectedValue == v)
                {
                    cmb.SelectedIndex = i;

                    return;
                }
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

                        if (cmbTypePayFee.SelectedText == PercentSumGuarant)
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

                        if (cmbTypePayFee.SelectedText == PercentSumGuarant)
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

                m_paramOut.Clear();
                m_paramOut.ItemsVector = base.UbsChannel_ParamsOut;
                lblUID.Visible = true;
                lblUID.Text = $"{lblUID.Text} {Convert.ToString(m_paramOut["UID"])}";

                if (m_paramOut.Contains("strError"))
                {
                    MessageBox.Show(Convert.ToString(m_paramOut["strError"]),
                        MsgContractGuarantee, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnExit_Click(this, EventArgs.Empty);
                    return;
                }

                if (Convert.ToInt32(m_paramOut["Доступ"]) == 0)
                {
                    m_isSecure = false;

                    MessageBox.Show(MsgNoEditAccess,
                        MsgContractGuarantee, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    btnSave.Enabled = false;
                }

                m_arrPeriodPay = m_paramOut["Срок погашения оплаченной суммы"] as object[,];

                m_isFixSum = (Convert.ToString(m_paramOut["Тип вознаграждения"]) == "Фиксированная сумма");

                if (Convert.ToInt32(m_paramOut["Доступ"]) == 0)
                {
                    if (m_paramOut.Contains("ОИ"))
                    {
                        var one = new List<KeyValuePair<int, string>>();
                        one.Add(new KeyValuePair<int, string>(
                            Convert.ToInt32(m_paramOut["ОИ"]),
                            Convert.ToString(m_paramOut["Наименование ОИ"])));

                        InitComboBox(cmbExecutor, one);

                        cmbExecutor.SelectedValue = Convert.ToInt32(m_paramOut["ОИ"]);
                    }
                }

                // Тексты
                txtModel.Text = Convert.ToString(m_paramOut["Типовой договор"]);
                txtAgent.Text = Convert.ToString(m_paramOut["Агент"]);

                txtNumAgent.Text = Convert.ToString(m_paramOut["Номер договора агента"]);
                dateAgent.Text = Convert.ToString(m_paramOut["Дата договора агента"]);

                txtBeneficiar.Text = Convert.ToString(m_paramOut["Бенефициар"]);
                txtGarant.Text = Convert.ToString(m_paramOut["Гарант"]);
                txtFrameContract.Text = Convert.ToString(m_paramOut["Наименование рамочного договора"]);

                cmbKindGarant.Text = Convert.ToString(m_paramOut["Вид гарантии"]);

                m_idModel = Convert.ToInt32(m_paramOut["Идентификатор типового договора"]);
                m_idAgent = Convert.ToInt32(m_paramOut["Идентификатор договора агента"]);
                m_idPrincipal = Convert.ToInt32(m_paramOut["Идентификатор клиента-принципала"]);
                m_idBeneficiar = Convert.ToInt32(m_paramOut["Идентификатор клиента-бенефициара"]);

                if (m_idBeneficiar == 0)
                    m_arrDetailsBeneficiar = m_paramOut["Реквизиты бенефициара"] as object[,];

                m_idDivision = Convert.ToInt32(m_paramOut["Номер отделения"]);
                m_idOI = Convert.ToInt32(m_paramOut["Идентификатор ОИ"]);
                m_idWarrant = Convert.ToInt32(m_paramOut["Идентификатор доверенности"]);

                m_idState = Convert.ToInt32(m_paramOut["Состояние"]);

                SetEnableCombos();

                m_idCurrency = Convert.ToInt32(m_paramOut["Идентификатор валюты"]);
                m_idCoverType = Convert.ToInt32(m_paramOut["Тип покрытия"]);

                m_idGarant = Convert.ToInt32(m_paramOut["Идентификатор клиента-гаранта"]);
                m_idFrameContract = Convert.ToInt32(m_paramOut["Идентификатор рамочного договора"]);

                // ===== Данные рамочного договора =====
                if (m_idFrameContract > 0)
                {
                    m_dateBeginFrameContract = Convert.ToDateTime(m_paramOut["Дата начала действия рамочного договора"]);
                    m_dateEndFrameContract = Convert.ToDateTime(m_paramOut["Дата окончания действия рамочного договора"]);
                    m_termsFrameContract = m_paramOut["Срок гарантии рамочного договора"] as object[,];
                    m_limitSaldoFrameContract = Convert.ToDecimal(m_paramOut["Остаток лимита рамочного договора"]);
                    m_issueEndDate = Convert.ToDateTime(m_paramOut["Дата окончания выдачи"]);
                    m_divisionFrameContract = Convert.ToInt32(m_paramOut["Отделение рамочного договора"]);

                    SetInfoByFrameContract();
                }
                else
                {
                    txtPrincipal.Text = Convert.ToString(m_paramOut["Принципал"]);
                }

                // ===== Агент: Reward/Cost блок =====
                if (m_idAgent > 0)
                {
                    dateReward.Enabled = true;
                    dateReward.Text = Convert.ToString(m_paramOut["Дата вознаграждения"]);

                    costAmount.Enabled = true;

                    costAmount.DecimalValue = Convert.ToDecimal(m_paramOut["Сумма затрат"]);

                    base.IUbsChannel.ParamIn("IdAgent", m_idAgent);
                    base.IUbsChannel.Run("AgentRewardAvaliable");

                    bool avaliable = Convert.ToBoolean(m_paramOut["Avaliable"]);
                    dateReward.Enabled = avaliable;
                    costAmount.Enabled = avaliable;

                    var pOut = base.IUbsChannel.ParamsOutParam;
                    dateAdjustment.DateValue = Convert.ToDateTime(pOut["Дата по корректировке"]);
                    paidAmount.DecimalValue = Convert.ToDecimal(pOut["Уплаченная сумма"]);
                    transAmount.DecimalValue = Convert.ToDecimal(pOut["Перечисленная сумма"]);
                }

                if (m_paramOut.Contains("Обеспечения"))
                {
                    m_arrGuarant = m_paramOut["Обеспечения"] as object[,];

                    FillLvwGuarant();
                }

                if (m_idModel == 0)
                    SetTabsEnabled(false, 2, 3);
                else
                    SetTabsEnabled(true, 2, 3);

                m_modelType = Convert.ToString(m_paramOut["Шаблон"]);

                if (m_modelType == UbsGuarantCommand)
                {
                    if (m_idState == 4)
                    {
                        linkGarant.Enabled = true;

                        if (cmbTypePayFee.Text == PercentSumGuarant)
                        {
                            cmbCurrencyPayFee.SelectedValue = Convert.ToInt32(cmbCurrencyGarant.SelectedValue);
                            cmbCurrencyPayFee.Enabled = false;
                        }

                        if (cmbTypePayFeeBonus.Text == PercentSumGuarant)
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
                    txtGarant.Text = string.Empty;
                }
                else
                {
                    linkGarant.Enabled = true;
                }

                // ===================== Вознаграждение (гарант) =====================
                m_orderPayFeeGuarant = Convert.ToString(m_paramOut["Порядок уплаты вознаграждения (гарант)"]);

                if (m_orderPayFeeGuarant != null && m_orderPayFeeGuarant != string.Empty)
                {
                    gbPayFeeGuarant.Visible = true;

                    m_typePayFeeGuarant = Convert.ToString(m_paramOut["Тип вознаграждения (гарант)"]);

                    if (m_orderPayFeeGuarant == "Периодически")
                    {
                        dateNextPayFeeGuarant.DateValue = Convert.ToDateTime(m_paramOut["Дата след. уплаты вознаграждения (гарант)"]);

                        m_arrIntervalGuarant = m_paramOut["Вознаграждение (гарант)"] as object[,];
                    }
                }
                else
                {
                    gbPayFeeGuarant.Visible = false;
                }

                // ===================== Портфель =====================
                if (m_paramOut.Contains("Идентификатор портфеля"))
                {
                    cmbPortfolio.SelectedValue = Convert.ToInt32(m_paramOut["Идентификатор портфеля"]);
                }

                // ===================== Тип оценки риска =====================
                if (m_paramOut.Contains("Тип оценки риска"))
                {
                    cmbTypeValidationRisk.SelectedValue = Convert.ToInt32(m_paramOut["Тип оценки риска"]);
                }

                // ===================== Группа риска =====================
                if (m_paramOut.Contains("Группа риска"))
                {
                    cmbQualityCategory.SelectedValue = Convert.ToInt32(m_paramOut["Группа риска"]);
                }
                // ===================== Группа риска =====================
                if (m_paramOut.Contains("Ставка резервирования"))
                {
                    ucdRateReservation.DecimalValue = Convert.ToDecimal(m_paramOut["Ставка резервирования"]);
                }

                // ===== Выбор комбо по ID через SelectedValue (аналог SetComboText) =====
                cmbNumberDiv.SelectedValue = m_idDivision;
                cmbExecutor.SelectedValue = m_idOI;
                cmbWarrant.SelectedValue = m_idWarrant;
                cmbTypeCover.SelectedValue = m_idCoverType;
                cmbState.SelectedValue = m_idState;
                cmbCurrencyGarant.SelectedValue = m_idCurrency;

                // Поля договора
                txtNumberGarant.Text = Convert.ToString(m_paramOut["Номер договора"]);
                dateOpenGarant.DateValue = Convert.ToDateTime(m_paramOut["Дата заключения"]);
                dateCloseGarant.DateValue = Convert.ToDateTime(m_paramOut["Дата закрытия"]);
                dateBeginGarant.DateValue = Convert.ToDateTime(m_paramOut["Дата начала действия"]);
                datePrincipal.DateValue = Convert.ToDateTime(m_paramOut["Дата возникн. обязательства Принципала"]);

                m_dateBegin = Convert.ToDateTime(m_paramOut["Дата начала действия"]);
                dateEndGarant.DateValue = Convert.ToDateTime(m_paramOut["Дата окончания действия"]);

                if (m_idState == 4)
                {
                    linkPreviousContract.Enabled = true;

                    if (cmbTypePayFee.Text == PercentSumGuarant)
                    {
                        cmbCurrencyPayFee.SelectedValue = cmbCurrencyGarant.SelectedValue;
                        cmbCurrencyPayFee.Enabled = false;
                    }
                    else
                    {
                        cmbCurrencyPayFee.Enabled = true;
                    }

                    if (cmbTypePayFeeBonus.Text == PercentSumGuarant)
                    {
                        cmbCurrencyRewardGuarant.SelectedValue = cmbCurrencyGarant.SelectedValue;
                        cmbCurrencyRewardGuarant.Enabled = false;
                    }
                    else
                    {
                        cmbCurrencyRewardGuarant.Enabled = true;
                    }
                }
                else
                {
                    linkPreviousContract.Enabled = false;
                }

                txtPreviousContract.Enabled = false;
                txtPreviousContract.Text = Convert.ToString(m_paramOut["InfoPrevContract"]);

                // ===== Доп.поля по валютам вознаграждения =====
                int idBonusValuta = Convert.ToInt32(m_paramOut["Идентификатор валюты вознаграждения"]);
                cmbCurrencyPayFee.SelectedValue = idBonusValuta;

                int idBonusValutaBonus = Convert.ToInt32(m_paramOut["Идентификатор валюты вознаграждения за выдачу"]);

                cmbCurrencyRewardGuarant.SelectedValue = idBonusValutaBonus;

                m_isInitCurrencyBonus = true;

                ucdSumGarant.DecimalValue = Convert.ToDecimal(m_paramOut["Сумма гарантии"]);

                m_idCoverType = Convert.ToInt32(m_paramOut["Тип покрытия"]);

                cmbTypeCover.SelectedValue = m_idCoverType;

                ucdSumCover.DecimalValue = Convert.ToDecimal(m_paramOut["Сумма покрытия"]);

                int idCoverValuta = Convert.ToInt32(m_paramOut["Идентификатор валюты покрытия"]);

                cmbCurrencyCover.SelectedValue = idCoverValuta;

                m_idContractCover = Convert.ToInt32(m_paramOut["Идентификатор договора покрытия"]);
                if (base.UbsChannel_ExistParamOut("Договор покрытия"))
                    txtContractCover.Text = Convert.ToString(m_paramOut["Договор покрытия"]);

                // ===== Списки порядка/типа вознаграждения =====
                object[] arrPayOrder = m_paramOut["Порядок уплаты вознаграждения (список)"] as object[];
                if (arrPayOrder != null)
                {
                    InitComboBox(cmbOrderPayFee, MakeKvpList(arrPayOrder));
                    cmbOrderPayFee.Text = Convert.ToString(m_paramOut["Порядок уплаты вознаграждения"]);

                    InitComboBox(cmbOrderPayFeeBonus, MakeKvpList(arrPayOrder));
                    cmbOrderPayFeeBonus.Text = Convert.ToString(m_paramOut["Порядок уплаты вознаграждения за выдачу"]);
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

                object[] arrTypeBonus = m_paramOut["Тип вознаграждения (список)"] as object[];
                if (arrTypeBonus != null)
                {
                    InitComboBox(cmbTypePayFee, MakeKvpList(arrTypeBonus));
                    cmbTypePayFee.Text = Convert.ToString(m_paramOut["Тип вознаграждения"]);

                    InitComboBox(cmbTypePayFeeBonus, MakeKvpList(arrTypeBonus));
                    cmbTypePayFeeBonus.Text = Convert.ToString(m_paramOut["Тип вознаграждения за выдачу"]);

                    if (cmbTypePayFee.Text == PercentSumGuarant)
                        cmbCurrencyPayFee.Enabled = false;
                    if (cmbTypePayFeeBonus.Text == PercentSumGuarant)
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

                if (m_idState == 4)
                {
                    if (cmbTypePayFee.Text == PercentSumGuarant)
                    {
                        cmbCurrencyPayFee.SelectedValue = cmbCurrencyGarant.SelectedValue;
                        cmbCurrencyPayFee.Enabled = false;
                    }
                    else
                    {
                        cmbCurrencyPayFee.Enabled = true;
                    }

                    if (cmbTypePayFeeBonus.Text == PercentSumGuarant)
                    {
                        cmbCurrencyRewardGuarant.SelectedValue = cmbCurrencyGarant.SelectedValue;
                        cmbCurrencyRewardGuarant.Enabled = false;
                    }
                    else
                    {
                        cmbCurrencyRewardGuarant.Enabled = true;
                    }
                }
            }
            else
            {
                dateCloseGarant.Text = string.Empty;
            }

            cmbCurrencyGarant.Enabled = CurrencyGarantEnabled();

            SetBonusCtrlsState();

            m_arrRateValues = null;
            InitTrvRates(true);

            if (txtModel.Text.Length > 0)
            {
                linkModel.Enabled = false;
            }

            UpdateCover();

            m_arrAccounts = m_paramOut.Value("Счета") as object[,];
            FilllstAccounts();

            m_axUbsControlProperty.UbsAddFields = this.ubsCtrlFields;
            m_axUbsControlProperty.Refresh();

            if (m_idState != 2 && m_idState != 4)
            {
                datePrincipal.Enabled = false;
            }

            //'IdState = 4 - подготовлен
            if (m_idState != 4 && m_command != CopyCommand)
            {
                EnabledCmdControl(false);

                txtModel.Enabled = false;
                txtPrincipal.Enabled = false;
                txtBeneficiar.Enabled = false;
                txtGarant.Enabled = false;

                txtNumberGarant.Enabled = false;
                dateBeginGarant.Enabled = false;
                dateEndGarant.Enabled = true;
                dateOpenGarant.Enabled = false;
                dateCloseGarant.Enabled = false;
                ucdSumGarant.Enabled = false;

                cmbCurrencyGarant.Enabled = CurrencyGarantEnabled();

                cmbTypeCover.Enabled = false;
                cmbCurrencyCover.Enabled = false;
                txtContractCover.Enabled = false;
                ucdSumCover.Enabled = false;

                cmbExecutor.Enabled = true;
                cmbState.Enabled = false;
                cmbNumberDiv.Enabled = false;


                btnAddRate.Enabled = false;
                btnDelRate.Enabled = false;
                btnEditRate.Enabled = false;


                linkFrameContract.Enabled = false;
                btnFrameContractDel.Visible = false;


                linkAgent.Enabled = false;
                btnAgentDel.Visible = false;

                dateEndGarant.Enabled = false;

                if (m_idState == 1)
                {
                    btnSave.Enabled = false;
                    btnReRead.Enabled = false;
                }
            }
            cmbCurrencyGarant.Enabled = CurrencyGarantEnabled();

            CorrectFormDateCtrls();
        }

        /// <summary>
        /// Заполняет список счетов (lvwAccounts) из m_arrAccounts через BGReadAccountsByContract.
        /// VB: FilllstAccounts.
        /// </summary>
        private void FilllstAccounts()
        {
            try
            {
                lvwAccounts.Items.Clear();

                if (m_arrAccounts == null || m_arrAccounts.GetLength(0) == 0)
                {
                    MessageBox.Show(MsgAccountsListEmpty, MsgContractGuaranteeCaption,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                base.UbsChannel_ParamIn("Счета", m_arrAccounts);
                base.UbsChannel_Run("BGReadAccountsByContract");

                string strError = Convert.ToString(base.UbsChannel_ParamOut("StrError") ?? string.Empty);
                if (!string.IsNullOrEmpty(strError))
                {
                    MessageBox.Show(strError, MsgContractGuaranteeCaption,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    EnabledCmdControl(true);
                    return;
                }

                object[,] accounts = base.UbsChannel_ParamOut("Счета") as object[,];
                if (accounts != null)
                {
                    m_arrAccounts = accounts;
                    int rows = accounts.GetLength(0);
                    for (int i = 0; i < rows; i++)
                    {
                        string key = Convert.ToString(accounts[i, 0]);
                        var item = lvwAccounts.Items.Add(key, Convert.ToString(accounts[i, 0]), 0);
                        item.SubItems.Add(Convert.ToString(accounts[i, 1]));
                        item.SubItems.Add(Convert.ToString(accounts[i, 2]));
                        item.SubItems.Add(Convert.ToString(accounts[i, 3]));
                    }
                }
            }
            catch (Exception ex)
            {
                base.Ubs_ShowError(ex);
            }
        }

        private void UpdateCover()
        {
            if (m_idCoverType > 0)
            {
                ucdSumGarant.Enabled = true;
                cmbCurrencyCover.Enabled = true;
            }
            else
            {
                ucdSumGarant.Enabled = false;
                cmbCurrencyCover.Enabled = false;
            }
        }

        private void CorrectFormDateCtrls()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is UbsCtrlDate dateCtrl)
                {
                    DateTime dateValue = dateCtrl.DateValue;

                    if (dateValue >= MaxDate || dateValue <= MinDate)
                    {
                        dateCtrl.Text = string.Empty;
                    }
                }
            }
        }

        private void SetBonusCtrlsState()
        {
            if (m_idState == 4)
            {
                if (cmbTypePayFee.Text == PercentSumGuarant)
                {
                    cmbCurrencyPayFee.SelectedValue = cmbCurrencyGarant.SelectedValue;
                    cmbCurrencyPayFee.Enabled = false;
                }
                else
                {
                    cmbCurrencyPayFee.Enabled = true;
                }

                if (cmbTypePayFeeBonus.Text == PercentSumGuarant)
                {
                    cmbCurrencyRewardGuarant.SelectedValue = cmbCurrencyGarant.SelectedValue;
                    cmbCurrencyRewardGuarant.Enabled = false;
                }
                else
                {
                    cmbCurrencyRewardGuarant.Enabled = true;
                }
            }

            if (m_idState == 4)
            {
                if (Convert.ToInt32(cmbState.SelectedValue) == 4)
                {
                    dateNextPayFee.Enabled = true;
                }
                else
                {
                    dateNextPayFee.Enabled = false;
                }

                btnPeriodPayFee.Enabled = true;
            }
            else
            {
                btnPeriodPayFee.Enabled = false;
                dateNextPayFee.Enabled = false;
            }
        }

        private bool CurrencyGarantEnabled() => (m_idState == 4 && m_idFrameContract == 0);

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
                if (cmbTypePayFee.Text == PercentSumGuarant)
                {
                    cmbCurrencyPayFee.SelectedValue = Convert.ToInt32(cmbCurrencyGarant.SelectedValue);
                    cmbCurrencyPayFee.Enabled = false;
                }
                else
                {
                    cmbCurrencyPayFee.Enabled = true;
                }

                if (cmbTypePayFeeBonus.Text == PercentSumGuarant)
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

                m_paramOut.Clear();
                m_paramOut.Clear();

                base.IUbsChannel.ParamIn("ID", m_idFrameContract);

                RunUbsChannelFunction("BGReadFrameContractById", m_paramIn, m_paramOut);

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
                m_paramIn.Clear();
                m_paramOut.Clear();

                m_paramIn.Value("DateBegin", dateOpenGarant.DateValue);
                m_paramIn.Value("IdAgent", m_idAgent);

                RunUbsChannelFunction("GetDatePayment", m_paramIn, m_paramOut);

                dateReward.DateValue = Convert.ToDateTime(base.IUbsChannel.ParamOut("DatePayment"));

                if (m_command == EditCommand)
                {
                    RunUbsChannelFunction("AgentRewardAvaliable", m_paramIn, m_paramOut);

                    dateReward.Enabled =
                        costAmount.Enabled =
                        Convert.ToBoolean(base.IUbsChannel.ParamOut("Avaliable"));
                }
                else
                {
                    dateReward.Enabled = true;
                    costAmount.Enabled = true;
                }

                m_paramOut.Clear();
                m_paramIn.Clear();

                m_paramIn.Value("ID", m_idAgent);//ЗДЕСЬ ЛЕЖИТ ДОГОВОР С АГЕНТОМ - ID_AG_CONTRACT

                RunUbsChannelFunction("BGReadAgContr", m_paramIn, m_paramOut);

                var idAgClient = Convert.ToInt32(base.IUbsChannel.ParamOut("Ид клиента"));
                var numAg = Convert.ToString(base.IUbsChannel.ParamOut("Номер договора агента"));
                var dateAg = Convert.ToDateTime(base.IUbsChannel.ParamOut("Дата договора агента"));

                txtNumAgent.Text = numAg;
                dateAgent.DateValue = dateAg;

                m_paramIn.Value("ID", idAgClient);

                RunUbsChannelFunction("BGReadClientById", m_paramIn, m_paramOut);

                txtAgent.Text = Convert.ToString(m_paramOut.Value("Наименование"));
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

                m_paramIn.Clear();
                m_paramOut.Clear();

                m_paramIn.Value("ID", m_idPrincipal);

                RunUbsChannelFunction("BGReadClientById", m_paramIn, m_paramOut);

                txtPrincipal.Text = Convert.ToString(m_paramOut.Value("Наименование"));

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

                m_paramIn.Clear();
                m_paramOut.Clear();

                m_paramIn.Value("ID", m_idBeneficiar);

                RunUbsChannelFunction("BGReadClientById", m_paramIn, m_paramOut);

                txtBeneficiar.Text = Convert.ToString(m_paramOut.Value("Наименование"));

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

                m_paramIn.Clear();
                m_paramOut.Clear();

                m_paramIn.Value("ID", m_idGarant);

                RunUbsChannelFunction("BGReadClientById", m_paramIn, m_paramOut);

                txtGarant.Text = Convert.ToString(m_paramOut.Value("Наименование"));

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

                m_paramOut.Clear();
                m_paramIn.Clear();

                m_paramIn.Value("IdPrevContract", m_idPrevContract);

                RunUbsChannelFunction("BGReadPreviuosContract", m_paramIn, m_paramOut);

                txtPreviousContract.Text = Convert.ToString(m_paramOut.Value("InfoPrevContract"));

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

        private void btnManualBenificiar_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new UbsBgDetailsBenificiarFrm();

                if (m_arrCountry == null)
                {
                    m_paramIn.Clear();
                    m_paramOut.Clear();

                    m_paramIn.Value("Тип данных", "Страны");

                    RunUbsChannelFunction("GetAddressData", m_paramIn, m_paramOut);

                    m_arrCountry = m_paramOut.Value("Данные") as object[,];

                    if (m_arrCountry != null)
                    {
                        for (int i = 0; i < m_arrCountry.GetLength(0); i++)
                        {
                            form.CbCodeCountry.Items.Add(m_arrCountry[i, 0]);
                            form.CbCountry.Items.Add(m_arrCountry[i, 1]);
                        }

                        form.arrCountry = m_arrCountry;
                        form.CodeCountryValue = "643";
                    }
                }
                else
                {
                    form.arrCountry = m_arrCountry;

                    if (form.CbCodeCountry.Items.Count == 0)
                    {
                        for (int i = 0; i < m_arrCountry.GetLength(0); i++)
                        {
                            form.CbCodeCountry.Items.Add(m_arrCountry[i, 0]);
                            form.CbCountry.Items.Add(m_arrCountry[i, 1]);
                        }
                    }
                    form.CodeCountryValue = "643";
                }

                if (m_arrTypeObject == null)
                {
                    m_paramIn.Clear();
                    m_paramOut.Clear();

                    m_paramIn.Value("Тип данных", "Типы объектов");
                    m_paramIn.Value("Код страны", "643");
                    m_paramIn.Value("Условия получения данных", m_paramIn.Items);

                    RunUbsChannelFunction("GetAddressData", m_paramIn, m_paramOut);

                    m_arrTypeObject = m_paramOut.Value("Данные") as object[,];
                    form.arrTypeObject = m_arrTypeObject;
                }
                else
                {
                    form.arrTypeObject = m_arrTypeObject;
                }

                if (m_arrTypeObject != null && form.CodeCountryValue == "643" &&
                    form.CbTypeRegion.Items.Count == 0 && form.CbTypeArea.Items.Count == 0 &&
                    form.CbTypeCity.Items.Count == 0 && form.CbTypeSettl.Items.Count == 0 &&
                    form.CbTypeStreet.Items.Count == 0 && form.CbTypeHome.Items.Count == 0 &&
                    form.CbTypeHousing.Items.Count == 0 && form.CbTypeFlat.Items.Count == 0)
                {
                    for (int i = 0; i < m_arrTypeObject.GetLength(0); i++)
                    {
                        int typeId = Convert.ToInt32(m_arrTypeObject[i, 0]);
                        string typeName = Convert.ToString(m_arrTypeObject[i, 1]);

                        switch (typeId)
                        {
                            case 1:
                                form.CbTypeRegion.Items.Add(typeName);
                                break;
                            case 2:
                                form.CbTypeArea.Items.Add(typeName);
                                break;
                            case 3:
                                form.CbTypeCity.Items.Add(typeName);
                                break;
                            case 4:
                                form.CbTypeSettl.Items.Add(typeName);
                                break;
                            case 5:
                                form.CbTypeStreet.Items.Add(typeName);
                                break;
                            case 6:
                                form.CbTypeHome.Items.Add(typeName);
                                break;
                            case 7:
                                form.CbTypeHousing.Items.Add(typeName);
                                break;
                            case 8:
                                form.CbTypeFlat.Items.Add(typeName);
                                break;
                        }
                    }
                }

                if (m_arrDetailsBeneficiar != null && m_arrDetailsBeneficiar.GetLength(0) > 0)
                {
                    form.NameValue = Convert.ToString(m_arrDetailsBeneficiar[0, 0]);
                    form.INNValue = Convert.ToString(m_arrDetailsBeneficiar[0, 1]);

                    object[,] arrAddress = m_arrDetailsBeneficiar[0, 2] as object[,];
                    if (arrAddress != null && arrAddress.GetLength(0) > 0)
                    {
                        form.IndexValue = Convert.ToString(arrAddress[0, 0]);
                        form.CodeCountryValue = Convert.ToString(arrAddress[0, 1]);
                        form.TypeRegionValue = Convert.ToString(arrAddress[0, 2]);
                        form.RegionValue = Convert.ToString(arrAddress[0, 3]);
                        form.TypeAreaValue = Convert.ToString(arrAddress[0, 4]);
                        form.AreaValue = Convert.ToString(arrAddress[0, 5]);
                        form.TypeCityValue = Convert.ToString(arrAddress[0, 6]);
                        form.CityValue = Convert.ToString(arrAddress[0, 7]);
                        form.TypeSettlValue = Convert.ToString(arrAddress[0, 8]);
                        form.SettlValue = Convert.ToString(arrAddress[0, 9]);
                        form.TypeStreetValue = Convert.ToString(arrAddress[0, 10]);
                        form.StreetValue = Convert.ToString(arrAddress[0, 11]);
                        form.TypeHomeValue = Convert.ToString(arrAddress[0, 12]);
                        form.HomeValue = Convert.ToString(arrAddress[0, 13]);
                        form.TypeHousingValue = Convert.ToString(arrAddress[0, 14]);
                        form.HousingValue = Convert.ToString(arrAddress[0, 15]);
                        form.TypeFlatValue = Convert.ToString(arrAddress[0, 16]);
                        form.FlatValue = Convert.ToString(arrAddress[0, 17]);
                    }
                }
                else
                {
                    form.NameValue = string.Empty;
                    form.INNValue = string.Empty;
                    form.IndexValue = string.Empty;
                    form.CodeCountryValue = "643";
                    form.TypeRegionValue = string.Empty;
                    form.RegionValue = string.Empty;
                    form.TypeAreaValue = string.Empty;
                    form.AreaValue = string.Empty;
                    form.TypeCityValue = string.Empty;
                    form.CityValue = string.Empty;
                    form.TypeSettlValue = string.Empty;
                    form.SettlValue = string.Empty;
                    form.TypeStreetValue = string.Empty;
                    form.StreetValue = string.Empty;
                    form.TypeHomeValue = string.Empty;
                    form.HomeValue = string.Empty;
                    form.TypeHousingValue = string.Empty;
                    form.HousingValue = string.Empty;
                    form.TypeFlatValue = string.Empty;
                    form.FlatValue = string.Empty;
                }

                form.ShowDialog(this);

                if (form.ApplyClicked)
                {
                    m_idBeneficiar = 0;

                    txtBeneficiar.Text = form.NameValue;

                    var arrDetails = new object[1, 3];
                    arrDetails[0, 0] = form.NameValue;
                    arrDetails[0, 1] = form.INNValue;

                    var arrAddress = new object[1, 18];
                    arrAddress[0, 0] = form.IndexValue;
                    arrAddress[0, 1] = form.CodeCountryValue;
                    arrAddress[0, 2] = form.TypeRegionValue;
                    arrAddress[0, 3] = form.RegionValue;
                    arrAddress[0, 4] = form.TypeAreaValue;
                    arrAddress[0, 5] = form.AreaValue;
                    arrAddress[0, 6] = form.TypeCityValue;
                    arrAddress[0, 7] = form.CityValue;
                    arrAddress[0, 8] = form.TypeSettlValue;
                    arrAddress[0, 9] = form.SettlValue;
                    arrAddress[0, 10] = form.TypeStreetValue;
                    arrAddress[0, 11] = form.StreetValue;
                    arrAddress[0, 12] = form.TypeHomeValue;
                    arrAddress[0, 13] = form.HomeValue;
                    arrAddress[0, 14] = form.TypeHousingValue;
                    arrAddress[0, 15] = form.HousingValue;
                    arrAddress[0, 16] = form.TypeFlatValue;
                    arrAddress[0, 17] = form.FlatValue;

                    arrDetails[0, 2] = arrAddress;
                    m_arrDetailsBeneficiar = arrDetails;
                }

                form.Dispose();
            }
            catch (Exception ex)
            {
                base.Ubs_ShowError(ex);
            }
        }

        /// <summary>
        /// Инициализация дерева ставок (trvRates). Заполняет узлы по m_arrRates и m_arrRateValues,
        /// при in_Refresh подтягивает значения через BG_GetRateByRateType при отсутствии локальных.
        /// </summary>
        /// <param name="inRefresh">При true — запрашивать значение по типу ставки из канала, если в m_arrRateValues ещё нет.</param>
        private void InitTrvRates(bool inRefresh = false)
        {
            try
            {
                if (trvRates.Nodes.Count > 0)
                    trvRates.Nodes.Clear();

                if (m_arrRates == null || m_arrRates.GetLength(0) == 0)
                {
                    MessageBox.Show(MsgRateTypesArrayEmpty, m_captionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                int rateCount = m_arrRates.GetLength(0);
                if (m_arrRateValues == null || m_arrRateValues.Length != rateCount)
                    m_arrRateValues = new object[rateCount];

                for (int i = 0; i < rateCount; i++)
                {
                    string rateType = Convert.ToString(m_arrRates[i]);
                    string key = "RateType: " + rateType;
                    TreeNode parentNode = trvRates.Nodes.Add(key, rateType);

                    object[,] arrRateTemp = null;
                    if (!string.IsNullOrEmpty(rateType) && inRefresh)
                    {
                        if (m_arrRateValues[i] is object[,] existing)
                            arrRateTemp = existing;
                        else
                        {
                            base.UbsChannel_ParamIn("RateType", StrRatePrefix + rateType);
                            base.UbsChannel_Run("BG_GetRateByRateType");
                            object valueRate = base.UbsChannel_ParamOut("ValueRate");
                            if (valueRate is object[,] fromChannel)
                            {
                                arrRateTemp = fromChannel;
                                m_arrRateValues[i] = fromChannel;
                            }
                        }
                    }
                    else
                        arrRateTemp = m_arrRateValues[i] as object[,];

                    if (arrRateTemp != null)
                    {
                        int rows = arrRateTemp.GetLength(0);
                        for (int j = 0; j < rows; j++)
                        {
                            string strKeyChild = key + " Date: " + Convert.ToString(arrRateTemp[j, 0]);
                            decimal val = Convert.ToDecimal(arrRateTemp[j, 1]);
                            string tstr = Convert.ToString(arrRateTemp[j, 0]) + " - " + val.ToString("N2");
                            parentNode.Nodes.Add(strKeyChild, tstr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                base.Ubs_ShowError(ex);
            }
        }

        private void btnAddRate_Click(object sender, EventArgs e)
        {
            GetRate(AddCommand);
        }

        private void GetRate(string addCommand)
        {
            throw new NotImplementedException();
        }
    }
}