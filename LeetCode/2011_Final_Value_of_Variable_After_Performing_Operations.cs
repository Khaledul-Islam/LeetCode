using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _2011_Final_Value_of_Variable_After_Performing_Operations
    {
        public static int FinalValueAfterOperations(string[] operations)
        {
            var dic  = new Dictionary<string, int>();
            dic.Add("X++",1);
            dic.Add("++X",1);
            dic.Add("X--",-1);
            dic.Add("--X",-1);
            int x = 0;
            foreach (var op in operations)
            {
                if (dic.TryGetValue(op, out var value))
                {
                    x = x + (value);
                }
            }

            return x;
        }
    }
}
