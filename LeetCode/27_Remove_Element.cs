using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _27_Remove_Element
    {
        public static int RemoveElement(int[] nums, int val)
        {
            var result = nums.Where(a => a != val).ToArray();

            for (int i = 0; i < result.Length; i++)
            {
                nums[i] = result[i];
            }

            return result.Length;
        }
        public static void ExecutionTime()
        {
            int[] x = [0, 1, 2, 2, 3, 0, 4, 2];

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int result1 = RemoveElement(x,2);
            stopwatch.Stop();
            Console.WriteLine($"RemoveDuplicates Result: {result1}");
            Console.WriteLine("Execution Time: " + stopwatch.Elapsed.TotalMilliseconds + " ms");
        }
    }
}
