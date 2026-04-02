using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace UbsBusiness
{
    partial class FrmInstr
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
            this.lvwInstrOplata = new System.Windows.Forms.ListView();
            this.colBankBic = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colBankName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colKS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colClientName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNote = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colINN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDirectDebit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelMain.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.lvwInstrOplata);
            this.panelMain.Controls.Add(this.tableLayoutPanel);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4);
            this.panelMain.Size = new System.Drawing.Size(923, 623);
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
            this.tableLayoutPanel.Size = new System.Drawing.Size(923, 32);
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
            this.ubsCtrlInfo.Size = new System.Drawing.Size(741, 13);
            this.ubsCtrlInfo.TabIndex = 1;
            this.ubsCtrlInfo.Text = "ubsCtrlInfo";
            this.ubsCtrlInfo.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(750, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 26);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Выбор";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.CausesValidation = false;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(838, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 26);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lvwInstrOplata
            // 
            this.lvwInstrOplata.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colBankBic,
            this.colBankName,
            this.colKS,
            this.colRS,
            this.colClientName,
            this.colNote,
            this.colINN,
            this.colDirectDebit});
            this.lvwInstrOplata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwInstrOplata.FullRowSelect = true;
            this.lvwInstrOplata.GridLines = true;
            this.lvwInstrOplata.HideSelection = false;
            this.lvwInstrOplata.Location = new System.Drawing.Point(0, 0);
            this.lvwInstrOplata.MultiSelect = false;
            this.lvwInstrOplata.Name = "lvwInstrOplata";
            this.lvwInstrOplata.Size = new System.Drawing.Size(923, 591);
            this.lvwInstrOplata.TabIndex = 101;
            this.lvwInstrOplata.UseCompatibleStateImageBehavior = false;
            this.lvwInstrOplata.View = System.Windows.Forms.View.Details;
            this.lvwInstrOplata.DoubleClick += new System.EventHandler(this.lvwInstrOplata_DoubleClick);
            // 
            // colBankBic
            // 
            this.colBankBic.Text = "БИК банка";
            this.colBankBic.Width = 96;
            // 
            // colBankName
            // 
            this.colBankName.Text = "Наименование банка";
            this.colBankName.Width = 141;
            // 
            // colKS
            // 
            this.colKS.Text = "К/С";
            this.colKS.Width = 96;
            // 
            // colRS
            // 
            this.colRS.Text = "Р/С";
            this.colRS.Width = 96;
            // 
            // colClientName
            // 
            this.colClientName.Text = "Наименование клиента";
            this.colClientName.Width = 138;
            // 
            // colNote
            // 
            this.colNote.Text = "Примечание";
            this.colNote.Width = 96;
            // 
            // colINN
            // 
            this.colINN.Text = "ИНН";
            this.colINN.Width = 96;
            // 
            // colDirectDebit
            // 
            this.colDirectDebit.Text = "Безакцептное списание";
            this.colDirectDebit.Width = 157;
            // 
            // FrmInstr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 623);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmInstr";
            this.Text = "Инструкции по оплате";
            this.panelMain.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        // ── Bottom strip ──────────────────────────────────────────────────────────
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private UbsControl.UbsCtrlInfo ubsCtrlInfo;
        private ListView lvwInstrOplata;
        private ColumnHeader colBankBic;
        private ColumnHeader colBankName;
        private ColumnHeader colKS;
        private ColumnHeader colRS;
        private ColumnHeader colClientName;
        private ColumnHeader colNote;
        private ColumnHeader colINN;
        private ColumnHeader colDirectDebit;
    }
}
