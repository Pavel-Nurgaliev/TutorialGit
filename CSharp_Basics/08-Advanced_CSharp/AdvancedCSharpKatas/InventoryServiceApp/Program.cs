using InventoryServiceApp;

var sys = new InventorySystem();
sys.Add(new Product(1, "Phone", new Money(800m, "USD"), Category.Electronics));
sys.Add(new Product(2, "Bread", new Money(3m, "USD"), Category.Food));
bool cheapest = sys.CheapestFirst().First().Name == "Bread";
var cis = sys.CategoriesInStock();
bool isFoodEqual = sys.CountByCategory()[Category.Food] == 1;
var rule = new PercentageDiscount(10);
var ruleEquals = rule.Apply(new Money(800m, "USD")).Amount == 720m;
