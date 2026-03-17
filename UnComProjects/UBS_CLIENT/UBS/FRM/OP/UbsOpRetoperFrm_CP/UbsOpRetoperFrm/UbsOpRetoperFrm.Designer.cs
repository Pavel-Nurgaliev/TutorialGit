using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    partial class UbsOpRetoperFrm
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
            this.linkClient = new System.Windows.Forms.LinkLabel();
            this.grpPayDoc = new System.Windows.Forms.GroupBox();
            this.lblPayDoc = new System.Windows.Forms.Label();
            this.txtPayDoc = new System.Windows.Forms.TextBox();
            this.lblSerPayDoc = new System.Windows.Forms.Label();
            this.lblNumPayDoc = new System.Windows.Forms.Label();
            this.txtSerPayDoc = new System.Windows.Forms.TextBox();
            this.ucdNumPayDoc = new UbsControl.UbsCtrlDecimal();
            this.lblCurrencyNominal = new System.Windows.Forms.Label();
            this.lblSumNominal = new System.Windows.Forms.Label();
            this.txtCurrencyNominal = new System.Windows.Forms.TextBox();
            this.ucdSumNominal = new UbsControl.UbsCtrlDecimal();
            this.chkPayMoney = new System.Windows.Forms.CheckBox();
            this.lblValueMinus = new System.Windows.Forms.Label();
            this.cmbValueMinus = new System.Windows.Forms.ComboBox();
            this.ucdValueMinus = new UbsControl.UbsCtrlDecimal();
            this.lblRate = new System.Windows.Forms.Label();
            this.lblRatePer = new System.Windows.Forms.Label();
            this.ucdRate = new UbsControl.UbsCtrlDecimal();
            this.ucdRatePer = new UbsControl.UbsCtrlDecimal();
            this.lblCommission = new System.Windows.Forms.Label();
            this.cmbComission = new System.Windows.Forms.ComboBox();
            this.ucdCommission = new UbsControl.UbsCtrlDecimal();
            this.lblIncomeTax = new System.Windows.Forms.Label();
            this.ucdIncomeTax = new UbsControl.UbsCtrlDecimal();
            this.lblNumReceipt = new System.Windows.Forms.Label();
            this.ucdNumReceipt = new UbsControl.UbsCtrlDecimal();
            this.txtClient = new System.Windows.Forms.TextBox();
            this.lblDoc = new System.Windows.Forms.Label();
            this.cmbDocument = new System.Windows.Forms.ComboBox();
            this.lblSerDocument = new System.Windows.Forms.Label();
            this.txtSerDocument = new System.Windows.Forms.TextBox();
            this.lblNumDocument = new System.Windows.Forms.Label();
            this.txtNumDocument = new System.Windows.Forms.TextBox();
            this.chkResident = new System.Windows.Forms.CheckBox();
            this.panelMain.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.grpPayDoc.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.linkClient);
            this.panelMain.Controls.Add(this.grpPayDoc);
            this.panelMain.Controls.Add(this.chkPayMoney);
            this.panelMain.Controls.Add(this.lblValueMinus);
            this.panelMain.Controls.Add(this.cmbValueMinus);
            this.panelMain.Controls.Add(this.ucdValueMinus);
            this.panelMain.Controls.Add(this.lblRate);
            this.panelMain.Controls.Add(this.lblRatePer);
            this.panelMain.Controls.Add(this.ucdRate);
            this.panelMain.Controls.Add(this.ucdRatePer);
            this.panelMain.Controls.Add(this.lblCommission);
            this.panelMain.Controls.Add(this.cmbComission);
            this.panelMain.Controls.Add(this.ucdCommission);
            this.panelMain.Controls.Add(this.lblIncomeTax);
            this.panelMain.Controls.Add(this.ucdIncomeTax);
            this.panelMain.Controls.Add(this.lblNumReceipt);
            this.panelMain.Controls.Add(this.ucdNumReceipt);
            this.panelMain.Controls.Add(this.txtClient);
            this.panelMain.Controls.Add(this.lblDoc);
            this.panelMain.Controls.Add(this.cmbDocument);
            this.panelMain.Controls.Add(this.lblSerDocument);
            this.panelMain.Controls.Add(this.txtSerDocument);
            this.panelMain.Controls.Add(this.lblNumDocument);
            this.panelMain.Controls.Add(this.txtNumDocument);
            this.panelMain.Controls.Add(this.chkResident);
            this.panelMain.Controls.Add(this.tableLayoutPanel);
            this.panelMain.Size = new System.Drawing.Size(450, 326);
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
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 294);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(450, 32);
            this.tableLayoutPanel.TabIndex = 100;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(277, 3);
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
            this.btnExit.Location = new System.Drawing.Point(365, 3);
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
            this.ubsCtrlInfo.Interval = 25000;
            this.ubsCtrlInfo.Location = new System.Drawing.Point(3, 19);
            this.ubsCtrlInfo.Name = "ubsCtrlInfo";
            this.ubsCtrlInfo.Size = new System.Drawing.Size(268, 13);
            this.ubsCtrlInfo.TabIndex = 1;
            this.ubsCtrlInfo.Text = "ubsCtrlInfo";
            this.ubsCtrlInfo.Visible = false;
            // 
            // linkClient
            // 
            this.linkClient.AutoSize = true;
            this.linkClient.Location = new System.Drawing.Point(6, 218);
            this.linkClient.Name = "linkClient";
            this.linkClient.Size = new System.Drawing.Size(43, 13);
            this.linkClient.TabIndex = 126;
            this.linkClient.TabStop = true;
            this.linkClient.Text = "Клиент";
            this.linkClient.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkClient_LinkClicked);
            // 
            // grpPayDoc
            // 
            this.grpPayDoc.Controls.Add(this.lblPayDoc);
            this.grpPayDoc.Controls.Add(this.txtPayDoc);
            this.grpPayDoc.Controls.Add(this.lblSerPayDoc);
            this.grpPayDoc.Controls.Add(this.lblNumPayDoc);
            this.grpPayDoc.Controls.Add(this.txtSerPayDoc);
            this.grpPayDoc.Controls.Add(this.ucdNumPayDoc);
            this.grpPayDoc.Controls.Add(this.lblCurrencyNominal);
            this.grpPayDoc.Controls.Add(this.lblSumNominal);
            this.grpPayDoc.Controls.Add(this.txtCurrencyNominal);
            this.grpPayDoc.Controls.Add(this.ucdSumNominal);
            this.grpPayDoc.Enabled = false;
            this.grpPayDoc.Location = new System.Drawing.Point(6, 12);
            this.grpPayDoc.Name = "grpPayDoc";
            this.grpPayDoc.Size = new System.Drawing.Size(435, 95);
            this.grpPayDoc.TabIndex = 0;
            this.grpPayDoc.TabStop = false;
            this.grpPayDoc.Text = "Платежный документ";
            // 
            // lblPayDoc
            // 
            this.lblPayDoc.AutoSize = true;
            this.lblPayDoc.Location = new System.Drawing.Point(21, 20);
            this.lblPayDoc.Name = "lblPayDoc";
            this.lblPayDoc.Size = new System.Drawing.Size(83, 13);
            this.lblPayDoc.TabIndex = 0;
            this.lblPayDoc.Text = "Наименование";
            // 
            // txtPayDoc
            // 
            this.txtPayDoc.Enabled = false;
            this.txtPayDoc.Location = new System.Drawing.Point(110, 17);
            this.txtPayDoc.Name = "txtPayDoc";
            this.txtPayDoc.Size = new System.Drawing.Size(314, 20);
            this.txtPayDoc.TabIndex = 1;
            // 
            // lblSerPayDoc
            // 
            this.lblSerPayDoc.AutoSize = true;
            this.lblSerPayDoc.Location = new System.Drawing.Point(66, 46);
            this.lblSerPayDoc.Name = "lblSerPayDoc";
            this.lblSerPayDoc.Size = new System.Drawing.Size(38, 13);
            this.lblSerPayDoc.TabIndex = 2;
            this.lblSerPayDoc.Text = "Серия";
            // 
            // lblNumPayDoc
            // 
            this.lblNumPayDoc.AutoSize = true;
            this.lblNumPayDoc.Location = new System.Drawing.Point(200, 46);
            this.lblNumPayDoc.Name = "lblNumPayDoc";
            this.lblNumPayDoc.Size = new System.Drawing.Size(41, 13);
            this.lblNumPayDoc.TabIndex = 4;
            this.lblNumPayDoc.Text = "Номер";
            // 
            // txtSerPayDoc
            // 
            this.txtSerPayDoc.Enabled = false;
            this.txtSerPayDoc.Location = new System.Drawing.Point(110, 43);
            this.txtSerPayDoc.Name = "txtSerPayDoc";
            this.txtSerPayDoc.Size = new System.Drawing.Size(80, 20);
            this.txtSerPayDoc.TabIndex = 3;
            // 
            // ucdNumPayDoc
            // 
            this.ucdNumPayDoc.Location = new System.Drawing.Point(247, 43);
            this.ucdNumPayDoc.Name = "ucdNumPayDoc";
            this.ucdNumPayDoc.Size = new System.Drawing.Size(85, 20);
            this.ucdNumPayDoc.TabIndex = 5;
            this.ucdNumPayDoc.Text = "0";
            // 
            // lblCurrencyNominal
            // 
            this.lblCurrencyNominal.AutoSize = true;
            this.lblCurrencyNominal.Location = new System.Drawing.Point(6, 72);
            this.lblCurrencyNominal.Name = "lblCurrencyNominal";
            this.lblCurrencyNominal.Size = new System.Drawing.Size(98, 13);
            this.lblCurrencyNominal.TabIndex = 6;
            this.lblCurrencyNominal.Text = "Валюта номинала";
            // 
            // lblSumNominal
            // 
            this.lblSumNominal.AutoSize = true;
            this.lblSumNominal.Location = new System.Drawing.Point(200, 72);
            this.lblSumNominal.Name = "lblSumNominal";
            this.lblSumNominal.Size = new System.Drawing.Size(108, 13);
            this.lblSumNominal.TabIndex = 8;
            this.lblSumNominal.Text = "Сумма по номиналу";
            // 
            // txtCurrencyNominal
            // 
            this.txtCurrencyNominal.Enabled = false;
            this.txtCurrencyNominal.Location = new System.Drawing.Point(110, 69);
            this.txtCurrencyNominal.Name = "txtCurrencyNominal";
            this.txtCurrencyNominal.Size = new System.Drawing.Size(80, 20);
            this.txtCurrencyNominal.TabIndex = 7;
            // 
            // ucdSumNominal
            // 
            this.ucdSumNominal.Location = new System.Drawing.Point(314, 69);
            this.ucdSumNominal.Name = "ucdSumNominal";
            this.ucdSumNominal.Size = new System.Drawing.Size(113, 20);
            this.ucdSumNominal.TabIndex = 9;
            this.ucdSumNominal.Text = "0";
            // 
            // chkPayMoney
            // 
            this.chkPayMoney.AutoSize = true;
            this.chkPayMoney.Location = new System.Drawing.Point(9, 113);
            this.chkPayMoney.Name = "chkPayMoney";
            this.chkPayMoney.Size = new System.Drawing.Size(213, 17);
            this.chkPayMoney.TabIndex = 1;
            this.chkPayMoney.Text = "Выплатить в денежном эквиваленте";
            this.chkPayMoney.UseVisualStyleBackColor = true;
            // 
            // lblValueMinus
            // 
            this.lblValueMinus.AutoSize = true;
            this.lblValueMinus.Location = new System.Drawing.Point(6, 140);
            this.lblValueMinus.Name = "lblValueMinus";
            this.lblValueMinus.Size = new System.Drawing.Size(45, 13);
            this.lblValueMinus.TabIndex = 3;
            this.lblValueMinus.Text = "Выдать";
            // 
            // cmbValueMinus
            // 
            this.cmbValueMinus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbValueMinus.FormattingEnabled = true;
            this.cmbValueMinus.Location = new System.Drawing.Point(70, 137);
            this.cmbValueMinus.Name = "cmbValueMinus";
            this.cmbValueMinus.Size = new System.Drawing.Size(70, 21);
            this.cmbValueMinus.TabIndex = 4;
            // 
            // ucdValueMinus
            // 
            this.ucdValueMinus.Enabled = false;
            this.ucdValueMinus.Location = new System.Drawing.Point(146, 137);
            this.ucdValueMinus.Name = "ucdValueMinus";
            this.ucdValueMinus.Size = new System.Drawing.Size(90, 20);
            this.ucdValueMinus.TabIndex = 5;
            this.ucdValueMinus.Text = "0";
            // 
            // lblRate
            // 
            this.lblRate.AutoSize = true;
            this.lblRate.Location = new System.Drawing.Point(255, 140);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(31, 13);
            this.lblRate.TabIndex = 6;
            this.lblRate.Text = "Курс";
            // 
            // lblRatePer
            // 
            this.lblRatePer.AutoSize = true;
            this.lblRatePer.Location = new System.Drawing.Point(345, 140);
            this.lblRatePer.Name = "lblRatePer";
            this.lblRatePer.Size = new System.Drawing.Size(13, 13);
            this.lblRatePer.TabIndex = 8;
            this.lblRatePer.Text = "к";
            // 
            // ucdRate
            // 
            this.ucdRate.Enabled = false;
            this.ucdRate.Location = new System.Drawing.Point(293, 137);
            this.ucdRate.Name = "ucdRate";
            this.ucdRate.Size = new System.Drawing.Size(40, 20);
            this.ucdRate.TabIndex = 7;
            this.ucdRate.Text = "0";
            // 
            // ucdRatePer
            // 
            this.ucdRatePer.Enabled = false;
            this.ucdRatePer.Location = new System.Drawing.Point(360, 137);
            this.ucdRatePer.Name = "ucdRatePer";
            this.ucdRatePer.Size = new System.Drawing.Size(70, 20);
            this.ucdRatePer.TabIndex = 9;
            this.ucdRatePer.Text = "0";
            // 
            // lblCommission
            // 
            this.lblCommission.AutoSize = true;
            this.lblCommission.Location = new System.Drawing.Point(6, 166);
            this.lblCommission.Name = "lblCommission";
            this.lblCommission.Size = new System.Drawing.Size(58, 13);
            this.lblCommission.TabIndex = 10;
            this.lblCommission.Text = "Комиссия";
            // 
            // cmbComission
            // 
            this.cmbComission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComission.FormattingEnabled = true;
            this.cmbComission.Location = new System.Drawing.Point(70, 163);
            this.cmbComission.Name = "cmbComission";
            this.cmbComission.Size = new System.Drawing.Size(70, 21);
            this.cmbComission.TabIndex = 11;
            // 
            // ucdCommission
            // 
            this.ucdCommission.Location = new System.Drawing.Point(146, 163);
            this.ucdCommission.Name = "ucdCommission";
            this.ucdCommission.Size = new System.Drawing.Size(90, 20);
            this.ucdCommission.TabIndex = 12;
            this.ucdCommission.Text = "0";
            // 
            // lblIncomeTax
            // 
            this.lblIncomeTax.AutoSize = true;
            this.lblIncomeTax.Location = new System.Drawing.Point(255, 166);
            this.lblIncomeTax.Name = "lblIncomeTax";
            this.lblIncomeTax.Size = new System.Drawing.Size(102, 13);
            this.lblIncomeTax.TabIndex = 13;
            this.lblIncomeTax.Text = "Подоходный налог";
            // 
            // ucdIncomeTax
            // 
            this.ucdIncomeTax.Location = new System.Drawing.Point(360, 163);
            this.ucdIncomeTax.Name = "ucdIncomeTax";
            this.ucdIncomeTax.Size = new System.Drawing.Size(70, 20);
            this.ucdIncomeTax.TabIndex = 14;
            this.ucdIncomeTax.Text = "0";
            // 
            // lblNumReceipt
            // 
            this.lblNumReceipt.AutoSize = true;
            this.lblNumReceipt.Location = new System.Drawing.Point(6, 192);
            this.lblNumReceipt.Name = "lblNumReceipt";
            this.lblNumReceipt.Size = new System.Drawing.Size(195, 13);
            this.lblNumReceipt.TabIndex = 15;
            this.lblNumReceipt.Text = "Номер квитанции(справки) о приеме";
            // 
            // ucdNumReceipt
            // 
            this.ucdNumReceipt.Enabled = false;
            this.ucdNumReceipt.Location = new System.Drawing.Point(211, 189);
            this.ucdNumReceipt.Name = "ucdNumReceipt";
            this.ucdNumReceipt.Size = new System.Drawing.Size(60, 20);
            this.ucdNumReceipt.TabIndex = 16;
            this.ucdNumReceipt.Text = "0";
            // 
            // txtClient
            // 
            this.txtClient.Enabled = false;
            this.txtClient.Location = new System.Drawing.Point(70, 215);
            this.txtClient.Name = "txtClient";
            this.txtClient.Size = new System.Drawing.Size(371, 20);
            this.txtClient.TabIndex = 17;
            // 
            // lblDoc
            // 
            this.lblDoc.AutoSize = true;
            this.lblDoc.Location = new System.Drawing.Point(6, 244);
            this.lblDoc.Name = "lblDoc";
            this.lblDoc.Size = new System.Drawing.Size(58, 13);
            this.lblDoc.TabIndex = 19;
            this.lblDoc.Text = "Документ";
            // 
            // cmbDocument
            // 
            this.cmbDocument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDocument.Enabled = false;
            this.cmbDocument.FormattingEnabled = true;
            this.cmbDocument.Location = new System.Drawing.Point(70, 241);
            this.cmbDocument.Name = "cmbDocument";
            this.cmbDocument.Size = new System.Drawing.Size(150, 21);
            this.cmbDocument.TabIndex = 20;
            // 
            // lblSerDocument
            // 
            this.lblSerDocument.AutoSize = true;
            this.lblSerDocument.Location = new System.Drawing.Point(226, 244);
            this.lblSerDocument.Name = "lblSerDocument";
            this.lblSerDocument.Size = new System.Drawing.Size(38, 13);
            this.lblSerDocument.TabIndex = 21;
            this.lblSerDocument.Text = "Серия";
            // 
            // txtSerDocument
            // 
            this.txtSerDocument.Enabled = false;
            this.txtSerDocument.Location = new System.Drawing.Point(270, 241);
            this.txtSerDocument.Name = "txtSerDocument";
            this.txtSerDocument.Size = new System.Drawing.Size(60, 20);
            this.txtSerDocument.TabIndex = 22;
            // 
            // lblNumDocument
            // 
            this.lblNumDocument.AutoSize = true;
            this.lblNumDocument.Location = new System.Drawing.Point(336, 244);
            this.lblNumDocument.Name = "lblNumDocument";
            this.lblNumDocument.Size = new System.Drawing.Size(41, 13);
            this.lblNumDocument.TabIndex = 23;
            this.lblNumDocument.Text = "Номер";
            // 
            // txtNumDocument
            // 
            this.txtNumDocument.Enabled = false;
            this.txtNumDocument.Location = new System.Drawing.Point(383, 241);
            this.txtNumDocument.Name = "txtNumDocument";
            this.txtNumDocument.Size = new System.Drawing.Size(58, 20);
            this.txtNumDocument.TabIndex = 24;
            // 
            // chkResident
            // 
            this.chkResident.AutoSize = true;
            this.chkResident.Enabled = false;
            this.chkResident.Location = new System.Drawing.Point(9, 270);
            this.chkResident.Name = "chkResident";
            this.chkResident.Size = new System.Drawing.Size(74, 17);
            this.chkResident.TabIndex = 25;
            this.chkResident.Text = "Резидент";
            this.chkResident.UseVisualStyleBackColor = true;
            // 
            // UbsOpRetoperFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 326);
            this.Name = "UbsOpRetoperFrm";
            this.Text = "Шаблон формы";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.grpPayDoc.ResumeLayout(false);
            this.grpPayDoc.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button btnExit;
        private UbsControl.UbsCtrlInfo ubsCtrlInfo;
        private System.Windows.Forms.Button btnSave;
        private LinkLabel linkClient;
        private GroupBox grpPayDoc;
        private Label lblPayDoc;
        private TextBox txtPayDoc;
        private Label lblSerPayDoc;
        private Label lblNumPayDoc;
        private TextBox txtSerPayDoc;
        private UbsControl.UbsCtrlDecimal ucdNumPayDoc;
        private Label lblCurrencyNominal;
        private Label lblSumNominal;
        private TextBox txtCurrencyNominal;
        private UbsControl.UbsCtrlDecimal ucdSumNominal;
        private CheckBox chkPayMoney;
        private Label lblValueMinus;
        private ComboBox cmbValueMinus;
        private UbsControl.UbsCtrlDecimal ucdValueMinus;
        private Label lblRate;
        private Label lblRatePer;
        private UbsControl.UbsCtrlDecimal ucdRate;
        private UbsControl.UbsCtrlDecimal ucdRatePer;
        private Label lblCommission;
        private ComboBox cmbComission;
        private UbsControl.UbsCtrlDecimal ucdCommission;
        private Label lblIncomeTax;
        private UbsControl.UbsCtrlDecimal ucdIncomeTax;
        private Label lblNumReceipt;
        private UbsControl.UbsCtrlDecimal ucdNumReceipt;
        private TextBox txtClient;
        private Label lblDoc;
        private ComboBox cmbDocument;
        private Label lblSerDocument;
        private TextBox txtSerDocument;
        private Label lblNumDocument;
        private TextBox txtNumDocument;
        private CheckBox chkResident;
    }
}
