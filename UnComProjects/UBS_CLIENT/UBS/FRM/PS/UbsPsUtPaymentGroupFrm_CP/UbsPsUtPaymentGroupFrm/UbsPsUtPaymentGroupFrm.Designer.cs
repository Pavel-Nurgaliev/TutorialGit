using System;
using System.Drawing;
using System.Windows.Forms;

namespace UbsBusiness
{
    partial class UbsPsUtPaymentGroupFrm
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
            this.tblActions = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.uciInfo = new UbsControl.UbsCtrlInfo();
            this.tabPayment = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.pnlMainScroll = new System.Windows.Forms.Panel();
            this.grpPayer = new System.Windows.Forms.GroupBox();
            this.txtFIOPay = new System.Windows.Forms.TextBox();
            this.lblINNPayer = new System.Windows.Forms.Label();
            this.txtINNPay = new System.Windows.Forms.TextBox();
            this.lblAddressPay = new System.Windows.Forms.Label();
            this.txtAdressPay = new System.Windows.Forms.TextBox();
            this.lblRequisites = new System.Windows.Forms.Label();
            this.txtInfoClient = new System.Windows.Forms.TextBox();
            this.lblNomerCardPay = new System.Windows.Forms.Label();
            this.txtNomerCardPay = new System.Windows.Forms.TextBox();
            this.grpRecipient = new System.Windows.Forms.GroupBox();
            this.lblContractCode = new System.Windows.Forms.Label();
            this.cmbCode = new System.Windows.Forms.ComboBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.btnListAttributeRecip = new System.Windows.Forms.Button();
            this.btnSaveAttribute = new System.Windows.Forms.Button();
            this.lblBic = new System.Windows.Forms.Label();
            this.txtBic = new System.Windows.Forms.TextBox();
            this.lblCorrAccount = new System.Windows.Forms.Label();
            this.ucaAccKorr = new UbsControl.UbsCtrlAccount();
            this.lblBankName = new System.Windows.Forms.Label();
            this.txtNameBank = new System.Windows.Forms.TextBox();
            this.lblSettleAccount = new System.Windows.Forms.Label();
            this.ucaAccClient = new UbsControl.UbsCtrlAccount();
            this.lblINNRecipient = new System.Windows.Forms.Label();
            this.txtINN = new System.Windows.Forms.TextBox();
            this.lblPurpose = new System.Windows.Forms.Label();
            this.cmbPurpose = new System.Windows.Forms.ComboBox();
            this.lblRecip = new System.Windows.Forms.Label();
            this.txtRecip = new System.Windows.Forms.TextBox();
            this.lblSumma = new System.Windows.Forms.Label();
            this.udcSumma = new UbsControl.UbsCtrlDecimal();
            this.lblPeny = new System.Windows.Forms.Label();
            this.udcPeny = new UbsControl.UbsCtrlDecimal();
            this.lblCommission = new System.Windows.Forms.Label();
            this.udcSummaRateSend = new UbsControl.UbsCtrlDecimal();
            this.lblTotal = new System.Windows.Forms.Label();
            this.udcSummaTotal = new UbsControl.UbsCtrlDecimal();
            this.tabPageAddProperties = new System.Windows.Forms.TabPage();
            this.ucfAddProperties = new UbsControl.UbsCtrlFields();
            this.linkFIO = new System.Windows.Forms.LinkLabel();
            this.panelMain.SuspendLayout();
            this.tblActions.SuspendLayout();
            this.tabPayment.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.pnlMainScroll.SuspendLayout();
            this.grpPayer.SuspendLayout();
            this.grpRecipient.SuspendLayout();
            this.tabPageAddProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tabPayment);
            this.panelMain.Controls.Add(this.tblActions);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4);
            this.panelMain.Size = new System.Drawing.Size(735, 600);
            // 
            // tblActions
            // 
            this.tblActions.CausesValidation = false;
            this.tblActions.ColumnCount = 3;
            this.tblActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tblActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tblActions.Controls.Add(this.btnSave, 1, 0);
            this.tblActions.Controls.Add(this.btnExit, 2, 0);
            this.tblActions.Controls.Add(this.uciInfo, 0, 0);
            this.tblActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tblActions.Location = new System.Drawing.Point(0, 568);
            this.tblActions.Name = "tblActions";
            this.tblActions.RowCount = 1;
            this.tblActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblActions.Size = new System.Drawing.Size(735, 32);
            this.tblActions.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(562, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 26);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.CausesValidation = false;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(650, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 26);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // uciInfo
            // 
            this.uciInfo.AutoSize = true;
            this.uciInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uciInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.uciInfo.ForeColor = System.Drawing.SystemColors.Highlight;
            this.uciInfo.Interval = 25000;
            this.uciInfo.Location = new System.Drawing.Point(3, 19);
            this.uciInfo.Name = "uciInfo";
            this.uciInfo.Size = new System.Drawing.Size(553, 13);
            this.uciInfo.TabIndex = 0;
            this.uciInfo.Text = "uciInfo";
            this.uciInfo.Visible = false;
            // 
            // tabPayment
            // 
            this.tabPayment.Controls.Add(this.tabPageMain);
            this.tabPayment.Controls.Add(this.tabPageAddProperties);
            this.tabPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPayment.Location = new System.Drawing.Point(0, 0);
            this.tabPayment.Name = "tabPayment";
            this.tabPayment.SelectedIndex = 0;
            this.tabPayment.Size = new System.Drawing.Size(735, 568);
            this.tabPayment.TabIndex = 0;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.pnlMainScroll);
            this.tabPageMain.Location = new System.Drawing.Point(4, 22);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(727, 542);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "Основные";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // pnlMainScroll
            // 
            this.pnlMainScroll.AutoScroll = true;
            this.pnlMainScroll.Controls.Add(this.grpPayer);
            this.pnlMainScroll.Controls.Add(this.grpRecipient);
            this.pnlMainScroll.Controls.Add(this.lblSumma);
            this.pnlMainScroll.Controls.Add(this.udcSumma);
            this.pnlMainScroll.Controls.Add(this.lblPeny);
            this.pnlMainScroll.Controls.Add(this.udcPeny);
            this.pnlMainScroll.Controls.Add(this.lblCommission);
            this.pnlMainScroll.Controls.Add(this.udcSummaRateSend);
            this.pnlMainScroll.Controls.Add(this.lblTotal);
            this.pnlMainScroll.Controls.Add(this.udcSummaTotal);
            this.pnlMainScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainScroll.Location = new System.Drawing.Point(3, 3);
            this.pnlMainScroll.Name = "pnlMainScroll";
            this.pnlMainScroll.Size = new System.Drawing.Size(721, 536);
            this.pnlMainScroll.TabIndex = 0;
            // 
            // grpPayer
            // 
            this.grpPayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPayer.Controls.Add(this.linkFIO);
            this.grpPayer.Controls.Add(this.txtFIOPay);
            this.grpPayer.Controls.Add(this.lblINNPayer);
            this.grpPayer.Controls.Add(this.txtINNPay);
            this.grpPayer.Controls.Add(this.lblAddressPay);
            this.grpPayer.Controls.Add(this.txtAdressPay);
            this.grpPayer.Controls.Add(this.lblRequisites);
            this.grpPayer.Controls.Add(this.txtInfoClient);
            this.grpPayer.Controls.Add(this.lblNomerCardPay);
            this.grpPayer.Controls.Add(this.txtNomerCardPay);
            this.grpPayer.Location = new System.Drawing.Point(8, 3);
            this.grpPayer.Name = "grpPayer";
            this.grpPayer.Size = new System.Drawing.Size(702, 130);
            this.grpPayer.TabIndex = 0;
            this.grpPayer.TabStop = false;
            this.grpPayer.Text = "Плательщик";
            // 
            // txtFIOPay
            // 
            this.txtFIOPay.Location = new System.Drawing.Point(88, 15);
            this.txtFIOPay.MaxLength = 70;
            this.txtFIOPay.Name = "txtFIOPay";
            this.txtFIOPay.Size = new System.Drawing.Size(253, 20);
            this.txtFIOPay.TabIndex = 1;
            // 
            // lblINNPayer
            // 
            this.lblINNPayer.AutoSize = true;
            this.lblINNPayer.Location = new System.Drawing.Point(384, 18);
            this.lblINNPayer.Name = "lblINNPayer";
            this.lblINNPayer.Size = new System.Drawing.Size(31, 13);
            this.lblINNPayer.TabIndex = 51;
            this.lblINNPayer.Text = "ИНН";
            // 
            // txtINNPay
            // 
            this.txtINNPay.Location = new System.Drawing.Point(421, 15);
            this.txtINNPay.MaxLength = 12;
            this.txtINNPay.Name = "txtINNPay";
            this.txtINNPay.Size = new System.Drawing.Size(120, 20);
            this.txtINNPay.TabIndex = 3;
            // 
            // lblAddressPay
            // 
            this.lblAddressPay.AutoSize = true;
            this.lblAddressPay.Location = new System.Drawing.Point(6, 44);
            this.lblAddressPay.Name = "lblAddressPay";
            this.lblAddressPay.Size = new System.Drawing.Size(38, 13);
            this.lblAddressPay.TabIndex = 52;
            this.lblAddressPay.Text = "Адрес";
            // 
            // txtAdressPay
            // 
            this.txtAdressPay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAdressPay.Location = new System.Drawing.Point(88, 41);
            this.txtAdressPay.MaxLength = 255;
            this.txtAdressPay.Name = "txtAdressPay";
            this.txtAdressPay.Size = new System.Drawing.Size(608, 20);
            this.txtAdressPay.TabIndex = 4;
            // 
            // lblRequisites
            // 
            this.lblRequisites.AutoSize = true;
            this.lblRequisites.Location = new System.Drawing.Point(6, 70);
            this.lblRequisites.Name = "lblRequisites";
            this.lblRequisites.Size = new System.Drawing.Size(63, 13);
            this.lblRequisites.TabIndex = 53;
            this.lblRequisites.Text = "Реквизиты";
            // 
            // txtInfoClient
            // 
            this.txtInfoClient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInfoClient.Enabled = false;
            this.txtInfoClient.Location = new System.Drawing.Point(88, 67);
            this.txtInfoClient.Name = "txtInfoClient";
            this.txtInfoClient.Size = new System.Drawing.Size(608, 20);
            this.txtInfoClient.TabIndex = 5;
            // 
            // lblNomerCardPay
            // 
            this.lblNomerCardPay.AutoSize = true;
            this.lblNomerCardPay.Location = new System.Drawing.Point(6, 96);
            this.lblNomerCardPay.Name = "lblNomerCardPay";
            this.lblNomerCardPay.Size = new System.Drawing.Size(75, 13);
            this.lblNomerCardPay.TabIndex = 54;
            this.lblNomerCardPay.Text = "Номер карты";
            // 
            // txtNomerCardPay
            // 
            this.txtNomerCardPay.Location = new System.Drawing.Point(88, 93);
            this.txtNomerCardPay.Name = "txtNomerCardPay";
            this.txtNomerCardPay.Size = new System.Drawing.Size(253, 20);
            this.txtNomerCardPay.TabIndex = 6;
            // 
            // grpRecipient
            // 
            this.grpRecipient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRecipient.Controls.Add(this.lblContractCode);
            this.grpRecipient.Controls.Add(this.cmbCode);
            this.grpRecipient.Controls.Add(this.txtComment);
            this.grpRecipient.Controls.Add(this.btnListAttributeRecip);
            this.grpRecipient.Controls.Add(this.btnSaveAttribute);
            this.grpRecipient.Controls.Add(this.lblBic);
            this.grpRecipient.Controls.Add(this.txtBic);
            this.grpRecipient.Controls.Add(this.lblCorrAccount);
            this.grpRecipient.Controls.Add(this.ucaAccKorr);
            this.grpRecipient.Controls.Add(this.lblBankName);
            this.grpRecipient.Controls.Add(this.txtNameBank);
            this.grpRecipient.Controls.Add(this.lblSettleAccount);
            this.grpRecipient.Controls.Add(this.ucaAccClient);
            this.grpRecipient.Controls.Add(this.lblINNRecipient);
            this.grpRecipient.Controls.Add(this.txtINN);
            this.grpRecipient.Controls.Add(this.lblPurpose);
            this.grpRecipient.Controls.Add(this.cmbPurpose);
            this.grpRecipient.Controls.Add(this.lblRecip);
            this.grpRecipient.Controls.Add(this.txtRecip);
            this.grpRecipient.Location = new System.Drawing.Point(8, 139);
            this.grpRecipient.Name = "grpRecipient";
            this.grpRecipient.Size = new System.Drawing.Size(702, 230);
            this.grpRecipient.TabIndex = 1;
            this.grpRecipient.TabStop = false;
            this.grpRecipient.Text = "Получатель";
            // 
            // lblContractCode
            // 
            this.lblContractCode.AutoSize = true;
            this.lblContractCode.Location = new System.Drawing.Point(6, 18);
            this.lblContractCode.Name = "lblContractCode";
            this.lblContractCode.Size = new System.Drawing.Size(76, 13);
            this.lblContractCode.TabIndex = 50;
            this.lblContractCode.Text = "Код договора";
            // 
            // cmbCode
            // 
            this.cmbCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCode.FormattingEnabled = true;
            this.cmbCode.Location = new System.Drawing.Point(98, 15);
            this.cmbCode.Name = "cmbCode";
            this.cmbCode.Size = new System.Drawing.Size(140, 21);
            this.cmbCode.TabIndex = 7;
            this.cmbCode.SelectedIndexChanged += new System.EventHandler(this.cmbCode_SelectedIndexChanged);
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.Enabled = false;
            this.txtComment.Location = new System.Drawing.Point(263, 15);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(395, 20);
            this.txtComment.TabIndex = 8;
            // 
            // btnListAttributeRecip
            // 
            this.btnListAttributeRecip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnListAttributeRecip.Location = new System.Drawing.Point(668, 39);
            this.btnListAttributeRecip.Name = "btnListAttributeRecip";
            this.btnListAttributeRecip.Size = new System.Drawing.Size(28, 23);
            this.btnListAttributeRecip.TabIndex = 9;
            this.btnListAttributeRecip.Text = "...";
            this.btnListAttributeRecip.UseVisualStyleBackColor = true;
            this.btnListAttributeRecip.Click += new System.EventHandler(this.btnListAttributeRecip_Click);
            // 
            // btnSaveAttribute
            // 
            this.btnSaveAttribute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAttribute.Location = new System.Drawing.Point(668, 65);
            this.btnSaveAttribute.Name = "btnSaveAttribute";
            this.btnSaveAttribute.Size = new System.Drawing.Size(28, 23);
            this.btnSaveAttribute.TabIndex = 10;
            this.btnSaveAttribute.Text = "C";
            this.btnSaveAttribute.UseVisualStyleBackColor = true;
            this.btnSaveAttribute.Click += new System.EventHandler(this.btnSaveAttribute_Click);
            // 
            // lblBic
            // 
            this.lblBic.AutoSize = true;
            this.lblBic.Location = new System.Drawing.Point(6, 44);
            this.lblBic.Name = "lblBic";
            this.lblBic.Size = new System.Drawing.Size(29, 13);
            this.lblBic.TabIndex = 51;
            this.lblBic.Text = "БИК";
            // 
            // txtBic
            // 
            this.txtBic.Location = new System.Drawing.Point(98, 41);
            this.txtBic.Name = "txtBic";
            this.txtBic.Size = new System.Drawing.Size(140, 20);
            this.txtBic.TabIndex = 11;
            // 
            // lblCorrAccount
            // 
            this.lblCorrAccount.AutoSize = true;
            this.lblCorrAccount.Location = new System.Drawing.Point(292, 44);
            this.lblCorrAccount.Name = "lblCorrAccount";
            this.lblCorrAccount.Size = new System.Drawing.Size(60, 13);
            this.lblCorrAccount.TabIndex = 52;
            this.lblCorrAccount.Text = "Корр. счет";
            // 
            // ucaAccKorr
            // 
            this.ucaAccKorr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucaAccKorr.Enabled = false;
            this.ucaAccKorr.Location = new System.Drawing.Point(358, 41);
            this.ucaAccKorr.MaxLength = 24;
            this.ucaAccKorr.Name = "ucaAccKorr";
            this.ucaAccKorr.Size = new System.Drawing.Size(300, 20);
            this.ucaAccKorr.TabIndex = 12;
            // 
            // lblBankName
            // 
            this.lblBankName.Location = new System.Drawing.Point(6, 67);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Size = new System.Drawing.Size(95, 26);
            this.lblBankName.TabIndex = 53;
            this.lblBankName.Text = "Наименование банка";
            // 
            // txtNameBank
            // 
            this.txtNameBank.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNameBank.Enabled = false;
            this.txtNameBank.Location = new System.Drawing.Point(98, 67);
            this.txtNameBank.Name = "txtNameBank";
            this.txtNameBank.Size = new System.Drawing.Size(560, 20);
            this.txtNameBank.TabIndex = 13;
            // 
            // lblSettleAccount
            // 
            this.lblSettleAccount.AutoSize = true;
            this.lblSettleAccount.Location = new System.Drawing.Point(6, 96);
            this.lblSettleAccount.Name = "lblSettleAccount";
            this.lblSettleAccount.Size = new System.Drawing.Size(25, 13);
            this.lblSettleAccount.TabIndex = 54;
            this.lblSettleAccount.Text = "Р/с";
            // 
            // ucaAccClient
            // 
            this.ucaAccClient.Location = new System.Drawing.Point(98, 93);
            this.ucaAccClient.MaxLength = 24;
            this.ucaAccClient.Name = "ucaAccClient";
            this.ucaAccClient.Size = new System.Drawing.Size(200, 20);
            this.ucaAccClient.TabIndex = 14;
            // 
            // lblINNRecipient
            // 
            this.lblINNRecipient.AutoSize = true;
            this.lblINNRecipient.Location = new System.Drawing.Point(340, 96);
            this.lblINNRecipient.Name = "lblINNRecipient";
            this.lblINNRecipient.Size = new System.Drawing.Size(31, 13);
            this.lblINNRecipient.TabIndex = 55;
            this.lblINNRecipient.Text = "ИНН";
            // 
            // txtINN
            // 
            this.txtINN.Location = new System.Drawing.Point(377, 93);
            this.txtINN.MaxLength = 12;
            this.txtINN.Name = "txtINN";
            this.txtINN.Size = new System.Drawing.Size(140, 20);
            this.txtINN.TabIndex = 15;
            // 
            // lblPurpose
            // 
            this.lblPurpose.AutoSize = true;
            this.lblPurpose.Location = new System.Drawing.Point(6, 122);
            this.lblPurpose.Name = "lblPurpose";
            this.lblPurpose.Size = new System.Drawing.Size(68, 13);
            this.lblPurpose.TabIndex = 56;
            this.lblPurpose.Text = "Назначение";
            // 
            // cmbPurpose
            // 
            this.cmbPurpose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPurpose.FormattingEnabled = true;
            this.cmbPurpose.Location = new System.Drawing.Point(98, 119);
            this.cmbPurpose.Name = "cmbPurpose";
            this.cmbPurpose.Size = new System.Drawing.Size(560, 21);
            this.cmbPurpose.TabIndex = 16;
            // 
            // lblRecip
            // 
            this.lblRecip.Location = new System.Drawing.Point(6, 148);
            this.lblRecip.Name = "lblRecip";
            this.lblRecip.Size = new System.Drawing.Size(86, 30);
            this.lblRecip.TabIndex = 57;
            this.lblRecip.Text = "Наименование получателя";
            // 
            // txtRecip
            // 
            this.txtRecip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecip.Location = new System.Drawing.Point(98, 148);
            this.txtRecip.MaxLength = 160;
            this.txtRecip.Name = "txtRecip";
            this.txtRecip.Size = new System.Drawing.Size(570, 20);
            this.txtRecip.TabIndex = 17;
            // 
            // lblSumma
            // 
            this.lblSumma.AutoSize = true;
            this.lblSumma.Location = new System.Drawing.Point(8, 378);
            this.lblSumma.Name = "lblSumma";
            this.lblSumma.Size = new System.Drawing.Size(41, 13);
            this.lblSumma.TabIndex = 60;
            this.lblSumma.Text = "Сумма";
            // 
            // udcSumma
            // 
            this.udcSumma.Location = new System.Drawing.Point(96, 375);
            this.udcSumma.Name = "udcSumma";
            this.udcSumma.Size = new System.Drawing.Size(150, 20);
            this.udcSumma.TabIndex = 18;
            this.udcSumma.Text = "0";
            // 
            // lblPeny
            // 
            this.lblPeny.AutoSize = true;
            this.lblPeny.Location = new System.Drawing.Point(290, 378);
            this.lblPeny.Name = "lblPeny";
            this.lblPeny.Size = new System.Drawing.Size(68, 13);
            this.lblPeny.TabIndex = 61;
            this.lblPeny.Text = "Сумма пени";
            // 
            // udcPeny
            // 
            this.udcPeny.Location = new System.Drawing.Point(368, 375);
            this.udcPeny.Name = "udcPeny";
            this.udcPeny.Size = new System.Drawing.Size(150, 20);
            this.udcPeny.TabIndex = 19;
            this.udcPeny.Text = "0";
            // 
            // lblCommission
            // 
            this.lblCommission.Location = new System.Drawing.Point(8, 401);
            this.lblCommission.Name = "lblCommission";
            this.lblCommission.Size = new System.Drawing.Size(85, 30);
            this.lblCommission.TabIndex = 62;
            this.lblCommission.Text = "Комиссия с плательщика";
            // 
            // udcSummaRateSend
            // 
            this.udcSummaRateSend.Enabled = false;
            this.udcSummaRateSend.Location = new System.Drawing.Point(96, 401);
            this.udcSummaRateSend.Name = "udcSummaRateSend";
            this.udcSummaRateSend.Size = new System.Drawing.Size(150, 20);
            this.udcSummaRateSend.TabIndex = 20;
            this.udcSummaRateSend.Text = "0";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(8, 430);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(84, 13);
            this.lblTotal.TabIndex = 63;
            this.lblTotal.Text = "Итого к оплате";
            // 
            // udcSummaTotal
            // 
            this.udcSummaTotal.Enabled = false;
            this.udcSummaTotal.Location = new System.Drawing.Point(96, 427);
            this.udcSummaTotal.Name = "udcSummaTotal";
            this.udcSummaTotal.Size = new System.Drawing.Size(150, 20);
            this.udcSummaTotal.TabIndex = 21;
            this.udcSummaTotal.Text = "0";
            // 
            // tabPageAddProperties
            // 
            this.tabPageAddProperties.Controls.Add(this.ucfAddProperties);
            this.tabPageAddProperties.Location = new System.Drawing.Point(4, 22);
            this.tabPageAddProperties.Name = "tabPageAddProperties";
            this.tabPageAddProperties.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAddProperties.Size = new System.Drawing.Size(727, 542);
            this.tabPageAddProperties.TabIndex = 1;
            this.tabPageAddProperties.Text = "Дополнительные свойства";
            this.tabPageAddProperties.UseVisualStyleBackColor = true;
            // 
            // ucfAddProperties
            // 
            this.ucfAddProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucfAddProperties.Location = new System.Drawing.Point(3, 3);
            this.ucfAddProperties.Margin = new System.Windows.Forms.Padding(4);
            this.ucfAddProperties.Name = "ucfAddProperties";
            this.ucfAddProperties.ReadOnly = false;
            this.ucfAddProperties.Size = new System.Drawing.Size(721, 536);
            this.ucfAddProperties.TabIndex = 0;
            // 
            // linkFIO
            // 
            this.linkFIO.AutoSize = true;
            this.linkFIO.Location = new System.Drawing.Point(6, 18);
            this.linkFIO.Name = "linkFIO";
            this.linkFIO.Size = new System.Drawing.Size(34, 13);
            this.linkFIO.TabIndex = 50;
            this.linkFIO.TabStop = true;
            this.linkFIO.Text = "ФИО";
            this.linkFIO.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkFIO_LinkClicked);
            // 
            // UbsPsUtPaymentGroupFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 600);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UbsPsUtPaymentGroupFrm";
            this.Text = "Групповой платеж";
            this.panelMain.ResumeLayout(false);
            this.tblActions.ResumeLayout(false);
            this.tblActions.PerformLayout();
            this.tabPayment.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.pnlMainScroll.ResumeLayout(false);
            this.pnlMainScroll.PerformLayout();
            this.grpPayer.ResumeLayout(false);
            this.grpPayer.PerformLayout();
            this.grpRecipient.ResumeLayout(false);
            this.grpRecipient.PerformLayout();
            this.tabPageAddProperties.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblActions;
        private System.Windows.Forms.Button btnExit;
        private UbsControl.UbsCtrlInfo uciInfo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabControl tabPayment;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.TabPage tabPageAddProperties;
        private System.Windows.Forms.Panel pnlMainScroll;
        private System.Windows.Forms.GroupBox grpPayer;
        private System.Windows.Forms.TextBox txtFIOPay;
        private System.Windows.Forms.Label lblINNPayer;
        private System.Windows.Forms.TextBox txtINNPay;
        private System.Windows.Forms.Label lblAddressPay;
        private System.Windows.Forms.TextBox txtAdressPay;
        private System.Windows.Forms.Label lblRequisites;
        private System.Windows.Forms.TextBox txtInfoClient;
        private System.Windows.Forms.Label lblNomerCardPay;
        private System.Windows.Forms.TextBox txtNomerCardPay;
        private System.Windows.Forms.GroupBox grpRecipient;
        private System.Windows.Forms.Label lblContractCode;
        private System.Windows.Forms.ComboBox cmbCode;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Button btnListAttributeRecip;
        private System.Windows.Forms.Button btnSaveAttribute;
        private System.Windows.Forms.Label lblBic;
        private System.Windows.Forms.TextBox txtBic;
        private System.Windows.Forms.Label lblCorrAccount;
        private UbsControl.UbsCtrlAccount ucaAccKorr;
        private System.Windows.Forms.Label lblBankName;
        private System.Windows.Forms.TextBox txtNameBank;
        private System.Windows.Forms.Label lblSettleAccount;
        private UbsControl.UbsCtrlAccount ucaAccClient;
        private System.Windows.Forms.Label lblINNRecipient;
        private System.Windows.Forms.TextBox txtINN;
        private System.Windows.Forms.Label lblPurpose;
        private System.Windows.Forms.ComboBox cmbPurpose;
        private System.Windows.Forms.Label lblRecip;
        private System.Windows.Forms.TextBox txtRecip;
        private System.Windows.Forms.Label lblSumma;
        private UbsControl.UbsCtrlDecimal udcSumma;
        private System.Windows.Forms.Label lblPeny;
        private UbsControl.UbsCtrlDecimal udcPeny;
        private System.Windows.Forms.Label lblCommission;
        private UbsControl.UbsCtrlDecimal udcSummaRateSend;
        private System.Windows.Forms.Label lblTotal;
        private UbsControl.UbsCtrlDecimal udcSummaTotal;
        private UbsControl.UbsCtrlFields ucfAddProperties;
        private LinkLabel linkFIO;
    }
}
