namespace UbsBusiness
{
    partial class FrmCalc
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblAcceptedPaymentsAmount = new System.Windows.Forms.Label();
            this.lblCashAmount = new System.Windows.Forms.Label();
            this.lblChangeAmount = new System.Windows.Forms.Label();
            this.lblPaymentAmountValue = new System.Windows.Forms.Label();
            this.lblChangeAmountValue = new System.Windows.Forms.Label();
            this.udcCashAmount = new UbsControl.UbsCtrlDecimal();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAcceptedPaymentsAmount
            // 
            this.lblAcceptedPaymentsAmount.AutoSize = true;
            this.lblAcceptedPaymentsAmount.Location = new System.Drawing.Point(12, 20);
            this.lblAcceptedPaymentsAmount.Name = "lblAcceptedPaymentsAmount";
            this.lblAcceptedPaymentsAmount.Size = new System.Drawing.Size(149, 13);
            this.lblAcceptedPaymentsAmount.TabIndex = 0;
            this.lblAcceptedPaymentsAmount.Text = "\u0421\u0443\u043c\u043c\u0430 \u043f\u0440\u0438\u043d\u044f\u0442\u044b\u0445 \u043f\u043b\u0430\u0442\u0435\u0436\u0435\u0439";
            // 
            // lblCashAmount
            // 
            this.lblCashAmount.AutoSize = true;
            this.lblCashAmount.Location = new System.Drawing.Point(12, 56);
            this.lblCashAmount.Name = "lblCashAmount";
            this.lblCashAmount.Size = new System.Drawing.Size(93, 13);
            this.lblCashAmount.TabIndex = 1;
            this.lblCashAmount.Text = "\u0421\u0443\u043c\u043c\u0430 \u043d\u0430\u043b\u0438\u0447\u043d\u044b\u0445";
            // 
            // lblChangeAmount
            // 
            this.lblChangeAmount.AutoSize = true;
            this.lblChangeAmount.Location = new System.Drawing.Point(12, 92);
            this.lblChangeAmount.Name = "lblChangeAmount";
            this.lblChangeAmount.Size = new System.Drawing.Size(40, 13);
            this.lblChangeAmount.TabIndex = 2;
            this.lblChangeAmount.Text = "\u0421\u0434\u0430\u0447\u0430";
            // 
            // lblPaymentAmountValue
            // 
            this.lblPaymentAmountValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPaymentAmountValue.Location = new System.Drawing.Point(210, 15);
            this.lblPaymentAmountValue.Name = "lblPaymentAmountValue";
            this.lblPaymentAmountValue.Size = new System.Drawing.Size(180, 23);
            this.lblPaymentAmountValue.TabIndex = 3;
            this.lblPaymentAmountValue.Text = "0.00";
            this.lblPaymentAmountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblChangeAmountValue
            // 
            this.lblChangeAmountValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChangeAmountValue.Location = new System.Drawing.Point(210, 87);
            this.lblChangeAmountValue.Name = "lblChangeAmountValue";
            this.lblChangeAmountValue.Size = new System.Drawing.Size(180, 23);
            this.lblChangeAmountValue.TabIndex = 5;
            this.lblChangeAmountValue.Text = "0.00";
            this.lblChangeAmountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // udcCashAmount
            // 
            this.udcCashAmount.Location = new System.Drawing.Point(210, 51);
            this.udcCashAmount.Name = "udcCashAmount";
            this.udcCashAmount.Size = new System.Drawing.Size(180, 20);
            this.udcCashAmount.TabIndex = 4;
            this.udcCashAmount.Text = "0.00";
            this.udcCashAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.udcCashAmount_KeyPress);
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(96, 138);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(121, 28);
            this.btnCalculate.TabIndex = 6;
            this.btnCalculate.Text = "\u0420\u0430\u0441\u0447\u0435\u0442";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(248, 138);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(121, 28);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "\u0412\u044b\u0445\u043e\u0434";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FrmCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 182);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.udcCashAmount);
            this.Controls.Add(this.lblChangeAmountValue);
            this.Controls.Add(this.lblPaymentAmountValue);
            this.Controls.Add(this.lblChangeAmount);
            this.Controls.Add(this.lblCashAmount);
            this.Controls.Add(this.lblAcceptedPaymentsAmount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCalc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "\u0420\u0430\u0441\u0447\u0435\u0442 \u0441 \u043a\u043b\u0438\u0435\u043d\u0442\u043e\u043c";
            this.Load += new System.EventHandler(this.FrmCalc_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAcceptedPaymentsAmount;
        private System.Windows.Forms.Label lblCashAmount;
        private System.Windows.Forms.Label lblChangeAmount;
        private System.Windows.Forms.Label lblPaymentAmountValue;
        private System.Windows.Forms.Label lblChangeAmountValue;
        private UbsControl.UbsCtrlDecimal udcCashAmount;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnExit;
    }
}
