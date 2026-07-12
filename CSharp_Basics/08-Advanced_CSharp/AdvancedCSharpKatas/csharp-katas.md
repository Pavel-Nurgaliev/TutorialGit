# C# Practical Track: Types, Generics & Collections

15 katas ordered from warm-up to capstone. Each one names the concepts it drills, gives a spec + signature, sample cases to check yourself against, and a "gotcha" that's the actual point of the exercise. Do them in order — later ones assume types you built earlier.

**Rules of the game**
- Write your own tests (xUnit/NUnit) or a `Main` with asserts. Every kata lists cases you must pass.
- No LINQ on katas 1–6 unless stated — you're building intuition for the primitives LINQ hides.
- When a kata says "explain," write a one-line comment with the answer. If you can't, you haven't finished it.

---

## Tier 1 — Value vs reference, enums, records (katas 1–4)

### Kata 1 — `Point` struct and the mutation trap
**Concepts:** structs, value types, boxing.

Build a `struct Point { public int X; public int Y; }` with a method `Point Moved(int dx, int dy)` that returns a *new* point (does not mutate `this`).

Then answer in comments:
- Given `var a = new Point(1,1); var b = a; b.X = 99;` — what is `a.X`? Why?
- What does `object o = a;` do at the runtime level, and what's the cost of doing it in a loop a million times?

**Check:**
```
var a = new Point(1, 1);
var c = a.Moved(2, 3);   // c == (3,4), a still (1,1)
var b = a; b.X = 99;     // a.X == 1
```
**Gotcha:** copy-by-value is the whole lesson. If `a.X` came out 99 you accidentally made a class.

---

### Kata 2 — Traffic light state machine with an enum
**Concepts:** enums, exhaustive `switch`.

Model `enum Light { Red, Green, Yellow }`. Write `Light Next(Light current)` implementing Red→Green→Yellow→Red.

Then add `enum Permissions` as a `[Flags]` enum (`None=0, Read=1, Write=2, Execute=4`) and a method `bool CanWrite(Permissions p)`.

**Check:**
```
Next(Light.Red) == Light.Green
Next(Light.Yellow) == Light.Red
var p = Permissions.Read | Permissions.Write;
CanWrite(p) == true;
p.HasFlag(Permissions.Execute) == false;
```
**Gotcha:** flag values must be powers of two. Explain in a comment why `Read | Write == 3`.

---

### Kata 3 — `Money` as a record
**Concepts:** records, value equality, `with` expressions, immutability.

Define `record Money(decimal Amount, string Currency)`. Add:
- `Money Add(Money other)` — throw if currencies differ.
- Override `ToString()` to render `"12.50 USD"`.

**Check:**
```
var a = new Money(10m, "USD");
var b = new Money(10m, "USD");
a == b;                              // true — value equality, free from record
var c = a with { Amount = 20m };     // new instance, a unchanged
a.Add(new Money(5m,"USD")).Amount == 15m;
// a.Add(new Money(5m,"EUR")) throws
```
**Gotcha:** you got `a == b` for free. Explain why the same two values as a `class` would print `false`.

---

### Kata 4 — Records vs structs vs classes, side by side
**Concepts:** value vs reference semantics; when to reach for each.

No new code — extend kata 1 & 3. Fill a comment table for `Point` (struct), `Money` (record), and a hypothetical `class Account`:

| Type | Copied by | `==` compares | Lives on | Good when |
|------|-----------|---------------|----------|-----------|

Point - struct - copied by value, == doesn't support by default. .Equals on strucе does work through reflection (field-by-field) which is slow. User implementation ==/!= developer must define by themselves, lives on wherever it's declared (if method - stack, if field of class - heap, if boxed - heap), good when is used as value-like immutable object
Money - record (class (by default)/struc) - copied by reference (for class) by value (for struc) - provides == value equality by default. a record struct gets value equality and ==/!= generated - lives on stack/heap (struc/class consequently) - immutable data with value equality (good for storing data without changing its state)
Account - class - copied by reference - == provides reference equality by default - lives on heap - good when is used as mutable state type, which constantly update their state during the app

