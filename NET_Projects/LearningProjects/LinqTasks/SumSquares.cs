using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTasks
{
    /*Complete the square sum function so that it squares each number passed into it and then sums the results together.*/
    internal class SumSquares
    {
        public static int SquareSum(int[] numbers)
        {
            return numbers.Sum(x => x * x);
        }
    }
}
