namespace UbsBusiness
{
    /// <summary>
    /// Константы формы Принятая ценность (Blank_ud): ресурс канала, команды, параметры, сообщения.
    /// </summary>
    public partial class UbsOpBlankFrm
    {
        #region ComboConst
        private const string StateProcessed = "Документ обработан (изменение запрещено)";
        private const string StatePaymentRefused = "Отказано в оплате";
        private const string StatePaid = "Оплачен";
        private const string StateNotAuthentic = "Признан неподлинным";
        private const string StateAuthentic = "Признан подлинным";
        private const string StateSentToNonResidentBank = "Отправлен в банк-нерезидент";
        private const string StateSentToGO = "Отправлен в ГО";
        private const string StateAcceptedFromClient = "Принят от клиента";
        #endregion
        #region Ресурс канала

        /// <summary>Имя ресурса для загрузки канала (OP Blank).</summary>
        public const string LoadResource = "VBS:UBS_VBD\\OP\\Blank.vbs";

        #endregion

        #region Команды канала

        /// <summary>Команда загрузки данных записи.</summary>
        public const string GetDataCommand = "Get_Data";
        /// <summary>Команда сохранения.</summary>
        public const string BlankSaveCommand = "Blank_Save";

        #endregion

        #region Параметры канала

        /// <summary>Идентификатор записи.</summary>
        public const string ParamId = "Идентификатор";
        /// <summary>Дата учета.</summary>
        public const string ParamDate = "Дата учета";
        /// <summary>Наименование ценности.</summary>
        public const string ParamNameVal = "Наименование ценности";
        /// <summary>Вид ценности.</summary>
        public const string ParamKindVal = "Вид ценности";
        /// <summary>Идентификатор вида.</summary>
        public const string ParamKindId = "Идентификатор вида";
        /// <summary>Серия.</summary>
        public const string ParamSer = "Серия";
        /// <summary>Номер.</summary>
        public const string ParamNum = "Номер";
        /// <summary>Состояние.</summary>
        public const string ParamState = "Состояние";

        #endregion

        #region Сообщения

        /// <summary>Сообщение после успешного сохранения.</summary>
        public const string MsgSaved = "Данные сохранены!";
        /// <summary>Ошибка: пустой список принятых ценностей.</summary>
        public const string MsgEmptyList = "Список принятых ценностей пуст!";
        /// <summary>Заголовок формы.</summary>
        public const string FormTitle = "Принятая ценность";

        #endregion
    }
}
