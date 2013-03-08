using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static void InsertionSort(T[] input)
        {
            T current;
            int j;
            for (int i = 1; i < input.Length; i++)
            {
                current = input[i];
                for (j = i; j > 0; j--)
                {
                    if (input[j - 1].CompareTo(current) > 0)
                        input[j] = input[j - 1];
                    else
                        break;
                }
                input[j] = current;
            }
        }
    }
}