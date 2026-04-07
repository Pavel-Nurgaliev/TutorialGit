namespace UbsBusiness
{
    public partial class UbsPsContractFrm
    {
        #region FLT

        private const string UBS_PS_UT_LIST_KIND_PAYMENT = @"UBS_PS_UT_LIST_KIND_PAYMENT";

        #endregion

        #region Resource

        private const string LoadResource = @"VBS:UBS_VBD\PS\Contract.vbs";

        #endregion

        #region commands

        private const string EditCommand = "EDIT";
        private const string DelCommand = "DEL";

        #endregion

        #region Commission UI (legacy Contract.dob EnableSum — ListIndex rules)

        private const int CommissionComboIndexDisablePercentFirst = 0;
        private const int CommissionComboIndexDisablePercentSecond = 3;

        #endregion

        #region UbsCtrlFields support key

        private const string AddFieldsSupportKey = "Доп. поля";

        #endregion

        #region Account / BIK placeholders

        private const string CorrespondentAccountPlaceholder = "00000000000000000000";

        #endregion

        #region Shell list actions (Ubs_ActionRun — legacy GetWindow names)

        private const string ActionListCommonClient = "UBS_COMMON_LIST_CLIENT";
        private const string ActionListOdAccount0 = "UBS_OD_LIST_ACCOUNT0";
        private const string FilterKindPaymentListFallback = "UBS_PS_LIST_KINDPAYM_ALL";

        #endregion

        #region Messages
        private const string InnIsEmptyErrorMessage = "ИНН не заполнен";
        private const string InnIsDigitsOnlyErrorMessage = "ИНН должен содержать только цифры";
        private const string InnIsLessTwelveDigitsOnlyErrorMessage = "ИНН должен содержать не более 12 цифр";
        private const string BikIsNineDigitsOnlyErrorMessage = "БИК должен содержать 9 цифр";
        private const string BikIsDigitsOnlyErrorMessage = "БИК должен содержать только цифры";
        private const string BikIsEmptyErrorMessage = "БИК не заполнен";
        private const string MsgNoContractSelected = "Не выбран договор.";
        private const string MsgKindListFilterMissing = "Не удалось определить фильтр списка видов платежа.";
        private const string MsgContractCardTitle = "Карточка договора";
        private const string MsgBikNotFound = "БИК не найден ";
        private const string MsgBikNotFoundTitle = "Обработка данных БИКа";
        private const string MsgContractSaved = "Договор сохранен в БД";
        private const string MsgCloseDateRequired = "Не заполнена дата закрытия договора";
        private const string MsgCloseDateTitle = "Обработка даты закрытия договора";
        private const string MsgDuplicateContractCode = "Объект с таким кодом уже существует!";
        private const string MsgValidationTitle = "Проверка корректности данных";
        private const string MsgInvalidContractCode = "Некорректный код договора!";
        private const string MsgInvalidContractCodeLength = "Некорректная длина кода договора!";
        private const string MsgInvalidCommentLength = "Некорректная длина комментария!";
        private const string MsgInvalidCommentEmpty = "Некорректный комментарий!";
        private const string MsgInvalidContractNumberLength = "Некорректная длина номера договора!";
        private const string MsgInvalidPaymentKind = "Некорректный вид платежа !";
        private const string MsgInnRequiredForBankClient = "Не заполнен ИНН для клиента банка!";
        private const string MsgInvalidBikLength = "Некорректная длина БИКа!";
        private const string MsgInvalidInnLength = "Некорректная длина ИНН!";
        private const string MsgInvalidAddressLength = "Некорректная длина адреса!";
        private const string MsgInvalidClientAccount = "Некорректный счет клиента!";
        private const string MsgInvalidAccountKey = "Некорректный ключ счета!";
        private const string MsgAccountNotForClient = "Выбранный счет не принадлежит выбранному клиенту!";
        private const string MsgCommissionPayerMinMaxOrder = "Некорректное заполнение доп.поля 'Доп.параметры комиссии с плательщика Минимальная сумма должна быть меньше максимальной'!";
        private const string MsgCommissionPayerMaxRequired = "Некорректное заполнение доп.поля 'Доп.параметры комиссии с плательщика' Максимальная сумма должна быть заполнена!";
        private const string MsgCommissionPayerMinRequired = "Некорректное заполнение доп.поля 'Доп.параметры комиссии с плательщика' Минимальная сумма должна быть заполнена!";
        private const string MsgCommissionPayerBothRequired = "Некорректное заполнение доп.поля 'Доп.параметры комиссии с плательщика' Минимальная и максимальныя суммы должны быть заполнены!";
        private const string MsgCommissionRecMinMaxOrder = "Некорректное заполнение доп.поля 'Доп.параметры комиссии с получателя Минимальная сумма должна быть меньше максимальной'!";
        private const string MsgCommissionRecMaxRequired = "Некорректное заполнение доп.поля 'Доп.параметры комиссии с получателя' Максимальная сумма должна быть заполнена!";
        private const string MsgCommissionRecMinRequired = "Некорректное заполнение доп.поля 'Доп.параметры комиссии с получателя' Минимальная сумма должна быть заполнена!";
        private const string MsgCommissionRecBothRequired = "Некорректное заполнение доп.поля 'Доп.параметры комиссии с получателя' Минимальная и максимальныя суммы должны быть заполнены!";

        #endregion
    }
}
