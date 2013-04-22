using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] BinaryInsertionSort(T[] input) { return BinaryInsertionSort(input, 0, input.Length); }

        public static T[] BinaryInsertionSort(T[] input, int start) { return BinaryInsertionSort(input, start, input.Length - start); }

        public static T[] BinaryInsertionSort(T[] input, int start, int length)
        {
            CheckArguments(input, start, length);

            T current;
            int upper, lower, half = 0;

            if (length <= 1)
                return input;

            if (length > 1 && input[start].CompareTo(input[start + 1]) > 0)
            {
                current = input[start];
                input[start] = input[start + 1];
                input[start + 1] = current;
            }

            for (int i = start + 2; i < start + length; i++)
            {
                current = input[i];
                upper = i;
                lower = start - 1;
                while (upper > lower + 1)
                {
                    half = (upper + lower) / 2;
                    if (input[half].CompareTo(current) <= 0)
                        lower = half;
                    else
                        upper = half;
                }

                Buffer.BlockCopy(input, (lower + 1) * sizeof(int), input, (lower + 2) * sizeof(int), (i - lower - 1) * sizeof(int));
                input[lower + 1] = current;
            }

            return input;
        }
    }
}