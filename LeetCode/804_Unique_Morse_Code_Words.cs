using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _804_Unique_Morse_Code_Words
    {
        public static int UniqueMorseRepresentations(string[] words)
        {
            var hashSet = new HashSet<string>();
            var morseCodeDict = new Dictionary<string, string>
            {
                { "A", ".-" },    { "B", "-..." },  { "C", "-.-." },  { "D", "-.." },   { "E", "." },
                { "F", "..-." },  { "G", "--." },   { "H", "...." },  { "I", ".." },    { "J", ".---" },
                { "K", "-.-" },   { "L", ".-.." },  { "M", "--" },    { "N", "-." },    { "O", "---" },
                { "P", ".--." },  { "Q", "--.-" },  { "R", ".-." },   { "S", "..." },   { "T", "-" },
                { "U", "..-" },   { "V", "...-" },  { "W", ".--" },   { "X", "-..-" },  { "Y", "-.--" },
                { "Z", "--.." }
            };

            foreach (var w in words)
            {
                var mc = new StringBuilder();
                foreach (var a in w)
                {
                    morseCodeDict.TryGetValue(a.ToString().ToUpper(), out var code);
                    mc.Append(code);
                }

                hashSet.Add(mc.ToString());
            }
            return hashSet.Count;

        }

        public static int UniqueMorseRepresentations2(string[] words)
        {
            var morseCodeArray = new string[]
            {
                ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---",
                "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-",
                "..-", "...-", ".--", "-..-", "-.--", "--.."
            };

            var hashSet = new HashSet<string>();

            foreach (var word in words)
            {
                var mr = new StringBuilder();
                foreach (var ch in word)
                {
                    mr.Append(morseCodeArray[ch - 'a']);
                }

                hashSet.Add(mr.ToString());
            }

            return hashSet.Count;
        }

    }
}