**Gotcha:** the "Good when" column is the interview answer. Struct = small, immutable, value-like (coordinates, money). Record = immutable data with value equality (DTOs). Class = identity + mutable state (an account you keep updating).

---

## Tier 2 — Implementing the core interfaces (katas 5–7)

### Kata 5 — `IEquatable<T>` on `SemanticVersion`
**Concepts:** `IEquatable<T>`, `GetHashCode` contract.

`class SemanticVersion : IEquatable<SemanticVersion>` with `Major, Minor, Patch`. Implement `Equals(SemanticVersion)`, override `Equals(object)` and `GetHashCode()` consistently.

**Check:**
```
var v1 = new SemanticVersion(1,2,3);
var v2 = new SemanticVersion(1,2,3);
v1.Equals(v2);                        // true
var set = new HashSet<SemanticVersion>{ v1, v2 };
set.Count == 1;                       // proves hashcode + equals agree
```
**Gotcha:** if `set.Count` is 2, your `GetHashCode` disagrees with `Equals`. Two equal objects **must** return the same hash. Use `HashCode.Combine(Major, Minor, Patch)`.

---

### Kata 6 — `IComparable<T>` so versions sort
**Concepts:** `IComparable<T>`, total ordering.

Extend `SemanticVersion` with `IComparable<SemanticVersion>`. Compare by Major, then Minor, then Patch.

**Check:**
```
var list = new List<SemanticVersion> {
    new(2,0,0), new(1,5,0), new(1,5,3), new(1,0,0)
};
list.Sort();   // -> 1.0.0, 1.5.0, 1.5.3, 2.0.0
```
**Gotcha:** `List.Sort()` "just works" once you implement `CompareTo`. Explain what contract `Sort` relies on and what happens if your `CompareTo` is inconsistent with `Equals`.

---

### Kata 7 — Abstract class vs interface: a notification system
**Concepts:** abstract classes, interfaces, when each fits.

- `interface INotifier { void Send(string message); }`
- `abstract class Notifier : INotifier` with shared field `string Recipient`, a protected `Format(string)` helper, and `abstract void Send(string)`.
- Concrete `EmailNotifier` and `SmsNotifier` overriding `Send`.

Then write, in comments, the rule you'd give a teammate for choosing interface vs abstract class.

**Check:**
```
INotifier n = new EmailNotifier("me@x.com");
n.Send("hi");   // uses shared Format() from the base
```
**Gotcha:** the answer to "which?" — interface = a capability/contract many unrelated types can implement (and you can implement several). Abstract class = shared implementation + state for a family of related types (single inheritance). If you're copy-pasting method bodies between implementers, you wanted an abstract base.

---

## Tier 3 — Generics and constraints (katas 8–10)

### Kata 8 — Generic `Stack<T>` from scratch
**Concepts:** generics, no constraint needed.

Implement `class Stack<T>` backed by an array (no `System.Collections`): `Push`, `Pop` (throw on empty), `Peek`, `Count`, and make it `IEnumerable<T>` iterating top-to-bottom.

**Check:**
```
var s = new Stack<int>();
s.Push(1); s.Push(2); s.Push(3);
s.Peek() == 3;
s.Pop() == 3;
s.Count == 2;
string.Join(",", s) == "2,1";   // enumerates top-first
```
**Gotcha:** implementing `IEnumerable<T>` with `yield return` is the cleanest path. Feel where the generic type parameter `T` frees you from `object` + casting.

---

### Kata 9 — Constrained generic method `Max<T>`
**Concepts:** generic constraints (`where T : IComparable<T>`).

Write `static T Max<T>(IEnumerable<T> items) where T : IComparable<T>` returning the largest element; throw on empty.

