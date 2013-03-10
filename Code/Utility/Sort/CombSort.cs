using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] CombSort(T[] input)
        {
            int gap = input.Length;
            bool swapped = true;
            T temp;

            while (gap > 1 || swapped)
            {
                gap = Math.Max(1, (int)(gap / 1.3f));
                swapped = false;

                for (int i = 0; i < input.Length - gap; i++)
                {
                    if (input[i].CompareTo(input[i + gap]) > 0)
                    {
                        temp = input[i];
                        input[i] = input[i + gap];
                        input[i + gap] = temp;
                        swapped = true;
                    }
                }
            }

            return input;
        }
    }
}