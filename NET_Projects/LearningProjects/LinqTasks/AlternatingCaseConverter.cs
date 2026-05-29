using System.Linq;

namespace LinqTasks
{
    internal static class AlternatingCaseConverter
    {/*each lowercase letter becomes uppercase and each uppercase letter becomes lowercase. */
        public static string ToAlternatingCase(this string s)
        {
            return new string(s.Select(c => char.IsUpper(c) ? char.ToLower(c) : char.ToUpper(c)).ToArray());
        }
    }
}
