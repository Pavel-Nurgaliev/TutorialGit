using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    partial class UbsOpCommissionFrm
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.ubsCtrlInfo = new UbsControl.UbsCtrlInfo();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.tabPageAddFields = new System.Windows.Forms.TabPage();
            this.ubsCtrlAddFields = new UbsControl.UbsCtrlFields();
            this.panelMain.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            this.tabPageAddFields.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.tabControl);
            this.panelMain.Controls.Add(this.tableLayoutPanel);
            this.panelMain.Size = new System.Drawing.Size(535, 379);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.CausesValidation = false;
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel.Controls.Add(this.btnSave, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.btnExit, 2, 0);
            this.tableLayoutPanel.Controls.Add(this.ubsCtrlInfo, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 324);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(535, 55);
            this.tableLayoutPanel.TabIndex = 100;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(308, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(109, 49);
            this.btnSave.TabIndex = 101;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.CausesValidation = false;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(423, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(109, 49);
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
            this.ubsCtrlInfo.Location = new System.Drawing.Point(3, 42);
            this.ubsCtrlInfo.Name = "ubsCtrlInfo";
            this.ubsCtrlInfo.Size = new System.Drawing.Size(299, 13);
            this.ubsCtrlInfo.TabIndex = 1;
            this.ubsCtrlInfo.Text = "ubsCtrlInfo";
            this.ubsCtrlInfo.Visible = false;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageMain);
            this.tabControl.Controls.Add(this.tabPageAddFields);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(535, 321);
            this.tabControl.TabIndex = 1;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.lblDesc);
            this.tabPageMain.Controls.Add(this.lblName);
            this.tabPageMain.Controls.Add(this.txtDesc);
            this.tabPageMain.Controls.Add(this.txtName);
            this.tabPageMain.Location = new System.Drawing.Point(4, 22);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(527, 295);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "Основные";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(6, 60);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(60, 13);
            this.lblDesc.TabIndex = 2;
            this.lblDesc.Text = "Описание:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(86, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Наименование:";
            // 
            // txbDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(6, 76);
            this.txtDesc.Name = "txbDesc";
            this.txtDesc.Size = new System.Drawing.Size(475, 20);
            this.txtDesc.TabIndex = 3;
            // 
            // txbName
            // 
            this.txtName.Location = new System.Drawing.Point(6, 32);
            this.txtName.Name = "txbName";
            this.txtName.Size = new System.Drawing.Size(475, 20);
            this.txtName.TabIndex = 1;
            // 
            // tabPageAddFields
            // 
            this.tabPageAddFields.Controls.Add(this.ubsCtrlAddFields);
            this.tabPageAddFields.Location = new System.Drawing.Point(4, 22);
            this.tabPageAddFields.Name = "tabPageAddFields";
            this.tabPageAddFields.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAddFields.Size = new System.Drawing.Size(527, 295);
            this.tabPageAddFields.TabIndex = 1;
            this.tabPageAddFields.Text = "Доп. поля";
            this.tabPageAddFields.UseVisualStyleBackColor = true;
            // 
            // ubsCtrlAddFields
            // 
            this.ubsCtrlAddFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ubsCtrlAddFields.Location = new System.Drawing.Point(3, 3);
            this.ubsCtrlAddFields.Name = "ubsCtrlAddFields";
            this.ubsCtrlAddFields.ReadOnly = false;
            this.ubsCtrlAddFields.Size = new System.Drawing.Size(521, 289);
            this.ubsCtrlAddFields.TabIndex = 0;
            // 
            // UbsOpCommissionFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 379);
            this.Name = "UbsOpCommissionFrm";
            this.Text = "Комиссия";
            this.panelMain.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.tabPageMain.PerformLayout();
            this.tabPageAddFields.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button btnExit;
        private UbsControl.UbsCtrlInfo ubsCtrlInfo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.TabPage tabPageAddFields;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtDesc;
        private UbsControl.UbsCtrlFields ubsCtrlAddFields;
    }
}
