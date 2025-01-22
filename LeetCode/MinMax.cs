using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class MinMax
    {
        public static void FindMinMax(int[] nums)
        {
            var min = nums.Min();
            var max = nums.Max();
        }

        public static void FindMinMax_2(int[] nums)
        {
            var min = nums[0];
            var max = nums[0];
            foreach (var num in nums)
            {
                if (min > num)
                {
                    min = num;
                }
                if (max < num)
                {
                    max = num;
                }
            }

            var ss = "";
        }
    }
}
