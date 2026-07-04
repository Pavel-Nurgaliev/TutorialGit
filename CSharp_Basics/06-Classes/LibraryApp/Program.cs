/*Task: model a small Library (console app)
No fancy UI needed. The whole point is clean class design, so success is measured by the shape of your types, not the output.
Stage 1 — Core class & encapsulation. Create an abstract LibraryItem with Id, Title, Author, and IsAvailable. Give Id a private setter and auto-assign it from a static counter so IDs are unique across all items (this is your static-vs-instance rep). IsAvailable must only be changeable from inside the class — no public setter. A constructor sets title and author.
csharppublic abstract class LibraryItem
{
    private static int _nextId = 1;
    public int Id { get; }
    public string Title { get; }
    public string Author { get; }
    public bool IsAvailable { get; private set; } = true;

    protected LibraryItem(string title, string author)
    {
        Id = _nextId++;
        Title = title;
        Author = author;
    }

    public abstract int LoanPeriodDays { get; }   // each type answers differently
    public virtual string Describe() => $"[{Id}] {Title} by {Author}";

}
Stage 2 — Inheritance & polymorphism. Add three subclasses, each overriding LoanPeriodDays and Describe() with type-specific data: Book (21-day loan, plus PageCount), Dvd (7-day loan, plus RuntimeMinutes), Magazine (3-day loan, plus IssueNumber). This is where override and abstract members earn their keep.
Stage 3 — Behaviour & invariants. Add a Member (name, id, a list of borrowed items, and a max-loans limit like 5) and a Library that holds a List<LibraryItem> catalogue and members. Give Library methods AddItem, RegisterMember, Borrow(memberId, itemId), and Return(itemId). Borrowing must refuse if the item is unavailable or the member is at their limit — enforce those rules inside the methods, so bad states are impossible from outside. Compute the due date as DateTime.Today.AddDays(item.LoanPeriodDays).
Stage 4 — The polymorphism payoff. Write one method that loops the catalogue and calls item.Describe() on each. Same call, three different outputs, zero if (item is Book) branching. If you feel the urge to type-check, that's the smell polymorphism removes.*/

using System.Text;

var library = new Library();

var book = new Book("csharp course", "noname1");
library.AddItem(book);
library.AddItem(new Book("asp.net course", "noname2"));
library.AddItem(new Book("sql course", "noname3"));

library.AddItem(new Magazine("louis vuitton", "big name1"));
library.AddItem(new Magazine("givenchy", "big name2"));
library.AddItem(new Magazine("guccie", "big name3"));

library.AddItem(new Dvd("The wire", "name1"));
library.AddItem(new Dvd("peaky blanders", "name2"));
library.AddItem(new Dvd("naruto", "name3"));

var firstMember = new Member("Pavel Nurgaliev", 4);

library.RegisterMember(firstMember);

var dueDate = library.Borrow(firstMember.Id, book.Id);
Console.WriteLine($"Due back on {dueDate.ToShortDateString()}");

var sbItemsDescription = new StringBuilder();
foreach (var item in library.ListItems)
{
    sbItemsDescription.AppendLine(item.Describe());
}

Console.WriteLine(sbItemsDescription);