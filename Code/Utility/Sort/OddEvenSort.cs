using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] OddEvenSort(T[] input)
        {
            bool swapped = true;
            T temp;

            while (swapped)
            {
                swapped = false;
                for (int i = 1; i < input.Length - 1; i += 2)
                {
                    if (input[i].CompareTo(input[i + 1]) > 0)
                    {
                        swapped = true;
                        temp = input[i];
                        input[i] = input[i + 1];
                        input[i + 1] = temp;
                    }
                }

                for (int i = 0; i < input.Length - 1; i += 2)
                {
                    if (input[i].CompareTo(input[i + 1]) > 0)
                    {
                        swapped = true;
                        temp = input[i];
                        input[i] = input[i + 1];
                        input[i + 1] = temp;
                    }
                }
            }

            return input;
        }
    }
}