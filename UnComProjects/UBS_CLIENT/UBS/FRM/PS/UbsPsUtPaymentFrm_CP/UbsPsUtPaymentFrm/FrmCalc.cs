using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    public partial class FrmCalc : Form
    {
        public decimal PaymentAmount { get; set; }
        public decimal CashAmount { get; private set; }
        public decimal ChangeAmount { get; private set; }
        public bool IsConfirmed { get; private set; }

        public FrmCalc()
        {
            InitializeComponent();
        }

        private void FrmCalc_Load(object sender, EventArgs e)
        {
            lblPaymentAmountValue.Text = FormatMoney(PaymentAmount);
            lblChangeAmountValue.Text = FormatMoney(0m);
            udcCashAmount.Text = FormatMoney(0m);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            TryConfirm();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            IsConfirmed = false;
            this.DialogResult = DialogResult.Cancel;
            this.btnExit_Click(this, EventArgs.Empty);
        }

        private void udcCashAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                UpdateChangePreview();
                btnCalculate.Focus();
            }
        }

        private void UpdateChangePreview()
        {
            decimal cashValue = GetCashAmountValue();
            decimal changeValue = cashValue - PaymentAmount;
            lblChangeAmountValue.Text = FormatMoney(changeValue < 0m ? 0m : changeValue);
        }

        private void TryConfirm()
        {
            decimal cashValue = GetCashAmountValue();

            if (PaymentAmount == 0m)
            {
                MessageBox.Show("\u0421\u0443\u043c\u043c\u0430 \u043f\u043b\u0430\u0442\u0435\u0436\u0430 \u043d\u0443\u043b\u0435\u0432\u0430\u044f! \u041d\u0430\u0436\u043c\u0438\u0442\u0435 \u043a\u043d\u043e\u043f\u043a\u0443 \u0412\u044b\u0445\u043e\u0434 \u0438 \u0435\u0449\u0435 \u0440\u0430\u0437 \u043a\u043d\u043e\u043f\u043a\u0443 \u0420\u0430\u0441\u0447\u0435\u0442",
                    "\u041f\u043b\u0430\u0442\u0435\u0436", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal changeValue = cashValue - PaymentAmount;
            if (changeValue < 0m)
            {
                MessageBox.Show("\u041d\u0435\u0434\u043e\u0441\u0442\u0430\u0442\u043e\u043a \u0441\u0443\u043c\u043c\u044b \u043d\u0430\u043b\u0438\u0447\u043d\u044b\u0445!", "\u041f\u043b\u0430\u0442\u0435\u0436",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CashAmount = cashValue;
            ChangeAmount = changeValue;
            IsConfirmed = true;
            lblChangeAmountValue.Text = FormatMoney(ChangeAmount);
            this.DialogResult = DialogResult.OK;
            this.btnExit_Click(this, EventArgs.Empty);
        }

        private decimal GetCashAmountValue()
        {
            try
            {
                return udcCashAmount.DecimalValue;
            }
            catch (Exception)
            {
                decimal parsed;
                if (decimal.TryParse(udcCashAmount.Text, out parsed))
                {
                    return parsed;
                }

                return 0m;
            }
        }

        private static string FormatMoney(decimal value)
        {
            return value.ToString("0.00");
        }
    }
}
