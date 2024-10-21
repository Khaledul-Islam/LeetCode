using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _13_Roman_to_Integer
    {
        public static int RomanToInt(string s)
        {
            var dic = new Dictionary<string, int>();
            var dicDiff = new Dictionary<string, int>();
            dic.TryAdd("I", 1);
            dic.TryAdd("V", 5);
            dic.TryAdd("X", 10);
            dic.TryAdd("L", 50);
            dic.TryAdd("C", 100);
            dic.TryAdd("D", 500);
            dic.TryAdd("M", 1000);
            dicDiff.TryAdd("IV", 2);
            dicDiff.TryAdd("IX", 2);
            dicDiff.TryAdd("XL", 20);
            dicDiff.TryAdd("XC", 20);
            dicDiff.TryAdd("CD", 200);
            dicDiff.TryAdd("CM", 200);

            int totalSum = 0;

            for (int i = 0; i < s.Length; i++)
            {
                string key = s[i].ToString();
                dic.TryGetValue(key, out int value);
                totalSum += value;
            }

            foreach (var pair in dicDiff)
            {
                if (s.Contains(pair.Key))
                {
                    totalSum -= pair.Value;
                }
            }

            return totalSum;
        }

        public static void ExecutionTime()
        {
            string x = "MLCXVMCLXIVXXLMC";

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int result1 = RomanToInt(x);
            stopwatch.Stop();
            Console.WriteLine($"RomanToInt Result: {result1}");
            Console.WriteLine("Execution Time: " + stopwatch.Elapsed.TotalMilliseconds + " ms");
        }
    }
}
