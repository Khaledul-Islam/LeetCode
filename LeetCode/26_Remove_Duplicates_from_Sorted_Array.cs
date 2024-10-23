using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LeetCode
{
    public static class _26_Remove_Duplicates_from_Sorted_Array
    {
        public static int RemoveDuplicates(int[] nums)
        {
            var result = nums.Distinct().ToArray();

            for (int i = 0; i < result.Length; i++)
            {
                nums[i]= result[i];
            }

            return result.Length;
        }
    }
}