**Check:**
```
Max(new[] { 3, 7, 2 }) == 7;
Max(new[] { "apple", "pear", "fig" }) == "pear";
Max(new[] {
    new SemanticVersion(1,0,0), new SemanticVersion(2,1,0)
}).Major == 2;   // reuses kata 6!
```
**Gotcha:** without the `where T : IComparable<T>` constraint, `a.CompareTo(b)` won't compile — `T` could be anything. The constraint is what unlocks the method call. This is why kata 6 mattered.

---

### Kata 10 — Generic `Repository<T>` with an entity constraint
**Concepts:** generics + interface constraint, `Dictionary` as a store.

- `interface IEntity { int Id { get; } }`
- `class Repository<T> where T : IEntity` backed by `Dictionary<int, T>`: `Add`, `GetById(int)` (return `T?` or throw — your call, document it), `Remove(int)`, `GetAll()` returning `IEnumerable<T>`.
- A sample `record User(int Id, string Name) : IEntity`.

**Check:**
```
var repo = new Repository<User>();
repo.Add(new User(1, "Ada"));
repo.Add(new User(2, "Linus"));
repo.GetById(1)!.Name == "Ada";
repo.GetAll().Count() == 2;
repo.Remove(1);
repo.GetById(1) == null;   // if you chose the nullable route
```
**Gotcha:** the constraint `where T : IEntity` is what lets you read `item.Id` inside the repo to key the dictionary. Without it, `T` has no `Id`.

---

## Tier 4 — Collections deep dive (katas 11–13)

### Kata 11 — Word frequency with `Dictionary`
**Concepts:** `Dictionary<TKey,TValue>`, `TryGetValue` / indexer patterns.

`Dictionary<string,int> WordCount(string text)` — split on whitespace, lowercase, count occurrences.

**Check:**
```
WordCount("the cat the dog the")["the"] == 3;
WordCount("a A a").Keys.Count == 1;   // case-insensitive
```
**Gotcha:** compare `dict[key] = dict.GetValueOrDefault(key) + 1;` vs a `TryGetValue` version vs a `ContainsKey` check. Note in a comment which does the fewest lookups.

---

### Kata 12 — Anagram groups with `Dictionary` + sorted key
**Concepts:** `Dictionary`, `List` as a value, building keys.

