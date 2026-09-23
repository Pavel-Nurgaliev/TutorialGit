public record Money : IComparable<Money>
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
        set
        {
            this.sum = value;
        }
    }

    public string Currency => currency;

    public Money Add(Money money)
    {
        if (this.currency != money.currency)
        {
            throw new ArgumentException("Currencies are not same");
        }

        return new Money(this.Amount + money.Amount, this.currency);
    }

    public int CompareTo(Money other)
    {
        var result = this.Currency.CompareTo(other.Currency);

        if (result != 0)
        {
            return result;
        }

        result = this.Amount.CompareTo(other.Amount);

        return result;
    }

    public override string ToString()
    {
        return $"{Amount:0.00} {currency}";
    }

    public static Money operator *(Money v1, int val)
    {
        return new Money(v1.Amount * val, v1.Currency);
    }
    public static Money operator /(Money v1, int val)
    {
        return new Money(v1.Amount / val, v1.Currency);
    }
    public static Money operator -(Money v1, int val)
    {
        return new Money(v1.Amount - val, v1.Currency);
    }
    public static Money operator -(Money v1, Money v2)
    {
        if (v1.Currency != v2.Currency)
        {
            throw new ArgumentException($"Currency v1 {v1.Currency} and v2 {v2.Currency} are not same");
        }

        return new Money(v1.Amount - v2.Amount, v1.Currency);
    }
}