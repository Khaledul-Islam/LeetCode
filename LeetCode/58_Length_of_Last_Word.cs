using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _58_Length_of_Last_Word
    {
        public static int LengthOfLastWord(string s)
        {
            var arrayList = s.Split(' ');
            return arrayList.LastOrDefault(a => !string.IsNullOrEmpty(a))?.Length??0;
        }
    }
}
