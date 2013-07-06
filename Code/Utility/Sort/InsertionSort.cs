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
                public static T[] InsertionSort(T[] input) { return InsertionSort(input, 0, input.Length); }

                public static T[] InsertionSort(T[] input, int start) { return InsertionSort(input, start, input.Length - start); }

                public static T[] InsertionSort(T[] input, int start, int length)
                {
                    CheckArguments(input, start, length);

                    T current;
                    int j;
                    for (int i = start + 1; i < start + length; i++)
                    {
                        current = input[i];
                        for (j = i; j > start; j--)
                        {
                            if (input[j - 1].CompareTo(current) <= 0)
                                break;
                        }
                        Buffer.BlockCopy(input, j * sizeof(int), input, (j + 1) * sizeof(int), (i - j) * sizeof(int));
                        input[j] = current;
                    }
                    return input;
                }
            }
        }
    }
}