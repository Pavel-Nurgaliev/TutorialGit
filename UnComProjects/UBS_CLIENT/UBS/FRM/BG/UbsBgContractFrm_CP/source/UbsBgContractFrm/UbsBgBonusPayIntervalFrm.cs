using System;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// Форма периода
    /// </summary>
    public partial class UbsBgBonusPayIntervalFrm : UbsFormBase
    {
        #region Блок констант

        private const string MsgPeriodTypeRequired = "Необходимо указать тип периода";
        private const string MsgPeriodRequired = "Необходимо указать корректный период";
        private const string MsgDayNumberRequired = "Необходимо указать номер дня периода";
        private const string MsgDateTypeRequired = "Необходимо указать тип даты гашений";
        private const string MsgWeekDaysRequired = "Необходимо указать дни недели для исполнения поручения";
        private const string MsgError = "Ошибка";

        #endregion
        #region Блок объявления переменных

        private string m_command = string.Empty;
        private bool m_blnEditRegime = false;
        private CheckBox[] m_chkDays; // Массив чекбоксов дней недели

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public UbsBgBonusPayIntervalFrm()
        {
            InitializeComponent();

            m_chkDays = new CheckBox[]
            {
                chkDay0, chkDay1, chkDay2, chkDay3, chkDay4, chkDay5, chkDay6
            };

            base.Ubs_CommandLock = true;

            if (cbTypePeriod.SelectedIndex == 0)
            {
                cbTypeDate.SelectedIndex = 0;
                cbTypeDate.Enabled = false;
            }
            else
            {
                cbTypeDate.Enabled = true;
            }
        }
        public bool ApplyClicked { get; set; }

        #region Обработчики событий кнопок

        private void btnExit_Click(object sender, EventArgs e)
        {
            ApplyClicked = false;
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren()) { return; }

                if (!Check())
                {
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
        private void cbTypePeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTypePeriod.SelectedIndex == 0)
            {
                cbTypeDate.SelectedIndex = 0;
                cbTypeDate.Enabled = false;
            }
            else
            {
                cbTypeDate.Enabled = true;
            }
            cbTypeDate_SelectedIndexChanged(sender, e);
        }

        private void cbTypeDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_blnEditRegime = true;

            try
            {
                if (cbTypeDate.SelectedIndex >= 0)
                {
                    // Получаем значение из Tag или используем SelectedIndex как идентификатор
                    int dateTypeId = cbTypeDate.SelectedIndex;

                    if (dateTypeId == 0 || dateTypeId == 1)
                    {
                        // "Последний день периода", "Последний рабочий день периода" - отключаем все
                        ubsCtrlNumDay.Enabled = false;
                        foreach (CheckBox chk in m_chkDays)
                        {
                            chk.Enabled = false;
                        }
                    }
                    else if (dateTypeId == 2)
                    {
                        // "Указанный день периода"
                        if (cbTypePeriod.SelectedIndex == 1) // "Неделя"
                        {
                            ubsCtrlNumDay.Enabled = false;
                            foreach (CheckBox chk in m_chkDays)
                            {
                                chk.Enabled = true;
                            }
                        }
                        else
                        {
                            ubsCtrlNumDay.Enabled = true;
                            foreach (CheckBox chk in m_chkDays)
                            {
                                chk.Enabled = false;
                            }
                        }
                    }
                    else if (dateTypeId == 3)
                    {
                        // "В конкретную дату" - отключаем все
                        ubsCtrlNumDay.Enabled = false;
                        foreach (CheckBox chk in m_chkDays)
                        {
                            chk.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
            finally
            {
                m_blnEditRegime = false;
            }
        }

        private bool Check()
        {
            if (cbTypePeriod.SelectedIndex < 0)
            {
                MessageBox.Show(MsgPeriodTypeRequired, MsgError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (m_blnEditRegime) btnApply.Enabled = true;
                btnExit.Enabled = true;
                cbTypePeriod.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(ubsCtrlPeriod.Text) || ubsCtrlPeriod.DecimalValue == 0)
            {
                MessageBox.Show(MsgPeriodRequired, MsgError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (m_blnEditRegime) btnApply.Enabled = true;
                btnExit.Enabled = true;
                ubsCtrlPeriod.Focus();
                return false;
            }

            if (ubsCtrlNumDay.Enabled && cbTypePeriod.SelectedIndex != 0 &&
                (string.IsNullOrEmpty(ubsCtrlNumDay.Text) || ubsCtrlNumDay.DecimalValue == 0))
            {
                MessageBox.Show(MsgDayNumberRequired, MsgError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (m_blnEditRegime) btnApply.Enabled = true;
                btnExit.Enabled = true;
                ubsCtrlNumDay.Focus();
                return false;
            }

            if (cbTypeDate.SelectedIndex < 0)
            {
                MessageBox.Show(MsgDateTypeRequired, MsgError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (m_blnEditRegime) btnApply.Enabled = true;
                btnExit.Enabled = true;
                cbTypeDate.Focus();
                return false;
            }

            if (m_chkDays[0].Enabled)
            {
                bool blnFlagChecked = false;
                foreach (CheckBox chk in m_chkDays)
                {
                    if (chk.Checked)
                    {
                        blnFlagChecked = true;
                        break;
                    }
                }

                if (!blnFlagChecked)
                {
                    MessageBox.Show(MsgWeekDaysRequired, MsgError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (m_blnEditRegime) btnApply.Enabled = true;
                    btnExit.Enabled = true;
                    m_chkDays[0].Focus();
                    return false;
                }
            }

            return true;
        }
    }
}
