using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _1967_Number_of_Strings_That_Appear_as_Substrings_in_Word
    {
        public static int NumOfStrings(string[] patterns, string word)
        {
            return patterns.Count(word.Contains);
        }
    }
}
