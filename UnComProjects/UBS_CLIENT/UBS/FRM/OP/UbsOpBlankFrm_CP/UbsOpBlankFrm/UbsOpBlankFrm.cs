using System;
using System.Collections.Generic;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// Форма обработки документа
    /// </summary>
    public partial class UbsOpBlankFrm : UbsFormBase
    {
        #region Поля формы

        private string m_command = string.Empty;
        private int m_id = 0;
        private int m_kindVal = 0;
        private List<int> m_stateIds = new List<int>();

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public UbsOpBlankFrm()
        {
            m_addCommand(); // регистрация методов обработки команд IUbs

            InitializeComponent();

            // регистрация метода загрузки ресурсов
            this.IUbsChannel.LoadResource = LoadResource;

            base.UbsCtrlFieldsSupportCollection.Add("Доп. поля", ubsCtrlAddFields);
            this.Text = FormTitle;

            base.Ubs_CommandLock = true;
        }


        #region Обработчики кнопок (в форме)

        private void btnExit_Click(object sender, EventArgs e) { this.Close(); }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren()) { return; }

                base.UbsChannel_ParamIn(ParamId, m_id);
                if (cmbState.Enabled && cmbState.SelectedIndex >= 0 && cmbState.SelectedIndex < m_stateIds.Count)
                    base.UbsChannel_ParamIn(ParamState, m_stateIds[cmbState.SelectedIndex]);

                base.UbsChannel_Run(BlankSaveCommand);

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

                base.UbsChannel_ParamIn(ParamId, m_id);
                base.UbsChannel_Run(GetDataCommand);

                var paramOut = new UbsParam(base.UbsChannel_ParamsOut);
                LoadFromParams(paramOut);

                m_kindVal = paramOut.Contains(ParamKindId) ? Convert.ToInt32(paramOut.Value(ParamKindId)) : 0;
                int currentState = paramOut.Contains(ParamState) ? Convert.ToInt32(paramOut.Value(ParamState)) : 0;

                FillCombo(currentState);
                ubsCtrlAddFields.Refresh();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void LoadFromParams(UbsParam paramOut)
        {
            if (paramOut.Contains(ParamDate))
            {
                object v = paramOut.Value(ParamDate);
                if (v != null && v != DBNull.Value)
                {
                    try { dateCalc.DateValue = Convert.ToDateTime(v); }
                    catch { }
                }
            }
            if (paramOut.Contains(ParamSer)) txbSer.Text = Convert.ToString(paramOut.Value(ParamSer));
            if (paramOut.Contains(ParamNum)) txbNum.Text = Convert.ToString(paramOut.Value(ParamNum));
            if (paramOut.Contains(ParamNameVal)) txbNameVal.Text = Convert.ToString(paramOut.Value(ParamNameVal));
            if (paramOut.Contains(ParamKindVal)) txbKindVal.Text = Convert.ToString(paramOut.Value(ParamKindVal));
        }

        private void FillCombo(int currentState)
        {
            cmbState.Items.Clear();
            m_stateIds.Clear();

            if (currentState == 17)
            {
                cmbState.Items.Add("Документ обработан (изменение запрещено)");
                m_stateIds.Add(17);
                cmbState.Enabled = false;
            }
            else
            {
                cmbState.Enabled = true;
                if (m_kindVal == 3)
                {
                    cmbState.Items.Add("Отказано в оплате"); m_stateIds.Add(15);
                    cmbState.Items.Add("Оплачен"); m_stateIds.Add(13);
                }
                else if (m_kindVal == 4)
                {
                    cmbState.Items.Add("Признан неподлинным"); m_stateIds.Add(16);
                    cmbState.Items.Add("Признан подлинным"); m_stateIds.Add(14);
                }
                cmbState.Items.Add("Отправлен в банк-нерезидент"); m_stateIds.Add(12);
                cmbState.Items.Add("Отправлен в ГО"); m_stateIds.Add(11);
                cmbState.Items.Add("Принят от клиента"); m_stateIds.Add(10);
            }

            for (int i = 0; i < m_stateIds.Count; i++)
            {
                if (m_stateIds[i] == currentState) { cmbState.SelectedIndex = i; break; }
            }
        }

        #endregion
    }
}