namespace LinqPractical;

/// <summary>
/// YOUR WORKSPACE. Every method below throws NotImplementedException — replace
/// each body with your own LINQ query so it returns the described data.
///
/// Workflow:
///   1. Implement a method.
///   2. Run `dotnet run` (or F5). The checker prints ✓ PASS / ✗ FAIL / – skipped.
///   3. On FAIL it shows expected vs. your output so you can debug the LOGIC.
///   4. Stuck? Open SOLUTIONS.md — but try first.
///
/// Data lives in Store (see Data.cs). Ordering/tiebreaks are part of each task,
/// so read them carefully — the checker compares sequences in order.
/// </summary>
public static class Challenges
{
    // 1. Names of ACTIVE, ADULT (Age >= 18) customers, ordered by name ascending.
    //    Return: the names only. Hint: Where, OrderBy, Select.
    public static IEnumerable<string> Challenge01_ActiveAdults()
        => Store.Customers.Where(customer => customer.IsActive && customer.Age >= 18).OrderBy(customer => customer.Name).Select(customer => customer.Name);

    // 2. Summary stats for ACTIVE customers:
    //    Count, average Age (as double), youngest Age, oldest Age.
    //    Hint: Count, Average, Min, Max.
    public static Stats Challenge02_Stats()
        => Store.Customers.Where(customer => customer.IsActive).GroupBy(_ => 0).Select(group => new Stats(group.Count()
                                                                                                    , group.Average(customer => customer.Age)
                                                                                                    , group.Min(customer => customer.Age)
                                                                                                    , group.Max(customer => customer.Age))).Single();

    // 3. Number of customers per City.
    //    Order: Count descending, then City ascending (tiebreak).
    //    Hint: GroupBy, Count, OrderByDescending, ThenBy.
    public static IEnumerable<CityCount> Challenge03_PerCity()
        => Store.Customers.GroupBy(customers => customers.City, (city, customers) => new { Count = customers.Count(), City = city })
                          .Select(group => new CityCount(group.City, group.Count))
                          .OrderByDescending(cc => cc.Count)
                          .ThenBy(cc => cc.City);

    // 4. Every product with its category name and price.
    //    Order: Category ascending, then Price descending, then Product ascending.
    //    Hint: join Products→Categories, OrderBy + ThenByDescending + ThenBy.
    public static IEnumerable<ProductPrice> Challenge04_ByCategoryThenPrice()
        => Store.Products.Join(Store.Categories, p => p.CategoryId, c => c.Id, (p, c) => new ProductPrice(c.Name, p.Name, p.Price)).OrderBy(pp => pp.Category).ThenByDescending(pp => pp.Price).ThenBy(pp => pp.Product);

    // 5. One line per order: OrderId, customer Name, order Date, number of items.
    //    Order: Date ascending, then OrderId ascending.
    //    Hint: join Orders→Customers; Items count is order.Items.Count.
    public static IEnumerable<OrderLine> Challenge05_OrdersWithCustomer()
        => Store.Orders.Join(Store.Customers, o => o.CustomerId, c => c.Id, (o, c) => new OrderLine(o.Id, c.Name, o.Date, o.Items.Count())).OrderBy(ol => ol.Date).ThenBy(ol => ol.OrderId);

    // 6. Total UNITS sold per product (sum the quantities across all orders).
    //    Order: Units descending, then Product name ascending.
    //    Hint: SelectMany(o => o.Items), GroupBy ProductId, Sum, join for the name.
    public static IEnumerable<ProductUnits> Challenge06_UnitsPerProduct()
        => Store.Orders.SelectMany(o => o.Items)
                       .Join(Store.Products, oi => oi.ProductId, p => p.Id, (oi, p) => new { Id = oi.ProductId, Name = p.Name, oi.Quantity })
                       .GroupBy(item => item.Name)
                       .Select(group => new ProductUnits(group.Key, group.Sum(g => g.Quantity)))
                       .OrderByDescending(productUnit => productUnit.Units)
                       .ThenBy(productUnit => productUnit.Product);

