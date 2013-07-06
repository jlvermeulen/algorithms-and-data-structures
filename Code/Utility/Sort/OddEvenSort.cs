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
                public static T[] OddEvenSort(T[] input) { return OddEvenSort(input, 0, input.Length); }

                public static T[] OddEvenSort(T[] input, int start) { return OddEvenSort(input, start, input.Length - start); }

                public static T[] OddEvenSort(T[] input, int start, int length)
                {
                    CheckArguments(input, start, length);

                    bool swapped = true;
                    T temp;

                    while (swapped)
                    {
                        swapped = false;
                        for (int i = start + 1; i < start + length - 1; i += 2)
                        {
                            if (input[i].CompareTo(input[i + 1]) > 0)
                            {
                                swapped = true;
                                temp = input[i];
                                input[i] = input[i + 1];
                                input[i + 1] = temp;
                            }
                        }

                        for (int i = start; i < start + length - 1; i += 2)
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
    }
}