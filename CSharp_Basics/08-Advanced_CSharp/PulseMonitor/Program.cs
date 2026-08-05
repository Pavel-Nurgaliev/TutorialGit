using PulseMonitor;

var t0 = new DateTime(2026, 1, 1, 10, 0, 0);

var ms = new MetricSource();

var readingList = new List<Reading>(){
  new Reading(Metrics.Cpu, 40, t0)
, new Reading(Metrics.Cpu, 92, t0.AddSeconds(1))
, new Reading(Metrics.Temp, 60, t0.AddSeconds(2))
, new Reading(Metrics.Temp, 78, t0.AddSeconds(3))
, new Reading(Metrics.Cpu, 99, t0.AddSeconds(4))
, new Reading(Metrics.Latency, 120, t0.AddSeconds(5))
, new Reading(Metrics.Latency, 350, t0.AddSeconds(6)) };

var alertRulesList = new List<AlertRule>()
{
    new()
    {
        Condition = r=>r.Metric==Metrics.Cpu && r.Value >90,
        Description = r=>$"HIGH CPU {r.Value}% at {r.At:HH:mm:ss}",
        NotifyPrefix = r=> "[PAGER]"
    },
    new()
    {
        Condition = r => r.Metric==Metrics.Temp && r.Value>75,
        Description = r=> $"Overheating: {r.Value}C at {r.At:HH:mm:ss}",
        NotifyPrefix = r=> "[EMAIL]"
    },
    new()
    {
        Condition = r=> r.Metric==Metrics.Latency && r.Value>300,
        Description = r=> $"Slow response: {r.Value}ms at {r.At:HH:mm:ss}",
        NotifyPrefix = r=> "[SLACK]"
    },
};

Action<Reading> loginAppliancesNotification = (r) =>
{
    Console.WriteLine($"[LOG] {r.At:HH:mm:ss} {r.Metric} = {r.Value}");
};

Action<Reading> alertRulesNotification = (r) =>
{
    foreach (var rule in alertRulesList)
    {
        if (rule.Condition(r))
        {
            Console.WriteLine($"{rule.NotifyPrefix(r)} {rule.Description(r)}");
        }
    }
};

ms.ReadingTaken += loginAppliancesNotification;
ms.ReadingTaken += alertRulesNotification;

foreach (var reading in readingList)
{
    ms.Emit(reading);   
}

ms.ReadingTaken -= loginAppliancesNotification;
ms.ReadingTaken -= alertRulesNotification;