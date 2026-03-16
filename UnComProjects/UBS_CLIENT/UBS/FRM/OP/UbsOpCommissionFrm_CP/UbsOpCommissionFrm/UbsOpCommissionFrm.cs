using System;
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

            this.IUbsChannel.LoadResource = LoadResource;

            base.UbsCtrlFieldsSupportCollection.Add("Доп. поля", ubsCtrlAddFields);

            m_addFields();

            this.Load += UbsOpCommissionFrm_Load;

            base.Ubs_CommandLock = true;
        }

        private void UbsOpCommissionFrm_Load(object sender, EventArgs e)
        {
            if (!m_isInitialized)
            {
                InitDoc();
                ubsCtrlAddFields.Refresh();
                m_isInitialized = true;
            }
        }

        #region Обработчики событий кнопок

        private void btnExit_Click(object sender, EventArgs e) { this.Close(); }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren()) { return; }
                if (!CheckData()) { return; }

                base.IUbsChannel.ParamIn(ParamAction, m_command);
                base.IUbsChannel.ParamIn(ParamName, txbName.Text.Trim());
                base.IUbsChannel.ParamIn(ParamDesc, txbDesc.Text.Trim());

                base.IUbsChannel.Run(ComSaveAction);

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
            m_command = param_in != null ? Convert.ToString(param_in) : "";
            return null;
        }

        private object ListKey(object param_in, ref object param_out)
        {
            try
            {
                m_id = (param_in != null && param_in is object[] && ((object[])param_in).Length > 0)
                    ? Convert.ToInt32(((object[])param_in)[0])
                    : 0;

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
                txbName.Focus();

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
                if (m_command == EditCommand)
                {
                    base.IUbsChannel.ParamIn(ParamId, m_id);
                    base.IUbsChannel.Run(GetDataAction);

                    if (base.IUbsChannel.ExistParamOut(ParamName))
                        txbName.Text = Convert.ToString(base.IUbsChannel.ParamOut(ParamName));
                    if (base.IUbsChannel.ExistParamOut(ParamDesc))
                        txbDesc.Text = Convert.ToString(base.IUbsChannel.ParamOut(ParamDesc));
                }
                else
                {
                    txbName.Text = "";
                    txbDesc.Text = "";
                }

                ubsCtrlAddFields.Refresh();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region CheckData

        private bool CheckData()
        {
            if (string.IsNullOrEmpty(txbName.Text.Trim()))
            {
                base.Ubs_ShowErrorBox(MsgNameRequired);
                tabControl.SelectedTab = tabPageMain;
                txbName.Focus();
                return false;
            }
            return true;
        }

        #endregion

        #region m_addFields

        private void m_addFields()
        {
            base.IUbsFieldCollection.Add(ParamName, new UbsFormField(txbName, "Text"));
            base.IUbsFieldCollection.Add(ParamDesc, new UbsFormField(txbDesc, "Text"));
        }

        #endregion
    }
}
