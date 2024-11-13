using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _1662_Check_If_Two_String_Arrays_are_Equivalent
    {
        public static bool ArrayStringsAreEqual(string[] word1, string[] word2)
        {
            return string.Concat(word1) == string.Concat(word2);
        }
    }
}
