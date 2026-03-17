namespace UbsBusiness
{
    /// <summary>
    /// Константы формы UbsOpCommissionFrm (ресурс канала, команды, параметры, сообщения).
    /// </summary>
    public partial class UbsOpCommissionFrm
    {
        #region Ресурс канала

        /// <summary>Имя ресурса для загрузки канала.</summary>
        private const string LoadResource = "VBS:UBS_VBD\\OP\\Commission.vbs";
        /// <summary>Имя ресурса для загрузки канала (удаление).</summary>
        private const string DelSimpleObjectLoadResource = "VBS:UBS_VBS\\SYSTEM\\del_simple_object_channel.vbs";

        #endregion

        #region Команды

        private const string AddCommand = "ADD";
        private const string EditCommand = "EDIT";
        private const string DeleteCommand = "DEL";

        #endregion

        #region Сообщения

        private const string MsgCommissionListEmpty = "Список комиссий пуст!";
        private const string MsgNameRequired = "Не введено наименование комиссии.";
        private const string MsgCheckProps = "Проверка свойств";
        private const string MsgDataSaved = "Данные сохранены!";
        private const string MsgError = "Ошибка";
        private const string MsgErrorExecution = "ошибка выполнения";

        private const string AreYouSureAboutDeletingRecords = "Вы уверены, что хотите удалить выделенные записи?";
        private const string MsgDeletingRecords = "Удаление записей";
        private const string RecordsIsNotChosen = "Не выбраны записи для удаления!";

        #endregion
    }
}
