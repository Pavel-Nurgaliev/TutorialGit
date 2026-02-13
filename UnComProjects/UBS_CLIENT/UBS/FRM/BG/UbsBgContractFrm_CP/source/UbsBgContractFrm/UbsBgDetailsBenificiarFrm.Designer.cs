using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    partial class UbsBgDetailsBenificiarFrm
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
            this.gbAddress = new System.Windows.Forms.GroupBox();
            this.tbFlat = new System.Windows.Forms.TextBox();
            this.tbIndex = new System.Windows.Forms.TextBox();
            this.cbTypeFlat = new System.Windows.Forms.ComboBox();
            this.lblIndex = new System.Windows.Forms.Label();
            this.tbHousing = new System.Windows.Forms.TextBox();
            this.cbTypeHousing = new System.Windows.Forms.ComboBox();
            this.tbHome = new System.Windows.Forms.TextBox();
            this.cbTypeHome = new System.Windows.Forms.ComboBox();
            this.tbStreet = new System.Windows.Forms.TextBox();
            this.cbTypeStreet = new System.Windows.Forms.ComboBox();
            this.tbSettl = new System.Windows.Forms.TextBox();
            this.cbTypeSettl = new System.Windows.Forms.ComboBox();
            this.tbCity = new System.Windows.Forms.TextBox();
            this.cbTypeCity = new System.Windows.Forms.ComboBox();
            this.tbArea = new System.Windows.Forms.TextBox();
            this.cbTypeArea = new System.Windows.Forms.ComboBox();
            this.tbRegion = new System.Windows.Forms.TextBox();
            this.cbTypeRegion = new System.Windows.Forms.ComboBox();
            this.cbCountry = new System.Windows.Forms.ComboBox();
            this.cbCodeCountry = new System.Windows.Forms.ComboBox();
            this.lblStreet = new System.Windows.Forms.Label();
            this.lblSettl = new System.Windows.Forms.Label();
            this.lblCity = new System.Windows.Forms.Label();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblRegion = new System.Windows.Forms.Label();
            this.lblCountry = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbINN = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblINN = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.gbAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.gbAddress);
            this.panelMain.Controls.Add(this.tbINN);
            this.panelMain.Controls.Add(this.lblINN);
            this.panelMain.Controls.Add(this.tbName);
            this.panelMain.Controls.Add(this.lblName);
            this.panelMain.Controls.Add(this.tableLayoutPanel);
            this.panelMain.Size = new System.Drawing.Size(734, 332);
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
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 300);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(734, 32);
            this.tableLayoutPanel.TabIndex = 100;
            // 
            // btnApply
            // 
            this.btnApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnApply.Location = new System.Drawing.Point(561, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(82, 26);
            this.btnApply.TabIndex = 31;
            this.btnApply.Text = "Применить";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnExit
            // 
            this.btnExit.CausesValidation = false;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Location = new System.Drawing.Point(649, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 26);
            this.btnExit.TabIndex = 32;
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
            this.ubsCtrlInfo.Size = new System.Drawing.Size(552, 13);
            this.ubsCtrlInfo.TabIndex = 1;
            this.ubsCtrlInfo.Text = "ubsCtrlInfo";
            this.ubsCtrlInfo.Visible = false;
            // 
            // gbAddress
            // 
            this.gbAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAddress.Controls.Add(this.tbFlat);
            this.gbAddress.Controls.Add(this.tbIndex);
            this.gbAddress.Controls.Add(this.cbTypeFlat);
            this.gbAddress.Controls.Add(this.lblIndex);
            this.gbAddress.Controls.Add(this.tbHousing);
            this.gbAddress.Controls.Add(this.cbTypeHousing);
            this.gbAddress.Controls.Add(this.tbHome);
            this.gbAddress.Controls.Add(this.cbTypeHome);
            this.gbAddress.Controls.Add(this.tbStreet);
            this.gbAddress.Controls.Add(this.cbTypeStreet);
            this.gbAddress.Controls.Add(this.tbSettl);
            this.gbAddress.Controls.Add(this.cbTypeSettl);
            this.gbAddress.Controls.Add(this.tbCity);
            this.gbAddress.Controls.Add(this.cbTypeCity);
            this.gbAddress.Controls.Add(this.tbArea);
            this.gbAddress.Controls.Add(this.cbTypeArea);
            this.gbAddress.Controls.Add(this.tbRegion);
            this.gbAddress.Controls.Add(this.cbTypeRegion);
            this.gbAddress.Controls.Add(this.cbCountry);
            this.gbAddress.Controls.Add(this.cbCodeCountry);
            this.gbAddress.Controls.Add(this.lblStreet);
            this.gbAddress.Controls.Add(this.lblSettl);
            this.gbAddress.Controls.Add(this.lblCity);
            this.gbAddress.Controls.Add(this.lblArea);
            this.gbAddress.Controls.Add(this.lblRegion);
            this.gbAddress.Controls.Add(this.lblCountry);
            this.gbAddress.Location = new System.Drawing.Point(6, 65);
            this.gbAddress.Name = "gbAddress";
            this.gbAddress.Size = new System.Drawing.Size(710, 229);
            this.gbAddress.TabIndex = 4;
            this.gbAddress.TabStop = false;
            this.gbAddress.Text = "Адрес";
            // 
            // tbFlat
            // 
            this.tbFlat.Location = new System.Drawing.Point(600, 200);
            this.tbFlat.Name = "tbFlat";
            this.tbFlat.Size = new System.Drawing.Size(97, 20);
            this.tbFlat.TabIndex = 30;
            // 
            // tbIndex
            // 
            this.tbIndex.Location = new System.Drawing.Point(591, 13);
            this.tbIndex.Name = "tbIndex";
            this.tbIndex.Size = new System.Drawing.Size(109, 20);
            this.tbIndex.TabIndex = 6;
            // 
            // cbTypeFlat
            // 
            this.cbTypeFlat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeFlat.FormattingEnabled = true;
            this.cbTypeFlat.Location = new System.Drawing.Point(494, 199);
            this.cbTypeFlat.Name = "cbTypeFlat";
            this.cbTypeFlat.Size = new System.Drawing.Size(97, 21);
            this.cbTypeFlat.TabIndex = 29;
            // 
            // lblIndex
            // 
            this.lblIndex.AutoSize = true;
            this.lblIndex.Location = new System.Drawing.Point(540, 16);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Size = new System.Drawing.Size(45, 13);
            this.lblIndex.TabIndex = 5;
            this.lblIndex.Text = "Индекс";
            // 
            // tbHousing
            // 
            this.tbHousing.Location = new System.Drawing.Point(379, 200);
            this.tbHousing.Name = "tbHousing";
            this.tbHousing.Size = new System.Drawing.Size(109, 20);
            this.tbHousing.TabIndex = 28;
            // 
            // cbTypeHousing
            // 
            this.cbTypeHousing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeHousing.FormattingEnabled = true;
            this.cbTypeHousing.Location = new System.Drawing.Point(276, 199);
            this.cbTypeHousing.Name = "cbTypeHousing";
            this.cbTypeHousing.Size = new System.Drawing.Size(97, 21);
            this.cbTypeHousing.TabIndex = 27;
            // 
            // tbHome
            // 
            this.tbHome.Location = new System.Drawing.Point(120, 199);
            this.tbHome.Name = "tbHome";
            this.tbHome.Size = new System.Drawing.Size(145, 20);
            this.tbHome.TabIndex = 26;
            // 
            // cbTypeHome
            // 
            this.cbTypeHome.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeHome.FormattingEnabled = true;
            this.cbTypeHome.Location = new System.Drawing.Point(12, 199);
            this.cbTypeHome.Name = "cbTypeHome";
            this.cbTypeHome.Size = new System.Drawing.Size(106, 21);
            this.cbTypeHome.TabIndex = 25;
            // 
            // tbStreet
            // 
            this.tbStreet.Location = new System.Drawing.Point(279, 172);
            this.tbStreet.Name = "tbStreet";
            this.tbStreet.Size = new System.Drawing.Size(421, 20);
            this.tbStreet.TabIndex = 24;
            // 
            // cbTypeStreet
            // 
            this.cbTypeStreet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeStreet.FormattingEnabled = true;
            this.cbTypeStreet.Location = new System.Drawing.Point(120, 172);
            this.cbTypeStreet.Name = "cbTypeStreet";
            this.cbTypeStreet.Size = new System.Drawing.Size(145, 21);
            this.cbTypeStreet.TabIndex = 23;
            // 
            // tbSettl
            // 
            this.tbSettl.Location = new System.Drawing.Point(279, 146);
            this.tbSettl.Name = "tbSettl";
            this.tbSettl.Size = new System.Drawing.Size(421, 20);
            this.tbSettl.TabIndex = 21;
            // 
            // cbTypeSettl
            // 
            this.cbTypeSettl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeSettl.FormattingEnabled = true;
            this.cbTypeSettl.Location = new System.Drawing.Point(120, 146);
            this.cbTypeSettl.Name = "cbTypeSettl";
            this.cbTypeSettl.Size = new System.Drawing.Size(145, 21);
            this.cbTypeSettl.TabIndex = 20;
            // 
            // tbCity
            // 
            this.tbCity.Location = new System.Drawing.Point(279, 120);
            this.tbCity.Name = "tbCity";
            this.tbCity.Size = new System.Drawing.Size(421, 20);
            this.tbCity.TabIndex = 18;
            // 
            // cbTypeCity
            // 
            this.cbTypeCity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeCity.FormattingEnabled = true;
            this.cbTypeCity.Location = new System.Drawing.Point(120, 120);
            this.cbTypeCity.Name = "cbTypeCity";
            this.cbTypeCity.Size = new System.Drawing.Size(145, 21);
            this.cbTypeCity.TabIndex = 17;
            // 
            // tbArea
            // 
            this.tbArea.Location = new System.Drawing.Point(279, 92);
            this.tbArea.Name = "tbArea";
            this.tbArea.Size = new System.Drawing.Size(421, 20);
            this.tbArea.TabIndex = 15;
            // 
            // cbTypeArea
            // 
            this.cbTypeArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeArea.FormattingEnabled = true;
            this.cbTypeArea.Location = new System.Drawing.Point(120, 92);
            this.cbTypeArea.Name = "cbTypeArea";
            this.cbTypeArea.Size = new System.Drawing.Size(145, 21);
            this.cbTypeArea.TabIndex = 14;
            // 
            // tbRegion
            // 
            this.tbRegion.Location = new System.Drawing.Point(279, 66);
            this.tbRegion.Name = "tbRegion";
            this.tbRegion.Size = new System.Drawing.Size(421, 20);
            this.tbRegion.TabIndex = 12;
            // 
            // cbTypeRegion
            // 
            this.cbTypeRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTypeRegion.FormattingEnabled = true;
            this.cbTypeRegion.Location = new System.Drawing.Point(120, 66);
            this.cbTypeRegion.Name = "cbTypeRegion";
            this.cbTypeRegion.Size = new System.Drawing.Size(145, 21);
            this.cbTypeRegion.TabIndex = 11;
            // 
            // cbCountry
            // 
            this.cbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCountry.FormattingEnabled = true;
            this.cbCountry.Location = new System.Drawing.Point(279, 39);
            this.cbCountry.Name = "cbCountry";
            this.cbCountry.Size = new System.Drawing.Size(421, 21);
            this.cbCountry.TabIndex = 9;
            this.cbCountry.SelectedIndexChanged += new System.EventHandler(this.cbCountry_SelectedIndexChanged);
            // 
            // cbCodeCountry
            // 
            this.cbCodeCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCodeCountry.FormattingEnabled = true;
            this.cbCodeCountry.Location = new System.Drawing.Point(120, 39);
            this.cbCodeCountry.Name = "cbCodeCountry";
            this.cbCodeCountry.Size = new System.Drawing.Size(145, 21);
            this.cbCodeCountry.Sorted = true;
            this.cbCodeCountry.TabIndex = 8;
            this.cbCodeCountry.SelectedIndexChanged += new System.EventHandler(this.cbCodeCountry_SelectedIndexChanged);
            // 
            // lblStreet
            // 
            this.lblStreet.AutoSize = true;
            this.lblStreet.Location = new System.Drawing.Point(12, 175);
            this.lblStreet.Name = "lblStreet";
            this.lblStreet.Size = new System.Drawing.Size(39, 13);
            this.lblStreet.TabIndex = 22;
            this.lblStreet.Text = "Улица";
            // 
            // lblSettl
            // 
            this.lblSettl.AutoSize = true;
            this.lblSettl.Location = new System.Drawing.Point(12, 149);
            this.lblSettl.Name = "lblSettl";
            this.lblSettl.Size = new System.Drawing.Size(73, 13);
            this.lblSettl.TabIndex = 19;
            this.lblSettl.Text = "Насел. пункт";
            // 
            // lblCity
            // 
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(12, 123);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(37, 13);
            this.lblCity.TabIndex = 16;
            this.lblCity.Text = "Город";
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(12, 95);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(38, 13);
            this.lblArea.TabIndex = 13;
            this.lblArea.Text = "Район";
            // 
            // lblRegion
            // 
            this.lblRegion.AutoSize = true;
            this.lblRegion.Location = new System.Drawing.Point(12, 69);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(43, 13);
            this.lblRegion.TabIndex = 10;
            this.lblRegion.Text = "Регион";
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Location = new System.Drawing.Point(12, 42);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(43, 13);
            this.lblCountry.TabIndex = 7;
            this.lblCountry.Text = "Страна";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(136, 12);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(577, 20);
            this.tbName.TabIndex = 1;
            // 
            // tbINN
            // 
            this.tbINN.Location = new System.Drawing.Point(136, 39);
            this.tbINN.MaxLength = 12;
            this.tbINN.Name = "tbINN";
            this.tbINN.Size = new System.Drawing.Size(313, 20);
            this.tbINN.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(9, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(121, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Наименование";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblINN
            // 
            this.lblINN.Location = new System.Drawing.Point(9, 43);
            this.lblINN.Name = "lblINN";
            this.lblINN.Size = new System.Drawing.Size(121, 13);
            this.lblINN.TabIndex = 2;
            this.lblINN.Text = "ИНН";
            this.lblINN.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UbsBgDetailsBenificiarFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 332);
            this.Name = "UbsBgDetailsBenificiarFrm";
            this.Text = "Реквизиты бенефициара";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.gbAddress.ResumeLayout(false);
            this.gbAddress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button btnExit;
        private UbsControl.UbsCtrlInfo ubsCtrlInfo;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.GroupBox gbAddress;
        private System.Windows.Forms.TextBox tbFlat;
        private System.Windows.Forms.ComboBox cbTypeFlat;
        private System.Windows.Forms.TextBox tbHousing;
        private System.Windows.Forms.ComboBox cbTypeHousing;
        private System.Windows.Forms.TextBox tbHome;
        private System.Windows.Forms.ComboBox cbTypeHome;
        private System.Windows.Forms.TextBox tbStreet;
        private System.Windows.Forms.ComboBox cbTypeStreet;
        private System.Windows.Forms.TextBox tbSettl;
        private System.Windows.Forms.ComboBox cbTypeSettl;
        private System.Windows.Forms.TextBox tbCity;
        private System.Windows.Forms.ComboBox cbTypeCity;
        private System.Windows.Forms.TextBox tbArea;
        private System.Windows.Forms.ComboBox cbTypeArea;
        private System.Windows.Forms.TextBox tbRegion;
        private System.Windows.Forms.ComboBox cbTypeRegion;
        private System.Windows.Forms.ComboBox cbCountry;
        private System.Windows.Forms.ComboBox cbCodeCountry;
        private System.Windows.Forms.Label lblStreet;
        private System.Windows.Forms.Label lblSettl;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbINN;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblINN;
        private System.Windows.Forms.TextBox tbIndex;
        private System.Windows.Forms.Label lblIndex;
    }
}
