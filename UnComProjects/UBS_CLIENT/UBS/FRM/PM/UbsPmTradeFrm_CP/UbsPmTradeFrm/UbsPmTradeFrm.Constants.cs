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
    }
}

