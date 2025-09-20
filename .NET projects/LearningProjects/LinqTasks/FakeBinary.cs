using System.Text;

namespace LinqTasks
{
    /*Given a string of digits, you should replace any digit below 5 with '0' and any digit 5 and above with '1'. Return the resulting string.*/
    internal class FakeBinary
    {
        private const char ZeroChar = '0';
        private const char OneChar = '1';
        public static string FakeBin(string x)
        {
            return new string(x.Select(c => c - ZeroChar < 5 ? ZeroChar : OneChar).ToArray());
        }
    }
}
