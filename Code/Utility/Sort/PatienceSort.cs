using System;
using System.Collections.Generic;
using Utility;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] PatienceSort(T[] input)
        {
            List<ComparableStack> stacks = new List<ComparableStack>();

            for (int i = 0; i < input.Length; i++)
            {
                ComparableStack stack = new ComparableStack();
                stack.Push(input[i]);
                int index = stacks.BinarySearch(stack);
                if (index < 0)
                    index = ~index;
                if (index != stacks.Count)
                    stacks[index].Push(input[i]);
                else
                    stacks.Add(stack);
            }

            DMinHeap<ComparableStack> heap = new DMinHeap<ComparableStack>(stacks, 3);
            for (int i = 0; i < input.Length; i++)
            {
                ComparableStack stack = heap.Extract();
                input[i] = stack.Pop();
                if (stack.Count != 0)
                    heap.Add(stack);
            }

            return input;
        }

        private class ComparableStack : Stack<T>, IComparable<ComparableStack>
        {
            public int CompareTo(ComparableStack other)
            {
                return this.Peek().CompareTo(other.Peek());
            }
        }
    }
}