    // 7. Names of products that were NEVER ordered.
    //    Order: any (checker compares as a set here).
    //    Hint: collect ordered ProductIds (Distinct), then Except against all products.
    public static IEnumerable<string> Challenge07_NeverOrdered()
        => Store.Products.ExceptBy(Store.Orders.SelectMany(o => o.Items).Select(item => item.ProductId).Distinct(), p => p.Id).Select(item => item.Name);

    // 8. Total revenue per category. revenue of a line = Quantity * Product.Price.
    //    Order: Revenue descending, then Category ascending.
    //    Hint: SelectMany items, join Products then Categories, GroupBy, Sum.
    public static IEnumerable<CategoryRevenue> Challenge08_RevenuePerCategory()
        => Store.Orders.SelectMany(o => o.Items)
                       .Join(Store.Products, oi => oi.ProductId, p => p.Id, (oi, p) => new { ProductId = oi.ProductId, CategoryId = p.CategoryId, oi.Quantity, p.Price })
                       .Join(Store.Categories, oi => oi.CategoryId, c => c.Id, (oi, c) => new { ProductId = oi.ProductId, CategoryName = c.Name, oi.Quantity, oi.Price })
                       .GroupBy(item => item.CategoryName)
                       .Select(group => new CategoryRevenue(group.Key, group.Sum(g => g.Quantity * g.Price)))
                       .OrderByDescending(categoryRevenue => categoryRevenue.Revenue)
                       .ThenBy(categoryRevenue => categoryRevenue.Category);

    // 9. Top 2 products by revenue WITHIN each category.
    //    Order: Category ascending; inside each category Revenue descending,
    //           then Product ascending. Then take the top 2 per category.
    //    Hint: compute per-product revenue, GroupBy category,
    //          SelectMany(g => g.OrderByDescending(...).Take(2)).
    public static IEnumerable<CategoryProductRevenue> Challenge09_Top2PerCategory()
        => Store.Orders.SelectMany(o => o.Items)
                       .Join(Store.Products, oi => oi.ProductId, p => p.Id, (oi, p) => new { ProductId = oi.ProductId, CategoryId = p.CategoryId, Name = p.Name, oi.Quantity, p.Price })
                       .Join(Store.Categories, p => p.CategoryId, c => c.Id, (p, c) => new { CategoryId = c.Id, CategoryName = c.Name, ProductName = p.Name, Revenue = p.Quantity * p.Price })
                       .GroupBy(catProd => new { catProd.CategoryId, catProd.CategoryName, catProd.ProductName })
                       .Select(gp => new { gp.Key.CategoryId, gp.Key.CategoryName, gp.Key.ProductName, Revenue = gp.Sum(g => g.Revenue) })
                       .GroupBy(item => item.CategoryId)
                       .SelectMany(g => g.OrderBy(gi => gi.CategoryId).ThenByDescending(gi => gi.Revenue).ThenBy(gi => gi.ProductName)
                       .Take(2))
                       .Select(g => new CategoryProductRevenue(g.CategoryName, g.ProductName, g.Revenue))
                       .OrderBy(cpr => cpr.Category);

    // 10. Monthly revenue with a RUNNING (cumulative) total.
    //     One row per month: Month (first day of month), Monthly revenue, Running total.
    //     Order: Month ascending. Hint: group by first-of-month, order, then
    //     accumulate with a running variable in a Select (LINQ has no built-in scan).
    public static IEnumerable<MonthRunning> Challenge10_RunningTotal()
    {
        var runningTotal = 0m;

        return Store.Orders.GroupBy(x => new DateOnly(x.Date.Year, x.Date.Month, 1))
                           .Select(gdi => new
                           {
                               Date = gdi.Key
                                                              ,
                               MonthlyTotal = gdi.SelectMany(o => o.Items)
                                      .Join(Store.Products
                                         , oi => oi.ProductId
                                         , p => p.Id
                                         , (oi, p) => new { oi.Quantity, p.Price })
                                      .Sum(p => p.Quantity * p.Price)
                           })
                           .OrderBy(s => s.Date)
                           .Select(s =>
                           {
                               runningTotal += s.MonthlyTotal;

                               return new MonthRunning(s.Date, s.MonthlyTotal, runningTotal);
                           }).ToList();
    }
}
