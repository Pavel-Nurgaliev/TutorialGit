using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    partial class UbsBgContractFrm
    {
        /// <summary>
        /// Инициализация дерева ставок (trvRates). Заполняет узлы по m_arrRates и m_arrRateValues,
        /// при in_Refresh подтягивает значения через BG_GetRateByRateType при отсутствии локальных.
        /// </summary>
        /// <param name="inRefresh">При true — запрашивать значение по типу ставки из канала, если в m_arrRateValues ещё нет.</param>
        private void InitTrvRates(bool inRefresh = false)
        {
            try
            {
                if (trvRates.Nodes.Count > 0)
                    trvRates.Nodes.Clear();

                if (m_arrRates == null || m_arrRates.GetLength(0) == 0)
                {
                    MessageBox.Show(MsgRateTypesArrayEmpty, m_captionForm,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                int rateCount = m_arrRates.GetLength(0);
                if (m_arrRateValues == null || m_arrRateValues.Length != rateCount)
                    m_arrRateValues = new object[rateCount];

                var pIn = new UbsParam();
                var pOut = new UbsParam();

                for (int i = 0; i < rateCount; i++)
                {
                    string rateType = Convert.ToString(m_arrRates[i]);
                    string key = "RateType: " + rateType;
                    TreeNode parentNode = trvRates.Nodes.Add(key, rateType);

                    object[,] arrRateTemp = null;
                    if (!string.IsNullOrEmpty(rateType) && inRefresh)
                    {
                        if (m_arrRateValues[i] is object[,] existing)
                            arrRateTemp = existing;
                        else
                        {
                            pIn.Value("RateType", StrRatePrefix + rateType);

                            RunUbsChannel("BG_GetRateByRateType", pIn, pOut);

                            object valueRate = pOut.Value("ValueRate");

                            if (valueRate is object[,] fromChannel)
                            {
                                arrRateTemp = fromChannel;
                                m_arrRateValues[i] = fromChannel;
                            }
                        }
                    }
                    else
                        arrRateTemp = m_arrRateValues[i] as object[,];

                    if (arrRateTemp != null)
                    {
                        int rows = arrRateTemp.GetLength(0);
                        for (int j = 0; j < rows; j++)
                        {
                            string strKeyChild = key + " Date: " + Convert.ToDateTime(arrRateTemp[j, 0]).ToShortDateString();
                            decimal val = Convert.ToDecimal(arrRateTemp[j, 1]);
                            string tstr = Convert.ToDateTime(arrRateTemp[j, 0]).ToShortDateString() + " - " + val.ToString("N2");
                            parentNode.Nodes.Add(strKeyChild, tstr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                base.Ubs_ShowError(ex);
            }
        }

        /// <summary>
        /// Сортирует массив ставок [row, col] по колонке 0 (дата). VB: objArray.ASort arrRateTemp, ArrCond.
        /// </summary>
        private static void SortRateValuesByDate(object[,] arr)
        {
            if (arr == null || arr.GetLength(0) <= 1) return;
            int rows = arr.GetLength(0);
            var list = new List<KeyValuePair<object, object>>(rows);
            for (int r = 0; r < rows; r++)
                list.Add(new KeyValuePair<object, object>(arr[r, 0], arr[r, 1]));
            list.Sort((a, b) =>
            {
                try
                {
                    DateTime da = Convert.ToDateTime(a.Key);
                    DateTime db = Convert.ToDateTime(b.Key);
                    return da.CompareTo(db);
                }
                catch { return 0; }
            });
            for (int r = 0; r < rows; r++)
            {
                arr[r, 0] = list[r].Key;
                arr[r, 1] = list[r].Value;
            }
        }

        /// <summary>
        /// Открывает форму ставки (добавить/редактировать), обновляет m_arrRateValues, перестраивает дерево. VB: GetRate.
        /// </summary>
        private void GetRate(string strCommandRates)
        {
            try
            {
                if (trvRates.SelectedNode == null)
                    return;

                using (var form = new UbsBgRatesFrm())
                {
                    string rateTypeForForm = trvRates.SelectedNode.Parent == null
                        ? trvRates.SelectedNode.Text
                        : trvRates.SelectedNode.Parent.Text;

                    form.RateTypeText = rateTypeForForm;
                    form.DateValue = dateOpenGarant.DateValue;

                    bool isEdit = string.Equals(strCommandRates, EditCommand, StringComparison.OrdinalIgnoreCase);
                    form.Text = isEdit ? CaptionEditRate : CaptionAddRate;

                    if (isEdit)
                    {
                        string nodeText = trvRates.SelectedNode.Text;
                        string datePart = nodeText.Length >= 10 ? nodeText.Substring(0, 10) : string.Empty;
                        string valuePart = nodeText.Length > 13 ? nodeText.Substring(13).Trim() : "0";
                        if (DateTime.TryParse(datePart, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime editDate))
                            form.DateValue = editDate;
                        decimal.TryParse(valuePart, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal editVal);
                        form.RateValue = editVal;
                        form.DateEnabled = false;
                    }

                    form.ShowDialog(this);

                    if (form.ApplyClicked && m_arrRates != null && m_arrRateValues != null)
                    {
                        string selectedRateType = form.RateTypeText;
                        DateTime newDate = form.DateValue;
                        decimal newValue = form.RateValue;

                        int rateCount = m_arrRates.Length;
                        for (int i = 0; i < rateCount; i++)
                        {
                            string rateType = Convert.ToString(m_arrRates[i]);
                            if (rateType != selectedRateType) continue;

                            object[,] arrRateTemp = m_arrRateValues[i] as object[,];
                            if (string.Equals(strCommandRates, AddCommand, StringComparison.OrdinalIgnoreCase))
                            {
                                if (arrRateTemp != null)
                                {
                                    int rows = arrRateTemp.GetLength(0);
                                    for (int j = 0; j < rows; j++)
                                    {
                                        object cellDate = arrRateTemp[j, 0];
                                        if (cellDate != null && Convert.ToDateTime(cellDate).Date == newDate.Date)
                                        {
                                            MessageBox.Show(MsgRateDateAlreadyExists, m_captionForm,
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                    }
                                    int newRows = rows + 1;
                                    var newArr = new object[newRows, 2];
                                    for (int r = 0; r < rows; r++) { newArr[r, 0] = arrRateTemp[r, 0]; newArr[r, 1] = arrRateTemp[r, 1]; }
                                    newArr[rows, 0] = newDate;
                                    newArr[rows, 1] = newValue;
                                    m_arrRateValues[i] = newArr;
                                }
                                else
                                {
                                    var newArr = new object[1, 2];
                                    newArr[0, 0] = newDate;
                                    newArr[0, 1] = newValue;
                                    m_arrRateValues[i] = newArr;
                                }
                            }
                            else if (string.Equals(strCommandRates, EditCommand, StringComparison.OrdinalIgnoreCase) && arrRateTemp != null)
                            {
                                int rows = arrRateTemp.GetLength(0);
                                for (int j = 0; j < rows; j++)
                                {
                                    object cellDate = arrRateTemp[j, 0];
                                    if (cellDate != null && Convert.ToDateTime(cellDate).Date == newDate.Date)
                                    {
                                        arrRateTemp[j, 0] = newDate;
                                        arrRateTemp[j, 1] = newValue;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    }
                }

                for (int i = 0; i < (m_arrRateValues?.Length ?? 0); i++)
                {
                    if (m_arrRateValues[i] is object[,] arr && arr.GetLength(0) > 1)
                        SortRateValuesByDate(arr);
                }

                InitTrvRates(false);
                int ratesTabIndex = tabControl.TabPages.IndexOf(tabPage4);
                if (ratesTabIndex >= 0)
                    tabControl.SelectedIndex = ratesTabIndex;

                btnAddRate.Enabled = false;
                btnEditRate.Enabled = false;
                btnDelRate.Enabled = false;

                trvRates.Focus();
                trvRates_AfterSelect(trvRates, new TreeViewEventArgs(trvRates.SelectedNode, TreeViewAction.Unknown));
            }
            catch (Exception ex)
            {
                base.Ubs_ShowError(ex);
            }
        }

    }
}
