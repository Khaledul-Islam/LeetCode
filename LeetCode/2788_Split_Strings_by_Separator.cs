using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _2788_Split_Strings_by_Separator
    {
        public static IList<string> SplitWordsBySeparator(IList<string> words, char separator)
        {
            var splitedWords = words.Select(a => a.Split(separator, StringSplitOptions.RemoveEmptyEntries));

            return splitedWords.SelectMany(a=>a).ToList();
        }
    }
}
