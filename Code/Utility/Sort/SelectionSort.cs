using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] SelectionSort(T[] input)
        {
            int p;
            T temp;

            for (int i = 0; i < input.Length - 1; i++)
            {
                p = i;
                for (int j = i + 1; j < input.Length; j++)
                    if (input[j].CompareTo(input[p]) < 0)
                        p = j;

                temp = input[p];
                input[p] = input[i];
                input[i] = temp;
            }

            return input;
        }
    }
}