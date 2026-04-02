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
        /// <summary>Имя ресурса для загрузки канала (удаление).</summary>
        private const string DelSimpleObjectLoadResource = "VBS:UBS_VBS\\SYSTEM\\del_simple_object_channel.vbs";

        #endregion

        #region Команды

        private const string AddCommand = "ADD";
        private const string EditCommand = "EDIT";
        private const string DeleteCommand = "DEL";

        #endregion

        #region Сообщения

        private const string MsgDataSaved = "Данные сохранены!";
        private const string MsgListEmpty = "Список установок комиссий пуст!";
        private const string MsgCommissionRequired = "Не указана комиссия.";
        private const string MsgOperationRequired = "Не указана операция.";
        private const string MsgSetupExists = "Такая установка уже существует!";

        private const string AreYouSureAboutDeletingRecords = "Вы уверены, что хотите удалить выделенные записи?";
        private const string MsgDeletingRecords = "Удаление записей";
        private const string RecordsIsNotChosen = "Не выбраны записи для удаления!";

        #endregion
    }
}
