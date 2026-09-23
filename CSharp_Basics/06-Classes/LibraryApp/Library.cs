class Library
{
    private List<Member> _members = new List<Member>();
    private List<LibraryItem> _items = new List<LibraryItem>();

    public List<LibraryItem> ListItems => _items;

    public void AddItem(LibraryItem libraryItem)
    {
        _items.Add(libraryItem);
    }
    public void RegisterMember(Member member)
    {
        _members.Add(member);
    }
    public DateTime Borrow(int memberId, int itemId)
    {
        var member = _members.FirstOrDefault(m => m.Id == memberId, null);
        if (member == null)
        {
            throw new NullReferenceException($"member {memberId} is not found");
        }

        var borrowedItem = _items.FirstOrDefault(i => i.Id == itemId, null);
        if (borrowedItem == null)
        {
            throw new NullReferenceException($"member {itemId} is not found");
        }

        if (member.ListBorrowedItems.Count >= member.MaxLoanLimit)
        {
            throw new InvalidOperationException("The item can not be borrowed, because max-loan limmit is reached already");
        }

        borrowedItem.Borrow();

        member.AddBorrowedItem(borrowedItem);

        return DateTime.Today.AddDays(borrowedItem.LoanPeriodDays);
    }
    public void Return(int itemId)
    {
        var borrowedItem = _items.FirstOrDefault(i => i.Id == itemId, null);

        if (borrowedItem == null)
        {
            throw new NullReferenceException($"borrowed item {itemId} is not found");
        }

        var member = _members.FirstOrDefault(m => m.ListBorrowedItems.Contains(borrowedItem), null);

        if (member == null)
        {
            throw new NullReferenceException($"member with borrowed item {itemId} is not found");
        }

        if (member.ListBorrowedItems.Contains(borrowedItem))
        {
            borrowedItem.Return();

            member.RemoveBorrowedItem(borrowedItem);
        }
    }
}
