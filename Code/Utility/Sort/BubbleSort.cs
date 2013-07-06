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
                public static T[] BubbleSort(T[] input) { return BubbleSort(input, 0, input.Length); }

                public static T[] BubbleSort(T[] input, int start) { return BubbleSort(input, start, input.Length - start); }

                public static T[] BubbleSort(T[] input, int start, int length)
                {
                    CheckArguments(input, start, length);

                    T temp;
                    int newEnd, end;

                    while (length > 1)
                    {
                        newEnd = 0;
                        end = start + length - 1;
                        for (int i = start; i < end; i++)
                        {
                            if (input[i].CompareTo(input[i + 1]) > 0)
                            {
                                temp = input[i];
                                input[i] = input[i + 1];
                                input[i + 1] = temp;
                                newEnd = i;
                            }
                        }
                        length = newEnd - start + 1;
                    }

                    return input;
                }
            }
        }
    }
}