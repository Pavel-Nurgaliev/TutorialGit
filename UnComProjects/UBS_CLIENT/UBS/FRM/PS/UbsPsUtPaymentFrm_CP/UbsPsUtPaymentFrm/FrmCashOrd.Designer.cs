namespace UbsBusiness
{
    partial class FrmCashOrd
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
            this.lvwDocuments = new System.Windows.Forms.ListView();
            this.colPayerAccount = new System.Windows.Forms.ColumnHeader();
            this.colRecipientAccount = new System.Windows.Forms.ColumnHeader();
            this.colAmount = new System.Windows.Forms.ColumnHeader();
            this.colNote = new System.Windows.Forms.ColumnHeader();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvwDocuments
            // 
            this.lvwDocuments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPayerAccount,
            this.colRecipientAccount,
            this.colAmount,
            this.colNote});
            this.lvwDocuments.FullRowSelect = true;
            this.lvwDocuments.GridLines = true;
            this.lvwDocuments.HideSelection = false;
            this.lvwDocuments.Location = new System.Drawing.Point(12, 12);
            this.lvwDocuments.Name = "lvwDocuments";
            this.lvwDocuments.Size = new System.Drawing.Size(759, 257);
            this.lvwDocuments.TabIndex = 0;
            this.lvwDocuments.UseCompatibleStateImageBehavior = false;
            this.lvwDocuments.View = System.Windows.Forms.View.Details;
            // 
            // colPayerAccount
            // 
            this.colPayerAccount.Text = "\u0421\u0447\u0435\u0442 \u043f\u043b\u0430\u0442\u0435\u043b\u044c\u0449\u0438\u043a\u0430";
            this.colPayerAccount.Width = 160;
            // 
            // colRecipientAccount
            // 
            this.colRecipientAccount.Text = "\u0421\u0447\u0435\u0442 \u043f\u043e\u043b\u0443\u0447\u0430\u0442\u0435\u043b\u044f";
            this.colRecipientAccount.Width = 160;
            // 
            // colAmount
            // 
            this.colAmount.Text = "\u0421\u0443\u043c\u043c\u0430";
            this.colAmount.Width = 120;
            // 
            // colNote
            // 
            this.colNote.Text = "\u041f\u0440\u0438\u043c\u0435\u0447\u0430\u043d\u0438\u0435";
            this.colNote.Width = 295;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(214, 286);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(157, 31);
            this.btnExecute.TabIndex = 1;
            this.btnExecute.Text = "\u0412\u044b\u043f\u043e\u043b\u043d\u0438\u0442\u044c";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(405, 286);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(157, 31);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "\u0412\u044b\u0445\u043e\u0434";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FrmCashOrd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 329);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.lvwDocuments);
            this.Name = "FrmCashOrd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "\u041f\u043e\u0434\u0433\u043e\u0442\u043e\u0432\u043a\u0430 \u043a\u0430\u0441\u0441\u043e\u0432\u044b\u0445 \u043e\u0440\u0434\u0435\u0440\u043e\u0432";
            this.Load += new System.EventHandler(this.FrmCashOrd_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwDocuments;
        private System.Windows.Forms.ColumnHeader colPayerAccount;
        private System.Windows.Forms.ColumnHeader colRecipientAccount;
        private System.Windows.Forms.ColumnHeader colAmount;
        private System.Windows.Forms.ColumnHeader colNote;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnExit;
    }
}
