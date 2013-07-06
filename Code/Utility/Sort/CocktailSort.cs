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
                public static T[] CocktailSort(T[] input) { return CocktailSort(input, 0, input.Length); }

                public static T[] CocktailSort(T[] input, int start) { return CocktailSort(input, start, input.Length - start); }

                public static T[] CocktailSort(T[] input, int start, int length)
                {
                    CheckArguments(input, start, length);

                    T temp;
                    int newLength, i = start, j = start + length - 1;

                    while (length > 1)
                    {
                        newLength = 0;
                        for (int k = i; k < j; k++)
                        {
                            if (input[k].CompareTo(input[k + 1]) > 0)
                            {
                                temp = input[k];
                                input[k] = input[k + 1];
                                input[k + 1] = temp;
                                newLength = k - i;
                            }
                        }
                        length = newLength;
                        if (length == 1)
                            break;
                        j = i + length;
                        for (int l = j; l > 0; l--)
                        {
                            if (input[l].CompareTo(input[l - 1]) < 0)
                            {
                                temp = input[l];
                                input[l] = input[l - 1];
                                input[l - 1] = temp;
                                newLength = j - l - 1;
                            }
                        }
                        length = newLength;
                        i = j - length;
                    }

                    return input;
                }
            }
        }
    }
}