using System;

namespace UbsBusiness
{
    public partial class UbsPsUtPaymentFrm
    {
        public readonly DateTime MinDateValue = new DateTime(2222, 1, 1);
        #region Resource

        private const string LoadResource = @"VBS:UBS_VBD\PS\UT\UtPaymentF.vbs";

        #endregion

        #region StrCommand

        private const string StrCommandAdd = "ADD";
        private const string StrCommandRead = "READ";
        private const string StrCommandClear = "CLEAR";
        private const string StrCommandView = "VIEW";
        private const string StrCommandCopy = "COPY";
        private const string StrCommandAddFromClient = "ADD_FROM_CLIENT";
        private const string StrCommandAddParam = "ADD_PARAM";
        private const string StrCommandGroupAdd = "GROUP_ADD";
        private const string StrCommandGroupProceed = "GROUP_PROCEED";
        private const string StrCommandGroupView = "GROUP_VIEW";
        private const string StrCommandGroupChange = "GROUP_CHANGE";
        private const string StrCommandChangePart = "CHANGE_PART";
        private const string StrCommandChangePartIncoming = "CHANGE_PART_INCOMING";
        private const string StrCommandAddIncoming = "ADD_INCOMING";
        private const string StrCommandChangeContract = "CHANGECONTRACT";

        #endregion

        #region Caption

        private const string CaptionForm = "Платеж";
        private const string CaptionError = "Ошибка";
        private const string CaptionValidation = "Проверка корректности данных";
        private const string CaptionCheck = "Проверка";
        private const string CaptionCashSymbolControl = "Контроль кассовых символов";
        private const string CaptionClientCheck = "Проверка клиента";
        private const string CaptionGroupInputPrefix = "Ввод группы платежей ID = ";
        private const string CaptionPaymentAccept = "Приём платежа";
        private const string CaptionInitForm = "Инициализация формы";
        private const string CaptionBlockCheck = "Проверка блокировки";

        #endregion

        #region Message

