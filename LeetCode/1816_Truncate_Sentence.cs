using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _1816_Truncate_Sentence
    {
        public static string TruncateSentence(string s, int k)
        {
            int spaceCount = 0;
            string word = string.Empty;
            for (int i = 0; i < s.Length; i++) 
            {
                if (s[i] == ' ') spaceCount++;
                if (k == spaceCount)
                {
                    break;
                }
                word += s[i];
               
            }
            return word;
        }
    }
}
