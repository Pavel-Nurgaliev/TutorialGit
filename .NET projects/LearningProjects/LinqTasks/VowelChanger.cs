using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTasks
{
    internal static class VowelChanger
    {
        /*Replace all vowel to exclamation mark in the sentence*/
        private const string Vowels = "aeiouAEIOU";
        private const char ExclamationSymbol = '!';
        public static string Replace(this string s)
        {
            return new string(s.Select(c => IsVowel(c)? ExclamationSymbol : c).ToArray());
        }

        public static bool IsVowel(char c) => Vowels.Contains(c);
    }
}
