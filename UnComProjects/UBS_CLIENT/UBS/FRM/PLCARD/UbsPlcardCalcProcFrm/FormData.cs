using System;

namespace UbsPlcardCalcProcFrm
{
    public class FormData
    {
        public int CountAll { get; set; }
        public int CountComplete { get; set; }
        public int[] ArrNumProcessor { get; set; } = new int[7];
        public int CountProcessor { get; set; }
        public bool IsInProcessing { get; set; }
        public DateTime DateStart { get; set; }
        public string[] ArrStrServerFile { get; set; } = new string[7];
    }
}
