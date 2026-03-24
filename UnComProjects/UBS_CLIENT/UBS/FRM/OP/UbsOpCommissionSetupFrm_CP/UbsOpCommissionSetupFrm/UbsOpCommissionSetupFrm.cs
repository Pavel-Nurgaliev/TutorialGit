using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// Форма установки комиссии. Конвертирована из Commission_Setup_ud (VB6 UserDocument).
    /// </summary>
    public partial class UbsOpCommissionSetupFrm : UbsFormBase
    {
        #region Поля формы

        private string m_command = string.Empty;
        private int m_id = 0;
        private bool m_filling = false;

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public UbsOpCommissionSetupFrm()
        {
            m_addCommand();

            InitializeComponent();

            base.Ubs_CommandLock = true;
        }

        #region Обработчики кнопок

        private void btnExit_Click(object sender, EventArgs e) { this.Close(); }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren()) { return; }
                if (!CheckData()) { return; }

                base.IUbsChannel.ParamIn("Действие", m_command);
                if (m_command == EditCommand)
                    base.IUbsChannel.ParamIn("Идентификатор", m_id);

                base.IUbsChannel.ParamIn("Комиссия", GetComboKey(cmbCommission));
                base.IUbsChannel.ParamIn("Ценность", cmbValue.Enabled ? GetComboKey(cmbValue) : 0);
                base.IUbsChannel.ParamIn("Валюта", GetComboKey(cmbCurrency));
                base.IUbsChannel.ParamIn("Касса", GetComboKey(cmbDiv));
                base.IUbsChannel.ParamIn("Операция", GetComboKey(cmbOperation));

                base.IUbsChannel.Run("Save_Set");

                var paramOut = new UbsParam(base.IUbsChannel.ParamsOut);

                if (paramOut.Contains("Запись существует") && Convert.ToBoolean(paramOut.Value("Запись существует")))
                {
                    base.Ubs_ShowErrorBox(MsgSetupExists);
                    return;
                }

                if (paramOut.Contains("Идентификатор"))
                {
                    m_id = Convert.ToInt32(paramOut.Value("Идентификатор"));
                    m_command = EditCommand;
                }

                ubsCtrlInfo.Show(MsgDataSaved);
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region Обработчики событий

        private void cmbOper_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_filling) return;
            try
            {
                if (cmbOperation.SelectedIndex < 0) return;

                base.IUbsChannel.ParamIn("Операция", GetComboKey(cmbOperation));
                base.IUbsChannel.Run("Check_PayDoc");

                var paramOut = new UbsParam(base.IUbsChannel.ParamsOut);
                cmbValue.Enabled = paramOut.Contains("Документ") && Convert.ToBoolean(paramOut.Value("Документ"));
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region IUbs команды

        private void m_addCommand()
        {
            base.Ubs_AddName(new UbsDelegate(CommandLine));
            base.Ubs_AddName(new UbsDelegate(ListKey));
        }

        private object CommandLine(object param_in, ref object param_out)
        {
            m_command = param_in != null ? Convert.ToString(param_in) : string.Empty;
            return null;
        }

        private object ListKey(object param_in, ref object param_out)
        {
            try
            {
                m_id = (param_in != null && param_in is object[] && ((object[])param_in).Length > 0)
                    ? Convert.ToInt32(((object[])param_in)[0])
                    : 0;

                base.IUbsChannel.LoadResource = LoadResource;

                if (m_command == DeleteCommand)
                {
                    if (m_id == 0)
                    {
                        base.Ubs_ShowErrorBox(RecordsIsNotChosen);
                        return false;
                    }

                    if (MessageBox.Show(AreYouSureAboutDeletingRecords, MsgDeletingRecords, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        base.IUbsChannel.ParamIn("KeyArray", param_in);
                        base.IUbsChannel.Run("Del_Set");

                        IUbs iubs = Control.FromHandle((IntPtr)base.IUbs.Run("ParentHandle", null)) as IUbs;
                        if (iubs != null && iubs.ExistName("RefreshGrid")) iubs.Run("RefreshGrid", null);
                    }

                    return false;
                }

                if (m_command == EditCommand && m_id == 0)
                {
                    base.Ubs_ShowErrorBox(MsgListEmpty);
                    this.Close();
                    return null;
                }

                InitDoc();
                cmbCommission.Focus();
                return null;
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        #endregion

        #region InitDoc, FillCombos

        private void InitDoc()
        {
            FillCombos();

            if (m_command == AddCommand)
            {
                cmbCommission.SelectedIndex = 0;
                cmbOperation.SelectedIndex = 0;
                cmbValue.SelectedIndex = 0;
                cmbDiv.SelectedIndex = 0;
                cmbCurrency.SelectedIndex = 0;
                cmbValue.Enabled = false;
            }
            else
            {
                base.IUbsChannel.ParamIn("Идентификатор", m_id);
                base.IUbsChannel.Run("Get_Data");

                var paramOut = new UbsParam(base.IUbsChannel.ParamsOut);

                if (paramOut.Contains("Комиссия")) SetComboByKey(cmbCommission, Convert.ToInt32(paramOut.Value("Комиссия")));
                if (paramOut.Contains("Ценность")) SetComboByKey(cmbValue, Convert.ToInt32(paramOut.Value("Ценность")));
                if (paramOut.Contains("Валюта")) SetComboByKey(cmbCurrency, Convert.ToInt32(paramOut.Value("Валюта")));
                if (paramOut.Contains("Касса")) SetComboByKey(cmbDiv, Convert.ToInt32(paramOut.Value("Касса")));
                if (paramOut.Contains("Операция")) SetComboByKey(cmbOperation, Convert.ToInt32(paramOut.Value("Операция")));
            }
        }

        private void FillCombos()
        {
            m_filling = true;
            try
            {
                base.IUbsChannel.Run("Combo_fill");
                var paramOut = new UbsParam(base.IUbsChannel.ParamsOut);

                if (paramOut.Contains("Комиссии"))
                    FillCombo(cmbCommission, paramOut.Value("Комиссии") as Array, null);
                if (paramOut.Contains("Операции"))
                    FillCombo(cmbOperation, paramOut.Value("Операции") as Array, null);
                if (paramOut.Contains("Ценности"))
                    FillCombo(cmbValue, paramOut.Value("Ценности") as Array, "Все");
                if (paramOut.Contains("Кассы"))
                    FillCombo(cmbDiv, paramOut.Value("Кассы") as Array, "Все");
                if (paramOut.Contains("Валюты"))
                    FillCombo(cmbCurrency, paramOut.Value("Валюты") as Array, "Все");
            }
            finally
            {
                m_filling = false;
            }
        }

        private static void FillCombo(ComboBox cmb, Array arr, string allItemText)
        {
            var list = new List<KeyValuePair<int, string>>();

            if (arr != null && arr.Rank == 2)
            {
                int count = arr.GetLength(0);
                for (int n = 0; n < count; n++)
                {
                    int id = Convert.ToInt32(arr.GetValue(n, 0));
                    string text = Convert.ToString(arr.GetValue(n, 1));
                    list.Add(new KeyValuePair<int, string>(id, text));
                }
            }

            if (allItemText != null)
                list.Insert(0, new KeyValuePair<int, string>(0, allItemText));

            cmb.DataSource = list;
            cmb.ValueMember = "Key";
            cmb.DisplayMember = "Value";
            cmb.SelectedIndex = -1;
        }

        #endregion

        #region CheckData

        private bool CheckData()
        {
            if (cmbCommission.SelectedItem == null)
            {
                base.Ubs_ShowErrorBox(MsgCommissionRequired);
                cmbCommission.Focus();
                return false;
            }
            if (cmbOperation.SelectedItem == null)
            {
                base.Ubs_ShowErrorBox(MsgOperationRequired);
                cmbOperation.Focus();
                return false;
            }
            return true;
        }

        #endregion

        #region Helpers

        private static int GetComboKey(ComboBox cmb)
        {
            if (cmb.SelectedItem == null) return 0;
            return ((KeyValuePair<int, string>)cmb.SelectedItem).Key;
        }

        private static void SetComboByKey(ComboBox cmb, int key)
        {
            foreach (object item in cmb.Items)
            {
                if (item is KeyValuePair<int, string> && ((KeyValuePair<int, string>)item).Key == key)
                {
                    cmb.SelectedItem = item;
                    return;
                }
            }
        }

        #endregion
    }
}
