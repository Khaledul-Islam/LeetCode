using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _205_Isomorphic_Strings
    {
        public static bool IsIsomorphic(string s, string t)
        {
            var shortWord = new[] { s, t }.MinBy(a => a.Length);
            var lst = new Dictionary<string, string>();
            for (int i = 0; i < shortWord.Length; i++)
            {
                var ss = $"{s[i]}{t[i]}";
                try
                {
                    if(lst.ContainsKey($"{s[i]}") && lst.ContainsValue(ss))continue;
                    lst.Add($"{s[i]}", ss);
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
