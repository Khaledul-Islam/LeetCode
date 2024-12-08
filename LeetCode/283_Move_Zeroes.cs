using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _283_Move_Zeroes
    {
        public static void MoveZeroes(int[] nums)
        {
            int pos = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] != 0)
                {
                    nums[pos] = nums[i];
                    pos++;
                }
            }

            for (int i = pos; i < nums.Length; i++)
            {
                nums[i] = 0;
            }
            var ss = 0;
            //var zeros = new List<int>();
            //var withOutZeros = new List<int>();
            //foreach (var num in nums)
            //{
            //    if (num == 0)
            //    {
            //        zeros.Add(num);
            //    }
            //    else
            //    {
            //        withOutZeros.Add(num);
            //    }
            //}

            //nums = new int[nums.Length];
            //nums = withOutZeros.Concat(zeros).ToArray();

        }
    }
}
