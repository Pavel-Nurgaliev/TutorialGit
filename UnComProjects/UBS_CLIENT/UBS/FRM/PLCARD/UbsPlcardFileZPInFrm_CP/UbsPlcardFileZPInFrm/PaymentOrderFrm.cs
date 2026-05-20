using System;
using System.Windows.Forms;
using UbsService;

namespace UbsPlcardFileZPInFrm
{
    public partial class PaymentOrderFrm : UbsFormBase
    {
        public PaymentOrderFrm()
        {
            InitializeComponent();
        }
        public ListView LvwPayment { get => this.lvwPayment; }
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void lvwPayment_DoubleClick(object sender, EventArgs e)
        {
            if (this.lvwPayment.SelectedItems.Count >= 0)
            {
                btnSave_Click(this, EventArgs.Empty);
            }
        }
    }
}
