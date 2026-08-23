namespace LinqPractical;

// Each challenge returns DATA (these records), not formatted text.
// That way the checker verifies your query logic, and you're free to format
// output however you like. Records give value-equality for free, so the
// checker can compare sequences directly.

public record Stats(int Count, double AvgAge, int Youngest, int Oldest);

public record CityCount(string City, int Count);

public record ProductPrice(string Category, string Product, decimal Price);

public record OrderLine(int OrderId, string Customer, DateOnly Date, int Items);

public record ProductUnits(string Product, int Units);

public record CategoryRevenue(string Category, decimal Revenue);

public record CategoryProductRevenue(string Category, string Product, decimal Revenue);

public record MonthRunning(DateOnly Month, decimal Monthly, decimal Running);
