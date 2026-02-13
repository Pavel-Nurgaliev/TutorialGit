using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    partial class UbsBgBonusPayIntervalFrm
    {
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
            this.btnApply = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.ubsCtrlInfo = new UbsControl.UbsCtrlInfo();
            this.gbPeriod = new System.Windows.Forms.GroupBox();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.lblPeriodType = new System.Windows.Forms.Label();
            this.ubsCtrlPeriod = new UbsControl.UbsCtrlDecimal();
            this.cbTypePeriod = new System.Windows.Forms.ComboBox();
            this.gbDate = new System.Windows.Forms.GroupBox();
            this.lblDayNumber = new System.Windows.Forms.Label();
            this.lblDayType = new System.Windows.Forms.Label();
            this.chkDay0 = new System.Windows.Forms.CheckBox();
            this.chkDay1 = new System.Windows.Forms.CheckBox();
            this.chkDay2 = new System.Windows.Forms.CheckBox();
            this.chkDay3 = new System.Windows.Forms.CheckBox();
            this.chkDay4 = new System.Windows.Forms.CheckBox();
            this.chkDay5 = new System.Windows.Forms.CheckBox();
            this.chkDay6 = new System.Windows.Forms.CheckBox();
            this.ubsCtrlNumDay = new UbsControl.UbsCtrlDecimal();
            this.cbTypeDate = new System.Windows.Forms.ComboBox();
            this.panelMain.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.gbPeriod.SuspendLayout();
            this.gbDate.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.gbDate);
            this.panelMain.Controls.Add(this.gbPeriod);
            this.panelMain.Controls.Add(this.tableLayoutPanel);
            this.panelMain.Size = new System.Drawing.Size(514, 229);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.CausesValidation = false;
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel.Controls.Add(this.btnApply, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.btnExit, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.ubsCtrlInfo, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 197);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(514, 32);
            this.tableLayoutPanel.TabIndex = 100;
            // 
            // btnApply
            // 
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnApply.Location = new System.Drawing.Point(341, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(82, 26);
            this.btnApply.TabIndex = 12;
            this.btnApply.Text = "Применить";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnExit
            // 
            this.btnExit.CausesValidation = false;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(429, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 26);
            this.btnExit.TabIndex = 13;
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
            this.ubsCtrlInfo.Size = new System.Drawing.Size(332, 13);
            this.ubsCtrlInfo.TabIndex = 1;
            this.ubsCtrlInfo.Text = "ubsCtrlInfo";
            this.ubsCtrlInfo.Visible = false;
            // 
            // gbPeriod
            // 
            this.gbPeriod.Controls.Add(this.lblPeriod);
            this.gbPeriod.Controls.Add(this.lblPeriodType);
            this.gbPeriod.Controls.Add(this.ubsCtrlPeriod);
            this.gbPeriod.Controls.Add(this.cbTypePeriod);
            this.gbPeriod.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPeriod.Location = new System.Drawing.Point(0, 0);
            this.gbPeriod.Name = "gbPeriod";
            this.gbPeriod.Size = new System.Drawing.Size(514, 81);
            this.gbPeriod.TabIndex = 0;
            this.gbPeriod.TabStop = false;
            this.gbPeriod.Text = "Период";
            // 
            // lblPeriod
            // 
            this.lblPeriod.Location = new System.Drawing.Point(18, 54);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(61, 13);
            this.lblPeriod.TabIndex = 15;
            this.lblPeriod.Text = "Период";
            this.lblPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPeriodType
            // 
            this.lblPeriodType.Location = new System.Drawing.Point(36, 27);
            this.lblPeriodType.Name = "lblPeriodType";
            this.lblPeriodType.Size = new System.Drawing.Size(43, 13);
            this.lblPeriodType.TabIndex = 14;
            this.lblPeriodType.Text = "Тип";
            this.lblPeriodType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ubsCtrlPeriod
            // 
            this.ubsCtrlPeriod.Location = new System.Drawing.Point(84, 51);
            this.ubsCtrlPeriod.Name = "ubsCtrlPeriod";
            this.ubsCtrlPeriod.Size = new System.Drawing.Size(55, 20);
            this.ubsCtrlPeriod.TabIndex = 2;
            this.ubsCtrlPeriod.Text = "0";
            // 
            // cbTypePeriod
            // 
            this.cbTypePeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypePeriod.FormattingEnabled = true;
            this.cbTypePeriod.Location = new System.Drawing.Point(84, 24);
            this.cbTypePeriod.Name = "cbTypePeriod";
            this.cbTypePeriod.Size = new System.Drawing.Size(403, 21);
            this.cbTypePeriod.TabIndex = 1;
            this.cbTypePeriod.SelectedIndexChanged += new System.EventHandler(this.cbTypePeriod_SelectedIndexChanged);
            // 
            // gbDate
            // 
            this.gbDate.Controls.Add(this.lblDayNumber);
            this.gbDate.Controls.Add(this.lblDayType);
            this.gbDate.Controls.Add(this.chkDay0);
            this.gbDate.Controls.Add(this.chkDay1);
            this.gbDate.Controls.Add(this.chkDay2);
            this.gbDate.Controls.Add(this.chkDay3);
            this.gbDate.Controls.Add(this.chkDay4);
            this.gbDate.Controls.Add(this.chkDay5);
            this.gbDate.Controls.Add(this.chkDay6);
            this.gbDate.Controls.Add(this.ubsCtrlNumDay);
            this.gbDate.Controls.Add(this.cbTypeDate);
            this.gbDate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbDate.Location = new System.Drawing.Point(0, 89);
            this.gbDate.Name = "gbDate";
            this.gbDate.Size = new System.Drawing.Size(514, 108);
            this.gbDate.TabIndex = 16;
            this.gbDate.TabStop = false;
            this.gbDate.Text = "Дата гашений";
            // 
            // lblDayNumber
            // 
            this.lblDayNumber.Location = new System.Drawing.Point(6, 54);
            this.lblDayNumber.Name = "lblDayNumber";
            this.lblDayNumber.Size = new System.Drawing.Size(151, 13);
            this.lblDayNumber.TabIndex = 18;
            this.lblDayNumber.Text = "Номер дня периода";
            this.lblDayNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDayType
            // 
            this.lblDayType.Location = new System.Drawing.Point(42, 30);
            this.lblDayType.Name = "lblDayType";
            this.lblDayType.Size = new System.Drawing.Size(37, 13);
            this.lblDayType.TabIndex = 17;
            this.lblDayType.Text = "Тип";
            this.lblDayType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkDay0
            // 
            this.chkDay0.AutoSize = true;
            this.chkDay0.Enabled = false;
            this.chkDay0.Location = new System.Drawing.Point(12, 79);
            this.chkDay0.Name = "chkDay0";
            this.chkDay0.Size = new System.Drawing.Size(40, 17);
            this.chkDay0.TabIndex = 5;
            this.chkDay0.Text = "Пн";
            this.chkDay0.UseVisualStyleBackColor = true;
            // 
            // chkDay1
            // 
            this.chkDay1.AutoSize = true;
            this.chkDay1.Enabled = false;
            this.chkDay1.Location = new System.Drawing.Point(84, 79);
            this.chkDay1.Name = "chkDay1";
            this.chkDay1.Size = new System.Drawing.Size(38, 17);
            this.chkDay1.TabIndex = 6;
            this.chkDay1.Text = "Вт";
            this.chkDay1.UseVisualStyleBackColor = true;
            // 
            // chkDay2
            // 
            this.chkDay2.AutoSize = true;
            this.chkDay2.Enabled = false;
            this.chkDay2.Location = new System.Drawing.Point(156, 79);
            this.chkDay2.Name = "chkDay2";
            this.chkDay2.Size = new System.Drawing.Size(39, 17);
            this.chkDay2.TabIndex = 7;
            this.chkDay2.Text = "Ср";
            this.chkDay2.UseVisualStyleBackColor = true;
            // 
            // chkDay3
            // 
            this.chkDay3.AutoSize = true;
            this.chkDay3.Enabled = false;
            this.chkDay3.Location = new System.Drawing.Point(228, 79);
            this.chkDay3.Name = "chkDay3";
            this.chkDay3.Size = new System.Drawing.Size(39, 17);
            this.chkDay3.TabIndex = 8;
            this.chkDay3.Text = "Чт";
            this.chkDay3.UseVisualStyleBackColor = true;
            // 
            // chkDay4
            // 
            this.chkDay4.AutoSize = true;
            this.chkDay4.Enabled = false;
            this.chkDay4.Location = new System.Drawing.Point(300, 79);
            this.chkDay4.Name = "chkDay4";
            this.chkDay4.Size = new System.Drawing.Size(39, 17);
            this.chkDay4.TabIndex = 9;
            this.chkDay4.Text = "Пт";
            this.chkDay4.UseVisualStyleBackColor = true;
            // 
            // chkDay5
            // 
            this.chkDay5.AutoSize = true;
            this.chkDay5.Enabled = false;
            this.chkDay5.Location = new System.Drawing.Point(372, 79);
            this.chkDay5.Name = "chkDay5";
            this.chkDay5.Size = new System.Drawing.Size(39, 17);
            this.chkDay5.TabIndex = 10;
            this.chkDay5.Text = "Сб";
            this.chkDay5.UseVisualStyleBackColor = true;
            // 
            // chkDay6
            // 
            this.chkDay6.AutoSize = true;
            this.chkDay6.Enabled = false;
            this.chkDay6.Location = new System.Drawing.Point(444, 79);
            this.chkDay6.Name = "chkDay6";
            this.chkDay6.Size = new System.Drawing.Size(39, 17);
            this.chkDay6.TabIndex = 11;
            this.chkDay6.Text = "Вс";
            this.chkDay6.UseVisualStyleBackColor = true;
            // 
            // ubsCtrlNumDay
            // 
            this.ubsCtrlNumDay.Location = new System.Drawing.Point(162, 51);
            this.ubsCtrlNumDay.Name = "ubsCtrlNumDay";
            this.ubsCtrlNumDay.Size = new System.Drawing.Size(55, 20);
            this.ubsCtrlNumDay.TabIndex = 4;
            this.ubsCtrlNumDay.Text = "0";
            // 
            // cbTypeDate
            // 
            this.cbTypeDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeDate.FormattingEnabled = true;
            this.cbTypeDate.Location = new System.Drawing.Point(84, 24);
            this.cbTypeDate.Name = "cbTypeDate";
            this.cbTypeDate.Size = new System.Drawing.Size(403, 21);
            this.cbTypeDate.TabIndex = 3;
            this.cbTypeDate.SelectedIndexChanged += new System.EventHandler(this.cbTypeDate_SelectedIndexChanged);
            // 
            // UbsBgBonusPayIntervalFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 229);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UbsBgBonusPayIntervalFrm";
            this.Text = "Период";
            this.panelMain.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.gbPeriod.ResumeLayout(false);
            this.gbPeriod.PerformLayout();
            this.gbDate.ResumeLayout(false);
            this.gbDate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button btnExit;
        private UbsControl.UbsCtrlInfo ubsCtrlInfo;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.GroupBox gbPeriod;
        private System.Windows.Forms.ComboBox cbTypePeriod;
        private UbsControl.UbsCtrlDecimal ubsCtrlPeriod;
        private System.Windows.Forms.Label lblPeriodType;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.GroupBox gbDate;
        private System.Windows.Forms.ComboBox cbTypeDate;
        private UbsControl.UbsCtrlDecimal ubsCtrlNumDay;
        private System.Windows.Forms.CheckBox chkDay6;
        private System.Windows.Forms.CheckBox chkDay5;
        private System.Windows.Forms.CheckBox chkDay4;
        private System.Windows.Forms.CheckBox chkDay3;
        private System.Windows.Forms.CheckBox chkDay2;
        private System.Windows.Forms.CheckBox chkDay1;
        private System.Windows.Forms.CheckBox chkDay0;
        private System.Windows.Forms.Label lblDayType;
        private System.Windows.Forms.Label lblDayNumber;
    }
}
