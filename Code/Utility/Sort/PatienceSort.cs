using System;
using System.Collections.Generic;
using Utility.DataStructures.PriorityQueue;

namespace Utility
{
    namespace Algorithms
    {
        namespace Sort
        {
            public static partial class Sort<T>
                where T : IComparable<T>
            {
                public static T[] PatienceSort(T[] input) { return PatienceSort(input, 0, input.Length); }

                public static T[] PatienceSort(T[] input, int start) { return PatienceSort(input, start, input.Length - start); }

                public static T[] PatienceSort(T[] input, int start, int length)
                {
                    CheckArguments(input, start, length);

                    List<ComparableStack> stacks = new List<ComparableStack>();

                    for (int i = start; i < start + length; i++)
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
                    for (int i = start; i < start + length; i++)
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
    }
}