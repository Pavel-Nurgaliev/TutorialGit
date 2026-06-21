namespace DigitCounterConsolApp
{
    /*Take an integer n (n >= 0) and a digit d (0 <= d <= 9) as an integer.
Square all numbers k (0 <= k <= n) between 0 and n.
Count the numbers of digits d used in the writing of all the k**2.
Implement the function taking n and d as parameters and returning this count.
    */

    public class DigCounter
    {
        private const string NumberInvalidMessage = "The number {0} invalid.";
        private const string DigitInvalidMessage = "The digit {0} invalid.";

        public static int NbDig(int n, int d)
        {
            if (!IsNumberValid(n))
            {
                throw new ArgumentException(string.Format(NumberInvalidMessage, n));
            }
            if (!IsDigitValid(d))
            {
                throw new ArgumentException(string.Format(DigitInvalidMessage, d));
            }

            var squares = GetSquares(n);

            var digitString = Convert.ToString(d);
            var result = squares.Where(s => s.Contains(digitString)).Sum(s => s.Count(ss => ss == digitString[0]));

            return result;
        }

        private static IEnumerable<string> GetSquares(int n)
        {
            for (int i = 0; i <= n; i++)
            {
                yield return (i * i).ToString();
            }
        }

        private static bool IsDigitValid(int d) => (d >= 0) && (d <= 9);

        public static bool IsNumberValid(int n) => n > 0;
    }
}
