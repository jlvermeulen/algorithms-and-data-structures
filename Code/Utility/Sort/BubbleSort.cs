using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] BubbleSort(T[] input)
        {
            T temp;
            int length = input.Length - 1;
            int newlength;

            while (length > 0)
            {
                newlength = 0;
                for (int i = 0; i < length; i++)
                {
                    if (input[i].CompareTo(input[i + 1]) > 0)
                    {
                        temp = input[i];
                        input[i] = input[i + 1];
                        input[i + 1] = temp;
                        newlength = i;
                    }
                }
                length = newlength;
            }

            return input;
        }
    }
}