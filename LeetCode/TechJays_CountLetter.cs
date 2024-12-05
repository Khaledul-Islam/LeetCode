using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class TechJays_CountLetter
    {
        public static void CountLetter()
        {
            var word = "Hello World";
            word = string.Concat(word.ToLower().Where(a=>!char.IsWhiteSpace(a)));
            var dic = new Dictionary<char, int>();

            for(int i=0; i<word.Length; i++)
            {
                if (!dic.ContainsKey(word[i]))
                {
                    dic.Add(word[i], 1);
                    continue;
                }
                dic[word[i]]++;
            }
        }
    }
}
