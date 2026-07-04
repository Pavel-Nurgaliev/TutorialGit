class Member
{
    private static int _nextId = 1;
    public Member(string name, int maxLoanLimit = 5)
    {
        Id = _nextId++;
        Name = name;
        MaxLoanLimit = maxLoanLimit;
        _borrowedItems = new List<LibraryItem>();
    }
    public int Id { get; private set; }
    public string Name { get; private set; }
    public int MaxLoanLimit { get; private set; }

    private readonly List<LibraryItem> _borrowedItems;
    public IReadOnlyList<LibraryItem> ListBorrowedItems => _borrowedItems;

    public void AddBorrowedItem(LibraryItem item) => _borrowedItems.Add(item);
    public void RemoveBorrowedItem(LibraryItem item) => _borrowedItems.Remove(item);
}
