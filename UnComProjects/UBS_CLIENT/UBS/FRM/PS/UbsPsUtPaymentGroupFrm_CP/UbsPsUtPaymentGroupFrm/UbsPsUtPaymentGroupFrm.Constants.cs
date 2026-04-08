namespace UbsBusiness
{
    public partial class UbsPsUtPaymentGroupFrm
    {
        #region Resource

        private const string LoadResource = @"VBS:UBS_VBD\PS\UT\UtPaymentF.vbs";

        #endregion

        #region StrCommand

        private const string StrCommandAdd = "ADD";
        private const string StrCommandGroupEdit = "GROUP_EDIT";
        private const string StrCommandGroupAdd = "GROUP_ADD";
        private const string StrCommandRead = "READ";
        private const string StrCommandClear = "CLEAR";
        private const string StrCommandChangeContract = "CHANGECONTRACT";
        private const string StrCommandChangePart = "CHANGE_PART";

        #endregion

        #region Captions

        private const string CaptionForm = "Платеж";
        private const string CaptionError = "Ошибка";
        private const string CaptionInitCheck = "Проверка";
        private const string CaptionValidation = "Проверка корректности данных";
        private const string CaptionGroupInput = "Ввод группы платежей";
        private const string CaptionPaymentSpelling = "Платёж";
        private const string CaptionCheckAddFieldsPrefix = "Проверка доп. полей. ";

        #endregion

        #region Messages

        private const string MsgIpdlSaveForbidden = "Проверка ИПДЛ, сохранение запрещено!";
        private const string MsgPaymentSavedDb = "Платеж сохранен в БД";
        private const string MsgRecipientRequisitesMissing = "Реквизиты получателя не заданы!";
        private const string MsgRecipientRequisitesSaved = "Реквизиты получателя сохранены в справочнике";
        private const string MsgGroupPaymentListEmpty = "Список групповых платежей пуст!";
        private const string MsgGroupContinue = "Вы хотите продолжить ввод платежей в группу?";
        private const string MsgNoRecipientContract = "Не выбран договор с получателем!";
        private const string MsgPayerFioRequired = "Введите ФИО плательщика.";
        private const string MsgInvalidPaymentAmount = "Некорректная сумма платежа!";
        private const string MsgInvalidTotalAmount = "Некорректная общая сумма платежа!";
        private const string MsgBikLimitContinueQuestionSuffix = "Продолжить ввод платежа?";
        private const string MsgInvalidSettlementAccount = "Некорректный расчетный счет!";
        private const string MsgInvalidAccountKey = "Некорректный ключ счета!";
        private const string MsgTerrorChooseClientFromDirectory = "Клиента подозреваемого в террористической деятельности надо выбрать из справочника!!!";
        private const string MsgContractClosedWarning = "Внимание!!! Договор закрыт.";

        #endregion

        #region Script / COM

        private const string ScriptGroupContinuation = @"UBS_VBS\PS\PsPaymentIncomingReception.vbs";
        private const string ProgIdUbsRunScript = "URunScr.IUbsRunScript";

        #endregion

        #region Ut_CheckBeforeSave / script parameter keys

        private const string ParamKeyIdPayment = "IDPAYMENT";
        private const string ParamKeyPaymentGroupId = "Идентификатор группового платежа";
        private const string ParamKeyMainPaymentId = "Идентификатор основного платежа";
        private const string ParamKeyParent = "Parent";
        private const string ParamKeyScriptKey = "Key";
        private const string ParamKeyEndGroup = "EndGroup";

        #endregion

        #region ReadBankBIK matrix tags

        private const string BankBikTagBankName = "BANKNAME";
        private const string BankBikTagCorrAcc = "CORRACC";

        #endregion

        #region Account placeholder

        private const string AccountPlaceholder = "00000000000000000000";

        #endregion

        #region UbsCtrlFields support key

        private const string AddFieldsSupportKey = "Доп. поля";

        #endregion
    }
}
