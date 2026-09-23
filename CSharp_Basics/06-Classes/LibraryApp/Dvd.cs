class Dvd : LibraryItem
{
    public Dvd(string title, string author) : base(title, author)
    {
    }

    public override int LoanPeriodDays => 7;
    public int RuntimeMinutes { get; set; }
    public override string Describe()
    {
        return nameof(Dvd) + base.Describe();
    }
}
