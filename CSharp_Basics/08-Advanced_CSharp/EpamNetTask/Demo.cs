public static class Demo
{
    public static void Main()
    {
        Console.WriteLine(string.Join(", ", Solution.HowToUseCharSequenceGenerator(8, 'A', 'B')));
        Console.WriteLine(string.Join(", ", Solution.HowToUseIntegerSequenceGenerator(10, 1, 2)));
        Console.WriteLine(string.Join(", ", Solution.HowToUseFibonacciSequenceGenerator(8, 0, 1)));
        Console.WriteLine(string.Join(", ", Solution.HowToUseDoubleSequenceGenerator(10, 1.0, 2.0)
            .Select(d => d.ToString("F5"))));
        Console.WriteLine(string.Join(", ", Solution.HowToUseDelegateSequenceGenerator(8, 1, 1, (p, c) => p + c)));
    }
}
