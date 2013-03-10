using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        private static readonly int[] gaps = { 701, 301, 132, 57, 23, 10, 4, 1 };

        public static T[] ShellSort(T[] input)
        {
            int gap, k;
            T current;

            for (int i = 0; i < 8; i++)
            {
                gap = gaps[i];
                for (int j = gap; j < input.Length; j++)
                {
                    current = input[j];
                    for (k = j; k >= gap; k -= gap)
                    {
                        if (input[k - gap].CompareTo(current) > 0)
                            input[k] = input[k - gap];
                        else
                            break;
                    }
                    input[k] = current;
                }
            }

            return input;
        }
    }
}