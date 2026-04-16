using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace UbsBusiness
{
    public partial class FrmCashSymb : Form
    {
        private sealed class CashSymbolRow
        {
            public string Symbol;
            public decimal Amount;
        }

        public object UbsChannelRef { get; set; }
        public object[,] CashSymbolsSource { get; set; }
        public object[,] AllowedCashSymbols { get; set; }
        public decimal ExpectedTotal { get; set; }

        public object[,] CashSymbolsResult { get; private set; }
        public bool IsConfirmed { get; private set; }

        public FrmCashSymb()
        {
            InitializeComponent();
        }

        private void FrmCashSymb_Load(object sender, EventArgs e)
        {
            LoadGridRows();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!TryBuildAndValidateMatrix())
            {
                return;
            }

            IsConfirmed = true;
            this.DialogResult = DialogResult.OK;
            this.btnExit_Click(this, EventArgs.Empty);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            IsConfirmed = false;
            this.DialogResult = DialogResult.Cancel;
            this.btnExit_Click(this, EventArgs.Empty);
        }

        private void LoadGridRows()
        {
            grdCashSymbols.Rows.Clear();

            List<CashSymbolRow> rows = ConvertMatrixToRows(CashSymbolsSource);
            int i;
            for (i = 0; i < rows.Count; i++)
            {
                grdCashSymbols.Rows.Add(rows[i].Symbol, rows[i].Amount.ToString("0.00"));
            }

            if (grdCashSymbols.Rows.Count == 0)
            {
                grdCashSymbols.Rows.Add(string.Empty, string.Empty);
            }
        }

        private List<CashSymbolRow> ConvertMatrixToRows(object[,] matrix)
        {
            List<CashSymbolRow> rows = new List<CashSymbolRow>();
            if (matrix == null)
            {
                return rows;
            }

            int rowCount = matrix.GetLength(0);
            int colCount = matrix.GetLength(1);
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                int symbolColumn = colCount > 2 ? 1 : 0;
                int amountColumn = colCount > 2 ? 2 : (colCount > 1 ? 1 : 0);

                string symbol = Convert.ToString(matrix[rowIndex, symbolColumn]);
                decimal amount = 0m;
                object rawAmount = matrix[rowIndex, amountColumn];
                if (rawAmount != null && Convert.ToString(rawAmount).Trim().Length > 0)
                {
                    try
                    {
                        amount = Convert.ToDecimal(rawAmount);
                    }
                    catch (Exception)
                    {
                        amount = 0m;
                    }
                }

                if (symbol.Length > 0 || amount != 0m)
                {
                    CashSymbolRow row = new CashSymbolRow();
                    row.Symbol = symbol;
                    row.Amount = amount;
                    rows.Add(row);
                }
            }

            return rows;
        }

        private bool TryBuildAndValidateMatrix()
        {
            List<CashSymbolRow> rows = new List<CashSymbolRow>();
            decimal total = 0m;
            int rowIndex;
            for (rowIndex = 0; rowIndex < grdCashSymbols.Rows.Count; rowIndex++)
            {
                DataGridViewRow gridRow = grdCashSymbols.Rows[rowIndex];
                if (gridRow.IsNewRow)
                {
                    continue;
                }

                string symbol = Convert.ToString(gridRow.Cells[0].Value);
                string amountText = Convert.ToString(gridRow.Cells[1].Value);
                bool symbolEmpty = string.IsNullOrEmpty(symbol == null ? string.Empty : symbol.Trim());
                bool amountEmpty = string.IsNullOrEmpty(amountText == null ? string.Empty : amountText.Trim());

                if (symbolEmpty && amountEmpty)
                {
                    continue;
                }

                if (symbolEmpty)
                {
                    MessageBox.Show("\u041a\u0430\u0441\u0441\u043e\u0432\u044b\u0439 \u0441\u0438\u043c\u0432\u043e\u043b \u043d\u0435 \u0437\u0430\u0434\u0430\u043d!", "\u041f\u0440\u043e\u0432\u0435\u0440\u043a\u0430 \u043a\u0430\u0441\u0441\u043e\u0432\u044b\u0445 \u0441\u0438\u043c\u0432\u043e\u043b\u043e\u0432",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    grdCashSymbols.CurrentCell = gridRow.Cells[0];
                    grdCashSymbols.Focus();
                    return false;
                }

                decimal amount;
                if (amountEmpty || !decimal.TryParse(amountText, out amount) || amount == 0m)
                {
                    MessageBox.Show("\u0421\u0443\u043c\u043c\u0430 \u043d\u0435 \u0437\u0430\u0434\u0430\u043d\u0430!", "\u041f\u0440\u043e\u0432\u0435\u0440\u043a\u0430 \u043a\u0430\u0441\u0441\u043e\u0432\u044b\u0445 \u0441\u0438\u043c\u0432\u043e\u043b\u043e\u0432",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    grdCashSymbols.CurrentCell = gridRow.Cells[1];
                    grdCashSymbols.Focus();
                    return false;
                }

                CashSymbolRow row = new CashSymbolRow();
                row.Symbol = symbol.Trim();
                row.Amount = amount;
                rows.Add(row);
                total += amount;
            }

            if (total != ExpectedTotal)
            {
                MessageBox.Show("\u041e\u0431\u0449\u0430\u044f \u0441\u0443\u043c\u043c\u0430, \u0434\u043e\u043b\u0436\u043d\u0430 \u0441\u043e\u0432\u043f\u0430\u0434\u0430\u0442\u044c \u0441 \u0432\u0432\u0435\u0434\u0435\u043d\u043d\u043e\u0439 \u043d\u0430 \u0444\u043e\u0440\u043c\u0435",
                    "\u041f\u0440\u043e\u0432\u0435\u0440\u043a\u0430 \u043a\u0430\u0441\u0441\u043e\u0432\u044b\u0445 \u0441\u0438\u043c\u0432\u043e\u043b\u043e\u0432", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            object[,] result = ConvertRowsToMatrix(rows);
            object[,] validated = ValidateCashSymbols(result);
            CashSymbolsResult = validated != null ? validated : result;
            return true;
        }

        private static object[,] ConvertRowsToMatrix(List<CashSymbolRow> rows)
        {
            object[,] result = new object[rows.Count, 2];
            int rowIndex;
            for (rowIndex = 0; rowIndex < rows.Count; rowIndex++)
            {
                result[rowIndex, 0] = rows[rowIndex].Symbol;
                result[rowIndex, 1] = rows[rowIndex].Amount;
            }

            return result;
        }

        private object[,] ValidateCashSymbols(object[,] matrix)
        {
            if (UbsChannelRef == null)
            {
                return matrix;
            }

            object paramIn = CreateParamObject();
            object paramOut = CreateParamObject();
            if (paramIn == null || paramOut == null)
            {
                return matrix;
            }

            SetParameter(paramIn, "arrCashSymb", matrix);
            SetParameter(paramIn, "arrTypeCashSymbol", AllowedCashSymbols);

            RunChannel("UtCheckArrayCashSymbol", paramIn, paramOut);

            bool bRetValCheck = GetBoolParameter(paramOut, "bRetValCheck");
            string strError = Convert.ToString(GetParameter(paramOut, "strError"));
            if (!bRetValCheck && !string.IsNullOrEmpty(strError))
            {
                MessageBox.Show(strError, "\u041f\u0440\u043e\u0432\u0435\u0440\u043a\u0430 \u043a\u0430\u0441\u0441\u043e\u0432\u044b\u0445 \u0441\u0438\u043c\u0432\u043e\u043b\u043e\u0432",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            object[,] validated = GetParameter(paramOut, "arrCashSymb") as object[,];
            return validated != null ? validated : matrix;
        }

        private object CreateParamObject()
        {
            Type type = Type.GetTypeFromProgID("ToolPubs.IUbsParam");
            if (type == null)
            {
                return null;
            }

            return Activator.CreateInstance(type);
        }

        private static void SetParameter(object param, string name, object value)
        {
            if (param == null)
            {
                return;
            }

            param.GetType().InvokeMember("Parameter", BindingFlags.SetProperty, null, param, new object[] { name, value });
        }

        private static object GetParameter(object param, string name)
        {
            if (param == null)
            {
                return null;
            }

            return param.GetType().InvokeMember("Parameter", BindingFlags.GetProperty, null, param, new object[] { name });
        }

        private bool GetBoolParameter(object param, string name)
        {
            object value = GetParameter(param, name);
            return value != null && Convert.ToBoolean(value);
        }

        private void RunChannel(string command, object paramIn, object paramOut)
        {
            UbsChannelRef.GetType().InvokeMember("Run", BindingFlags.InvokeMethod, null, UbsChannelRef,
                new object[] { command, paramIn, paramOut });
        }
    }
}
