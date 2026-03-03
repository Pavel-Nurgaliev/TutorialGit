using System;

namespace UbsBusiness
{
    partial class UbsBgContractFrm
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
        private const string MsgRateDateAlreadyExists = "Ставка на эту дату уже существует!";
        private const string CaptionAddRate = "Добавить ставку";
        private const string CaptionEditRate = "Изменить ставку";
        private const string UbsGuarantCommand = "UBS_GUARANT";
        private const string PercentSumGuarant = "Процент суммы гарантии";

        /// <summary>Префикс типа ставки для канала (процентные ставки).</summary>
        private const string StrRatePrefix = "[Процентные ставки].";

        private const string AddCommand = "ADD";
        private const string EditCommand = "EDIT";
        private const string CopyCommand = "COPY";
        private const string PrepareCommand = "PREPARE";

        private const string OrderPayPeriodically = "Периодически";

        private const string AddByFrameContrantCommand = "ADD_BY_FRAME_CONTRACT";
        private const string AddByFramePrepareCommand = "ADD_BY_FRAME_PREPARE";

        private const string UbsCounterGuarantTypeCommand = "UBS_COUNTER_GUARANT";

        private const string ActionUbsBgFrameContractList = "UBS_BG_FRAME_CONTRACT_LIST";
        private const string ActionUbsBgListModel = "UBS_BG_LIST_MODEL";
        private const string ActionUbsBgListAgent = "UBS_BG_LIST_AGENT";
        private const string ActionUbsCommonListClient = "UBS_COMMON_LIST_CLIENT";
        private const string ActionUbsBgListContract = "UBS_BG_LIST_CONTRACT";

        private readonly DateTime MinDate = new DateTime(1990, 1, 1);
        private readonly DateTime MaxDate = new DateTime(2222, 1, 1);

        #endregion
    }
}
