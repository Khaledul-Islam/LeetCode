using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _2185_Counting_Words_With_a_Given_Prefix
    {
        public static int PrefixCount(string[] words, string pref)
        {
            return (words.Select(word => word.IndexOf(pref, StringComparison.Ordinal))).Count(a=>a==0);
        }
    }
}
