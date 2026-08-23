namespace LinqPractical;

// ----- Domain models (records = concise, immutable, value-equality) -----

public record Category(int Id, string Name);

public record Product(int Id, string Name, int CategoryId, decimal Price);

public record Customer(
    int Id,
    string Name,
    string City,
    int Age,
    bool IsActive,
    DateOnly SignupDate);

public record OrderItem(int ProductId, int Quantity);

public record Order(
    int Id,
    int CustomerId,
    DateOnly Date,
    IReadOnlyList<OrderItem> Items);

/// <summary>
/// A tiny in-memory "database" shared by every challenge.
/// Deliberately small so you can verify answers by hand.
/// </summary>
public static class Store
{
    public static readonly List<Category> Categories =
    [
        new(1, "Electronics"),
        new(2, "Books"),
        new(3, "Home"),
    ];

    public static readonly List<Product> Products =
    [
        new(101, "Wireless Mouse",      1, 25.00m),
        new(102, "Mechanical Keyboard", 1, 79.00m),
        new(103, "USB-C Hub",           1, 39.00m),
        new(104, "Noise-Cancel Headset",1, 199.00m),
        new(201, "Clean Code",          2, 32.00m),
        new(202, "The Pragmatic Programmer", 2, 40.00m),
        new(203, "Refactoring",         2, 45.00m),
        new(301, "Ceramic Mug",         3, 12.00m),
        new(302, "Desk Lamp",           3, 34.00m),
        new(303, "Plant Pot",           3, 9.00m), // never ordered (used in challenge 7)
    ];

    public static readonly List<Customer> Customers =
    [
        new(1, "Aygerim", "Oral",     29, true,  new DateOnly(2023, 03, 14)),
        new(2, "Bolat",   "Almaty",   17, true,  new DateOnly(2024, 01, 02)),
        new(3, "Camila",  "Oral",     42, true,  new DateOnly(2022, 11, 30)),
        new(4, "Daniyar", "Astana",   35, false, new DateOnly(2021, 07, 19)),
        new(5, "Elena",   "Almaty",   24, true,  new DateOnly(2024, 06, 08)),
        new(6, "Farhad",  "Astana",   51, true,  new DateOnly(2020, 02, 27)),
    ];

    public static readonly List<Order> Orders =
    [
        new(1001, 1, new DateOnly(2024, 01, 10),
            [new(102, 1), new(201, 2)]),
        new(1002, 3, new DateOnly(2024, 01, 22),
            [new(104, 1), new(103, 1), new(301, 3)]),
        new(1003, 1, new DateOnly(2024, 02, 05),
            [new(101, 2), new(302, 1)]),
        new(1004, 5, new DateOnly(2024, 02, 18),
            [new(202, 1), new(203, 1)]),
        new(1005, 6, new DateOnly(2024, 03, 03),
            [new(104, 1), new(102, 1), new(301, 2)]),
        new(1006, 3, new DateOnly(2024, 03, 27),
            [new(203, 2), new(201, 1)]),
        // Customer 2 (Bolat) and 4 (Daniyar) never place an order -> used in challenge 7
    ];
}
