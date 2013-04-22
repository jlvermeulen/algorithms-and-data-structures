using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] CycleSort(T[] input) { return CycleSort(input, 0, input.Length); }

        public static T[] CycleSort(T[] input, int start) { return CycleSort(input, start, input.Length - start); }

        public static T[] CycleSort(T[] input, int start, int length)
        {
            CheckArguments(input, start, length);

            T item, temp;
            int pos;

            for (int i = start; i < start + length - 1; i++)
            {
                item = input[i];
                pos = i;

                for (int j = i + 1; j < input.Length; j++)
                    if (input[j].CompareTo(item) < 0)
                        pos++;

                if (pos == i)
                    continue;

                while (item.CompareTo(input[pos]) == 0)
                    pos++;

                temp = input[pos];
                input[pos] = item;
                item = temp;

                while (pos != i)
                {
                    pos = i;
                    for (int j = i + 1; j < input.Length; j++)
                        if (input[j].CompareTo(item) < 0)
                            pos++;

                    while (item.CompareTo(input[pos]) == 0)
                        pos++;

                    temp = input[pos];
                    input[pos] = item;
                    item = temp;
                }
            }

            return input;
        }
    }
}