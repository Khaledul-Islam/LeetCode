using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _9_Palindrome_Number
    {
        public static bool IsPalindrome(int x)
        {
            return string.Concat(x.ToString().Reverse()) == x.ToString();
        }
        public static bool IsPalindrome2(int x)
        {
            char[] num = x.ToString().ToArray();
            if (x.ToString() == string.Join("", num.Reverse()))
            {
                return true;
            }

            return false;
        }
        public static bool IsPalindromeBeginner(int x)//log10
        {
            int result = 0;
            if (x < 0)
            {
                return false;
            }
            double length = Math.Floor(Math.Log10(x) + 1);
            int orgValue = x;
            for (int i = 0; i < length; i++)
            {
                int temp = x % 10;
                result = result*10 + temp;
                x /= 10;
            }
            return result == orgValue;
        }

        public static void ExecutionTime()
        {
            int x  = 123321;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            bool result1 = IsPalindrome(x);
            stopwatch.Stop();
            Console.WriteLine($"IsPalindrome (String Reverse) Result: {result1}");
            Console.WriteLine("Execution Time: " + stopwatch.Elapsed.TotalMilliseconds + " ms");

            stopwatch.Restart();
            bool result2 = IsPalindrome2(x);
            stopwatch.Stop();
            Console.WriteLine($"IsPalindrome (Char Reverse) Result: {result2}");
            Console.WriteLine("Execution Time: " + stopwatch.Elapsed.TotalMilliseconds + " ms");

            stopwatch.Restart();
            bool result3 = IsPalindromeBeginner(x);
            stopwatch.Stop();
            Console.WriteLine($"IsPalindrome (Beginner) Result: {result3}");
            Console.WriteLine("Execution Time: " + stopwatch.Elapsed.TotalMilliseconds + " ms");

        }
    }
}
