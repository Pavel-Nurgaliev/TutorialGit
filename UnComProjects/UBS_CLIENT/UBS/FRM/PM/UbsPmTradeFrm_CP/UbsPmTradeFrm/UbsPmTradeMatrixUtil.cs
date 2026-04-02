using System;

namespace UbsBusiness
{
    internal static class UbsPmTradeMatrixUtil
    {
        public static string GetMatrixCellString(Array arr, int row, int col)
        {
            if (arr == null || arr.Rank != 2) return string.Empty;
            if (row < 0 || row >= arr.GetLength(0) || col < 0 || col >= arr.GetLength(1)) return string.Empty;
            object v = arr.GetValue(row, col);
            if (v == null || v == DBNull.Value) return string.Empty;
            return Convert.ToString(v);
        }

        public static int GetMatrixCellInt(Array arr, int row, int col)
        {
            if (arr == null || arr.Rank != 2) return 0;
            if (row < 0 || row >= arr.GetLength(0) || col < 0 || col >= arr.GetLength(1)) return 0;
            object v = arr.GetValue(row, col);
            if (v == null || v == DBNull.Value) return 0;
            return Convert.ToInt32(v);
        }
    }
}
