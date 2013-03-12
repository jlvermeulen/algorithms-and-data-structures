using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] CombInsertionSort(T[] input)
        {
            int gap = input.Length;
            T temp;

            while (gap > 1)
            {
                gap = Math.Max(1, (int)(gap / 1.3f));

                for (int i = 0; i < input.Length - gap; i++)
                {
                    if (input[i].CompareTo(input[i + gap]) > 0)
                    {
                        temp = input[i];
                        input[i] = input[i + gap];
                        input[i + gap] = temp;
                    }
                }
            }

            return InsertionSort(input);
        }
    }
}