using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _14_Longest_Common_Prefix
    {
        public static string LongestCommonPrefix(string[] strs)//Input: strs = ["flower","flow","flight"] //Output: "fl"
        {
            string? minText = strs.OrderBy(a => a.Length).FirstOrDefault();
            if (minText == null)
            {
                return string.Empty;
            }
            var others = strs.Where(a => a != minText).ToArray();
            string result = string.Empty;
            if (others.Length == 0)
            {
                return minText;
            }

            for (int i = 0; i < minText.Length; i++)
            {
                char letter = minText[i];

                for (int index = 0; index < others.Length; index++)
                {
                    string str = others[index];
                    if (str[i] == letter)
                    {
                        if (index == others.Length - 1)
                        {
                            result = result + letter;
                        }
                        continue;
                    }

                    return result;
                }
            }

            return result;
        }

        public static void ExecutionTime()
        {
            string[] strs = ["flower", "flow", "flight"];

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string result1 = LongestCommonPrefix(strs);
            stopwatch.Stop();
            Console.WriteLine($"LongestCommonPrefix Result: {result1}");
            Console.WriteLine("Execution Time: " + stopwatch.Elapsed.TotalMilliseconds + " ms");
        }
    }
}
