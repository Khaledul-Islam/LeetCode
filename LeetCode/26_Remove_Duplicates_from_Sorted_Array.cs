using System.Diagnostics;

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

        public static void ExecutionTime()
        {
            int[] x = [1, 1, 2];

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int result1 = RemoveDuplicates(x);
            stopwatch.Stop();
            Console.WriteLine($"RemoveDuplicates Result: {result1}");
            Console.WriteLine("Execution Time: " + stopwatch.Elapsed.TotalMilliseconds + " ms");
        }
    }
}
