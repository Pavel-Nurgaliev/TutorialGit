using System;
using System.Collections.Generic;
using System.Text;

namespace PulseMonitor
{
    public class AlertRule
    {
        public Predicate<Reading> Condition;
        public Func<Reading, string> Description;
        public Func<Reading, string> NotifyPrefix;
    }
}
