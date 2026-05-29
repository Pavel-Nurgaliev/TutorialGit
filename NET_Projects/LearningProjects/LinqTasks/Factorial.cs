using System;
using System.Collections;
/*Given a non-empty array of integers, return the result of multiplying the values together in order. Example:

[1, 2, 3, 4] => 1 * 2 * 3 * 4 = 24*/
namespace ReduceButGrow
{
    internal class Factorial
    {
        public static int Grow(int[] x)
        {
            Array.Sort(x);

            return x.Aggregate(1, (acc, val) => acc * val);
        }
    }

}