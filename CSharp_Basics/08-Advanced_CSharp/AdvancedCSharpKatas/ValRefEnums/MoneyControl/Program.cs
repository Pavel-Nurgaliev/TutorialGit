
var a = new Money(10m, "USD");
var b = new Money(10m, "USD");
Console.WriteLine(a == b); // true — value equality, free from record. Class would print false because the == is an operatort hat  compares two objects by reference to the memory in the heap, but record provides value equality for free. Equality based on the stored values.
var c = a with { Amount = 20m };// new instance, a unchanged
Console.WriteLine(a.Add(new Money(5m, "USD")).Amount == 15m);
// a.Add(new Money(5m,"EUR")) throws