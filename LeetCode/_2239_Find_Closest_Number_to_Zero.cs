using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class TechjaysPairStringSumNearZero
    {
        public static int PairStringSumNearZero(int[] nums)
        {
            var allPossibleSum = new List<int>();
            for (int i = 0; i < nums.Length-1; i++)
            {
                for (int j = i; j < nums.Length-1; j++)
                {
                    var sum = Math.Abs(nums[i] + nums[j]);
                    allPossibleSum.Add(sum);
                }
            }

            var ss = allPossibleSum.Min();
            return ss;
        }
    }
}
