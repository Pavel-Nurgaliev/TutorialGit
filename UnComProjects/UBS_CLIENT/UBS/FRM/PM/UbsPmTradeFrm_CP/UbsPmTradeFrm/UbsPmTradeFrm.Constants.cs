using UbsService;

namespace UbsBusiness
{
    public partial class UbsPmTradeFrm : UbsFormBase
    {
        private const string LoadResource = @"VBS:UBS_VBD\PM\Pm_Trade.vbs";

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
    }
}

