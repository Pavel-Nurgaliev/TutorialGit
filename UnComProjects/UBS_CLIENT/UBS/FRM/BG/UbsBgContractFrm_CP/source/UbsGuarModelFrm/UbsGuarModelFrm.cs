using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// Шаблон класса формы
    /// </summary>
    public partial class UbsGuarModelFrm : UbsFormBase
    {
        private const string NameOperation = "Типовой договор обеспечения";
        private const string EditCommand = "Edit";
        private const string DeleteCommand = "Delete";
        private const string DataCheckingMessage = "Проверка корректности данных";

        #region Блок объявления переменных

        private string m_command = "";    //параметер запуска формы
        private object[,] m_models = null;
        private int m_idObject = 0;
        bool m_isModelInit = false;

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public UbsGuarModelFrm()
        {
            m_addCommand();

            InitializeComponent();

            base.UbsCtrlFieldsSupportCollection.Add("Доп. поля", ubsCtrlAddFields);

            base.Ubs_CommandLock = true;
        }


        #region Обработчики событий кнопок (с примерами)

        private void btnExit_Click(object sender, EventArgs e) { this.Close(); }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren()) { return; }

                if (!Check())
                {
                    return;
                }

                if (!IsAddFieldFilled("Тип счета учета"))
                {
                    MessageBox.Show("Некорректное заполнение доп.поля 'Тип счета учета'!", DataCheckingMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                base.UbsChannel_ParamIn("StrCommand", m_command);
                base.UbsChannel_ParamIn("Наименование", tbName.Text);
                base.UbsChannel_ParamIn("Шаблон", m_models[cbModel.SelectedIndex, 1]);
                base.UbsChannel_ParamIn("ОИ", cbExecutor.SelectedValue);
                base.UbsChannel_ParamIn("Состояние", cbState.SelectedValue);
                base.UbsChannel_ParamIn("Тип клиента", cbClientType.SelectedValue);

                base.UbsChannel_Run("GuarModelEdit");

                if (base.UbsChannel_ExistParamOut("Error"))
                {
                    MessageBox.Show((string)base.UbsChannel_ParamOut("Error"), NameOperation, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    btnSave.Enabled = true;
                    btnExit.Enabled = true;

                    return;
                }

                m_command = EditCommand;

                ubsCtrlInfo.Show("Данные сохранены!");

                cbModel.Enabled = false;

                btnSave.Enabled = true;
                btnExit.Enabled = true;

                btnExit.Focus();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private bool IsAddFieldFilled(string nameField)
        {
            if (Convert.ToString(ubsCtrlAddFields.Collection[nameField].Value) == string.Empty)
            {
                return false;
            }

            return true;
        }

        private bool Check()
        {
            if (tbName.Text.Length < 1)
            {
                MessageBox.Show("Задайте наименование.", NameOperation, MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnSave.Enabled = true;
                btnExit.Enabled = true;

                tabControl1.SelectedTab = tabControl1.TabPages[0];

                tbName.Focus();

                return false;
            }

            return true;
        }

        #endregion


        #region Обработчики команд IUbs интерфейса (с примерами)

        /// <summary>
        /// Процедура регистрации обработчиков команд интерфейса IUbs в базовом классе
        /// </summary>
        private void m_addCommand()
        {
            base.Ubs_AddName(new UbsDelegate(CommandLine));
            base.Ubs_AddName(new UbsDelegate(ListKey));
        }
        /// <summary>
        /// Процедура обработки команды CommandLine
        /// </summary>
        /// <param name="param_in">Входной параметер</param>
        /// <param name="param_out">Выходной параметер</param>
        /// <returns></returns>
        private object CommandLine(object param_in, ref object param_out)
        {
            m_command = (string)param_in;

            return null;
        }

        /// <summary>
        /// Процедура обработки команды ListKey
        /// </summary>
        /// <param name="param_in">Входной параметер</param>
        /// <param name="param_out">Выходной параметер</param>
        /// <returns></returns>
        private object ListKey(object param_in, ref object param_out)
        {
            try
            {
                m_idObject = param_in != null && param_in is object[]? Convert.ToInt32(((object[])param_in)[0]) : 0;

                if (m_command == DeleteCommand)
                {
                    if (m_idObject == 0)
                    {
                        base.Ubs_ShowErrorBox("Не выбраны записи для удаления!");
                        return false;
                    }

                    if (MessageBox.Show("Вы уверены что хотите удалить выделенные записи?", "Удаление записей", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        base.IUbsChannel.LoadResource = "VBS:UBS_VBS\\SYSTEM\\del_simple_object_channel.vbs";
                        base.IUbsChannel.ParamIn("KeyArray", param_in);
                        base.IUbsChannel.ParamIn("ProgId", "UbsGuarantyModel");
                        base.IUbsChannel.Run("DeleteInstances");

                        IUbs iubs = Control.FromHandle((IntPtr)base.IUbs.Run("ParentHandle", null)) as IUbs;
                        if (iubs != null && iubs.ExistName("RefreshGrid")) iubs.Run("RefreshGrid", null);
                    }

                    return false;
                }

                this.IUbsChannel.LoadResource = "VBS:UBS_VBD\\GUAR\\GuarModel.vbs";

                InitForm();

                ubsCtrlAddFields.Refresh();

                if (m_command == EditCommand)
                {
                    tbName.Enabled = false;

                    cbExecutor.Focus();
                }
                else
                {
                    tbName.Focus();
                }

                return null;
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }
        private void InitForm()
        {
            base.IUbsChannel.Run("GuarModelInit");

            m_isModelInit = true;

            if (base.IUbsChannel.ExistParamOut("Шаблоны"))
            {
                var models = base.IUbsChannel.ParamOut("Шаблоны") as object[,];

                InitModels(models);
            }
            else
            {
                MessageBox.Show("Массив шаблонов пуст!", NameOperation, MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnExit_Click(btnExit, EventArgs.Empty);

                return;
            }

            var arrTypeClient = base.IUbsChannel.ParamOut("Типы клиентов") as object[,];

            m_isModelInit = false;

            if (base.IUbsChannel.ExistParamOut("ОИ") && IsNotNullArray(base.IUbsChannel.ParamOut("ОИ"), out object[,] arrExecut))
            {
                List<KeyValuePair<short, string>> list = new List<KeyValuePair<short, string>>();
                for (int i = 0; i < arrExecut.GetLength(0); i++)
                    list.Add(new KeyValuePair<short, string>((short)arrExecut[i, 0], (string)arrExecut[i, 1]));

                InitComboBox(cbExecutor, list);
            }

            if (base.IUbsChannel.ExistParamOut("Состояния") && IsNotNullArray(base.IUbsChannel.ParamOut("Состояния"), out object[,] arrState))
            {
                List<KeyValuePair<short, string>> list = new List<KeyValuePair<short, string>>();
                for (int i = 0; i < arrState.GetLength(0); i++)
                    list.Add(new KeyValuePair<short, string>((short)arrState[i, 0], (string)arrState[i, 1]));

                InitComboBox(cbState, list);
            }
            else
            {
                MessageBox.Show("Массив состояний пуст!", NameOperation, MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnExit_Click(btnExit, EventArgs.Empty);

                return;
            }

            base.UbsInit();

            cbModel_SelectedIndexChanged(cbModel, EventArgs.Empty);

            short clientType = 0;
            if (m_command == EditCommand)
            {
                base.UbsChannel_ParamIn("Id", m_idObject);

                base.UbsChannel_Run("GuarModelRead");

                tbName.Text = Convert.ToString(base.UbsChannel_ParamOut("Наименование"));
                cbExecutor.SelectedValue = Convert.ToInt16(base.UbsChannel_ParamOut("ОИ"));
                cbState.SelectedValue = Convert.ToInt16(base.UbsChannel_ParamOut("Состояние"));

                if (base.UbsChannel_ExistParamOut("Тип клиента"))
                {
                    clientType = Convert.ToInt16(base.UbsChannel_ParamOut("Тип клиента"));
                }

                for (int i = 0; i < m_models.GetLength(0); i++)
                {
                    if ((string)m_models[i, 1] == (string)base.UbsChannel_ParamOut("Шаблон"))
                    {
                        cbModel.SelectedIndex = i;
                    }
                }

                cbModel.Enabled = false;
            }

            if (!CheckRights())
            {
                return;
            }

            InitCbClientType(arrTypeClient, clientType);
        }

        private bool CheckRights()
        {
            base.IUbsChannel.ParamIn("Идентификатор договора", m_idObject);
            base.IUbsChannel.ParamIn("Идентификатор ОИ", Convert.ToInt16(cbExecutor.SelectedValue));

            base.IUbsChannel.Run("GuarModelCheckRights");

            int accessRight = 0;

            if (base.IUbsChannel.ExistParamOut("Права"))
            {
                accessRight = Convert.ToInt32(base.IUbsChannel.ParamOut("Права"));

                if ((accessRight & 4) == 0)
                {
                    if ((accessRight & 2) == 0)
                        accessRight = -2;
                    else
                        accessRight = -1;
                }

                if (m_command == EditCommand)
                {
                    if (accessRight == -2)
                    {
                        MessageBox.Show("Для <Обеспечения - типовой договор> нет прав на <Просмотр>!", "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        this.Close();

                        return false;
                    }
                    if (accessRight == -1)
                    {
                        ubsCtrlInfo.Visible = true;
                        ubsCtrlInfo.Text = "Нет прав на редактирование. Форма запущена в режиме просмотра.";
                        btnSave.Visible = false;
                    }
                }
                else
                {
                    if (accessRight == -1)
                    {
                        MessageBox.Show("Для <Обеспечения - типовой договор> нет прав на <Редактирование>!", "Доступ запрещен", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        this.Close();

                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsNotNullArray(object inputValue, out object[,] arrayOutput)
        {
            arrayOutput = null;

            if (inputValue is null)
            {
                return false;
            }

            arrayOutput = inputValue as object[,];

            return true;
        }

        private void InitCbClientType(object[,] arrTypeClient, short clientType)
        {
            if (m_command == EditCommand && clientType == 0)
            {
                var arrTypeClientAdditionalRow = new object[,] { { 0, "" } };
                arrTypeClient = ConcatArrays(arrTypeClientAdditionalRow, arrTypeClient);
            }

            if (arrTypeClient.GetLength(0) > 0)
            {
                List<KeyValuePair<short, string>> list = new List<KeyValuePair<short, string>>();
                for (int i = 0; i < arrTypeClient.GetLength(0); i++)
                    list.Add(new KeyValuePair<short, string>(Convert.ToInt16(arrTypeClient[i, 0]), (string)arrTypeClient[i, 1]));

                InitComboBox(cbClientType, list);
            }
            else
            {
                MessageBox.Show("Массив типов клиента пуст!", NameOperation, MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnExit_Click(btnExit, EventArgs.Empty);

                return;

            }

            cbClientType.SelectedValue = clientType;
        }

        private void InitComboBox(ComboBox cmb, object list)
        {
            cmb.DataSource = list;
            cmb.ValueMember = "Key";
            cmb.DisplayMember = "Value";
            cmb.SelectedIndex = 0;
        }
        private void InitModels(object[,] models)
        {
            m_models = new object[models.GetLength(0), 2];

            for (int i = 0; i < models.GetLength(0); i++)
            {
                cbModel.Items.Add(models[i, 1]);

                m_models[i, 0] = i;
                m_models[i, 1] = models[i, 0];
            }

            cbModel.SelectedIndex = 0;
        }


        #endregion


        private void cbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_isModelInit && cbModel != null)
            {
                base.UbsChannel_ParamIn("Шаблон", m_models[cbModel.SelectedIndex, 1]);

                base.UbsChannel_Run("GuarModelInitUcp");
            }
        }

        private object[,] ConcatArrays(object[,] firstArray, object[,] secondArray)
        {
            int rows1 = firstArray.GetLength(0);
            int cols1 = firstArray.GetLength(1);
            int rows2 = secondArray.GetLength(0);
            int cols2 = secondArray.GetLength(1);

            if (cols1 != cols2)
                throw new ArgumentException("Массивы должны иметь одинаковое число столбцов.");

            object[,] result = new object[rows1 + rows2, cols1];

            for (int i = 0; i < rows1; i++)
                for (int j = 0; j < cols1; j++)
                    result[i, j] = firstArray[i, j];

            for (int i = 0; i < rows2; i++)
                for (int j = 0; j < cols2; j++)
                    result[rows1 + i, j] = secondArray[i, j];

            return result;
        }

        private void UbsGuarModelFrm_Shown(object sender, EventArgs e)
        {
            tbName.Focus();
        }
    }
}