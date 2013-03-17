using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] BinaryInsertionSort(T[] input)
        {
            T current;
            int upper, lower, half = 0;

            if (input.Length > 1 && input[0].CompareTo(input[1]) > 0)
            {
                current = input[0];
                input[0] = input[1];
                input[1] = current;
            }

            for (int i = 2; i < input.Length; i++)
            {
                current = input[i];
                lower = 0;
                upper = i - 1;
                while (upper >= lower)
                {
                    half = (upper - lower) / 2 + lower;
                    if (current.CompareTo(input[half]) < 0)
                        upper = half - 1;
                    else
                        lower = ++half;
                }

                Buffer.BlockCopy(input, half * sizeof(int), input, (half + 1) * sizeof(int), (i - half) * sizeof(int));
                input[half] = current;
            }

            return input;
        }
    }
}