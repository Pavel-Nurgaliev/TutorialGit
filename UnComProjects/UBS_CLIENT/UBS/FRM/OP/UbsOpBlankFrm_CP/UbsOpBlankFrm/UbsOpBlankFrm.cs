using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// Форма обработки документа
    /// </summary>
    public partial class UbsOpBlankFrm : UbsFormBase
    {
        #region Поля формы

        private string m_command = "";
        private int m_id = 0;
        private int m_kindVal = 0;

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public UbsOpBlankFrm()
        {
            m_addCommand(); // регистрация методов обработки команд IUbs

            InitializeComponent();

            base.UbsCtrlFieldsSupportCollection.Add("Доп. поля", ubsCtrlAddFields);

            base.Ubs_CommandLock = true;
        }


        #region Обработчики кнопок (в форме)

        private void btnExit_Click(object sender, EventArgs e) { this.Close(); }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren()) { return; }

                base.UbsChannel_ParamIn("Идентификатор", m_id);
                if (cmbState.Enabled && cmbState.SelectedItem != null)
                    base.UbsChannel_ParamIn("Состояние", ((KeyValuePair<int, string>)cmbState.SelectedItem).Key);

                base.UbsChannel_Run("Blank_Save");

                ubsCtrlInfo.Show(MsgSaved);
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion


        #region Методы обработки IUbs команд (в форме)

        /// <summary>
        /// Регистрация методов обработки команд IUbs в данной форме
        /// </summary>
        private void m_addCommand()
        {
            base.Ubs_AddName(new UbsDelegate(CommandLine));
            base.Ubs_AddName(new UbsDelegate(ListKey));
        }

        /// <summary>
        /// Метод обработки команды CommandLine
        /// </summary>
        /// <param name="param_in">входные параметры</param>
        /// <param name="param_out">выходные параметры</param>
        /// <returns></returns>
        private object CommandLine(object param_in, ref object param_out)
        {
            // запись строки команды
            m_command = (string)param_in;
            return null;
        }

        /// <summary>
        /// Метод обработки команды ListKey
        /// </summary>
        /// <param name="param_in">входные параметры</param>
        /// <param name="param_out">выходные параметры</param>
        /// <returns></returns>
        private object ListKey(object param_in, ref object param_out)
        {
            try
            {
                // получение идентификатора документа
                m_id = (param_in != null && param_in is object[] && ((object[])param_in).Length > 0)
                    ? Convert.ToInt32(((object[])param_in)[0])
                    : 0;

                this.IUbsChannel.LoadResource = LoadResource;

                if (m_id == 0)
                {
                    base.Ubs_ShowErrorBox(MsgEmptyList);
                    this.Close();
                    return null;
                }

                InitDoc();

                ubsCtrlAddFields.Refresh();

                tabControl.SelectedTab = tabPageMain;
                if (cmbState.Enabled) cmbState.Focus();

                return null;
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        #endregion

        #region InitDoc, LoadFromParams, FillCombo

        private void InitDoc()
        {
            try
            {
                base.IUbsChannel.Run("InitForm");

                base.UbsInit();

                base.UbsChannel_ParamIn("Идентификатор", m_id);
                base.UbsChannel_Run("Get_Data");

                var paramOut = new UbsParam(base.UbsChannel_ParamsOut);
                LoadFromParams(paramOut);

                m_kindVal = paramOut.Contains("Идентификатор вида") ? Convert.ToInt32(paramOut.Value("Идентификатор вида")) : 0;
                int currentState = paramOut.Contains("Состояние") ? Convert.ToInt32(paramOut.Value("Состояние")) : 0;

                FillCombo(currentState);
                ubsCtrlAddFields.Refresh();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void LoadFromParams(UbsParam paramOut)
        {
            if (paramOut.Contains("Дата учета"))
            {
                object v = paramOut.Value("Дата учета");
                if (v != null && v != DBNull.Value)
                {
                    try { dateCalc.DateValue = Convert.ToDateTime(v); }
                    catch { }
                }
            }
            if (paramOut.Contains("Серия")) txbSer.Text = Convert.ToString(paramOut.Value("Серия"));
            if (paramOut.Contains("Номер")) txbNum.Text = Convert.ToString(paramOut.Value("Номер"));
            if (paramOut.Contains("Наименование ценности")) txbNameVal.Text = Convert.ToString(paramOut.Value("Наименование ценности"));
            if (paramOut.Contains("Вид ценности")) txbKindVal.Text = Convert.ToString(paramOut.Value("Вид ценности"));
        }

        private void FillCombo(int currentState)
        {
            cmbState.Items.Clear();

            var kvpList = new List<KeyValuePair<int, string>>();

            if (currentState == 17)
            {
                kvpList.Add(new KeyValuePair<int, string>(17, StateProcessed));
                cmbState.Enabled = false;
            }
            else
            {
                cmbState.Enabled = true;

                if (m_kindVal == 3)
                {
                    kvpList.Add(new KeyValuePair<int, string>(15, StatePaymentRefused));
                    kvpList.Add(new KeyValuePair<int, string>(13, StatePaid));
                }
                else if (m_kindVal == 4)
                {
                    kvpList.Add(new KeyValuePair<int, string>(16, StateNotAuthentic));
                    kvpList.Add(new KeyValuePair<int, string>(14, StateAuthentic));
                }

                kvpList.Add(new KeyValuePair<int, string>(12, StateSentToNonResidentBank));
                kvpList.Add(new KeyValuePair<int, string>(11, StateSentToGO));
                kvpList.Add(new KeyValuePair<int, string>(10, StateAcceptedFromClient));
            }

            InitComboBox(cmbState, kvpList);

            foreach (KeyValuePair<int, string> item in cmbState.Items)
            {
                if (item.Key == currentState)
                {
                    cmbState.SelectedItem = item;
                }
            }
        }

        private void InitComboBox(ComboBox cmb, object list)
        {
            cmb.DataSource = list;
            cmb.ValueMember = "Key";
            cmb.DisplayMember = "Value";
            cmb.SelectedIndex = 0;
        }
    }

    #endregion
}