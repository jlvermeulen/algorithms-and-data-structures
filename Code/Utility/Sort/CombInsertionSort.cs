using System;

namespace Utility
{
    namespace Algorithms
    {
        namespace Sort
        {
            public static partial class Sort<T>
                where T : IComparable<T>
            {
                public static T[] CombInsertionSort(T[] input) { return CombInsertionSort(input, 0, input.Length); }

                public static T[] CombInsertionSort(T[] input, int start) { return CombInsertionSort(input, start, input.Length - start); }

                public static T[] CombInsertionSort(T[] input, int start, int length)
                {
                    CheckArguments(input, start, length);

                    int gap = length;
                    T temp;

                    while (gap > 1)
                    {
                        gap = Math.Max(1, (int)(gap / 1.3f));

                        for (int i = start; i < start + length - gap; i++)
                        {
                            if (input[i].CompareTo(input[i + gap]) > 0)
                            {
                                temp = input[i];
                                input[i] = input[i + gap];
                                input[i + gap] = temp;
                            }
                        }
                    }

                    return InsertionSort(input, start, length);
                }
            }
        }
    }
}