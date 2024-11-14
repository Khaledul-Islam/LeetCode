using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _2828_Check_if_a_String_Is_an_Acronym_of_Words
    {
        public static bool IsAcronym(IList<string> words, string s)
        {
            return s == string.Concat(words.Select(a => a.First()));
        }
    }
}
