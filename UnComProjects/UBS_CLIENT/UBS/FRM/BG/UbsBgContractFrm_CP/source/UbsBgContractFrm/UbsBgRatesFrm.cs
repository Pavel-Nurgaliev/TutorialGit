using System;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// Форма ставки
    /// </summary>
    public partial class UbsBgRatesFrm : UbsFormBase
    {
        #region Блок констант
        private const string MsgDateRequired = "Необходимо указать дату";
        private const string MsgError = "Ошибка";
        #endregion

        #region Блок объявления переменных

        private string m_command = string.Empty;    // параметер запуска формы

        private readonly DateTime m_date1990 = new DateTime(1990, 1, 1);
        private readonly DateTime m_date2222 = new DateTime(2222, 1, 1);

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public UbsBgRatesFrm()
        {
            InitializeComponent();

            base.Ubs_CommandLock = true;
        }

        #region Обработчики событий кнопок

        private void btnExit_Click(object sender, EventArgs e)
        {
            ApplyClicked = false;
            this.Close();
        }

        public bool ApplyClicked { get; set; }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren()) { return; }

                // Валидация даты
                if (ubsCtrlDateRate.DateValue >= m_date2222 || ubsCtrlDateRate.DateValue <= m_date1990)
                {
                    MessageBox.Show(MsgDateRequired, MsgError, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ubsCtrlDateRate.Focus();
                    return;
                }

                ApplyClicked = true;
                this.Close();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region Обработчики команд IUbs интерфейса

        #endregion
    }
}
