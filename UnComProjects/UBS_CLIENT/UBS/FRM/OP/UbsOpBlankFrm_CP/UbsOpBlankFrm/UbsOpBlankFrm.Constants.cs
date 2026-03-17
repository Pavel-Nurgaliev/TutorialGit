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
