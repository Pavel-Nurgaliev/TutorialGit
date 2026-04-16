using System;
using System.Drawing;
using System.Windows.Forms;

namespace UbsBusiness
{
    partial class UbsPsUtPaymentFrm
    {
        //private const int WM_CLOSE = 0x0010;

        //#region  WndProc

        ///// <summary>
        /////   
        ///// </summary>
        ///// <param name="m"></param>
        //protected override void WndProc(ref Message m)
        //{
        //    try
        //    {
        //        switch (m.Msg)
        //        {
        //            case WM_CLOSE:
        //                if (!btnExit.Focused)
        //                {
        //                    btnExit.Focus();
        //                    btnExit_Click(btnExit, EventArgs.Empty);
        //                    return;
        //                }
        //                break;
        //        }
        //        base.WndProc(ref m);
        //    }
        //    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        //}

        //#endregion


        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tabPayment = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlGeneralFooterArea = new System.Windows.Forms.Panel();
            this.linkPaymentAccount = new System.Windows.Forms.LinkLabel();
            this.lblCityCode = new System.Windows.Forms.Label();
            this.txtCheckSum = new System.Windows.Forms.TextBox();
            this.lblBatchNumber = new System.Windows.Forms.Label();
            this.txtBatchNumber = new System.Windows.Forms.TextBox();
            this.chkThirdPerson = new System.Windows.Forms.CheckBox();
            this.lblCashSymbolPayment = new System.Windows.Forms.Label();
            this.txtCashSymbolPayment = new System.Windows.Forms.TextBox();
            this.lblCashSymbolCommission = new System.Windows.Forms.Label();
            this.txtCashSymbolCommission = new System.Windows.Forms.TextBox();
            this.lblCashSymbolNds = new System.Windows.Forms.Label();
            this.txtCashSymbolNds = new System.Windows.Forms.TextBox();
            this.lblPaymentCode = new System.Windows.Forms.Label();
            this.txtPaymentCode = new System.Windows.Forms.TextBox();
            this.lblPaymentAmount = new System.Windows.Forms.Label();
            this.udcPaymentAmount = new UbsControl.UbsCtrlDecimal();
            this.lblPenaltyAmount = new System.Windows.Forms.Label();
            this.udcPenaltyAmount = new UbsControl.UbsCtrlDecimal();
            this.lblPayerRateAmount = new System.Windows.Forms.Label();
            this.udcPayerRateAmount = new UbsControl.UbsCtrlDecimal();
            this.lblAmountWithRate = new System.Windows.Forms.Label();
            this.udcAmountWithRate = new UbsControl.UbsCtrlDecimal();
            this.lblPeriodBeg = new System.Windows.Forms.Label();
            this.cmbCityCode = new System.Windows.Forms.ComboBox();
            this.txtPeriodDayBeg = new System.Windows.Forms.TextBox();
            this.txtPeriodMonthBeg = new System.Windows.Forms.TextBox();
            this.txtPeriodYearBeg = new System.Windows.Forms.TextBox();
            this.lblPeriodEnd = new System.Windows.Forms.Label();
            this.txtPeriodDayEnd = new System.Windows.Forms.TextBox();
            this.txtPeriodMonthEnd = new System.Windows.Forms.TextBox();
            this.txtPeriodYearEnd = new System.Windows.Forms.TextBox();
            this.txtPayerAccount = new System.Windows.Forms.TextBox();
            this.grpRecipient = new System.Windows.Forms.GroupBox();
            this.linkContractCode = new System.Windows.Forms.LinkLabel();
            this.linkRecipientBankName = new System.Windows.Forms.LinkLabel();
            this.lblCharCount160 = new System.Windows.Forms.Label();
            this.lblCharCount210 = new System.Windows.Forms.Label();
            this.txtContractCode = new System.Windows.Forms.TextBox();
            this.txtRecipientComment = new System.Windows.Forms.TextBox();
            this.lblRecipientBik = new System.Windows.Forms.Label();
            this.txtRecipientBik = new System.Windows.Forms.TextBox();
            this.lblRecipientCorrAccount = new System.Windows.Forms.Label();
            this.ucaRecipientCorrAccount = new UbsControl.UbsCtrlAccount();
            this.txtRecipientBankName = new System.Windows.Forms.TextBox();
            this.lblRecipientAccount = new System.Windows.Forms.Label();
            this.ucaRecipientAccount = new UbsControl.UbsCtrlAccount();
            this.lblRecipientInn = new System.Windows.Forms.Label();
            this.txtRecipientInn = new System.Windows.Forms.TextBox();
            this.lblRecipientKpp = new System.Windows.Forms.Label();
            this.txtRecipientKpp = new System.Windows.Forms.TextBox();
            this.lblRecipientPurpose = new System.Windows.Forms.Label();
            this.cmbPurpose = new System.Windows.Forms.ComboBox();
            this.lblRecipientName = new System.Windows.Forms.Label();
            this.txtRecipientName = new System.Windows.Forms.TextBox();
            this.txtRecipientNote = new System.Windows.Forms.TextBox();
            this.btnSaveRecipientAttribute = new System.Windows.Forms.Button();
            this.grpSender = new System.Windows.Forms.GroupBox();
            this.ucaPayerAccount = new UbsControl.UbsCtrlAccount();
            this.linkPayerFullName = new System.Windows.Forms.LinkLabel();
            this.linkFindFilter = new System.Windows.Forms.LinkLabel();
            this.txtPayerFullName = new System.Windows.Forms.TextBox();
            this.lblPayerInn = new System.Windows.Forms.Label();
            this.txtPayerInn = new System.Windows.Forms.TextBox();
            this.lblPayerAddress = new System.Windows.Forms.Label();
            this.txtPayerAddress = new System.Windows.Forms.TextBox();
            this.lblPayerClientInfo = new System.Windows.Forms.Label();
            this.txtPayerClientInfo = new System.Windows.Forms.TextBox();
            this.lblSourceMeans = new System.Windows.Forms.Label();
            this.lblPayerCardNumber = new System.Windows.Forms.Label();
            this.txtPayerCardNumber = new System.Windows.Forms.TextBox();
            this.chkBenefits = new System.Windows.Forms.CheckBox();
            this.lblBenefitReason = new System.Windows.Forms.Label();
            this.txtBenefitReason = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ubsCtrlInfo1 = new UbsControl.UbsCtrlInfo();
            this.uciInfo = new UbsControl.UbsCtrlInfo();
            this.tabPageThirdPerson = new System.Windows.Forms.TabPage();
            this.linkThirdPersonName = new System.Windows.Forms.LinkLabel();
            this.txtThirdPersonName = new System.Windows.Forms.TextBox();
            this.lblThirdPersonKind = new System.Windows.Forms.Label();
            this.cmbThirdPersonKind = new System.Windows.Forms.ComboBox();
            this.lblThirdPersonInn = new System.Windows.Forms.Label();
            this.txtThirdPersonInn = new System.Windows.Forms.TextBox();
            this.lblThirdPersonKpp = new System.Windows.Forms.Label();
            this.txtThirdPersonKpp = new System.Windows.Forms.TextBox();
            this.tabPageTariff = new System.Windows.Forms.TabPage();
            this.lblTariff = new System.Windows.Forms.Label();
            this.cmbTariff = new System.Windows.Forms.ComboBox();
            this.tabPageTelephone = new System.Windows.Forms.TabPage();
            this.lblPhone = new System.Windows.Forms.Label();
            this.cmbPhone = new System.Windows.Forms.ComboBox();
            this.tabPageTax = new System.Windows.Forms.TabPage();
            this.lblTaxStatus = new System.Windows.Forms.Label();
            this.txtTaxStatus = new System.Windows.Forms.TextBox();
            this.lblTaxKbk = new System.Windows.Forms.Label();
            this.txtTaxKbk = new System.Windows.Forms.TextBox();
            this.lblTaxOkato = new System.Windows.Forms.Label();
            this.txtTaxOkato = new System.Windows.Forms.TextBox();
            this.lblTaxReasonCode = new System.Windows.Forms.Label();
            this.txtTaxReasonCode = new System.Windows.Forms.TextBox();
            this.lblTaxPeriodCode = new System.Windows.Forms.Label();
            this.txtTaxPeriodCode = new System.Windows.Forms.TextBox();
            this.lblTaxDocumentNumber = new System.Windows.Forms.Label();
            this.txtTaxDocumentNumber = new System.Windows.Forms.TextBox();
            this.lblTaxDocumentDate = new System.Windows.Forms.Label();
            this.txtTaxDocumentDate = new System.Windows.Forms.TextBox();
            this.lblTaxType = new System.Windows.Forms.Label();
            this.txtTaxType = new System.Windows.Forms.TextBox();
            this.lblTaxImns = new System.Windows.Forms.Label();
            this.txtTaxImns = new System.Windows.Forms.TextBox();
            this.lblTaxKbkNote = new System.Windows.Forms.Label();
            this.txtTaxKbkNote = new System.Windows.Forms.TextBox();
            this.tabPageAddFields = new System.Windows.Forms.TabPage();
            this.ucfAddProperties = new UbsControl.UbsCtrlFields();
            this.tblActions = new System.Windows.Forms.TableLayoutPanel();
            this.btnCashSymb = new System.Windows.Forms.Button();
            this.btnPattern = new System.Windows.Forms.Button();
            this.btnCalc = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSubPaymentCount = new System.Windows.Forms.Label();
            this.txtGroupPaymentId = new System.Windows.Forms.TextBox();
            this.chkPrintForms = new System.Windows.Forms.CheckBox();
            this.udcCommonAmount = new UbsControl.UbsCtrlDecimal();
            this.lblCommonAmount = new System.Windows.Forms.Label();
            this.txtSubPaymentCount = new System.Windows.Forms.TextBox();
            this.udcTotalAmount = new UbsControl.UbsCtrlDecimal();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelMain.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPayment.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlGeneralFooterArea.SuspendLayout();
            this.grpRecipient.SuspendLayout();
            this.grpSender.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPageThirdPerson.SuspendLayout();
            this.tabPageTariff.SuspendLayout();
            this.tabPageTelephone.SuspendLayout();
            this.tabPageTax.SuspendLayout();
            this.tabPageAddFields.SuspendLayout();
            this.tblActions.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tableLayoutPanel3);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4);
            this.panelMain.Size = new System.Drawing.Size(636, 793);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tabPayment, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tblActions, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(636, 793);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // tabPayment
            // 
            this.tabPayment.Controls.Add(this.tabPageGeneral);
            this.tabPayment.Controls.Add(this.tabPageThirdPerson);
            this.tabPayment.Controls.Add(this.tabPageTariff);
            this.tabPayment.Controls.Add(this.tabPageTelephone);
            this.tabPayment.Controls.Add(this.tabPageTax);
            this.tabPayment.Controls.Add(this.tabPageAddFields);
            this.tabPayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPayment.Location = new System.Drawing.Point(3, 3);
            this.tabPayment.Name = "tabPayment";
            this.tabPayment.SelectedIndex = 0;
            this.tabPayment.Size = new System.Drawing.Size(630, 704);
            this.tabPayment.TabIndex = 3;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.tableLayoutPanel1);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(622, 678);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "Основные";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnlGeneralFooterArea, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.grpRecipient, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.grpSender, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(616, 672);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pnlGeneralFooterArea
            // 
            this.pnlGeneralFooterArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGeneralFooterArea.Controls.Add(this.linkPaymentAccount);
            this.pnlGeneralFooterArea.Controls.Add(this.lblCityCode);
            this.pnlGeneralFooterArea.Controls.Add(this.txtCheckSum);
            this.pnlGeneralFooterArea.Controls.Add(this.lblBatchNumber);
            this.pnlGeneralFooterArea.Controls.Add(this.txtBatchNumber);
            this.pnlGeneralFooterArea.Controls.Add(this.chkThirdPerson);
            this.pnlGeneralFooterArea.Controls.Add(this.lblCashSymbolPayment);
            this.pnlGeneralFooterArea.Controls.Add(this.txtCashSymbolPayment);
            this.pnlGeneralFooterArea.Controls.Add(this.lblCashSymbolCommission);
            this.pnlGeneralFooterArea.Controls.Add(this.txtCashSymbolCommission);
            this.pnlGeneralFooterArea.Controls.Add(this.lblCashSymbolNds);
            this.pnlGeneralFooterArea.Controls.Add(this.txtCashSymbolNds);
            this.pnlGeneralFooterArea.Controls.Add(this.lblPaymentCode);
            this.pnlGeneralFooterArea.Controls.Add(this.txtPaymentCode);
            this.pnlGeneralFooterArea.Controls.Add(this.lblPaymentAmount);
            this.pnlGeneralFooterArea.Controls.Add(this.udcPaymentAmount);
            this.pnlGeneralFooterArea.Controls.Add(this.lblPenaltyAmount);
            this.pnlGeneralFooterArea.Controls.Add(this.udcPenaltyAmount);
            this.pnlGeneralFooterArea.Controls.Add(this.lblPayerRateAmount);
            this.pnlGeneralFooterArea.Controls.Add(this.udcPayerRateAmount);
            this.pnlGeneralFooterArea.Controls.Add(this.lblAmountWithRate);
            this.pnlGeneralFooterArea.Controls.Add(this.udcAmountWithRate);
            this.pnlGeneralFooterArea.Controls.Add(this.lblPeriodBeg);
            this.pnlGeneralFooterArea.Controls.Add(this.cmbCityCode);
            this.pnlGeneralFooterArea.Controls.Add(this.txtPeriodDayBeg);
            this.pnlGeneralFooterArea.Controls.Add(this.txtPeriodMonthBeg);
            this.pnlGeneralFooterArea.Controls.Add(this.txtPeriodYearBeg);
            this.pnlGeneralFooterArea.Controls.Add(this.lblPeriodEnd);
            this.pnlGeneralFooterArea.Controls.Add(this.txtPeriodDayEnd);
            this.pnlGeneralFooterArea.Controls.Add(this.txtPeriodMonthEnd);
            this.pnlGeneralFooterArea.Controls.Add(this.txtPeriodYearEnd);
            this.pnlGeneralFooterArea.Controls.Add(this.txtPayerAccount);
            this.pnlGeneralFooterArea.Location = new System.Drawing.Point(3, 348);
            this.pnlGeneralFooterArea.Name = "pnlGeneralFooterArea";
            this.pnlGeneralFooterArea.Size = new System.Drawing.Size(610, 192);
            this.pnlGeneralFooterArea.TabIndex = 3;
            // 
            // linkPaymentAccount
            // 
            this.linkPaymentAccount.Location = new System.Drawing.Point(253, 122);
            this.linkPaymentAccount.Name = "linkPaymentAccount";
            this.linkPaymentAccount.Size = new System.Drawing.Size(73, 34);
            this.linkPaymentAccount.TabIndex = 44;
            this.linkPaymentAccount.TabStop = true;
            this.linkPaymentAccount.Text = "Л/с плательщика";
            this.linkPaymentAccount.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkPaymentAccount_LinkClicked);
            // 
            // lblCityCode
            // 
            this.lblCityCode.AutoSize = true;
            this.lblCityCode.Location = new System.Drawing.Point(253, 160);
            this.lblCityCode.Name = "lblCityCode";
            this.lblCityCode.Size = new System.Drawing.Size(64, 13);
            this.lblCityCode.TabIndex = 43;
            this.lblCityCode.Text = "Код города";
            // 
            // txtCheckSum
            // 
            this.txtCheckSum.Location = new System.Drawing.Point(514, 122);
            this.txtCheckSum.Name = "txtCheckSum";
            this.txtCheckSum.Size = new System.Drawing.Size(24, 20);
            this.txtCheckSum.TabIndex = 41;
            // 
            // lblBatchNumber
            // 
            this.lblBatchNumber.AutoSize = true;
            this.lblBatchNumber.Location = new System.Drawing.Point(435, 10);
            this.lblBatchNumber.Name = "lblBatchNumber";
            this.lblBatchNumber.Size = new System.Drawing.Size(73, 13);
            this.lblBatchNumber.TabIndex = 39;
            this.lblBatchNumber.Text = "Номер пачки";
            // 
            // txtBatchNumber
            // 
            this.txtBatchNumber.Location = new System.Drawing.Point(514, 7);
            this.txtBatchNumber.Name = "txtBatchNumber";
            this.txtBatchNumber.Size = new System.Drawing.Size(89, 20);
            this.txtBatchNumber.TabIndex = 40;
            // 
            // chkThirdPerson
            // 
            this.chkThirdPerson.AutoSize = true;
            this.chkThirdPerson.Location = new System.Drawing.Point(11, 9);
            this.chkThirdPerson.Name = "chkThirdPerson";
            this.chkThirdPerson.Size = new System.Drawing.Size(142, 17);
            this.chkThirdPerson.TabIndex = 38;
            this.chkThirdPerson.Text = "Оплата за третье лицо";
            this.chkThirdPerson.UseVisualStyleBackColor = true;
            this.chkThirdPerson.CheckedChanged += new System.EventHandler(this.chkThirdPerson_CheckedChanged);
            // 
            // lblCashSymbolPayment
            // 
            this.lblCashSymbolPayment.Location = new System.Drawing.Point(8, 37);
            this.lblCashSymbolPayment.Name = "lblCashSymbolPayment";
            this.lblCashSymbolPayment.Size = new System.Drawing.Size(64, 27);
            this.lblCashSymbolPayment.TabIndex = 0;
            this.lblCashSymbolPayment.Text = "Касс/симв платежа";
            // 
            // txtCashSymbolPayment
            // 
            this.txtCashSymbolPayment.Location = new System.Drawing.Point(75, 39);
            this.txtCashSymbolPayment.Name = "txtCashSymbolPayment";
            this.txtCashSymbolPayment.Size = new System.Drawing.Size(50, 20);
            this.txtCashSymbolPayment.TabIndex = 1;
            // 
            // lblCashSymbolCommission
            // 
            this.lblCashSymbolCommission.Location = new System.Drawing.Point(127, 37);
            this.lblCashSymbolCommission.Name = "lblCashSymbolCommission";
            this.lblCashSymbolCommission.Size = new System.Drawing.Size(66, 27);
            this.lblCashSymbolCommission.TabIndex = 2;
            this.lblCashSymbolCommission.Text = "Касс/симв комиссия";
            // 
            // txtCashSymbolCommission
            // 
            this.txtCashSymbolCommission.Location = new System.Drawing.Point(191, 39);
            this.txtCashSymbolCommission.Name = "txtCashSymbolCommission";
            this.txtCashSymbolCommission.Size = new System.Drawing.Size(50, 20);
            this.txtCashSymbolCommission.TabIndex = 3;
            // 
            // lblCashSymbolNds
            // 
            this.lblCashSymbolNds.Location = new System.Drawing.Point(247, 37);
            this.lblCashSymbolNds.Name = "lblCashSymbolNds";
            this.lblCashSymbolNds.Size = new System.Drawing.Size(63, 27);
            this.lblCashSymbolNds.TabIndex = 4;
            this.lblCashSymbolNds.Text = "Касс/симв НДС";
            // 
            // txtCashSymbolNds
            // 
            this.txtCashSymbolNds.Location = new System.Drawing.Point(314, 39);
            this.txtCashSymbolNds.Name = "txtCashSymbolNds";
            this.txtCashSymbolNds.Size = new System.Drawing.Size(50, 20);
            this.txtCashSymbolNds.TabIndex = 5;
            // 
            // lblPaymentCode
            // 
            this.lblPaymentCode.AutoSize = true;
            this.lblPaymentCode.Location = new System.Drawing.Point(370, 42);
            this.lblPaymentCode.Name = "lblPaymentCode";
            this.lblPaymentCode.Size = new System.Drawing.Size(72, 13);
            this.lblPaymentCode.TabIndex = 6;
            this.lblPaymentCode.Text = "Код платежа";
            // 
            // txtPaymentCode
            // 
            this.txtPaymentCode.Location = new System.Drawing.Point(446, 39);
            this.txtPaymentCode.Name = "txtPaymentCode";
            this.txtPaymentCode.Size = new System.Drawing.Size(158, 20);
            this.txtPaymentCode.TabIndex = 7;
            // 
            // lblPaymentAmount
            // 
            this.lblPaymentAmount.AutoSize = true;
            this.lblPaymentAmount.Location = new System.Drawing.Point(7, 70);
            this.lblPaymentAmount.Name = "lblPaymentAmount";
            this.lblPaymentAmount.Size = new System.Drawing.Size(41, 13);
            this.lblPaymentAmount.TabIndex = 9;
            this.lblPaymentAmount.Text = "Сумма";
            // 
            // udcPaymentAmount
            // 
            this.udcPaymentAmount.Location = new System.Drawing.Point(97, 67);
            this.udcPaymentAmount.Name = "udcPaymentAmount";
            this.udcPaymentAmount.Size = new System.Drawing.Size(144, 20);
            this.udcPaymentAmount.TabIndex = 10;
            this.udcPaymentAmount.Text = "0";
            // 
            // lblPenaltyAmount
            // 
            this.lblPenaltyAmount.AutoSize = true;
            this.lblPenaltyAmount.Location = new System.Drawing.Point(253, 70);
            this.lblPenaltyAmount.Name = "lblPenaltyAmount";
            this.lblPenaltyAmount.Size = new System.Drawing.Size(68, 13);
            this.lblPenaltyAmount.TabIndex = 11;
            this.lblPenaltyAmount.Text = "Сумма пени";
            // 
            // udcPenaltyAmount
            // 
            this.udcPenaltyAmount.Location = new System.Drawing.Point(333, 67);
            this.udcPenaltyAmount.Name = "udcPenaltyAmount";
            this.udcPenaltyAmount.Size = new System.Drawing.Size(150, 20);
            this.udcPenaltyAmount.TabIndex = 12;
            this.udcPenaltyAmount.Text = "0";
            // 
            // lblPayerRateAmount
            // 
            this.lblPayerRateAmount.Location = new System.Drawing.Point(7, 93);
            this.lblPayerRateAmount.Name = "lblPayerRateAmount";
            this.lblPayerRateAmount.Size = new System.Drawing.Size(76, 28);
            this.lblPayerRateAmount.TabIndex = 13;
            this.lblPayerRateAmount.Text = "Комиссия плательщика";
            // 
            // udcPayerRateAmount
            // 
            this.udcPayerRateAmount.Enabled = false;
            this.udcPayerRateAmount.Location = new System.Drawing.Point(97, 93);
            this.udcPayerRateAmount.Name = "udcPayerRateAmount";
            this.udcPayerRateAmount.Size = new System.Drawing.Size(144, 20);
            this.udcPayerRateAmount.TabIndex = 14;
            this.udcPayerRateAmount.Text = "0";
            // 
            // lblAmountWithRate
            // 
            this.lblAmountWithRate.AutoSize = true;
            this.lblAmountWithRate.Location = new System.Drawing.Point(7, 122);
            this.lblAmountWithRate.Name = "lblAmountWithRate";
            this.lblAmountWithRate.Size = new System.Drawing.Size(84, 13);
            this.lblAmountWithRate.TabIndex = 15;
            this.lblAmountWithRate.Text = "Итого к оплате";
            // 
            // udcAmountWithRate
            // 
            this.udcAmountWithRate.Enabled = false;
            this.udcAmountWithRate.Location = new System.Drawing.Point(97, 119);
            this.udcAmountWithRate.Name = "udcAmountWithRate";
            this.udcAmountWithRate.Size = new System.Drawing.Size(144, 20);
            this.udcAmountWithRate.TabIndex = 16;
            this.udcAmountWithRate.Text = "0";
            // 
            // lblPeriodBeg
            // 
            this.lblPeriodBeg.AutoSize = true;
            this.lblPeriodBeg.Location = new System.Drawing.Point(253, 96);
            this.lblPeriodBeg.Name = "lblPeriodBeg";
            this.lblPeriodBeg.Size = new System.Drawing.Size(54, 13);
            this.lblPeriodBeg.TabIndex = 17;
            this.lblPeriodBeg.Text = "Период с";
            // 
            // cmbCityCode
            // 
            this.cmbCityCode.FormattingEnabled = true;
            this.cmbCityCode.Location = new System.Drawing.Point(333, 157);
            this.cmbCityCode.Name = "cmbCityCode";
            this.cmbCityCode.Size = new System.Drawing.Size(67, 21);
            this.cmbCityCode.TabIndex = 18;
            // 
            // txtPeriodDayBeg
            // 
            this.txtPeriodDayBeg.Location = new System.Drawing.Point(333, 93);
            this.txtPeriodDayBeg.Name = "txtPeriodDayBeg";
            this.txtPeriodDayBeg.Size = new System.Drawing.Size(24, 20);
            this.txtPeriodDayBeg.TabIndex = 19;
            // 
            // txtPeriodMonthBeg
            // 
            this.txtPeriodMonthBeg.Location = new System.Drawing.Point(363, 93);
            this.txtPeriodMonthBeg.Name = "txtPeriodMonthBeg";
            this.txtPeriodMonthBeg.Size = new System.Drawing.Size(24, 20);
            this.txtPeriodMonthBeg.TabIndex = 20;
            // 
            // txtPeriodYearBeg
            // 
            this.txtPeriodYearBeg.Location = new System.Drawing.Point(393, 93);
            this.txtPeriodYearBeg.Name = "txtPeriodYearBeg";
            this.txtPeriodYearBeg.Size = new System.Drawing.Size(42, 20);
            this.txtPeriodYearBeg.TabIndex = 21;
            // 
            // lblPeriodEnd
            // 
            this.lblPeriodEnd.AutoSize = true;
            this.lblPeriodEnd.Location = new System.Drawing.Point(440, 96);
            this.lblPeriodEnd.Name = "lblPeriodEnd";
            this.lblPeriodEnd.Size = new System.Drawing.Size(19, 13);
            this.lblPeriodEnd.TabIndex = 22;
            this.lblPeriodEnd.Text = "по";
            // 
            // txtPeriodDayEnd
            // 
            this.txtPeriodDayEnd.Location = new System.Drawing.Point(465, 93);
            this.txtPeriodDayEnd.Name = "txtPeriodDayEnd";
            this.txtPeriodDayEnd.Size = new System.Drawing.Size(24, 20);
            this.txtPeriodDayEnd.TabIndex = 23;
            // 
            // txtPeriodMonthEnd
            // 
            this.txtPeriodMonthEnd.Location = new System.Drawing.Point(495, 93);
            this.txtPeriodMonthEnd.Name = "txtPeriodMonthEnd";
            this.txtPeriodMonthEnd.Size = new System.Drawing.Size(24, 20);
            this.txtPeriodMonthEnd.TabIndex = 24;
            // 
            // txtPeriodYearEnd
            // 
            this.txtPeriodYearEnd.Location = new System.Drawing.Point(525, 93);
            this.txtPeriodYearEnd.Name = "txtPeriodYearEnd";
            this.txtPeriodYearEnd.Size = new System.Drawing.Size(42, 20);
            this.txtPeriodYearEnd.TabIndex = 25;
            // 
            // txtPayerAccount
            // 
            this.txtPayerAccount.Location = new System.Drawing.Point(333, 122);
            this.txtPayerAccount.Name = "txtPayerAccount";
            this.txtPayerAccount.Size = new System.Drawing.Size(175, 20);
            this.txtPayerAccount.TabIndex = 27;
            // 
            // grpRecipient
            // 
            this.grpRecipient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRecipient.Controls.Add(this.linkContractCode);
            this.grpRecipient.Controls.Add(this.linkRecipientBankName);
            this.grpRecipient.Controls.Add(this.lblCharCount160);
            this.grpRecipient.Controls.Add(this.lblCharCount210);
            this.grpRecipient.Controls.Add(this.txtContractCode);
            this.grpRecipient.Controls.Add(this.txtRecipientComment);
            this.grpRecipient.Controls.Add(this.lblRecipientBik);
            this.grpRecipient.Controls.Add(this.txtRecipientBik);
            this.grpRecipient.Controls.Add(this.lblRecipientCorrAccount);
            this.grpRecipient.Controls.Add(this.ucaRecipientCorrAccount);
            this.grpRecipient.Controls.Add(this.txtRecipientBankName);
            this.grpRecipient.Controls.Add(this.lblRecipientAccount);
            this.grpRecipient.Controls.Add(this.ucaRecipientAccount);
            this.grpRecipient.Controls.Add(this.lblRecipientInn);
            this.grpRecipient.Controls.Add(this.txtRecipientInn);
            this.grpRecipient.Controls.Add(this.lblRecipientKpp);
            this.grpRecipient.Controls.Add(this.txtRecipientKpp);
            this.grpRecipient.Controls.Add(this.lblRecipientPurpose);
            this.grpRecipient.Controls.Add(this.cmbPurpose);
            this.grpRecipient.Controls.Add(this.lblRecipientName);
            this.grpRecipient.Controls.Add(this.txtRecipientName);
            this.grpRecipient.Controls.Add(this.txtRecipientNote);
            this.grpRecipient.Controls.Add(this.btnSaveRecipientAttribute);
            this.grpRecipient.Location = new System.Drawing.Point(3, 159);
            this.grpRecipient.Name = "grpRecipient";
            this.grpRecipient.Size = new System.Drawing.Size(610, 183);
            this.grpRecipient.TabIndex = 2;
            this.grpRecipient.TabStop = false;
            this.grpRecipient.Text = "Получатель";
            // 
            // linkContractCode
            // 
            this.linkContractCode.AutoSize = true;
            this.linkContractCode.Location = new System.Drawing.Point(8, 22);
            this.linkContractCode.Name = "linkContractCode";
            this.linkContractCode.Size = new System.Drawing.Size(76, 13);
            this.linkContractCode.TabIndex = 28;
            this.linkContractCode.TabStop = true;
            this.linkContractCode.Text = "Код договора";
            this.linkContractCode.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkContractCode_LinkClicked);
            // 
            // linkRecipientBankName
            // 
            this.linkRecipientBankName.Location = new System.Drawing.Point(8, 68);
            this.linkRecipientBankName.Name = "linkRecipientBankName";
            this.linkRecipientBankName.Size = new System.Drawing.Size(83, 32);
            this.linkRecipientBankName.TabIndex = 27;
            this.linkRecipientBankName.TabStop = true;
            this.linkRecipientBankName.Text = "Наименование банка";
            this.linkRecipientBankName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkRecipientBankName_LinkClicked);
            // 
            // lblCharCount160
            // 
            this.lblCharCount160.AutoSize = true;
            this.lblCharCount160.Location = new System.Drawing.Point(578, 153);
            this.lblCharCount160.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCharCount160.Name = "lblCharCount160";
            this.lblCharCount160.Size = new System.Drawing.Size(25, 13);
            this.lblCharCount160.TabIndex = 26;
            this.lblCharCount160.Text = "160";
            // 
            // lblCharCount210
            // 
            this.lblCharCount210.AutoSize = true;
            this.lblCharCount210.Location = new System.Drawing.Point(578, 126);
            this.lblCharCount210.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCharCount210.Name = "lblCharCount210";
            this.lblCharCount210.Size = new System.Drawing.Size(25, 13);
            this.lblCharCount210.TabIndex = 25;
            this.lblCharCount210.Text = "210";
            // 
            // txtContractCode
            // 
            this.txtContractCode.Location = new System.Drawing.Point(97, 19);
            this.txtContractCode.Name = "txtContractCode";
            this.txtContractCode.Size = new System.Drawing.Size(140, 20);
            this.txtContractCode.TabIndex = 1;
            // 
            // txtRecipientComment
            // 
            this.txtRecipientComment.Location = new System.Drawing.Point(243, 19);
            this.txtRecipientComment.Name = "txtRecipientComment";
            this.txtRecipientComment.Size = new System.Drawing.Size(360, 20);
            this.txtRecipientComment.TabIndex = 4;
            // 
            // lblRecipientBik
            // 
            this.lblRecipientBik.AutoSize = true;
            this.lblRecipientBik.Location = new System.Drawing.Point(8, 48);
            this.lblRecipientBik.Name = "lblRecipientBik";
            this.lblRecipientBik.Size = new System.Drawing.Size(29, 13);
            this.lblRecipientBik.TabIndex = 5;
            this.lblRecipientBik.Text = "БИК";
            // 
            // txtRecipientBik
            // 
            this.txtRecipientBik.Location = new System.Drawing.Point(97, 45);
            this.txtRecipientBik.Name = "txtRecipientBik";
            this.txtRecipientBik.Size = new System.Drawing.Size(140, 20);
            this.txtRecipientBik.TabIndex = 6;
            // 
            // lblRecipientCorrAccount
            // 
            this.lblRecipientCorrAccount.AutoSize = true;
            this.lblRecipientCorrAccount.Location = new System.Drawing.Point(298, 48);
            this.lblRecipientCorrAccount.Name = "lblRecipientCorrAccount";
            this.lblRecipientCorrAccount.Size = new System.Drawing.Size(60, 13);
            this.lblRecipientCorrAccount.TabIndex = 7;
            this.lblRecipientCorrAccount.Text = "Корр. счет";
            // 
            // ucaRecipientCorrAccount
            // 
            this.ucaRecipientCorrAccount.Location = new System.Drawing.Point(365, 45);
            this.ucaRecipientCorrAccount.MaxLength = 24;
            this.ucaRecipientCorrAccount.Name = "ucaRecipientCorrAccount";
            this.ucaRecipientCorrAccount.Size = new System.Drawing.Size(238, 20);
            this.ucaRecipientCorrAccount.TabIndex = 8;
            // 
            // txtRecipientBankName
            // 
            this.txtRecipientBankName.Location = new System.Drawing.Point(97, 71);
            this.txtRecipientBankName.Name = "txtRecipientBankName";
            this.txtRecipientBankName.Size = new System.Drawing.Size(474, 20);
            this.txtRecipientBankName.TabIndex = 10;
            // 
            // lblRecipientAccount
            // 
            this.lblRecipientAccount.AutoSize = true;
            this.lblRecipientAccount.Location = new System.Drawing.Point(8, 100);
            this.lblRecipientAccount.Name = "lblRecipientAccount";
            this.lblRecipientAccount.Size = new System.Drawing.Size(25, 13);
            this.lblRecipientAccount.TabIndex = 11;
            this.lblRecipientAccount.Text = "Р/с";
            // 
            // ucaRecipientAccount
            // 
            this.ucaRecipientAccount.Location = new System.Drawing.Point(97, 97);
            this.ucaRecipientAccount.MaxLength = 24;
            this.ucaRecipientAccount.Name = "ucaRecipientAccount";
            this.ucaRecipientAccount.Size = new System.Drawing.Size(170, 20);
            this.ucaRecipientAccount.TabIndex = 12;
            // 
            // lblRecipientInn
            // 
            this.lblRecipientInn.AutoSize = true;
            this.lblRecipientInn.Location = new System.Drawing.Point(298, 101);
            this.lblRecipientInn.Name = "lblRecipientInn";
            this.lblRecipientInn.Size = new System.Drawing.Size(31, 13);
            this.lblRecipientInn.TabIndex = 13;
            this.lblRecipientInn.Text = "ИНН";
            // 
            // txtRecipientInn
            // 
            this.txtRecipientInn.Location = new System.Drawing.Point(335, 98);
            this.txtRecipientInn.Name = "txtRecipientInn";
            this.txtRecipientInn.Size = new System.Drawing.Size(90, 20);
            this.txtRecipientInn.TabIndex = 14;
            // 
            // lblRecipientKpp
            // 
            this.lblRecipientKpp.AutoSize = true;
            this.lblRecipientKpp.Location = new System.Drawing.Point(433, 101);
            this.lblRecipientKpp.Name = "lblRecipientKpp";
            this.lblRecipientKpp.Size = new System.Drawing.Size(38, 13);
            this.lblRecipientKpp.TabIndex = 15;
            this.lblRecipientKpp.Text = "КППУ";
            // 
            // txtRecipientKpp
            // 
            this.txtRecipientKpp.Location = new System.Drawing.Point(477, 97);
            this.txtRecipientKpp.Name = "txtRecipientKpp";
            this.txtRecipientKpp.Size = new System.Drawing.Size(94, 20);
            this.txtRecipientKpp.TabIndex = 16;
            // 
            // lblRecipientPurpose
            // 
            this.lblRecipientPurpose.AutoSize = true;
            this.lblRecipientPurpose.Location = new System.Drawing.Point(8, 126);
            this.lblRecipientPurpose.Name = "lblRecipientPurpose";
            this.lblRecipientPurpose.Size = new System.Drawing.Size(68, 13);
            this.lblRecipientPurpose.TabIndex = 17;
            this.lblRecipientPurpose.Text = "Назначение";
            // 
            // cmbPurpose
            // 
            this.cmbPurpose.FormattingEnabled = true;
            this.cmbPurpose.Location = new System.Drawing.Point(97, 123);
            this.cmbPurpose.Name = "cmbPurpose";
            this.cmbPurpose.Size = new System.Drawing.Size(474, 21);
            this.cmbPurpose.TabIndex = 18;
            // 
            // lblRecipientName
            // 
            this.lblRecipientName.Location = new System.Drawing.Point(8, 150);
            this.lblRecipientName.Name = "lblRecipientName";
            this.lblRecipientName.Size = new System.Drawing.Size(83, 28);
            this.lblRecipientName.TabIndex = 19;
            this.lblRecipientName.Text = "Наименование получателя";
            // 
            // txtRecipientName
            // 
            this.txtRecipientName.Location = new System.Drawing.Point(97, 150);
            this.txtRecipientName.Name = "txtRecipientName";
            this.txtRecipientName.Size = new System.Drawing.Size(474, 20);
            this.txtRecipientName.TabIndex = 20;
            // 
            // txtRecipientNote
            // 
            this.txtRecipientNote.Location = new System.Drawing.Point(577, 97);
            this.txtRecipientNote.Name = "txtRecipientNote";
            this.txtRecipientNote.Size = new System.Drawing.Size(27, 20);
            this.txtRecipientNote.TabIndex = 22;
            // 
            // btnSaveRecipientAttribute
            // 
            this.btnSaveRecipientAttribute.Location = new System.Drawing.Point(577, 69);
            this.btnSaveRecipientAttribute.Name = "btnSaveRecipientAttribute";
            this.btnSaveRecipientAttribute.Size = new System.Drawing.Size(26, 22);
            this.btnSaveRecipientAttribute.TabIndex = 24;
            this.btnSaveRecipientAttribute.Text = "C";
            this.btnSaveRecipientAttribute.UseVisualStyleBackColor = true;
            // 
            // grpSender
            // 
            this.grpSender.Controls.Add(this.ucaPayerAccount);
            this.grpSender.Controls.Add(this.linkPayerFullName);
            this.grpSender.Controls.Add(this.linkFindFilter);
            this.grpSender.Controls.Add(this.txtPayerFullName);
            this.grpSender.Controls.Add(this.lblPayerInn);
            this.grpSender.Controls.Add(this.txtPayerInn);
            this.grpSender.Controls.Add(this.lblPayerAddress);
            this.grpSender.Controls.Add(this.txtPayerAddress);
            this.grpSender.Controls.Add(this.lblPayerClientInfo);
            this.grpSender.Controls.Add(this.txtPayerClientInfo);
            this.grpSender.Controls.Add(this.lblSourceMeans);
            this.grpSender.Controls.Add(this.lblPayerCardNumber);
            this.grpSender.Controls.Add(this.txtPayerCardNumber);
            this.grpSender.Controls.Add(this.chkBenefits);
            this.grpSender.Controls.Add(this.lblBenefitReason);
            this.grpSender.Controls.Add(this.txtBenefitReason);
            this.grpSender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSender.Location = new System.Drawing.Point(3, 3);
            this.grpSender.Name = "grpSender";
            this.grpSender.Size = new System.Drawing.Size(610, 150);
            this.grpSender.TabIndex = 1;
            this.grpSender.TabStop = false;
            this.grpSender.Text = "Плательщик";
            // 
            // ucaPayerAccount
            // 
            this.ucaPayerAccount.Location = new System.Drawing.Point(402, 70);
            this.ucaPayerAccount.MaxLength = 24;
            this.ucaPayerAccount.Name = "ucaPayerAccount";
            this.ucaPayerAccount.Size = new System.Drawing.Size(201, 20);
            this.ucaPayerAccount.TabIndex = 21;
            // 
            // linkPayerFullName
            // 
            this.linkPayerFullName.AutoSize = true;
            this.linkPayerFullName.Location = new System.Drawing.Point(8, 22);
            this.linkPayerFullName.Name = "linkPayerFullName";
            this.linkPayerFullName.Size = new System.Drawing.Size(34, 13);
            this.linkPayerFullName.TabIndex = 20;
            this.linkPayerFullName.TabStop = true;
            this.linkPayerFullName.Text = "ФИО";
            this.linkPayerFullName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkPayerFullName_LinkClicked);
            // 
            // linkFindFilter
            // 
            this.linkFindFilter.AutoSize = true;
            this.linkFindFilter.Location = new System.Drawing.Point(437, 101);
            this.linkFindFilter.Name = "linkFindFilter";
            this.linkFindFilter.Size = new System.Drawing.Size(167, 13);
            this.linkFindFilter.TabIndex = 19;
            this.linkFindFilter.TabStop = true;
            this.linkFindFilter.Text = "Список платежей плательщика";
            this.linkFindFilter.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkFindFilter_LinkClicked);
            // 
            // txtPayerFullName
            // 
            this.txtPayerFullName.Location = new System.Drawing.Point(90, 19);
            this.txtPayerFullName.Name = "txtPayerFullName";
            this.txtPayerFullName.Size = new System.Drawing.Size(242, 20);
            this.txtPayerFullName.TabIndex = 1;
            // 
            // lblPayerInn
            // 
            this.lblPayerInn.AutoSize = true;
            this.lblPayerInn.Location = new System.Drawing.Point(409, 22);
            this.lblPayerInn.Name = "lblPayerInn";
            this.lblPayerInn.Size = new System.Drawing.Size(31, 13);
            this.lblPayerInn.TabIndex = 3;
            this.lblPayerInn.Text = "ИНН";
            // 
            // txtPayerInn
            // 
            this.txtPayerInn.Location = new System.Drawing.Point(453, 19);
            this.txtPayerInn.Name = "txtPayerInn";
            this.txtPayerInn.Size = new System.Drawing.Size(150, 20);
            this.txtPayerInn.TabIndex = 4;
            // 
            // lblPayerAddress
            // 
            this.lblPayerAddress.AutoSize = true;
            this.lblPayerAddress.Location = new System.Drawing.Point(8, 48);
            this.lblPayerAddress.Name = "lblPayerAddress";
            this.lblPayerAddress.Size = new System.Drawing.Size(38, 13);
            this.lblPayerAddress.TabIndex = 5;
            this.lblPayerAddress.Text = "Адрес";
            // 
            // txtPayerAddress
            // 
            this.txtPayerAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPayerAddress.Location = new System.Drawing.Point(90, 45);
            this.txtPayerAddress.Name = "txtPayerAddress";
            this.txtPayerAddress.Size = new System.Drawing.Size(514, 20);
            this.txtPayerAddress.TabIndex = 6;
            // 
            // lblPayerClientInfo
            // 
            this.lblPayerClientInfo.AutoSize = true;
            this.lblPayerClientInfo.Location = new System.Drawing.Point(8, 74);
            this.lblPayerClientInfo.Name = "lblPayerClientInfo";
            this.lblPayerClientInfo.Size = new System.Drawing.Size(63, 13);
            this.lblPayerClientInfo.TabIndex = 7;
            this.lblPayerClientInfo.Text = "Реквизиты";
            // 
            // txtPayerClientInfo
            // 
            this.txtPayerClientInfo.Location = new System.Drawing.Point(90, 71);
            this.txtPayerClientInfo.Name = "txtPayerClientInfo";
            this.txtPayerClientInfo.Size = new System.Drawing.Size(242, 20);
            this.txtPayerClientInfo.TabIndex = 8;
            // 
            // lblSourceMeans
            // 
            this.lblSourceMeans.Location = new System.Drawing.Point(341, 73);
            this.lblSourceMeans.Name = "lblSourceMeans";
            this.lblSourceMeans.Size = new System.Drawing.Size(55, 27);
            this.lblSourceMeans.TabIndex = 9;
            this.lblSourceMeans.Text = "Источник средств";
            // 
            // lblPayerCardNumber
            // 
            this.lblPayerCardNumber.AutoSize = true;
            this.lblPayerCardNumber.Location = new System.Drawing.Point(8, 102);
            this.lblPayerCardNumber.Name = "lblPayerCardNumber";
            this.lblPayerCardNumber.Size = new System.Drawing.Size(75, 13);
            this.lblPayerCardNumber.TabIndex = 14;
            this.lblPayerCardNumber.Text = "Номер карты";
            // 
            // txtPayerCardNumber
            // 
            this.txtPayerCardNumber.Location = new System.Drawing.Point(90, 98);
            this.txtPayerCardNumber.Name = "txtPayerCardNumber";
            this.txtPayerCardNumber.Size = new System.Drawing.Size(242, 20);
            this.txtPayerCardNumber.TabIndex = 15;
            // 
            // chkBenefits
            // 
            this.chkBenefits.AutoSize = true;
            this.chkBenefits.Location = new System.Drawing.Point(11, 125);
            this.chkBenefits.Name = "chkBenefits";
            this.chkBenefits.Size = new System.Drawing.Size(114, 17);
            this.chkBenefits.TabIndex = 16;
            this.chkBenefits.Text = "Льготный клиент";
            this.chkBenefits.UseVisualStyleBackColor = true;
            // 
            // lblBenefitReason
            // 
            this.lblBenefitReason.AutoSize = true;
            this.lblBenefitReason.Location = new System.Drawing.Point(169, 126);
            this.lblBenefitReason.Name = "lblBenefitReason";
            this.lblBenefitReason.Size = new System.Drawing.Size(63, 13);
            this.lblBenefitReason.TabIndex = 17;
            this.lblBenefitReason.Text = "Основание";
            // 
            // txtBenefitReason
            // 
            this.txtBenefitReason.Location = new System.Drawing.Point(238, 123);
            this.txtBenefitReason.Name = "txtBenefitReason";
            this.txtBenefitReason.Size = new System.Drawing.Size(366, 20);
            this.txtBenefitReason.TabIndex = 18;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.ubsCtrlInfo1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.uciInfo, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 647);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(610, 22);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // ubsCtrlInfo1
            // 
            this.ubsCtrlInfo1.AutoSize = true;
            this.ubsCtrlInfo1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ubsCtrlInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.ubsCtrlInfo1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.ubsCtrlInfo1.Interval = 500;
            this.ubsCtrlInfo1.Location = new System.Drawing.Point(308, 9);
            this.ubsCtrlInfo1.Name = "ubsCtrlInfo1";
            this.ubsCtrlInfo1.Size = new System.Drawing.Size(299, 13);
            this.ubsCtrlInfo1.TabIndex = 2;
            this.ubsCtrlInfo1.Text = "uciInfo";
            this.ubsCtrlInfo1.Visible = false;
            // 
            // uciInfo
            // 
            this.uciInfo.AutoSize = true;
            this.uciInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uciInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.uciInfo.ForeColor = System.Drawing.SystemColors.Highlight;
            this.uciInfo.Interval = 500;
            this.uciInfo.Location = new System.Drawing.Point(3, 9);
            this.uciInfo.Name = "uciInfo";
            this.uciInfo.Size = new System.Drawing.Size(299, 13);
            this.uciInfo.TabIndex = 1;
            this.uciInfo.Text = "uciFR";
            this.uciInfo.Visible = false;
            // 
            // tabPageThirdPerson
            // 
            this.tabPageThirdPerson.Controls.Add(this.linkThirdPersonName);
            this.tabPageThirdPerson.Controls.Add(this.txtThirdPersonName);
            this.tabPageThirdPerson.Controls.Add(this.lblThirdPersonKind);
            this.tabPageThirdPerson.Controls.Add(this.cmbThirdPersonKind);
            this.tabPageThirdPerson.Controls.Add(this.lblThirdPersonInn);
            this.tabPageThirdPerson.Controls.Add(this.txtThirdPersonInn);
            this.tabPageThirdPerson.Controls.Add(this.lblThirdPersonKpp);
            this.tabPageThirdPerson.Controls.Add(this.txtThirdPersonKpp);
            this.tabPageThirdPerson.Location = new System.Drawing.Point(4, 22);
            this.tabPageThirdPerson.Name = "tabPageThirdPerson";
            this.tabPageThirdPerson.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageThirdPerson.Size = new System.Drawing.Size(378, 158);
            this.tabPageThirdPerson.TabIndex = 1;
            this.tabPageThirdPerson.Text = "Сведения о третьем лице";
            this.tabPageThirdPerson.UseVisualStyleBackColor = true;
            // 
            // linkThirdPersonName
            // 
            this.linkThirdPersonName.AutoSize = true;
            this.linkThirdPersonName.Location = new System.Drawing.Point(8, 23);
            this.linkThirdPersonName.Name = "linkThirdPersonName";
            this.linkThirdPersonName.Size = new System.Drawing.Size(158, 13);
            this.linkThirdPersonName.TabIndex = 9;
            this.linkThirdPersonName.TabStop = true;
            this.linkThirdPersonName.Text = "Наименование третьего лица";
            // 
            // txtThirdPersonName
            // 
            this.txtThirdPersonName.Location = new System.Drawing.Point(172, 20);
            this.txtThirdPersonName.Name = "txtThirdPersonName";
            this.txtThirdPersonName.Size = new System.Drawing.Size(255, 20);
            this.txtThirdPersonName.TabIndex = 1;
            // 
            // lblThirdPersonKind
            // 
            this.lblThirdPersonKind.AutoSize = true;
            this.lblThirdPersonKind.Location = new System.Drawing.Point(8, 54);
            this.lblThirdPersonKind.Name = "lblThirdPersonKind";
            this.lblThirdPersonKind.Size = new System.Drawing.Size(101, 13);
            this.lblThirdPersonKind.TabIndex = 3;
            this.lblThirdPersonKind.Text = "Вид третьего лица";
            // 
            // cmbThirdPersonKind
            // 
            this.cmbThirdPersonKind.FormattingEnabled = true;
            this.cmbThirdPersonKind.Location = new System.Drawing.Point(172, 51);
            this.cmbThirdPersonKind.Name = "cmbThirdPersonKind";
            this.cmbThirdPersonKind.Size = new System.Drawing.Size(255, 21);
            this.cmbThirdPersonKind.TabIndex = 4;
            // 
            // lblThirdPersonInn
            // 
            this.lblThirdPersonInn.AutoSize = true;
            this.lblThirdPersonInn.Location = new System.Drawing.Point(8, 87);
            this.lblThirdPersonInn.Name = "lblThirdPersonInn";
            this.lblThirdPersonInn.Size = new System.Drawing.Size(31, 13);
            this.lblThirdPersonInn.TabIndex = 5;
            this.lblThirdPersonInn.Text = "ИНН";
            // 
            // txtThirdPersonInn
            // 
            this.txtThirdPersonInn.Location = new System.Drawing.Point(172, 84);
            this.txtThirdPersonInn.Name = "txtThirdPersonInn";
            this.txtThirdPersonInn.Size = new System.Drawing.Size(255, 20);
            this.txtThirdPersonInn.TabIndex = 6;
            // 
            // lblThirdPersonKpp
            // 
            this.lblThirdPersonKpp.AutoSize = true;
            this.lblThirdPersonKpp.Location = new System.Drawing.Point(8, 120);
            this.lblThirdPersonKpp.Name = "lblThirdPersonKpp";
            this.lblThirdPersonKpp.Size = new System.Drawing.Size(30, 13);
            this.lblThirdPersonKpp.TabIndex = 7;
            this.lblThirdPersonKpp.Text = "КПП";
            // 
            // txtThirdPersonKpp
            // 
            this.txtThirdPersonKpp.Location = new System.Drawing.Point(172, 117);
            this.txtThirdPersonKpp.Name = "txtThirdPersonKpp";
            this.txtThirdPersonKpp.Size = new System.Drawing.Size(255, 20);
            this.txtThirdPersonKpp.TabIndex = 8;
            this.txtThirdPersonKpp.Text = "0";
            // 
            // tabPageTariff
            // 
            this.tabPageTariff.Controls.Add(this.lblTariff);
            this.tabPageTariff.Controls.Add(this.cmbTariff);
            this.tabPageTariff.Location = new System.Drawing.Point(4, 22);
            this.tabPageTariff.Name = "tabPageTariff";
            this.tabPageTariff.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTariff.Size = new System.Drawing.Size(378, 158);
            this.tabPageTariff.TabIndex = 2;
            this.tabPageTariff.Text = "Тариф";
            this.tabPageTariff.UseVisualStyleBackColor = true;
            // 
            // lblTariff
            // 
            this.lblTariff.AutoSize = true;
            this.lblTariff.Location = new System.Drawing.Point(8, 20);
            this.lblTariff.Name = "lblTariff";
            this.lblTariff.Size = new System.Drawing.Size(125, 13);
            this.lblTariff.TabIndex = 0;
            this.lblTariff.Text = "Тариф электроэнергии";
            // 
            // cmbTariff
            // 
            this.cmbTariff.FormattingEnabled = true;
            this.cmbTariff.Location = new System.Drawing.Point(139, 17);
            this.cmbTariff.Name = "cmbTariff";
            this.cmbTariff.Size = new System.Drawing.Size(228, 21);
            this.cmbTariff.TabIndex = 1;
            // 
            // tabPageTelephone
            // 
            this.tabPageTelephone.Controls.Add(this.lblPhone);
            this.tabPageTelephone.Controls.Add(this.cmbPhone);
            this.tabPageTelephone.Location = new System.Drawing.Point(4, 22);
            this.tabPageTelephone.Name = "tabPageTelephone";
            this.tabPageTelephone.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTelephone.Size = new System.Drawing.Size(378, 158);
            this.tabPageTelephone.TabIndex = 3;
            this.tabPageTelephone.Text = "Телефонный узел";
            this.tabPageTelephone.UseVisualStyleBackColor = true;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(8, 22);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(98, 13);
            this.lblPhone.TabIndex = 0;
            this.lblPhone.Text = "Телефонный узел";
            // 
            // cmbPhone
            // 
            this.cmbPhone.FormattingEnabled = true;
            this.cmbPhone.Location = new System.Drawing.Point(111, 19);
            this.cmbPhone.Name = "cmbPhone";
            this.cmbPhone.Size = new System.Drawing.Size(228, 21);
            this.cmbPhone.TabIndex = 1;
            // 
            // tabPageTax
            // 
            this.tabPageTax.Controls.Add(this.lblTaxStatus);
            this.tabPageTax.Controls.Add(this.txtTaxStatus);
            this.tabPageTax.Controls.Add(this.lblTaxKbk);
            this.tabPageTax.Controls.Add(this.txtTaxKbk);
            this.tabPageTax.Controls.Add(this.lblTaxOkato);
            this.tabPageTax.Controls.Add(this.txtTaxOkato);
            this.tabPageTax.Controls.Add(this.lblTaxReasonCode);
            this.tabPageTax.Controls.Add(this.txtTaxReasonCode);
            this.tabPageTax.Controls.Add(this.lblTaxPeriodCode);
            this.tabPageTax.Controls.Add(this.txtTaxPeriodCode);
            this.tabPageTax.Controls.Add(this.lblTaxDocumentNumber);
            this.tabPageTax.Controls.Add(this.txtTaxDocumentNumber);
            this.tabPageTax.Controls.Add(this.lblTaxDocumentDate);
            this.tabPageTax.Controls.Add(this.txtTaxDocumentDate);
            this.tabPageTax.Controls.Add(this.lblTaxType);
            this.tabPageTax.Controls.Add(this.txtTaxType);
            this.tabPageTax.Controls.Add(this.lblTaxImns);
            this.tabPageTax.Controls.Add(this.txtTaxImns);
            this.tabPageTax.Controls.Add(this.lblTaxKbkNote);
            this.tabPageTax.Controls.Add(this.txtTaxKbkNote);
            this.tabPageTax.Location = new System.Drawing.Point(4, 22);
            this.tabPageTax.Name = "tabPageTax";
            this.tabPageTax.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTax.Size = new System.Drawing.Size(378, 158);
            this.tabPageTax.TabIndex = 4;
            this.tabPageTax.Text = "Налог";
            this.tabPageTax.UseVisualStyleBackColor = true;
            // 
            // lblTaxStatus
            // 
            this.lblTaxStatus.AutoSize = true;
            this.lblTaxStatus.Location = new System.Drawing.Point(8, 22);
            this.lblTaxStatus.Name = "lblTaxStatus";
            this.lblTaxStatus.Size = new System.Drawing.Size(68, 13);
            this.lblTaxStatus.TabIndex = 0;
            this.lblTaxStatus.Text = "(101) Статус";
            // 
            // txtTaxStatus
            // 
            this.txtTaxStatus.Location = new System.Drawing.Point(192, 19);
            this.txtTaxStatus.Name = "txtTaxStatus";
            this.txtTaxStatus.Size = new System.Drawing.Size(252, 20);
            this.txtTaxStatus.TabIndex = 1;
            // 
            // lblTaxKbk
            // 
            this.lblTaxKbk.AutoSize = true;
            this.lblTaxKbk.Location = new System.Drawing.Point(8, 49);
            this.lblTaxKbk.Name = "lblTaxKbk";
            this.lblTaxKbk.Size = new System.Drawing.Size(113, 13);
            this.lblTaxKbk.TabIndex = 2;
            this.lblTaxKbk.Text = "(104) Код бюджетной";
            // 
            // txtTaxKbk
            // 
            this.txtTaxKbk.Location = new System.Drawing.Point(192, 46);
            this.txtTaxKbk.Name = "txtTaxKbk";
            this.txtTaxKbk.Size = new System.Drawing.Size(252, 20);
            this.txtTaxKbk.TabIndex = 3;
            // 
            // lblTaxOkato
            // 
            this.lblTaxOkato.AutoSize = true;
            this.lblTaxOkato.Location = new System.Drawing.Point(8, 76);
            this.lblTaxOkato.Name = "lblTaxOkato";
            this.lblTaxOkato.Size = new System.Drawing.Size(102, 13);
            this.lblTaxOkato.TabIndex = 4;
            this.lblTaxOkato.Text = "Расшифровка КБК";
            // 
            // txtTaxOkato
            // 
            this.txtTaxOkato.Location = new System.Drawing.Point(192, 73);
            this.txtTaxOkato.Name = "txtTaxOkato";
            this.txtTaxOkato.Size = new System.Drawing.Size(252, 20);
            this.txtTaxOkato.TabIndex = 5;
            // 
            // lblTaxReasonCode
            // 
            this.lblTaxReasonCode.AutoSize = true;
            this.lblTaxReasonCode.Location = new System.Drawing.Point(8, 103);
            this.lblTaxReasonCode.Name = "lblTaxReasonCode";
            this.lblTaxReasonCode.Size = new System.Drawing.Size(137, 13);
            this.lblTaxReasonCode.TabIndex = 6;
            this.lblTaxReasonCode.Text = "(105) Код ОКАТО/ОКТМО";
            // 
            // txtTaxReasonCode
            // 
            this.txtTaxReasonCode.Location = new System.Drawing.Point(192, 100);
            this.txtTaxReasonCode.Name = "txtTaxReasonCode";
            this.txtTaxReasonCode.Size = new System.Drawing.Size(252, 20);
            this.txtTaxReasonCode.TabIndex = 7;
            // 
            // lblTaxPeriodCode
            // 
            this.lblTaxPeriodCode.AutoSize = true;
            this.lblTaxPeriodCode.Location = new System.Drawing.Point(8, 130);
            this.lblTaxPeriodCode.Name = "lblTaxPeriodCode";
            this.lblTaxPeriodCode.Size = new System.Drawing.Size(151, 13);
            this.lblTaxPeriodCode.TabIndex = 8;
            this.lblTaxPeriodCode.Text = "(106) Основание налогового";
            // 
            // txtTaxPeriodCode
            // 
            this.txtTaxPeriodCode.Location = new System.Drawing.Point(192, 127);
            this.txtTaxPeriodCode.Name = "txtTaxPeriodCode";
            this.txtTaxPeriodCode.Size = new System.Drawing.Size(252, 20);
            this.txtTaxPeriodCode.TabIndex = 9;
            // 
            // lblTaxDocumentNumber
            // 
            this.lblTaxDocumentNumber.AutoSize = true;
            this.lblTaxDocumentNumber.Location = new System.Drawing.Point(8, 157);
            this.lblTaxDocumentNumber.Name = "lblTaxDocumentNumber";
            this.lblTaxDocumentNumber.Size = new System.Drawing.Size(91, 13);
            this.lblTaxDocumentNumber.TabIndex = 10;
            this.lblTaxDocumentNumber.Text = "(107) Налоговый";
            // 
            // txtTaxDocumentNumber
            // 
            this.txtTaxDocumentNumber.Location = new System.Drawing.Point(192, 154);
            this.txtTaxDocumentNumber.Name = "txtTaxDocumentNumber";
            this.txtTaxDocumentNumber.Size = new System.Drawing.Size(252, 20);
            this.txtTaxDocumentNumber.TabIndex = 11;
            // 
            // lblTaxDocumentDate
            // 
            this.lblTaxDocumentDate.AutoSize = true;
            this.lblTaxDocumentDate.Location = new System.Drawing.Point(8, 184);
            this.lblTaxDocumentDate.Name = "lblTaxDocumentDate";
            this.lblTaxDocumentDate.Size = new System.Drawing.Size(129, 13);
            this.lblTaxDocumentDate.TabIndex = 12;
            this.lblTaxDocumentDate.Text = "(108) Номер налогового";
            // 
            // txtTaxDocumentDate
            // 
            this.txtTaxDocumentDate.Location = new System.Drawing.Point(192, 181);
            this.txtTaxDocumentDate.Name = "txtTaxDocumentDate";
            this.txtTaxDocumentDate.Size = new System.Drawing.Size(252, 20);
            this.txtTaxDocumentDate.TabIndex = 13;
            // 
            // lblTaxType
            // 
            this.lblTaxType.AutoSize = true;
            this.lblTaxType.Location = new System.Drawing.Point(8, 211);
            this.lblTaxType.Name = "lblTaxType";
            this.lblTaxType.Size = new System.Drawing.Size(121, 13);
            this.lblTaxType.TabIndex = 14;
            this.lblTaxType.Text = "(109) Дата налогового";
            // 
            // txtTaxType
            // 
            this.txtTaxType.Location = new System.Drawing.Point(192, 208);
            this.txtTaxType.Name = "txtTaxType";
            this.txtTaxType.Size = new System.Drawing.Size(252, 20);
            this.txtTaxType.TabIndex = 15;
            // 
            // lblTaxImns
            // 
            this.lblTaxImns.AutoSize = true;
            this.lblTaxImns.Location = new System.Drawing.Point(8, 238);
            this.lblTaxImns.Name = "lblTaxImns";
            this.lblTaxImns.Size = new System.Drawing.Size(114, 13);
            this.lblTaxImns.TabIndex = 16;
            this.lblTaxImns.Text = "(110) Тип налогового";
            // 
            // txtTaxImns
            // 
            this.txtTaxImns.Location = new System.Drawing.Point(192, 235);
            this.txtTaxImns.Name = "txtTaxImns";
            this.txtTaxImns.Size = new System.Drawing.Size(252, 20);
            this.txtTaxImns.TabIndex = 17;
            // 
            // lblTaxKbkNote
            // 
            this.lblTaxKbkNote.AutoSize = true;
            this.lblTaxKbkNote.Location = new System.Drawing.Point(8, 265);
            this.lblTaxKbkNote.Name = "lblTaxKbkNote";
            this.lblTaxKbkNote.Size = new System.Drawing.Size(39, 13);
            this.lblTaxKbkNote.TabIndex = 18;
            this.lblTaxKbkNote.Text = "ИМНС";
            // 
            // txtTaxKbkNote
            // 
            this.txtTaxKbkNote.Location = new System.Drawing.Point(192, 262);
            this.txtTaxKbkNote.Name = "txtTaxKbkNote";
            this.txtTaxKbkNote.Size = new System.Drawing.Size(252, 20);
            this.txtTaxKbkNote.TabIndex = 19;
            // 
            // tabPageAddFields
            // 
            this.tabPageAddFields.Controls.Add(this.ucfAddProperties);
            this.tabPageAddFields.Location = new System.Drawing.Point(4, 22);
            this.tabPageAddFields.Name = "tabPageAddFields";
            this.tabPageAddFields.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAddFields.Size = new System.Drawing.Size(622, 678);
            this.tabPageAddFields.TabIndex = 5;
            this.tabPageAddFields.Text = "Дополнительные свойства";
            this.tabPageAddFields.UseVisualStyleBackColor = true;
            // 
            // ucfAddProperties
            // 
            this.ucfAddProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucfAddProperties.Location = new System.Drawing.Point(3, 3);
            this.ucfAddProperties.Margin = new System.Windows.Forms.Padding(4);
            this.ucfAddProperties.Name = "ucfAddProperties";
            this.ucfAddProperties.ReadOnly = false;
            this.ucfAddProperties.Size = new System.Drawing.Size(616, 672);
            this.ucfAddProperties.TabIndex = 0;
            // 
            // tblActions
            // 
            this.tblActions.CausesValidation = false;
            this.tblActions.ColumnCount = 6;
            this.tblActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.tblActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 164F));
            this.tblActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tblActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tblActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tblActions.Controls.Add(this.btnCashSymb, 1, 0);
            this.tblActions.Controls.Add(this.btnPattern, 2, 0);
            this.tblActions.Controls.Add(this.btnCalc, 4, 0);
            this.tblActions.Controls.Add(this.btnSave, 3, 0);
            this.tblActions.Controls.Add(this.btnExit, 5, 0);
            this.tblActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblActions.Location = new System.Drawing.Point(3, 758);
            this.tblActions.Name = "tblActions";
            this.tblActions.RowCount = 1;
            this.tblActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblActions.Size = new System.Drawing.Size(630, 32);
            this.tblActions.TabIndex = 2;
            // 
            // btnCashSymb
            // 
            this.btnCashSymb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCashSymb.Location = new System.Drawing.Point(126, 3);
            this.btnCashSymb.Name = "btnCashSymb";
            this.btnCashSymb.Size = new System.Drawing.Size(91, 26);
            this.btnCashSymb.TabIndex = 5;
            this.btnCashSymb.Text = "Касс символа";
            this.btnCashSymb.UseVisualStyleBackColor = true;
            // 
            // btnPattern
            // 
            this.btnPattern.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPattern.Location = new System.Drawing.Point(223, 3);
            this.btnPattern.Name = "btnPattern";
            this.btnPattern.Size = new System.Drawing.Size(158, 26);
            this.btnPattern.TabIndex = 4;
            this.btnPattern.Text = "Пользовательская форма";
            this.btnPattern.UseVisualStyleBackColor = true;
            // 
            // btnCalc
            // 
            this.btnCalc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCalc.Location = new System.Drawing.Point(469, 3);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(76, 26);
            this.btnCalc.TabIndex = 3;
            this.btnCalc.Text = "Расчет";
            this.btnCalc.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(387, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(76, 26);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.CausesValidation = false;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(551, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(76, 26);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSubPaymentCount);
            this.panel1.Controls.Add(this.txtGroupPaymentId);
            this.panel1.Controls.Add(this.chkPrintForms);
            this.panel1.Controls.Add(this.udcCommonAmount);
            this.panel1.Controls.Add(this.lblCommonAmount);
            this.panel1.Controls.Add(this.txtSubPaymentCount);
            this.panel1.Controls.Add(this.udcTotalAmount);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 713);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(630, 39);
            this.panel1.TabIndex = 4;
            // 
            // lblSubPaymentCount
            // 
            this.lblSubPaymentCount.AutoSize = true;
            this.lblSubPaymentCount.Location = new System.Drawing.Point(4, 14);
            this.lblSubPaymentCount.Name = "lblSubPaymentCount";
            this.lblSubPaymentCount.Size = new System.Drawing.Size(93, 13);
            this.lblSubPaymentCount.TabIndex = 42;
            this.lblSubPaymentCount.Text = "Кол-во платежей";
            // 
            // txtGroupPaymentId
            // 
            this.txtGroupPaymentId.Location = new System.Drawing.Point(475, 11);
            this.txtGroupPaymentId.Name = "txtGroupPaymentId";
            this.txtGroupPaymentId.Size = new System.Drawing.Size(24, 20);
            this.txtGroupPaymentId.TabIndex = 37;
            this.txtGroupPaymentId.Visible = false;
            // 
            // chkPrintForms
            // 
            this.chkPrintForms.AutoSize = true;
            this.chkPrintForms.Location = new System.Drawing.Point(505, 14);
            this.chkPrintForms.Name = "chkPrintForms";
            this.chkPrintForms.Size = new System.Drawing.Size(115, 17);
            this.chkPrintForms.TabIndex = 36;
            this.chkPrintForms.Text = "Печатные формы";
            this.chkPrintForms.UseVisualStyleBackColor = true;
            // 
            // udcCommonAmount
            // 
            this.udcCommonAmount.Location = new System.Drawing.Point(283, 11);
            this.udcCommonAmount.Name = "udcCommonAmount";
            this.udcCommonAmount.Size = new System.Drawing.Size(98, 20);
            this.udcCommonAmount.TabIndex = 33;
            this.udcCommonAmount.Text = "0";
            // 
            // lblCommonAmount
            // 
            this.lblCommonAmount.AutoSize = true;
            this.lblCommonAmount.Location = new System.Drawing.Point(152, 14);
            this.lblCommonAmount.Name = "lblCommonAmount";
            this.lblCommonAmount.Size = new System.Drawing.Size(129, 13);
            this.lblCommonAmount.TabIndex = 32;
            this.lblCommonAmount.Text = "Общая/итоговая сумма";
            // 
            // txtSubPaymentCount
            // 
            this.txtSubPaymentCount.Location = new System.Drawing.Point(100, 11);
            this.txtSubPaymentCount.Name = "txtSubPaymentCount";
            this.txtSubPaymentCount.ReadOnly = true;
            this.txtSubPaymentCount.Size = new System.Drawing.Size(46, 20);
            this.txtSubPaymentCount.TabIndex = 31;
            this.txtSubPaymentCount.Text = "0";
            // 
            // udcTotalAmount
            // 
            this.udcTotalAmount.Location = new System.Drawing.Point(387, 11);
            this.udcTotalAmount.Name = "udcTotalAmount";
            this.udcTotalAmount.Size = new System.Drawing.Size(76, 20);
            this.udcTotalAmount.TabIndex = 35;
            this.udcTotalAmount.Text = "0";
            // 
            // UbsPsUtPaymentFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 793);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UbsPsUtPaymentFrm";
            this.Text = "Платеж";
            this.panelMain.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tabPayment.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlGeneralFooterArea.ResumeLayout(false);
            this.pnlGeneralFooterArea.PerformLayout();
            this.grpRecipient.ResumeLayout(false);
            this.grpRecipient.PerformLayout();
            this.grpSender.ResumeLayout(false);
            this.grpSender.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabPageThirdPerson.ResumeLayout(false);
            this.tabPageThirdPerson.PerformLayout();
            this.tabPageTariff.ResumeLayout(false);
            this.tabPageTariff.PerformLayout();
            this.tabPageTelephone.ResumeLayout(false);
            this.tabPageTelephone.PerformLayout();
            this.tabPageTax.ResumeLayout(false);
            this.tabPageTax.PerformLayout();
            this.tabPageAddFields.ResumeLayout(false);
            this.tblActions.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel3;
        private TabControl tabPayment;
        private TabPage tabPageGeneral;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel pnlGeneralFooterArea;
        private Label lblBatchNumber;
        private TextBox txtBatchNumber;
        private CheckBox chkThirdPerson;
        private Label lblCashSymbolPayment;
        private TextBox txtCashSymbolPayment;
        private Label lblCashSymbolCommission;
        private TextBox txtCashSymbolCommission;
        private Label lblCashSymbolNds;
        private TextBox txtCashSymbolNds;
        private Label lblPaymentCode;
        private TextBox txtPaymentCode;
        private Label lblPaymentAmount;
        private UbsControl.UbsCtrlDecimal udcPaymentAmount;
        private Label lblPenaltyAmount;
        private UbsControl.UbsCtrlDecimal udcPenaltyAmount;
        private Label lblPayerRateAmount;
        private UbsControl.UbsCtrlDecimal udcPayerRateAmount;
        private Label lblAmountWithRate;
        private UbsControl.UbsCtrlDecimal udcAmountWithRate;
        private Label lblPeriodBeg;
        private ComboBox cmbCityCode;
        private TextBox txtPeriodDayBeg;
        private TextBox txtPeriodMonthBeg;
        private TextBox txtPeriodYearBeg;
        private Label lblPeriodEnd;
        private TextBox txtPeriodDayEnd;
        private TextBox txtPeriodMonthEnd;
        private TextBox txtPeriodYearEnd;
        private TextBox txtPayerAccount;
        private TextBox txtSubPaymentCount;
        private Label lblCommonAmount;
        private UbsControl.UbsCtrlDecimal udcCommonAmount;
        private UbsControl.UbsCtrlDecimal udcTotalAmount;
        private CheckBox chkPrintForms;
        private TextBox txtGroupPaymentId;
        private GroupBox grpRecipient;
        private Label lblCharCount160;
        private Label lblCharCount210;
        private TextBox txtContractCode;
        private TextBox txtRecipientComment;
        private Label lblRecipientBik;
        private TextBox txtRecipientBik;
        private Label lblRecipientCorrAccount;
        private UbsControl.UbsCtrlAccount ucaRecipientCorrAccount;
        private TextBox txtRecipientBankName;
        private Label lblRecipientAccount;
        private UbsControl.UbsCtrlAccount ucaRecipientAccount;
        private Label lblRecipientInn;
        private TextBox txtRecipientInn;
        private Label lblRecipientKpp;
        private TextBox txtRecipientKpp;
        private Label lblRecipientPurpose;
        private ComboBox cmbPurpose;
        private Label lblRecipientName;
        private TextBox txtRecipientName;
        private TextBox txtRecipientNote;
        private Button btnSaveRecipientAttribute;
        private GroupBox grpSender;
        private TextBox txtPayerFullName;
        private Label lblPayerInn;
        private TextBox txtPayerInn;
        private Label lblPayerAddress;
        private TextBox txtPayerAddress;
        private Label lblPayerClientInfo;
        private TextBox txtPayerClientInfo;
        private Label lblSourceMeans;
        private Label lblPayerCardNumber;
        private TextBox txtPayerCardNumber;
        private CheckBox chkBenefits;
        private Label lblBenefitReason;
        private TextBox txtBenefitReason;
        private TableLayoutPanel tableLayoutPanel2;
        private UbsControl.UbsCtrlInfo ubsCtrlInfo1;
        private UbsControl.UbsCtrlInfo uciInfo;
        private TabPage tabPageThirdPerson;
        private TextBox txtThirdPersonName;
        private Label lblThirdPersonKind;
        private ComboBox cmbThirdPersonKind;
        private Label lblThirdPersonInn;
        private TextBox txtThirdPersonInn;
        private Label lblThirdPersonKpp;
        private TextBox txtThirdPersonKpp;
        private TabPage tabPageTariff;
        private Label lblTariff;
        private ComboBox cmbTariff;
        private TabPage tabPageTelephone;
        private Label lblPhone;
        private ComboBox cmbPhone;
        private TabPage tabPageTax;
        private Label lblTaxStatus;
        private TextBox txtTaxStatus;
        private Label lblTaxKbk;
        private TextBox txtTaxKbk;
        private Label lblTaxOkato;
        private TextBox txtTaxOkato;
        private Label lblTaxReasonCode;
        private TextBox txtTaxReasonCode;
        private Label lblTaxPeriodCode;
        private TextBox txtTaxPeriodCode;
        private Label lblTaxDocumentNumber;
        private TextBox txtTaxDocumentNumber;
        private Label lblTaxDocumentDate;
        private TextBox txtTaxDocumentDate;
        private Label lblTaxType;
        private TextBox txtTaxType;
        private Label lblTaxImns;
        private TextBox txtTaxImns;
        private Label lblTaxKbkNote;
        private TextBox txtTaxKbkNote;
        private TabPage tabPageAddFields;
        private UbsControl.UbsCtrlFields ucfAddProperties;
        private TableLayoutPanel tblActions;
        private Button btnCashSymb;
        private Button btnPattern;
        private Button btnCalc;
        private Button btnSave;
        private Button btnExit;
        private Panel panel1;
        private Label lblSubPaymentCount;
        private TextBox txtCheckSum;
        private Label lblCityCode;
        private LinkLabel linkThirdPersonName;
        private LinkLabel linkFindFilter;
        private LinkLabel linkPayerFullName;
        private Timer timer1;
        private LinkLabel linkPaymentAccount;
        private LinkLabel linkContractCode;
        private LinkLabel linkRecipientBankName;
        private UbsControl.UbsCtrlAccount ucaPayerAccount;
    }
}
