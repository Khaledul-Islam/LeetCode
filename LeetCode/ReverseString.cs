using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class ReverseString
    {

        public static void Reverse_String(string input)
        {
            var result = string.Empty;

            for (int i = input.Length-1; i >-1; i--)
            {
                result = result + input[i];
            }
            Console.WriteLine(result);
        }
        public static void Reverse_String_2(string input)
        {
            var result = string.Empty;

            var inputArray = input.ToCharArray();
            Array.Reverse(inputArray);
            Console.WriteLine(string.Concat(inputArray));
        }
    }
}
