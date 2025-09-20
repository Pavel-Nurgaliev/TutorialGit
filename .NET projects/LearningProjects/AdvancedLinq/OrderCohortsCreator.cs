using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedLinq
{
    /*1) Multi-key grouping with conditional bins (order cohorts)

Goal: Group orders by (Customer.Segment, OrderDate (yyyy-MM)) and also bucket Amount into bins: Small(<100), Mid(100–999), Large(≥1000). Return counts per bin.*/
    internal static class OrderCohortsCreator
    {
        public static IEnumerable<(string Segment, string YearMonth, string Bin, int Count)> OrdersByCohortAndBin(IEnumerable<Order> orders, IEnumerable<Customer> customers)
        {
            var joined = orders.Join(customers, o => o.CustomerId, c => c.Id, (o, c) => new { c.Segment, YearMonth = o.CreatedAt.ToString("yyyy-MM"), o.Amount });

            var bins = joined.Select(x => new { x.Segment, x.YearMonth, Bin = GetBin(x.Amount) });

            var grouped = bins.GroupBy(x => new { x.Segment, x.YearMonth, x.Bin})
                              .Select(g=>new { g.Key.Segment,
                                               g.Key.YearMonth,
                                               g.Key.Bin,
                                               Count = g.Count()});

            foreach (var g in grouped)
            {
                yield return (g.Segment, g.YearMonth, g.Bin, g.Count);
            }
        }

        private static string GetBin(decimal amount) =>
     amount < 100 ? "Small" :
     amount < 1000 ? "Mid" : "Large";
    }
}
