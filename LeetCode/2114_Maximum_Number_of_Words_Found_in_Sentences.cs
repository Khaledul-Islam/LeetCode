using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _2114_Maximum_Number_of_Words_Found_in_Sentences
    {
        public static int MostWordsFound(string[] sentences)
        {
            return sentences.Select(a=>a.Split(' ')).Max(a=>a.Length);
        }
    }
}
