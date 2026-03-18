namespace UbsBusiness
{
    /// <summary>
    /// Константы формы UbsOpCommissionSetupFrm (ресурс канала, команды, сообщения).
    /// </summary>
    public partial class UbsOpCommissionSetupFrm
    {
        #region Ресурс канала

        /// <summary>Имя ресурса для загрузки канала.</summary>
        private const string LoadResource = "VBS:UBS_VBD\\OP\\Commission_Setup.vbs";

        #endregion

        #region Команды

        private const string AddCommand = "ADD";
        private const string EditCommand = "EDIT";

        #endregion

        #region Сообщения

        private const string MsgDataSaved = "Данные сохранены!";
        private const string MsgListEmpty = "Список установок комиссий пуст!";
        private const string MsgCommissionRequired = "Не указана комиссия.";
        private const string MsgOperationRequired = "Не указана операция.";
        private const string MsgSetupExists = "Такая установка уже существует!";

        #endregion
    }
}
