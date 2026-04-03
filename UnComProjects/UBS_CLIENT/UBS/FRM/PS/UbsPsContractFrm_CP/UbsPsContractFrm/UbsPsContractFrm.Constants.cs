namespace UbsBusiness
{
    public partial class UbsPsContractFrm
    {
        #region Resource

        private const string LoadResource = "ASM:UBS_ASM\\Business\\DllName.dll->UbsBusiness.UbsPsContractFrm";

        #endregion

        #region Command names (IUbs)

        private const string CmdAdd = "ADD";
        private const string CmdEdit = "EDIT";

        #endregion

        #region STRCOMMAND values

        private const string ActionRead = "READ";
        private const string ActionReadFilter = "READF";
        private const string ActionChangeKind = "CHANGEKIND";

        #endregion

        #region Commission UI (legacy Contract.dob EnableSum — ListIndex rules)

        private const int CommissionComboIndexDisablePercentFirst = 0;
        private const int CommissionComboIndexDisablePercentSecond = 3;

        #endregion

        #region UbsCtrlFields support key

        private const string AddFieldsSupportKey = "Доп. поля";

        #endregion

        #region ReadBankBIK (legacy GetBankNameACC)

        private const string CorrespondentAccountPlaceholder = "00000000000000000000";

        #endregion

        #region Messages

        private const string MsgNoContractSelected = "Не выбран договор.";
        private const string MsgSaveNotImplemented = "Сохранение будет добавлено на следующем этапе.";
        private const string MsgBrowseNotImplemented = "Выбор из списка будет подключён на интеграции с оболочкой.";

        #endregion
    }
}
