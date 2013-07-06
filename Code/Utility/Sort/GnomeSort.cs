using System;

namespace Utility
{
    namespace Algorithms
    {
        namespace Sort
        {
            public static partial class Sort<T>
                where T : IComparable<T>
            {
                public static T[] GnomeSort(T[] input) { return GnomeSort(input, 0, input.Length); }

                public static T[] GnomeSort(T[] input, int start) { return GnomeSort(input, start, input.Length - start); }

                public static T[] GnomeSort(T[] input, int start, int length)
                {
                    CheckArguments(input, start, length);

                    int position = start + 1, previous = start;
                    T temp;

                    while (position < start + length)
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
    }
}