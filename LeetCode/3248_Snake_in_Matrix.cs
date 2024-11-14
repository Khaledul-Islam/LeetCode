using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _3248_Snake_in_Matrix
    {
        public static int FinalPositionOfSnake(int n, IList<string> commands)
        {
            int res = 0;

            foreach (var command in commands)
            {
                var first = command[0];
                switch (first)
                {
                    case 'D':
                        res += n;
                        break;
                    case 'R':
                        res++;
                        break;
                    case 'L':
                        res--;
                        break;
                    case 'U':
                        res -= n;
                        break;
                }
            }

            return res;
        }
    }
}
