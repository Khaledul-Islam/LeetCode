using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public static class _21_Merge_Two_Sorted_Lists
    {
        public static LinkedListNode<int> MergeTwoLists(LinkedListNode<int> list1, LinkedListNode<int> list2)
        {
            if (list1.List is { Count: 0 })
            {
                return list2;
            }
            if (list2.List is { Count: 0 })
            {
                return list1;
            }

            var result = new LinkedList<int>();

            for (int i = 0; i < list1.List.Count; i++)
            {
                for (int j = i; j < list2.List.Count; j++)
                {
                    result.AddLast(list1.Next);
                }
            }

            return null;
        }
    }
}
