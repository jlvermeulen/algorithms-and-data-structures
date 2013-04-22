using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] MergeSort(T[] input) { return MergeSort(input, 0, input.Length); }

        public static T[] MergeSort(T[] input, int start) { return MergeSort(input, start, input.Length - start); }

        public static T[] MergeSort(T[] input, int start, int length)
        {
            CheckArguments(input, start, length);

            T[] temp, output = new T[input.Length];
            int p1, p2, pLeft, pRight, pEnd, end = start + length;
            for (int i = start + 1; i < end; i *= 2)
            {
                for (int j = start; j < end; j += 2 * i)
                {
                    pLeft = p1 = j;
                    pRight = p2 = Math.Min(i + j, end);
                    pEnd = Math.Min(2 * i + j, end);

                    for (int k = p1; k < pEnd; k++)
                    {
                        if (p1 < pRight && (p2 >= pEnd || input[p1].CompareTo(input[p2]) < 0))
                            output[k] = input[p1++];
                        else
                            output[k] = input[p2++];
                    }
                }

                temp = input;
                input = output;
                output = temp;
            }
            return input;
        }
    }
}