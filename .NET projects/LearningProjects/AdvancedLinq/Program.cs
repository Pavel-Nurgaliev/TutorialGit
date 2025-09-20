using AdvancedLinq;

var data = Seed.Data();

var cohorts = OrderCohortsCreator.OrdersByCohortAndBin(data.orders, data.customers);

foreach (var c in cohorts)
{
    Console.WriteLine($" {c.Segment} {c.YearMonth} {c.Bin} {c.Count}");
}
