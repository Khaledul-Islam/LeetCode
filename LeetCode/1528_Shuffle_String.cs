using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _1528_Shuffle_String
    {
        public static string RestoreString(string s, int[] indices)
        {
            var sufChar = new char[indices.Length];

            for (var i = 0; i < s.Length; i++)
            {
                sufChar[indices[i]] = s[i];
            }

            return new string(sufChar);
        }
    }
}
