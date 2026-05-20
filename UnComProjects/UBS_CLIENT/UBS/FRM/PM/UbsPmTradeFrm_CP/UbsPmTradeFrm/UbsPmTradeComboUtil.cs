using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UbsService;

namespace UbsBusiness
{
    internal static class UbsPmTradeComboUtil
    {
        public static void FillComboFrom2DArray(ComboBox cmb, UbsParam paramOut, string keyParam)
        {
            if (cmb == null) return;

            if (!paramOut.Contains(keyParam))
            {
                List<KeyValuePair<int, string>> empty = new List<KeyValuePair<int, string>>();
                empty.Add(new KeyValuePair<int, string>(0, ""));
                cmb.DataSource = empty;
                cmb.ValueMember = "Key";
                cmb.DisplayMember = "Value";
                return;
            }

            Array arr = paramOut.Value(keyParam) as Array;
            List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();

            if (arr != null && arr.Rank == 2)
            {
                int len0 = arr.GetLength(0);
                int len1 = arr.GetLength(1);

                if (len1 == 2)
                {
                    for (int n = 0; n < len0; n++)
                    {
                        int id = Convert.ToInt32(arr.GetValue(n, 0));
                        string text = Convert.ToString(arr.GetValue(n, 1));
                        list.Add(new KeyValuePair<int, string>(id, text));
                    }
                }
            }

            if (list.Count == 0)
                list.Add(new KeyValuePair<int, string>(0, ""));

            cmb.DataSource = list;
            cmb.ValueMember = "Key";
            cmb.DisplayMember = "Value";
        }

        public static void SetComboByKey(ComboBox cmb, int key)
        {
            if (cmb == null || cmb.Items == null) return;

            foreach (object item in cmb.Items)
            {
                if (item is KeyValuePair<int, string>)
                {
                    KeyValuePair<int, string> kv = (KeyValuePair<int, string>)item;
                    if (kv.Key == key)
                    {
                        cmb.SelectedItem = item;
                        return;
                    }
                }
            }
        }

        public static bool TryGetSelectedKey(ComboBox cmb, out int key)
        {
            key = 0;
            if (cmb == null || cmb.SelectedItem == null)
                return false;
            if (cmb.SelectedItem is KeyValuePair<int, string>)
            {
                key = ((KeyValuePair<int, string>)cmb.SelectedItem).Key;
                return true;
            }
            return false;
        }

    }
}
