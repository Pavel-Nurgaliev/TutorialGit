namespace UbsBusiness
{
    /// <summary>
    /// Константы формы Операция возмещения (op_ret_oper): ресурс канала, пользовательские сообщения.
    /// Команды и параметры канала — явные строки в коде (см. channel contract).
    /// </summary>
    public partial class UbsOpRetoperFrm
    {
        #region Ресурс канала

        /// <summary>Имя ресурса для загрузки канала (OP opers).</summary>
        public const string LoadResource = "VBS:UBS_VBS\\OP\\opers.vbs";

        #endregion

        #region Сообщения

        /// <summary>Сообщение после успешного сохранения.</summary>
        public const string MsgSaved = "Данные сохранены!";
        /// <summary>Ошибка: пустой список ценностей.</summary>
        public const string MsgEmptyList = "Список ценностей пуст.";
        /// <summary>Ошибка: выберите валюту.</summary>
        public const string MsgSelectCurrency = "Выберите валюту.";
        /// <summary>Заголовок формы.</summary>
        public const string FormTitle = "Операция возмещения";

        #endregion
    }
}
