using System;
using System.Collections.Generic;
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

        #region Инициализация и обмен данными (для GetBonusPayOrder)

        /// <summary>
        /// Инициализирует комбобоксы типа периода и типа даты гашений.
        /// </summary>
        /// <param name="periodTypes">Список (id, текст) для типа периода. Соответствует VB arrTypePeriod (row 0 = id, row 1 = text).</param>
        /// <param name="dateTypes">Список (id, текст) для типа даты гашений. Соответствует VB arrTypeDate.</param>
        public void InitPeriodAndDateTypes(List<KeyValuePair<int, string>> periodTypes, List<KeyValuePair<int, string>> dateTypes)
        {
            if (periodTypes != null && periodTypes.Count > 0)
            {
                cbTypePeriod.DataSource = new List<KeyValuePair<int, string>>(periodTypes);
                cbTypePeriod.ValueMember = "Key";
                cbTypePeriod.DisplayMember = "Value";
                cbTypePeriod.SelectedIndex = 0;
            }
            if (dateTypes != null && dateTypes.Count > 0)
            {
                cbTypeDate.DataSource = new List<KeyValuePair<int, string>>(dateTypes);
                cbTypeDate.ValueMember = "Key";
                cbTypeDate.DisplayMember = "Value";
                cbTypeDate.SelectedIndex = 0;
            }
            ubsCtrlPeriod.DecimalValue = 1;
        }

        /// <summary>
        /// Устанавливает начальные значения формы из массива интервала (layout [1,4]: тип периода, период, тип даты, номер(а) дня).
        /// </summary>
        public void SetInitialData(object[,] arrInterval)
        {
            if (arrInterval == null || arrInterval.GetLength(0) < 1 || arrInterval.GetLength(1) < 4)
                return;
            SetComboByValue(cbTypePeriod, Convert.ToInt32(arrInterval[0, 0]));
            ubsCtrlPeriod.DecimalValue = Convert.ToDecimal(arrInterval[0, 1]);
            SetComboByValue(cbTypeDate, Convert.ToInt32(arrInterval[0, 2]));
            object dayData = arrInterval[0, 3];
            if (dayData == null || dayData == DBNull.Value) return;
            if (ubsCtrlNumDay.Enabled)
            {
                if (dayData is object[,] arrDays && arrDays.GetLength(0) > 0 && arrDays.GetLength(1) > 0)
                    ubsCtrlNumDay.DecimalValue = Convert.ToDecimal(arrDays[0, 0]);
                else
                    ubsCtrlNumDay.DecimalValue = Convert.ToDecimal(dayData);
            }
            else if (m_chkDays != null && m_chkDays.Length > 0 && m_chkDays[0].Enabled)
            {
                for (int i = 0; i < m_chkDays.Length; i++)
                    m_chkDays[i].Checked = false;
                if (dayData is object[,] arrDays && arrDays.GetLength(1) > 0)
                {
                    for (int c = 0; c < arrDays.GetLength(1); c++)
                    {
                        int oneBased = Convert.ToInt32(arrDays[0, c]);
                        if (oneBased >= 1 && oneBased <= m_chkDays.Length)
                            m_chkDays[oneBased - 1].Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает выбранные значения в формате [1,4]: тип периода, период, тип даты, номер(а) дня.
        /// </summary>
        public object[,] GetResult()
        {
            var result = new object[1, 4];
            result[0, 0] = cbTypePeriod.SelectedValue != null ? Convert.ToInt32(cbTypePeriod.SelectedValue) : 0;
            result[0, 1] = ubsCtrlPeriod.DecimalValue;
            result[0, 2] = cbTypeDate.SelectedValue != null ? Convert.ToInt32(cbTypeDate.SelectedValue) : 0;
            if (ubsCtrlNumDay.Enabled)
            {
                result[0, 3] = ubsCtrlNumDay.DecimalValue;
            }
            else if (m_chkDays != null && m_chkDays[0].Enabled)
            {
                var list = new List<int>();
                for (int i = 0; i < m_chkDays.Length; i++)
                {
                    if (m_chkDays[i].Checked)
                        list.Add(i + 1);
                }
                if (list.Count > 0)
                {
                    var arrNumDays = new object[1, list.Count];
                    for (int i = 0; i < list.Count; i++)
                        arrNumDays[0, i] = list[i];
                    result[0, 3] = arrNumDays;
                }
                else
                    result[0, 3] = null;
            }
            else
                result[0, 3] = null;
            return result;
        }

        private static void SetComboByValue(ComboBox cmb, int value)
        {
            if (cmb.DataSource == null) return;
            try
            {
                cmb.SelectedValue = value;
            }
            catch
            {
                for (int i = 0; i < cmb.Items.Count; i++)
                {
                    if (cmb.Items[i] is KeyValuePair<int, string> kvp && kvp.Key == value)
                    {
                        cmb.SelectedIndex = i;
                        return;
                    }
                }
            }
        }

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

        /// <summary>
        /// Разрешает или запрещает кнопку «Применить» (для режима просмотра, когда IdState &lt; 2).
        /// </summary>
        public bool ApplyButtonEnabled
        {
            get => btnApply.Enabled;
            set => btnApply.Enabled = value;
        }

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
