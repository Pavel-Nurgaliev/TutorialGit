namespace UbsBusiness
{
    public partial class UbsPsUtPaymentFrm
    {
        #region Resource

        private const string LoadResource = @"VBS:UBS_VBD\PS\UT\UtPaymentF.vbs";
        private const string LoadResourcePrintForm = @"VBS:UBS_VBS\PS\PsCheckPrintForm.vbs";

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

        #endregion

        #region Message

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

        #region Support

        private const string AddFieldsSupportKey = "Доп. поля";

        #endregion
    }
}
