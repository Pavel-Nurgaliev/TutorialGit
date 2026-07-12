public record Money
{
    private decimal sum;
    private string currency;

    public Money(decimal sum, string currency)
    {
        this.sum = sum;
        this.currency = currency;
    }

    public decimal Amount
    {
        get
        {
            return sum;
        }
        internal set
        {
            this.sum = value;
        }
    }

    public Money Add(Money money)
    {
        if (this.currency != money.currency)
        {
            throw new ArgumentException("Currencies are not same");
        }

        return new Money(this.Amount + money.Amount, this.currency);
    }

    public override string ToString()
    {
        return $"{Amount:0.00} {currency}";
    }
}