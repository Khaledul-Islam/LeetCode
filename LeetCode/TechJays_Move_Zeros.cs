using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class TechJays_Move_Zeros
    {
        public static void MoveZeros()
        {
            var nums = new int[]{ 0,1,0,2,0,0,4,5,6,7,8,0,8,0,5,4,3,4,0,6};
            var k = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if(nums[i] != 0)
                {
                    nums[k] =nums[i];
                    k++;
                }
            }
            for (int i = k;i < nums.Length; i++)
            {
                nums[i] = 0;
            }
        }
    }
}
