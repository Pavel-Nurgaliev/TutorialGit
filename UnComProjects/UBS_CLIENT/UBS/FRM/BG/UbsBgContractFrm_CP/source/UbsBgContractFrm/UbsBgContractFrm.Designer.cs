using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    partial class UbsBgContractFrm
    {
        //private const int WM_CLOSE = 0x0010;

        //#region Переопределение WndProc

        ///// <summary>
        ///// Переопределение оконной процедуры
        ///// </summary>
        ///// <param name="m">Сообщение</param>
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnReRead = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.ubsCtrlInfo = new UbsControl.UbsCtrlInfo();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblUID = new System.Windows.Forms.Label();
            this.cmbWarrant = new System.Windows.Forms.ComboBox();
            this.lblWarrant = new System.Windows.Forms.Label();
            this.txtPreviousContract = new System.Windows.Forms.TextBox();
            this.linkPreviousContract = new System.Windows.Forms.LinkLabel();
            this.cmbNumberDiv = new System.Windows.Forms.ComboBox();
            this.lblNumberDiv = new System.Windows.Forms.Label();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.lblState = new System.Windows.Forms.Label();
            this.cmbExecutor = new System.Windows.Forms.ComboBox();
            this.lblExecutor = new System.Windows.Forms.Label();
            this.gbCover = new System.Windows.Forms.GroupBox();
            this.lblContractCover = new System.Windows.Forms.Label();
            this.cmbCurrencyCover = new System.Windows.Forms.ComboBox();
            this.lblCurrencyCover = new System.Windows.Forms.Label();
            this.ucdSumCover = new UbsControl.UbsCtrlDecimal();
            this.lblCoverSum = new System.Windows.Forms.Label();
            this.lblTypeCover = new System.Windows.Forms.Label();
            this.cmbTypeCover = new System.Windows.Forms.ComboBox();
            this.txtContractCover = new System.Windows.Forms.TextBox();
            this.gbGarant = new System.Windows.Forms.GroupBox();
            this.cmbCurrencyGarant = new System.Windows.Forms.ComboBox();
            this.lblCurrencyGarant = new System.Windows.Forms.Label();
            this.ucdSumGarant = new UbsControl.UbsCtrlDecimal();
            this.lblSumGarant = new System.Windows.Forms.Label();
            this.dateCloseGarant = new UbsControl.UbsCtrlDate();
            this.lblDateCloseGarant = new System.Windows.Forms.Label();
            this.dateEndGarant = new UbsControl.UbsCtrlDate();
            this.lblDateEndGarant = new System.Windows.Forms.Label();
            this.dateBeginGarant = new UbsControl.UbsCtrlDate();
            this.lblDateBeginGarant = new System.Windows.Forms.Label();
            this.dateOpenGarant = new UbsControl.UbsCtrlDate();
            this.lblDateOpenGarant = new System.Windows.Forms.Label();
            this.lblNumberGarant = new System.Windows.Forms.Label();
            this.txtNumberGarant = new System.Windows.Forms.TextBox();
            this.gbClients = new System.Windows.Forms.GroupBox();
            this.btnManualBenificiar = new System.Windows.Forms.Button();
            this.txtGarant = new System.Windows.Forms.TextBox();
            this.linkGarant = new System.Windows.Forms.LinkLabel();
            this.txtBeneficiar = new System.Windows.Forms.TextBox();
            this.linkBeneficiar = new System.Windows.Forms.LinkLabel();
            this.datePrincipal = new UbsControl.UbsCtrlDate();
            this.lblDatePrincipal = new System.Windows.Forms.Label();
            this.txtPrincipal = new System.Windows.Forms.TextBox();
            this.linkPrincipal = new System.Windows.Forms.LinkLabel();
            this.transAmount = new UbsControl.UbsCtrlDecimal();
            this.label6 = new System.Windows.Forms.Label();
            this.paidAmount = new UbsControl.UbsCtrlDecimal();
            this.costAmount = new UbsControl.UbsCtrlDecimal();
            this.label4 = new System.Windows.Forms.Label();
            this.dateAdjustment = new UbsControl.UbsCtrlDate();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateReward = new UbsControl.UbsCtrlDate();
            this.lblDateReward = new System.Windows.Forms.Label();
            this.btnAgentDel = new System.Windows.Forms.Button();
            this.linkAgent = new System.Windows.Forms.LinkLabel();
            this.dateAgent = new UbsControl.UbsCtrlDate();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNumAgent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAgent = new System.Windows.Forms.TextBox();
            this.cmbKindGarant = new System.Windows.Forms.ComboBox();
            this.lblkind = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.linkModel = new System.Windows.Forms.LinkLabel();
            this.btnFrameContractDel = new System.Windows.Forms.Button();
            this.txtFrameContract = new System.Windows.Forms.TextBox();
            this.linkFrameContract = new System.Windows.Forms.LinkLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cmbTypeValidationRisk = new System.Windows.Forms.ComboBox();
            this.lblTypeValidationRisk = new System.Windows.Forms.Label();
            this.ucdRateReservation = new UbsControl.UbsCtrlDecimal();
            this.lblRateReservation = new System.Windows.Forms.Label();
            this.cmbQualityCategory = new System.Windows.Forms.ComboBox();
            this.lblQualityCategory = new System.Windows.Forms.Label();
            this.cmbPortfolio = new System.Windows.Forms.ComboBox();
            this.lblPortfolio = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lvwAccounts = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsAccounts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.gbPayRewardGuarant = new System.Windows.Forms.GroupBox();
            this.btnPeriodPayFeeBonus = new System.Windows.Forms.Button();
            this.dateNextPayFeeBonus = new UbsControl.UbsCtrlDate();
            this.lblDateNextPayFeeBonus = new System.Windows.Forms.Label();
            this.cmbTypePayFeeBonus = new System.Windows.Forms.ComboBox();
            this.lblTypePayFeeBonus = new System.Windows.Forms.Label();
            this.cmbCurrencyRewardGuarant = new System.Windows.Forms.ComboBox();
            this.lblCurrencyRewardGuarant = new System.Windows.Forms.Label();
            this.lblOrderPayFeeBonus = new System.Windows.Forms.Label();
            this.cmbOrderPayFeeBonus = new System.Windows.Forms.ComboBox();
            this.gbPayFeeGuarant = new System.Windows.Forms.GroupBox();
            this.cmbTypePayFeeGuarant = new System.Windows.Forms.ComboBox();
            this.lblTypePayFeeGuarant = new System.Windows.Forms.Label();
            this.btnPeriodPayFeeGuarant = new System.Windows.Forms.Button();
            this.dateNextPayFeeGuarant = new UbsControl.UbsCtrlDate();
            this.lblDateNextPayFeeGuarant = new System.Windows.Forms.Label();
            this.lblOrderPayFeeGuarant = new System.Windows.Forms.Label();
            this.cmbOrderPayFeeGuarant = new System.Windows.Forms.ComboBox();
            this.gbOrderPayFee = new System.Windows.Forms.GroupBox();
            this.btnPeriodPayFee = new System.Windows.Forms.Button();
            this.dateNextPayFee = new UbsControl.UbsCtrlDate();
            this.lblDateNextPayFee = new System.Windows.Forms.Label();
            this.cmbTypePayFee = new System.Windows.Forms.ComboBox();
            this.lblTypePayFee = new System.Windows.Forms.Label();
            this.cmbCurrencyPayFee = new System.Windows.Forms.ComboBox();
            this.lblCurrencyPayFee = new System.Windows.Forms.Label();
            this.lblOrderPayFee = new System.Windows.Forms.Label();
            this.cmbOrderPayFee = new System.Windows.Forms.ComboBox();
            this.btnDelRate = new System.Windows.Forms.Button();
            this.btnEditRate = new System.Windows.Forms.Button();
            this.btnAddRate = new System.Windows.Forms.Button();
            this.trvRates = new System.Windows.Forms.TreeView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btnListGuarantOperDog = new System.Windows.Forms.Button();
            this.btnInclude = new System.Windows.Forms.Button();
            this.btnEditGuarant = new System.Windows.Forms.Button();
            this.btnAddGuarant = new System.Windows.Forms.Button();
            this.lvwGuarant = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.ubsCtrlFields = new UbsControl.UbsCtrlFields();
            this.panelMain.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gbCover.SuspendLayout();
            this.gbGarant.SuspendLayout();
            this.gbClients.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.cmsAccounts.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.gbPayRewardGuarant.SuspendLayout();
            this.gbPayFeeGuarant.SuspendLayout();
            this.gbOrderPayFee.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tabControl);
            this.panelMain.Controls.Add(this.tableLayoutPanel);
            this.panelMain.Size = new System.Drawing.Size(633, 721);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.CausesValidation = false;
            this.tableLayoutPanel.ColumnCount = 4;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel.Controls.Add(this.btnReRead, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.btnSave, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.btnExit, 3, 0);
            this.tableLayoutPanel.Controls.Add(this.ubsCtrlInfo, 1, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 689);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(633, 32);
            this.tableLayoutPanel.TabIndex = 100;
            // 
            // btnReRead
            // 
            this.btnReRead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReRead.Location = new System.Drawing.Point(3, 3);
            this.btnReRead.Name = "btnReRead";
            this.btnReRead.Size = new System.Drawing.Size(82, 26);
            this.btnReRead.TabIndex = 101;
            this.btnReRead.Text = "Перечитать";
            this.btnReRead.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(460, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 26);
            this.btnSave.TabIndex = 104;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.CausesValidation = false;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(548, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 26);
            this.btnExit.TabIndex = 105;
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
            this.ubsCtrlInfo.Location = new System.Drawing.Point(91, 19);
            this.ubsCtrlInfo.Name = "ubsCtrlInfo";
            this.ubsCtrlInfo.Size = new System.Drawing.Size(363, 13);
            this.ubsCtrlInfo.TabIndex = 102;
            this.ubsCtrlInfo.Text = "ubsCtrlInfo";
            this.ubsCtrlInfo.Visible = false;
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
            this.tabControl.Size = new System.Drawing.Size(633, 689);
            this.tabControl.TabIndex = 101;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblUID);
            this.tabPage1.Controls.Add(this.cmbWarrant);
            this.tabPage1.Controls.Add(this.lblWarrant);
            this.tabPage1.Controls.Add(this.txtPreviousContract);
            this.tabPage1.Controls.Add(this.linkPreviousContract);
            this.tabPage1.Controls.Add(this.cmbNumberDiv);
            this.tabPage1.Controls.Add(this.lblNumberDiv);
            this.tabPage1.Controls.Add(this.cmbState);
            this.tabPage1.Controls.Add(this.lblState);
            this.tabPage1.Controls.Add(this.cmbExecutor);
            this.tabPage1.Controls.Add(this.lblExecutor);
            this.tabPage1.Controls.Add(this.gbCover);
            this.tabPage1.Controls.Add(this.gbGarant);
            this.tabPage1.Controls.Add(this.gbClients);
            this.tabPage1.Controls.Add(this.transAmount);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.paidAmount);
            this.tabPage1.Controls.Add(this.costAmount);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.dateAdjustment);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.dateReward);
            this.tabPage1.Controls.Add(this.lblDateReward);
            this.tabPage1.Controls.Add(this.btnAgentDel);
            this.tabPage1.Controls.Add(this.linkAgent);
            this.tabPage1.Controls.Add(this.dateAgent);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtNumAgent);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtAgent);
            this.tabPage1.Controls.Add(this.cmbKindGarant);
            this.tabPage1.Controls.Add(this.lblkind);
            this.tabPage1.Controls.Add(this.txtModel);
            this.tabPage1.Controls.Add(this.linkModel);
            this.tabPage1.Controls.Add(this.btnFrameContractDel);
            this.tabPage1.Controls.Add(this.txtFrameContract);
            this.tabPage1.Controls.Add(this.linkFrameContract);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(625, 663);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Основные";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblUID
            // 
            this.lblUID.AutoSize = true;
            this.lblUID.Location = new System.Drawing.Point(125, 639);
            this.lblUID.Name = "lblUID";
            this.lblUID.Size = new System.Drawing.Size(35, 13);
            this.lblUID.TabIndex = 36;
            this.lblUID.Text = "УИД:";
            this.lblUID.Visible = false;
            // 
            // cmbWarrant
            // 
            this.cmbWarrant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWarrant.FormattingEnabled = true;
            this.cmbWarrant.Location = new System.Drawing.Point(165, 612);
            this.cmbWarrant.Name = "cmbWarrant";
            this.cmbWarrant.Size = new System.Drawing.Size(444, 21);
            this.cmbWarrant.TabIndex = 35;
            // 
            // lblWarrant
            // 
            this.lblWarrant.AutoSize = true;
            this.lblWarrant.Location = new System.Drawing.Point(6, 615);
            this.lblWarrant.Name = "lblWarrant";
            this.lblWarrant.Size = new System.Drawing.Size(154, 13);
            this.lblWarrant.TabIndex = 34;
            this.lblWarrant.Text = "Уполномоченное лицо банка";
            // 
            // txtPreviousContract
            // 
            this.txtPreviousContract.Location = new System.Drawing.Point(165, 586);
            this.txtPreviousContract.Name = "txtPreviousContract";
            this.txtPreviousContract.ReadOnly = true;
            this.txtPreviousContract.Size = new System.Drawing.Size(444, 20);
            this.txtPreviousContract.TabIndex = 33;
            // 
            // linkPreviousContract
            // 
            this.linkPreviousContract.AutoSize = true;
            this.linkPreviousContract.Location = new System.Drawing.Point(14, 589);
            this.linkPreviousContract.Name = "linkPreviousContract";
            this.linkPreviousContract.Size = new System.Drawing.Size(146, 13);
            this.linkPreviousContract.TabIndex = 32;
            this.linkPreviousContract.TabStop = true;
            this.linkPreviousContract.Text = "Прежний договор гарантии";
            this.linkPreviousContract.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkPreviousContract_LinkClicked);
            // 
            // cmbNumberDiv
            // 
            this.cmbNumberDiv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNumberDiv.FormattingEnabled = true;
            this.cmbNumberDiv.Location = new System.Drawing.Point(165, 559);
            this.cmbNumberDiv.Name = "cmbNumberDiv";
            this.cmbNumberDiv.Size = new System.Drawing.Size(444, 21);
            this.cmbNumberDiv.TabIndex = 31;
            // 
            // lblNumberDiv
            // 
            this.lblNumberDiv.AutoSize = true;
            this.lblNumberDiv.Location = new System.Drawing.Point(98, 562);
            this.lblNumberDiv.Name = "lblNumberDiv";
            this.lblNumberDiv.Size = new System.Drawing.Size(62, 13);
            this.lblNumberDiv.TabIndex = 30;
            this.lblNumberDiv.Text = "Отделение";
            // 
            // cmbState
            // 
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(165, 532);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(444, 21);
            this.cmbState.TabIndex = 29;
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(98, 535);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(61, 13);
            this.lblState.TabIndex = 28;
            this.lblState.Text = "Состояние";
            // 
            // cmbExecutor
            // 
            this.cmbExecutor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExecutor.FormattingEnabled = true;
            this.cmbExecutor.Location = new System.Drawing.Point(165, 505);
            this.cmbExecutor.Name = "cmbExecutor";
            this.cmbExecutor.Size = new System.Drawing.Size(444, 21);
            this.cmbExecutor.TabIndex = 27;
            // 
            // lblExecutor
            // 
            this.lblExecutor.AutoSize = true;
            this.lblExecutor.Location = new System.Drawing.Point(6, 508);
            this.lblExecutor.Name = "lblExecutor";
            this.lblExecutor.Size = new System.Drawing.Size(154, 13);
            this.lblExecutor.TabIndex = 26;
            this.lblExecutor.Text = "Ответственный исполнитель";
            // 
            // gbCover
            // 
            this.gbCover.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCover.Controls.Add(this.lblContractCover);
            this.gbCover.Controls.Add(this.cmbCurrencyCover);
            this.gbCover.Controls.Add(this.lblCurrencyCover);
            this.gbCover.Controls.Add(this.ucdSumCover);
            this.gbCover.Controls.Add(this.lblCoverSum);
            this.gbCover.Controls.Add(this.lblTypeCover);
            this.gbCover.Controls.Add(this.cmbTypeCover);
            this.gbCover.Controls.Add(this.txtContractCover);
            this.gbCover.Location = new System.Drawing.Point(10, 403);
            this.gbCover.Name = "gbCover";
            this.gbCover.Size = new System.Drawing.Size(606, 95);
            this.gbCover.TabIndex = 25;
            this.gbCover.TabStop = false;
            this.gbCover.Text = "Покрытие";
            // 
            // lblContractCover
            // 
            this.lblContractCover.AutoSize = true;
            this.lblContractCover.Location = new System.Drawing.Point(97, 68);
            this.lblContractCover.Name = "lblContractCover";
            this.lblContractCover.Size = new System.Drawing.Size(51, 13);
            this.lblContractCover.TabIndex = 7;
            this.lblContractCover.Text = "Договор";
            // 
            // cmbCurrencyCover
            // 
            this.cmbCurrencyCover.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrencyCover.FormattingEnabled = true;
            this.cmbCurrencyCover.Location = new System.Drawing.Point(439, 37);
            this.cmbCurrencyCover.Name = "cmbCurrencyCover";
            this.cmbCurrencyCover.Size = new System.Drawing.Size(156, 21);
            this.cmbCurrencyCover.TabIndex = 6;
            // 
            // lblCurrencyCover
            // 
            this.lblCurrencyCover.AutoSize = true;
            this.lblCurrencyCover.Location = new System.Drawing.Point(391, 40);
            this.lblCurrencyCover.Name = "lblCurrencyCover";
            this.lblCurrencyCover.Size = new System.Drawing.Size(45, 13);
            this.lblCurrencyCover.TabIndex = 5;
            this.lblCurrencyCover.Text = "Валюта";
            // 
            // ucdSumCover
            // 
            this.ucdSumCover.Location = new System.Drawing.Point(154, 37);
            this.ucdSumCover.Name = "ucdSumCover";
            this.ucdSumCover.Size = new System.Drawing.Size(234, 20);
            this.ucdSumCover.TabIndex = 4;
            this.ucdSumCover.Text = "0";
            // 
            // lblCoverSum
            // 
            this.lblCoverSum.AutoSize = true;
            this.lblCoverSum.Location = new System.Drawing.Point(107, 40);
            this.lblCoverSum.Name = "lblCoverSum";
            this.lblCoverSum.Size = new System.Drawing.Size(41, 13);
            this.lblCoverSum.TabIndex = 3;
            this.lblCoverSum.Text = "Сумма";
            // 
            // lblTypeCover
            // 
            this.lblTypeCover.AutoSize = true;
            this.lblTypeCover.Location = new System.Drawing.Point(122, 16);
            this.lblTypeCover.Name = "lblTypeCover";
            this.lblTypeCover.Size = new System.Drawing.Size(26, 13);
            this.lblTypeCover.TabIndex = 1;
            this.lblTypeCover.Text = "Тип";
            // 
            // cmbTypeCover
            // 
            this.cmbTypeCover.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeCover.FormattingEnabled = true;
            this.cmbTypeCover.Location = new System.Drawing.Point(155, 13);
            this.cmbTypeCover.Name = "cmbTypeCover";
            this.cmbTypeCover.Size = new System.Drawing.Size(440, 21);
            this.cmbTypeCover.TabIndex = 2;
            // 
            // txtContractCover
            // 
            this.txtContractCover.Enabled = false;
            this.txtContractCover.Location = new System.Drawing.Point(155, 65);
            this.txtContractCover.Name = "txtContractCover";
            this.txtContractCover.Size = new System.Drawing.Size(439, 20);
            this.txtContractCover.TabIndex = 8;
            // 
            // gbGarant
            // 
            this.gbGarant.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGarant.Controls.Add(this.cmbCurrencyGarant);
            this.gbGarant.Controls.Add(this.lblCurrencyGarant);
            this.gbGarant.Controls.Add(this.ucdSumGarant);
            this.gbGarant.Controls.Add(this.lblSumGarant);
            this.gbGarant.Controls.Add(this.dateCloseGarant);
            this.gbGarant.Controls.Add(this.lblDateCloseGarant);
            this.gbGarant.Controls.Add(this.dateEndGarant);
            this.gbGarant.Controls.Add(this.lblDateEndGarant);
            this.gbGarant.Controls.Add(this.dateBeginGarant);
            this.gbGarant.Controls.Add(this.lblDateBeginGarant);
            this.gbGarant.Controls.Add(this.dateOpenGarant);
            this.gbGarant.Controls.Add(this.lblDateOpenGarant);
            this.gbGarant.Controls.Add(this.lblNumberGarant);
            this.gbGarant.Controls.Add(this.txtNumberGarant);
            this.gbGarant.Location = new System.Drawing.Point(10, 301);
            this.gbGarant.Name = "gbGarant";
            this.gbGarant.Size = new System.Drawing.Size(606, 96);
            this.gbGarant.TabIndex = 24;
            this.gbGarant.TabStop = false;
            this.gbGarant.Text = "Гарантия";
            // 
            // cmbCurrencyGarant
            // 
            this.cmbCurrencyGarant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrencyGarant.FormattingEnabled = true;
            this.cmbCurrencyGarant.Location = new System.Drawing.Point(439, 65);
            this.cmbCurrencyGarant.Name = "cmbCurrencyGarant";
            this.cmbCurrencyGarant.Size = new System.Drawing.Size(156, 21);
            this.cmbCurrencyGarant.TabIndex = 14;
            // 
            // lblCurrencyGarant
            // 
            this.lblCurrencyGarant.AutoSize = true;
            this.lblCurrencyGarant.Location = new System.Drawing.Point(391, 68);
            this.lblCurrencyGarant.Name = "lblCurrencyGarant";
            this.lblCurrencyGarant.Size = new System.Drawing.Size(45, 13);
            this.lblCurrencyGarant.TabIndex = 13;
            this.lblCurrencyGarant.Text = "Валюта";
            // 
            // ucdSumGarant
            // 
            this.ucdSumGarant.Location = new System.Drawing.Point(156, 65);
            this.ucdSumGarant.Name = "ucdSumGarant";
            this.ucdSumGarant.Size = new System.Drawing.Size(232, 20);
            this.ucdSumGarant.TabIndex = 12;
            this.ucdSumGarant.Text = "0";
            // 
            // lblSumGarant
            // 
            this.lblSumGarant.AutoSize = true;
            this.lblSumGarant.Location = new System.Drawing.Point(109, 68);
            this.lblSumGarant.Name = "lblSumGarant";
            this.lblSumGarant.Size = new System.Drawing.Size(41, 13);
            this.lblSumGarant.TabIndex = 11;
            this.lblSumGarant.Text = "Сумма";
            // 
            // dateCloseGarant
            // 
            this.dateCloseGarant.Location = new System.Drawing.Point(494, 39);
            this.dateCloseGarant.MaxLength = 10;
            this.dateCloseGarant.Name = "dateCloseGarant";
            this.dateCloseGarant.ReadOnly = true;
            this.dateCloseGarant.Size = new System.Drawing.Size(100, 20);
            this.dateCloseGarant.TabIndex = 10;
            this.dateCloseGarant.Text = "  .  .    ";
            this.dateCloseGarant.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDateCloseGarant
            // 
            this.lblDateCloseGarant.AutoSize = true;
            this.lblDateCloseGarant.Location = new System.Drawing.Point(433, 42);
            this.lblDateCloseGarant.Name = "lblDateCloseGarant";
            this.lblDateCloseGarant.Size = new System.Drawing.Size(56, 13);
            this.lblDateCloseGarant.TabIndex = 9;
            this.lblDateCloseGarant.Text = "закрытия";
            // 
            // dateEndGarant
            // 
            this.dateEndGarant.Location = new System.Drawing.Point(327, 39);
            this.dateEndGarant.MaxLength = 10;
            this.dateEndGarant.Name = "dateEndGarant";
            this.dateEndGarant.Size = new System.Drawing.Size(100, 20);
            this.dateEndGarant.TabIndex = 8;
            this.dateEndGarant.Text = "  .  .    ";
            this.dateEndGarant.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDateEndGarant
            // 
            this.lblDateEndGarant.AutoSize = true;
            this.lblDateEndGarant.Location = new System.Drawing.Point(261, 42);
            this.lblDateEndGarant.Name = "lblDateEndGarant";
            this.lblDateEndGarant.Size = new System.Drawing.Size(60, 13);
            this.lblDateEndGarant.TabIndex = 7;
            this.lblDateEndGarant.Text = "окончания";
            // 
            // dateBeginGarant
            // 
            this.dateBeginGarant.Location = new System.Drawing.Point(156, 39);
            this.dateBeginGarant.MaxLength = 10;
            this.dateBeginGarant.Name = "dateBeginGarant";
            this.dateBeginGarant.Size = new System.Drawing.Size(100, 20);
            this.dateBeginGarant.TabIndex = 6;
            this.dateBeginGarant.Text = "  .  .    ";
            this.dateBeginGarant.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDateBeginGarant
            // 
            this.lblDateBeginGarant.AutoSize = true;
            this.lblDateBeginGarant.Location = new System.Drawing.Point(79, 42);
            this.lblDateBeginGarant.Name = "lblDateBeginGarant";
            this.lblDateBeginGarant.Size = new System.Drawing.Size(71, 13);
            this.lblDateBeginGarant.TabIndex = 5;
            this.lblDateBeginGarant.Text = "Дата начала";
            // 
            // dateOpenGarant
            // 
            this.dateOpenGarant.Location = new System.Drawing.Point(495, 13);
            this.dateOpenGarant.MaxLength = 10;
            this.dateOpenGarant.Name = "dateOpenGarant";
            this.dateOpenGarant.Size = new System.Drawing.Size(100, 20);
            this.dateOpenGarant.TabIndex = 4;
            this.dateOpenGarant.Text = "  .  .    ";
            this.dateOpenGarant.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDateOpenGarant
            // 
            this.lblDateOpenGarant.AutoSize = true;
            this.lblDateOpenGarant.Location = new System.Drawing.Point(392, 16);
            this.lblDateOpenGarant.Name = "lblDateOpenGarant";
            this.lblDateOpenGarant.Size = new System.Drawing.Size(97, 13);
            this.lblDateOpenGarant.TabIndex = 3;
            this.lblDateOpenGarant.Text = "Дата заключения";
            // 
            // lblNumberGarant
            // 
            this.lblNumberGarant.AutoSize = true;
            this.lblNumberGarant.Location = new System.Drawing.Point(109, 16);
            this.lblNumberGarant.Name = "lblNumberGarant";
            this.lblNumberGarant.Size = new System.Drawing.Size(41, 13);
            this.lblNumberGarant.TabIndex = 1;
            this.lblNumberGarant.Text = "Номер";
            // 
            // txtNumberGarant
            // 
            this.txtNumberGarant.Location = new System.Drawing.Point(156, 13);
            this.txtNumberGarant.Name = "txtNumberGarant";
            this.txtNumberGarant.Size = new System.Drawing.Size(233, 20);
            this.txtNumberGarant.TabIndex = 2;
            // 
            // gbClients
            // 
            this.gbClients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbClients.Controls.Add(this.btnManualBenificiar);
            this.gbClients.Controls.Add(this.txtGarant);
            this.gbClients.Controls.Add(this.linkGarant);
            this.gbClients.Controls.Add(this.txtBeneficiar);
            this.gbClients.Controls.Add(this.linkBeneficiar);
            this.gbClients.Controls.Add(this.datePrincipal);
            this.gbClients.Controls.Add(this.lblDatePrincipal);
            this.gbClients.Controls.Add(this.txtPrincipal);
            this.gbClients.Controls.Add(this.linkPrincipal);
            this.gbClients.Location = new System.Drawing.Point(11, 176);
            this.gbClients.Name = "gbClients";
            this.gbClients.Size = new System.Drawing.Size(606, 119);
            this.gbClients.TabIndex = 23;
            this.gbClients.TabStop = false;
            this.gbClients.Text = "Клиенты";
            // 
            // btnManualBenificiar
            // 
            this.btnManualBenificiar.Location = new System.Drawing.Point(479, 38);
            this.btnManualBenificiar.Name = "btnManualBenificiar";
            this.btnManualBenificiar.Size = new System.Drawing.Size(114, 21);
            this.btnManualBenificiar.TabIndex = 5;
            this.btnManualBenificiar.Text = "Ввести реквизиты";
            this.btnManualBenificiar.UseVisualStyleBackColor = true;
            this.btnManualBenificiar.Click += new System.EventHandler(this.btnManualBenificiar_Click);
            // 
            // txtGarant
            // 
            this.txtGarant.Location = new System.Drawing.Point(155, 65);
            this.txtGarant.Name = "txtGarant";
            this.txtGarant.ReadOnly = true;
            this.txtGarant.Size = new System.Drawing.Size(438, 20);
            this.txtGarant.TabIndex = 7;
            // 
            // linkGarant
            // 
            this.linkGarant.AutoSize = true;
            this.linkGarant.Location = new System.Drawing.Point(107, 68);
            this.linkGarant.Name = "linkGarant";
            this.linkGarant.Size = new System.Drawing.Size(42, 13);
            this.linkGarant.TabIndex = 6;
            this.linkGarant.TabStop = true;
            this.linkGarant.Text = "Гарант";
            this.linkGarant.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGarant_LinkClicked);
            // 
            // txtBeneficiar
            // 
            this.txtBeneficiar.Location = new System.Drawing.Point(155, 39);
            this.txtBeneficiar.Name = "txtBeneficiar";
            this.txtBeneficiar.ReadOnly = true;
            this.txtBeneficiar.Size = new System.Drawing.Size(318, 20);
            this.txtBeneficiar.TabIndex = 4;
            // 
            // linkBeneficiar
            // 
            this.linkBeneficiar.AutoSize = true;
            this.linkBeneficiar.Location = new System.Drawing.Point(79, 42);
            this.linkBeneficiar.Name = "linkBeneficiar";
            this.linkBeneficiar.Size = new System.Drawing.Size(70, 13);
            this.linkBeneficiar.TabIndex = 3;
            this.linkBeneficiar.TabStop = true;
            this.linkBeneficiar.Text = "Бенефициар";
            this.linkBeneficiar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkBeneficiar_LinkClicked);
            // 
            // datePrincipal
            // 
            this.datePrincipal.Location = new System.Drawing.Point(155, 91);
            this.datePrincipal.MaxLength = 10;
            this.datePrincipal.Name = "datePrincipal";
            this.datePrincipal.Size = new System.Drawing.Size(100, 20);
            this.datePrincipal.TabIndex = 9;
            this.datePrincipal.Text = "  .  .    ";
            this.datePrincipal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDatePrincipal
            // 
            this.lblDatePrincipal.AutoSize = true;
            this.lblDatePrincipal.Location = new System.Drawing.Point(6, 94);
            this.lblDatePrincipal.Name = "lblDatePrincipal";
            this.lblDatePrincipal.Size = new System.Drawing.Size(143, 13);
            this.lblDatePrincipal.TabIndex = 8;
            this.lblDatePrincipal.Text = "Дата возн. обяз. Принцип.";
            // 
            // txtPrincipal
            // 
            this.txtPrincipal.Location = new System.Drawing.Point(155, 13);
            this.txtPrincipal.Name = "txtPrincipal";
            this.txtPrincipal.ReadOnly = true;
            this.txtPrincipal.Size = new System.Drawing.Size(438, 20);
            this.txtPrincipal.TabIndex = 2;
            // 
            // linkPrincipal
            // 
            this.linkPrincipal.AutoSize = true;
            this.linkPrincipal.Location = new System.Drawing.Point(86, 16);
            this.linkPrincipal.Name = "linkPrincipal";
            this.linkPrincipal.Size = new System.Drawing.Size(63, 13);
            this.linkPrincipal.TabIndex = 1;
            this.linkPrincipal.TabStop = true;
            this.linkPrincipal.Text = "Принципал";
            this.linkPrincipal.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkPrincipal_LinkClicked);
            // 
            // transAmount
            // 
            this.transAmount.Location = new System.Drawing.Point(510, 147);
            this.transAmount.Name = "transAmount";
            this.transAmount.ReadOnly = true;
            this.transAmount.Size = new System.Drawing.Size(106, 20);
            this.transAmount.TabIndex = 22;
            this.transAmount.Text = "0";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(417, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 28);
            this.label6.TabIndex = 22;
            this.label6.Text = "Перечисленная сумма";
            // 
            // paidAmount
            // 
            this.paidAmount.Location = new System.Drawing.Point(311, 147);
            this.paidAmount.Name = "paidAmount";
            this.paidAmount.ReadOnly = true;
            this.paidAmount.Size = new System.Drawing.Size(100, 20);
            this.paidAmount.TabIndex = 21;
            this.paidAmount.Text = "0";
            // 
            // costAmount
            // 
            this.costAmount.Location = new System.Drawing.Point(311, 117);
            this.costAmount.Name = "costAmount";
            this.costAmount.ReadOnly = true;
            this.costAmount.Size = new System.Drawing.Size(100, 20);
            this.costAmount.TabIndex = 17;
            this.costAmount.Text = "0";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(232, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 28);
            this.label4.TabIndex = 20;
            this.label4.Text = "Уплаченная сумма";
            // 
            // dateAdjustment
            // 
            this.dateAdjustment.Location = new System.Drawing.Point(127, 147);
            this.dateAdjustment.MaxLength = 10;
            this.dateAdjustment.Name = "dateAdjustment";
            this.dateAdjustment.ReadOnly = true;
            this.dateAdjustment.Size = new System.Drawing.Size(100, 20);
            this.dateAdjustment.TabIndex = 19;
            this.dateAdjustment.Text = "  .  .    ";
            this.dateAdjustment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 28);
            this.label5.TabIndex = 18;
            this.label5.Text = "Дата последнего ДС по корректировке";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(232, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 28);
            this.label1.TabIndex = 16;
            this.label1.Text = "Сумма затрат";
            // 
            // dateReward
            // 
            this.dateReward.Location = new System.Drawing.Point(127, 117);
            this.dateReward.MaxLength = 10;
            this.dateReward.Name = "dateReward";
            this.dateReward.ReadOnly = true;
            this.dateReward.Size = new System.Drawing.Size(100, 20);
            this.dateReward.TabIndex = 15;
            this.dateReward.Text = "  .  .    ";
            this.dateReward.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDateReward
            // 
            this.lblDateReward.Location = new System.Drawing.Point(8, 114);
            this.lblDateReward.Name = "lblDateReward";
            this.lblDateReward.Size = new System.Drawing.Size(93, 28);
            this.lblDateReward.TabIndex = 14;
            this.lblDateReward.Text = "Дата уплаты вознаграждения";
            // 
            // btnAgentDel
            // 
            this.btnAgentDel.Location = new System.Drawing.Point(596, 85);
            this.btnAgentDel.Name = "btnAgentDel";
            this.btnAgentDel.Size = new System.Drawing.Size(20, 20);
            this.btnAgentDel.TabIndex = 13;
            this.btnAgentDel.Text = "X";
            this.btnAgentDel.UseVisualStyleBackColor = true;
            this.btnAgentDel.Click += new System.EventHandler(this.btnAgentDel_Click);
            // 
            // linkAgent
            // 
            this.linkAgent.AutoSize = true;
            this.linkAgent.Location = new System.Drawing.Point(8, 88);
            this.linkAgent.Name = "linkAgent";
            this.linkAgent.Size = new System.Drawing.Size(36, 13);
            this.linkAgent.TabIndex = 7;
            this.linkAgent.TabStop = true;
            this.linkAgent.Text = "Агент";
            this.linkAgent.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkAgent_LinkClicked);
            // 
            // dateAgent
            // 
            this.dateAgent.Location = new System.Drawing.Point(490, 85);
            this.dateAgent.MaxLength = 10;
            this.dateAgent.Name = "dateAgent";
            this.dateAgent.ReadOnly = true;
            this.dateAgent.Size = new System.Drawing.Size(100, 20);
            this.dateAgent.TabIndex = 12;
            this.dateAgent.Text = "  .  .    ";
            this.dateAgent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(451, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Дата";
            // 
            // txtNumAgent
            // 
            this.txtNumAgent.Location = new System.Drawing.Point(345, 85);
            this.txtNumAgent.Name = "txtNumAgent";
            this.txtNumAgent.Size = new System.Drawing.Size(100, 20);
            this.txtNumAgent.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(298, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Номер";
            // 
            // txtAgent
            // 
            this.txtAgent.Location = new System.Drawing.Point(117, 85);
            this.txtAgent.Name = "txtAgent";
            this.txtAgent.ReadOnly = true;
            this.txtAgent.Size = new System.Drawing.Size(175, 20);
            this.txtAgent.TabIndex = 8;
            // 
            // cmbKindGarant
            // 
            this.cmbKindGarant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKindGarant.FormattingEnabled = true;
            this.cmbKindGarant.Location = new System.Drawing.Point(117, 58);
            this.cmbKindGarant.Name = "cmbKindGarant";
            this.cmbKindGarant.Size = new System.Drawing.Size(473, 21);
            this.cmbKindGarant.TabIndex = 6;
            // 
            // lblkind
            // 
            this.lblkind.AutoSize = true;
            this.lblkind.Location = new System.Drawing.Point(7, 61);
            this.lblkind.Name = "lblkind";
            this.lblkind.Size = new System.Drawing.Size(75, 13);
            this.lblkind.TabIndex = 5;
            this.lblkind.Text = "Вид гарантии";
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(117, 32);
            this.txtModel.Name = "txtModel";
            this.txtModel.ReadOnly = true;
            this.txtModel.Size = new System.Drawing.Size(473, 20);
            this.txtModel.TabIndex = 4;
            // 
            // linkModel
            // 
            this.linkModel.AutoSize = true;
            this.linkModel.Location = new System.Drawing.Point(7, 35);
            this.linkModel.Name = "linkModel";
            this.linkModel.Size = new System.Drawing.Size(94, 13);
            this.linkModel.TabIndex = 3;
            this.linkModel.TabStop = true;
            this.linkModel.Text = "Типовой договор";
            this.linkModel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkModel_LinkClicked);
            // 
            // btnFrameContractDel
            // 
            this.btnFrameContractDel.Location = new System.Drawing.Point(596, 6);
            this.btnFrameContractDel.Name = "btnFrameContractDel";
            this.btnFrameContractDel.Size = new System.Drawing.Size(20, 20);
            this.btnFrameContractDel.TabIndex = 2;
            this.btnFrameContractDel.Text = "X";
            this.btnFrameContractDel.UseVisualStyleBackColor = true;
            this.btnFrameContractDel.Click += new System.EventHandler(this.btnFrameContractDel_Click);
            // 
            // txtFrameContract
            // 
            this.txtFrameContract.Location = new System.Drawing.Point(117, 6);
            this.txtFrameContract.Name = "txtFrameContract";
            this.txtFrameContract.ReadOnly = true;
            this.txtFrameContract.Size = new System.Drawing.Size(473, 20);
            this.txtFrameContract.TabIndex = 1;
            // 
            // linkFrameContract
            // 
            this.linkFrameContract.AutoSize = true;
            this.linkFrameContract.Location = new System.Drawing.Point(7, 9);
            this.linkFrameContract.Name = "linkFrameContract";
            this.linkFrameContract.Size = new System.Drawing.Size(103, 13);
            this.linkFrameContract.TabIndex = 0;
            this.linkFrameContract.TabStop = true;
            this.linkFrameContract.Text = "Рамочный договор";
            this.linkFrameContract.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkFrameContract_LinkClicked);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cmbTypeValidationRisk);
            this.tabPage2.Controls.Add(this.lblTypeValidationRisk);
            this.tabPage2.Controls.Add(this.ucdRateReservation);
            this.tabPage2.Controls.Add(this.lblRateReservation);
            this.tabPage2.Controls.Add(this.cmbQualityCategory);
            this.tabPage2.Controls.Add(this.lblQualityCategory);
            this.tabPage2.Controls.Add(this.cmbPortfolio);
            this.tabPage2.Controls.Add(this.lblPortfolio);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(625, 663);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Риски";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cmbTypeValidationRisk
            // 
            this.cmbTypeValidationRisk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeValidationRisk.FormattingEnabled = true;
            this.cmbTypeValidationRisk.Location = new System.Drawing.Point(149, 85);
            this.cmbTypeValidationRisk.Name = "cmbTypeValidationRisk";
            this.cmbTypeValidationRisk.Size = new System.Drawing.Size(280, 21);
            this.cmbTypeValidationRisk.TabIndex = 12;
            // 
            // lblTypeValidationRisk
            // 
            this.lblTypeValidationRisk.AutoSize = true;
            this.lblTypeValidationRisk.Location = new System.Drawing.Point(43, 88);
            this.lblTypeValidationRisk.Name = "lblTypeValidationRisk";
            this.lblTypeValidationRisk.Size = new System.Drawing.Size(98, 13);
            this.lblTypeValidationRisk.TabIndex = 11;
            this.lblTypeValidationRisk.Text = "Тип оценки риска";
            // 
            // ucdRateReservation
            // 
            this.ucdRateReservation.Location = new System.Drawing.Point(149, 59);
            this.ucdRateReservation.Name = "ucdRateReservation";
            this.ucdRateReservation.Size = new System.Drawing.Size(152, 20);
            this.ucdRateReservation.TabIndex = 10;
            this.ucdRateReservation.Text = "0";
            // 
            // lblRateReservation
            // 
            this.lblRateReservation.AutoSize = true;
            this.lblRateReservation.Location = new System.Drawing.Point(11, 62);
            this.lblRateReservation.Name = "lblRateReservation";
            this.lblRateReservation.Size = new System.Drawing.Size(130, 13);
            this.lblRateReservation.TabIndex = 5;
            this.lblRateReservation.Text = "Ставка резервирования";
            // 
            // cmbQualityCategory
            // 
            this.cmbQualityCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQualityCategory.FormattingEnabled = true;
            this.cmbQualityCategory.Location = new System.Drawing.Point(149, 33);
            this.cmbQualityCategory.Name = "cmbQualityCategory";
            this.cmbQualityCategory.Size = new System.Drawing.Size(100, 21);
            this.cmbQualityCategory.TabIndex = 4;
            // 
            // lblQualityCategory
            // 
            this.lblQualityCategory.AutoSize = true;
            this.lblQualityCategory.Location = new System.Drawing.Point(32, 36);
            this.lblQualityCategory.Name = "lblQualityCategory";
            this.lblQualityCategory.Size = new System.Drawing.Size(109, 13);
            this.lblQualityCategory.TabIndex = 3;
            this.lblQualityCategory.Text = "Категория качества";
            // 
            // cmbPortfolio
            // 
            this.cmbPortfolio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortfolio.FormattingEnabled = true;
            this.cmbPortfolio.Location = new System.Drawing.Point(149, 6);
            this.cmbPortfolio.Name = "cmbPortfolio";
            this.cmbPortfolio.Size = new System.Drawing.Size(468, 21);
            this.cmbPortfolio.TabIndex = 2;
            // 
            // lblPortfolio
            // 
            this.lblPortfolio.AutoSize = true;
            this.lblPortfolio.Location = new System.Drawing.Point(83, 9);
            this.lblPortfolio.Name = "lblPortfolio";
            this.lblPortfolio.Size = new System.Drawing.Size(58, 13);
            this.lblPortfolio.TabIndex = 1;
            this.lblPortfolio.Text = "Портфель";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lvwAccounts);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(625, 663);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Счета";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lvwAccounts
            // 
            this.lvwAccounts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvwAccounts.ContextMenuStrip = this.cmsAccounts;
            this.lvwAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwAccounts.FullRowSelect = true;
            this.lvwAccounts.HideSelection = false;
            this.lvwAccounts.Location = new System.Drawing.Point(3, 3);
            this.lvwAccounts.Name = "lvwAccounts";
            this.lvwAccounts.Size = new System.Drawing.Size(619, 657);
            this.lvwAccounts.TabIndex = 0;
            this.lvwAccounts.UseCompatibleStateImageBehavior = false;
            this.lvwAccounts.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Тип счета";
            this.columnHeader1.Width = 173;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Раздел";
            this.columnHeader2.Width = 22;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Счет";
            this.columnHeader3.Width = 249;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Остаток";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 168;
            // 
            // cmsAccounts
            // 
            this.cmsAccounts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.cmsAccounts.Name = "cmsAccounts";
            this.cmsAccounts.Size = new System.Drawing.Size(149, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem1.Text = "Выбрать счет";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem2.Text = "Очистить";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.gbPayRewardGuarant);
            this.tabPage4.Controls.Add(this.gbPayFeeGuarant);
            this.tabPage4.Controls.Add(this.gbOrderPayFee);
            this.tabPage4.Controls.Add(this.btnDelRate);
            this.tabPage4.Controls.Add(this.btnEditRate);
            this.tabPage4.Controls.Add(this.btnAddRate);
            this.tabPage4.Controls.Add(this.trvRates);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(625, 663);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Ставки";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // gbPayRewardGuarant
            // 
            this.gbPayRewardGuarant.Controls.Add(this.btnPeriodPayFeeBonus);
            this.gbPayRewardGuarant.Controls.Add(this.dateNextPayFeeBonus);
            this.gbPayRewardGuarant.Controls.Add(this.lblDateNextPayFeeBonus);
            this.gbPayRewardGuarant.Controls.Add(this.cmbTypePayFeeBonus);
            this.gbPayRewardGuarant.Controls.Add(this.lblTypePayFeeBonus);
            this.gbPayRewardGuarant.Controls.Add(this.cmbCurrencyRewardGuarant);
            this.gbPayRewardGuarant.Controls.Add(this.lblCurrencyRewardGuarant);
            this.gbPayRewardGuarant.Controls.Add(this.lblOrderPayFeeBonus);
            this.gbPayRewardGuarant.Controls.Add(this.cmbOrderPayFeeBonus);
            this.gbPayRewardGuarant.Location = new System.Drawing.Point(9, 365);
            this.gbPayRewardGuarant.Name = "gbPayRewardGuarant";
            this.gbPayRewardGuarant.Size = new System.Drawing.Size(613, 104);
            this.gbPayRewardGuarant.TabIndex = 6;
            this.gbPayRewardGuarant.TabStop = false;
            this.gbPayRewardGuarant.Text = "Порядок уплаты вознаграждения за выдачу гарантии";
            // 
            // btnPeriodPayFeeBonus
            // 
            this.btnPeriodPayFeeBonus.Enabled = false;
            this.btnPeriodPayFeeBonus.Location = new System.Drawing.Point(263, 42);
            this.btnPeriodPayFeeBonus.Name = "btnPeriodPayFeeBonus";
            this.btnPeriodPayFeeBonus.Size = new System.Drawing.Size(82, 22);
            this.btnPeriodPayFeeBonus.TabIndex = 133;
            this.btnPeriodPayFeeBonus.Text = "Период";
            this.btnPeriodPayFeeBonus.UseVisualStyleBackColor = true;
            // 
            // dateNextPayFeeBonus
            // 
            this.dateNextPayFeeBonus.Enabled = false;
            this.dateNextPayFeeBonus.Location = new System.Drawing.Point(155, 42);
            this.dateNextPayFeeBonus.MaxLength = 10;
            this.dateNextPayFeeBonus.Name = "dateNextPayFeeBonus";
            this.dateNextPayFeeBonus.Size = new System.Drawing.Size(100, 20);
            this.dateNextPayFeeBonus.TabIndex = 132;
            this.dateNextPayFeeBonus.Text = "  .  .    ";
            this.dateNextPayFeeBonus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDateNextPayFeeBonus
            // 
            this.lblDateNextPayFeeBonus.AutoSize = true;
            this.lblDateNextPayFeeBonus.Location = new System.Drawing.Point(16, 45);
            this.lblDateNextPayFeeBonus.Name = "lblDateNextPayFeeBonus";
            this.lblDateNextPayFeeBonus.Size = new System.Drawing.Size(133, 13);
            this.lblDateNextPayFeeBonus.TabIndex = 131;
            this.lblDateNextPayFeeBonus.Text = "Дата следующей уплаты";
            // 
            // cmbTypePayFeeBonus
            // 
            this.cmbTypePayFeeBonus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypePayFeeBonus.FormattingEnabled = true;
            this.cmbTypePayFeeBonus.Location = new System.Drawing.Point(38, 72);
            this.cmbTypePayFeeBonus.Name = "cmbTypePayFeeBonus";
            this.cmbTypePayFeeBonus.Size = new System.Drawing.Size(256, 21);
            this.cmbTypePayFeeBonus.TabIndex = 130;
            // 
            // lblTypePayFeeBonus
            // 
            this.lblTypePayFeeBonus.AutoSize = true;
            this.lblTypePayFeeBonus.Location = new System.Drawing.Point(6, 75);
            this.lblTypePayFeeBonus.Name = "lblTypePayFeeBonus";
            this.lblTypePayFeeBonus.Size = new System.Drawing.Size(26, 13);
            this.lblTypePayFeeBonus.TabIndex = 129;
            this.lblTypePayFeeBonus.Text = "Тип";
            // 
            // cmbCurrencyRewardGuarant
            // 
            this.cmbCurrencyRewardGuarant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrencyRewardGuarant.FormattingEnabled = true;
            this.cmbCurrencyRewardGuarant.Location = new System.Drawing.Point(351, 72);
            this.cmbCurrencyRewardGuarant.Name = "cmbCurrencyRewardGuarant";
            this.cmbCurrencyRewardGuarant.Size = new System.Drawing.Size(244, 21);
            this.cmbCurrencyRewardGuarant.TabIndex = 128;
            // 
            // lblCurrencyRewardGuarant
            // 
            this.lblCurrencyRewardGuarant.AutoSize = true;
            this.lblCurrencyRewardGuarant.Location = new System.Drawing.Point(300, 75);
            this.lblCurrencyRewardGuarant.Name = "lblCurrencyRewardGuarant";
            this.lblCurrencyRewardGuarant.Size = new System.Drawing.Size(45, 13);
            this.lblCurrencyRewardGuarant.TabIndex = 127;
            this.lblCurrencyRewardGuarant.Text = "Валюта";
            // 
            // lblOrderPayFeeBonus
            // 
            this.lblOrderPayFeeBonus.AutoSize = true;
            this.lblOrderPayFeeBonus.Location = new System.Drawing.Point(59, 20);
            this.lblOrderPayFeeBonus.Name = "lblOrderPayFeeBonus";
            this.lblOrderPayFeeBonus.Size = new System.Drawing.Size(90, 13);
            this.lblOrderPayFeeBonus.TabIndex = 126;
            this.lblOrderPayFeeBonus.Text = "Порядок уплаты";
            // 
            // cmbOrderPayFeeBonus
            // 
            this.cmbOrderPayFeeBonus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrderPayFeeBonus.FormattingEnabled = true;
            this.cmbOrderPayFeeBonus.Location = new System.Drawing.Point(155, 17);
            this.cmbOrderPayFeeBonus.Name = "cmbOrderPayFeeBonus";
            this.cmbOrderPayFeeBonus.Size = new System.Drawing.Size(440, 21);
            this.cmbOrderPayFeeBonus.TabIndex = 125;
            // 
            // gbPayFeeGuarant
            // 
            this.gbPayFeeGuarant.Controls.Add(this.cmbTypePayFeeGuarant);
            this.gbPayFeeGuarant.Controls.Add(this.lblTypePayFeeGuarant);
            this.gbPayFeeGuarant.Controls.Add(this.btnPeriodPayFeeGuarant);
            this.gbPayFeeGuarant.Controls.Add(this.dateNextPayFeeGuarant);
            this.gbPayFeeGuarant.Controls.Add(this.lblDateNextPayFeeGuarant);
            this.gbPayFeeGuarant.Controls.Add(this.lblOrderPayFeeGuarant);
            this.gbPayFeeGuarant.Controls.Add(this.cmbOrderPayFeeGuarant);
            this.gbPayFeeGuarant.Location = new System.Drawing.Point(9, 255);
            this.gbPayFeeGuarant.Name = "gbPayFeeGuarant";
            this.gbPayFeeGuarant.Size = new System.Drawing.Size(613, 104);
            this.gbPayFeeGuarant.TabIndex = 5;
            this.gbPayFeeGuarant.TabStop = false;
            this.gbPayFeeGuarant.Text = "Порядок уплаты вознаграждения (гарант)";
            // 
            // cmbTypePayFeeGuarant
            // 
            this.cmbTypePayFeeGuarant.FormattingEnabled = true;
            this.cmbTypePayFeeGuarant.Location = new System.Drawing.Point(155, 45);
            this.cmbTypePayFeeGuarant.Name = "cmbTypePayFeeGuarant";
            this.cmbTypePayFeeGuarant.Size = new System.Drawing.Size(156, 21);
            this.cmbTypePayFeeGuarant.TabIndex = 131;
            // 
            // lblTypePayFeeGuarant
            // 
            this.lblTypePayFeeGuarant.AutoSize = true;
            this.lblTypePayFeeGuarant.Location = new System.Drawing.Point(123, 48);
            this.lblTypePayFeeGuarant.Name = "lblTypePayFeeGuarant";
            this.lblTypePayFeeGuarant.Size = new System.Drawing.Size(26, 13);
            this.lblTypePayFeeGuarant.TabIndex = 130;
            this.lblTypePayFeeGuarant.Text = "Тип";
            // 
            // btnPeriodPayFeeGuarant
            // 
            this.btnPeriodPayFeeGuarant.Location = new System.Drawing.Point(263, 74);
            this.btnPeriodPayFeeGuarant.Name = "btnPeriodPayFeeGuarant";
            this.btnPeriodPayFeeGuarant.Size = new System.Drawing.Size(82, 22);
            this.btnPeriodPayFeeGuarant.TabIndex = 129;
            this.btnPeriodPayFeeGuarant.Text = "Период";
            this.btnPeriodPayFeeGuarant.UseVisualStyleBackColor = true;
            // 
            // dateNextPayFeeGuarant
            // 
            this.dateNextPayFeeGuarant.Enabled = false;
            this.dateNextPayFeeGuarant.Location = new System.Drawing.Point(155, 74);
            this.dateNextPayFeeGuarant.MaxLength = 10;
            this.dateNextPayFeeGuarant.Name = "dateNextPayFeeGuarant";
            this.dateNextPayFeeGuarant.Size = new System.Drawing.Size(100, 20);
            this.dateNextPayFeeGuarant.TabIndex = 128;
            this.dateNextPayFeeGuarant.Text = "  .  .    ";
            this.dateNextPayFeeGuarant.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDateNextPayFeeGuarant
            // 
            this.lblDateNextPayFeeGuarant.AutoSize = true;
            this.lblDateNextPayFeeGuarant.Location = new System.Drawing.Point(16, 77);
            this.lblDateNextPayFeeGuarant.Name = "lblDateNextPayFeeGuarant";
            this.lblDateNextPayFeeGuarant.Size = new System.Drawing.Size(133, 13);
            this.lblDateNextPayFeeGuarant.TabIndex = 127;
            this.lblDateNextPayFeeGuarant.Text = "Дата следующей уплаты";
            // 
            // lblOrderPayFeeGuarant
            // 
            this.lblOrderPayFeeGuarant.AutoSize = true;
            this.lblOrderPayFeeGuarant.Location = new System.Drawing.Point(59, 21);
            this.lblOrderPayFeeGuarant.Name = "lblOrderPayFeeGuarant";
            this.lblOrderPayFeeGuarant.Size = new System.Drawing.Size(90, 13);
            this.lblOrderPayFeeGuarant.TabIndex = 126;
            this.lblOrderPayFeeGuarant.Text = "Порядок уплаты";
            // 
            // cmbOrderPayFeeGuarant
            // 
            this.cmbOrderPayFeeGuarant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrderPayFeeGuarant.FormattingEnabled = true;
            this.cmbOrderPayFeeGuarant.Location = new System.Drawing.Point(155, 18);
            this.cmbOrderPayFeeGuarant.Name = "cmbOrderPayFeeGuarant";
            this.cmbOrderPayFeeGuarant.Size = new System.Drawing.Size(440, 21);
            this.cmbOrderPayFeeGuarant.TabIndex = 125;
            // 
            // gbOrderPayFee
            // 
            this.gbOrderPayFee.Controls.Add(this.btnPeriodPayFee);
            this.gbOrderPayFee.Controls.Add(this.dateNextPayFee);
            this.gbOrderPayFee.Controls.Add(this.lblDateNextPayFee);
            this.gbOrderPayFee.Controls.Add(this.cmbTypePayFee);
            this.gbOrderPayFee.Controls.Add(this.lblTypePayFee);
            this.gbOrderPayFee.Controls.Add(this.cmbCurrencyPayFee);
            this.gbOrderPayFee.Controls.Add(this.lblCurrencyPayFee);
            this.gbOrderPayFee.Controls.Add(this.lblOrderPayFee);
            this.gbOrderPayFee.Controls.Add(this.cmbOrderPayFee);
            this.gbOrderPayFee.Location = new System.Drawing.Point(9, 151);
            this.gbOrderPayFee.Name = "gbOrderPayFee";
            this.gbOrderPayFee.Size = new System.Drawing.Size(613, 98);
            this.gbOrderPayFee.TabIndex = 4;
            this.gbOrderPayFee.TabStop = false;
            this.gbOrderPayFee.Text = "Порядок уплаты вознаграждения за пользование гарантией";
            // 
            // btnPeriodPayFee
            // 
            this.btnPeriodPayFee.Enabled = false;
            this.btnPeriodPayFee.Location = new System.Drawing.Point(263, 42);
            this.btnPeriodPayFee.Name = "btnPeriodPayFee";
            this.btnPeriodPayFee.Size = new System.Drawing.Size(82, 21);
            this.btnPeriodPayFee.TabIndex = 5;
            this.btnPeriodPayFee.Text = "Период";
            this.btnPeriodPayFee.UseVisualStyleBackColor = true;
            // 
            // dateNextPayFee
            // 
            this.dateNextPayFee.Enabled = false;
            this.dateNextPayFee.Location = new System.Drawing.Point(155, 43);
            this.dateNextPayFee.MaxLength = 10;
            this.dateNextPayFee.Name = "dateNextPayFee";
            this.dateNextPayFee.Size = new System.Drawing.Size(100, 20);
            this.dateNextPayFee.TabIndex = 4;
            this.dateNextPayFee.Text = "  .  .    ";
            this.dateNextPayFee.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDateNextPayFee
            // 
            this.lblDateNextPayFee.AutoSize = true;
            this.lblDateNextPayFee.Location = new System.Drawing.Point(16, 46);
            this.lblDateNextPayFee.Name = "lblDateNextPayFee";
            this.lblDateNextPayFee.Size = new System.Drawing.Size(133, 13);
            this.lblDateNextPayFee.TabIndex = 3;
            this.lblDateNextPayFee.Text = "Дата следующей уплаты";
            // 
            // cmbTypePayFee
            // 
            this.cmbTypePayFee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypePayFee.FormattingEnabled = true;
            this.cmbTypePayFee.Location = new System.Drawing.Point(38, 69);
            this.cmbTypePayFee.Name = "cmbTypePayFee";
            this.cmbTypePayFee.Size = new System.Drawing.Size(256, 21);
            this.cmbTypePayFee.TabIndex = 7;
            // 
            // lblTypePayFee
            // 
            this.lblTypePayFee.AutoSize = true;
            this.lblTypePayFee.Location = new System.Drawing.Point(6, 72);
            this.lblTypePayFee.Name = "lblTypePayFee";
            this.lblTypePayFee.Size = new System.Drawing.Size(26, 13);
            this.lblTypePayFee.TabIndex = 6;
            this.lblTypePayFee.Text = "Тип";
            // 
            // cmbCurrencyPayFee
            // 
            this.cmbCurrencyPayFee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCurrencyPayFee.FormattingEnabled = true;
            this.cmbCurrencyPayFee.Location = new System.Drawing.Point(351, 69);
            this.cmbCurrencyPayFee.Name = "cmbCurrencyPayFee";
            this.cmbCurrencyPayFee.Size = new System.Drawing.Size(244, 21);
            this.cmbCurrencyPayFee.TabIndex = 9;
            // 
            // lblCurrencyPayFee
            // 
            this.lblCurrencyPayFee.AutoSize = true;
            this.lblCurrencyPayFee.Location = new System.Drawing.Point(300, 72);
            this.lblCurrencyPayFee.Name = "lblCurrencyPayFee";
            this.lblCurrencyPayFee.Size = new System.Drawing.Size(45, 13);
            this.lblCurrencyPayFee.TabIndex = 8;
            this.lblCurrencyPayFee.Text = "Валюта";
            // 
            // lblOrderPayFee
            // 
            this.lblOrderPayFee.AutoSize = true;
            this.lblOrderPayFee.Location = new System.Drawing.Point(62, 21);
            this.lblOrderPayFee.Name = "lblOrderPayFee";
            this.lblOrderPayFee.Size = new System.Drawing.Size(90, 13);
            this.lblOrderPayFee.TabIndex = 1;
            this.lblOrderPayFee.Text = "Порядок уплаты";
            // 
            // cmbOrderPayFee
            // 
            this.cmbOrderPayFee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrderPayFee.FormattingEnabled = true;
            this.cmbOrderPayFee.Location = new System.Drawing.Point(155, 18);
            this.cmbOrderPayFee.Name = "cmbOrderPayFee";
            this.cmbOrderPayFee.Size = new System.Drawing.Size(440, 21);
            this.cmbOrderPayFee.TabIndex = 2;
            // 
            // btnDelRate
            // 
            this.btnDelRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelRate.Location = new System.Drawing.Point(540, 71);
            this.btnDelRate.Name = "btnDelRate";
            this.btnDelRate.Size = new System.Drawing.Size(82, 26);
            this.btnDelRate.TabIndex = 3;
            this.btnDelRate.Text = "Удалить";
            this.btnDelRate.UseVisualStyleBackColor = true;
            // 
            // btnEditRate
            // 
            this.btnEditRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditRate.Location = new System.Drawing.Point(540, 39);
            this.btnEditRate.Name = "btnEditRate";
            this.btnEditRate.Size = new System.Drawing.Size(82, 26);
            this.btnEditRate.TabIndex = 2;
            this.btnEditRate.Text = "Изменить";
            this.btnEditRate.UseVisualStyleBackColor = true;
            // 
            // btnAddRate
            // 
            this.btnAddRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRate.Location = new System.Drawing.Point(540, 7);
            this.btnAddRate.Name = "btnAddRate";
            this.btnAddRate.Size = new System.Drawing.Size(82, 26);
            this.btnAddRate.TabIndex = 1;
            this.btnAddRate.Text = "Добавить";
            this.btnAddRate.UseVisualStyleBackColor = true;
            this.btnAddRate.Click += new System.EventHandler(this.btnAddRate_Click);
            // 
            // trvRates
            // 
            this.trvRates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trvRates.Location = new System.Drawing.Point(9, 7);
            this.trvRates.Name = "trvRates";
            this.trvRates.Size = new System.Drawing.Size(529, 138);
            this.trvRates.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.btnListGuarantOperDog);
            this.tabPage5.Controls.Add(this.btnInclude);
            this.tabPage5.Controls.Add(this.btnEditGuarant);
            this.tabPage5.Controls.Add(this.btnAddGuarant);
            this.tabPage5.Controls.Add(this.lvwGuarant);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(625, 663);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Обеспечения";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // btnListGuarantOperDog
            // 
            this.btnListGuarantOperDog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnListGuarantOperDog.Location = new System.Drawing.Point(541, 103);
            this.btnListGuarantOperDog.Name = "btnListGuarantOperDog";
            this.btnListGuarantOperDog.Size = new System.Drawing.Size(82, 26);
            this.btnListGuarantOperDog.TabIndex = 4;
            this.btnListGuarantOperDog.Text = "Операции";
            this.btnListGuarantOperDog.UseVisualStyleBackColor = true;
            // 
            // btnInclude
            // 
            this.btnInclude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInclude.Location = new System.Drawing.Point(541, 71);
            this.btnInclude.Name = "btnInclude";
            this.btnInclude.Size = new System.Drawing.Size(82, 26);
            this.btnInclude.TabIndex = 3;
            this.btnInclude.Text = "Привязать";
            this.btnInclude.UseVisualStyleBackColor = true;
            // 
            // btnEditGuarant
            // 
            this.btnEditGuarant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditGuarant.Location = new System.Drawing.Point(541, 39);
            this.btnEditGuarant.Name = "btnEditGuarant";
            this.btnEditGuarant.Size = new System.Drawing.Size(82, 26);
            this.btnEditGuarant.TabIndex = 2;
            this.btnEditGuarant.Text = "Изменить";
            this.btnEditGuarant.UseVisualStyleBackColor = true;
            // 
            // btnAddGuarant
            // 
            this.btnAddGuarant.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddGuarant.Location = new System.Drawing.Point(541, 7);
            this.btnAddGuarant.Name = "btnAddGuarant";
            this.btnAddGuarant.Size = new System.Drawing.Size(82, 26);
            this.btnAddGuarant.TabIndex = 1;
            this.btnAddGuarant.Text = "Заключить";
            this.btnAddGuarant.UseVisualStyleBackColor = true;
            // 
            // lvwGuarant
            // 
            this.lvwGuarant.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwGuarant.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lvwGuarant.FullRowSelect = true;
            this.lvwGuarant.GridLines = true;
            this.lvwGuarant.HideSelection = false;
            this.lvwGuarant.Location = new System.Drawing.Point(3, 3);
            this.lvwGuarant.Name = "lvwGuarant";
            this.lvwGuarant.Size = new System.Drawing.Size(535, 660);
            this.lvwGuarant.TabIndex = 0;
            this.lvwGuarant.UseCompatibleStateImageBehavior = false;
            this.lvwGuarant.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Идентификатор";
            this.columnHeader5.Width = 71;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Номер договора";
            this.columnHeader6.Width = 110;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Описание";
            this.columnHeader7.Width = 339;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.ubsCtrlFields);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(625, 663);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Дополнительные свойства";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // ubsCtrlFields
            // 
            this.ubsCtrlFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ubsCtrlFields.Location = new System.Drawing.Point(3, 3);
            this.ubsCtrlFields.Name = "ubsCtrlFields";
            this.ubsCtrlFields.ReadOnly = false;
            this.ubsCtrlFields.Size = new System.Drawing.Size(619, 657);
            this.ubsCtrlFields.TabIndex = 0;
            // 
            // UbsBgContractFrm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(633, 721);
            this.Name = "UbsBgContractFrm";
            this.Text = "Шаблон формы";
            this.Ubs_ActionRunBegin += new UbsService.UbsActionRunBeginEventHandler(this.ubsBgContractFrm_Ubs_ActionRunBegin);
            this.Controls.SetChildIndex(this.panelMain, 0);
            this.panelMain.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.gbCover.ResumeLayout(false);
            this.gbCover.PerformLayout();
            this.gbGarant.ResumeLayout(false);
            this.gbGarant.PerformLayout();
            this.gbClients.ResumeLayout(false);
            this.gbClients.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.cmsAccounts.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.gbPayRewardGuarant.ResumeLayout(false);
            this.gbPayRewardGuarant.PerformLayout();
            this.gbPayFeeGuarant.ResumeLayout(false);
            this.gbPayFeeGuarant.PerformLayout();
            this.gbOrderPayFee.ResumeLayout(false);
            this.gbOrderPayFee.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button btnExit;
        private UbsControl.UbsCtrlInfo ubsCtrlInfo;
        private Button btnSave;
        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private LinkLabel linkFrameContract;
        private TextBox txtFrameContract;
        private Button btnFrameContractDel;
        private TextBox txtModel;
        private LinkLabel linkModel;
        private ComboBox cmbKindGarant;
        private Label lblkind;
        private UbsControl.UbsCtrlDate dateAgent;
        private Label label3;
        private TextBox txtNumAgent;
        private Label label2;
        private TextBox txtAgent;
        private LinkLabel linkAgent;
        private Button btnAgentDel;
        private UbsControl.UbsCtrlDate dateReward;
        private Label lblDateReward;
        private Label label1;
        private Label label4;
        private UbsControl.UbsCtrlDate dateAdjustment;
        private Label label5;
        private UbsControl.UbsCtrlDecimal paidAmount;
        private UbsControl.UbsCtrlDecimal costAmount;
        private UbsControl.UbsCtrlDecimal transAmount;
        private Label label6;
        private GroupBox gbClients;
        private TextBox txtPrincipal;
        private LinkLabel linkPrincipal;
        private UbsControl.UbsCtrlDate datePrincipal;
        private Label lblDatePrincipal;
        private TextBox txtGarant;
        private LinkLabel linkGarant;
        private TextBox txtBeneficiar;
        private LinkLabel linkBeneficiar;
        private Button btnManualBenificiar;
        private GroupBox gbCover;
        private TextBox txtContractCover;
        private GroupBox gbGarant;
        private TextBox txtNumberGarant;
        private ComboBox cmbExecutor;
        private Label lblExecutor;
        private ComboBox cmbState;
        private Label lblState;
        private ComboBox cmbNumberDiv;
        private Label lblNumberDiv;
        private TextBox txtPreviousContract;
        private LinkLabel linkPreviousContract;
        private ComboBox cmbWarrant;
        private Label lblWarrant;
        private Label lblUID;
        private Button btnReRead;
        private Label lblNumberGarant;
        private UbsControl.UbsCtrlDate dateOpenGarant;
        private Label lblDateOpenGarant;
        private UbsControl.UbsCtrlDate dateBeginGarant;
        private Label lblDateBeginGarant;
        private UbsControl.UbsCtrlDate dateCloseGarant;
        private Label lblDateCloseGarant;
        private UbsControl.UbsCtrlDate dateEndGarant;
        private Label lblDateEndGarant;
        private Label lblSumGarant;
        private UbsControl.UbsCtrlDecimal ucdSumGarant;
        private ComboBox cmbCurrencyGarant;
        private Label lblCurrencyGarant;
        private ComboBox cmbTypeCover;
        private Label lblTypeCover;
        private ComboBox cmbCurrencyCover;
        private Label lblCurrencyCover;
        private UbsControl.UbsCtrlDecimal ucdSumCover;
        private Label lblCoverSum;
        private Label lblContractCover;
        private ComboBox cmbPortfolio;
        private Label lblPortfolio;
        private ComboBox cmbQualityCategory;
        private Label lblQualityCategory;
        private UbsControl.UbsCtrlDecimal ucdRateReservation;
        private Label lblRateReservation;
        private ComboBox cmbTypeValidationRisk;
        private Label lblTypeValidationRisk;
        private ListView lvwAccounts;
        private Button btnAddRate;
        private TreeView trvRates;
        private Button btnDelRate;
        private Button btnEditRate;
        private GroupBox gbPayRewardGuarant;
        private GroupBox gbPayFeeGuarant;
        private GroupBox gbOrderPayFee;
        private ComboBox cmbCurrencyPayFee;
        private Label lblCurrencyPayFee;
        private Label lblOrderPayFee;
        private ComboBox cmbOrderPayFee;
        private ListView lvwGuarant;
        private Button btnListGuarantOperDog;
        private Button btnInclude;
        private Button btnEditGuarant;
        private Button btnAddGuarant;
        private UbsControl.UbsCtrlFields ubsCtrlFields;
        private Button btnPeriodPayFee;
        private UbsControl.UbsCtrlDate dateNextPayFee;
        private Label lblDateNextPayFee;
        private ComboBox cmbTypePayFee;
        private Label lblTypePayFee;
        private Button btnPeriodPayFeeGuarant;
        private UbsControl.UbsCtrlDate dateNextPayFeeGuarant;
        private Label lblDateNextPayFeeGuarant;
        private Label lblOrderPayFeeGuarant;
        private ComboBox cmbOrderPayFeeGuarant;
        private ComboBox cmbTypePayFeeGuarant;
        private Label lblTypePayFeeGuarant;
        private Button btnPeriodPayFeeBonus;
        private UbsControl.UbsCtrlDate dateNextPayFeeBonus;
        private Label lblDateNextPayFeeBonus;
        private ComboBox cmbTypePayFeeBonus;
        private Label lblTypePayFeeBonus;
        private ComboBox cmbCurrencyRewardGuarant;
        private Label lblCurrencyRewardGuarant;
        private Label lblOrderPayFeeBonus;
        private ComboBox cmbOrderPayFeeBonus;
        private ContextMenuStrip cmsAccounts;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
    }
}