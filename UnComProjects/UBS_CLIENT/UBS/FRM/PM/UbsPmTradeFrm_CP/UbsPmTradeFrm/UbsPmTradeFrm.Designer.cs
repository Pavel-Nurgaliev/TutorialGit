using System;
using System.Windows.Forms;

namespace UbsPmTradeFrm
{
    partial class UbsPmTradeFrm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ubsCtrlInfo = new UbsControl.UbsCtrlInfo();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grpTrade = new System.Windows.Forms.GroupBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.dateTrade = new UbsControl.UbsCtrlDate();
            this.lblNum = new System.Windows.Forms.Label();
            this.txtTradeNum = new System.Windows.Forms.TextBox();
            this.chkIsComposit = new System.Windows.Forms.CheckBox();
            this.lblDelivery = new System.Windows.Forms.Label();
            this.cmbKindSupplyTrade = new System.Windows.Forms.ComboBox();
            this.lblTradeType = new System.Windows.Forms.Label();
            this.cmbTradeType = new System.Windows.Forms.ComboBox();
            this.lblCurrencyPost = new System.Windows.Forms.Label();
            this.cmbCurrencyPost = new System.Windows.Forms.ComboBox();
            this.lblCurrencyOpl = new System.Windows.Forms.Label();
            this.cmbCurrencyOpl = new System.Windows.Forms.ComboBox();
            this.grpContracts = new System.Windows.Forms.GroupBox();
            this.btnContract2 = new System.Windows.Forms.Button();
            this.btnContract1 = new System.Windows.Forms.Button();
            this.cmbContractType1 = new System.Windows.Forms.ComboBox();
            this.txtContractCode1 = new System.Windows.Forms.TextBox();
            this.txtClientName1 = new System.Windows.Forms.TextBox();
            this.cmbContractType2 = new System.Windows.Forms.ComboBox();
            this.txtContractCode2 = new System.Windows.Forms.TextBox();
            this.txtClientName2 = new System.Windows.Forms.TextBox();
            this.chkNDS = new System.Windows.Forms.CheckBox();
            this.chkExport = new System.Windows.Forms.CheckBox();
            this.lblCommission = new System.Windows.Forms.Label();
            this.cmbComission = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lstViewOblig = new System.Windows.Forms.ListView();
            this.colObligDir = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObligNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObligDateOpl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObligDatePost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObligCostUnit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObligMassa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObligSumOblig = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObligCurId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObligRate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObligUnit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObligFixRate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cmdAddOblig = new System.Windows.Forms.Button();
            this.cmdDelOblig = new System.Windows.Forms.Button();
            this.cmdEditOblig = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.linkAccountsOblig = new System.Windows.Forms.LinkLabel();
            this.cmdExitOblig = new System.Windows.Forms.Button();
            this.cmdApplayOblig = new System.Windows.Forms.Button();
            this.tabControlOblig = new System.Windows.Forms.TabControl();
            this.tabPageOblig2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lblObligInfo1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lstViewObject = new System.Windows.Forms.ListView();
            this.colObjInstr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObjCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObjLigMassa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObjProba = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObjMassa1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObjMassa2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObjId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.cmdAddObject = new System.Windows.Forms.Button();
            this.cmdDelObject = new System.Windows.Forms.Button();
            this.lblObligInfo2 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.linkStorage = new System.Windows.Forms.LinkLabel();
            this.lblDeliveryInstrTitle = new System.Windows.Forms.Label();
            this.chkExternalStorage = new System.Windows.Forms.CheckBox();
            this.lblStorageNum = new System.Windows.Forms.Label();
            this.txtStorageCode = new System.Windows.Forms.TextBox();
            this.lblStorageName = new System.Windows.Forms.Label();
            this.txtStorageName = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabControlInstr = new System.Windows.Forms.TabControl();
            this.tabPageInstr1 = new System.Windows.Forms.TabPage();
            this.linkAccountPay = new System.Windows.Forms.LinkLabel();
            this.linkListInstr = new System.Windows.Forms.LinkLabel();
            this.lblInstrTitle_0 = new System.Windows.Forms.Label();
            this.chkCash_0 = new System.Windows.Forms.CheckBox();
            this.lblBIK_0 = new System.Windows.Forms.Label();
            this.txtBIK_0 = new System.Windows.Forms.TextBox();
            this.lblKS_0 = new System.Windows.Forms.Label();
            this.txtKS_0 = new System.Windows.Forms.TextBox();
            this.lblNameBank_0 = new System.Windows.Forms.Label();
            this.txtName_0 = new System.Windows.Forms.TextBox();
            this.txtRS_0 = new System.Windows.Forms.TextBox();
            this.lblClient_0 = new System.Windows.Forms.Label();
            this.txtClient_0 = new System.Windows.Forms.TextBox();
            this.lblNote_0 = new System.Windows.Forms.Label();
            this.txtNote_0 = new System.Windows.Forms.TextBox();
            this.lblINN_0 = new System.Windows.Forms.Label();
            this.txtINN_0 = new System.Windows.Forms.TextBox();
            this.chkNotAkcept_0 = new System.Windows.Forms.CheckBox();
            this.tabPageInstr2 = new System.Windows.Forms.TabPage();
            this.linkAccountSeller = new System.Windows.Forms.LinkLabel();
            this.linkListInstrSeller = new System.Windows.Forms.LinkLabel();
            this.lblInstrTitle_1 = new System.Windows.Forms.Label();
            this.chkCash_1 = new System.Windows.Forms.CheckBox();
            this.lblBIK_1 = new System.Windows.Forms.Label();
            this.txtBIK_1 = new System.Windows.Forms.TextBox();
            this.lblKS_1 = new System.Windows.Forms.Label();
            this.txtKS_1 = new System.Windows.Forms.TextBox();
            this.lblNameBank_1 = new System.Windows.Forms.Label();
            this.txtName_1 = new System.Windows.Forms.TextBox();
            this.txtRS_1 = new System.Windows.Forms.TextBox();
            this.lblClient_1 = new System.Windows.Forms.Label();
            this.txtClient_1 = new System.Windows.Forms.TextBox();
            this.lblNote_1 = new System.Windows.Forms.Label();
            this.txtNote_1 = new System.Windows.Forms.TextBox();
            this.lblINN_1 = new System.Windows.Forms.Label();
            this.txtINN_1 = new System.Windows.Forms.TextBox();
            this.chkNotAkcept_1 = new System.Windows.Forms.CheckBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.ubsCtrlField = new UbsControl.UbsCtrlFields();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grpMetalCharPost = new System.Windows.Forms.GroupBox();
            this.ucdSumOpl = new UbsControl.UbsCtrlDecimal();
            this.lblSumOpl = new System.Windows.Forms.Label();
            this.ucdSumOblig = new UbsControl.UbsCtrlDecimal();
            this.lblSumOblig = new System.Windows.Forms.Label();
            this.lblDateOpl = new System.Windows.Forms.Label();
            this.grpMetalChar = new System.Windows.Forms.GroupBox();
            this.ucdMassaGramm = new UbsControl.UbsCtrlDecimal();
            this.lblMassaGramm = new System.Windows.Forms.Label();
            this.ucdMassa = new UbsControl.UbsCtrlDecimal();
            this.lblMassa = new System.Windows.Forms.Label();
            this.lblDatePost = new System.Windows.Forms.Label();
            this.ucdCostCurOpl = new UbsControl.UbsCtrlDecimal();
            this.chkSumInCurValue = new System.Windows.Forms.CheckBox();
            this.ucdRateCurOblig = new UbsControl.UbsCtrlDecimal();
            this.chkRate = new System.Windows.Forms.CheckBox();
            this.ucdCostUnit = new UbsControl.UbsCtrlDecimal();
            this.lblCostUnit = new System.Windows.Forms.Label();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.cmbCurOblig = new System.Windows.Forms.ComboBox();
            this.lblCurOblig = new System.Windows.Forms.Label();
            this.cmbNaprTrade = new System.Windows.Forms.ComboBox();
            this.lblNaprTrade = new System.Windows.Forms.Label();
            this.tabPageOblig1 = new System.Windows.Forms.TabPage();
            this.dateOpl = new UbsControl.UbsCtrlDate();
            this.datePost = new UbsControl.UbsCtrlDate();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.panelMain.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.grpTrade.SuspendLayout();
            this.grpContracts.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabControlOblig.SuspendLayout();
            this.tabPageOblig2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabControlInstr.SuspendLayout();
            this.tabPageInstr1.SuspendLayout();
            this.tabPageInstr2.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.grpMetalCharPost.SuspendLayout();
            this.grpMetalChar.SuspendLayout();
            this.tabPageOblig1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tabControl);
            this.panelMain.Controls.Add(this.tableLayoutPanel);
            this.panelMain.Size = new System.Drawing.Size(685, 623);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.CausesValidation = false;
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel.Controls.Add(this.ubsCtrlInfo, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.btnSave, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.btnExit, 2, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 591);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(685, 32);
            this.tableLayoutPanel.TabIndex = 100;
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
            this.ubsCtrlInfo.Size = new System.Drawing.Size(503, 13);
            this.ubsCtrlInfo.TabIndex = 1;
            this.ubsCtrlInfo.Text = "ubsCtrlInfo";
            this.ubsCtrlInfo.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(512, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.CausesValidation = false;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(600, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 26);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage5);
            this.tabControl.Controls.Add(this.tabPage6);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(685, 591);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grpTrade);
            this.tabPage1.Controls.Add(this.lblCurrencyPost);
            this.tabPage1.Controls.Add(this.cmbCurrencyPost);
            this.tabPage1.Controls.Add(this.lblCurrencyOpl);
            this.tabPage1.Controls.Add(this.cmbCurrencyOpl);
            this.tabPage1.Controls.Add(this.grpContracts);
            this.tabPage1.Controls.Add(this.lblCommission);
            this.tabPage1.Controls.Add(this.cmbComission);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(677, 565);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Основные";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grpTrade
            // 
            this.grpTrade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTrade.Controls.Add(this.lblDate);
            this.grpTrade.Controls.Add(this.dateTrade);
            this.grpTrade.Controls.Add(this.lblNum);
            this.grpTrade.Controls.Add(this.txtTradeNum);
            this.grpTrade.Controls.Add(this.chkIsComposit);
            this.grpTrade.Controls.Add(this.lblDelivery);
            this.grpTrade.Controls.Add(this.cmbKindSupplyTrade);
            this.grpTrade.Controls.Add(this.lblTradeType);
            this.grpTrade.Controls.Add(this.cmbTradeType);
            this.grpTrade.Location = new System.Drawing.Point(6, 6);
            this.grpTrade.Name = "grpTrade";
            this.grpTrade.Size = new System.Drawing.Size(665, 76);
            this.grpTrade.TabIndex = 0;
            this.grpTrade.TabStop = false;
            this.grpTrade.Text = "Сделка";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(8, 24);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(33, 13);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "Дата";
            // 
            // dateTrade
            // 
            this.dateTrade.Location = new System.Drawing.Point(70, 20);
            this.dateTrade.MaxLength = 10;
            this.dateTrade.Name = "dateTrade";
            this.dateTrade.Size = new System.Drawing.Size(120, 20);
            this.dateTrade.TabIndex = 1;
            this.dateTrade.Text = "  .  .    ";
            this.dateTrade.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(249, 24);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(41, 13);
            this.lblNum.TabIndex = 0;
            this.lblNum.Text = "Номер";
            // 
            // txtTradeNum
            // 
            this.txtTradeNum.Location = new System.Drawing.Point(299, 20);
            this.txtTradeNum.MaxLength = 20;
            this.txtTradeNum.Name = "txtTradeNum";
            this.txtTradeNum.Size = new System.Drawing.Size(80, 20);
            this.txtTradeNum.TabIndex = 2;
            // 
            // chkIsComposit
            // 
            this.chkIsComposit.AutoSize = true;
            this.chkIsComposit.Location = new System.Drawing.Point(389, 22);
            this.chkIsComposit.Name = "chkIsComposit";
            this.chkIsComposit.Size = new System.Drawing.Size(86, 17);
            this.chkIsComposit.TabIndex = 3;
            this.chkIsComposit.Text = "Составная?";
            this.chkIsComposit.UseVisualStyleBackColor = true;
            // 
            // lblDelivery
            // 
            this.lblDelivery.AutoSize = true;
            this.lblDelivery.Location = new System.Drawing.Point(8, 50);
            this.lblDelivery.Name = "lblDelivery";
            this.lblDelivery.Size = new System.Drawing.Size(56, 13);
            this.lblDelivery.TabIndex = 0;
            this.lblDelivery.Text = "Поставка";
            // 
            // cmbKindSupplyTrade
            // 
            this.cmbKindSupplyTrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKindSupplyTrade.Location = new System.Drawing.Point(70, 46);
            this.cmbKindSupplyTrade.Name = "cmbKindSupplyTrade";
            this.cmbKindSupplyTrade.Size = new System.Drawing.Size(173, 21);
            this.cmbKindSupplyTrade.TabIndex = 4;
            // 
            // lblTradeType
            // 
            this.lblTradeType.AutoSize = true;
            this.lblTradeType.Location = new System.Drawing.Point(249, 50);
            this.lblTradeType.Name = "lblTradeType";
            this.lblTradeType.Size = new System.Drawing.Size(26, 13);
            this.lblTradeType.TabIndex = 0;
            this.lblTradeType.Text = "Тип";
            // 
            // cmbTradeType
            // 
            this.cmbTradeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTradeType.Location = new System.Drawing.Point(299, 46);
            this.cmbTradeType.Name = "cmbTradeType";
            this.cmbTradeType.Size = new System.Drawing.Size(176, 21);
            this.cmbTradeType.TabIndex = 5;
            // 
            // lblCurrencyPost
            // 
            this.lblCurrencyPost.AutoSize = true;
            this.lblCurrencyPost.Location = new System.Drawing.Point(6, 93);
            this.lblCurrencyPost.Name = "lblCurrencyPost";
            this.lblCurrencyPost.Size = new System.Drawing.Size(73, 13);
            this.lblCurrencyPost.TabIndex = 0;
            this.lblCurrencyPost.Text = "Драг.металл";
            // 
            // cmbCurrencyPost
            // 
            this.cmbCurrencyPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrencyPost.Location = new System.Drawing.Point(112, 89);
            this.cmbCurrencyPost.Name = "cmbCurrencyPost";
            this.cmbCurrencyPost.Size = new System.Drawing.Size(173, 21);
            this.cmbCurrencyPost.TabIndex = 1;
            // 
            // lblCurrencyOpl
            // 
            this.lblCurrencyOpl.AutoSize = true;
            this.lblCurrencyOpl.Location = new System.Drawing.Point(6, 119);
            this.lblCurrencyOpl.Name = "lblCurrencyOpl";
            this.lblCurrencyOpl.Size = new System.Drawing.Size(85, 13);
            this.lblCurrencyOpl.TabIndex = 0;
            this.lblCurrencyOpl.Text = "Валюта оплаты";
            // 
            // cmbCurrencyOpl
            // 
            this.cmbCurrencyOpl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrencyOpl.Location = new System.Drawing.Point(112, 115);
            this.cmbCurrencyOpl.Name = "cmbCurrencyOpl";
            this.cmbCurrencyOpl.Size = new System.Drawing.Size(173, 21);
            this.cmbCurrencyOpl.TabIndex = 2;
            // 
            // grpContracts
            // 
            this.grpContracts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpContracts.Controls.Add(this.label2);
            this.grpContracts.Controls.Add(this.label1);
            this.grpContracts.Controls.Add(this.btnContract2);
            this.grpContracts.Controls.Add(this.btnContract1);
            this.grpContracts.Controls.Add(this.cmbContractType1);
            this.grpContracts.Controls.Add(this.txtContractCode1);
            this.grpContracts.Controls.Add(this.txtClientName1);
            this.grpContracts.Controls.Add(this.cmbContractType2);
            this.grpContracts.Controls.Add(this.txtContractCode2);
            this.grpContracts.Controls.Add(this.txtClientName2);
            this.grpContracts.Controls.Add(this.chkNDS);
            this.grpContracts.Controls.Add(this.chkExport);
            this.grpContracts.Location = new System.Drawing.Point(6, 143);
            this.grpContracts.Name = "grpContracts";
            this.grpContracts.Size = new System.Drawing.Size(665, 148);
            this.grpContracts.TabIndex = 3;
            this.grpContracts.TabStop = false;
            this.grpContracts.Text = "Стороны сделки";
            // 
            // btnContract2
            // 
            this.btnContract2.Location = new System.Drawing.Point(311, 71);
            this.btnContract2.Name = "btnContract2";
            this.btnContract2.Size = new System.Drawing.Size(26, 21);
            this.btnContract2.TabIndex = 8;
            this.btnContract2.Text = "...";
            this.btnContract2.UseVisualStyleBackColor = true;
            // 
            // btnContract1
            // 
            this.btnContract1.Location = new System.Drawing.Point(311, 19);
            this.btnContract1.Name = "btnContract1";
            this.btnContract1.Size = new System.Drawing.Size(26, 21);
            this.btnContract1.TabIndex = 3;
            this.btnContract1.Text = "...";
            this.btnContract1.UseVisualStyleBackColor = true;
            // 
            // cmbContractType1
            // 
            this.cmbContractType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContractType1.Location = new System.Drawing.Point(88, 19);
            this.cmbContractType1.Name = "cmbContractType1";
            this.cmbContractType1.Size = new System.Drawing.Size(209, 21);
            this.cmbContractType1.TabIndex = 2;
            // 
            // txtContractCode1
            // 
            this.txtContractCode1.Location = new System.Drawing.Point(8, 45);
            this.txtContractCode1.MaxLength = 20;
            this.txtContractCode1.Name = "txtContractCode1";
            this.txtContractCode1.ReadOnly = true;
            this.txtContractCode1.Size = new System.Drawing.Size(75, 20);
            this.txtContractCode1.TabIndex = 4;
            this.txtContractCode1.TabStop = false;
            // 
            // txtClientName1
            // 
            this.txtClientName1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientName1.Location = new System.Drawing.Point(88, 45);
            this.txtClientName1.MaxLength = 50;
            this.txtClientName1.Name = "txtClientName1";
            this.txtClientName1.ReadOnly = true;
            this.txtClientName1.Size = new System.Drawing.Size(567, 20);
            this.txtClientName1.TabIndex = 5;
            this.txtClientName1.TabStop = false;
            // 
            // cmbContractType2
            // 
            this.cmbContractType2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContractType2.Location = new System.Drawing.Point(88, 70);
            this.cmbContractType2.Name = "cmbContractType2";
            this.cmbContractType2.Size = new System.Drawing.Size(209, 21);
            this.cmbContractType2.TabIndex = 7;
            // 
            // txtContractCode2
            // 
            this.txtContractCode2.Location = new System.Drawing.Point(8, 97);
            this.txtContractCode2.MaxLength = 20;
            this.txtContractCode2.Name = "txtContractCode2";
            this.txtContractCode2.ReadOnly = true;
            this.txtContractCode2.Size = new System.Drawing.Size(75, 20);
            this.txtContractCode2.TabIndex = 9;
            this.txtContractCode2.TabStop = false;
            // 
            // txtClientName2
            // 
            this.txtClientName2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClientName2.Location = new System.Drawing.Point(88, 97);
            this.txtClientName2.MaxLength = 50;
            this.txtClientName2.Name = "txtClientName2";
            this.txtClientName2.ReadOnly = true;
            this.txtClientName2.Size = new System.Drawing.Size(567, 20);
            this.txtClientName2.TabIndex = 10;
            this.txtClientName2.TabStop = false;
            // 
            // chkNDS
            // 
            this.chkNDS.AutoSize = true;
            this.chkNDS.Location = new System.Drawing.Point(8, 123);
            this.chkNDS.Name = "chkNDS";
            this.chkNDS.Size = new System.Drawing.Size(50, 17);
            this.chkNDS.TabIndex = 11;
            this.chkNDS.Text = "НДС";
            this.chkNDS.UseVisualStyleBackColor = true;
            this.chkNDS.Visible = false;
            // 
            // chkExport
            // 
            this.chkExport.AutoSize = true;
            this.chkExport.Location = new System.Drawing.Point(60, 123);
            this.chkExport.Name = "chkExport";
            this.chkExport.Size = new System.Drawing.Size(86, 17);
            this.chkExport.TabIndex = 12;
            this.chkExport.Text = "Экспортная";
            this.chkExport.UseVisualStyleBackColor = true;
            this.chkExport.Visible = false;
            // 
            // lblCommission
            // 
            this.lblCommission.AutoSize = true;
            this.lblCommission.Location = new System.Drawing.Point(11, 300);
            this.lblCommission.Name = "lblCommission";
            this.lblCommission.Size = new System.Drawing.Size(58, 13);
            this.lblCommission.TabIndex = 0;
            this.lblCommission.Text = "Комиссия";
            // 
            // cmbComission
            // 
            this.cmbComission.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComission.Location = new System.Drawing.Point(95, 297);
            this.cmbComission.Name = "cmbComission";
            this.cmbComission.Size = new System.Drawing.Size(130, 21);
            this.cmbComission.TabIndex = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(677, 565);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Обязательства";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.48137F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.51863F));
            this.tableLayoutPanel1.Controls.Add(this.lstViewOblig, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(671, 559);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // lstViewOblig
            // 
            this.lstViewOblig.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colObligDir,
            this.colObligNum,
            this.colObligDateOpl,
            this.colObligDatePost,
            this.colObligCostUnit,
            this.colObligMassa,
            this.colObligSumOblig,
            this.colObligCurId,
            this.colObligRate,
            this.colObligUnit,
            this.colObligFixRate});
            this.lstViewOblig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstViewOblig.FullRowSelect = true;
            this.lstViewOblig.GridLines = true;
            this.lstViewOblig.HideSelection = false;
            this.lstViewOblig.Location = new System.Drawing.Point(3, 3);
            this.lstViewOblig.MultiSelect = false;
            this.lstViewOblig.Name = "lstViewOblig";
            this.lstViewOblig.Size = new System.Drawing.Size(581, 553);
            this.lstViewOblig.TabIndex = 0;
            this.lstViewOblig.UseCompatibleStateImageBehavior = false;
            this.lstViewOblig.View = System.Windows.Forms.View.Details;
            // 
            // colObligDir
            // 
            this.colObligDir.Text = "Направление";
            this.colObligDir.Width = 90;
            // 
            // colObligNum
            // 
            this.colObligNum.Text = "Номер в части";
            this.colObligNum.Width = 85;
            // 
            // colObligDateOpl
            // 
            this.colObligDateOpl.Text = "Дата оплаты";
            this.colObligDateOpl.Width = 85;
            // 
            // colObligDatePost
            // 
            this.colObligDatePost.Text = "Дата поставки";
            this.colObligDatePost.Width = 85;
            // 
            // colObligCostUnit
            // 
            this.colObligCostUnit.Text = "Цена за ед.";
            this.colObligCostUnit.Width = 80;
            // 
            // colObligMassa
            // 
            this.colObligMassa.Text = "Масса";
            this.colObligMassa.Width = 65;
            // 
            // colObligSumOblig
            // 
            this.colObligSumOblig.Text = "Сумма";
            this.colObligSumOblig.Width = 80;
            // 
            // colObligCurId
            // 
            this.colObligCurId.Text = "Валюта";
            this.colObligCurId.Width = 0;
            // 
            // colObligRate
            // 
            this.colObligRate.Text = "Курс";
            this.colObligRate.Width = 0;
            // 
            // colObligUnit
            // 
            this.colObligUnit.Text = "Ед. изм.";
            this.colObligUnit.Width = 0;
            // 
            // colObligFixRate
            // 
            this.colObligFixRate.Text = "Фикс. курс";
            this.colObligFixRate.Width = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.cmdAddOblig, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmdDelOblig, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.cmdEditOblig, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(590, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(78, 553);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // cmdAddOblig
            // 
            this.cmdAddOblig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdAddOblig.Location = new System.Drawing.Point(3, 3);
            this.cmdAddOblig.Name = "cmdAddOblig";
            this.cmdAddOblig.Size = new System.Drawing.Size(72, 26);
            this.cmdAddOblig.TabIndex = 1;
            this.cmdAddOblig.Text = "Добавить";
            this.cmdAddOblig.UseVisualStyleBackColor = true;
            // 
            // cmdDelOblig
            // 
            this.cmdDelOblig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdDelOblig.Location = new System.Drawing.Point(3, 67);
            this.cmdDelOblig.Name = "cmdDelOblig";
            this.cmdDelOblig.Size = new System.Drawing.Size(72, 26);
            this.cmdDelOblig.TabIndex = 3;
            this.cmdDelOblig.Text = "Удалить";
            this.cmdDelOblig.UseVisualStyleBackColor = true;
            // 
            // cmdEditOblig
            // 
            this.cmdEditOblig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdEditOblig.Location = new System.Drawing.Point(3, 35);
            this.cmdEditOblig.Name = "cmdEditOblig";
            this.cmdEditOblig.Size = new System.Drawing.Size(72, 26);
            this.cmdEditOblig.TabIndex = 2;
            this.cmdEditOblig.Text = "Изменить";
            this.cmdEditOblig.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tableLayoutPanel6);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(677, 565);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Данные";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // linkAccountsOblig
            // 
            this.linkAccountsOblig.AutoSize = true;
            this.linkAccountsOblig.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.linkAccountsOblig.Location = new System.Drawing.Point(3, 13);
            this.linkAccountsOblig.Name = "linkAccountsOblig";
            this.linkAccountsOblig.Size = new System.Drawing.Size(483, 13);
            this.linkAccountsOblig.TabIndex = 1;
            this.linkAccountsOblig.TabStop = true;
            this.linkAccountsOblig.Text = "Счета по обязательству";
            // 
            // cmdExitOblig
            // 
            this.cmdExitOblig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExitOblig.Location = new System.Drawing.Point(580, 3);
            this.cmdExitOblig.Name = "cmdExitOblig";
            this.cmdExitOblig.Size = new System.Drawing.Size(82, 20);
            this.cmdExitOblig.TabIndex = 3;
            this.cmdExitOblig.Text = "Отмена";
            this.cmdExitOblig.UseVisualStyleBackColor = true;
            this.cmdExitOblig.Visible = false;
            // 
            // cmdApplayOblig
            // 
            this.cmdApplayOblig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdApplayOblig.Location = new System.Drawing.Point(492, 3);
            this.cmdApplayOblig.Name = "cmdApplayOblig";
            this.cmdApplayOblig.Size = new System.Drawing.Size(82, 20);
            this.cmdApplayOblig.TabIndex = 2;
            this.cmdApplayOblig.Text = "Применить";
            this.cmdApplayOblig.UseVisualStyleBackColor = true;
            this.cmdApplayOblig.Visible = false;
            // 
            // tabControlOblig
            // 
            this.tabControlOblig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlOblig.Controls.Add(this.tabPageOblig1);
            this.tabControlOblig.Controls.Add(this.tabPageOblig2);
            this.tabControlOblig.Location = new System.Drawing.Point(3, 3);
            this.tabControlOblig.Name = "tabControlOblig";
            this.tabControlOblig.SelectedIndex = 0;
            this.tabControlOblig.Size = new System.Drawing.Size(665, 521);
            this.tabControlOblig.TabIndex = 0;
            // 
            // tabPageOblig2
            // 
            this.tabPageOblig2.Controls.Add(this.tableLayoutPanel5);
            this.tabPageOblig2.Location = new System.Drawing.Point(4, 22);
            this.tabPageOblig2.Name = "tabPageOblig2";
            this.tabPageOblig2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOblig2.Size = new System.Drawing.Size(657, 495);
            this.tabPageOblig2.TabIndex = 1;
            this.tabPageOblig2.Text = "Объекты";
            this.tabPageOblig2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.lblObligInfo1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.lblObligInfo2, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(651, 489);
            this.tableLayoutPanel5.TabIndex = 5;
            // 
            // lblObligInfo1
            // 
            this.lblObligInfo1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblObligInfo1.Location = new System.Drawing.Point(3, 0);
            this.lblObligInfo1.Name = "lblObligInfo1";
            this.lblObligInfo1.Size = new System.Drawing.Size(645, 15);
            this.lblObligInfo1.TabIndex = 0;
            this.lblObligInfo1.Text = "Дата оплаты, Дата поставки";
            this.lblObligInfo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.5969F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.4031F));
            this.tableLayoutPanel3.Controls.Add(this.lstViewObject, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 43);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(645, 443);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // lstViewObject
            // 
            this.lstViewObject.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstViewObject.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colObjInstr,
            this.colObjCode,
            this.colObjLigMassa,
            this.colObjProba,
            this.colObjMassa1,
            this.colObjMassa2,
            this.colObjId});
            this.lstViewObject.FullRowSelect = true;
            this.lstViewObject.GridLines = true;
            this.lstViewObject.HideSelection = false;
            this.lstViewObject.Location = new System.Drawing.Point(3, 3);
            this.lstViewObject.MultiSelect = false;
            this.lstViewObject.Name = "lstViewObject";
            this.lstViewObject.Size = new System.Drawing.Size(559, 437);
            this.lstViewObject.TabIndex = 1;
            this.lstViewObject.UseCompatibleStateImageBehavior = false;
            this.lstViewObject.View = System.Windows.Forms.View.Details;
            // 
            // colObjInstr
            // 
            this.colObjInstr.Text = "Инстр.объект";
            this.colObjInstr.Width = 100;
            // 
            // colObjCode
            // 
            this.colObjCode.Text = "Код объекта";
            this.colObjCode.Width = 100;
            // 
            // colObjLigMassa
            // 
            this.colObjLigMassa.Text = "Лигат. масса";
            this.colObjLigMassa.Width = 80;
            // 
            // colObjProba
            // 
            this.colObjProba.Text = "Проба";
            // 
            // colObjMassa1
            // 
            this.colObjMassa1.Text = "Масса";
            this.colObjMassa1.Width = 70;
            // 
            // colObjMassa2
            // 
            this.colObjMassa2.Text = "Масса";
            this.colObjMassa2.Width = 70;
            // 
            // colObjId
            // 
            this.colObjId.Text = "Идентификатор";
            this.colObjId.Width = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.cmdAddObject, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.cmdDelObject, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(568, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(74, 437);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // cmdAddObject
            // 
            this.cmdAddObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdAddObject.Location = new System.Drawing.Point(3, 3);
            this.cmdAddObject.Name = "cmdAddObject";
            this.cmdAddObject.Size = new System.Drawing.Size(68, 26);
            this.cmdAddObject.TabIndex = 2;
            this.cmdAddObject.Text = "Добавить";
            this.cmdAddObject.UseVisualStyleBackColor = true;
            // 
            // cmdDelObject
            // 
            this.cmdDelObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdDelObject.Location = new System.Drawing.Point(3, 35);
            this.cmdDelObject.Name = "cmdDelObject";
            this.cmdDelObject.Size = new System.Drawing.Size(68, 26);
            this.cmdDelObject.TabIndex = 3;
            this.cmdDelObject.Text = "Удалить";
            this.cmdDelObject.UseVisualStyleBackColor = true;
            // 
            // lblObligInfo2
            // 
            this.lblObligInfo2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblObligInfo2.Location = new System.Drawing.Point(3, 20);
            this.lblObligInfo2.Name = "lblObligInfo2";
            this.lblObligInfo2.Size = new System.Drawing.Size(645, 15);
            this.lblObligInfo2.TabIndex = 0;
            this.lblObligInfo2.Text = "Цена, Масса, Сумма";
            this.lblObligInfo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.linkStorage);
            this.tabPage4.Controls.Add(this.lblDeliveryInstrTitle);
            this.tabPage4.Controls.Add(this.chkExternalStorage);
            this.tabPage4.Controls.Add(this.lblStorageNum);
            this.tabPage4.Controls.Add(this.txtStorageCode);
            this.tabPage4.Controls.Add(this.lblStorageName);
            this.tabPage4.Controls.Add(this.txtStorageName);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(677, 565);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Поставка";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // linkStorage
            // 
            this.linkStorage.AutoSize = true;
            this.linkStorage.Location = new System.Drawing.Point(273, 75);
            this.linkStorage.Name = "linkStorage";
            this.linkStorage.Size = new System.Drawing.Size(98, 13);
            this.linkStorage.TabIndex = 2;
            this.linkStorage.TabStop = true;
            this.linkStorage.Text = "выбор хранилища";
            // 
            // lblDeliveryInstrTitle
            // 
            this.lblDeliveryInstrTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDeliveryInstrTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDeliveryInstrTitle.Location = new System.Drawing.Point(6, 16);
            this.lblDeliveryInstrTitle.Name = "lblDeliveryInstrTitle";
            this.lblDeliveryInstrTitle.Size = new System.Drawing.Size(947, 20);
            this.lblDeliveryInstrTitle.TabIndex = 0;
            this.lblDeliveryInstrTitle.Text = "Инструкция по поставке";
            this.lblDeliveryInstrTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkExternalStorage
            // 
            this.chkExternalStorage.AutoSize = true;
            this.chkExternalStorage.Location = new System.Drawing.Point(96, 46);
            this.chkExternalStorage.Name = "chkExternalStorage";
            this.chkExternalStorage.Size = new System.Drawing.Size(130, 17);
            this.chkExternalStorage.TabIndex = 1;
            this.chkExternalStorage.Text = "Внешнее хранилище";
            this.chkExternalStorage.UseVisualStyleBackColor = true;
            // 
            // lblStorageNum
            // 
            this.lblStorageNum.AutoSize = true;
            this.lblStorageNum.Location = new System.Drawing.Point(6, 76);
            this.lblStorageNum.Name = "lblStorageNum";
            this.lblStorageNum.Size = new System.Drawing.Size(41, 13);
            this.lblStorageNum.TabIndex = 0;
            this.lblStorageNum.Text = "Номер";
            // 
            // txtStorageCode
            // 
            this.txtStorageCode.Enabled = false;
            this.txtStorageCode.Location = new System.Drawing.Point(96, 72);
            this.txtStorageCode.MaxLength = 20;
            this.txtStorageCode.Name = "txtStorageCode";
            this.txtStorageCode.ReadOnly = true;
            this.txtStorageCode.Size = new System.Drawing.Size(171, 20);
            this.txtStorageCode.TabIndex = 3;
            this.txtStorageCode.TabStop = false;
            // 
            // lblStorageName
            // 
            this.lblStorageName.AutoSize = true;
            this.lblStorageName.Location = new System.Drawing.Point(6, 104);
            this.lblStorageName.Name = "lblStorageName";
            this.lblStorageName.Size = new System.Drawing.Size(83, 13);
            this.lblStorageName.TabIndex = 0;
            this.lblStorageName.Text = "Наименование";
            // 
            // txtStorageName
            // 
            this.txtStorageName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStorageName.Enabled = false;
            this.txtStorageName.Location = new System.Drawing.Point(96, 100);
            this.txtStorageName.MaxLength = 50;
            this.txtStorageName.Name = "txtStorageName";
            this.txtStorageName.ReadOnly = true;
            this.txtStorageName.Size = new System.Drawing.Size(573, 20);
            this.txtStorageName.TabIndex = 4;
            this.txtStorageName.TabStop = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.tabControlInstr);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(677, 565);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Оплата";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabControlInstr
            // 
            this.tabControlInstr.Controls.Add(this.tabPageInstr1);
            this.tabControlInstr.Controls.Add(this.tabPageInstr2);
            this.tabControlInstr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlInstr.Location = new System.Drawing.Point(3, 3);
            this.tabControlInstr.Name = "tabControlInstr";
            this.tabControlInstr.SelectedIndex = 0;
            this.tabControlInstr.Size = new System.Drawing.Size(671, 559);
            this.tabControlInstr.TabIndex = 0;
            // 
            // tabPageInstr1
            // 
            this.tabPageInstr1.Controls.Add(this.linkAccountPay);
            this.tabPageInstr1.Controls.Add(this.linkListInstr);
            this.tabPageInstr1.Controls.Add(this.lblInstrTitle_0);
            this.tabPageInstr1.Controls.Add(this.chkCash_0);
            this.tabPageInstr1.Controls.Add(this.lblBIK_0);
            this.tabPageInstr1.Controls.Add(this.txtBIK_0);
            this.tabPageInstr1.Controls.Add(this.lblKS_0);
            this.tabPageInstr1.Controls.Add(this.txtKS_0);
            this.tabPageInstr1.Controls.Add(this.lblNameBank_0);
            this.tabPageInstr1.Controls.Add(this.txtName_0);
            this.tabPageInstr1.Controls.Add(this.txtRS_0);
            this.tabPageInstr1.Controls.Add(this.lblClient_0);
            this.tabPageInstr1.Controls.Add(this.txtClient_0);
            this.tabPageInstr1.Controls.Add(this.lblNote_0);
            this.tabPageInstr1.Controls.Add(this.txtNote_0);
            this.tabPageInstr1.Controls.Add(this.lblINN_0);
            this.tabPageInstr1.Controls.Add(this.txtINN_0);
            this.tabPageInstr1.Controls.Add(this.chkNotAkcept_0);
            this.tabPageInstr1.Location = new System.Drawing.Point(4, 22);
            this.tabPageInstr1.Name = "tabPageInstr1";
            this.tabPageInstr1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInstr1.Size = new System.Drawing.Size(663, 533);
            this.tabPageInstr1.TabIndex = 0;
            this.tabPageInstr1.Text = "Покупатель";
            this.tabPageInstr1.UseVisualStyleBackColor = true;
            // 
            // linkAccountPay
            // 
            this.linkAccountPay.AutoSize = true;
            this.linkAccountPay.Location = new System.Drawing.Point(6, 139);
            this.linkAccountPay.Name = "linkAccountPay";
            this.linkAccountPay.Size = new System.Drawing.Size(59, 13);
            this.linkAccountPay.TabIndex = 7;
            this.linkAccountPay.TabStop = true;
            this.linkAccountPay.Text = "Расч. счет";
            // 
            // linkListInstr
            // 
            this.linkListInstr.AutoSize = true;
            this.linkListInstr.Location = new System.Drawing.Point(6, 72);
            this.linkListInstr.Name = "linkListInstr";
            this.linkListInstr.Size = new System.Drawing.Size(237, 13);
            this.linkListInstr.TabIndex = 2;
            this.linkListInstr.TabStop = true;
            this.linkListInstr.Text = "Выбор платежной инструкции по покупателю";
            // 
            // lblInstrTitle_0
            // 
            this.lblInstrTitle_0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInstrTitle_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblInstrTitle_0.Location = new System.Drawing.Point(6, 12);
            this.lblInstrTitle_0.Name = "lblInstrTitle_0";
            this.lblInstrTitle_0.Size = new System.Drawing.Size(358, 20);
            this.lblInstrTitle_0.TabIndex = 0;
            this.lblInstrTitle_0.Text = "Инструкция по оплате";
            this.lblInstrTitle_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkCash_0
            // 
            this.chkCash_0.AutoSize = true;
            this.chkCash_0.Location = new System.Drawing.Point(9, 40);
            this.chkCash_0.Name = "chkCash_0";
            this.chkCash_0.Size = new System.Drawing.Size(125, 17);
            this.chkCash_0.TabIndex = 1;
            this.chkCash_0.Text = "Расчет через кассу";
            this.chkCash_0.UseVisualStyleBackColor = true;
            // 
            // lblBIK_0
            // 
            this.lblBIK_0.AutoSize = true;
            this.lblBIK_0.Location = new System.Drawing.Point(6, 92);
            this.lblBIK_0.Name = "lblBIK_0";
            this.lblBIK_0.Size = new System.Drawing.Size(29, 13);
            this.lblBIK_0.TabIndex = 0;
            this.lblBIK_0.Text = "БИК";
            // 
            // txtBIK_0
            // 
            this.txtBIK_0.Location = new System.Drawing.Point(90, 88);
            this.txtBIK_0.MaxLength = 9;
            this.txtBIK_0.Name = "txtBIK_0";
            this.txtBIK_0.Size = new System.Drawing.Size(80, 20);
            this.txtBIK_0.TabIndex = 3;
            // 
            // lblKS_0
            // 
            this.lblKS_0.AutoSize = true;
            this.lblKS_0.Location = new System.Drawing.Point(176, 92);
            this.lblKS_0.Name = "lblKS_0";
            this.lblKS_0.Size = new System.Drawing.Size(60, 13);
            this.lblKS_0.TabIndex = 0;
            this.lblKS_0.Text = "Корр. счет";
            // 
            // txtKS_0
            // 
            this.txtKS_0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKS_0.Enabled = false;
            this.txtKS_0.Location = new System.Drawing.Point(242, 88);
            this.txtKS_0.Name = "txtKS_0";
            this.txtKS_0.ReadOnly = true;
            this.txtKS_0.Size = new System.Drawing.Size(122, 20);
            this.txtKS_0.TabIndex = 4;
            this.txtKS_0.TabStop = false;
            // 
            // lblNameBank_0
            // 
            this.lblNameBank_0.AutoSize = true;
            this.lblNameBank_0.Location = new System.Drawing.Point(6, 116);
            this.lblNameBank_0.Name = "lblNameBank_0";
            this.lblNameBank_0.Size = new System.Drawing.Size(71, 13);
            this.lblNameBank_0.TabIndex = 0;
            this.lblNameBank_0.Text = "Наим. банка";
            // 
            // txtName_0
            // 
            this.txtName_0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName_0.Location = new System.Drawing.Point(90, 112);
            this.txtName_0.Name = "txtName_0";
            this.txtName_0.ReadOnly = true;
            this.txtName_0.Size = new System.Drawing.Size(274, 20);
            this.txtName_0.TabIndex = 5;
            this.txtName_0.TabStop = false;
            // 
            // txtRS_0
            // 
            this.txtRS_0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRS_0.Enabled = false;
            this.txtRS_0.Location = new System.Drawing.Point(90, 136);
            this.txtRS_0.Name = "txtRS_0";
            this.txtRS_0.Size = new System.Drawing.Size(274, 20);
            this.txtRS_0.TabIndex = 6;
            this.txtRS_0.TabStop = false;
            // 
            // lblClient_0
            // 
            this.lblClient_0.AutoSize = true;
            this.lblClient_0.Location = new System.Drawing.Point(6, 164);
            this.lblClient_0.Name = "lblClient_0";
            this.lblClient_0.Size = new System.Drawing.Size(43, 13);
            this.lblClient_0.TabIndex = 0;
            this.lblClient_0.Text = "Клиент";
            // 
            // txtClient_0
            // 
            this.txtClient_0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClient_0.Location = new System.Drawing.Point(90, 160);
            this.txtClient_0.Name = "txtClient_0";
            this.txtClient_0.Size = new System.Drawing.Size(274, 20);
            this.txtClient_0.TabIndex = 8;
            // 
            // lblNote_0
            // 
            this.lblNote_0.AutoSize = true;
            this.lblNote_0.Location = new System.Drawing.Point(6, 188);
            this.lblNote_0.Name = "lblNote_0";
            this.lblNote_0.Size = new System.Drawing.Size(70, 13);
            this.lblNote_0.TabIndex = 0;
            this.lblNote_0.Text = "Примечание";
            // 
            // txtNote_0
            // 
            this.txtNote_0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote_0.Location = new System.Drawing.Point(90, 184);
            this.txtNote_0.Multiline = true;
            this.txtNote_0.Name = "txtNote_0";
            this.txtNote_0.Size = new System.Drawing.Size(274, 65);
            this.txtNote_0.TabIndex = 9;
            // 
            // lblINN_0
            // 
            this.lblINN_0.AutoSize = true;
            this.lblINN_0.Location = new System.Drawing.Point(6, 257);
            this.lblINN_0.Name = "lblINN_0";
            this.lblINN_0.Size = new System.Drawing.Size(31, 13);
            this.lblINN_0.TabIndex = 0;
            this.lblINN_0.Text = "ИНН";
            // 
            // txtINN_0
            // 
            this.txtINN_0.Location = new System.Drawing.Point(90, 253);
            this.txtINN_0.Name = "txtINN_0";
            this.txtINN_0.Size = new System.Drawing.Size(150, 20);
            this.txtINN_0.TabIndex = 10;
            // 
            // chkNotAkcept_0
            // 
            this.chkNotAkcept_0.AutoSize = true;
            this.chkNotAkcept_0.Location = new System.Drawing.Point(90, 281);
            this.chkNotAkcept_0.Name = "chkNotAkcept_0";
            this.chkNotAkcept_0.Size = new System.Drawing.Size(149, 17);
            this.chkNotAkcept_0.TabIndex = 11;
            this.chkNotAkcept_0.Text = "Безакцептное списание";
            this.chkNotAkcept_0.UseVisualStyleBackColor = true;
            // 
            // tabPageInstr2
            // 
            this.tabPageInstr2.Controls.Add(this.linkAccountSeller);
            this.tabPageInstr2.Controls.Add(this.linkListInstrSeller);
            this.tabPageInstr2.Controls.Add(this.lblInstrTitle_1);
            this.tabPageInstr2.Controls.Add(this.chkCash_1);
            this.tabPageInstr2.Controls.Add(this.lblBIK_1);
            this.tabPageInstr2.Controls.Add(this.txtBIK_1);
            this.tabPageInstr2.Controls.Add(this.lblKS_1);
            this.tabPageInstr2.Controls.Add(this.txtKS_1);
            this.tabPageInstr2.Controls.Add(this.lblNameBank_1);
            this.tabPageInstr2.Controls.Add(this.txtName_1);
            this.tabPageInstr2.Controls.Add(this.txtRS_1);
            this.tabPageInstr2.Controls.Add(this.lblClient_1);
            this.tabPageInstr2.Controls.Add(this.txtClient_1);
            this.tabPageInstr2.Controls.Add(this.lblNote_1);
            this.tabPageInstr2.Controls.Add(this.txtNote_1);
            this.tabPageInstr2.Controls.Add(this.lblINN_1);
            this.tabPageInstr2.Controls.Add(this.txtINN_1);
            this.tabPageInstr2.Controls.Add(this.chkNotAkcept_1);
            this.tabPageInstr2.Location = new System.Drawing.Point(4, 22);
            this.tabPageInstr2.Name = "tabPageInstr2";
            this.tabPageInstr2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInstr2.Size = new System.Drawing.Size(663, 533);
            this.tabPageInstr2.TabIndex = 1;
            this.tabPageInstr2.Text = "Продавец";
            this.tabPageInstr2.UseVisualStyleBackColor = true;
            // 
            // linkAccountSeller
            // 
            this.linkAccountSeller.AutoSize = true;
            this.linkAccountSeller.Location = new System.Drawing.Point(6, 139);
            this.linkAccountSeller.Name = "linkAccountSeller";
            this.linkAccountSeller.Size = new System.Drawing.Size(59, 13);
            this.linkAccountSeller.TabIndex = 7;
            this.linkAccountSeller.TabStop = true;
            this.linkAccountSeller.Text = "Расч. счет";
            // 
            // linkListInstrSeller
            // 
            this.linkListInstrSeller.AutoSize = true;
            this.linkListInstrSeller.Location = new System.Drawing.Point(6, 72);
            this.linkListInstrSeller.Name = "linkListInstrSeller";
            this.linkListInstrSeller.Size = new System.Drawing.Size(224, 13);
            this.linkListInstrSeller.TabIndex = 2;
            this.linkListInstrSeller.TabStop = true;
            this.linkListInstrSeller.Text = "Выбор платежной инструкции по продавцу";
            // 
            // lblInstrTitle_1
            // 
            this.lblInstrTitle_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInstrTitle_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblInstrTitle_1.Location = new System.Drawing.Point(6, 12);
            this.lblInstrTitle_1.Name = "lblInstrTitle_1";
            this.lblInstrTitle_1.Size = new System.Drawing.Size(651, 20);
            this.lblInstrTitle_1.TabIndex = 0;
            this.lblInstrTitle_1.Text = "Инструкция по оплате";
            this.lblInstrTitle_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkCash_1
            // 
            this.chkCash_1.AutoSize = true;
            this.chkCash_1.Location = new System.Drawing.Point(9, 40);
            this.chkCash_1.Name = "chkCash_1";
            this.chkCash_1.Size = new System.Drawing.Size(125, 17);
            this.chkCash_1.TabIndex = 1;
            this.chkCash_1.Text = "Расчет через кассу";
            this.chkCash_1.UseVisualStyleBackColor = true;
            // 
            // lblBIK_1
            // 
            this.lblBIK_1.AutoSize = true;
            this.lblBIK_1.Location = new System.Drawing.Point(6, 92);
            this.lblBIK_1.Name = "lblBIK_1";
            this.lblBIK_1.Size = new System.Drawing.Size(29, 13);
            this.lblBIK_1.TabIndex = 0;
            this.lblBIK_1.Text = "БИК";
            // 
            // txtBIK_1
            // 
            this.txtBIK_1.Location = new System.Drawing.Point(90, 88);
            this.txtBIK_1.MaxLength = 9;
            this.txtBIK_1.Name = "txtBIK_1";
            this.txtBIK_1.Size = new System.Drawing.Size(80, 20);
            this.txtBIK_1.TabIndex = 3;
            // 
            // lblKS_1
            // 
            this.lblKS_1.AutoSize = true;
            this.lblKS_1.Location = new System.Drawing.Point(176, 92);
            this.lblKS_1.Name = "lblKS_1";
            this.lblKS_1.Size = new System.Drawing.Size(60, 13);
            this.lblKS_1.TabIndex = 0;
            this.lblKS_1.Text = "Корр. счет";
            // 
            // txtKS_1
            // 
            this.txtKS_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKS_1.Enabled = false;
            this.txtKS_1.Location = new System.Drawing.Point(242, 88);
            this.txtKS_1.Name = "txtKS_1";
            this.txtKS_1.ReadOnly = true;
            this.txtKS_1.Size = new System.Drawing.Size(415, 20);
            this.txtKS_1.TabIndex = 4;
            this.txtKS_1.TabStop = false;
            // 
            // lblNameBank_1
            // 
            this.lblNameBank_1.AutoSize = true;
            this.lblNameBank_1.Location = new System.Drawing.Point(6, 116);
            this.lblNameBank_1.Name = "lblNameBank_1";
            this.lblNameBank_1.Size = new System.Drawing.Size(71, 13);
            this.lblNameBank_1.TabIndex = 0;
            this.lblNameBank_1.Text = "Наим. банка";
            // 
            // txtName_1
            // 
            this.txtName_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName_1.Location = new System.Drawing.Point(90, 112);
            this.txtName_1.Name = "txtName_1";
            this.txtName_1.ReadOnly = true;
            this.txtName_1.Size = new System.Drawing.Size(567, 20);
            this.txtName_1.TabIndex = 5;
            this.txtName_1.TabStop = false;
            // 
            // txtRS_1
            // 
            this.txtRS_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRS_1.Enabled = false;
            this.txtRS_1.Location = new System.Drawing.Point(90, 136);
            this.txtRS_1.Name = "txtRS_1";
            this.txtRS_1.Size = new System.Drawing.Size(567, 20);
            this.txtRS_1.TabIndex = 6;
            this.txtRS_1.TabStop = false;
            // 
            // lblClient_1
            // 
            this.lblClient_1.AutoSize = true;
            this.lblClient_1.Location = new System.Drawing.Point(6, 164);
            this.lblClient_1.Name = "lblClient_1";
            this.lblClient_1.Size = new System.Drawing.Size(43, 13);
            this.lblClient_1.TabIndex = 0;
            this.lblClient_1.Text = "Клиент";
            // 
            // txtClient_1
            // 
            this.txtClient_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClient_1.Location = new System.Drawing.Point(90, 160);
            this.txtClient_1.Name = "txtClient_1";
            this.txtClient_1.Size = new System.Drawing.Size(567, 20);
            this.txtClient_1.TabIndex = 8;
            // 
            // lblNote_1
            // 
            this.lblNote_1.AutoSize = true;
            this.lblNote_1.Location = new System.Drawing.Point(6, 188);
            this.lblNote_1.Name = "lblNote_1";
            this.lblNote_1.Size = new System.Drawing.Size(70, 13);
            this.lblNote_1.TabIndex = 0;
            this.lblNote_1.Text = "Примечание";
            // 
            // txtNote_1
            // 
            this.txtNote_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote_1.Location = new System.Drawing.Point(90, 184);
            this.txtNote_1.Multiline = true;
            this.txtNote_1.Name = "txtNote_1";
            this.txtNote_1.Size = new System.Drawing.Size(567, 65);
            this.txtNote_1.TabIndex = 9;
            // 
            // lblINN_1
            // 
            this.lblINN_1.AutoSize = true;
            this.lblINN_1.Location = new System.Drawing.Point(6, 257);
            this.lblINN_1.Name = "lblINN_1";
            this.lblINN_1.Size = new System.Drawing.Size(31, 13);
            this.lblINN_1.TabIndex = 0;
            this.lblINN_1.Text = "ИНН";
            // 
            // txtINN_1
            // 
            this.txtINN_1.Location = new System.Drawing.Point(90, 253);
            this.txtINN_1.Name = "txtINN_1";
            this.txtINN_1.Size = new System.Drawing.Size(150, 20);
            this.txtINN_1.TabIndex = 10;
            // 
            // chkNotAkcept_1
            // 
            this.chkNotAkcept_1.AutoSize = true;
            this.chkNotAkcept_1.Location = new System.Drawing.Point(90, 281);
            this.chkNotAkcept_1.Name = "chkNotAkcept_1";
            this.chkNotAkcept_1.Size = new System.Drawing.Size(149, 17);
            this.chkNotAkcept_1.TabIndex = 11;
            this.chkNotAkcept_1.Text = "Безакцептное списание";
            this.chkNotAkcept_1.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.ubsCtrlField);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(677, 565);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Дополнительные";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // ubsCtrlField
            // 
            this.ubsCtrlField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ubsCtrlField.Location = new System.Drawing.Point(3, 3);
            this.ubsCtrlField.Name = "ubsCtrlField";
            this.ubsCtrlField.ReadOnly = false;
            this.ubsCtrlField.Size = new System.Drawing.Size(671, 559);
            this.ubsCtrlField.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Продавец";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Покупатель";
            // 
            // grpMetalCharPost
            // 
            this.grpMetalCharPost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMetalCharPost.Controls.Add(this.lblDateOpl);
            this.grpMetalCharPost.Controls.Add(this.dateOpl);
            this.grpMetalCharPost.Controls.Add(this.lblSumOblig);
            this.grpMetalCharPost.Controls.Add(this.ucdSumOblig);
            this.grpMetalCharPost.Controls.Add(this.lblSumOpl);
            this.grpMetalCharPost.Controls.Add(this.ucdSumOpl);
            this.grpMetalCharPost.Location = new System.Drawing.Point(6, 241);
            this.grpMetalCharPost.Name = "grpMetalCharPost";
            this.grpMetalCharPost.Size = new System.Drawing.Size(645, 100);
            this.grpMetalCharPost.TabIndex = 10;
            this.grpMetalCharPost.TabStop = false;
            this.grpMetalCharPost.Text = "Обязательство оплаты";
            // 
            // ucdSumOpl
            // 
            this.ucdSumOpl.Enabled = false;
            this.ucdSumOpl.Location = new System.Drawing.Point(175, 73);
            this.ucdSumOpl.Name = "ucdSumOpl";
            this.ucdSumOpl.Size = new System.Drawing.Size(120, 20);
            this.ucdSumOpl.TabIndex = 3;
            this.ucdSumOpl.Text = "0";
            // 
            // lblSumOpl
            // 
            this.lblSumOpl.AutoSize = true;
            this.lblSumOpl.Location = new System.Drawing.Point(8, 76);
            this.lblSumOpl.Name = "lblSumOpl";
            this.lblSumOpl.Size = new System.Drawing.Size(130, 13);
            this.lblSumOpl.TabIndex = 0;
            this.lblSumOpl.Text = "Сумма в валюте оплаты";
            // 
            // ucdSumOblig
            // 
            this.ucdSumOblig.Enabled = false;
            this.ucdSumOblig.Location = new System.Drawing.Point(175, 47);
            this.ucdSumOblig.Name = "ucdSumOblig";
            this.ucdSumOblig.Size = new System.Drawing.Size(120, 20);
            this.ucdSumOblig.TabIndex = 2;
            this.ucdSumOblig.Text = "0";
            // 
            // lblSumOblig
            // 
            this.lblSumOblig.AutoSize = true;
            this.lblSumOblig.Location = new System.Drawing.Point(8, 50);
            this.lblSumOblig.Name = "lblSumOblig";
            this.lblSumOblig.Size = new System.Drawing.Size(132, 13);
            this.lblSumOblig.TabIndex = 0;
            this.lblSumOblig.Text = "Сумма в валюте обяз-ва";
            // 
            // lblDateOpl
            // 
            this.lblDateOpl.AutoSize = true;
            this.lblDateOpl.Location = new System.Drawing.Point(8, 23);
            this.lblDateOpl.Name = "lblDateOpl";
            this.lblDateOpl.Size = new System.Drawing.Size(73, 13);
            this.lblDateOpl.TabIndex = 0;
            this.lblDateOpl.Text = "Дата оплаты";
            // 
            // grpMetalChar
            // 
            this.grpMetalChar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMetalChar.Controls.Add(this.lblDatePost);
            this.grpMetalChar.Controls.Add(this.datePost);
            this.grpMetalChar.Controls.Add(this.lblMassa);
            this.grpMetalChar.Controls.Add(this.ucdMassa);
            this.grpMetalChar.Controls.Add(this.lblMassaGramm);
            this.grpMetalChar.Controls.Add(this.ucdMassaGramm);
            this.grpMetalChar.Location = new System.Drawing.Point(6, 158);
            this.grpMetalChar.Name = "grpMetalChar";
            this.grpMetalChar.Size = new System.Drawing.Size(645, 77);
            this.grpMetalChar.TabIndex = 9;
            this.grpMetalChar.TabStop = false;
            this.grpMetalChar.Text = "Обязательство поставки";
            // 
            // ucdMassaGramm
            // 
            this.ucdMassaGramm.Enabled = false;
            this.ucdMassaGramm.Location = new System.Drawing.Point(429, 49);
            this.ucdMassaGramm.Name = "ucdMassaGramm";
            this.ucdMassaGramm.Size = new System.Drawing.Size(112, 20);
            this.ucdMassaGramm.TabIndex = 3;
            this.ucdMassaGramm.Text = "0";
            // 
            // lblMassaGramm
            // 
            this.lblMassaGramm.AutoSize = true;
            this.lblMassaGramm.Location = new System.Drawing.Point(281, 52);
            this.lblMassaGramm.Name = "lblMassaGramm";
            this.lblMassaGramm.Size = new System.Drawing.Size(142, 13);
            this.lblMassaGramm.TabIndex = 0;
            this.lblMassaGramm.Text = "Масса металла в граммах";
            // 
            // ucdMassa
            // 
            this.ucdMassa.Location = new System.Drawing.Point(110, 49);
            this.ucdMassa.Name = "ucdMassa";
            this.ucdMassa.Size = new System.Drawing.Size(120, 20);
            this.ucdMassa.TabIndex = 2;
            this.ucdMassa.Text = "0";
            // 
            // lblMassa
            // 
            this.lblMassa.AutoSize = true;
            this.lblMassa.Location = new System.Drawing.Point(8, 52);
            this.lblMassa.Name = "lblMassa";
            this.lblMassa.Size = new System.Drawing.Size(93, 13);
            this.lblMassa.TabIndex = 0;
            this.lblMassa.Text = "Масса в ед. изм.";
            // 
            // lblDatePost
            // 
            this.lblDatePost.AutoSize = true;
            this.lblDatePost.Location = new System.Drawing.Point(8, 23);
            this.lblDatePost.Name = "lblDatePost";
            this.lblDatePost.Size = new System.Drawing.Size(83, 13);
            this.lblDatePost.TabIndex = 0;
            this.lblDatePost.Text = "Дата поставки";
            // 
            // ucdCostCurOpl
            // 
            this.ucdCostCurOpl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ucdCostCurOpl.Enabled = false;
            this.ucdCostCurOpl.Location = new System.Drawing.Point(443, 131);
            this.ucdCostCurOpl.Name = "ucdCostCurOpl";
            this.ucdCostCurOpl.Size = new System.Drawing.Size(207, 20);
            this.ucdCostCurOpl.TabIndex = 8;
            this.ucdCostCurOpl.Text = "0";
            // 
            // chkSumInCurValue
            // 
            this.chkSumInCurValue.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSumInCurValue.Location = new System.Drawing.Point(6, 131);
            this.chkSumInCurValue.Name = "chkSumInCurValue";
            this.chkSumInCurValue.Size = new System.Drawing.Size(225, 17);
            this.chkSumInCurValue.TabIndex = 7;
            this.chkSumInCurValue.Text = "Цена за ед. в валюте оплаты";
            this.chkSumInCurValue.UseVisualStyleBackColor = true;
            // 
            // ucdRateCurOblig
            // 
            this.ucdRateCurOblig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ucdRateCurOblig.Enabled = false;
            this.ucdRateCurOblig.Location = new System.Drawing.Point(443, 107);
            this.ucdRateCurOblig.Name = "ucdRateCurOblig";
            this.ucdRateCurOblig.Size = new System.Drawing.Size(207, 20);
            this.ucdRateCurOblig.TabIndex = 6;
            this.ucdRateCurOblig.Text = "0";
            // 
            // chkRate
            // 
            this.chkRate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkRate.Location = new System.Drawing.Point(6, 107);
            this.chkRate.Name = "chkRate";
            this.chkRate.Size = new System.Drawing.Size(225, 17);
            this.chkRate.TabIndex = 5;
            this.chkRate.Text = "Коэф. пересчета валюты обяз-ва";
            this.chkRate.UseVisualStyleBackColor = true;
            // 
            // ucdCostUnit
            // 
            this.ucdCostUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ucdCostUnit.Location = new System.Drawing.Point(443, 82);
            this.ucdCostUnit.Name = "ucdCostUnit";
            this.ucdCostUnit.Size = new System.Drawing.Size(207, 20);
            this.ucdCostUnit.TabIndex = 4;
            this.ucdCostUnit.Text = "0";
            // 
            // lblCostUnit
            // 
            this.lblCostUnit.AutoSize = true;
            this.lblCostUnit.Location = new System.Drawing.Point(6, 85);
            this.lblCostUnit.Name = "lblCostUnit";
            this.lblCostUnit.Size = new System.Drawing.Size(157, 13);
            this.lblCostUnit.TabIndex = 0;
            this.lblCostUnit.Text = "Цена за ед. в валюте обяз-ва";
            // 
            // cmbUnit
            // 
            this.cmbUnit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.Location = new System.Drawing.Point(168, 56);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(482, 21);
            this.cmbUnit.TabIndex = 3;
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Location = new System.Drawing.Point(6, 60);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(136, 13);
            this.lblUnit.TabIndex = 0;
            this.lblUnit.Text = "Единица измерения веса";
            // 
            // cmbCurOblig
            // 
            this.cmbCurOblig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCurOblig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurOblig.Location = new System.Drawing.Point(168, 31);
            this.cmbCurOblig.Name = "cmbCurOblig";
            this.cmbCurOblig.Size = new System.Drawing.Size(482, 21);
            this.cmbCurOblig.TabIndex = 2;
            // 
            // lblCurOblig
            // 
            this.lblCurOblig.AutoSize = true;
            this.lblCurOblig.Location = new System.Drawing.Point(6, 35);
            this.lblCurOblig.Name = "lblCurOblig";
            this.lblCurOblig.Size = new System.Drawing.Size(124, 13);
            this.lblCurOblig.TabIndex = 0;
            this.lblCurOblig.Text = "Валюта обязательства";
            // 
            // cmbNaprTrade
            // 
            this.cmbNaprTrade.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbNaprTrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNaprTrade.Location = new System.Drawing.Point(168, 6);
            this.cmbNaprTrade.Name = "cmbNaprTrade";
            this.cmbNaprTrade.Size = new System.Drawing.Size(482, 21);
            this.cmbNaprTrade.TabIndex = 1;
            // 
            // lblNaprTrade
            // 
            this.lblNaprTrade.AutoSize = true;
            this.lblNaprTrade.Location = new System.Drawing.Point(6, 10);
            this.lblNaprTrade.Name = "lblNaprTrade";
            this.lblNaprTrade.Size = new System.Drawing.Size(114, 13);
            this.lblNaprTrade.TabIndex = 0;
            this.lblNaprTrade.Text = "Направление сделки";
            // 
            // tabPageOblig1
            // 
            this.tabPageOblig1.Controls.Add(this.lblNaprTrade);
            this.tabPageOblig1.Controls.Add(this.cmbNaprTrade);
            this.tabPageOblig1.Controls.Add(this.lblCurOblig);
            this.tabPageOblig1.Controls.Add(this.cmbCurOblig);
            this.tabPageOblig1.Controls.Add(this.lblUnit);
            this.tabPageOblig1.Controls.Add(this.cmbUnit);
            this.tabPageOblig1.Controls.Add(this.lblCostUnit);
            this.tabPageOblig1.Controls.Add(this.ucdCostUnit);
            this.tabPageOblig1.Controls.Add(this.chkRate);
            this.tabPageOblig1.Controls.Add(this.ucdRateCurOblig);
            this.tabPageOblig1.Controls.Add(this.chkSumInCurValue);
            this.tabPageOblig1.Controls.Add(this.ucdCostCurOpl);
            this.tabPageOblig1.Controls.Add(this.grpMetalChar);
            this.tabPageOblig1.Controls.Add(this.grpMetalCharPost);
            this.tabPageOblig1.Location = new System.Drawing.Point(4, 22);
            this.tabPageOblig1.Name = "tabPageOblig1";
            this.tabPageOblig1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOblig1.Size = new System.Drawing.Size(657, 495);
            this.tabPageOblig1.TabIndex = 0;
            this.tabPageOblig1.Text = "Обязательство";
            this.tabPageOblig1.UseVisualStyleBackColor = true;
            // 
            // dateOpl
            // 
            this.dateOpl.Location = new System.Drawing.Point(175, 19);
            this.dateOpl.MaxLength = 10;
            this.dateOpl.Name = "dateOpl";
            this.dateOpl.Size = new System.Drawing.Size(100, 20);
            this.dateOpl.TabIndex = 1;
            this.dateOpl.Text = "  .  .    ";
            this.dateOpl.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // datePost
            // 
            this.datePost.Location = new System.Drawing.Point(110, 19);
            this.datePost.MaxLength = 10;
            this.datePost.Name = "datePost";
            this.datePost.Size = new System.Drawing.Size(100, 20);
            this.datePost.TabIndex = 1;
            this.datePost.Text = "  .  .    ";
            this.datePost.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.tabControlOblig, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(671, 559);
            this.tableLayoutPanel6.TabIndex = 4;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel7.Controls.Add(this.linkAccountsOblig, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.cmdExitOblig, 2, 0);
            this.tableLayoutPanel7.Controls.Add(this.cmdApplayOblig, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 530);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(665, 26);
            this.tableLayoutPanel7.TabIndex = 1;
            // 
            // UbsPmTradeFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 623);
            this.Name = "UbsPmTradeFrm";
            this.Text = "Сделка";
            this.panelMain.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.grpTrade.ResumeLayout(false);
            this.grpTrade.PerformLayout();
            this.grpContracts.ResumeLayout(false);
            this.grpContracts.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabControlOblig.ResumeLayout(false);
            this.tabPageOblig2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabControlInstr.ResumeLayout(false);
            this.tabPageInstr1.ResumeLayout(false);
            this.tabPageInstr1.PerformLayout();
            this.tabPageInstr2.ResumeLayout(false);
            this.tabPageInstr2.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.grpMetalCharPost.ResumeLayout(false);
            this.grpMetalCharPost.PerformLayout();
            this.grpMetalChar.ResumeLayout(false);
            this.grpMetalChar.PerformLayout();
            this.tabPageOblig1.ResumeLayout(false);
            this.tabPageOblig1.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        // ── Bottom strip ──────────────────────────────────────────────────────────
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private UbsControl.UbsCtrlInfo ubsCtrlInfo;

        // ── Outer TabControl ──────────────────────────────────────────────────────
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;

        // ── Tab 1 ─────────────────────────────────────────────────────────────────
        private System.Windows.Forms.GroupBox grpTrade;
        private System.Windows.Forms.Label lblDate;
        private UbsControl.UbsCtrlDate dateTrade;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.TextBox txtTradeNum;
        private System.Windows.Forms.CheckBox chkIsComposit;
        private System.Windows.Forms.Label lblDelivery;
        private System.Windows.Forms.ComboBox cmbKindSupplyTrade;
        private System.Windows.Forms.Label lblTradeType;
        private System.Windows.Forms.ComboBox cmbTradeType;
        private System.Windows.Forms.Label lblCurrencyPost;
        private System.Windows.Forms.ComboBox cmbCurrencyPost;
        private System.Windows.Forms.Label lblCurrencyOpl;
        private System.Windows.Forms.ComboBox cmbCurrencyOpl;
        private System.Windows.Forms.GroupBox grpContracts;
        private System.Windows.Forms.ComboBox cmbContractType1;
        private System.Windows.Forms.TextBox txtContractCode1;
        private System.Windows.Forms.TextBox txtClientName1;
        private System.Windows.Forms.ComboBox cmbContractType2;
        private System.Windows.Forms.TextBox txtContractCode2;
        private System.Windows.Forms.TextBox txtClientName2;
        private System.Windows.Forms.CheckBox chkNDS;
        private System.Windows.Forms.CheckBox chkExport;
        private System.Windows.Forms.Label lblCommission;
        private System.Windows.Forms.ComboBox cmbComission;

        // ── Tab 2 ─────────────────────────────────────────────────────────────────
        private System.Windows.Forms.ListView lstViewOblig;
        private System.Windows.Forms.ColumnHeader colObligDir;
        private System.Windows.Forms.ColumnHeader colObligNum;
        private System.Windows.Forms.ColumnHeader colObligDateOpl;
        private System.Windows.Forms.ColumnHeader colObligDatePost;
        private System.Windows.Forms.ColumnHeader colObligCostUnit;
        private System.Windows.Forms.ColumnHeader colObligMassa;
        private System.Windows.Forms.ColumnHeader colObligSumOblig;
        private System.Windows.Forms.ColumnHeader colObligCurId;
        private System.Windows.Forms.ColumnHeader colObligRate;
        private System.Windows.Forms.ColumnHeader colObligUnit;
        private System.Windows.Forms.ColumnHeader colObligFixRate;
        private System.Windows.Forms.Button cmdAddOblig;
        private System.Windows.Forms.Button cmdEditOblig;
        private System.Windows.Forms.Button cmdDelOblig;

        // ── Tab 3 ─────────────────────────────────────────────────────────────────
        private System.Windows.Forms.TabControl tabControlOblig;
        private System.Windows.Forms.TabPage tabPageOblig2;
        private System.Windows.Forms.Label lblObligInfo1;
        private System.Windows.Forms.Label lblObligInfo2;
        private System.Windows.Forms.ListView lstViewObject;
        private System.Windows.Forms.ColumnHeader colObjInstr;
        private System.Windows.Forms.ColumnHeader colObjCode;
        private System.Windows.Forms.ColumnHeader colObjLigMassa;
        private System.Windows.Forms.ColumnHeader colObjProba;
        private System.Windows.Forms.ColumnHeader colObjMassa1;
        private System.Windows.Forms.ColumnHeader colObjMassa2;
        private System.Windows.Forms.ColumnHeader colObjId;
        private System.Windows.Forms.Button cmdAddObject;
        private System.Windows.Forms.Button cmdDelObject;

        // ── Tab 4 (Поставка) ──────────────────────────────────────────────────────
        private System.Windows.Forms.Label lblDeliveryInstrTitle;
        private System.Windows.Forms.CheckBox chkExternalStorage;
        private System.Windows.Forms.Label lblStorageNum;
        private System.Windows.Forms.TextBox txtStorageCode;
        private System.Windows.Forms.Label lblStorageName;
        private System.Windows.Forms.TextBox txtStorageName;

        // ── Tab 5 (Оплата) ────────────────────────────────────────────────────────
        private System.Windows.Forms.TabControl tabControlInstr;
        private System.Windows.Forms.TabPage tabPageInstr1;
        private System.Windows.Forms.TabPage tabPageInstr2;
        // Покупатель (index 0)
        private System.Windows.Forms.Label lblInstrTitle_0;
        private System.Windows.Forms.CheckBox chkCash_0;
        private System.Windows.Forms.Label lblBIK_0;
        private System.Windows.Forms.TextBox txtBIK_0;
        private System.Windows.Forms.Label lblKS_0;
        private System.Windows.Forms.TextBox txtKS_0;
        private System.Windows.Forms.Label lblNameBank_0;
        private System.Windows.Forms.TextBox txtName_0;
        private System.Windows.Forms.TextBox txtRS_0;
        private System.Windows.Forms.Label lblClient_0;
        private System.Windows.Forms.TextBox txtClient_0;
        private System.Windows.Forms.Label lblNote_0;
        private System.Windows.Forms.TextBox txtNote_0;
        private System.Windows.Forms.Label lblINN_0;
        private System.Windows.Forms.TextBox txtINN_0;
        private System.Windows.Forms.CheckBox chkNotAkcept_0;
        // Продавец (index 1)
        private System.Windows.Forms.Label lblInstrTitle_1;
        private System.Windows.Forms.CheckBox chkCash_1;
        private System.Windows.Forms.Label lblBIK_1;
        private System.Windows.Forms.TextBox txtBIK_1;
        private System.Windows.Forms.Label lblKS_1;
        private System.Windows.Forms.TextBox txtKS_1;
        private System.Windows.Forms.Label lblNameBank_1;
        private System.Windows.Forms.TextBox txtName_1;
        private System.Windows.Forms.TextBox txtRS_1;
        private System.Windows.Forms.Label lblClient_1;
        private System.Windows.Forms.TextBox txtClient_1;
        private System.Windows.Forms.Label lblNote_1;
        private System.Windows.Forms.TextBox txtNote_1;
        private System.Windows.Forms.Label lblINN_1;
        private System.Windows.Forms.TextBox txtINN_1;
        private System.Windows.Forms.CheckBox chkNotAkcept_1;

        // ── Tab 6 (Дополнительные) ────────────────────────────────────────────────
        private UbsControl.UbsCtrlFields ubsCtrlField;
        private Button btnContract1;
        private Button btnContract2;
        private LinkLabel linkStorage;
        private LinkLabel linkListInstr;
        private LinkLabel linkAccountPay;
        private LinkLabel linkListInstrSeller;
        private LinkLabel linkAccountSeller;
        private Button cmdExitOblig;
        private Button cmdApplayOblig;
        private LinkLabel linkAccountsOblig;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label2;
        private Label label1;
        private TabPage tabPageOblig1;
        private Label lblNaprTrade;
        private ComboBox cmbNaprTrade;
        private Label lblCurOblig;
        private ComboBox cmbCurOblig;
        private Label lblUnit;
        private ComboBox cmbUnit;
        private Label lblCostUnit;
        private UbsControl.UbsCtrlDecimal ucdCostUnit;
        private CheckBox chkRate;
        private UbsControl.UbsCtrlDecimal ucdRateCurOblig;
        private CheckBox chkSumInCurValue;
        private UbsControl.UbsCtrlDecimal ucdCostCurOpl;
        private GroupBox grpMetalChar;
        private Label lblDatePost;
        private UbsControl.UbsCtrlDate datePost;
        private Label lblMassa;
        private UbsControl.UbsCtrlDecimal ucdMassa;
        private Label lblMassaGramm;
        private UbsControl.UbsCtrlDecimal ucdMassaGramm;
        private GroupBox grpMetalCharPost;
        private Label lblDateOpl;
        private UbsControl.UbsCtrlDate dateOpl;
        private Label lblSumOblig;
        private UbsControl.UbsCtrlDecimal ucdSumOblig;
        private Label lblSumOpl;
        private UbsControl.UbsCtrlDecimal ucdSumOpl;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel7;
    }
}
