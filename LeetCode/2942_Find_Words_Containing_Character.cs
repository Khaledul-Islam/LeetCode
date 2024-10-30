using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _2942_Find_Words_Containing_Character
    {
        public static IList<int> FindWordsContaining(string[] words, char x)
        {
            int i = 0;
            var result = new List<int>();
            foreach (string word in words)
            {
                if (word.Contains(x))
                {
                    result.Add(i);
                }

                i++;
            }

            return result;
        }
    }
}