`IEnumerable<List<string>> GroupAnagrams(IEnumerable<string> words)` — group words that are anagrams (sort each word's letters to form the group key).

**Check:**
```
GroupAnagrams(new[]{"eat","tea","tan","ate","nat","bat"})
// -> groups: {eat,tea,ate}, {tan,nat}, {bat}
```
**Gotcha:** the key is `new string(word.OrderBy(c=>c).ToArray())`. This is the first kata where LINQ is allowed — notice how much it compresses.

---

### Kata 13 — Set operations with `HashSet`
**Concepts:** `HashSet<T>`, O(1) membership, set algebra.

Implement, using only `HashSet` operations (no manual loops for the set logic):
- `FirstDuplicate(IEnumerable<int>)` → first value seen twice, or `null`.
- `Intersection(a, b)`, `OnlyInFirst(a, b)` (difference).

**Check:**
```
FirstDuplicate(new[]{1,2,3,2,1}) == 2;
FirstDuplicate(new[]{1,2,3}) == null;
Intersection(new[]{1,2,3}, new[]{2,3,4}) -> {2,3}
OnlyInFirst(new[]{1,2,3}, new[]{2,3,4}) -> {1}
```
**Gotcha:** `IntersectWith` / `ExceptWith` mutate the set in place — clone first if you need the original. Explain why `HashSet` makes `FirstDuplicate` O(n) vs O(n²) with a `List.Contains`.

---

## Tier 5 — Deferred execution & capstone (katas 14–15)

### Kata 14 — `IEnumerable` laziness and `IQueryable`
**Concepts:** `IEnumerable<T>`, `yield return`, deferred execution, `IEnumerable` vs `IQueryable`.

1. Write `IEnumerable<int> Naturals()` that yields 1,2,3… forever with `yield return`. Prove it's lazy: `Naturals().Take(5)` must terminate.
2. Write `IEnumerable<int> WhereEven(IEnumerable<int> src)` with `yield return` and a `Console.WriteLine` inside. Show that nothing prints until you enumerate (deferred execution).
3. In comments, explain the difference between `IEnumerable<T>` and `IQueryable<T>`: which runs the predicate as *compiled delegates in memory*, and which turns your lambda into an *expression tree* a provider (e.g. EF Core → SQL) translates. When does calling `.Where(...)` on an `IQueryable` hit the database vs the app?

**Check:**
```
Naturals().Take(5).ToList();          // [1,2,3,4,5], doesn't hang
var q = WhereEven(Naturals());        // prints nothing yet
q.Take(3).ToList();                   // NOW it prints/runs -> [2,4,6]
```
**Gotcha:** the mental model — `IEnumerable` = "give me items one at a time, filtering happens here in C#." `IQueryable` = "here's a description of what I want; the provider decides how to fetch it." Calling `.ToList()` too early on an `IQueryable` pulls the whole table into memory, then filters — the classic performance bug.

---

### Kata 15 — Capstone: generic in-memory `InventorySystem`
**Concepts:** everything — records, enums, `IComparable`/`IEquatable`, generics + constraint, `Dictionary`/`List`/`HashSet`, `IEnumerable`, interface + abstract class.

Build a small inventory service that reuses your earlier pieces:

- `enum Category { Electronics, Food, Clothing }`
- `record Product(int Id, string Name, Money Price, Category Category) : IEntity` — reuse `Money` (kata 3) and `IEntity` (kata 10). Make `Product` sortable by price via `IComparable<Product>`.
- Store products in your `Repository<Product>` (kata 10).
- `IEnumerable<Product> InCategory(Category c)` — deferred, `yield return`.
- `IEnumerable<Product> CheapestFirst()` — returns products sorted by price (uses `IComparable`).
- `HashSet<Category> CategoriesInStock()` — distinct categories present.
- `Dictionary<Category, int> CountByCategory()`.
- An `abstract class PricingRule { abstract Money Apply(Money price); }` with two concretes: `PercentageDiscount` and `FlatDiscount`. Apply a rule across all products.

**Check (sketch):**
```
var sys = new InventorySystem();
sys.Add(new Product(1,"Phone", new Money(800m,"USD"), Category.Electronics));
sys.Add(new Product(2,"Bread", new Money(3m,"USD"),   Category.Food));
sys.CheapestFirst().First().Name == "Bread";
sys.CategoriesInStock().SetEquals(new[]{Category.Electronics, Category.Food});
sys.CountByCategory()[Category.Food] == 1;
var rule = new PercentageDiscount(10);
rule.Apply(new Money(800m,"USD")).Amount == 720m;
```
**Gotcha:** if this kata feels easy, the earlier ones did their job — you're just composing types you already built. If a piece fights you, that's the topic to review.

---

## Coverage map

| Topic | Katas |
|-------|-------|
| Interfaces vs abstract classes | 7, 15 |
| `IComparable` / `IEquatable` | 5, 6, 9, 15 |
| Structs, records, enums; value vs reference | 1, 2, 3, 4, 15 |
| Generics + constraints (Stack, Repository) | 8, 9, 10, 15 |
| `List` / `Dictionary` / `HashSet` | 10, 11, 12, 13, 15 |
| `IEnumerable` vs `IQueryable` | 8, 14, 15 |

## Suggested order & pacing
Tier 1 in one sitting (they're quick). Tiers 2–3 are the meat — one kata per session, don't rush the "explain" comments. Tier 4 is a good day of collection reps. Save kata 15 for when tiers 1–4 are green; it's the exam.

## Real katas to pair with these
If you want graded versions on the platforms you mentioned: on Exercism's C# track do *Grains*, *Two Fer*, *High Scores* (collections), *Circular Buffer* and *Simple Linked List* (generics), *Complex Numbers* (structs/equality). On Codewars, filter by C# + collections/generics tags around 6–7 kyu to match this difficulty.
