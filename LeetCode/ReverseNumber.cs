using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class ReverseNumber
    {
        public static void Reverse_Number(int number)
        {
            var arrayNum = number.ToString().ToCharArray();
            Array.Reverse(arrayNum);
            var result = Convert.ToInt32(string.Concat(arrayNum));
        }
    }
}
