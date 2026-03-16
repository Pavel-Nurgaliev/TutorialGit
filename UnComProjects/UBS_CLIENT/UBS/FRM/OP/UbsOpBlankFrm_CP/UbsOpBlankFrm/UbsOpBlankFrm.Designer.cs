using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    partial class UbsOpBlankFrm
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.ubsCtrlInfo = new UbsControl.UbsCtrlInfo();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblSer = new System.Windows.Forms.Label();
            this.lblNum = new System.Windows.Forms.Label();
            this.lblNameVal = new System.Windows.Forms.Label();
            this.lblKindVal = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.txbSer = new System.Windows.Forms.TextBox();
            this.txbNum = new System.Windows.Forms.TextBox();
            this.txbNameVal = new System.Windows.Forms.TextBox();
            this.txbKindVal = new System.Windows.Forms.TextBox();
            this.cmbState = new System.Windows.Forms.ComboBox();
            this.tabPageAddFields = new System.Windows.Forms.TabPage();
            this.ubsCtrlAddFields = new UbsControl.UbsCtrlFields();
            this.dateCalc = new UbsControl.UbsCtrlDate();
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
            this.panelMain.Size = new System.Drawing.Size(501, 374);
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
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 342);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(501, 32);
            this.tableLayoutPanel.TabIndex = 100;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(328, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 26);
            this.btnSave.TabIndex = 101;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.CausesValidation = false;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(416, 3);
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
            this.ubsCtrlInfo.Size = new System.Drawing.Size(319, 13);
            this.ubsCtrlInfo.TabIndex = 1;
            this.ubsCtrlInfo.Text = "ubsCtrlInfo";
            this.ubsCtrlInfo.Visible = false;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageMain);
            this.tabControl.Controls.Add(this.tabPageAddFields);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(501, 342);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.dateCalc);
            this.tabPageMain.Controls.Add(this.lblDate);
            this.tabPageMain.Controls.Add(this.lblSer);
            this.tabPageMain.Controls.Add(this.lblNum);
            this.tabPageMain.Controls.Add(this.lblNameVal);
            this.tabPageMain.Controls.Add(this.lblKindVal);
            this.tabPageMain.Controls.Add(this.lblState);
            this.tabPageMain.Controls.Add(this.txbSer);
            this.tabPageMain.Controls.Add(this.txbNum);
            this.tabPageMain.Controls.Add(this.txbNameVal);
            this.tabPageMain.Controls.Add(this.txbKindVal);
            this.tabPageMain.Controls.Add(this.cmbState);
            this.tabPageMain.Location = new System.Drawing.Point(4, 22);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMain.Size = new System.Drawing.Size(493, 316);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "Основные";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(21, 12);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(133, 13);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "Дата принятия ценности";
            // 
            // lblSer
            // 
            this.lblSer.AutoSize = true;
            this.lblSer.Location = new System.Drawing.Point(116, 44);
            this.lblSer.Name = "lblSer";
            this.lblSer.Size = new System.Drawing.Size(38, 13);
            this.lblSer.TabIndex = 2;
            this.lblSer.Text = "Серия";
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(301, 44);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(41, 13);
            this.lblNum.TabIndex = 4;
            this.lblNum.Text = "Номер";
            // 
            // lblNameVal
            // 
            this.lblNameVal.AutoSize = true;
            this.lblNameVal.Location = new System.Drawing.Point(21, 76);
            this.lblNameVal.Name = "lblNameVal";
            this.lblNameVal.Size = new System.Drawing.Size(133, 13);
            this.lblNameVal.TabIndex = 6;
            this.lblNameVal.Text = "Наименование ценности";
            // 
            // lblKindVal
            // 
            this.lblKindVal.AutoSize = true;
            this.lblKindVal.Location = new System.Drawing.Point(78, 108);
            this.lblKindVal.Name = "lblKindVal";
            this.lblKindVal.Size = new System.Drawing.Size(76, 13);
            this.lblKindVal.TabIndex = 8;
            this.lblKindVal.Text = "Вид ценности";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Location = new System.Drawing.Point(93, 140);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(61, 13);
            this.lblState.TabIndex = 10;
            this.lblState.Text = "Состояние";
            // 
            // txbSer
            // 
            this.txbSer.Enabled = false;
            this.txbSer.Location = new System.Drawing.Point(160, 40);
            this.txbSer.Name = "txbSer";
            this.txbSer.Size = new System.Drawing.Size(137, 20);
            this.txbSer.TabIndex = 3;
            // 
            // txbNum
            // 
            this.txbNum.Enabled = false;
            this.txbNum.Location = new System.Drawing.Point(348, 40);
            this.txbNum.Name = "txbNum";
            this.txbNum.Size = new System.Drawing.Size(137, 20);
            this.txbNum.TabIndex = 5;
            // 
            // txbNameVal
            // 
            this.txbNameVal.Enabled = false;
            this.txbNameVal.Location = new System.Drawing.Point(160, 72);
            this.txbNameVal.Name = "txbNameVal";
            this.txbNameVal.Size = new System.Drawing.Size(325, 20);
            this.txbNameVal.TabIndex = 7;
            // 
            // txbKindVal
            // 
            this.txbKindVal.Enabled = false;
            this.txbKindVal.Location = new System.Drawing.Point(160, 104);
            this.txbKindVal.Name = "txbKindVal";
            this.txbKindVal.Size = new System.Drawing.Size(325, 20);
            this.txbKindVal.TabIndex = 9;
            // 
            // cmbState
            // 
            this.cmbState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbState.FormattingEnabled = true;
            this.cmbState.Location = new System.Drawing.Point(160, 136);
            this.cmbState.Name = "cmbState";
            this.cmbState.Size = new System.Drawing.Size(325, 21);
            this.cmbState.TabIndex = 11;
            // 
            // tabPageAddFields
            // 
            this.tabPageAddFields.Controls.Add(this.ubsCtrlAddFields);
            this.tabPageAddFields.Location = new System.Drawing.Point(4, 22);
            this.tabPageAddFields.Name = "tabPageAddFields";
            this.tabPageAddFields.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAddFields.Size = new System.Drawing.Size(444, 316);
            this.tabPageAddFields.TabIndex = 1;
            this.tabPageAddFields.Text = "Набор параметров";
            this.tabPageAddFields.UseVisualStyleBackColor = true;
            // 
            // ubsCtrlAddFields
            // 
            this.ubsCtrlAddFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ubsCtrlAddFields.Location = new System.Drawing.Point(3, 3);
            this.ubsCtrlAddFields.Name = "ubsCtrlAddFields";
            this.ubsCtrlAddFields.ReadOnly = false;
            this.ubsCtrlAddFields.Size = new System.Drawing.Size(438, 310);
            this.ubsCtrlAddFields.TabIndex = 0;
            // 
            // txbDateCalc
            // 
            this.dateCalc.Location = new System.Drawing.Point(160, 9);
            this.dateCalc.MaxLength = 10;
            this.dateCalc.Name = "txbDateCalc";
            this.dateCalc.Size = new System.Drawing.Size(100, 20);
            this.dateCalc.TabIndex = 1;
            this.dateCalc.Text = "  .  .    ";
            this.dateCalc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UbsForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 374);
            this.Name = "UbsForm1";
            this.Text = "Принятая ценность";
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
        private Button btnSave;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.TabPage tabPageAddFields;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblSer;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.Label lblNameVal;
        private System.Windows.Forms.Label lblKindVal;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.TextBox txbSer;
        private System.Windows.Forms.TextBox txbNum;
        private System.Windows.Forms.TextBox txbNameVal;
        private System.Windows.Forms.TextBox txbKindVal;
        private System.Windows.Forms.ComboBox cmbState;
        private UbsControl.UbsCtrlFields ubsCtrlAddFields;
        private UbsControl.UbsCtrlDate dateCalc;
    }
}

