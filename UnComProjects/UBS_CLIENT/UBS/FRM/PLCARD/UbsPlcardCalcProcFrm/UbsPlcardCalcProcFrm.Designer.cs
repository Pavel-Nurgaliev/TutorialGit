using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    partial class UbsPlcardCalcProcFrm
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.ubsCtrlInfo = new UbsControl.UbsCtrlInfo();
            this.lblOperationCaption = new System.Windows.Forms.Label();
            this.rbColectedPercent = new System.Windows.Forms.RadioButton();
            this.rbPaymentPercent = new System.Windows.Forms.RadioButton();
            this.lblChannel = new System.Windows.Forms.Label();
            this.lblChannelInfo6 = new System.Windows.Forms.Label();
            this.lblChannelInfo5 = new System.Windows.Forms.Label();
            this.lblChannelInfo4 = new System.Windows.Forms.Label();
            this.lblChannelInfo3 = new System.Windows.Forms.Label();
            this.lblChannelInfo2 = new System.Windows.Forms.Label();
            this.lblChannelInfo1 = new System.Windows.Forms.Label();
            this.lblChannelInfo0 = new System.Windows.Forms.Label();
            this.chkTestMode = new System.Windows.Forms.CheckBox();
            this.lblMainDateOverDraft = new System.Windows.Forms.Label();
            this.lblDateOverDraft = new System.Windows.Forms.Label();
            this.lblDateTrn = new System.Windows.Forms.Label();
            this.dateOverDraft = new UbsControl.UbsCtrlDate();
            this.dateTrn = new UbsControl.UbsCtrlDate();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timerChannel = new System.Windows.Forms.Timer(this.components);
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panelMain.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.label5);
            this.panelMain.Controls.Add(this.lblTime);
            this.panelMain.Controls.Add(this.label4);
            this.panelMain.Controls.Add(this.dateTrn);
            this.panelMain.Controls.Add(this.dateOverDraft);
            this.panelMain.Controls.Add(this.lblDateTrn);
            this.panelMain.Controls.Add(this.lblDateOverDraft);
            this.panelMain.Controls.Add(this.lblMainDateOverDraft);
            this.panelMain.Controls.Add(this.chkTestMode);
            this.panelMain.Controls.Add(this.lblChannelInfo6);
            this.panelMain.Controls.Add(this.lblChannelInfo5);
            this.panelMain.Controls.Add(this.lblChannelInfo4);
            this.panelMain.Controls.Add(this.lblChannelInfo3);
            this.panelMain.Controls.Add(this.lblChannelInfo2);
            this.panelMain.Controls.Add(this.lblChannelInfo1);
            this.panelMain.Controls.Add(this.lblChannelInfo0);
            this.panelMain.Controls.Add(this.lblChannel);
            this.panelMain.Controls.Add(this.rbPaymentPercent);
            this.panelMain.Controls.Add(this.rbColectedPercent);
            this.panelMain.Controls.Add(this.lblOperationCaption);
            this.panelMain.Controls.Add(this.tableLayoutPanel);
            this.panelMain.Size = new System.Drawing.Size(399, 294);
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
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 262);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(399, 32);
            this.tableLayoutPanel.TabIndex = 100;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(226, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 26);
            this.btnSave.TabIndex = 101;
            this.btnSave.Text = "Выполнить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.CausesValidation = false;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(314, 3);
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
            this.ubsCtrlInfo.Size = new System.Drawing.Size(217, 13);
            this.ubsCtrlInfo.TabIndex = 1;
            this.ubsCtrlInfo.Text = "ubsCtrlInfo";
            this.ubsCtrlInfo.Visible = false;
            // 
            // lblOperationCaption
            // 
            this.lblOperationCaption.AutoSize = true;
            this.lblOperationCaption.Location = new System.Drawing.Point(10, 9);
            this.lblOperationCaption.Name = "lblOperationCaption";
            this.lblOperationCaption.Size = new System.Drawing.Size(77, 13);
            this.lblOperationCaption.TabIndex = 1;
            this.lblOperationCaption.Text = "Вид операции";
            // 
            // rbColectedPercent
            // 
            this.rbColectedPercent.AutoSize = true;
            this.rbColectedPercent.Location = new System.Drawing.Point(13, 30);
            this.rbColectedPercent.Name = "rbColectedPercent";
            this.rbColectedPercent.Size = new System.Drawing.Size(143, 17);
            this.rbColectedPercent.TabIndex = 2;
            this.rbColectedPercent.TabStop = true;
            this.rbColectedPercent.Text = "Накопление процентов";
            this.rbColectedPercent.UseVisualStyleBackColor = true;
            this.rbColectedPercent.CheckedChanged += new System.EventHandler(this.rbColectedPercent_CheckedChanged);
            // 
            // rbPaymentPercent
            // 
            this.rbPaymentPercent.AutoSize = true;
            this.rbPaymentPercent.Location = new System.Drawing.Point(13, 53);
            this.rbPaymentPercent.Name = "rbPaymentPercent";
            this.rbPaymentPercent.Size = new System.Drawing.Size(118, 17);
            this.rbPaymentPercent.TabIndex = 3;
            this.rbPaymentPercent.TabStop = true;
            this.rbPaymentPercent.Text = "Уплата процентов";
            this.rbPaymentPercent.UseVisualStyleBackColor = true;
            this.rbPaymentPercent.CheckedChanged += new System.EventHandler(this.rbPaymentPercent_CheckedChanged);
            // 
            // lblChannel
            // 
            this.lblChannel.AutoSize = true;
            this.lblChannel.Location = new System.Drawing.Point(196, 55);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(46, 13);
            this.lblChannel.TabIndex = 4;
            this.lblChannel.Text = "Каналы";
            this.lblChannel.Visible = false;
            // 
            // lblChannelInfo6
            // 
            this.lblChannelInfo6.AutoSize = true;
            this.lblChannelInfo6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannelInfo6.Location = new System.Drawing.Point(374, 55);
            this.lblChannelInfo6.Name = "lblChannelInfo6";
            this.lblChannelInfo6.Size = new System.Drawing.Size(15, 15);
            this.lblChannelInfo6.TabIndex = 11;
            this.lblChannelInfo6.Text = "6";
            this.lblChannelInfo6.Visible = false;
            // 
            // lblChannelInfo5
            // 
            this.lblChannelInfo5.AutoSize = true;
            this.lblChannelInfo5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannelInfo5.Location = new System.Drawing.Point(353, 55);
            this.lblChannelInfo5.Name = "lblChannelInfo5";
            this.lblChannelInfo5.Size = new System.Drawing.Size(15, 15);
            this.lblChannelInfo5.TabIndex = 10;
            this.lblChannelInfo5.Text = "5";
            this.lblChannelInfo5.Visible = false;
            // 
            // lblChannelInfo4
            // 
            this.lblChannelInfo4.AutoSize = true;
            this.lblChannelInfo4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannelInfo4.Location = new System.Drawing.Point(332, 55);
            this.lblChannelInfo4.Name = "lblChannelInfo4";
            this.lblChannelInfo4.Size = new System.Drawing.Size(15, 15);
            this.lblChannelInfo4.TabIndex = 9;
            this.lblChannelInfo4.Text = "4";
            this.lblChannelInfo4.Visible = false;
            // 
            // lblChannelInfo3
            // 
            this.lblChannelInfo3.AutoSize = true;
            this.lblChannelInfo3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannelInfo3.Location = new System.Drawing.Point(311, 55);
            this.lblChannelInfo3.Name = "lblChannelInfo3";
            this.lblChannelInfo3.Size = new System.Drawing.Size(15, 15);
            this.lblChannelInfo3.TabIndex = 8;
            this.lblChannelInfo3.Text = "3";
            this.lblChannelInfo3.Visible = false;
            // 
            // lblChannelInfo2
            // 
            this.lblChannelInfo2.AutoSize = true;
            this.lblChannelInfo2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannelInfo2.Location = new System.Drawing.Point(290, 55);
            this.lblChannelInfo2.Name = "lblChannelInfo2";
            this.lblChannelInfo2.Size = new System.Drawing.Size(15, 15);
            this.lblChannelInfo2.TabIndex = 7;
            this.lblChannelInfo2.Text = "2";
            this.lblChannelInfo2.Visible = false;
            // 
            // lblChannelInfo1
            // 
            this.lblChannelInfo1.AutoSize = true;
            this.lblChannelInfo1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannelInfo1.Location = new System.Drawing.Point(269, 55);
            this.lblChannelInfo1.Name = "lblChannelInfo1";
            this.lblChannelInfo1.Size = new System.Drawing.Size(15, 15);
            this.lblChannelInfo1.TabIndex = 6;
            this.lblChannelInfo1.Text = "1";
            this.lblChannelInfo1.Visible = false;
            // 
            // lblChannelInfo0
            // 
            this.lblChannelInfo0.AutoSize = true;
            this.lblChannelInfo0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannelInfo0.Location = new System.Drawing.Point(248, 55);
            this.lblChannelInfo0.Name = "lblChannelInfo0";
            this.lblChannelInfo0.Size = new System.Drawing.Size(15, 15);
            this.lblChannelInfo0.TabIndex = 5;
            this.lblChannelInfo0.Text = "0";
            this.lblChannelInfo0.Visible = false;
            // 
            // chkTestMode
            // 
            this.chkTestMode.AutoSize = true;
            this.chkTestMode.Location = new System.Drawing.Point(13, 77);
            this.chkTestMode.Name = "chkTestMode";
            this.chkTestMode.Size = new System.Drawing.Size(131, 17);
            this.chkTestMode.TabIndex = 12;
            this.chkTestMode.Text = "Контрольный расчет";
            this.chkTestMode.UseVisualStyleBackColor = true;
            this.chkTestMode.CheckedChanged += new System.EventHandler(this.chkTestMode_CheckedChanged);
            // 
            // lblMainDateOverDraft
            // 
            this.lblMainDateOverDraft.Location = new System.Drawing.Point(10, 122);
            this.lblMainDateOverDraft.Name = "lblMainDateOverDraft";
            this.lblMainDateOverDraft.Size = new System.Drawing.Size(379, 13);
            this.lblMainDateOverDraft.TabIndex = 13;
            // 
            // lblDateOverDraft
            // 
            this.lblDateOverDraft.Location = new System.Drawing.Point(10, 141);
            this.lblDateOverDraft.Name = "lblDateOverDraft";
            this.lblDateOverDraft.Size = new System.Drawing.Size(274, 13);
            this.lblDateOverDraft.TabIndex = 14;
            // 
            // lblDateTrn
            // 
            this.lblDateTrn.AutoSize = true;
            this.lblDateTrn.Location = new System.Drawing.Point(10, 161);
            this.lblDateTrn.Name = "lblDateTrn";
            this.lblDateTrn.Size = new System.Drawing.Size(84, 13);
            this.lblDateTrn.TabIndex = 16;
            this.lblDateTrn.Text = "Дата проводки";
            // 
            // dateOverDraft
            // 
            this.dateOverDraft.Location = new System.Drawing.Point(290, 138);
            this.dateOverDraft.MaxLength = 10;
            this.dateOverDraft.Name = "dateOverDraft";
            this.dateOverDraft.Size = new System.Drawing.Size(99, 20);
            this.dateOverDraft.TabIndex = 15;
            this.dateOverDraft.Text = "  .  .    ";
            this.dateOverDraft.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dateTrn
            // 
            this.dateTrn.Location = new System.Drawing.Point(290, 158);
            this.dateTrn.MaxLength = 10;
            this.dateTrn.Name = "dateTrn";
            this.dateTrn.Size = new System.Drawing.Size(99, 20);
            this.dateTrn.TabIndex = 17;
            this.dateTrn.Text = "  .  .    ";
            this.dateTrn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 241);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(379, 13);
            this.label5.TabIndex = 20;
            // 
            // lblTime
            // 
            this.lblTime.Location = new System.Drawing.Point(10, 221);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(379, 13);
            this.lblTime.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(379, 13);
            this.label4.TabIndex = 18;
            // 
            // timerChannel
            // 
            this.timerChannel.Tick += new System.EventHandler(this.timerChannel_Tick);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // UbsPlcardCalcProcFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 294);
            this.Name = "UbsPlcardCalcProcFrm";
            this.Text = "Шаблон формы";
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
        private Label lblChannelInfo6;
        private Label lblChannelInfo5;
        private Label lblChannelInfo4;
        private Label lblChannelInfo3;
        private Label lblChannelInfo2;
        private Label lblChannelInfo1;
        private Label lblChannelInfo0;
        private Label lblChannel;
        private RadioButton rbPaymentPercent;
        private RadioButton rbColectedPercent;
        private Label lblOperationCaption;
        private CheckBox chkTestMode;
        private Label lblMainDateOverDraft;
        private Label lblDateTrn;
        private Label lblDateOverDraft;
        private UbsControl.UbsCtrlDate dateTrn;
        private UbsControl.UbsCtrlDate dateOverDraft;
        private Label label5;
        private Label lblTime;
        private Label label4;
        private Timer timerChannel;
        private Timer timer;
    }
}

