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
            this.colCashSymbol.HeaderText = "\u041a\u0430\u0441\u0441\u043e\u0432\u044b\u0439 \u0441\u0438\u043c\u0432\u043e\u043b";
            this.colCashSymbol.Name = "colCashSymbol";
            this.colCashSymbol.Width = 160;
            // 
            // colAmount
            // 
            this.colAmount.HeaderText = "\u0421\u0443\u043c\u043c\u0430";
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
            this.uciHelp.Text = "Enter - \u0432\u0441\u0442\u0430\u0432\u0438\u0442\u044c \u0441\u0442\u0440\u043e\u043a\u0443; Esc - \u0443\u0434\u0430\u043b\u0438\u0442\u044c \u0441\u0442\u0440\u043e\u043a\u0443; BackSpace - \u0441\u0442\u0435\u0440\u0435\u0442\u044c \u0446\u0438\u0444\u0440\u044b; Delete - \u0443\u0434\u0430\u043b\u0438\u0442\u044c \u0437\u0430\u043f\u0438\u0441\u044c";
            this.uciHelp.Visible = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(186, 334);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(128, 28);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "\u0421\u043e\u0445\u0440\u0430\u043d\u0438\u0442\u044c";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(330, 334);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(128, 28);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "\u0412\u044b\u0445\u043e\u0434";
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
            this.Text = "\u041a\u0430\u0441\u0441\u043e\u0432\u044b\u0435 \u0441\u0438\u043c\u0432\u043e\u043b\u044b";
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
