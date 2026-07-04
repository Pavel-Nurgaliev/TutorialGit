class Book:LibraryItem
{
    public Book(string title, string author) : base(title, author)
    {
    }

    public override int LoanPeriodDays => 21;
    public int PageCount { get; set; }
    public override string Describe()
    {
        return nameof(Book) + base.Describe();
    }
}
