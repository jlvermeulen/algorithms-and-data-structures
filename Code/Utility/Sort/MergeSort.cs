using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] MergeSort(T[] input)
        {
            return MergeSort(input, new T[input.Length]);
        }

        private static T[] MergeSort(T[] input, T[] output)
        {
            T[] temp;
            int p1, p2, pLeft, pRight, pEnd;
            for (int i = 1; i < input.Length; i *= 2)
            {
                for (int j = 0; j < input.Length; j += 2 * i)
                {
                    pLeft = p1 = j;
                    pRight = p2 = Math.Min(i + j, input.Length);
                    pEnd = Math.Min(2 * i + j, input.Length);

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