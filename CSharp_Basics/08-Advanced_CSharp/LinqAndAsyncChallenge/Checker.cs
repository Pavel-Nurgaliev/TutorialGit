namespace LinqPractical;

/// <summary>
/// Grades your implementations. It knows the EXPECTED data for each challenge and
/// compares (in order, where order is specified). It never shows you how to write
/// the query — only whether your output matches, and the diff if it doesn't.
/// </summary>
public static class Checker
{
    private static int _pass, _fail, _skip;

    public static void RunLinq()
    {
        Console.WriteLine("=== LINQ CHALLENGE CHECKER ===\n");
        _pass = _fail = _skip = 0;

        CheckSeq(1, Challenges.Challenge01_ActiveAdults,
            ["Aygerim", "Camila", "Elena", "Farhad"]);

        CheckStats(2, Challenges.Challenge02_Stats,
            new Stats(5, 32.6, 17, 51));

        CheckSeq(3, Challenges.Challenge03_PerCity,
            [new CityCount("Almaty", 2), new CityCount("Astana", 2), new CityCount("Oral", 2)]);

        CheckSeq(4, Challenges.Challenge04_ByCategoryThenPrice,
        [
            new ProductPrice("Books", "Refactoring", 45m),
            new ProductPrice("Books", "The Pragmatic Programmer", 40m),
            new ProductPrice("Books", "Clean Code", 32m),
            new ProductPrice("Electronics", "Noise-Cancel Headset", 199m),
            new ProductPrice("Electronics", "Mechanical Keyboard", 79m),
            new ProductPrice("Electronics", "USB-C Hub", 39m),
            new ProductPrice("Electronics", "Wireless Mouse", 25m),
            new ProductPrice("Home", "Desk Lamp", 34m),
            new ProductPrice("Home", "Ceramic Mug", 12m),
            new ProductPrice("Home", "Plant Pot", 9m),
        ]);

        CheckSeq(5, Challenges.Challenge05_OrdersWithCustomer,
        [
            new OrderLine(1001, "Aygerim", new DateOnly(2024, 1, 10), 2),
            new OrderLine(1002, "Camila",  new DateOnly(2024, 1, 22), 3),
            new OrderLine(1003, "Aygerim", new DateOnly(2024, 2, 5),  2),
            new OrderLine(1004, "Elena",   new DateOnly(2024, 2, 18), 2),
            new OrderLine(1005, "Farhad",  new DateOnly(2024, 3, 3),  3),
            new OrderLine(1006, "Camila",  new DateOnly(2024, 3, 27), 2),
        ]);

        CheckSeq(6, Challenges.Challenge06_UnitsPerProduct,
        [
            new ProductUnits("Ceramic Mug", 5),
            new ProductUnits("Clean Code", 3),
            new ProductUnits("Refactoring", 3),
            new ProductUnits("Mechanical Keyboard", 2),
            new ProductUnits("Noise-Cancel Headset", 2),
            new ProductUnits("Wireless Mouse", 2),
            new ProductUnits("Desk Lamp", 1),
            new ProductUnits("The Pragmatic Programmer", 1),
            new ProductUnits("USB-C Hub", 1),
        ]);

        CheckSet(7, Challenges.Challenge07_NeverOrdered, ["Plant Pot"]);

        CheckSeq(8, Challenges.Challenge08_RevenuePerCategory,
        [
            new CategoryRevenue("Electronics", 645m),
            new CategoryRevenue("Books", 271m),
            new CategoryRevenue("Home", 94m),
        ]);

        CheckSeq(9, Challenges.Challenge09_Top2PerCategory,
        [
            new CategoryProductRevenue("Books", "Refactoring", 135m),
            new CategoryProductRevenue("Books", "Clean Code", 96m),
            new CategoryProductRevenue("Electronics", "Noise-Cancel Headset", 398m),
            new CategoryProductRevenue("Electronics", "Mechanical Keyboard", 158m),
            new CategoryProductRevenue("Home", "Ceramic Mug", 60m),
            new CategoryProductRevenue("Home", "Desk Lamp", 34m),
        ]);

        CheckSeq(10, Challenges.Challenge10_RunningTotal,
        [
            new MonthRunning(new DateOnly(2024, 1, 1), 417m, 417m),
            new MonthRunning(new DateOnly(2024, 2, 1), 169m, 586m),
            new MonthRunning(new DateOnly(2024, 3, 1), 424m, 1010m),
        ]);

        Summary();
    }

    // ---- check helpers ----

    private static void CheckSeq<T>(int n, Func<IEnumerable<T>> attempt, IReadOnlyList<T> expected)
    {
        try
        {
            var got = attempt().ToList();
            if (got.SequenceEqual(expected)) Ok(n);
            else Bad(n, expected, got);
        }
        catch (NotImplementedException) { Skip(n); }
        catch (Exception ex) { Err(n, ex); }
    }

    // Order-independent comparison (used for the "never ordered" set).
    private static void CheckSet<T>(int n, Func<IEnumerable<T>> attempt, IReadOnlyList<T> expected)
    {
        try
        {
            var got = attempt().ToList();
            var same = got.Count == expected.Count &&
                       new HashSet<T>(got).SetEquals(expected);
            if (same) Ok(n);
            else Bad(n, expected, got);
        }
        catch (NotImplementedException) { Skip(n); }
        catch (Exception ex) { Err(n, ex); }
    }

    private static void CheckStats(int n, Func<Stats> attempt, Stats expected)
    {
        try
        {
            var g = attempt();
            var same = g.Count == expected.Count
                       && Math.Abs(g.AvgAge - expected.AvgAge) < 0.05
                       && g.Youngest == expected.Youngest
                       && g.Oldest == expected.Oldest;
            if (same) Ok(n);
            else Bad(n, new[] { expected }, new[] { g });
        }
        catch (NotImplementedException) { Skip(n); }
        catch (Exception ex) { Err(n, ex); }
    }

    // ---- reporting ----

    private static void Ok(int n)   { _pass++; Console.WriteLine($"✓ Challenge {n,2}: PASS"); }
    private static void Skip(int n) { _skip++; Console.WriteLine($"– Challenge {n,2}: not attempted"); }

    private static void Err(int n, Exception ex)
    {
        _fail++;
        Console.WriteLine($"✗ Challenge {n,2}: threw {ex.GetType().Name}: {ex.Message}");
    }

    private static void Bad<T>(int n, IEnumerable<T> expected, IEnumerable<T> got)
    {
        _fail++;
        Console.WriteLine($"✗ Challenge {n,2}: FAIL");
        Console.WriteLine("     expected:");
        foreach (var e in expected) Console.WriteLine("       " + e);
        Console.WriteLine("     your output:");
        foreach (var g in got) Console.WriteLine("       " + g);
    }

    private static void Summary()
    {
        Console.WriteLine();
        Console.WriteLine($"Result: {_pass} passed, {_fail} failed, {_skip} not attempted (of 10).");
        if (_skip == 10) Console.WriteLine("Start with Challenge 1 in Challenges.cs. You've got this.");
        Console.WriteLine();
    }
}
