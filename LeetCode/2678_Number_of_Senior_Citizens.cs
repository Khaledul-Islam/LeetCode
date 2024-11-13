using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _2678_Number_of_Senior_Citizens
    {
        public static int CountSeniors(string[] details)
        {
            int count = 0;
            foreach (var detail in details)
            {
                var regex = new Regex(@"[M|F](\d{2})");
                var match = regex.Match(detail);
                if (match.Success)
                {
                    if (Convert.ToInt32(match.Groups[1].Value) > 60)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        public static int CountSeniors2(string[] details)
        {
            int count = 0;
            foreach (var detail in details)
            {
                var str = detail.Substring(11, 2);
                if (Convert.ToInt32(str) > 60)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
