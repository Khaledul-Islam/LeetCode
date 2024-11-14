using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _2496_Maximum_Value_of_a_String_in_an_Array
    {
        public static int MaximumValue(string[] strs)
        {
            var hashSet = new HashSet<int>();
            foreach (var str in strs)
            {
                try
                {
                    hashSet.Add(Convert.ToInt32(str));
                }
                catch (Exception e)
                {
                    hashSet.Add(str.Length);
                }
            }

            return hashSet.MaxBy(a => a);
        }
    }
}
