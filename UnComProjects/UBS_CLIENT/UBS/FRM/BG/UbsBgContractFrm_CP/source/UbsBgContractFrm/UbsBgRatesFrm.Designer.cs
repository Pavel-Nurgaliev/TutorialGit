using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    partial class UbsBgRatesFrm
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
            this.cbRateTypes = new System.Windows.Forms.ComboBox();
            this.ubsCtrlDateRate = new UbsControl.UbsCtrlDate();
            this.ubsCtrlRate = new UbsControl.UbsCtrlDecimal();
            this.lblRateType = new System.Windows.Forms.Label();
            this.lblDateRate = new System.Windows.Forms.Label();
            this.lblRate = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.lblRate);
            this.panelMain.Controls.Add(this.lblDateRate);
            this.panelMain.Controls.Add(this.lblRateType);
            this.panelMain.Controls.Add(this.ubsCtrlRate);
            this.panelMain.Controls.Add(this.ubsCtrlDateRate);
            this.panelMain.Controls.Add(this.cbRateTypes);
            this.panelMain.Controls.Add(this.tableLayoutPanel);
            this.panelMain.Size = new System.Drawing.Size(429, 130);
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
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 98);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(429, 32);
            this.tableLayoutPanel.TabIndex = 100;
            // 
            // btnApply
            // 
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnApply.Location = new System.Drawing.Point(256, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(82, 26);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "Применить";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnExit
            // 
            this.btnExit.CausesValidation = false;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(344, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 26);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Отмена";
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
            this.ubsCtrlInfo.Size = new System.Drawing.Size(247, 13);
            this.ubsCtrlInfo.TabIndex = 1;
            this.ubsCtrlInfo.Text = "ubsCtrlInfo";
            this.ubsCtrlInfo.Visible = false;
            // 
            // cbRateTypes
            // 
            this.cbRateTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRateTypes.Enabled = false;
            this.cbRateTypes.FormattingEnabled = true;
            this.cbRateTypes.Location = new System.Drawing.Point(144, 12);
            this.cbRateTypes.Name = "cbRateTypes";
            this.cbRateTypes.Size = new System.Drawing.Size(277, 21);
            this.cbRateTypes.TabIndex = 0;
            // 
            // ubsCtrlDateRate
            // 
            this.ubsCtrlDateRate.Location = new System.Drawing.Point(144, 39);
            this.ubsCtrlDateRate.MaxLength = 10;
            this.ubsCtrlDateRate.Name = "ubsCtrlDateRate";
            this.ubsCtrlDateRate.Size = new System.Drawing.Size(121, 20);
            this.ubsCtrlDateRate.TabIndex = 1;
            this.ubsCtrlDateRate.Text = "  .  .    ";
            // 
            // ubsCtrlRate
            // 
            this.ubsCtrlRate.Location = new System.Drawing.Point(144, 65);
            this.ubsCtrlRate.Name = "ubsCtrlRate";
            this.ubsCtrlRate.Size = new System.Drawing.Size(157, 20);
            this.ubsCtrlRate.TabIndex = 2;
            this.ubsCtrlRate.Text = "0";
            // 
            // lblRateType
            // 
            this.lblRateType.Location = new System.Drawing.Point(36, 15);
            this.lblRateType.Name = "lblRateType";
            this.lblRateType.Size = new System.Drawing.Size(91, 13);
            this.lblRateType.TabIndex = 5;
            this.lblRateType.Text = "Тип ставки";
            this.lblRateType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDateRate
            // 
            this.lblDateRate.Location = new System.Drawing.Point(0, 42);
            this.lblDateRate.Name = "lblDateRate";
            this.lblDateRate.Size = new System.Drawing.Size(127, 13);
            this.lblDateRate.TabIndex = 6;
            this.lblDateRate.Text = "Дата установки";
            this.lblDateRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRate
            // 
            this.lblRate.Location = new System.Drawing.Point(36, 68);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(91, 13);
            this.lblRate.TabIndex = 7;
            this.lblRate.Text = "Ставка";
            this.lblRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UbsBgRatesFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 130);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UbsBgRatesFrm";
            this.ShowInTaskbar = false;
            this.Text = "Ставка";
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
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ComboBox cbRateTypes;
        private UbsControl.UbsCtrlDate ubsCtrlDateRate;
        private UbsControl.UbsCtrlDecimal ubsCtrlRate;
        private System.Windows.Forms.Label lblRateType;
        private System.Windows.Forms.Label lblDateRate;
        private System.Windows.Forms.Label lblRate;
    }
}
