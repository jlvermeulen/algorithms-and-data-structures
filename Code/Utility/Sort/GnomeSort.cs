using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] GnomeSort(T[] input)
        {
            int position = 1, previous = 0;
            T temp;

            while (position < input.Length)
            {
                if (input[position].CompareTo(input[position - 1]) >= 0)
                {
                    if (previous != 0)
                    {
                        position = previous;
                        previous = 0;
                    }
                    position++;
                }
                else
                {
                    temp = input[position];
                    input[position] = input[position - 1];
                    input[position - 1] = temp;
                    if (position > 1)
                    {
                        if (previous == 0)
                            previous = position;
                        position--;
                    }
                    else
                        position++;
                }
            }

            return input;
        }
    }
}