using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _2108_Find_First_Palindromic_String_in_the_Array
    {
        public static string FirstPalindrome(string[] words)
        {
            foreach (var word in words)
            {
                if (word == string.Concat(word.Reverse()))
                {
                    return word;
                }
            }

            return string.Empty;
        }
    }
}
