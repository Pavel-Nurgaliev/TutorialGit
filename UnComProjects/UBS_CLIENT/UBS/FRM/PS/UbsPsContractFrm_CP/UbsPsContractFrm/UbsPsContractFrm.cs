using System;
using System.ComponentModel;
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
#pragma warning disable 0414
        private bool m_isInitialized = false;
#pragma warning restore 0414
        private int m_idClient = 0;
        private string m_idKind = string.Empty;
        private string m_strOurBik = string.Empty;
        private int m_idOiDefault = 0;
        private bool m_blnArbitrary = false;
        private bool m_blnMayBeArbitrary = false;
        private bool m_blnIsOurBik = false;
        private bool m_blnIsPublicPayments = true;
        private string m_contractListFilterName = string.Empty;

        #endregion

        /// <summary>Creates the form and registers IUbs handlers, add-fields support, and field collection.</summary>
        public UbsPsContractFrm()
        {
            m_addCommand();

            InitializeComponent();

            base.UbsCtrlFieldsSupportCollection.Add(AddFieldsSupportKey, ucfAdditionalFields);

            this.KeyPreview = true;
            this.AcceptButton = btnSave;
            this.KeyDown += new KeyEventHandler(UbsPsContractFrm_KeyDown);

            base.Ubs_CommandLock = true;

            this.Ubs_ActionRunBegin += new UbsActionRunBeginEventHandler(UbsPsContractFrm_Ubs_ActionRunBegin);
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
                ExecuteSaveContract();
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void btnRecipientClientClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_idClient <= 0)
                {
                    return;
                }
                m_idClient = 0;
                txtRecipientClient.Text = string.Empty;
                ClearBankFields();
                EnableFieldsCl();
                SetSignOurBik();
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void linkPaymentKind_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                OpenBrowseKindPaymentList();
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void linkRecipientBankClient_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                OpenBrowseClientList();
            }
            catch (Exception ex)
            {
                this.Ubs_ShowError(ex);
            }
        }

        private void linkRecipientClient_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                OpenBrowseAccountList();
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
                bool isEdit = string.Equals(m_command, EditCommand, StringComparison.Ordinal);
                bool isDelete = string.Equals(m_command, DelCommand, StringComparison.Ordinal);

                m_idContract = isRecordsExist && isEdit ? Convert.ToInt32(((object[])param_in)[0]) : 0;

                this.IUbsChannel.LoadResource = LoadResource;

                if (isDelete)
                {
                    if (!isRecordsExist)
                    {
                        base.Ubs_ShowErrorBox("Не выбраны записи для удаления.");

                        return false;
                    }

                    if (MessageBox.Show("Вы уверены что хотите удалить выделенные записи?", "Удаление записей", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        base.IUbsChannel.ParamIn("varContract", param_in);
                        base.IUbsChannel.Run("DelContractByParam");

                        if (base.IUbsChannel.ExistParamOut("Error"))
                        {
                            base.Ubs_ShowMsg(Convert.ToString(base.IUbsChannel.ParamOut("Error")));
                        }

                        IUbs iubs = Control.FromHandle((IntPtr)base.IUbs.Run("ParentHandle", null)) as IUbs;
                        if (iubs != null && iubs.ExistName("RefreshGrid")) iubs.Run("RefreshGrid", null);
                    }

                    return false;
                }

                if (isEdit && m_idContract == 0)
                {
                    this.Ubs_ShowErrorBox(MsgNoContractSelected);
                    return false;
                }

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

        private void txtRecipientInn_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string inn = txtRecipientInn.Text.Trim().TrimStart('\'');

            if (string.IsNullOrEmpty(inn))
            {
                base.Ubs_ShowErrorBox(InnIsEmptyErrorMessage);
                e.Cancel = true;
                return;
            }

            if (!IsDigitsOnly(inn))
            {
                base.Ubs_ShowErrorBox(InnIsDigitsOnlyErrorMessage);
                e.Cancel = true;
                return;
            }

            if (inn.Length > 12)
            {
                base.Ubs_ShowErrorBox(InnIsLessTwelveDigitsOnlyErrorMessage);
                e.Cancel = true;
                return;
            }
        }

        private bool IsDigitsOnly(string value)
        {
            foreach (char c in value)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        private void txtRecipientBik_Validating(object sender, CancelEventArgs e)
        {
            string bik = txtRecipientBik.Text.Trim().TrimStart('\'');

            if (string.IsNullOrEmpty(bik))
            {
                base.Ubs_ShowErrorBox(BikIsEmptyErrorMessage);
                e.Cancel = true;
                return;
            }

            if (!IsDigitsOnly(bik))
            {
                base.Ubs_ShowErrorBox(BikIsDigitsOnlyErrorMessage);
                e.Cancel = true;
                return;
            }

            if (bik.Length != 9)
            {
                base.Ubs_ShowErrorBox(BikIsNineDigitsOnlyErrorMessage);
                e.Cancel = true;
                return;
            }
        }
    }
}
