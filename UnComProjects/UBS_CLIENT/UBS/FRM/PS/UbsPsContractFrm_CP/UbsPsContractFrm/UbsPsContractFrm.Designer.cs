using System;
using System.Drawing;
using System.Windows.Forms;

namespace UbsBusiness
{
    partial class UbsPsContractFrm
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>Освобождает ресурсы компонентов формы.</summary>
        /// <param name="disposing">true — освободить управляемые ресурсы.</param>
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
            this.tblActions = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.uciContract = new UbsControl.UbsCtrlInfo();
            this.tabContract = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.pnlMainScroll = new System.Windows.Forms.Panel();
            this.linkPaymentKind = new System.Windows.Forms.LinkLabel();
            this.lblArbitraryContract = new System.Windows.Forms.Label();
            this.lblContractCode = new System.Windows.Forms.Label();
            this.txtContractCode = new System.Windows.Forms.TextBox();
            this.lblExecutor = new System.Windows.Forms.Label();
            this.cmbExecutor = new System.Windows.Forms.ComboBox();
            this.lblContractNumber = new System.Windows.Forms.Label();
            this.txtContractNumber = new System.Windows.Forms.TextBox();
            this.lblContractDate = new System.Windows.Forms.Label();
            this.ucdContract = new UbsControl.UbsCtrlDate();
            this.txtPaymentKindCode = new System.Windows.Forms.TextBox();
            this.txtPaymentKindComment = new System.Windows.Forms.TextBox();
            this.lblContractStatus = new System.Windows.Forms.Label();
            this.cmbContractStatus = new System.Windows.Forms.ComboBox();
            this.lblCloseDate = new System.Windows.Forms.Label();
            this.ucdContractClose = new UbsControl.UbsCtrlDate();
            this.grpRecipient = new System.Windows.Forms.GroupBox();
            this.linkRecipientClient = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.txtRecipientAddress = new System.Windows.Forms.TextBox();
            this.lblRecipientAddress = new System.Windows.Forms.Label();
            this.udcRecipientInn = new UbsControl.UbsCtrlDecimal();
            this.lblRecipientInn = new System.Windows.Forms.Label();
            this.ucaRecipientAccount = new UbsControl.UbsCtrlAccount();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.lblBankName = new System.Windows.Forms.Label();
            this.ucaCorrespondentAccount = new UbsControl.UbsCtrlAccount();
            this.lblCorrespondentAccount = new System.Windows.Forms.Label();
            this.udcRecipientBik = new UbsControl.UbsCtrlDecimal();
            this.lblRecipientBik = new System.Windows.Forms.Label();
            this.btnRecipientClientClear = new System.Windows.Forms.Button();
            this.txtRecipientClient = new System.Windows.Forms.TextBox();
            this.lblComment = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.tabPageCommission = new System.Windows.Forms.TabPage();
            this.grpRecipientCommission = new System.Windows.Forms.GroupBox();
            this.chkRecipientCommissionReverse = new System.Windows.Forms.CheckBox();
            this.udcRecipientCommissionPercent = new UbsControl.UbsCtrlDecimal();
            this.lblRecipientCommissionPercent = new System.Windows.Forms.Label();
            this.cmbRecipientCommissionType = new System.Windows.Forms.ComboBox();
            this.lblRecipientCommissionType = new System.Windows.Forms.Label();
            this.grpPayerCommission = new System.Windows.Forms.GroupBox();
            this.udcPayerCommissionPercent = new UbsControl.UbsCtrlDecimal();
            this.lblPayerCommissionPercent = new System.Windows.Forms.Label();
            this.cmbPayerCommissionType = new System.Windows.Forms.ComboBox();
            this.lblPayerCommissionType = new System.Windows.Forms.Label();
            this.tabPageAddFields = new System.Windows.Forms.TabPage();
            this.ucfAdditionalFields = new UbsControl.UbsCtrlFields();
            this.panelMain.SuspendLayout();
            this.tblActions.SuspendLayout();
            this.tabContract.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.pnlMainScroll.SuspendLayout();
            this.grpRecipient.SuspendLayout();
            this.tabPageCommission.SuspendLayout();
            this.grpRecipientCommission.SuspendLayout();
            this.grpPayerCommission.SuspendLayout();
            this.tabPageAddFields.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tabContract);
            this.panelMain.Controls.Add(this.tblActions);
            this.panelMain.Margin = new System.Windows.Forms.Padding(5);
            this.panelMain.Size = new System.Drawing.Size(980, 738);
            // 
            // tblActions
            // 
            this.tblActions.CausesValidation = false;
            this.tblActions.ColumnCount = 3;
            this.tblActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tblActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tblActions.Controls.Add(this.btnSave, 1, 0);
            this.tblActions.Controls.Add(this.btnExit, 2, 0);
            this.tblActions.Controls.Add(this.uciContract, 0, 0);
            this.tblActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblActions.Location = new System.Drawing.Point(0, 699);
            this.tblActions.Margin = new System.Windows.Forms.Padding(4);
            this.tblActions.Name = "tblActions";
            this.tblActions.RowCount = 1;
            this.tblActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblActions.Size = new System.Drawing.Size(980, 39);
            this.tblActions.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(750, 4);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(109, 31);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.CausesValidation = false;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(867, 4);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(109, 31);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // uciContract
            // 
            this.uciContract.AutoSize = true;
            this.uciContract.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uciContract.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.uciContract.ForeColor = System.Drawing.SystemColors.Highlight;
            this.uciContract.Interval = 25000;
            this.uciContract.Location = new System.Drawing.Point(4, 22);
            this.uciContract.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.uciContract.Name = "uciContract";
            this.uciContract.Size = new System.Drawing.Size(738, 17);
            this.uciContract.TabIndex = 0;
            this.uciContract.Text = "uciContract";
            this.uciContract.Visible = false;
            // 
            // tabContract
            // 
            this.tabContract.Controls.Add(this.tabPageMain);
            this.tabContract.Controls.Add(this.tabPageCommission);
            this.tabContract.Controls.Add(this.tabPageAddFields);
            this.tabContract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabContract.Location = new System.Drawing.Point(0, 0);
            this.tabContract.Margin = new System.Windows.Forms.Padding(4);
            this.tabContract.Name = "tabContract";
            this.tabContract.SelectedIndex = 0;
            this.tabContract.Size = new System.Drawing.Size(980, 699);
            this.tabContract.TabIndex = 0;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.pnlMainScroll);
            this.tabPageMain.Location = new System.Drawing.Point(4, 25);
            this.tabPageMain.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageMain.Size = new System.Drawing.Size(972, 670);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "Основные";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // pnlMainScroll
            // 
            this.pnlMainScroll.AutoScroll = true;
            this.pnlMainScroll.Controls.Add(this.linkPaymentKind);
            this.pnlMainScroll.Controls.Add(this.lblArbitraryContract);
            this.pnlMainScroll.Controls.Add(this.lblContractCode);
            this.pnlMainScroll.Controls.Add(this.txtContractCode);
            this.pnlMainScroll.Controls.Add(this.lblExecutor);
            this.pnlMainScroll.Controls.Add(this.cmbExecutor);
            this.pnlMainScroll.Controls.Add(this.lblContractNumber);
            this.pnlMainScroll.Controls.Add(this.txtContractNumber);
            this.pnlMainScroll.Controls.Add(this.lblContractDate);
            this.pnlMainScroll.Controls.Add(this.ucdContract);
            this.pnlMainScroll.Controls.Add(this.txtPaymentKindCode);
            this.pnlMainScroll.Controls.Add(this.txtPaymentKindComment);
            this.pnlMainScroll.Controls.Add(this.lblContractStatus);
            this.pnlMainScroll.Controls.Add(this.cmbContractStatus);
            this.pnlMainScroll.Controls.Add(this.lblCloseDate);
            this.pnlMainScroll.Controls.Add(this.ucdContractClose);
            this.pnlMainScroll.Controls.Add(this.grpRecipient);
            this.pnlMainScroll.Controls.Add(this.lblComment);
            this.pnlMainScroll.Controls.Add(this.txtComment);
            this.pnlMainScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainScroll.Location = new System.Drawing.Point(4, 4);
            this.pnlMainScroll.Margin = new System.Windows.Forms.Padding(4);
            this.pnlMainScroll.Name = "pnlMainScroll";
            this.pnlMainScroll.Size = new System.Drawing.Size(964, 662);
            this.pnlMainScroll.TabIndex = 0;
            // 
            // linkPaymentKind
            // 
            this.linkPaymentKind.AutoSize = true;
            this.linkPaymentKind.Location = new System.Drawing.Point(16, 119);
            this.linkPaymentKind.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkPaymentKind.Name = "linkPaymentKind";
            this.linkPaymentKind.Size = new System.Drawing.Size(91, 16);
            this.linkPaymentKind.TabIndex = 5;
            this.linkPaymentKind.TabStop = true;
            this.linkPaymentKind.Text = "Вид платежа";
            // 
            // lblArbitraryContract
            // 
            this.lblArbitraryContract.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblArbitraryContract.Location = new System.Drawing.Point(15, 10);
            this.lblArbitraryContract.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblArbitraryContract.Name = "lblArbitraryContract";
            this.lblArbitraryContract.Size = new System.Drawing.Size(932, 22);
            this.lblArbitraryContract.TabIndex = 0;
            this.lblArbitraryContract.Text = "Произвольный договор";
            this.lblArbitraryContract.Visible = false;
            // 
            // lblContractCode
            // 
            this.lblContractCode.AutoSize = true;
            this.lblContractCode.Location = new System.Drawing.Point(16, 43);
            this.lblContractCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblContractCode.Name = "lblContractCode";
            this.lblContractCode.Size = new System.Drawing.Size(96, 16);
            this.lblContractCode.TabIndex = 50;
            this.lblContractCode.Text = "Код договора";
            // 
            // txtContractCode
            // 
            this.txtContractCode.Location = new System.Drawing.Point(125, 39);
            this.txtContractCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtContractCode.Name = "txtContractCode";
            this.txtContractCode.Size = new System.Drawing.Size(319, 22);
            this.txtContractCode.TabIndex = 1;
            // 
            // lblExecutor
            // 
            this.lblExecutor.AutoSize = true;
            this.lblExecutor.Location = new System.Drawing.Point(473, 43);
            this.lblExecutor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExecutor.Name = "lblExecutor";
            this.lblExecutor.Size = new System.Drawing.Size(27, 16);
            this.lblExecutor.TabIndex = 51;
            this.lblExecutor.Text = "ОИ";
            // 
            // cmbExecutor
            // 
            this.cmbExecutor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbExecutor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExecutor.FormattingEnabled = true;
            this.cmbExecutor.Location = new System.Drawing.Point(535, 39);
            this.cmbExecutor.Margin = new System.Windows.Forms.Padding(4);
            this.cmbExecutor.Name = "cmbExecutor";
            this.cmbExecutor.Size = new System.Drawing.Size(605, 24);
            this.cmbExecutor.TabIndex = 2;
            // 
            // lblContractNumber
            // 
            this.lblContractNumber.AutoSize = true;
            this.lblContractNumber.Location = new System.Drawing.Point(16, 82);
            this.lblContractNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblContractNumber.Name = "lblContractNumber";
            this.lblContractNumber.Size = new System.Drawing.Size(50, 16);
            this.lblContractNumber.TabIndex = 52;
            this.lblContractNumber.Text = "Номер";
            // 
            // txtContractNumber
            // 
            this.txtContractNumber.Location = new System.Drawing.Point(125, 79);
            this.txtContractNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtContractNumber.Name = "txtContractNumber";
            this.txtContractNumber.Size = new System.Drawing.Size(319, 22);
            this.txtContractNumber.TabIndex = 3;
            // 
            // lblContractDate
            // 
            this.lblContractDate.AutoSize = true;
            this.lblContractDate.Location = new System.Drawing.Point(473, 82);
            this.lblContractDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblContractDate.Name = "lblContractDate";
            this.lblContractDate.Size = new System.Drawing.Size(39, 16);
            this.lblContractDate.TabIndex = 53;
            this.lblContractDate.Text = "Дата";
            // 
            // ucdContract
            // 
            this.ucdContract.Location = new System.Drawing.Point(535, 79);
            this.ucdContract.Margin = new System.Windows.Forms.Padding(4);
            this.ucdContract.MaxLength = 10;
            this.ucdContract.Name = "ucdContract";
            this.ucdContract.Size = new System.Drawing.Size(159, 22);
            this.ucdContract.TabIndex = 4;
            this.ucdContract.Text = "  .  .    ";
            this.ucdContract.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPaymentKindCode
            // 
            this.txtPaymentKindCode.Enabled = false;
            this.txtPaymentKindCode.Location = new System.Drawing.Point(125, 116);
            this.txtPaymentKindCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtPaymentKindCode.Name = "txtPaymentKindCode";
            this.txtPaymentKindCode.Size = new System.Drawing.Size(105, 22);
            this.txtPaymentKindCode.TabIndex = 6;
            // 
            // txtPaymentKindComment
            // 
            this.txtPaymentKindComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPaymentKindComment.Enabled = false;
            this.txtPaymentKindComment.Location = new System.Drawing.Point(247, 116);
            this.txtPaymentKindComment.Margin = new System.Windows.Forms.Padding(4);
            this.txtPaymentKindComment.Name = "txtPaymentKindComment";
            this.txtPaymentKindComment.Size = new System.Drawing.Size(893, 22);
            this.txtPaymentKindComment.TabIndex = 7;
            // 
            // lblContractStatus
            // 
            this.lblContractStatus.AutoSize = true;
            this.lblContractStatus.Location = new System.Drawing.Point(16, 156);
            this.lblContractStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblContractStatus.Name = "lblContractStatus";
            this.lblContractStatus.Size = new System.Drawing.Size(53, 16);
            this.lblContractStatus.TabIndex = 54;
            this.lblContractStatus.Text = "Статус";
            // 
            // cmbContractStatus
            // 
            this.cmbContractStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContractStatus.FormattingEnabled = true;
            this.cmbContractStatus.Location = new System.Drawing.Point(125, 153);
            this.cmbContractStatus.Margin = new System.Windows.Forms.Padding(4);
            this.cmbContractStatus.Name = "cmbContractStatus";
            this.cmbContractStatus.Size = new System.Drawing.Size(259, 24);
            this.cmbContractStatus.TabIndex = 8;
            // 
            // lblCloseDate
            // 
            this.lblCloseDate.AutoSize = true;
            this.lblCloseDate.Location = new System.Drawing.Point(400, 156);
            this.lblCloseDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCloseDate.Name = "lblCloseDate";
            this.lblCloseDate.Size = new System.Drawing.Size(107, 16);
            this.lblCloseDate.TabIndex = 55;
            this.lblCloseDate.Text = "Дата закрытия:";
            this.lblCloseDate.Visible = false;
            // 
            // ucdContractClose
            // 
            this.ucdContractClose.Location = new System.Drawing.Point(535, 153);
            this.ucdContractClose.Margin = new System.Windows.Forms.Padding(4);
            this.ucdContractClose.MaxLength = 10;
            this.ucdContractClose.Name = "ucdContractClose";
            this.ucdContractClose.Size = new System.Drawing.Size(159, 22);
            this.ucdContractClose.TabIndex = 9;
            this.ucdContractClose.Text = "  .  .    ";
            this.ucdContractClose.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ucdContractClose.Visible = false;
            // 
            // grpRecipient
            // 
            this.grpRecipient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRecipient.Controls.Add(this.linkRecipientClient);
            this.grpRecipient.Controls.Add(this.linkLabel1);
            this.grpRecipient.Controls.Add(this.txtRecipientAddress);
            this.grpRecipient.Controls.Add(this.lblRecipientAddress);
            this.grpRecipient.Controls.Add(this.udcRecipientInn);
            this.grpRecipient.Controls.Add(this.lblRecipientInn);
            this.grpRecipient.Controls.Add(this.ucaRecipientAccount);
            this.grpRecipient.Controls.Add(this.txtBankName);
            this.grpRecipient.Controls.Add(this.lblBankName);
            this.grpRecipient.Controls.Add(this.ucaCorrespondentAccount);
            this.grpRecipient.Controls.Add(this.lblCorrespondentAccount);
            this.grpRecipient.Controls.Add(this.udcRecipientBik);
            this.grpRecipient.Controls.Add(this.lblRecipientBik);
            this.grpRecipient.Controls.Add(this.btnRecipientClientClear);
            this.grpRecipient.Controls.Add(this.txtRecipientClient);
            this.grpRecipient.Location = new System.Drawing.Point(11, 186);
            this.grpRecipient.Margin = new System.Windows.Forms.Padding(4);
            this.grpRecipient.Name = "grpRecipient";
            this.grpRecipient.Padding = new System.Windows.Forms.Padding(4);
            this.grpRecipient.Size = new System.Drawing.Size(1183, 192);
            this.grpRecipient.TabIndex = 10;
            this.grpRecipient.TabStop = false;
            this.grpRecipient.Text = "Получатель";
            // 
            // linkRecipientClient
            // 
            this.linkRecipientClient.AutoSize = true;
            this.linkRecipientClient.Location = new System.Drawing.Point(477, 122);
            this.linkRecipientClient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkRecipientClient.Name = "linkRecipientClient";
            this.linkRecipientClient.Size = new System.Drawing.Size(27, 16);
            this.linkRecipientClient.TabIndex = 7;
            this.linkRecipientClient.TabStop = true;
            this.linkRecipientClient.Text = "Р/с";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(5, 22);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(96, 16);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Клиент банка";
            // 
            // txtRecipientAddress
            // 
            this.txtRecipientAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecipientAddress.Location = new System.Drawing.Point(168, 156);
            this.txtRecipientAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtRecipientAddress.Name = "txtRecipientAddress";
            this.txtRecipientAddress.Size = new System.Drawing.Size(961, 22);
            this.txtRecipientAddress.TabIndex = 9;
            // 
            // lblRecipientAddress
            // 
            this.lblRecipientAddress.Location = new System.Drawing.Point(5, 160);
            this.lblRecipientAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecipientAddress.Name = "lblRecipientAddress";
            this.lblRecipientAddress.Size = new System.Drawing.Size(103, 22);
            this.lblRecipientAddress.TabIndex = 60;
            this.lblRecipientAddress.Text = "Адрес";
            // 
            // udcRecipientInn
            // 
            this.udcRecipientInn.Location = new System.Drawing.Point(168, 118);
            this.udcRecipientInn.Margin = new System.Windows.Forms.Padding(4);
            this.udcRecipientInn.Name = "udcRecipientInn";
            this.udcRecipientInn.Size = new System.Drawing.Size(252, 22);
            this.udcRecipientInn.TabIndex = 6;
            this.udcRecipientInn.Text = "0";
            // 
            // lblRecipientInn
            // 
            this.lblRecipientInn.AutoSize = true;
            this.lblRecipientInn.Location = new System.Drawing.Point(5, 122);
            this.lblRecipientInn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecipientInn.Name = "lblRecipientInn";
            this.lblRecipientInn.Size = new System.Drawing.Size(37, 16);
            this.lblRecipientInn.TabIndex = 61;
            this.lblRecipientInn.Text = "ИНН";
            // 
            // ucaRecipientAccount
            // 
            this.ucaRecipientAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucaRecipientAccount.Location = new System.Drawing.Point(524, 118);
            this.ucaRecipientAccount.Margin = new System.Windows.Forms.Padding(4);
            this.ucaRecipientAccount.MaxLength = 24;
            this.ucaRecipientAccount.Name = "ucaRecipientAccount";
            this.ucaRecipientAccount.Size = new System.Drawing.Size(605, 22);
            this.ucaRecipientAccount.TabIndex = 8;
            // 
            // txtBankName
            // 
            this.txtBankName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBankName.Enabled = false;
            this.txtBankName.Location = new System.Drawing.Point(168, 84);
            this.txtBankName.Margin = new System.Windows.Forms.Padding(4);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(961, 22);
            this.txtBankName.TabIndex = 5;
            // 
            // lblBankName
            // 
            this.lblBankName.AutoSize = true;
            this.lblBankName.Location = new System.Drawing.Point(5, 87);
            this.lblBankName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(148, 16);
            this.lblBankName.TabIndex = 62;
            this.lblBankName.Text = "Наименование банка";
            // 
            // ucaCorrespondentAccount
            // 
            this.ucaCorrespondentAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucaCorrespondentAccount.Location = new System.Drawing.Point(524, 50);
            this.ucaCorrespondentAccount.Margin = new System.Windows.Forms.Padding(4);
            this.ucaCorrespondentAccount.MaxLength = 24;
            this.ucaCorrespondentAccount.Name = "ucaCorrespondentAccount";
            this.ucaCorrespondentAccount.Size = new System.Drawing.Size(605, 22);
            this.ucaCorrespondentAccount.TabIndex = 4;
            // 
            // lblCorrespondentAccount
            // 
            this.lblCorrespondentAccount.AutoSize = true;
            this.lblCorrespondentAccount.Location = new System.Drawing.Point(436, 54);
            this.lblCorrespondentAccount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCorrespondentAccount.Name = "lblCorrespondentAccount";
            this.lblCorrespondentAccount.Size = new System.Drawing.Size(75, 16);
            this.lblCorrespondentAccount.TabIndex = 63;
            this.lblCorrespondentAccount.Text = "Корр. счет";
            // 
            // udcRecipientBik
            // 
            this.udcRecipientBik.Location = new System.Drawing.Point(168, 50);
            this.udcRecipientBik.Margin = new System.Windows.Forms.Padding(4);
            this.udcRecipientBik.Name = "udcRecipientBik";
            this.udcRecipientBik.Size = new System.Drawing.Size(252, 22);
            this.udcRecipientBik.TabIndex = 3;
            this.udcRecipientBik.Text = "0";
            // 
            // lblRecipientBik
            // 
            this.lblRecipientBik.AutoSize = true;
            this.lblRecipientBik.Location = new System.Drawing.Point(5, 54);
            this.lblRecipientBik.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecipientBik.Name = "lblRecipientBik";
            this.lblRecipientBik.Size = new System.Drawing.Size(34, 16);
            this.lblRecipientBik.TabIndex = 64;
            this.lblRecipientBik.Text = "БИК";
            // 
            // btnRecipientClientClear
            // 
            this.btnRecipientClientClear.Location = new System.Drawing.Point(899, 16);
            this.btnRecipientClientClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnRecipientClientClear.Name = "btnRecipientClientClear";
            this.btnRecipientClientClear.Size = new System.Drawing.Size(37, 27);
            this.btnRecipientClientClear.TabIndex = 2;
            this.btnRecipientClientClear.Text = "X";
            this.btnRecipientClientClear.UseVisualStyleBackColor = true;
            this.btnRecipientClientClear.Click += new System.EventHandler(this.btnRecipientClientClear_Click);
            // 
            // txtRecipientClient
            // 
            this.txtRecipientClient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecipientClient.Enabled = false;
            this.txtRecipientClient.Location = new System.Drawing.Point(168, 18);
            this.txtRecipientClient.Margin = new System.Windows.Forms.Padding(4);
            this.txtRecipientClient.Name = "txtRecipientClient";
            this.txtRecipientClient.Size = new System.Drawing.Size(961, 22);
            this.txtRecipientClient.TabIndex = 1;
            // 
            // lblComment
            // 
            this.lblComment.Location = new System.Drawing.Point(16, 395);
            this.lblComment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(111, 22);
            this.lblComment.TabIndex = 56;
            this.lblComment.Text = "Наименование";
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.Location = new System.Drawing.Point(179, 391);
            this.txtComment.Margin = new System.Windows.Forms.Padding(4);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(961, 22);
            this.txtComment.TabIndex = 11;
            // 
            // tabPageCommission
            // 
            this.tabPageCommission.Controls.Add(this.grpRecipientCommission);
            this.tabPageCommission.Controls.Add(this.grpPayerCommission);
            this.tabPageCommission.Location = new System.Drawing.Point(4, 25);
            this.tabPageCommission.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageCommission.Name = "tabPageCommission";
            this.tabPageCommission.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageCommission.Size = new System.Drawing.Size(972, 670);
            this.tabPageCommission.TabIndex = 1;
            this.tabPageCommission.Text = "Комиссия";
            this.tabPageCommission.UseVisualStyleBackColor = true;
            // 
            // grpRecipientCommission
            // 
            this.grpRecipientCommission.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRecipientCommission.Controls.Add(this.chkRecipientCommissionReverse);
            this.grpRecipientCommission.Controls.Add(this.udcRecipientCommissionPercent);
            this.grpRecipientCommission.Controls.Add(this.lblRecipientCommissionPercent);
            this.grpRecipientCommission.Controls.Add(this.cmbRecipientCommissionType);
            this.grpRecipientCommission.Controls.Add(this.lblRecipientCommissionType);
            this.grpRecipientCommission.Location = new System.Drawing.Point(11, 98);
            this.grpRecipientCommission.Margin = new System.Windows.Forms.Padding(4);
            this.grpRecipientCommission.Name = "grpRecipientCommission";
            this.grpRecipientCommission.Padding = new System.Windows.Forms.Padding(4);
            this.grpRecipientCommission.Size = new System.Drawing.Size(2145, 1238);
            this.grpRecipientCommission.TabIndex = 1;
            this.grpRecipientCommission.TabStop = false;
            this.grpRecipientCommission.Text = "Комиссия с получателя";
            // 
            // chkRecipientCommissionReverse
            // 
            this.chkRecipientCommissionReverse.AutoSize = true;
            this.chkRecipientCommissionReverse.Location = new System.Drawing.Point(12, 85);
            this.chkRecipientCommissionReverse.Margin = new System.Windows.Forms.Padding(4);
            this.chkRecipientCommissionReverse.Name = "chkRecipientCommissionReverse";
            this.chkRecipientCommissionReverse.Size = new System.Drawing.Size(195, 20);
            this.chkRecipientCommissionReverse.TabIndex = 4;
            this.chkRecipientCommissionReverse.Text = "Обратный метод расчета";
            this.chkRecipientCommissionReverse.UseVisualStyleBackColor = true;
            // 
            // udcRecipientCommissionPercent
            // 
            this.udcRecipientCommissionPercent.Location = new System.Drawing.Point(147, 53);
            this.udcRecipientCommissionPercent.Margin = new System.Windows.Forms.Padding(4);
            this.udcRecipientCommissionPercent.Name = "udcRecipientCommissionPercent";
            this.udcRecipientCommissionPercent.Size = new System.Drawing.Size(219, 22);
            this.udcRecipientCommissionPercent.TabIndex = 3;
            this.udcRecipientCommissionPercent.Text = "0";
            // 
            // lblRecipientCommissionPercent
            // 
            this.lblRecipientCommissionPercent.AutoSize = true;
            this.lblRecipientCommissionPercent.Location = new System.Drawing.Point(8, 57);
            this.lblRecipientCommissionPercent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecipientCommissionPercent.Name = "lblRecipientCommissionPercent";
            this.lblRecipientCommissionPercent.Size = new System.Drawing.Size(121, 16);
            this.lblRecipientCommissionPercent.TabIndex = 2;
            this.lblRecipientCommissionPercent.Text = "размер комиссии";
            // 
            // cmbRecipientCommissionType
            // 
            this.cmbRecipientCommissionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecipientCommissionType.FormattingEnabled = true;
            this.cmbRecipientCommissionType.Location = new System.Drawing.Point(147, 20);
            this.cmbRecipientCommissionType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbRecipientCommissionType.Name = "cmbRecipientCommissionType";
            this.cmbRecipientCommissionType.Size = new System.Drawing.Size(643, 24);
            this.cmbRecipientCommissionType.TabIndex = 1;
            this.cmbRecipientCommissionType.SelectedIndexChanged += new System.EventHandler(this.cmbRecipientCommissionType_SelectedIndexChanged);
            // 
            // lblRecipientCommissionType
            // 
            this.lblRecipientCommissionType.AutoSize = true;
            this.lblRecipientCommissionType.Location = new System.Drawing.Point(8, 23);
            this.lblRecipientCommissionType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecipientCommissionType.Name = "lblRecipientCommissionType";
            this.lblRecipientCommissionType.Size = new System.Drawing.Size(95, 16);
            this.lblRecipientCommissionType.TabIndex = 0;
            this.lblRecipientCommissionType.Text = "тип комиссии";
            // 
            // grpPayerCommission
            // 
            this.grpPayerCommission.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPayerCommission.Controls.Add(this.udcPayerCommissionPercent);
            this.grpPayerCommission.Controls.Add(this.lblPayerCommissionPercent);
            this.grpPayerCommission.Controls.Add(this.cmbPayerCommissionType);
            this.grpPayerCommission.Controls.Add(this.lblPayerCommissionType);
            this.grpPayerCommission.Location = new System.Drawing.Point(11, 10);
            this.grpPayerCommission.Margin = new System.Windows.Forms.Padding(4);
            this.grpPayerCommission.Name = "grpPayerCommission";
            this.grpPayerCommission.Padding = new System.Windows.Forms.Padding(4);
            this.grpPayerCommission.Size = new System.Drawing.Size(2145, 1207);
            this.grpPayerCommission.TabIndex = 0;
            this.grpPayerCommission.TabStop = false;
            this.grpPayerCommission.Text = "Комиссия с плательщика";
            // 
            // udcPayerCommissionPercent
            // 
            this.udcPayerCommissionPercent.Location = new System.Drawing.Point(147, 50);
            this.udcPayerCommissionPercent.Margin = new System.Windows.Forms.Padding(4);
            this.udcPayerCommissionPercent.Name = "udcPayerCommissionPercent";
            this.udcPayerCommissionPercent.Size = new System.Drawing.Size(219, 22);
            this.udcPayerCommissionPercent.TabIndex = 3;
            this.udcPayerCommissionPercent.Text = "0";
            // 
            // lblPayerCommissionPercent
            // 
            this.lblPayerCommissionPercent.AutoSize = true;
            this.lblPayerCommissionPercent.Location = new System.Drawing.Point(8, 54);
            this.lblPayerCommissionPercent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPayerCommissionPercent.Name = "lblPayerCommissionPercent";
            this.lblPayerCommissionPercent.Size = new System.Drawing.Size(121, 16);
            this.lblPayerCommissionPercent.TabIndex = 2;
            this.lblPayerCommissionPercent.Text = "размер комиссии";
            // 
            // cmbPayerCommissionType
            // 
            this.cmbPayerCommissionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPayerCommissionType.FormattingEnabled = true;
            this.cmbPayerCommissionType.Location = new System.Drawing.Point(147, 21);
            this.cmbPayerCommissionType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPayerCommissionType.Name = "cmbPayerCommissionType";
            this.cmbPayerCommissionType.Size = new System.Drawing.Size(643, 24);
            this.cmbPayerCommissionType.TabIndex = 1;
            this.cmbPayerCommissionType.SelectedIndexChanged += new System.EventHandler(this.cmbPayerCommissionType_SelectedIndexChanged);
            // 
            // lblPayerCommissionType
            // 
            this.lblPayerCommissionType.AutoSize = true;
            this.lblPayerCommissionType.Location = new System.Drawing.Point(8, 25);
            this.lblPayerCommissionType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPayerCommissionType.Name = "lblPayerCommissionType";
            this.lblPayerCommissionType.Size = new System.Drawing.Size(95, 16);
            this.lblPayerCommissionType.TabIndex = 0;
            this.lblPayerCommissionType.Text = "тип комиссии";
            // 
            // tabPageAddFields
            // 
            this.tabPageAddFields.Controls.Add(this.ucfAdditionalFields);
            this.tabPageAddFields.Location = new System.Drawing.Point(4, 25);
            this.tabPageAddFields.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageAddFields.Name = "tabPageAddFields";
            this.tabPageAddFields.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageAddFields.Size = new System.Drawing.Size(515, 268);
            this.tabPageAddFields.TabIndex = 2;
            this.tabPageAddFields.Text = "Дополнительные свойства";
            this.tabPageAddFields.UseVisualStyleBackColor = true;
            // 
            // ucfAdditionalFields
            // 
            this.ucfAdditionalFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucfAdditionalFields.Location = new System.Drawing.Point(4, 4);
            this.ucfAdditionalFields.Margin = new System.Windows.Forms.Padding(5);
            this.ucfAdditionalFields.Name = "ucfAdditionalFields";
            this.ucfAdditionalFields.ReadOnly = false;
            this.ucfAdditionalFields.Size = new System.Drawing.Size(507, 260);
            this.ucfAdditionalFields.TabIndex = 0;
            // 
            // UbsPsContractFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 738);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "UbsPsContractFrm";
            this.Text = "Карточка договора";
            this.panelMain.ResumeLayout(false);
            this.tblActions.ResumeLayout(false);
            this.tblActions.PerformLayout();
            this.tabContract.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.pnlMainScroll.ResumeLayout(false);
            this.pnlMainScroll.PerformLayout();
            this.grpRecipient.ResumeLayout(false);
            this.grpRecipient.PerformLayout();
            this.tabPageCommission.ResumeLayout(false);
            this.grpRecipientCommission.ResumeLayout(false);
            this.grpRecipientCommission.PerformLayout();
            this.grpPayerCommission.ResumeLayout(false);
            this.grpPayerCommission.PerformLayout();
            this.tabPageAddFields.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblActions;
        private System.Windows.Forms.Button btnExit;
        private UbsControl.UbsCtrlInfo uciContract;
        private Button btnSave;
        private System.Windows.Forms.TabControl tabContract;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.TabPage tabPageCommission;
        private System.Windows.Forms.TabPage tabPageAddFields;
        private System.Windows.Forms.Panel pnlMainScroll;
        private System.Windows.Forms.Label lblContractCode;
        private System.Windows.Forms.TextBox txtContractCode;
        private System.Windows.Forms.Label lblContractNumber;
        private System.Windows.Forms.TextBox txtContractNumber;
        private System.Windows.Forms.Label lblExecutor;
        private System.Windows.Forms.ComboBox cmbExecutor;
        private System.Windows.Forms.Label lblContractDate;
        private UbsControl.UbsCtrlDate ucdContract;
        private System.Windows.Forms.Label lblContractStatus;
        private System.Windows.Forms.ComboBox cmbContractStatus;
        private System.Windows.Forms.Label lblCloseDate;
        private UbsControl.UbsCtrlDate ucdContractClose;
        private System.Windows.Forms.TextBox txtPaymentKindCode;
        private System.Windows.Forms.TextBox txtPaymentKindComment;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label lblArbitraryContract;
        private System.Windows.Forms.GroupBox grpRecipient;
        private System.Windows.Forms.TextBox txtRecipientClient;
        private System.Windows.Forms.Button btnRecipientClientClear;
        private System.Windows.Forms.Label lblRecipientBik;
        private UbsControl.UbsCtrlDecimal udcRecipientBik;
        private System.Windows.Forms.Label lblCorrespondentAccount;
        private UbsControl.UbsCtrlAccount ucaCorrespondentAccount;
        private System.Windows.Forms.Label lblBankName;
        private System.Windows.Forms.TextBox txtBankName;
        private UbsControl.UbsCtrlAccount ucaRecipientAccount;
        private System.Windows.Forms.Label lblRecipientInn;
        private UbsControl.UbsCtrlDecimal udcRecipientInn;
        private System.Windows.Forms.Label lblRecipientAddress;
        private System.Windows.Forms.TextBox txtRecipientAddress;
        private System.Windows.Forms.GroupBox grpPayerCommission;
        private System.Windows.Forms.Label lblPayerCommissionType;
        private System.Windows.Forms.ComboBox cmbPayerCommissionType;
        private System.Windows.Forms.Label lblPayerCommissionPercent;
        private UbsControl.UbsCtrlDecimal udcPayerCommissionPercent;
        private System.Windows.Forms.GroupBox grpRecipientCommission;
        private System.Windows.Forms.Label lblRecipientCommissionType;
        private System.Windows.Forms.ComboBox cmbRecipientCommissionType;
        private System.Windows.Forms.Label lblRecipientCommissionPercent;
        private UbsControl.UbsCtrlDecimal udcRecipientCommissionPercent;
        private System.Windows.Forms.CheckBox chkRecipientCommissionReverse;
        private UbsControl.UbsCtrlFields ucfAdditionalFields;
        private LinkLabel linkPaymentKind;
        private LinkLabel linkLabel1;
        private LinkLabel linkRecipientClient;
    }
}
