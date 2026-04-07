using System;
using UbsService;

namespace UbsBusiness
{
    public partial class UbsPsUtPaymentGroupFrm : UbsFormBase
    {
        #region Fields

        private string m_command = string.Empty;

        #endregion

        public UbsPsUtPaymentGroupFrm()
        {
            m_addCommand();

            InitializeComponent();

            this.IUbsChannel.LoadResource = LoadResource;

            base.Ubs_CommandLock = true;
        }

        #region Button Handlers

        private void btnExit_Click(object sender, EventArgs e) { this.Close(); }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ValidateChildren()) { return; }
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
            m_command = (string)param_in;
            return null;
        }

        private object ListKey(object param_in, ref object param_out)
        {
            try
            {
                return null;
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        #endregion
    }
}
