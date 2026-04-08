using System;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    public partial class UbsPsUtPaymentGroupFrm : UbsFormBase
    {
        #region Fields

        private string m_command = string.Empty;
        private int m_idPayment;
        private int m_idContract;
        private int m_idClient;
        private bool m_blnGuest;
        private bool m_blnEndGroup;
        private bool m_blnArbitrary;
        private bool m_blnSave;
        private bool m_blnClickSave;
        private bool m_blnNoMessage = true;
        private bool m_blnChangeContract;
        private string m_strAddress = string.Empty;
        private string m_strTypeDoc = string.Empty;
        private string m_strDocNumber = string.Empty;
        private string m_strDocSeries = string.Empty;
        private int m_idAttributeRecip;
        private int m_intState;
        private object[] m_arrRateSend;
        private object[,] m_groupContract;
        private bool m_blnTerror;
        private bool m_blnIPDL;
        private bool m_blnNeedRefreshGrid;

        #endregion

        public UbsPsUtPaymentGroupFrm()
        {
            m_addCommand();

            InitializeComponent();

            this.IUbsChannel.LoadResource = LoadResource;

            base.UbsCtrlFieldsSupportCollection.Add(AddFieldsSupportKey, ucfAddProperties);

            base.Ubs_CommandLock = true;
        }

        #region Button Handlers

        private void btnExit_Click(object sender, EventArgs e) { this.Close(); }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSave_ClickImpl();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void linkFIO_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                OpenClientPicker();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void OpenClientPicker()
        {
        }

        private void btnListAttributeRecip_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void btnSaveAttribute_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSaveAttribute_ClickImpl();
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region ComboBox Handlers

        private void cmbCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        #endregion

        #region IUbs Command Handlers

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

                m_idContract = 0;
                m_blnChangeContract = false;
                m_idPayment = 0;

                if (!string.Equals(m_command, StrCommandAdd, StringComparison.Ordinal))
                {
                    if (isRecordsExist)
                    {
                        m_idPayment = Convert.ToInt32(((object[])param_in)[0]);
                    }
                    else
                    {
                        MessageBox.Show(MsgGroupPaymentListEmpty, CaptionError,
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                        this.Close();

                        return null;
                    }
                }

                InitDoc();

                if (txtFIOPay.Enabled)
                {
                    txtFIOPay.Focus();
                }

                return null;
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        #endregion
    }
}
