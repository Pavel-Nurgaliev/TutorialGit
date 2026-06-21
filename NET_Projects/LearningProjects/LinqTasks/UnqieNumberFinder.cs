using System.Linq;

namespace LinqTasks
{
    internal static class UnqieNumberFinder
    {
        private const int UniqueNumber = 1;
        public static int GetUnique(IEnumerable<int> array)
        {
            var number = array.GroupBy(digit => digit)
                              .Where(groupItem => groupItem.Count() == 1)
                              .Select(g => g.Key)
                              .First();

            return number;
        }

        public static int GetUnique2(IEnumerable<int> array)
        {
            var numbers = array.OrderBy(a=>a).ToArray();

            return numbers[0] != numbers[1] ? numbers.First() : numbers.Last();
        }
    }
}
