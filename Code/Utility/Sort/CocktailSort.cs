using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] CocktailSort(T[] input)
        {
            T temp;
            int length = input.Length - 1;
            int newlength, i = 0, j = input.Length - 1;

            while (length > 0)
            {
                newlength = 0;
                for (int k = i; k < j; k++)
                {
                    if (input[k].CompareTo(input[k + 1]) > 0)
                    {
                        temp = input[k];
                        input[k] = input[k + 1];
                        input[k + 1] = temp;
                        newlength = k - i;
                    }
                }
                length = newlength;
                if (length == 0)
                    break;
                j = i + length;
                for (int l = j; l > 0; l--)
                {
                    if (input[l].CompareTo(input[l - 1]) < 0)
                    {
                        temp = input[l];
                        input[l] = input[l - 1];
                        input[l - 1] = temp;
                        newlength = j - l - 1;
                    }
                }
                length = newlength;
                i = j - length;
            }

            return input;
        }
    }
}