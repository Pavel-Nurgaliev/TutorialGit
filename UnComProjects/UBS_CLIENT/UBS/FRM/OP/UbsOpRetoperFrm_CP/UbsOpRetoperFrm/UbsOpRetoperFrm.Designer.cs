using System;
using System.Windows.Forms;

namespace UbsBusiness
{
    partial class UbsOpRetoperFrm
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
            this.contentPanel = new System.Windows.Forms.Panel();
            this.grpPayDoc = new System.Windows.Forms.GroupBox();
            this.lblPlDoc = new System.Windows.Forms.Label();
            this.lblSerPDoc = new System.Windows.Forms.Label();
            this.lblNumPDoc = new System.Windows.Forms.Label();
            this.lblValNom = new System.Windows.Forms.Label();
            this.lblNomPDoc = new System.Windows.Forms.Label();
            this.txbPlDoc = new System.Windows.Forms.TextBox();
            this.txbSerPDoc = new System.Windows.Forms.TextBox();
            this.txbValNom = new System.Windows.Forms.TextBox();
            this.numPDoc = new UbsControl.UbsCtrlDecimal();
            this.nomPDoc = new UbsControl.UbsCtrlDecimal();
            this.chkPayMoney = new System.Windows.Forms.CheckBox();
            this.lblVydat = new System.Windows.Forms.Label();
            this.valMinusCB = new System.Windows.Forms.ComboBox();
            this.sumMinusCur = new UbsControl.UbsCtrlDecimal();
            this.lblKurs = new System.Windows.Forms.Label();
            this.lblK = new System.Windows.Forms.Label();
            this.rateCur = new UbsControl.UbsCtrlDecimal();
            this.NU = new UbsControl.UbsCtrlDecimal();
            this.lblKomissija = new System.Windows.Forms.Label();
            this.valComis = new System.Windows.Forms.ComboBox();
            this.komCur = new UbsControl.UbsCtrlDecimal();
            this.lblPodNal = new System.Windows.Forms.Label();
            this.podNalCur = new UbsControl.UbsCtrlDecimal();
            this.lblNKvit = new System.Windows.Forms.Label();
            this.nKvit = new UbsControl.UbsCtrlDecimal();
            this.lblClient = new System.Windows.Forms.Label();
            this.clientTxt = new System.Windows.Forms.TextBox();
            this.btnClient = new System.Windows.Forms.Button();
            this.lblDoc = new System.Windows.Forms.Label();
            this.docCB = new System.Windows.Forms.ComboBox();
            this.lblSerTxt = new System.Windows.Forms.Label();
            this.serTxt = new System.Windows.Forms.TextBox();
            this.lblNumTxt = new System.Windows.Forms.Label();
            this.numTxt = new System.Windows.Forms.TextBox();
            this.resCHB = new System.Windows.Forms.CheckBox();
            this.panelMain.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.grpPayDoc.SuspendLayout();
            this.SuspendLayout();
            //
            // panelMain
            //
            this.panelMain.Controls.Add(this.contentPanel);
            this.panelMain.Controls.Add(this.tableLayoutPanel);
            this.panelMain.Size = new System.Drawing.Size(449, 281);
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
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 249);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(449, 32);
            this.tableLayoutPanel.TabIndex = 100;
            //
            // btnSave
            //
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Name = "btnSave";
            this.btnSave.TabIndex = 101;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnExit
            //
            this.btnExit.CausesValidation = false;
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Name = "btnExit";
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
            this.ubsCtrlInfo.Name = "ubsCtrlInfo";
            this.ubsCtrlInfo.TabIndex = 1;
            this.ubsCtrlInfo.Text = "ubsCtrlInfo";
            this.ubsCtrlInfo.Visible = false;
            //
            // contentPanel
            //
            this.contentPanel.AutoScroll = true;
            this.contentPanel.Controls.Add(this.grpPayDoc);
            this.contentPanel.Controls.Add(this.chkPayMoney);
            this.contentPanel.Controls.Add(this.lblVydat);
            this.contentPanel.Controls.Add(this.valMinusCB);
            this.contentPanel.Controls.Add(this.sumMinusCur);
            this.contentPanel.Controls.Add(this.lblKurs);
            this.contentPanel.Controls.Add(this.lblK);
            this.contentPanel.Controls.Add(this.rateCur);
            this.contentPanel.Controls.Add(this.NU);
            this.contentPanel.Controls.Add(this.lblKomissija);
            this.contentPanel.Controls.Add(this.valComis);
            this.contentPanel.Controls.Add(this.komCur);
            this.contentPanel.Controls.Add(this.lblPodNal);
            this.contentPanel.Controls.Add(this.podNalCur);
            this.contentPanel.Controls.Add(this.lblNKvit);
            this.contentPanel.Controls.Add(this.nKvit);
            this.contentPanel.Controls.Add(this.lblClient);
            this.contentPanel.Controls.Add(this.clientTxt);
            this.contentPanel.Controls.Add(this.btnClient);
            this.contentPanel.Controls.Add(this.lblDoc);
            this.contentPanel.Controls.Add(this.docCB);
            this.contentPanel.Controls.Add(this.lblSerTxt);
            this.contentPanel.Controls.Add(this.serTxt);
            this.contentPanel.Controls.Add(this.lblNumTxt);
            this.contentPanel.Controls.Add(this.numTxt);
            this.contentPanel.Controls.Add(this.resCHB);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(8);
            this.contentPanel.Size = new System.Drawing.Size(449, 249);
            this.contentPanel.TabIndex = 0;
            //
            // grpPayDoc
            //
            this.grpPayDoc.Controls.Add(this.lblPlDoc);
            this.grpPayDoc.Controls.Add(this.txbPlDoc);
            this.grpPayDoc.Controls.Add(this.lblSerPDoc);
            this.grpPayDoc.Controls.Add(this.lblNumPDoc);
            this.grpPayDoc.Controls.Add(this.txbSerPDoc);
            this.grpPayDoc.Controls.Add(this.numPDoc);
            this.grpPayDoc.Controls.Add(this.lblValNom);
            this.grpPayDoc.Controls.Add(this.lblNomPDoc);
            this.grpPayDoc.Controls.Add(this.txbValNom);
            this.grpPayDoc.Controls.Add(this.nomPDoc);
            this.grpPayDoc.Enabled = false;
            this.grpPayDoc.Location = new System.Drawing.Point(8, 8);
            this.grpPayDoc.Name = "grpPayDoc";
            this.grpPayDoc.Size = new System.Drawing.Size(433, 90);
            this.grpPayDoc.TabIndex = 0;
            this.grpPayDoc.TabStop = false;
            this.grpPayDoc.Text = "Платежный документ";
            //
            // lblPlDoc
            //
            this.lblPlDoc.AutoSize = true;
            this.lblPlDoc.Location = new System.Drawing.Point(6, 20);
            this.lblPlDoc.Name = "lblPlDoc";
            this.lblPlDoc.Size = new System.Drawing.Size(75, 13);
            this.lblPlDoc.TabIndex = 0;
            this.lblPlDoc.Text = "Наименование:";
            //
            // txbPlDoc
            //
            this.txbPlDoc.Enabled = false;
            this.txbPlDoc.Location = new System.Drawing.Point(104, 17);
            this.txbPlDoc.Name = "txbPlDoc";
            this.txbPlDoc.Size = new System.Drawing.Size(320, 20);
            this.txbPlDoc.TabIndex = 1;
            //
            // lblSerPDoc
            //
            this.lblSerPDoc.AutoSize = true;
            this.lblSerPDoc.Location = new System.Drawing.Point(48, 46);
            this.lblSerPDoc.Name = "lblSerPDoc";
            this.lblSerPDoc.Size = new System.Drawing.Size(38, 13);
            this.lblSerPDoc.TabIndex = 2;
            this.lblSerPDoc.Text = "Серия:";
            //
            // txbSerPDoc
            //
            this.txbSerPDoc.Enabled = false;
            this.txbSerPDoc.Location = new System.Drawing.Point(104, 43);
            this.txbSerPDoc.Name = "txbSerPDoc";
            this.txbSerPDoc.Size = new System.Drawing.Size(80, 20);
            this.txbSerPDoc.TabIndex = 3;
            //
            // lblNumPDoc
            //
            this.lblNumPDoc.AutoSize = true;
            this.lblNumPDoc.Location = new System.Drawing.Point(200, 46);
            this.lblNumPDoc.Name = "lblNumPDoc";
            this.lblNumPDoc.Size = new System.Drawing.Size(41, 13);
            this.lblNumPDoc.TabIndex = 4;
            this.lblNumPDoc.Text = "Номер:";
            //
            // numPDoc
            //
            this.numPDoc.Location = new System.Drawing.Point(247, 43);
            this.numPDoc.Name = "numPDoc";
            this.numPDoc.Size = new System.Drawing.Size(85, 20);
            this.numPDoc.TabIndex = 5;
            this.numPDoc.DecimalValue = 0m;
            //
            // lblValNom
            //
            this.lblValNom.AutoSize = true;
            this.lblValNom.Location = new System.Drawing.Point(6, 72);
            this.lblValNom.Name = "lblValNom";
            this.lblValNom.Size = new System.Drawing.Size(92, 13);
            this.lblValNom.TabIndex = 6;
            this.lblValNom.Text = "Валюта номинала:";
            //
            // txbValNom
            //
            this.txbValNom.Enabled = false;
            this.txbValNom.Location = new System.Drawing.Point(104, 69);
            this.txbValNom.Name = "txbValNom";
            this.txbValNom.Size = new System.Drawing.Size(80, 20);
            this.txbValNom.TabIndex = 7;
            //
            // lblNomPDoc
            //
            this.lblNomPDoc.AutoSize = true;
            this.lblNomPDoc.Location = new System.Drawing.Point(200, 72);
            this.lblNomPDoc.Name = "lblNomPDoc";
            this.lblNomPDoc.Size = new System.Drawing.Size(105, 13);
            this.lblNomPDoc.TabIndex = 8;
            this.lblNomPDoc.Text = "Сумма по номиналу:";
            //
            // nomPDoc
            //
            this.nomPDoc.Location = new System.Drawing.Point(311, 69);
            this.nomPDoc.Name = "nomPDoc";
            this.nomPDoc.Size = new System.Drawing.Size(113, 20);
            this.nomPDoc.TabIndex = 9;
            this.nomPDoc.DecimalValue = 0m;
            //
            // chkPayMoney
            //
            this.chkPayMoney.AutoSize = true;
            this.chkPayMoney.Location = new System.Drawing.Point(8, 105);
            this.chkPayMoney.Name = "chkPayMoney";
            this.chkPayMoney.Size = new System.Drawing.Size(210, 17);
            this.chkPayMoney.TabIndex = 10;
            this.chkPayMoney.Text = "Выплатить в денежном эквиваленте";
            this.chkPayMoney.UseVisualStyleBackColor = true;
            this.chkPayMoney.CheckedChanged += new System.EventHandler(this.chkPayMoney_CheckedChanged);
            //
            // lblVydat
            //
            this.lblVydat.AutoSize = true;
            this.lblVydat.Location = new System.Drawing.Point(8, 132);
            this.lblVydat.Name = "lblVydat";
            this.lblVydat.Size = new System.Drawing.Size(48, 13);
            this.lblVydat.TabIndex = 11;
            this.lblVydat.Text = "Выдать:";
            //
            // valMinusCB
            //
            this.valMinusCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.valMinusCB.FormattingEnabled = true;
            this.valMinusCB.Location = new System.Drawing.Point(72, 129);
            this.valMinusCB.Name = "valMinusCB";
            this.valMinusCB.Size = new System.Drawing.Size(70, 21);
            this.valMinusCB.TabIndex = 12;
            this.valMinusCB.SelectedIndexChanged += new System.EventHandler(this.valMinusCB_SelectedIndexChanged);
            //
            // sumMinusCur
            //
            this.sumMinusCur.Enabled = false;
            this.sumMinusCur.Location = new System.Drawing.Point(148, 129);
            this.sumMinusCur.Name = "sumMinusCur";
            this.sumMinusCur.Size = new System.Drawing.Size(90, 20);
            this.sumMinusCur.TabIndex = 13;
            this.sumMinusCur.DecimalValue = 0m;
            //
            // lblKurs
            //
            this.lblKurs.AutoSize = true;
            this.lblKurs.Location = new System.Drawing.Point(248, 132);
            this.lblKurs.Name = "lblKurs";
            this.lblKurs.Size = new System.Drawing.Size(32, 13);
            this.lblKurs.TabIndex = 14;
            this.lblKurs.Text = "Курс:";
            //
            // lblK
            //
            this.lblK.AutoSize = true;
            this.lblK.Location = new System.Drawing.Point(330, 132);
            this.lblK.Name = "lblK";
            this.lblK.Size = new System.Drawing.Size(12, 13);
            this.lblK.TabIndex = 16;
            this.lblK.Text = "к";
            //
            // rateCur
            //
            this.rateCur.Enabled = false;
            this.rateCur.Location = new System.Drawing.Point(286, 129);
            this.rateCur.Name = "rateCur";
            this.rateCur.Size = new System.Drawing.Size(40, 20);
            this.rateCur.TabIndex = 15;
            this.rateCur.DecimalValue = 0m;
            this.rateCur.TextChanged += new System.EventHandler(this.rateCur_TextChanged);
            //
            // NU
            //
            this.NU.Enabled = false;
            this.NU.Location = new System.Drawing.Point(348, 129);
            this.NU.Name = "NU";
            this.NU.Size = new System.Drawing.Size(60, 20);
            this.NU.TabIndex = 17;
            this.NU.DecimalValue = 0m;
            this.NU.TextChanged += new System.EventHandler(this.NU_TextChanged);
            //
            // lblKomissija
            //
            this.lblKomissija.AutoSize = true;
            this.lblKomissija.Location = new System.Drawing.Point(8, 158);
            this.lblKomissija.Name = "lblKomissija";
            this.lblKomissija.Size = new System.Drawing.Size(58, 13);
            this.lblKomissija.TabIndex = 18;
            this.lblKomissija.Text = "Комиссия:";
            //
            // valComis
            //
            this.valComis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.valComis.FormattingEnabled = true;
            this.valComis.Location = new System.Drawing.Point(72, 155);
            this.valComis.Name = "valComis";
            this.valComis.Size = new System.Drawing.Size(70, 21);
            this.valComis.TabIndex = 19;
            //
            // komCur
            //
            this.komCur.Location = new System.Drawing.Point(148, 155);
            this.komCur.Name = "komCur";
            this.komCur.Size = new System.Drawing.Size(70, 20);
            this.komCur.TabIndex = 20;
            this.komCur.DecimalValue = 0m;
            //
            // lblPodNal
            //
            this.lblPodNal.AutoSize = true;
            this.lblPodNal.Location = new System.Drawing.Point(248, 158);
            this.lblPodNal.Name = "lblPodNal";
            this.lblPodNal.Size = new System.Drawing.Size(99, 13);
            this.lblPodNal.TabIndex = 21;
            this.lblPodNal.Text = "Подоходный налог:";
            //
            // podNalCur
            //
            this.podNalCur.Location = new System.Drawing.Point(353, 155);
            this.podNalCur.Name = "podNalCur";
            this.podNalCur.Size = new System.Drawing.Size(70, 20);
            this.podNalCur.TabIndex = 22;
            this.podNalCur.DecimalValue = 0m;
            //
            // lblNKvit
            //
            this.lblNKvit.AutoSize = true;
            this.lblNKvit.Location = new System.Drawing.Point(8, 184);
            this.lblNKvit.Name = "lblNKvit";
            this.lblNKvit.Size = new System.Drawing.Size(210, 13);
            this.lblNKvit.TabIndex = 23;
            this.lblNKvit.Text = "Номер квитанции(справки) о приеме:";
            //
            // nKvit
            //
            this.nKvit.Enabled = false;
            this.nKvit.Location = new System.Drawing.Point(224, 181);
            this.nKvit.Name = "nKvit";
            this.nKvit.Size = new System.Drawing.Size(60, 20);
            this.nKvit.TabIndex = 24;
            this.nKvit.DecimalValue = 0m;
            //
            // lblClient
            //
            this.lblClient.AutoSize = true;
            this.lblClient.Location = new System.Drawing.Point(8, 210);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(43, 13);
            this.lblClient.TabIndex = 25;
            this.lblClient.Text = "Клиент:";
            //
            // clientTxt
            //
            this.clientTxt.Enabled = false;
            this.clientTxt.Location = new System.Drawing.Point(72, 207);
            this.clientTxt.Name = "clientTxt";
            this.clientTxt.Size = new System.Drawing.Size(340, 20);
            this.clientTxt.TabIndex = 26;
            //
            // btnClient
            //
            this.btnClient.Enabled = false;
            this.btnClient.Location = new System.Drawing.Point(418, 205);
            this.btnClient.Name = "btnClient";
            this.btnClient.Size = new System.Drawing.Size(25, 23);
            this.btnClient.TabIndex = 27;
            this.btnClient.Text = "...";
            this.btnClient.UseVisualStyleBackColor = true;
            //
            // lblDoc
            //
            this.lblDoc.AutoSize = true;
            this.lblDoc.Location = new System.Drawing.Point(8, 236);
            this.lblDoc.Name = "lblDoc";
            this.lblDoc.Size = new System.Drawing.Size(58, 13);
            this.lblDoc.TabIndex = 28;
            this.lblDoc.Text = "Документ";
            //
            // docCB
            //
            this.docCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.docCB.Enabled = false;
            this.docCB.FormattingEnabled = true;
            this.docCB.Location = new System.Drawing.Point(72, 233);
            this.docCB.Name = "docCB";
            this.docCB.Size = new System.Drawing.Size(150, 21);
            this.docCB.TabIndex = 29;
            //
            // lblSerTxt
            //
            this.lblSerTxt.AutoSize = true;
            this.lblSerTxt.Location = new System.Drawing.Point(228, 236);
            this.lblSerTxt.Name = "lblSerTxt";
            this.lblSerTxt.Size = new System.Drawing.Size(38, 13);
            this.lblSerTxt.TabIndex = 30;
            this.lblSerTxt.Text = "Серия:";
            //
            // serTxt
            //
            this.serTxt.Enabled = false;
            this.serTxt.Location = new System.Drawing.Point(272, 233);
            this.serTxt.Name = "serTxt";
            this.serTxt.Size = new System.Drawing.Size(60, 20);
            this.serTxt.TabIndex = 31;
            //
            // lblNumTxt
            //
            this.lblNumTxt.AutoSize = true;
            this.lblNumTxt.Location = new System.Drawing.Point(338, 236);
            this.lblNumTxt.Name = "lblNumTxt";
            this.lblNumTxt.Size = new System.Drawing.Size(41, 13);
            this.lblNumTxt.TabIndex = 32;
            this.lblNumTxt.Text = "Номер:";
            //
            // numTxt
            //
            this.numTxt.Enabled = false;
            this.numTxt.Location = new System.Drawing.Point(385, 233);
            this.numTxt.Name = "numTxt";
            this.numTxt.Size = new System.Drawing.Size(58, 20);
            this.numTxt.TabIndex = 33;
            //
            // resCHB
            //
            this.resCHB.AutoSize = true;
            this.resCHB.Enabled = false;
            this.resCHB.Location = new System.Drawing.Point(8, 262);
            this.resCHB.Name = "resCHB";
            this.resCHB.Size = new System.Drawing.Size(69, 17);
            this.resCHB.TabIndex = 34;
            this.resCHB.Text = "Резидент";
            this.resCHB.UseVisualStyleBackColor = true;
            //
            // UbsOpRetoperFrm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 281);
            this.Name = "UbsOpRetoperFrm";
            this.Text = FormTitle;
            this.panelMain.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            this.grpPayDoc.ResumeLayout(false);
            this.grpPayDoc.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button btnExit;
        private UbsControl.UbsCtrlInfo ubsCtrlInfo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.GroupBox grpPayDoc;
        private System.Windows.Forms.Label lblPlDoc;
        private System.Windows.Forms.Label lblSerPDoc;
        private System.Windows.Forms.Label lblNumPDoc;
        private System.Windows.Forms.Label lblValNom;
        private System.Windows.Forms.Label lblNomPDoc;
        private System.Windows.Forms.TextBox txbPlDoc;
        private System.Windows.Forms.TextBox txbSerPDoc;
        private System.Windows.Forms.TextBox txbValNom;
        private UbsControl.UbsCtrlDecimal numPDoc;
        private UbsControl.UbsCtrlDecimal nomPDoc;
        private System.Windows.Forms.CheckBox chkPayMoney;
        private System.Windows.Forms.Label lblVydat;
        private System.Windows.Forms.ComboBox valMinusCB;
        private UbsControl.UbsCtrlDecimal sumMinusCur;
        private System.Windows.Forms.Label lblKurs;
        private System.Windows.Forms.Label lblK;
        private UbsControl.UbsCtrlDecimal rateCur;
        private UbsControl.UbsCtrlDecimal NU;
        private System.Windows.Forms.Label lblKomissija;
        private System.Windows.Forms.ComboBox valComis;
        private UbsControl.UbsCtrlDecimal komCur;
        private System.Windows.Forms.Label lblPodNal;
        private UbsControl.UbsCtrlDecimal podNalCur;
        private System.Windows.Forms.Label lblNKvit;
        private UbsControl.UbsCtrlDecimal nKvit;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.TextBox clientTxt;
        private System.Windows.Forms.Button btnClient;
        private System.Windows.Forms.Label lblDoc;
        private System.Windows.Forms.ComboBox docCB;
        private System.Windows.Forms.Label lblSerTxt;
        private System.Windows.Forms.TextBox serTxt;
        private System.Windows.Forms.Label lblNumTxt;
        private System.Windows.Forms.TextBox numTxt;
        private System.Windows.Forms.CheckBox resCHB;
    }
}
