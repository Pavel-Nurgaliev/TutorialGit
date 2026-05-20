namespace UbsBusiness
{
    partial class FrmCashSymb
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
            this.grdCashSymbols = new System.Windows.Forms.DataGridView();
            this.colCashSymbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uciHelp = new UbsControl.UbsCtrlInfo();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdCashSymbols)).BeginInit();
            this.SuspendLayout();
            // 
            // grdCashSymbols
            // 
            this.grdCashSymbols.AllowUserToResizeRows = false;
            this.grdCashSymbols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCashSymbols.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCashSymbol,
            this.colAmount});
            this.grdCashSymbols.Location = new System.Drawing.Point(12, 12);
            this.grdCashSymbols.Name = "grdCashSymbols";
            this.grdCashSymbols.Size = new System.Drawing.Size(458, 284);
            this.grdCashSymbols.TabIndex = 0;
            // 
            // colCashSymbol
            // 
            this.colCashSymbol.HeaderText = "Кассовый символ";
            this.colCashSymbol.Name = "colCashSymbol";
            this.colCashSymbol.Width = 160;
            // 
            // colAmount
            // 
            this.colAmount.HeaderText = "Сумма";
            this.colAmount.Name = "colAmount";
            this.colAmount.Width = 180;
            // 
            // uciHelp
            // 
            this.uciHelp.AutoSize = true;
            this.uciHelp.Interval = 500;
            this.uciHelp.Location = new System.Drawing.Point(12, 304);
            this.uciHelp.Name = "uciHelp";
            this.uciHelp.Size = new System.Drawing.Size(446, 13);
            this.uciHelp.TabIndex = 1;
            this.uciHelp.Text = "Enter - вставить строку; Esc - удалить строку; BackSpace - стереть цифры; Delete - удалить запись";
            this.uciHelp.Visible = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(186, 334);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(128, 28);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(330, 334);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(128, 28);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FrmCashSymb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 376);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.uciHelp);
            this.Controls.Add(this.grdCashSymbols);
            this.Name = "FrmCashSymb";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Кассовые символы";
            this.Load += new System.EventHandler(this.FrmCashSymb_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdCashSymbols)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdCashSymbols;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCashSymbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private UbsControl.UbsCtrlInfo uciHelp;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
    }
}
