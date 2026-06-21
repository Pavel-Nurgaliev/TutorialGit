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
            this.colPayerAccount.Text = "Счет плательщика";
            this.colPayerAccount.Width = 160;
            // 
            // colRecipientAccount
            // 
            this.colRecipientAccount.Text = "Счет получателя";
            this.colRecipientAccount.Width = 160;
            // 
            // colAmount
            // 
            this.colAmount.Text = "Сумма";
            this.colAmount.Width = 120;
            // 
            // colNote
            // 
            this.colNote.Text = "Примечание";
            this.colNote.Width = 295;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(214, 286);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(157, 31);
            this.btnExecute.TabIndex = 1;
            this.btnExecute.Text = "Выполнить";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(405, 286);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(157, 31);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Выход";
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
            this.Text = "Подготовка кассовых ордеров";
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
