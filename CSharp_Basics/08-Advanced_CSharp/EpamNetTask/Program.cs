// !!! Predefined class name 'Solution' and method names !!!
//                  !!! DO NOT CHANGE !!!

public static class Solution
{
    /// <summary>
    /// Generates a sequence of characters using the specified count, previous character, and current character.
    /// This method will call the `CharSequenceGenerator` to generate the sequence.
    /// </summary>
    /// <param name="count">The number of characters to generate in the sequence.</param>
    /// <param name="previous">The previous character in the sequence.</param>
    /// <param name="current">The current character in the sequence.</param>
    /// <returns>A list of characters representing the generated sequence.</returns>
    ///
    public static IList<char> HowToUseCharSequenceGenerator(int count, char previous, char current)
    {
        ISequenceGenerator<char> generator = new CharSequenceGenerator(previous, current);

        var sequence = new List<char> { generator.Previous, generator.Current };

        for (int i = 1; i <= count; i++)
        {
            sequence.Add(generator.Next);
        }

        return sequence;
    }

    /// <summary>
    /// Generates a sequence of integers using the specified count, previous integer, and current integer.
    /// This method will call the `IntegerSequenceGenerator` to generate the sequence.
    /// </summary>
    /// <param name="count">The number of integers to generate in the sequence.</param>
    /// <param name="previous">The previous integer in the sequence.</param>
    /// <param name="current">The current integer in the sequence.</param>
    /// <returns>A list of integers representing the generated sequence.</returns>
    public static IList<int> HowToUseIntegerSequenceGenerator(int count, int previous, int current)
    {
        ISequenceGenerator<int> generator = new IntegerSequenceGenerator(previous, current);

        var sequence = new List<int> { generator.Previous, generator.Current };

        for (int i = 1; i <= count; i++)
        {
            sequence.Add(generator.Next);
        }

        return sequence;
    }

    /// <summary>
    /// Generates a sequence of integers using the specified count, previous integer, and current integer.
    /// This method will call the `FibonacciSequenceGenerator` to generate the sequence.
    /// </summary>
    /// <param name="count">The number of integers to generate in the sequence.</param>
    /// <param name="previous">The previous integer in the sequence.</param>
    /// <param name="current">The current integer in the sequence.</param>
    /// <returns>A list of integers representing the generated sequence.</returns>
    public static IList<int> HowToUseFibonacciSequenceGenerator(int count, int previous, int current)
    {
        ISequenceGenerator<int> generator = new FibonacciSequenceGenerator(previous, current);

        var sequence = new List<int> { generator.Previous, generator.Current };

        for (int i = 1; i <= count; i++)
        {
            sequence.Add(generator.Next);
        }

        return sequence;
    }

    /// <summary>
    /// Generates a sequence of doubles using the specified count, previous double, and current double.
    /// This method will call the `DoubleSequenceGenerator` to generate the sequence.
    /// </summary>
    /// <param name="count">The number of doubles to generate in the sequence.</param>
    /// <param name="previous">The previous double in the sequence.</param>
    /// <param name="current">The current double in the sequence.</param>
    /// <returns>A list of doubles representing the generated sequence.</returns>
    public static IList<double> HowToUseDoubleSequenceGenerator(int count, double previous, double current)
    {
        ISequenceGenerator<double> generator = new DoubleSequenceGenerator(previous, current);

        var sequence = new List<double> { generator.Previous, generator.Current };

        for (int i = 1; i <= count; i++)
        {
            sequence.Add(generator.Next);
        }

        return sequence;
    }


    /// <summary>
    /// Generates a sequence of elements using the specified count, previous element, current element, and a delegate function.
    /// This method will call the `DelegateSequenceGenerator` to generate the sequence.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <param name="count">The number of elements to generate in the sequence.</param>
    /// <param name="previous">The previous element in the sequence.</param>
    /// <param name="current">The current element in the sequence.</param>
    /// <param name="nextFunc">The delegate function to calculate the next element in the sequence.</param>
    /// <returns>A list of elements representing the generated sequence.</returns>
    public static IList<T> HowToUseDelegateSequenceGenerator<T>(int count, T previous, T current,
        Func<T, T, T> nextFunc)
    {
        ISequenceGenerator<T> generator = new DelegateSequenceGenerator<T>(previous, current, nextFunc);

        var sequence = new List<T> { generator.Previous, generator.Current };

        while (sequence.Count < count)
        {
            sequence.Add(generator.Next);
        }

        return sequence.Take(count).ToList();
    }
}

public interface ISequenceGenerator<T>
{
    T Previous { get; }

    T Current { get; }

    T Next { get; }
}

public abstract class SequenceGenerator<T> : ISequenceGenerator<T>
{
    private T previous;
    private T current;

    protected SequenceGenerator(T previous, T current)
    {
        this.previous = previous;
        this.current = current;
        Count = 2;
    }

    public T Previous => this.previous;

    public T Current => this.current;

    public int Count { get; private set; }

    public T Next
    {
        get
        {
            T next = GetNext();

            this.previous = this.current;
            this.current = next;
            Count++;

            return next;
        }
    }

    protected abstract T GetNext();
}

public class CharSequenceGenerator : SequenceGenerator<char>
{
    private const int AlphabetLength = 26;

    public CharSequenceGenerator(char previous, char current)
        : base(previous, current)
    {
    }

    protected override char GetNext()
    {
        char origin = char.IsLower(Current) ? 'a' : 'A';

        int next = (Previous - origin + (Current - origin)) % AlphabetLength;

        return (char)(origin + next);
    }
}

public class IntegerSequenceGenerator : SequenceGenerator<int>
{
    public IntegerSequenceGenerator(int previous, int current)
        : base(previous, current)
    {
    }

    protected override int GetNext() => Current * 2;
}

public class DoubleSequenceGenerator : SequenceGenerator<double>
{
    public DoubleSequenceGenerator(double previous, double current)
        : base(previous, current)
    {
    }

    protected override double GetNext() => Current + Previous / Current;
}

public class FibonacciSequenceGenerator : SequenceGenerator<int>
{
    public FibonacciSequenceGenerator(int previous, int current)
        : base(previous, current)
    {
    }

    protected override int GetNext() => Previous + Current;
}

public class DelegateSequenceGenerator<T> : SequenceGenerator<T>
{
    private readonly Func<T, T, T> nextFunc;

    public DelegateSequenceGenerator(T previous, T current, Func<T, T, T> nextFunc)
        : base(previous, current)
    {
        this.nextFunc = nextFunc ?? throw new ArgumentNullException(nameof(nextFunc));
    }

    protected override T GetNext() => this.nextFunc(Previous, Current);
}
