using System.Windows.Forms;
using System;
using UbsService;

namespace UbsBusiness
{
    /// <summary>
    /// Форма сделки с драгоценными металлами
    /// </summary>
    public partial class FrmInstr : UbsFormBase
    {
        #region Блок объявления переменных

        private const int ColumnCount = 8;
        private object[] m_selectedInstr;

        #endregion

        /// <summary>
        /// Конструктор
        /// </summary>
        public FrmInstr()
        {
            InitializeComponent();

            base.Ubs_CommandLock = true;
        }
        public object[] SelectedInstr
        {
            get { return m_selectedInstr; }
        }

        public void LoadData(object arrInstr)
        {
            lvwInstrOplata.Items.Clear();

            Array arr = arrInstr as Array;
            if (arr == null || arr.Rank != 2)
                return;

            int rowCount = arr.GetLength(0);
            for (int row = 0; row < rowCount; row++)
            {
                string code = UbsPmTradeMatrixUtil.GetMatrixCellString(arr, row, 0);
                ListViewItem item = new ListViewItem(code);
                for (int col = 1; col < ColumnCount; col++)
                {
                    item.SubItems.Add(UbsPmTradeMatrixUtil.GetMatrixCellString(arr, row, col));
                }
                lvwInstrOplata.Items.Add(item);
            }
        }

        private void AcceptSelection()
        {
            if (lvwInstrOplata.Items.Count == 0 || lvwInstrOplata.SelectedItems.Count == 0)
                return;

            ListViewItem selected = lvwInstrOplata.SelectedItems[0];
            m_selectedInstr = new object[ColumnCount];
            m_selectedInstr[0] = selected.Text;
            for (int i = 1; i < ColumnCount; i++)
            {
                m_selectedInstr[i] = (i < selected.SubItems.Count)
                    ? selected.SubItems[i].Text
                    : string.Empty;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            AcceptSelection();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            try { this.Close(); }
            catch (Exception ex) { this.Ubs_ShowError(ex); }
        }

        private void lvwInstrOplata_DoubleClick(object sender, EventArgs e)
        {
            AcceptSelection();
        }
    }
}
