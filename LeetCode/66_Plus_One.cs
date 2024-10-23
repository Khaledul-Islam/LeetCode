using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _66_Plus_One
    {
        public static int[] PlusOne(int[] digits)
        {
            var digit = string.Concat(digits);
            var plusOne = (BigInteger.Parse(digit) + 1).ToString();
            return plusOne.Select(a=>Convert.ToInt32(a.ToString())).ToArray();
        }
    }
}
