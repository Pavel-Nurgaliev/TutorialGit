using System.Windows.Forms;

namespace UbsBusiness
{
    partial class UbsOpCommissionSetupFrm
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.ubsCtrlInfo = new UbsControl.UbsCtrlInfo();
            this.lblCommission = new System.Windows.Forms.Label();
            this.cmbCommission = new System.Windows.Forms.ComboBox();
            this.lblOperation = new System.Windows.Forms.Label();
            this.cmbOperation = new System.Windows.Forms.ComboBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.cmbValue = new System.Windows.Forms.ComboBox();
            this.lblDiv = new System.Windows.Forms.Label();
            this.cmbDiv = new System.Windows.Forms.ComboBox();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.cmbCurrency = new System.Windows.Forms.ComboBox();
            this.panelMain.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.lblCommission);
            this.panelMain.Controls.Add(this.cmbCommission);
            this.panelMain.Controls.Add(this.lblOperation);
            this.panelMain.Controls.Add(this.cmbOperation);
            this.panelMain.Controls.Add(this.lblValue);
            this.panelMain.Controls.Add(this.cmbValue);
            this.panelMain.Controls.Add(this.lblDiv);
            this.panelMain.Controls.Add(this.cmbDiv);
            this.panelMain.Controls.Add(this.lblCurrency);
            this.panelMain.Controls.Add(this.cmbCurrency);
            this.panelMain.Controls.Add(this.tableLayoutPanel);
            this.panelMain.Size = new System.Drawing.Size(440, 197);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.CausesValidation = false;
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel.Controls.Add(this.btnSave, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.btnExit, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.ubsCtrlInfo, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 165);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(440, 32);
            this.tableLayoutPanel.TabIndex = 100;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(267, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 26);
            this.btnSave.TabIndex = 101;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.CausesValidation = false;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(355, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 26);
            this.btnExit.TabIndex = 102;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ubsCtrlInfo
            // 
            this.ubsCtrlInfo.AutoSize = true;
            this.ubsCtrlInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ubsCtrlInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.ubsCtrlInfo.ForeColor = System.Drawing.SystemColors.Highlight;
            this.ubsCtrlInfo.Interval = 500;
            this.ubsCtrlInfo.Location = new System.Drawing.Point(3, 19);
            this.ubsCtrlInfo.Name = "ubsCtrlInfo";
            this.ubsCtrlInfo.Size = new System.Drawing.Size(258, 13);
            this.ubsCtrlInfo.TabIndex = 1;
            this.ubsCtrlInfo.Text = "ubsCtrlInfo";
            this.ubsCtrlInfo.Visible = false;
            // 
            // lblCommission
            // 
            this.lblCommission.AutoSize = true;
            this.lblCommission.Location = new System.Drawing.Point(8, 17);
            this.lblCommission.Name = "lblCommission";
            this.lblCommission.Size = new System.Drawing.Size(61, 13);
            this.lblCommission.TabIndex = 8;
            this.lblCommission.Text = "Комиссия:";
            this.lblCommission.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCommission
            // 
            this.cmbCommission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCommission.FormattingEnabled = true;
            this.cmbCommission.Location = new System.Drawing.Point(88, 14);
            this.cmbCommission.Name = "cmbCommission";
            this.cmbCommission.Size = new System.Drawing.Size(334, 21);
            this.cmbCommission.TabIndex = 0;
            // 
            // lblOperation
            // 
            this.lblOperation.AutoSize = true;
            this.lblOperation.Location = new System.Drawing.Point(8, 47);
            this.lblOperation.Name = "lblOperation";
            this.lblOperation.Size = new System.Drawing.Size(60, 13);
            this.lblOperation.TabIndex = 9;
            this.lblOperation.Text = "Операция:";
            this.lblOperation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbOperation
            // 
            this.cmbOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperation.FormattingEnabled = true;
            this.cmbOperation.Location = new System.Drawing.Point(88, 44);
            this.cmbOperation.Name = "cmbOperation";
            this.cmbOperation.Size = new System.Drawing.Size(334, 21);
            this.cmbOperation.TabIndex = 1;
            this.cmbOperation.SelectedIndexChanged += new System.EventHandler(this.cmbOper_SelectedIndexChanged);
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(8, 77);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(59, 13);
            this.lblValue.TabIndex = 10;
            this.lblValue.Text = "Ценность:";
            this.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbValue
            // 
            this.cmbValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbValue.FormattingEnabled = true;
            this.cmbValue.Location = new System.Drawing.Point(88, 74);
            this.cmbValue.Name = "cmbValue";
            this.cmbValue.Size = new System.Drawing.Size(334, 21);
            this.cmbValue.TabIndex = 2;
            // 
            // lblDiv
            // 
            this.lblDiv.AutoSize = true;
            this.lblDiv.Location = new System.Drawing.Point(8, 107);
            this.lblDiv.Name = "lblDiv";
            this.lblDiv.Size = new System.Drawing.Size(41, 13);
            this.lblDiv.TabIndex = 11;
            this.lblDiv.Text = "Касса:";
            this.lblDiv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbDiv
            // 
            this.cmbDiv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiv.FormattingEnabled = true;
            this.cmbDiv.Location = new System.Drawing.Point(88, 104);
            this.cmbDiv.Name = "cmbDiv";
            this.cmbDiv.Size = new System.Drawing.Size(334, 21);
            this.cmbDiv.TabIndex = 3;
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Location = new System.Drawing.Point(8, 137);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(48, 13);
            this.lblCurrency.TabIndex = 12;
            this.lblCurrency.Text = "Валюта:";
            this.lblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrency.FormattingEnabled = true;
            this.cmbCurrency.Location = new System.Drawing.Point(88, 134);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Size = new System.Drawing.Size(334, 21);
            this.cmbCurrency.TabIndex = 4;
            // 
            // UbsOpCommissionSetupFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 197);
            this.Name = "UbsOpCommissionSetupFrm";
            this.Text = "Установка комиссии";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private UbsControl.UbsCtrlInfo ubsCtrlInfo;
        private System.Windows.Forms.Label lblCommission;
        private System.Windows.Forms.ComboBox cmbCommission;
        private System.Windows.Forms.Label lblOperation;
        private System.Windows.Forms.ComboBox cmbOperation;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.ComboBox cmbValue;
        private System.Windows.Forms.Label lblDiv;
        private System.Windows.Forms.ComboBox cmbDiv;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.ComboBox cmbCurrency;
    }
}
