using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    partial class UbsPlcardFileIOFrm
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.ubsCtrlInfo = new UbsControl.UbsCtrlInfo();
            this.lblDateLoad = new System.Windows.Forms.Label();
            this.dtLoadDate = new UbsControl.UbsCtrlDate();
            this.lblProcessing = new System.Windows.Forms.Label();
            this.cmbProcessing = new System.Windows.Forms.ComboBox();
            this.cmbMode = new System.Windows.Forms.ComboBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.linkFileLoad = new System.Windows.Forms.LinkLabel();
            this.txtPathFile = new System.Windows.Forms.TextBox();
            this.lblChannel = new System.Windows.Forms.Label();
            this.lblChannel1 = new System.Windows.Forms.Label();
            this.lblChannel2 = new System.Windows.Forms.Label();
            this.lblChannel3 = new System.Windows.Forms.Label();
            this.lblChannel4 = new System.Windows.Forms.Label();
            this.lblChannel5 = new System.Windows.Forms.Label();
            this.lblChannel6 = new System.Windows.Forms.Label();
            this.lblChannel7 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.lblResult);
            this.panelMain.Controls.Add(this.lblChannel7);
            this.panelMain.Controls.Add(this.lblChannel6);
            this.panelMain.Controls.Add(this.lblChannel5);
            this.panelMain.Controls.Add(this.lblChannel4);
            this.panelMain.Controls.Add(this.lblChannel3);
            this.panelMain.Controls.Add(this.lblChannel2);
            this.panelMain.Controls.Add(this.lblChannel1);
            this.panelMain.Controls.Add(this.lblChannel);
            this.panelMain.Controls.Add(this.txtPathFile);
            this.panelMain.Controls.Add(this.linkFileLoad);
            this.panelMain.Controls.Add(this.cmbMode);
            this.panelMain.Controls.Add(this.lblMode);
            this.panelMain.Controls.Add(this.cmbProcessing);
            this.panelMain.Controls.Add(this.lblProcessing);
            this.panelMain.Controls.Add(this.dtLoadDate);
            this.panelMain.Controls.Add(this.lblDateLoad);
            this.panelMain.Controls.Add(this.tableLayoutPanel);
            this.panelMain.Size = new System.Drawing.Size(380, 262);
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
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 230);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(380, 32);
            this.tableLayoutPanel.TabIndex = 100;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(207, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 26);
            this.btnSave.TabIndex = 101;
            this.btnSave.Text = "Загрузить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.CausesValidation = false;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(295, 3);
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
            this.ubsCtrlInfo.Size = new System.Drawing.Size(198, 13);
            this.ubsCtrlInfo.TabIndex = 1;
            this.ubsCtrlInfo.Text = "ubsCtrlInfo";
            this.ubsCtrlInfo.Visible = false;
            // 
            // lblDateLoad
            // 
            this.lblDateLoad.AutoSize = true;
            this.lblDateLoad.Location = new System.Drawing.Point(16, 13);
            this.lblDateLoad.Name = "lblDateLoad";
            this.lblDateLoad.Size = new System.Drawing.Size(82, 13);
            this.lblDateLoad.TabIndex = 1;
            this.lblDateLoad.Text = "Дата загрузки";
            // 
            // dtLoadDate
            // 
            this.dtLoadDate.Location = new System.Drawing.Point(265, 10);
            this.dtLoadDate.MaxLength = 10;
            this.dtLoadDate.Name = "dtLoadDate";
            this.dtLoadDate.Size = new System.Drawing.Size(100, 20);
            this.dtLoadDate.TabIndex = 2;
            this.dtLoadDate.Text = "  .  .    ";
            this.dtLoadDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblProcessing
            // 
            this.lblProcessing.AutoSize = true;
            this.lblProcessing.Location = new System.Drawing.Point(16, 41);
            this.lblProcessing.Name = "lblProcessing";
            this.lblProcessing.Size = new System.Drawing.Size(68, 13);
            this.lblProcessing.TabIndex = 3;
            this.lblProcessing.Text = "Процессинг";
            // 
            // cmbProcessing
            // 
            this.cmbProcessing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProcessing.FormattingEnabled = true;
            this.cmbProcessing.Location = new System.Drawing.Point(90, 38);
            this.cmbProcessing.Name = "cmbProcessing";
            this.cmbProcessing.Size = new System.Drawing.Size(275, 21);
            this.cmbProcessing.TabIndex = 4;
            this.cmbProcessing.SelectedIndexChanged += new System.EventHandler(this.cmbProcessing_SelectedIndexChanged);
            // 
            // cmbMode
            // 
            this.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMode.FormattingEnabled = true;
            this.cmbMode.Location = new System.Drawing.Point(16, 84);
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Size = new System.Drawing.Size(349, 21);
            this.cmbMode.TabIndex = 6;
            this.cmbMode.Visible = false;
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Location = new System.Drawing.Point(16, 68);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(131, 13);
            this.lblMode.TabIndex = 5;
            this.lblMode.Text = "Выгружаемые операции";
            this.lblMode.Visible = false;
            // 
            // linkFileLoad
            // 
            this.linkFileLoad.AutoSize = true;
            this.linkFileLoad.Location = new System.Drawing.Point(16, 112);
            this.linkFileLoad.Name = "linkFileLoad";
            this.linkFileLoad.Size = new System.Drawing.Size(106, 13);
            this.linkFileLoad.TabIndex = 7;
            this.linkFileLoad.TabStop = true;
            this.linkFileLoad.Text = "Файл для загрузки";
            this.linkFileLoad.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkFileLoad_LinkClicked);
            // 
            // txtPathFile
            // 
            this.txtPathFile.Location = new System.Drawing.Point(16, 129);
            this.txtPathFile.Name = "txtPathFile";
            this.txtPathFile.Size = new System.Drawing.Size(348, 20);
            this.txtPathFile.TabIndex = 8;
            // 
            // lblChannel
            // 
            this.lblChannel.AutoSize = true;
            this.lblChannel.Location = new System.Drawing.Point(171, 169);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(46, 13);
            this.lblChannel.TabIndex = 9;
            this.lblChannel.Text = "Каналы";
            // 
            // lblChannel1
            // 
            this.lblChannel1.AutoSize = true;
            this.lblChannel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannel1.Location = new System.Drawing.Point(223, 169);
            this.lblChannel1.Name = "lblChannel1";
            this.lblChannel1.Size = new System.Drawing.Size(15, 15);
            this.lblChannel1.TabIndex = 10;
            this.lblChannel1.Text = "1";
            // 
            // lblChannel2
            // 
            this.lblChannel2.AutoSize = true;
            this.lblChannel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannel2.Location = new System.Drawing.Point(244, 169);
            this.lblChannel2.Name = "lblChannel2";
            this.lblChannel2.Size = new System.Drawing.Size(15, 15);
            this.lblChannel2.TabIndex = 11;
            this.lblChannel2.Text = "2";
            // 
            // lblChannel3
            // 
            this.lblChannel3.AutoSize = true;
            this.lblChannel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannel3.Location = new System.Drawing.Point(265, 169);
            this.lblChannel3.Name = "lblChannel3";
            this.lblChannel3.Size = new System.Drawing.Size(15, 15);
            this.lblChannel3.TabIndex = 12;
            this.lblChannel3.Text = "3";
            // 
            // lblChannel4
            // 
            this.lblChannel4.AutoSize = true;
            this.lblChannel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannel4.Location = new System.Drawing.Point(286, 169);
            this.lblChannel4.Name = "lblChannel4";
            this.lblChannel4.Size = new System.Drawing.Size(15, 15);
            this.lblChannel4.TabIndex = 13;
            this.lblChannel4.Text = "4";
            // 
            // lblChannel5
            // 
            this.lblChannel5.AutoSize = true;
            this.lblChannel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannel5.Location = new System.Drawing.Point(307, 169);
            this.lblChannel5.Name = "lblChannel5";
            this.lblChannel5.Size = new System.Drawing.Size(15, 15);
            this.lblChannel5.TabIndex = 14;
            this.lblChannel5.Text = "5";
            // 
            // lblChannel6
            // 
            this.lblChannel6.AutoSize = true;
            this.lblChannel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannel6.Location = new System.Drawing.Point(328, 169);
            this.lblChannel6.Name = "lblChannel6";
            this.lblChannel6.Size = new System.Drawing.Size(15, 15);
            this.lblChannel6.TabIndex = 15;
            this.lblChannel6.Text = "6";
            // 
            // lblChannel7
            // 
            this.lblChannel7.AutoSize = true;
            this.lblChannel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannel7.Location = new System.Drawing.Point(349, 169);
            this.lblChannel7.Name = "lblChannel7";
            this.lblChannel7.Size = new System.Drawing.Size(15, 15);
            this.lblChannel7.TabIndex = 16;
            this.lblChannel7.Text = "7";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(19, 200);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(0, 13);
            this.lblResult.TabIndex = 17;
            // 
            // UbsPlcardFileIOFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 262);
            this.Name = "UbsPlcardFileIOFrm";
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
        private Label lblDateLoad;
        private TextBox txtPathFile;
        private LinkLabel linkFileLoad;
        private ComboBox cmbMode;
        private Label lblMode;
        private ComboBox cmbProcessing;
        private Label lblProcessing;
        private UbsControl.UbsCtrlDate dtLoadDate;
        private Label lblChannel7;
        private Label lblChannel6;
        private Label lblChannel5;
        private Label lblChannel4;
        private Label lblChannel3;
        private Label lblChannel2;
        private Label lblChannel1;
        private Label lblChannel;
        private Label lblResult;
    }
}

