class Magazine : LibraryItem
{
    public Magazine(string title, string author) : base(title, author)
    {
    }

    public override int LoanPeriodDays => 3;

    public int IssueNumber { get; set; }
    public override string Describe()
    {
        return nameof(Magazine) + base.Describe();
    }
}
