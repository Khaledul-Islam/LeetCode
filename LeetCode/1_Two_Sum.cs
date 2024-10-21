using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _1_Two_Sum
    {
        public static int[] TwoSum(int[] nums, int target)
        {

            var map = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                var diff = target - nums[i];

                if (map.ContainsKey(nums[i]))
                {
                    return [map[nums[i]], i];
                }

                map.TryAdd(diff, i);

            }
           
            return [];
        }
        public static int[] TwoSumBeginner(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i+1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        Console.WriteLine(i + "," + j);
                        Console.ReadKey();
                        return [nums[i],nums[j]];
                    }
                }
                
            }

            return [];
        }

        public static void ExecutionTime()
        {
            int[] nums = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20,21,22,23,24,25,26,27,28,29,30];
            int target = 57;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int[] resultHashMap = TwoSum(nums, target);
            stopwatch.Stop();
            Console.WriteLine("TwoSum (HashMap) Result: [" + resultHashMap[0] + ", " + resultHashMap[1] + "]");
            Console.WriteLine("Execution Time (HashMap): " + stopwatch.Elapsed.TotalMilliseconds + " ms");

            // Measure execution time for TwoSumBeginner (Brute Force Approach)
            stopwatch.Restart();
            int[] resultBruteForce = TwoSumBeginner(nums, target);
            stopwatch.Stop();
            Console.WriteLine("TwoSumBeginner (Brute Force) Result: [" + resultBruteForce[0] + ", " + resultBruteForce[1] + "]");
            Console.WriteLine("Execution Time (Brute Force): " + stopwatch.Elapsed.TotalMilliseconds + " ms");

        }
    }
}
