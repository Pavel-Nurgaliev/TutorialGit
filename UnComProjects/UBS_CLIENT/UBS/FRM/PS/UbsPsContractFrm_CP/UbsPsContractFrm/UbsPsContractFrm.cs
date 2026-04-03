using System;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// PS Contract — конвертация из Contract.dob (VB6).
    /// </summary>
    public partial class UbsPsContractFrm : UbsFormBase
    {
        #region Блок объявления переменных

        private string m_command = string.Empty;
        private int m_idContract = 0;
        private bool m_isInitialized = false;
        private int m_idClient = 0;
        private string m_idKind = string.Empty;
        private string m_strOurBik = string.Empty;
        private int m_nIdOiDefault = 0;
        private bool m_blnArbitrary = false;
        private bool m_blnMayBeArbitrary = false;
        private bool m_blnIsPublicPayments = true;

        #endregion

        /// <summary>Creates the form and registers IUbs handlers, add-fields support, and field collection.</summary>
        public UbsPsContractFrm()
        {
            m_addCommand();

            InitializeComponent();

            this.IUbsChannel.LoadResource = LoadResource;

            base.UbsCtrlFieldsSupportCollection.Add(AddFieldsSupportKey, ucfAdditionalFields);

            this.KeyPreview = true;
            this.AcceptButton = btnSave;

            base.Ubs_CommandLock = true;
        }

        #region Обработчики событий кнопок

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren())
                {
                    return;
                }
                this.uciContract.Text = MsgSaveNotImplemented;
                this.uciContract.Show();
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void btnRecipientClient_Click(object sender, EventArgs e)
        {
            this.uciContract.Text = MsgBrowseNotImplemented;
            this.uciContract.Show();
        }

        private void btnRecipientAccount_Click(object sender, EventArgs e)
        {
            this.uciContract.Text = MsgBrowseNotImplemented;
            this.uciContract.Show();
        }

        private void btnPaymentKind_Click(object sender, EventArgs e)
        {
            this.uciContract.Text = MsgBrowseNotImplemented;
            this.uciContract.Show();
        }

        private void btnRecipientClientClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearBankFields();
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        #endregion

        #region Обработчики команд IUbs интерфейса

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
                bool isRecordsExist = (param_in != null && param_in is object[] && ((object[])param_in).Length > 0);
                m_idContract = isRecordsExist ? Convert.ToInt32(((object[])param_in)[0]) : 0;

                if (string.Equals(m_command, CmdEdit, StringComparison.Ordinal) && m_idContract == 0)
                {
                    this.Ubs_ShowErrorBox(MsgNoContractSelected);
                    return false;
                }

                this.IUbsChannel.LoadResource = LoadResource;

                InitDoc();

                m_isInitialized = true;

                this.tabContract.SelectedTab = this.tabPageMain;
                if (this.txtContractCode.Enabled)
                {
                    this.txtContractCode.Focus();
                }
                else
                {
                    this.txtContractNumber.Focus();
                }

                return null;
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
                return null;
            }
        }

        #endregion
    }
}
