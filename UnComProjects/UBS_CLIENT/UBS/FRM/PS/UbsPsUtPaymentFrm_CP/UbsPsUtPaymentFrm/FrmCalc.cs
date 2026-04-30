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
                MessageBox.Show("Сумма платежа нулевая! Нажмите кнопку Выход и еще раз кнопку Расчет", "Платеж", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal changeValue = cashValue - PaymentAmount;
            if (changeValue < 0m)
            {
                MessageBox.Show("Недостаток суммы наличных!", "Платеж",
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
