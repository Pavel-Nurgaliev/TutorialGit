using UbsService;

namespace UbsBusiness
{
    public partial class UbsPmTradeFrm : UbsFormBase
    {
        private const string LoadResource = @"VBS:UBS_VBD\PM\Pm_Trade.vbs";

        private const string PM_ACCOUNTS_BY_OBLIGATION = @"UBS_FLT\PM\PM_ACCOUNTS_BY_OBLIGATION.flt";
        private const string PM_FOR_OPERATION_SALE = @"UBS_FLT\PM\PM_FOR_OPERATION_SALE.flt";
        private const string PM_FOR_OPERATION = @"UBS_FLT\PM\PM_FOR_OPERATION.flt";
        private const string ACCOUNT0_FLT = @"UBS_FLT\OD\account0.flt";

        // Run-mode commands passed through ListKey/CommandLine.
        private const string CmdAdd = "ADD";
        private const string CmdEdit = "EDIT";

        private const string MsgListEmpty = "Не выбрана запись для редактирования";

        private const string MsgDeleteReverseObligations = "Удалить обязательства обратной сделки ?";
        private const string MsgWarningTitle = "Предупреждение";

        private const string TextTradeDirectionReverse = "обратная";

        private const string TextContractCodeBank = "банк";

        private const string MsgTitleValidationProps = "Проверка свойств";
        private const string MsgTitleInputError = "Ошибка ввода";

        private const string MsgContractTypesMustDiffer = "Тип договора покупателя и продавца не может совпадать";
        private const string MsgNoTradeDate = "Не указана дата сделки";
        private const string MsgNoCurrencyPost = "Не выбран драг.металл.";
        private const string MsgNoBuyer = "Не выбран покупатель";
        private const string MsgNoKindSupply = "Не выбран вид поставки по сделке";

        private const string MsgFltRecordsIsNotSelected = "Не выбрано записи для добавления.";
        private const string ErrorCaption = "Ошибка";

        private const string MsgNoDirection = "Не указано направление.";
        private const string MsgNoCostUnit = "Не указана цена за единицу.";
        private const string MsgNoDeliveryDate = "Не указана дата поставки.";
        private const string MsgDeliveryDateBeforeTrade = "Дата поставки не может быть меньше даты сделки.";
        private const string MsgNoPaymentDate = "Не указана дата оплаты.";
        private const string MsgPaymentDateBeforeTrade = "Дата оплаты не может быть меньше даты сделки.";
        private const string MsgNoExchangeRate = "Не указан курс пересчета";
        private const string MsgNoMass = "Не указана масса металла.";
        private const string MsgNoObjects = "Не заполнен список объектов по обязательству";

        private const string PrefixAddOblig = "Add";
        private const string PrefixEditOblig = "Edit";

        private const string TextTradeDirectionDirect = "прямая";

        private const string MsgSaved = "Данные сохранены";
        private const string MsgNoTradeNum = "Не указан номер сделки";
        private const string MsgNoCurrencyPayment = "Не выбрана валюта оплаты";
        private const string MsgNoCurrencyObligation = "Не выбрана валюта обязательства";
        private const string MsgNoWeightUnit = "Не указана единица измерения веса";
        private const string MsgNoSeller = "Не выбран продавец";
        private const string MsgNoObligations = "Отсутствуют обязательства";
        private const string MsgNoBuyerInstruction = "Отсутствует инструкция по оплате у покупателя";
        private const string MsgNoSellerInstruction = "Отсутствует инструкция по оплате у продавца";
        private const string MsgNoObjectsByObligation = "Отсутствует список объектов по обязательству";
        private const string MsgNoStorageInstruction = "Отсутствует инструкция по поставке";
        private const string MsgSpecifyTradeDate = "Укажите дату сделки";
        private const string MsgBuyerRsCurrencyMismatch = "Валюта расчетного счета инструкции оплаты покупателя должна соответствовать валюте оплаты.";
        private const string MsgSellerRsCurrencyMismatch = "Валюта расчетного счета инструкции оплаты продавца должна соответствовать валюте оплаты.";
        private const string MsgInvalidDate = "Введите корректную дату.";
        private const string MsgFillBuyerData = "Заполните данные о покупателе";
        private const string MsgFillSellerData = "Заполните данные о продавце";
        private const string MsgErrorTitle = "Ошибка ввода";
        private const string MsgCheckBIK = "Проверка БИК";
        private const string MsgCheckKey = "Проверка ключа";
        private const string MsgCheckINN = "Проверка ИНН";
        private const string TextKindPhysical = "физическая";
    }
}