        private const string MsgCalcDoneInfo = "Расчет с клиентом проведен";
        private const string MsgCashSymbolTypeEmpty = "Не заполнено доп поле 'Разбивка платежа по символам'!";
        private const string MsgPaymentAmountEmpty = "Не заполнена сумма платежа!";
        private const string MsgGettingDataContract = "Ошибка получения данных контракта";
        private const string MsgPaymentSavedDb = "Платеж сохранен в БД";
        private const string MsgRecipientAttributesSaved = "Реквизиты получателя сохранены в справочнике";
        private const string MsgRecipientContractRequired = "Не выбран договор с получателем!";
        private const string MsgInvalidSettlementAccount = "Некорректный расчетный счет!";
        private const string MsgInvalidAccountKey = "Некорректный ключ счета!";
        private const string MsgInvalidPaymentAmount = "Некорректная сумма платежа!";
        private const string MsgInvalidTotalAmount = "Некорректная общая сумма платежа!";
        private const string MsgGroupContinue = "Вы хотите продолжить ввод платежей в группу?";
        private const string MsgContractClosedWarning = "Внимание!!! Договор закрыт.";
        private const string MsgIpdlSaveForbidden = "Проверка ИПДЛ, сохранение запрещено!";
        private const string MsgPaymentListEmpty = "Список платежей пуст.";
        private const string MsgNotBankClient = "Не Клиент банка";
        private const string MsgPaymentNotSelected = "Не выбран платеж для просмотра";
        private const string MsgPaymentNotSelectedForAdd = "Не выбран платеж для добавления";
        private const string MsgPaymentCancelled = "Данный платеж анулирован";
        private const string MsgNotCashier = "Текущий пользователь не является кассиром !!!";
        private const string MsgPaymentBlocked = "Платеж заблокирован, редактирование запрещено";
        private const string MsgDocumentsExistViewOnly = "По платежу сформированы документы. Форма открыта в режиме просмотра";
        private const string MsgInnPayerCheckFailed = "Проверка контрольного числа в ИНН плательщика <{0}> не пройдена!";
        private const string MsgInnRecipientCheckFailed = "Проверка контрольного числа в ИНН получателя <{0}> не пройдена!";
        private const string MsgThirdPersonInnRequired = "Поле 'ИНН' третьего лица должно быть заполнено!";
        private const string MsgThirdPersonNameRequired = "Не заполнено поле 'Наименование третьего лица'!";
        private const string MsgThirdPersonInn12Required = "Некорректно заполнено поле 'ИНН' (вкладка 'Сведения о третьем лице'): ИНН должен состоять из 12 символов!";
        private const string MsgThirdPersonInn12Or0Required = "Некорректно заполнено поле 'ИНН' (вкладка 'Сведения о третьем лице'): ИНН должен состоять из 12 символов или ИНН должен быть равен 0!";
        private const string MsgThirdPersonInn10Required = "Некорректно заполнено поле 'ИНН' (вкладка 'Сведения о третьем лице'): ИНН юр. лица должен состоять из 10 символов!";
        private const string MsgThirdPersonKpp9Required = "Некорректно заполнено поле 'КПП' (вкладка 'Сведения о третьем лице'): КПП должен состоять из 9 символов!";
        private const string MsgBatchNumberRequired = "Поле 'Номер пачки' должно быть заполнено!";
        private const string MsgTaxInnPayerRequired = "Для налоговых платежей обязателен ИНН плательщика!";
        private const string MsgTaxInnRecipientRequired = "Для налоговых платежей обязательно заполнение поля ИНН!!!";
        private const string MsgPurposeEmpty = "Не заполнено назначение платежа!!!";
        private const string MsgPurposeTooLong = "Назначение платежа не может содержать более 210 символов!";
        private const string MsgRecipientNameTooLong = "Наименование получателя не может содержать более 160 символов!";
        private const string MsgPayerAccountEmpty = "Лицевой счет плательщика не может быть пустым!";
        private const string MsgPayerCheckSumEmpty = "Ключ л/с плательщика не может быть пустым!";
        private const string MsgSumControlExceeded = "Сумма платежа превышает допустимую, требуется дополнительный контроль";
        private const string MsgFrDisconnected = "Разрыв связи с фискальным регистратором";
        private const string MsgGroupSumExceeds = "Сумма принятых платежей {0} больше суммы группового платежа {1}. Выход?";
        private const string MsgGroupSumUnder = "Сумма принятых платежей {0} меньше суммы группового платежа {1}. Продолжить ввод?";
        private const string MsgGroupCountSummary = "Введено {0} платежей на общую сумму {1} руб.\r\nВы хотите продолжить ввод платежей в группу?";
        private const string MsgInvalidCurrencyCode = "Введён недопустимый код валюты!";
        private const string MsgCheckAccountBadLength = "Некорректная длина лицевого счета плательщика! ({0})";
        private const string MsgCheckKeyError = "Ошибка в ключе! Правильный ключ '{0}'.";
        private const string MsgCheckKeySaveWrongKey = "Ошибка в ключе! Правильный ключ '{0}'. Сохранить ошибочный ключ?";
        private const string MsgGroupInputCaption = "Ввод группы платежей";
        private const string MsgReturnToEdit = "Вернуться к редактированию?";
        private const string CaptionPrintDocPrep = "Подготовка внешних документов";
        private const string CaptionFrPrint = "Печать на ФР. ";
        private const string CaptionCashOrderCreate = "Создание кассовых ордеров";
        private const string CaptionCheckSumPayment = "Проверка суммы платежа";
        private const string SidAction = "PS";
        private const string MsgDateError = "Ошибка в дате!";

        #endregion

        #region UiText

        private const string TabTextGeneral = "Платеж";
        private const string TabTextThirdPerson = "Плательщик и третье лицо";
        private const string TabTextTariff = "Тариф";
        private const string TabTextTelephone = "Телефонный платеж";
        private const string TabTextTax = "Налог";
        private const string TabTextAddFields = "Дополнительные поля";
        private const string GroupTextPayer = "Плательщик";
        private const string GroupTextRecipient = "Получатель";

        #endregion

        #region Pattern IDs

        private const string PatternEnergy = "UBS_UT_ENERGY";
        private const string PatternPhone = "UBS_UT_PHONE";
        private const string PatternNalog = "UBS_UT_NALOG";
        private const string PatternPhoneAcc = "UBS_UT_PHONE_ACC";

        #endregion

        #region Support

        private const string AddFieldsSupportKey = "Доп. поля";

        #endregion
    }
}
