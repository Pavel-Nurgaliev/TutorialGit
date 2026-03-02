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

        /// <summary>Тип ставки (текст выбранного элемента комбобокса).</summary>
        public string RateTypeText
        {
            get { return cbRateTypes.Text ?? string.Empty; }
            set
            {
                cbRateTypes.Items.Clear();
                if (!string.IsNullOrEmpty(value))
                {
                    cbRateTypes.Items.Add(value);
                    cbRateTypes.SelectedIndex = 0;
                }
            }
        }

        /// <summary>Дата установки ставки.</summary>
        public DateTime DateValue
        {
            get { return ubsCtrlDateRate.DateValue; }
            set { ubsCtrlDateRate.DateValue = value; }
        }

        /// <summary>Значение ставки.</summary>
        public decimal RateValue
        {
            get { return ubsCtrlRate.DecimalValue; }
            set { ubsCtrlRate.DecimalValue = value; }
        }

        /// <summary>Разрешить редактирование даты (false для режима редактирования).</summary>
        public bool DateEnabled
        {
            set { ubsCtrlDateRate.Enabled = value; }
        }

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
