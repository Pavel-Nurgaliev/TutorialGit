//Question 7 — Implement IComparable<T> and IEquatable<T>.

//Write a Money type (your choice — class or struct) that holds an Amount (decimal) and a Currency (string). Implement:

//IEquatable < Money > — two Money values are equal when both Amount and Currency match.
//IComparable<Money> — money should sort by Amount.

//In your answer, also address:

//If you override Equals, what else should you override, and why?
//What should CompareTo do if the two values have different currencies (is comparing them even meaningful)?

//Write the full type. Take your time — 8+ to advance.

using System.Diagnostics.CodeAnalysis;

public readonly struct Money : IComparable<Money>, IEquatable<Money>
{
    public Money(decimal amount, string currency)
    {
        this.Amount = amount;
        this.Currency = currency;
    }
    public decimal Amount { get; }
    public string Currency { get; }
    public int CompareTo(Money other)
    {
        if (this.Currency != other.Currency)
        {
            throw new InvalidOperationException("Cannot compare money in different currencies.");
        }

        return this.Amount.CompareTo(other.Amount);
    }

    public bool Equals(Money other)
    {
        return this.Currency == other.Currency && this.Amount == other.Amount;
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Money other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, Currency);
    }
}


public record Person(string Name);
