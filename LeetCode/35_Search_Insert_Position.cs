using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _35_Search_Insert_Position
    {
        public static int SearchInsert(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == target)
                {
                    return i;
                }

                if (nums[i] > target)
                {
                    return i;
                }
            }

            return nums.Length;
        }
    }
}
