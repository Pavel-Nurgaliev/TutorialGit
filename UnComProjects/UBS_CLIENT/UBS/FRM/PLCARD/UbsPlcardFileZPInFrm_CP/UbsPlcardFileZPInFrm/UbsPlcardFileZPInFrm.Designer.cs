using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    partial class UbsPlcardFileZPInFrm
    {
        //private const int WM_CLOSE = 0x0010;

        //#region ��������������� WndProc

        ///// <summary>
        ///// ��������������� ������� ���������
        ///// </summary>
        ///// <param name="m">���������</param>
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.ubsCtrlInfo = new UbsControl.UbsCtrlInfo();
            this.dateLoad = new UbsControl.UbsCtrlDate();
            this.lblDateLoad = new System.Windows.Forms.Label();
            this.linkFileLoad = new System.Windows.Forms.LinkLabel();
            this.txtFileLoad = new System.Windows.Forms.TextBox();
            this.txtPayment = new System.Windows.Forms.TextBox();
            this.linkPayment = new System.Windows.Forms.LinkLabel();
            this.lblProfitKind = new System.Windows.Forms.Label();
            this.cmbProfitKind = new System.Windows.Forms.ComboBox();
            this.cmbCardSearch = new System.Windows.Forms.ComboBox();
            this.lblCardSearch = new System.Windows.Forms.Label();
            this.chkControlRun = new System.Windows.Forms.CheckBox();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblChannelInfo7 = new System.Windows.Forms.Label();
            this.lblChannelInfo6 = new System.Windows.Forms.Label();
            this.lblChannelInfo5 = new System.Windows.Forms.Label();
            this.lblChannelInfo4 = new System.Windows.Forms.Label();
            this.lblChannelInfo3 = new System.Windows.Forms.Label();
            this.lblChannelInfo2 = new System.Windows.Forms.Label();
            this.lblChannelInfo1 = new System.Windows.Forms.Label();
            this.lblChannel = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panelMain.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.lblProgress);
            this.panelMain.Controls.Add(this.lblChannelInfo7);
            this.panelMain.Controls.Add(this.lblChannelInfo6);
            this.panelMain.Controls.Add(this.lblChannelInfo5);
            this.panelMain.Controls.Add(this.lblChannelInfo4);
            this.panelMain.Controls.Add(this.lblChannelInfo3);
            this.panelMain.Controls.Add(this.lblChannelInfo2);
            this.panelMain.Controls.Add(this.lblChannelInfo1);
            this.panelMain.Controls.Add(this.lblChannel);
            this.panelMain.Controls.Add(this.chkControlRun);
            this.panelMain.Controls.Add(this.cmbCardSearch);
            this.panelMain.Controls.Add(this.lblCardSearch);
            this.panelMain.Controls.Add(this.cmbProfitKind);
            this.panelMain.Controls.Add(this.lblProfitKind);
            this.panelMain.Controls.Add(this.txtPayment);
            this.panelMain.Controls.Add(this.linkPayment);
            this.panelMain.Controls.Add(this.txtFileLoad);
            this.panelMain.Controls.Add(this.linkFileLoad);
            this.panelMain.Controls.Add(this.dateLoad);
            this.panelMain.Controls.Add(this.lblDateLoad);
            this.panelMain.Controls.Add(this.tableLayoutPanel);
            this.panelMain.Size = new System.Drawing.Size(460, 283);
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
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 251);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(460, 32);
            this.tableLayoutPanel.TabIndex = 100;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(287, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 26);
            this.btnSave.TabIndex = 101;
            this.btnSave.Text = "��������";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.CausesValidation = false;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(375, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 26);
            this.btnExit.TabIndex = 102;
            this.btnExit.Text = "�����";
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
            this.ubsCtrlInfo.Size = new System.Drawing.Size(278, 13);
            this.ubsCtrlInfo.TabIndex = 1;
            this.ubsCtrlInfo.Text = "ubsCtrlInfo";
            this.ubsCtrlInfo.Visible = false;
            // 
            // dateLoad
            // 
            this.dateLoad.Location = new System.Drawing.Point(105, 12);
            this.dateLoad.MaxLength = 10;
            this.dateLoad.Name = "dateLoad";
            this.dateLoad.Size = new System.Drawing.Size(100, 20);
            this.dateLoad.TabIndex = 2;
            this.dateLoad.Text = "  .  .    ";
            this.dateLoad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblDateLoad
            // 
            this.lblDateLoad.AutoSize = true;
            this.lblDateLoad.Location = new System.Drawing.Point(15, 15);
            this.lblDateLoad.Name = "lblDateLoad";
            this.lblDateLoad.Size = new System.Drawing.Size(82, 13);
            this.lblDateLoad.TabIndex = 1;
            this.lblDateLoad.Text = "���� ��������";
            // 
            // linkFileLoad
            // 
            this.linkFileLoad.AutoSize = true;
            this.linkFileLoad.Location = new System.Drawing.Point(15, 41);
            this.linkFileLoad.Name = "linkFileLoad";
            this.linkFileLoad.Size = new System.Drawing.Size(106, 13);
            this.linkFileLoad.TabIndex = 3;
            this.linkFileLoad.TabStop = true;
            this.linkFileLoad.Text = "���� ��� ��������";
            this.linkFileLoad.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkFileLoad_LinkClicked);
            // 
            // txtFileLoad
            // 
            this.txtFileLoad.Location = new System.Drawing.Point(15, 58);
            this.txtFileLoad.Name = "txtFileLoad";
            this.txtFileLoad.Size = new System.Drawing.Size(429, 20);
            this.txtFileLoad.TabIndex = 4;
            // 
            // txtPayment
            // 
            this.txtPayment.Location = new System.Drawing.Point(15, 99);
            this.txtPayment.Name = "txtPayment";
            this.txtPayment.ReadOnly = true;
            this.txtPayment.Size = new System.Drawing.Size(429, 20);
            this.txtPayment.TabIndex = 6;
            // 
            // linkPayment
            // 
            this.linkPayment.AutoSize = true;
            this.linkPayment.Location = new System.Drawing.Point(15, 82);
            this.linkPayment.Name = "linkPayment";
            this.linkPayment.Size = new System.Drawing.Size(92, 13);
            this.linkPayment.TabIndex = 5;
            this.linkPayment.TabStop = true;
            this.linkPayment.Text = "������� ������";
            this.linkPayment.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkPayment_LinkClicked);
            // 
            // lblProfitKind
            // 
            this.lblProfitKind.AutoSize = true;
            this.lblProfitKind.Location = new System.Drawing.Point(15, 122);
            this.lblProfitKind.Name = "lblProfitKind";
            this.lblProfitKind.Size = new System.Drawing.Size(64, 13);
            this.lblProfitKind.TabIndex = 7;
            this.lblProfitKind.Text = "��� ������";
            // 
            // cmbProfitKind
            // 
            this.cmbProfitKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProfitKind.FormattingEnabled = true;
            this.cmbProfitKind.Location = new System.Drawing.Point(15, 139);
            this.cmbProfitKind.Name = "cmbProfitKind";
            this.cmbProfitKind.Size = new System.Drawing.Size(429, 21);
            this.cmbProfitKind.TabIndex = 8;
            // 
            // cmbCardSearch
            // 
            this.cmbCardSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCardSearch.FormattingEnabled = true;
            this.cmbCardSearch.Location = new System.Drawing.Point(15, 180);
            this.cmbCardSearch.Name = "cmbCardSearch";
            this.cmbCardSearch.Size = new System.Drawing.Size(292, 21);
            this.cmbCardSearch.TabIndex = 10;
            // 
            // lblCardSearch
            // 
            this.lblCardSearch.AutoSize = true;
            this.lblCardSearch.Location = new System.Drawing.Point(15, 163);
            this.lblCardSearch.Name = "lblCardSearch";
            this.lblCardSearch.Size = new System.Drawing.Size(73, 13);
            this.lblCardSearch.TabIndex = 9;
            this.lblCardSearch.Text = "����� �����";
            // 
            // chkControlRun
            // 
            this.chkControlRun.AutoSize = true;
            this.chkControlRun.Location = new System.Drawing.Point(313, 182);
            this.chkControlRun.Name = "chkControlRun";
            this.chkControlRun.Size = new System.Drawing.Size(131, 17);
            this.chkControlRun.TabIndex = 11;
            this.chkControlRun.Text = "����������� ������";
            this.chkControlRun.UseVisualStyleBackColor = true;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(12, 229);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(165, 13);
            this.lblProgress.TabIndex = 20;
            this.lblProgress.Text = "";
            // 
            // lblChannelInfo7
            // 
            this.lblChannelInfo7.AutoSize = true;
            this.lblChannelInfo7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannelInfo7.Location = new System.Drawing.Point(211, 208);
            this.lblChannelInfo7.Name = "lblChannelInfo7";
            this.lblChannelInfo7.Size = new System.Drawing.Size(15, 15);
            this.lblChannelInfo7.TabIndex = 19;
            this.lblChannelInfo7.Text = "7";
            this.lblChannelInfo7.Visible = false;
            // 
            // lblChannelInfo6
            // 
            this.lblChannelInfo6.AutoSize = true;
            this.lblChannelInfo6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannelInfo6.Location = new System.Drawing.Point(190, 208);
            this.lblChannelInfo6.Name = "lblChannelInfo6";
            this.lblChannelInfo6.Size = new System.Drawing.Size(15, 15);
            this.lblChannelInfo6.TabIndex = 18;
            this.lblChannelInfo6.Text = "6";
            this.lblChannelInfo6.Visible = false;
            // 
            // lblChannelInfo5
            // 
            this.lblChannelInfo5.AutoSize = true;
            this.lblChannelInfo5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannelInfo5.Location = new System.Drawing.Point(169, 208);
            this.lblChannelInfo5.Name = "lblChannelInfo5";
            this.lblChannelInfo5.Size = new System.Drawing.Size(15, 15);
            this.lblChannelInfo5.TabIndex = 17;
            this.lblChannelInfo5.Text = "5";
            this.lblChannelInfo5.Visible = false;
            // 
            // lblChannelInfo4
            // 
            this.lblChannelInfo4.AutoSize = true;
            this.lblChannelInfo4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannelInfo4.Location = new System.Drawing.Point(148, 208);
            this.lblChannelInfo4.Name = "lblChannelInfo4";
            this.lblChannelInfo4.Size = new System.Drawing.Size(15, 15);
            this.lblChannelInfo4.TabIndex = 16;
            this.lblChannelInfo4.Text = "4";
            this.lblChannelInfo4.Visible = false;
            // 
            // lblChannelInfo3
            // 
            this.lblChannelInfo3.AutoSize = true;
            this.lblChannelInfo3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannelInfo3.Location = new System.Drawing.Point(127, 208);
            this.lblChannelInfo3.Name = "lblChannelInfo3";
            this.lblChannelInfo3.Size = new System.Drawing.Size(15, 15);
            this.lblChannelInfo3.TabIndex = 15;
            this.lblChannelInfo3.Text = "3";
            this.lblChannelInfo3.Visible = false;
            // 
            // lblChannelInfo2
            // 
            this.lblChannelInfo2.AutoSize = true;
            this.lblChannelInfo2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannelInfo2.Location = new System.Drawing.Point(106, 208);
            this.lblChannelInfo2.Name = "lblChannelInfo2";
            this.lblChannelInfo2.Size = new System.Drawing.Size(15, 15);
            this.lblChannelInfo2.TabIndex = 14;
            this.lblChannelInfo2.Text = "2";
            this.lblChannelInfo2.Visible = false;
            // 
            // lblChannelInfo1
            // 
            this.lblChannelInfo1.AutoSize = true;
            this.lblChannelInfo1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannelInfo1.Location = new System.Drawing.Point(85, 208);
            this.lblChannelInfo1.Name = "lblChannelInfo1";
            this.lblChannelInfo1.Size = new System.Drawing.Size(15, 15);
            this.lblChannelInfo1.TabIndex = 13;
            this.lblChannelInfo1.Text = "1";
            this.lblChannelInfo1.Visible = false;
            // 
            // lblChannel
            // 
            this.lblChannel.AutoSize = true;
            this.lblChannel.Location = new System.Drawing.Point(12, 208);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(46, 13);
            this.lblChannel.TabIndex = 12;
            this.lblChannel.Text = "������";
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // UbsPlcardFileZPInFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 283);
            this.Name = "UbsPlcardFileZPInFrm";
            this.Text = "������ �����";
            this.Ubs_ActionRunBegin += new UbsService.UbsActionRunBeginEventHandler(this.UbsPlcardFileZPInFrm_Ubs_ActionRunBegin);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button btnExit;
        private UbsControl.UbsCtrlInfo ubsCtrlInfo;
        private Button btnSave;
        private UbsControl.UbsCtrlDate dateLoad;
        private Label lblDateLoad;
        private CheckBox chkControlRun;
        private ComboBox cmbCardSearch;
        private Label lblCardSearch;
        private ComboBox cmbProfitKind;
        private Label lblProfitKind;
        private TextBox txtPayment;
        private LinkLabel linkPayment;
        private TextBox txtFileLoad;
        private LinkLabel linkFileLoad;
        private Label lblProgress;
        private Label lblChannelInfo7;
        private Label lblChannelInfo6;
        private Label lblChannelInfo5;
        private Label lblChannelInfo4;
        private Label lblChannelInfo3;
        private Label lblChannelInfo2;
        private Label lblChannelInfo1;
        private Label lblChannel;
        private Timer timer;
    }
}

