using System.Diagnostics;

namespace LeetCode
{
    public static class _20_Valid_Parentheses
    {
        public static bool IsValid(string s)
        {
            if (s.Length <= 1) return false;
            var stack = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(' || s[i] == '{' || s[i] == '[')
                {
                    stack.Push(s[i]);
                }
                else
                {
                    if (stack.Any() && IsMatch(stack.Peek(), s[i]))
                    {
                        stack.Pop();
                        continue;
                    }

                    return false;
                }
            }

            return stack.Count == 0;
        }
        private static bool IsMatch(char a, char b)
        {
            switch (a)
            {
                case '(' when b == ')':
                case '{' when b == '}':
                case '[' when b == ']':
                    return true;
                default:
                    return false;
            }
        }

        public static void ExecutionTime()
        {
            string x = "({})[]([])({})[]([])({})[]([])({})[]([])({})[]([])({})[]([])({})[]([])({})[]([])({})[]([])({})[]([])({})[]([])({})[]([])";

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            bool result1 = IsValid(x);
            stopwatch.Stop();
            Console.WriteLine($"IsValid Result: {result1}");
            Console.WriteLine("Execution Time: " + stopwatch.Elapsed.TotalMilliseconds + " ms");
        }
    }

}
