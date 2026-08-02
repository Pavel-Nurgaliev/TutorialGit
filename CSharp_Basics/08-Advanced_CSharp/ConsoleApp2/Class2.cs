using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    [Flags]
    public enum DaysOfTheWeek { Mon = 1, Thue = 2, Wedn = 3, Thur = 4, Fri = 5, Sat = 6, Sun = 7, Weekends = Sat | Sun }
}
