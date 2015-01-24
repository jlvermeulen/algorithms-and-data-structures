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

                    List<Stack<T>> stacks = new List<Stack<T>>();

                    for (int i = start; i < start + length; i++)
                    {
                        Stack<T> stack = new Stack<T>();
                        stack.Push(input[i]);
                        int index = stacks.BinarySearch(stack);
                        if (index < 0)
                            index = ~index;
                        if (index != stacks.Count)
                            stacks[index].Push(input[i]);
                        else
                            stacks.Add(stack);
                    }

                    DMinHeap<T, Stack<T>> heap = new DMinHeap<T, Stack<T>>(3);
                    foreach (Stack<T> s in stacks)
                        heap.Add(s, s.Peek());

                    for (int i = start; i < start + length; i++)
                    {
                        Stack<T> stack = heap.Extract();
                        input[i] = stack.Pop();
                        if (stack.Count != 0)
                            heap.Add(stack, stack.Peek());
                    }

                    return input;
                }
            }
        }
    }
}