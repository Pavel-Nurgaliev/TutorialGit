using System;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// Форма комиссии (OP Commission). Конвертирована из Commission_ud (VB6 UserDocument).
    /// </summary>
    public partial class UbsOpCommissionFrm : UbsFormBase
    {
        #region Блок объявления переменных

        private string m_command = string.Empty;
        private int m_id = 0;
        private bool m_isInitialized = false;

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public UbsOpCommissionFrm()
        {
            m_addCommand();

            InitializeComponent();

            base.UbsCtrlFieldsSupportCollection.Add("Доп. поля", ubsCtrlAddFields);

            base.Ubs_CommandLock = true;
        }

        #region Обработчики событий кнопок

        private void btnExit_Click(object sender, EventArgs e) { this.Close(); }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren()) { return; }
                if (!CheckData()) { return; }

                base.UbsChannel_ParamIn("Действие", m_command);
                base.UbsChannel_ParamIn("Наименование", txtName.Text.Trim());
                base.UbsChannel_ParamIn("Описание", txtDesc.Text.Trim());

                base.UbsChannel_Run("Com_Save");

                m_command = EditCommand;
                ubsCtrlInfo.Show(MsgDataSaved);
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region Обработчики команд IUbs

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

                if (m_command == DeleteCommand)
                {
                    if (m_id == 0)
                    {
                        base.Ubs_ShowErrorBox(RecordsIsNotChosen);
                        return false;
                    }

                    if (MessageBox.Show(AreYouSureAboutDeletingRecords, MsgDeletingRecords, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        base.IUbsChannel.LoadResource = DelSimpleObjectLoadResource;
                        base.IUbsChannel.ParamIn("KeyArray", param_in);
                        base.IUbsChannel.ParamIn("ProgId", "UbsGuarantyModel");
                        base.IUbsChannel.Run("DeleteInstances");

                        IUbs iubs = Control.FromHandle((IntPtr)base.IUbs.Run("ParentHandle", null)) as IUbs;
                        if (iubs != null && iubs.ExistName("RefreshGrid")) iubs.Run("RefreshGrid", null);
                    }

                    return false;
                }

                this.IUbsChannel.LoadResource = LoadResource;

                if (m_command == EditCommand && m_id == 0)
                {
                    base.Ubs_ShowErrorBox(MsgCommissionListEmpty);
                    this.Close();
                    return false;
                }

                InitDoc();
                ubsCtrlAddFields.Refresh();
                m_isInitialized = true;

                tabControl.SelectedTab = tabPageMain;
                txtName.Focus();

                return null;
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        #endregion

        #region InitDoc

        private void InitDoc()
        {
            try
            {
                //первое обращение к серверной части формы (необходимо для инициализации доп полей объекта)
                base.IUbsChannel.Run("InitForm");

                base.UbsInit();

                if (m_command == EditCommand)
                {
                    base.UbsChannel_ParamIn("Идентификатор", m_id);
                    base.UbsChannel_Run("Get_Data");

                    var paramOut = new UbsParam(base.UbsChannel_ParamsOut);

                    if (paramOut.Contains("Наименование"))
                        txtName.Text = Convert.ToString(paramOut.Value("Наименование"));
                    if (paramOut.Contains("Описание"))
                        txtDesc.Text = Convert.ToString(paramOut.Value("Описание"));
                }
                else
                {
                    txtName.Text = string.Empty;
                    txtDesc.Text = string.Empty;
                }

                ubsCtrlAddFields.Refresh();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region CheckData

        private bool CheckData()
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                base.Ubs_ShowErrorBox(MsgNameRequired);
                tabControl.SelectedTab = tabPageMain;
                txtName.Focus();
                return false;
            }
            return true;
        }

        #endregion

    }
}
