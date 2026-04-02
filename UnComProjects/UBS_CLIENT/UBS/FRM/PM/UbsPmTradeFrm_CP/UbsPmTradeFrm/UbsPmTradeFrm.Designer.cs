using System;
using System.Windows.Forms;

namespace UbsBusiness
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
            this.chkComposit = new System.Windows.Forms.CheckBox();
            this.lblDelivery = new System.Windows.Forms.Label();
            this.cmbKindSupplyTrade = new System.Windows.Forms.ComboBox();
            this.lblTradeType = new System.Windows.Forms.Label();
            this.cmbTradeType = new System.Windows.Forms.ComboBox();
            this.lblCurrencyPost = new System.Windows.Forms.Label();
            this.cmbCurrencyPost = new System.Windows.Forms.ComboBox();
            this.lblCurrencyOpl = new System.Windows.Forms.Label();
            this.cmbCurrencyPayment = new System.Windows.Forms.ComboBox();
            this.grpContracts = new System.Windows.Forms.GroupBox();
            this.linkContract2 = new System.Windows.Forms.LinkLabel();
            this.linkContract1 = new System.Windows.Forms.LinkLabel();
            this.lblBuyer = new System.Windows.Forms.Label();
            this.lblSeller = new System.Windows.Forms.Label();
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
            this.lvwObligation = new System.Windows.Forms.ListView();
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
            this.cmdAddObligation = new System.Windows.Forms.Button();
            this.cmdDelObligation = new System.Windows.Forms.Button();
            this.cmdEditObligation = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControlOblig = new System.Windows.Forms.TabControl();
            this.tabPageOblig1 = new System.Windows.Forms.TabPage();
            this.lblTradeDirection = new System.Windows.Forms.Label();
            this.cmbTradeDirection = new System.Windows.Forms.ComboBox();
            this.lblObligationCurrency = new System.Windows.Forms.Label();
            this.cmbCurrencyObligation = new System.Windows.Forms.ComboBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.lblCostUnit = new System.Windows.Forms.Label();
            this.ucdCostUnit = new UbsControl.UbsCtrlDecimal();
            this.chkRate = new System.Windows.Forms.CheckBox();
            this.ucdRateCurOblig = new UbsControl.UbsCtrlDecimal();
            this.chkSumInCurValue = new System.Windows.Forms.CheckBox();
            this.ucdCostCurPayment = new UbsControl.UbsCtrlDecimal();
            this.grpMetalChar = new System.Windows.Forms.GroupBox();
            this.lblDatePost = new System.Windows.Forms.Label();
            this.datePost = new UbsControl.UbsCtrlDate();
            this.lblMass = new System.Windows.Forms.Label();
            this.ucdMass = new UbsControl.UbsCtrlDecimal();
            this.lblMassGramm = new System.Windows.Forms.Label();
            this.ucdMassGramm = new UbsControl.UbsCtrlDecimal();
            this.grpMetalCharPost = new System.Windows.Forms.GroupBox();
            this.lblDatePayment = new System.Windows.Forms.Label();
            this.datePayment = new UbsControl.UbsCtrlDate();
            this.lblSumObligation = new System.Windows.Forms.Label();
            this.ucdSumObligation = new UbsControl.UbsCtrlDecimal();
            this.lblSumPayment = new System.Windows.Forms.Label();
            this.ucdSumPayment = new UbsControl.UbsCtrlDecimal();
            this.tabPageOblig2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lblObligationInfo1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lvwObject = new System.Windows.Forms.ListView();
            this.colObjInstr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObjCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObjLigMassa = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObjProba = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObjSeries = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObjNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colObjId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.cmdAddObject = new System.Windows.Forms.Button();
            this.cmdDelObject = new System.Windows.Forms.Button();
            this.lblObligationInfo2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.linkAccountsObligation = new System.Windows.Forms.LinkLabel();
            this.cmdExitObligation = new System.Windows.Forms.Button();
            this.cmdApplayObligation = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.linkStorage = new System.Windows.Forms.LinkLabel();
            this.lblDeliveryInstructionTitle = new System.Windows.Forms.Label();
            this.chkExternalStorage = new System.Windows.Forms.CheckBox();
            this.lblStorageNum = new System.Windows.Forms.Label();
            this.txtStorageCode = new System.Windows.Forms.TextBox();
            this.lblStorageName = new System.Windows.Forms.Label();
            this.txtStorageName = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabControlInstr = new System.Windows.Forms.TabControl();
            this.tabPageInstr1 = new System.Windows.Forms.TabPage();
            this.linkAccountPayment0 = new System.Windows.Forms.LinkLabel();
            this.linkListInstr0 = new System.Windows.Forms.LinkLabel();
            this.lblInstrTitlePayment0 = new System.Windows.Forms.Label();
            this.chkCash0 = new System.Windows.Forms.CheckBox();
            this.lblBIK0 = new System.Windows.Forms.Label();
            this.txtBIK0 = new System.Windows.Forms.TextBox();
            this.lblKS0 = new System.Windows.Forms.Label();
            this.ucaKS0 = new UbsControl.UbsCtrlAccount();
            this.lblNameBank0 = new System.Windows.Forms.Label();
            this.txtName0 = new System.Windows.Forms.TextBox();
            this.ucaRS0 = new UbsControl.UbsCtrlAccount();
            this.lblClient0 = new System.Windows.Forms.Label();
            this.txtClient0 = new System.Windows.Forms.TextBox();
            this.lblNote0 = new System.Windows.Forms.Label();
            this.txtNote0 = new System.Windows.Forms.TextBox();
            this.lblINN0 = new System.Windows.Forms.Label();
            this.txtINN0 = new System.Windows.Forms.TextBox();
            this.chkNotAkcept0 = new System.Windows.Forms.CheckBox();
            this.tabPageInstr2 = new System.Windows.Forms.TabPage();
            this.linkAccountPayment1 = new System.Windows.Forms.LinkLabel();
            this.linkListInstr1 = new System.Windows.Forms.LinkLabel();
            this.lblInstrTitlePayment1 = new System.Windows.Forms.Label();
            this.chkCash1 = new System.Windows.Forms.CheckBox();
            this.lblBIK1 = new System.Windows.Forms.Label();
            this.txtBIK1 = new System.Windows.Forms.TextBox();
            this.lblKS1 = new System.Windows.Forms.Label();
            this.ucaKS1 = new UbsControl.UbsCtrlAccount();
            this.lblNameBank1 = new System.Windows.Forms.Label();
            this.txtName1 = new System.Windows.Forms.TextBox();
            this.ucaRS1 = new UbsControl.UbsCtrlAccount();
            this.lblClient1 = new System.Windows.Forms.Label();
            this.txtClient1 = new System.Windows.Forms.TextBox();
            this.lblNote1 = new System.Windows.Forms.Label();
            this.txtNote1 = new System.Windows.Forms.TextBox();
            this.lblINN1 = new System.Windows.Forms.Label();
            this.txtINN1 = new System.Windows.Forms.TextBox();
            this.chkNotAkcept1 = new System.Windows.Forms.CheckBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.ubsCtrlField = new UbsControl.UbsCtrlFields();
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
            this.tableLayoutPanel6.SuspendLayout();
            this.tabControlOblig.SuspendLayout();
            this.tabPageOblig1.SuspendLayout();
            this.grpMetalChar.SuspendLayout();
            this.grpMetalCharPost.SuspendLayout();
            this.tabPageOblig2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabControlInstr.SuspendLayout();
            this.tabPageInstr1.SuspendLayout();
            this.tabPageInstr2.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tabControl);
            this.panelMain.Controls.Add(this.tableLayoutPanel);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4);
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
            this.tabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl_Selecting);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grpTrade);
            this.tabPage1.Controls.Add(this.lblCurrencyPost);
            this.tabPage1.Controls.Add(this.cmbCurrencyPost);
            this.tabPage1.Controls.Add(this.lblCurrencyOpl);
            this.tabPage1.Controls.Add(this.cmbCurrencyPayment);
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
            this.grpTrade.Controls.Add(this.chkComposit);
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
            this.dateTrade.Leave += new System.EventHandler(this.dateTrade_Leave);
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(202, 24);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(41, 13);
            this.lblNum.TabIndex = 0;
            this.lblNum.Text = "Номер";
            // 
            // txtTradeNum
            // 
            this.txtTradeNum.Location = new System.Drawing.Point(249, 20);
            this.txtTradeNum.MaxLength = 20;
            this.txtTradeNum.Name = "txtTradeNum";
            this.txtTradeNum.Size = new System.Drawing.Size(134, 20);
            this.txtTradeNum.TabIndex = 2;
            // 
            // chkComposit
            // 
            this.chkComposit.AutoSize = true;
            this.chkComposit.Location = new System.Drawing.Point(389, 22);
            this.chkComposit.Name = "chkComposit";
            this.chkComposit.Size = new System.Drawing.Size(86, 17);
            this.chkComposit.TabIndex = 3;
            this.chkComposit.Text = "Составная?";
            this.chkComposit.UseVisualStyleBackColor = true;
            this.chkComposit.CheckedChanged += new System.EventHandler(this.chkComposit_CheckedChanged);
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
            this.cmbKindSupplyTrade.SelectedIndexChanged += new System.EventHandler(this.cmbKindSupplyTrade_SelectedIndexChanged);
            // 
            // lblTradeType
            // 
            this.lblTradeType.AutoSize = true;
            this.lblTradeType.Location = new System.Drawing.Point(249, 50);
            this.lblTradeType.Name = "lblTradeType";
            this.lblTradeType.Size = new System.Drawing.Size(26, 13);
            this.lblTradeType.TabIndex = 0;
            this.lblTradeType.Text = "Тип";
            this.lblTradeType.Visible = false;
            // 
            // cmbTradeType
            // 
            this.cmbTradeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTradeType.Location = new System.Drawing.Point(281, 46);
            this.cmbTradeType.Name = "cmbTradeType";
            this.cmbTradeType.Size = new System.Drawing.Size(176, 21);
            this.cmbTradeType.TabIndex = 5;
            this.cmbTradeType.Visible = false;
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
            this.cmbCurrencyPost.SelectedIndexChanged += new System.EventHandler(this.cmbCurrencyPost_SelectedIndexChanged);
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
            // cmbCurrencyPayment
            // 
            this.cmbCurrencyPayment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrencyPayment.Location = new System.Drawing.Point(112, 115);
            this.cmbCurrencyPayment.Name = "cmbCurrencyPayment";
            this.cmbCurrencyPayment.Size = new System.Drawing.Size(173, 21);
            this.cmbCurrencyPayment.TabIndex = 2;
            this.cmbCurrencyPayment.SelectedIndexChanged += new System.EventHandler(this.cmbCurrencyPayment_SelectedIndexChanged);
            // 
            // grpContracts
            // 
            this.grpContracts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpContracts.Controls.Add(this.linkContract2);
            this.grpContracts.Controls.Add(this.linkContract1);
            this.grpContracts.Controls.Add(this.lblBuyer);
            this.grpContracts.Controls.Add(this.lblSeller);
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
            // linkContract2
            // 
            this.linkContract2.AutoSize = true;
            this.linkContract2.Location = new System.Drawing.Point(303, 73);
            this.linkContract2.Name = "linkContract2";
            this.linkContract2.Size = new System.Drawing.Size(51, 13);
            this.linkContract2.TabIndex = 8;
            this.linkContract2.TabStop = true;
            this.linkContract2.Text = "Договор";
            this.linkContract2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkContract1
            // 
            this.linkContract1.AutoSize = true;
            this.linkContract1.Location = new System.Drawing.Point(303, 22);
            this.linkContract1.Name = "linkContract1";
            this.linkContract1.Size = new System.Drawing.Size(51, 13);
            this.linkContract1.TabIndex = 3;
            this.linkContract1.TabStop = true;
            this.linkContract1.Text = "Договор";
            this.linkContract1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lblBuyer
            // 
            this.lblBuyer.AutoSize = true;
            this.lblBuyer.Location = new System.Drawing.Point(8, 73);
            this.lblBuyer.Name = "lblBuyer";
            this.lblBuyer.Size = new System.Drawing.Size(67, 13);
            this.lblBuyer.TabIndex = 6;
            this.lblBuyer.Text = "Покупатель";
            // 
            // lblSeller
            // 
            this.lblSeller.AutoSize = true;
            this.lblSeller.Location = new System.Drawing.Point(8, 22);
            this.lblSeller.Name = "lblSeller";
            this.lblSeller.Size = new System.Drawing.Size(57, 13);
            this.lblSeller.TabIndex = 1;
            this.lblSeller.Text = "Продавец";
            // 
            // cmbContractType1
            // 
            this.cmbContractType1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbContractType1.Location = new System.Drawing.Point(88, 19);
            this.cmbContractType1.Name = "cmbContractType1";
            this.cmbContractType1.Size = new System.Drawing.Size(209, 21);
            this.cmbContractType1.TabIndex = 2;
            this.cmbContractType1.SelectedIndexChanged += new System.EventHandler(this.cmbContractType1_SelectedIndexChanged);
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
            this.cmbContractType2.SelectedIndexChanged += new System.EventHandler(this.cmbContractType2_SelectedIndexChanged);
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
            this.chkNDS.CheckedChanged += new System.EventHandler(this.chkNDS_CheckedChanged);
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
            this.cmbComission.Size = new System.Drawing.Size(208, 21);
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
            this.tableLayoutPanel1.Controls.Add(this.lvwObligation, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(671, 559);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // lvwObligation
            // 
            this.lvwObligation.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
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
            this.lvwObligation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwObligation.FullRowSelect = true;
            this.lvwObligation.GridLines = true;
            this.lvwObligation.HideSelection = false;
            this.lvwObligation.Location = new System.Drawing.Point(3, 3);
            this.lvwObligation.MultiSelect = false;
            this.lvwObligation.Name = "lvwObligation";
            this.lvwObligation.Size = new System.Drawing.Size(581, 553);
            this.lvwObligation.TabIndex = 0;
            this.lvwObligation.UseCompatibleStateImageBehavior = false;
            this.lvwObligation.View = System.Windows.Forms.View.Details;
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
            this.tableLayoutPanel2.Controls.Add(this.cmdAddObligation, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmdDelObligation, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.cmdEditObligation, 0, 1);
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
            // cmdAddObligation
            // 
            this.cmdAddObligation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdAddObligation.Location = new System.Drawing.Point(3, 3);
            this.cmdAddObligation.Name = "cmdAddObligation";
            this.cmdAddObligation.Size = new System.Drawing.Size(72, 26);
            this.cmdAddObligation.TabIndex = 1;
            this.cmdAddObligation.Text = "Добавить";
            this.cmdAddObligation.UseVisualStyleBackColor = true;
            this.cmdAddObligation.Click += new System.EventHandler(this.cmdAddObligation_Click);
            // 
            // cmdDelObligation
            // 
            this.cmdDelObligation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdDelObligation.Location = new System.Drawing.Point(3, 67);
            this.cmdDelObligation.Name = "cmdDelObligation";
            this.cmdDelObligation.Size = new System.Drawing.Size(72, 26);
            this.cmdDelObligation.TabIndex = 3;
            this.cmdDelObligation.Text = "Удалить";
            this.cmdDelObligation.UseVisualStyleBackColor = true;
            this.cmdDelObligation.Click += new System.EventHandler(this.cmdDelObligation_Click);
            // 
            // cmdEditObligation
            // 
            this.cmdEditObligation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdEditObligation.Location = new System.Drawing.Point(3, 35);
            this.cmdEditObligation.Name = "cmdEditObligation";
            this.cmdEditObligation.Size = new System.Drawing.Size(72, 26);
            this.cmdEditObligation.TabIndex = 2;
            this.cmdEditObligation.Text = "Изменить";
            this.cmdEditObligation.UseVisualStyleBackColor = true;
            this.cmdEditObligation.Click += new System.EventHandler(this.cmdEditObligation_Click);
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
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(671, 559);
            this.tableLayoutPanel6.TabIndex = 4;
            // 
            // tabControlOblig
            // 
            this.tabControlOblig.Controls.Add(this.tabPageOblig1);
            this.tabControlOblig.Controls.Add(this.tabPageOblig2);
            this.tabControlOblig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlOblig.Location = new System.Drawing.Point(3, 3);
            this.tabControlOblig.Name = "tabControlOblig";
            this.tabControlOblig.SelectedIndex = 0;
            this.tabControlOblig.Size = new System.Drawing.Size(665, 515);
            this.tabControlOblig.TabIndex = 0;
            this.tabControlOblig.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControlOblig_Selecting);
            // 
            // tabPageOblig1
            // 
            this.tabPageOblig1.Controls.Add(this.lblTradeDirection);
            this.tabPageOblig1.Controls.Add(this.cmbTradeDirection);
            this.tabPageOblig1.Controls.Add(this.lblObligationCurrency);
            this.tabPageOblig1.Controls.Add(this.cmbCurrencyObligation);
            this.tabPageOblig1.Controls.Add(this.lblUnit);
            this.tabPageOblig1.Controls.Add(this.cmbUnit);
            this.tabPageOblig1.Controls.Add(this.lblCostUnit);
            this.tabPageOblig1.Controls.Add(this.ucdCostUnit);
            this.tabPageOblig1.Controls.Add(this.chkRate);
            this.tabPageOblig1.Controls.Add(this.ucdRateCurOblig);
            this.tabPageOblig1.Controls.Add(this.chkSumInCurValue);
            this.tabPageOblig1.Controls.Add(this.ucdCostCurPayment);
            this.tabPageOblig1.Controls.Add(this.grpMetalChar);
            this.tabPageOblig1.Controls.Add(this.grpMetalCharPost);
            this.tabPageOblig1.Location = new System.Drawing.Point(4, 22);
            this.tabPageOblig1.Name = "tabPageOblig1";
            this.tabPageOblig1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOblig1.Size = new System.Drawing.Size(657, 489);
            this.tabPageOblig1.TabIndex = 0;
            this.tabPageOblig1.Text = "Обязательство";
            this.tabPageOblig1.UseVisualStyleBackColor = true;
            // 
            // lblTradeDirection
            // 
            this.lblTradeDirection.AutoSize = true;
            this.lblTradeDirection.Location = new System.Drawing.Point(6, 10);
            this.lblTradeDirection.Name = "lblTradeDirection";
            this.lblTradeDirection.Size = new System.Drawing.Size(114, 13);
            this.lblTradeDirection.TabIndex = 0;
            this.lblTradeDirection.Text = "Направление сделки";
            // 
            // cmbTradeDirection
            // 
            this.cmbTradeDirection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTradeDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTradeDirection.Location = new System.Drawing.Point(168, 6);
            this.cmbTradeDirection.Name = "cmbTradeDirection";
            this.cmbTradeDirection.Size = new System.Drawing.Size(486, 21);
            this.cmbTradeDirection.TabIndex = 1;
            // 
            // lblObligationCurrency
            // 
            this.lblObligationCurrency.AutoSize = true;
            this.lblObligationCurrency.Location = new System.Drawing.Point(6, 35);
            this.lblObligationCurrency.Name = "lblObligationCurrency";
            this.lblObligationCurrency.Size = new System.Drawing.Size(124, 13);
            this.lblObligationCurrency.TabIndex = 0;
            this.lblObligationCurrency.Text = "Валюта обязательства";
            // 
            // cmbCurrencyObligation
            // 
            this.cmbCurrencyObligation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCurrencyObligation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrencyObligation.Location = new System.Drawing.Point(168, 31);
            this.cmbCurrencyObligation.Name = "cmbCurrencyObligation";
            this.cmbCurrencyObligation.Size = new System.Drawing.Size(486, 21);
            this.cmbCurrencyObligation.TabIndex = 2;
            this.cmbCurrencyObligation.SelectedIndexChanged += new System.EventHandler(this.cmbObligationCurrency_SelectedIndexChanged);
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
            // cmbUnit
            // 
            this.cmbUnit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.Location = new System.Drawing.Point(168, 56);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(486, 21);
            this.cmbUnit.TabIndex = 3;
            this.cmbUnit.SelectedIndexChanged += new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
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
            // ucdCostUnit
            // 
            this.ucdCostUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ucdCostUnit.Location = new System.Drawing.Point(281, 82);
            this.ucdCostUnit.Name = "ucdCostUnit";
            this.ucdCostUnit.Precision = 4;
            this.ucdCostUnit.Range = 14;
            this.ucdCostUnit.Size = new System.Drawing.Size(207, 20);
            this.ucdCostUnit.TabIndex = 4;
            this.ucdCostUnit.Text = "0";
            this.ucdCostUnit.Leave += new System.EventHandler(this.ucdCostUnit_Leave);
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
            this.chkRate.CheckedChanged += new System.EventHandler(this.chkRate_CheckedChanged);
            // 
            // ucdRateCurOblig
            // 
            this.ucdRateCurOblig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ucdRateCurOblig.Enabled = false;
            this.ucdRateCurOblig.Location = new System.Drawing.Point(281, 107);
            this.ucdRateCurOblig.Name = "ucdRateCurOblig";
            this.ucdRateCurOblig.Precision = 10;
            this.ucdRateCurOblig.Range = 14;
            this.ucdRateCurOblig.Size = new System.Drawing.Size(207, 20);
            this.ucdRateCurOblig.TabIndex = 6;
            this.ucdRateCurOblig.Text = "0";
            this.ucdRateCurOblig.Leave += new System.EventHandler(this.ucdRateCurOblig_Leave);
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
            this.chkSumInCurValue.CheckedChanged += new System.EventHandler(this.chkSumInCurValue_CheckedChanged);
            // 
            // ucdCostCurPayment
            // 
            this.ucdCostCurPayment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ucdCostCurPayment.Enabled = false;
            this.ucdCostCurPayment.Location = new System.Drawing.Point(281, 131);
            this.ucdCostCurPayment.Name = "ucdCostCurPayment";
            this.ucdCostCurPayment.Precision = 4;
            this.ucdCostCurPayment.Range = 14;
            this.ucdCostCurPayment.Size = new System.Drawing.Size(207, 20);
            this.ucdCostCurPayment.TabIndex = 8;
            this.ucdCostCurPayment.Text = "0";
            this.ucdCostCurPayment.Leave += new System.EventHandler(this.ucdCostCurOpl_Leave);
            // 
            // grpMetalChar
            // 
            this.grpMetalChar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMetalChar.Controls.Add(this.lblDatePost);
            this.grpMetalChar.Controls.Add(this.datePost);
            this.grpMetalChar.Controls.Add(this.lblMass);
            this.grpMetalChar.Controls.Add(this.ucdMass);
            this.grpMetalChar.Controls.Add(this.lblMassGramm);
            this.grpMetalChar.Controls.Add(this.ucdMassGramm);
            this.grpMetalChar.Location = new System.Drawing.Point(6, 158);
            this.grpMetalChar.Name = "grpMetalChar";
            this.grpMetalChar.Size = new System.Drawing.Size(503, 77);
            this.grpMetalChar.TabIndex = 9;
            this.grpMetalChar.TabStop = false;
            this.grpMetalChar.Text = "Обязательство поставки";
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
            // lblMass
            // 
            this.lblMass.AutoSize = true;
            this.lblMass.Location = new System.Drawing.Point(8, 52);
            this.lblMass.Name = "lblMass";
            this.lblMass.Size = new System.Drawing.Size(93, 13);
            this.lblMass.TabIndex = 0;
            this.lblMass.Text = "Масса в ед. изм.";
            // 
            // ucdMass
            // 
            this.ucdMass.Location = new System.Drawing.Point(110, 49);
            this.ucdMass.Name = "ucdMass";
            this.ucdMass.Range = 14;
            this.ucdMass.Size = new System.Drawing.Size(120, 20);
            this.ucdMass.TabIndex = 2;
            this.ucdMass.Text = "0";
            this.ucdMass.Leave += new System.EventHandler(this.ucdMass_Leave);
            // 
            // lblMassGramm
            // 
            this.lblMassGramm.AutoSize = true;
            this.lblMassGramm.Location = new System.Drawing.Point(236, 52);
            this.lblMassGramm.Name = "lblMassGramm";
            this.lblMassGramm.Size = new System.Drawing.Size(142, 13);
            this.lblMassGramm.TabIndex = 0;
            this.lblMassGramm.Text = "Масса металла в граммах";
            // 
            // ucdMassGramm
            // 
            this.ucdMassGramm.Enabled = false;
            this.ucdMassGramm.Location = new System.Drawing.Point(384, 49);
            this.ucdMassGramm.Name = "ucdMassGramm";
            this.ucdMassGramm.Precision = 1;
            this.ucdMassGramm.Range = 14;
            this.ucdMassGramm.Size = new System.Drawing.Size(112, 20);
            this.ucdMassGramm.TabIndex = 3;
            this.ucdMassGramm.Text = "0";
            // 
            // grpMetalCharPost
            // 
            this.grpMetalCharPost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMetalCharPost.Controls.Add(this.lblDatePayment);
            this.grpMetalCharPost.Controls.Add(this.datePayment);
            this.grpMetalCharPost.Controls.Add(this.lblSumObligation);
            this.grpMetalCharPost.Controls.Add(this.ucdSumObligation);
            this.grpMetalCharPost.Controls.Add(this.lblSumPayment);
            this.grpMetalCharPost.Controls.Add(this.ucdSumPayment);
            this.grpMetalCharPost.Location = new System.Drawing.Point(6, 241);
            this.grpMetalCharPost.Name = "grpMetalCharPost";
            this.grpMetalCharPost.Size = new System.Drawing.Size(504, 99);
            this.grpMetalCharPost.TabIndex = 10;
            this.grpMetalCharPost.TabStop = false;
            this.grpMetalCharPost.Text = "Обязательство оплаты";
            // 
            // lblDatePayment
            // 
            this.lblDatePayment.AutoSize = true;
            this.lblDatePayment.Location = new System.Drawing.Point(8, 23);
            this.lblDatePayment.Name = "lblDatePayment";
            this.lblDatePayment.Size = new System.Drawing.Size(73, 13);
            this.lblDatePayment.TabIndex = 0;
            this.lblDatePayment.Text = "Дата оплаты";
            // 
            // datePayment
            // 
            this.datePayment.Location = new System.Drawing.Point(175, 19);
            this.datePayment.MaxLength = 10;
            this.datePayment.Name = "datePayment";
            this.datePayment.Size = new System.Drawing.Size(100, 20);
            this.datePayment.TabIndex = 1;
            this.datePayment.Text = "  .  .    ";
            this.datePayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblSumObligation
            // 
            this.lblSumObligation.AutoSize = true;
            this.lblSumObligation.Location = new System.Drawing.Point(8, 50);
            this.lblSumObligation.Name = "lblSumObligation";
            this.lblSumObligation.Size = new System.Drawing.Size(132, 13);
            this.lblSumObligation.TabIndex = 0;
            this.lblSumObligation.Text = "Сумма в валюте обяз-ва";
            // 
            // ucdSumObligation
            // 
            this.ucdSumObligation.Enabled = false;
            this.ucdSumObligation.Location = new System.Drawing.Point(175, 47);
            this.ucdSumObligation.Name = "ucdSumObligation";
            this.ucdSumObligation.Range = 14;
            this.ucdSumObligation.Size = new System.Drawing.Size(120, 20);
            this.ucdSumObligation.TabIndex = 2;
            this.ucdSumObligation.Text = "0";
            // 
            // lblSumPayment
            // 
            this.lblSumPayment.AutoSize = true;
            this.lblSumPayment.Location = new System.Drawing.Point(8, 76);
            this.lblSumPayment.Name = "lblSumPayment";
            this.lblSumPayment.Size = new System.Drawing.Size(130, 13);
            this.lblSumPayment.TabIndex = 0;
            this.lblSumPayment.Text = "Сумма в валюте оплаты";
            // 
            // ucdSumPayment
            // 
            this.ucdSumPayment.Enabled = false;
            this.ucdSumPayment.Location = new System.Drawing.Point(175, 73);
            this.ucdSumPayment.Name = "ucdSumPayment";
            this.ucdSumPayment.Range = 14;
            this.ucdSumPayment.Size = new System.Drawing.Size(120, 20);
            this.ucdSumPayment.TabIndex = 3;
            this.ucdSumPayment.Text = "0";
            // 
            // tabPageOblig2
            // 
            this.tabPageOblig2.Controls.Add(this.tableLayoutPanel5);
            this.tabPageOblig2.Location = new System.Drawing.Point(4, 22);
            this.tabPageOblig2.Name = "tabPageOblig2";
            this.tabPageOblig2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOblig2.Size = new System.Drawing.Size(657, 489);
            this.tabPageOblig2.TabIndex = 1;
            this.tabPageOblig2.Text = "Объекты";
            this.tabPageOblig2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.lblObligationInfo1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.lblObligationInfo2, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(651, 483);
            this.tableLayoutPanel5.TabIndex = 5;
            // 
            // lblObligationInfo1
            // 
            this.lblObligationInfo1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblObligationInfo1.Location = new System.Drawing.Point(3, 0);
            this.lblObligationInfo1.Name = "lblObligationInfo1";
            this.lblObligationInfo1.Size = new System.Drawing.Size(645, 15);
            this.lblObligationInfo1.TabIndex = 0;
            this.lblObligationInfo1.Text = "Дата оплаты, Дата поставки";
            this.lblObligationInfo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.5969F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.4031F));
            this.tableLayoutPanel3.Controls.Add(this.lvwObject, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 43);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(645, 437);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // lvwObject
            // 
            this.lvwObject.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colObjInstr,
            this.colObjCode,
            this.colObjLigMassa,
            this.colObjProba,
            this.colObjSeries,
            this.colObjNumber,
            this.colObjId});
            this.lvwObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwObject.FullRowSelect = true;
            this.lvwObject.GridLines = true;
            this.lvwObject.HideSelection = false;
            this.lvwObject.Location = new System.Drawing.Point(3, 3);
            this.lvwObject.MultiSelect = false;
            this.lvwObject.Name = "lvwObject";
            this.lvwObject.Size = new System.Drawing.Size(559, 431);
            this.lvwObject.TabIndex = 1;
            this.lvwObject.UseCompatibleStateImageBehavior = false;
            this.lvwObject.View = System.Windows.Forms.View.Details;
            // 
            // colObjInstr
            // 
            this.colObjInstr.Text = "Драг.металл";
            this.colObjInstr.Width = 100;
            // 
            // colObjCode
            // 
            this.colObjCode.Text = "Вид поставки";
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
            // colObjSeries
            // 
            this.colObjSeries.Text = "Серия";
            this.colObjSeries.Width = 70;
            // 
            // colObjNumber
            // 
            this.colObjNumber.Text = "Номер";
            this.colObjNumber.Width = 69;
            // 
            // colObjId
            // 
            this.colObjId.Text = "Идентификатор объекта";
            this.colObjId.Width = 100;
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
            this.tableLayoutPanel4.Size = new System.Drawing.Size(74, 431);
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
            this.cmdAddObject.Click += new System.EventHandler(this.cmdAddObject_Click);
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
            this.cmdDelObject.Click += new System.EventHandler(this.cmdDelObject_Click);
            // 
            // lblObligationInfo2
            // 
            this.lblObligationInfo2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblObligationInfo2.Location = new System.Drawing.Point(3, 20);
            this.lblObligationInfo2.Name = "lblObligationInfo2";
            this.lblObligationInfo2.Size = new System.Drawing.Size(645, 15);
            this.lblObligationInfo2.TabIndex = 0;
            this.lblObligationInfo2.Text = "Цена, Масса, Сумма";
            this.lblObligationInfo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel7.Controls.Add(this.linkAccountsObligation, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.cmdExitObligation, 2, 0);
            this.tableLayoutPanel7.Controls.Add(this.cmdApplayObligation, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 524);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(665, 32);
            this.tableLayoutPanel7.TabIndex = 1;
            // 
            // linkAccountsObligation
            // 
            this.linkAccountsObligation.AutoSize = true;
            this.linkAccountsObligation.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.linkAccountsObligation.Location = new System.Drawing.Point(3, 19);
            this.linkAccountsObligation.Name = "linkAccountsObligation";
            this.linkAccountsObligation.Size = new System.Drawing.Size(483, 13);
            this.linkAccountsObligation.TabIndex = 1;
            this.linkAccountsObligation.TabStop = true;
            this.linkAccountsObligation.Text = "Счета по обязательству";
            this.linkAccountsObligation.Visible = false;
            // 
            // cmdExitObligation
            // 
            this.cmdExitObligation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdExitObligation.Location = new System.Drawing.Point(580, 3);
            this.cmdExitObligation.Name = "cmdExitObligation";
            this.cmdExitObligation.Size = new System.Drawing.Size(82, 26);
            this.cmdExitObligation.TabIndex = 3;
            this.cmdExitObligation.Text = "Отмена";
            this.cmdExitObligation.UseVisualStyleBackColor = true;
            this.cmdExitObligation.Visible = false;
            this.cmdExitObligation.Click += new System.EventHandler(this.cmdExitObligation_Click);
            // 
            // cmdApplayObligation
            // 
            this.cmdApplayObligation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdApplayObligation.Location = new System.Drawing.Point(492, 3);
            this.cmdApplayObligation.Name = "cmdApplayObligation";
            this.cmdApplayObligation.Size = new System.Drawing.Size(82, 26);
            this.cmdApplayObligation.TabIndex = 2;
            this.cmdApplayObligation.Text = "Применить";
            this.cmdApplayObligation.UseVisualStyleBackColor = true;
            this.cmdApplayObligation.Visible = false;
            this.cmdApplayObligation.Click += new System.EventHandler(this.cmdApplayObligation_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.linkStorage);
            this.tabPage4.Controls.Add(this.lblDeliveryInstructionTitle);
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
            this.linkStorage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkStorage_LinkClicked);
            // 
            // lblDeliveryInstructionTitle
            // 
            this.lblDeliveryInstructionTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDeliveryInstructionTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDeliveryInstructionTitle.Location = new System.Drawing.Point(6, 16);
            this.lblDeliveryInstructionTitle.Name = "lblDeliveryInstructionTitle";
            this.lblDeliveryInstructionTitle.Size = new System.Drawing.Size(663, 20);
            this.lblDeliveryInstructionTitle.TabIndex = 0;
            this.lblDeliveryInstructionTitle.Text = "Инструкция по поставке";
            this.lblDeliveryInstructionTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.tabPageInstr1.Controls.Add(this.linkAccountPayment0);
            this.tabPageInstr1.Controls.Add(this.linkListInstr0);
            this.tabPageInstr1.Controls.Add(this.lblInstrTitlePayment0);
            this.tabPageInstr1.Controls.Add(this.chkCash0);
            this.tabPageInstr1.Controls.Add(this.lblBIK0);
            this.tabPageInstr1.Controls.Add(this.txtBIK0);
            this.tabPageInstr1.Controls.Add(this.lblKS0);
            this.tabPageInstr1.Controls.Add(this.ucaKS0);
            this.tabPageInstr1.Controls.Add(this.lblNameBank0);
            this.tabPageInstr1.Controls.Add(this.txtName0);
            this.tabPageInstr1.Controls.Add(this.ucaRS0);
            this.tabPageInstr1.Controls.Add(this.lblClient0);
            this.tabPageInstr1.Controls.Add(this.txtClient0);
            this.tabPageInstr1.Controls.Add(this.lblNote0);
            this.tabPageInstr1.Controls.Add(this.txtNote0);
            this.tabPageInstr1.Controls.Add(this.lblINN0);
            this.tabPageInstr1.Controls.Add(this.txtINN0);
            this.tabPageInstr1.Controls.Add(this.chkNotAkcept0);
            this.tabPageInstr1.Location = new System.Drawing.Point(4, 22);
            this.tabPageInstr1.Name = "tabPageInstr1";
            this.tabPageInstr1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInstr1.Size = new System.Drawing.Size(663, 533);
            this.tabPageInstr1.TabIndex = 0;
            this.tabPageInstr1.Text = "Покупатель";
            this.tabPageInstr1.UseVisualStyleBackColor = true;
            // 
            // linkAccountPayment0
            // 
            this.linkAccountPayment0.AutoSize = true;
            this.linkAccountPayment0.Location = new System.Drawing.Point(6, 139);
            this.linkAccountPayment0.Name = "linkAccountPayment0";
            this.linkAccountPayment0.Size = new System.Drawing.Size(59, 13);
            this.linkAccountPayment0.TabIndex = 6;
            this.linkAccountPayment0.TabStop = true;
            this.linkAccountPayment0.Text = "Расч. счет";
            this.linkAccountPayment0.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAccountPayment0_LinkClicked);
            // 
            // linkListInstr0
            // 
            this.linkListInstr0.AutoSize = true;
            this.linkListInstr0.Location = new System.Drawing.Point(6, 60);
            this.linkListInstr0.Name = "linkListInstr0";
            this.linkListInstr0.Size = new System.Drawing.Size(237, 13);
            this.linkListInstr0.TabIndex = 2;
            this.linkListInstr0.TabStop = true;
            this.linkListInstr0.Text = "Выбор платежной инструкции по покупателю";
            this.linkListInstr0.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkListInstr0_LinkClicked);
            // 
            // lblInstrTitlePayment0
            // 
            this.lblInstrTitlePayment0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInstrTitlePayment0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblInstrTitlePayment0.Location = new System.Drawing.Point(6, 12);
            this.lblInstrTitlePayment0.Name = "lblInstrTitlePayment0";
            this.lblInstrTitlePayment0.Size = new System.Drawing.Size(651, 20);
            this.lblInstrTitlePayment0.TabIndex = 0;
            this.lblInstrTitlePayment0.Text = "Инструкция по оплате";
            this.lblInstrTitlePayment0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkCash0
            // 
            this.chkCash0.AutoSize = true;
            this.chkCash0.Location = new System.Drawing.Point(9, 40);
            this.chkCash0.Name = "chkCash0";
            this.chkCash0.Size = new System.Drawing.Size(125, 17);
            this.chkCash0.TabIndex = 1;
            this.chkCash0.Text = "Расчет через кассу";
            this.chkCash0.UseVisualStyleBackColor = true;
            this.chkCash0.CheckedChanged += new System.EventHandler(this.chkCash_Click);
            // 
            // lblBIK0
            // 
            this.lblBIK0.AutoSize = true;
            this.lblBIK0.Location = new System.Drawing.Point(6, 92);
            this.lblBIK0.Name = "lblBIK0";
            this.lblBIK0.Size = new System.Drawing.Size(29, 13);
            this.lblBIK0.TabIndex = 0;
            this.lblBIK0.Text = "БИК";
            // 
            // txtBIK0
            // 
            this.txtBIK0.Location = new System.Drawing.Point(90, 88);
            this.txtBIK0.MaxLength = 9;
            this.txtBIK0.Name = "txtBIK0";
            this.txtBIK0.Size = new System.Drawing.Size(80, 20);
            this.txtBIK0.TabIndex = 3;
            // 
            // lblKS0
            // 
            this.lblKS0.AutoSize = true;
            this.lblKS0.Location = new System.Drawing.Point(176, 92);
            this.lblKS0.Name = "lblKS0";
            this.lblKS0.Size = new System.Drawing.Size(60, 13);
            this.lblKS0.TabIndex = 0;
            this.lblKS0.Text = "Корр. счет";
            // 
            // ucaKS0
            // 
            this.ucaKS0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucaKS0.Enabled = false;
            this.ucaKS0.Location = new System.Drawing.Point(242, 88);
            this.ucaKS0.MaxLength = 24;
            this.ucaKS0.Name = "ucaKS0";
            this.ucaKS0.ReadOnly = true;
            this.ucaKS0.Size = new System.Drawing.Size(415, 20);
            this.ucaKS0.TabIndex = 4;
            this.ucaKS0.TabStop = false;
            // 
            // lblNameBank0
            // 
            this.lblNameBank0.AutoSize = true;
            this.lblNameBank0.Location = new System.Drawing.Point(6, 116);
            this.lblNameBank0.Name = "lblNameBank0";
            this.lblNameBank0.Size = new System.Drawing.Size(71, 13);
            this.lblNameBank0.TabIndex = 0;
            this.lblNameBank0.Text = "Наим. банка";
            // 
            // txtName0
            // 
            this.txtName0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName0.Location = new System.Drawing.Point(90, 112);
            this.txtName0.Name = "txtName0";
            this.txtName0.ReadOnly = true;
            this.txtName0.Size = new System.Drawing.Size(567, 20);
            this.txtName0.TabIndex = 5;
            this.txtName0.TabStop = false;
            // 
            // ucaRS0
            // 
            this.ucaRS0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucaRS0.Enabled = false;
            this.ucaRS0.Location = new System.Drawing.Point(90, 136);
            this.ucaRS0.MaxLength = 24;
            this.ucaRS0.Name = "ucaRS0";
            this.ucaRS0.ReadOnly = true;
            this.ucaRS0.Size = new System.Drawing.Size(567, 20);
            this.ucaRS0.TabIndex = 7;
            this.ucaRS0.TabStop = false;
            // 
            // lblClient0
            // 
            this.lblClient0.AutoSize = true;
            this.lblClient0.Location = new System.Drawing.Point(6, 164);
            this.lblClient0.Name = "lblClient0";
            this.lblClient0.Size = new System.Drawing.Size(43, 13);
            this.lblClient0.TabIndex = 0;
            this.lblClient0.Text = "Клиент";
            // 
            // txtClient0
            // 
            this.txtClient0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClient0.Location = new System.Drawing.Point(90, 160);
            this.txtClient0.Name = "txtClient0";
            this.txtClient0.Size = new System.Drawing.Size(567, 20);
            this.txtClient0.TabIndex = 8;
            // 
            // lblNote0
            // 
            this.lblNote0.AutoSize = true;
            this.lblNote0.Location = new System.Drawing.Point(6, 188);
            this.lblNote0.Name = "lblNote0";
            this.lblNote0.Size = new System.Drawing.Size(70, 13);
            this.lblNote0.TabIndex = 0;
            this.lblNote0.Text = "Примечание";
            // 
            // txtNote0
            // 
            this.txtNote0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote0.Location = new System.Drawing.Point(90, 184);
            this.txtNote0.Multiline = true;
            this.txtNote0.Name = "txtNote0";
            this.txtNote0.Size = new System.Drawing.Size(567, 65);
            this.txtNote0.TabIndex = 9;
            // 
            // lblINN0
            // 
            this.lblINN0.AutoSize = true;
            this.lblINN0.Location = new System.Drawing.Point(6, 257);
            this.lblINN0.Name = "lblINN0";
            this.lblINN0.Size = new System.Drawing.Size(31, 13);
            this.lblINN0.TabIndex = 0;
            this.lblINN0.Text = "ИНН";
            // 
            // txtINN0
            // 
            this.txtINN0.Location = new System.Drawing.Point(90, 253);
            this.txtINN0.Name = "txtINN0";
            this.txtINN0.Size = new System.Drawing.Size(150, 20);
            this.txtINN0.TabIndex = 10;
            // 
            // chkNotAkcept0
            // 
            this.chkNotAkcept0.AutoSize = true;
            this.chkNotAkcept0.Location = new System.Drawing.Point(90, 281);
            this.chkNotAkcept0.Name = "chkNotAkcept0";
            this.chkNotAkcept0.Size = new System.Drawing.Size(149, 17);
            this.chkNotAkcept0.TabIndex = 11;
            this.chkNotAkcept0.Text = "Безакцептное списание";
            this.chkNotAkcept0.UseVisualStyleBackColor = true;
            // 
            // tabPageInstr2
            // 
            this.tabPageInstr2.Controls.Add(this.linkAccountPayment1);
            this.tabPageInstr2.Controls.Add(this.linkListInstr1);
            this.tabPageInstr2.Controls.Add(this.lblInstrTitlePayment1);
            this.tabPageInstr2.Controls.Add(this.chkCash1);
            this.tabPageInstr2.Controls.Add(this.lblBIK1);
            this.tabPageInstr2.Controls.Add(this.txtBIK1);
            this.tabPageInstr2.Controls.Add(this.lblKS1);
            this.tabPageInstr2.Controls.Add(this.ucaKS1);
            this.tabPageInstr2.Controls.Add(this.lblNameBank1);
            this.tabPageInstr2.Controls.Add(this.txtName1);
            this.tabPageInstr2.Controls.Add(this.ucaRS1);
            this.tabPageInstr2.Controls.Add(this.lblClient1);
            this.tabPageInstr2.Controls.Add(this.txtClient1);
            this.tabPageInstr2.Controls.Add(this.lblNote1);
            this.tabPageInstr2.Controls.Add(this.txtNote1);
            this.tabPageInstr2.Controls.Add(this.lblINN1);
            this.tabPageInstr2.Controls.Add(this.txtINN1);
            this.tabPageInstr2.Controls.Add(this.chkNotAkcept1);
            this.tabPageInstr2.Location = new System.Drawing.Point(4, 22);
            this.tabPageInstr2.Name = "tabPageInstr2";
            this.tabPageInstr2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInstr2.Size = new System.Drawing.Size(663, 533);
            this.tabPageInstr2.TabIndex = 1;
            this.tabPageInstr2.Text = "Продавец";
            this.tabPageInstr2.UseVisualStyleBackColor = true;
            // 
            // linkAccountPayment1
            // 
            this.linkAccountPayment1.AutoSize = true;
            this.linkAccountPayment1.Location = new System.Drawing.Point(6, 139);
            this.linkAccountPayment1.Name = "linkAccountPayment1";
            this.linkAccountPayment1.Size = new System.Drawing.Size(59, 13);
            this.linkAccountPayment1.TabIndex = 6;
            this.linkAccountPayment1.TabStop = true;
            this.linkAccountPayment1.Text = "Расч. счет";
            this.linkAccountPayment1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAccountPayment1_LinkClicked);
            // 
            // linkListInstr1
            // 
            this.linkListInstr1.AutoSize = true;
            this.linkListInstr1.Location = new System.Drawing.Point(6, 60);
            this.linkListInstr1.Name = "linkListInstr1";
            this.linkListInstr1.Size = new System.Drawing.Size(224, 13);
            this.linkListInstr1.TabIndex = 2;
            this.linkListInstr1.TabStop = true;
            this.linkListInstr1.Text = "Выбор платежной инструкции по продавцу";
            this.linkListInstr1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkListInstr1_LinkClicked);
            // 
            // lblInstrTitlePayment1
            // 
            this.lblInstrTitlePayment1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInstrTitlePayment1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblInstrTitlePayment1.Location = new System.Drawing.Point(6, 12);
            this.lblInstrTitlePayment1.Name = "lblInstrTitlePayment1";
            this.lblInstrTitlePayment1.Size = new System.Drawing.Size(651, 20);
            this.lblInstrTitlePayment1.TabIndex = 0;
            this.lblInstrTitlePayment1.Text = "Инструкция по оплате";
            this.lblInstrTitlePayment1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkCash1
            // 
            this.chkCash1.AutoSize = true;
            this.chkCash1.Location = new System.Drawing.Point(9, 40);
            this.chkCash1.Name = "chkCash1";
            this.chkCash1.Size = new System.Drawing.Size(125, 17);
            this.chkCash1.TabIndex = 1;
            this.chkCash1.Text = "Расчет через кассу";
            this.chkCash1.UseVisualStyleBackColor = true;
            this.chkCash1.CheckedChanged += new System.EventHandler(this.chkCash_Click);
            // 
            // lblBIK1
            // 
            this.lblBIK1.AutoSize = true;
            this.lblBIK1.Location = new System.Drawing.Point(6, 92);
            this.lblBIK1.Name = "lblBIK1";
            this.lblBIK1.Size = new System.Drawing.Size(29, 13);
            this.lblBIK1.TabIndex = 0;
            this.lblBIK1.Text = "БИК";
            // 
            // txtBIK1
            // 
            this.txtBIK1.Location = new System.Drawing.Point(90, 88);
            this.txtBIK1.MaxLength = 9;
            this.txtBIK1.Name = "txtBIK1";
            this.txtBIK1.Size = new System.Drawing.Size(80, 20);
            this.txtBIK1.TabIndex = 3;
            // 
            // lblKS1
            // 
            this.lblKS1.AutoSize = true;
            this.lblKS1.Location = new System.Drawing.Point(176, 92);
            this.lblKS1.Name = "lblKS1";
            this.lblKS1.Size = new System.Drawing.Size(60, 13);
            this.lblKS1.TabIndex = 0;
            this.lblKS1.Text = "Корр. счет";
            // 
            // ucaKS1
            // 
            this.ucaKS1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucaKS1.Enabled = false;
            this.ucaKS1.Location = new System.Drawing.Point(242, 88);
            this.ucaKS1.MaxLength = 24;
            this.ucaKS1.Name = "ucaKS1";
            this.ucaKS1.ReadOnly = true;
            this.ucaKS1.Size = new System.Drawing.Size(415, 20);
            this.ucaKS1.TabIndex = 4;
            this.ucaKS1.TabStop = false;
            // 
            // lblNameBank1
            // 
            this.lblNameBank1.AutoSize = true;
            this.lblNameBank1.Location = new System.Drawing.Point(6, 116);
            this.lblNameBank1.Name = "lblNameBank1";
            this.lblNameBank1.Size = new System.Drawing.Size(71, 13);
            this.lblNameBank1.TabIndex = 0;
            this.lblNameBank1.Text = "Наим. банка";
            // 
            // txtName1
            // 
            this.txtName1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName1.Location = new System.Drawing.Point(90, 112);
            this.txtName1.Name = "txtName1";
            this.txtName1.ReadOnly = true;
            this.txtName1.Size = new System.Drawing.Size(567, 20);
            this.txtName1.TabIndex = 5;
            this.txtName1.TabStop = false;
            // 
            // ucaRS1
            // 
            this.ucaRS1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucaRS1.Enabled = false;
            this.ucaRS1.Location = new System.Drawing.Point(90, 136);
            this.ucaRS1.MaxLength = 24;
            this.ucaRS1.Name = "ucaRS1";
            this.ucaRS1.ReadOnly = true;
            this.ucaRS1.Size = new System.Drawing.Size(567, 20);
            this.ucaRS1.TabIndex = 7;
            this.ucaRS1.TabStop = false;
            // 
            // lblClient1
            // 
            this.lblClient1.AutoSize = true;
            this.lblClient1.Location = new System.Drawing.Point(6, 164);
            this.lblClient1.Name = "lblClient1";
            this.lblClient1.Size = new System.Drawing.Size(43, 13);
            this.lblClient1.TabIndex = 0;
            this.lblClient1.Text = "Клиент";
            // 
            // txtClient1
            // 
            this.txtClient1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClient1.Location = new System.Drawing.Point(90, 160);
            this.txtClient1.Name = "txtClient1";
            this.txtClient1.Size = new System.Drawing.Size(567, 20);
            this.txtClient1.TabIndex = 8;
            // 
            // lblNote1
            // 
            this.lblNote1.AutoSize = true;
            this.lblNote1.Location = new System.Drawing.Point(6, 188);
            this.lblNote1.Name = "lblNote1";
            this.lblNote1.Size = new System.Drawing.Size(70, 13);
            this.lblNote1.TabIndex = 0;
            this.lblNote1.Text = "Примечание";
            // 
            // txtNote1
            // 
            this.txtNote1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote1.Location = new System.Drawing.Point(90, 184);
            this.txtNote1.Multiline = true;
            this.txtNote1.Name = "txtNote1";
            this.txtNote1.Size = new System.Drawing.Size(567, 65);
            this.txtNote1.TabIndex = 9;
            // 
            // lblINN1
            // 
            this.lblINN1.AutoSize = true;
            this.lblINN1.Location = new System.Drawing.Point(6, 257);
            this.lblINN1.Name = "lblINN1";
            this.lblINN1.Size = new System.Drawing.Size(31, 13);
            this.lblINN1.TabIndex = 0;
            this.lblINN1.Text = "ИНН";
            // 
            // txtINN1
            // 
            this.txtINN1.Location = new System.Drawing.Point(90, 253);
            this.txtINN1.Name = "txtINN1";
            this.txtINN1.Size = new System.Drawing.Size(150, 20);
            this.txtINN1.TabIndex = 10;
            // 
            // chkNotAkcept1
            // 
            this.chkNotAkcept1.AutoSize = true;
            this.chkNotAkcept1.Location = new System.Drawing.Point(90, 281);
            this.chkNotAkcept1.Name = "chkNotAkcept1";
            this.chkNotAkcept1.Size = new System.Drawing.Size(149, 17);
            this.chkNotAkcept1.TabIndex = 11;
            this.chkNotAkcept1.Text = "Безакцептное списание";
            this.chkNotAkcept1.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.ubsCtrlField);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(384, 215);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Дополнительные";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // ubsCtrlField
            // 
            this.ubsCtrlField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ubsCtrlField.Location = new System.Drawing.Point(3, 3);
            this.ubsCtrlField.Margin = new System.Windows.Forms.Padding(4);
            this.ubsCtrlField.Name = "ubsCtrlField";
            this.ubsCtrlField.ReadOnly = false;
            this.ubsCtrlField.Size = new System.Drawing.Size(378, 209);
            this.ubsCtrlField.TabIndex = 0;
            // 
            // UbsPmTradeFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 623);
            this.Margin = new System.Windows.Forms.Padding(4);
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
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tabControlOblig.ResumeLayout(false);
            this.tabPageOblig1.ResumeLayout(false);
            this.tabPageOblig1.PerformLayout();
            this.grpMetalChar.ResumeLayout(false);
            this.grpMetalChar.PerformLayout();
            this.grpMetalCharPost.ResumeLayout(false);
            this.grpMetalCharPost.PerformLayout();
            this.tabPageOblig2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabControlInstr.ResumeLayout(false);
            this.tabPageInstr1.ResumeLayout(false);
            this.tabPageInstr1.PerformLayout();
            this.tabPageInstr2.ResumeLayout(false);
            this.tabPageInstr2.PerformLayout();
            this.tabPage6.ResumeLayout(false);
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
        private System.Windows.Forms.CheckBox chkComposit;
        private System.Windows.Forms.Label lblDelivery;
        private System.Windows.Forms.ComboBox cmbKindSupplyTrade;
        private System.Windows.Forms.Label lblTradeType;
        private System.Windows.Forms.ComboBox cmbTradeType;
        private System.Windows.Forms.Label lblCurrencyPost;
        private System.Windows.Forms.ComboBox cmbCurrencyPost;
        private System.Windows.Forms.Label lblCurrencyOpl;
        private System.Windows.Forms.ComboBox cmbCurrencyPayment;
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
        private System.Windows.Forms.ListView lvwObligation;
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
        private System.Windows.Forms.Button cmdAddObligation;
        private System.Windows.Forms.Button cmdEditObligation;
        private System.Windows.Forms.Button cmdDelObligation;

        // ── Tab 3 ─────────────────────────────────────────────────────────────────
        private System.Windows.Forms.TabControl tabControlOblig;
        private System.Windows.Forms.TabPage tabPageOblig2;
        private System.Windows.Forms.Label lblObligationInfo1;
        private System.Windows.Forms.Label lblObligationInfo2;
        private System.Windows.Forms.ListView lvwObject;
        private System.Windows.Forms.ColumnHeader colObjInstr;
        private System.Windows.Forms.ColumnHeader colObjCode;
        private System.Windows.Forms.ColumnHeader colObjLigMassa;
        private System.Windows.Forms.ColumnHeader colObjProba;
        private System.Windows.Forms.ColumnHeader colObjSeries;
        private System.Windows.Forms.ColumnHeader colObjNumber;
        private System.Windows.Forms.ColumnHeader colObjId;
        private System.Windows.Forms.Button cmdAddObject;
        private System.Windows.Forms.Button cmdDelObject;

        // ── Tab 4 (Поставка) ──────────────────────────────────────────────────────
        private System.Windows.Forms.Label lblDeliveryInstructionTitle;
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
        private System.Windows.Forms.Label lblInstrTitlePayment0;
        private System.Windows.Forms.CheckBox chkCash0;
        private System.Windows.Forms.Label lblBIK0;
        private System.Windows.Forms.TextBox txtBIK0;
        private System.Windows.Forms.Label lblKS0;
        private UbsControl.UbsCtrlAccount ucaKS0;
        private System.Windows.Forms.Label lblNameBank0;
        private System.Windows.Forms.TextBox txtName0;
        private UbsControl.UbsCtrlAccount ucaRS0;
        private System.Windows.Forms.Label lblClient0;
        private System.Windows.Forms.TextBox txtClient0;
        private System.Windows.Forms.Label lblNote0;
        private System.Windows.Forms.TextBox txtNote0;
        private System.Windows.Forms.Label lblINN0;
        private System.Windows.Forms.TextBox txtINN0;
        private System.Windows.Forms.CheckBox chkNotAkcept0;
        // Продавец (index 1)
        private System.Windows.Forms.Label lblInstrTitlePayment1;
        private System.Windows.Forms.CheckBox chkCash1;
        private System.Windows.Forms.Label lblBIK1;
        private System.Windows.Forms.TextBox txtBIK1;
        private System.Windows.Forms.Label lblKS1;
        private UbsControl.UbsCtrlAccount ucaKS1;
        private System.Windows.Forms.Label lblNameBank1;
        private System.Windows.Forms.TextBox txtName1;
        private UbsControl.UbsCtrlAccount ucaRS1;
        private System.Windows.Forms.Label lblClient1;
        private System.Windows.Forms.TextBox txtClient1;
        private System.Windows.Forms.Label lblNote1;
        private System.Windows.Forms.TextBox txtNote1;
        private System.Windows.Forms.Label lblINN1;
        private System.Windows.Forms.TextBox txtINN1;
        private System.Windows.Forms.CheckBox chkNotAkcept1;

        // ── Tab 6 (Дополнительные) ────────────────────────────────────────────────
        private UbsControl.UbsCtrlFields ubsCtrlField;
        private LinkLabel linkStorage;
        private LinkLabel linkListInstr0;
        private LinkLabel linkAccountPayment0;
        private LinkLabel linkListInstr1;
        private LinkLabel linkAccountPayment1;
        private Button cmdExitObligation;
        private Button cmdApplayObligation;
        private LinkLabel linkAccountsObligation;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel5;
        private Label lblBuyer;
        private Label lblSeller;
        private TabPage tabPageOblig1;
        private Label lblTradeDirection;
        private ComboBox cmbTradeDirection;
        private Label lblObligationCurrency;
        private ComboBox cmbCurrencyObligation;
        private Label lblUnit;
        private ComboBox cmbUnit;
        private Label lblCostUnit;
        private UbsControl.UbsCtrlDecimal ucdCostUnit;
        private CheckBox chkRate;
        private UbsControl.UbsCtrlDecimal ucdRateCurOblig;
        private CheckBox chkSumInCurValue;
        private UbsControl.UbsCtrlDecimal ucdCostCurPayment;
        private GroupBox grpMetalChar;
        private Label lblDatePost;
        private UbsControl.UbsCtrlDate datePost;
        private Label lblMass;
        private UbsControl.UbsCtrlDecimal ucdMass;
        private Label lblMassGramm;
        private UbsControl.UbsCtrlDecimal ucdMassGramm;
        private GroupBox grpMetalCharPost;
        private Label lblDatePayment;
        private UbsControl.UbsCtrlDate datePayment;
        private Label lblSumObligation;
        private UbsControl.UbsCtrlDecimal ucdSumObligation;
        private Label lblSumPayment;
        private UbsControl.UbsCtrlDecimal ucdSumPayment;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel7;
        private LinkLabel linkContract2;
        private LinkLabel linkContract1;
    }
}
