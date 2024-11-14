using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _2418_Sort_the_People
    {
        public static string[] SortPeople(string[] names, int[] heights)
        {
            var dictionary = names.Zip(heights, (n, h) => new { name = n, height = h });

            return dictionary.OrderByDescending(a=>a.height).Select(a=>a.name).ToArray();
        }
    }   
}
