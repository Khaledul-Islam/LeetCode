using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static  class Example
    {
        public static string encrypass(string cleanString)
        {
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(cleanString);
            Byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);
            var hashedBytesss = hashedBytes.ToString();
            var bitString =  BitConverter.ToString(hashedBytes);
            return bitString;
        }

        public static string ReversePass(string bitString)
        {
            var orgHashByte = bitString
                .Split('-')
                .Select(hex => Convert.ToByte(hex, 16)) // Convert each hex to a byte
                .ToArray();
            return "";
        }
    }
}
