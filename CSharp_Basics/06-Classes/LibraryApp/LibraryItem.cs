abstract class LibraryItem
{
    private static int _nextId = 1;
    protected LibraryItem(string title, string author)
    {
        Id = _nextId++;
        Author = author;
        Title = title;
    }
    public int Id { get; private set; }
    public string Title { get; } = string.Empty;
    public string Author { get; } = string.Empty;
    public bool IsAvailable { get; private set; } = true;
    public abstract int LoanPeriodDays { get; }   // each type answers differently
    public DateTime BorrowedDate { get; private set; } = DateTime.MinValue;
    public virtual string Describe() => $"[{Id}] {Title} by {Author}. " + PrintAvailability();

    private string PrintAvailability()
    {
        return IsAvailable ? $"Available for borrowing. Loan period days {LoanPeriodDays}" :
                             $"Unavailable for borrowing. Remaining time until {BorrowedDate.AddDays(LoanPeriodDays).ToShortDateString()}";
    }

    public void Borrow()
    {
        if (!this.IsAvailable)
        {
            throw new InvalidOperationException("The item can not be borrowed, because it is not available");
        }

        BorrowedDate = DateTime.Today;

        IsAvailable = false;
    }
    public void Return()
    {
        BorrowedDate = DateTime.MinValue;
        IsAvailable = true;
    }
}
